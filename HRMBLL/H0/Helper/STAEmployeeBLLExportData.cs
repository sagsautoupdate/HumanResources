using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Excel;
using HRMDAL.H0.Statistic;
using HRMUtil;
using Constants = Excel.Constants;
using DataTable = System.Data.DataTable;

namespace HRMBLL.H0.Helper
{
    public class STAEmployeeBLLExportData
    {
        #region Position Data

        public static string StatisticHumanResource(DateTime FromDateA, DateTime ToDateA, DateTime FromDateB,
            DateTime ToDateB, string pathName)
        {
            ///////////////////////////////////////////
            var dt = new STAEmployeeDAL().StatisticHumanResource(FromDateA, ToDateA, FromDateB, ToDateB);
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

                    oWorkSheet.Name = "BCNS";


                    var rangeHeader4 = oWorkSheet.get_Range("A5", "F5");
                    rangeHeader4.Merge(Type.Missing);
                    oWorkSheet.Cells[5, 1] = "( Tính Đến Ngày " + FormatDate.FormatVNDate(ToDateB) + " )";
                    rangeHeader4.Font.Size = 11;
                    rangeHeader4.Font.Bold = true;
                    rangeHeader4.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    rangeHeader4.Font.Name = "Times New Roman";

                    InsertDataToWorkSheetStatisticHumanResource(dt, ref oWorkSheet);

                    oExcel.Visible = false;
                    oExcel.UserControl = false;
                    fileName = pathName + "\\BCNS.xls";
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

        private static void InsertDataToWorkSheetStatisticHumanResource(DataTable dt, ref _Worksheet oWorkSheet)
        {
            var rootId = 0;
            var departmentId = 0;
            var parentId = 0;

            var initTitleIndex = 7;
            // create Content
            var indexRow = initTitleIndex;
            //int orderNumber = 1;
            double r = 0;
            double totalA = 0;
            double totalB = 0;
            var departmentName = "";

            CreateHeaderAndTitleStatisticHumanResource(ref oWorkSheet, ref initTitleIndex);

            var _OrderNumberRoot = 0;
            var _OrderNumberLevel1 = 0;
            var _OrderNumberLevel2 = 0;

            double _SagsTotalA = 0;
            double _SagsTotalB = 0;
            Range rangeDept;

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                indexRow++;


                rootId = dr["RootId"] == DBNull.Value ? 0 : int.Parse(dr["RootId"].ToString());
                departmentId = dr["DepartmentId"] == DBNull.Value ? 0 : int.Parse(dr["DepartmentId"].ToString());
                parentId = dr["ParentId"] == DBNull.Value ? 0 : int.Parse(dr["ParentId"].ToString());
                departmentName = dr["DepartmentName"] == DBNull.Value ? string.Empty : (string) dr["DepartmentName"];
                totalA = dr["TotalA"] == DBNull.Value ? 0 : int.Parse(dr["TotalA"].ToString());
                totalB = dr["TotalB"] == DBNull.Value ? 0 : int.Parse(dr["TotalB"].ToString());

                if (rootId > 0)
                {
                    if ((totalB > 0) && (totalA > 0))
                        r = totalB/totalA*100;
                    if (r > 0)
                        oWorkSheet.Cells[indexRow, 6] = r.ToString(StringFormat.FormatCurrencyFinal) + "(%)";

                    rangeDept = oWorkSheet.get_Range("A" + indexRow, "F" + indexRow);
                    rangeDept.Font.Bold = true;
                }

                if ((rootId > 0) && (parentId <= 0))
                {
                    _SagsTotalA = _SagsTotalA + totalA;
                    _SagsTotalB = _SagsTotalB + totalB;
                    _OrderNumberRoot++;
                    _OrderNumberLevel1 = 0;
                    oWorkSheet.Cells[indexRow, 1] = _OrderNumberRoot.ToString();
                    oWorkSheet.Cells[indexRow, 2] = departmentName.ToUpper();
                }
                else if ((rootId > 0) && (rootId == parentId))
                {
                    _OrderNumberLevel1++;
                    _OrderNumberLevel2 = 0;
                    oWorkSheet.Cells[indexRow, 1] = _OrderNumberRoot + "." + _OrderNumberLevel1;
                    oWorkSheet.Cells[indexRow, 2] = departmentName;
                }
                else if ((rootId > 0) && (rootId != parentId) && (parentId > 0))
                {
                    _OrderNumberLevel2++;
                    oWorkSheet.Cells[indexRow, 1] = _OrderNumberRoot + "." + _OrderNumberLevel1 + "." +
                                                    _OrderNumberLevel2;
                    oWorkSheet.Cells[indexRow, 2] = departmentName;
                }
                else
                {
                    oWorkSheet.Cells[indexRow, 2] = departmentName;
                }


                if (totalA > 0)
                    oWorkSheet.Cells[indexRow, 3] = totalA;
                if (totalB > 0)
                    oWorkSheet.Cells[indexRow, 4] = totalB;
            }

            /////////////////////////////////////////////////////////////////
            indexRow++;
            oWorkSheet.Cells[indexRow, 2] = "SAGS";
            if (_SagsTotalA > 0)
                oWorkSheet.Cells[indexRow, 3] = _SagsTotalA;
            if (_SagsTotalB > 0)
                oWorkSheet.Cells[indexRow, 4] = _SagsTotalB;

            if ((_SagsTotalB > 0) && (_SagsTotalA > 0))
                r = _SagsTotalB/_SagsTotalA*100;
            if (r > 0)
                oWorkSheet.Cells[indexRow, 6] = r.ToString(StringFormat.FormatCurrencyFinal) + "(%)";

            var rangeFooter = oWorkSheet.get_Range("A" + indexRow, "F" + indexRow);
            rangeFooter.Font.Bold = true;

            /////////////////////////////////////////////////////////////////

            var rangeSTT = oWorkSheet.get_Range("A7", "A" + indexRow);
            rangeSTT.HorizontalAlignment = XlHAlign.xlHAlignCenter;


            var range = oWorkSheet.get_Range("A7", "F" + indexRow);
            range.Font.Name = "Times New Roman";
            range.Borders.LineStyle = Constants.xlSolid;
        }

        private static void CreateHeaderAndTitleStatisticHumanResource(ref _Worksheet oWorkSheet, ref int initTitleIndex)
        {
            #region create header for printing from oWorkSheet

            var rangeHeader1 = oWorkSheet.get_Range("A1", "B1");
            rangeHeader1.Merge(Type.Missing);
            oWorkSheet.Cells[1, 1] = "TỔNG CÔNG TY CẢNG HÀNG KHÔNG MIỀN NAM";
            rangeHeader1.Font.Size = 12;
            rangeHeader1.Font.Bold = true;
            rangeHeader1.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader1.Font.Name = "Times New Roman";

            var rangeHeader2 = oWorkSheet.get_Range("A2", "B2");
            rangeHeader2.Merge(Type.Missing);
            oWorkSheet.Cells[2, 1] = "CÔNG TY PHỤC VỤ MẶT ĐẤT SÀI GÒN";
            rangeHeader2.Font.Size = 12;
            rangeHeader2.Font.Bold = true;
            rangeHeader2.Font.Underline = true;
            rangeHeader2.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader2.Font.Name = "Times New Roman";

            var rangeHeader3 = oWorkSheet.get_Range("A4", "F4");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "BÁO CÁO NHÂN SỰ";
            rangeHeader3.Font.Size = 16;
            rangeHeader3.Font.Bold = true;
            rangeHeader3.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader3.Font.Name = "Times New Roman";

            #endregion

            #region Create Title for Account List from oWorkSheet

            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            oWorkSheet.Cells[initTitleIndex, 2] = "CHỨC DANH";
            ((Range) oWorkSheet.Cells[initTitleIndex, 2]).ColumnWidth = 60;
            oWorkSheet.Cells[initTitleIndex, 3] = "Tổng A";
            oWorkSheet.Cells[initTitleIndex, 4] = "Tổng B";
            oWorkSheet.Cells[initTitleIndex, 5] = "GHI CHÚ";
            ((Range) oWorkSheet.Cells[initTitleIndex, 5]).ColumnWidth = 30;
            oWorkSheet.Cells[initTitleIndex, 6] = "SO VỚI CÙNG\nKỲ THÁMG TRƯỚC";
            ((Range) oWorkSheet.Cells[initTitleIndex, 6]).ColumnWidth = 20;

            var rangeHeader = oWorkSheet.get_Range("A7", "F7");
            rangeHeader.Font.Bold = true;

            #endregion
        }

        #endregion
    }
}