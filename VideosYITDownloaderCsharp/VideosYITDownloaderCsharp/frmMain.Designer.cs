namespace VideosYITDownloaderCsharp
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
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
            // 
            // btnVideo
            // 
            this.btnVideo.Location = new System.Drawing.Point(36, 130);
            this.btnVideo.Name = "btnVideo";
            this.btnVideo.Size = new System.Drawing.Size(167, 24);
            this.btnVideo.TabIndex = 3;
            this.btnVideo.Text = "Video";
            this.btnVideo.UseVisualStyleBackColor = true;
            // 
            // btnAudio
            // 
            this.btnAudio.Location = new System.Drawing.Point(209, 130);
            this.btnAudio.Name = "btnAudio";
            this.btnAudio.Size = new System.Drawing.Size(167, 24);
            this.btnAudio.TabIndex = 4;
            this.btnAudio.Text = "Audio";
            this.btnAudio.UseVisualStyleBackColor = true;
            // 
            // btnVideoAudio
            // 
            this.btnVideoAudio.Location = new System.Drawing.Point(382, 130);
            this.btnVideoAudio.Name = "btnVideoAudio";
            this.btnVideoAudio.Size = new System.Drawing.Size(167, 24);
            this.btnVideoAudio.TabIndex = 5;
            this.btnVideoAudio.Text = "Video + Audio";
            this.btnVideoAudio.UseVisualStyleBackColor = true;
            // 
            // btnPlayList
            // 
            this.btnPlayList.Location = new System.Drawing.Point(555, 130);
            this.btnPlayList.Name = "btnPlayList";
            this.btnPlayList.Size = new System.Drawing.Size(167, 24);
            this.btnPlayList.TabIndex = 6;
            this.btnPlayList.Text = "PlayList";
            this.btnPlayList.UseVisualStyleBackColor = true;
            // 
            // listLog
            // 
            this.listLog.FormattingEnabled = true;
            this.listLog.Location = new System.Drawing.Point(36, 198);
            this.listLog.Name = "listLog";
            this.listLog.Size = new System.Drawing.Size(686, 238);
            this.listLog.TabIndex = 7;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 450);
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
    }
}

