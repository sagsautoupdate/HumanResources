using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Excel;
using HRMDAL.H1;
using HRMUtil.KeyNames.H0;
using HRMUtil.KeyNames.H1;
using Constants = HRMUtil.Constants;
using DataTable = System.Data.DataTable;

namespace HRMBLL.H1.Helper
{
    public class WorkdayEmployeesBLLLExportData
    {
        public static string ExcelByFilter(string fullName, string departmentIds, int month, int year, string pathName)
        {
            ///////////////////////////////////////////
            var dt = new WorkdayEmployeesDALL().GetByFilter(fullName, departmentIds, month, year, 2);
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

                    oWorkSheet.Name = "BCCHuongLuong";

                    InsertDataToWorkSheet(dt, ref oWorkSheet, month, year);

                    oExcel.Visible = false;
                    oExcel.UserControl = false;
                    fileName = pathName + "\\BCCHuongLuong_" + month + "_" + year + ".xls";
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

        private static void InsertDataToWorkSheet(DataTable dt, ref _Worksheet oWorkSheet, int month, int year)
        {
            var sCN = "CN";
            var deparmentId = 0;
            var departmentIdBefore = 0;
            var initTitleIndex = 8;
            // create Content
            var indexRow = initTitleIndex + 1;
            var orderNumber = 1;
            var value = string.Empty;

            CreateHeaderAndTitle(ref oWorkSheet, ref initTitleIndex, month, year);

            #region Create FirstRow For Director, Administration and Training Department from oWorkSheet

            var rangeDept = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangeDept.Merge(Type.Missing);
            oWorkSheet.Cells[indexRow, 1] = "BAN GIÁM ĐỐC";
            rangeDept.Font.Bold = true;

            var dr0 = dt.Rows[0];

            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            oWorkSheet.Cells[indexRow, 2] = dr0[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE] == DBNull.Value
                ? string.Empty
                : "'" + dr0[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE];
            oWorkSheet.Cells[indexRow, 3] = dr0[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                ? string.Empty
                : dr0[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();
            oWorkSheet.Cells[indexRow, 4] = dr0[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                ? string.Empty
                : dr0[PositionKeys.FIELD_POSITION_NAME].ToString();

            oWorkSheet.Cells[indexRow, 5] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day1L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day1L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day1L].ToString();
            oWorkSheet.Cells[indexRow, 6] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day2L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day2L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day2L].ToString();
            oWorkSheet.Cells[indexRow, 7] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day3L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day3L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day3L].ToString();
            oWorkSheet.Cells[indexRow, 8] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day4L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day4L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day4L].ToString();
            oWorkSheet.Cells[indexRow, 9] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day5L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day5L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day5L].ToString();
            oWorkSheet.Cells[indexRow, 10] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day6L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day6L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day6L].ToString();
            oWorkSheet.Cells[indexRow, 11] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day7L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day7L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day7L].ToString();
            oWorkSheet.Cells[indexRow, 12] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day8L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day8L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day8L].ToString();
            oWorkSheet.Cells[indexRow, 13] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day9L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day9L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day9L].ToString();
            oWorkSheet.Cells[indexRow, 14] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day10L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day10L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day10L].ToString();


            oWorkSheet.Cells[indexRow, 15] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day11L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day11L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day11L].ToString();
            oWorkSheet.Cells[indexRow, 16] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day12L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day12L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day12L].ToString();
            oWorkSheet.Cells[indexRow, 17] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day13L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day13L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day13L].ToString();
            oWorkSheet.Cells[indexRow, 18] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day14L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day14L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day14L].ToString();
            oWorkSheet.Cells[indexRow, 19] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day15L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day15L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day15L].ToString();
            oWorkSheet.Cells[indexRow, 20] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day16L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day16L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day16L].ToString();
            oWorkSheet.Cells[indexRow, 21] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day17L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day17L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day17L].ToString();
            oWorkSheet.Cells[indexRow, 22] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day18L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day18L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day18L].ToString();
            oWorkSheet.Cells[indexRow, 23] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day19L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day19L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day19L].ToString();
            oWorkSheet.Cells[indexRow, 24] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day20L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day20L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day20L].ToString();

            oWorkSheet.Cells[indexRow, 25] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day21L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day21L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day21L].ToString();
            oWorkSheet.Cells[indexRow, 26] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day22L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day22L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day22L].ToString();
            oWorkSheet.Cells[indexRow, 27] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day23L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day23L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day23L].ToString();
            oWorkSheet.Cells[indexRow, 28] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day24L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day24L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day24L].ToString();
            oWorkSheet.Cells[indexRow, 29] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day25L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day25L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day25L].ToString();
            oWorkSheet.Cells[indexRow, 30] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day26L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day26L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day26L].ToString();
            oWorkSheet.Cells[indexRow, 31] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day27L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day27L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day27L].ToString();
            oWorkSheet.Cells[indexRow, 32] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day28L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day28L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day28L].ToString();
            oWorkSheet.Cells[indexRow, 33] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day29L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day29L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day29L].ToString();
            oWorkSheet.Cells[indexRow, 34] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day30L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day30L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day30L].ToString();

            oWorkSheet.Cells[indexRow, 35] = dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day31L] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day31L].ToString()
                    .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                    ? sCN
                    : dr0[WorkdayEmployeeLKeys.Field_Workday_Employee_Day31L].ToString();

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            var oXL = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_XL] == DBNull.Value
                ? string.Empty
                : double.Parse(dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_XL].ToString()) <= 0
                    ? string.Empty
                    : "+" + dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_XL];

            var oF_OmL = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_OmL] == DBNull.Value
                ? string.Empty
                : double.Parse(dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_OmL].ToString()) <= 0
                    ? string.Empty
                    : "+" + dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_OmL] + Constants.LEAVE_TYPE_O_BAN_THAN_CODE;
            var oF_OmDaiNgayL = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_OM_DaiNgayL] == DBNull.Value
                ? string.Empty
                : double.Parse(dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_OM_DaiNgayL].ToString()) <= 0
                    ? string.Empty
                    : "+" + dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_OM_DaiNgayL] +
                      Constants.LEAVE_TYPE_O_DAI_NGAY_CODE;
            var oF_ThaiSanL = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_ThaiSanL] == DBNull.Value
                ? string.Empty
                : double.Parse(dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_ThaiSanL].ToString()) <= 0
                    ? string.Empty
                    : "+" + dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_ThaiSanL] +
                      Constants.LEAVE_TYPE_THAI_SAN_CODE;
            var oF_TNLDL = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_TNLDL] == DBNull.Value
                ? string.Empty
                : double.Parse(dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_TNLDL].ToString()) <= 0
                    ? string.Empty
                    : "+" + dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_TNLDL] + Constants.LEAVE_TYPE_TNLD_CODE;
            var oF_NamL = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_NamL] == DBNull.Value
                ? string.Empty
                : double.Parse(dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_NamL].ToString()) <= 0
                    ? string.Empty
                    : "+" + dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_NamL] + Constants.LEAVE_TYPE_F_NAM_CODE;

            var oF_dbL = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_dbL] == DBNull.Value
                ? string.Empty
                : double.Parse(dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_dbL].ToString()) <= 0
                    ? string.Empty
                    : "+" + dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_dbL] + Constants.LEAVE_TYPE_FDB_CODE;
            var oF_KoLuongCLDL = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KoLuongCLDL] == DBNull.Value
                ? string.Empty
                : double.Parse(dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KoLuongCLDL].ToString()) <= 0
                    ? string.Empty
                    : "+" + dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KoLuongCLDL] +
                      Constants.LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO_CODE;
            var oF_KoLuongKLDL = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KoLuongKLDL] == DBNull.Value
                ? string.Empty
                : double.Parse(dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KoLuongKLDL].ToString()) <= 0
                    ? string.Empty
                    : "+" + dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KoLuongKLDL] +
                      Constants.LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO_CODE;
            var oF_DiDuongL = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_DiDuongL] == DBNull.Value
                ? string.Empty
                : double.Parse(dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_DiDuongL].ToString()) <= 0
                    ? string.Empty
                    : "+" + dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_DiDuongL] +
                      Constants.LEAVE_TYPE_F_DI_DUONG_CODE;
            var oF_CongTacL = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_CongTacL] == DBNull.Value
                ? string.Empty
                : double.Parse(dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_CongTacL].ToString()) <= 0
                    ? string.Empty
                    : "+" + dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_CongTacL] +
                      Constants.LEAVE_TYPE_F_CONG_TAC_CODE;

            var oF_HocSAGSL = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_HocSAGSL] == DBNull.Value
                ? string.Empty
                : double.Parse(dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_HocSAGSL].ToString()) <= 0
                    ? string.Empty
                    : "+" + dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_HocSAGSL] +
                      Constants.LEAVE_TYPE_HOC_SAGS_CODE;
            var oF_Hoc1L = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc1L] == DBNull.Value
                ? string.Empty
                : double.Parse(dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc1L].ToString()) <= 0
                    ? string.Empty
                    : "+" + dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc1L] + Constants.LEAVE_TYPE_HOC_1_CODE;
            var oF_Hoc2L = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc2L] == DBNull.Value
                ? string.Empty
                : double.Parse(dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc2L].ToString()) <= 0
                    ? string.Empty
                    : "+" + dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc2L] + Constants.LEAVE_TYPE_HOC_2_CODE;
            var oF_Hoc3L = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc3L] == DBNull.Value
                ? string.Empty
                : double.Parse(dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc3L].ToString()) <= 0
                    ? string.Empty
                    : "+" + dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc3L] + Constants.LEAVE_TYPE_HOC_3_CODE;
            var oF_Hoc4L = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc4L] == DBNull.Value
                ? string.Empty
                : double.Parse(dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc4L].ToString()) <= 0
                    ? string.Empty
                    : "+" + dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc4L] + Constants.LEAVE_TYPE_HOC_4_CODE;
            var oF_Hoc5L = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc5L] == DBNull.Value
                ? string.Empty
                : double.Parse(dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc5L].ToString()) <= 0
                    ? string.Empty
                    : "+" + dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc5L] + Constants.LEAVE_TYPE_HOC_5_CODE;
            var oF_Hoc6L = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc6L] == DBNull.Value
                ? string.Empty
                : double.Parse(dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc6L].ToString()) <= 0
                    ? string.Empty
                    : "+" + dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc6L] + Constants.LEAVE_TYPE_HOC_6_CODE;
            var oF_Hoc7L = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc7L] == DBNull.Value
                ? string.Empty
                : double.Parse(dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc7L].ToString()) <= 0
                    ? string.Empty
                    : "+" + dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc7L] + Constants.LEAVE_TYPE_HOC_7_CODE;


            var oF_Con_OmL = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Con_OmL] == DBNull.Value
                ? string.Empty
                : double.Parse(dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Con_OmL].ToString()) <= 0
                    ? string.Empty
                    : "+" + dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Con_OmL] + Constants.LEAVE_TYPE_CON_OM_CODE;
            var oF_KHHDSL = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KHHDSL] == DBNull.Value
                ? string.Empty
                : double.Parse(dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KHHDSL].ToString()) <= 0
                    ? string.Empty
                    : "+" + dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KHHDSL] + Constants.LEAVE_TYPE_KHHDS_CODE;
            var oF_SayThaiL = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_SayThaiL] == DBNull.Value
                ? string.Empty
                : double.Parse(dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_SayThaiL].ToString()) <= 0
                    ? string.Empty
                    : "+" + dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_SayThaiL] +
                      Constants.LEAVE_TYPE_SAY_THAI_CODE;
            var oF_KhamThaiL = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KhamThaiL] == DBNull.Value
                ? string.Empty
                : double.Parse(dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KhamThaiL].ToString()) <= 0
                    ? string.Empty
                    : "+" + dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KhamThaiL] +
                      Constants.LEAVE_TYPE_KHAM_THAI_CODE;
            var oF_ConChetL = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_ConChetL] == DBNull.Value
                ? string.Empty
                : double.Parse(dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_ConChetL].ToString()) <= 0
                    ? string.Empty
                    : "+" + dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_ConChetL] +
                      Constants.LEAVE_TYPE_CON_CHET_SAU_KHI_SINH_CODE;
            var oF_DinhChiCongTacL = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_DinhChiCongTacL] == DBNull.Value
                ? string.Empty
                : double.Parse(dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_DinhChiCongTacL].ToString()) <= 0
                    ? string.Empty
                    : "+" + dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_DinhChiCongTacL] +
                      Constants.LEAVE_TYPE_DINH_CHI_CONG_TAC_CODE;

            var oF_TamHoanHDL = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_TamHoanHDL] == DBNull.Value
                ? string.Empty
                : double.Parse(dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_TamHoanHDL].ToString()) <= 0
                    ? string.Empty
                    : "+" + dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_TamHoanHDL] +
                      Constants.LEAVE_TYPE_TAM_HOAN_HOP_DONG_CODE;
            var oF_HoiHopL = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_HoiHopL] == DBNull.Value
                ? string.Empty
                : double.Parse(dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_HoiHopL].ToString()) <= 0
                    ? string.Empty
                    : "+" + dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_HoiHopL] +
                      Constants.LEAVE_TYPE_HOI_HOP_CODE;

            var oCongHuongLuong = oXL + oF_HocSAGSL + oF_Hoc1L + oF_Hoc2L + oF_Hoc3L + oF_Hoc4L + oF_Hoc5L + oF_Hoc6L +
                                  oF_Hoc7L + oF_CongTacL + oF_DinhChiCongTacL + oF_TamHoanHDL + oF_HoiHopL;
            var oCongHuongBHXH = oF_OmL + oF_OmDaiNgayL + oF_ThaiSanL + oF_TNLDL + oF_NamL + oF_dbL + oF_DiDuongL +
                                 oF_Con_OmL + oF_KHHDSL + oF_SayThaiL + oF_KhamThaiL + oF_ConChetL;
            var oNghikhac = oF_KoLuongCLDL + oF_KoLuongKLDL;
            oWorkSheet.Cells[indexRow, 36] = oCongHuongLuong.Length <= 0
                ? string.Empty
                : oCongHuongLuong.Substring(1, oCongHuongLuong.Length - 1);
            oWorkSheet.Cells[indexRow, 37] = oCongHuongBHXH.Length <= 0
                ? string.Empty
                : oCongHuongBHXH.Substring(1, oCongHuongBHXH.Length - 1);
            oWorkSheet.Cells[indexRow, 38] = oNghikhac.Length <= 0
                ? string.Empty
                : oNghikhac.Substring(1, oNghikhac.Length - 1);
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            oWorkSheet.Cells[indexRow, 39] = string.Empty;
            oWorkSheet.Cells[indexRow, 40] = string.Empty;
            oWorkSheet.Cells[indexRow, 41] = string.Empty;
            oWorkSheet.Cells[indexRow, 42] = string.Empty;

            var nightTime0 = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_NightTimeL] == DBNull.Value
                ? 0
                : double.Parse(dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_NightTimeL].ToString());
            oWorkSheet.Cells[indexRow, 43] = nightTime0 <= 0 ? string.Empty : nightTime0.ToString();

            var markL0 = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_MarkL] == DBNull.Value
                ? 0
                : double.Parse(dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_MarkL].ToString());
            oWorkSheet.Cells[indexRow, 44] = markL0;
            oWorkSheet.Cells[indexRow, 45] = markL0/100;
            oWorkSheet.Cells[indexRow, 46] = DefaultValues.HTCV(markL0);

            oWorkSheet.Cells[indexRow, 47] = dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_RemarkL] == DBNull.Value
                ? string.Empty
                : dr0[WorkdayEmployeeLKeys.Field_WorkdayEmployee_RemarkL].ToString();

            #endregion

            for (var i = 1; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                var dr_1 = dt.Rows[i - 1];
                var rootId = dr[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID].ToString());
                var rootId_1 = dr_1[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr_1[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID].ToString());


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
                    oWorkSheet.Cells[indexRow, 1] = dr[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME] == DBNull.Value
                        ? string.Empty
                        : dr[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME].ToString().ToUpper();
                    rangeDept.Font.Bold = true;
                    indexRow++;
                }

                oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                oWorkSheet.Cells[indexRow, 2] = dr[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE] == DBNull.Value
                    ? string.Empty
                    : "'" + dr[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE];
                oWorkSheet.Cells[indexRow, 3] = dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();
                oWorkSheet.Cells[indexRow, 4] = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[PositionKeys.FIELD_POSITION_NAME].ToString();

                oWorkSheet.Cells[indexRow, 5] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day1L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day1L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day1L].ToString();
                oWorkSheet.Cells[indexRow, 6] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day2L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day2L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day2L].ToString();
                oWorkSheet.Cells[indexRow, 7] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day3L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day3L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day3L].ToString();
                oWorkSheet.Cells[indexRow, 8] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day4L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day4L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day4L].ToString();
                oWorkSheet.Cells[indexRow, 9] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day5L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day5L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day5L].ToString();
                oWorkSheet.Cells[indexRow, 10] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day6L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day6L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day6L].ToString();
                oWorkSheet.Cells[indexRow, 11] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day7L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day7L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day7L].ToString();
                oWorkSheet.Cells[indexRow, 12] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day8L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day8L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day8L].ToString();
                oWorkSheet.Cells[indexRow, 13] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day9L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day9L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day9L].ToString();
                oWorkSheet.Cells[indexRow, 14] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day10L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day10L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day10L].ToString();


                oWorkSheet.Cells[indexRow, 15] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day11L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day11L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day11L].ToString();
                oWorkSheet.Cells[indexRow, 16] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day12L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day12L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day12L].ToString();
                oWorkSheet.Cells[indexRow, 17] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day13L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day13L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day13L].ToString();
                oWorkSheet.Cells[indexRow, 18] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day14L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day14L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day14L].ToString();
                oWorkSheet.Cells[indexRow, 19] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day15L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day15L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day15L].ToString();
                oWorkSheet.Cells[indexRow, 20] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day16L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day16L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day16L].ToString();
                oWorkSheet.Cells[indexRow, 21] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day17L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day17L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day17L].ToString();
                oWorkSheet.Cells[indexRow, 22] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day18L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day18L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day18L].ToString();
                oWorkSheet.Cells[indexRow, 23] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day19L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day19L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day19L].ToString();
                oWorkSheet.Cells[indexRow, 24] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day20L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day20L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day20L].ToString();

                oWorkSheet.Cells[indexRow, 25] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day21L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day21L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day21L].ToString();
                oWorkSheet.Cells[indexRow, 26] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day22L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day22L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day22L].ToString();
                oWorkSheet.Cells[indexRow, 27] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day23L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day23L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day23L].ToString();
                oWorkSheet.Cells[indexRow, 28] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day24L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day24L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day24L].ToString();
                oWorkSheet.Cells[indexRow, 29] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day25L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day25L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day25L].ToString();
                oWorkSheet.Cells[indexRow, 30] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day26L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day26L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day26L].ToString();
                oWorkSheet.Cells[indexRow, 31] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day27L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day27L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day27L].ToString();
                oWorkSheet.Cells[indexRow, 32] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day28L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day28L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day28L].ToString();
                oWorkSheet.Cells[indexRow, 33] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day29L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day29L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day29L].ToString();
                oWorkSheet.Cells[indexRow, 34] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day30L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day30L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day30L].ToString();

                oWorkSheet.Cells[indexRow, 35] = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day31L] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day31L].ToString()
                        .Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                        ? sCN
                        : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day31L].ToString();

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                var XL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_XL] == DBNull.Value
                    ? string.Empty
                    : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_XL].ToString()) <= 0
                        ? string.Empty
                        : "+" + dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_XL];

                var F_OmL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_OmL] == DBNull.Value
                    ? string.Empty
                    : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_OmL].ToString()) <= 0
                        ? string.Empty
                        : "+" + dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_OmL] +
                          Constants.LEAVE_TYPE_O_BAN_THAN_CODE;
                var F_OmDaiNgayL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_OM_DaiNgayL] == DBNull.Value
                    ? string.Empty
                    : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_OM_DaiNgayL].ToString()) <= 0
                        ? string.Empty
                        : "+" + dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_OM_DaiNgayL] +
                          Constants.LEAVE_TYPE_O_DAI_NGAY_CODE;
                var F_ThaiSanL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_ThaiSanL] == DBNull.Value
                    ? string.Empty
                    : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_ThaiSanL].ToString()) <= 0
                        ? string.Empty
                        : "+" + dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_ThaiSanL] +
                          Constants.LEAVE_TYPE_THAI_SAN_CODE;
                var F_TNLDL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_TNLDL] == DBNull.Value
                    ? string.Empty
                    : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_TNLDL].ToString()) <= 0
                        ? string.Empty
                        : "+" + dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_TNLDL] + Constants.LEAVE_TYPE_TNLD_CODE;
                var F_NamL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_NamL] == DBNull.Value
                    ? string.Empty
                    : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_NamL].ToString()) <= 0
                        ? string.Empty
                        : "+" + dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_NamL] + Constants.LEAVE_TYPE_F_NAM_CODE;

                var F_dbL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_dbL] == DBNull.Value
                    ? string.Empty
                    : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_dbL].ToString()) <= 0
                        ? string.Empty
                        : "+" + dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_dbL] + Constants.LEAVE_TYPE_FDB_CODE;
                var F_KoLuongCLDL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KoLuongCLDL] == DBNull.Value
                    ? string.Empty
                    : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KoLuongCLDL].ToString()) <= 0
                        ? string.Empty
                        : "+" + dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KoLuongCLDL] +
                          Constants.LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO_CODE;
                var F_KoLuongKLDL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KoLuongKLDL] == DBNull.Value
                    ? string.Empty
                    : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KoLuongKLDL].ToString()) <= 0
                        ? string.Empty
                        : "+" + dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KoLuongKLDL] +
                          Constants.LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO_CODE;
                var F_DiDuongL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_DiDuongL] == DBNull.Value
                    ? string.Empty
                    : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_DiDuongL].ToString()) <= 0
                        ? string.Empty
                        : "+" + dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_DiDuongL] +
                          Constants.LEAVE_TYPE_F_DI_DUONG_CODE;
                var F_CongTacL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_CongTacL] == DBNull.Value
                    ? string.Empty
                    : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_CongTacL].ToString()) <= 0
                        ? string.Empty
                        : "+" + dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_CongTacL] +
                          Constants.LEAVE_TYPE_F_CONG_TAC_CODE;

                var F_HocSAGSL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_HocSAGSL] == DBNull.Value
                    ? string.Empty
                    : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_HocSAGSL].ToString()) <= 0
                        ? string.Empty
                        : "+" + dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_HocSAGSL] +
                          Constants.LEAVE_TYPE_HOC_SAGS_CODE;
                var F_Hoc1L = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc1L] == DBNull.Value
                    ? string.Empty
                    : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc1L].ToString()) <= 0
                        ? string.Empty
                        : "+" + dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc1L] + Constants.LEAVE_TYPE_HOC_1_CODE;
                var F_Hoc2L = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc2L] == DBNull.Value
                    ? string.Empty
                    : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc2L].ToString()) <= 0
                        ? string.Empty
                        : "+" + dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc2L] + Constants.LEAVE_TYPE_HOC_2_CODE;
                var F_Hoc3L = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc3L] == DBNull.Value
                    ? string.Empty
                    : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc3L].ToString()) <= 0
                        ? string.Empty
                        : "+" + dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc3L] + Constants.LEAVE_TYPE_HOC_3_CODE;
                var F_Hoc4L = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc4L] == DBNull.Value
                    ? string.Empty
                    : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc4L].ToString()) <= 0
                        ? string.Empty
                        : "+" + dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc4L] + Constants.LEAVE_TYPE_HOC_4_CODE;
                var F_Hoc5L = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc5L] == DBNull.Value
                    ? string.Empty
                    : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc5L].ToString()) <= 0
                        ? string.Empty
                        : "+" + dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc5L] + Constants.LEAVE_TYPE_HOC_5_CODE;
                var F_Hoc6L = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc6L] == DBNull.Value
                    ? string.Empty
                    : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc6L].ToString()) <= 0
                        ? string.Empty
                        : "+" + dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc6L] + Constants.LEAVE_TYPE_HOC_6_CODE;
                var F_Hoc7L = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc7L] == DBNull.Value
                    ? string.Empty
                    : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc7L].ToString()) <= 0
                        ? string.Empty
                        : "+" + dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc7L] + Constants.LEAVE_TYPE_HOC_7_CODE;


                var F_Con_OmL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Con_OmL] == DBNull.Value
                    ? string.Empty
                    : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Con_OmL].ToString()) <= 0
                        ? string.Empty
                        : "+" + dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Con_OmL] +
                          Constants.LEAVE_TYPE_CON_OM_CODE;
                var F_KHHDSL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KHHDSL] == DBNull.Value
                    ? string.Empty
                    : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KHHDSL].ToString()) <= 0
                        ? string.Empty
                        : "+" + dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KHHDSL] +
                          Constants.LEAVE_TYPE_KHHDS_CODE;
                var F_SayThaiL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_SayThaiL] == DBNull.Value
                    ? string.Empty
                    : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_SayThaiL].ToString()) <= 0
                        ? string.Empty
                        : "+" + dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_SayThaiL] +
                          Constants.LEAVE_TYPE_SAY_THAI_CODE;
                var F_KhamThaiL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KhamThaiL] == DBNull.Value
                    ? string.Empty
                    : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KhamThaiL].ToString()) <= 0
                        ? string.Empty
                        : "+" + dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KhamThaiL] +
                          Constants.LEAVE_TYPE_KHAM_THAI_CODE;
                var F_ConChetL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_ConChetL] == DBNull.Value
                    ? string.Empty
                    : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_ConChetL].ToString()) <= 0
                        ? string.Empty
                        : "+" + dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_ConChetL] +
                          Constants.LEAVE_TYPE_CON_CHET_SAU_KHI_SINH_CODE;
                var F_DinhChiCongTacL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_DinhChiCongTacL] == DBNull.Value
                    ? string.Empty
                    : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_DinhChiCongTacL].ToString()) <= 0
                        ? string.Empty
                        : "+" + dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_DinhChiCongTacL] +
                          Constants.LEAVE_TYPE_DINH_CHI_CONG_TAC_CODE;

                var F_TamHoanHDL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_TamHoanHDL] == DBNull.Value
                    ? string.Empty
                    : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_TamHoanHDL].ToString()) <= 0
                        ? string.Empty
                        : "+" + dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_TamHoanHDL] +
                          Constants.LEAVE_TYPE_TAM_HOAN_HOP_DONG_CODE;
                var F_HoiHopL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_HoiHopL] == DBNull.Value
                    ? string.Empty
                    : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_HoiHopL].ToString()) <= 0
                        ? string.Empty
                        : "+" + dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_HoiHopL] +
                          Constants.LEAVE_TYPE_HOI_HOP_CODE;

                var CongHuongLuong = XL + F_HocSAGSL + F_Hoc1L + F_Hoc2L + F_Hoc3L + F_Hoc4L + F_Hoc5L + F_Hoc6L +
                                     F_Hoc7L + F_CongTacL + F_DinhChiCongTacL + F_TamHoanHDL + F_HoiHopL;
                var CongHuongBHXH = F_OmL + F_OmDaiNgayL + F_ThaiSanL + F_TNLDL + F_NamL + F_dbL + F_DiDuongL +
                                    F_Con_OmL + F_KHHDSL + F_SayThaiL + F_KhamThaiL + F_ConChetL;
                var Nghikhac = F_KoLuongCLDL + F_KoLuongKLDL;

                oWorkSheet.Cells[indexRow, 36] = CongHuongLuong.Length <= 0
                    ? string.Empty
                    : CongHuongLuong.Substring(1, CongHuongLuong.Length - 1);
                oWorkSheet.Cells[indexRow, 37] = CongHuongBHXH.Length <= 0
                    ? string.Empty
                    : CongHuongBHXH.Substring(1, CongHuongBHXH.Length - 1);
                oWorkSheet.Cells[indexRow, 38] = Nghikhac.Length <= 0
                    ? string.Empty
                    : Nghikhac.Substring(1, Nghikhac.Length - 1);
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                oWorkSheet.Cells[indexRow, 39] = string.Empty;
                oWorkSheet.Cells[indexRow, 40] = string.Empty;
                oWorkSheet.Cells[indexRow, 41] = string.Empty;
                oWorkSheet.Cells[indexRow, 42] = string.Empty;

                var nightTime = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_NightTimeL] == DBNull.Value
                    ? 0
                    : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_NightTimeL].ToString());
                oWorkSheet.Cells[indexRow, 43] = nightTime <= 0 ? string.Empty : nightTime.ToString();

                var markL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_MarkL] == DBNull.Value
                    ? 0
                    : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_MarkL].ToString());
                oWorkSheet.Cells[indexRow, 44] = markL;
                oWorkSheet.Cells[indexRow, 45] = markL/100;
                oWorkSheet.Cells[indexRow, 46] = DefaultValues.HTCV(markL);
                oWorkSheet.Cells[indexRow, 47] = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_RemarkL] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_RemarkL].ToString();
            }

            var rangeName = oWorkSheet.get_Range("A7", "D" + indexRow);
            rangeName.Font.Size = 10;
            rangeName.Font.Name = "Times New Roman";


            var rangeSTT = oWorkSheet.get_Range("A7", "B" + indexRow);
            rangeSTT.HorizontalAlignment = XlHAlign.xlHAlignCenter;


            var range = oWorkSheet.get_Range("E7", "AU" + indexRow);
            range.Font.Size = 10;
            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            range.Font.Name = "Times New Roman";

            #region Create footer

            //indexRow = indexRow + 2;
            //Range rangeFooter1 = oWorkSheet.get_Range(oWorkSheet.Cells[indexRow, 4], oWorkSheet.Cells[indexRow, 5]);
            //rangeFooter1.Merge(Type.Missing);
            //oWorkSheet.Cells[indexRow, 4] = "Ngày       tháng      năm " + DateTime.Now.Year;
            //rangeFooter1.Font.Name = "Times New Roman";
            //rangeFooter1.Font.Size = 12;
            //rangeFooter1.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            //indexRow = indexRow + 1;
            //Range rangeFooter2 = oWorkSheet.get_Range(oWorkSheet.Cells[indexRow, 4], oWorkSheet.Cells[indexRow, 5]);
            //rangeFooter2.Merge(Type.Missing);
            //oWorkSheet.Cells[indexRow, 4] = "GIÁM ĐỐC";
            //rangeFooter2.Font.Name = "Times New Roman";
            //rangeFooter2.Font.Size = 12;
            //rangeFooter2.Font.Bold = true;
            //rangeFooter2.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            //indexRow = indexRow + 5;
            //Range rangeFooter3 = oWorkSheet.get_Range(oWorkSheet.Cells[indexRow, 4], oWorkSheet.Cells[indexRow, 5]);
            //rangeFooter3.Merge(Type.Missing);
            //oWorkSheet.Cells[indexRow, 4] = "NGUYỄN ĐÌNH HÙNG";
            //rangeFooter3.Font.Name = "Times New Roman";
            //rangeFooter3.Font.Size = 12;
            //rangeFooter3.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            #endregion

            // release range object            
        }

        private static void CreateHeaderAndTitle(ref _Worksheet oWorkSheet, ref int initTitleIndex, int month, int year)
        {
            #region create header for printing from oWorkSheet

            var rangeHeader1 = oWorkSheet.get_Range("B1", "F1");
            rangeHeader1.Merge(Type.Missing);
            oWorkSheet.Cells[1, 2] = "TỔNG CÔNG TY CẢNG HÀNG KHÔNG MIỀN NAM ";
            rangeHeader1.Font.Size = 12;
            rangeHeader1.Font.Bold = true;
            rangeHeader1.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader1.Font.Name = "Times New Roman";

            var rangeHeader2 = oWorkSheet.get_Range("B2", "F2");
            rangeHeader2.Merge(Type.Missing);
            oWorkSheet.Cells[2, 2] = "CÔNG TY PHỤC VỤ MẶT ĐẤT SÀI GÒN";
            rangeHeader2.Font.Size = 12;
            rangeHeader2.Font.Bold = true;
            rangeHeader2.Font.Underline = true;
            rangeHeader2.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader2.Font.Name = "Times New Roman";

            var rangeHeader3 = oWorkSheet.get_Range("A4", "AS4");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "BẢNG CHẤM CÔNG THÁNG " + month + " NĂM " + year;
            rangeHeader3.Font.Size = 16;
            rangeHeader3.Font.Bold = true;
            rangeHeader3.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader3.Font.Name = "Times New Roman";

            var rangeHeader31 = oWorkSheet.get_Range("A5", "AS5");
            rangeHeader31.Merge(Type.Missing);
            oWorkSheet.Cells[5, 1] = "Từ ngày 01 tháng " + month + " năm " + year + "  Đến ngày " +
                                     DateTime.DaysInMonth(year, month) + " tháng " + month + " năm " + year;
            rangeHeader31.Font.Size = 10;
            rangeHeader31.Font.Bold = true;
            rangeHeader31.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader31.Font.Name = "Times New Roman";

            var rangeHeader4 = oWorkSheet.get_Range("E7", "AI7");
            rangeHeader4.Merge(Type.Missing);
            oWorkSheet.Cells[7, 5] = "NGÀY CÔNG QUY ĐỊNH TRONG THÁNG";
            rangeHeader4.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader4.Font.Size = 10;
            rangeHeader4.Font.Bold = true;
            rangeHeader4.Font.Name = "Times New Roman";

            var rangeHeader41 = oWorkSheet.get_Range("AJ7", "AL7");
            rangeHeader41.Merge(Type.Missing);
            oWorkSheet.Cells[7, 36] = "QUY RA CÔNG";
            rangeHeader41.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader41.Font.Size = 10;
            rangeHeader41.Font.Name = "Times New Roman";

            var rangeHeader42 = oWorkSheet.get_Range("AM7", "AP7");
            rangeHeader42.Merge(Type.Missing);
            oWorkSheet.Cells[7, 39] = "GiỜ LÀM THÊM";
            rangeHeader42.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader42.Font.Size = 10;
            rangeHeader42.Font.Name = "Times New Roman";

            #endregion

            #region Create Title for Account List from oWorkSheet

            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            oWorkSheet.Cells[initTitleIndex, 2] = "Mã SAA";
            oWorkSheet.Cells[initTitleIndex, 3] = "Họ Tên";
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 30;
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
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
            oWorkSheet.Cells[initTitleIndex, columnIndex + 1] = "Công\nhưởng\nlương";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 2] = "Công\nhưởng\nBHXH";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 3] = "Nghỉ\nkhác";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 4] = "Ngày\nthường";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 5] = "Ngày\nnghỉ";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 6] = "Lễ\nTết";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 7] = "Đêm\n21h - 5h";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 8] = "Giờ làm\nđêm";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 9] = "Điểm\nHTCV";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 10] = "Hệ Số\nK";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 11] = "Xếp\nLoại";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 12] = "Ghi chú";

            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 1]).ColumnWidth = 10;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 1]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 2]).ColumnWidth = 10;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 2]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 2]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 3]).ColumnWidth = 6;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 3]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 3]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 4]).ColumnWidth = 6;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 4]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 4]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 5]).ColumnWidth = 6;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 5]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 5]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 6]).ColumnWidth = 6;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 6]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 6]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 7]).ColumnWidth = 6;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 7]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 7]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 8]).ColumnWidth = 6;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 8]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 8]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 9]).ColumnWidth = 6;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 9]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 9]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 10]).ColumnWidth = 6;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 10]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 10]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 11]).ColumnWidth = 6;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 11]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 11]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 12]).ColumnWidth = 10;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 12]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 12]).VerticalAlignment = XlVAlign.xlVAlignCenter;

            #endregion
        }
    }
}