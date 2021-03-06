using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Excel;
using HRMDAL.H1;
using HRMUtil;
using HRMUtil.KeyNames.H0;
using HRMUtil.KeyNames.H1;
using DataTable = System.Data.DataTable;

namespace HRMBLL.H1.Helper
{
    public class WorkdayEmployeesBLLExportData
    {
        public static string ExcelByFilter(string fullName, string departmentIds, int month, int year, int status,
            int receivedUserId, string pathName, int rootId)
        {
            ///////////////////////////////////////////
            var dt = new WorkdayEmployeesDAL().GetAllByFilter(fullName, departmentIds, month, year, status,
                receivedUserId, 2);
            ///////////////////////////////////////////

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                _Application oExcel;
                _Workbook oWorkBook;
                _Worksheet oWorkSheet;

                var fileName = string.Empty;
                try
                {
                    GC.Collect();
                    oExcel = new Application();
                    oExcel.Visible = false;

                    // Get new workbook
                    oWorkBook = oExcel.Workbooks.Add(Missing.Value);
                    oWorkSheet = (_Worksheet) oWorkBook.Worksheets[1];

                    oWorkSheet.Name = "BCCThucTe";

                    InsertDataToWorkSheet(dt, ref oWorkSheet, month, year, rootId);

                    oExcel.Visible = false;
                    oExcel.UserControl = false;
                    fileName = pathName + "\\BCCThucTe_" + month + "_" + year + ".xls";
                    if (File.Exists(fileName))
                        File.Delete(fileName);
                    oWorkBook.SaveAs(fileName, XlFileFormat.xlWorkbookNormal, null, null, false, false,
                        XlSaveAsAccessMode.xlShared, false, false, null, null, null);

                    // Need all following code to clean up and extingush all references!!!
                    oWorkBook.Close(null, null, null);
                    oExcel.Workbooks.Close();
                    oExcel.Quit();
                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(oRange);
                    Marshal.ReleaseComObject(oExcel);
                    Marshal.ReleaseComObject(oWorkSheet);

                    Marshal.ReleaseComObject(oWorkBook);
                    oWorkSheet = null;
                    oWorkSheet = null;
                    oExcel = null;
                    GC.Collect(); // force final cleanup!

                    return fileName;
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

        private static void InsertDataToWorkSheet(DataTable dt, ref _Worksheet oWorkSheet, int month, int year,
            int rootIdType)
        {
            var deparmentId = 0;
            var departmentIdBefore = 0;
            var initTitleIndex = 7;
            // create Content
            var indexRow = initTitleIndex + 1;
            var orderNumber = 1;
            var value = string.Empty;
            var deptIdKey = "";
            var deptNameKey = "";
            if (rootIdType == 0)
            {
                deptIdKey = DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID;
                deptNameKey = DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME;
            }
            else
            {
                deptIdKey = DepartmentKeys.FIELD_DEPARTMENT_ID;
                deptNameKey = DepartmentKeys.FIELD_DEPARTMENT_NAME;
            }

            CreateHeaderAndTitle(ref oWorkSheet, ref initTitleIndex, month, year);

            #region Create FirstRow For Director, Administration and Training Department from oWorkSheet

            var rangeDept = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangeDept.Merge(Type.Missing);


            var dr0 = dt.Rows[0];

            oWorkSheet.Cells[indexRow, 1] = dr0[deptNameKey] == DBNull.Value
                ? string.Empty
                : dr0[deptNameKey].ToString().ToUpper();

            rangeDept.Font.Bold = true;


            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;

            oWorkSheet.Cells[indexRow, 2] = dr0[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE] == DBNull.Value
                ? string.Empty
                : dr0[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE].ToString();
            oWorkSheet.Cells[indexRow, 3] = dr0[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                ? string.Empty
                : dr0[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();
            oWorkSheet.Cells[indexRow, 4] = dr0[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                ? string.Empty
                : dr0[PositionKeys.FIELD_POSITION_NAME].ToString();

            oWorkSheet.Cells[indexRow, 5] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day1] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day1].ToString();
            oWorkSheet.Cells[indexRow, 6] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day2] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day2].ToString();
            oWorkSheet.Cells[indexRow, 7] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day3] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day3].ToString();
            oWorkSheet.Cells[indexRow, 8] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day4] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day4].ToString();
            oWorkSheet.Cells[indexRow, 9] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day5] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day5].ToString();
            oWorkSheet.Cells[indexRow, 10] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day6] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day6].ToString();
            oWorkSheet.Cells[indexRow, 11] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day7] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day7].ToString();
            oWorkSheet.Cells[indexRow, 12] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day8] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day8].ToString();
            oWorkSheet.Cells[indexRow, 13] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day9] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day9].ToString();
            oWorkSheet.Cells[indexRow, 14] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day10] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day10].ToString();


            oWorkSheet.Cells[indexRow, 15] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day11] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day11].ToString();
            oWorkSheet.Cells[indexRow, 16] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day12] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day12].ToString();
            oWorkSheet.Cells[indexRow, 17] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day13] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day13].ToString();
            oWorkSheet.Cells[indexRow, 18] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day14] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day14].ToString();
            oWorkSheet.Cells[indexRow, 19] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day15] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day15].ToString();
            oWorkSheet.Cells[indexRow, 20] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day16] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day16].ToString();
            oWorkSheet.Cells[indexRow, 21] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day17] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day17].ToString();
            oWorkSheet.Cells[indexRow, 22] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day18] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day18].ToString();
            oWorkSheet.Cells[indexRow, 23] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day19] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day19].ToString();
            oWorkSheet.Cells[indexRow, 24] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day20] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day20].ToString();

            oWorkSheet.Cells[indexRow, 25] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day21] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day21].ToString();
            oWorkSheet.Cells[indexRow, 26] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day22] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day22].ToString();
            oWorkSheet.Cells[indexRow, 27] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day23] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day23].ToString();
            oWorkSheet.Cells[indexRow, 28] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day24] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day24].ToString();
            oWorkSheet.Cells[indexRow, 29] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day25] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day25].ToString();
            oWorkSheet.Cells[indexRow, 30] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day26] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day26].ToString();
            oWorkSheet.Cells[indexRow, 31] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day27] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day27].ToString();
            oWorkSheet.Cells[indexRow, 32] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day28] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day28].ToString();
            oWorkSheet.Cells[indexRow, 33] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day29] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day29].ToString();
            oWorkSheet.Cells[indexRow, 34] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day30] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day30].ToString();

            oWorkSheet.Cells[indexRow, 35] = dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day31] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_Workday_Employee_Day31].ToString();

            oWorkSheet.Cells[indexRow, 36] = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_NC_LAM_VIEC] == DBNull.Value
                ? 0
                : double.Parse(dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_NC_LAM_VIEC].ToString());

            var _F_Om = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_OM] == DBNull.Value
                ? 0
                : double.Parse(dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_OM].ToString());
            var _F_Con_Om = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_Con_Om] == DBNull.Value
                ? 0
                : double.Parse(dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_Con_Om].ToString());
            var _F_KHHDS = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_KHHDS] == DBNull.Value
                ? 0
                : double.Parse(dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_KHHDS].ToString());
            var _F_O_Co_KHHDS = _F_Om + _F_Con_Om + _F_KHHDS;
            oWorkSheet.Cells[indexRow, 37] = _F_O_Co_KHHDS == 0 ? string.Empty : _F_O_Co_KHHDS.ToString();

            oWorkSheet.Cells[indexRow, 38] = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_THAI_SAN] == DBNull.Value
                ? 0
                : double.Parse(dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_THAI_SAN].ToString());
            oWorkSheet.Cells[indexRow, 39] = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_TNLD] == DBNull.Value
                ? 0
                : double.Parse(dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_TNLD].ToString());
            oWorkSheet.Cells[indexRow, 40] = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_DI_DUONG] == DBNull.Value
                ? 0
                : double.Parse(dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_DI_DUONG].ToString());
            oWorkSheet.Cells[indexRow, 41] = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_DAC_BIET] == DBNull.Value
                ? 0
                : double.Parse(dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_DAC_BIET].ToString());

            var _F_HocSAGS = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HocSAGS] == DBNull.Value
                ? 0
                : double.Parse(dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HocSAGS].ToString());
            var _F_Hoc1 = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC1] == DBNull.Value
                ? 0
                : double.Parse(dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC1].ToString());
            var _F_Hoc2 = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC2] == DBNull.Value
                ? 0
                : double.Parse(dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC2].ToString());
            var _F_Hoc3 = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC3] == DBNull.Value
                ? 0
                : double.Parse(dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC3].ToString());
            var _F_Hoc4 = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC4] == DBNull.Value
                ? 0
                : double.Parse(dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC4].ToString());
            var _F_Hoc5 = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC5] == DBNull.Value
                ? 0
                : double.Parse(dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC5].ToString());
            var _F_Hoc6 = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC6] == DBNull.Value
                ? 0
                : double.Parse(dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC6].ToString());
            var _F_Hoc7 = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC7] == DBNull.Value
                ? 0
                : double.Parse(dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC7].ToString());
            var _F_Hoc = _F_HocSAGS + _F_Hoc1 + _F_Hoc2 + _F_Hoc3 + _F_Hoc4 + _F_Hoc5 + _F_Hoc6 + _F_Hoc7;

            oWorkSheet.Cells[indexRow, 42] = _F_Hoc == 0 ? string.Empty : _F_Hoc.ToString();
            oWorkSheet.Cells[indexRow, 43] = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_NAM] == DBNull.Value
                ? 0
                : double.Parse(dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_NAM].ToString());

            var _F_Le = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_LE] == DBNull.Value
                ? 0
                : double.Parse(dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_LE].ToString());
            var _F_KoLuongCLD = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_KO_LUONG_CLD] == DBNull.Value
                ? 0
                : double.Parse(dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_KO_LUONG_CLD].ToString());
            var _F_KoLuongKLD = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_KO_LUONG_KLD] == DBNull.Value
                ? 0
                : double.Parse(dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_KO_LUONG_KLD].ToString());
            var _F_Khac = _F_Le + _F_KoLuongCLD + _F_KoLuongKLD;

            oWorkSheet.Cells[indexRow, 44] = _F_Khac == 0 ? string.Empty : _F_Khac.ToString();
            oWorkSheet.Cells[indexRow, 45] = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_NghiTuan] == DBNull.Value
                ? 0
                : double.Parse(dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_NghiTuan].ToString());
            oWorkSheet.Cells[indexRow, 46] = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_NghiBu] == DBNull.Value
                ? 0
                : double.Parse(dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_NghiBu].ToString());
            oWorkSheet.Cells[indexRow, 47] = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_GC_LamDem] == DBNull.Value
                ? 0
                : double.Parse(dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_GC_LamDem].ToString());

            var _Mark = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_Mark] == DBNull.Value
                ? 0
                : double.Parse(dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_Mark].ToString());
            oWorkSheet.Cells[indexRow, 48] = _Mark;
            oWorkSheet.Cells[indexRow, 49] = _Mark/100;
            if (((month == 3) && (year == 2008)) || ((month == 4) && (year == 2008)))
                oWorkSheet.Cells[indexRow, 50] = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_HTCV] == DBNull.Value
                    ? string.Empty
                    : dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_HTCV].ToString();
            else
                oWorkSheet.Cells[indexRow, 50] = DefaultValues.HTCV(_Mark);

            oWorkSheet.Cells[indexRow, 51] = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_Remark] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_Remark].ToString();

            #endregion

            for (var i = 1; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                var dr_1 = dt.Rows[i - 1];
                var rootId = dr[deptIdKey] == DBNull.Value ? 0 : int.Parse(dr[deptIdKey].ToString());
                var rootId_1 = dr_1[deptIdKey] == DBNull.Value ? 0 : int.Parse(dr_1[deptIdKey].ToString());

                indexRow++;
                if ((i < dt.Rows.Count - 1) && (i > 1))
                {
                    deparmentId = rootId;
                    departmentIdBefore = rootId_1;
                }

                if (deparmentId != departmentIdBefore)
                {
                    rangeDept = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
                    rangeDept.Merge(Type.Missing);
                    oWorkSheet.Cells[indexRow, 1] = dr[deptNameKey] == DBNull.Value
                        ? string.Empty
                        : dr[deptNameKey].ToString().ToUpper();
                    rangeDept.Font.Bold = true;
                    indexRow++;
                }

                /// inserting order number
                oWorkSheet.Cells[indexRow, 1] = orderNumber++;

                oWorkSheet.Cells[indexRow, 2] = dr[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE].ToString();
                oWorkSheet.Cells[indexRow, 3] = dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();
                oWorkSheet.Cells[indexRow, 4] = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[PositionKeys.FIELD_POSITION_NAME].ToString();

                oWorkSheet.Cells[indexRow, 5] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day1] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day1].ToString();
                oWorkSheet.Cells[indexRow, 6] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day2] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day2].ToString();
                oWorkSheet.Cells[indexRow, 7] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day3] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day3].ToString();
                oWorkSheet.Cells[indexRow, 8] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day4] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day4].ToString();
                oWorkSheet.Cells[indexRow, 9] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day5] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day5].ToString();
                oWorkSheet.Cells[indexRow, 10] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day6] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day6].ToString();
                oWorkSheet.Cells[indexRow, 11] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day7] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day7].ToString();
                oWorkSheet.Cells[indexRow, 12] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day8] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day8].ToString();
                oWorkSheet.Cells[indexRow, 13] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day9] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day9].ToString();
                oWorkSheet.Cells[indexRow, 14] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day10] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day10].ToString();


                oWorkSheet.Cells[indexRow, 15] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day11] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day11].ToString();
                oWorkSheet.Cells[indexRow, 16] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day12] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day12].ToString();
                oWorkSheet.Cells[indexRow, 17] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day13] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day13].ToString();
                oWorkSheet.Cells[indexRow, 18] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day14] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day14].ToString();
                oWorkSheet.Cells[indexRow, 19] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day15] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day15].ToString();
                oWorkSheet.Cells[indexRow, 20] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day16] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day16].ToString();
                oWorkSheet.Cells[indexRow, 21] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day17] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day17].ToString();
                oWorkSheet.Cells[indexRow, 22] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day18] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day18].ToString();
                oWorkSheet.Cells[indexRow, 23] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day19] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day19].ToString();
                oWorkSheet.Cells[indexRow, 24] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day20] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day20].ToString();

                oWorkSheet.Cells[indexRow, 25] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day21] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day21].ToString();
                oWorkSheet.Cells[indexRow, 26] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day22] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day22].ToString();
                oWorkSheet.Cells[indexRow, 27] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day23] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day23].ToString();
                oWorkSheet.Cells[indexRow, 28] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day24] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day24].ToString();
                oWorkSheet.Cells[indexRow, 29] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day25] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day25].ToString();
                oWorkSheet.Cells[indexRow, 30] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day26] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day26].ToString();
                oWorkSheet.Cells[indexRow, 31] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day27] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day27].ToString();
                oWorkSheet.Cells[indexRow, 32] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day28] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day28].ToString();
                oWorkSheet.Cells[indexRow, 33] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day29] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day29].ToString();
                oWorkSheet.Cells[indexRow, 34] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day30] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day30].ToString();

                oWorkSheet.Cells[indexRow, 35] = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day31] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day31].ToString();

                oWorkSheet.Cells[indexRow, 36] = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_NC_LAM_VIEC] ==
                                                 DBNull.Value
                    ? 0
                    : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_NC_LAM_VIEC].ToString());

                var F_Om = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_OM] == DBNull.Value
                    ? 0
                    : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_OM].ToString());
                var F_Con_Om = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_Con_Om] == DBNull.Value
                    ? 0
                    : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_Con_Om].ToString());
                var F_KHHDS = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_KHHDS] == DBNull.Value
                    ? 0
                    : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_KHHDS].ToString());
                var F_O_Co_KHHDS = F_Om + F_Con_Om + F_KHHDS;
                oWorkSheet.Cells[indexRow, 37] = F_O_Co_KHHDS == 0 ? string.Empty : F_O_Co_KHHDS.ToString();

                oWorkSheet.Cells[indexRow, 38] = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_THAI_SAN] ==
                                                 DBNull.Value
                    ? 0
                    : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_THAI_SAN].ToString());
                oWorkSheet.Cells[indexRow, 39] = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_TNLD] == DBNull.Value
                    ? 0
                    : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_TNLD].ToString());
                oWorkSheet.Cells[indexRow, 40] = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_DI_DUONG] ==
                                                 DBNull.Value
                    ? 0
                    : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_DI_DUONG].ToString());
                oWorkSheet.Cells[indexRow, 41] = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_DAC_BIET] ==
                                                 DBNull.Value
                    ? 0
                    : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_DAC_BIET].ToString());

                var F_HocSAGS = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HocSAGS] == DBNull.Value
                    ? 0
                    : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HocSAGS].ToString());
                var F_Hoc1 = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC1] == DBNull.Value
                    ? 0
                    : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC1].ToString());
                var F_Hoc2 = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC2] == DBNull.Value
                    ? 0
                    : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC2].ToString());
                var F_Hoc3 = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC3] == DBNull.Value
                    ? 0
                    : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC3].ToString());
                var F_Hoc4 = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC4] == DBNull.Value
                    ? 0
                    : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC4].ToString());
                var F_Hoc5 = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC5] == DBNull.Value
                    ? 0
                    : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC5].ToString());
                var F_Hoc6 = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC6] == DBNull.Value
                    ? 0
                    : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC6].ToString());
                var F_Hoc7 = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC7] == DBNull.Value
                    ? 0
                    : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC7].ToString());
                var F_Hoc = F_HocSAGS + F_Hoc1 + F_Hoc2 + F_Hoc3 + F_Hoc4 + F_Hoc5 + F_Hoc6 + F_Hoc7;

                oWorkSheet.Cells[indexRow, 42] = F_Hoc == 0 ? string.Empty : F_Hoc.ToString();
                oWorkSheet.Cells[indexRow, 43] = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_NAM] == DBNull.Value
                    ? 0
                    : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_NAM].ToString());

                var F_Le = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_LE] == DBNull.Value
                    ? 0
                    : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_LE].ToString());
                var F_KoLuongCLD = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_KO_LUONG_CLD] == DBNull.Value
                    ? 0
                    : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_KO_LUONG_CLD].ToString());
                var F_KoLuongKLD = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_KO_LUONG_KLD] == DBNull.Value
                    ? 0
                    : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_KO_LUONG_KLD].ToString());
                var F_Khac = F_Le + F_KoLuongCLD + F_KoLuongKLD;

                oWorkSheet.Cells[indexRow, 44] = F_Khac == 0 ? string.Empty : F_Khac.ToString();
                oWorkSheet.Cells[indexRow, 45] = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_NghiTuan] == DBNull.Value
                    ? 0
                    : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_NghiTuan].ToString());
                oWorkSheet.Cells[indexRow, 46] = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_NghiBu] == DBNull.Value
                    ? 0
                    : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_NghiBu].ToString());
                oWorkSheet.Cells[indexRow, 47] = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_GC_LamDem] == DBNull.Value
                    ? 0
                    : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_GC_LamDem].ToString());

                var Mark = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_Mark] == DBNull.Value
                    ? 0
                    : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_Mark].ToString());
                oWorkSheet.Cells[indexRow, 48] = Mark;
                oWorkSheet.Cells[indexRow, 49] = Mark/100;
                if (((month == 3) && (year == 2008)) || ((month == 4) && (year == 2008)))
                    oWorkSheet.Cells[indexRow, 50] = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_HTCV] == DBNull.Value
                        ? string.Empty
                        : dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_HTCV].ToString();
                else
                    oWorkSheet.Cells[indexRow, 50] = DefaultValues.HTCV(Mark);

                oWorkSheet.Cells[indexRow, 51] = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_Remark] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_Remark].ToString();
            }
        }

        private static void CreateHeaderAndTitle(ref _Worksheet oWorkSheet, ref int initTitleIndex, int month, int year)
        {
            #region create header for printing from oWorkSheet

            var rangeHeader1 = oWorkSheet.get_Range("A1", "D1");
            rangeHeader1.Merge(Type.Missing);
            oWorkSheet.Cells[1, 1] = "TỔNG CÔNG TY CẢNG HÀNG KHÔNG MIỀN NAM ";
            rangeHeader1.Font.Size = 12;

            var rangeHeader2 = oWorkSheet.get_Range("A2", "D2");
            rangeHeader2.Merge(Type.Missing);
            oWorkSheet.Cells[2, 1] = "CÔNG TY PHỤC VỤ MẶT ĐẤT SÀI GÒN";
            rangeHeader2.Font.Size = 12;
            rangeHeader2.Font.Bold = true;

            var rangeHeader3 = oWorkSheet.get_Range("A4", "AI4");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "BẢNG CHẤM CÔNG THÁNG " + month + " NĂM " + year;
            rangeHeader3.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader3.Font.Size = 15;
            rangeHeader3.Font.Bold = true;

            #endregion

            #region Create Title for Account List from oWorkSheet

            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            oWorkSheet.Cells[initTitleIndex, 2] = "Mã SAA";
            oWorkSheet.Cells[initTitleIndex, 3] = "Họ Tên";
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 30;
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            oWorkSheet.Cells[initTitleIndex, 4] = "Chức danh";
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).ColumnWidth = 20;
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

            var daysInNow = DateTime.DaysInMonth(year, month);
            DateTime dt;
            var columnIndex = 0;

            for (var i = 1; i <= daysInNow; i++)
            {
                columnIndex = i + 4;
                dt = new DateTime(year, month, i);
                if (dt.DayOfWeek == DayOfWeek.Sunday)
                    oWorkSheet.Cells[initTitleIndex, columnIndex] = i + "\nCN";
                else
                    oWorkSheet.Cells[initTitleIndex, columnIndex] = i.ToString();
                ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex]).ColumnWidth = 3;
                ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            }
            if (daysInNow == 28)
            {
                columnIndex++;
                oWorkSheet.Cells[initTitleIndex, columnIndex] = "";
                columnIndex++;
                oWorkSheet.Cells[initTitleIndex, columnIndex] = "";
                columnIndex++;
                oWorkSheet.Cells[initTitleIndex, columnIndex] = "";
            }
            else if (daysInNow == 29)
            {
                columnIndex++;
                oWorkSheet.Cells[initTitleIndex, columnIndex + 1] = "";
                columnIndex++;
                oWorkSheet.Cells[initTitleIndex, columnIndex + 2] = "";
            }
            else if (daysInNow == 30)
            {
                columnIndex++;
                oWorkSheet.Cells[initTitleIndex, columnIndex + 1] = "";
            }

            oWorkSheet.Cells[initTitleIndex, columnIndex + 1] = "X";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 2] = "Ô\nCo\nKHHDS";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 3] = "TS";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 4] = "TNLD";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 5] = "DD";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 6] = "Fdb";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 7] = "Ho";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 8] = "F";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 9] = "Khác";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 10] = "NT";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 11] = "NB";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 12] = "Giờ\nĐêm";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 13] = "Điểm\nHTCV";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 14] = "Hệ Số\nK";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 15] = "Xếp\nLoại";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 16] = "Ghi Chú";

            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 1]).ColumnWidth = 3;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 1]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 2]).ColumnWidth = 3;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 2]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 2]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 3]).ColumnWidth = 3;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 3]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 3]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 4]).ColumnWidth = 3;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 4]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 4]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 5]).ColumnWidth = 3;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 5]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 5]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 6]).ColumnWidth = 3;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 6]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 6]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 7]).ColumnWidth = 3;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 7]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 7]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 8]).ColumnWidth = 3;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 8]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 8]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 9]).ColumnWidth = 3;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 9]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 9]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 10]).ColumnWidth = 3;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 10]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 10]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 11]).ColumnWidth = 3;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 11]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 11]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 12]).ColumnWidth = 3;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 12]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 12]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 13]).ColumnWidth = 3;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 13]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 13]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 16]).ColumnWidth = 50;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 16]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 16]).VerticalAlignment = XlVAlign.xlVAlignCenter;

            #endregion
        }


        public static string ExcelByFilterForHTCV(string fullName, string deptIds, int minMark, int maxMark, int month,
            int year, int rootTypeId, string pathName)
        {
            ///////////////////////////////////////////
            var dt = new WorkdayEmployeesDAL().GetAllByFilterHTCV(fullName, deptIds, minMark, maxMark, month, year);
            ///////////////////////////////////////////

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                _Application oExcel;
                _Workbook oWorkBook;
                _Worksheet oWorkSheet;

                var fileName = string.Empty;
                try
                {
                    GC.Collect();
                    oExcel = new Application();
                    oExcel.Visible = false;

                    // Get new workbook
                    oWorkBook = oExcel.Workbooks.Add(Missing.Value);
                    oWorkSheet = (_Worksheet) oWorkBook.Worksheets[1];

                    oWorkSheet.Name = "BangHTCV";

                    InsertDataToWorkSheetForHTCV(dt, ref oWorkSheet, month, year, rootTypeId);

                    oExcel.Visible = false;
                    oExcel.UserControl = false;
                    fileName = pathName + "\\BangHTCV_" + month + "_" + year + ".xls";
                    if (File.Exists(fileName))
                        File.Delete(fileName);
                    oWorkBook.SaveAs(fileName, XlFileFormat.xlWorkbookNormal, null, null, false, false,
                        XlSaveAsAccessMode.xlShared, false, false, null, null, null);

                    // Need all following code to clean up and extingush all references!!!
                    oWorkBook.Close(null, null, null);
                    oExcel.Workbooks.Close();
                    oExcel.Quit();
                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(oRange);
                    Marshal.ReleaseComObject(oExcel);
                    Marshal.ReleaseComObject(oWorkSheet);

                    Marshal.ReleaseComObject(oWorkBook);
                    oWorkSheet = null;
                    oWorkSheet = null;
                    oExcel = null;
                    GC.Collect(); // force final cleanup!

                    return fileName;
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

        private static void CreateHeaderAndTitleForHTCV(ref _Worksheet oWorkSheet, ref int initTitleIndex, int month,
            int year)
        {
            #region create header for printing from oWorkSheet

            var rangeHeader1 = oWorkSheet.get_Range("A1", "D1");
            rangeHeader1.Merge(Type.Missing);
            oWorkSheet.Cells[1, 1] = "TỔNG CÔNG TY CẢNG HÀNG KHÔNG MIỀN NAM ";
            rangeHeader1.Font.Size = 12;

            var rangeHeader2 = oWorkSheet.get_Range("A2", "D2");
            rangeHeader2.Merge(Type.Missing);
            oWorkSheet.Cells[2, 1] = "CÔNG TY PHỤC VỤ MẶT ĐẤT SÀI GÒN";
            rangeHeader2.Font.Size = 12;
            rangeHeader2.Font.Bold = true;

            var rangeHeader3 = oWorkSheet.get_Range("A4", "AI4");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "BẢNG CHẤM ĐIỂM HTCV THÁNG " + month + " NĂM " + year;
            rangeHeader3.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader3.Font.Size = 15;
            rangeHeader3.Font.Bold = true;

            #endregion

            #region Create Title for Account List from oWorkSheet

            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            oWorkSheet.Cells[initTitleIndex, 2] = "Họ Tên";
            ((Range) oWorkSheet.Cells[initTitleIndex, 2]).ColumnWidth = 30;
            ((Range) oWorkSheet.Cells[initTitleIndex, 2]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            oWorkSheet.Cells[initTitleIndex, 3] = "Chức danh";
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 20;
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            oWorkSheet.Cells[initTitleIndex, 4] = "Phòng";
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).ColumnWidth = 30;
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            oWorkSheet.Cells[initTitleIndex, 5] = "Điểm HTCV";
            oWorkSheet.Cells[initTitleIndex, 6] = "Xếp loại";
            oWorkSheet.Cells[initTitleIndex, 7] = "Tháng/năm";
            oWorkSheet.Cells[initTitleIndex, 8] = "Remark";
            ((Range) oWorkSheet.Cells[initTitleIndex, 8]).ColumnWidth = 60;
            ((Range) oWorkSheet.Cells[initTitleIndex, 8]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

            #endregion
        }

        private static void InsertDataToWorkSheetForHTCV(DataTable dt, ref _Worksheet oWorkSheet, int month, int year,
            int rootIdType)
        {
            var deparmentId = 0;
            var departmentIdBefore = 0;
            var initTitleIndex = 7;
            // create Content
            var indexRow = initTitleIndex + 1;
            var orderNumber = 1;
            var value = string.Empty;
            var deptIdKey = "";
            var deptNameKey = "";
            if (rootIdType == 0)
            {
                deptIdKey = DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID;
                deptNameKey = DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME;
            }
            else
            {
                deptIdKey = DepartmentKeys.FIELD_DEPARTMENT_ID;
                deptNameKey = DepartmentKeys.FIELD_DEPARTMENT_NAME;
            }

            CreateHeaderAndTitleForHTCV(ref oWorkSheet, ref initTitleIndex, month, year);

            #region Create FirstRow For Director, Administration and Training Department from oWorkSheet

            var rangeDept = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangeDept.Merge(Type.Missing);


            var dr0 = dt.Rows[0];

            oWorkSheet.Cells[indexRow, 1] = dr0[deptNameKey] == DBNull.Value
                ? string.Empty
                : dr0[deptNameKey].ToString().ToUpper();

            rangeDept.Font.Bold = true;


            indexRow++;

            var oUserId = dr0[EmployeeKeys.FIELD_EMPLOYEES_USERID] == DBNull.Value
                ? 0
                : int.Parse(dr0[EmployeeKeys.FIELD_EMPLOYEES_USERID].ToString());
            var oMark = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_Mark] == DBNull.Value
                ? 0
                : double.Parse(dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_Mark].ToString());
            var oWorkDayDate = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_WorkdayDate] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_WorkdayDate].ToString());
            var oRemark = dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_Remark] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeKeys.Field_WorkdayEmployee_Remark].ToString();
            var oAllRemark = HTCVEmployeeBLL.GetForAllRemarkByUserIdDate(oUserId, oWorkDayDate.Month, oWorkDayDate.Year);
            oAllRemark = oRemark + ";" + oAllRemark;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            oWorkSheet.Cells[indexRow, 2] = dr0[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                ? string.Empty
                : dr0[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();
            oWorkSheet.Cells[indexRow, 3] = dr0[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                ? string.Empty
                : dr0[PositionKeys.FIELD_POSITION_NAME].ToString();
            oWorkSheet.Cells[indexRow, 4] = dr0[DepartmentKeys.FIELD_DEPARTMENT_DEPARTMENTFULLNAME] == DBNull.Value
                ? string.Empty
                : dr0[DepartmentKeys.FIELD_DEPARTMENT_DEPARTMENTFULLNAME].ToString();
            oWorkSheet.Cells[indexRow, 5] = oMark;
            oWorkSheet.Cells[indexRow, 6] = DefaultValues.HTCV(oMark);
            oWorkSheet.Cells[indexRow, 7] = StringFormat.FormatMonthYearVN(oWorkDayDate);
            oWorkSheet.Cells[indexRow, 8] = oAllRemark;

            #endregion

            for (var i = 1; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                var dr_1 = dt.Rows[i - 1];
                var rootId = dr[deptIdKey] == DBNull.Value ? 0 : int.Parse(dr[deptIdKey].ToString());
                var rootId_1 = dr_1[deptIdKey] == DBNull.Value ? 0 : int.Parse(dr_1[deptIdKey].ToString());

                indexRow++;
                if ((i < dt.Rows.Count - 1) && (i > 1))
                {
                    deparmentId = rootId;
                    departmentIdBefore = rootId_1;
                }

                if (deparmentId != departmentIdBefore)
                {
                    rangeDept = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
                    rangeDept.Merge(Type.Missing);
                    oWorkSheet.Cells[indexRow, 1] = dr[deptNameKey] == DBNull.Value
                        ? string.Empty
                        : dr[deptNameKey].ToString().ToUpper();
                    rangeDept.Font.Bold = true;
                    indexRow++;
                }

                /// inserting order number
                var UserId = dr[EmployeeKeys.FIELD_EMPLOYEES_USERID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[EmployeeKeys.FIELD_EMPLOYEES_USERID].ToString());
                var Mark = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_Mark] == DBNull.Value
                    ? 0
                    : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_Mark].ToString());
                var WorkDayDate = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_WorkdayDate] == DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_WorkdayDate].ToString());
                var Remark = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_Remark] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_Remark].ToString();
                var AllRemark = HTCVEmployeeBLL.GetForAllRemarkByUserIdDate(UserId, WorkDayDate.Month, WorkDayDate.Year);
                AllRemark = Remark + ";" + AllRemark;
                oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                oWorkSheet.Cells[indexRow, 2] = dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();
                oWorkSheet.Cells[indexRow, 3] = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[PositionKeys.FIELD_POSITION_NAME].ToString();
                oWorkSheet.Cells[indexRow, 4] = dr[DepartmentKeys.FIELD_DEPARTMENT_DEPARTMENTFULLNAME] == DBNull.Value
                    ? string.Empty
                    : dr[DepartmentKeys.FIELD_DEPARTMENT_DEPARTMENTFULLNAME].ToString();
                oWorkSheet.Cells[indexRow, 5] = Mark;
                oWorkSheet.Cells[indexRow, 6] = DefaultValues.HTCV(Mark);
                oWorkSheet.Cells[indexRow, 7] = StringFormat.FormatMonthYearVN(WorkDayDate);
                oWorkSheet.Cells[indexRow, 8] = AllRemark;
            }
        }
    }
}