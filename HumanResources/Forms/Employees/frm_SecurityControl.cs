using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HRMBLL.H0;
using HRMUtil;
using HumanResources.Properties;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Employees
{
    public partial class frm_SecurityControl : RadForm
    {
        private static frm_SecurityControl s_Instance;

        private readonly RadContextMenu _rcm = Utilities.Utilities.DefaultRadContextMenu();
        private DataTable _TempTable;

        public frm_SecurityControl()
        {
            InitializeComponent();
        }

        public static frm_SecurityControl Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_SecurityControl();
                return s_Instance;
            }
        }

        private void frm_SecurityControl_Load(object sender, EventArgs e)
        {
            AddMenu();
            _rcm.Items["rmiAdd"].Click += Frm_SecurityControl_Click2;
            _rcm.Items["rmiDelete"].Enabled = false;
            _rcm.Items["rmiEdit"].Click += Frm_SecurityControl_Click;
            _rcm.Items["rmiRefresh"].Click += Frm_SecurityControl_Click1;

            Utilities.Utilities.PopulateRootLevel(radTreeView2);
            documentWindow2.Text = "DANH SÁCH THẺ KIỂM SOÁT AN NINH";

            radGridView1.DataSource = SecurityControlBLL.GetAll();
            Utilities.Utilities.GridFormatting(radGridView1);

            toolWindow2.AutoHide();
            radLabelElement1.Text = "Tổng số: " + radGridView1.ChildRows.Count;

            radGridView1.ContextMenuOpening += RadGridView1_ContextMenuOpening;
            FormClosed += Frm_SecurityControl_FormClosed;
            radGridView1.CellFormatting += RadGridView1_CellFormatting;
        }

        private void radGridView1_CellFormatting_1(object sender, CellFormattingEventArgs e)
        {
            var dataColumn = e.CellElement.ColumnInfo as GridViewDataColumn;
            if (dataColumn.Name == "ContractToDate")
            {
                var dtValue = (DateTime) e.CellElement.RowInfo.Cells[dataColumn.Name].Value;
                if (dtValue != null)
                    if (dtValue.Year == 1753)
                        e.CellElement.Text = string.Empty;
            }
        }

        private void radCheckBox1_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            Cursor.Current = Cursors.AppStarting;
            if (args.ToggleState == ToggleState.On)
            {
                radGridView1.DataSource = GetExpiredRemind();
                Utilities.Utilities.GridFormatting(radGridView1);
            }
            else
            {
                radGridView1.DataSource = SecurityControlBLL.GetAll();
                Utilities.Utilities.GridFormatting(radGridView1);
            }
            Cursor.Current = Cursors.Default;
        }

        private DataTable GetExpiredRemind()
        {
            InitTempTableStructure();
            for (var i = 0; i < radGridView1.Rows.Count; i++)
                if (radGridView1.Rows[i].Cells["ContractTypeCode"].Value.ToString().Equals("HDKX"))
                {
                    var _ExpiredRemind = DateTime.Now.AddDays(-21);
                    var _Expired = Convert.ToDateTime(radGridView1.Rows[i].Cells["Expired"].Value);
                    if (_ExpiredRemind >= _Expired)
                        if (radGridView1.Rows[i].Cells["UserId"].Value.ToString() != string.Empty)
                        {
                            var newRow = _TempTable.NewRow();
                            for (var j = 0; j < radGridView1.Columns.Count; j++)
                                newRow[j] = radGridView1.Rows[i].Cells[j].Value;
                            _TempTable.Rows.InsertAt(newRow, 0);
                        }
                }
                else
                {
                    var _ContractToDateRemind = DateTime.Now.AddDays(21);
                    var _ContractToDate = Convert.ToDateTime(radGridView1.Rows[i].Cells["ContractToDate"].Value);
                    if (_ContractToDate <= _ContractToDateRemind)
                        if (radGridView1.Rows[i].Cells["UserId"].Value.ToString() != string.Empty)
                        {
                            var newRow = _TempTable.NewRow();
                            for (var j = 0; j < radGridView1.Columns.Count; j++)
                                newRow[j] = radGridView1.Rows[i].Cells[j].Value;
                            _TempTable.Rows.InsertAt(newRow, 0);
                        }
                }
            return _TempTable;
        }

        private void InitTempTableStructure()
        {
            _TempTable = new DataTable();
            foreach (var column in radGridView1.Columns)
                _TempTable.Columns.Add(column.Name, column.DataType);
        }

        private void AddTempTableRows()
        {
            if (radGridView1.Rows.Count > 0)
                for (var i = 0; i < radGridView1.Rows.Count; i++)
                {
                    _TempTable.Rows.Add();
                    for (var j = 0; j < radGridView1.Columns.Count; j++)
                        _TempTable.Rows[i][j] = radGridView1.Rows[i].Cells[j].Value;
                }
        }

        private void frm_SecurityControl_Shown(object sender, EventArgs e)
        {
            if (GetExpiredRemind().Rows.Count > 0)
                if (
                    MessageBox.Show($"Có {GetExpiredRemind().Rows.Count} thẻ gần hết hạn. Chọn YES để xem danh sách",
                        "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) ==
                    DialogResult.Yes)
                {
                    radCheckBox1.Checked = true;
                    radGridView1.DataSource = GetExpiredRemind();
                    Utilities.Utilities.GridFormatting(radGridView1);
                }
        }


        public void ViewData()
        {
            Cursor.Current = Cursors.AppStarting;
            try
            {
                radGridView1.DataSource = SecurityControlBLL.GetAll();
                Utilities.Utilities.GridFormatting(radGridView1);
            }
            catch
            {
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void AddMenu()
        {
            var _rmsi = new RadMenuSeparatorItem();
            _rcm.Items.Add(_rmsi);

            var _rmiImport = new RadMenuItem
            {
                Text = "Import",
                Image = Resources.Import,
                Name = "rmiImport"
            };
            _rmiImport.Click += _rmiImport_Click;
            _rcm.Items.Add(_rmiImport);

            _rmsi = new RadMenuSeparatorItem();
            _rcm.Items.Add(_rmsi);

            var _rmiHistory = new RadMenuItem
            {
                Text = "Lịch sử",
                Image = Resources.History,
                Name = "rmiHistory"
            };
            _rmiHistory.Click += _rmiHistory_Click;
            _rcm.Items.Add(_rmiHistory);
        }


        private void _rmiHistory_Click(object sender, EventArgs e)
        {
            var fsch = new frm_SecurityControlHistory(Convert.ToInt32(radGridView1.CurrentRow.Cells["UserId"].Value));
            Utilities.Utilities.SetScreenColor(fsch);
            fsch.ShowDialog();
        }

        private void _rmiImport_Click(object sender, EventArgs e)
        {
            var fie = new frm_Import_EmployeeSecurityControl();
            Utilities.Utilities.SetScreenColor(fie);
            fie.ShowDialog();
        }

        private void Frm_SecurityControl_Click2(object sender, EventArgs e)
        {
            var fasc = new frm_Add_SecurityControl();
            Utilities.Utilities.SetScreenColor(fasc);
            fasc.ShowDialog();
        }

        private void Frm_SecurityControl_Click1(object sender, EventArgs e)
        {
            ViewData();
        }

        private void Frm_SecurityControl_Click(object sender, EventArgs e)
        {
            if (!(radGridView1.CurrentRow is GridViewSearchRowInfo) ||
                !(radGridView1.CurrentRow is GridViewFilteringRowInfo) ||
                !(radGridView1.CurrentRow is GridViewTableHeaderRowInfo))
                if (radGridView1.CurrentRow is GridViewDataRowInfo)
                {
                    var fasc =
                        new frm_Add_SecurityControl(
                            Convert.ToInt32(radGridView1.CurrentRow.Cells["SecurityControlId"].Value),
                            Convert.ToInt32(radGridView1.CurrentRow.Cells["UserId"].Value),
                            radGridView1.CurrentRow.Cells["FullName"].Value.ToString(), this);
                    Utilities.Utilities.SetScreenColor(fasc);
                    fasc.ShowDialog();
                }
        }

        private void RadGridView1_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is GridCellElement)
                if ((e.CellElement.ColumnInfo.Name == "Area1") || (e.CellElement.ColumnInfo.Name == "Area2") ||
                    (e.CellElement.ColumnInfo.Name == "Area3") || (e.CellElement.ColumnInfo.Name == "Area4") ||
                    (e.CellElement.ColumnInfo.Name == "Area5") || (e.CellElement.ColumnInfo.Name == "Area6"))
                {
                    e.CellElement.Text = string.Empty;

                    var _index = e.CellElement.Value.ToString().LastIndexOf(';');
                    var _str = string.Empty;
                    if (_index > 0)
                        _str = e.CellElement.Value.ToString().Remove(_index);
                    if (!_str.Contains(";"))
                        e.CellElement.Text = Constants.GetNameBySecurityControlCode(_str);
                    else
                        foreach (var item in e.CellElement.Value.ToString().Split(';'))
                            if (item != string.Empty)
                                e.CellElement.Text += Constants.GetNameBySecurityControlCode(item) + Environment.NewLine;
                    if (e.CellElement.Text.Contains("1 đỏ") || e.CellElement.Text.Contains("4 đỏ"))
                    {
                        e.CellElement.ForeColor = Color.Red;
                        e.CellElement.TextAlignment = ContentAlignment.MiddleCenter;
                        e.CellElement.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
                    }
                    else
                    {
                        e.CellElement.ForeColor = Color.Black;
                        e.CellElement.TextAlignment = ContentAlignment.MiddleCenter;
                        e.CellElement.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
                    }
                }
        }

        private void RadGridView1_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            e.ContextMenu = _rcm.DropDown;
        }

        private void Frm_SecurityControl_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }
    }
}