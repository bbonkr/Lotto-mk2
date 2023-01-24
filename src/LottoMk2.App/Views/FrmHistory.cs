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

using LottoMk2.Data.Services;
using LottoMk2.Data.Services.Models;

using Microsoft.Extensions.Logging;

using Tools.Windows.Forms;

namespace LottoMk2
{
    public partial class FrmHistory : Form
    {
        public FrmHistory(LottoDataService dataService, ILogger<FrmHistory> logger)
        {
            this.dataService = dataService;
            this.logger = logger;

            InitializeComponent();
            this.lblMessage.Text = String.Empty;
            this.Load += async (s, e) =>
            {
                try
                {
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

                    await this.Retrieve();
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex, ex.Message);
                }
            };

            this._worker.WorkerSupportsCancellation = true;
            this._worker.WorkerReportsProgress = true;

            this.btnRetrieve.Click += async (s, e) =>
            {
                try
                {
                    await this.Retrieve();
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex, ex.Message);
                }
            };

            this.btnSave.Click += async (s, e) =>
            {
                try
                {
                    int? nLastId = null;
                    bool hasChanged = false;
                    foreach (DataGridViewRow row in this.dataGridViewDx1.Rows)
                    {
                        if (row.IsChanged() && row.Vaild())
                        {
                            if (row.DataBoundItem is LottoModel item)
                            {
                                await dataService.UpdateAsync(item);

                                nLastId = item.Round;
                                hasChanged = true;
                            }
                        }
                    }

                    if (hasChanged)
                    {
                        await dataService.SaveAsync();


                        await this.Retrieve();

                        if (nLastId.HasValue)
                        {
                            this.dataGridViewDx1.SelectRow(nLastId.Value);
                        }
                        
                        this.lblMessage.Text = "저장되었습니다.";
                    }
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex, ex.Message);
                }
            };

            this.btnDelete.Click += async (s, e) =>
            {
                try
                {
                    int selectedCount = this.dataGridViewDx1.SelectedRows.Count;
                    bool hasChanged = false;
                    if (selectedCount > 0)
                    {
                        DialogResult dialogResult = MessageBox.Show("Do you want to delete selected rows?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (DialogResult.Yes == dialogResult)
                        {
                            for (int i = selectedCount - 1; i >= 0; i--)
                            {
                                if (this.dataGridViewDx1.SelectedRows[i].IsNewRow) { continue; }
                                if (this.dataGridViewDx1.SelectedRows[i].DataBoundItem is LottoModel item)
                                {
                                    await dataService.DeleteAsync(item);
                                    hasChanged = true;
                                }
                            }

                            if (hasChanged)
                            {
                                var affectedCount = await dataService.SaveAsync();

                                await this.Retrieve();

                                this.lblMessage.Text = "삭제되었습니다.";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex, ex.Message);
                }
            };

            this.닫기XToolStripMenuItem.Click += (s, e) =>
            {
                this.Close();
            };
        }

        private bool RowValidator(DataGridViewRow row)
        {
            //RowTagObject tag = row.Tag as RowTagObject;
            if (row.Tag is RowTagObject tag)
            {
                var lotto = row.DataBoundItem as LottoModel;

                if (lotto != null)
                {
                    if (lotto.Round < 1) { return false; }

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
            }

            return true;
        }

        private async Task Retrieve()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                var dataSource = await dataService.GetAllAsync(null, null, null, null, null);
                this.dataGridViewDx1.DataBind(dataSource, String.Empty);

                this.lblMessage.Text = String.Format("{0:n0} 건이 조회되었습니다.", dataSource.Count());
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private BindingSource _source = new BindingSource();
        private BackgroundWorker _worker = new BackgroundWorker();

        private readonly LottoDataService dataService;
        private readonly ILogger logger;
    }
}