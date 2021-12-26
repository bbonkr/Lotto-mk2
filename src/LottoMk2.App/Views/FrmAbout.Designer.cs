namespace LottoMk2
{
    partial class FrmAbout
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
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lnkTobbonkr = new System.Windows.Forms.LinkLabel();
            this.lnkToTwitter = new System.Windows.Forms.LinkLabel();
            this.lnkToFacebook = new System.Windows.Forms.LinkLabel();
            this.lnkToGitHub = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(151, 446);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 48);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "닫기";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Gulim", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(17, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(371, 117);
            this.label1.TabIndex = 1;
            this.label1.Text = "Lotto MK2";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lnkTobbonkr
            // 
            this.lnkTobbonkr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkTobbonkr.Location = new System.Drawing.Point(91, 171);
            this.lnkTobbonkr.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkTobbonkr.Name = "lnkTobbonkr";
            this.lnkTobbonkr.Size = new System.Drawing.Size(297, 48);
            this.lnkTobbonkr.TabIndex = 1;
            this.lnkTobbonkr.TabStop = true;
            this.lnkTobbonkr.Text = "Web site: http://bbon.kr";
            this.lnkTobbonkr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lnkToTwitter
            // 
            this.lnkToTwitter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkToTwitter.Location = new System.Drawing.Point(91, 219);
            this.lnkToTwitter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkToTwitter.Name = "lnkToTwitter";
            this.lnkToTwitter.Size = new System.Drawing.Size(297, 48);
            this.lnkToTwitter.TabIndex = 2;
            this.lnkToTwitter.TabStop = true;
            this.lnkToTwitter.Text = "Twitter: @bbonkr";
            this.lnkToTwitter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lnkToFacebook
            // 
            this.lnkToFacebook.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkToFacebook.Location = new System.Drawing.Point(91, 267);
            this.lnkToFacebook.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkToFacebook.Name = "lnkToFacebook";
            this.lnkToFacebook.Size = new System.Drawing.Size(297, 48);
            this.lnkToFacebook.TabIndex = 3;
            this.lnkToFacebook.TabStop = true;
            this.lnkToFacebook.Text = "Facebook: bbonkr";
            this.lnkToFacebook.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lnkToGitHub
            // 
            this.lnkToGitHub.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkToGitHub.Location = new System.Drawing.Point(91, 315);
            this.lnkToGitHub.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lnkToGitHub.Name = "lnkToGitHub";
            this.lnkToGitHub.Size = new System.Drawing.Size(297, 48);
            this.lnkToGitHub.TabIndex = 4;
            this.lnkToGitHub.TabStop = true;
            this.lnkToGitHub.Text = "GitHub: bbonkr";
            this.lnkToGitHub.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmAbout
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(406, 519);
            this.ControlBox = false;
            this.Controls.Add(this.lnkToGitHub);
            this.Controls.Add(this.lnkToFacebook);
            this.Controls.Add(this.lnkToTwitter);
            this.Controls.Add(this.lnkTobbonkr);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "FrmAbout";
            this.Text = "정보";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lnkTobbonkr;
        private System.Windows.Forms.LinkLabel lnkToTwitter;
        private System.Windows.Forms.LinkLabel lnkToFacebook;
        private System.Windows.Forms.LinkLabel lnkToGitHub;
    }
}