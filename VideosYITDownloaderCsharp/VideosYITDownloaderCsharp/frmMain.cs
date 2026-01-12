using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideosYITDownloaderCsharp.Licensing;
using VideosYITDownloaderCsharp.Services;
using VideosYITDownloaderCsharp.Utils;

namespace VideosYITDownloaderCsharp
{
    public partial class frmMain : Form
    {
        private readonly LicenseInfo _license;
        private readonly ILogger _logger;
        private readonly YtDlpService _ytDlp;
        private CancellationTokenSource _cts;

        public frmMain(LicenseInfo license, ILogger logger)
        {
            _license = license;
            _logger = logger;
            _ytDlp = new YtDlpService(_logger);
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            string status;
            if (_license.IsFullLicense || _license.IsDeveloperLicense)
            {
                status = _license.IsDeveloperLicense ? "Licença: Developer" : "Licença: Ativada";
            }
            else if (_license.IsTrialActive(out var daysLeft))
            {
                status = $"Trial ativo - dias restantes: {daysLeft}";
            }
            else
            {
                status = "Licença inválida ou trial expirado";
            }

            lblLicenseStatus.Text = status;
            AppendLog(status);

            cboQuality.Items.Clear();
            cboQuality.Items.AddRange(new object[] { "best", "1080p", "720p", "480p", "360p" });
            cboQuality.SelectedIndex = 0;
        }

        private void btnPasta_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtOutputFolder.Text = dlg.SelectedPath;
                }
            }
        }

        private async void btnVideo_Click(object sender, EventArgs e)
        {
            await StartQueue(DownloadFormat.VideoOnlyMp4);
        }

        private async void btnAudio_Click(object sender, EventArgs e)
        {
            await StartQueue(DownloadFormat.Mp3);
        }

        private async void btnVideoAudio_Click(object sender, EventArgs e)
        {
            await StartQueue(DownloadFormat.Mp4Full);
        }

        private async void btnPlayList_Click(object sender, EventArgs e)
        {
            await StartQueue(DownloadFormat.PlaylistMp4);
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            listLog.Items.Clear();
        }

        private async Task StartQueue(DownloadFormat format)
        {
            var urls = ParseUrls();
            if (urls.Count == 0)
            {
                MessageBox.Show("Informe pelo menos uma URL (uma por linha).");
                return;
            }

            string baseOutput = string.IsNullOrWhiteSpace(txtOutputFolder.Text)
                ? AppDomain.CurrentDomain.BaseDirectory
                : txtOutputFolder.Text.Trim();

            string targetDir = format == DownloadFormat.Mp3
                ? System.IO.Path.Combine(baseOutput, "Downloads", "mp3")
                : System.IO.Path.Combine(baseOutput, "Downloads", "mp4");

            _cts?.Cancel();
            _cts = new CancellationTokenSource();

            btnToggle(false);
            progressBar1.Value = 0;
            lblProgress.Text = "0%";

            var quality = cboQuality.SelectedItem?.ToString() ?? "best";
            int success = 0, fail = 0;
            string lastError = null;

            foreach (var url in urls)
            {
                AppendLog($"Iniciando: {url}");
                var (ok, err) = await StartDownload(format, quality, url, targetDir);
                if (ok) success++;
                else
                {
                    fail++;
                    if (!string.IsNullOrWhiteSpace(err))
                        lastError = err;
                }

                if (_cts.IsCancellationRequested) break;
            }

            btnToggle(true);

            var resumo = $"Fila concluída.\nSucesso: {success}\nFalha: {fail}";
            if (fail > 0 && !string.IsNullOrWhiteSpace(lastError))
                resumo += $"\nÚltimo erro: {lastError}";
            MessageBox.Show(resumo, "Resumo");
            AppendLog(resumo);
        }

        private async Task<(bool ok, string error)> StartDownload(DownloadFormat format, string quality, string url, string targetDir)
        {
            progressBar1.Value = 0;
            lblProgress.Text = "0%";

            var request = new DownloadRequest
            {
                Url = url,
                Format = format,
                OutputDirectory = targetDir,
                Quality = quality
            };

            string lastError = null;

            var progress = new Progress<DownloadProgressInfo>(info =>
            {
                if (info.Percent.HasValue)
                {
                    var val = Math.Min(100, Math.Max(0, (int)Math.Round(info.Percent.Value)));
                    progressBar1.Value = val;
                    lblProgress.Text = $"{val}%";
                }
                if (!string.IsNullOrWhiteSpace(info.StatusLine))
                {
                    AppendLog(info.StatusLine);
                }
            });

            var result = await _ytDlp.DownloadAsync(request, progress, _cts.Token);

            if (result.Success)
            {
                AppendLog("Download concluído.");
                return (true, null);
            }
            else
            {
                lastError = result.Message;
                AppendLog("Falhou: " + result.Message);
                if (!string.IsNullOrWhiteSpace(result.StdErr))
                {
                    AppendLog("stderr: " + result.StdErr);
                    // Se o stderr tiver mensagem mais clara, usamos no resumo
                    lastError = result.StdErr;
                }
                return (false, lastError);
            }
        }

        private List<string> ParseUrls()
        {
            return textBox1.Text
                .Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(u => u.Trim())
                .Where(u => !string.IsNullOrWhiteSpace(u))
                .Distinct()
                .ToList();
        }

        private void btnToggle(bool enabled)
        {
            btnVideo.Enabled = enabled;
            btnVideoAudio.Enabled = enabled;
            btnAudio.Enabled = enabled;
            btnPlayList.Enabled = enabled;
            btnPasta.Enabled = enabled;
            btnClearLog.Enabled = enabled;
        }

        private void AppendLog(string message)
        {
            var line = $"[{DateTime.Now:HH:mm:ss}] {message}";
            listLog.Items.Add(line);
            listLog.TopIndex = listLog.Items.Count - 1;
            _logger.Info(message);
        }
    }
}