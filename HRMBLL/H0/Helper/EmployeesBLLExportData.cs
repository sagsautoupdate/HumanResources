using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Excel;
using HRMDAL.H0;
using HRMUtil;
using HRMUtil.KeyNames.H0;
using Constants = HRMUtil.Constants;
using DataTable = System.Data.DataTable;

namespace HRMBLL.H0.Helper
{
    public class EmployeesBLLExportData
    {
        #region employee Data

        public static _Application ExcelByFilter(string deptIds, int rootId, string fullname, string pathName)
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

                    InsertDataToWorkSheet(dt, ref oWorkSheet);

                    oExcel.Visible = false;
                    oExcel.UserControl = false;
                    //fileName = pathName + "\\DSNhanVien.xls";
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

                    //return fileName;
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

        private static void InsertDataToWorkSheet(DataTable dt, ref _Worksheet oWorkSheet)
        {
            //int deparmentId = 0;
            //int departmentIdBefore = 0;
            var initTitleIndex = 7;
            // create Content
            var indexRow = initTitleIndex + 1;
            var orderNumber = 1;
            var value = string.Empty;
            double totalAllMonth = 0;
            double totalMonthOfTotalYear = 0;
            double totalMonth = 0;
            double totalYear = 0;
            var sTotalYear = string.Empty;
            string[] arrTotalYear = null;
            var temp = DateTime.Now;

            var EducationLevel = string.Empty;
            List<EmployeeEducationLevelsBLL> listEEL = null;
            List<EmployeeJobHistoryBLL> listH = null;
            var birthDay = DateTime.Now;
            var joinAviationDate = DateTime.Now;
            var joinCompanyDate = DateTime.Now;
            var issueDateIDCard = DateTime.Now;
            var LeaveDate = DateTime.Now;

            CreateHeaderAndTitle(ref oWorkSheet, ref initTitleIndex);

            #region Create FirstRow For Director, Administration and Training Department from oWorkSheet

            //Range rangeDept = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            //rangeDept.Merge(Type.Missing);
            //oWorkSheet.Cells[indexRow, 1] = "BAN GIÁM ĐỐC";
            //rangeDept.Font.Bold = true;

            //DataRow dr0 = dt.Rows[0];

            //indexRow++;
            //oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            //oWorkSheet.Cells[indexRow, 2] = dr0[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE] == DBNull.Value ? string.Empty : dr0[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE].ToString();
            //int userid0 = dr0[EmployeeKeys.FIELD_EMPLOYEES_USERID] == DBNull.Value ? 0 : int.Parse(dr0[EmployeeKeys.FIELD_EMPLOYEES_USERID].ToString());
            //oWorkSheet.Cells[indexRow, 3] = StringFormat.GetUserCode(userid0);
            //oWorkSheet.Cells[indexRow, 4] = dr0[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value ? string.Empty : dr0[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();
            //int sex0 = dr0[EmployeeKeys.FIELD_EMPLOYEES_SEX] == DBNull.Value ? -1 : (int)dr0[EmployeeKeys.FIELD_EMPLOYEES_SEX];
            //string sexName0 = string.Empty;
            //if(sex0 == 1)
            //    sexName0 = "Nam";
            //else if(sex0 == 0)
            //    sexName0 = "Nữ";
            //oWorkSheet.Cells[indexRow, 5] = sexName0;
            //oWorkSheet.Cells[indexRow, 6] = dr0[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value ? string.Empty : dr0[PositionKeys.FIELD_POSITION_NAME].ToString();

            //DateTime birthDay = dr0[EmployeeKeys.FIELD_EMPLOYEES_BIRTHDAY] == DBNull.Value ? FormatDate.GetSQLDateMinValue : Convert.ToDateTime(dr0[EmployeeKeys.FIELD_EMPLOYEES_BIRTHDAY].ToString());
            //DateTime joinAviationDate = dr0[EmployeeKeys.FIELD_EMPLOYEES_JOINDATE] == DBNull.Value ? FormatDate.GetSQLDateMinValue : Convert.ToDateTime(dr0[EmployeeKeys.FIELD_EMPLOYEES_JOINDATE].ToString());
            //DateTime joinCompanyDate = dr0[EmployeeKeys.Field_Employees_JoinCompanyDate] == DBNull.Value ? FormatDate.GetSQLDateMinValue : Convert.ToDateTime(dr0[EmployeeKeys.Field_Employees_JoinCompanyDate].ToString());

            //oWorkSheet.Cells[indexRow, 7] = birthDay;
            //oWorkSheet.Cells[indexRow, 8] = joinAviationDate;
            //oWorkSheet.Cells[indexRow, 9] = joinCompanyDate;
            ////if (joinAviationDate0.Year < DateTime.Now.Year)
            ////{
            ////    oWorkSheet.Cells[indexRow, 10] = 12;
            ////}
            ////else
            ////{


            //DateTime temp = new DateTime(DateTime.Now.Year, 8, 31);
            //TimeSpan ts = temp.Subtract(joinCompanyDate);

            //totalYear = ts.TotalDays / 365.25;
            //totalAllMonth = ts.TotalDays / 30.4375;


            //string sTotalYear = totalYear.ToString();
            ////double totalYear = totalMonth / 12;
            //string[] arrTotalYear = sTotalYear.Split('.');
            //if(arrTotalYear.Length==2)
            //{
            //    totalMonthOfTotalYear = Convert.ToDouble(arrTotalYear[0]) * 12;                
            //    totalYear = Convert.ToDouble(arrTotalYear[0]);
            //    totalMonth = totalAllMonth - totalMonthOfTotalYear;
            //}
            //oWorkSheet.Cells[indexRow, 10] = totalYear;
            //if (joinCompanyDate.Day == 1)
            //{
            //    oWorkSheet.Cells[indexRow, 11] = totalMonth.ToString(StringFormat.FormatCurrencyFinal);
            //}
            //else
            //{
            //    oWorkSheet.Cells[indexRow, 11] = totalMonth.ToString().Split('.')[0];
            //}

            //oWorkSheet.Cells[indexRow, 12] = dr0[DepartmentKeys.FIELD_DEPARTMENT_DEPARTMENTFULLNAME] == DBNull.Value ? string.Empty : (string)dr0[DepartmentKeys.FIELD_DEPARTMENT_DEPARTMENTFULLNAME];


            //List<EmployeeEducationLevelsBLL> listEEL = EmployeeEducationLevelsBLL.GetById(userid0);
            //for (int i = 0; i < listEEL.Count; i++)
            //{
            //    EmployeeEducationLevelsBLL objCEL = listEEL[i];
            //    if (i == listEEL.Count - 1)
            //    {
            //        EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue;
            //        if (objCEL.Remark.Trim().Length > 0)
            //        {
            //            EducationLevel += "(" + objCEL.Remark + ")";
            //        }
            //    }
            //    else
            //    {
            //        EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue;
            //        if (objCEL.Remark.Trim().Length > 0)
            //        {
            //            EducationLevel += "(" + objCEL.Remark + ")";
            //        }
            //        EducationLevel += "\n";
            //    }
            //}
            //oWorkSheet.Cells[indexRow, 13] = EducationLevel;
            //oWorkSheet.Cells[indexRow, 14] = dr0[EmployeeKeys.FIELD_EMPLOYEES_WORKED_COMPANY] == DBNull.Value ? string.Empty : dr0[EmployeeKeys.FIELD_EMPLOYEES_WORKED_COMPANY].ToString();
            var quatrinhcongtac = "";
            //List<EmployeeJobHistoryBLL> listH = EmployeeJobHistoryBLL.GetByFilter(HRMUtil.Constants.JOB_HISTORY_SELF, userid0);
            //for (int j = 0; j < listH.Count; j++)
            //{
            //    EmployeeJobHistoryBLL objH = listH[j];
            //    if (j == listH.Count - 1)
            //    {
            //        quatrinhcongtac += "- Từ " + objH.FromYear + " Đến " + objH.ToYear + ": "+objH.Infor;
            //    }
            //    else
            //    {
            //        quatrinhcongtac += "- Từ " + objH.FromYear + " Đến " + objH.ToYear + ": " + objH.Infor; 
            //        quatrinhcongtac += "\n";
            //    }
            //}
            //oWorkSheet.Cells[indexRow, 15] = quatrinhcongtac;
            //oWorkSheet.Cells[indexRow, 16] = dr0[EmployeeKeys.FIELD_EMPLOYEES_LIVE] == DBNull.Value ? string.Empty : dr0[EmployeeKeys.FIELD_EMPLOYEES_LIVE].ToString();
            //oWorkSheet.Cells[indexRow, 17] = dr0[EmployeeKeys.FIELD_EMPLOYEES_RESIDENT] == DBNull.Value ? string.Empty : dr0[EmployeeKeys.FIELD_EMPLOYEES_RESIDENT].ToString();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            #endregion

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                //DataRow dr_1 = dt.Rows[i - 1];
                //int rootId = dr[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID] == DBNull.Value ? 0 : int.Parse(dr[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID].ToString());
                //int rootId_1 = dr_1[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID] == DBNull.Value ? 0 : int.Parse(dr_1[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID].ToString());

                //indexRow++;
                //if (i < dt.Rows.Count - 1 && i > 1)
                //{
                //    deparmentId = rootId;
                //    departmentIdBefore = rootId_1;
                //}

                //if ((deparmentId != departmentIdBefore))
                //{
                //    rangeDept = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
                //    rangeDept.Merge(Type.Missing);
                //    oWorkSheet.Cells[indexRow, 1] = dr[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME] == DBNull.Value ? string.Empty : dr[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME].ToString().ToUpper();
                //    rangeDept.Font.Bold = true;

                //}


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

                birthDay = dr[EmployeeKeys.FIELD_EMPLOYEES_BIRTHDAY] == DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(dr[EmployeeKeys.FIELD_EMPLOYEES_BIRTHDAY].ToString());
                joinAviationDate = dr[EmployeeKeys.FIELD_EMPLOYEES_JOINDATE] == DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(dr[EmployeeKeys.FIELD_EMPLOYEES_JOINDATE].ToString());
                joinCompanyDate = dr[EmployeeKeys.Field_Employees_JoinCompanyDate] == DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(dr[EmployeeKeys.Field_Employees_JoinCompanyDate].ToString());
                if (birthDay.Year > 1753)
                    oWorkSheet.Cells[indexRow, 7] = birthDay;
                if (joinAviationDate.Year > 1753)
                    oWorkSheet.Cells[indexRow, 8] = joinAviationDate;
                if (joinCompanyDate.Year > 1753)
                    oWorkSheet.Cells[indexRow, 9] = joinCompanyDate;

                totalAllMonth = 0;
                totalMonthOfTotalYear = 0;
                totalMonth = 0;
                totalYear = 0;

                temp = new DateTime(DateTime.Now.Year, 8, 31);
                var ts = temp.Subtract(joinCompanyDate);

                totalYear = ts.TotalDays/365.25;
                totalAllMonth = ts.TotalDays/30.4375;


                sTotalYear = totalYear.ToString();
                //double totalYear = totalMonth / 12;
                arrTotalYear = sTotalYear.Split('.');
                if (arrTotalYear.Length == 2)
                {
                    totalMonthOfTotalYear = Convert.ToDouble(arrTotalYear[0])*12;
                    totalYear = Convert.ToDouble(arrTotalYear[0]);
                    totalMonth = totalAllMonth - totalMonthOfTotalYear;
                }

                oWorkSheet.Cells[indexRow, 10] = totalYear;

                if (joinCompanyDate.Day == 1)
                    oWorkSheet.Cells[indexRow, 11] = totalMonth.ToString("#");
                else
                    oWorkSheet.Cells[indexRow, 11] = totalMonth.ToString().Split('.')[0];

                oWorkSheet.Cells[indexRow, 12] = dr[DepartmentKeys.FIELD_DEPARTMENT_DEPARTMENTFULLNAME] == DBNull.Value
                    ? string.Empty
                    : (string) dr[DepartmentKeys.FIELD_DEPARTMENT_DEPARTMENTFULLNAME];

                EducationLevel = string.Empty;
                listEEL = EmployeeEducationLevelsBLL.GetById(userid);
                for (var j = 0; j < listEEL.Count; j++)
                {
                    var objCEL = listEEL[j];

                    if (j == listEEL.Count - 1)
                    {
                        EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue;
                        if (objCEL.Remark.Trim().Length > 0)
                            EducationLevel += "(" + objCEL.Remark + ")";
                    }
                    else
                    {
                        EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue;
                        if (objCEL.Remark.Trim().Length > 0)
                            EducationLevel += "(" + objCEL.Remark + ")";
                        EducationLevel += "\n";
                    }
                }
                oWorkSheet.Cells[indexRow, 13] = EducationLevel; //dr["HighestLevelName"].ToString();
                oWorkSheet.Cells[indexRow, 14] = dr[EmployeeKeys.FIELD_EMPLOYEES_WORKED_COMPANY] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeKeys.FIELD_EMPLOYEES_WORKED_COMPANY].ToString();
                quatrinhcongtac = "";
                listH = EmployeeJobHistoryBLL.GetByFilter(Constants.JOB_HISTORY_SELF, userid);
                for (var k = 0; k < listH.Count; k++)
                {
                    var objH = listH[k];
                    if (k == listH.Count - 1)
                    {
                        quatrinhcongtac += "- Từ " + objH.FromYear + " Đến " + objH.ToYear + ": " + objH.Infor;
                    }
                    else
                    {
                        quatrinhcongtac += "- Từ " + objH.FromYear + " Đến " + objH.ToYear + ": " + objH.Infor;
                        quatrinhcongtac += "\n";
                    }
                }
                oWorkSheet.Cells[indexRow, 15] = quatrinhcongtac;
                oWorkSheet.Cells[indexRow, 16] = dr[EmployeeKeys.FIELD_EMPLOYEES_LIVE] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeKeys.FIELD_EMPLOYEES_LIVE].ToString();
                oWorkSheet.Cells[indexRow, 17] = dr[EmployeeKeys.FIELD_EMPLOYEES_RESIDENT] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeKeys.FIELD_EMPLOYEES_RESIDENT].ToString();
                oWorkSheet.Cells[indexRow, 18] = dr[EmployeeKeys.FIELD_EMPLOYEES_IDCARD] == DBNull.Value
                    ? string.Empty
                    : "'" + dr[EmployeeKeys.FIELD_EMPLOYEES_IDCARD];

                issueDateIDCard = dr[EmployeeKeys.FIELD_EMPLOYEES_DATE_OF_ISSUE] == DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(dr[EmployeeKeys.FIELD_EMPLOYEES_DATE_OF_ISSUE].ToString());
                if (issueDateIDCard.Year > 1753)
                    oWorkSheet.Cells[indexRow, 19] = issueDateIDCard;
                oWorkSheet.Cells[indexRow, 20] = dr[EmployeeKeys.FIELD_EMPLOYEES_PLACE_OF_ISSUE] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeKeys.FIELD_EMPLOYEES_PLACE_OF_ISSUE].ToString();
                oWorkSheet.Cells[indexRow, 21] = dr[EmployeeKeys.FIELD_EMPLOYEES_HAND_PHONE] == DBNull.Value
                    ? string.Empty
                    : "'" + dr[EmployeeKeys.FIELD_EMPLOYEES_HAND_PHONE];
                oWorkSheet.Cells[indexRow, 22] = dr[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME].ToString();
                oWorkSheet.Cells[indexRow, 23] = dr[DepartmentKeys.FIELD_DEPARTMENT_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[DepartmentKeys.FIELD_DEPARTMENT_NAME].ToString();
                oWorkSheet.Cells[indexRow, 24] = dr[DepartmentKeys.FIELD_DEPARTMENT_DEPARTMENTFULLNAME] == DBNull.Value
                    ? string.Empty
                    : dr[DepartmentKeys.FIELD_DEPARTMENT_DEPARTMENTFULLNAME].ToString();


                oWorkSheet.Cells[indexRow, 25] = dr[EmployeeKeys.Field_Employees_InvestorNo] == DBNull.Value
                    ? string.Empty
                    : "SAG" + Convert.ToInt32(dr[EmployeeKeys.Field_Employees_InvestorNo]).ToString("000000000#");
                oWorkSheet.Cells[indexRow, 26] = dr[EmployeeKeys.Field_Employees_SeniorStock] == DBNull.Value
                    ? 0
                    : int.Parse(dr[EmployeeKeys.Field_Employees_SeniorStock].ToString());
                oWorkSheet.Cells[indexRow, 27] = dr[EmployeeKeys.Field_Employees_UndertakingYear] == DBNull.Value
                    ? 0
                    : int.Parse(dr[EmployeeKeys.Field_Employees_UndertakingYear].ToString());
                oWorkSheet.Cells[indexRow, 28] = dr[EmployeeKeys.Field_Employees_UnderTakingStock] == DBNull.Value
                    ? 0
                    : int.Parse(dr[EmployeeKeys.Field_Employees_UnderTakingStock].ToString());
                oWorkSheet.Cells[indexRow, 29] = dr[EmployeeKeys.Field_Employees_SeniorStockBought] == DBNull.Value
                    ? 0
                    : int.Parse(dr[EmployeeKeys.Field_Employees_SeniorStockBought].ToString());
                oWorkSheet.Cells[indexRow, 30] = dr[EmployeeKeys.Field_Employees_UnderTakingStockBought] == DBNull.Value
                    ? 0
                    : int.Parse(dr[EmployeeKeys.Field_Employees_UnderTakingStockBought].ToString());

                oWorkSheet.Cells[indexRow, 31] = dr[EmployeeKeys.Field_Employees_ConfirmStocks] == DBNull.Value
                    ? 0
                    : int.Parse(dr[EmployeeKeys.Field_Employees_ConfirmStocks].ToString());
                oWorkSheet.Cells[indexRow, 32] = dr["HighestLevelName"] == DBNull.Value
                    ? ""
                    : dr["HighestLevelName"].ToString();
                oWorkSheet.Cells[indexRow, 33] = dr["HealthInsuranceNo"] == DBNull.Value
                    ? ""
                    : "'" + dr["HealthInsuranceNo"];
                oWorkSheet.Cells[indexRow, 34] = dr["SocialInsuranceNo"] == DBNull.Value
                    ? ""
                    : "'" + dr["SocialInsuranceNo"];

                LeaveDate = dr["LeaveDate"] == DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(dr["LeaveDate"]);
                if (LeaveDate.Year > 1753)
                    oWorkSheet.Cells[indexRow, 35] = LeaveDate;
                else
                    oWorkSheet.Cells[indexRow, 35] = "";

                indexRow++;
            }

            var range = oWorkSheet.get_Range("A8", "C" + indexRow);
            range.Font.Size = 10;
            range.Font.Name = "Times New Roman";
            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            range.VerticalAlignment = XlVAlign.xlVAlignTop;

            range = oWorkSheet.get_Range("D8", "D" + indexRow);
            range.Font.Size = 10;
            range.Font.Name = "Times New Roman";
            range.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            range.VerticalAlignment = XlVAlign.xlVAlignTop;

            range = oWorkSheet.get_Range("E8", "E" + indexRow);
            range.Font.Size = 10;
            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            range.Font.Name = "Times New Roman";
            range.VerticalAlignment = XlVAlign.xlVAlignTop;

            range = oWorkSheet.get_Range("F8", "F" + indexRow);
            range.Font.Size = 10;
            range.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            range.VerticalAlignment = XlVAlign.xlVAlignTop;
            range.Font.Name = "Times New Roman";

            range = oWorkSheet.get_Range("G8", "I" + indexRow);
            range.Font.Size = 10;
            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            range.VerticalAlignment = XlVAlign.xlVAlignTop;
            range.Font.Name = "Times New Roman";

            range = oWorkSheet.get_Range("J8", "K" + indexRow);
            range.Font.Size = 10;
            range.HorizontalAlignment = XlHAlign.xlHAlignRight;
            range.VerticalAlignment = XlVAlign.xlVAlignTop;
            range.Font.Name = "Times New Roman";

            range = oWorkSheet.get_Range("L8", "O" + indexRow);
            range.Font.Size = 10;
            range.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            range.VerticalAlignment = XlVAlign.xlVAlignTop;
            range.Font.Name = "Times New Roman";

            range = oWorkSheet.get_Range("P8", "Q" + indexRow);
            range.Font.Size = 10;
            range.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            range.VerticalAlignment = XlVAlign.xlVAlignTop;
            range.Font.Name = "Times New Roman";

            range = oWorkSheet.get_Range("R8", "S" + indexRow);
            range.Font.Size = 10;
            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            range.VerticalAlignment = XlVAlign.xlVAlignTop;
            range.Font.Name = "Times New Roman";

            range = oWorkSheet.get_Range("T8", "T" + indexRow);
            range.Font.Size = 10;
            range.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            range.VerticalAlignment = XlVAlign.xlVAlignTop;
            range.Font.Name = "Times New Roman";

            range = oWorkSheet.get_Range("U8", "U" + indexRow);
            range.Font.Size = 10;
            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            range.VerticalAlignment = XlVAlign.xlVAlignTop;
            range.Font.Name = "Times New Roman";

            range = oWorkSheet.get_Range("V8", "X" + indexRow);
            range.Font.Size = 10;
            range.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            range.VerticalAlignment = XlVAlign.xlVAlignTop;
            range.Font.Name = "Times New Roman";

            range = oWorkSheet.get_Range("Y8", "Y" + indexRow);
            range.Font.Size = 10;
            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            range.VerticalAlignment = XlVAlign.xlVAlignTop;
            range.Font.Name = "Times New Roman";

            range = oWorkSheet.get_Range("Z8", "Z" + indexRow);
            range.Font.Size = 10;
            range.HorizontalAlignment = XlHAlign.xlHAlignRight;
            range.VerticalAlignment = XlVAlign.xlVAlignTop;
            range.Font.Name = "Times New Roman";

            range = oWorkSheet.get_Range("AA8", "AE" + indexRow);
            range.Font.Size = 10;
            range.HorizontalAlignment = XlHAlign.xlHAlignRight;
            range.VerticalAlignment = XlVAlign.xlVAlignTop;
            range.Font.Name = "Times New Roman";

            range = oWorkSheet.get_Range("AF8", "AF" + indexRow);
            range.Font.Size = 10;
            range.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            range.VerticalAlignment = XlVAlign.xlVAlignTop;
            range.Font.Name = "Times New Roman";

            range = oWorkSheet.get_Range("AG8", "AG" + indexRow);
            range.Font.Size = 10;
            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            range.VerticalAlignment = XlVAlign.xlVAlignTop;
            range.Font.Name = "Times New Roman";

            range = oWorkSheet.get_Range("AH8", "AH" + indexRow);
            range.Font.Size = 10;
            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            range.VerticalAlignment = XlVAlign.xlVAlignTop;
            range.Font.Name = "Times New Roman";

            range = oWorkSheet.get_Range("AI8", "AI" + indexRow);
            range.Font.Size = 10;
            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            range.VerticalAlignment = XlVAlign.xlVAlignTop;
            range.Font.Name = "Times New Roman";

            range = oWorkSheet.get_Range("A7", "K7");
            range.Font.Size = 10;
            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            range.VerticalAlignment = XlVAlign.xlVAlignCenter;
            range.Font.Name = "Times New Roman";

            range = oWorkSheet.get_Range("L7", "Q7");
            range.Font.Size = 10;
            range.HorizontalAlignment = XlHAlign.xlHAlignLeft;
            range.VerticalAlignment = XlVAlign.xlVAlignCenter;
            range.Font.Name = "Times New Roman";

            range = oWorkSheet.get_Range("R7", "AI7");
            range.Font.Size = 10;
            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            range.VerticalAlignment = XlVAlign.xlVAlignCenter;
            range.Font.Name = "Times New Roman";

            indexRow++;
        }

        private static void CreateHeaderAndTitle(ref _Worksheet oWorkSheet, ref int initTitleIndex)
        {
            #region create header for printing from oWorkSheet

            //Range rangeHeader1 = oWorkSheet.get_Range("A1", "D1");
            //rangeHeader1.Merge(Type.Missing);
            //oWorkSheet.Cells[1, 1] = "TỔNG CÔNG TY CẢNG HÀNG KHÔNG VIỆT NAM ";
            //rangeHeader1.Font.Size = 12;
            //rangeHeader1.Font.Bold = true;
            //rangeHeader1.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //rangeHeader1.Font.Name = "Times New Roman";

            var rangeHeader2 = oWorkSheet.get_Range("A1", "D1");
            rangeHeader2.Merge(Type.Missing);
            oWorkSheet.Cells[1, 1] = "CÔNG TY CỔ PHẦN PHỤC VỤ MẶT ĐẤT SÀI GÒN";
            rangeHeader2.Font.Size = 12;
            rangeHeader2.Font.Bold = true;
            rangeHeader2.Font.Underline = true;
            rangeHeader2.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader2.Font.Name = "Times New Roman";

            var rangeHeader3 = oWorkSheet.get_Range("A4", "H4");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "DANH SÁCH NHÂN VIÊN CÔNG TY";
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
            oWorkSheet.Cells[initTitleIndex, 7] = "Ngày sinh";
            oWorkSheet.Cells[initTitleIndex, 8] = "Ngày vào\nngành HK";
            oWorkSheet.Cells[initTitleIndex, 9] = "Ngày vào\ncông ty";
            oWorkSheet.Cells[initTitleIndex, 10] = "Số năm";
            oWorkSheet.Cells[initTitleIndex, 11] = "Số tháng";
            oWorkSheet.Cells[initTitleIndex, 12] = "Phòng";
            ((Range) oWorkSheet.Cells[initTitleIndex, 12]).ColumnWidth = 50;
            oWorkSheet.Cells[initTitleIndex, 13] = "Trình độ";
            ((Range) oWorkSheet.Cells[initTitleIndex, 13]).ColumnWidth = 100;
            oWorkSheet.Cells[initTitleIndex, 14] = "Trước khi vào Cty";
            ((Range) oWorkSheet.Cells[initTitleIndex, 14]).ColumnWidth = 100;
            oWorkSheet.Cells[initTitleIndex, 15] = "Quá trình Công tác";
            ((Range) oWorkSheet.Cells[initTitleIndex, 15]).ColumnWidth = 100;
            oWorkSheet.Cells[initTitleIndex, 16] = "Nơi ở hiện nay";
            ((Range) oWorkSheet.Cells[initTitleIndex, 16]).ColumnWidth = 100;
            oWorkSheet.Cells[initTitleIndex, 17] = "Hộ khẩu thường trú";
            ((Range) oWorkSheet.Cells[initTitleIndex, 17]).ColumnWidth = 100;

            oWorkSheet.Cells[initTitleIndex, 18] = "CMND";
            oWorkSheet.Cells[initTitleIndex, 19] = "CMND-Ngày cấp";
            oWorkSheet.Cells[initTitleIndex, 20] = "CMND-Nơi cấp";
            oWorkSheet.Cells[initTitleIndex, 21] = "Di động";
            oWorkSheet.Cells[initTitleIndex, 22] = "Phòng";
            oWorkSheet.Cells[initTitleIndex, 23] = "Tổ/Đội";
            oWorkSheet.Cells[initTitleIndex, 24] = "Phòng đầy đủ";

            oWorkSheet.Cells[initTitleIndex, 25] = "Mã số cổ đông";
            oWorkSheet.Cells[initTitleIndex, 26] = "CP thâm niên";
            oWorkSheet.Cells[initTitleIndex, 27] = "Năm cam kết";
            oWorkSheet.Cells[initTitleIndex, 28] = "CP cam kết";
            oWorkSheet.Cells[initTitleIndex, 29] = "CP đã mua thâm niên";
            oWorkSheet.Cells[initTitleIndex, 30] = "CP đã mua cam kết";
            oWorkSheet.Cells[initTitleIndex, 31] = "Xác nhận";
            oWorkSheet.Cells[initTitleIndex, 32] = "Trình độ cao nhất";
            oWorkSheet.Cells[initTitleIndex, 33] = "Mã số thuế";
            oWorkSheet.Cells[initTitleIndex, 34] = "Số bảo hiểm xã hội";
            oWorkSheet.Cells[initTitleIndex, 35] = "Ngày rời công ty";

            #endregion
        }

        #endregion

        #region AccountBank Employee Data

        public static string ExcelAccountBankByFilter(string deptIds, int rootId, string fullname, string pathName)
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

                    oWorkSheet.Name = "DS_TaiKhoanNhanVien";

                    InsertDataToWorkSheetAccountBank(dt, ref oWorkSheet);

                    oExcel.Visible = false;
                    oExcel.UserControl = false;
                    fileName = pathName + "\\DSTaiKhoanNhanVien.xls";
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

        private static void InsertDataToWorkSheetAccountBank(DataTable dt, ref _Worksheet oWorkSheet)
        {
            var deparmentId = 0;
            var departmentIdBefore = 0;
            var initTitleIndex = 7;
            // create Content
            var indexRow = initTitleIndex + 1;
            var orderNumber = 1;
            var value = string.Empty;

            CreateHeaderAndTitleAccountBank(ref oWorkSheet, ref initTitleIndex);

            #region Create FirstRow For Director, Administration and Training Department from oWorkSheet

            var rangeDept = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangeDept.Merge(Type.Missing);
            oWorkSheet.Cells[indexRow, 1] = "BAN GIÁM ĐỐC";
            rangeDept.Font.Bold = true;

            var dr0 = dt.Rows[0];

            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            var userid0 = dr0[EmployeeKeys.FIELD_EMPLOYEES_USERID] == DBNull.Value
                ? 0
                : int.Parse(dr0[EmployeeKeys.FIELD_EMPLOYEES_USERID].ToString());
            oWorkSheet.Cells[indexRow, 2] = "'" + StringFormat.GetUserCode(userid0);
            oWorkSheet.Cells[indexRow, 3] = dr0[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
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
            oWorkSheet.Cells[indexRow, 4] = sexName0;
            var birthDay0 = dr0[EmployeeKeys.FIELD_EMPLOYEES_BIRTHDAY] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr0[EmployeeKeys.FIELD_EMPLOYEES_BIRTHDAY].ToString());
            oWorkSheet.Cells[indexRow, 5] = FormatDate.FormatVNDate(birthDay0);

            oWorkSheet.Cells[indexRow, 6] = dr0[EmployeeKeys.FIELD_EMPLOYEES_IDCARD] == DBNull.Value
                ? string.Empty
                : dr0[EmployeeKeys.FIELD_EMPLOYEES_IDCARD].ToString();
            var IssueDateo = dr0[EmployeeKeys.FIELD_EMPLOYEES_DATE_OF_ISSUE] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr0[EmployeeKeys.FIELD_EMPLOYEES_DATE_OF_ISSUE].ToString());
            oWorkSheet.Cells[indexRow, 7] = FormatDate.FormatVNDate(IssueDateo);
            oWorkSheet.Cells[indexRow, 8] = dr0[EmployeeKeys.FIELD_EMPLOYEES_PLACE_OF_ISSUE] == DBNull.Value
                ? string.Empty
                : dr0[EmployeeKeys.FIELD_EMPLOYEES_PLACE_OF_ISSUE].ToString();


            oWorkSheet.Cells[indexRow, 9] = "'" +
                                            (dr0[EmployeeKeys.FIELD_EMPLOYEES_ACCOUNT_NO] == DBNull.Value
                                                ? string.Empty
                                                : dr0[EmployeeKeys.FIELD_EMPLOYEES_ACCOUNT_NO].ToString());
            oWorkSheet.Cells[indexRow, 10] = "'" +
                                             (dr0[EmployeeKeys.FIELD_EMPLOYEES_ATM_NO] == DBNull.Value
                                                 ? string.Empty
                                                 : dr0[EmployeeKeys.FIELD_EMPLOYEES_ATM_NO].ToString());

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
                var userid = dr[EmployeeKeys.FIELD_EMPLOYEES_USERID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[EmployeeKeys.FIELD_EMPLOYEES_USERID].ToString());
                oWorkSheet.Cells[indexRow, 2] = "'" + StringFormat.GetUserCode(userid);
                oWorkSheet.Cells[indexRow, 3] = dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
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
                oWorkSheet.Cells[indexRow, 4] = sexName;

                var birthDay = dr[EmployeeKeys.FIELD_EMPLOYEES_BIRTHDAY] == DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(dr[EmployeeKeys.FIELD_EMPLOYEES_BIRTHDAY].ToString());
                oWorkSheet.Cells[indexRow, 5] = FormatDate.FormatVNDate(birthDay);

                oWorkSheet.Cells[indexRow, 6] = dr[EmployeeKeys.FIELD_EMPLOYEES_IDCARD] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeKeys.FIELD_EMPLOYEES_IDCARD].ToString();
                var IssueDate = dr[EmployeeKeys.FIELD_EMPLOYEES_DATE_OF_ISSUE] == DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(dr[EmployeeKeys.FIELD_EMPLOYEES_DATE_OF_ISSUE].ToString());
                oWorkSheet.Cells[indexRow, 7] = FormatDate.FormatVNDate(IssueDate);
                oWorkSheet.Cells[indexRow, 8] = dr[EmployeeKeys.FIELD_EMPLOYEES_PLACE_OF_ISSUE] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeKeys.FIELD_EMPLOYEES_PLACE_OF_ISSUE].ToString();


                oWorkSheet.Cells[indexRow, 9] = "'" +
                                                (dr[EmployeeKeys.FIELD_EMPLOYEES_ACCOUNT_NO] == DBNull.Value
                                                    ? string.Empty
                                                    : dr[EmployeeKeys.FIELD_EMPLOYEES_ACCOUNT_NO].ToString());
                oWorkSheet.Cells[indexRow, 10] = "'" +
                                                 (dr[EmployeeKeys.FIELD_EMPLOYEES_ATM_NO] == DBNull.Value
                                                     ? string.Empty
                                                     : dr[EmployeeKeys.FIELD_EMPLOYEES_ATM_NO].ToString());
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

        private static void CreateHeaderAndTitleAccountBank(ref _Worksheet oWorkSheet, ref int initTitleIndex)
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
            oWorkSheet.Cells[4, 1] = "DANH SÁCH THÔNG TIN TÀI KHOẢN NHÂN VIÊN CÔNG TY";
            rangeHeader3.Font.Size = 16;
            rangeHeader3.Font.Bold = true;
            rangeHeader3.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader3.Font.Name = "Times New Roman";

            #endregion

            #region Create Title for Account List from oWorkSheet

            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            oWorkSheet.Cells[initTitleIndex, 2] = "Mã Công Ty";
            oWorkSheet.Cells[initTitleIndex, 3] = "Họ Tên";
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 30;
            oWorkSheet.Cells[initTitleIndex, 4] = "Giới tính";
            oWorkSheet.Cells[initTitleIndex, 5] = "Ngày sinh";
            oWorkSheet.Cells[initTitleIndex, 6] = "Số CMND";
            oWorkSheet.Cells[initTitleIndex, 7] = "Ngày Cấp";
            oWorkSheet.Cells[initTitleIndex, 8] = "Nơi Cấp";
            ((Range) oWorkSheet.Cells[initTitleIndex, 8]).ColumnWidth = 20;
            oWorkSheet.Cells[initTitleIndex, 9] = "Số Tài Khoản";
            ((Range) oWorkSheet.Cells[initTitleIndex, 9]).ColumnWidth = 20;
            oWorkSheet.Cells[initTitleIndex, 10] = "Số Thẻ";
            ((Range) oWorkSheet.Cells[initTitleIndex, 10]).ColumnWidth = 20;

            #endregion
        }

        #endregion
    }
}