using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using HRMBLL.BLLHelper;
using HRMBLL.H0;
using HRMBLL.H1;
using HRMUtil;
using HumanResources.Reports.Report_Preview;
using Telerik.WinControls;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Contract.Contract
{
    public partial class frm_Add_EmployeeContract : RadForm
    {
        private static frm_Add_EmployeeContract s_Instance;
        private readonly string _FullName;
        private readonly string _OldContent = string.Empty;
        private readonly int _Type;

        private readonly int _UserId;
        private frm_List_Contract _flc;
        private bool _IsShown;
        private string _ListUserIds = string.Empty;

        private string _SP = string.Empty;
        private string _SPValue = string.Empty;
        private DataTable _TempTable;
        private bool isColumnAdded;

        public frm_Add_EmployeeContract()
        {
            InitializeComponent();
            Utilities.Utilities.SetScreenColor(this);

            InitData();
            Text = "THÊM MỚI HỢP ĐỒNG LAO ĐỘNG";
        }

        public frm_Add_EmployeeContract(frm_List_Contract flc, int DepartmentId)
        {
            InitializeComponent();
            Utilities.Utilities.SetScreenColor(this);

            _flc = flc;
            this.DepartmentId = DepartmentId;

            InitData();
            Text = "THÊM MỚI HỢP ĐỒNG LAO ĐỘNG";
            BindData();
        }

        public frm_Add_EmployeeContract(frm_List_Contract flc, int UserId, string FullName, int Type)
        {
            InitializeComponent();
            Utilities.Utilities.SetScreenColor(this);

            _flc = flc;
            _UserId = UserId;
            _FullName = FullName;
            _Type = Type;
            DepartmentId = DepartmentId;

            InitData();
            if (_Type == 0)
            {
                Text = string.Format("HỢP ĐỒNG CỦA {0} (Mã NV: {1})", _FullName.ToUpper(), _UserId);

                var dt = EmployeeContractBLL.DT_GetActiveContractByUserId(_UserId);
                radGridView1.DataSource = dt;

                _OldContent =
                    $"EmployeeContractId: {dt.Rows[0]["EmployeeContractId"]}, UserId: {dt.Rows[0]["UserId"]}, PositionId: {dt.Rows[0]["PositionId"]}, ContractTypeId: {dt.Rows[0]["ContractTypeId"]}, FromDate: '{dt.Rows[0]["FromDate"]}', ToDate: '{dt.Rows[0]["ToDate"]}', ContractName: N'{dt.Rows[0]["ContractName"]}', RepresentativeOfCompany: N'{dt.Rows[0]["RepresentativeOfCompany"]}', CompanyName: N'{dt.Rows[0]["CompanyName"]}', WorkingHour: {dt.Rows[0]["WorkingHour"]}, Overtime: {dt.Rows[0]["Overtime"]}, SalaryLevel: {dt.Rows[0]["SalaryLevel"]}, ScaleOfSalaryId: {dt.Rows[0]["ScaleOfSalaryId"]}, ContractTitle: N'{dt.Rows[0]["ContractTitle"]}', Office: '{dt.Rows[0]["Office"]}', IsReplaced: {dt.Rows[0]["IsReplaced"]}, PreviousEmployeeContractId: {dt.Rows[0]["PreviousEmployeeContractId"]}, CntRemark: N'{dt.Rows[0]["CntRemark"]}'";
            }
            else
            {
                Text = string.Format("LỊCH SỬ HỢP ĐỒNG CỦA {0} (Mã NV: {1})", _FullName.ToUpper(), _UserId);

                var dt = EmployeeContractBLL.GetByUserIdToDT(_UserId, -1);
                radGridView1.DataSource = dt;
            }
            if (radGridView1.Rows.Count > 0)
            {
            }
            tableLayoutPanel1.Visible = false;
        }

        public static frm_Add_EmployeeContract Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_Add_EmployeeContract();
                return s_Instance;
            }
        }

        public int DepartmentId { get; set; }

        private void frm_EmployeeContract_Load(object sender, EventArgs e)
        {
            rdpFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            rdpToDate.Text = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");

            txtCompanyName.Text = clsGlobal.CompanyName;
            txtRepresentation.Text = clsGlobal.Representation;

            if (Convert.ToInt32(ddlContractType.SelectedValue) == 6)
                txtContractTitle.Text = "HỢP ĐỒNG THỬ VIỆC".ToUpper();
            else
                txtContractTitle.Text = "HỢP ĐỒNG LAO ĐỘNG".ToUpper();
            Utilities.Utilities.GridFormatting(radGridView1);
            radGridView1.CellEditorInitialized += RadGridView1_CellEditorInitialized;
            ddlScaleOfSalary.SelectedValueChanged += ddlScaleOfSalary_SelectedValueChanged;
            ddlScaleOfSalaryLevel.SelectedValueChanged += DdlScaleOfSalaryLevel_SelectedValueChanged;
            ddlContractType.SelectedValueChanged += DdlContractType_SelectedValueChanged;
            rdpFromDate.ValueChanged += RdpFromDate_ValueChanged;
            radGridView1.CellFormatting += RadGridView1_CellFormatting;
            FormClosed += Frm_EmployeeContract_FormClosed;
            KeyDown += Frm_EmployeeContract_KeyDown;

            rmbEmployees.SelectedValueChanged += RmbEmployees_SelectedValueChanged;
        }

        private void radGridView1_CellValueChanged(object sender, GridViewCellEventArgs e)
        {
            if ((Convert.ToInt32(e.Row.Cells["ContractTypeId"].Value) == 77) ||
                (Convert.ToInt32(e.Row.Cells["ContractTypeId"].Value) == 78))
            {
                e.Row.Cells["ToDate"].ReadOnly = false;
            }
            else
            {
                e.Row.Cells["ToDate"].ReadOnly = true;
                e.Row.Cells["ToDate"].Value = Todate(Convert.ToInt32(e.Row.Cells["ContractTypeId"].Value),
                    Convert.ToDateTime(e.Row.Cells["FromDate"].Value));
            }
        }


        private void BindData()
        {
            var dt = EmployeesBLL.GetDTByDeptId(DepartmentId);
            foreach (DataRow dr in dt.Rows)
            {
                if ((_TempTable == null) || (_TempTable.Columns.Count <= 0) || (_TempTable.Rows.Count <= 0))
                {
                    InitTempTableStructure();
                    AddTempTableRows();
                }

                var drEmp = EmployeesBLL.DR_GetEmployeeById(Convert.ToInt32(dr["UserId"]));

                var newRow = _TempTable.NewRow();

                newRow["UserId"] = string.Format("{0:00000#}", drEmp["UserId"]);
                newRow["ContractNo"] = string.Format("{0:00000#}", 0);
                newRow["FullName"] = drEmp["FullName"];


                try
                {
                    var _PositionId = Convert.ToInt32(drEmp["PositionId"]);
                    newRow["PositionId"] = drEmp["PositionId"];
                }
                catch
                {
                }

                newRow["ContractTypeId"] = ddlContractType.SelectedValue;

                DateTime FromDate;
                try
                {
                    FromDate = DateTime.ParseExact(rdpFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                catch
                {
                    FromDate = FormatDate.GetSQLDateMinValue;
                }
                newRow["FromDate"] = FromDate;

                DateTime ToDate;
                try
                {
                    ToDate = DateTime.ParseExact(rdpToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                catch
                {
                    ToDate = FormatDate.GetSQLDateMinValue;
                }
                newRow["ToDate"] = ToDate;


                newRow["IsReplaced"] = false;
                newRow["PreviousEmployeeContractId"] = DBNull.Value;
                newRow["OldContractNo"] = DBNull.Value;

                newRow["Office"] = "theo Quyết định số /QĐ-SAGS ngày ";

                newRow["CntRemark"] = txtRemark.Text;
                var _FullName2 = drEmp["FullName2"].ToString();
                newRow["FullName2"] = _FullName2;

                newRow["EmployeeContractId"] = 0;
                newRow["SalaryLevel"] = ddlScaleOfSalaryLevel.SelectedValue;
                newRow["ScaleOfSalaryId"] = ddlScaleOfSalary.SelectedValue;

                newRow["ContractTitle"] = txtContractTitle.Text.Trim();
                newRow["CompanyName"] = txtCompanyName.Text.Trim();
                newRow["RepresentativeOfCompany"] = txtRepresentation.Text.Trim();

                var _Expression = string.Format("Convert(FullName2, 'System.String') LIKE '{0}'", _FullName2);
                if (_TempTable.Select(_Expression).Any())
                {
                    MessageBox.Show(
                        string.Format(
                            "Đã tạo hợp đồng cho nhân viên {0}. Vui lòng lưu trước khi tạo thêm hoặc chỉnh sửa",
                            _FullName2), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    _TempTable.Rows.InsertAt(newRow, 0);
                    radGridView1.DataSource = _TempTable;
                }
            }
        }

        private void LoadData(int EmployeeContractId)
        {
            InitData();
            var dr = EmployeeContractBLL.GetContractById(EmployeeContractId);
            if (dr != null)
            {
                rmbEmployees.SelectedValue = dr["UserId"];
                ddlContractType.SelectedValue = dr["ContractTypeId"];
                if (_IsShown == false)
                {
                    CheckScaleOfSalary(Convert.ToInt32(dr["ScaleOfSalaryId"]));
                    _IsShown = true;
                }
                ddlScaleOfSalary.SelectedValue = dr["ScaleOfSalaryId"];
                ddlScaleOfSalaryLevel.SelectedValue = dr["SalaryLevel"];
                txtScaleOfSalaryValue.Text = string.Format("{0:#,###0}", Get_ScaleOfSalaryValue());
                txtScaleOfSalaryCode.Text = Get_ScaleOfSalaryCode();
                txtJobDescription.Text = Get_ScaleOfSalaryJobDescription();
                txtContractTitle.Text = dr["ContractTitle"].ToString();
                txtCompanyName.Text = dr["CompanyName"] == DBNull.Value
                    ? clsGlobal.CompanyName.Trim()
                    : dr["CompanyName"].ToString();
                txtRepresentation.Text = dr["RepresentativeOfCompany"] == DBNull.Value
                    ? clsGlobal.Representation.Trim()
                    : dr["RepresentativeOfCompany"].ToString();
                rdpFromDate.Text = Convert.ToDateTime(dr["FromDate"]).ToString("dd/MM/yyyy");
                rdpToDate.Text = Convert.ToDateTime(dr["ToDate"]).ToString("dd/MM/yyyy");
                txtRemark.Text = dr["CntRemark"].ToString();
            }
        }

        private void CheckScaleOfSalary(int ScaleOfSalaryId)
        {
            var dr = ScaleOfSalariesBLL.GetOne(ScaleOfSalaryId);
            if (Convert.ToInt32(dr["Active"]) == 0)
                MessageBox.Show(
                    string.Format("Chức danh chuyên môn/ Mã lương/ Mức lương/ Số tiền/ Mô tả công việc quá hạn ({0})",
                        Convert.ToDateTime(dr["AppliedDate"]).ToShortDateString()));
        }

        private void InitData()
        {
            BS_EmployeeList.DataSource = EmployeesBLL.GetAllDT(1);

            BS_ScaleOfSalary.DataSource = ScaleOfSalariesBLL.GetAll();
            BS_ScaleOfSalary1.DataSource = ScaleOfSalariesBLL.GetAllWithFilter(9999);

            BS_ContractType.DataSource = ContractTypesBLL.GetAllToDT(1);

            BS_ContractTypeEdit.DataSource = ContractTypesBLL.GetAllToDT(1);

            BS_Position.DataSource = PositionsBLL.GetAllToDT();

            ddlScaleOfSalaryLevel.DataSource = Constants.GetScaleOfSalary_Level();
            ddlScaleOfSalaryLevel.ValueMember = "UnitId";
            ddlScaleOfSalaryLevel.DisplayMember = "UnitName";
        }

        private DateTime Todate(int contracttypeid, DateTime fromdate)
        {
            var dt = DefaultValues.GetSQLDateMinValue();
            var todate = DefaultValues.GetSQLDateMinValue();
            var temp = dt;
            if ((contracttypeid > 0) && !fromdate.Equals(dt))
            {
                var obj = ContractTypesBLL.GetById(contracttypeid);
                if (obj != null)
                {
                    if (obj.ContractTypeValue <= 0)
                    {
                        todate = dt;
                        temp = todate;
                    }
                    else
                    {
                        try
                        {
                            todate = fromdate.AddMonths(int.Parse(obj.ContractTypeValue.ToString()));
                            todate = todate.AddDays(-1);
                            temp = todate;
                        }
                        catch
                        {
                        }
                    }
                    return todate;
                }
            }
            return temp;
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

        private void Refresh_ContractInfo()
        {
            var _SelectedValue = Convert.ToInt32(ddlScaleOfSalary.SelectedValue);
            txtScaleOfSalaryCode.Text = Get_ScaleOfSalaryCode();
            ddlScaleOfSalaryLevel.SelectedValue = 1;
            txtScaleOfSalaryValue.Text = string.Format("{0:#,###0}", Get_ScaleOfSalaryValue());

            var JobDescription = Get_ScaleOfSalaryJobDescription();
            txtJobDescription.Text = JobDescription;
            txtJobDescription.TextBoxElement.AutoToolTip = true;
            txtJobDescription.TextBoxElement.ToolTipText = JobDescription;
        }

        private string Get_ScaleOfSalaryCode()
        {
            try
            {
                return ScaleOfSalariesBLL.GetOne(Convert.ToInt32(ddlScaleOfSalary.SelectedValue))["Code"].ToString();
            }
            catch
            {
                return "Vui lòng chọn chức danh rồi thử lại";
            }
        }

        private string Get_ScaleOfSalaryJobDescription()
        {
            try
            {
                return
                    ScaleOfSalariesBLL.GetOne(Convert.ToInt32(ddlScaleOfSalary.SelectedValue))["JobDescription"]
                        .ToString();
            }
            catch
            {
                return "Vui lòng chọn chức danh rồi thử lại";
            }
        }

        private double Get_ScaleOfSalaryValue()
        {
            double ReturnValue = 0;

            var dr = ScaleOfSalariesBLL.GetOne(Convert.ToInt32(ddlScaleOfSalary.SelectedValue));

            var Index = Convert.ToInt32(ddlScaleOfSalaryLevel.SelectedValue);
            switch (Index)
            {
                case 1:
                    ReturnValue = Convert.ToDouble(dr["Value1"]);
                    break;
                case 2:
                    ReturnValue = Convert.ToDouble(dr["Value2"]);
                    break;
                case 3:
                    ReturnValue = Convert.ToDouble(dr["Value3"]);
                    break;
            }

            return ReturnValue;
        }

        private string GetFullName(int userid)
        {
            return EmployeesBLL.GetDataRowEmployeeById(userid)["FullName"].ToString();
        }

        private int GetDirectByUserId(int userid)
        {
            var Id = 0;
            var dr = EmployeesBLL.GetDataRowEmployeeById(userid);
            try
            {
                Id = int.Parse(dr["DirectWorking"].ToString());
            }
            catch
            {
                Id = 9999;
            }
            return Id;
        }

        private void RefreshGrid()
        {
            var dt = EmployeeContractBLL.GetByUserIdsToDT(Util.RejectLastComma(_ListUserIds));

            radGridView1.DataSource = dt;
            Utilities.Utilities.GridFormatting(radGridView1);
        }


        private void RmbEmployees_SelectedValueChanged(object sender, EventArgs e)
        {
            var dr = EmployeesBLL.GetDataRowEmployeeById(Convert.ToInt32(rmbEmployees.SelectedValue));
            var _tmpDirect = -1;
            if (dr != null)
                try
                {
                    _tmpDirect = Convert.ToInt32(dr["DirectWorking"]);
                }
                catch
                {
                    _tmpDirect = -1;
                }
            if ((_tmpDirect == 0) || (_tmpDirect == 1))
                chbIsDirect.Checked = Convert.ToBoolean(_tmpDirect);
            else
                chbIsDirect.Checked = true;
        }

        private void RadGridView1_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
        }

        private void RadGridView1_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (!(e.Row is GridViewTableHeaderRowInfo))
                LoadData(Convert.ToInt32(e.Row.Cells["EmployeeContractId"].Value));
        }

        private void RadGridView1_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.Column.Name == "Office")
                if (e.CellElement.RowInfo.Cells["Office"].Value.ToString().Trim().Length ==
                    "theo Quyết định số /QĐ-SAGS ngày ".Trim().Length)
                    e.CellElement.Text = string.Empty;
        }

        private void RdpFromDate_ValueChanged(object sender, EventArgs e)
        {
            if (ddlContractType.SelectedValue != null)
                try
                {
                    rdpToDate.Text =
                        Todate(int.Parse(ddlContractType.SelectedValue.ToString()),
                                DateTime.ParseExact(rdpFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                            .ToString("dd/MM/yyyy");
                }
                catch
                {
                }
        }

        private void DdlContractType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ddlContractType.SelectedValue != null)
                try
                {
                    rdpToDate.Text =
                        Todate(int.Parse(ddlContractType.SelectedValue.ToString()),
                                DateTime.ParseExact(rdpFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                            .ToString("dd/MM/yyyy");

                    if (Convert.ToInt32(ddlContractType.SelectedValue) == 6)
                        txtContractTitle.Text = "HỢP ĐỒNG THỬ VIỆC".ToUpper();
                    else
                        txtContractTitle.Text = "HỢP ĐỒNG LAO ĐỘNG".ToUpper();
                }
                catch
                {
                }
        }

        private void Frm_EmployeeContract_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                rbtAdd.PerformClick();
            if (e.KeyCode == Keys.F2)
                rbtSavetvtg.PerformClick();
            if (e.KeyCode == Keys.F3)
                rbtPrint.PerformClick();
            if (e.KeyCode == Keys.F4)
                rbtPreview.PerformClick();
        }

        private void Frm_EmployeeContract_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }

        private void radGridView1_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if (radGridView1.CurrentColumn is GridViewMultiComboBoxColumn)
                if (radGridView1.CurrentColumn.Name == "PositionId")
                    if (!isColumnAdded)
                    {
                        isColumnAdded = true;

                        var editor = (RadMultiColumnComboBoxElement) radGridView1.ActiveEditor;
                        editor.EditorControl.MasterTemplate.AutoGenerateColumns = false;

                        editor.AutoSizeDropDownToBestFit = true;
                        editor.AutoSizeDropDownColumnMode = BestFitColumnMode.AllCells;
                        editor.DropDownSizingMode = SizingMode.UpDownAndRightBottom;
                        editor.DropDownStyle = RadDropDownStyle.DropDown;
                        editor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

                        editor.AutoFilter = true;
                        editor.DisplayMember = "PositionName";

                        var filter = new FilterDescriptor
                        {
                            PropertyName = "PositionName2",
                            Operator = FilterOperator.Contains
                        };
                        editor.EditorControl.MasterTemplate.FilterDescriptors.Add(filter);

                        var column = new GridViewTextBoxColumn("PositionId");
                        column.HeaderText = "Mã chức danh";
                        column.IsVisible = false;
                        editor.EditorControl.Columns.Add(column);

                        column = new GridViewTextBoxColumn("PositionName");
                        column.HeaderText = "Chức danh";
                        editor.EditorControl.Columns.Add(column);

                        column = new GridViewTextBoxColumn("PositionName2");
                        column.HeaderText = "Chuc danh";
                        editor.EditorControl.Columns.Add(column);
                    }
            if ((e.Column.Name == "UserId") || (e.Column.Name == "ContractNo") || (e.Column.Name == "FullName") ||
                (e.Column.Name == "OldContractNo"))
                e.Cancel = true;
        }

        private void DdlScaleOfSalaryLevel_SelectedValueChanged(object sender, EventArgs e)
        {
            txtScaleOfSalaryValue.Text = string.Format("{0:#,###0}", Get_ScaleOfSalaryValue());
        }

        private void ddlScaleOfSalary_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                Refresh_ContractInfo();
            }
            catch
            {
            }
        }

        private void rbtAdd_Click(object sender, EventArgs e)
        {
            if ((_TempTable == null) || (_TempTable.Columns.Count <= 0) || (_TempTable.Rows.Count <= 0))
            {
                InitTempTableStructure();
                AddTempTableRows();
            }

            var drEmp = EmployeesBLL.DR_GetEmployeeById(Convert.ToInt32(rmbEmployees.SelectedValue));

            var newRow = _TempTable.NewRow();

            newRow["UserId"] = string.Format("{0:00000#}", rmbEmployees.SelectedValue);
            newRow["ContractNo"] = string.Format("{0:00000#}", 0);
            newRow["FullName"] = rmbEmployees.SelectedValue;

            try
            {
                var _PositionId = Convert.ToInt32(drEmp["PositionId"]);
                newRow["PositionId"] = drEmp["PositionId"];
            }
            catch
            {
            }

            newRow["ContractTypeId"] = ddlContractType.SelectedValue;

            DateTime FromDate;
            try
            {
                FromDate = DateTime.ParseExact(rdpFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {
                FromDate = FormatDate.GetSQLDateMinValue;
            }
            newRow["FromDate"] = FromDate;

            DateTime ToDate;
            try
            {
                ToDate = DateTime.ParseExact(rdpToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {
                ToDate = FormatDate.GetSQLDateMinValue;
            }
            newRow["ToDate"] = ToDate;

            var _OldContent = string.Empty;
            var oldDR = EmployeesBLL.DR_GetEmployeeById(Convert.ToInt32(rmbEmployees.SelectedValue));

            var _DirectWorking = 0;
            if (oldDR != null)
                _OldContent = $"DirectWorking: {oldDR["DirectWorking"]}, UserId: {oldDR["UserId"]}";
            try
            {
                _DirectWorking = chbIsDirect.Checked ? 1 : 0;
            }
            catch
            {
                _DirectWorking = 0;
            }
            EmployeesBLL.UpdateDirectWorking(_DirectWorking, Convert.ToInt32(rmbEmployees.SelectedValue));
            _SP = "Upd_H0_Employee_DirectWorking";
            _SPValue = $"DirectWorking: {_DirectWorking}, UserId: {Convert.ToInt32(rmbEmployees.SelectedValue)}";
            Utilities.Utilities.SaveHRMLog("Employees", _SP, _SPValue, _OldContent);
            newRow["DirectWorking"] = _DirectWorking;

            newRow["IsReplaced"] = false;
            newRow["PreviousEmployeeContractId"] = DBNull.Value;
            newRow["OldContractNo"] = DBNull.Value;

            newRow["Office"] = "theo Quyết định số /QĐ-SAGS ngày ";

            newRow["CntRemark"] = txtRemark.Text;
            var _FullName2 = drEmp["FullName2"].ToString();
            newRow["FullName2"] = _FullName2;

            newRow["EmployeeContractId"] = 0;
            newRow["SalaryLevel"] = ddlScaleOfSalaryLevel.SelectedValue;
            newRow["ScaleOfSalaryId"] = ddlScaleOfSalary.SelectedValue;

            newRow["ContractTitle"] = txtContractTitle.Text.Trim();
            newRow["CompanyName"] = txtCompanyName.Text.Trim();
            newRow["RepresentativeOfCompany"] = txtRepresentation.Text.Trim();

            var _Expression = string.Format("Convert(FullName2, 'System.String') LIKE '{0}'", _FullName2);
            if (_TempTable.Select(_Expression).Any())
            {
                MessageBox.Show(
                    string.Format("Đã tạo hợp đồng cho nhân viên {0}. Vui lòng lưu trước khi tạo thêm hoặc chỉnh sửa",
                        _FullName2), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                _TempTable.Rows.InsertAt(newRow, 0);
                radGridView1.DataSource = _TempTable;
            }
            Utilities.Utilities.GridFormatting(radGridView1);
        }

        private void txtJobDescription_ToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
        {
            try
            {
                e.ToolTipText =
                    ScaleOfSalariesBLL.GetOne(Convert.ToInt32(ddlScaleOfSalary.SelectedValue))["JobDescription"]
                        .ToString();
            }
            catch
            {
                e.ToolTipText = "Lỗi: dữ liệu";
            }
        }

        private void rbtSavetvtg_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            foreach (var row in radGridView1.Rows)
            {
                var _ContractTitle = row.Cells["ContractTitle"].Value == null
                    ? ((txtContractTitle.Text == null) || (txtContractTitle.Text.Length <= 0)
                        ? string.Empty
                        : txtContractTitle.Text)
                    : row.Cells["ContractTitle"].Value.ToString();
                var _CompanyName = row.Cells["CompanyName"].Value == null
                    ? ((txtCompanyName.Text == null) || (txtCompanyName.Text.Length <= 0)
                        ? string.Empty
                        : txtCompanyName.Text)
                    : row.Cells["CompanyName"].Value.ToString();
                var _RepresentativeOfCompany = row.Cells["RepresentativeOfCompany"].Value == DBNull.Value
                    ? ((txtRepresentation.Text == null) || (txtRepresentation.Text.Length <= 0)
                        ? string.Empty
                        : txtRepresentation.Text)
                    : row.Cells["RepresentativeOfCompany"].Value.ToString();

                var _EmployeeContractId = Convert.ToInt32(row.Cells["EmployeeContractId"].Value);

                var _UserId = Convert.ToInt32(row.Cells["UserId"].Value);

                var _PositionId = 0;
                try
                {
                    _PositionId = Convert.ToInt32(row.Cells["PositionId"].Value);
                }
                catch
                {
                    MessageBox.Show("Lỗi: chưa chọn chức danh");
                }

                var _ContractTypeId = Convert.ToInt32(row.Cells["ContractTypeId"].Value);

                var _FromDate = (row.Cells["FromDate"].Value == null) ||
                                (Convert.ToDateTime(row.Cells["FromDate"].Value) == FormatDate.GetSQLDateMinValue)
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(row.Cells["FromDate"].Value);
                var _ToDate = Convert.ToDateTime(row.Cells["ToDate"].Value);


                var _Direct = Convert.ToInt32(row.Cells["DirectWorking"].Value);

                var _WorkingHour = _Direct == 1 ? 48 : 40;
                const int _Overtime = 8;

                var _IsReplaced = false;

                var _Office = row.Cells["Office"].Value == null ? string.Empty : row.Cells["Office"].Value.ToString();
                var _Remark = row.Cells["CntRemark"].Value == null
                    ? string.Empty
                    : row.Cells["CntRemark"].Value.ToString();

                const int _PreviousEmployeeContractId = 0;


                var _ScaleOfSalaryId = Convert.ToInt32(row.Cells["ScaleOfSalaryId"].Value);

                var _ScaleOfSalaryLevel = Convert.ToInt32(row.Cells["SalaryLevel"].Value);


                var objBLL = new EmployeeContractBLL {EmployeeContractId = _EmployeeContractId, UserId = _UserId};
                if (_PositionId != 0)
                    objBLL.PositionId = _PositionId;
                else
                    MessageBox.Show("Lỗi: chưa chọn chức danh");
                objBLL.ContractTypeId = _ContractTypeId;
                objBLL.FromDate = _FromDate;
                objBLL.ToDate = _ToDate;
                objBLL.ContractName = string.Empty;
                objBLL.RepresentativeOfCompany = _RepresentativeOfCompany;
                objBLL.CompanyName = _CompanyName;
                objBLL.WorkingHour = _WorkingHour;
                objBLL.Overtime = _Overtime;
                objBLL.CreateDate = DateTime.Now;
                objBLL.SalaryLevel = _ScaleOfSalaryLevel;
                objBLL.ScaleOfSalaryId = _ScaleOfSalaryId;
                objBLL.ContractTitle = _ContractTitle;
                objBLL.Office = _Office;
                objBLL.IsReplaced = _IsReplaced;
                objBLL.PreviousEmployeeContractId = _PreviousEmployeeContractId;
                objBLL.CntRemark = _Remark.Trim();
                if ((_ScaleOfSalaryLevel >= 1) && (_ScaleOfSalaryLevel <= 3))
                    if (_Direct == 9999)
                    {
                        MessageBox.Show(
                            "Lỗi: nhân viên này chưa được gán trực tiếp/ gián tiếp. \r\n Vào thẻ \"Nhân sự\" -> \"Nhân viên đang làm việc\" -> Tìm đến nhân viên cần chỉnh và \"tick\" vào ô \"Trực tiếp?\" -> Lưu",
                            "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        try
                        {
                            var ReturnId = objBLL.Save();
                            _SP = objBLL.ReturnSP();
                            _SPValue = objBLL.ReturnSPValue();
                            if (ReturnId == -1)
                                if (
                                    MessageBox.Show(
                                        string.Format("Hợp đồng của {0} bị trùng. Tiếp tục lưu những hợp đồng khác?",
                                            GetFullName(_UserId).ToUpper()), "Alert", MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Warning) == DialogResult.Yes)
                                    continue;
                                else
                                    break;
                        }
                        catch
                        {
                            MessageBox.Show("Lỗi: dữ liệu", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        finally
                        {
                            Utilities.Utilities.SaveHRMLog("H0_EmployeeContract", _SP, _SPValue, _OldContent);
                        }
                        _ListUserIds += _UserId + ",";
                    }
                else
                    MessageBox.Show("Lỗi: mức lương chỉ có giá trị từ 1-3", "Warning", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
            }
            MessageBox.Show("Thành công!");
            RefreshGrid();

            Cursor.Current = Cursors.Default;
        }

        private void rbtPrint_Click(object sender, EventArgs e)
        {
            if (radGridView1.SelectedRows.Count > 1)
            {
                if (MessageBox.Show("In nhiều hợp đồng?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.Yes)
                    foreach (var row in radGridView1.SelectedRows)
                        try
                        {
                            var rp = new ReportPreview(this, Convert.ToInt32(row.Cells["EmployeeContractId"].Value),
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
                        Convert.ToInt32(radGridView1.CurrentRow.Cells["EmployeeContractId"].Value),
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
                    Convert.ToInt32(radGridView1.CurrentRow.Cells["EmployeeContractId"].Value),
                    Convert.ToInt32(radGridView1.CurrentRow.Cells["UserId"].Value),
                    radGridView1.CurrentRow.Cells["FullName"].Value.ToString(), "Pre");
                rp.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Crystal report missing");
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;

            foreach (var row in radGridView1.Rows)
            {
                row.Cells["ContractTypeId"].Value = ddlContractType.SelectedValue;
                DateTime FromDate;
                try
                {
                    FromDate = DateTime.ParseExact(rdpFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                catch
                {
                    FromDate = FormatDate.GetSQLDateMinValue;
                }
                row.Cells["FromDate"].Value = FromDate;

                DateTime ToDate;
                try
                {
                    ToDate = DateTime.ParseExact(rdpToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                catch
                {
                    ToDate = FormatDate.GetSQLDateMinValue;
                }
                row.Cells["ToDate"].Value = ToDate;
                row.Cells["IsReplaced"].Value = false;
                row.Cells["DirectWorking"].Value = Convert.ToInt32(chbIsDirect.Checked);
                row.Cells["PreviousEmployeeContractId"].Value = DBNull.Value;
                row.Cells["OldContractNo"].Value = DBNull.Value;

                row.Cells["Office"].Value = "theo Quyết định số /QĐ-SAGS ngày ";

                row.Cells["SalaryLevel"].Value = ddlScaleOfSalaryLevel.SelectedValue;
                row.Cells["ScaleOfSalaryId"].Value = ddlScaleOfSalary.SelectedValue;

                row.Cells["ContractTitle"].Value = txtContractTitle.Text.Trim();
                row.Cells["CompanyName"].Value = txtCompanyName.Text.Trim();
                row.Cells["RepresentativeOfCompany"].Value = txtRepresentation.Text.Trim();
            }

            Cursor.Current = Cursors.Default;
        }
    }
}