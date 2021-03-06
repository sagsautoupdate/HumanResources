using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Excel;
using HRMDAL.H0;
using HRMUtil;
using HRMUtil.KeyNames.H0;
using Constants = Excel.Constants;
using DataTable = System.Data.DataTable;

namespace HRMBLL.H0.Helper
{
    public class EmployeeTimeBillBLLExportData
    {
        public static double GetXQD(int deptId, int month, int year)
        {
            double XQD = 0;
            if (_DeptId != deptId)
            {
                var isOfficeHours = false;
                if (H1.Helper.DefaultValues.IsMediateDepartment(deptId))
                    isOfficeHours = true;
                else
                    isOfficeHours = false;

                XQD = H1.Helper.DefaultValues.XQD(month, year);
                //XQD = XQD -3;
                if (!isOfficeHours)
                    XQD = H1.Helper.DefaultValues.XQDSalaryMinusHoliday(month, year);
            }
            else
            {
                XQD = _XQD;
            }

            return XQD;
        }

        public static double GetXReal(int userId, int month, int year, double xQD, ref double dayOff, ref double hourX,
            ref int countNotCheckin, ref int countNotCheckout, ref int countLateIn, ref int countEarlyOut)
        {
            double countX = 0;
            double totalHours = 0;

            var list = EmployeeTimeBillBLL.GetByFilter(userId, -1, -1, month, year);
            if (list.Count > 0)
            {
                for (var i = 0; i < list.Count - 1; i++)
                {
                    var obj = list[i];
                    var objNext = list[i + 1];
                    //if (obj.WorkDate.ToShortDateString().CompareTo(objNext.WorkDate.ToShortDateString()) != 0)
                    //{
                    //    countX++;
                    //}
                    //////////////////////////////////////////////////////////////
                    ////// tinh gio lam thuc te
                    if (obj.Status == 2)
                    {
                        var tempTotalHours = obj.TimeOut - obj.TimeIn;
                        totalHours = tempTotalHours.TotalHours;
                        if ((totalHours < 4) && (obj.TimeIn.Hour > 13) && CheckNotCheckin(obj.UserId, obj.TimeIn))
                            countNotCheckin++;
                        if ((totalHours > 12) || (obj.Status == 1))
                        {
                            if (totalHours > 16)
                                countNotCheckout++;
                            totalHours = 8;
                            var tt = new DateTime(obj.TimeIn.Year, obj.TimeIn.Month, obj.TimeIn.Day, 15, 00, 0);
                            var ttTotalHours = obj.TimeIn - tt;
                            if (ttTotalHours.TotalHours > 0)
                                totalHours = 0;
                        }
                        else
                        {
                            //// tru gio nghi trua
                            totalHours = totalHours - 1;
                        }
                        hourX = hourX + totalHours;
                        ///////////////////////////////////////////////////////
                        //// Tinh so lan di tre ve som
                        var standardTimeIn = new DateTime(obj.TimeIn.Year, obj.TimeIn.Month, obj.TimeIn.Day, 7, 30, 0);
                        var spanLateIn = obj.TimeIn - standardTimeIn;
                        if ((spanLateIn.TotalMinutes >= 15) && CheckNotCheckin(obj.UserId, obj.TimeIn))
                            countLateIn++;
                        var standardTimeOut = new DateTime(obj.TimeIn.Year, obj.TimeIn.Month, obj.TimeIn.Day, 16, 30, 0);
                        var spanLateOut = standardTimeOut - obj.TimeOut;
                        if ((spanLateOut.TotalMinutes > 15) && (obj.Status == 2) &&
                            CheckNotCheckin(obj.UserId, obj.TimeOut))
                            countEarlyOut++;
                        if (totalHours >= 6)
                            countX = countX + 1;
                        else if ((totalHours >= 2) && (totalHours < 6))
                            countX = countX + 0.5;
                    }
                    if (i == list.Count - 2) /// last item
                        if (objNext.Status == 2)
                        {
                            totalHours = 0;
                            var tempTotalHours = objNext.TimeOut - objNext.TimeIn;
                            totalHours = tempTotalHours.TotalHours;
                            if ((totalHours < 4) && (objNext.TimeIn.Hour > 13) &&
                                CheckNotCheckin(objNext.UserId, objNext.TimeIn))
                                countNotCheckin++;
                            if (totalHours > 12)
                            {
                                totalHours = 8;
                                /// dem so lan khong checkout
                                countNotCheckout++;
                            }
                            else
                            {
                                //// tru gio nghi trua
                                totalHours = totalHours - 1;
                            }
                            hourX = hourX + totalHours;
                            ///////////////////////////////////////////////////////
                            //// Tinh so lan di tre ve som
                            var standardTimeIn = new DateTime(objNext.TimeIn.Year, objNext.TimeIn.Month,
                                objNext.TimeIn.Day, 7, 30, 0);
                            var spanLateIn = objNext.TimeIn - standardTimeIn;
                            if ((spanLateIn.TotalMinutes >= 15) && CheckNotCheckin(objNext.UserId, objNext.TimeIn))
                                countLateIn++;
                            var standardTimeOut = new DateTime(objNext.TimeIn.Year, objNext.TimeIn.Month,
                                objNext.TimeIn.Day, 16, 30, 0);
                            var spanLateOut = standardTimeOut - objNext.TimeOut;
                            if ((spanLateOut.TotalMinutes >= 5) && (objNext.Status == 2) &&
                                CheckNotCheckin(objNext.UserId, objNext.TimeOut))
                                countEarlyOut++;
                            if ((objNext.Status == 1) &&
                                (DateTime.Now.ToShortDateString().CompareTo(objNext.WorkDate.ToShortDateString()) > 0))
                                countNotCheckout++;
                            if (totalHours >= 6)
                                countX = countX + 1;
                            else if ((totalHours >= 2) && (totalHours < 6))
                                countX = countX + 0.5;
                        }
                }

                dayOff = xQD - countX;
            }

            return countX;
        }

        private static bool CheckNotCheckin(int userId, DateTime Datein)
        {
            var list = EmployeeTimeBillBLL.GetByFilter(userId, -1, Datein.Day, Datein.Month, Datein.Year);
            if (list.Count == 1)
                return true;
            foreach (var objE in list)
                if (objE.TimeIn.Hour < 11)
                    return false;
            return true;
        }

        //////////////////////////////////////////////////////////////////////////////////////////
        public static double GetXRealDirectDepartment(int userId, int month, int year, double xQD, ref double dayOff,
            ref double hourX, ref double Ho, ref int countNotCheckin, ref int countNotCheckout, ref int countLateIn,
            ref int countEarlyOut)
        {
            double countX = 0;
            double totalHours = 0;
            //double totalByDate = 0;

            var list = EmployeeTimeBillBLL.GetByFilter(userId, -1, -1, month, year);
            if (list.Count > 0)
                for (var i = 0; i < list.Count; i++)
                {
                    var obj = list[i];
                    //EmployeeTimeBillBLL objNext = list[i + 1];
                    //if (obj.WorkDate.ToShortDateString().CompareTo(objNext.WorkDate.ToShortDateString()) != 0)
                    //{
                    //    countX++;
                    //}
                    //////////////////////////////////////////////////////////////
                    ////// tinh gio lam thuc te
                    if (obj.Status == 2)
                    {
                        var tempTotalHours = obj.TimeOut - obj.TimeIn;
                        totalHours = tempTotalHours.TotalHours;
                        if ((totalHours > 9) && (obj.OverTime != 1))
                            totalHours = 8;
                        else if (totalHours < 0)
                            totalHours = 0;

                        hourX = hourX + totalHours;
                        if ((totalHours >= 2) && (totalHours < 6))
                            countX = countX + 0.5;
                        else if ((totalHours >= 6) && (totalHours < 10))
                            countX = countX + 1;
                        else if ((totalHours >= 10) && (totalHours <= 13))
                            countX = countX + 1.5;
                        else if (totalHours > 13)
                            countX = countX + 1;
                    }
                }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////// tinh lai theo form hien thi
            //List<string> listDate = EmployeeTimeBillBLL.GetDistinctWorkDareByUserId(userId, month, year);
            //foreach (string sDate in listDate)
            //{
            //    DateTime workdate = FormatDate.FormatUSDate(sDate, 111);
            //    List<EmployeeTimeBillBLL> lstDetail = EmployeeTimeBillBLL.GetByUserDateForWorkingHo(userId, workdate.Day, workdate.Month, workdate.Year);
            //    foreach (EmployeeTimeBillBLL obj in lstDetail)
            //    {
            //        if (obj.Status == 2)
            //        {
            //            TimeSpan tempTotalHours = obj.TimeOut - obj.TimeIn;
            //            totalHours = tempTotalHours.TotalHours;
            //            if (totalHours > 9 && obj.OverTime != 1)
            //            {
            //                totalHours = 8;
            //            }
            //            else if (totalHours < 0)
            //            {
            //                totalHours = 0;
            //            }
            //        }
            //    }
            //    string tt = totalHours.ToString("0#.00");
            //    totalByDate = totalByDate + double.Parse(tt);
            //    if (totalByDate < 0)
            //    {
            //        totalByDate = 0;
            //    }
            //    double realTimeWork = 0;
            //    realTimeWork = totalByDate;

            //    hourX = hourX + realTimeWork;
            //    if (realTimeWork >= 2 && realTimeWork < 6)
            //    {
            //        countX = countX + 0.5;
            //    }
            //    else if (realTimeWork >= 6 && realTimeWork < 12)
            //    {
            //        countX = countX + 1;
            //    }
            //    else if (realTimeWork >= 12 && realTimeWork <= 13)
            //    {
            //        countX = countX + 1.5;
            //    }
            //    else if (realTimeWork > 13)
            //    {
            //        countX = countX + 1;
            //    }
            //}

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////// Tinh cong hoc

            var listHo = EmployeeTimeBillBLL.GetByFilterByHo(userId, -1, -1, month, year);
            if (listHo.Count > 0)
                for (var i = 0; i < listHo.Count - 1; i++)
                {
                    var obj = listHo[i];
                    Ho = Ho + 1;
                }
            dayOff = xQD - (countX + Ho);
            return countX;
        }

        #region employee Data

        private static readonly double _XQD = 0;
        private static readonly int _DeptId = 0;

        public static string ExcelKhoiVanPhongByFilter(string deptIds, int rootId, string fullname, int month, int year,
            string pathName)
        {
            ///////////////////////////////////////////
            var dt = new EmployeesDAL().GetByDeptIds(deptIds, rootId, fullname, 1);
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

                    oWorkSheet.Name = "DS_NHAN_VIEN";

                    InsertDataKhoiVanPhongToWorkSheet(dt, ref oWorkSheet, month, year);

                    oExcel.Visible = false;
                    oExcel.UserControl = false;
                    fileName = pathName + "\\BaoCaoChamCongKVP.xls";
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

        private static void InsertDataKhoiVanPhongToWorkSheet(DataTable dt, ref _Worksheet oWorkSheet, int month,
            int year)
        {
            var deparmentId = 0;
            var departmentIdBefore = 0;
            var initTitleIndex = 7;
            // create Content
            var indexRow = initTitleIndex + 1;
            var orderNumber = 1;
            var value = string.Empty;
            var userId = 0;
            var deptId = 0;
            //////////////////////////////////////////////
            double xQD = 0;
            double xReal = 0;
            double hourXQD = 0;
            double hourX = 0;

            double xOff = 0;
            var countNotCheckin = 0;
            var countNotCheckout = 0;
            var countLateIn = 0;
            var countEarlyOut = 0;
            double Ho = 0;
            /////////////////////////////////////////////
            CreateHeaderAndTitleKhoiVanPhong(ref oWorkSheet, ref initTitleIndex, month, year);

            #region Create FirstRow For Director, Administration and Training Department from oWorkSheet

            var rangeDept = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangeDept.Merge(Type.Missing);
            rangeDept.Font.Bold = true;

            var dr0 = dt.Rows[0];
            oWorkSheet.Cells[indexRow, 1] = dr0[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME] == DBNull.Value
                ? string.Empty
                : dr0[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME].ToString().ToUpper();

            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            userId = dr0[EmployeeKeys.FIELD_EMPLOYEES_USERID] == DBNull.Value
                ? 0
                : int.Parse(dr0[EmployeeKeys.FIELD_EMPLOYEES_USERID].ToString());
            oWorkSheet.Cells[indexRow, 2] = StringFormat.GetUserCode(userId);
            oWorkSheet.Cells[indexRow, 3] = dr0[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                ? string.Empty
                : dr0[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();
            deptId = dr0[DepartmentKeys.FIELD_DEPARTMENT_ID] == DBNull.Value
                ? 0
                : (int) dr0[DepartmentKeys.FIELD_DEPARTMENT_ID];

            var rootId = dr0[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID] == DBNull.Value
                ? 0
                : int.Parse(dr0[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID].ToString());

            /////////////////////////////////////////////////////////////////
            xQD = int.Parse(GetXQD(deptId, month, year).ToString());

            if ((rootId == 2) || (rootId == 3) || (rootId == 4) || (rootId == 58) || (rootId == 59))
            {
                hourXQD = xQD*8;
                xReal = GetXReal(userId, month, year, xQD, ref xOff, ref hourX, ref countNotCheckin,
                    ref countNotCheckout, ref countLateIn, ref countEarlyOut);
            }
            else
            {
                hourXQD = xQD*7;
                xReal = GetXRealDirectDepartment(userId, month, year, xQD, ref xOff, ref hourX, ref Ho,
                    ref countNotCheckin, ref countNotCheckout, ref countLateIn, ref countEarlyOut);
            }
            //xReal = GetXReal(userId, month, year, xQD, ref xOff, ref hourX, ref countNotCheckin, ref countNotCheckout, ref countLateIn, ref countEarlyOut);

            oWorkSheet.Cells[indexRow, 4] = xQD <= 0 ? "" : xQD.ToString(StringFormat.FormatCurrencyFinal);
            oWorkSheet.Cells[indexRow, 5] = hourXQD <= 0 ? "" : hourXQD.ToString(StringFormat.FormatCurrencyFinal);
            oWorkSheet.Cells[indexRow, 6] = xReal <= 0 ? "" : xReal.ToString(StringFormat.FormatMark);
            oWorkSheet.Cells[indexRow, 7] = Ho <= 0 ? "" : Ho.ToString(StringFormat.FormatMark);
            oWorkSheet.Cells[indexRow, 8] = xOff <= 0 ? "" : xOff.ToString(StringFormat.FormatMark);
            oWorkSheet.Cells[indexRow, 9] = hourX <= 0 ? "" : hourX.ToString(StringFormat.FormatMark);
            oWorkSheet.Cells[indexRow, 10] = countNotCheckin <= 0
                ? ""
                : countNotCheckin.ToString(StringFormat.FormatCurrencyFinal);
            oWorkSheet.Cells[indexRow, 11] = countNotCheckout <= 0
                ? ""
                : countNotCheckout.ToString(StringFormat.FormatCurrencyFinal);
            oWorkSheet.Cells[indexRow, 12] = countLateIn <= 0
                ? ""
                : countLateIn.ToString(StringFormat.FormatCurrencyFinal);
            oWorkSheet.Cells[indexRow, 13] = countEarlyOut <= 0
                ? ""
                : countEarlyOut.ToString(StringFormat.FormatCurrencyFinal);

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            #endregion

            for (var i = 1; i < dt.Rows.Count; i++)
            {
                userId = 0;
                deptId = 0;
                rootId = 0;

                xQD = 0;
                xReal = 0;
                hourXQD = 0;
                hourX = 0;
                Ho = 0;
                xOff = 0;
                countNotCheckin = 0;
                countNotCheckout = 0;
                countLateIn = 0;
                countEarlyOut = 0;

                var dr = dt.Rows[i];
                var dr_1 = dt.Rows[i - 1];
                rootId = dr[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID] == DBNull.Value
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
                userId = dr[EmployeeKeys.FIELD_EMPLOYEES_USERID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[EmployeeKeys.FIELD_EMPLOYEES_USERID].ToString());
                oWorkSheet.Cells[indexRow, 2] = StringFormat.GetUserCode(userId);
                oWorkSheet.Cells[indexRow, 3] = dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();
                deptId = dr[DepartmentKeys.FIELD_DEPARTMENT_ID] == DBNull.Value
                    ? 0
                    : (int) dr[DepartmentKeys.FIELD_DEPARTMENT_ID];
                /////////////////////////////////////////////////////////////////
                xQD = int.Parse(GetXQD(deptId, month, year).ToString());


                if ((rootId == 2) || (rootId == 3) || (rootId == 4) || (rootId == 58) || (rootId == 59))
                {
                    hourXQD = xQD*8;
                    xReal = GetXReal(userId, month, year, xQD, ref xOff, ref hourX, ref countNotCheckin,
                        ref countNotCheckout, ref countLateIn, ref countEarlyOut);
                }
                else
                {
                    hourXQD = xQD*7;
                    xReal = GetXRealDirectDepartment(userId, month, year, xQD, ref xOff, ref hourX, ref Ho,
                        ref countNotCheckin, ref countNotCheckout, ref countLateIn, ref countEarlyOut);
                }
                //xReal = GetXReal(userId, month, year, xQD, ref xOff, ref hourX, ref countNotCheckin, ref countNotCheckout, ref countLateIn, ref countEarlyOut);

                oWorkSheet.Cells[indexRow, 4] = xQD <= 0 ? "" : xQD.ToString(StringFormat.FormatCurrencyFinal);
                oWorkSheet.Cells[indexRow, 5] = hourXQD <= 0 ? "" : hourXQD.ToString(StringFormat.FormatCurrencyFinal);
                oWorkSheet.Cells[indexRow, 6] = xReal <= 0 ? "" : xReal.ToString(StringFormat.FormatMark);
                oWorkSheet.Cells[indexRow, 7] = Ho <= 0 ? "" : Ho.ToString(StringFormat.FormatMark);
                oWorkSheet.Cells[indexRow, 8] = xOff <= 0 ? "" : xOff.ToString(StringFormat.FormatMark);
                oWorkSheet.Cells[indexRow, 9] = hourX <= 0 ? "" : hourX.ToString(StringFormat.FormatMark);
                oWorkSheet.Cells[indexRow, 10] = countNotCheckin <= 0
                    ? ""
                    : countNotCheckin.ToString(StringFormat.FormatCurrencyFinal);
                oWorkSheet.Cells[indexRow, 11] = countNotCheckout <= 0
                    ? ""
                    : countNotCheckout.ToString(StringFormat.FormatCurrencyFinal);
                oWorkSheet.Cells[indexRow, 12] = countLateIn <= 0
                    ? ""
                    : countLateIn.ToString(StringFormat.FormatCurrencyFinal);
                oWorkSheet.Cells[indexRow, 13] = countEarlyOut <= 0
                    ? ""
                    : countEarlyOut.ToString(StringFormat.FormatCurrencyFinal);
            }

            //Range rangeSTT = oWorkSheet.get_Range("A6", "C" + indexRow);
            //rangeSTT.Font.Size = 10;
            //rangeSTT.Font.Name = "Times New Roman";
            //rangeSTT.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            //Range rangeName = oWorkSheet.get_Range("D6", "D" + indexRow);
            //rangeName.Font.Size = 10;
            //rangeName.Font.Name = "Times New Roman";
            //rangeSTT.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

            //Range range = oWorkSheet.get_Range("E6", "AU" + indexRow);
            //range.Font.Size = 10;
            //range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //range.Font.Name = "Times New Roman";

            var range = oWorkSheet.get_Range("A" + (initTitleIndex - 1), "M" + indexRow);
            range.Font.Size = 10;
            range.Font.Name = "Times New Roman";
            range.Borders.LineStyle = Constants.xlSolid;
        }

        private static void CreateHeaderAndTitleKhoiVanPhong(ref _Worksheet oWorkSheet, ref int initTitleIndex,
            int month, int year)
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

            var rangeHeader3 = oWorkSheet.get_Range("A4", "M4");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "BÁO CÁO CHẤM CÔNG BẰNG DẤU VÂN TAY KHỐI VĂN PHÒNG THÁNG " + month + "/" + year;
            rangeHeader3.Font.Size = 16;
            rangeHeader3.Font.Bold = true;
            rangeHeader3.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader3.Font.Name = "Times New Roman";

            #endregion

            #region Create Title for Account List from oWorkSheet

            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            var rangeHeaderColunm = oWorkSheet.get_Range("A" + (initTitleIndex - 1), "A" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);

            oWorkSheet.Cells[initTitleIndex, 2] = "Mã NV";
            rangeHeaderColunm = oWorkSheet.get_Range("B" + (initTitleIndex - 1), "B" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);

            oWorkSheet.Cells[initTitleIndex, 3] = "Họ Tên";
            rangeHeaderColunm = oWorkSheet.get_Range("C" + (initTitleIndex - 1), "C" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 30;

            oWorkSheet.Cells[initTitleIndex, 4] = "Ngày Công Quy Định";
            rangeHeaderColunm = oWorkSheet.get_Range("D" + (initTitleIndex - 1), "D" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            rangeHeaderColunm.WrapText = true;

            oWorkSheet.Cells[initTitleIndex, 5] = "Giờ Công Quy Định";
            rangeHeaderColunm = oWorkSheet.get_Range("E" + (initTitleIndex - 1), "E" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            rangeHeaderColunm.WrapText = true;

            oWorkSheet.Cells[initTitleIndex, 6] = "Ngày Công Thực Tế";
            rangeHeaderColunm = oWorkSheet.get_Range("F" + (initTitleIndex - 1), "F" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            rangeHeaderColunm.WrapText = true;

            oWorkSheet.Cells[initTitleIndex, 7] = "Ngày Công Học";
            rangeHeaderColunm = oWorkSheet.get_Range("G" + (initTitleIndex - 1), "G" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            rangeHeaderColunm.WrapText = true;

            oWorkSheet.Cells[initTitleIndex, 8] = "Số Ngày nghỉ";
            rangeHeaderColunm = oWorkSheet.get_Range("H" + (initTitleIndex - 1), "H" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            rangeHeaderColunm.WrapText = true;

            oWorkSheet.Cells[initTitleIndex, 9] = "Giờ Công Thực Tế";
            rangeHeaderColunm = oWorkSheet.get_Range("I" + (initTitleIndex - 1), "I" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            rangeHeaderColunm.WrapText = true;


            oWorkSheet.Cells[initTitleIndex - 1, 10] = "Số ngày Không điểm danh";
            rangeHeaderColunm = oWorkSheet.get_Range("J" + (initTitleIndex - 1), "K" + (initTitleIndex - 1));
            rangeHeaderColunm.Merge(Type.Missing);
            rangeHeaderColunm.WrapText = true;

            oWorkSheet.Cells[initTitleIndex, 10] = "Vào";
            oWorkSheet.Cells[initTitleIndex, 11] = "Ra";

            oWorkSheet.Cells[initTitleIndex - 1, 12] = "Số ngày đi trễ, về sớm 15 phút";
            rangeHeaderColunm = oWorkSheet.get_Range("L" + (initTitleIndex - 1), "M" + (initTitleIndex - 1));
            rangeHeaderColunm.Merge(Type.Missing);
            rangeHeaderColunm.WrapText = true;


            oWorkSheet.Cells[initTitleIndex, 12] = "Vào";
            oWorkSheet.Cells[initTitleIndex, 13] = "Ra";

            var rangeHeaderColunm1 = oWorkSheet.get_Range("A" + (initTitleIndex - 1), "M" + initTitleIndex);
            rangeHeaderColunm1.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeaderColunm1.VerticalAlignment = XlVAlign.xlVAlignCenter;
            rangeHeaderColunm1.RowHeight = 15;
            rangeHeaderColunm1.RowHeight = 30;

            #endregion
        }

        #endregion
    }
}