using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Excel;
using DataTable = System.Data.DataTable;

namespace HRMBLL.H1.Helper
{
    public class IncomeBLLExportData
    {
        public static string ExcelByFilter(string fullName, int rootId, int month, int year, string pathName)
        {
            ///////////////////////////////////////////
            DataTable dt = null; // new IncomeEmployeesDAL().GetByFilter(fullName, rootId, month, year);
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

                    oWorkSheet.Name = "BangLuong_" + month + "_" + year;

                    InsertDataToWorkSheet(dt, ref oWorkSheet, month, year);

                    oExcel.Visible = false;
                    oExcel.UserControl = false;
                    fileName = pathName + "\\BangLuongThang_" + month + "_" + year + ".xls";
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
            //int deparmentId = 0;
            //int departmentIdBefore = 0;
            //int initTitleIndex = 8;
            //// create Content
            //int indexRow = initTitleIndex + 1;
            //int orderNumber = 1;
            //string value = string.Empty;

            //CreateHeaderAndTitle(ref oWorkSheet, ref initTitleIndex, month, year);

            //#region Create FirstRow For Director, Administration and Training Department from oWorkSheet

            //DataRow dr0 = dt.Rows[0];

            //Range rangeDept = oWorkSheet.get_Range("A" + indexRow, "C" + indexRow);
            //rangeDept.Merge(Type.Missing);
            //oWorkSheet.Cells[indexRow, 1] = dr0[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME] == DBNull.Value ? string.Empty : dr0[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME].ToString().ToUpper();
            //rangeDept.Font.Bold = true;
            //rangeDept.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

            //indexRow++;
            //oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            //oWorkSheet.Cells[indexRow, 2] = dr0[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE] == DBNull.Value ? string.Empty : dr0[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE].ToString();
            //oWorkSheet.Cells[indexRow, 3] = dr0[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value ? string.Empty : dr0[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();

            //oWorkSheet.Cells[indexRow, 4] = dr0[IncomeKeys.Field_Income_LNS] == DBNull.Value ? string.Empty : Convert.ToDecimal(dr0[IncomeKeys.Field_Income_LNS].ToString()) == 0 ? string.Empty : Convert.ToDecimal(dr0[IncomeKeys.Field_Income_LNS].ToString()).ToString(StringFormat.FormatCurrencyFinal);
            //oWorkSheet.Cells[indexRow, 5] = dr0[IncomeKeys.Field_Income_LCBNN] == DBNull.Value ? string.Empty : Convert.ToDecimal(dr0[IncomeKeys.Field_Income_LCBNN].ToString()) == 0 ? string.Empty : Convert.ToDecimal(dr0[IncomeKeys.Field_Income_LCBNN].ToString()).ToString(StringFormat.FormatCurrencyFinal);

            //oWorkSheet.Cells[indexRow, 6] = dr0[IncomeKeys.Field_Income_PCCV] == DBNull.Value ? string.Empty : Convert.ToDecimal(dr0[IncomeKeys.Field_Income_PCCV].ToString()) == 0 ? string.Empty : Convert.ToDecimal(dr0[IncomeKeys.Field_Income_PCCV].ToString()).ToString(StringFormat.FormatCurrencyFinal);
            //oWorkSheet.Cells[indexRow, 7] = dr0[IncomeKeys.Field_Income_PCTN] == DBNull.Value ? string.Empty : Convert.ToDecimal(dr0[IncomeKeys.Field_Income_PCTN].ToString()) == 0 ? string.Empty : Convert.ToDecimal(dr0[IncomeKeys.Field_Income_PCTN].ToString()).ToString(StringFormat.FormatCurrencyFinal);
            //oWorkSheet.Cells[indexRow, 8] = dr0[IncomeKeys.Field_Income_PCDH] == DBNull.Value ? string.Empty : Convert.ToDecimal(dr0[IncomeKeys.Field_Income_PCDH].ToString()) == 0 ? string.Empty : Convert.ToDecimal(dr0[IncomeKeys.Field_Income_PCDH].ToString()).ToString(StringFormat.FormatCurrencyFinal);
            //oWorkSheet.Cells[indexRow, 9] = string.Empty;
            //oWorkSheet.Cells[indexRow, 10] = string.Empty;
            //oWorkSheet.Cells[indexRow, 11] = dr0[IncomeKeys.Field_Income_TienAn] == DBNull.Value ? string.Empty : Convert.ToDecimal(dr0[IncomeKeys.Field_Income_TienAn].ToString()) == 0 ? string.Empty : Convert.ToDecimal(dr0[IncomeKeys.Field_Income_TienAn].ToString()).ToString(StringFormat.FormatCurrencyFinal);
            //oWorkSheet.Cells[indexRow, 12] = dr0[IncomeKeys.Field_Income_BoSungLuong] == DBNull.Value ? string.Empty : Convert.ToDecimal(dr0[IncomeKeys.Field_Income_BoSungLuong].ToString()) == 0 ? string.Empty : Convert.ToDecimal(dr0[IncomeKeys.Field_Income_BoSungLuong].ToString()).ToString(StringFormat.FormatCurrencyFinal);
            //oWorkSheet.Cells[indexRow, 13] = dr0[IncomeKeys.Field_Income_TienThemGio] == DBNull.Value ? string.Empty : Convert.ToDecimal(dr0[IncomeKeys.Field_Income_TienThemGio].ToString()) == 0 ? string.Empty : Convert.ToDecimal(dr0[IncomeKeys.Field_Income_TienThemGio].ToString()).ToString(StringFormat.FormatCurrencyFinal);
            //oWorkSheet.Cells[indexRow, 14] = dr0[IncomeKeys.Field_Income_TienLamDem] == DBNull.Value ? string.Empty : Convert.ToDecimal(dr0[IncomeKeys.Field_Income_TienLamDem].ToString()) == 0 ? string.Empty : Convert.ToDecimal(dr0[IncomeKeys.Field_Income_TienLamDem].ToString()).ToString(StringFormat.FormatCurrencyFinal);
            //oWorkSheet.Cells[indexRow, 15] = dr0[IncomeKeys.Field_Income_TienThuong] == DBNull.Value ? string.Empty : Convert.ToDecimal(dr0[IncomeKeys.Field_Income_TienThuong].ToString()) == 0 ? string.Empty : Convert.ToDecimal(dr0[IncomeKeys.Field_Income_TienThuong].ToString()).ToString(StringFormat.FormatCurrencyFinal);

            //decimal _totalIncome = dr0[IncomeKeys.Field_Income_TotalIncome] == DBNull.Value ? 0 : Convert.ToDecimal(dr0[IncomeKeys.Field_Income_TotalIncome].ToString());
            //oWorkSheet.Cells[indexRow, 16] = _totalIncome == 0 ? string.Empty : _totalIncome.ToString(StringFormat.FormatCurrencyFinal);

            //decimal _trBHXH = dr0[IncomeKeys.Field_Income_TrBHXH] == DBNull.Value ? 0 : Convert.ToDecimal(dr0[IncomeKeys.Field_Income_TrBHXH].ToString());
            //oWorkSheet.Cells[indexRow, 17] = _trBHXH == 0 ? string.Empty : _trBHXH.ToString(StringFormat.FormatCurrencyFinal);
            //decimal _trBHYT = dr0[IncomeKeys.Field_Income_TrBHYT] == DBNull.Value ? 0 : Convert.ToDecimal(dr0[IncomeKeys.Field_Income_TrBHYT].ToString());
            //oWorkSheet.Cells[indexRow, 18] = _trBHYT == 0 ? string.Empty : _trBHYT.ToString(StringFormat.FormatCurrencyFinal);
            //decimal _trBHTN = dr0[IncomeKeys.Field_Income_TrBHTN] == DBNull.Value ? 0 : Convert.ToDecimal(dr0[IncomeKeys.Field_Income_TrBHTN].ToString());
            //oWorkSheet.Cells[indexRow, 19] = _trBHTN == 0 ? string.Empty : _trBHTN.ToString(StringFormat.FormatCurrencyFinal);
            //decimal _trDPCD = dr0[IncomeKeys.Field_Income_TrDoanPhi] == DBNull.Value ? 0 : Convert.ToDecimal(dr0[IncomeKeys.Field_Income_TrDoanPhi].ToString());
            //oWorkSheet.Cells[indexRow, 20] = _trDPCD == 0 ? string.Empty : _trDPCD.ToString(StringFormat.FormatCurrencyFinal);
            //decimal _TTN = dr0[IncomeKeys.Field_Income_ThueThuNhap] == DBNull.Value ? 0 : Convert.ToDecimal(dr0[IncomeKeys.Field_Income_ThueThuNhap].ToString());
            //oWorkSheet.Cells[indexRow, 21] = _TTN == 0 ? string.Empty : _TTN.ToString(StringFormat.FormatCurrencyFinal);

            //decimal _totalContribution = _trBHXH + _trBHYT + _trDPCD + _trBHTN;//_TTN;
            //decimal _thanhtien = _totalIncome - _totalContribution;

            //oWorkSheet.Cells[indexRow, 22] = _thanhtien == 0 ? string.Empty : _thanhtien.ToString(StringFormat.FormatCurrencyFinal);

            ////decimal _TotalShortTerm = dr0[IncomeKeys.Field_Income_TotalShortTerm] == DBNull.Value ? 0 : Convert.ToDecimal(dr0[IncomeKeys.Field_Income_TotalShortTerm].ToString());
            ////oWorkSheet.Cells[indexRow, 22] = _TotalShortTerm == 0 ? string.Empty : _TotalShortTerm.ToString(StringFormat.FormatCurrencyFinal);

            //#endregion

            //for (int i = 1; i < dt.Rows.Count; i++)
            //{
            //    DataRow dr = dt.Rows[i];
            //    DataRow dr_1 = dt.Rows[i - 1];
            //    int rootId = dr[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID] == DBNull.Value ? 0 : int.Parse(dr[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID].ToString());
            //    int rootId_1 = dr_1[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID] == DBNull.Value ? 0 : int.Parse(dr_1[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID].ToString());

            //    indexRow++;
            //    if (i < dt.Rows.Count - 1 && i > 1)
            //    {
            //        deparmentId = rootId;
            //        departmentIdBefore = rootId_1;
            //    }

            //    if ((deparmentId != departmentIdBefore))
            //    {
            //        rangeDept = oWorkSheet.get_Range("A" + indexRow, "C" + indexRow);
            //        rangeDept.Merge(Type.Missing);
            //        oWorkSheet.Cells[indexRow, 1] = dr[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME] == DBNull.Value ? string.Empty : dr[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME].ToString().ToUpper();
            //        rangeDept.Font.Bold = true;
            //        rangeDept.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            //        indexRow++;
            //    }

            //    oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            //    oWorkSheet.Cells[indexRow, 2] = dr[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE] == DBNull.Value ? string.Empty : dr[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE].ToString();
            //    oWorkSheet.Cells[indexRow, 3] = dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value ? string.Empty : dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();

            //    oWorkSheet.Cells[indexRow, 4] = dr[IncomeKeys.Field_Income_LNS] == DBNull.Value ? string.Empty : Convert.ToDecimal(dr[IncomeKeys.Field_Income_LNS].ToString()) == 0 ? string.Empty : Convert.ToDecimal(dr[IncomeKeys.Field_Income_LNS].ToString()).ToString(StringFormat.FormatCurrencyFinal);
            //    oWorkSheet.Cells[indexRow, 5] = dr[IncomeKeys.Field_Income_LCBNN] == DBNull.Value ? string.Empty : Convert.ToDecimal(dr[IncomeKeys.Field_Income_LCBNN].ToString()) == 0 ? string.Empty : Convert.ToDecimal(dr[IncomeKeys.Field_Income_LCBNN].ToString()).ToString(StringFormat.FormatCurrencyFinal);

            //    oWorkSheet.Cells[indexRow, 6] = dr[IncomeKeys.Field_Income_PCCV] == DBNull.Value ? string.Empty : Convert.ToDecimal(dr[IncomeKeys.Field_Income_PCCV].ToString()) == 0 ? string.Empty : Convert.ToDecimal(dr[IncomeKeys.Field_Income_PCCV].ToString()).ToString(StringFormat.FormatCurrencyFinal);
            //    oWorkSheet.Cells[indexRow, 7] = dr[IncomeKeys.Field_Income_PCTN] == DBNull.Value ? string.Empty : Convert.ToDecimal(dr[IncomeKeys.Field_Income_PCTN].ToString()) == 0 ? string.Empty : Convert.ToDecimal(dr[IncomeKeys.Field_Income_PCTN].ToString()).ToString(StringFormat.FormatCurrencyFinal);
            //    oWorkSheet.Cells[indexRow, 8] = dr[IncomeKeys.Field_Income_PCDH] == DBNull.Value ? string.Empty : Convert.ToDecimal(dr[IncomeKeys.Field_Income_PCDH].ToString()) == 0 ? string.Empty : Convert.ToDecimal(dr[IncomeKeys.Field_Income_PCDH].ToString()).ToString(StringFormat.FormatCurrencyFinal);
            //    oWorkSheet.Cells[indexRow, 9] = string.Empty;
            //    oWorkSheet.Cells[indexRow, 10] = string.Empty;
            //    oWorkSheet.Cells[indexRow, 11] = dr[IncomeKeys.Field_Income_TienAn] == DBNull.Value ? string.Empty : Convert.ToDecimal(dr[IncomeKeys.Field_Income_TienAn].ToString()) == 0 ? string.Empty : Convert.ToDecimal(dr[IncomeKeys.Field_Income_TienAn].ToString()).ToString(StringFormat.FormatCurrencyFinal);
            //    oWorkSheet.Cells[indexRow, 12] = dr[IncomeKeys.Field_Income_BoSungLuong] == DBNull.Value ? string.Empty : Convert.ToDecimal(dr[IncomeKeys.Field_Income_BoSungLuong].ToString()) == 0 ? string.Empty : Convert.ToDecimal(dr[IncomeKeys.Field_Income_BoSungLuong].ToString()).ToString(StringFormat.FormatCurrencyFinal);
            //    oWorkSheet.Cells[indexRow, 13] = dr[IncomeKeys.Field_Income_TienThemGio] == DBNull.Value ? string.Empty : Convert.ToDecimal(dr[IncomeKeys.Field_Income_TienThemGio].ToString()) == 0 ? string.Empty : Convert.ToDecimal(dr[IncomeKeys.Field_Income_TienThemGio].ToString()).ToString(StringFormat.FormatCurrencyFinal);
            //    oWorkSheet.Cells[indexRow, 14] = dr[IncomeKeys.Field_Income_TienLamDem] == DBNull.Value ? string.Empty : Convert.ToDecimal(dr[IncomeKeys.Field_Income_TienLamDem].ToString()) == 0 ? string.Empty : Convert.ToDecimal(dr[IncomeKeys.Field_Income_TienLamDem].ToString()).ToString(StringFormat.FormatCurrencyFinal);
            //    oWorkSheet.Cells[indexRow, 15] = dr[IncomeKeys.Field_Income_TienThuong] == DBNull.Value ? string.Empty : Convert.ToDecimal(dr[IncomeKeys.Field_Income_TienThuong].ToString()) == 0 ? string.Empty : Convert.ToDecimal(dr[IncomeKeys.Field_Income_TienThuong].ToString()).ToString(StringFormat.FormatCurrencyFinal);

            //    decimal totalIncome = dr[IncomeKeys.Field_Income_TotalIncome] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeKeys.Field_Income_TotalIncome].ToString());
            //    oWorkSheet.Cells[indexRow, 16] = totalIncome == 0 ? string.Empty : totalIncome.ToString(StringFormat.FormatCurrencyFinal);

            //    decimal trBHXH = dr[IncomeKeys.Field_Income_TrBHXH] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeKeys.Field_Income_TrBHXH].ToString());
            //    oWorkSheet.Cells[indexRow, 17] = trBHXH == 0 ? string.Empty : trBHXH.ToString(StringFormat.FormatCurrencyFinal);
            //    decimal trBHYT = dr[IncomeKeys.Field_Income_TrBHYT] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeKeys.Field_Income_TrBHYT].ToString());
            //    oWorkSheet.Cells[indexRow, 18] = trBHYT == 0 ? string.Empty : trBHYT.ToString(StringFormat.FormatCurrencyFinal);
            //    decimal trDPCD = dr[IncomeKeys.Field_Income_TrDoanPhi] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeKeys.Field_Income_TrDoanPhi].ToString());
            //    oWorkSheet.Cells[indexRow, 19] = trDPCD == 0 ? string.Empty : trDPCD.ToString(StringFormat.FormatCurrencyFinal);
            //    decimal trBHTN = dr[IncomeKeys.Field_Income_TrBHTN] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeKeys.Field_Income_TrBHTN].ToString());
            //    oWorkSheet.Cells[indexRow, 20] = trBHTN == 0 ? string.Empty : trBHTN.ToString(StringFormat.FormatCurrencyFinal);
            //    decimal TTN = dr[IncomeKeys.Field_Income_ThueThuNhap] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeKeys.Field_Income_ThueThuNhap].ToString());
            //    oWorkSheet.Cells[indexRow, 21] = TTN == 0 ? string.Empty : TTN.ToString(StringFormat.FormatCurrencyFinal);

            //    decimal totalContribution = trBHXH + trBHYT + trDPCD + trBHTN;//TTN;
            //    decimal thanhtien = totalIncome - totalContribution;

            //    oWorkSheet.Cells[indexRow, 22] = thanhtien == 0 ? string.Empty : thanhtien.ToString(StringFormat.FormatCurrencyFinal);

            //    //decimal TotalShortTerm = dr[IncomeKeys.Field_Income_TotalShortTerm] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeKeys.Field_Income_TotalShortTerm].ToString());
            //    //oWorkSheet.Cells[indexRow, 22] = TotalShortTerm == 0 ? string.Empty : TotalShortTerm.ToString(StringFormat.FormatCurrencyFinal);

            //}

            //Range rangeSTT = oWorkSheet.get_Range("A7", "A" + indexRow);
            //rangeSTT.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            ////Range rangeName = oWorkSheet.get_Range("B7", "B" + indexRow);
            ////rangeName.Font.Size = 10;
            ////rangeName.Font.Name = "Times New Roman";

            //Range range = oWorkSheet.get_Range("A7", "V" + indexRow);
            //range.Font.Size = 10;            
            //range.Font.Name = "Times New Roman";
            //range.Borders.LineStyle = Excel.Constants.xlSolid;
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

            var rangeHeader3 = oWorkSheet.get_Range("A4", "T4");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "BẢNG TIỀN LƯƠNG, THƯỞNG THÁNG " + month + " NĂM " + year;
            rangeHeader3.Font.Size = 16;
            rangeHeader3.Font.Bold = true;
            rangeHeader3.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader3.Font.Name = "Times New Roman";

            #endregion

            #region Create Title for Account List from oWorkSheet

            var rangeHeaderPhuCap = oWorkSheet.get_Range("F7", "H7");
            oWorkSheet.Cells[7, 6] = "Các Khoản Phụ Cấp";
            rangeHeaderPhuCap.Merge(Type.Missing);

            var rangeHeaderPhaiNop = oWorkSheet.get_Range("Q7", "U7");
            oWorkSheet.Cells[7, 17] = "Các Khoản Phải Nộp";
            rangeHeaderPhaiNop.Merge(Type.Missing);

            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            var rangeHeaderColunm = oWorkSheet.get_Range("A7", "A8");
            rangeHeaderColunm.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 2] = "Mã SAC";
            rangeHeaderColunm = oWorkSheet.get_Range("B7", "B8");
            rangeHeaderColunm.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 3] = "Họ Tên";
            rangeHeaderColunm = oWorkSheet.get_Range("C7", "C8");
            rangeHeaderColunm.Merge(Type.Missing);
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 30;
            oWorkSheet.Cells[initTitleIndex, 4] = "Lương\nnăng suất";
            rangeHeaderColunm = oWorkSheet.get_Range("D7", "D8");
            rangeHeaderColunm.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 5] = "Lương\ncơ bản";
            rangeHeaderColunm = oWorkSheet.get_Range("E7", "E8");
            rangeHeaderColunm.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 6] = "PCCV";
            oWorkSheet.Cells[initTitleIndex, 7] = "PCTN";
            oWorkSheet.Cells[initTitleIndex, 8] = "PCDH";
            oWorkSheet.Cells[initTitleIndex, 9] = "Trợ cấp\nBHXH";
            rangeHeaderColunm = oWorkSheet.get_Range("I7", "I8");
            rangeHeaderColunm.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 10] = "Thưởng\nATHK";
            rangeHeaderColunm = oWorkSheet.get_Range("J7", "J8");
            rangeHeaderColunm.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 11] = "Ăn\ngiữa ca";
            rangeHeaderColunm = oWorkSheet.get_Range("K7", "K8");
            rangeHeaderColunm.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 12] = "Bổ sung\nlương";
            rangeHeaderColunm = oWorkSheet.get_Range("L7", "L8");
            rangeHeaderColunm.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 13] = "Tiền\nthêm giờ";
            rangeHeaderColunm = oWorkSheet.get_Range("M7", "M8");
            rangeHeaderColunm.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 14] = "Tiền\nlàm đêm";
            rangeHeaderColunm = oWorkSheet.get_Range("N7", "N8");
            rangeHeaderColunm.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 15] = "Tiền\nthưởng";
            //rangeHeaderColunm = oWorkSheet.get_Range("O7", "o8");            
            //rangeHeaderColunm.Merge(Type.Missing);

            oWorkSheet.Cells[initTitleIndex, 16] = "Tổng";
            rangeHeaderColunm = oWorkSheet.get_Range("P7", "P8");
            rangeHeaderColunm.Merge(Type.Missing);

            oWorkSheet.Cells[initTitleIndex, 17] = "BHXH";
            oWorkSheet.Cells[initTitleIndex, 18] = "BHYT";
            oWorkSheet.Cells[initTitleIndex, 19] = "BHTN";
            oWorkSheet.Cells[initTitleIndex, 20] = "ĐPCĐ";
            oWorkSheet.Cells[initTitleIndex, 21] = "TTN";


            oWorkSheet.Cells[initTitleIndex, 22] = "Thành tiền";
            rangeHeaderColunm = oWorkSheet.get_Range("V7", "V8");
            rangeHeaderColunm.Merge(Type.Missing);

            var rangeHeader4 = oWorkSheet.get_Range("A7", "T7");
            rangeHeader4.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader4.Font.Name = "Times New Roman";

            #endregion
        }
    }
}