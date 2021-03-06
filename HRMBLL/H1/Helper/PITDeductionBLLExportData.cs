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
    public class PITDeductionBLLExportData
    {
        public static string ExcelByFilter(string fullName, int rootId, string pathName)
        {
            ///////////////////////////////////////////
            var dt = new PITDeductionDAL().GetByFilter(fullName, rootId);
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

                    oWorkSheet.Name = "GiamTruGiaCanh";

                    InsertDataToWorkSheet(dt, ref oWorkSheet);

                    oExcel.Visible = false;
                    oExcel.UserControl = false;
                    fileName = pathName + "\\GiamTruGiaCanh.xls";
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

            var dr0 = dt.Rows[0];

            var rangeDept = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangeDept.Merge(Type.Missing);
            oWorkSheet.Cells[indexRow, 1] = dr0[PITDeductionKeys.Field_PITDeduction_RootName] == DBNull.Value
                ? string.Empty
                : dr0[PITDeductionKeys.Field_PITDeduction_RootName].ToString().ToUpper();
            rangeDept.Font.Bold = true;


            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            var userid0 = dr0[PITDeductionKeys.Field_PITDeduction_UserId] == DBNull.Value
                ? 0
                : int.Parse(dr0[PITDeductionKeys.Field_PITDeduction_UserId].ToString());
            oWorkSheet.Cells[indexRow, 2] = StringFormat.GetUserCode(userid0);
            oWorkSheet.Cells[indexRow, 3] = dr0[PITDeductionKeys.Field_PITDeduction_FullName] == DBNull.Value
                ? string.Empty
                : dr0[PITDeductionKeys.Field_PITDeduction_FullName].ToString();
            oWorkSheet.Cells[indexRow, 4] = dr0[PITDeductionKeys.Field_PITDeduction_RFullName] == DBNull.Value
                ? string.Empty
                : dr0[PITDeductionKeys.Field_PITDeduction_RFullName].ToString();

            var rDayOfBirth0 = dr0[EmployeeRelationKeys.Field_EmployeeRelation_RDayOfBirth] == DBNull.Value
                ? 0
                : int.Parse(dr0[EmployeeRelationKeys.Field_EmployeeRelation_RDayOfBirth].ToString());
            var rMonthOfBirth0 = dr0[EmployeeRelationKeys.Field_EmployeeRelation_RMonthOfBirth] == DBNull.Value
                ? 0
                : int.Parse(dr0[EmployeeRelationKeys.Field_EmployeeRelation_RMonthOfBirth].ToString());
            var rYearOfBirth0 = dr0[EmployeeRelationKeys.Field_EmployeeRelation_RYearOfBirth] == DBNull.Value
                ? 0
                : int.Parse(dr0[EmployeeRelationKeys.Field_EmployeeRelation_RYearOfBirth].ToString());
            var birthDay0 = string.Empty;
            if ((rDayOfBirth0 > 0) && (rMonthOfBirth0 > 0) && (rYearOfBirth0 > 0))
                birthDay0 = rDayOfBirth0 + "/" + rMonthOfBirth0 + "/" + rYearOfBirth0;
            else if ((rMonthOfBirth0 > 0) && (rYearOfBirth0 > 0))
                birthDay0 = rMonthOfBirth0 + "/" + rYearOfBirth0;
            else if (rYearOfBirth0 > 0)
                birthDay0 = rYearOfBirth0.ToString();


            oWorkSheet.Cells[indexRow, 5] = birthDay0;

            oWorkSheet.Cells[indexRow, 6] = dr0[PITDeductionKeys.Field_PITDeduction_RelationTypeName] == DBNull.Value
                ? string.Empty
                : dr0[PITDeductionKeys.Field_PITDeduction_RelationTypeName].ToString();
            oWorkSheet.Cells[indexRow, 7] = dr0[PITDeductionKeys.Field_PITDeduction_TaxNumber] == DBNull.Value
                ? string.Empty
                : dr0[PITDeductionKeys.Field_PITDeduction_TaxNumber].ToString();
            oWorkSheet.Cells[indexRow, 8] = dr0[PITDeductionKeys.Field_PITDeduction_Id_Passport] == DBNull.Value
                ? string.Empty
                : dr0[PITDeductionKeys.Field_PITDeduction_Id_Passport].ToString();

            var fromMonth0 = dr0[PITDeductionKeys.Field_PITDeduction_FromMonth] == DBNull.Value
                ? 0
                : int.Parse(dr0[PITDeductionKeys.Field_PITDeduction_FromMonth].ToString());
            var fromYear0 = dr0[PITDeductionKeys.Field_PITDeduction_FromYear] == DBNull.Value
                ? 0
                : int.Parse(dr0[PITDeductionKeys.Field_PITDeduction_FromYear].ToString());
            oWorkSheet.Cells[indexRow, 9] = fromMonth0 + "/" + fromYear0;

            var toMonth0 = dr0[PITDeductionKeys.Field_PITDeduction_ToMonth] == DBNull.Value
                ? 0
                : int.Parse(dr0[PITDeductionKeys.Field_PITDeduction_ToMonth].ToString());
            var toYear0 = dr0[PITDeductionKeys.Field_PITDeduction_ToYear] == DBNull.Value
                ? 0
                : int.Parse(dr0[PITDeductionKeys.Field_PITDeduction_ToYear].ToString());
            oWorkSheet.Cells[indexRow, 10] = toMonth0 + "/" + toYear0;
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
                    oWorkSheet.Cells[indexRow, 1] = dr[PITDeductionKeys.Field_PITDeduction_RootName] == DBNull.Value
                        ? string.Empty
                        : dr[PITDeductionKeys.Field_PITDeduction_RootName].ToString().ToUpper();
                    rangeDept.Font.Bold = true;
                    indexRow++;
                }


                oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                var userid = dr[PITDeductionKeys.Field_PITDeduction_UserId] == DBNull.Value
                    ? 0
                    : int.Parse(dr[PITDeductionKeys.Field_PITDeduction_UserId].ToString());
                var userid_1 = dr_1[PITDeductionKeys.Field_PITDeduction_UserId] == DBNull.Value
                    ? 0
                    : int.Parse(dr_1[PITDeductionKeys.Field_PITDeduction_UserId].ToString());
                if (userid != userid_1)
                {
                    oWorkSheet.Cells[indexRow, 2] = StringFormat.GetUserCode(userid);
                    oWorkSheet.Cells[indexRow, 3] = dr[PITDeductionKeys.Field_PITDeduction_FullName] == DBNull.Value
                        ? string.Empty
                        : dr[PITDeductionKeys.Field_PITDeduction_FullName].ToString();
                }

                oWorkSheet.Cells[indexRow, 4] = dr[PITDeductionKeys.Field_PITDeduction_RFullName] == DBNull.Value
                    ? string.Empty
                    : dr[PITDeductionKeys.Field_PITDeduction_RFullName].ToString();
                var rDayOfBirth = dr[EmployeeRelationKeys.Field_EmployeeRelation_RDayOfBirth] == DBNull.Value
                    ? 0
                    : int.Parse(dr[EmployeeRelationKeys.Field_EmployeeRelation_RDayOfBirth].ToString());
                var rMonthOfBirth = dr[EmployeeRelationKeys.Field_EmployeeRelation_RMonthOfBirth] == DBNull.Value
                    ? 0
                    : int.Parse(dr[EmployeeRelationKeys.Field_EmployeeRelation_RMonthOfBirth].ToString());
                var rYearOfBirth = dr[EmployeeRelationKeys.Field_EmployeeRelation_RYearOfBirth] == DBNull.Value
                    ? 0
                    : int.Parse(dr[EmployeeRelationKeys.Field_EmployeeRelation_RYearOfBirth].ToString());
                var birthDay = string.Empty;
                if ((rDayOfBirth > 0) && (rMonthOfBirth > 0) && (rYearOfBirth > 0))
                    birthDay = rDayOfBirth + "/" + rMonthOfBirth + "/" + rYearOfBirth;
                else if ((rMonthOfBirth > 0) && (rYearOfBirth > 0))
                    birthDay = rMonthOfBirth + "/" + rYearOfBirth;
                else if (rYearOfBirth > 0)
                    birthDay = rYearOfBirth.ToString();

                oWorkSheet.Cells[indexRow, 5] = birthDay;

                oWorkSheet.Cells[indexRow, 6] = dr[PITDeductionKeys.Field_PITDeduction_RelationTypeName] == DBNull.Value
                    ? string.Empty
                    : dr[PITDeductionKeys.Field_PITDeduction_RelationTypeName].ToString();
                oWorkSheet.Cells[indexRow, 7] = dr[PITDeductionKeys.Field_PITDeduction_TaxNumber] == DBNull.Value
                    ? string.Empty
                    : dr[PITDeductionKeys.Field_PITDeduction_TaxNumber].ToString();
                oWorkSheet.Cells[indexRow, 8] = dr[PITDeductionKeys.Field_PITDeduction_Id_Passport] == DBNull.Value
                    ? string.Empty
                    : dr[PITDeductionKeys.Field_PITDeduction_Id_Passport].ToString();
                var fromMonth = dr[PITDeductionKeys.Field_PITDeduction_FromMonth] == DBNull.Value
                    ? 0
                    : int.Parse(dr[PITDeductionKeys.Field_PITDeduction_FromMonth].ToString());
                var fromYear = dr[PITDeductionKeys.Field_PITDeduction_FromYear] == DBNull.Value
                    ? 0
                    : int.Parse(dr[PITDeductionKeys.Field_PITDeduction_FromYear].ToString());
                oWorkSheet.Cells[indexRow, 9] = fromMonth + "/" + fromYear;

                var toMonth = dr[PITDeductionKeys.Field_PITDeduction_ToMonth] == DBNull.Value
                    ? 0
                    : int.Parse(dr[PITDeductionKeys.Field_PITDeduction_ToMonth].ToString());
                var toYear = dr[PITDeductionKeys.Field_PITDeduction_ToYear] == DBNull.Value
                    ? 0
                    : int.Parse(dr[PITDeductionKeys.Field_PITDeduction_ToYear].ToString());
                oWorkSheet.Cells[indexRow, 10] = toMonth + "/" + toYear;
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

            var rangeHeader3 = oWorkSheet.get_Range("A4", "I4");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "DANH SÁCH NGƯỜI CÓ PHỤ THUỘC GIẢM TRỪ GIA CẢNH";
            rangeHeader3.Font.Size = 16;
            rangeHeader3.Font.Bold = true;
            rangeHeader3.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader3.Font.Name = "Times New Roman";

            #endregion

            #region Create Title for Account List from oWorkSheet

            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            oWorkSheet.Cells[initTitleIndex, 2] = "Mã NV";
            oWorkSheet.Cells[initTitleIndex, 3] = "Họ Tên";
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 30;
            oWorkSheet.Cells[initTitleIndex, 4] = "Họ và Tên Người Thân";
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).ColumnWidth = 30;
            oWorkSheet.Cells[initTitleIndex, 5] = "Ngày sinh";
            oWorkSheet.Cells[initTitleIndex, 6] = "Quan Hệ";
            oWorkSheet.Cells[initTitleIndex, 7] = "Mã số thuế";
            oWorkSheet.Cells[initTitleIndex, 8] = "Số CMND/Hộ chiếu";
            oWorkSheet.Cells[initTitleIndex, 9] = "Thời điểm tính giảm trừ \n(tháng/năm)";
            oWorkSheet.Cells[initTitleIndex, 10] = "Thời điểm kết thúc giảm trừ \n(tháng/năm)";
            var rangeHeader4 = oWorkSheet.get_Range("A7", "I7");
            rangeHeader4.Font.Bold = true;

            #endregion
        }
    }
}