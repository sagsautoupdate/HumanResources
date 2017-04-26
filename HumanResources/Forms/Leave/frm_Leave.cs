using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using HRMBLL.H0;
using HRMBLL.H1;
using HRMUtil;
using HumanResources.Utilities;
using Telerik.WinControls;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Leave
{
    public partial class frm_Leave : RadForm
    {
        private static frm_Leave s_Instance;
        private readonly RadContextMenu _rcm = Utilities.Utilities.DefaultRadContextMenu();
        private readonly List<EmpLeaveType> lstEmpLeave = new List<EmpLeaveType>();

        private string _ListUserIds = string.Empty;
        private string _ListUserLeaveIds = string.Empty;
        private string _OldContent = string.Empty;

        private string _SP = string.Empty;
        private string _SPValue = string.Empty;
        private DataTable _TempTable;


        private bool isColumnAdded;

        public frm_Leave()
        {
            InitializeComponent();
            Utilities.Utilities.SetScreenColor(this);

            Text = "THÊM MỚI NGÀY PHÉP";
        }

        public frm_Leave(int UserId, string FullName, int Year, string Type)
        {
            InitializeComponent();
            Utilities.Utilities.SetScreenColor(this);

            this.UserId = UserId;
            this.FullName = FullName;
            this.Year = Year;
            this.Type = Type;

            Text = string.Format("LỊCH SỬ NGÀY PHÉP CỦA NHÂN VIÊN {0} (MÃ NV: {1})", this.FullName.ToUpper(),
                this.UserId);
        }

        public frm_Leave(int UserId, string FullName, int Year, string Type, int LeaveId)
        {
            InitializeComponent();
            Utilities.Utilities.SetScreenColor(this);

            this.UserId = UserId;
            this.FullName = FullName;
            this.Year = Year;
            this.Type = Type;
            EmpLeaveId = LeaveId;

            Text = string.Format("NGÀY PHÉP CỦA NHÂN VIÊN {0} (MÃ NV: {1})", this.FullName.ToUpper(), this.UserId);
        }

        public static frm_Leave Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_Leave();
                return s_Instance;
            }
        }

        public int UserId { get; set; }

        public string FullName { get; set; }

        public int Year { get; set; }

        public string Type { get; set; }

        public int EmpLeaveId { get; set; }

        private void Form_Leave_Load(object sender, EventArgs e)
        {
            _rcm.Items["rmiAdd"].Enabled = false;
            _rcm.Items["rmiEdit"].Enabled = false;
            _rcm.Items["rmiDelete"].Enabled = false;
            _rcm.Items["rmiRefresh"].Click += RmiRefresh_Click;

            if (Type == "His")
            {
                InitDataHistory();

                radGridView1.AllowEditRow = false;
                btnSavetvtg.Enabled = false;
                tableLayoutPanel1.Visible = false;
            }
            else
            {
                InitData();
                radGridView1.AllowEditRow = true;
                tableLayoutPanel1.Visible = true;
                btnSavetvtg.Enabled = true;
                rmbEmployees.SelectedValue = UserId;
                
                rdlLeaveType.SelectedValue = 5;
                rdpFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                rdpToDate.Text = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
                txtDays.Text =
                    HRMBLL.BLLHelper.Utilities.GetLeaveDaysV1(
                        LeaveTypesBLL.GetById(Convert.ToInt32(rdlLeaveType.SelectedValue)),
                        DateTime.ParseExact(rdpFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        DateTime.ParseExact(rdpToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture)).ToString();
                
                InitTempTableStructure();
                AddTempTableRows();
            }


            radGridView1.RowFormatting += RadGridView1_RowFormatting;
            radGridView1.CellEditorInitialized += RadGridView1_CellEditorInitialized;
            rdpFromDate.ValueChanged += RdpFromDate_ValueChanged;
            rdpToDate.ValueChanged += RdpToDate_ValueChanged;
            KeyDown += Frm_Leave_KeyDown;
            FormClosed += Frm_Leave_FormClosed;
            rdlLeaveType.SelectedValueChanged += RdlLeaveType_SelectedValueChanged;
            radGridView1.ContextMenuOpening += RadGridView1_ContextMenuOpening;
            
            Utilities.Utilities.GridFormatting(radGridView1);
        }

        private void RadGridView1_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            e.ContextMenu = _rcm.DropDown;
        }

        private void radGridView1_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            e.CellElement.DrawFill = true;
            if (Convert.ToInt32(e.CellElement.RowInfo.Cells["LeaveTypeId"].Value) == 1)
            {
                var Days = Convert.ToInt32(e.CellElement.RowInfo.Cells["Days"].Value);
                var OmRemain = Convert.ToInt32(e.CellElement.RowInfo.Cells["OmRemain"].Value);
                if (Days > OmRemain)
                {
                    e.CellElement.RowInfo.Cells["column1"].Value = "LỐ ỐM";
                    e.CellElement.BackColor = Color.DarkRed;
                    e.CellElement.ForeColor = Color.White;
                }
                else
                {
                    e.CellElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
                    e.CellElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
                    e.CellElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                    e.CellElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Local);
                }
            }
            else
            {
                if (Convert.ToInt32(e.CellElement.RowInfo.Cells["LeaveTypeId"].Value) == 5)
                {
                    var Days = Convert.ToInt32(e.CellElement.RowInfo.Cells["Days"].Value);
                    var FRemain = Convert.ToInt32(e.CellElement.RowInfo.Cells["FRemain"].Value);
                    if ((Days > FRemain) || (Days > 6))
                    {
                        e.CellElement.RowInfo.Cells["column1"].Value = "LỐ PHÉP HOẶC XIN NGHỈ HƠN 6 NGÀY";

                        e.CellElement.BackColor = Color.DarkRed;
                        e.CellElement.ForeColor = Color.White;
                    }
                    else
                    {
                        e.CellElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
                        e.CellElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
                        e.CellElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                        e.CellElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Local);
                    }
                }
                else
                {
                    if (Convert.ToInt32(e.CellElement.RowInfo.Cells["LeaveTypeId"].Value) == 8)
                    {
                        var Days = Convert.ToInt32(e.CellElement.RowInfo.Cells["Days"].Value);
                        var KoLeftMonth = Convert.ToInt32(e.CellElement.RowInfo.Cells["KoLeftMonth"].Value);
                        var KoLeft = Convert.ToInt32(e.CellElement.RowInfo.Cells["KoLeft"].Value);
                        if ((Days > KoLeftMonth) || (Days > KoLeft))
                        {
                            e.CellElement.RowInfo.Cells["column1"].Value = "LỐ KO";

                            e.CellElement.BackColor = Color.DarkRed;
                            e.CellElement.ForeColor = Color.White;
                        }
                        else
                        {
                            e.CellElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
                            e.CellElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
                            e.CellElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                            e.CellElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Local);
                        }
                    }
                    else
                    {
                        e.CellElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
                        e.CellElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
                        e.CellElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                        e.CellElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Local);
                    }
                }
            }
        }


        private void InitDataHistory()
        {
            Cursor.Current = Cursors.AppStarting;
            BS_EmployeeLeave.DataSource = EmployeeLeaveBLL.DT_GetByUserId_Date(UserId, 0, Year);
            if (clsGlobal.IsAdmin || clsGlobal.IsHRAdmin)
            {
                BS_Employees.DataSource = EmployeesBLL.DT_GetAll(1);
            }
            else
            {
                var deptSelected = int.Parse(EmployeesBLL.DR_GetEmployeeById(clsGlobal.UserId)["RootId"].ToString());
                var objD = new DepartmentsBLL();
                objD.GetAllChildId(deptSelected);
                var departmentIds = objD.ChildNodeIds;
                BS_Employees.DataSource = EmployeesBLL.DT_GetByDeptIds(departmentIds, 0, string.Empty, string.Empty);
            }
            BS_LeaveType.DataSource = Constants.GetAllLeaveCode();
            BS_LeaveType1.DataSource = Constants.GetAllLeaveCode();
            BS_Positions.DataSource = PositionsBLL.GetAllToDT();

            Utilities.Utilities.GridFormatting(radGridView1);
            Cursor.Current = Cursors.Default;
        }

        private void InitData()
        {
            Cursor.Current = Cursors.AppStarting;
            BS_EmployeeLeave.DataSource = EmployeeLeaveBLL.DT_GetByUserIds_Date(UserId.ToString(), 0, Year,
                EmpLeaveId.ToString());
            if (clsGlobal.IsAdmin || clsGlobal.IsHRAdmin)
            {
                BS_Employees.DataSource = EmployeesBLL.DT_GetAll(1);
            }
            else
            {
                var deptSelected = int.Parse(EmployeesBLL.DR_GetEmployeeById(clsGlobal.UserId)["RootId"].ToString());
                var objD = new DepartmentsBLL();
                objD.GetAllChildId(deptSelected);
                var departmentIds = objD.ChildNodeIds;
                BS_Employees.DataSource = EmployeesBLL.DT_GetByDeptIds(departmentIds, 0, string.Empty, string.Empty);
            }
            BS_LeaveType.DataSource = Constants.GetAllLeaveCode();
            BS_LeaveType1.DataSource = Constants.GetAllLeaveCode();
            BS_Positions.DataSource = PositionsBLL.GetAllToDT();

            Utilities.Utilities.GridFormatting(radGridView1);
            Cursor.Current = Cursors.Default;
        }

        public static void GridFormatting(RadGridView rgv)
        {
            rgv.MasterTemplate.ShowFilterCellOperatorText = false;

            rgv.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            rgv.MasterTemplate.BestFitColumns(BestFitColumnMode.AllCells);
            rgv.MasterView.TableHeaderRow.MinHeight = 35;
            for (var i = 0; i < rgv.Rows.Count; i++)
                rgv.Rows[i].MinHeight = 35;
            rgv.AutoSizeRows = true;
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

        private int CalculateData(int LeaveId)
        {
            var ReturnCode = 0;

            var FromDate = DateTime.ParseExact(rdpFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var ToDate = DateTime.ParseExact(rdpToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var dt = WorkdayCoefficientEmployeesFinalBLL.GetDataTableByDataDateForWorkingDayAll(1, FromDate.Year,
                string.Empty,
                Convert.ToInt32(rmbEmployees.SelectedValue), 12, ToDate.Year);
            if (dt != null)
            {
                var drF = dt.Compute("SUM(F)", null);
                var drOm = dt.Compute("SUM(Om)", null);
                var drKo = dt.Compute("SUM(Ko)", null);
                var drTS = dt.Compute("SUM(TS)", null);
                var drCo = dt.Compute("SUM(Co)", null);

                var SumF = 0;
                try
                {
                    SumF = Convert.ToInt32(drF);
                }
                catch
                {
                    SumF = 0;
                }
                var MaxF = 0;
                try
                {
                    MaxF = Convert.ToInt32(dt.Rows[0]["MaxFCurrent"]);
                }
                catch
                {
                    MaxF = 0;
                }

                var SumOm = 0;
                try
                {
                    SumOm = Convert.ToInt32(drOm);
                }
                catch
                {
                    SumOm = 0;
                }
                var MaxOm = 0;
                try
                {
                    MaxOm = Convert.ToInt32(dt.Rows[0]["MaxOm"]);
                }
                catch
                {
                    MaxOm = 0;
                }

                var SumKo = 0;
                try
                {
                    SumKo = Convert.ToInt32(drKo);
                }
                catch
                {
                    SumKo = 0;
                }
                var SumMaxKoInMonth = 0;
                var dtKoInMonth =
                    WorkdayCoefficientEmployeesFinalBLL.GetDataTableByDataDateForWorkingDayAll(FromDate.Month,
                        FromDate.Year, string.Empty, Convert.ToInt32(rmbEmployees.SelectedValue), ToDate.Month,
                        ToDate.Year);
                if (dtKoInMonth.Rows.Count > 0)
                    SumMaxKoInMonth = Convert.ToInt32(dtKoInMonth.Compute("SUM(Ko)", null));
                var SumTS = 0;
                try
                {
                    SumTS = Convert.ToInt32(drTS);
                }
                catch
                {
                    SumTS = 0;
                }
                var SumCo = 0;
                try
                {
                    SumCo = Convert.ToInt32(drCo);
                }
                catch
                {
                    SumCo = 0;
                }

                var _AddFromDate = DateTime.ParseExact(rdpFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var _AddToDate = DateTime.ParseExact(rdpToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if ((rmbRelation.SelectedValue != null) && rmbRelation.Enabled)
                {
                    var UserRelationId = Convert.ToInt32(rmbRelation.SelectedValue);
                    var dr = EmployeeRelationBLL.GetByUserRelationId(UserRelationId);
                    var dateOfBirth = new DateTime((int) dr["RYearOfBirth"], (int) dr["RMonthOfBirth"],
                        (int) dr["RDayOfBirth"]);
                    var today = DateTime.Today;
                    var years = today.Year - dateOfBirth.Year;
                    if (dateOfBirth.Month > today.Month)
                    {
                        --years;
                    }
                    else
                    {
                        if ((dateOfBirth.Month == today.Month) && (dateOfBirth.Day > today.Day))
                            --years;
                    }
                    var MaxCo = 0;
                    if (years <= 3)
                    {
                        MaxCo = 20;
                    }
                    else
                    {
                        if (years < 7)
                            MaxCo = 15;
                        else
                            MaxCo = 0;
                    }
                    if (SumCo >= MaxCo)
                        ReturnCode = (int) LeaveInputStatus.CoError;
                }

                foreach (DataRow dr in dt.Rows)
                    switch (Convert.ToInt32(rdlLeaveType.SelectedValue))
                    {
                        case 1:
                            foreach (var item in FindLeave("Om", dr))
                                if (item.ToString() != string.Empty)
                                    radTextBoxControl1.Text +=
                                        string.Format("{0}: Om: {1:dd/MM/yyyy} - SumOmByYear = {2} ngày\r\n",
                                            rmbEmployees.SelectedValue,
                                            new DateTime(FromDate.Year, Convert.ToDateTime(dr["DataDate"]).Month, item),
                                            SumOm);
                            break;
                        case 5:
                            foreach (var item in FindLeave("F", dr))
                                if (item.ToString() != string.Empty)
                                    radTextBoxControl1.Text +=
                                        string.Format("{0}: F: {1:dd/MM/yyyy} - SumFByYear = {2} ngày\r\n",
                                            rmbEmployees.SelectedValue,
                                            new DateTime(FromDate.Year, Convert.ToDateTime(dr["DataDate"]).Month, item),
                                            SumF);
                            break;
                        case 8:
                            foreach (var item in FindLeave("Ko", dr))
                                if (item.ToString() != string.Empty)
                                    radTextBoxControl1.Text +=
                                        string.Format(
                                            "{0}: Ko: {1:dd/MM/yyyy} - SumKoByMonth = {2} ngày - SumKoByYear = {3}\r\n",
                                            rmbEmployees.SelectedValue,
                                            new DateTime(FromDate.Year, Convert.ToDateTime(dr["DataDate"]).Month, item),
                                            SumMaxKoInMonth, SumKo);
                            break;
                        case 20:
                            foreach (var item in FindLeave("TS", dr))
                                if (item.ToString() != string.Empty)
                                    radTextBoxControl1.Text +=
                                        string.Format("{0}: TS: {1:dd/MM/yyyy} - SumTSByYear = {2} ngày\r\n",
                                            rmbEmployees.SelectedValue,
                                            new DateTime(FromDate.Year, Convert.ToDateTime(dr["DataDate"]).Month, item),
                                            SumTS);
                            break;
                        case 18:
                            foreach (var item in FindLeave("Co", dr))
                                if (item.ToString() != string.Empty)
                                    radTextBoxControl1.Text +=
                                        string.Format("{0}: Co: {1:dd/MM/yyyy} - SumCoByYear = {2} ngày\r\n",
                                            rmbEmployees.SelectedValue,
                                            new DateTime(FromDate.Year, Convert.ToDateTime(dr["DataDate"]).Month, item),
                                            SumCo);
                            break;

                        default:
                            radTextBoxControl1.Text += Environment.NewLine;
                            break;
                    }
                radTextBoxControl1.Text += "------------------------" + Environment.NewLine;
                switch (LeaveId)
                {
                    case 1:
                        if (SumOm > MaxOm)
                            ReturnCode = (int) LeaveInputStatus.OmError;
                        break;
                    case 5:
                        if (SumF > MaxF)
                            ReturnCode = (int) LeaveInputStatus.FError;
                        break;
                    case 8:
                        if ((SumMaxKoInMonth >= 5) || (SumKo >= 20))
                            ReturnCode = (int) LeaveInputStatus.KoError;
                        break;
                    case 20:
                        if (SumTS >= 180)
                            ReturnCode = (int) LeaveInputStatus.TSError;
                        break;
                    case 18:
                        ReturnCode = (int) LeaveInputStatus.CoError;
                        break;

                    default:
                        radTextBoxControl1.Text += Environment.NewLine;
                        break;
                }
            }
            else
            {
                ReturnCode = (int) LeaveInputStatus.UnknownError;
            }
            return ReturnCode;
        }

        private List<int> FindLeave(string LeaveCode, DataRow dr)
        {
            var _lstTemp = new List<int>();
            for (var i = 1; i <= 31; i++)
                if (dr["Day" + i].ToString().Equals(LeaveCode))
                    _lstTemp.Add(i);
            return _lstTemp;
        }


        private void RmiRefresh_Click(object sender, EventArgs e)
        {
            InitData();
        }

        private void RmiEdit_Click(object sender, EventArgs e)
        {
        }

        private void RdlLeaveType_SelectedValueChanged(object sender, EventArgs e)
        {
            if ((int) rdlLeaveType.SelectedValue == 18)
            {
                rmbRelation.Enabled = true;
                rmbRelation.DataSource = EmployeeRelationBLL.GetByFilterDT(24,
                    Convert.ToInt32(rmbEmployees.SelectedValue), 3);
            }
            else
            {
                rmbRelation.Enabled = false;
                rmbRelation.DataSource = null;
            }
        }

        private void RadGridView1_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            var editor = e.ActiveEditor as RadDropDownListEditor;
            if (editor != null)
            {
                var el = editor.EditorElement as RadDropDownListEditorElement;
                el.AutoCompleteMode = AutoCompleteMode.Suggest;
                el.AutoCompleteSuggest.SuggestMode = SuggestMode.Contains;
            }
        }

        private void Frm_Leave_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }

        private void Frm_Leave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                rbtAdd.PerformClick();
            if (e.KeyCode == Keys.F2)
                btnSavetvtg.PerformClick();
        }

        private void RdpToDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                var _FromDate = DateTime.ParseExact(rdpFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var _ToDate = DateTime.ParseExact(rdpToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                txtDays.Text =
                    HRMBLL.BLLHelper.Utilities.GetLeaveDaysV1(
                            LeaveTypesBLL.GetById(Convert.ToInt32(rdlLeaveType.SelectedValue)), _FromDate, _ToDate)
                        .ToString();
            }
            catch
            {
            }
        }

        private void RdpFromDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                var _FromDate = DateTime.ParseExact(rdpFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var _ToDate = DateTime.ParseExact(rdpToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                txtDays.Text =
                    HRMBLL.BLLHelper.Utilities.GetLeaveDaysV1(
                            LeaveTypesBLL.GetById(Convert.ToInt32(rdlLeaveType.SelectedValue)), _FromDate, _ToDate)
                        .ToString();
            }
            catch
            {
            }
        }

        private void RadGridView1_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            if (e.RowElement.RowInfo is GridViewRowInfo)
                e.RowElement.RowInfo.MinHeight = 35;
        }

        private void radGridView1_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
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
            if ((e.Column.Name == "UserId") || (e.Column.Name == "PositionName"))
                e.Cancel = true;
        }

        public static IEnumerable<Tuple<string, int>> MonthsBetween(
            DateTime startDate,
            DateTime endDate)
        {
            DateTime iterator;
            DateTime limit;

            if (endDate > startDate)
            {
                iterator = new DateTime(startDate.Year, startDate.Month, 1);
                limit = endDate;
            }
            else
            {
                iterator = new DateTime(endDate.Year, endDate.Month, 1);
                limit = startDate;
            }

            var dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
            while (iterator <= limit)
            {
                yield return Tuple.Create(
                    dateTimeFormat.GetMonthName(iterator.Month),
                    iterator.Year);
                iterator = iterator.AddMonths(1);
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

            var FromDate = DateTime.ParseExact(rdpFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var ToDate = DateTime.ParseExact(rdpToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var dateSpan = DateTimeSpan.CompareDates(FromDate, ToDate);
            var differentInMonths = dateSpan.Months;
            var differentInYears = dateSpan.Years;
            var _listDate = new List<DateSplit>();
            var _lstMonths = MonthsBetween(FromDate, ToDate);
            if ((differentInMonths == 0) && (differentInYears == 0))
                _listDate.Add(new DateSplit(FromDate, ToDate));
            else
                foreach (var item in _lstMonths)
                {
                    var _month = Convert.ToDateTime("01-" + item.Item1 + "-2011").Month;
                    var _year = item.Item2;
                    if ((_month == FromDate.Month) && (_year == ToDate.Year))
                    {
                        var _lastDateOfMonth = FromDate.LastDayOfMonth();
                        _listDate.Add(new DateSplit(FromDate, _lastDateOfMonth));
                    }
                    else
                    {
                        if (_month == ToDate.Month)
                        {
                            var _firstDateOfMonth = ToDate.FirstDayOfMonth();
                            _listDate.Add(new DateSplit(_firstDateOfMonth, ToDate));
                        }
                        else
                        {
                            var _firstDateOfMonth = new DateTime(_year, _month, 1).FirstDayOfMonth();
                            var _lastDateOfMonth = new DateTime(_year, _month, 1).LastDayOfMonth();

                            _listDate.Add(new DateSplit(_firstDateOfMonth, _lastDateOfMonth));
                        }
                    }
                }
            foreach (var item in _listDate)
            {
                var newRow = _TempTable.NewRow();

                newRow["UserId"] = rmbEmployees.SelectedValue;
                newRow["FullName"] = rmbEmployees.SelectedValue;
                var _PositionName =
                    EmployeesBLL.DR_GetEmployeeById(Convert.ToInt32(rmbEmployees.SelectedValue))["PositionName"]
                        .ToString();
                ;
                newRow["PositionName"] = _PositionName;
                newRow["LeaveTypeId"] = rdlLeaveType.SelectedValue;

                newRow["Remark"] = txtRemark.Text;
                var _FullName2 = drEmp["FullName2"].ToString();
                newRow["FullName2"] = _FullName2;
                newRow["EmployeeLeaveId"] = 0;

                newRow["FromDate"] = item.FromDate;
                newRow["ToDate"] = item.ToDate;

                var Days =
                    HRMBLL.BLLHelper.Utilities.GetLeaveDaysV1(
                        LeaveTypesBLL.GetById(Convert.ToInt32(rdlLeaveType.SelectedValue)), item.FromDate, item.ToDate);
                newRow["Days"] = Days;

                var dr = EmployeeLeaveBLL.DR_GetEmpLeaveDetail(item.FromDate.Month, item.FromDate.Year,
                    Convert.ToInt32(rmbEmployees.SelectedValue));
                var FRemain = Convert.ToDouble(dr["FRemain"]);
                var OmRemain = Convert.ToDouble(dr["OmRemain"]);
                var KoLeftMonth = Convert.ToDouble(dr["KoLeftMonth"]);
                var KoLeft = Convert.ToDouble(dr["KoLeft"]);

                newRow["FRemain"] = FRemain;
                newRow["OmRemain"] = OmRemain;
                newRow["KoLeftMonth"] = KoLeftMonth;
                newRow["KoLeft"] = KoLeft;


                _TempTable.Rows.InsertAt(newRow, 0);
            }

            switch (CalculateData(Convert.ToInt32(rdlLeaveType.SelectedValue)))
            {
                case -1:
                    MessageBox.Show("Lỗi không xác định");
                    break;


                case 1:
                    MessageBox.Show("Lỗi F");
                    break;
                case 2:
                    MessageBox.Show("Lỗi Om");
                    break;
                case 3:
                    MessageBox.Show("Lỗi Ko");
                    break;
                case 4:
                    MessageBox.Show("Lỗi TS");
                    break;
                case 5:
                    MessageBox.Show("Lỗi Co");
                    break;
            }

            rmbEmployees.MultiColumnComboBoxElement.ShowPopup();
            txtRemark.Text = string.Empty;

            BS_EmployeeLeave.DataSource = _TempTable;
            Utilities.Utilities.GridFormatting(radGridView1);
        }

        private void btnSavetvtg_Click(object sender, EventArgs e)
        {
            var msg = string.Empty;
            Cursor.Current = Cursors.AppStarting;
            foreach (var row in radGridView1.Rows)
            {
                DataRow dr0 = null;
                try
                {
                    dr0 = EmployeeLeaveBLL.GetDRById(Convert.ToInt64(row.Cells["EmployeeLeaveId"].Value));
                }
                catch
                {
                }
                if (dr0 != null)
                    _OldContent =
                        $"LeaveTypeId: {dr0["LeaveTypeId"]}, UserId: {dr0["UserId"]}, FromDate: '{dr0["FromDate"]}', ToDate: '{dr0["ToDate"]}', Days: {dr0["Days"]}, GroupId: {dr0["GroupId"]}, Remark: N'{dr0["Remark"]}', EmployeeLeaveId: {dr0["EmployeeLeaveId"]}, Status: {dr0["Status"]}";
                else
                    _OldContent = string.Empty;
                var EmployeeLeaveId = row.Cells["EmployeeLeaveId"].Value == DBNull.Value
                    ? 0
                    : Convert.ToInt64(row.Cells["EmployeeLeaveId"].Value);
                var UserId = row.Cells["UserId"].Value == DBNull.Value ? 0 : Convert.ToInt32(row.Cells["UserId"].Value);
                var LeaveTypeId = row.Cells["LeaveTypeId"].Value == DBNull.Value
                    ? 0
                    : Convert.ToInt32(row.Cells["LeaveTypeId"].Value);
                var FromDate = row.Cells["FromDate"].Value == DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(row.Cells["FromDate"].Value);
                var ToDate = row.Cells["ToDate"].Value == DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(row.Cells["ToDate"].Value);
                var Days = row.Cells["Days"].Value == DBNull.Value ? 0 : Convert.ToInt32(row.Cells["Days"].Value);
                var Remark = (row.Cells["Remark"].Value == DBNull.Value) ||
                             (row.Cells["Remark"].ToString() == string.Empty)
                    ? string.Empty
                    : row.Cells["Remark"].Value.ToString();

                var elBLL = new EmployeeLeaveBLL(EmployeeLeaveId, UserId, LeaveTypeId, FromDate, ToDate, Days, Remark);
                var Id = elBLL.Save();
                _SP = elBLL.ReturnSP();
                _SPValue = elBLL.ReturnSPValue();
                Utilities.Utilities.SaveHRMLog("H0_EmployeeLeave", _SP, _SPValue, _OldContent);

                var EmpId = Id <= 0 ? Convert.ToInt32(EmployeeLeaveId) : Convert.ToInt32(Id);
                if (Id == -1)
                    MessageBox.Show(string.Format("Ngày ốm của {0} bị trùng!",
                        EmployeesBLL.DR_GetEmployeeById(Convert.ToInt32(rmbEmployees.SelectedValue))["FullName"]));
                lstEmpLeave.Add(new EmpLeaveType(UserId, EmpId));
                _ListUserIds += UserId + ",";
                _ListUserLeaveIds += EmpId + ",";
            }
            Cursor.Current = Cursors.Default;
            foreach (var item in lstEmpLeave)
            {
                var dt = EmployeeLeaveBLL.DT_GetByUserIds(item.EmpId, item.EmpLeaveId);
                foreach (DataRow dr in dt.Rows)
                    msg += string.Format("- Tên: {0} - Mã NP: {1}\r\n", dr["FullName"], dr["LeaveNo"]);
            }

            MessageBox.Show(msg);

            BS_EmployeeLeave.DataSource = EmployeeLeaveBLL.DT_GetByUserIds_Date(Util.RejectLastComma(_ListUserIds), 0,
                Year, Util.RejectLastComma(_ListUserLeaveIds));
        }
    }

    public enum LeaveInputStatus
    {
        Success = 0,
        FError = 1,
        OmError = 2,
        KoError = 3,
        TSError = 4,
        CoError = 5,
        UnknownError = -1
    }

    public class DateSplit
    {
        public DateSplit()
        {
        }

        public DateSplit(DateTime FromDate, DateTime ToDate)
        {
            this.FromDate = FromDate;
            this.ToDate = ToDate;
        }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
    }
}