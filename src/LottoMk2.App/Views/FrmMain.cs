using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using LottoMk2.Data.Services;
using LottoMk2.Data.Services.Models;

using Microsoft.Extensions.Logging;

namespace LottoMk2.App.Views
{
    public partial class FrmMain : Form
    {
        public FrmMain(LottoDataService dataService, ILogger<FrmMain> logger, FrmHistory frmHistory, Splash splash)
        {
            this.dataService = dataService;
            this.logger = logger;
            this.frmHistory = frmHistory;
            this.splash = splash;

            InitializeComponent();

            this.toolStripStatusLabel1.Text = string.Empty;

            this.Load += FrmMain_Load;

            this.당첨번호관리ToolStripMenuItem.Visible = false;
            this.당첨번호관리ToolStripMenuItem.Click += (s, e) =>
            {
                try
                {
                    if (this.frmHistory != null)
                    {
                        this.frmHistory.StartPosition = FormStartPosition.CenterParent;
                        this.frmHistory.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.Message);
                }
            };

            this.btnAbout.Click += (s, e) =>
            {
                FrmAbout frm = new FrmAbout();
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            };

            this.종료XToolStripMenuItem.Click += (s, e) =>
            {
                this.Close();
            };
        }

        private void FrmMain_Load(object? sender, EventArgs e)
        {
            splash.ShowDialog();

            Show();

            try
            {
                this.InitializeLogSetting();

                this._NumberTextboxs.Add(this.textBox1);
                this._NumberTextboxs.Add(this.textBox2);
                this._NumberTextboxs.Add(this.textBox3);
                this._NumberTextboxs.Add(this.textBox4);
                this._NumberTextboxs.Add(this.textBox5);
                this._NumberTextboxs.Add(this.textBox6);
                
                this.dataGridViewDx1.AutoGenerateColumns = false;

                this.dataGridViewDx1.ReadOnly = true;
                this.dataGridViewDx1.AllowUserToAddRows = false;
                this.dataGridViewDx1.AllowUserToDeleteRows = false;
                this.dataGridViewDx1.AllowUserToResizeRows = false;
                this.dataGridViewDx1.AllowUserToResizeColumns = false;
                this.dataGridViewDx1.AllowUserToOrderColumns = false;
                this.dataGridViewDx1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                this.dataGridViewDx1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                this.dataGridViewDx1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                this.dataGridViewDx1.Columns.Add(nameof(LottoModel.Round), "회차");
                this.dataGridViewDx1.Columns.Add(nameof(LottoModel.LotteryDate), "추첨일자");
                this.dataGridViewDx1.Columns.Add(nameof(LottoModel.Num1), "No. 1");
                this.dataGridViewDx1.Columns.Add(nameof(LottoModel.Num2), "No. 2");
                this.dataGridViewDx1.Columns.Add(nameof(LottoModel.Num3), "No. 3");
                this.dataGridViewDx1.Columns.Add(nameof(LottoModel.Num4), "No. 4");
                this.dataGridViewDx1.Columns.Add(nameof(LottoModel.Num5), "No. 5");
                this.dataGridViewDx1.Columns.Add(nameof(LottoModel.Num6), "No. 6");
                this.dataGridViewDx1.Columns.Add(nameof(LottoModel.NumBonus), "Bonus");

                foreach (DataGridViewColumn col in this.dataGridViewDx1.Columns)
                {
                    col.DataPropertyName = col.Name;
                    col.SortMode = DataGridViewColumnSortMode.Automatic;
                    col.Resizable = DataGridViewTriState.True;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                foreach (var txt in this._NumberTextboxs)
                {
                    txt.Validating += NumberValidating;
                    txt.Validated += this.NumberChanged;
                }

                this.chkIncBonusNumber.CheckedChanged += this.NumberChanged;

                this.Retrieve();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }

            //실행횟수 증가

            //Properties.Settings.Default.StartCount++;
            //Properties.Settings.Default.Save();
        }

        private void NumberValidating(object sender, CancelEventArgs e)
        {
            TextBox thisControl = (TextBox)sender;
            if (thisControl.TextLength > 0)
            {
                int nTmp = 0;

                if (!int.TryParse(thisControl.Text, out nTmp)) { nTmp = -1; }

                if (nTmp <= 0 || nTmp > 45)
                {
                    thisControl.ResetText();
                    e.Cancel = true;
                }

                foreach (var tb in this._NumberTextboxs)
                {
                    if (tb.Name.Equals(thisControl.Name)) { continue; }
                    if (tb.TextLength == 0) { continue; }
                    int nTmp2 = 0;
                    if (!int.TryParse(tb.Text, out nTmp2)) { nTmp2 = -1; }

                    if (nTmp2 > 0 && nTmp == nTmp2)
                    {
                        thisControl.ResetText();
                        e.Cancel = true;
                    }
                }
            }
        }

        public void InitailizeDatabase()
        {
        }

        private void NumberChanged(object sender, EventArgs e)
        {
            //TextBox thisControl = (TextBox)sender;
            this.Retrieve();
        }

        private async void Retrieve()
        {
            bool blnIncBonusNumber = this.chkIncBonusNumber.Checked;

            //List<Lotto> dataSource = _db.CheckLotto(blnIncBonusNumber, this.GetInputNumbers());

            var result = await dataService.GetAllAsync(null, null, null, GetInputNumbers(), chkIncBonusNumber.Checked);

            this.dataGridViewDx1.DataBind(result, String.Empty);

            //this.lblRowCount.Text = String.Format("당첨번호 수: {0:n0}", dataSource.Count);
            //this.toolStripStatusLabel1.Text = String.Format("당첨번호 수: {0:n0}", this.dataGridViewDx1.Rows.Count);
            this.toolStripStatusLabel1.Text = String.Format("{0:n0} 건이 조회되었습니다.", result.Count());
        }

        private IEnumerable<int?> GetInputNumbers()
        {
            var numbers = new List<int?>();
            int nTmp = 0;
            foreach (var txt in this._NumberTextboxs)
            {
                if (!String.IsNullOrEmpty(txt.Text.Trim()))
                {
                    if (!Int32.TryParse(txt.Text, out nTmp)) { nTmp = -1; }

                    if (nTmp > 0)
                    {
                        numbers.Add(nTmp);
                    }
                }
            }
            return numbers.ToArray();
        }

        private void InitializeLogSetting()
        {
            string strLogDir = String.Empty;
            string strApplicationDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            strLogDir = Path.Combine(strApplicationDir, this.CompanyName, this.ProductName);

            if (!Directory.Exists(strLogDir))
            {
                Directory.CreateDirectory(strLogDir);
            }
        }

        private List<TextBox> _NumberTextboxs = new List<TextBox>();        
        private readonly LottoDataService dataService;
        private readonly ILogger logger;
        private readonly FrmHistory frmHistory;
        private readonly Splash splash;
    }
}