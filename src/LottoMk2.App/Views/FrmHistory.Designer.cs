namespace LottoMk2
{
    partial class FrmHistory
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRetrieve = new System.Windows.Forms.Button();
            this.dataGridViewDx1 = new Tools.Windows.Forms.DataGridViewDx();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.도구TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.데이터베이스ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.엑셀파일ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportFromExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.CopySampleExcelFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.닫기XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDx1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnRetrieve);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 37);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(911, 98);
            this.panel1.TabIndex = 0;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(803, 25);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(91, 50);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.TabStop = false;
            this.btnDelete.Text = "삭제";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(703, 25);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(91, 50);
            this.btnSave.TabIndex = 0;
            this.btnSave.TabStop = false;
            this.btnSave.Text = "저장";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnRetrieve
            // 
            this.btnRetrieve.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRetrieve.Location = new System.Drawing.Point(603, 25);
            this.btnRetrieve.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnRetrieve.Name = "btnRetrieve";
            this.btnRetrieve.Size = new System.Drawing.Size(91, 50);
            this.btnRetrieve.TabIndex = 0;
            this.btnRetrieve.TabStop = false;
            this.btnRetrieve.Text = "검색";
            this.btnRetrieve.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRetrieve.UseVisualStyleBackColor = true;
            // 
            // dataGridViewDx1
            // 
            this.dataGridViewDx1.BackgroundColor = System.Drawing.SystemColors.AppWorkspace;
            this.dataGridViewDx1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDx1.DataKeyColumnNames = null;
            this.dataGridViewDx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewDx1.Location = new System.Drawing.Point(14, 21);
            this.dataGridViewDx1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.dataGridViewDx1.Name = "dataGridViewDx1";
            this.dataGridViewDx1.RowHeadersWidth = 62;
            this.dataGridViewDx1.RowTemplate.Height = 23;
            this.dataGridViewDx1.RowValidator = null;
            this.dataGridViewDx1.Size = new System.Drawing.Size(883, 710);
            this.dataGridViewDx1.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.도구TToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 4, 0, 4);
            this.menuStrip1.Size = new System.Drawing.Size(911, 37);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 도구TToolStripMenuItem
            // 
            this.도구TToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.데이터베이스ToolStripMenuItem,
            this.엑셀파일ToolStripMenuItem,
            this.toolStripMenuItem2,
            this.닫기XToolStripMenuItem});
            this.도구TToolStripMenuItem.Name = "도구TToolStripMenuItem";
            this.도구TToolStripMenuItem.Size = new System.Drawing.Size(83, 29);
            this.도구TToolStripMenuItem.Text = "도구(&T)";
            // 
            // 데이터베이스ToolStripMenuItem
            // 
            this.데이터베이스ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyDatabase,
            this.ExportDatabase});
            this.데이터베이스ToolStripMenuItem.Name = "데이터베이스ToolStripMenuItem";
            this.데이터베이스ToolStripMenuItem.Size = new System.Drawing.Size(222, 34);
            this.데이터베이스ToolStripMenuItem.Text = "데이터베이스";
            // 
            // CopyDatabase
            // 
            this.CopyDatabase.Name = "CopyDatabase";
            this.CopyDatabase.Size = new System.Drawing.Size(186, 34);
            this.CopyDatabase.Text = "가져오기";
            // 
            // ExportDatabase
            // 
            this.ExportDatabase.Name = "ExportDatabase";
            this.ExportDatabase.Size = new System.Drawing.Size(186, 34);
            this.ExportDatabase.Text = "내보내기";
            // 
            // 엑셀파일ToolStripMenuItem
            // 
            this.엑셀파일ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImportFromExcel,
            this.toolStripMenuItem3,
            this.CopySampleExcelFile});
            this.엑셀파일ToolStripMenuItem.Enabled = false;
            this.엑셀파일ToolStripMenuItem.Name = "엑셀파일ToolStripMenuItem";
            this.엑셀파일ToolStripMenuItem.Size = new System.Drawing.Size(222, 34);
            this.엑셀파일ToolStripMenuItem.Text = "엑셀파일";
            this.엑셀파일ToolStripMenuItem.Visible = false;
            // 
            // ImportFromExcel
            // 
            this.ImportFromExcel.Name = "ImportFromExcel";
            this.ImportFromExcel.Size = new System.Drawing.Size(186, 34);
            this.ImportFromExcel.Text = "가져오기";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(183, 6);
            // 
            // CopySampleExcelFile
            // 
            this.CopySampleExcelFile.Name = "CopySampleExcelFile";
            this.CopySampleExcelFile.Size = new System.Drawing.Size(186, 34);
            this.CopySampleExcelFile.Text = "예제파일";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(219, 6);
            // 
            // 닫기XToolStripMenuItem
            // 
            this.닫기XToolStripMenuItem.Name = "닫기XToolStripMenuItem";
            this.닫기XToolStripMenuItem.Size = new System.Drawing.Size(222, 34);
            this.닫기XToolStripMenuItem.Text = "닫기(&X)";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridViewDx1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 135);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(14, 21, 14, 21);
            this.panel2.Size = new System.Drawing.Size(911, 752);
            this.panel2.TabIndex = 3;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMessage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 887);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 20, 0);
            this.statusStrip1.Size = new System.Drawing.Size(911, 32);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblMessage
            // 
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(101, 25);
            this.lblMessage.Text = "lblMessage";
            // 
            // FrmHistory
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(911, 919);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(925, 939);
            this.Name = "FrmHistory";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "당첨번호관리";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDx1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRetrieve;
        private Tools.Windows.Forms.DataGridViewDx dataGridViewDx1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 도구TToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripMenuItem 닫기XToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblMessage;
        private System.Windows.Forms.ToolStripMenuItem 데이터베이스ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyDatabase;
        private System.Windows.Forms.ToolStripMenuItem ExportDatabase;
        private System.Windows.Forms.ToolStripMenuItem 엑셀파일ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ImportFromExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem CopySampleExcelFile;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    }
}