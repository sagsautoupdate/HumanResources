using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Excel;
using HRMDAL.H1;
using HRMUtil;
using HRMUtil.KeyNames.H0;
using HRMUtil.KeyNames.H1;
using Constants = HRMUtil.Constants;
using DataTable = System.Data.DataTable;

namespace HRMBLL.H1.Helper
{
    public class LCBCoefficientEmployeeBLLExportData
    {
        public static string ExcelByFilter(string fullName, int deptId, int day, int month, int year, string pathName)
        {
            ///////////////////////////////////////////
            var dt = new LCBCoefficientEmployeesDAL().RemindLCBCoefficient(fullName, deptId, day, month, year);
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

                    oWorkSheet.Name = "LCBDenHan";

                    InsertDataToWorkSheet(dt, ref oWorkSheet, month, year);

                    oExcel.Visible = false;
                    oExcel.UserControl = false;
                    fileName = pathName + "\\HeSoLCBDenHan_" + month + "_" + year + ".xls";
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
            var deparmentId = 0;
            var departmentIdBefore = 0;
            var initTitleIndex = 7;
            // create Content
            var indexRow = initTitleIndex + 1;
            var orderNumber = 1;
            var value = string.Empty;

            CreateHeaderAndTitle(ref oWorkSheet, ref initTitleIndex, month, year);

            #region Create FirstRow For Director, Administration and Training Department from oWorkSheet

            var dr0 = dt.Rows[0];

            var rangeDept = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangeDept.Merge(Type.Missing);
            oWorkSheet.Cells[indexRow, 1] = dr0[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME] == DBNull.Value
                ? string.Empty
                : dr0[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME].ToString().ToUpper();
            rangeDept.Font.Bold = true;


            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            oWorkSheet.Cells[indexRow, 2] = dr0[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE] == DBNull.Value
                ? string.Empty
                : dr0[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE].ToString();
            var userid0 = dr0[EmployeeKeys.FIELD_EMPLOYEES_USERID] == DBNull.Value
                ? 0
                : int.Parse(dr0[EmployeeKeys.FIELD_EMPLOYEES_USERID].ToString());
            oWorkSheet.Cells[indexRow, 3] = StringFormat.GetUserCode(userid0);
            oWorkSheet.Cells[indexRow, 4] = dr0[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                ? string.Empty
                : dr0[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();
            oWorkSheet.Cells[indexRow, 5] = dr0[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                ? string.Empty
                : dr0[PositionKeys.FIELD_POSITION_NAME].ToString();
            oWorkSheet.Cells[indexRow, 6] = dr0[ContractTypeKeys.Field_ContractTypes_ContractTypeCode] == DBNull.Value
                ? string.Empty
                : dr0[ContractTypeKeys.Field_ContractTypes_ContractTypeCode].ToString();

            oWorkSheet.Cells[indexRow, 7] = dr0[CoefficientNameKeys.Field_CoefficientNames_CoefficientName] ==
                                            DBNull.Value
                ? string.Empty
                : dr0[CoefficientNameKeys.Field_CoefficientNames_CoefficientName].ToString();
            oWorkSheet.Cells[indexRow, 8] = dr0[CoefficientLevelKeys.FIELD_COEFFICIENT_LEVEL_NAME] == DBNull.Value
                ? string.Empty
                : dr0[CoefficientLevelKeys.FIELD_COEFFICIENT_LEVEL_NAME].ToString();
            oWorkSheet.Cells[indexRow, 9] = dr0[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE] == DBNull.Value
                ? string.Empty
                : dr0[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE].ToString();
            var _LCBUnit = dr0[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_LCBUnit] == DBNull.Value
                ? 0
                : int.Parse(dr0[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_LCBUnit].ToString());
            var _LCBUnitName = Constants.GetUnitNameById(_LCBUnit);
            var _LCBWages = dr0[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_LCBWages] == DBNull.Value
                ? 0
                : double.Parse(dr0[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_LCBWages].ToString());
            oWorkSheet.Cells[indexRow, 10] = _LCBWages + "" + _LCBUnitName;
            var _FromDate = dr0[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_FromDate] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr0[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_FromDate].ToString());
            oWorkSheet.Cells[indexRow, 11] = _FromDate.Equals(FormatDate.GetSQLDateMinValue)
                ? ""
                : _FromDate.ToString("dd/MM/yyyy");
            var _ToDate = dr0[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_ToDate] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr0[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_ToDate].ToString());
            oWorkSheet.Cells[indexRow, 12] = _ToDate.Equals(FormatDate.GetSQLDateMinValue)
                ? ""
                : _ToDate.ToString("dd/MM/yyyy");


            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
                    : dr[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE].ToString();
                var userid = dr[EmployeeKeys.FIELD_EMPLOYEES_USERID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[EmployeeKeys.FIELD_EMPLOYEES_USERID].ToString());
                oWorkSheet.Cells[indexRow, 3] = StringFormat.GetUserCode(userid);
                oWorkSheet.Cells[indexRow, 4] = dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();
                oWorkSheet.Cells[indexRow, 5] = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[PositionKeys.FIELD_POSITION_NAME].ToString();
                oWorkSheet.Cells[indexRow, 6] = dr[ContractTypeKeys.Field_ContractTypes_ContractTypeCode] ==
                                                DBNull.Value
                    ? string.Empty
                    : dr[ContractTypeKeys.Field_ContractTypes_ContractTypeCode].ToString();

                oWorkSheet.Cells[indexRow, 7] = dr[CoefficientNameKeys.Field_CoefficientNames_CoefficientName] ==
                                                DBNull.Value
                    ? string.Empty
                    : dr[CoefficientNameKeys.Field_CoefficientNames_CoefficientName].ToString();
                oWorkSheet.Cells[indexRow, 8] = dr[CoefficientLevelKeys.FIELD_COEFFICIENT_LEVEL_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[CoefficientLevelKeys.FIELD_COEFFICIENT_LEVEL_NAME].ToString();
                oWorkSheet.Cells[indexRow, 9] = dr[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE] == DBNull.Value
                    ? string.Empty
                    : dr[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE].ToString();
                var LCBUnit = dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_LCBUnit] == DBNull.Value
                    ? 0
                    : int.Parse(dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_LCBUnit].ToString());
                var LCBUnitName = Constants.GetUnitNameById(LCBUnit);
                var LCBWages = dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_LCBWages] == DBNull.Value
                    ? 0
                    : double.Parse(dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_LCBWages].ToString());
                oWorkSheet.Cells[indexRow, 10] = LCBWages + "" + LCBUnitName;

                var FromDate = dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_FromDate] == DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(
                        dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_FromDate].ToString());
                oWorkSheet.Cells[indexRow, 11] = FromDate.Equals(FormatDate.GetSQLDateMinValue)
                    ? ""
                    : FromDate.ToString("dd/MM/yyyy");
                var ToDate = dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_ToDate] == DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(
                        dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_ToDate].ToString());
                oWorkSheet.Cells[indexRow, 12] = ToDate.Equals(FormatDate.GetSQLDateMinValue)
                    ? ""
                    : ToDate.ToString("dd/MM/yyyy");
            }

            var rangeSTT = oWorkSheet.get_Range("A7", "C" + indexRow);
            rangeSTT.Font.Size = 10;
            rangeSTT.Font.Name = "Times New Roman";
            rangeSTT.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            var rangeName = oWorkSheet.get_Range("D7", "D" + indexRow);
            rangeName.Font.Size = 10;
            rangeName.Font.Name = "Times New Roman";
            rangeSTT.HorizontalAlignment = XlHAlign.xlHAlignLeft;

            var range = oWorkSheet.get_Range("E7", "AU" + indexRow);
            range.Font.Size = 10;
            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            range.Font.Name = "Times New Roman";
        }

        private static void CreateHeaderAndTitle(ref _Worksheet oWorkSheet, ref int initTitleIndex, int month, int year)
        {
            #region create header for printing from oWorkSheet

            var rangeHeader1 = oWorkSheet.get_Range("A1", "D1");
            rangeHeader1.Merge(Type.Missing);
            oWorkSheet.Cells[1, 1] = "TỔNG CÔNG TY CẢNG HÀNG KHÔNG MIỀN NAM ";
            rangeHeader1.Font.Size = 12;
            rangeHeader1.Font.Bold = true;
            rangeHeader1.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader1.Font.Name = "Times New Roman";

            var rangeHeader2 = oWorkSheet.get_Range("A2", "D2");
            rangeHeader2.Merge(Type.Missing);
            oWorkSheet.Cells[2, 1] = "CÔNG TY PHỤC VỤ MẶT ĐẤT SÀI GÒN";
            rangeHeader2.Font.Size = 12;
            rangeHeader2.Font.Bold = true;
            rangeHeader2.Font.Underline = true;
            rangeHeader2.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader2.Font.Name = "Times New Roman";

            var rangeHeader3 = oWorkSheet.get_Range("A4", "H4");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "BẢNG HỆ SỐ LƯƠNG CƠ BẢN ĐẾN HẠN CHUYỂN ĐỔI TRONG THÁNG " + month + "/" + year;
            rangeHeader3.Font.Size = 16;
            rangeHeader3.Font.Bold = true;
            rangeHeader3.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader3.Font.Name = "Times New Roman";

            #endregion

            #region Create Title for Account List from oWorkSheet

            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            oWorkSheet.Cells[initTitleIndex, 2] = "Mã SAA";
            oWorkSheet.Cells[initTitleIndex, 3] = "Mã Công Ty";
            oWorkSheet.Cells[initTitleIndex, 4] = "Họ Tên";
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).ColumnWidth = 30;
            oWorkSheet.Cells[initTitleIndex, 5] = "Chức danh công việc";
            oWorkSheet.Cells[initTitleIndex, 6] = "Hợp đồng";
            oWorkSheet.Cells[initTitleIndex, 7] = "Chức danh hệ số";
            oWorkSheet.Cells[initTitleIndex, 8] = "Bậc";
            oWorkSheet.Cells[initTitleIndex, 9] = "Hệ số";
            oWorkSheet.Cells[initTitleIndex, 10] = "Hưởng";
            oWorkSheet.Cells[initTitleIndex, 11] = "Từ ngày";
            oWorkSheet.Cells[initTitleIndex, 12] = "Đến ngày";

            #endregion
        }
    }
}