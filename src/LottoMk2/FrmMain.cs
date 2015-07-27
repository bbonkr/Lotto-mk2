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
using LottoMk2.Helper;
using LottoMk2.Model;
using Tools.Log;

namespace LottoMk2
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();

            this.toolStripStatusLabel1.Text = String.Empty;

            this.Load += (s, e) =>
            {
                try
                {
                    //DatabaseHelper db = new DatabaseHelper();
                    _db.Init();
                    this.InitializeLogSetting();

                    DebugHelper.WriteLine("Test connected: " + _db.TestConnection().ToString());
                    int maxRetry = 5;
                    int retry = 0;
                    while (!_db.TestTable())
                    {
                        DebugHelper.WriteLine("Test table: " + _db.TestTable().ToString());
                        if (_db.CreateTable())
                        {
                            DebugHelper.WriteLine("table 'Lotto' is Created.");
                            break;
                        }
                        retry++;
                        if (retry > maxRetry) { throw new Exception("Could not create table."); }
                    }

                    this._NumberTextboxs.Add(this.textBox1);
                    this._NumberTextboxs.Add(this.textBox2);
                    this._NumberTextboxs.Add(this.textBox3);
                    this._NumberTextboxs.Add(this.textBox4);
                    this._NumberTextboxs.Add(this.textBox5);
                    this._NumberTextboxs.Add(this.textBox6);

                    this.dataGridViewDx1.ReadOnly = true;
                    this.dataGridViewDx1.AllowUserToAddRows = false;
                    this.dataGridViewDx1.AllowUserToDeleteRows = false;
                    this.dataGridViewDx1.AllowUserToResizeRows = false;
                    this.dataGridViewDx1.AllowUserToResizeColumns = false;
                    this.dataGridViewDx1.AllowUserToOrderColumns = false;
                    this.dataGridViewDx1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    this.dataGridViewDx1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
                    this.dataGridViewDx1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                    this.dataGridViewDx1.Columns.Add("Id", "회차");
                    this.dataGridViewDx1.Columns.Add("Dt", "추첨일자");
                    this.dataGridViewDx1.Columns.Add("Num1", "No. 1");
                    this.dataGridViewDx1.Columns.Add("Num2", "No. 2");
                    this.dataGridViewDx1.Columns.Add("Num3", "No. 3");
                    this.dataGridViewDx1.Columns.Add("Num4", "No. 4");
                    this.dataGridViewDx1.Columns.Add("Num5", "No. 5");
                    this.dataGridViewDx1.Columns.Add("Num6", "No. 6");
                    this.dataGridViewDx1.Columns.Add("NumBonus", "Bonus");

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
                    Logger.Error(this.GetType(), ex);
                }

                //실행횟수 증가

                Properties.Settings.Default.StartCount++;
                Properties.Settings.Default.Save();
            };/*this.Load*/

            this.당첨번호관리ToolStripMenuItem.Click += (s, e) =>
            {
                try
                {
                    FrmHistory frm = new FrmHistory();
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog();
                }
                catch (Exception ex)
                {
                    Logger.Error(this.GetType(), ex);
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

        private void Retrieve()
        {
            bool blnIncBonusNumber = this.chkIncBonusNumber.Checked;

            List<Lotto> dataSource = _db.CheckLotto(blnIncBonusNumber, this.GetInputNumbers());
            this.dataGridViewDx1.DataBind(dataSource, String.Empty);
            //this.lblRowCount.Text = String.Format("당첨번호 수: {0:n0}", dataSource.Count);
            //this.toolStripStatusLabel1.Text = String.Format("당첨번호 수: {0:n0}", this.dataGridViewDx1.Rows.Count);
            this.toolStripStatusLabel1.Text = String.Format("{0:n0} 건이 조회되었습니다.", dataSource.Count);
        }

        private int[] GetInputNumbers()
        {
            List<int> numbers = new List<int>();
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
        private DatabaseHelper _db = new DatabaseHelper();
    }
}