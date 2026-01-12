using System;
using System.Windows.Forms;
using VideosYITDownloaderCsharp.Licensing;

namespace VideosYITDownloaderCsharp
{
    public partial class frmMain : Form
    {
        private readonly LicenseInfo _license;

        // Construtor que recebe a licença
        public frmMain(LicenseInfo license)
        {
            _license = license;
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
            listLog.Items.Add($"[{DateTime.Now:HH:mm:ss}] {status}");
        }
    }
}