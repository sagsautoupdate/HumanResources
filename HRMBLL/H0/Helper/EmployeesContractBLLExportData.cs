using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Excel;
using HRMDAL.H0;
using HRMUtil;
using HRMUtil.KeyNames.H0;
using Telerik.WinControls.UI;
using DataTable = System.Data.DataTable;

namespace HRMBLL.H0.Helper
{
    public class EmployeesContractBLLExportData
    {
        #region Contract Data

        public static string ExcelByFilter(string fullName, int rootId, int contractType, int typeSort, string pathName)
        {
            ///////////////////////////////////////////
            var dt = new EmployeeContractDAL().GetByFilter(fullName, rootId, contractType, typeSort);
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

                    oWorkSheet.Name = "HopDongNhanVien";

                    InsertDataToWorkSheet(dt, ref oWorkSheet);

                    oExcel.Visible = false;
                    oExcel.UserControl = false;
                    fileName = pathName + "\\HopDongNhanVien.xls";
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

        private static void InsertDataToWorkSheet(DataTable dt, ref _Worksheet oWorkSheet)
        {
            var deparmentId = 0;
            var departmentIdBefore = 0;
            var initTitleIndex = 7;
            // create Content
            var indexRow = initTitleIndex + 1;
            var orderNumber = 1;
            var value = string.Empty;

            CreateHeaderAndTitle(ref oWorkSheet, ref initTitleIndex);

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
                : dr0[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE].ToString();
            var userid0 = dr0[EmployeeKeys.FIELD_EMPLOYEES_USERID] == DBNull.Value
                ? 0
                : int.Parse(dr0[EmployeeKeys.FIELD_EMPLOYEES_USERID].ToString());
            oWorkSheet.Cells[indexRow, 3] = StringFormat.GetUserCode(userid0);
            oWorkSheet.Cells[indexRow, 4] = dr0[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                ? string.Empty
                : dr0[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();
            var sex0 = dr0[EmployeeKeys.FIELD_EMPLOYEES_SEX] == DBNull.Value
                ? -1
                : (int) dr0[EmployeeKeys.FIELD_EMPLOYEES_SEX];
            var sexName0 = string.Empty;
            if (sex0 == 1)
                sexName0 = "Nam";
            else if (sex0 == 0)
                sexName0 = "Nữ";
            oWorkSheet.Cells[indexRow, 5] = sexName0;
            oWorkSheet.Cells[indexRow, 6] = dr0[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                ? string.Empty
                : dr0[PositionKeys.FIELD_POSITION_NAME].ToString();
            oWorkSheet.Cells[indexRow, 7] = dr0[ContractTypeKeys.Field_ContractTypes_ContractTypeName] == DBNull.Value
                ? string.Empty
                : dr0[ContractTypeKeys.Field_ContractTypes_ContractTypeName].ToString();

            var fromDate0 = dr0[EmployeeContractKeys.Field_EmployeeContract_FromDate] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr0[EmployeeContractKeys.Field_EmployeeContract_FromDate].ToString());
            var toDate0 = dr0[EmployeeContractKeys.Field_EmployeeContract_ToDate] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr0[EmployeeContractKeys.Field_EmployeeContract_ToDate].ToString());
            if (fromDate0.Equals(DateTime.MinValue))
                oWorkSheet.Cells[indexRow, 8] = string.Empty;
            else
                oWorkSheet.Cells[indexRow, 8] = FormatDate.FormatVNDate(fromDate0);
            if (toDate0.Equals(DateTime.MinValue))
                oWorkSheet.Cells[indexRow, 9] = string.Empty;
            else if (toDate0.Equals(BLLHelper.DefaultValues.GetSQLDateMinValue()))
                oWorkSheet.Cells[indexRow, 9] = "Không xác định";
            else
                oWorkSheet.Cells[indexRow, 9] = FormatDate.FormatVNDate(toDate0);

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

                var sex = dr[EmployeeKeys.FIELD_EMPLOYEES_SEX] == DBNull.Value
                    ? -1
                    : (int) dr[EmployeeKeys.FIELD_EMPLOYEES_SEX];
                var sexName = string.Empty;
                if (sex == 1)
                    sexName = "Nam";
                else if (sex == 0)
                    sexName = "Nữ";
                oWorkSheet.Cells[indexRow, 5] = sexName;
                oWorkSheet.Cells[indexRow, 6] = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[PositionKeys.FIELD_POSITION_NAME].ToString();
                oWorkSheet.Cells[indexRow, 7] = dr[ContractTypeKeys.Field_ContractTypes_ContractTypeName] ==
                                                DBNull.Value
                    ? string.Empty
                    : dr[ContractTypeKeys.Field_ContractTypes_ContractTypeName].ToString();

                var fromDate = dr[EmployeeContractKeys.Field_EmployeeContract_FromDate] == DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(dr[EmployeeContractKeys.Field_EmployeeContract_FromDate].ToString());
                var toDate = dr[EmployeeContractKeys.Field_EmployeeContract_ToDate] == DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(dr[EmployeeContractKeys.Field_EmployeeContract_ToDate].ToString());
                if (fromDate.Equals(DateTime.MinValue))
                    oWorkSheet.Cells[indexRow, 8] = string.Empty;
                else
                    oWorkSheet.Cells[indexRow, 8] = FormatDate.FormatVNDate(fromDate);
                if (toDate.Equals(DateTime.MinValue))
                    oWorkSheet.Cells[indexRow, 9] = string.Empty;
                else if (toDate.Equals(BLLHelper.DefaultValues.GetSQLDateMinValue()))
                    oWorkSheet.Cells[indexRow, 9] = "Không xác định";
                else
                    oWorkSheet.Cells[indexRow, 9] = FormatDate.FormatVNDate(toDate);
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

        private static void CreateHeaderAndTitle(ref _Worksheet oWorkSheet, ref int initTitleIndex)
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
            oWorkSheet.Cells[4, 1] = "DANH SÁCH HỢP ĐỒNG NHÂN VIÊN CÔNG TY";
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
            oWorkSheet.Cells[initTitleIndex, 5] = "Giới tính";
            oWorkSheet.Cells[initTitleIndex, 6] = "Chức danh";

            oWorkSheet.Cells[initTitleIndex, 7] = "Hợp đồng";
            oWorkSheet.Cells[initTitleIndex, 8] = "Từ ngày";
            oWorkSheet.Cells[initTitleIndex, 9] = "Đến ngày";

            #endregion
        }

        #endregion

        #region ExpirbaleContract Data

        //public static string ExcelByFilterExpirbaleContract(string fullName, string rootId, DateTime expireDate, int typeSort, string pathName)
        //{
        //    ///////////////////////////////////////////
        //    System.Data.DataTable dt = new EmployeeContractDAL().RemindExpiredConstracts(fullName, rootId, expireDate, typeSort);
        //    ///////////////////////////////////////////

        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        Excel._Application oExcel;
        //        Excel._Workbook oWorkBook;
        //        Excel._Worksheet oWorkSheet;

        //        string fileName = string.Empty;
        //        try
        //        {
        //            GC.Collect();
        //            oExcel = new Excel.Application();
        //            oExcel.Visible = false;

        //            // Get new workbook
        //            oWorkBook = (Excel._Workbook)(oExcel.Workbooks.Add(Missing.Value));
        //            oWorkSheet = (Excel._Worksheet)oWorkBook.Worksheets[1];

        //            oWorkSheet.Name = "HopDongNhanVienDenHan";

        //            InsertDataToWorkSheetExpirbaleContract(dt, ref oWorkSheet);

        //            oExcel.Visible = false;
        //            oExcel.UserControl = false;
        //            fileName = pathName + "\\HopDongNhanVienDenHan.xls";
        //            if (File.Exists(fileName))
        //                File.Delete(fileName);
        //            oWorkBook.SaveAs(fileName, Excel.XlFileFormat.xlWorkbookNormal, null, null, false, false, Excel.XlSaveAsAccessMode.xlShared, false, false, null, null, null);

        //            // Need all following code to clean up and extingush all references!!!
        //            oWorkBook.Close(null, null, null);
        //            oExcel.Workbooks.Close();
        //            oExcel.Quit();
        //            //System.Runtime.InteropServices.Marshal.ReleaseComObject(oRange);
        //            System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel);
        //            System.Runtime.InteropServices.Marshal.ReleaseComObject(oWorkSheet);

        //            System.Runtime.InteropServices.Marshal.ReleaseComObject(oWorkBook);
        //            oWorkSheet = null;
        //            oWorkSheet = null;
        //            oExcel = null;
        //            GC.Collect();  // force final cleanup!

        //            return fileName;

        //        }
        //        catch (Exception ex)
        //        {
        //            String errorMessage;
        //            errorMessage = "Error: ";
        //            errorMessage = String.Concat(errorMessage, ex.Message);
        //            errorMessage = String.Concat(errorMessage, " Line: ");
        //            errorMessage = String.Concat(errorMessage, ex.Source);
        //            throw new Exception(errorMessage);
        //        }
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        public static _Application ExcelByFilterExpirbaleContract(RadGridView exportDt, string fullName, string pathName)
        {
            ///////////////////////////////////////////
            var dt = exportDt;
            // new EmployeeContractDAL().RemindExpiredConstracts(fullName, rootId, expireDate, typeSort);
            ///////////////////////////////////////////

            if (dt.Rows.Count > 0)
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

                    oWorkSheet.Name = "DANH_SACH_HOP_DONG";

                    InsertDataToWorkSheetExpirbaleContract(dt, ref oWorkSheet);

                    //oExcel.Visible = false;
                    //oExcel.UserControl = false;
                    //fileName = pathName + "\\HopDongNhanVienDenHan.xls";
                    //if (File.Exists(fileName))
                    //    File.Delete(fileName);
                    //oWorkBook.SaveAs(fileName, Excel.XlFileFormat.xlWorkbookNormal, null, null, false, false, Excel.XlSaveAsAccessMode.xlShared, false, false, null, null, null);

                    //// Need all following code to clean up and extingush all references!!!
                    //oWorkBook.Close(null, null, null);
                    //oExcel.Workbooks.Close();
                    //oExcel.Quit();
                    ////System.Runtime.InteropServices.Marshal.ReleaseComObject(oRange);
                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel);
                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(oWorkSheet);

                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(oWorkBook);
                    //oWorkSheet = null;
                    //oWorkSheet = null;
                    //oExcel = null;
                    //GC.Collect();  // force final cleanup!

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

        private static void InsertDataToWorkSheetExpirbaleContract(RadGridView dt, ref _Worksheet oWorkSheet)
        {
            var deparmentId = 0;
            var departmentIdBefore = 0;
            var initTitleIndex = 7;
            // create Content
            var indexRow = initTitleIndex + 1;
            var orderNumber = 1;
            var value = string.Empty;

            CreateHeaderAndTitleExpirbaleContract(ref oWorkSheet, ref initTitleIndex);

            #region Create FirstRow For Director, Administration and Training Department from oWorkSheet

            var dr0 = dt.Rows[0];
            var rangeDept = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangeDept.Merge(Type.Missing);
            oWorkSheet.Cells[indexRow, 1] = (dr0.Cells["RootName"].Value == DBNull.Value) ||
                                            (dr0.Cells["RootName"].Value.ToString().Length <= 0)
                ? string.Empty
                : dr0.Cells["RootName"].Value.ToString().ToUpper();
            rangeDept.Font.Bold = true;

            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            oWorkSheet.Cells[indexRow, 2] = (dr0.Cells["EmployeeCode"].Value == DBNull.Value) ||
                                            (dr0.Cells["EmployeeCode"].Value.ToString().Length <= 0)
                ? string.Empty
                : dr0.Cells["EmployeeCode"].Value.ToString();
            var userid0 = (dr0.Cells["UserId"].Value == DBNull.Value) ||
                          (dr0.Cells["UserId"].Value.ToString().Length <= 0)
                ? 0
                : int.Parse(dr0.Cells["UserId"].Value.ToString());
            oWorkSheet.Cells[indexRow, 3] = StringFormat.GetUserCode(userid0);
            oWorkSheet.Cells[indexRow, 4] = (dr0.Cells["FullName"].Value == DBNull.Value) ||
                                            (dr0.Cells["FullName"].Value.ToString().Length <= 0)
                ? string.Empty
                : dr0.Cells["FullName"].Value.ToString();
            var sex0 = (dr0.Cells["Sex"].Value == DBNull.Value) || (dr0.Cells["Sex"].Value.ToString().Length <= 0)
                ? -1
                : (int) dr0.Cells["Sex"].Value;
            var sexName0 = string.Empty;
            if (sex0 == 1)
                sexName0 = "Nam";
            else if (sex0 == 0)
                sexName0 = "Nữ";

            oWorkSheet.Cells[indexRow, 5] = sexName0;
            oWorkSheet.Cells[indexRow, 6] = (dr0.Cells["PositionName"].Value == DBNull.Value) ||
                                            (dr0.Cells["PositionName"].Value.ToString().Length <= 0)
                ? string.Empty
                : dr0.Cells["PositionName"].Value.ToString();
            oWorkSheet.Cells[indexRow, 7] = (dr0.Cells["ContractTypeName"].Value == DBNull.Value) ||
                                            (dr0.Cells["ContractTypeName"].Value.ToString().Length <= 0)
                ? string.Empty
                : dr0.Cells["ContractTypeName"].Value.ToString();

            var fromDate0 = (dr0.Cells["FromDate"].Value == DBNull.Value) ||
                            (dr0.Cells["FromDate"].Value.ToString().Length <= 0)
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr0.Cells["FromDate"].Value.ToString());
            var toDate0 = (dr0.Cells["ToDate"].Value == DBNull.Value) ||
                          (dr0.Cells["ToDate"].Value.ToString().Length <= 0)
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr0.Cells["ToDate"].Value.ToString());
            if (fromDate0.Equals(DateTime.MinValue))
                oWorkSheet.Cells[indexRow, 8] = string.Empty;
            else
                oWorkSheet.Cells[indexRow, 8] = FormatDate.FormatVNDate(fromDate0);
            if (toDate0.Equals(DateTime.MinValue))
                oWorkSheet.Cells[indexRow, 9] = string.Empty;
            else if (toDate0.Equals(BLLHelper.DefaultValues.GetSQLDateMinValue()))
                oWorkSheet.Cells[indexRow, 9] = "Không xác định";
            else
                oWorkSheet.Cells[indexRow, 9] = FormatDate.FormatVNDate(toDate0);

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            #endregion

            for (var i = 1; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                var dr_1 = dt.Rows[i - 1];
                var rootId = (dr.Cells["RootId"].Value == DBNull.Value) ||
                             (dr.Cells["RootId"].Value.ToString().Length <= 0)
                    ? 0
                    : int.Parse(dr.Cells["RootId"].Value.ToString());
                var rootId_1 = (dr_1.Cells["RootId"].Value == DBNull.Value) ||
                               (dr_1.Cells["RootId"].Value.ToString().Length <= 0)
                    ? 0
                    : int.Parse(dr_1.Cells["RootId"].Value.ToString());

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
                    oWorkSheet.Cells[indexRow, 1] = (dr.Cells["RootName"].Value == DBNull.Value) ||
                                                    (dr.Cells["RootName"].Value.ToString().Length <= 0)
                        ? string.Empty
                        : dr.Cells["RootName"].Value.ToString().ToUpper();
                    rangeDept.Font.Bold = true;
                    indexRow++;
                }

                if (rootId != rootId_1)
                {
                    rangeDept = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
                    rangeDept.Merge(Type.Missing);
                    oWorkSheet.Cells[indexRow, 1] = (dr.Cells["RootName"].Value == DBNull.Value) ||
                                                    (dr.Cells["RootName"].Value.ToString().Length <= 0)
                        ? string.Empty
                        : dr.Cells["RootName"].Value.ToString().ToUpper();
                    rangeDept.Font.Bold = true;
                    indexRow++;
                }

                oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                oWorkSheet.Cells[indexRow, 2] = (dr.Cells["EmployeeCode"].Value == DBNull.Value) ||
                                                (dr.Cells["EmployeeCode"].Value.ToString().Length <= 0)
                    ? string.Empty
                    : dr.Cells["EmployeeCode"].Value.ToString();
                var userid = (dr.Cells["UserId"].Value == DBNull.Value) ||
                             (dr.Cells["UserID"].Value.ToString().Length <= 0)
                    ? 0
                    : int.Parse(dr.Cells["UserId"].Value.ToString());
                oWorkSheet.Cells[indexRow, 3] = StringFormat.GetUserCode(userid);
                oWorkSheet.Cells[indexRow, 4] = (dr.Cells["FullName"].Value == DBNull.Value) ||
                                                (dr.Cells["FullName"].Value.ToString().Length <= 0)
                    ? string.Empty
                    : dr.Cells["FullName"].Value.ToString();

                var sex = (dr.Cells["Sex"].Value == DBNull.Value) || (dr.Cells["Sex"].Value.ToString().Length <= 0)
                    ? -1
                    : (int) dr.Cells["Sex"].Value;
                var sexName = string.Empty;
                if (sex == 1)
                    sexName = "Nam";
                else if (sex == 0)
                    sexName = "Nữ";
                oWorkSheet.Cells[indexRow, 5] = sexName;
                oWorkSheet.Cells[indexRow, 6] = (dr.Cells["PositionName"].Value == DBNull.Value) ||
                                                (dr.Cells["PositionName"].Value.ToString().Length <= 0)
                    ? string.Empty
                    : dr.Cells["PositionName"].Value.ToString();
                oWorkSheet.Cells[indexRow, 7] = (dr.Cells["ContractTypeName"].Value == DBNull.Value) ||
                                                (dr.Cells["ContractTypeName"].Value.ToString().Length <= 0)
                    ? string.Empty
                    : dr.Cells["ContractTypeName"].Value.ToString();

                var fromDate = (dr.Cells["FromDate"].Value == DBNull.Value) ||
                               (dr.Cells["FromDate"].Value.ToString().Length <= 0)
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(dr.Cells["FromDate"].Value.ToString());
                var toDate = (dr.Cells["ToDate"].Value == DBNull.Value) ||
                             (dr.Cells["ToDate"].Value.ToString().Length <= 0)
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(dr.Cells["ToDate"].Value.ToString());
                if (fromDate.Equals(DateTime.MinValue))
                    oWorkSheet.Cells[indexRow, 8] = string.Empty;
                else
                    oWorkSheet.Cells[indexRow, 8] = FormatDate.FormatVNDate(fromDate);
                if (toDate.Equals(DateTime.MinValue))
                    oWorkSheet.Cells[indexRow, 9] = string.Empty;
                else if (toDate.Equals(BLLHelper.DefaultValues.GetSQLDateMinValue()))
                    oWorkSheet.Cells[indexRow, 9] = "Không xác định";
                else
                    oWorkSheet.Cells[indexRow, 9] = FormatDate.FormatVNDate(toDate);
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

        private static void CreateHeaderAndTitleExpirbaleContract(ref _Worksheet oWorkSheet, ref int initTitleIndex)
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
            oWorkSheet.Cells[4, 1] = "DANH SÁCH HỢP ĐỒNG NHÂN VIÊN ĐẾN HẠN";
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
            oWorkSheet.Cells[initTitleIndex, 5] = "Giới tính";
            oWorkSheet.Cells[initTitleIndex, 6] = "Chức danh";

            oWorkSheet.Cells[initTitleIndex, 7] = "Hợp đồng";
            oWorkSheet.Cells[initTitleIndex, 8] = "Từ ngày";
            oWorkSheet.Cells[initTitleIndex, 9] = "Đến ngày";

            #endregion
        }

        #endregion

        #region ChangedContract Data

        public static string ExcelByFilterChangedContract(string fullName, int rootId, int month, int year, int typeSort,
            string pathName)
        {
            ///////////////////////////////////////////
            var dt = new EmployeeContractDAL().ChangedConstracts(fullName, rootId, month, year, typeSort);
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

                    oWorkSheet.Name = "HopDongNhanVienDaChuyenDoi";

                    InsertDataToWorkSheetChangedContract(dt, ref oWorkSheet);

                    oExcel.Visible = false;
                    oExcel.UserControl = false;
                    fileName = pathName + "\\HopDongNhanVienDaChuyenDoi.xls";
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

        private static void InsertDataToWorkSheetChangedContract(DataTable dt, ref _Worksheet oWorkSheet)
        {
            var deparmentId = 0;
            var departmentIdBefore = 0;
            var initTitleIndex = 7;
            // create Content
            var indexRow = initTitleIndex + 1;
            var orderNumber = 1;
            var value = string.Empty;

            CreateHeaderAndTitleChangedContract(ref oWorkSheet, ref initTitleIndex);

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
            var sex0 = dr0[EmployeeKeys.FIELD_EMPLOYEES_SEX] == DBNull.Value
                ? -1
                : (int) dr0[EmployeeKeys.FIELD_EMPLOYEES_SEX];
            var sexName0 = string.Empty;
            if (sex0 == 1)
                sexName0 = "Nam";
            else if (sex0 == 0)
                sexName0 = "Nữ";

            oWorkSheet.Cells[indexRow, 5] = sexName0;
            oWorkSheet.Cells[indexRow, 6] = dr0[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                ? string.Empty
                : dr0[PositionKeys.FIELD_POSITION_NAME].ToString();
            oWorkSheet.Cells[indexRow, 7] = dr0[ContractTypeKeys.Field_ContractTypes_ContractTypeName] == DBNull.Value
                ? string.Empty
                : dr0[ContractTypeKeys.Field_ContractTypes_ContractTypeName].ToString();

            var fromDate0 = dr0[EmployeeContractKeys.Field_EmployeeContract_FromDate] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr0[EmployeeContractKeys.Field_EmployeeContract_FromDate].ToString());
            var toDate0 = dr0[EmployeeContractKeys.Field_EmployeeContract_ToDate] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr0[EmployeeContractKeys.Field_EmployeeContract_ToDate].ToString());
            if (fromDate0.Equals(DateTime.MinValue))
                oWorkSheet.Cells[indexRow, 8] = string.Empty;
            else
                oWorkSheet.Cells[indexRow, 8] = FormatDate.FormatVNDate(fromDate0);
            if (toDate0.Equals(DateTime.MinValue))
                oWorkSheet.Cells[indexRow, 9] = string.Empty;
            else if (toDate0.Equals(BLLHelper.DefaultValues.GetSQLDateMinValue()))
                oWorkSheet.Cells[indexRow, 9] = "Không xác định";
            else
                oWorkSheet.Cells[indexRow, 9] = FormatDate.FormatVNDate(toDate0);

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

                var sex = dr[EmployeeKeys.FIELD_EMPLOYEES_SEX] == DBNull.Value
                    ? -1
                    : (int) dr[EmployeeKeys.FIELD_EMPLOYEES_SEX];
                var sexName = string.Empty;
                if (sex == 1)
                    sexName = "Nam";
                else if (sex == 0)
                    sexName = "Nữ";
                oWorkSheet.Cells[indexRow, 5] = sexName;
                oWorkSheet.Cells[indexRow, 6] = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[PositionKeys.FIELD_POSITION_NAME].ToString();
                oWorkSheet.Cells[indexRow, 7] = dr[ContractTypeKeys.Field_ContractTypes_ContractTypeName] ==
                                                DBNull.Value
                    ? string.Empty
                    : dr[ContractTypeKeys.Field_ContractTypes_ContractTypeName].ToString();

                var fromDate = dr[EmployeeContractKeys.Field_EmployeeContract_FromDate] == DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(dr[EmployeeContractKeys.Field_EmployeeContract_FromDate].ToString());
                var toDate = dr[EmployeeContractKeys.Field_EmployeeContract_ToDate] == DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(dr[EmployeeContractKeys.Field_EmployeeContract_ToDate].ToString());
                if (fromDate.Equals(DateTime.MinValue))
                    oWorkSheet.Cells[indexRow, 8] = string.Empty;
                else
                    oWorkSheet.Cells[indexRow, 8] = FormatDate.FormatVNDate(fromDate);
                if (toDate.Equals(DateTime.MinValue))
                    oWorkSheet.Cells[indexRow, 9] = string.Empty;
                else if (toDate.Equals(BLLHelper.DefaultValues.GetSQLDateMinValue()))
                    oWorkSheet.Cells[indexRow, 9] = "Không xác định";
                else
                    oWorkSheet.Cells[indexRow, 9] = FormatDate.FormatVNDate(toDate);
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

        private static void CreateHeaderAndTitleChangedContract(ref _Worksheet oWorkSheet, ref int initTitleIndex)
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
            oWorkSheet.Cells[4, 1] = "DANH SÁCH HỢP ĐỒNG NHÂN VIÊN ĐÃ CHUYỂN ĐỔI";
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
            oWorkSheet.Cells[initTitleIndex, 5] = "Giới tính";
            oWorkSheet.Cells[initTitleIndex, 6] = "Chức danh";

            oWorkSheet.Cells[initTitleIndex, 7] = "Hợp đồng";
            oWorkSheet.Cells[initTitleIndex, 8] = "Từ ngày";
            oWorkSheet.Cells[initTitleIndex, 9] = "Đến ngày";

            #endregion
        }

        #endregion
    }
}