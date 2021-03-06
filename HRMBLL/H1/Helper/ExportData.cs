using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Excel;

namespace HRMBLL.H1.Helper
{
    public class ExportData
    {
        public static string GetWorkdayEmployee(List<WorkdayEmployeesBLL> list, string pathName, int month, int year)
        {
            if ((list != null) && (list.Count > 0))
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

                    oWorkSheet.Name = "CHAMCONG";

                    InsertDataToWorkSheet(list, ref oWorkSheet, month, year);

                    oExcel.Visible = false;
                    oExcel.UserControl = false;
                    fileName = pathName + "\\BangChamCong_" + month + "_" + year + ".xls";
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

        private static void InsertDataToWorkSheet(List<WorkdayEmployeesBLL> list, ref _Worksheet oWorkSheet, int month,
            int year)
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

            var rangeDept = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangeDept.Merge(Type.Missing);
            oWorkSheet.Cells[indexRow, 1] = "BAN GIÁM ĐỐC + P.TCHC&ĐT-QLCL";
            rangeDept.Font.Bold = true;

            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            oWorkSheet.Cells[indexRow, 2] = list[0].EmployeeCode;
            oWorkSheet.Cells[indexRow, 3] = list[0].FullName;
            oWorkSheet.Cells[indexRow, 4] = list[0].PositionName;

            oWorkSheet.Cells[indexRow, 5] = list[0].Day1;
            oWorkSheet.Cells[indexRow, 6] = list[0].Day2;
            oWorkSheet.Cells[indexRow, 7] = list[0].Day3;
            oWorkSheet.Cells[indexRow, 8] = list[0].Day4;
            oWorkSheet.Cells[indexRow, 9] = list[0].Day5;
            oWorkSheet.Cells[indexRow, 10] = list[0].Day6;
            oWorkSheet.Cells[indexRow, 11] = list[0].Day7;
            oWorkSheet.Cells[indexRow, 12] = list[0].Day8;
            oWorkSheet.Cells[indexRow, 13] = list[0].Day9;
            oWorkSheet.Cells[indexRow, 14] = list[0].Day10;


            oWorkSheet.Cells[indexRow, 15] = list[0].Day11;
            oWorkSheet.Cells[indexRow, 16] = list[0].Day12;
            oWorkSheet.Cells[indexRow, 17] = list[0].Day13;
            oWorkSheet.Cells[indexRow, 18] = list[0].Day14;
            oWorkSheet.Cells[indexRow, 19] = list[0].Day15;
            oWorkSheet.Cells[indexRow, 20] = list[0].Day16;
            oWorkSheet.Cells[indexRow, 21] = list[0].Day17;
            oWorkSheet.Cells[indexRow, 22] = list[0].Day18;
            oWorkSheet.Cells[indexRow, 23] = list[0].Day19;
            oWorkSheet.Cells[indexRow, 24] = list[0].Day20;

            oWorkSheet.Cells[indexRow, 25] = list[0].Day21;
            oWorkSheet.Cells[indexRow, 26] = list[0].Day22;
            oWorkSheet.Cells[indexRow, 27] = list[0].Day23;
            oWorkSheet.Cells[indexRow, 28] = list[0].Day24;
            oWorkSheet.Cells[indexRow, 29] = list[0].Day25;
            oWorkSheet.Cells[indexRow, 30] = list[0].Day26;
            oWorkSheet.Cells[indexRow, 31] = list[0].Day27;
            oWorkSheet.Cells[indexRow, 32] = list[0].Day28;
            oWorkSheet.Cells[indexRow, 33] = list[0].Day29;
            oWorkSheet.Cells[indexRow, 34] = list[0].Day30;

            oWorkSheet.Cells[indexRow, 35] = list[0].Day31;

            oWorkSheet.Cells[indexRow, 36] = list[0].NC_LamViec == 0 ? string.Empty : list[0].NC_LamViec.ToString();
            oWorkSheet.Cells[indexRow, 37] = list[0].F_O_Co_KHHDS_TNLD == 0
                ? string.Empty
                : list[0].F_O_Co_KHHDS_TNLD.ToString();
            oWorkSheet.Cells[indexRow, 38] = list[0].F_ThaiSan == 0 ? string.Empty : list[0].F_ThaiSan.ToString();
            oWorkSheet.Cells[indexRow, 39] = list[0].F_TNLD == 0 ? string.Empty : list[0].F_TNLD.ToString();
            oWorkSheet.Cells[indexRow, 40] = list[0].F_DiDuong == 0 ? string.Empty : list[0].F_DiDuong.ToString();
            oWorkSheet.Cells[indexRow, 41] = list[0].F_db == 0 ? string.Empty : list[0].F_db.ToString();
            oWorkSheet.Cells[indexRow, 42] = list[0].F_Hoc == 0 ? string.Empty : list[0].F_Hoc.ToString();
            oWorkSheet.Cells[indexRow, 43] = list[0].F_Nam == 0 ? string.Empty : list[0].F_Nam.ToString();
            oWorkSheet.Cells[indexRow, 44] = list[0].F_Khac == 0 ? string.Empty : list[0].F_Khac.ToString();
            oWorkSheet.Cells[indexRow, 45] = list[0].NghiTuan == 0 ? string.Empty : list[0].NghiTuan.ToString();
            oWorkSheet.Cells[indexRow, 46] = list[0].NghiBu == 0 ? string.Empty : list[0].NghiBu.ToString();
            oWorkSheet.Cells[indexRow, 47] = list[0].CountNight == 0 ? string.Empty : list[0].CountNight.ToString();
            oWorkSheet.Cells[indexRow, 48] = list[0].GC_LamDem == 0 ? string.Empty : list[0].GC_LamDem.ToString();
            oWorkSheet.Cells[indexRow, 49] = list[0].HTCV;

            //((Range)oWorkSheet.Cells[initTitleIndex, 3]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //((Range)oWorkSheet.Cells[initTitleIndex, 4]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //((Range)oWorkSheet.Cells[initTitleIndex, 5]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //((Range)oWorkSheet.Cells[initTitleIndex, 6]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //((Range)oWorkSheet.Cells[initTitleIndex, 7]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //((Range)oWorkSheet.Cells[initTitleIndex, 8]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //((Range)oWorkSheet.Cells[initTitleIndex, 9]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //((Range)oWorkSheet.Cells[initTitleIndex, 10]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //((Range)oWorkSheet.Cells[initTitleIndex, 11]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //((Range)oWorkSheet.Cells[initTitleIndex, 12]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            //((Range)oWorkSheet.Cells[initTitleIndex, 13]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //((Range)oWorkSheet.Cells[initTitleIndex, 14]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //((Range)oWorkSheet.Cells[initTitleIndex, 15]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //((Range)oWorkSheet.Cells[initTitleIndex, 16]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //((Range)oWorkSheet.Cells[initTitleIndex, 17]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //((Range)oWorkSheet.Cells[initTitleIndex, 18]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //((Range)oWorkSheet.Cells[initTitleIndex, 19]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //((Range)oWorkSheet.Cells[initTitleIndex, 20]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //((Range)oWorkSheet.Cells[initTitleIndex, 21]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //((Range)oWorkSheet.Cells[initTitleIndex, 22]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            //((Range)oWorkSheet.Cells[initTitleIndex, 23]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //((Range)oWorkSheet.Cells[initTitleIndex, 24]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //((Range)oWorkSheet.Cells[initTitleIndex, 25]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //((Range)oWorkSheet.Cells[initTitleIndex, 26]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //((Range)oWorkSheet.Cells[initTitleIndex, 27]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //((Range)oWorkSheet.Cells[initTitleIndex, 28]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //((Range)oWorkSheet.Cells[initTitleIndex, 29]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //((Range)oWorkSheet.Cells[initTitleIndex, 30]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //((Range)oWorkSheet.Cells[initTitleIndex, 31]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //((Range)oWorkSheet.Cells[initTitleIndex, 32]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            //((Range)oWorkSheet.Cells[initTitleIndex, 33]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            #endregion

            for (var i = 1; i < list.Count; i++)
            {
                var obj = list[i];
                var obj_1 = list[i - 1];

                indexRow++;
                //isNextRowOfDept = false;
                if ((i < list.Count - 1) && (i > 1))
                {
                    deparmentId = obj.RootId;
                    departmentIdBefore = obj_1.RootId;
                }

                if (deparmentId != departmentIdBefore)
                {
                    rangeDept = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
                    rangeDept.Merge(Type.Missing);
                    oWorkSheet.Cells[indexRow, 1] = obj.RootName.ToUpper();
                    rangeDept.Font.Bold = true;
                    indexRow++;
                }

                /// inserting order number
                oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                oWorkSheet.Cells[indexRow, 2] = obj.EmployeeCode;
                oWorkSheet.Cells[indexRow, 3] = obj.FullName;
                oWorkSheet.Cells[indexRow, 4] = obj.PositionName;
                //// inserting timekeeping

                oWorkSheet.Cells[indexRow, 5] = obj.Day1;
                oWorkSheet.Cells[indexRow, 6] = obj.Day2;
                oWorkSheet.Cells[indexRow, 7] = obj.Day3;
                oWorkSheet.Cells[indexRow, 8] = obj.Day4;
                oWorkSheet.Cells[indexRow, 9] = obj.Day5;
                oWorkSheet.Cells[indexRow, 10] = obj.Day6;
                oWorkSheet.Cells[indexRow, 11] = obj.Day7;
                oWorkSheet.Cells[indexRow, 12] = obj.Day8;
                oWorkSheet.Cells[indexRow, 13] = obj.Day9;
                oWorkSheet.Cells[indexRow, 14] = obj.Day10;

                oWorkSheet.Cells[indexRow, 15] = obj.Day11;
                oWorkSheet.Cells[indexRow, 16] = obj.Day12;
                oWorkSheet.Cells[indexRow, 17] = obj.Day13;
                oWorkSheet.Cells[indexRow, 18] = obj.Day14;
                oWorkSheet.Cells[indexRow, 19] = obj.Day15;
                oWorkSheet.Cells[indexRow, 20] = obj.Day16;
                oWorkSheet.Cells[indexRow, 21] = obj.Day17;
                oWorkSheet.Cells[indexRow, 22] = obj.Day18;
                oWorkSheet.Cells[indexRow, 23] = obj.Day19;
                oWorkSheet.Cells[indexRow, 24] = obj.Day20;

                oWorkSheet.Cells[indexRow, 25] = obj.Day21;
                oWorkSheet.Cells[indexRow, 26] = obj.Day22;
                oWorkSheet.Cells[indexRow, 27] = obj.Day23;
                oWorkSheet.Cells[indexRow, 28] = obj.Day24;
                oWorkSheet.Cells[indexRow, 29] = obj.Day25;
                oWorkSheet.Cells[indexRow, 30] = obj.Day26;
                oWorkSheet.Cells[indexRow, 31] = obj.Day27;
                oWorkSheet.Cells[indexRow, 32] = obj.Day28;
                oWorkSheet.Cells[indexRow, 33] = obj.Day29;
                oWorkSheet.Cells[indexRow, 34] = obj.Day30;

                oWorkSheet.Cells[indexRow, 35] = obj.Day31;

                oWorkSheet.Cells[indexRow, 36] = obj.NC_LamViec == 0 ? string.Empty : obj.NC_LamViec.ToString();
                oWorkSheet.Cells[indexRow, 37] = obj.F_O_Co_KHHDS_TNLD == 0
                    ? string.Empty
                    : obj.F_O_Co_KHHDS_TNLD.ToString();
                oWorkSheet.Cells[indexRow, 38] = obj.F_ThaiSan == 0 ? string.Empty : obj.F_ThaiSan.ToString();
                oWorkSheet.Cells[indexRow, 39] = obj.F_TNLD == 0 ? string.Empty : obj.F_TNLD.ToString();
                oWorkSheet.Cells[indexRow, 40] = obj.F_DiDuong == 0 ? string.Empty : obj.F_DiDuong.ToString();
                oWorkSheet.Cells[indexRow, 41] = obj.F_db == 0 ? string.Empty : obj.F_db.ToString();
                oWorkSheet.Cells[indexRow, 42] = obj.F_Hoc == 0 ? string.Empty : obj.F_Hoc.ToString();
                oWorkSheet.Cells[indexRow, 43] = obj.F_Nam == 0 ? string.Empty : obj.F_Nam.ToString();
                oWorkSheet.Cells[indexRow, 44] = obj.F_Khac == 0 ? string.Empty : obj.F_Khac.ToString();
                oWorkSheet.Cells[indexRow, 45] = obj.NghiTuan == 0 ? string.Empty : obj.NghiTuan.ToString();
                oWorkSheet.Cells[indexRow, 46] = obj.NghiBu == 0 ? string.Empty : obj.NghiBu.ToString();
                oWorkSheet.Cells[indexRow, 47] = obj.CountNight == 0 ? string.Empty : obj.CountNight.ToString();
                oWorkSheet.Cells[indexRow, 48] = obj.GC_LamDem == 0 ? string.Empty : obj.GC_LamDem.ToString();
                oWorkSheet.Cells[indexRow, 49] = obj.HTCV;

                //((Range)oWorkSheet.Cells[initTitleIndex, 3]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //((Range)oWorkSheet.Cells[initTitleIndex, 4]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //((Range)oWorkSheet.Cells[initTitleIndex, 5]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //((Range)oWorkSheet.Cells[initTitleIndex, 6]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //((Range)oWorkSheet.Cells[initTitleIndex, 7]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //((Range)oWorkSheet.Cells[initTitleIndex, 8]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //((Range)oWorkSheet.Cells[initTitleIndex, 9]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //((Range)oWorkSheet.Cells[initTitleIndex, 10]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //((Range)oWorkSheet.Cells[initTitleIndex, 11]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //((Range)oWorkSheet.Cells[initTitleIndex, 12]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                //((Range)oWorkSheet.Cells[initTitleIndex, 13]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //((Range)oWorkSheet.Cells[initTitleIndex, 14]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //((Range)oWorkSheet.Cells[initTitleIndex, 15]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //((Range)oWorkSheet.Cells[initTitleIndex, 16]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //((Range)oWorkSheet.Cells[initTitleIndex, 17]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //((Range)oWorkSheet.Cells[initTitleIndex, 18]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //((Range)oWorkSheet.Cells[initTitleIndex, 19]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //((Range)oWorkSheet.Cells[initTitleIndex, 20]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //((Range)oWorkSheet.Cells[initTitleIndex, 21]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //((Range)oWorkSheet.Cells[initTitleIndex, 22]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                //((Range)oWorkSheet.Cells[initTitleIndex, 23]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //((Range)oWorkSheet.Cells[initTitleIndex, 24]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //((Range)oWorkSheet.Cells[initTitleIndex, 25]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //((Range)oWorkSheet.Cells[initTitleIndex, 26]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //((Range)oWorkSheet.Cells[initTitleIndex, 27]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //((Range)oWorkSheet.Cells[initTitleIndex, 28]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //((Range)oWorkSheet.Cells[initTitleIndex, 29]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //((Range)oWorkSheet.Cells[initTitleIndex, 30]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //((Range)oWorkSheet.Cells[initTitleIndex, 31]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                //((Range)oWorkSheet.Cells[initTitleIndex, 32]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                //((Range)oWorkSheet.Cells[initTitleIndex, 33]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            }

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

            var rangeHeader1 = oWorkSheet.get_Range("A1", "D1");
            rangeHeader1.Merge(Type.Missing);
            oWorkSheet.Cells[1, 1] = "TỔNG CÔNG TY CẢNG HÀNG KHÔNG MIỀN NAM ";
            rangeHeader1.Font.Size = 12;

            var rangeHeader2 = oWorkSheet.get_Range("A2", "D2");
            rangeHeader2.Merge(Type.Missing);
            oWorkSheet.Cells[2, 1] = "CÔNG TY PHỤC VỤ MẶT ĐẤT SÀI GÒN";
            rangeHeader2.Font.Size = 12;
            rangeHeader2.Font.Bold = true;

            var rangeHeader3 = oWorkSheet.get_Range("A4", "AI4");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "BẢNG CHẤM CÔNG THÁNG " + month + " NĂM " + year;
            rangeHeader3.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader3.Font.Size = 15;
            rangeHeader3.Font.Bold = true;

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

            oWorkSheet.Cells[initTitleIndex, columnIndex + 1] = "X";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 2] = "Ô\nCo\nKHHDS";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 3] = "TS";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 4] = "TNLD";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 5] = "DD";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 6] = "Fdb";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 7] = "Ho";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 8] = "F";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 9] = "Khác";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 10] = "NT";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 11] = "NB";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 12] = "Số\nĐêm";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 13] = "Giờ\nĐêm";
            oWorkSheet.Cells[initTitleIndex, columnIndex + 14] = "HTCV";

            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 1]).ColumnWidth = 3;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 1]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 2]).ColumnWidth = 3;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 2]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 2]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 3]).ColumnWidth = 3;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 3]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 3]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 4]).ColumnWidth = 3;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 4]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 4]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 5]).ColumnWidth = 3;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 5]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 5]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 6]).ColumnWidth = 3;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 6]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 6]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 7]).ColumnWidth = 3;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 7]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 7]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 8]).ColumnWidth = 3;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 8]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 8]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 9]).ColumnWidth = 3;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 9]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 9]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 10]).ColumnWidth = 3;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 10]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 10]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 11]).ColumnWidth = 3;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 11]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 11]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 12]).ColumnWidth = 3;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 12]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 12]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 13]).ColumnWidth = 3;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 13]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 13]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 14]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range) oWorkSheet.Cells[initTitleIndex, columnIndex + 14]).VerticalAlignment = XlVAlign.xlVAlignCenter;

            #endregion
        }
    }
}