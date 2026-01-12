using System;
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
        }

        private async void btnVideo_Click(object sender, EventArgs e)
        {
            await StartDownload(DownloadFormat.VideoOnlyMp4, quality: "bestvideo[ext=mp4]/bestvideo", isPlaylist: false);
        }

        private async void btnAudio_Click(object sender, EventArgs e)
        {
            await StartDownload(DownloadFormat.Mp3, quality: "bestaudio", isPlaylist: false);
        }

        private async void btnVideoAudio_Click(object sender, EventArgs e)
        {
            await StartDownload(DownloadFormat.Mp4Full, quality: "bestvideo+bestaudio/best", isPlaylist: false);
        }

        private async void btnPlayList_Click(object sender, EventArgs e)
        {
            await StartDownload(DownloadFormat.PlaylistMp4, quality: "bestvideo+bestaudio/best", isPlaylist: true);
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

        private async Task StartDownload(DownloadFormat format, string quality, bool isPlaylist)
        {
            var url = textBox1.Text.Trim();
            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("Informe a URL.");
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

            progressBar1.Value = 0;
            lblProgress.Text = "0%";

            var request = new DownloadRequest
            {
                Url = url,
                Format = format,
                OutputDirectory = targetDir,
                Quality = quality
            };

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

            btnToggle(false);
            var result = await _ytDlp.DownloadAsync(request, progress, _cts.Token);
            btnToggle(true);

            if (result.Success)
            {
                AppendLog("Download concluído.");
                MessageBox.Show("Download concluído.");
            }
            else
            {
                AppendLog("Falhou: " + result.Message);
                if (!string.IsNullOrWhiteSpace(result.StdErr))
                    AppendLog("stderr: " + result.StdErr);
                MessageBox.Show("Falhou: " + result.Message);
            }
        }

        private void btnToggle(bool enabled)
        {
            btnVideo.Enabled = enabled;
            btnVideoAudio.Enabled = enabled;
            btnAudio.Enabled = enabled;
            btnPlayList.Enabled = enabled;
            btnPasta.Enabled = enabled;
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