using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Excel;
using HRMDAL.H;
using HRMUtil.KeyNames.H;
using HRMUtil.KeyNames.H0;
using DataTable = System.Data.DataTable;

namespace HRMBLL.H.Helper
{
    public class IncomesMonthBLLExportData
    {
        #region Export Excel file IncomesMonthBLL

        public static string ExportIncomesMonthBLL(string fullName, int departmentId, int month, int year,
            string pathName)
        {
            var dt = new EmployeeIncomeDAL().GetByFilter(fullName, departmentId, month, year);

            if ((dt != null) && (dt.Rows.Count > 0))
            {
                _Application oExcel;
                _Workbook oWorkBook;
                _Worksheet oWorkSheet;
                _Worksheet oWorkSheetCash;

                var dr0 = dt.Rows[0];
                var date = dr0[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_DATE] == DBNull.Value
                    ? DateTime.MinValue
                    : Convert.ToDateTime(dr0[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_DATE].ToString());

                var fileName = string.Empty;
                try
                {
                    GC.Collect();
                    oExcel = new Application();
                    oExcel.Visible = false;

                    // Get new workbook
                    oWorkBook = oExcel.Workbooks.Add(Missing.Value);
                    oWorkSheet = (_Worksheet) oWorkBook.Worksheets[1];
                    oWorkSheetCash = (_Worksheet) oWorkBook.Worksheets[2];
                    oWorkSheet.Name = "QUA TÀI KHOẢN";
                    oWorkSheetCash.Name = "QUA TIỀN MẶT";
                    InsertDataToWorkSheet(dt, ref oWorkSheet, ref oWorkSheetCash, date);

                    oExcel.Visible = false;
                    oExcel.UserControl = false;
                    fileName = pathName + "\\DSTKLuongThang" + date.Month + "_" + date.Year + ".xls";
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
                    Marshal.ReleaseComObject(oWorkSheetCash);
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

        private static void InsertDataToWorkSheet(DataTable dt, ref _Worksheet oWorkSheet, ref _Worksheet oWorkSheetCash,
            DateTime date)
        {
            var deparmentId = 0;
            var departmentIdBefore = 0;
            decimal totalAllRealIncome = 0;
            decimal totalAllRealIncomeCash = 0;
            var initTitleIndex = 7;
            // create Content
            var indexRow = initTitleIndex + 1;
            var indexRowCash = initTitleIndex + 1;
            var orderNumber = 1;
            var orderNumberCash = 1;
            //bool isNextRowOfDept = false;
            var value = string.Empty;
            CreateHeaderAndTitleForAccount(ref oWorkSheet, ref initTitleIndex, date);
            CreateHeaderAndTitleForCash(ref oWorkSheetCash, ref initTitleIndex, date);

            #region Create FirstRow For Director, Administration and Training Department from oWorkSheet

            var dr0 = dt.Rows[0];

            var rangeDept = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangeDept.Merge(Type.Missing);
            oWorkSheet.Cells[indexRow, 1] = dr0[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_DEPARTMENT_NAME] ==
                                            DBNull.Value
                ? string.Empty
                : dr0[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_DEPARTMENT_NAME].ToString().ToUpper();
            rangeDept.Font.Bold = true;

            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            oWorkSheet.Cells[indexRow, 2] = dr0[EmployeeKeys.Field_Employees_FullName2] == DBNull.Value
                ? string.Empty
                : dr0[EmployeeKeys.Field_Employees_FullName2].ToString();
            oWorkSheet.Cells[indexRow, 3] = dr0[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_ACCOUNT_NO] == DBNull.Value
                ? string.Empty
                : dr0[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_ACCOUNT_NO].ToString();
            oWorkSheet.Cells[indexRow, 4] = "'" +
                                            (dr0[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_CARD_NO] == DBNull.Value
                                                ? string.Empty
                                                : dr0[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_CARD_NO].ToString());
            var oRealIncome = dr0[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_REAL_INCOME] == DBNull.Value
                ? 0
                : Convert.ToDecimal(dr0[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_REAL_INCOME].ToString());
            oWorkSheet.Cells[indexRow, 5] = oRealIncome.ToString("#,###");
            oWorkSheet.Cells[indexRow, 6] = dr0[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                ? string.Empty
                : dr0[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();
            totalAllRealIncome = totalAllRealIncome + oRealIncome;

            #endregion

            for (var i = 1; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                var accountNo = dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_ACCOUNT_NO] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_ACCOUNT_NO].ToString();

                if ((accountNo != "") && (accountNo.Trim().Length > 0))
                {
                    var dr_1 = dt.Rows[i - 1];
                    var rootId = dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_DEPARTMENT_ID] == DBNull.Value
                        ? 0
                        : int.Parse(dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_DEPARTMENT_ID].ToString());
                    var rootId_1 = dr_1[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_DEPARTMENT_ID] == DBNull.Value
                        ? 0
                        : int.Parse(dr_1[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_DEPARTMENT_ID].ToString());

                    indexRow++;
                    //isNextRowOfDept = false;
                    if ((i < dt.Rows.Count - 1) && (i > 1))
                    {
                        deparmentId = rootId;
                        departmentIdBefore = rootId_1;
                    }

                    if (deparmentId != departmentIdBefore)
                    {
                        rangeDept = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
                        rangeDept.Merge(Type.Missing);
                        oWorkSheet.Cells[indexRow, 1] = dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_DEPARTMENT_NAME] ==
                                                        DBNull.Value
                            ? string.Empty
                            : dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_DEPARTMENT_NAME].ToString().ToUpper();
                        rangeDept.Font.Bold = true;
                        indexRow++;
                    }

                    /// inserting order number
                    oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                    oWorkSheet.Cells[indexRow, 2] = dr[EmployeeKeys.Field_Employees_FullName2] == DBNull.Value
                        ? string.Empty
                        : dr[EmployeeKeys.Field_Employees_FullName2].ToString();
                    oWorkSheet.Cells[indexRow, 3] = dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_ACCOUNT_NO] ==
                                                    DBNull.Value
                        ? string.Empty
                        : dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_ACCOUNT_NO].ToString();
                    oWorkSheet.Cells[indexRow, 4] = "'" +
                                                    (dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_CARD_NO] ==
                                                     DBNull.Value
                                                        ? string.Empty
                                                        : dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_CARD_NO].ToString());
                    var realIncome = dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_REAL_INCOME] == DBNull.Value
                        ? 0
                        : Convert.ToDecimal(dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_REAL_INCOME].ToString());
                    oWorkSheet.Cells[indexRow, 5] = realIncome.ToString("#,###");
                    oWorkSheet.Cells[indexRow, 6] = dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                        ? string.Empty
                        : dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();
                    totalAllRealIncome = totalAllRealIncome + realIncome;
                }
                else
                {
                    oWorkSheetCash.Cells[indexRowCash, 1] = orderNumberCash++;

                    oWorkSheetCash.Cells[indexRowCash, 2] = dr[EmployeeKeys.Field_Employees_FullName2] == DBNull.Value
                        ? string.Empty
                        : dr[EmployeeKeys.Field_Employees_FullName2].ToString();
                    oWorkSheetCash.Cells[indexRowCash, 3] =
                        dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_DEPARTMENT_NAME] == DBNull.Value
                            ? string.Empty
                            : dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_DEPARTMENT_NAME].ToString();
                    var realIncomeCash = dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_REAL_INCOME] == DBNull.Value
                        ? 0
                        : Convert.ToDecimal(dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_REAL_INCOME].ToString());
                    oWorkSheetCash.Cells[indexRowCash, 4] = realIncomeCash.ToString("#,###");
                    oWorkSheetCash.Cells[indexRowCash, 5] = dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                        ? string.Empty
                        : dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();
                    totalAllRealIncomeCash = totalAllRealIncomeCash + realIncomeCash;
                    indexRowCash++;
                }
            }

            #region add row total and format for Account

            indexRow = indexRow + 1;
            oWorkSheet.Cells[indexRow, 1] = "TỔNG CỘNG";
            Range totalRange = oWorkSheet.get_Range(oWorkSheet.Cells[indexRow, 1], oWorkSheet.Cells[indexRow, 2]);
            totalRange.Merge(Type.Missing);
            totalRange.Font.Bold = true;
            totalRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            oWorkSheet.Cells[indexRow, 5] = totalAllRealIncome.ToString("#,###");
            ((Range) oWorkSheet.Cells[indexRow, 5]).Font.Bold = true;
            var rangeAll = oWorkSheet.get_Range("A7", "E" + indexRow);
            rangeAll.Borders.LineStyle = Constants.xlSolid;
            rangeAll.Borders.Weight = 2;
            rangeAll.Font.Size = 12;

            #endregion

            #region add row total and format for Cash

            oWorkSheetCash.Cells[indexRowCash, 1] = "TỔNG CỘNG";
            Range totalRangeCash = oWorkSheetCash.get_Range(oWorkSheetCash.Cells[indexRowCash, 1],
                oWorkSheetCash.Cells[indexRowCash, 2]);
            totalRangeCash.Merge(Type.Missing);
            totalRangeCash.Font.Bold = true;
            totalRangeCash.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            oWorkSheetCash.Cells[indexRowCash, 4] = totalAllRealIncomeCash.ToString("#,###");
            ((Range) oWorkSheetCash.Cells[indexRowCash, 4]).Font.Bold = true;
            var rangeAllCash = oWorkSheetCash.get_Range("A7", "E" + indexRowCash);
            rangeAllCash.Borders.LineStyle = Constants.xlSolid;
            rangeAllCash.Borders.Weight = 2;
            rangeAllCash.Font.Size = 12;

            #endregion

            #region Create footer

            indexRow = indexRow + 2;
            Range rangeFooter1 = oWorkSheet.get_Range(oWorkSheet.Cells[indexRow, 4], oWorkSheet.Cells[indexRow, 5]);
            rangeFooter1.Merge(Type.Missing);
            oWorkSheet.Cells[indexRow, 4] = "Ngày       tháng      năm " + DateTime.Now.Year;
            rangeFooter1.Font.Name = "Times New Roman";
            rangeFooter1.Font.Size = 12;
            rangeFooter1.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            indexRow = indexRow + 1;
            Range rangeFooter2 = oWorkSheet.get_Range(oWorkSheet.Cells[indexRow, 4], oWorkSheet.Cells[indexRow, 5]);
            rangeFooter2.Merge(Type.Missing);
            oWorkSheet.Cells[indexRow, 4] = "GIÁM ĐỐC";
            rangeFooter2.Font.Name = "Times New Roman";
            rangeFooter2.Font.Size = 12;
            rangeFooter2.Font.Bold = true;
            rangeFooter2.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            indexRow = indexRow + 5;
            Range rangeFooter3 = oWorkSheet.get_Range(oWorkSheet.Cells[indexRow, 4], oWorkSheet.Cells[indexRow, 5]);
            rangeFooter3.Merge(Type.Missing);
            oWorkSheet.Cells[indexRow, 4] = "NGUYỄN ĐÌNH HÙNG";
            rangeFooter3.Font.Name = "Times New Roman";
            rangeFooter3.Font.Size = 12;
            rangeFooter3.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            #endregion

            // release range object            
        }

        private static void CreateHeaderAndTitleForAccount(ref _Worksheet oWorkSheet, ref int initTitleIndex,
            DateTime date)
        {
            #region create header for printing from oWorkSheet

            var rangeHeader1 = oWorkSheet.get_Range("A1", "C1");
            rangeHeader1.Merge(Type.Missing);
            oWorkSheet.Cells[1, 1] = "TỔNG CÔNG TY CẢNG HÀNG KHÔNG MIỀN NAM ";
            rangeHeader1.Font.Size = 12;

            var rangeHeader2 = oWorkSheet.get_Range("A2", "C2");
            rangeHeader2.Merge(Type.Missing);
            oWorkSheet.Cells[2, 1] = "CÔNG TY PHỤC VỤ MẶT ĐẤT SÀI GÒN";
            rangeHeader2.Font.Size = 12;
            rangeHeader2.Font.Bold = true;

            var rangeHeader3 = oWorkSheet.get_Range("A4", "E4");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "DANH SÁCH CB.CNV NHẬN LƯƠNG QUA THẺ ATM";
            rangeHeader3.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader3.Font.Size = 15;
            rangeHeader3.Font.Bold = true;

            var rangeHeader4 = oWorkSheet.get_Range("A5", "E5");
            rangeHeader4.Merge(Type.Missing);
            oWorkSheet.Cells[5, 1] = "THÁNG " + date.Month + " / " + date.Year;
            rangeHeader4.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader4.Font.Size = 12;
            rangeHeader4.Font.Bold = true;

            #endregion

            #region Create Title for Account List from oWorkSheet

            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            oWorkSheet.Cells[initTitleIndex, 2] = "HỌ VÀ TÊN";
            ((Range) oWorkSheet.Cells[initTitleIndex, 2]).ColumnWidth = 30;
            oWorkSheet.Cells[initTitleIndex, 3] = "SỐ TÀI KHOẢN";
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 20;
            oWorkSheet.Cells[initTitleIndex, 4] = "SỐ THẺ";
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).ColumnWidth = 23;
            oWorkSheet.Cells[initTitleIndex, 5] = "SỐ TIỀN";
            ((Range) oWorkSheet.Cells[initTitleIndex, 5]).ColumnWidth = 17;
            oWorkSheet.Cells[initTitleIndex, 6] = "HỌ VÀ TÊN CÓ DẤU";
            ((Range) oWorkSheet.Cells[initTitleIndex, 5]).ColumnWidth = 17;
            /// format title
            Range oTitleRange = oWorkSheet.get_Range(oWorkSheet.Cells[initTitleIndex, 1],
                oWorkSheet.Cells[initTitleIndex, 5]);
            oTitleRange.Font.Bold = true;

            oTitleRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            #endregion
        }

        private static void CreateHeaderAndTitleForCash(ref _Worksheet oWorkSheetCash, ref int initTitleIndex,
            DateTime date)
        {
            #region create header for printing from oWorkSheetCash

            var rangeHeader1 = oWorkSheetCash.get_Range("A1", "C1");
            rangeHeader1.Merge(Type.Missing);
            oWorkSheetCash.Cells[1, 1] = "TỔNG CÔNG TY CẢNG HÀNG KHÔNG MIỀN NAM ";
            rangeHeader1.Font.Size = 12;

            var rangeHeader2 = oWorkSheetCash.get_Range("A2", "C2");
            rangeHeader2.Merge(Type.Missing);
            oWorkSheetCash.Cells[2, 1] = "CÔNG TY PHỤC VỤ MẶT ĐẤT SÀI GÒN";
            rangeHeader2.Font.Size = 12;
            rangeHeader2.Font.Bold = true;

            var rangeHeader3 = oWorkSheetCash.get_Range("A4", "E4");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheetCash.Cells[4, 1] = "DANH SÁCH CB.CNV NHẬN LƯƠNG QUA TIỀN MẶT";
            rangeHeader3.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader3.Font.Size = 15;
            rangeHeader3.Font.Bold = true;

            var rangeHeader4 = oWorkSheetCash.get_Range("A5", "E5");
            rangeHeader4.Merge(Type.Missing);
            oWorkSheetCash.Cells[5, 1] = "THÁNG " + date.Month + " / " + date.Year;
            rangeHeader4.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader4.Font.Size = 12;
            rangeHeader4.Font.Bold = true;

            #endregion

            #region Create Title for Account List from oWorkSheetCash

            /// inserting title
            oWorkSheetCash.Cells[initTitleIndex, 1] = "STT";
            oWorkSheetCash.Cells[initTitleIndex, 2] = "HỌ VÀ TÊN";
            ((Range) oWorkSheetCash.Cells[initTitleIndex, 2]).ColumnWidth = 30;
            oWorkSheetCash.Cells[initTitleIndex, 3] = "PHÒNG BAN";
            ((Range) oWorkSheetCash.Cells[initTitleIndex, 3]).ColumnWidth = 20;
            oWorkSheetCash.Cells[initTitleIndex, 4] = "SỐ TIỀN";
            ((Range) oWorkSheetCash.Cells[initTitleIndex, 4]).ColumnWidth = 23;
            oWorkSheetCash.Cells[initTitleIndex, 5] = "HỌ VÀ TÊN CÓ DẤU";
            ((Range) oWorkSheetCash.Cells[initTitleIndex, 5]).ColumnWidth = 17;
            /// format title
            Range oTitleRange = oWorkSheetCash.get_Range(oWorkSheetCash.Cells[initTitleIndex, 1],
                oWorkSheetCash.Cells[initTitleIndex, 5]);
            oTitleRange.Font.Bold = true;

            oTitleRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            #endregion
        }

        private static string DepartmentAbbreviate(string deparmentName)
        {
            var wordSplit = deparmentName.Split(' ');
            var deptAbbreviate = string.Empty;
            for (var i = 1; i < wordSplit.Length; i++)
            {
                var charArr = wordSplit[i].ToCharArray();
                deptAbbreviate += charArr[0].ToString();
            }

            return deptAbbreviate;
        }

        #endregion
    }
}