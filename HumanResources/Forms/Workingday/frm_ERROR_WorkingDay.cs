using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraWaitForm;
using HRMBLL.H0;
using HRMBLL.H1;
using HRMBLL.H1.Helper;
using HRMUtil;
using HumanResources.Forms.Workingday.Helper;
using HumanResources.Progress;
using HumanResources.Utilities;
using Telerik.WinControls;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Workingday
{
    public partial class frm_ERROR_WorkingDay : RadForm
    {
        #region Fields
        private readonly Font _defaultFontBold = new Font("Segoe UI", 8.25F, FontStyle.Bold);
        private readonly string _DeptIds;
        private DateTime _DataDate;
        private frm_WorkingdayFinal _fwf;
        private List<DepartmentsBLL> _listDep;
        private List<ListIndex> _listIndex = new List<ListIndex>();
        private string _OldContent = string.Empty;
        private string _SP = string.Empty;
        private string _SPValue = string.Empty;
        private DataTable _TempTable;
        private frm_WaitForm1 _wf1 = new frm_WaitForm1();
        private RadHostItem h;
        private bool m_Continue;
        private RadTreeView radTreeView1;
        private DataTable _dt;
        #endregion

        public frm_ERROR_WorkingDay()
        {
            InitializeComponent();
        }

        public frm_ERROR_WorkingDay(frm_WorkingdayFinal fwf, DateTime DataDate, string DeptIds)
        {
            InitializeComponent();

            _fwf = fwf;
            _DataDate = DataDate;
            _DeptIds = DeptIds;
        }

        private void frm_ERROR_WorkingDay_Load(object sender, EventArgs e)
        {
            _wf1.BringToFront();
            _wf1.ShowOnTopMode = ShowFormOnTopMode.AboveAll;
            radLabel3.Text =
                $"Tháng {_DataDate.Month} năm {_DataDate.Year}: Khối trực tiếp: {getWeekend(_DataDate.Month, _DataDate.Year, 1).Count} NT - Khối gián tiếp: {getWeekend(_DataDate.Month, _DataDate.Year, 0).Count} NT";
            radButton2.Enabled = false;
            m_Continue = true;
            
            InitData();
            
            HideCol();
            radGridView1.TableElement.VScrollBar.Value = 0;
        }
        
        private void radGridView1_FilterChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            radLabelElement1.Text = "Tổng cộng: " + Utilities.Utilities.GetDataRowCount(radGridView1.ChildRows);
        }

        private void radGridView1_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            if (e.CurrentRow != null)
            {
                this.Cursor = Cursors.AppStarting;
                /*radGridView2.FilterDescriptors.Clear();
                var filter = new FilterDescriptor();
                filter.PropertyName = "UserId";
                filter.Operator = FilterOperator.IsEqualTo;
                filter.Value = e.CurrentRow.Cells["UserId"].Value;
                filter.IsFilterEditor = true;
                radGridView2.FilterDescriptors.Add(filter);*/
                radGridView2.DataSource = WorkdayCoefficientEmployeesFinalBLL.GetDataTableByDataDateForWorkingDay_ERROR(
                            _DataDate.Month, _DataDate.Year,
                            clsGlobal.SalaryIsVCQLNN, _DeptIds, 9999, Constants.DataType_Run,
                            radDropDownList1.SelectedValue.ToString(), Convert.ToInt32(radGridView1.CurrentRow.Cells["UserId"].Value));

                var LeaveDT = EmployeeLeaveBLL.GetDTByUserId_Date(Convert.ToInt32(radGridView1.CurrentRow.Cells["UserId"].Value), _DataDate.Month, _DataDate.Year);
                if (LeaveDT.Rows.Count > 0)
                    radGridView4.DataSource = LeaveDT;
                else
                    radGridView4.DataSource = null;

                Utilities.Utilities.GridFormatting(radGridView2);
                Utilities.Utilities.GridFormatting(radGridView4);

                HideCol();

                this.Cursor = Cursors.Default;
            }
        }

        private void radGridView2_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement.ColumnInfo is GridViewDataColumn)
                if (e.CellElement.ColumnInfo.Name == "colTOTAL")
                    e.CellElement.Text = CalTotalLeave(e.CellElement.RowInfo).ToString();
            if (e.CellElement.RowInfo.Cells["UserId"].Value.Equals(radGridView1.CurrentRow.Cells["UserId"].Value))
                if (e.CellElement.RowInfo != null)
                {
                    for (var i = 1; i <= 31; i++)
                        if (radGridView1.CurrentRow.Cells["Day" + i].Style.ForeColor == Color.Red)
                            e.CellElement.RowInfo.Cells["Day" + i].Style.ForeColor = Color.Red;
                    if (
                        !e.CellElement.RowInfo.Cells["NghiTuan"].Value.Equals(
                            radGridView1.CurrentRow.Cells["NghiTuan"].Value) &&
                        (e.CellElement.RowInfo.Cells["NghiTuan"].Value != null))
                        e.CellElement.RowInfo.Cells["NghiTuan"].Style.ForeColor = Color.Red;
                    else
                        e.CellElement.RowInfo.Cells["NghiTuan"].Style.ResetValue(VisualElement.ForeColorProperty,
                            ValueResetFlags.Local);
                    if (
                        !e.CellElement.RowInfo.Cells["NghiBu"].Value.Equals(
                            radGridView1.CurrentRow.Cells["NghiBu"].Value) &&
                        (e.CellElement.RowInfo.Cells["NghiBu"].Value != null))
                        e.CellElement.RowInfo.Cells["NghiBu"].Style.ForeColor = Color.Red;
                    else
                        e.CellElement.RowInfo.Cells["NghiBu"].Style.ResetValue(VisualElement.ForeColorProperty,
                            ValueResetFlags.Local);


                    if (!e.CellElement.RowInfo.Cells["X"].Value.Equals(radGridView1.CurrentRow.Cells["X"].Value) &&
                        (e.CellElement.RowInfo.Cells["X"].Value != null))
                        e.CellElement.RowInfo.Cells["X"].Style.ForeColor = Color.Red;
                    else
                        e.CellElement.RowInfo.Cells["X"].Style.ResetValue(VisualElement.ForeColorProperty,
                            ValueResetFlags.Local);
                    if (
                        !e.CellElement.RowInfo.Cells["NCQD"].Value.Equals(radGridView1.CurrentRow.Cells["NCQD"].Value) &&
                        (e.CellElement.RowInfo.Cells["NCQD"].Value != null))
                        e.CellElement.RowInfo.Cells["NCQD"].Style.ForeColor = Color.Red;
                    else
                        e.CellElement.RowInfo.Cells["NCQD"].Style.ResetValue(VisualElement.ForeColorProperty,
                            ValueResetFlags.Local);
                    if (
                        !e.CellElement.RowInfo.Cells["NCDC"].Value.Equals(radGridView1.CurrentRow.Cells["NCDC"].Value) &&
                        (e.CellElement.RowInfo.Cells["NCDC"].Value != null))
                        e.CellElement.RowInfo.Cells["NCDC"].Style.ForeColor = Color.Red;
                    else
                        e.CellElement.RowInfo.Cells["NCDC"].Style.ResetValue(VisualElement.ForeColorProperty,
                            ValueResetFlags.Local);
                }
            if ((e.CellElement.ForeColor == Color.Red) && (e.CellElement.Value != null))
            {
                e.CellElement.DrawFill = true;
                e.CellElement.NumberOfColors = 1;
                e.CellElement.BackColor = Color.FromArgb(236, 220, 245);
                e.CellElement.Font = _defaultFontBold;
            }
        }

        private void radGridView2_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is GridHeaderCellElement)
                if (e.CellElement.ColumnInfo is GridViewDataColumn)
                    if ((((GridViewDataColumn) e.CellElement.ColumnInfo).Name == "colTOTAL") ||
                        (((GridViewDataColumn) e.CellElement.ColumnInfo).Name == "X") ||
                        (((GridViewDataColumn) e.CellElement.ColumnInfo).Name == "NCDC") ||
                        (((GridViewDataColumn) e.CellElement.ColumnInfo).Name == "NCQD") ||
                        (((GridViewDataColumn) e.CellElement.ColumnInfo).Name == "NghiTuan") ||
                        (((GridViewDataColumn) e.CellElement.ColumnInfo).Name == "NghiBu"))
                    {
                        e.CellElement.DrawFill = true;
                        e.CellElement.NumberOfColors = 1;
                        e.CellElement.BackColor = Color.FromArgb(233, 245, 220);
                    }
                    else
                    {
                        if ((e.CellElement.ColumnInfo.Name == "Day1") || (e.CellElement.ColumnInfo.Name == "Day2") ||
                            (e.CellElement.ColumnInfo.Name == "Day3") || (e.CellElement.ColumnInfo.Name == "Day4") ||
                            (e.CellElement.ColumnInfo.Name == "Day5") || (e.CellElement.ColumnInfo.Name == "Day14") ||
                            (e.CellElement.ColumnInfo.Name == "Day6") || (e.CellElement.ColumnInfo.Name == "Day15") ||
                            (e.CellElement.ColumnInfo.Name == "Day7") || (e.CellElement.ColumnInfo.Name == "Day16") ||
                            (e.CellElement.ColumnInfo.Name == "Day8") || (e.CellElement.ColumnInfo.Name == "Day17") ||
                            (e.CellElement.ColumnInfo.Name == "Day9") || (e.CellElement.ColumnInfo.Name == "Day18") ||
                            (e.CellElement.ColumnInfo.Name == "Day10") || (e.CellElement.ColumnInfo.Name == "Day19") ||
                            (e.CellElement.ColumnInfo.Name == "Day11") || (e.CellElement.ColumnInfo.Name == "Day20") ||
                            (e.CellElement.ColumnInfo.Name == "Day12") || (e.CellElement.ColumnInfo.Name == "Day21") ||
                            (e.CellElement.ColumnInfo.Name == "Day13") || (e.CellElement.ColumnInfo.Name == "Day22") ||
                            (e.CellElement.ColumnInfo.Name == "Day23") || (e.CellElement.ColumnInfo.Name == "Day28") ||
                            (e.CellElement.ColumnInfo.Name == "Day24") || (e.CellElement.ColumnInfo.Name == "Day29") ||
                            (e.CellElement.ColumnInfo.Name == "Day25") || (e.CellElement.ColumnInfo.Name == "Day30") ||
                            (e.CellElement.ColumnInfo.Name == "Day26") || (e.CellElement.ColumnInfo.Name == "Day31") ||
                            (e.CellElement.ColumnInfo.Name == "Day27"))
                        {
                            e.CellElement.Font = _defaultFontBold;
                            e.CellElement.DrawFill = true;
                            e.CellElement.NumberOfColors = 1;
                            e.CellElement.BackColor = Color.FromArgb(236, 220, 245);
                        }
                        else
                        {
                            e.CellElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                            e.CellElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Local);
                            e.CellElement.ResetValue(LightVisualElement.NumberOfColorsProperty, ValueResetFlags.Local);
                            e.CellElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
                        }
                    }
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            if (bWorker1 == null)
                bWorker1 = new BackgroundWorker();
            bWorker1.RunWorkerAsync();
            Task.Factory.StartNew(() =>
            {
                _wf1 = new frm_WaitForm1();
                _wf1.BringToFront();
                _wf1.ShowOnTopMode = ShowFormOnTopMode.AboveAll;
                _wf1.ShowDialog();
            });
            Thread.Sleep(1000);
        }

        private void radGridView1_CellValueChanged(object sender, GridViewCellEventArgs e)
        {
            if ((e.Column.Name == "Day1") || (e.Column.Name == "Day2") ||
                (e.Column.Name == "Day3") || (e.Column.Name == "Day4") ||
                (e.Column.Name == "Day5") || (e.Column.Name == "Day14") ||
                (e.Column.Name == "Day6") || (e.Column.Name == "Day15") ||
                (e.Column.Name == "Day7") || (e.Column.Name == "Day16") ||
                (e.Column.Name == "Day8") || (e.Column.Name == "Day17") ||
                (e.Column.Name == "Day9") || (e.Column.Name == "Day18") ||
                (e.Column.Name == "Day10") || (e.Column.Name == "Day19") ||
                (e.Column.Name == "Day11") || (e.Column.Name == "Day20") ||
                (e.Column.Name == "Day12") || (e.Column.Name == "Day21") ||
                (e.Column.Name == "Day13") || (e.Column.Name == "Day22") ||
                (e.Column.Name == "Day23") || (e.Column.Name == "Day28") ||
                (e.Column.Name == "Day24") || (e.Column.Name == "Day29") ||
                (e.Column.Name == "Day25") || (e.Column.Name == "Day30") ||
                (e.Column.Name == "Day26") || (e.Column.Name == "Day31") ||
                (e.Column.Name == "Day27"))
                try
                {
                    var _Om = Checker.CountCompareGridRow(radGridView1.CurrentRow, "O");
                    var _OmDN = Checker.CountCompareGridRow(radGridView1.CurrentRow, "OD");
                    var _ThaiSan = Checker.CountCompareGridRow(radGridView1.CurrentRow, "TS");
                    var _TNLD = Checker.CountCompareGridRow(radGridView1.CurrentRow, "TNLD");
                    var _F = Checker.CountCompareGridRow(radGridView1.CurrentRow, "F");
                    var _Fdb = Checker.CountCompareGridRow(radGridView1.CurrentRow, "Fdb");
                    var _CoLyDo = Checker.CountCompareGridRow(radGridView1.CurrentRow, "Ro");
                    var _KoLyDo = Checker.CountCompareGridRow(radGridView1.CurrentRow, "Ko");
                    var _DiDuong = Checker.CountCompareGridRow(radGridView1.CurrentRow, "DD");
                    var _CongTac = Checker.CountCompareGridRow(radGridView1.CurrentRow, "CT");
                    var _H1 = Checker.CountCompareGridRow(radGridView1.CurrentRow, "H1");
                    var _H2 = Checker.CountCompareGridRow(radGridView1.CurrentRow, "H2");
                    var _H3 = Checker.CountCompareGridRow(radGridView1.CurrentRow, "H3");
                    var _H4 = Checker.CountCompareGridRow(radGridView1.CurrentRow, "H4");
                    var _H5 = Checker.CountCompareGridRow(radGridView1.CurrentRow, "H5");
                    var _H6 = Checker.CountCompareGridRow(radGridView1.CurrentRow, "H6");
                    var _H7 = Checker.CountCompareGridRow(radGridView1.CurrentRow, "H7");
                    var _ConOm = Checker.CountCompareGridRow(radGridView1.CurrentRow, "Co");
                    var _KHH = Checker.CountCompareGridRow(radGridView1.CurrentRow, "KHH");
                    var _SayThai = Checker.CountCompareGridRow(radGridView1.CurrentRow, "ST");
                    var _KhamThai = Checker.CountCompareGridRow(radGridView1.CurrentRow, "KT");
                    var _ConChet = Checker.CountCompareGridRow(radGridView1.CurrentRow, "CC");
                    var _DinhChiCT = Checker.CountCompareGridRow(radGridView1.CurrentRow, "DC");
                    var _TamHoanHD = Checker.CountCompareGridRow(radGridView1.CurrentRow, "NK");
                    var _HoiHop = Checker.CountCompareGridRow(radGridView1.CurrentRow, "HH");
                    var _LeTet = Checker.CountCompareGridRow(radGridView1.CurrentRow, "LE");
                    var _NghiViec = Checker.CountCompareGridRow(radGridView1.CurrentRow, "NV");
                    var _ChuaDiLam = Checker.CountCompareGridRow(radGridView1.CurrentRow, "-");
                    var _HocSags = Checker.CountCompareGridRow(radGridView1.CurrentRow, "Ho");
                    var _OmDNBHXH = Checker.CountCompareGridRow(radGridView1.CurrentRow, "OBH");
                    var _NuoiCon = Checker.CountCompareGridRow(radGridView1.CurrentRow, "CN");
                    var _NghiMat = Checker.CountCompareGridRow(radGridView1.CurrentRow, "NM");
                    var _NghiTuan = Checker.CountCompareGridRow(radGridView1.CurrentRow, "NT");
                    var _NghiBu = Checker.CountCompareGridRow(radGridView1.CurrentRow, "NB");
                    var _X = Checker.CountCompareGridRow(radGridView1.CurrentRow, "X");

                    var _TotalLeave = CalTotalLeave(radGridView1.CurrentRow);
                    var _NCDC = _X + _NghiBu + _TotalLeave;

                    e.Row.Cells["NghiTuan"].Value = _NghiTuan;
                    e.Row.Cells["NghiBu"].Value = _NghiBu;
                    e.Row.Cells["colTOTAL"].Value = _TotalLeave;
                    e.Row.Cells["NghiTuan"].Value = _NghiTuan;
                    e.Row.Cells["X"].Value = _X;
                    e.Row.Cells["NCDC"].Value = _NCDC;
                }
                catch
                {
                }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }
        
        public void Save()
        {
            for (var i = 0; i < radGridView1.Rows.Count; i++)
            {
                var UserId = Convert.ToInt32(radGridView1.Rows[i].Cells["UserId"].Value);


                var Day1 = radGridView1.Rows[i].Cells["Day1"].Value.ToString();
                var Day2 = radGridView1.Rows[i].Cells["Day2"].Value.ToString();
                var Day3 = radGridView1.Rows[i].Cells["Day3"].Value.ToString();
                var Day4 = radGridView1.Rows[i].Cells["Day4"].Value.ToString();
                var Day5 = radGridView1.Rows[i].Cells["Day5"].Value.ToString();
                var Day6 = radGridView1.Rows[i].Cells["Day6"].Value.ToString();
                var Day7 = radGridView1.Rows[i].Cells["Day7"].Value.ToString();
                var Day8 = radGridView1.Rows[i].Cells["Day8"].Value.ToString();
                var Day9 = radGridView1.Rows[i].Cells["Day9"].Value.ToString();
                var Day10 = radGridView1.Rows[i].Cells["Day10"].Value.ToString();
                var Day11 = radGridView1.Rows[i].Cells["Day11"].Value.ToString();
                var Day12 = radGridView1.Rows[i].Cells["Day12"].Value.ToString();
                var Day13 = radGridView1.Rows[i].Cells["Day13"].Value.ToString();
                var Day14 = radGridView1.Rows[i].Cells["Day14"].Value.ToString();
                var Day15 = radGridView1.Rows[i].Cells["Day15"].Value.ToString();
                var Day16 = radGridView1.Rows[i].Cells["Day16"].Value.ToString();
                var Day17 = radGridView1.Rows[i].Cells["Day17"].Value.ToString();
                var Day18 = radGridView1.Rows[i].Cells["Day18"].Value.ToString();
                var Day19 = radGridView1.Rows[i].Cells["Day19"].Value.ToString();
                var Day20 = radGridView1.Rows[i].Cells["Day20"].Value.ToString();
                var Day21 = radGridView1.Rows[i].Cells["Day21"].Value.ToString();
                var Day22 = radGridView1.Rows[i].Cells["Day22"].Value.ToString();
                var Day23 = radGridView1.Rows[i].Cells["Day23"].Value.ToString();
                var Day24 = radGridView1.Rows[i].Cells["Day24"].Value.ToString();
                var Day25 = radGridView1.Rows[i].Cells["Day25"].Value.ToString();
                var Day26 = radGridView1.Rows[i].Cells["Day26"].Value.ToString();
                var Day27 = radGridView1.Rows[i].Cells["Day27"].Value.ToString();
                var Day28 = radGridView1.Rows[i].Cells["Day28"].Value.ToString();
                var Day29 = string.Empty;
                var Day30 = string.Empty;
                var Day31 = string.Empty;
                try
                {
                    Day29 = radGridView1.Rows[i].Cells["Day29"].Value.ToString();
                }
                catch
                {
                    Day29 = string.Empty;
                }
                try
                {
                    Day30 = radGridView1.Rows[i].Cells["Day30"].Value.ToString();
                }
                catch
                {
                    Day30 = string.Empty;
                }
                try
                {
                    Day31 = radGridView1.Rows[i].Cells["Day31"].Value.ToString();
                }
                catch
                {
                    Day31 = string.Empty;
                }
                
                var f_Om = Constants.WorkdayEmployee_DefaultValue;
                var f_OmDaiNgay = Constants.WorkdayEmployee_DefaultValue;
                var f_ThaiSan = Constants.WorkdayEmployee_DefaultValue;
                var f_TNLD = Constants.WorkdayEmployee_DefaultValue;
                var f_Nam = Constants.WorkdayEmployee_DefaultValue;
                var f_db = Constants.WorkdayEmployee_DefaultValue;
                var f_KoLuongCLD = Constants.WorkdayEmployee_DefaultValue;
                var f_KoLuongKLD = Constants.WorkdayEmployee_DefaultValue;
                var f_DiDuong = Constants.WorkdayEmployee_DefaultValue;
                var f_CongTac = Constants.WorkdayEmployee_DefaultValue;

                var f_HocSAGS = Constants.WorkdayEmployee_DefaultValue;
                var f_Hoc1 = Constants.WorkdayEmployee_DefaultValue;
                var f_Hoc2 = Constants.WorkdayEmployee_DefaultValue;
                var f_Hoc3 = Constants.WorkdayEmployee_DefaultValue;
                var f_Hoc4 = Constants.WorkdayEmployee_DefaultValue;
                var f_Hoc5 = Constants.WorkdayEmployee_DefaultValue;
                var f_Hoc6 = Constants.WorkdayEmployee_DefaultValue;
                var f_Hoc7 = Constants.WorkdayEmployee_DefaultValue;

                var f_Con_Om = Constants.WorkdayEmployee_DefaultValue;
                var f_KHHDS = Constants.WorkdayEmployee_DefaultValue;
                var f_SayThai = Constants.WorkdayEmployee_DefaultValue;
                var f_KhamThai = Constants.WorkdayEmployee_DefaultValue;
                var f_ConChet = Constants.WorkdayEmployee_DefaultValue;
                var f_DinhChiCongTac = Constants.WorkdayEmployee_DefaultValue;
                var f_TamHoanHD = Constants.WorkdayEmployee_DefaultValue;
                var f_HoiHop = Constants.WorkdayEmployee_DefaultValue;
                var f_Le = Constants.WorkdayEmployee_DefaultValue;
                var nghiTuan = Constants.WorkdayEmployee_DefaultValue;
                var nghiBu = Constants.WorkdayEmployee_DefaultValue;
                var nghiMat = Constants.WorkdayEmployee_DefaultValue;
                var nghiViec = Constants.WorkdayEmployee_DefaultValue;
                var chuaDiLam = Constants.WorkdayEmployee_DefaultValue;
                var f_OmDNBHXH = Constants.WorkdayEmployee_DefaultValue;
                
                f_OmDNBHXH = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                    Day4,
                    Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_O_DAI_NGAY_HUONG_BHXH);

                f_Om = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                    Day5,
                    Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_O_BAN_THAN);

                f_OmDaiNgay = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                    Day4,
                    Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_O_DAI_NGAY);

                f_KHHDS = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                    Day4,
                    Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_KHHDS);

                f_Con_Om = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                    Day4,
                    Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_CON_OM);

                f_ThaiSan = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                    Day4,
                    Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_THAI_SAN);

                f_SayThai = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                    Day4,
                    Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_SAY_THAI);

                f_KhamThai = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                    Day4,
                    Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_KHAM_THAI);

                f_TNLD = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                    Day5,
                    Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_TNLD);

                f_Nam = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                    Day5,
                    Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_F_NAM);

                f_DiDuong = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                    Day4,
                    Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_F_DI_DUONG);

                f_CongTac = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                    Day4,
                    Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_F_CONG_TAC);

                f_db = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                    Day5,
                    Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_FDB);

                f_Hoc1 = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                    Day5,
                    Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_1);

                f_Hoc2 = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                    Day5,
                    Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_2);

                f_Hoc3 = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                    Day5,
                    Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_3);

                f_Hoc4 = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                    Day5,
                    Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_4);

                f_Hoc5 = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                    Day5,
                    Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_5);

                f_Hoc6 = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                    Day5,
                    Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_6);

                f_Hoc7 = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                    Day5,
                    Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_7);

                f_DinhChiCongTac = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2,
                    Day3,
                    Day4, Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_DINH_CHI_CONG_TAC);

                f_KoLuongCLD = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                    Day4,
                    Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO);

                f_KoLuongKLD = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                    Day4,
                    Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO);


                f_HocSAGS = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                    Day4,
                    Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_SAGS);

                f_ConChet = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                    Day4,
                    Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_CON_CHET_SAU_KHI_SINH);

                f_TamHoanHD = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                    Day4,
                    Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_TAM_HOAN_HOP_DONG);
                f_HoiHop = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                    Day4,
                    Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOI_HOP);
                f_Le = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                    Day5,
                    Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_LE_TET);

                nghiTuan = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                    Day4,
                    Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_TUAN);

                nghiBu = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                    Day5,
                    Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_BU);

                nghiMat = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                    Day4,
                    Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_MAT);

                nghiViec = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                    Day4,
                    Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_VIEC);

                chuaDiLam = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                    Day4,
                    Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_CHUA_DI_LAM);

                var X = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                    Day5,
                    Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                    Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                    Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_X);


                var HSK = Convert.ToDouble(radGridView1.Rows[i].Cells["HSK"].Value);
                var Remark = radGridView1.Rows[i].Cells["Remark"].Value.ToString();

                var LamDem = double.Parse(radGridView1.Rows[i].Cells["Lamdem"].Value.ToString());

                var UpdateUserId = clsGlobal.UserId;
                var UpdatedDate = DateTime.Now;

                var TotalLeave = f_OmDNBHXH + f_Om + f_OmDaiNgay + f_KHHDS + f_Con_Om + f_ThaiSan + f_SayThai +
                                 f_KhamThai +
                                 f_TNLD + f_Nam + f_DiDuong + f_CongTac + f_db + f_Hoc1 + f_Hoc2 + f_Hoc3 + f_Hoc4 +
                                 f_Hoc5 +
                                 f_Hoc6 + f_Hoc7 + f_DinhChiCongTac + f_KoLuongCLD + f_KoLuongKLD + nghiBu;
                var DRWD =
                    WorkdayCoefficientEmployeesFinalBLL.GetByUserIdDataDate(UserId,
                        _DataDate, Constants.DataType_Run);

                var NCDC = X + TotalLeave;

                _SP = $"Upd_H1_WorkdayCoefficientEmployeesFinal_By_UserId_Date_For_EdittingWorkday";
                _SPValue =
                    $"UserId: {UserId}, DataDate: '{_DataDate}', Day1: '{Day1}', Day2: '{Day2}', Day3: '{Day3}', Day4: '{Day4}', Day5: '{Day5}', Day6: '{Day6}', Day7: '{Day7}', Day8: '{Day8}', Day9: '{Day9}', Day10: '{Day10}', Day11: '{Day11}', Day12: '{Day12}', Day13: '{Day13}', Day14: '{Day14}', Day15: '{Day15}', Day16: '{Day16}', Day17: '{Day17}', Day18: '{Day18}', Day19: '{Day19}', Day20: '{Day20}', Day21: '{Day21}', Day22: '{Day22}', Day23: '{Day23}', Day24: '{Day24}', Day25: '{Day25}', Day26: '{Day26}', Day27: '{Day27}', Day28: '{Day28}', Day29: '{Day29}', Day30: '{Day30}', Day31: '{Day31}', LamDem: {LamDem}, UpdatedDate: '{UpdatedDate}', UpdateUserId: {UpdateUserId}, f_OmDNBHXH: {f_OmDNBHXH}, f_Om: {f_Om}, f_OmDN: {f_OmDaiNgay}, f_KHHDS: {f_KHHDS}, f_ConOm: {f_Con_Om}, f_TS: {f_ThaiSan}, f_ST: {f_SayThai}, f_Khamthai: {f_KhamThai}, f_TNLD: {f_TNLD}, f_Nam: {f_Nam}, f_Diduong: {f_DiDuong}, f_CongTac: {f_CongTac}, f_db: {f_db}, f_Hoc1: {f_Hoc1}, f_Hoc2: {f_Hoc2}, f_Hoc3: {f_Hoc3}, f_Hoc4: {f_Hoc4}, f_Hoc5: {f_Hoc5}, f_Hoc6: {f_Hoc6}, f_Hoc7: {f_Hoc7}, f_DinhChiCT: {f_DinhChiCongTac}, f_KoLuongCLD: {f_KoLuongCLD}, f_KoLuongKLD: {f_KoLuongKLD}, X: {X}, NT: {nghiTuan}, NB: {nghiBu}, NghiViec: {nghiViec}, NghiMat: {nghiMat}, ChuaDiLam: {chuaDiLam}, HSK: {HSK}, Remark: N'{Remark}', RemarkHRMAdmin: N'{"" + DateTime.Now}, NCDC: {NCDC}";

                var drOld =
                    WorkdayCoefficientEmployeesFinalBLL.GetByUserIdDataDateToDR(
                        UserId, _DataDate, 1);
                _OldContent =
                    $"UserId: {drOld["UserId"]}, DataDate: '{drOld["DataDate"]}', Day1: '{drOld["Day1"]}', Day2: '{drOld["Day2"]}', Day3: '{drOld["Day3"]}', Day4: '{drOld["Day4"]}', Day5: '{drOld["Day5"]}', Day6: '{drOld["Day6"]}', Day7: '{drOld["Day7"]}', Day8: '{drOld["Day8"]}', Day9: '{drOld["Day9"]}', Day10: '{drOld["Day10"]}', Day11: '{drOld["Day11"]}', Day12: '{drOld["Day12"]}', Day13: '{drOld["Day13"]}', Day14: '{drOld["Day14"]}', Day15: '{drOld["Day15"]}', Day16: '{drOld["Day16"]}', Day17: '{drOld["Day17"]}', Day18: '{drOld["Day18"]}', Day19: '{drOld["Day19"]}', Day20: '{drOld["Day20"]}', Day21: '{drOld["Day21"]}', Day22: '{drOld["Day22"]}', Day23: '{drOld["Day23"]}', Day24: '{drOld["Day24"]}', Day25: '{drOld["Day25"]}', Day26: '{drOld["Day26"]}', Day27: '{drOld["Day27"]}', Day28: '{drOld["Day28"]}', Day29: '{drOld["Day29"]}', Day30: '{drOld["Day30"]}', Day31: '{drOld["Day31"]}', Lamdem: {drOld["Lamdem"]}, UpdateDate: '{drOld["UpdateDate"]}', UpdateUserId: {drOld["UpdateUserId"]}, OmDNBHXH: {drOld["OmDNBHXH"]}, Om: {drOld["Om"]}, OmDN: {drOld["OmDN"]}, KHH: {drOld["KHH"]}, Co: {drOld["Co"]}, TS: {drOld["TS"]}, ST: {drOld["ST"]}, Khamthai: {drOld["Khamthai"]}, TNLD: {drOld["TNLD"]}, F: {drOld["F"]}, Diduong: {drOld["Diduong"]}, CTac: {drOld["CTac"]}, Fdb: {drOld["Fdb"]}, H1: {drOld["H1"]}, H2: {drOld["H2"]}, H3: {drOld["H3"]}, H4: {drOld["H4"]}, H5: {drOld["H5"]}, H6: {drOld["H6"]}, H7: {drOld["H7"]}, DinhChiCT: {drOld["DinhChiCT"]}, Ro: {drOld["Ro"]}, Ko: {drOld["Ko"]}, X: {drOld["X"]}, NT: {drOld["NghiTuan"]}, NB: {drOld["NghiBu"]}, NghiViec: {drOld["NghiViec"]}, NghiMat: {drOld["NghiMat"]}, ChuaDiLam: {drOld["ChuaDiLam"]}, HSK: {drOld["HSK"]}, Remark: N'{drOld["Remark"]}', RemarkHRMAdmin: N'{drOld["RemarkHRMAdmin"]}', NCDC: {drOld["NCDC"]}";

                Utilities.Utilities.SaveHRMLog("H1_WorkdayCoefficientEmployeesFinal", _SP, _SPValue, _OldContent);
                WorkdayCoefficientEmployeesFinalBLL.UpdateWorkingDayFinal(UserId,
                    _DataDate, Day1, Day2, Day3, Day4, Day5, Day6, Day7, Day8, Day9, Day10,
                    Day11, Day12, Day13, Day14, Day15, Day16, Day17, Day18, Day19,
                    Day20, Day21, Day22, Day23, Day24, Day25, Day26, Day27, Day28, Day29,
                    Day30, Day31,
                    LamDem, UpdatedDate, UpdateUserId, f_OmDNBHXH, f_Om, f_OmDaiNgay, f_KHHDS, f_Con_Om, f_ThaiSan,
                    f_SayThai, f_KhamThai, f_TNLD,
                    f_Nam, f_DiDuong, f_CongTac, f_db, f_Hoc1, f_Hoc2, f_Hoc3, f_Hoc4, f_Hoc5, f_Hoc6, f_Hoc7,
                    f_DinhChiCongTac, f_KoLuongCLD,
                    f_KoLuongKLD, X, nghiTuan, nghiBu, nghiViec, nghiMat, chuaDiLam, HSK, Remark,
                    "Fixed by HRM on " + DateTime.Now, NCDC);

                var WorkdayCoefficientEmployeeIdFinal =
                    Convert.ToInt32(drOld["WorkdayCoefficientEmployeeIdFinal"]);
                _SP = $"Upd_H1_WorkdayCoefficientEmployeesFinal_WDStatus";
                _SPValue =
                    $"UserId: {UserId}, DataDate: '{_DataDate}', WDStatus: {2}, CheckRemark: {string.Empty}, WorkdayCoefficientEmployeeIdFinal: {WorkdayCoefficientEmployeeIdFinal}";
                Utilities.Utilities.SaveHRMLog("H1_WorkdayCoefficientEmployeesFinal", _SP, _SPValue, string.Empty);
                WorkdayCoefficientEmployeesFinalBLL.UpdateWDStatus(UserId, _DataDate, 3,
                    string.Empty, WorkdayCoefficientEmployeeIdFinal);

                bWorker1.ReportProgress(-1, string.Format("Saving {0}...", i + 1));
            }
        }

        public void PopulateRootLevel(RadTreeView radTreeView)
        {
            var listRoot = new List<DepartmentsBLL>();
            foreach (var VARIABLE in _listDep)
            {
                var objBLL = DepartmentsBLL.GetRootBySubId(VARIABLE.DepartmentId);
                if (!listRoot.Exists(x => x.DepartmentId == objBLL.DepartmentId))
                    listRoot.Add(new DepartmentsBLL(objBLL.DepartmentId, objBLL.DepartmentName));
            }
            PopulateNodes(listRoot, radTreeView.Nodes, radTreeView);
            radTreeView.Nodes[0].Expand();
        }

        public void PopulateNodes(List<DepartmentsBLL> list, RadTreeNodeCollection nodes, RadTreeView rtv)
        {
            foreach (var obj in list)
            {
                var tn = new RadTreeNode
                {
                    Text = obj.DepartmentName,
                    Value = obj.DepartmentId.ToString(),
                    ImageIndex = obj.Level
                };

                nodes.Add(tn);


                var dr = DepartmentsBLL.GetByIdDR(obj.DepartmentId);
                if (dr != null)
                    if (Convert.ToInt32(dr["ChildNodeCount"]) > 0)
                    {
                        PopulateSubLevel(obj.DepartmentId, tn, rtv);
                        tn.Font = new Font(rtv.Font, FontStyle.Bold);
                    }
            }
        }

        public void PopulateSubLevel(int parentid, RadTreeNode parentNode, RadTreeView rtv)
        {
            var list = DepartmentsBLL.GetAll_SubLevel(parentid);
            var _tmp = new List<DepartmentsBLL>();
            foreach (var item in list)
                if (WorkdayCoefficientEmployeesFinalBLL.GetDataTableByDataDateForWorkingDay_ERROR(
                        _DataDate.Month, _DataDate.Year,
                        clsGlobal.SalaryIsVCQLNN, item.DepartmentId.ToString(), 9999, Constants.DataType_Run,
                        radDropDownList1.SelectedValue.ToString(), 0).Rows.Count > 0)
                    _tmp.Add(item);
            if (_tmp.Count > 0)
                PopulateNodes(_tmp, parentNode.Nodes, rtv);
        }

        public void HideCol()
        {
            var DaysInMonth = DateTime.DaysInMonth(_DataDate.Year, _DataDate.Month);
            if (DaysInMonth == 28)
            {
                radGridView1.Columns["Day29"].IsVisible = false;
                radGridView1.Columns["Day30"].IsVisible = false;
                radGridView1.Columns["Day31"].IsVisible = false;

                radGridView2.Columns["Day29"].IsVisible = false;
                radGridView2.Columns["Day30"].IsVisible = false;
                radGridView2.Columns["Day31"].IsVisible = false;
            }
            else
            {
                if (DaysInMonth == 29)
                {
                    radGridView1.Columns["Day30"].IsVisible = false;
                    radGridView1.Columns["Day31"].IsVisible = false;

                    radGridView2.Columns["Day30"].IsVisible = false;
                    radGridView2.Columns["Day31"].IsVisible = false;
                }
                else
                {
                    if (DaysInMonth == 30)
                    {
                        radGridView1.Columns["Day31"].IsVisible = false;

                        radGridView2.Columns["Day31"].IsVisible = false;
                    }
                    else
                    {
                        radGridView1.Columns["Day29"].IsVisible = true;
                        radGridView1.Columns["Day30"].IsVisible = true;
                        radGridView1.Columns["Day31"].IsVisible = true;

                        radGridView2.Columns["Day29"].IsVisible = true;
                        radGridView2.Columns["Day30"].IsVisible = true;
                        radGridView2.Columns["Day31"].IsVisible = true;
                    }
                }
            }
        }

        public void InitRadtree()
        {
            radTreeView1 = new RadTreeView();
            radTreeView1.ShowLines = true;


            PopulateRootLevel(radTreeView1);

            radTreeView1.Dock = DockStyle.Fill;
            h = new RadHostItem(radTreeView1);
            radDropDownList2.DropDownMinSize = new Size(radDropDownList2.Width,
                radGridView1.Columns["DepartmentId"].DistinctValuesWithFilter.Count*10);
            radTreeView1.SelectedNodeChanged += radTreeView1_SelectedNodeChanged;

            if (radDropDownList2.DropDownListElement.ListElement.Parent != null)
                radDropDownList2.DropDownListElement.ListElement.Children.Insert(0, h);
            radTreeView1.ExpandAll();
        }

        private void RadGridView1_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if ((e.CellElement.ColumnInfo.Name == "NCQD") ||
                (e.CellElement.ColumnInfo.Name == "X") ||
                (e.CellElement.ColumnInfo.Name == "NCDC") ||
                (e.CellElement.ColumnInfo.Name == "colTOTAL") ||
                (e.CellElement.ColumnInfo.Name == "NghiTuan") ||
                (e.CellElement.ColumnInfo.Name == "NghiBu"))
            {
                if (Convert.ToInt32(e.CellElement.Value) >= 0)
                {
                    e.CellElement.DrawFill = true;
                    e.CellElement.NumberOfColors = 1;
                    e.CellElement.BackColor = Color.FromArgb(233, 245, 220);
                    e.CellElement.Font = _defaultFontBold;
                }
            }
            else
            {
                if ((e.CellElement.ColumnInfo.Name == "Day1") ||
                    (e.CellElement.ColumnInfo.Name == "Day2") ||
                    (e.CellElement.ColumnInfo.Name == "Day3") ||
                    (e.CellElement.ColumnInfo.Name == "Day4") ||
                    (e.CellElement.ColumnInfo.Name == "Day5") ||
                    (e.CellElement.ColumnInfo.Name == "Day6") ||
                    (e.CellElement.ColumnInfo.Name == "Day7") ||
                    (e.CellElement.ColumnInfo.Name == "Day8") ||
                    (e.CellElement.ColumnInfo.Name == "Day9") ||
                    (e.CellElement.ColumnInfo.Name == "Day10") ||
                    (e.CellElement.ColumnInfo.Name == "Day11") ||
                    (e.CellElement.ColumnInfo.Name == "Day12") ||
                    (e.CellElement.ColumnInfo.Name == "Day13") ||
                    (e.CellElement.ColumnInfo.Name == "Day14") ||
                    (e.CellElement.ColumnInfo.Name == "Day15") ||
                    (e.CellElement.ColumnInfo.Name == "Day16") ||
                    (e.CellElement.ColumnInfo.Name == "Day17") ||
                    (e.CellElement.ColumnInfo.Name == "Day18") ||
                    (e.CellElement.ColumnInfo.Name == "Day19") ||
                    (e.CellElement.ColumnInfo.Name == "Day20") ||
                    (e.CellElement.ColumnInfo.Name == "Day21") ||
                    (e.CellElement.ColumnInfo.Name == "Day22") ||
                    (e.CellElement.ColumnInfo.Name == "Day23") ||
                    (e.CellElement.ColumnInfo.Name == "Day24") ||
                    (e.CellElement.ColumnInfo.Name == "Day25") ||
                    (e.CellElement.ColumnInfo.Name == "Day26") ||
                    (e.CellElement.ColumnInfo.Name == "Day27") ||
                    (e.CellElement.ColumnInfo.Name == "Day28") ||
                    (e.CellElement.ColumnInfo.Name == "Day29") ||
                    (e.CellElement.ColumnInfo.Name == "Day30") ||
                    (e.CellElement.ColumnInfo.Name == "Day31"))
                {
                    if ((e.CellElement.ForeColor == Color.Red) && (e.CellElement.Value != null))
                    {
                        e.CellElement.DrawFill = true;
                        e.CellElement.NumberOfColors = 1;
                        e.CellElement.BackColor = Color.FromArgb(236, 220, 245);
                        e.CellElement.Font = _defaultFontBold;
                    }
                    else
                    {
                        e.CellElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                        e.CellElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Local);
                        e.CellElement.ResetValue(LightVisualElement.NumberOfColorsProperty, ValueResetFlags.Local);
                        e.CellElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
                    }
                }
                else
                {
                    e.CellElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                    e.CellElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Local);
                    e.CellElement.ResetValue(LightVisualElement.NumberOfColorsProperty, ValueResetFlags.Local);
                    e.CellElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
                }
            }
            if (e.CellElement.ColumnInfo is GridViewDataColumn)
                if (e.CellElement.ColumnInfo.Name == "colTOTAL")
                    e.CellElement.Text = CalTotalLeave(e.CellElement.RowInfo).ToString();

            if (e.CellElement.ColumnInfo.Name == "CheckRemark")
            {
                string _ERROR = "";
                try
                {
                    _ERROR = e.CellElement.RowInfo.Cells["CheckRemark"].Value.ToString().Replace(Environment.NewLine, "");
                }
                catch { }
                var _strError = "";
                var _str = _ERROR.Split(',');
                foreach (var item in _str)
                {
                    if (item.Length > 0 && Convert.ToInt32(item) > -1)
                        _strError += new WE().GetErrorNameById(Convert.ToInt32(item)) + Environment.NewLine;// Utilities.Utilities.GetEnumDescription((WORKDAYERROR)Convert.ToInt32(item)) + Environment.NewLine;
                }
                e.CellElement.Text = _strError;
            }

            e.CellElement.ToolTipText = e.CellElement.Text;
        }

        public void FixNT(string fullname)
        {
            _listIndex = new List<ListIndex>();
            for (var iRow = 0; iRow < radGridView1.Rows.Count; iRow++)
                try
                {
                    var _UserId = Convert.ToInt32(radGridView1.Rows[iRow].Cells["UserId"].Value);


                    var _DirectWorking = Convert.ToInt32(EmployeesBLL.DR_GetEmployeeById(_UserId)["DirectWorking"]);
                    var _WorkdayCoefficientEmployeeIdFinal =
                        Convert.ToInt32(radGridView1.Rows[iRow].Cells["WorkdayCoefficientEmployeeIdFinal"].Value);

                    var _lstWeekend = getWeekend(_DataDate.Month, _DataDate.Year, _DirectWorking);
                    var _WeekendAvailableCount = _lstWeekend.Count;


                    var _Day1 = string.Empty;
                    try
                    {
                        _Day1 = radGridView1.Rows[iRow].Cells["Day1"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day2 = string.Empty;
                    try
                    {
                        _Day2 = radGridView1.Rows[iRow].Cells["Day2"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day3 = string.Empty;
                    try
                    {
                        _Day3 = radGridView1.Rows[iRow].Cells["Day3"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day4 = string.Empty;
                    try
                    {
                        _Day4 = radGridView1.Rows[iRow].Cells["Day4"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day5 = string.Empty;
                    try
                    {
                        _Day5 = radGridView1.Rows[iRow].Cells["Day5"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day6 = string.Empty;
                    try
                    {
                        _Day6 = radGridView1.Rows[iRow].Cells["Day6"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day7 = string.Empty;
                    try
                    {
                        _Day7 = radGridView1.Rows[iRow].Cells["Day7"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day8 = string.Empty;
                    try
                    {
                        _Day8 = radGridView1.Rows[iRow].Cells["Day8"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day9 = string.Empty;
                    try
                    {
                        _Day9 = radGridView1.Rows[iRow].Cells["Day9"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day10 = string.Empty;
                    try
                    {
                        _Day10 = radGridView1.Rows[iRow].Cells["Day10"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day11 = string.Empty;
                    try
                    {
                        _Day11 = radGridView1.Rows[iRow].Cells["Day11"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day12 = string.Empty;
                    try
                    {
                        _Day12 = radGridView1.Rows[iRow].Cells["Day12"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day13 = string.Empty;
                    try
                    {
                        _Day13 = radGridView1.Rows[iRow].Cells["Day13"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day14 = string.Empty;
                    try
                    {
                        _Day14 = radGridView1.Rows[iRow].Cells["Day14"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day15 = string.Empty;
                    try
                    {
                        _Day15 = radGridView1.Rows[iRow].Cells["Day15"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day16 = string.Empty;
                    try
                    {
                        _Day16 = radGridView1.Rows[iRow].Cells["Day16"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day17 = string.Empty;
                    try
                    {
                        _Day17 = radGridView1.Rows[iRow].Cells["Day17"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day18 = string.Empty;
                    try
                    {
                        _Day18 = radGridView1.Rows[iRow].Cells["Day18"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day19 = string.Empty;
                    try
                    {
                        _Day19 = radGridView1.Rows[iRow].Cells["Day19"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day20 = string.Empty;
                    try
                    {
                        _Day20 = radGridView1.Rows[iRow].Cells["Day20"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day21 = string.Empty;
                    try
                    {
                        _Day21 = radGridView1.Rows[iRow].Cells["Day21"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day22 = string.Empty;
                    try
                    {
                        _Day22 = radGridView1.Rows[iRow].Cells["Day22"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day23 = string.Empty;
                    try
                    {
                        _Day23 = radGridView1.Rows[iRow].Cells["Day23"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day24 = string.Empty;
                    try
                    {
                        _Day24 = radGridView1.Rows[iRow].Cells["Day24"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day25 = string.Empty;
                    try
                    {
                        _Day25 = radGridView1.Rows[iRow].Cells["Day25"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day26 = string.Empty;
                    try
                    {
                        _Day26 = radGridView1.Rows[iRow].Cells["Day26"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day27 = string.Empty;
                    try
                    {
                        _Day27 = radGridView1.Rows[iRow].Cells["Day27"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day28 = string.Empty;
                    try
                    {
                        _Day28 = radGridView1.Rows[iRow].Cells["Day28"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day29 = string.Empty;
                    try
                    {
                        _Day29 = radGridView1.Rows[iRow].Cells["Day29"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day30 = string.Empty;
                    try
                    {
                        _Day30 = radGridView1.Rows[iRow].Cells["Day30"].Value.ToString();
                    }
                    catch
                    {
                    }
                    var _Day31 = string.Empty;
                    try
                    {
                        _Day31 = radGridView1.Rows[iRow].Cells["Day31"].Value.ToString();
                    }
                    catch
                    {
                    }


                    if (
                        radGridView1.Rows[iRow].Cells["CheckRemark"].Value.ToString()
                            .Contains(new WE().GetErrorNameById(Constants.ERROR_WORKING_DAYS_ID)))
                    {
                    }
                    else
                    {
                        if (
                            radGridView1.Rows[iRow].Cells["CheckRemark"].Value.ToString()
                                .Contains(new WE().GetErrorNameById(Constants.ERROR_NGHITUAN_ID)))
                        {
                            var countNT = CountLeave("NT", _Day1, _Day2, _Day3, _Day4, _Day5, _Day6, _Day7, _Day8, _Day9,
                                _Day10
                                , _Day11, _Day12, _Day13, _Day14, _Day15, _Day16, _Day17, _Day18, _Day19
                                , _Day20, _Day21, _Day22, _Day23, _Day24, _Day25, _Day26, _Day27, _Day28, _Day29, _Day30,
                                _Day31);

                            if (countNT - _WeekendAvailableCount == 0)
                            {
                            }
                            else
                            {
                                var reCount = countNT;
                                if (reCount < _WeekendAvailableCount)
                                {
                                    while (reCount != _WeekendAvailableCount)
                                        foreach (var day in _lstWeekend)
                                        {
                                            var _tryExit = false;

                                            if (
                                                !radGridView1.Rows[iRow].Cells["Day" + day.Day].Value.ToString()
                                                    .Equals("NT"))
                                            {
                                                radGridView1.Rows[iRow].Cells["Day" + day.Day].Value = "NT";
                                                radGridView1.Rows[iRow].Cells["Day" + day.Day].Style.ForeColor =
                                                    Color.Red;


                                                _tryExit = true;
                                                reCount++;
                                            }


                                            var _dDay1 = string.Empty;
                                            try
                                            {
                                                _dDay1 = radGridView1.Rows[iRow].Cells["Day1"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay2 = string.Empty;
                                            try
                                            {
                                                _dDay2 = radGridView1.Rows[iRow].Cells["Day2"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay3 = string.Empty;
                                            try
                                            {
                                                _dDay3 = radGridView1.Rows[iRow].Cells["Day3"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay4 = string.Empty;
                                            try
                                            {
                                                _dDay4 = radGridView1.Rows[iRow].Cells["Day4"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay5 = string.Empty;
                                            try
                                            {
                                                _dDay5 = radGridView1.Rows[iRow].Cells["Day5"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay6 = string.Empty;
                                            try
                                            {
                                                _dDay6 = radGridView1.Rows[iRow].Cells["Day6"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay7 = string.Empty;
                                            try
                                            {
                                                _dDay7 = radGridView1.Rows[iRow].Cells["Day7"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay8 = string.Empty;
                                            try
                                            {
                                                _dDay8 = radGridView1.Rows[iRow].Cells["Day8"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay9 = string.Empty;
                                            try
                                            {
                                                _dDay9 = radGridView1.Rows[iRow].Cells["Day9"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay10 = string.Empty;
                                            try
                                            {
                                                _dDay10 = radGridView1.Rows[iRow].Cells["Day10"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay11 = string.Empty;
                                            try
                                            {
                                                _dDay11 = radGridView1.Rows[iRow].Cells["Day11"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay12 = string.Empty;
                                            try
                                            {
                                                _dDay12 = radGridView1.Rows[iRow].Cells["Day12"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay13 = string.Empty;
                                            try
                                            {
                                                _dDay13 = radGridView1.Rows[iRow].Cells["Day13"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay14 = string.Empty;
                                            try
                                            {
                                                _dDay14 = radGridView1.Rows[iRow].Cells["Day14"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay15 = string.Empty;
                                            try
                                            {
                                                _dDay15 = radGridView1.Rows[iRow].Cells["Day15"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay16 = string.Empty;
                                            try
                                            {
                                                _dDay16 = radGridView1.Rows[iRow].Cells["Day16"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay17 = string.Empty;
                                            try
                                            {
                                                _dDay17 = radGridView1.Rows[iRow].Cells["Day17"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay18 = string.Empty;
                                            try
                                            {
                                                _dDay18 = radGridView1.Rows[iRow].Cells["Day18"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay19 = string.Empty;
                                            try
                                            {
                                                _dDay19 = radGridView1.Rows[iRow].Cells["Day19"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay20 = string.Empty;
                                            try
                                            {
                                                _dDay20 = radGridView1.Rows[iRow].Cells["Day20"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay21 = string.Empty;
                                            try
                                            {
                                                _dDay21 = radGridView1.Rows[iRow].Cells["Day21"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay22 = string.Empty;
                                            try
                                            {
                                                _dDay22 = radGridView1.Rows[iRow].Cells["Day22"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay23 = string.Empty;
                                            try
                                            {
                                                _dDay23 = radGridView1.Rows[iRow].Cells["Day23"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay24 = string.Empty;
                                            try
                                            {
                                                _dDay24 = radGridView1.Rows[iRow].Cells["Day24"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay25 = string.Empty;
                                            try
                                            {
                                                _dDay25 = radGridView1.Rows[iRow].Cells["Day25"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay26 = string.Empty;
                                            try
                                            {
                                                _dDay26 = radGridView1.Rows[iRow].Cells["Day26"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay27 = string.Empty;
                                            try
                                            {
                                                _dDay27 = radGridView1.Rows[iRow].Cells["Day27"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay28 = string.Empty;
                                            try
                                            {
                                                _dDay28 = radGridView1.Rows[iRow].Cells["Day28"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay29 = string.Empty;
                                            try
                                            {
                                                _dDay29 = radGridView1.Rows[iRow].Cells["Day29"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay30 = string.Empty;
                                            try
                                            {
                                                _dDay30 = radGridView1.Rows[iRow].Cells["Day30"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay31 = string.Empty;
                                            try
                                            {
                                                _dDay31 = radGridView1.Rows[iRow].Cells["Day31"].Value.ToString();
                                            }
                                            catch
                                            {
                                            }


                                            reCount = CountLeave("NT", _dDay1, _dDay2, _dDay3, _dDay4, _dDay5, _dDay6,
                                                _dDay7, _dDay8, _dDay9, _dDay10
                                                , _dDay11, _dDay12, _dDay13, _dDay14, _dDay15, _dDay16, _dDay17, _dDay18,
                                                _dDay19
                                                , _dDay20, _dDay21, _dDay22, _dDay23, _dDay24, _dDay25, _dDay26, _dDay27,
                                                _dDay28, _dDay29, _dDay30, _dDay31);
                                            if (_tryExit)
                                                break;
                                        }
                                }
                                else
                                {
                                    if (reCount > _WeekendAvailableCount)
                                        if (_DirectWorking == 0)
                                            foreach (var day in _lstWeekend)
                                            {
                                                var _tryExit = false;
                                                if (_DirectWorking == 0)
                                                    if (
                                                        radGridView1.Rows[iRow].Cells["Day" + day.Day].Value.ToString()
                                                            .Equals("NT"))
                                                    {
                                                        radGridView1.Rows[iRow].Cells["Day" + day.Day].Value = "X";
                                                        radGridView1.Rows[iRow].Cells["Day" + day.Day].Style.ForeColor =
                                                            Color.Red;


                                                        _tryExit = true;
                                                        reCount++;
                                                    }

                                                var _dDay1 = string.Empty;
                                                try
                                                {
                                                    _dDay1 = radGridView1.Rows[iRow].Cells["Day1"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay2 = string.Empty;
                                                try
                                                {
                                                    _dDay2 = radGridView1.Rows[iRow].Cells["Day2"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay3 = string.Empty;
                                                try
                                                {
                                                    _dDay3 = radGridView1.Rows[iRow].Cells["Day3"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay4 = string.Empty;
                                                try
                                                {
                                                    _dDay4 = radGridView1.Rows[iRow].Cells["Day4"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay5 = string.Empty;
                                                try
                                                {
                                                    _dDay5 = radGridView1.Rows[iRow].Cells["Day5"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay6 = string.Empty;
                                                try
                                                {
                                                    _dDay6 = radGridView1.Rows[iRow].Cells["Day6"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay7 = string.Empty;
                                                try
                                                {
                                                    _dDay7 = radGridView1.Rows[iRow].Cells["Day7"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay8 = string.Empty;
                                                try
                                                {
                                                    _dDay8 = radGridView1.Rows[iRow].Cells["Day8"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay9 = string.Empty;
                                                try
                                                {
                                                    _dDay9 = radGridView1.Rows[iRow].Cells["Day9"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay10 = string.Empty;
                                                try
                                                {
                                                    _dDay10 = radGridView1.Rows[iRow].Cells["Day10"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay11 = string.Empty;
                                                try
                                                {
                                                    _dDay11 = radGridView1.Rows[iRow].Cells["Day11"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay12 = string.Empty;
                                                try
                                                {
                                                    _dDay12 = radGridView1.Rows[iRow].Cells["Day12"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay13 = string.Empty;
                                                try
                                                {
                                                    _dDay13 = radGridView1.Rows[iRow].Cells["Day13"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay14 = string.Empty;
                                                try
                                                {
                                                    _dDay14 = radGridView1.Rows[iRow].Cells["Day14"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay15 = string.Empty;
                                                try
                                                {
                                                    _dDay15 = radGridView1.Rows[iRow].Cells["Day15"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay16 = string.Empty;
                                                try
                                                {
                                                    _dDay16 = radGridView1.Rows[iRow].Cells["Day16"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay17 = string.Empty;
                                                try
                                                {
                                                    _dDay17 = radGridView1.Rows[iRow].Cells["Day17"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay18 = string.Empty;
                                                try
                                                {
                                                    _dDay18 = radGridView1.Rows[iRow].Cells["Day18"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay19 = string.Empty;
                                                try
                                                {
                                                    _dDay19 = radGridView1.Rows[iRow].Cells["Day19"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay20 = string.Empty;
                                                try
                                                {
                                                    _dDay20 = radGridView1.Rows[iRow].Cells["Day20"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay21 = string.Empty;
                                                try
                                                {
                                                    _dDay21 = radGridView1.Rows[iRow].Cells["Day21"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay22 = string.Empty;
                                                try
                                                {
                                                    _dDay22 = radGridView1.Rows[iRow].Cells["Day22"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay23 = string.Empty;
                                                try
                                                {
                                                    _dDay23 = radGridView1.Rows[iRow].Cells["Day23"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay24 = string.Empty;
                                                try
                                                {
                                                    _dDay24 = radGridView1.Rows[iRow].Cells["Day24"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay25 = string.Empty;
                                                try
                                                {
                                                    _dDay25 = radGridView1.Rows[iRow].Cells["Day25"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay26 = string.Empty;
                                                try
                                                {
                                                    _dDay26 = radGridView1.Rows[iRow].Cells["Day26"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay27 = string.Empty;
                                                try
                                                {
                                                    _dDay27 = radGridView1.Rows[iRow].Cells["Day27"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay28 = string.Empty;
                                                try
                                                {
                                                    _dDay28 = radGridView1.Rows[iRow].Cells["Day28"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay29 = string.Empty;
                                                try
                                                {
                                                    _dDay29 = radGridView1.Rows[iRow].Cells["Day29"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay30 = string.Empty;
                                                try
                                                {
                                                    _dDay30 = radGridView1.Rows[iRow].Cells["Day30"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay31 = string.Empty;
                                                try
                                                {
                                                    _dDay31 = radGridView1.Rows[iRow].Cells["Day31"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }


                                                reCount = CountLeave("NT", _dDay1, _dDay2, _dDay3, _dDay4, _dDay5,
                                                    _dDay6,
                                                    _dDay7, _dDay8, _dDay9, _dDay10
                                                    , _dDay11, _dDay12, _dDay13, _dDay14, _dDay15, _dDay16, _dDay17,
                                                    _dDay18,
                                                    _dDay19
                                                    , _dDay20, _dDay21, _dDay22, _dDay23, _dDay24, _dDay25, _dDay26,
                                                    _dDay27,
                                                    _dDay28, _dDay29, _dDay30, _dDay31);
                                                if (_tryExit)
                                                    break;
                                            }
                                        else
                                            for (var i = 1; i <= 31; i++)
                                            {
                                                var _tryExit = false;
                                                if (
                                                    radGridView1.Rows[iRow].Cells["Day" + i].Value.ToString()
                                                        .Equals("NT"))
                                                {
                                                    radGridView1.Rows[iRow].Cells["Day" + i].Value = "X";
                                                    radGridView1.Rows[iRow].Cells["Day" + i].Style.ForeColor = Color.Red;

                                                    _tryExit = true;
                                                }


                                                var _dDay1 = string.Empty;
                                                try
                                                {
                                                    _dDay1 = radGridView1.Rows[iRow].Cells["Day1"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay2 = string.Empty;
                                                try
                                                {
                                                    _dDay2 = radGridView1.Rows[iRow].Cells["Day2"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay3 = string.Empty;
                                                try
                                                {
                                                    _dDay3 = radGridView1.Rows[iRow].Cells["Day3"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay4 = string.Empty;
                                                try
                                                {
                                                    _dDay4 = radGridView1.Rows[iRow].Cells["Day4"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay5 = string.Empty;
                                                try
                                                {
                                                    _dDay5 = radGridView1.Rows[iRow].Cells["Day5"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay6 = string.Empty;
                                                try
                                                {
                                                    _dDay6 = radGridView1.Rows[iRow].Cells["Day6"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay7 = string.Empty;
                                                try
                                                {
                                                    _dDay7 = radGridView1.Rows[iRow].Cells["Day7"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay8 = string.Empty;
                                                try
                                                {
                                                    _dDay8 = radGridView1.Rows[iRow].Cells["Day8"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay9 = string.Empty;
                                                try
                                                {
                                                    _dDay9 = radGridView1.Rows[iRow].Cells["Day9"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay10 = string.Empty;
                                                try
                                                {
                                                    _dDay10 = radGridView1.Rows[iRow].Cells["Day10"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay11 = string.Empty;
                                                try
                                                {
                                                    _dDay11 = radGridView1.Rows[iRow].Cells["Day11"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay12 = string.Empty;
                                                try
                                                {
                                                    _dDay12 = radGridView1.Rows[iRow].Cells["Day12"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay13 = string.Empty;
                                                try
                                                {
                                                    _dDay13 = radGridView1.Rows[iRow].Cells["Day13"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay14 = string.Empty;
                                                try
                                                {
                                                    _dDay14 = radGridView1.Rows[iRow].Cells["Day14"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay15 = string.Empty;
                                                try
                                                {
                                                    _dDay15 = radGridView1.Rows[iRow].Cells["Day15"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay16 = string.Empty;
                                                try
                                                {
                                                    _dDay16 = radGridView1.Rows[iRow].Cells["Day16"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay17 = string.Empty;
                                                try
                                                {
                                                    _dDay17 = radGridView1.Rows[iRow].Cells["Day17"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay18 = string.Empty;
                                                try
                                                {
                                                    _dDay18 = radGridView1.Rows[iRow].Cells["Day18"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay19 = string.Empty;
                                                try
                                                {
                                                    _dDay19 = radGridView1.Rows[iRow].Cells["Day19"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay20 = string.Empty;
                                                try
                                                {
                                                    _dDay20 = radGridView1.Rows[iRow].Cells["Day20"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay21 = string.Empty;
                                                try
                                                {
                                                    _dDay21 = radGridView1.Rows[iRow].Cells["Day21"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay22 = string.Empty;
                                                try
                                                {
                                                    _dDay22 = radGridView1.Rows[iRow].Cells["Day22"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay23 = string.Empty;
                                                try
                                                {
                                                    _dDay23 = radGridView1.Rows[iRow].Cells["Day23"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay24 = string.Empty;
                                                try
                                                {
                                                    _dDay24 = radGridView1.Rows[iRow].Cells["Day24"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay25 = string.Empty;
                                                try
                                                {
                                                    _dDay25 = radGridView1.Rows[iRow].Cells["Day25"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay26 = string.Empty;
                                                try
                                                {
                                                    _dDay26 = radGridView1.Rows[iRow].Cells["Day26"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay27 = string.Empty;
                                                try
                                                {
                                                    _dDay27 = radGridView1.Rows[iRow].Cells["Day27"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay28 = string.Empty;
                                                try
                                                {
                                                    _dDay28 = radGridView1.Rows[iRow].Cells["Day28"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay29 = string.Empty;
                                                try
                                                {
                                                    _dDay29 = radGridView1.Rows[iRow].Cells["Day29"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay30 = string.Empty;
                                                try
                                                {
                                                    _dDay30 = radGridView1.Rows[iRow].Cells["Day30"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay31 = string.Empty;
                                                try
                                                {
                                                    _dDay31 = radGridView1.Rows[iRow].Cells["Day31"].Value.ToString();
                                                }
                                                catch
                                                {
                                                }


                                                reCount = CountLeave("NT", _dDay1, _dDay2, _dDay3, _dDay4, _dDay5,
                                                    _dDay6,
                                                    _dDay7, _dDay8, _dDay9, _dDay10
                                                    , _dDay11, _dDay12, _dDay13, _dDay14, _dDay15, _dDay16, _dDay17,
                                                    _dDay18,
                                                    _dDay19
                                                    , _dDay20, _dDay21, _dDay22, _dDay23, _dDay24, _dDay25, _dDay26,
                                                    _dDay27,
                                                    _dDay28, _dDay29, _dDay30, _dDay31);
                                                if (_tryExit)
                                                    break;
                                            }
                                }
                            }
                        }
                    }
                    _worker.ReportProgress(iRow + 1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                }
        }

        public void FixNTonDT(DataTable dt)
        {
            for (var iRow = 0; iRow < dt.Rows.Count; iRow++)
                try
                {
                    var _UserId = Convert.ToInt32(dt.Rows[iRow]["UserId"]);


                    var _DirectWorking = Convert.ToInt32(EmployeesBLL.DR_GetEmployeeById(_UserId)["DirectWorking"]);
                    var _WorkdayCoefficientEmployeeIdFinal =
                        Convert.ToInt32(dt.Rows[iRow]["WorkdayCoefficientEmployeeIdFinal"]);

                    var _lstWeekend = getWeekend(_DataDate.Month, _DataDate.Year, _DirectWorking);
                    var _WeekendAvailableCount = _lstWeekend.Count;

                    #region Get Day 1-31
                    var _Day1 = string.Empty;
                    try
                    {
                        _Day1 = dt.Rows[iRow]["Day1"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day2 = string.Empty;
                    try
                    {
                        _Day2 = dt.Rows[iRow]["Day2"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day3 = string.Empty;
                    try
                    {
                        _Day3 = dt.Rows[iRow]["Day3"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day4 = string.Empty;
                    try
                    {
                        _Day4 = dt.Rows[iRow]["Day4"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day5 = string.Empty;
                    try
                    {
                        _Day5 = dt.Rows[iRow]["Day5"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day6 = string.Empty;
                    try
                    {
                        _Day6 = dt.Rows[iRow]["Day6"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day7 = string.Empty;
                    try
                    {
                        _Day7 = dt.Rows[iRow]["Day7"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day8 = string.Empty;
                    try
                    {
                        _Day8 = dt.Rows[iRow]["Day8"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day9 = string.Empty;
                    try
                    {
                        _Day9 = dt.Rows[iRow]["Day9"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day10 = string.Empty;
                    try
                    {
                        _Day10 = dt.Rows[iRow]["Day10"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day11 = string.Empty;
                    try
                    {
                        _Day11 = dt.Rows[iRow]["Day11"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day12 = string.Empty;
                    try
                    {
                        _Day12 = dt.Rows[iRow]["Day12"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day13 = string.Empty;
                    try
                    {
                        _Day13 = dt.Rows[iRow]["Day13"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day14 = string.Empty;
                    try
                    {
                        _Day14 = dt.Rows[iRow]["Day14"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day15 = string.Empty;
                    try
                    {
                        _Day15 = dt.Rows[iRow]["Day15"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day16 = string.Empty;
                    try
                    {
                        _Day16 = dt.Rows[iRow]["Day16"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day17 = string.Empty;
                    try
                    {
                        _Day17 = dt.Rows[iRow]["Day17"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day18 = string.Empty;
                    try
                    {
                        _Day18 = dt.Rows[iRow]["Day18"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day19 = string.Empty;
                    try
                    {
                        _Day19 = dt.Rows[iRow]["Day19"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day20 = string.Empty;
                    try
                    {
                        _Day20 = dt.Rows[iRow]["Day20"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day21 = string.Empty;
                    try
                    {
                        _Day21 = dt.Rows[iRow]["Day21"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day22 = string.Empty;
                    try
                    {
                        _Day22 = dt.Rows[iRow]["Day22"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day23 = string.Empty;
                    try
                    {
                        _Day23 = dt.Rows[iRow]["Day23"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day24 = string.Empty;
                    try
                    {
                        _Day24 = dt.Rows[iRow]["Day24"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day25 = string.Empty;
                    try
                    {
                        _Day25 = dt.Rows[iRow]["Day25"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day26 = string.Empty;
                    try
                    {
                        _Day26 = dt.Rows[iRow]["Day26"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day27 = string.Empty;
                    try
                    {
                        _Day27 = dt.Rows[iRow]["Day27"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day28 = string.Empty;
                    try
                    {
                        _Day28 = dt.Rows[iRow]["Day28"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day29 = string.Empty;
                    try
                    {
                        _Day29 = dt.Rows[iRow]["Day29"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day30 = string.Empty;
                    try
                    {
                        _Day30 = dt.Rows[iRow]["Day30"].ToString();
                    }
                    catch
                    {
                    }
                    var _Day31 = string.Empty;
                    try
                    {
                        _Day31 = dt.Rows[iRow]["Day31"].ToString();
                    }
                    catch
                    {
                    }
                    #endregion

                    if (
                        dt.Rows[iRow]["CheckRemark"].ToString()
                            .Contains(new WE().GetErrorNameById(Constants.ERROR_WORKING_DAYS_ID)))
                    {
                        //Đếu biết làm gì
                    }
                    else
                    {
                        if (
                            dt.Rows[iRow]["CheckRemark"].ToString()
                                .Contains(new WE().GetErrorNameById(Constants.ERROR_NGHITUAN_ID)))
                        {
                            var countNT = CountLeave("NT", _Day1, _Day2, _Day3, _Day4, _Day5, _Day6, _Day7, _Day8, _Day9,
                                _Day10
                                , _Day11, _Day12, _Day13, _Day14, _Day15, _Day16, _Day17, _Day18, _Day19
                                , _Day20, _Day21, _Day22, _Day23, _Day24, _Day25, _Day26, _Day27, _Day28, _Day29, _Day30,
                                _Day31);

                            if (countNT - _WeekendAvailableCount == 0)
                            {
                                //Nothing
                            }
                            else
                            {
                                var reCount = countNT;
                                if (reCount < _WeekendAvailableCount)
                                {
                                    while (reCount != _WeekendAvailableCount)
                                        foreach (var day in _lstWeekend)
                                        {
                                            var _tryExit = false;

                                            if (!dt.Rows[iRow]["Day" + day.Day].ToString().Equals("NT"))
                                            {
                                                dt.Rows[iRow]["Day" + day.Day] = "NT";

                                                _tryExit = true;
                                            }

                                            #region Get day 1-31
                                            var _dDay1 = string.Empty;
                                            try
                                            {
                                                _dDay1 = dt.Rows[iRow]["Day1"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay2 = string.Empty;
                                            try
                                            {
                                                _dDay2 = dt.Rows[iRow]["Day2"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay3 = string.Empty;
                                            try
                                            {
                                                _dDay3 = dt.Rows[iRow]["Day3"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay4 = string.Empty;
                                            try
                                            {
                                                _dDay4 = dt.Rows[iRow]["Day4"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay5 = string.Empty;
                                            try
                                            {
                                                _dDay5 = dt.Rows[iRow]["Day5"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay6 = string.Empty;
                                            try
                                            {
                                                _dDay6 = dt.Rows[iRow]["Day6"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay7 = string.Empty;
                                            try
                                            {
                                                _dDay7 = dt.Rows[iRow]["Day7"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay8 = string.Empty;
                                            try
                                            {
                                                _dDay8 = dt.Rows[iRow]["Day8"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay9 = string.Empty;
                                            try
                                            {
                                                _dDay9 = dt.Rows[iRow]["Day9"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay10 = string.Empty;
                                            try
                                            {
                                                _dDay10 = dt.Rows[iRow]["Day10"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay11 = string.Empty;
                                            try
                                            {
                                                _dDay11 = dt.Rows[iRow]["Day11"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay12 = string.Empty;
                                            try
                                            {
                                                _dDay12 = dt.Rows[iRow]["Day12"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay13 = string.Empty;
                                            try
                                            {
                                                _dDay13 = dt.Rows[iRow]["Day13"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay14 = string.Empty;
                                            try
                                            {
                                                _dDay14 = dt.Rows[iRow]["Day14"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay15 = string.Empty;
                                            try
                                            {
                                                _dDay15 = dt.Rows[iRow]["Day15"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay16 = string.Empty;
                                            try
                                            {
                                                _dDay16 = dt.Rows[iRow]["Day16"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay17 = string.Empty;
                                            try
                                            {
                                                _dDay17 = dt.Rows[iRow]["Day17"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay18 = string.Empty;
                                            try
                                            {
                                                _dDay18 = dt.Rows[iRow]["Day18"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay19 = string.Empty;
                                            try
                                            {
                                                _dDay19 = dt.Rows[iRow]["Day19"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay20 = string.Empty;
                                            try
                                            {
                                                _dDay20 = dt.Rows[iRow]["Day20"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay21 = string.Empty;
                                            try
                                            {
                                                _dDay21 = dt.Rows[iRow]["Day21"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay22 = string.Empty;
                                            try
                                            {
                                                _dDay22 = dt.Rows[iRow]["Day22"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay23 = string.Empty;
                                            try
                                            {
                                                _dDay23 = dt.Rows[iRow]["Day23"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay24 = string.Empty;
                                            try
                                            {
                                                _dDay24 = dt.Rows[iRow]["Day24"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay25 = string.Empty;
                                            try
                                            {
                                                _dDay25 = dt.Rows[iRow]["Day25"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay26 = string.Empty;
                                            try
                                            {
                                                _dDay26 = dt.Rows[iRow]["Day26"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay27 = string.Empty;
                                            try
                                            {
                                                _dDay27 = dt.Rows[iRow]["Day27"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay28 = string.Empty;
                                            try
                                            {
                                                _dDay28 = dt.Rows[iRow]["Day28"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay29 = string.Empty;
                                            try
                                            {
                                                _dDay29 = dt.Rows[iRow]["Day29"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay30 = string.Empty;
                                            try
                                            {
                                                _dDay30 = dt.Rows[iRow]["Day30"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            var _dDay31 = string.Empty;
                                            try
                                            {
                                                _dDay31 = dt.Rows[iRow]["Day31"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                            #endregion

                                            reCount = CountLeave("NT", _dDay1, _dDay2, _dDay3, _dDay4, _dDay5, _dDay6,
                                                _dDay7, _dDay8, _dDay9, _dDay10
                                                , _dDay11, _dDay12, _dDay13, _dDay14, _dDay15, _dDay16, _dDay17, _dDay18,
                                                _dDay19
                                                , _dDay20, _dDay21, _dDay22, _dDay23, _dDay24, _dDay25, _dDay26, _dDay27,
                                                _dDay28, _dDay29, _dDay30, _dDay31);
                                            if (_tryExit)
                                                break;
                                        }
                                }
                                else
                                {
                                    if (reCount > _WeekendAvailableCount)
                                        if (_DirectWorking == 0)
                                            foreach (var day in _lstWeekend)
                                            {
                                                var _tryExit = false;
                                                if (_DirectWorking == 0)
                                                    if (dt.Rows[iRow]["Day" + day.Day].ToString().Equals("NT"))
                                                    {
                                                        dt.Rows[iRow]["Day" + day.Day] = "X";

                                                        _tryExit = true;
                                                    }

                                                #region Get day 1-31
                                                var _dDay1 = string.Empty;
                                                try
                                                {
                                                    _dDay1 = dt.Rows[iRow]["Day1"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay2 = string.Empty;
                                                try
                                                {
                                                    _dDay2 = dt.Rows[iRow]["Day2"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay3 = string.Empty;
                                                try
                                                {
                                                    _dDay3 = dt.Rows[iRow]["Day3"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay4 = string.Empty;
                                                try
                                                {
                                                    _dDay4 = dt.Rows[iRow]["Day4"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay5 = string.Empty;
                                                try
                                                {
                                                    _dDay5 = dt.Rows[iRow]["Day5"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay6 = string.Empty;
                                                try
                                                {
                                                    _dDay6 = dt.Rows[iRow]["Day6"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay7 = string.Empty;
                                                try
                                                {
                                                    _dDay7 = dt.Rows[iRow]["Day7"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay8 = string.Empty;
                                                try
                                                {
                                                    _dDay8 = dt.Rows[iRow]["Day8"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay9 = string.Empty;
                                                try
                                                {
                                                    _dDay9 = dt.Rows[iRow]["Day9"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay10 = string.Empty;
                                                try
                                                {
                                                    _dDay10 = dt.Rows[iRow]["Day10"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay11 = string.Empty;
                                                try
                                                {
                                                    _dDay11 = dt.Rows[iRow]["Day11"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay12 = string.Empty;
                                                try
                                                {
                                                    _dDay12 = dt.Rows[iRow]["Day12"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay13 = string.Empty;
                                                try
                                                {
                                                    _dDay13 = dt.Rows[iRow]["Day13"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay14 = string.Empty;
                                                try
                                                {
                                                    _dDay14 = dt.Rows[iRow]["Day14"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay15 = string.Empty;
                                                try
                                                {
                                                    _dDay15 = dt.Rows[iRow]["Day15"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay16 = string.Empty;
                                                try
                                                {
                                                    _dDay16 = dt.Rows[iRow]["Day16"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay17 = string.Empty;
                                                try
                                                {
                                                    _dDay17 = dt.Rows[iRow]["Day17"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay18 = string.Empty;
                                                try
                                                {
                                                    _dDay18 = dt.Rows[iRow]["Day18"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay19 = string.Empty;
                                                try
                                                {
                                                    _dDay19 = dt.Rows[iRow]["Day19"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay20 = string.Empty;
                                                try
                                                {
                                                    _dDay20 = dt.Rows[iRow]["Day20"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay21 = string.Empty;
                                                try
                                                {
                                                    _dDay21 = dt.Rows[iRow]["Day21"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay22 = string.Empty;
                                                try
                                                {
                                                    _dDay22 = dt.Rows[iRow]["Day22"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay23 = string.Empty;
                                                try
                                                {
                                                    _dDay23 = dt.Rows[iRow]["Day23"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay24 = string.Empty;
                                                try
                                                {
                                                    _dDay24 = dt.Rows[iRow]["Day24"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay25 = string.Empty;
                                                try
                                                {
                                                    _dDay25 = dt.Rows[iRow]["Day25"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay26 = string.Empty;
                                                try
                                                {
                                                    _dDay26 = dt.Rows[iRow]["Day26"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay27 = string.Empty;
                                                try
                                                {
                                                    _dDay27 = dt.Rows[iRow]["Day27"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay28 = string.Empty;
                                                try
                                                {
                                                    _dDay28 = dt.Rows[iRow]["Day28"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay29 = string.Empty;
                                                try
                                                {
                                                    _dDay29 = dt.Rows[iRow]["Day29"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay30 = string.Empty;
                                                try
                                                {
                                                    _dDay30 = dt.Rows[iRow]["Day30"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay31 = string.Empty;
                                                try
                                                {
                                                    _dDay31 = dt.Rows[iRow]["Day31"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                #endregion

                                                reCount = CountLeave("NT", _dDay1, _dDay2, _dDay3, _dDay4, _dDay5,
                                                    _dDay6,
                                                    _dDay7, _dDay8, _dDay9, _dDay10
                                                    , _dDay11, _dDay12, _dDay13, _dDay14, _dDay15, _dDay16, _dDay17,
                                                    _dDay18,
                                                    _dDay19
                                                    , _dDay20, _dDay21, _dDay22, _dDay23, _dDay24, _dDay25, _dDay26,
                                                    _dDay27,
                                                    _dDay28, _dDay29, _dDay30, _dDay31);
                                                if (_tryExit)
                                                    break;
                                            }
                                        else
                                            for (var i = 1; i <= 31; i++)
                                            {
                                                var _tryExit = false;
                                                if (dt.Rows[iRow]["Day" + i].ToString().Equals("NT"))
                                                {
                                                    dt.Rows[iRow]["Day" + i] = "X";

                                                    _tryExit = true;
                                                }

                                                #region Get day 1-31
                                                var _dDay1 = string.Empty;
                                                try
                                                {
                                                    _dDay1 = dt.Rows[iRow]["Day1"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay2 = string.Empty;
                                                try
                                                {
                                                    _dDay2 = dt.Rows[iRow]["Day2"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay3 = string.Empty;
                                                try
                                                {
                                                    _dDay3 = dt.Rows[iRow]["Day3"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay4 = string.Empty;
                                                try
                                                {
                                                    _dDay4 = dt.Rows[iRow]["Day4"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay5 = string.Empty;
                                                try
                                                {
                                                    _dDay5 = dt.Rows[iRow]["Day5"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay6 = string.Empty;
                                                try
                                                {
                                                    _dDay6 = dt.Rows[iRow]["Day6"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay7 = string.Empty;
                                                try
                                                {
                                                    _dDay7 = dt.Rows[iRow]["Day7"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay8 = string.Empty;
                                                try
                                                {
                                                    _dDay8 = dt.Rows[iRow]["Day8"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay9 = string.Empty;
                                                try
                                                {
                                                    _dDay9 = dt.Rows[iRow]["Day9"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay10 = string.Empty;
                                                try
                                                {
                                                    _dDay10 = dt.Rows[iRow]["Day10"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay11 = string.Empty;
                                                try
                                                {
                                                    _dDay11 = dt.Rows[iRow]["Day11"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay12 = string.Empty;
                                                try
                                                {
                                                    _dDay12 = dt.Rows[iRow]["Day12"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay13 = string.Empty;
                                                try
                                                {
                                                    _dDay13 = dt.Rows[iRow]["Day13"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay14 = string.Empty;
                                                try
                                                {
                                                    _dDay14 = dt.Rows[iRow]["Day14"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay15 = string.Empty;
                                                try
                                                {
                                                    _dDay15 = dt.Rows[iRow]["Day15"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay16 = string.Empty;
                                                try
                                                {
                                                    _dDay16 = dt.Rows[iRow]["Day16"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay17 = string.Empty;
                                                try
                                                {
                                                    _dDay17 = dt.Rows[iRow]["Day17"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay18 = string.Empty;
                                                try
                                                {
                                                    _dDay18 = dt.Rows[iRow]["Day18"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay19 = string.Empty;
                                                try
                                                {
                                                    _dDay19 = dt.Rows[iRow]["Day19"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay20 = string.Empty;
                                                try
                                                {
                                                    _dDay20 = dt.Rows[iRow]["Day20"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay21 = string.Empty;
                                                try
                                                {
                                                    _dDay21 = dt.Rows[iRow]["Day21"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay22 = string.Empty;
                                                try
                                                {
                                                    _dDay22 = dt.Rows[iRow]["Day22"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay23 = string.Empty;
                                                try
                                                {
                                                    _dDay23 = dt.Rows[iRow]["Day23"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay24 = string.Empty;
                                                try
                                                {
                                                    _dDay24 = dt.Rows[iRow]["Day24"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay25 = string.Empty;
                                                try
                                                {
                                                    _dDay25 = dt.Rows[iRow]["Day25"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay26 = string.Empty;
                                                try
                                                {
                                                    _dDay26 = dt.Rows[iRow]["Day26"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay27 = string.Empty;
                                                try
                                                {
                                                    _dDay27 = dt.Rows[iRow]["Day27"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay28 = string.Empty;
                                                try
                                                {
                                                    _dDay28 = dt.Rows[iRow]["Day28"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay29 = string.Empty;
                                                try
                                                {
                                                    _dDay29 = dt.Rows[iRow]["Day29"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay30 = string.Empty;
                                                try
                                                {
                                                    _dDay30 = dt.Rows[iRow]["Day30"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                var _dDay31 = string.Empty;
                                                try
                                                {
                                                    _dDay31 = dt.Rows[iRow]["Day31"].ToString();
                                                }
                                                catch
                                                {
                                                }
                                                #endregion

                                                reCount = CountLeave("NT", _dDay1, _dDay2, _dDay3, _dDay4, _dDay5,
                                                    _dDay6,
                                                    _dDay7, _dDay8, _dDay9, _dDay10
                                                    , _dDay11, _dDay12, _dDay13, _dDay14, _dDay15, _dDay16, _dDay17,
                                                    _dDay18,
                                                    _dDay19
                                                    , _dDay20, _dDay21, _dDay22, _dDay23, _dDay24, _dDay25, _dDay26,
                                                    _dDay27,
                                                    _dDay28, _dDay29, _dDay30, _dDay31);
                                                if (_tryExit)
                                                    break;
                                            }
                                }
                            }
                        }
                    }

                    #region Get day 1-31
                    var _fDay1 = string.Empty;
                    try
                    {
                        _fDay1 = dt.Rows[iRow]["Day1"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay2 = string.Empty;
                    try
                    {
                        _fDay2 = dt.Rows[iRow]["Day2"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay3 = string.Empty;
                    try
                    {
                        _fDay3 = dt.Rows[iRow]["Day3"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay4 = string.Empty;
                    try
                    {
                        _fDay4 = dt.Rows[iRow]["Day4"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay5 = string.Empty;
                    try
                    {
                        _fDay5 = dt.Rows[iRow]["Day5"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay6 = string.Empty;
                    try
                    {
                        _fDay6 = dt.Rows[iRow]["Day6"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay7 = string.Empty;
                    try
                    {
                        _fDay7 = dt.Rows[iRow]["Day7"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay8 = string.Empty;
                    try
                    {
                        _fDay8 = dt.Rows[iRow]["Day8"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay9 = string.Empty;
                    try
                    {
                        _fDay9 = dt.Rows[iRow]["Day9"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay10 = string.Empty;
                    try
                    {
                        _fDay10 = dt.Rows[iRow]["Day10"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay11 = string.Empty;
                    try
                    {
                        _fDay11 = dt.Rows[iRow]["Day11"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay12 = string.Empty;
                    try
                    {
                        _fDay12 = dt.Rows[iRow]["Day12"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay13 = string.Empty;
                    try
                    {
                        _fDay13 = dt.Rows[iRow]["Day13"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay14 = string.Empty;
                    try
                    {
                        _fDay14 = dt.Rows[iRow]["Day14"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay15 = string.Empty;
                    try
                    {
                        _fDay15 = dt.Rows[iRow]["Day15"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay16 = string.Empty;
                    try
                    {
                        _fDay16 = dt.Rows[iRow]["Day16"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay17 = string.Empty;
                    try
                    {
                        _fDay17 = dt.Rows[iRow]["Day17"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay18 = string.Empty;
                    try
                    {
                        _fDay18 = dt.Rows[iRow]["Day18"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay19 = string.Empty;
                    try
                    {
                        _fDay19 = dt.Rows[iRow]["Day19"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay20 = string.Empty;
                    try
                    {
                        _fDay20 = dt.Rows[iRow]["Day20"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay21 = string.Empty;
                    try
                    {
                        _fDay21 = dt.Rows[iRow]["Day21"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay22 = string.Empty;
                    try
                    {
                        _fDay22 = dt.Rows[iRow]["Day22"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay23 = string.Empty;
                    try
                    {
                        _fDay23 = dt.Rows[iRow]["Day23"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay24 = string.Empty;
                    try
                    {
                        _fDay24 = dt.Rows[iRow]["Day24"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay25 = string.Empty;
                    try
                    {
                        _fDay25 = dt.Rows[iRow]["Day25"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay26 = string.Empty;
                    try
                    {
                        _fDay26 = dt.Rows[iRow]["Day26"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay27 = string.Empty;
                    try
                    {
                        _fDay27 = dt.Rows[iRow]["Day27"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay28 = string.Empty;
                    try
                    {
                        _fDay28 = dt.Rows[iRow]["Day28"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay29 = string.Empty;
                    try
                    {
                        _fDay29 = dt.Rows[iRow]["Day29"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay30 = string.Empty;
                    try
                    {
                        _fDay30 = dt.Rows[iRow]["Day30"].ToString();
                    }
                    catch
                    {
                    }
                    var _fDay31 = string.Empty;
                    try
                    {
                        _fDay31 = dt.Rows[iRow]["Day31"].ToString();
                    }
                    catch
                    {
                    }
                    #endregion

                    #region Re-calculate leaves
                    dt.Rows[iRow]["OmDNBHXH"] = CountLeave(Constants.LEAVE_TYPE_O_DAI_NGAY_HUONG_BHXH_CODE, _fDay1, _fDay2, _fDay3, _fDay4, _fDay5, _fDay6, _fDay7, _fDay8, _fDay9, _fDay10 , 
                                                                _fDay11, _fDay12, _fDay13, _fDay14, _fDay15, _fDay16, _fDay17, _fDay18, _fDay19 , _fDay20, 
                                                                _fDay21, _fDay22, _fDay23, _fDay24, _fDay25, _fDay26, _fDay27, _fDay28, _fDay29, _fDay30, 
                                                                _fDay31);
                    dt.Rows[iRow]["Om"] = CountLeave(Constants.LEAVE_TYPE_O_BAN_THAN_CODE, _fDay1, _fDay2, _fDay3, _fDay4, _fDay5, _fDay6, _fDay7, _fDay8, _fDay9, _fDay10,
                                                                _fDay11, _fDay12, _fDay13, _fDay14, _fDay15, _fDay16, _fDay17, _fDay18, _fDay19, _fDay20,
                                                                _fDay21, _fDay22, _fDay23, _fDay24, _fDay25, _fDay26, _fDay27, _fDay28, _fDay29, _fDay30,
                                                                _fDay31);
                    dt.Rows[iRow]["OmDN"] = CountLeave(Constants.LEAVE_TYPE_O_DAI_NGAY_CODE, _fDay1, _fDay2, _fDay3, _fDay4, _fDay5, _fDay6, _fDay7, _fDay8, _fDay9, _fDay10,
                                                                _fDay11, _fDay12, _fDay13, _fDay14, _fDay15, _fDay16, _fDay17, _fDay18, _fDay19, _fDay20,
                                                                _fDay21, _fDay22, _fDay23, _fDay24, _fDay25, _fDay26, _fDay27, _fDay28, _fDay29, _fDay30,
                                                                _fDay31);
                    dt.Rows[iRow]["KHH"] = CountLeave(Constants.LEAVE_TYPE_KHHDS_CODE, _fDay1, _fDay2, _fDay3, _fDay4, _fDay5, _fDay6, _fDay7, _fDay8, _fDay9, _fDay10,
                                                                _fDay11, _fDay12, _fDay13, _fDay14, _fDay15, _fDay16, _fDay17, _fDay18, _fDay19, _fDay20,
                                                                _fDay21, _fDay22, _fDay23, _fDay24, _fDay25, _fDay26, _fDay27, _fDay28, _fDay29, _fDay30,
                                                                _fDay31);
                    dt.Rows[iRow]["Co"] = CountLeave(Constants.LEAVE_TYPE_CON_OM_CODE, _fDay1, _fDay2, _fDay3, _fDay4, _fDay5, _fDay6, _fDay7, _fDay8, _fDay9, _fDay10,
                                                                _fDay11, _fDay12, _fDay13, _fDay14, _fDay15, _fDay16, _fDay17, _fDay18, _fDay19, _fDay20,
                                                                _fDay21, _fDay22, _fDay23, _fDay24, _fDay25, _fDay26, _fDay27, _fDay28, _fDay29, _fDay30,
                                                                _fDay31);
                    dt.Rows[iRow]["TS"] = CountLeave(Constants.LEAVE_TYPE_THAI_SAN_CODE, _fDay1, _fDay2, _fDay3, _fDay4, _fDay5, _fDay6, _fDay7, _fDay8, _fDay9, _fDay10,
                                                                _fDay11, _fDay12, _fDay13, _fDay14, _fDay15, _fDay16, _fDay17, _fDay18, _fDay19, _fDay20,
                                                                _fDay21, _fDay22, _fDay23, _fDay24, _fDay25, _fDay26, _fDay27, _fDay28, _fDay29, _fDay30,
                                                                _fDay31);
                    dt.Rows[iRow]["ST"] = CountLeave(Constants.LEAVE_TYPE_SAY_THAI_CODE, _fDay1, _fDay2, _fDay3, _fDay4, _fDay5, _fDay6, _fDay7, _fDay8, _fDay9, _fDay10,
                                                                _fDay11, _fDay12, _fDay13, _fDay14, _fDay15, _fDay16, _fDay17, _fDay18, _fDay19, _fDay20,
                                                                _fDay21, _fDay22, _fDay23, _fDay24, _fDay25, _fDay26, _fDay27, _fDay28, _fDay29, _fDay30,
                                                                _fDay31);
                    dt.Rows[iRow]["Khamthai"] = CountLeave(Constants.LEAVE_TYPE_KHAM_THAI_CODE, _fDay1, _fDay2, _fDay3, _fDay4, _fDay5, _fDay6, _fDay7, _fDay8, _fDay9, _fDay10,
                                                                _fDay11, _fDay12, _fDay13, _fDay14, _fDay15, _fDay16, _fDay17, _fDay18, _fDay19, _fDay20,
                                                                _fDay21, _fDay22, _fDay23, _fDay24, _fDay25, _fDay26, _fDay27, _fDay28, _fDay29, _fDay30,
                                                                _fDay31);
                    dt.Rows[iRow]["TNLD"] = CountLeave(Constants.LEAVE_TYPE_TNLD_CODE, _fDay1, _fDay2, _fDay3, _fDay4, _fDay5, _fDay6, _fDay7, _fDay8, _fDay9, _fDay10,
                                                                _fDay11, _fDay12, _fDay13, _fDay14, _fDay15, _fDay16, _fDay17, _fDay18, _fDay19, _fDay20,
                                                                _fDay21, _fDay22, _fDay23, _fDay24, _fDay25, _fDay26, _fDay27, _fDay28, _fDay29, _fDay30,
                                                                _fDay31);
                    dt.Rows[iRow]["F"] = CountLeave(Constants.LEAVE_TYPE_F_NAM_CODE, _fDay1, _fDay2, _fDay3, _fDay4, _fDay5, _fDay6, _fDay7, _fDay8, _fDay9, _fDay10,
                                                                _fDay11, _fDay12, _fDay13, _fDay14, _fDay15, _fDay16, _fDay17, _fDay18, _fDay19, _fDay20,
                                                                _fDay21, _fDay22, _fDay23, _fDay24, _fDay25, _fDay26, _fDay27, _fDay28, _fDay29, _fDay30,
                                                                _fDay31);
                    dt.Rows[iRow]["Diduong"] = CountLeave(Constants.LEAVE_TYPE_F_DI_DUONG_CODE, _fDay1, _fDay2, _fDay3, _fDay4, _fDay5, _fDay6, _fDay7, _fDay8, _fDay9, _fDay10,
                                                                _fDay11, _fDay12, _fDay13, _fDay14, _fDay15, _fDay16, _fDay17, _fDay18, _fDay19, _fDay20,
                                                                _fDay21, _fDay22, _fDay23, _fDay24, _fDay25, _fDay26, _fDay27, _fDay28, _fDay29, _fDay30,
                                                                _fDay31);
                    dt.Rows[iRow]["CTac"] = CountLeave(Constants.LEAVE_TYPE_F_CONG_TAC_CODE, _fDay1, _fDay2, _fDay3, _fDay4, _fDay5, _fDay6, _fDay7, _fDay8, _fDay9, _fDay10,
                                                                _fDay11, _fDay12, _fDay13, _fDay14, _fDay15, _fDay16, _fDay17, _fDay18, _fDay19, _fDay20,
                                                                _fDay21, _fDay22, _fDay23, _fDay24, _fDay25, _fDay26, _fDay27, _fDay28, _fDay29, _fDay30,
                                                                _fDay31);
                    dt.Rows[iRow]["Fdb"] = CountLeave(Constants.LEAVE_TYPE_FDB_CODE, _fDay1, _fDay2, _fDay3, _fDay4, _fDay5, _fDay6, _fDay7, _fDay8, _fDay9, _fDay10,
                                                                _fDay11, _fDay12, _fDay13, _fDay14, _fDay15, _fDay16, _fDay17, _fDay18, _fDay19, _fDay20,
                                                                _fDay21, _fDay22, _fDay23, _fDay24, _fDay25, _fDay26, _fDay27, _fDay28, _fDay29, _fDay30,
                                                                _fDay31);
                    dt.Rows[iRow]["H1"] = CountLeave(Constants.LEAVE_TYPE_HOC_1_CODE, _fDay1, _fDay2, _fDay3, _fDay4, _fDay5, _fDay6, _fDay7, _fDay8, _fDay9, _fDay10,
                                                                _fDay11, _fDay12, _fDay13, _fDay14, _fDay15, _fDay16, _fDay17, _fDay18, _fDay19, _fDay20,
                                                                _fDay21, _fDay22, _fDay23, _fDay24, _fDay25, _fDay26, _fDay27, _fDay28, _fDay29, _fDay30,
                                                                _fDay31);
                    dt.Rows[iRow]["H2"] = CountLeave(Constants.LEAVE_TYPE_HOC_2_CODE, _fDay1, _fDay2, _fDay3, _fDay4, _fDay5, _fDay6, _fDay7, _fDay8, _fDay9, _fDay10,
                                                                _fDay11, _fDay12, _fDay13, _fDay14, _fDay15, _fDay16, _fDay17, _fDay18, _fDay19, _fDay20,
                                                                _fDay21, _fDay22, _fDay23, _fDay24, _fDay25, _fDay26, _fDay27, _fDay28, _fDay29, _fDay30,
                                                                _fDay31);
                    dt.Rows[iRow]["H3"] = CountLeave(Constants.LEAVE_TYPE_HOC_3_CODE, _fDay1, _fDay2, _fDay3, _fDay4, _fDay5, _fDay6, _fDay7, _fDay8, _fDay9, _fDay10,
                                                                _fDay11, _fDay12, _fDay13, _fDay14, _fDay15, _fDay16, _fDay17, _fDay18, _fDay19, _fDay20,
                                                                _fDay21, _fDay22, _fDay23, _fDay24, _fDay25, _fDay26, _fDay27, _fDay28, _fDay29, _fDay30,
                                                                _fDay31);
                    dt.Rows[iRow]["H4"] = CountLeave(Constants.LEAVE_TYPE_HOC_4_CODE, _fDay1, _fDay2, _fDay3, _fDay4, _fDay5, _fDay6, _fDay7, _fDay8, _fDay9, _fDay10,
                                                                _fDay11, _fDay12, _fDay13, _fDay14, _fDay15, _fDay16, _fDay17, _fDay18, _fDay19, _fDay20,
                                                                _fDay21, _fDay22, _fDay23, _fDay24, _fDay25, _fDay26, _fDay27, _fDay28, _fDay29, _fDay30,
                                                                _fDay31);
                    dt.Rows[iRow]["H5"] = CountLeave(Constants.LEAVE_TYPE_HOC_5_CODE, _fDay1, _fDay2, _fDay3, _fDay4, _fDay5, _fDay6, _fDay7, _fDay8, _fDay9, _fDay10,
                                                                _fDay11, _fDay12, _fDay13, _fDay14, _fDay15, _fDay16, _fDay17, _fDay18, _fDay19, _fDay20,
                                                                _fDay21, _fDay22, _fDay23, _fDay24, _fDay25, _fDay26, _fDay27, _fDay28, _fDay29, _fDay30,
                                                                _fDay31);
                    dt.Rows[iRow]["H6"] = CountLeave(Constants.LEAVE_TYPE_HOC_6_CODE, _fDay1, _fDay2, _fDay3, _fDay4, _fDay5, _fDay6, _fDay7, _fDay8, _fDay9, _fDay10,
                                                                _fDay11, _fDay12, _fDay13, _fDay14, _fDay15, _fDay16, _fDay17, _fDay18, _fDay19, _fDay20,
                                                                _fDay21, _fDay22, _fDay23, _fDay24, _fDay25, _fDay26, _fDay27, _fDay28, _fDay29, _fDay30,
                                                                _fDay31);
                    dt.Rows[iRow]["H7"] = CountLeave(Constants.LEAVE_TYPE_HOC_7_CODE, _fDay1, _fDay2, _fDay3, _fDay4, _fDay5, _fDay6, _fDay7, _fDay8, _fDay9, _fDay10,
                                                                _fDay11, _fDay12, _fDay13, _fDay14, _fDay15, _fDay16, _fDay17, _fDay18, _fDay19, _fDay20,
                                                                _fDay21, _fDay22, _fDay23, _fDay24, _fDay25, _fDay26, _fDay27, _fDay28, _fDay29, _fDay30,
                                                                _fDay31);
                    dt.Rows[iRow]["DinhChiCT"] = CountLeave(Constants.LEAVE_TYPE_DINH_CHI_CONG_TAC_CODE, _fDay1, _fDay2, _fDay3, _fDay4, _fDay5, _fDay6, _fDay7, _fDay8, _fDay9, _fDay10,
                                                                _fDay11, _fDay12, _fDay13, _fDay14, _fDay15, _fDay16, _fDay17, _fDay18, _fDay19, _fDay20,
                                                                _fDay21, _fDay22, _fDay23, _fDay24, _fDay25, _fDay26, _fDay27, _fDay28, _fDay29, _fDay30,
                                                                _fDay31);
                    dt.Rows[iRow]["Ro"] = CountLeave(Constants.LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO_CODE, _fDay1, _fDay2, _fDay3, _fDay4, _fDay5, _fDay6, _fDay7, _fDay8, _fDay9, _fDay10,
                                                                _fDay11, _fDay12, _fDay13, _fDay14, _fDay15, _fDay16, _fDay17, _fDay18, _fDay19, _fDay20,
                                                                _fDay21, _fDay22, _fDay23, _fDay24, _fDay25, _fDay26, _fDay27, _fDay28, _fDay29, _fDay30,
                                                                _fDay31);
                    dt.Rows[iRow]["Ko"] = CountLeave(Constants.LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO_CODE, _fDay1, _fDay2, _fDay3, _fDay4, _fDay5, _fDay6, _fDay7, _fDay8, _fDay9, _fDay10,
                                                                _fDay11, _fDay12, _fDay13, _fDay14, _fDay15, _fDay16, _fDay17, _fDay18, _fDay19, _fDay20,
                                                                _fDay21, _fDay22, _fDay23, _fDay24, _fDay25, _fDay26, _fDay27, _fDay28, _fDay29, _fDay30,
                                                                _fDay31);
                    dt.Rows[iRow]["NghiBu"] = CountLeave(Constants.LEAVE_TYPE_NGHI_BU_CODE, _fDay1, _fDay2, _fDay3, _fDay4, _fDay5, _fDay6, _fDay7, _fDay8, _fDay9, _fDay10,
                                                                _fDay11, _fDay12, _fDay13, _fDay14, _fDay15, _fDay16, _fDay17, _fDay18, _fDay19, _fDay20,
                                                                _fDay21, _fDay22, _fDay23, _fDay24, _fDay25, _fDay26, _fDay27, _fDay28, _fDay29, _fDay30,
                                                                _fDay31);
                    #endregion

                    _worker.ReportProgress(-1, string.Format("Fixing {0}...", iRow + 1));
                    if (iRow + 1 == radGridView1.Rows.Count)
                        _worker.ReportProgress(-1, "Finalizing and formatting...");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private int CountLeave(string compare, string D1, string D2, string D3, string D4, string D5, string D6,
            string D7, string D8, string D9, string D10
            , string D11, string D12, string D13, string D14, string D15, string D16, string D17, string D18, string D19,
            string D20
            , string D21, string D22, string D23, string D24, string D25, string D26, string D27, string D28, string D29,
            string D30, string D31)
        {
            var count = 0;
            if (D1.Equals(compare))
                count++;
            if (D2.Equals(compare))
                count++;
            if (D3.Equals(compare))
                count++;
            if (D4.Equals(compare))
                count++;
            if (D5.Equals(compare))
                count++;
            if (D6.Equals(compare))
                count++;
            if (D7.Equals(compare))
                count++;
            if (D8.Equals(compare))
                count++;
            if (D9.Equals(compare))
                count++;
            if (D10.Equals(compare))
                count++;
            if (D11.Equals(compare))
                count++;
            if (D12.Equals(compare))
                count++;
            if (D13.Equals(compare))
                count++;
            if (D14.Equals(compare))
                count++;
            if (D15.Equals(compare))
                count++;
            if (D16.Equals(compare))
                count++;
            if (D17.Equals(compare))
                count++;
            if (D18.Equals(compare))
                count++;
            if (D19.Equals(compare))
                count++;
            if (D20.Equals(compare))
                count++;
            if (D21.Equals(compare))
                count++;
            if (D22.Equals(compare))
                count++;
            if (D23.Equals(compare))
                count++;
            if (D24.Equals(compare))
                count++;
            if (D25.Equals(compare))
                count++;
            if (D26.Equals(compare))
                count++;
            if (D27.Equals(compare))
                count++;
            if (D28.Equals(compare))
                count++;
            if (D29.Equals(compare))
                count++;
            if (D30.Equals(compare))
                count++;
            if (D31.Equals(compare))
                count++;
            return count;
        }

        public static List<DateTime> getWeekend(int Month, int Year, int DirectWorking)
        {
            var lstSundays = new List<DateTime>();
            var intMonth = Month;
            var intYear = Year;
            var ci = new CultureInfo("en-US");
            for (var i = 1; i <= ci.Calendar.GetDaysInMonth(intYear, intMonth); i++)
                if (DirectWorking == 0)
                {
                    if ((new DateTime(intYear, intMonth, i).DayOfWeek == DayOfWeek.Saturday) ||
                        (new DateTime(intYear, intMonth, i).DayOfWeek == DayOfWeek.Sunday))
                        lstSundays.Add(new DateTime(intYear, intMonth, i));
                }
                else
                {
                    if (DirectWorking == 1)
                        if (new DateTime(intYear, intMonth, i).DayOfWeek == DayOfWeek.Sunday)
                            lstSundays.Add(new DateTime(intYear, intMonth, i));
                }
            return lstSundays;
        }

        public List<DateTime> getSundays()
        {
            var lstSundays = new List<DateTime>();
            var intMonth = _DataDate.Month;
            var intYear = _DataDate.Year;
            var ci = new CultureInfo("en-US");
            for (var i = 1; i <= ci.Calendar.GetDaysInMonth(intYear, intMonth); i++)
                if (new DateTime(intYear, intMonth, i).DayOfWeek == DayOfWeek.Sunday)
                    lstSundays.Add(new DateTime(intYear, intMonth, i));
            return lstSundays;
        }

        public List<DateTime> getSaturdays()
        {
            var lstSundays = new List<DateTime>();
            var intMonth = _DataDate.Month;
            var intYear = _DataDate.Year;
            var ci = new CultureInfo("en-US");
            for (var i = 1; i <= ci.Calendar.GetDaysInMonth(intYear, intMonth); i++)
                if (new DateTime(intYear, intMonth, i).DayOfWeek == DayOfWeek.Saturday)
                    lstSundays.Add(new DateTime(intYear, intMonth, i));
            return lstSundays;
        }

        public void InitData()
        {
            BS_LeaveCode.DataSource = Constants.GetAllLeaveCode();

            radDropDownList1.DataSource = new WE().GetAllError();
            radDropDownList1.DisplayMember = "WEName";
            radDropDownList1.ValueMember = "WEId";
            radDropDownList1.SelectedValue = 0;

            DataTable dt = WorkdayCoefficientEmployeesFinalBLL.GetDataTableByDataDateForWorkingDay_ERROR(
                            _DataDate.Month, _DataDate.Year,
                            clsGlobal.SalaryIsVCQLNN, _DeptIds, 9999, Constants.DataType_Run,
                            radDropDownList1.SelectedValue.ToString(), 0);
            radGridView1.DataSource = dt;
            if (radGridView1.Rows.Count > 0) radGridView1.Rows[0].IsCurrent = true;

            radGridView2.DataSource = WorkdayCoefficientEmployeesFinalBLL.GetDataTableByDataDateForWorkingDay_ERROR(
                            _DataDate.Month, _DataDate.Year,
                            clsGlobal.SalaryIsVCQLNN, _DeptIds, 9999, Constants.DataType_Run,
                            radDropDownList1.SelectedValue.ToString(), Convert.ToInt32(radGridView1.CurrentRow.Cells["UserId"].Value));

            var LeaveDT = EmployeeLeaveBLL.GetDTByUserId_Date(Convert.ToInt32(radGridView1.CurrentRow.Cells["UserId"].Value), _DataDate.Month, _DataDate.Year);
            if (LeaveDT.Rows.Count > 0)
                radGridView4.DataSource = LeaveDT;
            else
                radGridView4.DataSource = null;

            Utilities.Utilities.GridFormatting(radGridView1);
            Utilities.Utilities.GridFormatting(radGridView2);
            Utilities.Utilities.GridFormatting(radGridView4);

            HideCol();
            radLabelElement1.Text = "Tổng cộng: " + Utilities.Utilities.GetDataRowCount(radGridView1.ChildRows);

            radDropDownList1.SelectedValueChanged += RadDropDownList1_SelectedValueChanged;
        }

        private void RadDropDownList1_SelectedValueChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;

            if (radDropDownList1.SelectedItem.Text == string.Empty)
            {
                radGridView1.FilterDescriptors.Clear();
            }
            else
            {
                radGridView1.FilterDescriptors.Clear();
                var filter = new FilterDescriptor();
                filter.PropertyName = "CheckRemark";
                filter.Operator = FilterOperator.Contains;
                filter.Value = radDropDownList1.SelectedValue;
                filter.IsFilterEditor = true;
                radGridView1.FilterDescriptors.Add(filter);
            }

            HideCol();
            Cursor.Current = Cursors.Default;
        }

        /*public List<ListERROR> EnumToList()
        {
            var _tmp = new List<ListERROR>();
            _tmp.Add(new ListERROR(24061991, string.Empty));
            _tmp.Add(new ListERROR((int) WORKDAYERROR.ERROR_NGHITUAN,
                Utilities.Utilities.GetEnumDescription(WORKDAYERROR.ERROR_NGHITUAN)));
            _tmp.Add(new ListERROR((int) WORKDAYERROR.ERROR_LEAVE_LEAVEWD,
                Utilities.Utilities.GetEnumDescription(WORKDAYERROR.ERROR_LEAVE_LEAVEWD)));
            _tmp.Add(new ListERROR((int) WORKDAYERROR.ERROR_NCQD_NCDC,
                Utilities.Utilities.GetEnumDescription(WORKDAYERROR.ERROR_NCQD_NCDC)));
            _tmp.Add(new ListERROR((int) WORKDAYERROR.ERROR_WORKING_DAYS,
                Utilities.Utilities.GetEnumDescription(WORKDAYERROR.ERROR_WORKING_DAYS)));

            return _tmp;
        }*/

        public static string WriteCount(string error, string addstr)
        {
            var strReturn = string.Empty;
            if (error.Length == 0)
                strReturn = addstr;
            else
                strReturn = error + " | " + addstr;
            return strReturn;
        }

        public double CalTotalLeave(GridViewRowInfo rowInfo)
        {
            var f_OmDNBHXH = Convert.ToDouble(rowInfo.Cells["OmDNBHXH"].Value);
            var f_Om = Convert.ToDouble(rowInfo.Cells["Om"].Value);
            var f_OmDaiNgay = Convert.ToDouble(rowInfo.Cells["OmDN"].Value);
            var f_KHHDS = Convert.ToDouble(rowInfo.Cells["KHH"].Value);
            var f_Con_Om = Convert.ToDouble(rowInfo.Cells["Co"].Value);
            var f_ThaiSan = Convert.ToDouble(rowInfo.Cells["TS"].Value);
            var f_SayThai = Convert.ToDouble(rowInfo.Cells["ST"].Value);
            var f_KhamThai = Convert.ToDouble(rowInfo.Cells["Khamthai"].Value);
            var f_TNLD = Convert.ToDouble(rowInfo.Cells["TNLD"].Value);
            var f_Nam = Convert.ToDouble(rowInfo.Cells["F"].Value);
            var f_DiDuong = Convert.ToDouble(rowInfo.Cells["Diduong"].Value);
            var f_CongTac = Convert.ToDouble(rowInfo.Cells["CTac"].Value);
            var f_db = Convert.ToDouble(rowInfo.Cells["Fdb"].Value);
            var f_Hoc1 = Convert.ToDouble(rowInfo.Cells["H1"].Value);
            var f_Hoc2 = Convert.ToDouble(rowInfo.Cells["H2"].Value);
            var f_Hoc3 = Convert.ToDouble(rowInfo.Cells["H3"].Value);
            var f_Hoc4 = Convert.ToDouble(rowInfo.Cells["H4"].Value);
            var f_Hoc5 = Convert.ToDouble(rowInfo.Cells["H5"].Value);
            var f_Hoc6 = Convert.ToDouble(rowInfo.Cells["H6"].Value);
            var f_Hoc7 = Convert.ToDouble(rowInfo.Cells["H7"].Value);
            var f_DinhChiCongTac = Convert.ToDouble(rowInfo.Cells["DinhChiCT"].Value);
            var f_KoLuongCLD = Convert.ToDouble(rowInfo.Cells["Ro"].Value);
            var f_KoLuongKLD = Convert.ToDouble(rowInfo.Cells["Ko"].Value);
            var nghiBu = Convert.ToDouble(rowInfo.Cells["NghiBu"].Value);

            var TotalLeave = f_OmDNBHXH + f_Om + f_OmDaiNgay + f_KHHDS + f_Con_Om + f_ThaiSan + f_SayThai + f_KhamThai +
                             f_TNLD + f_Nam + f_DiDuong + f_CongTac + f_db + f_Hoc1 + f_Hoc2 + f_Hoc3 + f_Hoc4 + f_Hoc5 +
                             f_Hoc6 + f_Hoc7 + f_DinhChiCongTac + f_KoLuongCLD + f_KoLuongKLD + nghiBu;

            return TotalLeave;
        }
        
        private void radTreeView1_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {
            radDropDownList2.DropDownListElement.ClosePopup();
            radDropDownList2.Text = radTreeView1.SelectedNode.Text;

            Cursor.Current = Cursors.AppStarting;

            if (radTreeView1.SelectedNode.Text != "All")
            {
                var deptSelected = int.Parse(radTreeView1.SelectedNode.Value.ToString());
                var objD = new DepartmentsBLL();
                objD.GetAllChildId(deptSelected);
                var departmentIds = objD.ChildNodeIds;

                radGridView1.DataSource =
                    WorkdayCoefficientEmployeesFinalBLL.GetDataTableByDataDateForWorkingDay_ERROR(
                        _DataDate.Month, _DataDate.Year,
                        clsGlobal.SalaryIsVCQLNN, departmentIds, 9999, Constants.DataType_Run,
                        radDropDownList1.SelectedValue.ToString(), 0);
            }
            else
            {
                radGridView1.DataSource =
                    WorkdayCoefficientEmployeesFinalBLL.GetDataTableByDataDateForWorkingDay_ERROR(
                        _DataDate.Month, _DataDate.Year,
                        clsGlobal.SalaryIsVCQLNN, string.Empty, 9999, Constants.DataType_Run,
                        radDropDownList1.SelectedValue.ToString(), 0);
            }
            Utilities.Utilities.GridFormatting(radGridView1);

            Cursor.Current = Cursors.Default;
            radLabelElement1.Text = "Tổng cộng: " + Utilities.Utilities.GetDataRowCount(radGridView1.ChildRows);
        }

        private void radGridView1_GroupSummaryEvaluate(object sender, GroupSummaryEvaluationEventArgs e)
        {
            if (e.SummaryItem.Name == "UserId")
                foreach (var row in e.Group)
                {
                    var _Om = Checker.CountCompareGridRow(row, "O");
                    var _OmDN = Checker.CountCompareGridRow(row, "OD");
                    var _ThaiSan = Checker.CountCompareGridRow(row, "TS");
                    var _TNLD = Checker.CountCompareGridRow(row, "TNLD");
                    var _F = Checker.CountCompareGridRow(row, "F");
                    var _Fdb = Checker.CountCompareGridRow(row, "Fdb");
                    var _CoLyDo = Checker.CountCompareGridRow(row, "Ro");
                    var _KoLyDo = Checker.CountCompareGridRow(row, "Ko");
                    var _DiDuong = Checker.CountCompareGridRow(row, "DD");
                    var _CongTac = Checker.CountCompareGridRow(row, "CT");
                    var _H1 = Checker.CountCompareGridRow(row, "H1");
                    var _H2 = Checker.CountCompareGridRow(row, "H2");
                    var _H3 = Checker.CountCompareGridRow(row, "H3");
                    var _H4 = Checker.CountCompareGridRow(row, "H4");
                    var _H5 = Checker.CountCompareGridRow(row, "H5");
                    var _H6 = Checker.CountCompareGridRow(row, "H6");
                    var _H7 = Checker.CountCompareGridRow(row, "H7");
                    var _ConOm = Checker.CountCompareGridRow(row, "Co");
                    var _KHH = Checker.CountCompareGridRow(row, "KHH");
                    var _SayThai = Checker.CountCompareGridRow(row, "ST");
                    var _KhamThai = Checker.CountCompareGridRow(row, "KT");
                    var _ConChet = Checker.CountCompareGridRow(row, "CC");
                    var _DinhChiCT = Checker.CountCompareGridRow(row, "DC");
                    var _TamHoanHD = Checker.CountCompareGridRow(row, "NK");
                    var _HoiHop = Checker.CountCompareGridRow(row, "HH");
                    var _LeTet = Checker.CountCompareGridRow(row, "LE");
                    var _NghiViec = Checker.CountCompareGridRow(row, "NV");
                    var _ChuaDiLam = Checker.CountCompareGridRow(row, "-");
                    var _HocSags = Checker.CountCompareGridRow(row, "Ho");
                    var _OmDNBHXH = Checker.CountCompareGridRow(row, "OBH");
                    var _NuoiCon = Checker.CountCompareGridRow(row, "CN");
                    var _NghiMat = Checker.CountCompareGridRow(row, "NM");
                    var _NghiTuan = Checker.CountCompareGridRow(row, "NT");
                    var _NghiBu = Checker.CountCompareGridRow(row, "NB");

                    //int _groupUserId = Convert.ToInt32(row.Cells["UserId"].Value);
                    string _strDicrect =
                        Convert.ToInt32(row.Cells["DirectWorking"].Value) == 1
                            ? " (Trực tiếp)"
                            : "(Gián tiếp)";
                    var _Count = string.Empty;
                    _Count = WriteCount(_Count, $"NT: {_NghiTuan}");
                    _Count = WriteCount(_Count, $"NB: {_NghiBu}");

                    if (_Om > 0)
                        _Count = WriteCount(_Count, $"O: {_Om}");
                    if (_OmDN > 0)
                        _Count = WriteCount(_Count, $"OD: {_OmDN}");
                    if (_ThaiSan > 0)
                        _Count = WriteCount(_Count, $"TS: {_ThaiSan}");
                    if (_TNLD > 0)
                        _Count = WriteCount(_Count, $"TNLD: {_TNLD}");
                    if (_F > 0)
                        _Count = WriteCount(_Count, $"F: {_F}");
                    if (_Fdb > 0)
                        _Count = WriteCount(_Count, $"Fdb: {_Fdb}");
                    if (_CoLyDo > 0)
                        _Count = WriteCount(_Count, $"Ro: {_CoLyDo}");
                    if (_KoLyDo > 0)
                        _Count = WriteCount(_Count, $"Ko: {_KoLyDo}");
                    if (_DiDuong > 0)
                        _Count = WriteCount(_Count, $"DD: {_DiDuong}");
                    if (_CongTac > 0)
                        _Count = WriteCount(_Count, $"CT: {_CongTac}");
                    if (_H1 > 0)
                        _Count = WriteCount(_Count, $"H1: {_H1}");
                    if (_H2 > 0)
                        _Count = WriteCount(_Count, $"H2: {_H2}");
                    if (_H3 > 0)
                        _Count = WriteCount(_Count, $"H3: {_H3}");
                    if (_H4 > 0)
                        _Count = WriteCount(_Count, $"H4: {_H4}");
                    if (_H5 > 0)
                        _Count = WriteCount(_Count, $"H5: {_H5}");
                    if (_H6 > 0)
                        _Count = WriteCount(_Count, $"H6: {_H6}");
                    if (_H7 > 0)
                        _Count = WriteCount(_Count, $"H7: {_H7}");
                    if (_ConOm > 0)
                        _Count = WriteCount(_Count, $"Co: {_ConOm}");
                    if (_KHH > 0)
                        _Count = WriteCount(_Count, $"KHH: {_KHH}");
                    if (_SayThai > 0)
                        _Count = WriteCount(_Count, $"ST: {_SayThai}");
                    if (_KhamThai > 0)
                        _Count = WriteCount(_Count, $"KT: {_KhamThai}");
                    if (_ConChet > 0)
                        _Count = WriteCount(_Count, $"CC: {_ConChet}");
                    if (_DinhChiCT > 0)
                        _Count = WriteCount(_Count, $"DC: {_DinhChiCT}");
                    if (_TamHoanHD > 0)
                        _Count = WriteCount(_Count, $"NK: {_TamHoanHD}");
                    if (_HoiHop > 0)
                        _Count = WriteCount(_Count, $"HH: {_HoiHop}");
                    if (_LeTet > 0)
                        _Count = WriteCount(_Count, $"Le: {_LeTet}");
                    if (_NghiViec > 0)
                        _Count = WriteCount(_Count, $"NV: {_NghiViec}");
                    if (_ChuaDiLam > 0)
                        _Count = WriteCount(_Count, $"-: {_ChuaDiLam}");
                    if (_HocSags > 0)
                        _Count = WriteCount(_Count, $"Ho: {_HocSags}");
                    if (_OmDNBHXH > 0)
                        _Count = WriteCount(_Count, $"OBH: {_OmDNBHXH}");
                    if (_NuoiCon > 0)
                        _Count = WriteCount(_Count, $"CN: {_NuoiCon}");
                    if (_NghiMat > 0)
                        _Count = WriteCount(_Count, $"NM: {_NghiMat}");
                    if (_Count.Length > 0)
                        e.FormatString = $"{e.Value}. {row.Cells["FullName"].Value} {_strDicrect}: {_Count}";
                    else
                        e.FormatString = $"{e.Value}. {row.Cells["FullName"].Value} {_strDicrect}";
                }
        }

        private void radGridView1_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is GridHeaderCellElement)
            {
                if (e.CellElement.ColumnInfo.Name == "colTOTAL")
                    e.CellElement.ColumnInfo.HeaderText = "Tổng\r\nPhép";
                if (e.CellElement.ColumnInfo.Name == "Lamdem")
                    e.CellElement.ColumnInfo.HeaderText = "Lam\r\ndem";
                if (e.CellElement.ColumnInfo.Name == "Khamthai")
                    e.CellElement.ColumnInfo.HeaderText = "Kham\r\nthai";
                if (e.CellElement.ColumnInfo.Name == "OmDNBHXH")
                    e.CellElement.ColumnInfo.HeaderText = "OmDN\r\nBHXH";
                if (e.CellElement.ColumnInfo.Name == "Diduong")
                    e.CellElement.ColumnInfo.HeaderText = "Di\r\nduong";
                if (e.CellElement.ColumnInfo.Name == "DinhChiCT")
                    e.CellElement.ColumnInfo.HeaderText = "Dinh\r\nChi\r\nCT";
                if (e.CellElement.ColumnInfo.Name == "NCQD")
                    e.CellElement.ColumnInfo.HeaderText = "NC\r\nQD";
                if (e.CellElement.ColumnInfo.Name == "NCDC")
                    e.CellElement.ColumnInfo.HeaderText = "NC\r\nDC\r\n(X+T)";
                if (e.CellElement.ColumnInfo is GridViewDataColumn)
                    if ((((GridViewDataColumn) e.CellElement.ColumnInfo).Name == "colTOTAL") ||
                        (((GridViewDataColumn) e.CellElement.ColumnInfo).Name == "X") ||
                        (((GridViewDataColumn) e.CellElement.ColumnInfo).Name == "NCDC") ||
                        (((GridViewDataColumn) e.CellElement.ColumnInfo).Name == "NCQD") ||
                        (((GridViewDataColumn) e.CellElement.ColumnInfo).Name == "NghiTuan") ||
                        (((GridViewDataColumn) e.CellElement.ColumnInfo).Name == "NghiBu"))
                    {
                        e.CellElement.DrawFill = true;
                        e.CellElement.NumberOfColors = 1;
                        e.CellElement.BackColor = Color.FromArgb(233, 245, 220);
                    }
                    else
                    {
                        if ((e.CellElement.ColumnInfo.Name == "Day1") || (e.CellElement.ColumnInfo.Name == "Day2") ||
                            (e.CellElement.ColumnInfo.Name == "Day3") || (e.CellElement.ColumnInfo.Name == "Day4") ||
                            (e.CellElement.ColumnInfo.Name == "Day5") || (e.CellElement.ColumnInfo.Name == "Day14") ||
                            (e.CellElement.ColumnInfo.Name == "Day6") || (e.CellElement.ColumnInfo.Name == "Day15") ||
                            (e.CellElement.ColumnInfo.Name == "Day7") || (e.CellElement.ColumnInfo.Name == "Day16") ||
                            (e.CellElement.ColumnInfo.Name == "Day8") || (e.CellElement.ColumnInfo.Name == "Day17") ||
                            (e.CellElement.ColumnInfo.Name == "Day9") || (e.CellElement.ColumnInfo.Name == "Day18") ||
                            (e.CellElement.ColumnInfo.Name == "Day10") || (e.CellElement.ColumnInfo.Name == "Day19") ||
                            (e.CellElement.ColumnInfo.Name == "Day11") || (e.CellElement.ColumnInfo.Name == "Day20") ||
                            (e.CellElement.ColumnInfo.Name == "Day12") || (e.CellElement.ColumnInfo.Name == "Day21") ||
                            (e.CellElement.ColumnInfo.Name == "Day13") || (e.CellElement.ColumnInfo.Name == "Day22") ||
                            (e.CellElement.ColumnInfo.Name == "Day23") || (e.CellElement.ColumnInfo.Name == "Day28") ||
                            (e.CellElement.ColumnInfo.Name == "Day24") || (e.CellElement.ColumnInfo.Name == "Day29") ||
                            (e.CellElement.ColumnInfo.Name == "Day25") || (e.CellElement.ColumnInfo.Name == "Day30") ||
                            (e.CellElement.ColumnInfo.Name == "Day26") || (e.CellElement.ColumnInfo.Name == "Day31") ||
                            (e.CellElement.ColumnInfo.Name == "Day27"))
                        {
                            e.CellElement.Font = _defaultFontBold;
                            e.CellElement.DrawFill = true;
                            e.CellElement.NumberOfColors = 1;
                            e.CellElement.BackColor = Color.FromArgb(236, 220, 245);
                        }
                        else
                        {
                            e.CellElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                            e.CellElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Local);
                            e.CellElement.ResetValue(LightVisualElement.NumberOfColorsProperty, ValueResetFlags.Local);
                            e.CellElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
                        }
                    }
            }
        }

        private void radGridView1_EditorRequired(object sender, EditorRequiredEventArgs e)
        {
            if ((radGridView1.CurrentColumn.Name == "Day1") || (radGridView1.CurrentColumn.Name == "Day2") ||
                (radGridView1.CurrentColumn.Name == "Day3") || (radGridView1.CurrentColumn.Name == "Day4") ||
                (radGridView1.CurrentColumn.Name == "Day5") || (radGridView1.CurrentColumn.Name == "Day14") ||
                (radGridView1.CurrentColumn.Name == "Day6") || (radGridView1.CurrentColumn.Name == "Day15") ||
                (radGridView1.CurrentColumn.Name == "Day7") || (radGridView1.CurrentColumn.Name == "Day16") ||
                (radGridView1.CurrentColumn.Name == "Day8") || (radGridView1.CurrentColumn.Name == "Day17") ||
                (radGridView1.CurrentColumn.Name == "Day9") || (radGridView1.CurrentColumn.Name == "Day18") ||
                (radGridView1.CurrentColumn.Name == "Day10") || (radGridView1.CurrentColumn.Name == "Day19") ||
                (radGridView1.CurrentColumn.Name == "Day11") || (radGridView1.CurrentColumn.Name == "Day20") ||
                (radGridView1.CurrentColumn.Name == "Day12") || (radGridView1.CurrentColumn.Name == "Day21") ||
                (radGridView1.CurrentColumn.Name == "Day13") || (radGridView1.CurrentColumn.Name == "Day22") ||
                (radGridView1.CurrentColumn.Name == "Day23") || (radGridView1.CurrentColumn.Name == "Day28") ||
                (radGridView1.CurrentColumn.Name == "Day24") || (radGridView1.CurrentColumn.Name == "Day29") ||
                (radGridView1.CurrentColumn.Name == "Day25") || (radGridView1.CurrentColumn.Name == "Day30") ||
                (radGridView1.CurrentColumn.Name == "Day26") || (radGridView1.CurrentColumn.Name == "Day31") ||
                (radGridView1.CurrentColumn.Name == "Day27"))
                e.Editor = new MyAutoCompleteEditor();
        }

        private void radGridView1_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.ActiveEditor is MyAutoCompleteEditor)
            {
                var editor = (MyAutoCompleteEditor) e.ActiveEditor;
                var element = (RadAutoCompleteBoxElement) editor.EditorElement;
                element.Delimiter = ' ';
                element.AutoCompleteDataSource = Constants.GetAllLeaveCode();
                element.AutoCompleteDisplayMember = "rTypeName";
                element.AutoCompleteValueMember = "rTypeName";
            }
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

        private void radButton1_Click(object sender, EventArgs e)
        {
            radButton1.Enabled = false;
            radButton2.Enabled = false;
            _worker.RunWorkerAsync();
            Task.Factory.StartNew(() => { _wf1.ShowDialog(); });
            Thread.Sleep(1000);
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is string)
            {
                _wf1.Invoke(new MethodInvoker(() => { _wf1.SetDescription((string) e.UserState); }));
                if (e.ProgressPercentage == 24061991)
                {
                    radGridView1.DataSource = _TempTable;
                    ReCalculate();
                }
            }
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (_worker.CancellationPending)
                e.Cancel = true;
            _worker.ReportProgress(-1, "Analyzing...");
            Thread.Sleep(500);
            if ((_TempTable == null) || (_TempTable.Columns.Count <= 0) || (_TempTable.Rows.Count <= 0))
            {
                InitTempTableStructure();
                AddTempTableRows();
            }
            FixNTonDT(_TempTable);
            Thread.Sleep(500);
            _worker.ReportProgress(24061991, "Finalizing and formatting...");
        }

        private void SetDataSource(object value)
        {
            radGridView1.DataSource = value;
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                _wf1.Invoke(new MethodInvoker(() =>
                {
                    _wf1.SetDescription("Operation cancelled by the user!");
                    Thread.Sleep(2000);
                }));
            }
            else
            {
                if (e.Error != null)
                    _wf1.Invoke(new MethodInvoker(() =>
                    {
                        _wf1.SetDescription("Operation failed: " + e.Error.Message);
                        Thread.Sleep(2000);
                    }));
                else
                    try
                    {
                        _wf1.Invoke(new MethodInvoker(() => { _wf1.SetDescription("Completed!"); }));

                        _wf1.Invoke(new MethodInvoker(() => { _wf1.Dispose(); }));
                    }
                    finally
                    {
                        radGridView1.MasterTemplate.SelectLastAddedRow = false;
                        radGridView1.CurrentRow = radGridView1.CurrentView.ViewInfo.Rows.FirstOrDefault();

                        radButton1.Enabled = true;
                        radButton2.Enabled = true;
                    }
            }
        }

        private void _bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                _wf1.Invoke(new MethodInvoker(() =>
                {
                    _wf1.SetDescription("Operation cancelled by the user!");
                    Thread.Sleep(2000);
                }));
            }
            else
            {
                if (e.Error != null)
                    _wf1.Invoke(new MethodInvoker(() =>
                    {
                        _wf1.SetDescription("Operation failed: " + e.Error.Message);
                        Thread.Sleep(2000);
                    }));
                else
                    try
                    {
                        _wf1.Invoke(new MethodInvoker(() => { _wf1.Dispose(); }));
                        MessageBox.Show("Done!");

                        radGridView1.DataSource =
                            WorkdayCoefficientEmployeesFinalBLL.GetDataTableByDataDateForWorkingDay_ERROR(
                                _DataDate.Month, _DataDate.Year,
                                clsGlobal.SalaryIsVCQLNN, _DeptIds, 3, Constants.DataType_Run,
                                radDropDownList1.SelectedValue.ToString(), 0);
                        Utilities.Utilities.GridFormatting(radGridView1);

                        //radGridView2.DataSource =
                        //    WorkdayCoefficientEmployeesFinalBLL.GetDataTableByDataDateForWorkingDay_ERROR(
                        //        _DataDate.Month, _DataDate.Year,
                        //        clsGlobal.SalaryIsVCQLNN, _DeptIds, 3, Constants.DataType_Run,
                        //        radDropDownList1.SelectedValue.ToString(), 0);
                        //Utilities.Utilities.GridFormatting(radGridView2);

                        HideCol();
                        
                        radLabelElement1.Text = "Tổng cộng: " +
                                                Utilities.Utilities.GetDataRowCount(radGridView1.ChildRows);
                    }
                    finally
                    {
                    }
            }
        }

        private void _bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is string)
                _wf1.Invoke(new MethodInvoker(() => { _wf1.SetDescription((string) e.UserState); }));
        }

        private void _bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            if (bWorker1.CancellationPending)
                e.Cancel = true;
            Save();
        }

        private void ReCalculate()
        {
            try
            {
                if (radGridView1.Rows.Count > 0)
                    radGridView1.CurrentRow = radGridView1.Rows[0];
                if (radGridView2.Rows.Count > 0)
                    radGridView2.CurrentRow = radGridView2.Rows[0];
                var dtOrigin = WorkdayCoefficientEmployeesFinalBLL.GetDataTableByDataDateForWorkingDay_ERROR(
                    _DataDate.Month, _DataDate.Year,
                    clsGlobal.SalaryIsVCQLNN, _DeptIds, 9999, Constants.DataType_Run,
                    radDropDownList1.SelectedValue.ToString(), 0);

                foreach (var VARIABLE in radGridView1.Rows)
                {
                    var rowToCompare = VARIABLE;
                    var drOrigin = dtOrigin.Select($"UserId = {rowToCompare.Cells["UserId"].Value}");

                    for (var i = 1; i <= 31; i++)
                        if (!rowToCompare.Cells["Day" + i].Value.Equals(drOrigin[0]["Day" + i]))
                            rowToCompare.Cells["Day" + i].Style.ForeColor = Color.Red;

                    var _TotalLeave = CalTotalLeave(rowToCompare);
                    rowToCompare.Cells["colTOTAL"].Value = _TotalLeave;
                    rowToCompare.Cells["colTOTAL"].Style.ForeColor = Color.Red;

                    var _NghiTuan = Checker.CountCompareGridRow(rowToCompare, "NT");
                    rowToCompare.Cells["NghiTuan"].Value = _NghiTuan;
                    rowToCompare.Cells["NghiTuan"].Style.ForeColor = Color.Red;

                    var _NghiBu = Checker.CountCompareGridRow(rowToCompare, "NB");
                    rowToCompare.Cells["NghiBu"].Value = _NghiBu;
                    rowToCompare.Cells["NghiBu"].Style.ForeColor = Color.Red;

                    var _X = Checker.CountCompareGridRow(rowToCompare, "X");
                    rowToCompare.Cells["X"].Value = _X;
                    rowToCompare.Cells["X"].Style.ForeColor = Color.Red;

                    var _NCDC = _X + _NghiBu + _TotalLeave;
                    rowToCompare.Cells["NCDC"].Value = _NCDC;
                    rowToCompare.Cells["NCDC"].Style.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
        }

        private delegate void SetDataSourceDelegate(object value);

        private void radGridView1_CommandCellClick(object sender, GridViewCellEventArgs e)
        {
            var UserId = Convert.ToInt32(e.Row.Cells["UserId"].Value);
            
            #region Days
            var Day1 = e.Row.Cells["Day1"].Value.ToString();
            var Day2 = e.Row.Cells["Day2"].Value.ToString();
            var Day3 = e.Row.Cells["Day3"].Value.ToString();
            var Day4 = e.Row.Cells["Day4"].Value.ToString();
            var Day5 = e.Row.Cells["Day5"].Value.ToString();
            var Day6 = e.Row.Cells["Day6"].Value.ToString();
            var Day7 = e.Row.Cells["Day7"].Value.ToString();
            var Day8 = e.Row.Cells["Day8"].Value.ToString();
            var Day9 = e.Row.Cells["Day9"].Value.ToString();
            var Day10 = e.Row.Cells["Day10"].Value.ToString();
            var Day11 = e.Row.Cells["Day11"].Value.ToString();
            var Day12 = e.Row.Cells["Day12"].Value.ToString();
            var Day13 = e.Row.Cells["Day13"].Value.ToString();
            var Day14 = e.Row.Cells["Day14"].Value.ToString();
            var Day15 = e.Row.Cells["Day15"].Value.ToString();
            var Day16 = e.Row.Cells["Day16"].Value.ToString();
            var Day17 = e.Row.Cells["Day17"].Value.ToString();
            var Day18 = e.Row.Cells["Day18"].Value.ToString();
            var Day19 = e.Row.Cells["Day19"].Value.ToString();
            var Day20 = e.Row.Cells["Day20"].Value.ToString();
            var Day21 = e.Row.Cells["Day21"].Value.ToString();
            var Day22 = e.Row.Cells["Day22"].Value.ToString();
            var Day23 = e.Row.Cells["Day23"].Value.ToString();
            var Day24 = e.Row.Cells["Day24"].Value.ToString();
            var Day25 = e.Row.Cells["Day25"].Value.ToString();
            var Day26 = e.Row.Cells["Day26"].Value.ToString();
            var Day27 = e.Row.Cells["Day27"].Value.ToString();
            var Day28 = e.Row.Cells["Day28"].Value.ToString();
            var Day29 = string.Empty;
            var Day30 = string.Empty;
            var Day31 = string.Empty;
            try
            {
                Day29 = e.Row.Cells["Day29"].Value.ToString();
            }
            catch
            {
                Day29 = string.Empty;
            }
            try
            {
                Day30 = e.Row.Cells["Day30"].Value.ToString();
            }
            catch
            {
                Day30 = string.Empty;
            }
            try
            {
                Day31 = e.Row.Cells["Day31"].Value.ToString();
            }
            catch
            {
                Day31 = string.Empty;
            }

            var f_Om = Constants.WorkdayEmployee_DefaultValue;
            var f_OmDaiNgay = Constants.WorkdayEmployee_DefaultValue;
            var f_ThaiSan = Constants.WorkdayEmployee_DefaultValue;
            var f_TNLD = Constants.WorkdayEmployee_DefaultValue;
            var f_Nam = Constants.WorkdayEmployee_DefaultValue;
            var f_db = Constants.WorkdayEmployee_DefaultValue;
            var f_KoLuongCLD = Constants.WorkdayEmployee_DefaultValue;
            var f_KoLuongKLD = Constants.WorkdayEmployee_DefaultValue;
            var f_DiDuong = Constants.WorkdayEmployee_DefaultValue;
            var f_CongTac = Constants.WorkdayEmployee_DefaultValue;

            var f_HocSAGS = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc1 = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc2 = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc3 = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc4 = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc5 = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc6 = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc7 = Constants.WorkdayEmployee_DefaultValue;

            var f_Con_Om = Constants.WorkdayEmployee_DefaultValue;
            var f_KHHDS = Constants.WorkdayEmployee_DefaultValue;
            var f_SayThai = Constants.WorkdayEmployee_DefaultValue;
            var f_KhamThai = Constants.WorkdayEmployee_DefaultValue;
            var f_ConChet = Constants.WorkdayEmployee_DefaultValue;
            var f_DinhChiCongTac = Constants.WorkdayEmployee_DefaultValue;
            var f_TamHoanHD = Constants.WorkdayEmployee_DefaultValue;
            var f_HoiHop = Constants.WorkdayEmployee_DefaultValue;
            var f_Le = Constants.WorkdayEmployee_DefaultValue;
            var nghiTuan = Constants.WorkdayEmployee_DefaultValue;
            var nghiBu = Constants.WorkdayEmployee_DefaultValue;
            var nghiMat = Constants.WorkdayEmployee_DefaultValue;
            var nghiViec = Constants.WorkdayEmployee_DefaultValue;
            var chuaDiLam = Constants.WorkdayEmployee_DefaultValue;
            var f_OmDNBHXH = Constants.WorkdayEmployee_DefaultValue;

            f_OmDNBHXH = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_O_DAI_NGAY_HUONG_BHXH);

            f_Om = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_O_BAN_THAN);

            f_OmDaiNgay = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_O_DAI_NGAY);

            f_KHHDS = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_KHHDS);

            f_Con_Om = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_CON_OM);

            f_ThaiSan = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_THAI_SAN);

            f_SayThai = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_SAY_THAI);

            f_KhamThai = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_KHAM_THAI);

            f_TNLD = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_TNLD);

            f_Nam = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_F_NAM);

            f_DiDuong = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_F_DI_DUONG);

            f_CongTac = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_F_CONG_TAC);

            f_db = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_FDB);

            f_Hoc1 = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_1);

            f_Hoc2 = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_2);

            f_Hoc3 = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_3);

            f_Hoc4 = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_4);

            f_Hoc5 = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_5);

            f_Hoc6 = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_6);

            f_Hoc7 = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_7);

            f_DinhChiCongTac = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2,
                Day3,
                Day4, Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_DINH_CHI_CONG_TAC);

            f_KoLuongCLD = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO);

            f_KoLuongKLD = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO);


            f_HocSAGS = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_SAGS);

            f_ConChet = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_CON_CHET_SAU_KHI_SINH);

            f_TamHoanHD = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_TAM_HOAN_HOP_DONG);
            f_HoiHop = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOI_HOP);
            f_Le = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_LE_TET);

            nghiTuan = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_TUAN);

            nghiBu = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_BU);

            nghiMat = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_MAT);

            nghiViec = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_VIEC);

            chuaDiLam = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3,
                Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_CHUA_DI_LAM);

            var X = DefaultValues.CalculateLeaveDay(_DataDate.Month, _DataDate.Year, UserId, Day1, Day2, Day3, Day4,
                Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_X);
            #endregion

            var HSK = Convert.ToDouble(e.Row.Cells["HSK"].Value);
            var Remark = e.Row.Cells["Remark"].Value.ToString();

            var LamDem = double.Parse(e.Row.Cells["Lamdem"].Value.ToString());

            var UpdateUserId = clsGlobal.UserId;
            var UpdatedDate = DateTime.Now;

            var TotalLeave = f_OmDNBHXH + f_Om + f_OmDaiNgay + f_KHHDS + f_Con_Om + f_ThaiSan + f_SayThai +
                             f_KhamThai +
                             f_TNLD + f_Nam + f_DiDuong + f_CongTac + f_db + f_Hoc1 + f_Hoc2 + f_Hoc3 + f_Hoc4 +
                             f_Hoc5 +
                             f_Hoc6 + f_Hoc7 + f_DinhChiCongTac + f_KoLuongCLD + f_KoLuongKLD + nghiBu;
            var DRWD =
                WorkdayCoefficientEmployeesFinalBLL.GetByUserIdDataDate(UserId,
                    _DataDate, Constants.DataType_Run);

            var NCDC = X + TotalLeave;

            _SP = $"Upd_H1_WorkdayCoefficientEmployeesFinal_By_UserId_Date_For_EdittingWorkday";
            _SPValue =
                $"UserId: {UserId}, DataDate: '{_DataDate}', Day1: '{Day1}', Day2: '{Day2}', Day3: '{Day3}', Day4: '{Day4}', Day5: '{Day5}', Day6: '{Day6}', Day7: '{Day7}', Day8: '{Day8}', Day9: '{Day9}', Day10: '{Day10}', Day11: '{Day11}', Day12: '{Day12}', Day13: '{Day13}', Day14: '{Day14}', Day15: '{Day15}', Day16: '{Day16}', Day17: '{Day17}', Day18: '{Day18}', Day19: '{Day19}', Day20: '{Day20}', Day21: '{Day21}', Day22: '{Day22}', Day23: '{Day23}', Day24: '{Day24}', Day25: '{Day25}', Day26: '{Day26}', Day27: '{Day27}', Day28: '{Day28}', Day29: '{Day29}', Day30: '{Day30}', Day31: '{Day31}', LamDem: {LamDem}, UpdatedDate: '{UpdatedDate}', UpdateUserId: {UpdateUserId}, f_OmDNBHXH: {f_OmDNBHXH}, f_Om: {f_Om}, f_OmDN: {f_OmDaiNgay}, f_KHHDS: {f_KHHDS}, f_ConOm: {f_Con_Om}, f_TS: {f_ThaiSan}, f_ST: {f_SayThai}, f_Khamthai: {f_KhamThai}, f_TNLD: {f_TNLD}, f_Nam: {f_Nam}, f_Diduong: {f_DiDuong}, f_CongTac: {f_CongTac}, f_db: {f_db}, f_Hoc1: {f_Hoc1}, f_Hoc2: {f_Hoc2}, f_Hoc3: {f_Hoc3}, f_Hoc4: {f_Hoc4}, f_Hoc5: {f_Hoc5}, f_Hoc6: {f_Hoc6}, f_Hoc7: {f_Hoc7}, f_DinhChiCT: {f_DinhChiCongTac}, f_KoLuongCLD: {f_KoLuongCLD}, f_KoLuongKLD: {f_KoLuongKLD}, X: {X}, NT: {nghiTuan}, NB: {nghiBu}, NghiViec: {nghiViec}, NghiMat: {nghiMat}, ChuaDiLam: {chuaDiLam}, HSK: {HSK}, Remark: N'{Remark}', RemarkHRMAdmin: N'{"" + DateTime.Now}, NCDC: {NCDC}";

            var drOld =
                WorkdayCoefficientEmployeesFinalBLL.GetByUserIdDataDateToDR(
                    UserId, _DataDate, 1);
            _OldContent =
                $"UserId: {drOld["UserId"]}, DataDate: '{drOld["DataDate"]}', Day1: '{drOld["Day1"]}', Day2: '{drOld["Day2"]}', Day3: '{drOld["Day3"]}', Day4: '{drOld["Day4"]}', Day5: '{drOld["Day5"]}', Day6: '{drOld["Day6"]}', Day7: '{drOld["Day7"]}', Day8: '{drOld["Day8"]}', Day9: '{drOld["Day9"]}', Day10: '{drOld["Day10"]}', Day11: '{drOld["Day11"]}', Day12: '{drOld["Day12"]}', Day13: '{drOld["Day13"]}', Day14: '{drOld["Day14"]}', Day15: '{drOld["Day15"]}', Day16: '{drOld["Day16"]}', Day17: '{drOld["Day17"]}', Day18: '{drOld["Day18"]}', Day19: '{drOld["Day19"]}', Day20: '{drOld["Day20"]}', Day21: '{drOld["Day21"]}', Day22: '{drOld["Day22"]}', Day23: '{drOld["Day23"]}', Day24: '{drOld["Day24"]}', Day25: '{drOld["Day25"]}', Day26: '{drOld["Day26"]}', Day27: '{drOld["Day27"]}', Day28: '{drOld["Day28"]}', Day29: '{drOld["Day29"]}', Day30: '{drOld["Day30"]}', Day31: '{drOld["Day31"]}', Lamdem: {drOld["Lamdem"]}, UpdateDate: '{drOld["UpdateDate"]}', UpdateUserId: {drOld["UpdateUserId"]}, OmDNBHXH: {drOld["OmDNBHXH"]}, Om: {drOld["Om"]}, OmDN: {drOld["OmDN"]}, KHH: {drOld["KHH"]}, Co: {drOld["Co"]}, TS: {drOld["TS"]}, ST: {drOld["ST"]}, Khamthai: {drOld["Khamthai"]}, TNLD: {drOld["TNLD"]}, F: {drOld["F"]}, Diduong: {drOld["Diduong"]}, CTac: {drOld["CTac"]}, Fdb: {drOld["Fdb"]}, H1: {drOld["H1"]}, H2: {drOld["H2"]}, H3: {drOld["H3"]}, H4: {drOld["H4"]}, H5: {drOld["H5"]}, H6: {drOld["H6"]}, H7: {drOld["H7"]}, DinhChiCT: {drOld["DinhChiCT"]}, Ro: {drOld["Ro"]}, Ko: {drOld["Ko"]}, X: {drOld["X"]}, NT: {drOld["NghiTuan"]}, NB: {drOld["NghiBu"]}, NghiViec: {drOld["NghiViec"]}, NghiMat: {drOld["NghiMat"]}, ChuaDiLam: {drOld["ChuaDiLam"]}, HSK: {drOld["HSK"]}, Remark: N'{drOld["Remark"]}', RemarkHRMAdmin: N'{drOld["RemarkHRMAdmin"]}', NCDC: {drOld["NCDC"]}";

            Utilities.Utilities.SaveHRMLog("H1_WorkdayCoefficientEmployeesFinal", _SP, _SPValue, _OldContent);
            WorkdayCoefficientEmployeesFinalBLL.UpdateWorkingDayFinal(UserId,
                _DataDate, Day1, Day2, Day3, Day4, Day5, Day6, Day7, Day8, Day9, Day10,
                Day11, Day12, Day13, Day14, Day15, Day16, Day17, Day18, Day19,
                Day20, Day21, Day22, Day23, Day24, Day25, Day26, Day27, Day28, Day29,
                Day30, Day31,
                LamDem, UpdatedDate, UpdateUserId, f_OmDNBHXH, f_Om, f_OmDaiNgay, f_KHHDS, f_Con_Om, f_ThaiSan,
                f_SayThai, f_KhamThai, f_TNLD,
                f_Nam, f_DiDuong, f_CongTac, f_db, f_Hoc1, f_Hoc2, f_Hoc3, f_Hoc4, f_Hoc5, f_Hoc6, f_Hoc7,
                f_DinhChiCongTac, f_KoLuongCLD,
                f_KoLuongKLD, X, nghiTuan, nghiBu, nghiViec, nghiMat, chuaDiLam, HSK, Remark,
                "Fixed by HRM on " + DateTime.Now, NCDC);

            var WorkdayCoefficientEmployeeIdFinal =
                Convert.ToInt32(drOld["WorkdayCoefficientEmployeeIdFinal"]);
            _SP = $"Upd_H1_WorkdayCoefficientEmployeesFinal_WDStatus";
            _SPValue =
                $"UserId: {UserId}, DataDate: '{_DataDate}', WDStatus: {2}, CheckRemark: {string.Empty}, WorkdayCoefficientEmployeeIdFinal: {WorkdayCoefficientEmployeeIdFinal}";
            Utilities.Utilities.SaveHRMLog("H1_WorkdayCoefficientEmployeesFinal", _SP, _SPValue, string.Empty);
            WorkdayCoefficientEmployeesFinalBLL.UpdateWDStatus(UserId, _DataDate, 3,
                string.Empty, WorkdayCoefficientEmployeeIdFinal);

            LeaveDay_WorkingDay_Checker_One(UserId, _DataDate.Month, _DataDate.Year, "");
            RefreshData();
        }

        public void RefreshData()
        {
            BS_LeaveCode.DataSource = Constants.GetAllLeaveCode();

            if (radDropDownList1.Items.Count <= 0)
            {
                radDropDownList1.DataSource = new WE().GetAllError();
                radDropDownList1.DisplayMember = "WEName";
                radDropDownList1.ValueMember = "WEId";
                radDropDownList1.SelectedValue = 0;
            }

            DataTable dt = WorkdayCoefficientEmployeesFinalBLL.GetDataTableByDataDateForWorkingDay_ERROR(
                _DataDate.Month, _DataDate.Year,
                clsGlobal.SalaryIsVCQLNN, _DeptIds, 9999, Constants.DataType_Run,
                radDropDownList1.SelectedValue.ToString(), 0);
            radGridView1.DataSource = dt;
            if (radGridView1.Rows.Count > 0) radGridView1.Rows[0].IsCurrent = true;

            radGridView2.DataSource = WorkdayCoefficientEmployeesFinalBLL.GetDataTableByDataDateForWorkingDay_ERROR(
                _DataDate.Month, _DataDate.Year,
                clsGlobal.SalaryIsVCQLNN, _DeptIds, 9999, Constants.DataType_Run,
                radDropDownList1.SelectedValue.ToString(), Convert.ToInt32(radGridView1.CurrentRow.Cells["UserId"].Value));

            var LeaveDT = EmployeeLeaveBLL.GetDTByUserId_Date(Convert.ToInt32(radGridView1.CurrentRow.Cells["UserId"].Value), _DataDate.Month, _DataDate.Year);
            if (LeaveDT.Rows.Count > 0)
                radGridView4.DataSource = LeaveDT;
            else
                radGridView4.DataSource = null;

            Utilities.Utilities.GridFormatting(radGridView1);
            Utilities.Utilities.GridFormatting(radGridView2);
            Utilities.Utilities.GridFormatting(radGridView4);

            HideCol();
            radLabelElement1.Text = "Tổng cộng: " + Utilities.Utilities.GetDataRowCount(radGridView1.ChildRows);
        }

        public void LeaveDay_WorkingDay_Checker_One(int UserId, int Month, int Year, string DeptIds)
        {
            var DataDate = new DateTime(Year, Month, 1);

            var DRWD = WorkdayCoefficientEmployeesFinalBLL.GetDRByUserIdDataDate(UserId, DataDate, Constants.DataType_Run,
                DeptIds);
            var _ERROR = string.Empty;
            var _WhatToSave = string.Empty;

            {
                if (DRWD != null)
                {
                    try
                    {
                        var Status = DRWD["WDStatus"] == DBNull.Value
                            ? 9999
                            : int.Parse(DRWD["WDStatus"].ToString());

                        if ((Status == 2))// || (Status == 3))
                        {
                        }
                        else
                        {
                            {
                                var DirectWorking = Convert.ToInt32(EmployeesBLL.DR_GetEmployeeById(UserId)["DirectWorking"]);
                                var NTInMonth = Checker.getWeekend(Month, Year, DirectWorking).Count;
                                var LeaveRow = EmployeeLeaveBLL.GetDTByUserId_Date(UserId, Month, Year);

                                var NTTotal = 0;
                                try
                                {
                                    NTTotal = Convert.ToInt32(DRWD["NghiTuan"]);
                                }
                                catch { }

                                var NTCount = 0;
                                try
                                {
                                    NTCount = Checker.CountCompare(DRWD, "NT");
                                }
                                catch { }

                                if ((NTTotal == NTCount) && (NTTotal == NTInMonth) && (NTCount == NTInMonth))
                                {
                                    _ERROR = Checker.WriteERROR(_ERROR, -1);
                                    _WhatToSave = Checker.WriteStatus(_WhatToSave, "1");
                                }
                                else
                                {
                                    _ERROR = Checker.WriteERROR(_ERROR, new WE().GetErrorIdByName(Constants.ERROR_NGHITUAN_NAME));
                                    _WhatToSave = Checker.WriteStatus(_WhatToSave, "0");
                                }

                                var DaysInMonth = DateTime.DaysInMonth(Year, Month);
                                var _flag = true;
                                for (var ii = 1; ii <= DaysInMonth; ii++)
                                    if ((DRWD["Day" + ii] == DBNull.Value) ||
                                        DRWD["Day" + ii].ToString().Contains("--"))
                                        _flag = false;
                                if (_flag == false)
                                {
                                    _ERROR = Checker.WriteERROR(_ERROR, new WE().GetErrorIdByName(Constants.ERROR_WORKING_DAYS_NAME));
                                    _WhatToSave = Checker.WriteStatus(_WhatToSave, "0");
                                }

                                double EmpLeave_WD = 0;
                                try
                                {
                                    EmpLeave_WD = Checker.GetDataByDR(DRWD, "TotalLeave", Month, Year);
                                }
                                catch
                                {
                                }

                                var WorkdayCoefficientEmployeeIdFinal =
                                    Convert.ToInt32(DRWD["WorkdayCoefficientEmployeeIdFinal"]);

                                var X = Convert.ToDouble(DRWD["X"]);

                                if (Convert.ToInt32(DRWD["NCQD"]).Equals(Convert.ToInt32(DRWD["NCDC"])) == false)
                                {
                                    _ERROR = Checker.WriteERROR(_ERROR, new WE().GetErrorIdByName(Constants.ERROR_NCQD_NCDC_NAME));
                                    _WhatToSave = Checker.WriteStatus(_WhatToSave, "0");
                                }

                                if ((X + EmpLeave_WD < Convert.ToInt32(DRWD["NCDC"])) || (X + EmpLeave_WD > Convert.ToInt32(DRWD["NCDC"])))
                                {
                                    _ERROR = Checker.WriteERROR(_ERROR, new WE().GetErrorIdByName(Constants.ERROR_NCQD_NCDC_NAME));
                                    _WhatToSave = Checker.WriteStatus(_WhatToSave, "0");
                                }

                                if (LeaveRow.Rows.Count > 0)
                                {
                                    var Count = 0;
                                    var EmpLeave_Leave = 0d;

                                    try
                                    {
                                        EmpLeave_Leave = Checker.GetTotalLeaveOnLeaveDayTable(UserId, Month, Year);
                                    }
                                    catch
                                    {
                                    }

                                    if (EmpLeave_Leave - EmpLeave_WD == 0)
                                    {
                                        {
                                            foreach (DataRow LeaveDR in LeaveRow.Rows)
                                            foreach (var date in GetDateRange(DateTime.Parse(LeaveDR["FromDate"].ToString()), DateTime.Parse(LeaveDR["ToDate"].ToString())))
                                            {
                                                var LeaveCode = Constants.GetSymbolTimekeeping(int.Parse(LeaveDR["LeaveTypeId"].ToString()));
                                                if ((LeaveCode != "DD") || (LeaveCode != "CT") ||
                                                    (LeaveCode != "Ko") ||
                                                    (LeaveCode != "Ho") || (LeaveCode != "H1") ||
                                                    (LeaveCode != "H2") ||
                                                    (LeaveCode != "H3") || (LeaveCode != "H4") ||
                                                    (LeaveCode != "H5") ||
                                                    (LeaveCode != "H6") || (LeaveCode != "H7") ||
                                                    (LeaveCode != "DC"))
                                                    if (bool.Parse(EmployeesBLL.GetDataRowEmployeeById(UserId)["Direct"].ToString()))
                                                    {
                                                        if (DRWD["Day" + date.Day].ToString() == LeaveCode)
                                                            Count++;
                                                        else
                                                            Count--;
                                                    }
                                                    else
                                                    {
                                                        if ((date.DayOfWeek != DayOfWeek.Sunday) && (date.DayOfWeek != DayOfWeek.Saturday))
                                                            if (DRWD["Day" + date.Day].ToString() == LeaveCode)
                                                                Count++;
                                                            else
                                                                Count--;
                                                    }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        _ERROR = Checker.WriteERROR(_ERROR, new WE().GetErrorIdByName(Constants.ERROR_LEAVE_LEAVEWD_NAME));
                                        _WhatToSave = Checker.WriteStatus(_WhatToSave, "0");
                                    }
                                    if (Count == EmpLeave_Leave)
                                    {
                                        _ERROR = Checker.WriteERROR(_ERROR, -1);
                                        _WhatToSave = Checker.WriteStatus(_WhatToSave, "1");
                                    }
                                }
                                else
                                {
                                    var EmpLeave_Leave = Checker.GetTotalLeaveOnLeaveDayTable(UserId, Month, Year);
                                    var NCDC = DRWD["NCDC"] == DBNull.Value ? 0 : Convert.ToDouble(DRWD["NCDC"]);

                                    if ((EmpLeave_WD > 0) || (X + EmpLeave_WD != NCDC))
                                    {
                                        _ERROR = Checker.WriteERROR(_ERROR, new WE().GetErrorIdByName(Constants.ERROR_LEAVE_LEAVEWD_NAME));
                                        _WhatToSave = Checker.WriteStatus(_WhatToSave, "0");
                                    }
                                    else
                                    {
                                        _ERROR = Checker.WriteERROR(_ERROR, -1);
                                        _WhatToSave = Checker.WriteStatus(_WhatToSave, "1");
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else
                {
                    _ERROR = Checker.WriteERROR(_ERROR, new WE().GetErrorIdByName(Constants.ERROR_WORKING_DAYS_NAME));
                    _WhatToSave = Checker.WriteStatus(_WhatToSave, "0");
                }
                var _WorkdayCoefficientEmployeeIdFinal =
                    Convert.ToInt32(DRWD["WorkdayCoefficientEmployeeIdFinal"]);
                var _UserId = int.Parse(DRWD["UserId"].ToString());

                if (_WhatToSave.Contains("0"))
                {
                    _SP = $"Upd_H1_WorkdayCoefficientEmployeesFinal_WDStatus";
                    _SPValue =
                        $"UserId: {_UserId}, DataDate: '{DataDate}', WDStatus: {0}, CheckRemark: {_ERROR}, WorkdayCoefficientEmployeeIdFinal: {_WorkdayCoefficientEmployeeIdFinal}";
                    Utilities.Utilities.SaveHRMLog("H1_WorkdayCoefficientEmployeesFinal", _SP, _SPValue,
                        string.Empty);
                    WorkdayCoefficientEmployeesFinalBLL.UpdateWDStatus(_UserId,
                        DataDate, 0,
                        _ERROR, _WorkdayCoefficientEmployeeIdFinal);
                }
                else
                {
                    _SP = $"Upd_H1_WorkdayCoefficientEmployeesFinal_WDStatus";
                    _SPValue =
                        $"UserId: {_UserId}, DataDate: '{DataDate}', WDStatus: {1}, CheckRemark: {_ERROR}, WorkdayCoefficientEmployeeIdFinal: {_WorkdayCoefficientEmployeeIdFinal}";
                    Utilities.Utilities.SaveHRMLog("H1_WorkdayCoefficientEmployeesFinal", _SP, _SPValue,
                        string.Empty);
                    WorkdayCoefficientEmployeesFinalBLL.UpdateWDStatus(_UserId,
                        DataDate, 3,
                        _ERROR, _WorkdayCoefficientEmployeeIdFinal);
                }
            }
        }

        private List<DateTime> GetDateRange(DateTime StartingDate, DateTime EndingDate)
        {
            if (StartingDate > EndingDate)
                return null;
            var rv = new List<DateTime>();
            var tmpDate = StartingDate;
            do
            {
                rv.Add(tmpDate);
                tmpDate = tmpDate.AddDays(1);
            } while (tmpDate <= EndingDate);
            return rv;
        }

        private void radGridView1_CurrentCellChanged(object sender, CurrentCellChangedEventArgs e)
        {
            if (radGridView1.Rows.Count > 0)
            {
                if (radGridView1.CurrentRow != null && e.CurrentCell != null)
                {
                    if (e.NewCell.ColumnInfo.Name == "Day1" ||
                            e.NewCell.ColumnInfo.Name == "Day2" ||
                            e.NewCell.ColumnInfo.Name == "Day3" ||
                            e.NewCell.ColumnInfo.Name == "Day4" ||
                            e.NewCell.ColumnInfo.Name == "Day5" ||
                            e.NewCell.ColumnInfo.Name == "Day6" ||
                            e.NewCell.ColumnInfo.Name == "Day7" ||
                            e.NewCell.ColumnInfo.Name == "Day8" ||
                            e.NewCell.ColumnInfo.Name == "Day9" ||
                            e.NewCell.ColumnInfo.Name == "Day10" ||
                            e.NewCell.ColumnInfo.Name == "Day11" ||
                            e.NewCell.ColumnInfo.Name == "Day12" ||
                            e.NewCell.ColumnInfo.Name == "Day13" ||
                            e.NewCell.ColumnInfo.Name == "Day14" ||
                            e.NewCell.ColumnInfo.Name == "Day15" ||
                            e.NewCell.ColumnInfo.Name == "Day16" ||
                            e.NewCell.ColumnInfo.Name == "Day17" ||
                            e.NewCell.ColumnInfo.Name == "Day18" ||
                            e.NewCell.ColumnInfo.Name == "Day19" ||
                            e.NewCell.ColumnInfo.Name == "Day20" ||
                            e.NewCell.ColumnInfo.Name == "Day21" ||
                            e.NewCell.ColumnInfo.Name == "Day22" ||
                            e.NewCell.ColumnInfo.Name == "Day23" ||
                            e.NewCell.ColumnInfo.Name == "Day24" ||
                            e.NewCell.ColumnInfo.Name == "Day25" ||
                            e.NewCell.ColumnInfo.Name == "Day26" ||
                            e.NewCell.ColumnInfo.Name == "Day27" ||
                            e.NewCell.ColumnInfo.Name == "Day28" ||
                            e.NewCell.ColumnInfo.Name == "Day29" ||
                            e.NewCell.ColumnInfo.Name == "Day30" ||
                            e.NewCell.ColumnInfo.Name == "Day31")
                    {
                        if (radGridView2.Rows.Count > 0)
                        {
                            radGridView2.Columns[e.NewCell.ColumnInfo.Name].IsCurrent = true;
                        }
                    }
                }
            }
        }
    }

    public class ListERROR
    {
        public ListERROR()
        {
        }

        public ListERROR(int _Id, string _Name)
        {
            Id = _Id;
            Name = _Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ListIndex
    {
        public ListIndex(int _ColumnIndex, int _RowIndex, string _CellName, string _FixContent)
        {
            RowIndex = _RowIndex;
            CellName = _CellName;
            FixContent = _FixContent;
            ColumnIndex = _ColumnIndex;
        }

        public ListIndex(GridViewRowInfo _RowInfo, string _CellName)
        {
            RowInfo = _RowInfo;
            CellName = _CellName;
        }

        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public GridViewRowInfo RowInfo { get; set; }
        public string CellName { get; set; }
        public string FixContent { get; set; }
        public int UserId { get; set; }
    }
}