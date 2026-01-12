using System;
using System.Windows.Forms;

namespace VideosYITDownloaderCsharp.Licensing
{
    public partial class LicenseForm : Form
    {
        private readonly string _hwKey;

        public LicenseForm(string hwKey)
        {
            _hwKey = hwKey;
            InitializeComponent();
            txtKey.Text = _hwKey;
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            var key = txtKey.Text.Trim();
            var lic = txtLicense.Text.Trim();

            if (LicenseManager.TryActivate(key, lic, out var msg))
            {
                lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
                lblStatus.Text = msg;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                lblStatus.ForeColor = System.Drawing.Color.DarkRed;
                lblStatus.Text = msg;
            }
        }

        private void btnTrial_Click(object sender, EventArgs e)
        {
            LicenseManager.StartTrial(_hwKey);
            lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
            lblStatus.Text = "Trial iniciado (30 dias).";
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}