using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Excel;
using HRMBLL.H;
using HRMBLL.H0;
using HRMUtil;
using Constants = Excel.Constants;

namespace HRMBLL.BLLHelper
{
    public class Utilities
    {
        #region Export Excel file to use namespace Microsoft.Office.Interop.Excel

        public static string CreateExcelWorkBook(List<EmployeeIncomeBLL> dt, string pathName)
        {
            if ((dt != null) && (dt.Count > 0))
            {
                _Application oExcel;
                _Workbook oWorkBook;
                _Worksheet oWorkSheet;
                _Worksheet oWorkSheetCash;
                var date = Convert.ToDateTime(dt[0].Date);

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

        private static void InsertDataToWorkSheet(List<EmployeeIncomeBLL> dt, ref _Worksheet oWorkSheet,
            ref _Worksheet oWorkSheetCash, DateTime date)
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

            var rangeDept = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangeDept.Merge(Type.Missing);
            oWorkSheet.Cells[indexRow, 1] = dt[0].DepartmentName.ToUpper();
            rangeDept.Font.Bold = true;

            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            oWorkSheet.Cells[indexRow, 2] = dt[0].FullName;
            oWorkSheet.Cells[indexRow, 3] = dt[0].AccountNo;
            oWorkSheet.Cells[indexRow, 4] = "'" + dt[0].CardNo;
            oWorkSheet.Cells[indexRow, 5] = Convert.ToDecimal(dt[0].RealIncome.ToString()).ToString("#,###");
            oWorkSheet.Cells[indexRow, 6] = dt[0].EmployeeCode;
            totalAllRealIncome = totalAllRealIncome + Convert.ToDecimal(dt[0].RealIncome.ToString());

            #endregion

            for (var i = 1; i < dt.Count; i++)
                if ((dt[i].AccountNo != "") && (dt[i].AccountNo.Trim().Length > 0))
                {
                    indexRow++;
                    //isNextRowOfDept = false;
                    if ((i < dt.Count - 1) && (i > 1))
                    {
                        deparmentId = int.Parse(dt[i].DepartmentId.ToString());
                        departmentIdBefore = int.Parse(dt[i - 1].DepartmentId.ToString());
                    }

                    if (deparmentId != departmentIdBefore)
                    {
                        rangeDept = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
                        rangeDept.Merge(Type.Missing);
                        oWorkSheet.Cells[indexRow, 1] = dt[i].DepartmentName.ToUpper();
                        rangeDept.Font.Bold = true;
                        indexRow++;
                    }

                    /// inserting order number
                    oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                    oWorkSheet.Cells[indexRow, 2] = dt[i].FullName;
                    oWorkSheet.Cells[indexRow, 3] = dt[i].AccountNo;
                    oWorkSheet.Cells[indexRow, 4] = "'" + dt[i].CardNo;
                    oWorkSheet.Cells[indexRow, 5] = Convert.ToDecimal(dt[i].RealIncome.ToString()).ToString("#,###");
                    oWorkSheet.Cells[indexRow, 6] = dt[i].EmployeeCode;
                    totalAllRealIncome = totalAllRealIncome + Convert.ToDecimal(dt[i].RealIncome.ToString());
                }
                else
                {
                    oWorkSheetCash.Cells[indexRowCash, 1] = orderNumberCash++;
                    oWorkSheetCash.Cells[indexRowCash, 2] = dt[i].FullName;
                    oWorkSheetCash.Cells[indexRowCash, 3] = DepartmentAbbreviate(dt[i].DepartmentName);
                    oWorkSheetCash.Cells[indexRowCash, 4] =
                        Convert.ToDecimal(dt[i].RealIncome.ToString()).ToString("#,###");
                    totalAllRealIncomeCash = totalAllRealIncomeCash + Convert.ToDecimal(dt[i].RealIncome.ToString());
                    oWorkSheetCash.Cells[indexRowCash, 5] = "";
                    oWorkSheet.Cells[indexRow, 6] = dt[i].EmployeeCode;
                    indexRowCash++;
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

            var rangeHeader1 = oWorkSheet.get_Range("A1", "B1");
            rangeHeader1.Merge(Type.Missing);
            oWorkSheet.Cells[1, 1] = "CỤM CẢNG HK MIỀN NAM ";
            rangeHeader1.Font.Size = 12;

            var rangeHeader2 = oWorkSheet.get_Range("A2", "B2");
            rangeHeader2.Merge(Type.Missing);
            oWorkSheet.Cells[2, 1] = "CÔNG TY PVMĐ SÀI GÒN";
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
            oWorkSheet.Cells[initTitleIndex, 6] = "UserCode";
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

            var rangeHeader1 = oWorkSheetCash.get_Range("A1", "B1");
            rangeHeader1.Merge(Type.Missing);
            oWorkSheetCash.Cells[1, 1] = "CỤM CẢNG HK MIỀN NAM ";
            rangeHeader1.Font.Size = 12;

            var rangeHeader2 = oWorkSheetCash.get_Range("A2", "B2");
            rangeHeader2.Merge(Type.Missing);
            oWorkSheetCash.Cells[2, 1] = "CÔNG TY PVMĐ SÀI GÒN";
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
            oWorkSheetCash.Cells[initTitleIndex, 5] = "GHI CHÚ";
            ((Range) oWorkSheetCash.Cells[initTitleIndex, 5]).ColumnWidth = 17;
            oWorkSheetCash.Cells[initTitleIndex, 5] = "UserCode";
            ((Range) oWorkSheetCash.Cells[initTitleIndex, 6]).ColumnWidth = 17;
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

        #region Set Default UserName

        public static string AnalysisFullName(string fullname)
        {
            var name = fullname.ToLower().Trim().Split(' ');
            var newFirstName = string.Empty;
            var a = string.Empty;
            if (name.Length > 0)
            {
                var firstname = name[name.Length - 1];
                newFirstName = splitFirsName(firstname);

                for (var i = 0; i < name.Length - 1; i++)
                    if (name[i].Trim().Length > 0)
                    {
                        var arr = name[i].ToCharArray();
                        if (arr[0].CompareTo('đ') == 0)
                            a += "d";
                        else
                            a += Convert.ToString(arr[0]);
                    }
            }
            return newFirstName + a;
        }

        private static string splitFirsName(string firstname)
        {
            char[] arrU = {'ú', 'ù', 'ủ', 'ũ', 'ụ', 'ư', 'ứ', 'ừ', 'ử', 'ữ', 'ự'};
            char[] arrA = {'á', 'à', 'ả', 'ã', 'ạ', 'â', 'ấ', 'ầ', 'ẩ', 'ẫ', 'ậ', 'ă', 'ắ', 'ằ', 'ẳ', 'ẵ', 'ặ'};
            char[] arrE = {'é', 'è', 'ẻ', 'ẽ', 'ẹ', 'ê', 'ế', 'ề', 'ể', 'ễ', 'ệ'};
            char[] arrO = {'ó', 'ò', 'ỏ', 'õ', 'ọ', 'ô', 'ố', 'ồ', 'ổ', 'ỗ', 'ộ', 'ơ', 'ớ', 'ờ', 'ở', 'ỡ', 'ợ'};
            char[] arrI = {'i', 'í', 'ì', 'ỉ', 'ĩ', 'ị'};
            char[] arrY = {'y', 'ý', 'ỳ', 'ỷ', 'ỹ', 'ỵ'};

            var arrFirsName = firstname.ToCharArray();
            var resultFirstName = string.Empty;

            var found = false;
            for (var i = 0; i < arrFirsName.Length; i++)
            {
                // doi voi ky tu U
                if (!found)
                    for (var j = 0; j < arrU.Length; j++)
                        if (arrFirsName[i].CompareTo(arrU[j]) == 0)
                        {
                            arrFirsName[i] = 'u';
                            found = true;
                            break;
                        }
                // doi voi ky tu A
                if (!found)
                    for (var j = 0; j < arrA.Length; j++)
                        if (arrFirsName[i].CompareTo(arrA[j]) == 0)
                        {
                            arrFirsName[i] = 'a';
                            found = true;
                            break;
                        }
                // doi voi ky tu E
                if (!found)
                    for (var j = 0; j < arrE.Length; j++)
                        if (arrFirsName[i].CompareTo(arrE[j]) == 0)
                        {
                            arrFirsName[i] = 'e';
                            found = true;
                            break;
                        }
                // doi voi ky tu O
                if (!found)
                    for (var j = 0; j < arrO.Length; j++)
                        if (arrFirsName[i].CompareTo(arrO[j]) == 0)
                        {
                            arrFirsName[i] = 'o';
                            found = true;
                            break;
                        }
                // doi voi ky tu I
                if (!found)
                    for (var j = 0; j < arrI.Length; j++)
                        if (arrFirsName[i].CompareTo(arrI[j]) == 0)
                        {
                            arrFirsName[i] = 'i';
                            found = true;
                            break;
                        }
                // doi voi ky tu Y
                if (!found)
                    for (var j = 0; j < arrY.Length; j++)
                        if (arrFirsName[i].CompareTo(arrY[j]) == 0)
                        {
                            arrFirsName[i] = 'y';
                            found = true;
                            break;
                        }

                resultFirstName += arrFirsName[i].ToString();
            }

            return resultFirstName;
        }

        #endregion

        #region Some methods for LeaveDays

        public static List<DateTime> getSundays(int Month, int Year)
        {
            var lstSundays = new List<DateTime>();
            var intMonth = Month;
            var intYear = Year;
            var ci = new CultureInfo("en-US");
            for (var i = 1; i <= ci.Calendar.GetDaysInMonth(intYear, intMonth); i++)
                if (new DateTime(intYear, intMonth, i).DayOfWeek == DayOfWeek.Sunday)
                    lstSundays.Add(new DateTime(intYear, intMonth, i));
            return lstSundays;
        }

        public static List<DateTime> GetDateRange(DateTime StartingDate, DateTime EndingDate)
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

        public static double GetLeaveDaysV1(LeaveTypesBLL objLeaveTypesBLL, DateTime fromDate, DateTime toDate)
        {
            var days = 0;
            var tempFromDate = fromDate;
            var leaveTypeId = objLeaveTypesBLL.LeaveTypeId;

            while (tempFromDate.CompareTo(toDate) <= 0)
            {
                days++;

                tempFromDate = tempFromDate.AddDays(1);
            }

            var DateRange = GetDateRange(fromDate, toDate);

            var SubDays = DateRange.Count/7;

            return days - objLeaveTypesBLL.Availabledays - SubDays;
        }

        public static double GetLeaveDays(LeaveTypesBLL objLeaveTypesBLL, DateTime fromDate, DateTime toDate)
        {
            var days = 0;
            var tempFromDate = fromDate;
            var leaveTypeId = objLeaveTypesBLL.LeaveTypeId;

            while (tempFromDate.CompareTo(toDate) <= 0)
            {
                //list.Contains(new HolidaysBLL(0,string.Empty,tempFromDate));
                //if (tempFromDate.DayOfWeek != DayOfWeek.Saturday && tempFromDate.DayOfWeek != DayOfWeek.Sunday)
                //HolidaysBLL objTemp = new HolidaysBLL(0, string.Empty, tempFromDate);
                if ((leaveTypeId == HRMUtil.Constants.LEAVE_TYPE_KHHDS) ||
                    (leaveTypeId == HRMUtil.Constants.LEAVE_TYPE_O_DAI_NGAY)
                    || (leaveTypeId == HRMUtil.Constants.LEAVE_TYPE_TNLD) ||
                    (leaveTypeId == HRMUtil.Constants.LEAVE_TYPE_F_CONG_TAC))
                {
                    /// CN va cac ngay nghi le, tet
                    /// 


                    //if (!CheckHoliday(tempFromDate) && tempFromDate.DayOfWeek != DayOfWeek.Sunday)
                    //{
                    days++;
                    //}
                }
                else if ((leaveTypeId == HRMUtil.Constants.LEAVE_TYPE_THAI_SAN) ||
                         (leaveTypeId == HRMUtil.Constants.LEAVE_TYPE_F_DI_DUONG))
                {
                    // tinh tat ca
                    if (tempFromDate.DayOfWeek != DayOfWeek.Sunday)
                        days++;
                }
                else if ((leaveTypeId == HRMUtil.Constants.LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO) ||
                         (leaveTypeId == HRMUtil.Constants.LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO))
                {
                    // CN va cac ngay nghi le, tet
                    //if (!CheckHoliday(tempFromDate) && tempFromDate.DayOfWeek != DayOfWeek.Sunday)
                    //{
                    days++;
                    //}
                }
                else if ((leaveTypeId == HRMUtil.Constants.LEAVE_TYPE_HOC_1) ||
                         (leaveTypeId == HRMUtil.Constants.LEAVE_TYPE_HOC_2)
                         || (leaveTypeId == HRMUtil.Constants.LEAVE_TYPE_HOC_3) ||
                         (leaveTypeId == HRMUtil.Constants.LEAVE_TYPE_HOC_4)
                         || (leaveTypeId == HRMUtil.Constants.LEAVE_TYPE_HOC_5) ||
                         (leaveTypeId == HRMUtil.Constants.LEAVE_TYPE_HOC_6)
                         || (leaveTypeId == HRMUtil.Constants.LEAVE_TYPE_HOC_7))
                {
                    // tinh tat ca
                    days++;
                }
                else
                {
                    // tinh tat ca
                    if (!CheckHoliday(tempFromDate) && (tempFromDate.DayOfWeek != DayOfWeek.Sunday))
                        days++;
                }

                tempFromDate = tempFromDate.AddDays(1);
            }

            return days - objLeaveTypesBLL.Availabledays;
        }

        public static List<LeaveDate> AnalyseLeaveDate(LeaveTypesBLL objLeaveTypesBLL, DateTime fromDate,
            DateTime toDate)
        {
            var list = new List<LeaveDate>();
            ;

            if (((fromDate.Month != toDate.Month) && (fromDate.Year == toDate.Year))
                || ((fromDate.Month == toDate.Month) && (fromDate.Year != toDate.Year))
                || ((fromDate.Month != toDate.Month) && (fromDate.Year != toDate.Year)))
            {
                var tempDate = fromDate;
                var startDate = fromDate;
                DateTime endDate;
                while (!((tempDate.Month > toDate.Month) && (tempDate.Year == toDate.Year)))
                {
                    if (tempDate.Month == toDate.Month)
                    {
                        endDate = toDate;
                    }
                    else
                    {
                        var daysInMonth = DateTime.DaysInMonth(tempDate.Year, tempDate.Month);
                        endDate = new DateTime(startDate.Year, startDate.Month, daysInMonth);
                    }

                    double days = 1;
                    days = GetLeaveDays(objLeaveTypesBLL, startDate, endDate);
                    list.Add(new LeaveDate(startDate, endDate, days));

                    startDate = endDate.AddDays(1);
                    tempDate = tempDate.AddMonths(1);
                }
            }
            else
            {
                double days = 1;
                days = GetLeaveDays(objLeaveTypesBLL, fromDate, toDate);
                list.Add(new LeaveDate(fromDate, toDate, days));
            }
            return list;
        }


        private static bool CheckHoliday(DateTime date)
        {
            var list = HolidaysBLL.GetByDate(date.Month, date.Year);
            var returnValue = false;

            foreach (var objTemp in list)
                if (objTemp.HolidayDate.Equals(date))
                {
                    returnValue = true;
                    break;
                }

            return returnValue;
        }

        #endregion
    }
}