using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Excel;

namespace HRMBLL.H2.Helper
{
    public class ExportData
    {
        public static string Run(List<CandidatesBLL> list, string pathName)
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

                    oWorkSheet.Name = "CANDIDATES";

                    InsertDataToWorkSheet(list, ref oWorkSheet, DateTime.Now);

                    oExcel.Visible = false;
                    oExcel.UserControl = false;
                    fileName = pathName + "\\DSCandidate.xls";
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

        private static void InsertDataToWorkSheet(List<CandidatesBLL> list, ref _Worksheet oWorkSheet, DateTime date)
        {
            var positionId = 0;
            var positionIdBefore = 0;
            var initTitleIndex = 7;
            // create Content
            var indexRow = initTitleIndex + 1;
            //int indexRowCash = initTitleIndex + 1;
            var orderNumber = 1;
            //int orderNumberCash = 1;
            //bool isNextRowOfDept = false;
            var value = string.Empty;
            CreateHeaderAndTitle(ref oWorkSheet, ref initTitleIndex, date);

            var rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName.Merge(Type.Missing);
            oWorkSheet.Cells[indexRow, 1] = list[0].PositionName.ToUpper();
            rangePositonName.Font.Bold = true;
            indexRow++;

            #region Create FirstRow

            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            oWorkSheet.Cells[indexRow, 2] = list[0].OrderNumber;
            oWorkSheet.Cells[indexRow, 3] = list[0].LastName;
            oWorkSheet.Cells[indexRow, 4] = list[0].FirstName;
            oWorkSheet.Cells[indexRow, 5] = "'" + list[0].DayOfBirth + "/" + list[0].MonthOfBirth + "/" +
                                            list[0].YearOfBirth;
            if (list[0].Sex)
                oWorkSheet.Cells[indexRow, 6] = "Nam";
            else
                oWorkSheet.Cells[indexRow, 6] = "Nữ";
            oWorkSheet.Cells[indexRow, 7] = list[0].Experience;
            oWorkSheet.Cells[indexRow, 8] = "'" + list[0].HandPhone;
            oWorkSheet.Cells[indexRow, 9] = "'" + list[0].HomePhone;
            oWorkSheet.Cells[indexRow, 10] = list[0].Height.ToString("#,###");
            oWorkSheet.Cells[indexRow, 11] = list[0].Remark;
            //////////////////////////////////////////////////////////////

            var listCEL = list[0].AllEducationLevelValue;

            for (var j = 0; j < listCEL.Count; j++)
            {
                var cel = listCEL[j];
                if (cel.EducationLevelId == 1)
                    oWorkSheet.Cells[indexRow, 11 + cel.EducationLevelId] = "'" + cel.EducationLevelValue;
                else
                    oWorkSheet.Cells[indexRow, 11 + cel.EducationLevelId] = cel.EducationLevelValue;
            }

            //////////////////////////////////////////////////////////////

            #endregion

            for (var i = 1; i < list.Count; i++)
            {
                var obj = list[i];
                var obj_1 = list[i - 1];

                indexRow++;
                //isNextRowOfDept = false;
                if ((i < list.Count - 1) && (i > 1))
                {
                    positionId = obj.PositionId;
                    positionIdBefore = obj_1.PositionId;
                }

                if (positionId != positionIdBefore)
                {
                    rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
                    rangePositonName.Merge(Type.Missing);
                    oWorkSheet.Cells[indexRow, 1] = obj.PositionName.ToUpper();
                    rangePositonName.Font.Bold = true;
                    indexRow++;
                }

                /// inserting order number
                oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                oWorkSheet.Cells[indexRow, 2] = obj.OrderNumber;
                oWorkSheet.Cells[indexRow, 3] = obj.LastName;
                oWorkSheet.Cells[indexRow, 4] = obj.FirstName;
                oWorkSheet.Cells[indexRow, 5] = "'" + obj.DayOfBirth + "/" + obj.MonthOfBirth + "/" + obj.YearOfBirth;
                if (obj.Sex)
                    oWorkSheet.Cells[indexRow, 6] = "Nam";
                else
                    oWorkSheet.Cells[indexRow, 6] = "Nữ";
                oWorkSheet.Cells[indexRow, 7] = obj.Experience;
                oWorkSheet.Cells[indexRow, 8] = "'" + obj.HandPhone;
                oWorkSheet.Cells[indexRow, 9] = "'" + obj.HomePhone;
                oWorkSheet.Cells[indexRow, 10] = obj.Height.ToString("#,###");
                oWorkSheet.Cells[indexRow, 11] = obj.Remark;

                //////////////////////////////////////////////////////////////

                var listF = obj.AllEducationLevelValue;

                for (var j = 0; j < listF.Count; j++)
                {
                    var cel = listF[j];
                    if (cel.EducationLevelId == 1)
                        oWorkSheet.Cells[indexRow, 11 + cel.EducationLevelId] = "'" + cel.EducationLevelValue;
                    else
                        oWorkSheet.Cells[indexRow, 11 + cel.EducationLevelId] = cel.EducationLevelValue;
                }

                //////////////////////////////////////////////////////////////
            }

            // release range object            
        }


        private static void CreateHeaderAndTitle(ref _Worksheet oWorkSheet, ref int initTitleIndex, DateTime date)
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

            var rangeHeader3 = oWorkSheet.get_Range("A4", "E4");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "DANH SÁCH ỨNG CỬ VIÊN";
            rangeHeader3.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader3.Font.Size = 15;
            rangeHeader3.Font.Bold = true;

            #endregion

            #region Create Title for Account List from oWorkSheet

            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "Stt";
            oWorkSheet.Cells[initTitleIndex, 2] = "SBD";
            oWorkSheet.Cells[initTitleIndex, 3] = "Họ và tên lót";
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 30;
            oWorkSheet.Cells[initTitleIndex, 4] = "Tên";
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 5] = "Năm sinh";
            ((Range) oWorkSheet.Cells[initTitleIndex, 5]).ColumnWidth = 20;
            oWorkSheet.Cells[initTitleIndex, 6] = "Giới tính";
            ((Range) oWorkSheet.Cells[initTitleIndex, 6]).ColumnWidth = 15;
            oWorkSheet.Cells[initTitleIndex, 7] = "Kinh nghiệm";
            ((Range) oWorkSheet.Cells[initTitleIndex, 7]).ColumnWidth = 25;
            oWorkSheet.Cells[initTitleIndex, 8] = "Điện thoại di động";
            ((Range) oWorkSheet.Cells[initTitleIndex, 8]).ColumnWidth = 25;
            oWorkSheet.Cells[initTitleIndex, 9] = "Điện thoại nhà";
            ((Range) oWorkSheet.Cells[initTitleIndex, 9]).ColumnWidth = 25;
            oWorkSheet.Cells[initTitleIndex, 10] = "Chiều cao";
            ((Range) oWorkSheet.Cells[initTitleIndex, 10]).ColumnWidth = 20;
            oWorkSheet.Cells[initTitleIndex, 11] = "Ghi chú";
            ((Range) oWorkSheet.Cells[initTitleIndex, 11]).ColumnWidth = 30;

            /////////////////////////////////////////////////////////////
            var list = EducationLevelsBLL.GetByFilter(string.Empty, 0);
            for (var i = 0; i < list.Count; i++)
            {
                var obj = list[i];
                oWorkSheet.Cells[initTitleIndex, 11 + i + 1] = obj.Name;
                ((Range) oWorkSheet.Cells[initTitleIndex, 11 + i + 1]).ColumnWidth = 20;
            }
            //////////////////////////////////////////////////////////////
            /// format title
            Range oTitleRange = oWorkSheet.get_Range(oWorkSheet.Cells[initTitleIndex, 1],
                oWorkSheet.Cells[initTitleIndex, 11 + list.Count]);
            oTitleRange.Font.Bold = true;

            oTitleRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            #endregion
        }
    }
}