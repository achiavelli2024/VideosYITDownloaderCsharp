namespace VideosYITDownloaderCsharp.Licensing
{
    partial class LicenseForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.TextBox txtLicense;
        private System.Windows.Forms.Button btnActivate;
        private System.Windows.Forms.Button btnTrial;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblContact;
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.Label lblLic;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtKey = new System.Windows.Forms.TextBox();
            this.txtLicense = new System.Windows.Forms.TextBox();
            this.btnActivate = new System.Windows.Forms.Button();
            this.btnTrial = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblContact = new System.Windows.Forms.Label();
            this.lblKey = new System.Windows.Forms.Label();
            this.lblLic = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(20, 40);
            this.txtKey.Name = "txtKey";
            this.txtKey.ReadOnly = true;
            this.txtKey.Size = new System.Drawing.Size(430, 20);
            this.txtKey.TabIndex = 0;
            // 
            // txtLicense
            // 
            this.txtLicense.Location = new System.Drawing.Point(20, 100);
            this.txtLicense.Name = "txtLicense";
            this.txtLicense.Size = new System.Drawing.Size(430, 20);
            this.txtLicense.TabIndex = 1;
            // 
            // btnActivate
            // 
            this.btnActivate.Location = new System.Drawing.Point(20, 140);
            this.btnActivate.Name = "btnActivate";
            this.btnActivate.Size = new System.Drawing.Size(140, 23);
            this.btnActivate.TabIndex = 2;
            this.btnActivate.Text = "Ativar licença";
            this.btnActivate.UseVisualStyleBackColor = true;
            this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
            // 
            // btnTrial
            // 
            this.btnTrial.Location = new System.Drawing.Point(170, 140);
            this.btnTrial.Name = "btnTrial";
            this.btnTrial.Size = new System.Drawing.Size(170, 23);
            this.btnTrial.TabIndex = 3;
            this.btnTrial.Text = "Iniciar Trial (30 dias)";
            this.btnTrial.UseVisualStyleBackColor = true;
            this.btnTrial.Click += new System.EventHandler(this.btnTrial_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(350, 140);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Fechar";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.DarkRed;
            this.lblStatus.Location = new System.Drawing.Point(20, 180);
            this.lblStatus.MaximumSize = new System.Drawing.Size(430, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(180, 13);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Não encontrado lic.data ou inválida.";
            // 
            // lblContact
            // 
            this.lblContact.AutoSize = true;
            this.lblContact.Location = new System.Drawing.Point(20, 220);
            this.lblContact.MaximumSize = new System.Drawing.Size(430, 0);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(296, 13);
            this.lblContact.TabIndex = 6;
            this.lblContact.Text = "Contatos: Cel 11 942963117 | email: alexandrechiavelli@gmail.com";
            // 
            // lblKey
            // 
            this.lblKey.AutoSize = true;
            this.lblKey.Location = new System.Drawing.Point(20, 20);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(108, 13);
            this.lblKey.TabIndex = 7;
            this.lblKey.Text = "KeyCode (hardware):";
            // 
            // lblLic
            // 
            this.lblLic.AutoSize = true;
            this.lblLic.Location = new System.Drawing.Point(20, 80);
            this.lblLic.Name = "lblLic";
            this.lblLic.Size = new System.Drawing.Size(201, 13);
            this.lblLic.TabIndex = 8;
            this.lblLic.Text = "Licença (10 dígitos ou 123456789@):";
            // 
            // LicenseForm
            // 
            this.AcceptButton = this.btnActivate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(480, 260);
            this.Controls.Add(this.lblLic);
            this.Controls.Add(this.lblKey);
            this.Controls.Add(this.lblContact);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnTrial);
            this.Controls.Add(this.btnActivate);
            this.Controls.Add(this.txtLicense);
            this.Controls.Add(this.txtKey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LicenseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ativação - VideosYITDownloader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}