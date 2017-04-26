using System;
using System.Windows.Forms;
using HRMBLL.H0;
using HumanResources.Forms.Contract.Contract;
using HumanResources.Properties;
using HumanResources.Reports.Report_Preview;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Contract.SubContract
{
    public partial class frm_SubContractHistory : RadForm
    {
        private readonly RadContextMenu _rcm = Utilities.Utilities.DefaultRadContextMenu();
        private frm_List_Contract _flc;


        private frm_List_SubContract _lsc;
        private string _OldContent = string.Empty;
        private string _SP = string.Empty;
        private string _SPValue = string.Empty;

        private bool isColumnAdded;

        public frm_SubContractHistory()
        {
            InitializeComponent();
            Utilities.Utilities.SetScreenColor(this);

            InitData();
        }

        public frm_SubContractHistory(frm_List_SubContract sc, int UserId, string FullName)
        {
            InitializeComponent();
            Utilities.Utilities.SetScreenColor(this);

            _lsc = sc;
            this.UserId = UserId;
            this.FullName = FullName;
            Text = string.Format("Lịch sử PLHĐ của {0} (Mã NV: {1})", this.UserId, this.FullName);

            InitData();
        }

        public frm_SubContractHistory(frm_List_Contract flc, int UserId, string FullName)
        {
            InitializeComponent();
            Utilities.Utilities.SetScreenColor(this);

            _flc = flc;
            this.UserId = UserId;
            this.FullName = FullName;
            Text = string.Format("Lịch sử PLHĐ của {0} (Mã NV: {1})", this.UserId, this.FullName);

            InitData();
        }

        public frm_SubContractHistory(frm_Add_SubContract sc, int UserId, string FullName)
        {
            InitializeComponent();
            Utilities.Utilities.SetScreenColor(this);

            Sc = sc;
            this.UserId = UserId;
            this.FullName = FullName;
            Text = string.Format("Lịch sử PLHĐ của {0} (Mã NV: {1})", this.UserId, this.FullName);

            InitData();
        }

        public frm_Add_SubContract Sc { get; set; }

        public int UserId { get; set; }

        public string FullName { get; set; }

        private void SubContractHistory_Load(object sender, EventArgs e)
        {
            AddMenu();
            _rcm.Items["rmiAdd"].Enabled = false;
            _rcm.Items["rmiDelete"].Enabled = false;
            _rcm.Items["rmiRefresh"].Enabled = false;
            _rcm.Items["rmiEdit"].Click += Frm_SubContractHistory_Click;

            rmbEmployees.SelectedValue = UserId;

            radGridView1.ContextMenuOpening += RadGridView1_ContextMenuOpening;
            radGridView1.RowFormatting += RadGridView1_RowFormatting;
            radGridView1.CellBeginEdit += RadGridView1_CellBeginEdit;
            rmbEmployees.SelectedValueChanged += RmbEmployees_SelectedValueChanged;
            radGridView1.CellDoubleClick += RadGridView1_CellDoubleClick;
            radGridView1.CellValueChanged += RadGridView1_CellValueChanged;
        }

        private void Frm_SubContractHistory_Click(object sender, EventArgs e)
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


        private void AddMenu()
        {
            var _rmsi = new RadMenuSeparatorItem();
            _rcm.Items.Add(_rmsi);

            var _rmiPreviewContract = new RadMenuItem
            {
                Text = "Xem hợp đồng",
                Image = Resources.Preview,
                Name = "rmiPreviewContract"
            };
            _rmiPreviewContract.Click += _rmiPreviewContract_Click;
            ;
            _rcm.Items.Add(_rmiPreviewContract);
        }

        private void InitData()
        {
            BS_Emp.DataSource = EmployeeContractBLL.GetAllToDT();
            BS_EmpSubContract.DataSource = EmployeeSubContractBLL.GetAllByUserIdDT(UserId);

            Utilities.Utilities.GridFormatting(radGridView1);
        }


        private void _rmiPreviewContract_Click(object sender, EventArgs e)
        {
            var rp = new ReportPreview(this,
                Convert.ToInt32(radGridView1.CurrentRow.Cells["EmployeeSubContractId"].Value),
                Convert.ToInt32(radGridView1.CurrentRow.Cells["UserId"].Value),
                radGridView1.CurrentRow.Cells["FullName"].Value.ToString(), "Pre");
            rp.ShowDialog();
        }

        private void RadGridView1_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            e.ContextMenu = _rcm.DropDown;
        }

        private void RadGridView1_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
        }

        private void RmbEmployees_SelectedValueChanged(object sender, EventArgs e)
        {
            BS_EmpSubContract.DataSource =
                EmployeeSubContractBLL.GetAllByUserIdDT(Convert.ToInt32(rmbEmployees.SelectedValue));

            Utilities.Utilities.GridFormatting(radGridView1);
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
            if ((e.Column.Name == "UserId") || (e.Column.Name == "FullName") || (e.Column.Name == "SalaryValue") ||
                (e.Column.Name == "FullName2"))
                e.Cancel = true;
        }

        private void RadGridView1_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            if (e.RowElement.RowInfo is GridViewRowInfo)
                e.RowElement.RowInfo.MinHeight = 35;
        }

        private void rbtSavetvtg_Click(object sender, EventArgs e)
        {
            var Active = Convert.ToBoolean(radGridView1.CurrentRow.Cells["Active"].Value);
            try
            {
                var _ID = Convert.ToInt32(radGridView1.CurrentRow.Cells["EmployeeSubContractId"].Value);
                EmployeeSubContractBLL.UpdateActive(
                    Convert.ToInt32(radGridView1.CurrentRow.Cells["EmployeeSubContractId"].Value), Active);
                _SP = "Upd_H0_EmployeeSubContract_Active";
                _SPValue = $"EmployeeSubContractId: {_ID}, Active: {Active}";
                MessageBox.Show("Cập nhật thành công!");
            }
            catch
            {
                MessageBox.Show("Thất bại!");
            }
            finally
            {
                Utilities.Utilities.SaveHRMLog("H0_EmployeeSubContract", _SP, _SPValue, _OldContent);
            }
        }

        private void RadGridView1_CellValueChanged(object sender, GridViewCellEventArgs e)
        {
            try
            {
                var _ID = Convert.ToInt32(e.Row.Cells["EmployeeSubContractId"].Value);
                var Active = Convert.ToBoolean(e.Row.Cells["Active"].Value);
                _OldContent = $"EmployeeSubContractId: {_ID}, Active: {Active}";
            }
            catch
            {
            }
        }

        private void frm_SubContractHistory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                rbtSavetvtg.PerformClick();
        }

        private void rbtAdd_Click(object sender, EventArgs e)
        {
            BS_EmpSubContract.DataSource =
                EmployeeSubContractBLL.GetAllByUserIdDT(Convert.ToInt32(rmbEmployees.SelectedValue));

            Utilities.Utilities.GridFormatting(radGridView1);
        }
    }
}