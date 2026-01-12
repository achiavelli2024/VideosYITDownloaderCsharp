using System;
using System.Drawing;
using System.Windows.Forms;

namespace VideosYITDownloaderCsharp.Licensing
{
    internal class LicenseForm : Form
    {
        private readonly string _hwKey;
        private readonly TextBox txtKey;
        private readonly TextBox txtLicense;
        private readonly Label lblStatus;

        public LicenseForm(string hwKey)
        {
            _hwKey = hwKey;
            Text = "Ativação - VideosYITDownloader";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(480, 280);

            var lblKey = new Label { Text = "KeyCode (hardware):", AutoSize = true, Top = 20, Left = 20 };
            var lblLic = new Label { Text = "Licença (10 dígitos ou 123456789@):", AutoSize = true, Top = 80, Left = 20 };

            txtKey = new TextBox { Left = 20, Top = 40, Width = 430, ReadOnly = true, Text = _hwKey };
            txtLicense = new TextBox { Left = 20, Top = 100, Width = 430 };

            var btnActivate = new Button { Text = "Ativar licença", Left = 20, Top = 140, Width = 140 };
            btnActivate.Click += (_, __) => ActivateLicense();

            var btnTrial = new Button { Text = "Iniciar Trial (30 dias)", Left = 170, Top = 140, Width = 170 };
            btnTrial.Click += (_, __) => StartTrial();

            var btnClose = new Button { Text = "Fechar", Left = 350, Top = 140, Width = 100 };
            btnClose.Click += (_, __) => DialogResult = DialogResult.Cancel;

            lblStatus = new Label
            {
                AutoSize = true,
                ForeColor = Color.DarkRed,
                Left = 20,
                Top = 180,
                Width = 430,
                MaximumSize = new Size(430, 0),
                Text = "Não encontrado lic.data ou licença inválida."
            };

            var lblContact = new Label
            {
                AutoSize = true,
                Left = 20,
                Top = 220,
                MaximumSize = new Size(430, 0),
                Text = "Contatos: Cel 11 942963117 | email: alexandrechiavelli@gmail.com"
            };

            Controls.Add(lblKey);
            Controls.Add(lblLic);
            Controls.Add(txtKey);
            Controls.Add(txtLicense);
            Controls.Add(btnActivate);
            Controls.Add(btnTrial);
            Controls.Add(btnClose);
            Controls.Add(lblStatus);
            Controls.Add(lblContact);
        }

        private void ActivateLicense()
        {
            var key = txtKey.Text.Trim();
            var lic = txtLicense.Text.Trim();

            if (LicenseManager.TryActivate(key, lic, out var msg))
            {
                lblStatus.ForeColor = Color.DarkGreen;
                lblStatus.Text = msg;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                lblStatus.ForeColor = Color.DarkRed;
                lblStatus.Text = msg;
            }
        }

        private void StartTrial()
        {
            LicenseManager.StartTrial(_hwKey);
            lblStatus.ForeColor = Color.DarkGreen;
            lblStatus.Text = "Trial iniciado (30 dias).";
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}