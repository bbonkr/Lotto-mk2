using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LottoMk2.Helper;
using LottoMk2.Model;
using Tools.Helper;
using Tools.Log;
using Tools.Windows.Forms;

namespace LottoMk2
{
    public partial class FrmHistory : Form
    {
        public FrmHistory()
        {
            InitializeComponent();
            this.lblMessage.Text = String.Empty;
            this.Load += (s, e) =>
            {
                try
                {
                    //DatabaseHelper db = new DatabaseHelper();
                    _db.Init();

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

                    this.dataGridViewDx1.ReadOnly = false;
                    this.dataGridViewDx1.AllowUserToAddRows = true;
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
                    this.dataGridViewDx1.DataKeyColumnNames = new string[] { "Id" };
                    this.dataGridViewDx1.RowValidator += RowValidator;

                    foreach (DataGridViewColumn col in this.dataGridViewDx1.Columns)
                    {
                        col.DataPropertyName = col.Name;
                        col.SortMode = DataGridViewColumnSortMode.Automatic;
                        col.Resizable = DataGridViewTriState.True;
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                    this.Retrieve();

                    // Excel 가져오기 가능여부 체크
                    //try
                    //{
                    //    Tools.Office.ExcelService excel = new Tools.Office.ExcelService();
                    //    excel.LoadData();
                    //}
                    //catch (Exception ex)
                    //{
                    //    int k = 0;
                    //}
                }
                catch (Exception ex)
                {
                    Logger.Error(this.GetType(), ex);
                }
            };

            this._worker.WorkerSupportsCancellation = true;
            this._worker.WorkerReportsProgress = true;
            this._worker.DoWork += _worker_DoWork;
            this._worker.ProgressChanged += _worker_ProgressChanged;
            this._worker.RunWorkerCompleted += _worker_RunWorkerCompleted;
            this.btnRetrieve.Click += (s, e) =>
            {
                try
                {
                    this.Retrieve();
                }
                catch (Exception ex)
                {
                    Logger.Error(this.GetType(), ex);
                }
            };

            this.btnSave.Click += (s, e) =>
            {
                try
                {
                    int? nLastId = null;
                    foreach (DataGridViewRow row in this.dataGridViewDx1.Rows)
                    {
                        if (row.IsChanged() && row.Vaild())
                        {
                            Lotto item = row.DataBoundItem as Lotto;
                            if (!this._db.Save(item))
                            {
                                DebugHelper.WriteLine("Fail to save.");
                            }
                            else
                            {
                                nLastId = item.Id;
                            }
                        }
                    }

                    this.Retrieve();
                    this.dataGridViewDx1.SelectRow(nLastId);
                    //MessageBox.Show("Saved.", "Notification");
                    this.lblMessage.Text = "저장되었습니다.";
                }
                catch (Exception ex)
                {
                    Logger.Error(this.GetType(), ex);
                }
            };

            this.btnDelete.Click += (s, e) =>
            {
                try
                {
                    int selectedCount = this.dataGridViewDx1.SelectedRows.Count;
                    if (selectedCount > 0)
                    {
                        DialogResult dialogResult = MessageBox.Show("Do you want to delete selected rows?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (DialogResult.Yes == dialogResult)
                        {
                            for (int i = selectedCount - 1; i >= 0; i--)
                            {
                                if (this.dataGridViewDx1.SelectedRows[i].IsNewRow) { continue; }
                                Lotto item = this.dataGridViewDx1.SelectedRows[i].DataBoundItem as Lotto;

                                if (!this._db.Delete(item))
                                {
                                    DebugHelper.WriteLine(String.Format("Could not delete. (id: {0})", item.Id));
                                }
                            }

                            //MessageBox.Show("Deleted.", "Notification");
                            this.Retrieve();
                            this.lblMessage.Text = "삭제되었습니다.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(this.GetType(), ex);
                }
            };

            this.ImportFromExcel.Click += (s, e) =>
            {
                try
                {
                    //this.ImportData();
                    if (!this._worker.IsBusy)
                    {
                        OpenFileDialog dialog = new OpenFileDialog();
                        dialog.Multiselect = false;
                        dialog.AutoUpgradeEnabled = true;
                        dialog.Filter = "엑셀 통합문서 (*.xlsx)|*.xlsx|엑셀 호환문서 (*.xls)|*.xls|전체파일(*.*)|*.*";

                        if (DialogResult.OK == dialog.ShowDialog())
                        {
                            this.btnRetrieve.Enabled = false;
                            this.btnDelete.Enabled = false;
                            this.btnSave.Enabled = false;
                            this.dataGridViewDx1.Enabled = false;

                            this.Cursor = Cursors.WaitCursor;

                            this._worker.RunWorkerAsync(dialog.FileName);
                        }
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(this.GetType(), ex);
                }
            };

            this.CopySampleExcelFile.Click += (s, e) =>
            {
                try
                {
                    this.GetSampleData();
                }
                catch (Exception ex)
                {
                    Logger.Error(this.GetType(), ex);
                }
            };

            this.CopyDatabase.Click += (s, e) =>
            {
                // 데이터베이스 파일을 가져온다.
                try
                {
                    this.CopyDatabaseFile();
                }
                catch (Exception ex)
                {
                    Logger.Error(this.GetType(), ex);
                }
            };

            this.ExportDatabase.Click += (s, e) =>
            {
                // 데이터베이스 파일을 내보낸다.
                try
                {
                    this.ExportDatabaseFile();
                }
                catch (Exception ex)
                {
                    Logger.Error(this.GetType(), ex);
                }
            };

            this.닫기XToolStripMenuItem.Click += (s, e) =>
            {
                this.Close();
            };
        }

        private bool RowValidator(DataGridViewRow row)
        {
            RowTagObject tag = row.Tag as RowTagObject;
            Lotto lotto = row.DataBoundItem as Lotto;

            if (lotto != null)
            {
                if (lotto.Id < 1) { return false; }
                int[] numbers = { lotto.Num1, lotto.Num2, lotto.Num3, lotto.Num4, lotto.Num5, lotto.Num6, lotto.NumBonus };
                foreach (int n in numbers)
                {
                    if (n < 1 || n > 46) { return false; }
                }

                if (numbers.Distinct().Count() < 7) { return false; }

                //tag.Changed = true;
                tag.Valid = true;
                row.Tag = tag;
            }

            return true;
        }

        private void Retrieve()
        {
            List<Lotto> dataSource = this._db.GetData(null, null, null);
            this.dataGridViewDx1.DataBind(dataSource, String.Empty);

            this.lblMessage.Text = String.Format("{0:n0} 건이 조회되었습니다.", dataSource.Count);
        }

        private void ImportData(BackgroundWorker worker, DoWorkEventArgs e)
        {
            string strFilename = String.Format("{0}", e.Argument);

            int nFailCount = 0;
            int nSaveCount = 0;

            Tools.Office.ExcelService excel = new Tools.Office.ExcelService();
            excel.FilePath = strFilename;
            excel.HeaderRowIndex = 0;
            excel.DataStartRowIndex = 1;
            DataTable dataTable = null;
            if (!e.Cancel)
            {
                worker.ReportProgress(0, "자료를 로드합니다.");
                dataTable = excel.LoadData();
                worker.ReportProgress(0, "자료를 로드했습니다.");
            }

            List<Lotto> dataSource = new List<Lotto>();
            if (dataTable != null)
            {
                dataSource = dataTable.Select().Select(r => new Lotto()
                  {
                      Id = Convert.ToInt32(r["회차"]),
                      Dt = String.Format("{0}", r["추첨일"]).Replace(".", "-"),
                      Num1 = Convert.ToInt32(r["1"]),
                      Num2 = Convert.ToInt32(r["2"]),
                      Num3 = Convert.ToInt32(r["3"]),
                      Num4 = Convert.ToInt32(r["4"]),
                      Num5 = Convert.ToInt32(r["5"]),
                      Num6 = Convert.ToInt32(r["6"]),
                      NumBonus = Convert.ToInt32(r["보너스"])
                  }).ToList();

                DatabaseHelper db = new DatabaseHelper();
                db.DeleteAll();

                foreach (var item in dataSource)
                {
                    if (!e.Cancel)
                    {
                        if (!db.Save(item)) { nFailCount++; }
                        nSaveCount++;
                        string strLabel = "{0:f2} % ({1:n0}/{2:n0}) 진행중 ...";
                        if (nSaveCount == dataSource.Count)
                        {
                            strLabel = "{0:f2} % ({1:n0}/{2:n0}) 완료";
                        }
                        worker.ReportProgress((nSaveCount) / dataSource.Count, String.Format(strLabel, ((nSaveCount * 1.0) / dataSource.Count) * 100, nSaveCount, dataSource.Count));
                    }
                }
            }
        }

        /// <summary>
        /// 가져오기 샘플 자료
        /// </summary>
        private void GetSampleData(string filePath, bool openFileExplorer)
        {
            if (String.IsNullOrEmpty(filePath)) { throw new ArgumentException("The path is not allow empty string."); }
            string filePathDirectory = String.Empty;
            filePathDirectory = Path.GetDirectoryName(filePath);

            string[] resourceNames = ResourceHelper.GetResourceNames();
            string dataFileName = resourceNames.Where(name => name.EndsWith("data.xlsx")).FirstOrDefault();
            using (Stream stream = ResourceHelper.LoadContainResourceFileStream(dataFileName))
            {
                int nLength = (int)stream.Length;
                byte[] buffer = new byte[nLength];
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    buffer = reader.ReadBytes(nLength);
                }

                try
                {
                    File.WriteAllBytes(filePath, buffer);
                }
                catch (Exception ex)
                {
                    Logger.Error(this.GetType(), ex);
                    this.lblMessage.Text = String.Format("[ERROR]: {0}", ex.Message);
                }
            }

            if (openFileExplorer)
            {
                Process.Start(filePathDirectory);
            }
        }

        private void GetSampleData(string filePath)
        {
            this.GetSampleData(filePath, true);
        }

        private void GetSampleData()
        {
            SaveFileDialog dialaog = new SaveFileDialog();
            dialaog.AutoUpgradeEnabled = true;
            dialaog.DefaultExt = ".xlsx";
            dialaog.FileName = "Sample";
            dialaog.Filter = "엑셀 통합문서 (*.xlsx)|*.xlsx";
            if (DialogResult.OK == dialaog.ShowDialog())
            {
                this.GetSampleData(dialaog.FileName, true);
            }
        }

        private void CopyDatabaseFile()
        {
            string strDatabaseFilePath = this._db.GetDbFilePath();
            string strDatabaseFileDirectory = Path.GetDirectoryName(strDatabaseFilePath);

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.AutoUpgradeEnabled = true;
            dialog.Filter = "데이터베이스 파일(*.db)|*.db|모든파일(*.*)|*.*";
            dialog.Multiselect = false;
            dialog.DefaultExt = ".db";
            if (DialogResult.OK == dialog.ShowDialog())
            {
                // 데이터베이스 파일 유효성 검사
                if (!this._db.TestConnection(dialog.FileName))
                {
                    this.lblMessage.Text = String.Format("[ERROR]: {0}", "데이터 베이스 파일이 유효하지 않습니다.");
                }
                else
                {
                    try
                    {
                        // 기존 파일을 백업한다.
                        File.Copy(strDatabaseFilePath, Path.Combine(strDatabaseFileDirectory, String.Format("backup_{0:yyyyMMddHHmmsss}.db", DateTime.Now)));

                        try
                        {
                            // 파일을 복사한다.
                            File.Copy(dialog.FileName, strDatabaseFilePath, true);

                            this.lblMessage.Text = "가져오기 완료";
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(this.GetType(), ex);
                            this.lblMessage.Text = String.Format("[ERROR]: {0}", ex.Message);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(this.GetType(), ex);
                        this.lblMessage.Text = String.Format("[ERROR]: {0}", ex.Message);
                    }
                }
            }
            else
            {
                this.lblMessage.Text = "가져오기 취소";
            }
        }

        private void ExportDatabaseFile()
        {
            string strDatabaseFilePath = this._db.GetDbFilePath();
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.AutoUpgradeEnabled = true;
            dialog.DefaultExt = ".db";
            dialog.Filter = "데이터베이스 파일(*.db)|*.db";
            if (DialogResult.OK == dialog.ShowDialog())
            {
                try
                {
                    File.Copy(strDatabaseFilePath, dialog.FileName, true);

                    this.lblMessage.Text = String.Format("내보내기 완료! {0}", dialog.FileName);
                }
                catch (Exception ex)
                {
                    Logger.Error(this.GetType(), ex);
                    this.lblMessage.Text = String.Format("[ERROR]: {0}", ex.Message);
                }
            }
            else
            {
                this.lblMessage.Text = "내보내기 취소";
            }
        }

        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.ImportData((BackgroundWorker)sender, e);
        }

        private void _worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //this.lblReportProgress.Text = String.Format("{0}", e.UserState);

            this.lblMessage.Text = String.Format("{0}", e.UserState);
        }

        private void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    this.lblMessage.Text = String.Format("[Error]: {0}", e.Error.Message);
                }
                else if (e.Cancelled)
                {
                    this.lblMessage.Text = "가져오기가 취소되었습니다.";
                }
                else
                {
                    //this.lblReportProgress.Text = "완료 ";

                    this.Retrieve();
                }
            }
            finally
            {
                this.btnRetrieve.Enabled = true;
                this.btnDelete.Enabled = true;
                this.btnSave.Enabled = true;
                this.dataGridViewDx1.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        private DatabaseHelper _db = new DatabaseHelper();
        private BindingSource _source = new BindingSource();
        private BackgroundWorker _worker = new BackgroundWorker();
    }
}