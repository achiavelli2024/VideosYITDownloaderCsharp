namespace VideosYITDownloaderCsharp
{
    partial class frmMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                _cts?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnPasta = new System.Windows.Forms.Button();
            this.btnVideo = new System.Windows.Forms.Button();
            this.btnAudio = new System.Windows.Forms.Button();
            this.btnVideoAudio = new System.Windows.Forms.Button();
            this.btnPlayList = new System.Windows.Forms.Button();
            this.listLog = new System.Windows.Forms.ListBox();
            this.lblLicenseStatus = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Link do vídeo";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(124, 33);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(598, 20);
            this.textBox1.TabIndex = 1;
            // 
            // btnPasta
            // 
            this.btnPasta.Location = new System.Drawing.Point(555, 80);
            this.btnPasta.Name = "btnPasta";
            this.btnPasta.Size = new System.Drawing.Size(167, 24);
            this.btnPasta.TabIndex = 2;
            this.btnPasta.Text = "Selecionar Pasta";
            this.btnPasta.UseVisualStyleBackColor = true;
            this.btnPasta.Click += new System.EventHandler(this.btnPasta_Click);
            // 
            // btnVideo
            // 
            this.btnVideo.Location = new System.Drawing.Point(36, 130);
            this.btnVideo.Name = "btnVideo";
            this.btnVideo.Size = new System.Drawing.Size(167, 24);
            this.btnVideo.TabIndex = 3;
            this.btnVideo.Text = "Video";
            this.btnVideo.UseVisualStyleBackColor = true;
            this.btnVideo.Click += new System.EventHandler(this.btnVideo_Click);
            // 
            // btnAudio
            // 
            this.btnAudio.Location = new System.Drawing.Point(209, 130);
            this.btnAudio.Name = "btnAudio";
            this.btnAudio.Size = new System.Drawing.Size(167, 24);
            this.btnAudio.TabIndex = 4;
            this.btnAudio.Text = "Audio";
            this.btnAudio.UseVisualStyleBackColor = true;
            this.btnAudio.Click += new System.EventHandler(this.btnAudio_Click);
            // 
            // btnVideoAudio
            // 
            this.btnVideoAudio.Location = new System.Drawing.Point(382, 130);
            this.btnVideoAudio.Name = "btnVideoAudio";
            this.btnVideoAudio.Size = new System.Drawing.Size(167, 24);
            this.btnVideoAudio.TabIndex = 5;
            this.btnVideoAudio.Text = "Video + Audio";
            this.btnVideoAudio.UseVisualStyleBackColor = true;
            this.btnVideoAudio.Click += new System.EventHandler(this.btnVideoAudio_Click);
            // 
            // btnPlayList
            // 
            this.btnPlayList.Location = new System.Drawing.Point(555, 130);
            this.btnPlayList.Name = "btnPlayList";
            this.btnPlayList.Size = new System.Drawing.Size(167, 24);
            this.btnPlayList.TabIndex = 6;
            this.btnPlayList.Text = "PlayList";
            this.btnPlayList.UseVisualStyleBackColor = true;
            this.btnPlayList.Click += new System.EventHandler(this.btnPlayList_Click);
            // 
            // listLog
            // 
            this.listLog.FormattingEnabled = true;
            this.listLog.Location = new System.Drawing.Point(36, 228);
            this.listLog.Name = "listLog";
            this.listLog.Size = new System.Drawing.Size(686, 212);
            this.listLog.TabIndex = 7;
            // 
            // lblLicenseStatus
            // 
            this.lblLicenseStatus.AutoSize = true;
            this.lblLicenseStatus.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblLicenseStatus.Location = new System.Drawing.Point(33, 9);
            this.lblLicenseStatus.Name = "lblLicenseStatus";
            this.lblLicenseStatus.Size = new System.Drawing.Size(106, 13);
            this.lblLicenseStatus.TabIndex = 8;
            this.lblLicenseStatus.Text = "Licença: (carregando)";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(36, 192);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(600, 23);
            this.progressBar1.TabIndex = 9;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(642, 198);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(21, 13);
            this.lblProgress.TabIndex = 10;
            this.lblProgress.Text = "0%";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Pasta de destino";
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Location = new System.Drawing.Point(124, 82);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.Size = new System.Drawing.Size(425, 20);
            this.txtOutputFolder.TabIndex = 12;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 460);
            this.Controls.Add(this.txtOutputFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblLicenseStatus);
            this.Controls.Add(this.listLog);
            this.Controls.Add(this.btnPlayList);
            this.Controls.Add(this.btnVideoAudio);
            this.Controls.Add(this.btnAudio);
            this.Controls.Add(this.btnVideo);
            this.Controls.Add(this.btnPasta);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VideosYITDownloader";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnPasta;
        private System.Windows.Forms.Button btnVideo;
        private System.Windows.Forms.Button btnAudio;
        private System.Windows.Forms.Button btnVideoAudio;
        private System.Windows.Forms.Button btnPlayList;
        private System.Windows.Forms.ListBox listLog;
        private System.Windows.Forms.Label lblLicenseStatus;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOutputFolder;
    }
}