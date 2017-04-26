using System;
using System.Drawing;
using System.Windows.Forms;
using HRMBLL.H0;
using HRMBLL.H1;
using HumanResources.Properties;
using HumanResources.Reports.Report_Preview;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Contract.SubContract
{
    public partial class frm_List_SubContract : RadForm
    {
        private static frm_List_SubContract s_Instance;

        private readonly RadContextMenu _rcm = Utilities.Utilities.DefaultRadContextMenu();


        private bool isColumnAdded;

        public frm_List_SubContract()
        {
            InitializeComponent();

            AddMenu();
            InitData();

            Utilities.Utilities.GridFormatting(radGridView1);
            Utilities.Utilities.GridTemplateFormatting(gridViewTemplate1);
        }

        public static frm_List_SubContract Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_List_SubContract();
                return s_Instance;
            }
        }

        private void SubContract_Load(object sender, EventArgs e)
        {
            FormClosed += SubContract_FormClosed;
            KeyDown += SubContract_KeyDown;

            _rcm.Items["rmiAdd"].Click += RmiAdd_Click;
            _rcm.Items["rmiEdit"].Click += Frm_List_SubContract_Click;
            _rcm.Items["rmiRefresh"].Click += Frm_List_SubContract_Click1;

            radGridView1.CellBeginEdit += RadGridView1_CellBeginEdit;
            radGridView1.RowFormatting += RadGridView1_RowFormatting;
            radGridView1.ContextMenuOpening += RadGridView1_ContextMenuOpening;
            radGridView1.ViewCellFormatting += RadGridView1_ViewCellFormatting;
        }


        public void AddMenu()
        {
            var _rmsi = new RadMenuSeparatorItem();
            _rcm.Items.Add(_rmsi);

            var _rmiHistory = new RadMenuItem
            {
                Text = "Lịch sử",
                Image = Resources.History,
                Name = "rmiHistory"
            };
            _rmiHistory.Click += RmiHistory_Click;
            _rcm.Items.Add(_rmiHistory);

            _rmsi = new RadMenuSeparatorItem();
            _rcm.Items.Add(_rmsi);

            var _rmiPreviewContract = new RadMenuItem
            {
                Text = "Xem hợp đồng",
                Image = Resources.Preview,
                Name = "rmiPreviewContract"
            };
            _rmiPreviewContract.Click += RmiViewContract_Click;
            _rcm.Items.Add(_rmiPreviewContract);
            var _rmiPrintContract = new RadMenuItem
            {
                Text = "In hợp đồng",
                Image = Resources.File,
                Name = "rmiPrintContract"
            };
            _rmiPrintContract.Click += RmiPrint_Click;
            _rcm.Items.Add(_rmiPrintContract);
        }

        private void InitData()
        {
            BS_Emp.DataSource = EmployeeContractBLL.GetAllToDT();
            BS_PositionContract.DataSource = PositionsBLL.GetAllToDT();
            BS_ScaleOfSalary.DataSource = ScaleOfSalariesBLL.GetAll();
            BS_ScaleOfSalary1.DataSource = ScaleOfSalariesBLL.GetAll();
            BS_EmpSubContract.DataSource = EmployeeSubContractBLL.GetAll();
            BS_SubAllDistinct.DataSource = EmployeeSubContractBLL.GetAllDistinct();
        }

        public void ViewData()
        {
            Cursor.Current = Cursors.AppStarting;

            try
            {
                radGridView1.DataSource = EmployeeSubContractBLL.GetAllDistinct();
                gridViewTemplate1.DataSource = EmployeeSubContractBLL.GetAll();
            }
            catch (Exception ex)
            {
            }

            Cursor.Current = Cursors.Default;
        }


        private void Frm_List_SubContract_Click1(object sender, EventArgs e)
        {
            ViewData();
        }

        private void Frm_List_SubContract_Click(object sender, EventArgs e)
        {
            if (!(radGridView1.CurrentRow is GridViewTableHeaderRowInfo))
            {
                Cursor.Current = Cursors.AppStarting;

                frm_Add_SubContract f_NewSubContract = null;
                try
                {
                    f_NewSubContract = new frm_Add_SubContract(this,
                        Convert.ToInt32(radGridView1.CurrentRow.Cells["EmployeeSubContractId"].Value),
                        Convert.ToInt32(radGridView1.CurrentRow.Cells["UserId"].Value),
                        radGridView1.CurrentRow.Cells["FullName"].Value.ToString());
                }
                catch
                {
                }
                Cursor.Current = Cursors.Default;
                if (f_NewSubContract != null)
                    f_NewSubContract.ShowDialog();
            }
        }

        private void RadGridView1_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement.ViewTemplate.Parent != null)
            {
                e.CellElement.BackColor = Color.FromArgb(0xED, 0xE4, 0xF3);
                e.CellElement.BackColor2 = Color.FromArgb(0xED, 0xE4, 0xF3);
                e.CellElement.BackColor3 = Color.FromArgb(0xED, 0xE4, 0xF3);
                e.CellElement.BackColor4 = Color.FromArgb(0xED, 0xE4, 0xF3);
                e.CellElement.DrawFill = true;
            }
        }

        private void RmiAdd_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;

            var f_NewSubContract = frm_Add_SubContract.Instance;

            Cursor.Current = Cursors.Default;
            f_NewSubContract.ShowDialog();
        }

        private void RmiHistory_Click(object sender, EventArgs e)
        {
            rbtHistory.PerformClick();
        }

        private void RmiPrint_Click(object sender, EventArgs e)
        {
            rbtPrint.PerformClick();
        }

        private void RadGridView1_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            e.ContextMenu = _rcm.DropDown;
        }

        private void RmiViewContract_Click(object sender, EventArgs e)
        {
            rbtPreview.PerformClick();
        }

        private void SubContract_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
                rbtPrint.PerformClick();
            if (e.KeyCode == Keys.F4)
                rbtPreview.PerformClick();
            if (e.KeyCode == Keys.F5)
                rbtHistory.PerformClick();
        }

        private void RadGridView1_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            if (e.RowElement.RowInfo is GridViewRowInfo)
                e.RowElement.RowInfo.MinHeight = 35;
        }

        private void RadGridView1_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (radGridView1.CurrentColumn is GridViewMultiComboBoxColumn)
                if (!isColumnAdded)
                {
                    isColumnAdded = true;

                    var editor = (RadMultiColumnComboBoxElement) radGridView1.ActiveEditor;
                    editor.EditorControl.MasterTemplate.AutoGenerateColumns = false;
                    editor.DropDownAnimationEnabled = true;

                    editor.DropDownSizingMode = SizingMode.UpDownAndRightBottom;
                    editor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    editor.AutoSizeDropDownToBestFit = true;
                    editor.AutoFilter = true;
                    editor.DisplayMember = "FullName2";

                    var column = new GridViewTextBoxColumn("UserId") {HeaderText = "Mã NV"};
                    editor.EditorControl.Columns.Add(column);

                    column = new GridViewTextBoxColumn("FullName2");
                    column.HeaderText = "Ho ten";
                    editor.EditorControl.Columns.Add(column);

                    column = new GridViewTextBoxColumn("FullName");
                    column.HeaderText = "Họ tên";
                    editor.EditorControl.Columns.Add(column);

                    var filter = new FilterDescriptor
                    {
                        PropertyName = "FullName2",
                        Operator = FilterOperator.Contains
                    };
                    editor.EditorControl.MasterTemplate.FilterDescriptors.Add(filter);
                }


            if (e.Row is GridViewRowInfo)
                e.Cancel = true;
            if (e.Row is GridViewFilteringRowInfo)
                e.Cancel = false;
        }

        private void SubContract_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }

        private void rbtPrint_Click(object sender, EventArgs e)
        {
            var _FullName = string.Empty;
            var _UserId = 0;
            var _EmployeeSubContractId = 0;
            if (radGridView1.CurrentRow.ViewTemplate.Parent != null)
            {
                _FullName = (radGridView1.CurrentRow.Parent as GridViewRowInfo).Cells["FullName"].Value.ToString();
                _UserId =
                    Convert.ToInt32((radGridView1.CurrentRow.Parent as GridViewRowInfo).Cells["UserId"].Value.ToString());

                _EmployeeSubContractId = Convert.ToInt32(radGridView1.CurrentRow.Cells["EmployeeSubContractId"].Value);
            }
            if ((_UserId > 0) && (_EmployeeSubContractId > 0) && (_FullName != string.Empty))
                try
                {
                    var rp = new ReportPreview(this, _EmployeeSubContractId, _UserId, _FullName, "Prt");
                }
                catch
                {
                    MessageBox.Show("Crystal report missing");
                }
        }

        private void rbtPreview_Click(object sender, EventArgs e)
        {
            var _FullName = string.Empty;
            var _UserId = 0;
            var _EmployeeSubContractId = 0;
            if (radGridView1.CurrentRow.ViewTemplate.Parent != null)
            {
                _FullName = (radGridView1.CurrentRow.Parent as GridViewRowInfo).Cells["FullName"].Value.ToString();
                _UserId =
                    Convert.ToInt32((radGridView1.CurrentRow.Parent as GridViewRowInfo).Cells["UserId"].Value.ToString());

                _EmployeeSubContractId = Convert.ToInt32(radGridView1.CurrentRow.Cells["EmployeeSubContractId"].Value);
            }
            if ((_UserId > 0) && (_EmployeeSubContractId > 0) && (_FullName != string.Empty))
                try
                {
                    var rp = new ReportPreview(this, _EmployeeSubContractId, _UserId, _FullName, "Pre");
                    rp.ShowDialog();
                }
                catch
                {
                    MessageBox.Show("Crystal report missing");
                }
        }

        private void rbtHistory_Click(object sender, EventArgs e)
        {
            frm_SubContractHistory sch = null;
            try
            {
                sch = new frm_SubContractHistory(this, Convert.ToInt32(radGridView1.CurrentRow.Cells["UserId"].Value),
                    radGridView1.CurrentRow.Cells["FullName"].Value.ToString());
            }
            catch
            {
            }
            if (sch != null)
                sch.ShowDialog();
        }
    }
}