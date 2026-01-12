using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using VideosYITDownloaderCsharp.Licensing;
using VideosYITDownloaderCsharp.Utils;

namespace VideosYITDownloaderCsharp.Services
{
    internal enum DownloadFormat
    {
        VideoOnlyMp4, // Botão “Video”
        Mp3,          // Botão “Audio”
        Mp4Full,      // Botão “Video + Audio”
        PlaylistMp4   // Botão “PlayList”
    }

    internal class DownloadRequest
    {
        public string Url { get; set; } = "";
        public DownloadFormat Format { get; set; }
        public string OutputDirectory { get; set; } = "";
        /// <summary>Ex.: "best", "1080p", "720p", "480p", "360p".</summary>
        public string Quality { get; set; } = "best";
    }

    internal class DownloadResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public string OutputPath { get; set; } = "";
        public int ExitCode { get; set; }
        public string StdOut { get; set; } = "";
        public string StdErr { get; set; } = "";
    }

    internal class DownloadProgressInfo
    {
        public double? Percent { get; set; }
        public string StatusLine { get; set; } = "";
    }

    internal class YtDlpService
    {
        private readonly string _toolsDir;
        private readonly string _ytDlpPath;
        private readonly string _ffmpegPath;
        private readonly string _ffprobePath;
        private readonly ILogger _logger;

        private static readonly Regex ProgressRegex = new Regex(@"\[download\]\s+(\d+(?:\.\d+)?)%\s", RegexOptions.Compiled);

        public YtDlpService(ILogger logger)
        {
            _logger = logger;
            _toolsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tools");
            _ytDlpPath = Path.Combine(_toolsDir, "yt-dlp.exe");
            _ffmpegPath = Path.Combine(_toolsDir, "ffmpeg.exe");
            _ffprobePath = Path.Combine(_toolsDir, "ffprobe.exe");
        }

        public async Task<DownloadResult> DownloadAsync(
            DownloadRequest request,
            IProgress<DownloadProgressInfo> progress,
            CancellationToken cancellationToken)
        {
            if (!LicenseManager.CanDownloadNow(out var reason))
                return new DownloadResult { Success = false, Message = reason };

            if (!File.Exists(_ytDlpPath))
                return new DownloadResult { Success = false, Message = "yt-dlp.exe não encontrado em Tools." };
            if (!File.Exists(_ffmpegPath))
                return new DownloadResult { Success = false, Message = "ffmpeg.exe não encontrado em Tools." };
            if (!File.Exists(_ffprobePath))
                return new DownloadResult { Success = false, Message = "ffprobe.exe não encontrado em Tools." };

            try
            {
                Directory.CreateDirectory(request.OutputDirectory);

                var stdout = new List<string>();
                var stderr = new List<string>();

                string args = BuildArguments(request, request.OutputDirectory);
                _logger.Info($"CMD: {_ytDlpPath} {args}");
                progress?.Report(new DownloadProgressInfo { StatusLine = "Iniciando..." });

                int exitCode = await ProcessRunner.RunAsync(
                    fileName: _ytDlpPath,
                    arguments: args,
                    workingDirectory: _toolsDir,
                    onOutput: line =>
                    {
                        stdout.Add(line);
                        HandleOutput(line, progress);
                    },
                    onError: line =>
                    {
                        stderr.Add(line);
                        _logger.Warn(line);
                        progress?.Report(new DownloadProgressInfo { StatusLine = line });
                    },
                    cancellationToken: cancellationToken);

                if (exitCode == 0)
                {
                    if (request.Format == DownloadFormat.VideoOnlyMp4)
                    {
                        var dest = TryGetLastDestination(stdout);
                        if (!string.IsNullOrWhiteSpace(dest) && File.Exists(dest))
                        {
                            StripAudio(dest);
                        }
                    }

                    LicenseManager.MarkDownloadDone();
                    progress?.Report(new DownloadProgressInfo { Percent = 100, StatusLine = "Concluído" });
                    return new DownloadResult
                    {
                        Success = true,
                        Message = "Download concluído.",
                        OutputPath = request.OutputDirectory,
                        ExitCode = exitCode,
                        StdOut = string.Join(Environment.NewLine, stdout),
                        StdErr = string.Join(Environment.NewLine, stderr)
                    };
                }

                var msg = $"yt-dlp retornou código {exitCode}";
                if (stderr.Count > 0)
                    msg += " | stderr: " + string.Join(" | ", stderr);

                return new DownloadResult
                {
                    Success = false,
                    Message = msg,
                    ExitCode = exitCode,
                    StdOut = string.Join(Environment.NewLine, stdout),
                    StdErr = string.Join(Environment.NewLine, stderr)
                };
            }
            catch (OperationCanceledException)
            {
                return new DownloadResult { Success = false, Message = "Operação cancelada." };
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                return new DownloadResult { Success = false, Message = "Erro: " + ex.Message };
            }
        }

        private string BuildArguments(DownloadRequest req, string outputDir)
        {
            string outputTemplate = req.Format == DownloadFormat.Mp3
                ? Path.Combine(outputDir, "%(title)s.mp3")
                : Path.Combine(outputDir, "%(title)s.mp4");

            var args = $"--ffmpeg-location \"{_ffmpegPath}\" -o \"{outputTemplate}\" --restrict-filenames --newline --no-part --no-cache-dir";

            if (req.Format == DownloadFormat.PlaylistMp4)
                args += " --yes-playlist";
            else
                args += " --no-playlist";

            switch (req.Format)
            {
                case DownloadFormat.VideoOnlyMp4:
                    args += " --extractor-args \"youtube:player_client=android\"";
                    args += $" -S vcodec:avc1,res,codec:mp4 -f \"{BuildVideoOnlyFormat(req.Quality)}\" --merge-output-format mp4 --recode-video mp4";
                    break;

                case DownloadFormat.Mp3:
                    args += " --extractor-args \"youtube:player_client=android\"";
                    args += " -f bestaudio/best --extract-audio --audio-format mp3 --audio-quality 0";
                    break;

                case DownloadFormat.Mp4Full:
                    args += " --extractor-args \"youtube:player_client=android\"";
                    args += $" -S vcodec:avc1,res,codec:mp4 -f \"{BuildVideoAudioFormat(req.Quality)}\" --merge-output-format mp4 --recode-video mp4";
                    break;

                case DownloadFormat.PlaylistMp4:
                    args += " --extractor-args \"youtube:player_client=android\"";
                    args += $" -S vcodec:avc1,res,codec:mp4 -f \"{BuildVideoAudioFormat(req.Quality)}\" --merge-output-format mp4 --recode-video mp4";
                    break;
            }

            args += $" \"{req.Url}\"";
            return args;
        }

        private string BuildVideoOnlyFormat(string quality)
        {
            // Prioriza vídeo avc1/mp4, respeitando altura se fornecida (ex.: 1080p -> height<=1080)
            var heightClause = TryParseHeight(quality) is int h ? $"[height<={h}]" : "";
            return $"bv*[vcodec^=avc1][ext=mp4]{heightClause}/bv*[ext=mp4]{heightClause}/bestvideo";
        }

        private string BuildVideoAudioFormat(string quality)
        {
            // Vídeo + áudio, priorizando avc1 + m4a, respeitando altura quando informada
            var heightClause = TryParseHeight(quality) is int h ? $"[height<={h}]" : "";
            return $"bv*[vcodec^=avc1][ext=mp4]{heightClause}+ba[ext=m4a]/bv*{heightClause}+ba/best[ext=mp4]/best";
        }

        private int? TryParseHeight(string quality)
        {
            if (string.IsNullOrWhiteSpace(quality)) return null;
            if (quality.Equals("best", StringComparison.OrdinalIgnoreCase)) return null;
            if (quality.EndsWith("p", StringComparison.OrdinalIgnoreCase))
            {
                var num = quality.Substring(0, quality.Length - 1);
                if (int.TryParse(num, out var h)) return h;
            }
            return null;
        }

        private void HandleOutput(string line, IProgress<DownloadProgressInfo> progress)
        {
            if (string.IsNullOrWhiteSpace(line)) return;

            var match = ProgressRegex.Match(line);
            if (match.Success && double.TryParse(match.Groups[1].Value, out var p))
            {
                progress?.Report(new DownloadProgressInfo { Percent = p, StatusLine = line.Trim() });
            }
            else
            {
                progress?.Report(new DownloadProgressInfo { StatusLine = line.Trim() });
            }
        }

        private string TryGetLastDestination(List<string> stdoutLines)
        {
            for (int i = stdoutLines.Count - 1; i >= 0; i--)
            {
                var line = stdoutLines[i];
                var idx = line.IndexOf("Destination:", StringComparison.OrdinalIgnoreCase);
                if (idx >= 0)
                {
                    var path = line.Substring(idx + "Destination:".Length).Trim();
                    path = path.Trim('"');
                    return path;
                }
            }
            return null;
        }

        private void StripAudio(string filePath)
        {
            try
            {
                var dir = Path.GetDirectoryName(filePath) ?? AppDomain.CurrentDomain.BaseDirectory;
                var tmp = Path.Combine(dir, Path.GetFileNameWithoutExtension(filePath) + ".noaudio.tmp.mp4");

                var psi = $" -y -i \"{filePath}\" -c:v copy -an \"{tmp}\"";
                _logger.Info($"ffmpeg strip audio: {_ffmpegPath} {psi}");

                var exit = ProcessRunner.RunAsync(
                    fileName: _ffmpegPath,
                    arguments: psi,
                    workingDirectory: dir,
                    onOutput: _ => { },
                    onError: _ => { },
                    cancellationToken: CancellationToken.None).GetAwaiter().GetResult();

                if (exit == 0 && File.Exists(tmp))
                {
                    File.Delete(filePath);
                    File.Move(tmp, filePath);
                }
                else if (File.Exists(tmp))
                {
                    File.Delete(tmp);
                }
            }
            catch (Exception ex)
            {
                _logger.Warn("StripAudio falhou: " + ex.Message);
            }
        }
    }
}