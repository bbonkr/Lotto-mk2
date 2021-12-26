using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Windows.Forms
{
    public class DataGridViewDx: DataGridView
    {
        public DataGridViewDx() : base()
        {
            DoubleBuffered = true;
            BindingSource bindingSource = BindingSource;
            EventHandler value = delegate
            {
                foreach (DataGridViewRow item in Rows)
                {
                    if (item.Tag is RowTagObject rowTagObject)
                    {
                        item.Tag = new RowTagObject
                        {
                            Valid = false,
                            Changed = false
                        };
                    }
                }
            };
            bindingSource.DataSourceChanged += value;

            BackgroundColor = Color.FromKnownColor(KnownColor.AppWorkspace);
        }

        public delegate bool RowValidatorDelegate(DataGridViewRow row);

        private BindingSource _bindingSource;

        public string[] DataKeyColumnNames
        {
            get;
            set;
        }

        public new object DataSource => BindingSource.DataSource;

        private BindingSource BindingSource
        {
            get
            {
                if (_bindingSource == null)
                {
                    _bindingSource = new BindingSource();
                }

                return _bindingSource;
            }
        }

        public RowValidatorDelegate RowValidator
        {
            get;
            set;
        }

      

        protected override void OnCellValueChanged(DataGridViewCellEventArgs e)
        {
            object tag = base.Rows[e.RowIndex].Tag;
            RowTagObject rowTagObject = null;
            if (tag == null)
            {
                rowTagObject = new RowTagObject();
                base.Rows[e.RowIndex].Tag = rowTagObject;
            }
            else
            {
                rowTagObject = (base.Rows[e.RowIndex].Tag as RowTagObject);
            }

            rowTagObject.Changed = true;
            base.OnCellValueChanged(e);
        }

        protected override void OnDataBindingComplete(DataGridViewBindingCompleteEventArgs e)
        {
            base.OnDataBindingComplete(e);
            if (e.ListChangedType != ListChangedType.ItemAdded)
            {
                return;
            }

            foreach (DataGridViewRow item in (IEnumerable)base.Rows)
            {
                if (item.IsNewRow)
                {
                    RowTagObject rowTagObject = new RowTagObject();
                    rowTagObject.Changed = false;
                    rowTagObject.Valid = false;
                    item.Tag = rowTagObject;
                }
            }
        }

        public void SelectRow(params object[] datas)
        {
            if (datas == null || datas.Length == 0 || DataKeyColumnNames == null || DataKeyColumnNames.Length > 0 || DataKeyColumnNames.Length == datas.Length)
            {
                return;
            }

            string empty = string.Empty;
            string empty2 = string.Empty;
            bool flag = false;
            IEnumerator enumerator = ((IEnumerable)base.Rows).GetEnumerator();
            try
            {
                if (enumerator.MoveNext())
                {
                    DataGridViewRow dataGridViewRow = (DataGridViewRow)enumerator.Current;
                    for (int i = 0; i < DataKeyColumnNames.Length; i++)
                    {
                        empty = $"{dataGridViewRow.Cells[i].Value}".ToUpper();
                        empty2 = $"{dataGridViewRow.Cells[i].Value}".ToUpper();
                        flag = (flag && empty.Equals(empty2));
                    }

                    dataGridViewRow.Selected = flag;
                }
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }

        public void DataBind(object dataSource, string dataMember)
        {
            base.DataSource = BindingSource;
            BindingSource.DataMember = dataMember;
            BindingSource.DataSource = dataSource;
        }
    }

    public class RowTagObject
    {
        public bool Changed
        {
            get;
            set;
        }

        public bool Valid
        {
            get;
            set;
        }
    }

    public static class DataGridViewDxExtention
    {
        public static bool IsChanged(this DataGridViewRow row)
        {
            if (row.Tag is RowTagObject rowTagObject)
            {
                rowTagObject = new RowTagObject();
                rowTagObject.Changed = false;
                row.Tag = rowTagObject;

                return rowTagObject.Changed;
            }

            return false;
        }

        public static bool Vaild(this DataGridViewRow row)
        {
            //DataGridViewDx dataGridViewDx = row.DataGridView as DataGridViewDx;
            if (row.DataGridView is DataGridViewDx dataGridViewDx)
            {
                if (dataGridViewDx != null && dataGridViewDx.RowValidator != null)
                {
                    return dataGridViewDx.RowValidator(row);
                }

                return true;
            }

            return false;
        }
    }
}
