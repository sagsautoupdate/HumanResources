using System;
using System.Data;
using System.Windows.Forms;
using HRMBLL.H0;
using HRMBLL.H1;
using HRMUtil;
using HumanResources.Forms.Contract.Contract;
using HumanResources.Reports.Report_Preview;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;
using PositionChangedEventArgs = Telerik.WinControls.UI.Data.PositionChangedEventArgs;

namespace HumanResources.Forms.Contract.SubContract
{
    public partial class frm_Add_SubContract : RadForm
    {
        private static frm_Add_SubContract s_Instance;
        private readonly string _OldContent = string.Empty;

        private frm_List_Contract _flc;
        private string _FullName;
        private string _ListUserIds = string.Empty;
        private string _SP = string.Empty;
        private string _SPValue = string.Empty;
        private int _SubContractId;
        private DataTable _TempTable;
        private int _UserId;
        private bool isColumnAdded;

        public frm_Add_SubContract()
        {
            InitializeComponent();
            Utilities.Utilities.SetScreenColor(this);

            InitData();

            var dr = EmployeeContractBLL.GetActiveContractByUserIdToDT(Convert.ToInt32(rmbEmployees.SelectedValue));
            if (dr != null)
            {
                var drScale = ScaleOfSalariesBLL.GetByName(dr["SOSPositionName"].ToString());

                ddlPosition.SelectedValue = drScale["ScaleOfSalaryId"];
                ddlLevel.SelectedIndex = Convert.ToInt32(dr["SalaryLevel"]) - 1;
                txtValue.Text =
                    StringFormat.SetFormatMoneyFinal(
                        Convert.ToDecimal(GetSOSValue(Convert.ToInt32(drScale["ScaleOfSalaryId"]),
                            Convert.ToInt32(dr["SalaryLevel"]))));
            }

            Utilities.Utilities.GridFormatting(radGridView1);
        }

        public frm_Add_SubContract(frm_List_Contract flc, int subContractId, int UserId, string FullName)
        {
            InitializeComponent();
            Utilities.Utilities.SetScreenColor(this);

            tableLayoutPanel1.Visible = false;

            _flc = flc;
            _SubContractId = subContractId;
            _UserId = UserId;
            _FullName = FullName;

            var dt = EmployeeSubContractBLL.GetAllBySubContractId(subContractId);
            BS_EmpSubContract.DataSource = dt;

            Text = string.Format("PHỤ LỤC HỢP ĐỒNG CỦA {0} (MÃ NV: {1})", FullName.ToUpper(), UserId);

            InitData();

            rmbEmployees.SelectedValue = UserId;
            ddlPosition.SelectedValue = dt.Rows[0]["ScaleOfSalaryId"];
            ddlLevel.SelectedValue = dt.Rows[0]["SalaryLevel"];
            txtValue.Text = dt.Rows[0]["SalaryValue"].ToString();
            txtDetail.Text = dt.Rows[0]["Detail"].ToString();
            txtDuration.Text = dt.Rows[0]["Duration"].ToString();

            var dr = EmployeeContractBLL.GetActiveContractByUserIdToDT(Convert.ToInt32(rmbEmployees.SelectedValue));
            if (dr != null)
            {
                var drScale = ScaleOfSalariesBLL.GetByName(dr["SOSPositionName"].ToString());

                ddlPosition.SelectedValue = drScale["ScaleOfSalaryId"];
                ddlLevel.SelectedIndex = Convert.ToInt32(dr["SalaryLevel"]) - 1;
                txtValue.Text =
                    StringFormat.SetFormatMoneyFinal(
                        Convert.ToDecimal(GetSOSValue(Convert.ToInt32(drScale["ScaleOfSalaryId"]),
                            Convert.ToInt32(dr["SalaryLevel"]))));
            }
            _OldContent =
                $"EmployeeSubContractId: {subContractId}, UserId: {dt.Rows[0]["UserId"]}, FromDate: '{dt.Rows[0]["FromDate"]}', ToDate: '{dt.Rows[0]["ToDate"]}', PositionId: {dt.Rows[0]["PositionId"]}, ScaleOfSalaryId: {dt.Rows[0]["ScaleOfSalaryId"]}, Value: {dt.Rows[0]["Value"]}, Remark: N'{dt.Rows[0]["Remark"]}', Detail: N'{dt.Rows[0]["Detail"]}', Duration: N'{dt.Rows[0]["Duration"]}', SubContractTypeId: {dt.Rows[0]["SubContractTypeId"]}";
            Utilities.Utilities.GridFormatting(radGridView1);
        }

        public frm_Add_SubContract(frm_List_SubContract fls, int subContractId, int UserId, string FullName)
        {
            InitializeComponent();
            Utilities.Utilities.SetScreenColor(this);

            tableLayoutPanel1.Visible = false;

            Fls = fls;
            _SubContractId = subContractId;
            _UserId = UserId;
            _FullName = FullName;

            var dt = EmployeeSubContractBLL.GetAllBySubContractId(subContractId);
            BS_EmpSubContract.DataSource = dt;

            Text = string.Format("PHỤ LỤC HỢP ĐỒNG CỦA {0} (MÃ NV: {1})", FullName.ToUpper(), UserId);

            InitData();

            rmbEmployees.SelectedValue = UserId;
            ddlPosition.SelectedValue = dt.Rows[0]["ScaleOfSalaryId"];
            ddlLevel.SelectedValue = dt.Rows[0]["SalaryLevel"];
            txtValue.Text = dt.Rows[0]["SalaryValue"].ToString();
            txtDetail.Text = dt.Rows[0]["Detail"].ToString();
            txtDuration.Text = dt.Rows[0]["Duration"].ToString();

            var dr = EmployeeContractBLL.GetActiveContractByUserIdToDT(Convert.ToInt32(rmbEmployees.SelectedValue));
            if (dr != null)
            {
                var drScale = ScaleOfSalariesBLL.GetByName(dr["SOSPositionName"].ToString());

                ddlPosition.SelectedValue = drScale["ScaleOfSalaryId"];
                ddlLevel.SelectedIndex = Convert.ToInt32(dr["SalaryLevel"]) - 1;
                txtValue.Text =
                    StringFormat.SetFormatMoneyFinal(
                        Convert.ToDecimal(GetSOSValue(Convert.ToInt32(drScale["ScaleOfSalaryId"]),
                            Convert.ToInt32(dr["SalaryLevel"]))));
            }
            _OldContent =
                $"EmployeeSubContractId: {subContractId}, UserId: {dt.Rows[0]["UserId"]}, FromDate: '{dt.Rows[0]["FromDate"]}', ToDate: '{dt.Rows[0]["ToDate"]}', PositionId: {dt.Rows[0]["PositionId"]}, ScaleOfSalaryId: {dt.Rows[0]["ScaleOfSalaryId"]}, Value: {dt.Rows[0]["Value"]}, Remark: N'{dt.Rows[0]["Remark"]}', Detail: N'{dt.Rows[0]["Detail"]}', Duration: N'{dt.Rows[0]["Duration"]}'";
            Utilities.Utilities.GridFormatting(radGridView1);
        }

        public frm_Add_SubContract(frm_SubContractHistory fsch, int subContractId, int UserId, string FullName)
        {
            InitializeComponent();
            Utilities.Utilities.SetScreenColor(this);

            tableLayoutPanel1.Visible = false;

            Fsch = fsch;
            _SubContractId = subContractId;
            _UserId = UserId;
            _FullName = FullName;

            var dt = EmployeeSubContractBLL.GetAllBySubContractId(subContractId);
            BS_EmpSubContract.DataSource = dt;

            Text = string.Format("PHỤ LỤC HỢP ĐỒNG CỦA {0} (MÃ NV: {1})", FullName.ToUpper(), UserId);

            InitData();

            rmbEmployees.SelectedValue = UserId;
            ddlPosition.SelectedValue = dt.Rows[0]["ScaleOfSalaryId"];
            ddlLevel.SelectedValue = dt.Rows[0]["SalaryLevel"];
            txtValue.Text = dt.Rows[0]["SalaryValue"].ToString();
            txtDetail.Text = dt.Rows[0]["Detail"].ToString();
            txtDuration.Text = dt.Rows[0]["Duration"].ToString();

            var dr = EmployeeContractBLL.GetActiveContractByUserIdToDT(Convert.ToInt32(rmbEmployees.SelectedValue));
            if (dr != null)
            {
                var drScale = ScaleOfSalariesBLL.GetByName(dr["SOSPositionName"].ToString());

                ddlPosition.SelectedValue = drScale["ScaleOfSalaryId"];
                ddlLevel.SelectedIndex = Convert.ToInt32(dr["SalaryLevel"]) - 1;
                txtValue.Text =
                    StringFormat.SetFormatMoneyFinal(
                        Convert.ToDecimal(GetSOSValue(Convert.ToInt32(drScale["ScaleOfSalaryId"]),
                            Convert.ToInt32(dr["SalaryLevel"]))));
            }
            _OldContent =
                $"EmployeeSubContractId: {subContractId}, UserId: {dt.Rows[0]["UserId"]}, FromDate: '{dt.Rows[0]["FromDate"]}', ToDate: '{dt.Rows[0]["ToDate"]}', PositionId: {dt.Rows[0]["PositionId"]}, ScaleOfSalaryId: {dt.Rows[0]["ScaleOfSalaryId"]}, Value: {dt.Rows[0]["Value"]}, Remark: N'{dt.Rows[0]["Remark"]}', Detail: N'{dt.Rows[0]["Detail"]}', Duration: N'{dt.Rows[0]["Duration"]}'";
            Utilities.Utilities.GridFormatting(radGridView1);
        }

        public static frm_Add_SubContract Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_Add_SubContract();
                return s_Instance;
            }
        }

        public frm_List_SubContract Fls { get; set; }

        public frm_SubContractHistory Fsch { get; set; }

        private void SubContract_Load(object sender, EventArgs e)
        {
            Utilities.Utilities.SetScreenColor(this);
            FormClosed += SubContract_FormClosed;
            KeyDown += SubContract_KeyDown;

            radGridView1.CellBeginEdit += RadGridView1_CellBeginEdit;
            radGridView1.RowFormatting += RadGridView1_RowFormatting;
            radGridView1.CellValueChanged += RadGridView1_CellValueChanged;
            ddlPosition.SelectedValueChanged += RadDropDownList1_SelectedValueChanged;
            ddlLevel.SelectedIndexChanged += RadDropDownList2_SelectedIndexChanged;
            radGridView1.ContextMenuOpening += RadGridView1_ContextMenuOpening;
            radGridView1.CellEditorInitialized += RadGridView1_CellEditorInitialized;

            InitTempTableStructure();

            rmbEmployees.SelectedValueChanged += RmbEmployees_SelectedValueChanged;
        }


        private void InitData()
        {
            BS_Emp.DataSource = EmployeeContractBLL.GetAllToDT();
            BS_PositionContract.DataSource = PositionsBLL.GetAllToDT();
            BS_ScaleOfSalary.DataSource = ScaleOfSalariesBLL.GetAll();
            BS_ScaleOfSalary1.DataSource = ScaleOfSalariesBLL.GetAll();

            radMultiColumnComboBox1.DataSource = ContractTypesBLL.GetAllToDT(3);
            radMultiColumnComboBox1.DisplayMember = "ContractTypeName";
            radMultiColumnComboBox1.ValueMember = "ContractTypeId";
            radMultiColumnComboBox1.SelectedIndex = 1;

            BS_SubContractType.DataSource = ContractTypesBLL.GetAllToDT(3);
        }

        private double GetSOSValue(int ScaleOfSalaryId, int Level)
        {
            double Value = 0;
            var dt = ScaleOfSalariesBLL.GetOne(ScaleOfSalaryId);
            switch (Level)
            {
                case 1:
                    Value = double.Parse(dt["Value1"].ToString());
                    break;
                case 2:
                    Value = double.Parse(dt["Value2"].ToString());
                    break;
                case 3:
                    Value = double.Parse(dt["Value3"].ToString());
                    break;
            }
            return Value;
        }

        private void InitTempTableStructure()
        {
            _TempTable = new DataTable();
            foreach (var column in radGridView1.Columns)
                _TempTable.Columns.Add(column.Name, column.DataType);
        }

        private void RadGridView1_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            var editor = e.ActiveEditor as RadDropDownListEditor;
            if (editor != null)
            {
                var el = editor.EditorElement as RadDropDownListEditorElement;
                el.DropDownSizingMode = SizingMode.UpDownAndRightBottom;
                el.AutoCompleteMode = AutoCompleteMode.Suggest;
                el.AutoCompleteSuggest.SuggestMode = SuggestMode.Contains;
            }
        }

        private void RmiHistory_Click(object sender, EventArgs e)
        {
            rbtHistory.PerformClick();
        }

        private void RmiPrint_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(radGridView1.CurrentRow.Cells["EmployeeSubContractId"].Value) != 0)
            {
                var rp = new ReportPreview(this,
                    Convert.ToInt32(radGridView1.CurrentRow.Cells["EmployeeSubContractId"].Value),
                    Convert.ToInt32(radGridView1.CurrentRow.Cells["UserId"].Value),
                    radGridView1.CurrentRow.Cells["FullName"].Value.ToString(), "Prt");
            }
            else
            {
                MessageBox.Show("Lỗi: chưa lưu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RadGridView1_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            var rmiViewContract = new RadMenuItem("Xem hợp đồng");
            rmiViewContract.Click += RmiViewContract_Click;
            var rmiPrint = new RadMenuItem("In hợp đồng");
            rmiPrint.Click += RmiPrint_Click;
            var rmiHistory = new RadMenuItem("Xem lịch sử");
            rmiHistory.Click += RmiHistory_Click;

            var separator = new RadMenuSeparatorItem();
            e.ContextMenu.Items.Add(separator);
            e.ContextMenu.Items.Add(rmiViewContract);
            e.ContextMenu.Items.Add(rmiPrint);
            e.ContextMenu.Items.Add(rmiHistory);
        }

        private void RmiViewContract_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(radGridView1.CurrentRow.Cells["EmployeeSubContractId"].Value) != 0)
            {
                var rp = new ReportPreview(this,
                    Convert.ToInt32(radGridView1.CurrentRow.Cells["EmployeeSubContractId"].Value),
                    Convert.ToInt32(radGridView1.CurrentRow.Cells["UserId"].Value),
                    radGridView1.CurrentRow.Cells["FullName"].Value.ToString(), "Pre");
                rp.ShowDialog();
            }
            else
            {
                MessageBox.Show("Lỗi: chưa lưu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SubContract_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                rbtAdd.PerformClick();
            if (e.KeyCode == Keys.F2)
                rbtSavetvtg.PerformClick();
            if (e.KeyCode == Keys.F3)
                rbtPrint.PerformClick();
            if (e.KeyCode == Keys.F4)
                rbtPreview.PerformClick();
            if (e.KeyCode == Keys.F5)
                rbtHistory.PerformClick();
        }

        private void RadDropDownList2_SelectedIndexChanged(object sender, PositionChangedEventArgs e)
        {
            try
            {
                txtValue.Text =
                    StringFormat.SetFormatMoneyFinal(
                        Convert.ToDecimal(GetSOSValue(Convert.ToInt32(ddlPosition.SelectedValue),
                            Convert.ToInt32(ddlLevel.SelectedIndex + 1))));
            }
            catch
            {
            }
        }

        private void RadDropDownList1_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtValue.Text =
                    StringFormat.SetFormatMoneyFinal(
                        Convert.ToDecimal(GetSOSValue(Convert.ToInt32(ddlPosition.SelectedValue),
                            Convert.ToInt32(ddlLevel.SelectedIndex + 1))));
            }
            catch
            {
            }
        }

        private void RadGridView1_CellValueChanged(object sender, GridViewCellEventArgs e)
        {
            if ((e.Column.Name == "ScaleOfSalaryId") || (e.Column.Name == "Value"))
                try
                {
                    e.Row.Cells["SalaryValue"].Value =
                        StringFormat.SetFormatMoneyFinal(
                            Convert.ToDecimal(
                                GetSOSValue(Convert.ToInt32(e.Row.Cells["ScaleOfSalaryId"].Value),
                                    Convert.ToInt32(e.Row.Cells["Value"].Value))));
                }
                catch
                {
                    e.Row.Cells["SalaryValue"].Value = 0;
                }
        }

        private void RmbEmployees_SelectedValueChanged(object sender, EventArgs e)
        {
            var dr = EmployeeContractBLL.GetActiveContractByUserIdToDT(Convert.ToInt32(rmbEmployees.SelectedValue));
            if (dr != null)
            {
                var drScale = ScaleOfSalariesBLL.GetByName(dr["SOSPositionName"].ToString());

                ddlPosition.SelectedValue = drScale["ScaleOfSalaryId"];
                ddlLevel.SelectedIndex = Convert.ToInt32(dr["SalaryLevel"]) - 1;
                txtValue.Text =
                    StringFormat.SetFormatMoneyFinal(
                        Convert.ToDecimal(GetSOSValue(Convert.ToInt32(drScale["ScaleOfSalaryId"]),
                            Convert.ToInt32(dr["SalaryLevel"]))));
            }
        }

        private void RadGridView1_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            if (e.RowElement.RowInfo is GridViewRowInfo)
                e.RowElement.RowInfo.MinHeight = 35;
        }

        private void RadGridView1_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (radGridView1.CurrentColumn is GridViewMultiComboBoxColumn)
            {
                ((RadMultiColumnComboBoxElement) radGridView1.ActiveEditor).Initialize();
                ((RadMultiColumnComboBoxElement) radGridView1.ActiveEditor).Columns.Clear();
                {
                    isColumnAdded = true;
                    var colName = e.Column.Name;
                    switch (colName)
                    {
                        case "ContractTypeId":
                        {
                            var editor = (RadMultiColumnComboBoxElement) radGridView1.ActiveEditor;
                            editor.EditorControl.MasterTemplate.AutoGenerateColumns = false;
                            editor.DropDownAnimationEnabled = true;
                            editor.DropDownSizingMode = SizingMode.UpDownAndRightBottom;
                            editor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                            editor.AutoSizeDropDownToBestFit = true;
                            editor.AutoFilter = true;

                            editor.DisplayMember = "ContractTypeName";
                            editor.ValueMember = "ContractTypeId";

                            var column = new GridViewTextBoxColumn("ContractTypeName") {HeaderText = "Loại phụ lục"};
                            editor.EditorControl.Columns.Add(column);

                            column = new GridViewTextBoxColumn("ContractTypeId");
                            column.HeaderText = "Ho ten";
                            column.IsVisible = false;
                            editor.EditorControl.Columns.Add(column);
                        }
                            break;

                        case "PositionId":
                        {
                            var editor = (RadMultiColumnComboBoxElement) radGridView1.ActiveEditor;
                            editor.EditorControl.MasterTemplate.AutoGenerateColumns = false;
                            editor.DropDownAnimationEnabled = true;

                            editor.DropDownSizingMode = SizingMode.UpDownAndRightBottom;
                            editor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                            editor.AutoSizeDropDownToBestFit = true;
                            editor.AutoFilter = true;
                            editor.DisplayMember = "PositionName";

                            var filter = new FilterDescriptor
                            {
                                PropertyName = "PositionName2",
                                Operator = FilterOperator.Contains
                            };
                            editor.EditorControl.MasterTemplate.FilterDescriptors.Add(filter);

                            var column = new GridViewTextBoxColumn("PositionId")
                            {
                                HeaderText = "Mã chức danh",
                                IsVisible = false
                            };
                            editor.EditorControl.Columns.Add(column);

                            column = new GridViewTextBoxColumn("PositionName");
                            column.HeaderText = "Chức danh";
                            editor.EditorControl.Columns.Add(column);

                            column = new GridViewTextBoxColumn("PositionName2");
                            column.HeaderText = "Chuc danh";
                            editor.EditorControl.Columns.Add(column);
                        }
                            break;

                        case "ScaleOfSalaryId":
                        {
                            var editor = (RadMultiColumnComboBoxElement) radGridView1.ActiveEditor;
                            editor.EditorControl.MasterTemplate.AutoGenerateColumns = false;
                            editor.DropDownAnimationEnabled = true;

                            editor.DropDownSizingMode = SizingMode.UpDownAndRightBottom;
                            editor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                            editor.AutoSizeDropDownToBestFit = true;
                            editor.AutoFilter = true;
                            editor.DisplayMember = "PositionName";

                            var filter = new FilterDescriptor
                            {
                                PropertyName = "PositionName2",
                                Operator = FilterOperator.Contains
                            };
                            editor.EditorControl.MasterTemplate.FilterDescriptors.Add(filter);

                            var column = new GridViewTextBoxColumn("ScaleOfSalaryId")
                            {
                                HeaderText = "Chức danh",
                                IsVisible = false
                            };
                            editor.EditorControl.Columns.Add(column);

                            column = new GridViewTextBoxColumn("PositionName");
                            column.HeaderText = "Chức danh";
                            editor.EditorControl.Columns.Add(column);

                            column = new GridViewTextBoxColumn("PositionName2");
                            column.HeaderText = "Chuc danh";
                            editor.EditorControl.Columns.Add(column);
                        }
                            break;
                    }
                }
            }
            if ((e.Column.Name == "UserId") || (e.Column.Name == "FullName") || (e.Column.Name == "SalaryValue") ||
                (e.Column.Name == "FullName2"))
                e.Cancel = true;
        }

        private void SubContract_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }

        private void rbtAdd_Click(object sender, EventArgs e)
        {
            var dr = EmployeeContractBLL.GetActiveContractByUserIdToDT(Convert.ToInt32(rmbEmployees.SelectedValue));
            var newRow = _TempTable.NewRow();

            newRow["EmployeeSubContractId"] = 0;
            newRow["UserId"] = rmbEmployees.SelectedValue;
            newRow["FullName"] = rmbEmployees.SelectedValue;
            newRow["FromDate"] = radDateTimePicker1.Value;
            newRow["PositionId"] = dr["PositionId"];
            newRow["ScaleOfSalaryId"] = ddlPosition.SelectedValue;

            newRow["Value"] = (ddlLevel.SelectedIndex + 1).ToString();
            newRow["SalaryValue"] = txtValue.Text;
            newRow["FullName2"] = dr["FullName2"];
            newRow["EmployeeContractId"] = dr["EmployeeContractId"];
            newRow["Detail"] = txtDetail.Text.Trim();
            newRow["Duration"] = txtDuration.Text.Trim();
            newRow["SubContractTypeId"] = radMultiColumnComboBox1.SelectedValue;


            _TempTable.Rows.InsertAt(newRow, 0);
            radGridView1.DataSource = _TempTable;
            Utilities.Utilities.GridFormatting(radGridView1);
        }

        private void rbtSavetvtg_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            foreach (var dr in radGridView1.Rows)
            {
                var _EmployeeSubContractId = dr.Cells["EmployeeSubContractId"].Value == DBNull.Value
                    ? 0
                    : Convert.ToInt32(dr.Cells["EmployeeSubContractId"].Value);
                var _EmployeeContractId = dr.Cells["EmployeeContractId"].Value == DBNull.Value
                    ? 0
                    : Convert.ToInt32(dr.Cells["EmployeeContractId"].Value);
                var _UserId = dr.Cells["UserId"].Value == DBNull.Value ? 0 : Convert.ToInt32(dr.Cells["UserId"].Value);
                var _CreatedDate = DateTime.Now;
                var _FromDate = Convert.ToDateTime(dr.Cells["FromDate"].Value);
                var _ToDate = FormatDate.GetSQLDateMinValue;
                var _PositionId = dr.Cells["PositionId"].Value == DBNull.Value
                    ? 0
                    : Convert.ToInt32(dr.Cells["PositionId"].Value);
                var _ScaleOfSalaryId = dr.Cells["ScaleOfSalaryId"].Value == DBNull.Value
                    ? 0
                    : Convert.ToInt32(dr.Cells["ScaleOfSalaryId"].Value);
                var _Value = dr.Cells["Value"].Value == DBNull.Value ? 0 : Convert.ToInt32(dr.Cells["Value"].Value);
                var _Remark = (dr.Cells["Remark"].Value == DBNull.Value) ||
                              (dr.Cells["Remark"].Value.ToString().Length == 0)
                    ? ""
                    : dr.Cells["Remark"].Value.ToString().Trim();
                var _Detail = (dr.Cells["Detail"].Value == DBNull.Value) ||
                              (dr.Cells["Detail"].Value.ToString().Length == 0)
                    ? string.Empty
                    : dr.Cells["Detail"].Value.ToString().Trim();
                var _Duration = (dr.Cells["Duration"].Value == DBNull.Value) ||
                                (dr.Cells["Duration"].Value.ToString().Length == 0)
                    ? string.Empty
                    : dr.Cells["Duration"].Value.ToString().Trim();
                var _SubContractTypeId = dr.Cells["SubContractTypeId"].Value == DBNull.Value
                    ? 0
                    : Convert.ToInt32(dr.Cells["SubContractTypeId"].Value);

                if (_EmployeeContractId == 0)
                {
                    MessageBox.Show("Lỗi: Chưa có hợp đồng");
                }
                else
                {
                    if ((_PositionId == 0) || (_ScaleOfSalaryId == 0) || (_Value == 0) || (_SubContractTypeId == 0))
                    {
                        MessageBox.Show("Lỗi: Thiếu thông tin");
                    }
                    else
                    {
                        if (_EmployeeSubContractId == 0)
                        {
                            EmployeeSubContractBLL.Insert(_EmployeeContractId, _UserId, _CreatedDate, _FromDate, _ToDate,
                                _PositionId, _ScaleOfSalaryId, _Value, _Detail, _Duration, _SubContractTypeId);
                            _SP = "Ins_H0_EmployeeSubContract";
                            _SPValue =
                                $"EmployeeContractId: {_EmployeeContractId}, UserId: {_UserId}, CreatedDate: '{_CreatedDate}', FromDate: '{_FromDate}', ToDate: '{_ToDate}', PositionId: {_PositionId}, ScaleOfSalaryId: {_ScaleOfSalaryId}, Value: {_Value}, Remark: N'{_Remark}', Detail: N'{_Detail}', Duration: N'{_Duration}', SubContractTypeId: {_SubContractTypeId}";
                            Utilities.Utilities.SaveHRMLog("H0_EmployeeSubContract", _SP, _SPValue, _OldContent);
                        }
                        else
                        {
                            EmployeeSubContractBLL.Update(_EmployeeSubContractId, _UserId, _FromDate, _ToDate,
                                _PositionId, _ScaleOfSalaryId, _Value, _Detail, _Duration, _SubContractTypeId);
                            _SP = "Upd_H0_EmployeeSubContract_By_UserId";
                            _SPValue =
                                $"EmployeeSubContractId: {_EmployeeSubContractId}, UserId: {_UserId}, FromDate: '{_FromDate}', ToDate: '{_ToDate}', PositionId: {_PositionId}, ScaleOfSalaryId: {_ScaleOfSalaryId}, Value: {_Value}, Remark: N'{_Remark}', Detail: N'{_Detail}', Duration: N'{_Duration}', SubContractTypeId: {_SubContractTypeId}";
                            Utilities.Utilities.SaveHRMLog("H0_EmployeeSubContract", _SP, _SPValue, _OldContent);
                        }
                        _ListUserIds += _UserId + ",";
                    }
                }
            }
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Lưu thành công");
            radGridView1.DataSource =
                EmployeeSubContractBLL.GetAllActiveByUserId(
                    Util.RejectLastComma(_ListUserIds));
            _TempTable.Rows.Clear();
        }

        private void rbtPrint_Click(object sender, EventArgs e)
        {
            if (radGridView1.SelectedRows.Count > 1)
            {
                if (MessageBox.Show("In nhiều phụ lục?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.Yes)
                    foreach (var row in radGridView1.SelectedRows)
                        try
                        {
                            var rp = new ReportPreview(this, Convert.ToInt32(row.Cells["EmployeeSubContractId"].Value),
                                Convert.ToInt32(row.Cells["UserId"].Value), row.Cells["FullName"].Value.ToString(),
                                "Prt");
                        }
                        catch
                        {
                            MessageBox.Show("Crystal report missing");
                        }
            }
            else
            {
                try
                {
                    var rp = new ReportPreview(this,
                        Convert.ToInt32(radGridView1.CurrentRow.Cells["EmployeeSubContractId"].Value),
                        Convert.ToInt32(radGridView1.CurrentRow.Cells["UserId"].Value),
                        radGridView1.CurrentRow.Cells["FullName"].Value.ToString(), "Prt");
                }
                catch
                {
                    MessageBox.Show("Crystal report missing");
                }
            }
        }

        private void rbtPreview_Click(object sender, EventArgs e)
        {
            try
            {
                var rp = new ReportPreview(this,
                    Convert.ToInt32(radGridView1.CurrentRow.Cells["EmployeeSubContractId"].Value),
                    Convert.ToInt32(radGridView1.CurrentRow.Cells["UserId"].Value),
                    radGridView1.CurrentRow.Cells["FullName"].Value.ToString(), "Pre");
                rp.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Crystal report missing");
            }
        }

        private void rbtHistory_Click(object sender, EventArgs e)
        {
            var sch = new frm_SubContractHistory(this, Convert.ToInt32(radGridView1.CurrentRow.Cells["UserId"].Value),
                radGridView1.CurrentRow.Cells["FullNam"].Value.ToString());
            Utilities.Utilities.SetScreenResolution(sch);
            Utilities.Utilities.SetFormSize(sch);
            sch.ShowDialog();
        }
    }
}