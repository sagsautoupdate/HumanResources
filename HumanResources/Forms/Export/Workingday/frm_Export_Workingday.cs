using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Excel;
using HRMBLL.H0;
using HRMBLL.H1;
using HumanResources.Utilities;
using Telerik.WinControls.UI;
using Application = Excel.Application;
using Constants = HRMUtil.Constants;
using DataTable = System.Data.DataTable;
using Range = Excel.Range;

namespace HumanResources.Forms.Export.Workingday
{
    public partial class frm_Export_Workingday : RadForm
    {
        private static frm_Export_Workingday s_Instance;
        private readonly RadWaitingBar rwb = new RadWaitingBar();
        private int _Total;

        public frm_Export_Workingday()
        {
            InitializeComponent();
        }

        public static frm_Export_Workingday Instance
        {
            get
            {
                if (s_Instance == null)
                    s_Instance = new frm_Export_Workingday();
                return s_Instance;
            }
        }

        private void frm_Export_Workingday_Load(object sender, EventArgs e)
        {
            Utilities.Utilities.PopulateRootLevel(radTreeView1);
            documentWindow1.Text = "DANH SÁCH CÔNG HƯỞNG LƯƠNG";
            toolWindow1.AutoHide();

            radCheckedDropDownList1.DataSource = BindSheetList();
            radCheckedDropDownList1.DisplayMember = "LeaveName";
            radCheckedDropDownList1.ValueMember = "LeaveName";
            radCheckedDropDownList1.AutoCompleteDataSource = BindSheetList();
            radCheckedDropDownList1.AutoCompleteDisplayMember = "LeaveName";
            radCheckedDropDownList1.AutoCompleteValueMember = "LeaveName";

            FormClosed += Frm_Export_Workingday_FormClosed;
            radDateTimePicker1.ValueChanged += RadDateTimePicker1_ValueChanged;
            radGridView1.FilterChanged += RadGridView1_FilterChanged;
            radMenuItem1.Click += RadMenuItem1_Click;
            radTreeView1.SelectedNodeChanged += RadTreeView1_SelectedNodeChanged;

            BS_EmpWorkingday.DataSource =
                WorkdayCoefficientEmployeesFinalBLL.GetDataForCoefficientWorkingDay(
                    Convert.ToInt32(radDateTimePicker1.Value.Month), Convert.ToInt32(radDateTimePicker1.Value.Year),
                    string.Empty,
                    2);
            radLabelElement1.Text = "Tổng số nhân viên: " + radGridView1.ChildRows.Count;
            Utilities.Utilities.GridFormatting(radGridView1);

            radGridView1.ShowColumnChooser();
        }

        private void RadTreeView1_SelectedNodeChanged(object sender, RadTreeViewEventArgs e)
        {
            if (radTreeView1.SelectedNode != null)
            {
                Cursor.Current = Cursors.AppStarting;

                var deptSelected = int.Parse(radTreeView1.SelectedNode.Value.ToString());
                var objD = new DepartmentsBLL();
                objD.GetAllChildId(deptSelected);
                var departmentIds = objD.ChildNodeIds;

                var list =
                    WorkdayCoefficientEmployeesFinalBLL.GetDataForCoefficientWorkingDay(
                        Convert.ToInt32(radDateTimePicker1.Value.Month), Convert.ToInt32(radDateTimePicker1.Value.Year),
                        departmentIds, 2);
                radGridView1.DataSource = list;
                Utilities.Utilities.GridFormatting(radGridView1);
                radLabelElement1.Text = "Tổng số nhân viên: " +
                                        Utilities.Utilities.GetDataRowCount(radGridView1.ChildRows);
                Cursor.Current = Cursors.Default;
            }
        }

        private List<LeaveNameExport> BindSheetList()
        {
            var _lst = new List<LeaveNameExport>();
            _lst.Add(new LeaveNameExport(9999, "Total"));
            _lst.Add(new LeaveNameExport(5, "F"));
            _lst.Add(new LeaveNameExport(1, "Om"));
            _lst.Add(new LeaveNameExport(3, "TS"));
            _lst.Add(new LeaveNameExport(8, "Ko"));
            _lst.Add(new LeaveNameExport(18, "Co"));
            _lst.Add(new LeaveNameExport(2, "OmDN"));
            _lst.Add(new LeaveNameExport(34, "OmDNBHXH"));
            _lst.Add(new LeaveNameExport(19, "KHH"));
            _lst.Add(new LeaveNameExport(20, "ST"));
            _lst.Add(new LeaveNameExport(21, "Khamthai"));
            _lst.Add(new LeaveNameExport(4, "TNLD"));
            _lst.Add(new LeaveNameExport(6, "Fdb"));
            _lst.Add(new LeaveNameExport(7, "Ro"));
            _lst.Add(new LeaveNameExport(9, "Diduong"));
            _lst.Add(new LeaveNameExport(23, "DinhChiCT"));

            return _lst;
        }

        private void RadMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!backgroundWorker1.IsBusy)
                {
                    var deptSelected = int.Parse(radTreeView1.SelectedNode.Value.ToString());
                    var objD = new DepartmentsBLL();
                    objD.GetAllChildId(deptSelected);
                    var departmentIds = objD.ChildNodeIds;

                    _Total =
                        WorkdayCoefficientEmployeesFinalBLL.GetForExport(departmentIds, 0, radDateTimePicker1.Value.Year)
                            .Rows.Count*radCheckedDropDownList1.CheckedItems.Count;
                    var fullName = string.Empty;
                    backgroundWorker1.RunWorkerAsync(fullName);
                    Utilities.Utilities.ShowWaiting(rwb, radGridView1);
                }
            }
            catch
            {
                MessageBox.Show("Lỗi trích xuất dữ liệu!");
            }
        }

        private void RadDateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            Cursor.Current = Cursors.AppStarting;
            BS_EmpWorkingday.DataSource =
                WorkdayCoefficientEmployeesFinalBLL.GetDataForCoefficientWorkingDay(
                    Convert.ToInt32(radDateTimePicker1.Value.Month), Convert.ToInt32(radDateTimePicker1.Value.Year),
                    string.Empty,
                    2);
            radLabelElement1.Text = "Tổng số nhân viên: " + radGridView1.ChildRows.Count;
            Utilities.Utilities.GridFormatting(radGridView1);
            Cursor.Current = Cursors.Default;
        }

        private void Frm_Export_Workingday_FormClosed(object sender, FormClosedEventArgs e)
        {
            s_Instance = null;
        }

        private void RadGridView1_FilterChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            radLabelElement1.Text = "Tổng số nhân viên: " + Utilities.Utilities.GetDataRowCount(radGridView1.ChildRows);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (radCheckedDropDownList1.CheckedItems.Count > 0)
            {
                var worker = (BackgroundWorker) sender;
                for (var i = 0; i < _Total; i++)
                {
                    worker.ReportProgress(i);


                    Thread.Sleep(100);
                }
                var fullName = (string) e.Argument;
                e.Result = runExport(fullName);
            }
            else
            {
                MessageBox.Show("Chọn ít nhất một loại phép để xuất Excel");
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!backgroundWorker1.CancellationPending)
                Utilities.Utilities.SetWaitingText(rwb, string.Format("{0}/{1}", e.ProgressPercentage, _Total));
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Xuất Excel thành công! File được lưu ngoài Desktop");
            Utilities.Utilities.StopWaiting(rwb, radGridView1);
        }

        private _Application runExport(string fullName)
        {
            var deptSelected = int.Parse(radTreeView1.SelectedNode.Value.ToString());
            var objD = new DepartmentsBLL();
            objD.GetAllChildId(deptSelected);
            var departmentIds = objD.ChildNodeIds;

            var _lstSheet = new List<string>();
            foreach (var item in radCheckedDropDownList1.CheckedItems)
                _lstSheet.Add(item.Text);

            return ExecuteExport_Workingday(departmentIds, _lstSheet, radGridView1, Text, Text,
                radDateTimePicker1.Value.Year);
        }

        public _Application ExecuteExport_Workingday(string depIds, List<string> lstSheet, RadGridView exportDt,
            string fullName, string pathName, int sYear)
        {
            if (exportDt.TableElement.VisualRows.Count > 0)
            {
                _Application oExcel;
                _Workbook oWorkBook;

                _Worksheet oWorkSheet;
                _Worksheet oWorkSheetF;
                _Worksheet oWorkSheetOm;
                _Worksheet oWorkSheetTS;
                _Worksheet oWorkSheetRo;
                _Worksheet oWorkSheetCo;
                _Worksheet oWorkSheetOmDN;
                _Worksheet oWorkSheetOmDNBHXH;
                _Worksheet oWorkSheetKHH;
                _Worksheet oWorkSheetST;
                _Worksheet oWorkSheetKhamthai;
                _Worksheet oWorkSheetTNLD;
                _Worksheet oWorkSheetFdb;
                _Worksheet oWorkSheetKo;
                _Worksheet oWorkSheetDiduong;
                _Worksheet oWorkSheetDinhChiCT;

                var fileName = string.Empty;
                try
                {
                    oExcel = new Application();
                    oExcel.Visible = false;


                    oWorkBook = oExcel.Workbooks.Add(Missing.Value);

                    oWorkSheet = (_Worksheet) oWorkBook.Worksheets[1];
                    oWorkSheet.Name = "Total";

                    oWorkSheetF = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheet, Type.Missing, Type.Missing);
                    oWorkSheetF.Name = "F";
                    oWorkSheetOm = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheetF, Type.Missing, Type.Missing);
                    oWorkSheetOm.Name = "Om";
                    oWorkSheetTS = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheetOm, Type.Missing, Type.Missing);
                    oWorkSheetTS.Name = "TS";
                    oWorkSheetKo = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheetTS, Type.Missing, Type.Missing);
                    oWorkSheetKo.Name = "Ko";
                    oWorkSheetCo = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheetKo, Type.Missing, Type.Missing);
                    oWorkSheetCo.Name = "Co";
                    oWorkSheetOmDN = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheetCo, Type.Missing, Type.Missing);
                    oWorkSheetOmDN.Name = "OmDN";
                    oWorkSheetOmDNBHXH = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheetOmDN, Type.Missing,
                        Type.Missing);
                    oWorkSheetOmDNBHXH.Name = "OmDNBHXH";
                    oWorkSheetKHH = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheetOmDNBHXH, Type.Missing,
                        Type.Missing);
                    oWorkSheetKHH.Name = "KHH";
                    oWorkSheetST = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheetKHH, Type.Missing, Type.Missing);
                    oWorkSheetST.Name = "ST";
                    oWorkSheetKhamthai = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheetST, Type.Missing, Type.Missing);
                    oWorkSheetKhamthai.Name = "Khamthai";
                    oWorkSheetTNLD = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheetKhamthai, Type.Missing,
                        Type.Missing);
                    oWorkSheetTNLD.Name = "TNLD";
                    oWorkSheetFdb = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheetTNLD, Type.Missing, Type.Missing);
                    oWorkSheetFdb.Name = "Fdb";
                    oWorkSheetRo = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheetFdb, Type.Missing, Type.Missing);
                    oWorkSheetRo.Name = "Ro";
                    oWorkSheetDiduong = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheetKo, Type.Missing, Type.Missing);
                    oWorkSheetDiduong.Name = "Diduong";
                    oWorkSheetDinhChiCT = oWorkBook.Worksheets.Add(Type.Missing, oWorkSheetDiduong, Type.Missing,
                        Type.Missing);
                    oWorkSheetDinhChiCT.Name = "DinhChiCT";

                    foreach (_Worksheet ws in oWorkBook.Worksheets)
                        if (lstSheet.Exists(x => x.Contains(ws.Name)))
                            InsertDataToWorkSheet_Workingday(depIds, ws, fullName, sYear);
                    oWorkSheet.Activate();

                    oExcel.Visible = false;
                    oExcel.UserControl = false;

                    fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + pathName.ToUpper() +
                               " " + DateTime.Now.ToString("dd-MM-yyyy");
                    if (File.Exists(fileName))
                        File.Delete(fileName);
                    oWorkBook.SaveAs(fileName, XlFileFormat.xlWorkbookNormal, null, null, false, false,
                        XlSaveAsAccessMode.xlShared, false, false, null, null, null);


                    oWorkBook.Close(null, null, null);
                    oExcel.Workbooks.Close();
                    oExcel.Quit();

                    Marshal.ReleaseComObject(oExcel);

                    Marshal.ReleaseComObject(oWorkSheet);
                    Marshal.ReleaseComObject(oWorkSheetF);
                    Marshal.ReleaseComObject(oWorkSheetOm);
                    Marshal.ReleaseComObject(oWorkSheetTS);
                    Marshal.ReleaseComObject(oWorkSheetRo);
                    Marshal.ReleaseComObject(oWorkSheetCo);
                    Marshal.ReleaseComObject(oWorkSheetOmDN);
                    Marshal.ReleaseComObject(oWorkSheetOmDNBHXH);
                    Marshal.ReleaseComObject(oWorkSheetKHH);
                    Marshal.ReleaseComObject(oWorkSheetST);
                    Marshal.ReleaseComObject(oWorkSheetKhamthai);
                    Marshal.ReleaseComObject(oWorkSheetTNLD);
                    Marshal.ReleaseComObject(oWorkSheetFdb);
                    Marshal.ReleaseComObject(oWorkSheetKo);
                    Marshal.ReleaseComObject(oWorkSheetDiduong);
                    Marshal.ReleaseComObject(oWorkSheetDinhChiCT);

                    Marshal.ReleaseComObject(oWorkBook);

                    oWorkSheet = null;
                    oWorkSheetF = null;
                    oWorkSheetOm = null;
                    oWorkSheetTS = null;
                    oWorkSheetRo = null;
                    oWorkSheetCo = null;
                    oWorkSheetOmDN = null;
                    oWorkSheetOmDNBHXH = null;
                    oWorkSheetKHH = null;
                    oWorkSheetST = null;
                    oWorkSheetKhamthai = null;
                    oWorkSheetTNLD = null;
                    oWorkSheetFdb = null;
                    oWorkSheetKo = null;
                    oWorkSheetDiduong = null;
                    oWorkSheetDinhChiCT = null;

                    oExcel = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    return oExcel;
                }
                catch (Exception ex)
                {
                    string errorMessage;
                    errorMessage = "Error: ";
                    errorMessage = string.Concat(errorMessage, ex.Message);
                    errorMessage = string.Concat(errorMessage, " Line: ");
                    errorMessage = string.Concat(errorMessage, ex.Source);
                    throw new Exception(errorMessage);
                }
            }
            return null;
        }

        public void InsertDataToWorkSheet_Workingday(string depIds, _Worksheet oWorkSheet, string ExportName,
            int sYear)
        {
            var RootId = 0;
            var RootIdBefore = 0;
            var initTitleIndex = 7;
            var orderNumber = 1;

            var indexRow = initTitleIndex + 2;
            var lstRoot = new List<int>();


            CreateHeader_Workingday(ref oWorkSheet, ref initTitleIndex, ExportName);


            if (oWorkSheet.Name == "Total")
            {
                var dt = WorkdayCoefficientEmployeesFinalBLL.GetForExport(depIds, 0, sYear);
                var dr0 = dt.Rows[0];
                var rangeDept = oWorkSheet.get_Range("A" + indexRow, "R" + indexRow);
                rangeDept.Merge(Type.Missing);
                oWorkSheet.Cells[indexRow, 1] = dr0["RootName"] == DBNull.Value
                    ? string.Empty
                    : dr0["RootName"].ToString().ToUpper();
                rangeDept.Font.Bold = true;
                lstRoot.Add(indexRow);

                indexRow++;

                oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                oWorkSheet.Cells[indexRow, 2] = dr0["UserId"] == DBNull.Value ? string.Empty : dr0["UserId"].ToString();
                oWorkSheet.Cells[indexRow, 3] = dr0["FullName"] == DBNull.Value
                    ? string.Empty
                    : dr0["FullName"].ToString();
                oWorkSheet.Cells[indexRow, 4] = dr0["PositionName"] == DBNull.Value
                    ? string.Empty
                    : dr0["PositionName"].ToString();
                var drDep = DepartmentEmployeeBLL.GetDRByDeptId(Convert.ToInt32(dr0["DepartmentId"]));
                if (drDep != null)
                {
                    var Level = Convert.ToInt32(drDep["Level"]);

                    oWorkSheet.Cells[indexRow, 5] = dr0["DepartmentFullName"] == DBNull.Value
                        ? string.Empty
                        : Utilities.Utilities.GetDepartmentFullName(dr0["DepartmentFullName"].ToString(), Level);
                }
                oWorkSheet.Cells[indexRow, 6] = dr0["FLeft"] == DBNull.Value ? string.Empty : dr0["FLeft"].ToString();
                oWorkSheet.Cells[indexRow, 7] = dr0["OmLeft"] == DBNull.Value ? string.Empty : dr0["OmLeft"].ToString();
                oWorkSheet.Cells[indexRow, 8] = dr0["TSLeft"] == DBNull.Value ? string.Empty : dr0["TSLeft"].ToString();
                oWorkSheet.Cells[indexRow, 9] = dr0["RoLeft"] == DBNull.Value ? string.Empty : dr0["RoLeft"].ToString();
                oWorkSheet.Cells[indexRow, 10] = dr0["CoLeft"] == DBNull.Value ? string.Empty : dr0["CoLeft"].ToString();
                oWorkSheet.Cells[indexRow, 11] = dr0["OmDN"] == DBNull.Value ? string.Empty : dr0["OmDN"].ToString();
                oWorkSheet.Cells[indexRow, 12] = dr0["OmDNBHXH"] == DBNull.Value
                    ? string.Empty
                    : dr0["OmDNBHXH"].ToString();
                oWorkSheet.Cells[indexRow, 13] = dr0["KHH"] == DBNull.Value ? string.Empty : dr0["KHH"].ToString();
                oWorkSheet.Cells[indexRow, 14] = dr0["ST"] == DBNull.Value ? string.Empty : dr0["ST"].ToString();
                oWorkSheet.Cells[indexRow, 15] = dr0["Khamthai"] == DBNull.Value
                    ? string.Empty
                    : dr0["Khamthai"].ToString();
                oWorkSheet.Cells[indexRow, 16] = dr0["TNLD"] == DBNull.Value ? string.Empty : dr0["TNLD"].ToString();
                oWorkSheet.Cells[indexRow, 17] = dr0["Fdb"] == DBNull.Value ? string.Empty : dr0["Fdb"].ToString();
                oWorkSheet.Cells[indexRow, 18] = dr0["Ro"] == DBNull.Value ? string.Empty : dr0["Ro"].ToString();
                oWorkSheet.Cells[indexRow, 19] = dr0["Diduong"] == DBNull.Value
                    ? string.Empty
                    : dr0["Diduong"].ToString();
                oWorkSheet.Cells[indexRow, 20] = dr0["DinhChiCT"] == DBNull.Value
                    ? string.Empty
                    : dr0["DinhChiCT"].ToString();
                for (var i = 1; i < dt.Rows.Count; i++)
                {
                    var dr = dt.Rows[i];
                    var dr_1 = dt.Rows[i - 1];
                    var rootId = dr["RootId"] == DBNull.Value ? 0 : int.Parse(dr["RootId"].ToString());
                    var rootId_1 = dr_1["RootId"] == DBNull.Value ? 0 : int.Parse(dr_1["RootId"].ToString());

                    indexRow++;
                    if ((i < dt.Rows.Count - 1) && (i > 1))
                    {
                        RootId = rootId;
                        RootIdBefore = rootId_1;
                    }

                    if (RootId != RootIdBefore)
                    {
                        rangeDept = oWorkSheet.get_Range("A" + indexRow, "R" + indexRow);
                        rangeDept.Merge(Type.Missing);
                        oWorkSheet.Cells[indexRow, 1] = dr["RootName"] == DBNull.Value
                            ? string.Empty
                            : dr["RootName"].ToString().ToUpper();
                        rangeDept.Font.Bold = true;
                        indexRow++;
                    }


                    oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                    oWorkSheet.Cells[indexRow, 2] = dr["UserId"] == DBNull.Value
                        ? string.Empty
                        : dr["UserId"].ToString();
                    oWorkSheet.Cells[indexRow, 3] = dr["FullName"] == DBNull.Value
                        ? string.Empty
                        : dr["FullName"].ToString();
                    oWorkSheet.Cells[indexRow, 4] = dr["PositionName"] == DBNull.Value
                        ? string.Empty
                        : dr["PositionName"].ToString();
                    var drDep1 = DepartmentEmployeeBLL.GetDRByDeptId(Convert.ToInt32(dr["DepartmentId"]));
                    if (drDep1 != null)
                    {
                        var Level = Convert.ToInt32(drDep1["Level"]);

                        oWorkSheet.Cells[indexRow, 5] = dr["DepartmentFullName"] == DBNull.Value
                            ? string.Empty
                            : Utilities.Utilities.GetDepartmentFullName(dr["DepartmentFullName"].ToString(), Level);
                    }
                    oWorkSheet.Cells[indexRow, 6] = dr["FLeft"] == DBNull.Value ? string.Empty : dr["FLeft"].ToString();
                    oWorkSheet.Cells[indexRow, 7] = dr["OmLeft"] == DBNull.Value
                        ? string.Empty
                        : dr["OmLeft"].ToString();
                    oWorkSheet.Cells[indexRow, 8] = dr["TSLeft"] == DBNull.Value
                        ? string.Empty
                        : dr["TSLeft"].ToString();
                    oWorkSheet.Cells[indexRow, 9] = dr["RoLeft"] == DBNull.Value
                        ? string.Empty
                        : dr["RoLeft"].ToString();
                    oWorkSheet.Cells[indexRow, 10] = dr["CoLeft"] == DBNull.Value
                        ? string.Empty
                        : dr["CoLeft"].ToString();
                    oWorkSheet.Cells[indexRow, 11] = dr["OmDN"] == DBNull.Value ? string.Empty : dr["OmDN"].ToString();
                    oWorkSheet.Cells[indexRow, 12] = dr["OmDNBHXH"] == DBNull.Value
                        ? string.Empty
                        : dr["OmDNBHXH"].ToString();
                    oWorkSheet.Cells[indexRow, 13] = dr["KHH"] == DBNull.Value ? string.Empty : dr["KHH"].ToString();
                    oWorkSheet.Cells[indexRow, 14] = dr["ST"] == DBNull.Value ? string.Empty : dr["ST"].ToString();
                    oWorkSheet.Cells[indexRow, 15] = dr["Khamthai"] == DBNull.Value
                        ? string.Empty
                        : dr["Khamthai"].ToString();
                    oWorkSheet.Cells[indexRow, 16] = dr["TNLD"] == DBNull.Value ? string.Empty : dr["TNLD"].ToString();
                    oWorkSheet.Cells[indexRow, 17] = dr["Fdb"] == DBNull.Value ? string.Empty : dr["Fdb"].ToString();
                    oWorkSheet.Cells[indexRow, 18] = dr["Ro"] == DBNull.Value ? string.Empty : dr["Ro"].ToString();
                    oWorkSheet.Cells[indexRow, 19] = dr["Diduong"] == DBNull.Value
                        ? string.Empty
                        : dr["Diduong"].ToString();
                    oWorkSheet.Cells[indexRow, 20] = dr["DinhChiCT"] == DBNull.Value
                        ? string.Empty
                        : dr["DinhChiCT"].ToString();
                }
            }


            else
            {
                var dt0 = WorkdayCoefficientEmployeesFinalBLL.GetForExport(depIds, 0, sYear);
                var dtEmp =
                    WorkdayCoefficientEmployeesFinalBLL.GetDataByMonthYear(Convert.ToInt32(dt0.Rows[0]["UserId"]), 0,
                        sYear, 2);
                var dr0Emp = dtEmp.Rows[0];
                var rangeDept = oWorkSheet.get_Range("A" + indexRow, "R" + indexRow);
                rangeDept.Merge(Type.Missing);
                oWorkSheet.Cells[indexRow, 1] = dr0Emp["RootName"] == DBNull.Value
                    ? string.Empty
                    : dr0Emp["RootName"].ToString().ToUpper();
                rangeDept.Font.Bold = true;


                indexRow++;

                oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                oWorkSheet.Cells[indexRow, 2] = dr0Emp["UserId"] == DBNull.Value
                    ? string.Empty
                    : dr0Emp["UserId"].ToString();
                oWorkSheet.Cells[indexRow, 3] = dr0Emp["FullName"] == DBNull.Value
                    ? string.Empty
                    : dr0Emp["FullName"].ToString();
                oWorkSheet.Cells[indexRow, 4] = dr0Emp["PositionName"] == DBNull.Value
                    ? string.Empty
                    : dr0Emp["PositionName"].ToString();
                var Level = Convert.ToInt32(dr0Emp["Level"]);
                oWorkSheet.Cells[indexRow, 5] = dr0Emp["DepartmentFullName"] == DBNull.Value
                    ? string.Empty
                    : Utilities.Utilities.GetDepartmentFullName(dr0Emp["DepartmentFullName"].ToString(), Level);

                CalculateTotalLeave(dtEmp, oWorkSheet, indexRow);
                for (var i = 1; i < dt0.Rows.Count; i++)
                {
                    var dr1Emp = dt0.Rows[i];
                    var drNext = dt0.Rows[i - 1];
                    var rootId = dr1Emp["RootId"] == DBNull.Value ? 0 : int.Parse(dr1Emp["RootId"].ToString());
                    var rootId_1 = drNext["RootId"] == DBNull.Value ? 0 : int.Parse(drNext["RootId"].ToString());
                    var dt1Emp =
                        WorkdayCoefficientEmployeesFinalBLL.GetDataByMonthYear(Convert.ToInt32(dr1Emp["UserId"]), 0,
                            sYear, 2);
                    indexRow++;
                    if ((i < dt0.Rows.Count - 1) && (i > 1))
                    {
                        RootId = rootId;
                        RootIdBefore = rootId_1;
                    }

                    if (RootId != RootIdBefore)
                    {
                        rangeDept = oWorkSheet.get_Range("A" + indexRow, "R" + indexRow);
                        rangeDept.Merge(Type.Missing);
                        oWorkSheet.Cells[indexRow, 1] = dr1Emp["RootName"] == DBNull.Value
                            ? string.Empty
                            : dr1Emp["RootName"].ToString().ToUpper();
                        rangeDept.Font.Bold = true;
                        indexRow++;
                    }

                    /// inserting order number
                    oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                    oWorkSheet.Cells[indexRow, 2] = dr1Emp["UserId"] == DBNull.Value
                        ? string.Empty
                        : dr1Emp["UserId"].ToString();
                    oWorkSheet.Cells[indexRow, 3] = dr1Emp["FullName"] == DBNull.Value
                        ? string.Empty
                        : dr1Emp["FullName"].ToString();
                    oWorkSheet.Cells[indexRow, 4] = dr1Emp["PositionName"] == DBNull.Value
                        ? string.Empty
                        : dr1Emp["PositionName"].ToString();
                    var Level0 = Convert.ToInt32(dt1Emp.Rows[0]["Level"]);
                    oWorkSheet.Cells[indexRow, 5] = dt1Emp.Rows[0]["DepartmentFullName"] == DBNull.Value
                        ? string.Empty
                        : Utilities.Utilities.GetDepartmentFullName(dt1Emp.Rows[0]["DepartmentFullName"].ToString(),
                            Level0);

                    CalculateTotalLeave(dt1Emp, oWorkSheet, indexRow);
                }
            }
        }

        public void CalculateTotalLeave(DataTable dt, _Worksheet oWorkSheet, int indexRow)
        {
            var _lstTemp = new List<LeaveCal>();
            if (dt.Rows.Count > 0)
                switch (oWorkSheet.Name)
                {
                    case "F":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) && (dr["F"] != DBNull.Value) &&
                                (Convert.ToInt32(dr["F"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["F"]), CalculateTotal("F", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("F", dt));
                    }
                        break;
                    case "Om":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) && (dr["Om"] != DBNull.Value) &&
                                (Convert.ToInt32(dr["Om"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["Om"]), CalculateTotal("Om", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("Om", dt));
                    }
                        break;
                    case "TS":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) && (dr["TS"] != DBNull.Value) &&
                                (Convert.ToInt32(dr["TS"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["TS"]), CalculateTotal("TS", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("TS", dt));
                    }
                        break;
                    case "Ko":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) && (dr["Ko"] != DBNull.Value) &&
                                (Convert.ToInt32(dr["Ko"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["Ko"]), CalculateTotal("Ko", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("Ko", dt));
                    }
                        break;
                    case "Co":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) && (dr["Co"] != DBNull.Value) &&
                                (Convert.ToInt32(dr["Co"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["Co"]), CalculateTotal("Co", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("Co", dt));
                    }
                        break;
                    case "OmDN":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) && (dr["OmDN"] != DBNull.Value) &&
                                (Convert.ToInt32(dr["OmDN"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["OmDN"]), CalculateTotal("OmDN", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("OmDN", dt));
                    }
                        break;
                    case "OmDNBHXH":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) &&
                                (dr["OmDNBHXH"] != DBNull.Value) && (Convert.ToInt32(dr["OmDNBHXH"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["OmDNBHXH"]), CalculateTotal("OmDNBHXH", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("OmDNBHXH", dt));
                    }
                        break;
                    case "KHH":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) && (dr["KHH"] != DBNull.Value) &&
                                (Convert.ToInt32(dr["KHH"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["KHH"]), CalculateTotal("KHH", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("KHH", dt));
                    }
                        break;
                    case "ST":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) && (dr["ST"] != DBNull.Value) &&
                                (Convert.ToInt32(dr["ST"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["ST"]), CalculateTotal("ST", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("ST", dt));
                    }
                        break;
                    case "Khamthai":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) &&
                                (dr["Khamthai"] != DBNull.Value) && (Convert.ToInt32(dr["Khamthai"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["Khamthai"]), CalculateTotal("Khamthai", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("Khamthai", dt));
                    }
                        break;
                    case "TNLD":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) && (dr["TNLD"] != DBNull.Value) &&
                                (Convert.ToInt32(dr["TNLD"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["TNLD"]), CalculateTotal("TNLD", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("TNLD", dt));
                    }
                        break;
                    case "Fdb":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) && (dr["Fdb"] != DBNull.Value) &&
                                (Convert.ToInt32(dr["Fdb"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["Fdb"]), CalculateTotal("Fdb", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("Fdb", dt));
                    }
                        break;
                    case "Ro":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) && (dr["Ro"] != DBNull.Value) &&
                                (Convert.ToInt32(dr["Ro"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["Ro"]), CalculateTotal("Ro", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("Ro", dt));
                    }
                        break;
                    case "Diduong":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) &&
                                (dr["Diduong"] != DBNull.Value) && (Convert.ToInt32(dr["Diduong"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["Diduong"]), CalculateTotal("Diduong", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("Diduong", dt));
                    }
                        break;
                    case "DinhChiCT":
                    {
                        foreach (DataRow dr in dt.Rows)
                            if ((dr != null) &&
                                (dr["DinhChiCT"] != DBNull.Value) && (Convert.ToInt32(dr["DinhChiCT"]) > 0))
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month,
                                    Convert.ToInt32(dr["DinhChiCT"]), CalculateTotal("DinhChiCT", dt)));
                            else
                                _lstTemp.Add(new LeaveCal(Convert.ToDateTime(dr["DataDate"]).Month, 2406, 1991));
                        BindCalculateToRow(_lstTemp, oWorkSheet, indexRow, CalculateTotal("DinhChiCT", dt));
                    }
                        break;
                }
        }

        public void BindCalculateToRow(List<LeaveCal> _lstTemp, _Worksheet oWorkSheet, int indexRow, int Sum)
        {
            foreach (var item in _lstTemp)
                switch (item.Month)
                {
                    case 1:
                        if ((item.Value != 2406) && (item.Sum != 1991))
                        {
                            oWorkSheet.Cells[indexRow, 6] = item.Value;
                            ((Range) oWorkSheet.Cells[indexRow, 6]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        else
                        {
                            oWorkSheet.Cells[indexRow, 6] = "-";
                            ((Range) oWorkSheet.Cells[indexRow, 6]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        break;
                    case 2:
                        if ((item.Value != 2406) && (item.Sum != 1991))
                        {
                            oWorkSheet.Cells[indexRow, 7] = item.Value;
                            ((Range) oWorkSheet.Cells[indexRow, 7]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        else
                        {
                            oWorkSheet.Cells[indexRow, 7] = "-";
                            ((Range) oWorkSheet.Cells[indexRow, 7]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        break;
                    case 3:
                        if ((item.Value != 2406) && (item.Sum != 1991))
                        {
                            oWorkSheet.Cells[indexRow, 8] = item.Value;
                            ((Range) oWorkSheet.Cells[indexRow, 8]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        else
                        {
                            oWorkSheet.Cells[indexRow, 8] = "-";
                            ((Range) oWorkSheet.Cells[indexRow, 8]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        break;
                    case 4:
                        if ((item.Value != 2406) && (item.Sum != 1991))
                        {
                            oWorkSheet.Cells[indexRow, 9] = item.Value;
                            ((Range) oWorkSheet.Cells[indexRow, 9]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        else
                        {
                            oWorkSheet.Cells[indexRow, 9] = "-";
                            ((Range) oWorkSheet.Cells[indexRow, 9]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        break;
                    case 5:
                        if ((item.Value != 2406) && (item.Sum != 1991))
                        {
                            oWorkSheet.Cells[indexRow, 10] = item.Value;
                            ((Range) oWorkSheet.Cells[indexRow, 10]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        else
                        {
                            oWorkSheet.Cells[indexRow, 10] = "-";
                            ((Range) oWorkSheet.Cells[indexRow, 10]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        break;
                    case 6:
                        if ((item.Value != 2406) && (item.Sum != 1991))
                        {
                            oWorkSheet.Cells[indexRow, 11] = item.Value;
                            ((Range) oWorkSheet.Cells[indexRow, 11]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        else
                        {
                            oWorkSheet.Cells[indexRow, 11] = "-";
                            ((Range) oWorkSheet.Cells[indexRow, 11]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        break;
                    case 7:
                        if ((item.Value != 2406) && (item.Sum != 1991))
                        {
                            oWorkSheet.Cells[indexRow, 12] = item.Value;
                            ((Range) oWorkSheet.Cells[indexRow, 12]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        else
                        {
                            oWorkSheet.Cells[indexRow, 12] = "-";
                            ((Range) oWorkSheet.Cells[indexRow, 12]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        break;
                    case 8:
                        if ((item.Value != 2406) && (item.Sum != 1991))
                        {
                            oWorkSheet.Cells[indexRow, 13] = item.Value;
                            ((Range) oWorkSheet.Cells[indexRow, 13]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        else
                        {
                            oWorkSheet.Cells[indexRow, 13] = "-";
                            ((Range) oWorkSheet.Cells[indexRow, 13]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        break;
                    case 9:
                        if ((item.Value != 2406) && (item.Sum != 1991))
                        {
                            oWorkSheet.Cells[indexRow, 14] = item.Value;
                            ((Range) oWorkSheet.Cells[indexRow, 14]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        else
                        {
                            oWorkSheet.Cells[indexRow, 14] = "-";
                            ((Range) oWorkSheet.Cells[indexRow, 14]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        break;
                    case 10:
                        if ((item.Value != 2406) && (item.Sum != 1991))
                        {
                            oWorkSheet.Cells[indexRow, 15] = item.Value;
                            ((Range) oWorkSheet.Cells[indexRow, 15]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        else
                        {
                            oWorkSheet.Cells[indexRow, 15] = "-";
                            ((Range) oWorkSheet.Cells[indexRow, 15]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        break;
                    case 11:
                        if ((item.Value != 2406) && (item.Sum != 1991))
                        {
                            oWorkSheet.Cells[indexRow, 16] = item.Value;
                            ((Range) oWorkSheet.Cells[indexRow, 16]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        else
                        {
                            oWorkSheet.Cells[indexRow, 16] = "-";
                            ((Range) oWorkSheet.Cells[indexRow, 16]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        break;
                    case 12:
                        if ((item.Value != 2406) && (item.Sum != 1991))
                        {
                            oWorkSheet.Cells[indexRow, 17] = item.Value;
                            ((Range) oWorkSheet.Cells[indexRow, 17]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        else
                        {
                            oWorkSheet.Cells[indexRow, 17] = "-";
                            ((Range) oWorkSheet.Cells[indexRow, 17]).HorizontalAlignment = XlHAlign.xlHAlignRight;
                        }
                        break;
                }
            oWorkSheet.Cells[indexRow, 18] = Sum == 0 ? "-" : Sum.ToString();
            ((Range) oWorkSheet.Cells[indexRow, 18]).HorizontalAlignment = XlHAlign.xlHAlignRight;
        }

        public int CalculateLeave(DataRow dr, string LeaveCode)
        {
            var Return = 0;
            ;
            if ((dr != null) && (dr[LeaveCode] != null) && (Convert.ToInt32(dr[LeaveCode]) > 0))
            {
                if (LeaveCode == "Om")
                    LeaveCode = Constants.LEAVE_TYPE_O_BAN_THAN_CODE;
                if (LeaveCode == "OmDN")
                    LeaveCode = Constants.LEAVE_TYPE_O_DAI_NGAY_CODE;
                if (LeaveCode == "TS")
                    LeaveCode = Constants.LEAVE_TYPE_THAI_SAN_CODE;
                if (LeaveCode == "TNLD")
                    LeaveCode = Constants.LEAVE_TYPE_TNLD_CODE;
                if (LeaveCode == "F")
                    LeaveCode = Constants.LEAVE_TYPE_F_NAM_CODE;
                if (LeaveCode == "Fdb")
                    LeaveCode = Constants.LEAVE_TYPE_FDB_CODE;
                if (LeaveCode == "Ro")
                    LeaveCode = Constants.LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO_CODE;
                if (LeaveCode == "Ko")
                    LeaveCode = Constants.LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO_CODE;
                if (LeaveCode == "Diduong")
                    LeaveCode = Constants.LEAVE_TYPE_F_DI_DUONG_CODE;
                if (LeaveCode == "Co")
                    LeaveCode = Constants.LEAVE_TYPE_CON_OM_CODE;
                if (LeaveCode == "KHH")
                    LeaveCode = Constants.LEAVE_TYPE_KHHDS_CODE;
                if (LeaveCode == "ST")
                    LeaveCode = Constants.LEAVE_TYPE_SAY_THAI_CODE;
                if (LeaveCode == "Khamthai")
                    LeaveCode = Constants.LEAVE_TYPE_KHAM_THAI_CODE;
                if (LeaveCode == "DinhChiCT")
                    LeaveCode = Constants.LEAVE_TYPE_DINH_CHI_CONG_TAC_CODE;
                if (LeaveCode == "OmDNBHXH")
                    LeaveCode = Constants.LEAVE_TYPE_O_DAI_NGAY_HUONG_BHXH_CODE;
                for (var i = 1; i <= 31; i++)
                    if (dr["Day" + i].ToString().Equals(LeaveCode))
                        Return++;
            }
            return Return;
        }


        public void CreateHeader_Workingday(ref _Worksheet oWorkSheet, ref int initTitleIndex,
            string oWorkSheetName)
        {
            var childTitleIndex = initTitleIndex + 1;

            var rangeHeader1 = oWorkSheet.get_Range("A1", "R1");
            rangeHeader1.Merge(Type.Missing);
            oWorkSheet.Cells[1, 1] = "CÔNG TY CỔ PHẦN PHỤC VỤ MẶT ĐẤT SÀI GON";
            rangeHeader1.Font.Size = 15;
            rangeHeader1.Font.Bold = true;
            rangeHeader1.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            rangeHeader1.Font.Name = "Times New Roman";

            var rangeHeader2 = oWorkSheet.get_Range("A3", "R3");
            rangeHeader2.Merge(Type.Missing);
            oWorkSheet.Cells[3, 1] = oWorkSheetName.ToUpper();
            rangeHeader2.Font.Size = 16;
            rangeHeader2.Font.Bold = true;
            rangeHeader2.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader2.Font.Name = "Times New Roman";

            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            ((Range) oWorkSheet.Cells[initTitleIndex, 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, 1]).ColumnWidth = 5;
            ((Range) oWorkSheet.Cells[initTitleIndex, 1]).Font.Bold = true;
            var rNo = oWorkSheet.get_Range("A" + initTitleIndex, "A" + childTitleIndex);
            rNo.Merge(Type.Missing);
            rNo.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rNo.VerticalAlignment = XlVAlign.xlVAlignCenter;

            oWorkSheet.Cells[initTitleIndex, 2] = "Mã nhân viên";
            ((Range) oWorkSheet.Cells[initTitleIndex, 2]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, 2]).ColumnWidth = 7;
            ((Range) oWorkSheet.Cells[initTitleIndex, 2]).Font.Bold = true;
            var rUserId = oWorkSheet.get_Range("B" + initTitleIndex, "B" + childTitleIndex);
            rUserId.Merge(Type.Missing);
            rUserId.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rUserId.VerticalAlignment = XlVAlign.xlVAlignCenter;

            oWorkSheet.Cells[initTitleIndex, 3] = "Họ và tên";
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 20;
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).Font.Bold = true;
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            var rFullName = oWorkSheet.get_Range("C" + initTitleIndex, "C" + childTitleIndex);
            rFullName.Merge(Type.Missing);
            rFullName.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rFullName.VerticalAlignment = XlVAlign.xlVAlignCenter;

            oWorkSheet.Cells[initTitleIndex, 4] = "Chức danh";
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).ColumnWidth = 25;
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).Font.Bold = true;
            var rPositionName = oWorkSheet.get_Range("D" + initTitleIndex, "D" + childTitleIndex);
            rPositionName.Merge(Type.Missing);
            rPositionName.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rPositionName.VerticalAlignment = XlVAlign.xlVAlignCenter;

            oWorkSheet.Cells[initTitleIndex, 5] = "Phòng ban";
            ((Range) oWorkSheet.Cells[initTitleIndex, 5]).ColumnWidth = 25;
            ((Range) oWorkSheet.Cells[initTitleIndex, 5]).Font.Bold = true;
            var rDepartment = oWorkSheet.get_Range("E" + initTitleIndex, "E" + childTitleIndex);
            rDepartment.Merge(Type.Missing);
            rDepartment.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rDepartment.VerticalAlignment = XlVAlign.xlVAlignCenter;

            if (oWorkSheet.Name == "Total")
            {
                oWorkSheet.Cells[initTitleIndex, 6] = "F";
                ((Range) oWorkSheet.Cells[initTitleIndex, 6]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 6]).Font.Bold = true;

                oWorkSheet.Cells[initTitleIndex, 7] = "Om";
                ((Range) oWorkSheet.Cells[initTitleIndex, 7]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 7]).Font.Bold = true;

                oWorkSheet.Cells[initTitleIndex, 8] = "TS";
                ((Range) oWorkSheet.Cells[initTitleIndex, 8]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 8]).Font.Bold = true;

                oWorkSheet.Cells[initTitleIndex, 18] = "Ko";
                ((Range) oWorkSheet.Cells[initTitleIndex, 18]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 18]).Font.Bold = true;

                oWorkSheet.Cells[initTitleIndex, 10] = "Co";
                ((Range) oWorkSheet.Cells[initTitleIndex, 10]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 10]).Font.Bold = true;

                oWorkSheet.Cells[initTitleIndex, 11] = "OmDN";
                ((Range) oWorkSheet.Cells[initTitleIndex, 11]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 11]).Font.Bold = true;

                oWorkSheet.Cells[initTitleIndex, 12] = "OmDNBHXH";
                ((Range) oWorkSheet.Cells[initTitleIndex, 12]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 12]).Font.Bold = true;

                oWorkSheet.Cells[initTitleIndex, 13] = "KHH";
                ((Range) oWorkSheet.Cells[initTitleIndex, 13]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 13]).Font.Bold = true;
                ;

                oWorkSheet.Cells[initTitleIndex, 14] = "ST";
                ((Range) oWorkSheet.Cells[initTitleIndex, 14]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 14]).Font.Bold = true;

                oWorkSheet.Cells[initTitleIndex, 15] = "Khamthai";
                ((Range) oWorkSheet.Cells[initTitleIndex, 15]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 15]).Font.Bold = true;

                oWorkSheet.Cells[initTitleIndex, 16] = "TNLD";
                ((Range) oWorkSheet.Cells[initTitleIndex, 16]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 16]).Font.Bold = true;

                oWorkSheet.Cells[initTitleIndex, 17] = "Fdb";
                ((Range) oWorkSheet.Cells[initTitleIndex, 17]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 17]).Font.Bold = true;

                oWorkSheet.Cells[initTitleIndex, 9] = "Ro";
                ((Range) oWorkSheet.Cells[initTitleIndex, 9]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 9]).Font.Bold = true;

                oWorkSheet.Cells[initTitleIndex, 19] = "Diduong";
                ((Range) oWorkSheet.Cells[initTitleIndex, 19]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 19]).Font.Bold = true;

                oWorkSheet.Cells[initTitleIndex, 20] = "DinhChiCT";
                ((Range) oWorkSheet.Cells[initTitleIndex, 20]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, 20]).Font.Bold = true;
            }
            else
            {
                if (oWorkSheet.Name == "F")
                {
                    oWorkSheet.Cells[initTitleIndex, 6] = "F";
                    ((Range) oWorkSheet.Cells[initTitleIndex, 6]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    ((Range) oWorkSheet.Cells[initTitleIndex, 6]).Font.Bold = true;
                    var rF = oWorkSheet.get_Range(GetExcelColumnName(6) + initTitleIndex,
                        GetExcelColumnName(18) + initTitleIndex);
                    rF.Merge(Type.Missing);
                    AddColumn_Workingday(oWorkSheet, childTitleIndex, 6);
                }
                else
                {
                    if (oWorkSheet.Name == "Om")
                    {
                        oWorkSheet.Cells[initTitleIndex, 6] = "Om";
                        ((Range) oWorkSheet.Cells[initTitleIndex, 6]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        ((Range) oWorkSheet.Cells[initTitleIndex, 6]).Font.Bold = true;
                        var rOm = oWorkSheet.get_Range(GetExcelColumnName(6) + initTitleIndex,
                            GetExcelColumnName(18) + initTitleIndex);
                        rOm.Merge(Type.Missing);
                        AddColumn_Workingday(oWorkSheet, childTitleIndex, 6);
                    }
                    else
                    {
                        if (oWorkSheet.Name == "TS")
                        {
                            oWorkSheet.Cells[initTitleIndex, 6] = "TS";
                            ((Range) oWorkSheet.Cells[initTitleIndex, 6]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                            ((Range) oWorkSheet.Cells[initTitleIndex, 6]).Font.Bold = true;
                            var rTS = oWorkSheet.get_Range(GetExcelColumnName(6) + initTitleIndex,
                                GetExcelColumnName(18) + initTitleIndex);
                            rTS.Merge(Type.Missing);
                            AddColumn_Workingday(oWorkSheet, childTitleIndex, 6);
                        }
                        else
                        {
                            if (oWorkSheet.Name == "Ro")
                            {
                                oWorkSheet.Cells[initTitleIndex, 6] = "Ro";
                                ((Range) oWorkSheet.Cells[initTitleIndex, 6]).HorizontalAlignment =
                                    XlHAlign.xlHAlignCenter;
                                ((Range) oWorkSheet.Cells[initTitleIndex, 6]).Font.Bold = true;
                                var rRo = oWorkSheet.get_Range(GetExcelColumnName(6) + initTitleIndex,
                                    GetExcelColumnName(18) + initTitleIndex);
                                rRo.Merge(Type.Missing);
                                AddColumn_Workingday(oWorkSheet, childTitleIndex, 6);
                            }
                            else
                            {
                                if (oWorkSheet.Name == "Co")
                                {
                                    oWorkSheet.Cells[initTitleIndex, 6] = "Co";
                                    ((Range) oWorkSheet.Cells[initTitleIndex, 6]).HorizontalAlignment =
                                        XlHAlign.xlHAlignCenter;
                                    ((Range) oWorkSheet.Cells[initTitleIndex, 6]).Font.Bold = true;
                                    var rCo = oWorkSheet.get_Range(GetExcelColumnName(6) + initTitleIndex,
                                        GetExcelColumnName(18) + initTitleIndex);
                                    rCo.Merge(Type.Missing);
                                    AddColumn_Workingday(oWorkSheet, childTitleIndex, 6);
                                }
                                else
                                {
                                    if (oWorkSheet.Name == "OmDN")
                                    {
                                        oWorkSheet.Cells[initTitleIndex, 6] = "OmDN";
                                        ((Range) oWorkSheet.Cells[initTitleIndex, 6]).HorizontalAlignment =
                                            XlHAlign.xlHAlignCenter;
                                        ((Range) oWorkSheet.Cells[initTitleIndex, 6]).Font.Bold = true;
                                        var rOmDN = oWorkSheet.get_Range(GetExcelColumnName(6) + initTitleIndex,
                                            GetExcelColumnName(18) + initTitleIndex);
                                        rOmDN.Merge(Type.Missing);
                                        AddColumn_Workingday(oWorkSheet, childTitleIndex, 6);
                                    }
                                    else
                                    {
                                        if (oWorkSheet.Name == "OmDNBHXH")
                                        {
                                            oWorkSheet.Cells[initTitleIndex, 6] = "OmDNBHXH";
                                            ((Range) oWorkSheet.Cells[initTitleIndex, 6]).HorizontalAlignment =
                                                XlHAlign.xlHAlignCenter;
                                            ((Range) oWorkSheet.Cells[initTitleIndex, 6]).Font.Bold = true;
                                            var rOmDNBHXH = oWorkSheet.get_Range(GetExcelColumnName(6) + initTitleIndex,
                                                GetExcelColumnName(18) + initTitleIndex);
                                            rOmDNBHXH.Merge(Type.Missing);
                                            AddColumn_Workingday(oWorkSheet, childTitleIndex, 6);
                                        }
                                        else
                                        {
                                            if (oWorkSheet.Name == "KHH")
                                            {
                                                oWorkSheet.Cells[initTitleIndex, 6] = "KHH";
                                                ((Range) oWorkSheet.Cells[initTitleIndex, 6]).HorizontalAlignment =
                                                    XlHAlign.xlHAlignCenter;
                                                ((Range) oWorkSheet.Cells[initTitleIndex, 6]).Font.Bold = true;
                                                var rKHH = oWorkSheet.get_Range(GetExcelColumnName(6) + initTitleIndex,
                                                    GetExcelColumnName(18) + initTitleIndex);
                                                rKHH.Merge(Type.Missing);
                                                AddColumn_Workingday(oWorkSheet, childTitleIndex, 6);
                                            }
                                            else
                                            {
                                                if (oWorkSheet.Name == "ST")
                                                {
                                                    oWorkSheet.Cells[initTitleIndex, 6] = "ST";
                                                    ((Range) oWorkSheet.Cells[initTitleIndex, 6]).HorizontalAlignment =
                                                        XlHAlign.xlHAlignCenter;
                                                    ((Range) oWorkSheet.Cells[initTitleIndex, 6]).Font.Bold = true;
                                                    var rST =
                                                        oWorkSheet.get_Range(GetExcelColumnName(6) + initTitleIndex,
                                                            GetExcelColumnName(18) + initTitleIndex);
                                                    rST.Merge(Type.Missing);
                                                    AddColumn_Workingday(oWorkSheet, childTitleIndex, 6);
                                                }
                                                else
                                                {
                                                    if (oWorkSheet.Name == "Khamthai")
                                                    {
                                                        oWorkSheet.Cells[initTitleIndex, 6] = "KHH";
                                                        ((Range) oWorkSheet.Cells[initTitleIndex, 6])
                                                            .HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                                        ((Range) oWorkSheet.Cells[initTitleIndex, 6]).Font.Bold = true;
                                                        var rKhamthai =
                                                            oWorkSheet.get_Range(GetExcelColumnName(6) + initTitleIndex,
                                                                GetExcelColumnName(18) + initTitleIndex);
                                                        rKhamthai.Merge(Type.Missing);
                                                        AddColumn_Workingday(oWorkSheet, childTitleIndex, 6);
                                                    }
                                                    else
                                                    {
                                                        if (oWorkSheet.Name == "TNLD")
                                                        {
                                                            oWorkSheet.Cells[initTitleIndex, 6] = "TNLD";
                                                            ((Range) oWorkSheet.Cells[initTitleIndex, 6])
                                                                .HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                                            ((Range) oWorkSheet.Cells[initTitleIndex, 6]).Font.Bold =
                                                                true;
                                                            var rTNLD =
                                                                oWorkSheet.get_Range(
                                                                    GetExcelColumnName(6) + initTitleIndex,
                                                                    GetExcelColumnName(18) + initTitleIndex);
                                                            rTNLD.Merge(Type.Missing);
                                                            AddColumn_Workingday(oWorkSheet, childTitleIndex, 6);
                                                        }
                                                        else
                                                        {
                                                            if (oWorkSheet.Name == "Fdb")
                                                            {
                                                                oWorkSheet.Cells[initTitleIndex, 6] = "Fdb";
                                                                ((Range) oWorkSheet.Cells[initTitleIndex, 6])
                                                                    .HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                                                ((Range) oWorkSheet.Cells[initTitleIndex, 6]).Font.Bold
                                                                    = true;
                                                                var rFdb =
                                                                    oWorkSheet.get_Range(
                                                                        GetExcelColumnName(6) + initTitleIndex,
                                                                        GetExcelColumnName(18) + initTitleIndex);
                                                                rFdb.Merge(Type.Missing);
                                                                AddColumn_Workingday(oWorkSheet, childTitleIndex, 6);
                                                            }
                                                            else
                                                            {
                                                                if (oWorkSheet.Name == "Ko")
                                                                {
                                                                    oWorkSheet.Cells[initTitleIndex, 6] = "Ko";
                                                                    ((Range) oWorkSheet.Cells[initTitleIndex, 6])
                                                                        .HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                                                    ((Range) oWorkSheet.Cells[initTitleIndex, 6]).Font
                                                                        .Bold = true;
                                                                    var rKo =
                                                                        oWorkSheet.get_Range(
                                                                            GetExcelColumnName(6) + initTitleIndex,
                                                                            GetExcelColumnName(18) + initTitleIndex);
                                                                    rKo.Merge(Type.Missing);
                                                                    AddColumn_Workingday(oWorkSheet, childTitleIndex, 6);
                                                                }
                                                                else
                                                                {
                                                                    if (oWorkSheet.Name == "Diduong")
                                                                    {
                                                                        oWorkSheet.Cells[initTitleIndex, 6] = "Diduong";
                                                                        ((Range) oWorkSheet.Cells[initTitleIndex, 6])
                                                                                .HorizontalAlignment =
                                                                            XlHAlign.xlHAlignCenter;
                                                                        ((Range) oWorkSheet.Cells[initTitleIndex, 6])
                                                                            .Font.Bold = true;
                                                                        var rDiduong =
                                                                            oWorkSheet.get_Range(
                                                                                GetExcelColumnName(6) + initTitleIndex,
                                                                                GetExcelColumnName(18) + initTitleIndex);
                                                                        rDiduong.Merge(Type.Missing);
                                                                        AddColumn_Workingday(oWorkSheet, childTitleIndex,
                                                                            6);
                                                                    }
                                                                    else
                                                                    {
                                                                        if (oWorkSheet.Name == "DinhChiCT")
                                                                        {
                                                                            oWorkSheet.Cells[initTitleIndex, 6] =
                                                                                "DinhChiCT";
                                                                            ((Range) oWorkSheet.Cells[initTitleIndex, 6])
                                                                                    .HorizontalAlignment =
                                                                                XlHAlign.xlHAlignCenter;
                                                                            ((Range) oWorkSheet.Cells[initTitleIndex, 6])
                                                                                .Font.Bold = true;
                                                                            var rDC =
                                                                                oWorkSheet.get_Range(
                                                                                    GetExcelColumnName(6) +
                                                                                    initTitleIndex,
                                                                                    GetExcelColumnName(18) +
                                                                                    initTitleIndex);
                                                                            rDC.Merge(Type.Missing);
                                                                            AddColumn_Workingday(oWorkSheet,
                                                                                childTitleIndex, 6);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public string GetExcelColumnName(int columnNumber)
        {
            var dividend = columnNumber;
            var columnName = string.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1)%26;
                columnName = Convert.ToChar(65 + modulo) + columnName;
                dividend = (dividend - modulo)/26;
            }

            return columnName;
        }

        public void AddColumn_Workingday(_Worksheet oWorkSheet, int initTitleIndex, int startNumber)
        {
            for (var i = 0; i < 13; i++)
                if (i == 12)
                {
                    oWorkSheet.Cells[initTitleIndex, startNumber + i] = "T";
                    ((Range) oWorkSheet.Cells[initTitleIndex, startNumber + i]).ColumnWidth = 4;
                    ((Range) oWorkSheet.Cells[initTitleIndex, startNumber + i]).Font.Bold = true;
                    ((Range) oWorkSheet.Cells[initTitleIndex, startNumber + i]).HorizontalAlignment =
                        XlHAlign.xlHAlignCenter;
                }
                else
                {
                    oWorkSheet.Cells[initTitleIndex, startNumber + i] = i + 1;
                    ((Range) oWorkSheet.Cells[initTitleIndex, startNumber + i]).ColumnWidth = 3;
                    ((Range) oWorkSheet.Cells[initTitleIndex, startNumber + i]).Font.Bold = true;
                    ((Range) oWorkSheet.Cells[initTitleIndex, startNumber + i]).HorizontalAlignment =
                        XlHAlign.xlHAlignCenter;
                }
        }

        public int CalculateTotal(string LeaveCode, DataTable dt)
        {
            return CalculateTotalByCode(LeaveCode, dt);
        }

        public int CalculateTotalByCode(string LeaveCode, DataTable dt)
        {
            var Return = 0;
            foreach (DataRow dr in dt.Rows)
                if (dr[LeaveCode] != DBNull.Value)
                    Return += Convert.ToInt32(dr[LeaveCode]);
            return Return;
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!backgroundWorker1.IsBusy)
                {
                    var deptSelected = int.Parse(radTreeView1.SelectedNode.Value.ToString());
                    var objD = new DepartmentsBLL();
                    objD.GetAllChildId(deptSelected);
                    var departmentIds = objD.ChildNodeIds;

                    _Total =
                        WorkdayCoefficientEmployeesFinalBLL.GetForExport(departmentIds, 0, radDateTimePicker1.Value.Year)
                            .Rows.Count*radCheckedDropDownList1.CheckedItems.Count;
                    var fullName = string.Empty;
                    backgroundWorker1.RunWorkerAsync(fullName);
                    Utilities.Utilities.ShowWaiting(rwb, radGridView1);
                }
            }
            catch
            {
                MessageBox.Show("Lỗi trích xuất dữ liệu!");
            }
        }
    }
}