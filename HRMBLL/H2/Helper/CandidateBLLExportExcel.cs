using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Excel;
using HRMDAL.H2;
using HRMUtil;
using HRMUtil.KeyNames.H0;
using HRMUtil.KeyNames.H2;
using Constants = Excel.Constants;
using DataTable = System.Data.DataTable;

namespace HRMBLL.H2.Helper
{
    public class CandidateBLLExportExcel
    {
        #region Export Candidate List

        public static string ExportCanidate(string firstName, int positionId, int result, int sessionId, int type,
            int sessionType, int typeSort, string pathName)
        {
            ///////////////////////////////////////////
            var dt = new CandidatesDAL().GetByFilter(firstName, positionId, result, sessionId, type, sessionType,
                typeSort);
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

                    oWorkSheet.Name = "DanhSachUngVien";

                    InsertDataToWorkSheetCandidateList(dt, ref oWorkSheet, DateTime.Now);

                    oExcel.Visible = false;
                    oExcel.UserControl = false;
                    fileName = pathName + "\\DanhSachUngVien.xls";
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

        private static void InsertDataToWorkSheetCandidateList(DataTable dt, ref _Worksheet oWorkSheet, DateTime date)
        {
            var positionId = 0;
            var positionIdBefore = 0;
            var initTitleIndex = 7;
            var indexRow = initTitleIndex + 1;
            var orderNumber = 1;
            var value = string.Empty;
            CreateHeaderAndTitleCandidateList(ref oWorkSheet, ref initTitleIndex, date);
            var dr0 = dt.Rows[0];

            var rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName.Merge(Type.Missing);
            var _PositionName = dr0[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                ? string.Empty
                : dr0[PositionKeys.FIELD_POSITION_NAME].ToString();
            oWorkSheet.Cells[indexRow, 1] = _PositionName.ToUpper();
            rangePositonName.Font.Bold = true;

            #region Create FirstRow

            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            oWorkSheet.Cells[indexRow, 2] = dr0[CandidatesKeys.FIELD_CANDIDATE_ORDER_NUMBER] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_ORDER_NUMBER].ToString());
            oWorkSheet.Cells[indexRow, 3] = dr0[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
            oWorkSheet.Cells[indexRow, 4] = dr0[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
            var _DayOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
            var _MonthOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
            var _YearOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
            var _BirthDay = string.Empty;
            if ((_DayOfBirth > 0) && (_MonthOfBirth > 0) && (_YearOfBirth > 0))
                _BirthDay = _DayOfBirth + "/" + _MonthOfBirth + "/" + _YearOfBirth;
            else if ((_MonthOfBirth > 0) && (_YearOfBirth > 0))
                _BirthDay = _MonthOfBirth + "/" + _YearOfBirth;
            else if (_YearOfBirth > 0)
                _BirthDay = _YearOfBirth.ToString();
            else
                _BirthDay = string.Empty;
            oWorkSheet.Cells[indexRow, 5] = _BirthDay;
            var _Sex = dr0[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                ? false
                : bool.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
            if (_Sex)
                oWorkSheet.Cells[indexRow, 6] = "Nam";
            else
                oWorkSheet.Cells[indexRow, 6] = "Nữ";
            /////////////////////////////////////////////
            //// Trinh do van hoa
            var _Id = dr0[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
            var _EducationLevel = string.Empty;
            var _listCEL = CandidateEducationLevelsBLL.GetById(_Id);
            for (var i = 0; i < _listCEL.Count; i++)
            {
                var objCEL = _listCEL[i];
                _EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
            }
            oWorkSheet.Cells[indexRow, 7] = _EducationLevel;
            /////////////////////////////////////////////
            oWorkSheet.Cells[indexRow, 8] = dr0[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE].ToString();
            oWorkSheet.Cells[indexRow, 9] = dr0[CandidatesKeys.FIELD_CANDIDATE_HEIGHT] == DBNull.Value
                ? 0
                : double.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_HEIGHT].ToString());
            oWorkSheet.Cells[indexRow, 10] = dr0[CandidatesKeys.Field_Candidate_Health] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.Field_Candidate_Health].ToString();
            oWorkSheet.Cells[indexRow, 11] = dr0[CandidatesKeys.FIELD_CANDIDATE_HAND_PHONE] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_HAND_PHONE].ToString();
            oWorkSheet.Cells[indexRow, 12] = dr0[CandidatesKeys.FIELD_CANDIDATE_HOME_PHONE] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_HOME_PHONE].ToString();
            oWorkSheet.Cells[indexRow, 13] = dr0[CandidatesKeys.FIELD_CANDIDATE_REMARK] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_REMARK].ToString();

            #endregion

            for (var i = 1; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                var dr_1 = dt.Rows[i - 1];

                positionId = dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());
                positionIdBefore = dr_1[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr_1[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());

                indexRow++;

                var PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[PositionKeys.FIELD_POSITION_NAME].ToString();

                if (positionId != positionIdBefore)
                {
                    rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
                    rangePositonName.Merge(Type.Missing);
                    oWorkSheet.Cells[indexRow, 1] = PositionName.ToUpper();
                    rangePositonName.Font.Bold = true;
                    indexRow++;
                }

                /// inserting order number
                oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                oWorkSheet.Cells[indexRow, 2] = dr[CandidatesKeys.FIELD_CANDIDATE_ORDER_NUMBER] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_ORDER_NUMBER].ToString());
                oWorkSheet.Cells[indexRow, 3] = dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
                oWorkSheet.Cells[indexRow, 4] = dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
                var DayOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
                var MonthOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
                var YearOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
                var BirthDay = string.Empty;
                if ((DayOfBirth > 0) && (MonthOfBirth > 0) && (YearOfBirth > 0))
                    BirthDay = DayOfBirth + "/" + MonthOfBirth + "/" + YearOfBirth;
                else if ((MonthOfBirth > 0) && (YearOfBirth > 0))
                    BirthDay = MonthOfBirth + "/" + YearOfBirth;
                else if (YearOfBirth > 0)
                    BirthDay = YearOfBirth.ToString();
                else
                    BirthDay = string.Empty;
                oWorkSheet.Cells[indexRow, 5] = BirthDay;
                var Sex = dr[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                    ? false
                    : bool.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
                if (Sex)
                    oWorkSheet.Cells[indexRow, 6] = "Nam";
                else
                    oWorkSheet.Cells[indexRow, 6] = "Nữ";
                /////////////////////////////////////////////
                //// Trinh do van hoa
                var Id = dr[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
                var EducationLevel = string.Empty;
                var listCEL = CandidateEducationLevelsBLL.GetById(Id);
                for (var j = 0; j < listCEL.Count; j++)
                {
                    var objCEL = listCEL[j];

                    if (j == listCEL.Count - 1)
                        EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue;
                    else
                        EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
                }
                oWorkSheet.Cells[indexRow, 7] = EducationLevel;
                oWorkSheet.Cells[indexRow, 8] = dr[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE].ToString();
                oWorkSheet.Cells[indexRow, 9] = dr[CandidatesKeys.FIELD_CANDIDATE_HEIGHT] == DBNull.Value
                    ? 0
                    : double.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_HEIGHT].ToString());
                oWorkSheet.Cells[indexRow, 10] = dr[CandidatesKeys.Field_Candidate_Health] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.Field_Candidate_Health].ToString();
                oWorkSheet.Cells[indexRow, 11] = dr[CandidatesKeys.FIELD_CANDIDATE_HAND_PHONE] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_HAND_PHONE].ToString();
                oWorkSheet.Cells[indexRow, 12] = dr[CandidatesKeys.FIELD_CANDIDATE_HOME_PHONE] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_HOME_PHONE].ToString();
                oWorkSheet.Cells[indexRow, 13] = dr[CandidatesKeys.FIELD_CANDIDATE_REMARK] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_REMARK].ToString();
            }

            // release range object            
            var range = oWorkSheet.get_Range("A" + initTitleIndex, "L" + indexRow);
            range.Font.Size = 10;
            range.Font.Name = "Times New Roman";
            range.Borders.LineStyle = Constants.xlSolid;
        }

        private static void CreateHeaderAndTitleCandidateList(ref _Worksheet oWorkSheet, ref int initTitleIndex,
            DateTime date)
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

            var rangeHeader3 = oWorkSheet.get_Range("A3", "D3");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[3, 1] = "HỘI ĐỒNG TUYỂN DỤNG";
            rangeHeader3.Font.Size = 12;
            rangeHeader3.Font.Bold = true;

            var rangeHeader4 = oWorkSheet.get_Range("A4", "H4");
            rangeHeader4.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "DANH SÁCH ỨNG VIÊN ";
            rangeHeader4.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader4.Font.Size = 15;
            rangeHeader4.Font.Bold = true;

            #endregion

            #region Create Title for Account List from oWorkSheet

            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            oWorkSheet.Cells[initTitleIndex, 2] = "STT Đăng ký";
            oWorkSheet.Cells[initTitleIndex, 3] = "Họ và tên lót";
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 30;
            oWorkSheet.Cells[initTitleIndex, 4] = "Tên";
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 5] = "Năm sinh";
            ((Range) oWorkSheet.Cells[initTitleIndex, 5]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 6] = "Giới tính";
            oWorkSheet.Cells[initTitleIndex, 7] = "Trình độ";
            ((Range) oWorkSheet.Cells[initTitleIndex, 7]).ColumnWidth = 30;
            oWorkSheet.Cells[initTitleIndex, 8] = "Kinh Nghiệm";
            ((Range) oWorkSheet.Cells[initTitleIndex, 8]).ColumnWidth = 30;
            oWorkSheet.Cells[initTitleIndex, 9] = "Chiều Cao";
            oWorkSheet.Cells[initTitleIndex, 10] = "Sức Khỏe";
            oWorkSheet.Cells[initTitleIndex, 11] = "Di Động";
            oWorkSheet.Cells[initTitleIndex, 12] = "ĐT Nhà";
            oWorkSheet.Cells[initTitleIndex, 13] = "Ghi Chú";

            var rangeHeader5 = oWorkSheet.get_Range("A7", "L7");
            rangeHeader5.Font.Size = 10;
            rangeHeader5.Font.Name = "Times New Roman";
            rangeHeader5.Font.Bold = true;

            #endregion
        }

        #endregion

        #region Export R1

        public static string ExportR1(string firstName, int positionId, int result, int sessionId, int type,
            int sessionType, int typeSort, string pathName)
        {
            ///////////////////////////////////////////
            var dt = new CandidatesDAL().GetByFilter(firstName, positionId, result, sessionId, type, sessionType,
                typeSort);
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

                    oWorkSheet.Name = "KetQuaVong1";

                    InsertDataToWorkSheetR1(dt, ref oWorkSheet, DateTime.Now);

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

        private static void InsertDataToWorkSheetR1(DataTable dt, ref _Worksheet oWorkSheet, DateTime date)
        {
            var positionId = 0;
            var positionIdBefore = 0;
            var initTitleIndex = 8;
            var indexRow = initTitleIndex + 1;
            var orderNumber = 1;
            var value = string.Empty;
            CreateHeaderAndTitleR1(ref oWorkSheet, ref initTitleIndex, date);
            var dr0 = dt.Rows[0];

            var rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName.Merge(Type.Missing);
            var _PositionName = dr0[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                ? string.Empty
                : dr0[PositionKeys.FIELD_POSITION_NAME].ToString();
            oWorkSheet.Cells[indexRow, 1] = _PositionName.ToUpper();
            rangePositonName.Font.Bold = true;

            #region Create FirstRow            

            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            oWorkSheet.Cells[indexRow, 2] = dr0[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
            oWorkSheet.Cells[indexRow, 3] = dr0[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
            var _DayOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
            var _MonthOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
            var _YearOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
            var _BirthDay = string.Empty;
            if ((_DayOfBirth > 0) && (_MonthOfBirth > 0) && (_YearOfBirth > 0))
                _BirthDay = _DayOfBirth + "/" + _MonthOfBirth + "/" + _YearOfBirth;
            else if ((_MonthOfBirth > 0) && (_YearOfBirth > 0))
                _BirthDay = _MonthOfBirth + "/" + _YearOfBirth;
            else if (_YearOfBirth > 0)
                _BirthDay = _YearOfBirth.ToString();
            else
                _BirthDay = string.Empty;
            oWorkSheet.Cells[indexRow, 4] = _BirthDay;
            var _Sex = dr0[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                ? false
                : bool.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
            if (_Sex)
                oWorkSheet.Cells[indexRow, 5] = "Nam";
            else
                oWorkSheet.Cells[indexRow, 5] = "Nữ";
            /////////////////////////////////////////////
            //// Trinh do van hoa
            var _Id = dr0[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
            var _EducationLevel = string.Empty;
            var _listCEL = CandidateEducationLevelsBLL.GetById(_Id);
            for (var i = 0; i < _listCEL.Count; i++)
            {
                var objCEL = _listCEL[i];
                _EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
            }
            oWorkSheet.Cells[indexRow, 6] = _EducationLevel;
            /////////////////////////////////////////////
            var oDHTB = dr0[CandidatesKeys.Field_Candidate_DHTB] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_DHTB].ToString());
            if (oDHTB == 1)
                oWorkSheet.Cells[indexRow, 7] = "Đạt";
            else
                oWorkSheet.Cells[indexRow, 7] = "";
            //oWorkSheet.Cells[indexRow, 7] = dr0[CandidatesKeys.Field_Candidate_DHTB] == DBNull.Value ? -1 : double.Parse(dr0[CandidatesKeys.Field_Candidate_DHTB].ToString());
            /////////////////////////////////////////////////////////////
            //double oDHGK1 = dr0[CandidatesKeys.Field_Candidate_DHGK1] == DBNull.Value ? -1 : double.Parse(dr0[CandidatesKeys.Field_Candidate_DHGK1].ToString());
            //if (oDHGK1 == 1)
            //{
            //    oWorkSheet.Cells[indexRow, 8] = "Đạt";
            //}
            //else
            //{
            //    oWorkSheet.Cells[indexRow, 8] = "";
            //}
            //double oDHGK2 = dr0[CandidatesKeys.Field_Candidate_DHGK2] == DBNull.Value ? -1 : double.Parse(dr0[CandidatesKeys.Field_Candidate_DHGK2].ToString());
            //if (oDHGK2 == 1)
            //{
            //    oWorkSheet.Cells[indexRow, 9] = "Đạt";
            //}
            //else
            //{
            //    oWorkSheet.Cells[indexRow, 9] = "";
            //}
            //double oDHGK3 = dr0[CandidatesKeys.Field_Candidate_DHGK3] == DBNull.Value ? -1 : double.Parse(dr0[CandidatesKeys.Field_Candidate_DHGK3].ToString());
            //if (oDHGK3 == 1)
            //{
            //    oWorkSheet.Cells[indexRow, 10] = "Đạt";
            //}
            //else
            //{
            //    oWorkSheet.Cells[indexRow, 10] = "";
            //}

            //oWorkSheet.Cells[indexRow, 8] = (dr0[CandidatesKeys.Field_Candidate_DHGK1] == DBNull.Value || double.Parse(dr0[CandidatesKeys.Field_Candidate_DHGK1].ToString()) <= 0) ? "" : dr0[CandidatesKeys.Field_Candidate_DHGK1].ToString();
            //oWorkSheet.Cells[indexRow, 9] = (dr0[CandidatesKeys.Field_Candidate_DHGK2] == DBNull.Value || double.Parse(dr0[CandidatesKeys.Field_Candidate_DHGK2].ToString()) <= 0) ? "" : dr0[CandidatesKeys.Field_Candidate_DHGK2].ToString();
            //oWorkSheet.Cells[indexRow, 10] = (dr0[CandidatesKeys.Field_Candidate_DHGK3] == DBNull.Value || double.Parse(dr0[CandidatesKeys.Field_Candidate_DHGK3].ToString()) <= 0) ? "" : dr0[CandidatesKeys.Field_Candidate_DHGK3].ToString();
            ///////////////////////////////////////////////////////////////

            oWorkSheet.Cells[indexRow, 8] = dr0[CandidatesKeys.Field_Candidate_VT] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_VT].ToString());
            var _Result = dr0[CandidatesKeys.FIELD_CANDIDATE_RESULT] == DBNull.Value
                ? -1
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_RESULT].ToString());
            if (_Result == 1)
                oWorkSheet.Cells[indexRow, 9] = "Đạt";
            else
                oWorkSheet.Cells[indexRow, 9] = "";
            oWorkSheet.Cells[indexRow, 10] = dr0[CandidatesKeys.Field_Candidate_RemarkR1] == DBNull.Value
                ? ""
                : dr0[CandidatesKeys.Field_Candidate_RemarkR1].ToString();

            #endregion

            for (var i = 1; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                var dr_1 = dt.Rows[i - 1];

                positionId = dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());
                positionIdBefore = dr_1[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr_1[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());

                indexRow++;

                var PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[PositionKeys.FIELD_POSITION_NAME].ToString();

                if (positionId != positionIdBefore)
                {
                    rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
                    rangePositonName.Merge(Type.Missing);
                    oWorkSheet.Cells[indexRow, 1] = PositionName.ToUpper();
                    rangePositonName.Font.Bold = true;
                    indexRow++;
                }

                /// inserting order number
                oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                oWorkSheet.Cells[indexRow, 2] = dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
                oWorkSheet.Cells[indexRow, 3] = dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
                var DayOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
                var MonthOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
                var YearOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
                var BirthDay = string.Empty;
                if ((DayOfBirth > 0) && (MonthOfBirth > 0) && (YearOfBirth > 0))
                    BirthDay = DayOfBirth + "/" + MonthOfBirth + "/" + YearOfBirth;
                else if ((MonthOfBirth > 0) && (YearOfBirth > 0))
                    BirthDay = MonthOfBirth + "/" + YearOfBirth;
                else if (YearOfBirth > 0)
                    BirthDay = YearOfBirth.ToString();
                else
                    BirthDay = string.Empty;
                oWorkSheet.Cells[indexRow, 4] = BirthDay;
                var Sex = dr[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                    ? false
                    : bool.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
                if (Sex)
                    oWorkSheet.Cells[indexRow, 5] = "Nam";
                else
                    oWorkSheet.Cells[indexRow, 5] = "Nữ";
                /////////////////////////////////////////////
                //// Trinh do van hoa
                var Id = dr[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
                var EducationLevel = string.Empty;
                var listCEL = CandidateEducationLevelsBLL.GetById(Id);
                for (var j = 0; j < listCEL.Count; j++)
                {
                    var objCEL = listCEL[j];

                    if (j == listCEL.Count - 1)
                        EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue;
                    else
                        EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
                }
                oWorkSheet.Cells[indexRow, 6] = EducationLevel;

                var DHTB = dr[CandidatesKeys.Field_Candidate_DHTB] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_DHTB].ToString());
                if (DHTB == 1)
                    oWorkSheet.Cells[indexRow, 7] = "Đạt";
                else
                    oWorkSheet.Cells[indexRow, 7] = "";
                //oWorkSheet.Cells[indexRow, 7] = dr[CandidatesKeys.Field_Candidate_DHTB] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_DHTB].ToString());

                /////////////////////////////////////////////////////////////
                //double DHGK1 = dr[CandidatesKeys.Field_Candidate_DHGK1] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_DHGK1].ToString());
                //if (DHGK1 == 1)
                //{
                //    oWorkSheet.Cells[indexRow, 8] = "Đạt";
                //}
                //else
                //{
                //    oWorkSheet.Cells[indexRow, 8] = "";
                //}
                //double DHGK2 = dr[CandidatesKeys.Field_Candidate_DHGK2] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_DHGK2].ToString());
                //if (DHGK2 == 1)
                //{
                //    oWorkSheet.Cells[indexRow, 9] = "Đạt";
                //}
                //else
                //{
                //    oWorkSheet.Cells[indexRow, 9] = "";
                //}
                //double DHGK3 = dr[CandidatesKeys.Field_Candidate_DHGK3] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_DHGK3].ToString());
                //if (DHGK3 == 1)
                //{
                //    oWorkSheet.Cells[indexRow, 10] = "Đạt";
                //}
                //else
                //{
                //    oWorkSheet.Cells[indexRow, 10] = "";
                //}

                //oWorkSheet.Cells[indexRow, 8] = (dr[CandidatesKeys.Field_Candidate_DHGK1] == DBNull.Value || double.Parse(dr[CandidatesKeys.Field_Candidate_DHGK1].ToString()) <= 0) ? "" : dr[CandidatesKeys.Field_Candidate_DHGK1].ToString();
                //oWorkSheet.Cells[indexRow, 9] = (dr[CandidatesKeys.Field_Candidate_DHGK2] == DBNull.Value || double.Parse(dr[CandidatesKeys.Field_Candidate_DHGK2].ToString()) <= 0) ? "" : dr[CandidatesKeys.Field_Candidate_DHGK2].ToString();
                //oWorkSheet.Cells[indexRow, 10] = (dr[CandidatesKeys.Field_Candidate_DHGK3] == DBNull.Value || double.Parse(dr[CandidatesKeys.Field_Candidate_DHGK3].ToString()) <= 0) ? "" : dr[CandidatesKeys.Field_Candidate_DHGK3].ToString();
                ///////////////////////////////////////////////////////////////
                oWorkSheet.Cells[indexRow, 8] = dr[CandidatesKeys.Field_Candidate_VT] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_VT].ToString());
                var Result = dr[CandidatesKeys.FIELD_CANDIDATE_RESULT] == DBNull.Value
                    ? -1
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_RESULT].ToString());
                if (Result == 1)
                    oWorkSheet.Cells[indexRow, 9] = "Đạt";
                else
                    oWorkSheet.Cells[indexRow, 9] = "Ko";
                oWorkSheet.Cells[indexRow, 10] = dr[CandidatesKeys.Field_Candidate_RemarkR1] == DBNull.Value
                    ? ""
                    : dr[CandidatesKeys.Field_Candidate_RemarkR1].ToString();
            }

            // release range object            
            var range = oWorkSheet.get_Range("A" + (initTitleIndex - 1), "J" + indexRow);
            range.Font.Size = 10;
            range.Font.Name = "Times New Roman";
            range.Borders.LineStyle = Constants.xlSolid;
        }

        private static void CreateHeaderAndTitleR1(ref _Worksheet oWorkSheet, ref int initTitleIndex, DateTime date)
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

            var rangeHeader3 = oWorkSheet.get_Range("A3", "D3");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[3, 1] = "HỘI ĐỒNG TUYỂN DỤNG";
            rangeHeader3.Font.Size = 12;
            rangeHeader3.Font.Bold = true;

            var rangeHeader4 = oWorkSheet.get_Range("A4", "H4");
            rangeHeader4.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "KẾT QUẢ THI VÒNG 1 (VI TÍNH - DỊCH ANH VIỆT)";
            rangeHeader4.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader4.Font.Size = 15;
            rangeHeader4.Font.Bold = true;

            #endregion

            #region Create Title for Account List from oWorkSheet

            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            //Range rangeHeaderColunm = oWorkSheet.get_Range("A" + (initTitleIndex - 1), "A" + initTitleIndex);
            //rangeHeaderColunm.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 2] = "Họ và tên lót";
            //rangeHeaderColunm = oWorkSheet.get_Range("B" + (initTitleIndex - 1), "B" + initTitleIndex);
            //rangeHeaderColunm.Merge(Type.Missing);
            ((Range) oWorkSheet.Cells[initTitleIndex, 2]).ColumnWidth = 30;
            oWorkSheet.Cells[initTitleIndex, 3] = "Tên";
            //rangeHeaderColunm = oWorkSheet.get_Range("C" + (initTitleIndex - 1), "C" + initTitleIndex);
            //rangeHeaderColunm.Merge(Type.Missing);
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 4] = "Năm sinh";
            //rangeHeaderColunm = oWorkSheet.get_Range("D" + (initTitleIndex - 1), "D" + initTitleIndex);
            //rangeHeaderColunm.Merge(Type.Missing);
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 5] = "Giới tính";
            //rangeHeaderColunm = oWorkSheet.get_Range("E" + (initTitleIndex - 1), "E" + initTitleIndex);
            //rangeHeaderColunm.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 6] = "Trình độ";
            //rangeHeaderColunm = oWorkSheet.get_Range("F" + (initTitleIndex - 1), "F" + initTitleIndex);
            //rangeHeaderColunm.Merge(Type.Missing);
            ((Range) oWorkSheet.Cells[initTitleIndex, 6]).ColumnWidth = 30;
            oWorkSheet.Cells[initTitleIndex, 7] = "Dịch Anh Việt";
            //rangeHeaderColunm = oWorkSheet.get_Range("G" + (initTitleIndex - 1), "G" + initTitleIndex);
            //rangeHeaderColunm.Merge(Type.Missing);
            //oWorkSheet.Cells[initTitleIndex, 8] = "Phỏng Vấn Tiếng Anh";
            // ngoai ngu
            //Range rangeHeaderDH = oWorkSheet.get_Range("H" + (initTitleIndex - 1), "J" + (initTitleIndex - 1));
            //oWorkSheet.Cells[initTitleIndex - 1, 8] = "Phỏng Vấn Tiếng Anh";
            //rangeHeaderDH.Merge(Type.Missing);
            //oWorkSheet.Cells[initTitleIndex, 8] = "GK1";
            //oWorkSheet.Cells[initTitleIndex, 9] = "GK2";
            //oWorkSheet.Cells[initTitleIndex, 10] = "GK3";

            oWorkSheet.Cells[initTitleIndex, 8] = "Vi Tính (%)";
            //rangeHeaderColunm = oWorkSheet.get_Range("K" + (initTitleIndex - 1), "K" + initTitleIndex);
            //rangeHeaderColunm.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 9] = "Kết Quả";
            //rangeHeaderColunm = oWorkSheet.get_Range("L" + (initTitleIndex - 1), "L" + initTitleIndex);
            //rangeHeaderColunm.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 10] = "Ghi Chú";
            //rangeHeaderColunm = oWorkSheet.get_Range("M" + (initTitleIndex - 1), "M" + initTitleIndex);
            //rangeHeaderColunm.Merge(Type.Missing);

            var rangeHeader5 = oWorkSheet.get_Range("A7", "J7");
            rangeHeader5.Font.Size = 10;
            rangeHeader5.Font.Name = "Times New Roman";
            rangeHeader5.Font.Bold = true;

            #endregion
        }

        #endregion

        #region Export R2

        public static string ExportR2(string firstName, int positionId, int result, int sessionId, int type,
            int sessionType, int typeSort, string pathName)
        {
            ///////////////////////////////////////////
            var dt = new CandidatesDAL().GetByFilter(firstName, positionId, result, sessionId, type, sessionType,
                typeSort);
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

                    oWorkSheet.Name = "KetQuaVong2";

                    InsertDataToWorkSheetR2(dt, ref oWorkSheet, DateTime.Now);

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

        private static void InsertDataToWorkSheetR2(DataTable dt, ref _Worksheet oWorkSheet, DateTime date)
        {
            var positionId = 0;
            var positionIdBefore = 0;
            var initTitleIndex = 8;
            var indexRow = initTitleIndex + 1;
            var orderNumber = 1;
            var value = string.Empty;
            CreateHeaderAndTitleR2(ref oWorkSheet, ref initTitleIndex, date);
            var dr0 = dt.Rows[0];

            var rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName.Merge(Type.Missing);
            var _PositionName = dr0[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                ? string.Empty
                : dr0[PositionKeys.FIELD_POSITION_NAME].ToString();
            oWorkSheet.Cells[indexRow, 1] = _PositionName.ToUpper();
            rangePositonName.Font.Bold = true;

            #region Create FirstRow

            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            oWorkSheet.Cells[indexRow, 2] = dr0[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
            oWorkSheet.Cells[indexRow, 3] = dr0[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
            var _DayOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
            var _MonthOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
            var _YearOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
            var _BirthDay = string.Empty;
            if ((_DayOfBirth > 0) && (_MonthOfBirth > 0) && (_YearOfBirth > 0))
                _BirthDay = _DayOfBirth + "/" + _MonthOfBirth + "/" + _YearOfBirth;
            else if ((_MonthOfBirth > 0) && (_YearOfBirth > 0))
                _BirthDay = _MonthOfBirth + "/" + _YearOfBirth;
            else if (_YearOfBirth > 0)
                _BirthDay = _YearOfBirth.ToString();
            else
                _BirthDay = string.Empty;
            oWorkSheet.Cells[indexRow, 4] = _BirthDay;
            var _Sex = dr0[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                ? false
                : bool.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
            if (_Sex)
                oWorkSheet.Cells[indexRow, 5] = "Nam";
            else
                oWorkSheet.Cells[indexRow, 5] = "Nữ";
            /////////////////////////////////////////////
            //// Trinh do van hoa
            var _Id = dr0[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
            var _EducationLevel = string.Empty;
            var _ngoaingu = "";
            var _listCEL = CandidateEducationLevelsBLL.GetById(_Id);
            for (var i = 0; i < _listCEL.Count; i++)
            {
                var objCEL = _listCEL[i];
                if ((objCEL.EducationLevelId == 2) || (objCEL.EducationLevelId == 3) || (objCEL.EducationLevelId == 4)
                    || (objCEL.EducationLevelId == 15) || (objCEL.EducationLevelId == 16) ||
                    (objCEL.EducationLevelId == 17) || (objCEL.EducationLevelId == 18))
                    _ngoaingu += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
                else
                    _EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
            }
            oWorkSheet.Cells[indexRow, 6] = _EducationLevel;
            oWorkSheet.Cells[indexRow, 7] = _ngoaingu;


            /////////////////////////////////////////////

            var _DHTB = dr0[CandidatesKeys.Field_Candidate_DHTB] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_DHTB].ToString());
            var _remark1 = dr0[CandidatesKeys.Field_Candidate_RemarkR1] == DBNull.Value
                ? ""
                : dr0[CandidatesKeys.Field_Candidate_RemarkR1].ToString();
            //oWorkSheet.Cells[indexRow, 7] = _DHTB == -1 ? "" : _DHTB.ToString(StringFormat.FormatMark);
            if (_DHTB == 1)
                oWorkSheet.Cells[indexRow, 8] = "Đạt (" + _remark1 + ")";
            else
                oWorkSheet.Cells[indexRow, 8] = "(" + _remark1 + ")";

            // diem vong 2
            var _NNGK1 = dr0[CandidatesKeys.Field_Candidate_NNGK1] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NNGK1].ToString());
            oWorkSheet.Cells[indexRow, 9] = _NNGK1 == -1 ? "" : _NNGK1.ToString(StringFormat.FormatMark);
            var _NNGK2 = dr0[CandidatesKeys.Field_Candidate_NNGK2] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NNGK2].ToString());
            oWorkSheet.Cells[indexRow, 10] = _NNGK2 == -1 ? "" : _NNGK2.ToString(StringFormat.FormatMark);
            var _NNGK3 = dr0[CandidatesKeys.Field_Candidate_NNGK3] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NNGK3].ToString());
            oWorkSheet.Cells[indexRow, 11] = _NNGK3 == -1 ? "" : _NNGK3.ToString(StringFormat.FormatMark);
            var _NNTB = dr0[CandidatesKeys.Field_Candidate_NNTB] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NNTB].ToString());
            oWorkSheet.Cells[indexRow, 12] = _NNTB == -1 ? "" : _NNTB.ToString(StringFormat.FormatMark);


            var _NHGK1 = dr0[CandidatesKeys.Field_Candidate_NHGK1] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NHGK1].ToString());
            oWorkSheet.Cells[indexRow, 13] = _NHGK1 == -1 ? "" : _NHGK1.ToString(StringFormat.FormatMark);
            var _NHGK2 = dr0[CandidatesKeys.Field_Candidate_NHGK2] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NHGK2].ToString());
            oWorkSheet.Cells[indexRow, 14] = _NHGK2 == -1 ? "" : _NHGK2.ToString(StringFormat.FormatMark);
            var _NHGK3 = dr0[CandidatesKeys.Field_Candidate_NHGK3] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NHGK3].ToString());
            oWorkSheet.Cells[indexRow, 15] = _NHGK3 == -1 ? "" : _NHGK3.ToString(StringFormat.FormatMark);
            var _NHTB = dr0[CandidatesKeys.Field_Candidate_NHTB] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NHTB].ToString());
            oWorkSheet.Cells[indexRow, 16] = _NHTB == -1 ? "" : _NHTB.ToString(StringFormat.FormatMark);

            var _PCGK1 = dr0[CandidatesKeys.Field_Candidate_PCGK1] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_PCGK1].ToString());
            oWorkSheet.Cells[indexRow, 17] = _PCGK1 == -1 ? "" : _PCGK1.ToString(StringFormat.FormatMark);
            var _PCGK2 = dr0[CandidatesKeys.Field_Candidate_PCGK2] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_PCGK2].ToString());
            oWorkSheet.Cells[indexRow, 18] = _PCGK2 == -1 ? "" : _PCGK2.ToString(StringFormat.FormatMark);
            var _PCGK3 = dr0[CandidatesKeys.Field_Candidate_PCGK3] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_PCGK3].ToString());
            oWorkSheet.Cells[indexRow, 19] = _PCGK3 == -1 ? "" : _PCGK3.ToString(StringFormat.FormatMark);
            var _PCTB = dr0[CandidatesKeys.Field_Candidate_PCTB] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_PCTB].ToString());
            oWorkSheet.Cells[indexRow, 20] = _PCTB == -1 ? "" : _PCTB.ToString(StringFormat.FormatMark);

            var _KNGK1 = dr0[CandidatesKeys.Field_Candidate_KNGK1] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_KNGK1].ToString());
            oWorkSheet.Cells[indexRow, 21] = _KNGK1 == -1 ? "" : _KNGK1.ToString(StringFormat.FormatMark);
            var _KNGK2 = dr0[CandidatesKeys.Field_Candidate_KNGK2] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_KNGK2].ToString());
            oWorkSheet.Cells[indexRow, 22] = _KNGK2 == -1 ? "" : _KNGK2.ToString(StringFormat.FormatMark);
            var _KNGK3 = dr0[CandidatesKeys.Field_Candidate_KNGK3] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_KNGK3].ToString());
            oWorkSheet.Cells[indexRow, 23] = _KNGK3 == -1 ? "" : _KNGK3.ToString(StringFormat.FormatMark);
            var _KNTB = dr0[CandidatesKeys.Field_Candidate_KNTB] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_KNTB].ToString());
            oWorkSheet.Cells[indexRow, 24] = _KNTB == -1 ? "" : _KNTB.ToString(StringFormat.FormatMark);

            var _DHNNGK1 = dr0[CandidatesKeys.Field_Candidate_DHNNGK1] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_DHNNGK1].ToString());
            oWorkSheet.Cells[indexRow, 25] = _DHNNGK1 == -1 ? "" : _DHNNGK1.ToString(StringFormat.FormatMark);
            var _DHNNGK2 = dr0[CandidatesKeys.Field_Candidate_DHNNGK2] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_DHNNGK2].ToString());
            oWorkSheet.Cells[indexRow, 26] = _DHNNGK2 == -1 ? "" : _DHNNGK2.ToString(StringFormat.FormatMark);
            var _DHNNGK3 = dr0[CandidatesKeys.Field_Candidate_DHNNGK3] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_DHNNGK3].ToString());
            oWorkSheet.Cells[indexRow, 27] = _DHNNGK3 == -1 ? "" : _DHNNGK3.ToString(StringFormat.FormatMark);
            var _DHNNTB = dr0[CandidatesKeys.Field_Candidate_DHNNTB] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_DHNNTB].ToString());
            oWorkSheet.Cells[indexRow, 28] = _DHNNTB == -1 ? "" : _DHNNTB.ToString(StringFormat.FormatMark);
            oWorkSheet.Cells[indexRow, 29] = dr0[CandidatesKeys.Field_Candidate_VT] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_VT].ToString());
            //////////////////////////////////////////////

            //oWorkSheet.Cells[indexRow, 27] = dr0[CandidatesKeys.Field_Candidate_DHTB] == DBNull.Value ? -1 : double.Parse(dr0[CandidatesKeys.Field_Candidate_DHTB].ToString());

            var _Result = dr0[CandidatesKeys.FIELD_CANDIDATE_RESULT] == DBNull.Value
                ? -1
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_RESULT].ToString());
            if (_Result >= 2)
                oWorkSheet.Cells[indexRow, 30] = "Đạt";
            else
                oWorkSheet.Cells[indexRow, 30] = "";
            oWorkSheet.Cells[indexRow, 31] = dr0[CandidatesKeys.Field_Candidate_RemarkR2] == DBNull.Value
                ? ""
                : dr0[CandidatesKeys.Field_Candidate_RemarkR2].ToString();

            #endregion

            for (var i = 1; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                var dr_1 = dt.Rows[i - 1];

                positionId = dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());
                positionIdBefore = dr_1[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr_1[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());

                indexRow++;

                var PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[PositionKeys.FIELD_POSITION_NAME].ToString();

                if (positionId != positionIdBefore)
                {
                    rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
                    rangePositonName.Merge(Type.Missing);
                    oWorkSheet.Cells[indexRow, 1] = PositionName.ToUpper();
                    rangePositonName.Font.Bold = true;
                    indexRow++;
                }

                /// inserting order number
                oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                oWorkSheet.Cells[indexRow, 2] = dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
                oWorkSheet.Cells[indexRow, 3] = dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
                var DayOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
                var MonthOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
                var YearOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
                var BirthDay = string.Empty;
                if ((DayOfBirth > 0) && (MonthOfBirth > 0) && (YearOfBirth > 0))
                    BirthDay = DayOfBirth + "/" + MonthOfBirth + "/" + YearOfBirth;
                else if ((MonthOfBirth > 0) && (YearOfBirth > 0))
                    BirthDay = MonthOfBirth + "/" + YearOfBirth;
                else if (YearOfBirth > 0)
                    BirthDay = YearOfBirth.ToString();
                else
                    BirthDay = string.Empty;
                oWorkSheet.Cells[indexRow, 4] = BirthDay;
                var Sex = dr[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                    ? false
                    : bool.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
                if (Sex)
                    oWorkSheet.Cells[indexRow, 5] = "Nam";
                else
                    oWorkSheet.Cells[indexRow, 5] = "Nữ";
                /////////////////////////////////////////////
                //// Trinh do van hoa
                var Id = dr[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
                var EducationLevel = string.Empty;
                var ngoaingu = "";
                var listCEL = CandidateEducationLevelsBLL.GetById(Id);
                for (var j = 0; j < listCEL.Count; j++)
                {
                    var objCEL = listCEL[j];
                    if ((objCEL.EducationLevelId == 2) || (objCEL.EducationLevelId == 3) ||
                        (objCEL.EducationLevelId == 4)
                        || (objCEL.EducationLevelId == 15) || (objCEL.EducationLevelId == 16) ||
                        (objCEL.EducationLevelId == 17) || (objCEL.EducationLevelId == 18))
                    {
                        if (j == listCEL.Count - 1)
                            ngoaingu += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue;
                        else
                            ngoaingu += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
                    }
                    else
                    {
                        if (j == listCEL.Count - 1)
                            EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue;
                        else
                            EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue +
                                              "\n";
                    }
                }
                oWorkSheet.Cells[indexRow, 6] = EducationLevel;
                oWorkSheet.Cells[indexRow, 7] = ngoaingu;

                /////////////////////////////////////////////
                var DHTB = dr[CandidatesKeys.Field_Candidate_DHTB] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_DHTB].ToString());
                var remark1 = dr[CandidatesKeys.Field_Candidate_RemarkR1] == DBNull.Value
                    ? ""
                    : dr[CandidatesKeys.Field_Candidate_RemarkR1].ToString();
                //oWorkSheet.Cells[indexRow, 7] = DHTB == -1 ? "" : DHTB.ToString(StringFormat.FormatMark);                
                if (DHTB == 1)
                    oWorkSheet.Cells[indexRow, 8] = "Đạt (" + remark1 + ")";
                else
                    oWorkSheet.Cells[indexRow, 8] = "(" + remark1 + ")";
                //if (DHTB == 1)
                //{
                //    oWorkSheet.Cells[indexRow, 7] = "Đạt";
                //}
                //else
                //{
                //    oWorkSheet.Cells[indexRow, 7] = "";
                //}
                // diem vong 2
                var NNGK1 = dr[CandidatesKeys.Field_Candidate_NNGK1] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NNGK1].ToString());
                oWorkSheet.Cells[indexRow, 9] = NNGK1 == -1 ? "" : NNGK1.ToString(StringFormat.FormatMark);
                var NNGK2 = dr[CandidatesKeys.Field_Candidate_NNGK2] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NNGK2].ToString());
                oWorkSheet.Cells[indexRow, 10] = NNGK2 == -1 ? "" : NNGK2.ToString(StringFormat.FormatMark);
                var NNGK3 = dr[CandidatesKeys.Field_Candidate_NNGK3] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NNGK3].ToString());
                oWorkSheet.Cells[indexRow, 11] = NNGK3 == -1 ? "" : NNGK3.ToString(StringFormat.FormatMark);
                var NNTB = dr[CandidatesKeys.Field_Candidate_NNTB] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NNTB].ToString());
                oWorkSheet.Cells[indexRow, 12] = NNTB == -1 ? "" : NNTB.ToString(StringFormat.FormatMark);


                var NHGK1 = dr[CandidatesKeys.Field_Candidate_NHGK1] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NHGK1].ToString());
                oWorkSheet.Cells[indexRow, 13] = NHGK1 == -1 ? "" : NHGK1.ToString(StringFormat.FormatMark);
                var NHGK2 = dr[CandidatesKeys.Field_Candidate_NHGK2] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NHGK2].ToString());
                oWorkSheet.Cells[indexRow, 14] = NHGK2 == -1 ? "" : NHGK2.ToString(StringFormat.FormatMark);
                var NHGK3 = dr[CandidatesKeys.Field_Candidate_NHGK3] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NHGK3].ToString());
                oWorkSheet.Cells[indexRow, 15] = NHGK3 == -1 ? "" : NHGK3.ToString(StringFormat.FormatMark);
                var NHTB = dr[CandidatesKeys.Field_Candidate_NHTB] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NHTB].ToString());
                oWorkSheet.Cells[indexRow, 16] = NHTB == -1 ? "" : NHTB.ToString(StringFormat.FormatMark);

                var PCGK1 = dr[CandidatesKeys.Field_Candidate_PCGK1] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_PCGK1].ToString());
                oWorkSheet.Cells[indexRow, 17] = PCGK1 == -1 ? "" : PCGK1.ToString(StringFormat.FormatMark);
                var PCGK2 = dr[CandidatesKeys.Field_Candidate_PCGK2] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_PCGK2].ToString());
                oWorkSheet.Cells[indexRow, 18] = PCGK2 == -1 ? "" : PCGK2.ToString(StringFormat.FormatMark);
                var PCGK3 = dr[CandidatesKeys.Field_Candidate_PCGK3] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_PCGK3].ToString());
                oWorkSheet.Cells[indexRow, 19] = PCGK3 == -1 ? "" : PCGK3.ToString(StringFormat.FormatMark);
                var PCTB = dr[CandidatesKeys.Field_Candidate_PCTB] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_PCTB].ToString());
                oWorkSheet.Cells[indexRow, 20] = PCTB == -1 ? "" : PCTB.ToString(StringFormat.FormatMark);

                var KNGK1 = dr[CandidatesKeys.Field_Candidate_KNGK1] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_KNGK1].ToString());
                oWorkSheet.Cells[indexRow, 21] = KNGK1 == -1 ? "" : KNGK1.ToString(StringFormat.FormatMark);
                var KNGK2 = dr[CandidatesKeys.Field_Candidate_KNGK2] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_KNGK2].ToString());
                oWorkSheet.Cells[indexRow, 22] = KNGK2 == -1 ? "" : KNGK2.ToString(StringFormat.FormatMark);
                var KNGK3 = dr[CandidatesKeys.Field_Candidate_KNGK3] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_KNGK3].ToString());
                oWorkSheet.Cells[indexRow, 23] = KNGK3 == -1 ? "" : KNGK3.ToString(StringFormat.FormatMark);
                var KNTB = dr[CandidatesKeys.Field_Candidate_KNTB] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_KNTB].ToString());
                oWorkSheet.Cells[indexRow, 24] = KNTB == -1 ? "" : KNTB.ToString(StringFormat.FormatMark);

                var DHNNGK1 = dr[CandidatesKeys.Field_Candidate_DHNNGK1] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_DHNNGK1].ToString());
                oWorkSheet.Cells[indexRow, 25] = DHNNGK1 == -1 ? "" : DHNNGK1.ToString(StringFormat.FormatMark);
                var DHNNGK2 = dr[CandidatesKeys.Field_Candidate_DHNNGK2] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_DHNNGK2].ToString());
                oWorkSheet.Cells[indexRow, 26] = DHNNGK2 == -1 ? "" : DHNNGK2.ToString(StringFormat.FormatMark);
                var DHNNGK3 = dr[CandidatesKeys.Field_Candidate_DHNNGK3] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_DHNNGK3].ToString());
                oWorkSheet.Cells[indexRow, 27] = DHNNGK3 == -1 ? "" : DHNNGK3.ToString(StringFormat.FormatMark);
                var DHNNTB = dr[CandidatesKeys.Field_Candidate_DHNNTB] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_DHNNTB].ToString());
                oWorkSheet.Cells[indexRow, 28] = DHNNTB == -1 ? "" : DHNNTB.ToString(StringFormat.FormatMark);

                //////////////////////////////////////////////

                //oWorkSheet.Cells[indexRow, 7] = dr[CandidatesKeys.Field_Candidate_DHTB] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_DHTB].ToString());
                oWorkSheet.Cells[indexRow, 29] = dr[CandidatesKeys.Field_Candidate_VT] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_VT].ToString());
                var Result = dr[CandidatesKeys.FIELD_CANDIDATE_RESULT] == DBNull.Value
                    ? -1
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_RESULT].ToString());

                if (Result >= 2)
                    oWorkSheet.Cells[indexRow, 30] = "Đạt";
                else
                    oWorkSheet.Cells[indexRow, 30] = "Ko";
                oWorkSheet.Cells[indexRow, 31] = dr[CandidatesKeys.Field_Candidate_RemarkR2] == DBNull.Value
                    ? ""
                    : dr[CandidatesKeys.Field_Candidate_RemarkR2].ToString();
            }

            // release range object            
            var range = oWorkSheet.get_Range("A" + (initTitleIndex - 1), "AD" + indexRow);
            range.Font.Size = 10;
            range.Font.Name = "Times New Roman";
            range.Borders.LineStyle = Constants.xlSolid;
        }

        private static void CreateHeaderAndTitleR2(ref _Worksheet oWorkSheet, ref int initTitleIndex, DateTime date)
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


            var rangeHeader3 = oWorkSheet.get_Range("A3", "D3");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[3, 1] = "HỘI ĐỒNG TUYỂN DỤNG";
            rangeHeader3.Font.Size = 12;
            rangeHeader3.Font.Bold = true;


            var rangeHeader4 = oWorkSheet.get_Range("A4", "H4");
            rangeHeader4.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "KẾT QUẢ THI VÒNG 2";
            rangeHeader4.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader4.Font.Size = 15;
            rangeHeader4.Font.Bold = true;

            #endregion

            #region Create Title for Account List from oWorkSheet

            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            var rangeHeaderColunm = oWorkSheet.get_Range("A" + (initTitleIndex - 1), "A" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 2] = "HỌ VÀ TÊN LÓT";
            rangeHeaderColunm = oWorkSheet.get_Range("B" + (initTitleIndex - 1), "B" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            ((Range) oWorkSheet.Cells[initTitleIndex, 2]).ColumnWidth = 30;
            oWorkSheet.Cells[initTitleIndex, 3] = "TÊN";
            rangeHeaderColunm = oWorkSheet.get_Range("C" + (initTitleIndex - 1), "C" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 4] = "NĂM SINH";
            rangeHeaderColunm = oWorkSheet.get_Range("D" + (initTitleIndex - 1), "D" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 5] = "GIỚI TÍNH";
            rangeHeaderColunm = oWorkSheet.get_Range("E" + (initTitleIndex - 1), "E" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 6] = "TRÌNH ĐỘ";
            rangeHeaderColunm = oWorkSheet.get_Range("F" + (initTitleIndex - 1), "F" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            ((Range) oWorkSheet.Cells[initTitleIndex, 6]).ColumnWidth = 30;
            oWorkSheet.Cells[initTitleIndex, 7] = "ANH VĂN";
            rangeHeaderColunm = oWorkSheet.get_Range("G" + (initTitleIndex - 1), "G" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            ((Range) oWorkSheet.Cells[initTitleIndex, 7]).ColumnWidth = 30;

            oWorkSheet.Cells[initTitleIndex, 8] = "DỊCH ANH VIỆT";
            rangeHeaderColunm = oWorkSheet.get_Range("H" + (initTitleIndex - 1), "H" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            ((Range) oWorkSheet.Cells[initTitleIndex, 8]).ColumnWidth = 30;

            // ngoai ngu
            var rangeHeaderNgoaiNgu = oWorkSheet.get_Range("I" + (initTitleIndex - 1), "L" + (initTitleIndex - 1));
            oWorkSheet.Cells[initTitleIndex - 1, 9] = "NGOẠI NGỮ";
            rangeHeaderNgoaiNgu.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 9] = "GK1";
            oWorkSheet.Cells[initTitleIndex, 10] = "GK2";
            oWorkSheet.Cells[initTitleIndex, 11] = "GK3";
            oWorkSheet.Cells[initTitleIndex, 12] = "TB";

            //// ngoai hinh
            var rangeHeaderNgoaiHinh = oWorkSheet.get_Range("M" + (initTitleIndex - 1), "P" + (initTitleIndex - 1));
            oWorkSheet.Cells[initTitleIndex - 1, 13] = "NGOẠI HÌNH";
            rangeHeaderNgoaiHinh.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 13] = "GK1";
            oWorkSheet.Cells[initTitleIndex, 14] = "GK2";
            oWorkSheet.Cells[initTitleIndex, 15] = "GK3";
            oWorkSheet.Cells[initTitleIndex, 16] = "TB";

            // ngoai hinh
            var rangeHeaderPhongCach = oWorkSheet.get_Range("Q" + (initTitleIndex - 1), "T" + (initTitleIndex - 1));
            oWorkSheet.Cells[initTitleIndex - 1, 16] = "PHONG CÁCH";
            rangeHeaderPhongCach.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 17] = "GK1";
            oWorkSheet.Cells[initTitleIndex, 18] = "GK2";
            oWorkSheet.Cells[initTitleIndex, 19] = "GK3";
            oWorkSheet.Cells[initTitleIndex, 20] = "TB";

            var rangeHeaderKinhNghiem = oWorkSheet.get_Range("U" + (initTitleIndex - 1), "X" + (initTitleIndex - 1));
            oWorkSheet.Cells[initTitleIndex - 1, 21] = "KINH NGHIỆM";
            rangeHeaderKinhNghiem.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 21] = "GK1";
            oWorkSheet.Cells[initTitleIndex, 22] = "GK2";
            oWorkSheet.Cells[initTitleIndex, 23] = "GK3";
            oWorkSheet.Cells[initTitleIndex, 24] = "TB";


            var rangeHeaderDHNgheNghiep = oWorkSheet.get_Range("Y" + (initTitleIndex - 1), "AB" + (initTitleIndex - 1));
            oWorkSheet.Cells[initTitleIndex - 1, 24] = "ĐH NGHỀ NGHIỆP";
            rangeHeaderDHNgheNghiep.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 25] = "GK1";
            oWorkSheet.Cells[initTitleIndex, 26] = "GK2";
            oWorkSheet.Cells[initTitleIndex, 27] = "GK3";
            oWorkSheet.Cells[initTitleIndex, 28] = "TB";

            oWorkSheet.Cells[initTitleIndex, 29] = "VI TÍNH";
            rangeHeaderColunm = oWorkSheet.get_Range("AC" + (initTitleIndex - 1), "AC" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 30] = "KẾT QUẢ";
            rangeHeaderColunm = oWorkSheet.get_Range("AD" + (initTitleIndex - 1), "AD" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 31] = "GHI CHÚ";
            rangeHeaderColunm = oWorkSheet.get_Range("AE" + (initTitleIndex - 1), "AE" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);

            var rangeHeader5 = oWorkSheet.get_Range("A" + (initTitleIndex - 1), "AE" + initTitleIndex);
            rangeHeader5.Font.Size = 10;
            rangeHeader5.Font.Name = "Times New Roman";
            rangeHeader5.Font.Bold = true;

            #endregion
        }

        #endregion

        #region Export R3

        public static string ExportR3(string firstName, int positionId, int result, int sessionId, int type,
            int sessionType, int typeSort, string pathName)
        {
            ///////////////////////////////////////////
            var dt = new CandidatesDAL().GetByFilter(firstName, positionId, result, sessionId, type, sessionType,
                typeSort);
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

                    oWorkSheet.Name = "KetQuaVong3";

                    InsertDataToWorkSheetR3(dt, ref oWorkSheet, DateTime.Now);

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

        private static void InsertDataToWorkSheetR3(DataTable dt, ref _Worksheet oWorkSheet, DateTime date)
        {
            var positionId = 0;
            var positionIdBefore = 0;
            var initTitleIndex = 8;
            var indexRow = initTitleIndex + 1;
            var orderNumber = 1;
            var value = string.Empty;
            CreateHeaderAndTitleR3(ref oWorkSheet, ref initTitleIndex, date);
            var dr0 = dt.Rows[0];

            var rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName.Merge(Type.Missing);
            var _PositionName = dr0[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                ? string.Empty
                : dr0[PositionKeys.FIELD_POSITION_NAME].ToString();
            oWorkSheet.Cells[indexRow, 1] = _PositionName.ToUpper();
            rangePositonName.Font.Bold = true;

            #region Create FirstRow

            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            oWorkSheet.Cells[indexRow, 2] = dr0[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
            oWorkSheet.Cells[indexRow, 3] = dr0[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
            var _DayOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
            var _MonthOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
            var _YearOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
            var _BirthDay = string.Empty;
            if ((_DayOfBirth > 0) && (_MonthOfBirth > 0) && (_YearOfBirth > 0))
                _BirthDay = _DayOfBirth + "/" + _MonthOfBirth + "/" + _YearOfBirth;
            else if ((_MonthOfBirth > 0) && (_YearOfBirth > 0))
                _BirthDay = _MonthOfBirth + "/" + _YearOfBirth;
            else if (_YearOfBirth > 0)
                _BirthDay = _YearOfBirth.ToString();
            else
                _BirthDay = string.Empty;
            oWorkSheet.Cells[indexRow, 4] = _BirthDay;
            var _Sex = dr0[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                ? false
                : bool.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
            if (_Sex)
                oWorkSheet.Cells[indexRow, 5] = "Nam";
            else
                oWorkSheet.Cells[indexRow, 5] = "Nữ";
            /////////////////////////////////////////////
            //// Trinh do van hoa
            var _Id = dr0[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
            var _EducationLevel = string.Empty;
            var _listCEL = CandidateEducationLevelsBLL.GetById(_Id);
            for (var i = 0; i < _listCEL.Count; i++)
            {
                var objCEL = _listCEL[i];
                _EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
            }
            oWorkSheet.Cells[indexRow, 6] = _EducationLevel;


            /////////////////////////////////////////////

            var _DHTB = dr0[CandidatesKeys.Field_Candidate_DHTB] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_DHTB].ToString());
            //oWorkSheet.Cells[indexRow, 7] = _DHTB == -1 ? "" : _DHTB.ToString(StringFormat.FormatMark);
            if (_DHTB == 1)
                oWorkSheet.Cells[indexRow, 7] = "Đạt";
            else
                oWorkSheet.Cells[indexRow, 7] = "";

            // diem vong 2
            var _L2NNGK1 = dr0[CandidatesKeys.Field_Candidate_L2NNGK1] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_L2NNGK1].ToString());
            oWorkSheet.Cells[indexRow, 8] = _L2NNGK1 == -1 ? "" : _L2NNGK1.ToString(StringFormat.FormatMark);
            var _L2NNGK2 = dr0[CandidatesKeys.Field_Candidate_L2NNGK2] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_L2NNGK2].ToString());
            oWorkSheet.Cells[indexRow, 9] = _L2NNGK2 == -1 ? "" : _L2NNGK2.ToString(StringFormat.FormatMark);
            var _L2NNGK3 = dr0[CandidatesKeys.Field_Candidate_L2NNGK3] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_L2NNGK3].ToString());
            oWorkSheet.Cells[indexRow, 10] = _L2NNGK3 == -1 ? "" : _L2NNGK3.ToString(StringFormat.FormatMark);
            var _L2NNTB = dr0[CandidatesKeys.Field_Candidate_L2NNTB] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_L2NNTB].ToString());
            oWorkSheet.Cells[indexRow, 11] = _L2NNTB == -1 ? "" : _L2NNTB.ToString(StringFormat.FormatMark);


            var _L2NHGK1 = dr0[CandidatesKeys.Field_Candidate_L2NHGK1] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_L2NHGK1].ToString());
            oWorkSheet.Cells[indexRow, 12] = _L2NHGK1 == -1 ? "" : _L2NHGK1.ToString(StringFormat.FormatMark);
            var _L2NHGK2 = dr0[CandidatesKeys.Field_Candidate_L2NHGK2] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_L2NHGK2].ToString());
            oWorkSheet.Cells[indexRow, 13] = _L2NHGK2 == -1 ? "" : _L2NHGK2.ToString(StringFormat.FormatMark);
            var _L2NHGK3 = dr0[CandidatesKeys.Field_Candidate_L2NHGK3] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_L2NHGK3].ToString());
            oWorkSheet.Cells[indexRow, 14] = _L2NHGK3 == -1 ? "" : _L2NHGK3.ToString(StringFormat.FormatMark);
            var _L2NHTB = dr0[CandidatesKeys.Field_Candidate_L2NHTB] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_L2NHTB].ToString());
            oWorkSheet.Cells[indexRow, 15] = _L2NHTB == -1 ? "" : _L2NHTB.ToString(StringFormat.FormatMark);

            var _L2PCGK1 = dr0[CandidatesKeys.Field_Candidate_L2PCGK1] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_L2PCGK1].ToString());
            oWorkSheet.Cells[indexRow, 16] = _L2PCGK1 == -1 ? "" : _L2PCGK1.ToString(StringFormat.FormatMark);
            var _L2PCGK2 = dr0[CandidatesKeys.Field_Candidate_L2PCGK2] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_L2PCGK2].ToString());
            oWorkSheet.Cells[indexRow, 17] = _L2PCGK2 == -1 ? "" : _L2PCGK2.ToString(StringFormat.FormatMark);
            var _L2PCGK3 = dr0[CandidatesKeys.Field_Candidate_L2PCGK3] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_L2PCGK3].ToString());
            oWorkSheet.Cells[indexRow, 18] = _L2PCGK3 == -1 ? "" : _L2PCGK3.ToString(StringFormat.FormatMark);
            var _L2PCTB = dr0[CandidatesKeys.Field_Candidate_L2PCTB] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_L2PCTB].ToString());
            oWorkSheet.Cells[indexRow, 19] = _L2PCTB == -1 ? "" : _L2PCTB.ToString(StringFormat.FormatMark);

            var _L2KNGK1 = dr0[CandidatesKeys.Field_Candidate_L2KNGK1] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_L2KNGK1].ToString());
            oWorkSheet.Cells[indexRow, 20] = _L2KNGK1 == -1 ? "" : _L2KNGK1.ToString(StringFormat.FormatMark);
            var _L2KNGK2 = dr0[CandidatesKeys.Field_Candidate_L2KNGK2] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_L2KNGK2].ToString());
            oWorkSheet.Cells[indexRow, 21] = _L2KNGK2 == -1 ? "" : _L2KNGK2.ToString(StringFormat.FormatMark);
            var _L2KNGK3 = dr0[CandidatesKeys.Field_Candidate_L2KNGK3] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_L2KNGK3].ToString());
            oWorkSheet.Cells[indexRow, 22] = _L2KNGK3 == -1 ? "" : _L2KNGK3.ToString(StringFormat.FormatMark);
            var _L2KNTB = dr0[CandidatesKeys.Field_Candidate_L2KNTB] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_L2KNTB].ToString());
            oWorkSheet.Cells[indexRow, 23] = _L2KNTB == -1 ? "" : _L2KNTB.ToString(StringFormat.FormatMark);

            var _L2DHNNGK1 = dr0[CandidatesKeys.Field_Candidate_L2DHNNGK1] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_L2DHNNGK1].ToString());
            oWorkSheet.Cells[indexRow, 24] = _L2DHNNGK1 == -1 ? "" : _L2DHNNGK1.ToString(StringFormat.FormatMark);
            var _L2DHNNGK2 = dr0[CandidatesKeys.Field_Candidate_L2DHNNGK2] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_L2DHNNGK2].ToString());
            oWorkSheet.Cells[indexRow, 25] = _L2DHNNGK2 == -1 ? "" : _L2DHNNGK2.ToString(StringFormat.FormatMark);
            var _L2DHNNGK3 = dr0[CandidatesKeys.Field_Candidate_L2DHNNGK3] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_L2DHNNGK3].ToString());
            oWorkSheet.Cells[indexRow, 26] = _L2DHNNGK3 == -1 ? "" : _L2DHNNGK3.ToString(StringFormat.FormatMark);
            var _L2DHNNTB = dr0[CandidatesKeys.Field_Candidate_L2DHNNTB] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_L2DHNNTB].ToString());
            oWorkSheet.Cells[indexRow, 27] = _L2DHNNTB == -1 ? "" : _L2DHNNTB.ToString(StringFormat.FormatMark);

            //////////////////////////////////////////////

            //oWorkSheet.Cells[indexRow, 27] = dr0[CandidatesKeys.Field_Candidate_DHTB] == DBNull.Value ? -1 : double.Parse(dr0[CandidatesKeys.Field_Candidate_DHTB].ToString());
            oWorkSheet.Cells[indexRow, 28] = dr0[CandidatesKeys.Field_Candidate_VT] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_VT].ToString());
            var _Result = dr0[CandidatesKeys.FIELD_CANDIDATE_RESULT] == DBNull.Value
                ? -1
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_RESULT].ToString());
            if (_Result >= 3)
                oWorkSheet.Cells[indexRow, 29] = "Đạt";
            else
                oWorkSheet.Cells[indexRow, 29] = "Ko";
            oWorkSheet.Cells[indexRow, 30] = dr0[CandidatesKeys.Field_Candidate_RemarkR3] == DBNull.Value
                ? ""
                : dr0[CandidatesKeys.Field_Candidate_RemarkR3].ToString();

            #endregion

            for (var i = 1; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                var dr_1 = dt.Rows[i - 1];

                positionId = dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());
                positionIdBefore = dr_1[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr_1[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());

                indexRow++;

                var PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[PositionKeys.FIELD_POSITION_NAME].ToString();

                if (positionId != positionIdBefore)
                {
                    rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
                    rangePositonName.Merge(Type.Missing);
                    oWorkSheet.Cells[indexRow, 1] = PositionName.ToUpper();
                    rangePositonName.Font.Bold = true;
                    indexRow++;
                }

                /// inserting order number
                oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                oWorkSheet.Cells[indexRow, 2] = dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
                oWorkSheet.Cells[indexRow, 3] = dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
                var DayOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
                var MonthOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
                var YearOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
                var BirthDay = string.Empty;
                if ((DayOfBirth > 0) && (MonthOfBirth > 0) && (YearOfBirth > 0))
                    BirthDay = DayOfBirth + "/" + MonthOfBirth + "/" + YearOfBirth;
                else if ((MonthOfBirth > 0) && (YearOfBirth > 0))
                    BirthDay = MonthOfBirth + "/" + YearOfBirth;
                else if (YearOfBirth > 0)
                    BirthDay = YearOfBirth.ToString();
                else
                    BirthDay = string.Empty;
                oWorkSheet.Cells[indexRow, 4] = BirthDay;
                var Sex = dr[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                    ? false
                    : bool.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
                if (Sex)
                    oWorkSheet.Cells[indexRow, 5] = "Nam";
                else
                    oWorkSheet.Cells[indexRow, 5] = "Nữ";
                /////////////////////////////////////////////
                //// Trinh do van hoa
                var Id = dr[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
                var EducationLevel = string.Empty;
                var listCEL = CandidateEducationLevelsBLL.GetById(Id);
                for (var j = 0; j < listCEL.Count; j++)
                {
                    var objCEL = listCEL[j];

                    if (j == listCEL.Count - 1)
                        EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue;
                    else
                        EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
                }
                oWorkSheet.Cells[indexRow, 6] = EducationLevel;

                /////////////////////////////////////////////
                var DHTB = dr[CandidatesKeys.Field_Candidate_DHTB] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_DHTB].ToString());
                //oWorkSheet.Cells[indexRow, 7] = DHTB == -1 ? "" : DHTB.ToString(StringFormat.FormatMark);                
                if (DHTB == 1)
                    oWorkSheet.Cells[indexRow, 7] = "Đạt";
                else
                    oWorkSheet.Cells[indexRow, 7] = "";
                // diem vong 2
                var L2NNGK1 = dr[CandidatesKeys.Field_Candidate_L2NNGK1] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_L2NNGK1].ToString());
                oWorkSheet.Cells[indexRow, 8] = L2NNGK1 == -1 ? "" : L2NNGK1.ToString(StringFormat.FormatMark);
                var L2NNGK2 = dr[CandidatesKeys.Field_Candidate_L2NNGK2] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_L2NNGK2].ToString());
                oWorkSheet.Cells[indexRow, 9] = L2NNGK2 == -1 ? "" : L2NNGK2.ToString(StringFormat.FormatMark);
                var L2NNGK3 = dr[CandidatesKeys.Field_Candidate_L2NNGK3] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_L2NNGK3].ToString());
                oWorkSheet.Cells[indexRow, 10] = L2NNGK3 == -1 ? "" : L2NNGK3.ToString(StringFormat.FormatMark);
                var L2NNTB = dr[CandidatesKeys.Field_Candidate_L2NNTB] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_L2NNTB].ToString());
                oWorkSheet.Cells[indexRow, 11] = L2NNTB == -1 ? "" : L2NNTB.ToString(StringFormat.FormatMark);


                var L2NHGK1 = dr[CandidatesKeys.Field_Candidate_L2NHGK1] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_L2NHGK1].ToString());
                oWorkSheet.Cells[indexRow, 12] = L2NHGK1 == -1 ? "" : L2NHGK1.ToString(StringFormat.FormatMark);
                var L2NHGK2 = dr[CandidatesKeys.Field_Candidate_L2NHGK2] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_L2NHGK2].ToString());
                oWorkSheet.Cells[indexRow, 13] = L2NHGK2 == -1 ? "" : L2NHGK2.ToString(StringFormat.FormatMark);
                var L2NHGK3 = dr[CandidatesKeys.Field_Candidate_L2NHGK3] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_L2NHGK3].ToString());
                oWorkSheet.Cells[indexRow, 14] = L2NHGK3 == -1 ? "" : L2NHGK3.ToString(StringFormat.FormatMark);
                var L2NHTB = dr[CandidatesKeys.Field_Candidate_L2NHTB] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_L2NHTB].ToString());
                oWorkSheet.Cells[indexRow, 15] = L2NHTB == -1 ? "" : L2NHTB.ToString(StringFormat.FormatMark);

                var L2PCGK1 = dr[CandidatesKeys.Field_Candidate_L2PCGK1] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_L2PCGK1].ToString());
                oWorkSheet.Cells[indexRow, 16] = L2PCGK1 == -1 ? "" : L2PCGK1.ToString(StringFormat.FormatMark);
                var L2PCGK2 = dr[CandidatesKeys.Field_Candidate_L2PCGK2] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_L2PCGK2].ToString());
                oWorkSheet.Cells[indexRow, 17] = L2PCGK2 == -1 ? "" : L2PCGK2.ToString(StringFormat.FormatMark);
                var L2PCGK3 = dr[CandidatesKeys.Field_Candidate_L2PCGK3] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_L2PCGK3].ToString());
                oWorkSheet.Cells[indexRow, 18] = L2PCGK3 == -1 ? "" : L2PCGK3.ToString(StringFormat.FormatMark);
                var L2PCTB = dr[CandidatesKeys.Field_Candidate_L2PCTB] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_L2PCTB].ToString());
                oWorkSheet.Cells[indexRow, 19] = L2PCTB == -1 ? "" : L2PCTB.ToString(StringFormat.FormatMark);

                var L2KNGK1 = dr[CandidatesKeys.Field_Candidate_L2KNGK1] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_L2KNGK1].ToString());
                oWorkSheet.Cells[indexRow, 20] = L2KNGK1 == -1 ? "" : L2KNGK1.ToString(StringFormat.FormatMark);
                var L2KNGK2 = dr[CandidatesKeys.Field_Candidate_L2KNGK2] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_L2KNGK2].ToString());
                oWorkSheet.Cells[indexRow, 21] = L2KNGK2 == -1 ? "" : L2KNGK2.ToString(StringFormat.FormatMark);
                var L2KNGK3 = dr[CandidatesKeys.Field_Candidate_L2KNGK3] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_L2KNGK3].ToString());
                oWorkSheet.Cells[indexRow, 22] = L2KNGK3 == -1 ? "" : L2KNGK3.ToString(StringFormat.FormatMark);
                var L2KNTB = dr[CandidatesKeys.Field_Candidate_L2KNTB] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_L2KNTB].ToString());
                oWorkSheet.Cells[indexRow, 23] = L2KNTB == -1 ? "" : L2KNTB.ToString(StringFormat.FormatMark);

                var L2DHNNGK1 = dr[CandidatesKeys.Field_Candidate_L2DHNNGK1] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_L2DHNNGK1].ToString());
                oWorkSheet.Cells[indexRow, 24] = L2DHNNGK1 == -1 ? "" : L2DHNNGK1.ToString(StringFormat.FormatMark);
                var L2DHNNGK2 = dr[CandidatesKeys.Field_Candidate_L2DHNNGK2] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_L2DHNNGK2].ToString());
                oWorkSheet.Cells[indexRow, 25] = L2DHNNGK2 == -1 ? "" : L2DHNNGK2.ToString(StringFormat.FormatMark);
                var L2DHNNGK3 = dr[CandidatesKeys.Field_Candidate_L2DHNNGK3] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_L2DHNNGK3].ToString());
                oWorkSheet.Cells[indexRow, 26] = L2DHNNGK3 == -1 ? "" : L2DHNNGK3.ToString(StringFormat.FormatMark);
                var L2DHNNTB = dr[CandidatesKeys.Field_Candidate_L2DHNNTB] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_L2DHNNTB].ToString());
                oWorkSheet.Cells[indexRow, 27] = L2DHNNTB == -1 ? "" : L2DHNNTB.ToString(StringFormat.FormatMark);

                //////////////////////////////////////////////

                //oWorkSheet.Cells[indexRow, 7] = dr[CandidatesKeys.Field_Candidate_DHTB] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_DHTB].ToString());
                oWorkSheet.Cells[indexRow, 28] = dr[CandidatesKeys.Field_Candidate_VT] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_VT].ToString());
                var Result = dr[CandidatesKeys.FIELD_CANDIDATE_RESULT] == DBNull.Value
                    ? -1
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_RESULT].ToString());
                if (Result >= 3)
                    oWorkSheet.Cells[indexRow, 29] = "Đạt";
                else
                    oWorkSheet.Cells[indexRow, 29] = "Ko";
                oWorkSheet.Cells[indexRow, 30] = dr[CandidatesKeys.Field_Candidate_RemarkR3] == DBNull.Value
                    ? ""
                    : dr[CandidatesKeys.Field_Candidate_RemarkR3].ToString();
            }

            // release range object            
            var range = oWorkSheet.get_Range("A" + (initTitleIndex - 1), "AD" + indexRow);
            range.Font.Size = 10;
            range.Font.Name = "Times New Roman";
            range.Borders.LineStyle = Constants.xlSolid;
        }

        private static void CreateHeaderAndTitleR3(ref _Worksheet oWorkSheet, ref int initTitleIndex, DateTime date)
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


            var rangeHeader3 = oWorkSheet.get_Range("A3", "D3");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[3, 1] = "HỘI ĐỒNG TUYỂN DỤNG";
            rangeHeader3.Font.Size = 12;
            rangeHeader3.Font.Bold = true;


            var rangeHeader4 = oWorkSheet.get_Range("A4", "H4");
            rangeHeader4.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "KẾT QUẢ THI VÒNG 3";
            rangeHeader4.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader4.Font.Size = 15;
            rangeHeader4.Font.Bold = true;

            #endregion

            #region Create Title for Account List from oWorkSheet

            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            var rangeHeaderColunm = oWorkSheet.get_Range("A" + (initTitleIndex - 1), "A" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 2] = "HỌ VÀ TÊN LÓT";
            rangeHeaderColunm = oWorkSheet.get_Range("B" + (initTitleIndex - 1), "B" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            ((Range) oWorkSheet.Cells[initTitleIndex, 2]).ColumnWidth = 30;
            oWorkSheet.Cells[initTitleIndex, 3] = "TÊN";
            rangeHeaderColunm = oWorkSheet.get_Range("C" + (initTitleIndex - 1), "C" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 4] = "NĂM SINH";
            rangeHeaderColunm = oWorkSheet.get_Range("D" + (initTitleIndex - 1), "D" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 5] = "GIỚI TÍNH";
            rangeHeaderColunm = oWorkSheet.get_Range("E" + (initTitleIndex - 1), "E" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 6] = "TRÌNH ĐỘ";
            rangeHeaderColunm = oWorkSheet.get_Range("F" + (initTitleIndex - 1), "F" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            ((Range) oWorkSheet.Cells[initTitleIndex, 6]).ColumnWidth = 30;

            oWorkSheet.Cells[initTitleIndex, 7] = "DỊCH ANH VIỆT";
            rangeHeaderColunm = oWorkSheet.get_Range("G" + (initTitleIndex - 1), "G" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            ((Range) oWorkSheet.Cells[initTitleIndex, 7]).ColumnWidth = 30;

            // ngoai ngu
            var rangeHeaderNgoaiNgu = oWorkSheet.get_Range("H" + (initTitleIndex - 1), "K" + (initTitleIndex - 1));
            oWorkSheet.Cells[initTitleIndex - 1, 8] = "NGOẠI NGỮ";
            rangeHeaderNgoaiNgu.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 8] = "GK1";
            oWorkSheet.Cells[initTitleIndex, 9] = "GK2";
            oWorkSheet.Cells[initTitleIndex, 10] = "GK3";
            oWorkSheet.Cells[initTitleIndex, 11] = "TB";

            //// ngoai hinh
            var rangeHeaderNgoaiHinh = oWorkSheet.get_Range("L" + (initTitleIndex - 1), "O" + (initTitleIndex - 1));
            oWorkSheet.Cells[initTitleIndex - 1, 12] = "NGOẠI HÌNH";
            rangeHeaderNgoaiHinh.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 12] = "GK1";
            oWorkSheet.Cells[initTitleIndex, 13] = "GK2";
            oWorkSheet.Cells[initTitleIndex, 14] = "GK3";
            oWorkSheet.Cells[initTitleIndex, 15] = "TB";

            // ngoai hinh
            var rangeHeaderPhongCach = oWorkSheet.get_Range("P" + (initTitleIndex - 1), "S" + (initTitleIndex - 1));
            oWorkSheet.Cells[initTitleIndex - 1, 16] = "PHONG CÁCH";
            rangeHeaderPhongCach.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 16] = "GK1";
            oWorkSheet.Cells[initTitleIndex, 17] = "GK2";
            oWorkSheet.Cells[initTitleIndex, 18] = "GK3";
            oWorkSheet.Cells[initTitleIndex, 19] = "TB";

            var rangeHeaderKinhNghiem = oWorkSheet.get_Range("T" + (initTitleIndex - 1), "W" + (initTitleIndex - 1));
            oWorkSheet.Cells[initTitleIndex - 1, 20] = "KINH NGHIỆM";
            rangeHeaderKinhNghiem.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 20] = "GK1";
            oWorkSheet.Cells[initTitleIndex, 21] = "GK2";
            oWorkSheet.Cells[initTitleIndex, 22] = "GK3";
            oWorkSheet.Cells[initTitleIndex, 23] = "TB";


            var rangeHeaderDHNgheNghiep = oWorkSheet.get_Range("X" + (initTitleIndex - 1), "AA" + (initTitleIndex - 1));
            oWorkSheet.Cells[initTitleIndex - 1, 24] = "ĐH NGHỀ NGHIỆP";
            rangeHeaderDHNgheNghiep.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 24] = "GK1";
            oWorkSheet.Cells[initTitleIndex, 25] = "GK2";
            oWorkSheet.Cells[initTitleIndex, 26] = "GK3";
            oWorkSheet.Cells[initTitleIndex, 27] = "TB";

            oWorkSheet.Cells[initTitleIndex, 28] = "VI TÍNH";
            rangeHeaderColunm = oWorkSheet.get_Range("AB" + (initTitleIndex - 1), "AB" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 29] = "KẾT QUẢ";
            rangeHeaderColunm = oWorkSheet.get_Range("AC" + (initTitleIndex - 1), "AC" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 30] = "GHI CHÚ";
            rangeHeaderColunm = oWorkSheet.get_Range("AD" + (initTitleIndex - 1), "AD" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);

            var rangeHeader5 = oWorkSheet.get_Range("A" + (initTitleIndex - 1), "AD" + initTitleIndex);
            rangeHeader5.Font.Size = 10;
            rangeHeader5.Font.Name = "Times New Roman";
            rangeHeader5.Font.Bold = true;

            #endregion
        }

        #endregion

        #region Export RBoard

        public static string ExportRBoard(string firstName, int positionId, int result, int sessionId, int type,
            int sessionType, int typeSort, string pathName)
        {
            ///////////////////////////////////////////
            var dt = new CandidatesDAL().GetByFilter(firstName, positionId, result, sessionId, type, sessionType,
                typeSort);
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

                    oWorkSheet.Name = "KetQuaVongBoard";

                    InsertDataToWorkSheetRBoard(dt, ref oWorkSheet, DateTime.Now);

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

        private static void InsertDataToWorkSheetRBoard(DataTable dt, ref _Worksheet oWorkSheet, DateTime date)
        {
            var positionId = 0;
            var positionIdBefore = 0;
            var initTitleIndex = 8;
            var indexRow = initTitleIndex + 1;
            var orderNumber = 1;
            var value = string.Empty;
            CreateHeaderAndTitleRBoard(ref oWorkSheet, ref initTitleIndex, date);
            var dr0 = dt.Rows[0];

            var rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName.Merge(Type.Missing);
            var _PositionName = dr0[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                ? string.Empty
                : dr0[PositionKeys.FIELD_POSITION_NAME].ToString();
            oWorkSheet.Cells[indexRow, 1] = _PositionName.ToUpper();
            rangePositonName.Font.Bold = true;

            #region Create FirstRow

            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            oWorkSheet.Cells[indexRow, 2] = dr0[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
            oWorkSheet.Cells[indexRow, 3] = dr0[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
            var _DayOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
            var _MonthOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
            var _YearOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
            var _BirthDay = string.Empty;
            if ((_DayOfBirth > 0) && (_MonthOfBirth > 0) && (_YearOfBirth > 0))
                _BirthDay = _DayOfBirth + "/" + _MonthOfBirth + "/" + _YearOfBirth;
            else if ((_MonthOfBirth > 0) && (_YearOfBirth > 0))
                _BirthDay = _MonthOfBirth + "/" + _YearOfBirth;
            else if (_YearOfBirth > 0)
                _BirthDay = _YearOfBirth.ToString();
            else
                _BirthDay = string.Empty;
            oWorkSheet.Cells[indexRow, 4] = _BirthDay;
            var _Sex = dr0[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                ? false
                : bool.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
            if (_Sex)
                oWorkSheet.Cells[indexRow, 5] = "Nam";
            else
                oWorkSheet.Cells[indexRow, 5] = "Nữ";
            /////////////////////////////////////////////
            //// Trinh do van hoa
            var _Id = dr0[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
            var _EducationLevel = string.Empty;
            var _ngoaingu = "";
            var _listCEL = CandidateEducationLevelsBLL.GetById(_Id);
            for (var i = 0; i < _listCEL.Count; i++)
            {
                var objCEL = _listCEL[i];
                if ((objCEL.EducationLevelId == 2) || (objCEL.EducationLevelId == 3) || (objCEL.EducationLevelId == 4)
                    || (objCEL.EducationLevelId == 15) || (objCEL.EducationLevelId == 16) ||
                    (objCEL.EducationLevelId == 17) || (objCEL.EducationLevelId == 18))
                    _ngoaingu += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
                else
                    _EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
            }
            oWorkSheet.Cells[indexRow, 6] = _EducationLevel;
            oWorkSheet.Cells[indexRow, 7] = _ngoaingu;


            /////////////////////////////////////////////
            // diem vong 3
            var _NNLRGK1 = dr0[CandidatesKeys.Field_Candidate_NNLRGK1] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NNLRGK1].ToString());
            oWorkSheet.Cells[indexRow, 8] = _NNLRGK1 == -1 ? "" : (_NNLRGK1 == 1 ? "đạt" : "ko");
            var _NNLRGK2 = dr0[CandidatesKeys.Field_Candidate_NNLRGK2] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NNLRGK2].ToString());
            oWorkSheet.Cells[indexRow, 9] = _NNLRGK2 == -1 ? "" : (_NNLRGK2 == 1 ? "đạt" : "ko");
            var _NNLRGK3 = dr0[CandidatesKeys.Field_Candidate_NNLRGK3] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NNLRGK3].ToString());
            oWorkSheet.Cells[indexRow, 10] = _NNLRGK3 == -1 ? "" : (_NNLRGK3 == 1 ? "đạt" : "ko");
            var _NNLRTB = dr0[CandidatesKeys.Field_Candidate_NNLRTB] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NNLRTB].ToString());
            oWorkSheet.Cells[indexRow, 11] = _NNLRTB == -1 ? "" : (_NNLRTB == 1 ? "đạt" : "ko");
            //double _DHGK1 = dr0[CandidatesKeys.Field_Candidate_DHGK1] == DBNull.Value ? -1 : double.Parse(dr0[CandidatesKeys.Field_Candidate_DHGK1].ToString());
            //oWorkSheet.Cells[indexRow, 11] = _DHGK1 == -1 ? "" : (_DHGK1 == 1 ? "đạt" : "ko");


            var _NHLRGK1 = dr0[CandidatesKeys.Field_Candidate_NHLRGK1] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NHLRGK1].ToString());
            oWorkSheet.Cells[indexRow, 12] = _NHLRGK1 == -1 ? "" : (_NHLRGK1 == 1 ? "đạt" : "ko");
            var _NHLRGK2 = dr0[CandidatesKeys.Field_Candidate_NHLRGK2] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NHLRGK2].ToString());
            oWorkSheet.Cells[indexRow, 13] = _NHLRGK2 == -1 ? "" : (_NHLRGK2 == 1 ? "đạt" : "ko");
            var _NHLRGK3 = dr0[CandidatesKeys.Field_Candidate_NHLRGK3] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NHLRGK3].ToString());
            oWorkSheet.Cells[indexRow, 14] = _NHLRGK3 == -1 ? "" : (_NHLRGK3 == 1 ? "đạt" : "ko");
            var _NHLRTB = dr0[CandidatesKeys.Field_Candidate_NHLRTB] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NHLRTB].ToString());
            oWorkSheet.Cells[indexRow, 15] = _NHLRTB == -1 ? "" : (_NHLRTB == 1 ? "đạt" : "ko");
            //double _DHGK2 = dr0[CandidatesKeys.Field_Candidate_DHGK2] == DBNull.Value ? -1 : double.Parse(dr0[CandidatesKeys.Field_Candidate_DHGK2].ToString());
            //oWorkSheet.Cells[indexRow, 16] = _DHGK2 == -1 ? "" : (_DHGK2 == 1 ? "đạt" : "ko");

            //////////////////////////////////////////////

            oWorkSheet.Cells[indexRow, 16] = dr0[CandidatesKeys.Field_Candidate_VT] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_VT].ToString());
            var _DHTB = dr0[CandidatesKeys.Field_Candidate_DHTB] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_DHTB].ToString());
            //oWorkSheet.Cells[indexRow, 18] = DHTB == -1 ? "" : DHTB.ToString(StringFormat.FormatMark);                
            var _remark1 = dr0[CandidatesKeys.Field_Candidate_RemarkR1] == DBNull.Value
                ? ""
                : dr0[CandidatesKeys.Field_Candidate_RemarkR1].ToString();
            if (_DHTB == 1)
                oWorkSheet.Cells[indexRow, 17] = "Đạt(" + _remark1 + ")";
            else
                oWorkSheet.Cells[indexRow, 17] = "(" + _remark1 + ")";
            var _Result = dr0[CandidatesKeys.FIELD_CANDIDATE_RESULT] == DBNull.Value
                ? -1
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_RESULT].ToString());
            if (_Result >= 3)
                oWorkSheet.Cells[indexRow, 18] = "Đạt";
            else
                oWorkSheet.Cells[indexRow, 18] = "";
            oWorkSheet.Cells[indexRow, 19] = dr0[CandidatesKeys.Field_Candidate_RemarkLR] == DBNull.Value
                ? ""
                : dr0[CandidatesKeys.Field_Candidate_RemarkLR].ToString();

            #endregion

            for (var i = 1; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                var dr_1 = dt.Rows[i - 1];

                positionId = dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());
                positionIdBefore = dr_1[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr_1[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());

                indexRow++;

                var PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[PositionKeys.FIELD_POSITION_NAME].ToString();

                if (positionId != positionIdBefore)
                {
                    rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
                    rangePositonName.Merge(Type.Missing);
                    oWorkSheet.Cells[indexRow, 1] = PositionName.ToUpper();
                    rangePositonName.Font.Bold = true;
                    indexRow++;
                }

                /// inserting order number
                oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                oWorkSheet.Cells[indexRow, 2] = dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
                oWorkSheet.Cells[indexRow, 3] = dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
                var DayOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
                var MonthOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
                var YearOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
                var BirthDay = string.Empty;
                if ((DayOfBirth > 0) && (MonthOfBirth > 0) && (YearOfBirth > 0))
                    BirthDay = DayOfBirth + "/" + MonthOfBirth + "/" + YearOfBirth;
                else if ((MonthOfBirth > 0) && (YearOfBirth > 0))
                    BirthDay = MonthOfBirth + "/" + YearOfBirth;
                else if (YearOfBirth > 0)
                    BirthDay = YearOfBirth.ToString();
                else
                    BirthDay = string.Empty;
                oWorkSheet.Cells[indexRow, 4] = BirthDay;
                var Sex = dr[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                    ? false
                    : bool.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
                if (Sex)
                    oWorkSheet.Cells[indexRow, 5] = "Nam";
                else
                    oWorkSheet.Cells[indexRow, 5] = "Nữ";
                /////////////////////////////////////////////
                //// Trinh do van hoa
                var Id = dr[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
                var EducationLevel = string.Empty;
                var ngoaingu = "";
                var listCEL = CandidateEducationLevelsBLL.GetById(Id);
                for (var j = 0; j < listCEL.Count; j++)
                {
                    var objCEL = listCEL[j];

                    if ((objCEL.EducationLevelId == 2) || (objCEL.EducationLevelId == 3) ||
                        (objCEL.EducationLevelId == 4)
                        || (objCEL.EducationLevelId == 15) || (objCEL.EducationLevelId == 16) ||
                        (objCEL.EducationLevelId == 17) || (objCEL.EducationLevelId == 18))
                    {
                        if (j == listCEL.Count - 1)
                            ngoaingu += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue;
                        else
                            ngoaingu += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
                    }
                    else
                    {
                        if (j == listCEL.Count - 1)
                            EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue;
                        else
                            EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue +
                                              "\n";
                    }
                }
                oWorkSheet.Cells[indexRow, 6] = EducationLevel;
                oWorkSheet.Cells[indexRow, 7] = ngoaingu;

                /////////////////////////////////////////////
                // diem vong 3
                var NNLRGK1 = dr[CandidatesKeys.Field_Candidate_NNLRGK1] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NNLRGK1].ToString());
                oWorkSheet.Cells[indexRow, 8] = NNLRGK1 == -1 ? "" : (NNLRGK1 == 1 ? "đạt" : "ko");
                var NNLRGK2 = dr[CandidatesKeys.Field_Candidate_NNLRGK2] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NNLRGK2].ToString());
                oWorkSheet.Cells[indexRow, 9] = NNLRGK2 == -1 ? "" : (NNLRGK2 == 1 ? "đạt" : "ko");
                var NNLRGK3 = dr[CandidatesKeys.Field_Candidate_NNLRGK3] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NNLRGK3].ToString());
                oWorkSheet.Cells[indexRow, 10] = NNLRGK3 == -1 ? "" : (NNLRGK3 == 1 ? "đạt" : "ko");
                var NNLRTB = dr[CandidatesKeys.Field_Candidate_NNLRTB] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NNLRTB].ToString());
                oWorkSheet.Cells[indexRow, 11] = NNLRTB == -1 ? "" : (NNLRTB == 1 ? "đạt" : "ko");
                //double DHGK1 = dr[CandidatesKeys.Field_Candidate_DHGK1] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_DHGK1].ToString());
                //oWorkSheet.Cells[indexRow, 11] = DHGK1 == -1 ? "" : (DHGK1 == 1 ? "đạt" : "ko");


                var NHLRGK1 = dr[CandidatesKeys.Field_Candidate_NHLRGK1] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NHLRGK1].ToString());
                oWorkSheet.Cells[indexRow, 12] = NHLRGK1 == -1 ? "" : (NHLRGK1 == 1 ? "đạt" : "ko");
                var NHLRGK2 = dr[CandidatesKeys.Field_Candidate_NHLRGK2] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NHLRGK2].ToString());
                oWorkSheet.Cells[indexRow, 13] = NHLRGK2 == -1 ? "" : (NHLRGK2 == 1 ? "đạt" : "ko");
                var NHLRGK3 = dr[CandidatesKeys.Field_Candidate_NHLRGK3] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NHLRGK3].ToString());
                oWorkSheet.Cells[indexRow, 14] = NHLRGK3 == -1 ? "" : (NHLRGK3 == 1 ? "đạt" : "ko");
                var NHLRTB = dr[CandidatesKeys.Field_Candidate_NHLRTB] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NHLRTB].ToString());
                oWorkSheet.Cells[indexRow, 15] = NHLRTB == -1 ? "" : (NHLRTB == 1 ? "đạt" : "ko");
                //double DHGK2 = dr[CandidatesKeys.Field_Candidate_DHGK2] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_DHGK2].ToString());
                //oWorkSheet.Cells[indexRow, 16] = DHGK2 == -1 ? "" : (DHGK2 == 1 ? "đạt" : "ko");


                //////////////////////////////////////////////

                //oWorkSheet.Cells[indexRow, 7] = dr[CandidatesKeys.Field_Candidate_DHTB] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_DHTB].ToString());
                oWorkSheet.Cells[indexRow, 16] = dr[CandidatesKeys.Field_Candidate_VT] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_VT].ToString());
                var DHTB = dr[CandidatesKeys.Field_Candidate_DHTB] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_DHTB].ToString());
                if (DHTB == 1)
                    oWorkSheet.Cells[indexRow, 17] = "Đạt(" + _remark1 + ")";
                else
                    oWorkSheet.Cells[indexRow, 17] = "(" + _remark1 + ")";
                var Result = dr[CandidatesKeys.FIELD_CANDIDATE_RESULT] == DBNull.Value
                    ? -1
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_RESULT].ToString());
                if (Result >= 3)
                    oWorkSheet.Cells[indexRow, 18] = "Đạt";
                else
                    oWorkSheet.Cells[indexRow, 18] = "";
                oWorkSheet.Cells[indexRow, 19] = dr[CandidatesKeys.Field_Candidate_RemarkLR] == DBNull.Value
                    ? ""
                    : dr[CandidatesKeys.Field_Candidate_RemarkLR].ToString();
            }

            // release range object            
            var range = oWorkSheet.get_Range("A" + (initTitleIndex - 1), "T" + indexRow);
            range.Font.Size = 10;
            range.Font.Name = "Times New Roman";
            range.Borders.LineStyle = Constants.xlSolid;
        }

        private static void CreateHeaderAndTitleRBoard(ref _Worksheet oWorkSheet, ref int initTitleIndex, DateTime date)
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


            var rangeHeader3 = oWorkSheet.get_Range("A3", "D3");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[3, 1] = "HỘI ĐỒNG TUYỂN DỤNG";
            rangeHeader3.Font.Size = 12;
            rangeHeader3.Font.Bold = true;


            var rangeHeader4 = oWorkSheet.get_Range("A4", "H4");
            rangeHeader4.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "KẾT QUẢ THI VÒNG PHỎNG VẤN HỘI ĐỒNG";
            rangeHeader4.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader4.Font.Size = 15;
            rangeHeader4.Font.Bold = true;

            #endregion

            #region Create Title for Account List from oWorkSheet

            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            var rangeHeaderColunm = oWorkSheet.get_Range("A" + (initTitleIndex - 1), "A" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 2] = "HỌ VÀ TÊN LÓT";
            rangeHeaderColunm = oWorkSheet.get_Range("B" + (initTitleIndex - 1), "B" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            ((Range) oWorkSheet.Cells[initTitleIndex, 2]).ColumnWidth = 30;
            oWorkSheet.Cells[initTitleIndex, 3] = "TÊN";
            rangeHeaderColunm = oWorkSheet.get_Range("C" + (initTitleIndex - 1), "C" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 4] = "NĂM SINH";
            rangeHeaderColunm = oWorkSheet.get_Range("D" + (initTitleIndex - 1), "D" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 5] = "GIỚI TÍNH";
            rangeHeaderColunm = oWorkSheet.get_Range("E" + (initTitleIndex - 1), "E" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 6] = "TRÌNH ĐỘ";
            rangeHeaderColunm = oWorkSheet.get_Range("F" + (initTitleIndex - 1), "F" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            ((Range) oWorkSheet.Cells[initTitleIndex, 6]).ColumnWidth = 30;

            oWorkSheet.Cells[initTitleIndex, 7] = "TRÌNH ĐỘ NGOẠI NGỮ";
            rangeHeaderColunm = oWorkSheet.get_Range("G" + (initTitleIndex - 1), "G" + initTitleIndex);
            rangeHeaderColunm.Merge(Type.Missing);
            ((Range) oWorkSheet.Cells[initTitleIndex, 7]).ColumnWidth = 30;

            // ngoai ngu
            var rangeHeaderNgoaiNgu = oWorkSheet.get_Range("H" + (initTitleIndex - 1), "K" + (initTitleIndex - 1));
            oWorkSheet.Cells[initTitleIndex - 1, 8] = "NGOẠI NGỮ";

            rangeHeaderNgoaiNgu.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 8] = "GK1";
            oWorkSheet.Cells[initTitleIndex, 9] = "GK2";
            oWorkSheet.Cells[initTitleIndex, 10] = "GK3";
            oWorkSheet.Cells[initTitleIndex, 11] = "GK4";
            //oWorkSheet.Cells[initTitleIndex, 11] = "GK5";

            //// ngoai hinh
            var rangeHeaderNgoaiHinh = oWorkSheet.get_Range("L" + (initTitleIndex - 1), "O" + (initTitleIndex - 1));
            oWorkSheet.Cells[initTitleIndex - 1, 12] = "NGOẠI HÌNH";
            rangeHeaderNgoaiHinh.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 12] = "GK1";
            oWorkSheet.Cells[initTitleIndex, 13] = "GK2";
            oWorkSheet.Cells[initTitleIndex, 14] = "GK3";
            oWorkSheet.Cells[initTitleIndex, 15] = "GK4";
            //oWorkSheet.Cells[initTitleIndex, 16] = "GK5";


            oWorkSheet.Cells[initTitleIndex, 16] = "VI TÍNH";
            oWorkSheet.Cells[initTitleIndex, 17] = "DỊCH ANH VIỆT";
            oWorkSheet.Cells[initTitleIndex, 18] = "KẾT QUẢ";
            oWorkSheet.Cells[initTitleIndex, 19] = "GHI CHÚ";
            ((Range) oWorkSheet.Cells[initTitleIndex, 19]).ColumnWidth = 50;

            var rangeHeader5 = oWorkSheet.get_Range("A" + (initTitleIndex - 1), "T" + initTitleIndex);
            rangeHeader5.Font.Size = 10;
            rangeHeader5.Font.Name = "Times New Roman";
            rangeHeader5.Font.Bold = true;

            #endregion
        }

        #endregion

        #region Export Driving Theory

        public static string ExportDrivingTheory(string firstName, int positionId, int result, int sessionId, int type,
            int sessionType, int typeSort, string pathName)
        {
            ///////////////////////////////////////////
            var dt = new CandidatesDAL().GetByFilter(firstName, positionId, result, sessionId, type, sessionType,
                typeSort);
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

                    oWorkSheet.Name = "KetQuaThiLaiXeVongLyThuyet";

                    InsertDataToWorkSheetDrivingTheory(dt, ref oWorkSheet, DateTime.Now);

                    oExcel.Visible = false;
                    oExcel.UserControl = false;
                    fileName = pathName + "\\DrivingTheory.xls";
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

        private static void InsertDataToWorkSheetDrivingTheory(DataTable dt, ref _Worksheet oWorkSheet, DateTime date)
        {
            var positionId = 0;
            var positionIdBefore = 0;
            var initTitleIndex = 8;
            var indexRow = initTitleIndex + 1;
            var orderNumber = 1;
            var value = string.Empty;
            CreateHeaderAndTitleDrivingTheory(ref oWorkSheet, ref initTitleIndex, date);
            var dr0 = dt.Rows[0];

            var rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName.Merge(Type.Missing);
            var _PositionName = dr0[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                ? string.Empty
                : dr0[PositionKeys.FIELD_POSITION_NAME].ToString();
            oWorkSheet.Cells[indexRow, 1] = _PositionName.ToUpper();
            rangePositonName.Font.Bold = true;

            #region Create FirstRow

            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            oWorkSheet.Cells[indexRow, 2] = dr0[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
            oWorkSheet.Cells[indexRow, 3] = dr0[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
            var _DayOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
            var _MonthOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
            var _YearOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
            var _BirthDay = string.Empty;
            if ((_DayOfBirth > 0) && (_MonthOfBirth > 0) && (_YearOfBirth > 0))
                _BirthDay = _DayOfBirth + "/" + _MonthOfBirth + "/" + _YearOfBirth;
            else if ((_MonthOfBirth > 0) && (_YearOfBirth > 0))
                _BirthDay = _MonthOfBirth + "/" + _YearOfBirth;
            else if (_YearOfBirth > 0)
                _BirthDay = _YearOfBirth.ToString();
            else
                _BirthDay = string.Empty;
            oWorkSheet.Cells[indexRow, 4] = _BirthDay;
            var _Sex = dr0[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                ? false
                : bool.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
            if (_Sex)
                oWorkSheet.Cells[indexRow, 5] = "Nam";
            else
                oWorkSheet.Cells[indexRow, 5] = "Nữ";
            /////////////////////////////////////////////
            //// Trinh do van hoa
            var _Id = dr0[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
            var _EducationLevel = string.Empty;
            var _listCEL = CandidateEducationLevelsBLL.GetById(_Id);
            for (var i = 0; i < _listCEL.Count; i++)
            {
                var objCEL = _listCEL[i];
                _EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
            }
            oWorkSheet.Cells[indexRow, 6] = _EducationLevel;
            /////////////////////////////////////////////
            oWorkSheet.Cells[indexRow, 7] = dr0[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE].ToString();

            oWorkSheet.Cells[indexRow, 8] = dr0[CandidatesKeys.Field_Candidate_NNGK1] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NNGK1].ToString());
            oWorkSheet.Cells[indexRow, 9] = dr0[CandidatesKeys.Field_Candidate_NNGK2] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NNGK2].ToString());
            var _Result = dr0[CandidatesKeys.FIELD_CANDIDATE_RESULT] == DBNull.Value
                ? -1
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_RESULT].ToString());
            if (_Result == 1)
                oWorkSheet.Cells[indexRow, 10] = "Đạt";
            else
                oWorkSheet.Cells[indexRow, 10] = "";

            #endregion

            for (var i = 1; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                var dr_1 = dt.Rows[i - 1];

                positionId = dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());
                positionIdBefore = dr_1[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr_1[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());

                indexRow++;

                var PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[PositionKeys.FIELD_POSITION_NAME].ToString();

                if (positionId != positionIdBefore)
                {
                    rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
                    rangePositonName.Merge(Type.Missing);
                    oWorkSheet.Cells[indexRow, 1] = PositionName.ToUpper();
                    rangePositonName.Font.Bold = true;
                    indexRow++;
                }

                /// inserting order number
                oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                oWorkSheet.Cells[indexRow, 2] = dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
                oWorkSheet.Cells[indexRow, 3] = dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
                var DayOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
                var MonthOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
                var YearOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
                var BirthDay = string.Empty;
                if ((DayOfBirth > 0) && (MonthOfBirth > 0) && (YearOfBirth > 0))
                    BirthDay = DayOfBirth + "/" + MonthOfBirth + "/" + YearOfBirth;
                else if ((MonthOfBirth > 0) && (YearOfBirth > 0))
                    BirthDay = MonthOfBirth + "/" + YearOfBirth;
                else if (YearOfBirth > 0)
                    BirthDay = YearOfBirth.ToString();
                else
                    BirthDay = string.Empty;
                oWorkSheet.Cells[indexRow, 4] = BirthDay;
                var Sex = dr[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                    ? false
                    : bool.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
                if (Sex)
                    oWorkSheet.Cells[indexRow, 5] = "Nam";
                else
                    oWorkSheet.Cells[indexRow, 5] = "Nữ";
                /////////////////////////////////////////////
                //// Trinh do van hoa
                var Id = dr[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
                var EducationLevel = string.Empty;
                var listCEL = CandidateEducationLevelsBLL.GetById(Id);
                for (var j = 0; j < listCEL.Count; j++)
                {
                    var objCEL = listCEL[j];

                    if (j == listCEL.Count - 1)
                        EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue;
                    else
                        EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
                }
                oWorkSheet.Cells[indexRow, 6] = EducationLevel;
                oWorkSheet.Cells[indexRow, 7] = dr[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE].ToString();
                oWorkSheet.Cells[indexRow, 8] = dr[CandidatesKeys.Field_Candidate_NNGK1] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NNGK1].ToString());
                oWorkSheet.Cells[indexRow, 9] = dr[CandidatesKeys.Field_Candidate_NNGK2] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NNGK2].ToString());
                var Result = dr[CandidatesKeys.FIELD_CANDIDATE_RESULT] == DBNull.Value
                    ? -1
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_RESULT].ToString());
                if (Result == 1)
                    oWorkSheet.Cells[indexRow, 10] = "Đạt";
                else
                    oWorkSheet.Cells[indexRow, 10] = "Ko";
            }

            // release range object            
            var range = oWorkSheet.get_Range("A" + initTitleIndex, "J" + indexRow);
            range.Font.Size = 10;
            range.Font.Name = "Times New Roman";
            range.Borders.LineStyle = Constants.xlSolid;
        }

        private static void CreateHeaderAndTitleDrivingTheory(ref _Worksheet oWorkSheet, ref int initTitleIndex,
            DateTime date)
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

            var rangeHeader3 = oWorkSheet.get_Range("A3", "D3");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[3, 1] = "HỘI ĐỒNG TUYỂN DỤNG";
            rangeHeader3.Font.Size = 12;
            rangeHeader3.Font.Bold = true;

            var rangeHeader4 = oWorkSheet.get_Range("A4", "H4");
            rangeHeader4.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "KẾT QUẢ THI LÁI XE - VÒNG LÝ THUYẾT";
            rangeHeader4.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader4.Font.Size = 15;
            rangeHeader4.Font.Bold = true;

            #endregion

            #region Create Title for Account List from oWorkSheet

            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            oWorkSheet.Cells[initTitleIndex, 2] = "Họ và tên lót";
            ((Range) oWorkSheet.Cells[initTitleIndex, 2]).ColumnWidth = 30;
            oWorkSheet.Cells[initTitleIndex, 3] = "Tên";
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 4] = "Năm sinh";
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 5] = "Giới tính";
            oWorkSheet.Cells[initTitleIndex, 6] = "Trình độ";
            ((Range) oWorkSheet.Cells[initTitleIndex, 6]).ColumnWidth = 30;
            oWorkSheet.Cells[initTitleIndex, 7] = "Kinh nghiệm";
            oWorkSheet.Cells[initTitleIndex, 8] = "Luật GTĐB";
            oWorkSheet.Cells[initTitleIndex, 9] = "LT Ôtô cơ bản";
            oWorkSheet.Cells[initTitleIndex, 10] = "Kết Quả";

            var rangeHeader5 = oWorkSheet.get_Range("A7", "J7");
            rangeHeader5.Font.Size = 10;
            rangeHeader5.Font.Name = "Times New Roman";
            rangeHeader5.Font.Bold = true;

            #endregion
        }

        #endregion

        #region Export Driving Practice

        public static string ExportDrivingPractice(string firstName, int positionId, int result, int sessionId, int type,
            int sessionType, int typeSort, string pathName)
        {
            ///////////////////////////////////////////
            var dt = new CandidatesDAL().GetByFilter(firstName, positionId, result, sessionId, type, sessionType,
                typeSort);
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

                    oWorkSheet.Name = "KetQuaThiLaiXeVongThucHanh";

                    InsertDataToWorkSheetDrivingPractice(dt, ref oWorkSheet, DateTime.Now);

                    oExcel.Visible = false;
                    oExcel.UserControl = false;
                    fileName = pathName + "\\DrivingPractice.xls";
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

        private static void InsertDataToWorkSheetDrivingPractice(DataTable dt, ref _Worksheet oWorkSheet, DateTime date)
        {
            var positionId = 0;
            var positionIdBefore = 0;
            var initTitleIndex = 8;
            var indexRow = initTitleIndex + 1;
            var orderNumber = 1;
            var value = string.Empty;
            CreateHeaderAndTitleDrivingPractice(ref oWorkSheet, ref initTitleIndex, date);
            var dr0 = dt.Rows[0];

            var rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName.Merge(Type.Missing);
            var _PositionName = dr0[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                ? string.Empty
                : dr0[PositionKeys.FIELD_POSITION_NAME].ToString();
            oWorkSheet.Cells[indexRow, 1] = _PositionName.ToUpper();
            rangePositonName.Font.Bold = true;

            #region Create FirstRow

            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            oWorkSheet.Cells[indexRow, 2] = dr0[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
            oWorkSheet.Cells[indexRow, 3] = dr0[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
            var _DayOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
            var _MonthOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
            var _YearOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
            var _BirthDay = string.Empty;
            if ((_DayOfBirth > 0) && (_MonthOfBirth > 0) && (_YearOfBirth > 0))
                _BirthDay = _DayOfBirth + "/" + _MonthOfBirth + "/" + _YearOfBirth;
            else if ((_MonthOfBirth > 0) && (_YearOfBirth > 0))
                _BirthDay = _MonthOfBirth + "/" + _YearOfBirth;
            else if (_YearOfBirth > 0)
                _BirthDay = _YearOfBirth.ToString();
            else
                _BirthDay = string.Empty;
            oWorkSheet.Cells[indexRow, 4] = _BirthDay;
            var _Sex = dr0[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                ? false
                : bool.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
            if (_Sex)
                oWorkSheet.Cells[indexRow, 5] = "Nam";
            else
                oWorkSheet.Cells[indexRow, 5] = "Nữ";
            /////////////////////////////////////////////
            //// Trinh do van hoa
            var _Id = dr0[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
            var _EducationLevel = string.Empty;
            var _listCEL = CandidateEducationLevelsBLL.GetById(_Id);
            for (var i = 0; i < _listCEL.Count; i++)
            {
                var objCEL = _listCEL[i];
                _EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
            }
            oWorkSheet.Cells[indexRow, 6] = _EducationLevel;
            /////////////////////////////////////////////
            oWorkSheet.Cells[indexRow, 7] = dr0[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE].ToString();

            oWorkSheet.Cells[indexRow, 8] = dr0[CandidatesKeys.Field_Candidate_NNGK1] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NNGK1].ToString());
            oWorkSheet.Cells[indexRow, 9] = dr0[CandidatesKeys.Field_Candidate_NNGK2] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NNGK2].ToString());
            oWorkSheet.Cells[indexRow, 10] = dr0[CandidatesKeys.Field_Candidate_NNGK3] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NNGK3].ToString());
            oWorkSheet.Cells[indexRow, 11] = dr0[CandidatesKeys.Field_Candidate_NNTB] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NNTB].ToString());
            var _Result = dr0[CandidatesKeys.FIELD_CANDIDATE_RESULT] == DBNull.Value
                ? -1
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_RESULT].ToString());
            if (_Result == 2)
                oWorkSheet.Cells[indexRow, 12] = "Đạt";
            else
                oWorkSheet.Cells[indexRow, 12] = "";

            #endregion

            for (var i = 1; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                var dr_1 = dt.Rows[i - 1];

                positionId = dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());
                positionIdBefore = dr_1[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr_1[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());

                indexRow++;

                var PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[PositionKeys.FIELD_POSITION_NAME].ToString();

                if (positionId != positionIdBefore)
                {
                    rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
                    rangePositonName.Merge(Type.Missing);
                    oWorkSheet.Cells[indexRow, 1] = PositionName.ToUpper();
                    rangePositonName.Font.Bold = true;
                    indexRow++;
                }

                /// inserting order number
                oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                oWorkSheet.Cells[indexRow, 2] = dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
                oWorkSheet.Cells[indexRow, 3] = dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
                var DayOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
                var MonthOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
                var YearOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
                var BirthDay = string.Empty;
                if ((DayOfBirth > 0) && (MonthOfBirth > 0) && (YearOfBirth > 0))
                    BirthDay = DayOfBirth + "/" + MonthOfBirth + "/" + YearOfBirth;
                else if ((MonthOfBirth > 0) && (YearOfBirth > 0))
                    BirthDay = MonthOfBirth + "/" + YearOfBirth;
                else if (YearOfBirth > 0)
                    BirthDay = YearOfBirth.ToString();
                else
                    BirthDay = string.Empty;
                oWorkSheet.Cells[indexRow, 4] = BirthDay;
                var Sex = dr[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                    ? false
                    : bool.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
                if (Sex)
                    oWorkSheet.Cells[indexRow, 5] = "Nam";
                else
                    oWorkSheet.Cells[indexRow, 5] = "Nữ";
                /////////////////////////////////////////////
                //// Trinh do van hoa
                var Id = dr[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
                var EducationLevel = string.Empty;
                var listCEL = CandidateEducationLevelsBLL.GetById(Id);
                for (var j = 0; j < listCEL.Count; j++)
                {
                    var objCEL = listCEL[j];

                    if (j == listCEL.Count - 1)
                        EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue;
                    else
                        EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
                }
                oWorkSheet.Cells[indexRow, 6] = EducationLevel;
                oWorkSheet.Cells[indexRow, 7] = dr[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE].ToString();
                oWorkSheet.Cells[indexRow, 8] = dr[CandidatesKeys.Field_Candidate_NNGK1] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NNGK1].ToString());
                oWorkSheet.Cells[indexRow, 9] = dr[CandidatesKeys.Field_Candidate_NNGK2] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NNGK2].ToString());
                oWorkSheet.Cells[indexRow, 10] = dr[CandidatesKeys.Field_Candidate_NNGK3] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NNGK3].ToString());
                oWorkSheet.Cells[indexRow, 11] = dr[CandidatesKeys.Field_Candidate_NNTB] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NNTB].ToString());
                var Result = dr[CandidatesKeys.FIELD_CANDIDATE_RESULT] == DBNull.Value
                    ? -1
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_RESULT].ToString());
                if (Result == 1)
                    oWorkSheet.Cells[indexRow, 12] = "Đạt";
                else
                    oWorkSheet.Cells[indexRow, 12] = "Ko";
            }

            // release range object            
            var range = oWorkSheet.get_Range("A" + initTitleIndex, "L" + indexRow);
            range.Font.Size = 10;
            range.Font.Name = "Times New Roman";
            range.Borders.LineStyle = Constants.xlSolid;
        }

        private static void CreateHeaderAndTitleDrivingPractice(ref _Worksheet oWorkSheet, ref int initTitleIndex,
            DateTime date)
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

            var rangeHeader3 = oWorkSheet.get_Range("A3", "D3");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[3, 1] = "HỘI ĐỒNG TUYỂN DỤNG";
            rangeHeader3.Font.Size = 12;
            rangeHeader3.Font.Bold = true;

            var rangeHeader4 = oWorkSheet.get_Range("A4", "H4");
            rangeHeader4.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "KẾT QUẢ THI LÁI XE - VÒNG THỰC HÀNH";
            rangeHeader4.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader4.Font.Size = 15;
            rangeHeader4.Font.Bold = true;

            #endregion

            #region Create Title for Account List from oWorkSheet

            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            oWorkSheet.Cells[initTitleIndex, 2] = "Họ và tên lót";
            ((Range) oWorkSheet.Cells[initTitleIndex, 2]).ColumnWidth = 30;
            oWorkSheet.Cells[initTitleIndex, 3] = "Tên";
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 4] = "Năm sinh";
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 5] = "Giới tính";
            oWorkSheet.Cells[initTitleIndex, 6] = "Trình độ";
            ((Range) oWorkSheet.Cells[initTitleIndex, 6]).ColumnWidth = 30;
            oWorkSheet.Cells[initTitleIndex, 7] = "Kinh nghiệm";
            oWorkSheet.Cells[initTitleIndex, 8] = "Luật GTĐB";
            oWorkSheet.Cells[initTitleIndex, 9] = "LT Ôtô cơ bản";
            oWorkSheet.Cells[initTitleIndex, 10] = "Thực Hành";
            oWorkSheet.Cells[initTitleIndex, 11] = "Điểm Trung Bình";
            oWorkSheet.Cells[initTitleIndex, 12] = "Kết Quả";

            var rangeHeader5 = oWorkSheet.get_Range("A7", "L7");
            rangeHeader5.Font.Size = 10;
            rangeHeader5.Font.Name = "Times New Roman";
            rangeHeader5.Font.Bold = true;

            #endregion
        }

        #endregion

        #region Export Repairing Theory

        public static string ExportRepairingTheory(string firstName, int positionId, int result, int sessionId, int type,
            int sessionType, int typeSort, string pathName)
        {
            ///////////////////////////////////////////
            var dt = new CandidatesDAL().GetByFilter(firstName, positionId, result, sessionId, type, sessionType,
                typeSort);
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

                    oWorkSheet.Name = "KetQuaThiSCTTBVongLyThuyet";

                    InsertDataToWorkSheetRepairingTheory(dt, ref oWorkSheet, DateTime.Now);

                    oExcel.Visible = false;
                    oExcel.UserControl = false;
                    fileName = pathName + "\\RepairingTheory.xls";
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

        private static void InsertDataToWorkSheetRepairingTheory(DataTable dt, ref _Worksheet oWorkSheet, DateTime date)
        {
            var positionId = 0;
            var positionIdBefore = 0;
            var initTitleIndex = 8;
            var indexRow = initTitleIndex + 1;
            var orderNumber = 1;
            var value = string.Empty;
            CreateHeaderAndTitleRepairingTheory(ref oWorkSheet, ref initTitleIndex, date);
            var dr0 = dt.Rows[0];

            var rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName.Merge(Type.Missing);
            var _PositionName = dr0[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                ? string.Empty
                : dr0[PositionKeys.FIELD_POSITION_NAME].ToString();
            oWorkSheet.Cells[indexRow, 1] = _PositionName.ToUpper();
            rangePositonName.Font.Bold = true;

            #region Create FirstRow

            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            oWorkSheet.Cells[indexRow, 2] = dr0[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
            oWorkSheet.Cells[indexRow, 3] = dr0[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
            var _DayOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
            var _MonthOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
            var _YearOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
            var _BirthDay = string.Empty;
            if ((_DayOfBirth > 0) && (_MonthOfBirth > 0) && (_YearOfBirth > 0))
                _BirthDay = _DayOfBirth + "/" + _MonthOfBirth + "/" + _YearOfBirth;
            else if ((_MonthOfBirth > 0) && (_YearOfBirth > 0))
                _BirthDay = _MonthOfBirth + "/" + _YearOfBirth;
            else if (_YearOfBirth > 0)
                _BirthDay = _YearOfBirth.ToString();
            else
                _BirthDay = string.Empty;
            oWorkSheet.Cells[indexRow, 4] = _BirthDay;
            var _Sex = dr0[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                ? false
                : bool.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
            if (_Sex)
                oWorkSheet.Cells[indexRow, 5] = "Nam";
            else
                oWorkSheet.Cells[indexRow, 5] = "Nữ";
            /////////////////////////////////////////////
            //// Trinh do van hoa
            var _Id = dr0[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
            var _EducationLevel = string.Empty;
            var _listCEL = CandidateEducationLevelsBLL.GetById(_Id);
            for (var i = 0; i < _listCEL.Count; i++)
            {
                var objCEL = _listCEL[i];
                _EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
            }
            oWorkSheet.Cells[indexRow, 6] = _EducationLevel;
            /////////////////////////////////////////////
            oWorkSheet.Cells[indexRow, 7] = dr0[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE].ToString();

            oWorkSheet.Cells[indexRow, 8] = dr0[CandidatesKeys.Field_Candidate_NNGK1] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NNGK1].ToString());
            var _Result = dr0[CandidatesKeys.FIELD_CANDIDATE_RESULT] == DBNull.Value
                ? -1
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_RESULT].ToString());
            if (_Result == 1)
                oWorkSheet.Cells[indexRow, 9] = "Đạt";
            else
                oWorkSheet.Cells[indexRow, 9] = "";

            oWorkSheet.Cells[indexRow, 10] = dr0[CandidatesKeys.Field_Candidate_RemarkR1] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.Field_Candidate_RemarkR1].ToString();

            #endregion

            for (var i = 1; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                var dr_1 = dt.Rows[i - 1];

                positionId = dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());
                positionIdBefore = dr_1[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr_1[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());

                indexRow++;

                var PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[PositionKeys.FIELD_POSITION_NAME].ToString();

                if (positionId != positionIdBefore)
                {
                    rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
                    rangePositonName.Merge(Type.Missing);
                    oWorkSheet.Cells[indexRow, 1] = PositionName.ToUpper();
                    rangePositonName.Font.Bold = true;
                    indexRow++;
                }

                /// inserting order number
                oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                oWorkSheet.Cells[indexRow, 2] = dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
                oWorkSheet.Cells[indexRow, 3] = dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
                var DayOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
                var MonthOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
                var YearOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
                var BirthDay = string.Empty;
                if ((DayOfBirth > 0) && (MonthOfBirth > 0) && (YearOfBirth > 0))
                    BirthDay = DayOfBirth + "/" + MonthOfBirth + "/" + YearOfBirth;
                else if ((MonthOfBirth > 0) && (YearOfBirth > 0))
                    BirthDay = MonthOfBirth + "/" + YearOfBirth;
                else if (YearOfBirth > 0)
                    BirthDay = YearOfBirth.ToString();
                else
                    BirthDay = string.Empty;
                oWorkSheet.Cells[indexRow, 4] = BirthDay;
                var Sex = dr[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                    ? false
                    : bool.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
                if (Sex)
                    oWorkSheet.Cells[indexRow, 5] = "Nam";
                else
                    oWorkSheet.Cells[indexRow, 5] = "Nữ";
                /////////////////////////////////////////////
                //// Trinh do van hoa
                var Id = dr[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
                var EducationLevel = string.Empty;
                var listCEL = CandidateEducationLevelsBLL.GetById(Id);
                for (var j = 0; j < listCEL.Count; j++)
                {
                    var objCEL = listCEL[j];

                    if (j == listCEL.Count - 1)
                        EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue;
                    else
                        EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
                }
                oWorkSheet.Cells[indexRow, 6] = EducationLevel;
                oWorkSheet.Cells[indexRow, 7] = dr[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE].ToString();
                oWorkSheet.Cells[indexRow, 8] = dr[CandidatesKeys.Field_Candidate_NNGK1] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NNGK1].ToString());
                var Result = dr[CandidatesKeys.FIELD_CANDIDATE_RESULT] == DBNull.Value
                    ? -1
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_RESULT].ToString());
                if (Result == 1)
                    oWorkSheet.Cells[indexRow, 9] = "Đạt";
                else
                    oWorkSheet.Cells[indexRow, 9] = "Ko";
                oWorkSheet.Cells[indexRow, 10] = dr[CandidatesKeys.Field_Candidate_RemarkR1] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.Field_Candidate_RemarkR1].ToString();
            }

            // release range object            
            var range = oWorkSheet.get_Range("A" + initTitleIndex, "J" + indexRow);
            range.Font.Size = 10;
            range.Font.Name = "Times New Roman";
            range.Borders.LineStyle = Constants.xlSolid;
        }

        private static void CreateHeaderAndTitleRepairingTheory(ref _Worksheet oWorkSheet, ref int initTitleIndex,
            DateTime date)
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

            var rangeHeader3 = oWorkSheet.get_Range("A3", "D3");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[3, 1] = "HỘI ĐỒNG TUYỂN DỤNG";
            rangeHeader3.Font.Size = 12;
            rangeHeader3.Font.Bold = true;

            var rangeHeader4 = oWorkSheet.get_Range("A4", "H4");
            rangeHeader4.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "KẾT QUẢ THI SỮA CHỮA TRANG THIẾT BỊ - VÒNG LÝ THUYẾT";
            rangeHeader4.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader4.Font.Size = 15;
            rangeHeader4.Font.Bold = true;

            #endregion

            #region Create Title for Account List from oWorkSheet

            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            oWorkSheet.Cells[initTitleIndex, 2] = "Họ và tên lót";
            ((Range) oWorkSheet.Cells[initTitleIndex, 2]).ColumnWidth = 20;
            oWorkSheet.Cells[initTitleIndex, 3] = "Tên";
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 4] = "Năm sinh";
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 5] = "Giới tính";
            oWorkSheet.Cells[initTitleIndex, 6] = "Trình độ";
            ((Range) oWorkSheet.Cells[initTitleIndex, 6]).ColumnWidth = 35;
            oWorkSheet.Cells[initTitleIndex, 7] = "Kinh nghiệm";
            oWorkSheet.Cells[initTitleIndex, 8] = "Lý thuyết";
            oWorkSheet.Cells[initTitleIndex, 9] = "Kết Quả";
            oWorkSheet.Cells[initTitleIndex, 10] = "Ghi chú";
            ((Range) oWorkSheet.Cells[initTitleIndex, 10]).ColumnWidth = 40;

            var rangeHeader5 = oWorkSheet.get_Range("A7", "J7");
            rangeHeader5.Font.Size = 10;
            rangeHeader5.Font.Name = "Times New Roman";
            rangeHeader5.Font.Bold = true;

            #endregion
        }

        #endregion

        #region Export Repairing Practice

        public static string ExportRepairingPractice(string firstName, int positionId, int result, int sessionId,
            int type, int sessionType, int typeSort, string pathName)
        {
            ///////////////////////////////////////////
            var dt = new CandidatesDAL().GetByFilter(firstName, positionId, result, sessionId, type, sessionType,
                typeSort);
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

                    oWorkSheet.Name = "KetQuaThiSCTTBVongThucHanh";

                    InsertDataToWorkSheetRepairingPractice(dt, ref oWorkSheet, DateTime.Now);

                    oExcel.Visible = false;
                    oExcel.UserControl = false;
                    fileName = pathName + "\\RepairingPractice.xls";
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

        private static void InsertDataToWorkSheetRepairingPractice(DataTable dt, ref _Worksheet oWorkSheet,
            DateTime date)
        {
            var positionId = 0;
            var positionIdBefore = 0;
            var initTitleIndex = 8;
            var indexRow = initTitleIndex + 1;
            var orderNumber = 1;
            var value = string.Empty;
            CreateHeaderAndTitleRepairingPractice(ref oWorkSheet, ref initTitleIndex, date);
            var dr0 = dt.Rows[0];

            var rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName.Merge(Type.Missing);
            var _PositionName = dr0[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                ? string.Empty
                : dr0[PositionKeys.FIELD_POSITION_NAME].ToString();
            oWorkSheet.Cells[indexRow, 1] = _PositionName.ToUpper();
            rangePositonName.Font.Bold = true;

            #region Create FirstRow

            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            oWorkSheet.Cells[indexRow, 2] = dr0[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
            oWorkSheet.Cells[indexRow, 3] = dr0[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
            var _DayOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
            var _MonthOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
            var _YearOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
            var _BirthDay = string.Empty;
            if ((_DayOfBirth > 0) && (_MonthOfBirth > 0) && (_YearOfBirth > 0))
                _BirthDay = _DayOfBirth + "/" + _MonthOfBirth + "/" + _YearOfBirth;
            else if ((_MonthOfBirth > 0) && (_YearOfBirth > 0))
                _BirthDay = _MonthOfBirth + "/" + _YearOfBirth;
            else if (_YearOfBirth > 0)
                _BirthDay = _YearOfBirth.ToString();
            else
                _BirthDay = string.Empty;
            oWorkSheet.Cells[indexRow, 4] = _BirthDay;
            var _Sex = dr0[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                ? false
                : bool.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
            if (_Sex)
                oWorkSheet.Cells[indexRow, 5] = "Nam";
            else
                oWorkSheet.Cells[indexRow, 5] = "Nữ";
            /////////////////////////////////////////////
            //// Trinh do van hoa
            var _Id = dr0[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
            var _EducationLevel = string.Empty;
            var _listCEL = CandidateEducationLevelsBLL.GetById(_Id);
            for (var i = 0; i < _listCEL.Count; i++)
            {
                var objCEL = _listCEL[i];
                _EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
            }
            oWorkSheet.Cells[indexRow, 6] = _EducationLevel;
            /////////////////////////////////////////////
            oWorkSheet.Cells[indexRow, 7] = dr0[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE].ToString();

            oWorkSheet.Cells[indexRow, 8] = dr0[CandidatesKeys.Field_Candidate_NNGK1] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NNGK1].ToString());
            oWorkSheet.Cells[indexRow, 9] = dr0[CandidatesKeys.Field_Candidate_NNGK2] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NNGK2].ToString());

            var _Result = dr0[CandidatesKeys.FIELD_CANDIDATE_RESULT] == DBNull.Value
                ? -1
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_RESULT].ToString());
            if (_Result == 2)
                oWorkSheet.Cells[indexRow, 10] = "Đạt";
            else
                oWorkSheet.Cells[indexRow, 10] = "";
            oWorkSheet.Cells[indexRow, 11] = dr0[CandidatesKeys.Field_Candidate_RemarkR2] == DBNull.Value
                ? ""
                : dr0[CandidatesKeys.Field_Candidate_RemarkR2].ToString();

            #endregion

            for (var i = 1; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                var dr_1 = dt.Rows[i - 1];

                positionId = dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());
                positionIdBefore = dr_1[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr_1[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());

                indexRow++;

                var PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[PositionKeys.FIELD_POSITION_NAME].ToString();

                if (positionId != positionIdBefore)
                {
                    rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
                    rangePositonName.Merge(Type.Missing);
                    oWorkSheet.Cells[indexRow, 1] = PositionName.ToUpper();
                    rangePositonName.Font.Bold = true;
                    indexRow++;
                }

                /// inserting order number
                oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                oWorkSheet.Cells[indexRow, 2] = dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
                oWorkSheet.Cells[indexRow, 3] = dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
                var DayOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
                var MonthOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
                var YearOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
                var BirthDay = string.Empty;
                if ((DayOfBirth > 0) && (MonthOfBirth > 0) && (YearOfBirth > 0))
                    BirthDay = DayOfBirth + "/" + MonthOfBirth + "/" + YearOfBirth;
                else if ((MonthOfBirth > 0) && (YearOfBirth > 0))
                    BirthDay = MonthOfBirth + "/" + YearOfBirth;
                else if (YearOfBirth > 0)
                    BirthDay = YearOfBirth.ToString();
                else
                    BirthDay = string.Empty;
                oWorkSheet.Cells[indexRow, 4] = BirthDay;
                var Sex = dr[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                    ? false
                    : bool.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
                if (Sex)
                    oWorkSheet.Cells[indexRow, 5] = "Nam";
                else
                    oWorkSheet.Cells[indexRow, 5] = "Nữ";
                /////////////////////////////////////////////
                //// Trinh do van hoa
                var Id = dr[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
                var EducationLevel = string.Empty;
                var listCEL = CandidateEducationLevelsBLL.GetById(Id);
                for (var j = 0; j < listCEL.Count; j++)
                {
                    var objCEL = listCEL[j];

                    if (j == listCEL.Count - 1)
                        EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue;
                    else
                        EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
                }
                oWorkSheet.Cells[indexRow, 6] = EducationLevel;
                oWorkSheet.Cells[indexRow, 7] = dr[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE].ToString();
                oWorkSheet.Cells[indexRow, 8] = dr[CandidatesKeys.Field_Candidate_NNGK1] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NNGK1].ToString());
                oWorkSheet.Cells[indexRow, 9] = dr[CandidatesKeys.Field_Candidate_NNGK2] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NNGK2].ToString());
                var Result = dr[CandidatesKeys.FIELD_CANDIDATE_RESULT] == DBNull.Value
                    ? -1
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_RESULT].ToString());
                if (Result == 2)
                    oWorkSheet.Cells[indexRow, 10] = "Đạt";
                else
                    oWorkSheet.Cells[indexRow, 10] = "";
                oWorkSheet.Cells[indexRow, 11] = dr[CandidatesKeys.Field_Candidate_RemarkR2] == DBNull.Value
                    ? ""
                    : dr[CandidatesKeys.Field_Candidate_RemarkR2].ToString();
            }

            // release range object            
            var range = oWorkSheet.get_Range("A" + initTitleIndex, "K" + indexRow);
            range.Font.Size = 10;
            range.Font.Name = "Times New Roman";
            range.Borders.LineStyle = Constants.xlSolid;
        }

        private static void CreateHeaderAndTitleRepairingPractice(ref _Worksheet oWorkSheet, ref int initTitleIndex,
            DateTime date)
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

            var rangeHeader3 = oWorkSheet.get_Range("A3", "D3");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[3, 1] = "HỘI ĐỒNG TUYỂN DỤNG";
            rangeHeader3.Font.Size = 12;
            rangeHeader3.Font.Bold = true;

            var rangeHeader4 = oWorkSheet.get_Range("A4", "H4");
            rangeHeader4.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "KẾT QUẢ THI LÁI XE - VÒNG THỰC HÀNH";
            rangeHeader4.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader4.Font.Size = 15;
            rangeHeader4.Font.Bold = true;

            #endregion

            #region Create Title for Account List from oWorkSheet

            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            oWorkSheet.Cells[initTitleIndex, 2] = "Họ và tên lót";
            ((Range) oWorkSheet.Cells[initTitleIndex, 2]).ColumnWidth = 20;
            oWorkSheet.Cells[initTitleIndex, 3] = "Tên";
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 4] = "Năm sinh";
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 5] = "Giới tính";
            oWorkSheet.Cells[initTitleIndex, 6] = "Trình độ";
            ((Range) oWorkSheet.Cells[initTitleIndex, 6]).ColumnWidth = 35;
            oWorkSheet.Cells[initTitleIndex, 7] = "Kinh nghiệm";
            oWorkSheet.Cells[initTitleIndex, 8] = "Lý thuyết";
            oWorkSheet.Cells[initTitleIndex, 9] = "Thực Hành";
            oWorkSheet.Cells[initTitleIndex, 10] = "Kết Quả";
            oWorkSheet.Cells[initTitleIndex, 11] = "Ghi chú";
            ((Range) oWorkSheet.Cells[initTitleIndex, 11]).ColumnWidth = 40;

            var rangeHeader5 = oWorkSheet.get_Range("A7", "K7");
            rangeHeader5.Font.Size = 10;
            rangeHeader5.Font.Name = "Times New Roman";
            rangeHeader5.Font.Bold = true;

            #endregion
        }

        #endregion

        #region Export Repairing Board

        public static string ExportRepairingBoard(string firstName, int positionId, int result, int sessionId, int type,
            int sessionType, int typeSort, string pathName)
        {
            ///////////////////////////////////////////
            var dt = new CandidatesDAL().GetByFilter(firstName, positionId, result, sessionId, type, sessionType,
                typeSort);
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

                    oWorkSheet.Name = "KqSCTTBVongHoiDong";

                    InsertDataToWorkSheetRepairingBoard(dt, ref oWorkSheet, DateTime.Now);

                    oExcel.Visible = false;
                    oExcel.UserControl = false;
                    fileName = pathName + "\\RepairingBoard.xls";
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

        private static void InsertDataToWorkSheetRepairingBoard(DataTable dt, ref _Worksheet oWorkSheet, DateTime date)
        {
            var positionId = 0;
            var positionIdBefore = 0;
            var initTitleIndex = 8;
            var indexRow = initTitleIndex + 1;
            var orderNumber = 1;
            var value = string.Empty;
            CreateHeaderAndTitleRepairingBoard(ref oWorkSheet, ref initTitleIndex, date);
            var dr0 = dt.Rows[0];

            var rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName.Merge(Type.Missing);
            var _PositionName = dr0[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                ? string.Empty
                : dr0[PositionKeys.FIELD_POSITION_NAME].ToString();
            oWorkSheet.Cells[indexRow, 1] = _PositionName.ToUpper();
            rangePositonName.Font.Bold = true;

            #region Create FirstRow

            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            oWorkSheet.Cells[indexRow, 2] = dr0[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
            oWorkSheet.Cells[indexRow, 3] = dr0[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
            var _DayOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
            var _MonthOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
            var _YearOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
            var _BirthDay = string.Empty;
            if ((_DayOfBirth > 0) && (_MonthOfBirth > 0) && (_YearOfBirth > 0))
                _BirthDay = _DayOfBirth + "/" + _MonthOfBirth + "/" + _YearOfBirth;
            else if ((_MonthOfBirth > 0) && (_YearOfBirth > 0))
                _BirthDay = _MonthOfBirth + "/" + _YearOfBirth;
            else if (_YearOfBirth > 0)
                _BirthDay = _YearOfBirth.ToString();
            else
                _BirthDay = string.Empty;
            oWorkSheet.Cells[indexRow, 4] = _BirthDay;
            var _Sex = dr0[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                ? false
                : bool.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
            if (_Sex)
                oWorkSheet.Cells[indexRow, 5] = "Nam";
            else
                oWorkSheet.Cells[indexRow, 5] = "Nữ";
            /////////////////////////////////////////////
            //// Trinh do van hoa
            var _Id = dr0[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
            var _EducationLevel = string.Empty;
            var _listCEL = CandidateEducationLevelsBLL.GetById(_Id);
            for (var i = 0; i < _listCEL.Count; i++)
            {
                var objCEL = _listCEL[i];
                _EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
            }
            oWorkSheet.Cells[indexRow, 6] = _EducationLevel;
            /////////////////////////////////////////////
            oWorkSheet.Cells[indexRow, 7] = dr0[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE].ToString();

            oWorkSheet.Cells[indexRow, 8] = dr0[CandidatesKeys.Field_Candidate_NNGK1] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NNGK1].ToString());
            oWorkSheet.Cells[indexRow, 9] = dr0[CandidatesKeys.Field_Candidate_NNGK2] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NNGK2].ToString());
            oWorkSheet.Cells[indexRow, 10] = dr0[CandidatesKeys.Field_Candidate_NNGK3] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NNGK3].ToString());

            var _PCGK1 = dr0[CandidatesKeys.Field_Candidate_PCGK1] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_PCGK1].ToString());
            oWorkSheet.Cells[indexRow, 11] = _PCGK1 == -1 ? "" : _PCGK1.ToString();
            var _PCGK2 = dr0[CandidatesKeys.Field_Candidate_PCGK2] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_PCGK2].ToString());
            oWorkSheet.Cells[indexRow, 12] = _PCGK2 == -1 ? "" : _PCGK2.ToString();
            var _PCGK3 = dr0[CandidatesKeys.Field_Candidate_PCGK3] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_PCGK3].ToString());
            oWorkSheet.Cells[indexRow, 13] = _PCGK3 == -1 ? "" : _PCGK3.ToString();
            var _PCTB = dr0[CandidatesKeys.Field_Candidate_PCTB] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_PCTB].ToString());
            oWorkSheet.Cells[indexRow, 14] = _PCTB == -1 ? "" : _PCTB.ToString();

            var _KNGK1 = dr0[CandidatesKeys.Field_Candidate_KNGK1] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_KNGK1].ToString());
            oWorkSheet.Cells[indexRow, 15] = _KNGK1 == -1 ? "" : _KNGK1.ToString();
            var _KNGK2 = dr0[CandidatesKeys.Field_Candidate_KNGK2] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_KNGK2].ToString());
            oWorkSheet.Cells[indexRow, 16] = _KNGK2 == -1 ? "" : _KNGK2.ToString();
            var _KNGK3 = dr0[CandidatesKeys.Field_Candidate_KNGK3] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_KNGK3].ToString());
            oWorkSheet.Cells[indexRow, 17] = _KNGK3 == -1 ? "" : _KNGK3.ToString();
            var _KNTB = dr0[CandidatesKeys.Field_Candidate_KNTB] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_KNTB].ToString());
            oWorkSheet.Cells[indexRow, 18] = _KNTB == -1 ? "" : _KNTB.ToString();

            //double _DHNNGK1 = dr0[CandidatesKeys.Field_Candidate_DHNNGK1] == DBNull.Value ? -1 : double.Parse(dr0[CandidatesKeys.Field_Candidate_DHNNGK1].ToString());
            //oWorkSheet.Cells[indexRow, 18] = _DHNNGK1 == -1 ? "" : _DHNNGK1.ToString();
            //double _DHNNGK2 = dr0[CandidatesKeys.Field_Candidate_DHNNGK2] == DBNull.Value ? -1 : double.Parse(dr0[CandidatesKeys.Field_Candidate_DHNNGK2].ToString());
            //oWorkSheet.Cells[indexRow, 19] = _DHNNGK2 == -1 ? "" : _DHNNGK2.ToString();
            //double _DHNNGK3 = dr0[CandidatesKeys.Field_Candidate_DHNNGK3] == DBNull.Value ? -1 : double.Parse(dr0[CandidatesKeys.Field_Candidate_DHNNGK3].ToString());
            //oWorkSheet.Cells[indexRow, 20] = _DHNNGK3 == -1 ? "" : _DHNNGK3.ToString();
            //double _DHNNTB = dr0[CandidatesKeys.Field_Candidate_DHNNTB] == DBNull.Value ? -1 : double.Parse(dr0[CandidatesKeys.Field_Candidate_DHNNTB].ToString());
            //oWorkSheet.Cells[indexRow, 21] = _DHNNTB == -1 ? "" : _DHNNTB.ToString();

            var _DHNNGK1 = dr0[CandidatesKeys.Field_Candidate_DHNNGK1] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_DHNNGK1].ToString());
            oWorkSheet.Cells[indexRow, 19] = _DHNNGK1 == -1 ? "" : "Đạt";
            var _DHNNGK2 = dr0[CandidatesKeys.Field_Candidate_DHNNGK2] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_DHNNGK2].ToString());
            oWorkSheet.Cells[indexRow, 20] = _DHNNGK2 == -1 ? "" : "Đạt";
            var _DHNNGK3 = dr0[CandidatesKeys.Field_Candidate_DHNNGK3] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_DHNNGK3].ToString());
            oWorkSheet.Cells[indexRow, 21] = _DHNNGK3 == -1 ? "" : "Đạt";
            var _DHNNTB = dr0[CandidatesKeys.Field_Candidate_DHNNTB] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_DHNNTB].ToString());
            oWorkSheet.Cells[indexRow, 22] = _DHNNTB == -1 ? "" : "Đạt";

            var _Result = dr0[CandidatesKeys.FIELD_CANDIDATE_RESULT] == DBNull.Value
                ? -1
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_RESULT].ToString());
            if (_Result == 3)
                oWorkSheet.Cells[indexRow, 23] = "Đạt";
            else
                oWorkSheet.Cells[indexRow, 23] = "";
            oWorkSheet.Cells[indexRow, 24] = dr0[CandidatesKeys.Field_Candidate_RemarkR3] == DBNull.Value
                ? ""
                : dr0[CandidatesKeys.Field_Candidate_RemarkR3].ToString();

            #endregion

            for (var i = 1; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                var dr_1 = dt.Rows[i - 1];

                positionId = dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());
                positionIdBefore = dr_1[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr_1[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());

                indexRow++;

                var PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[PositionKeys.FIELD_POSITION_NAME].ToString();

                if (positionId != positionIdBefore)
                {
                    rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
                    rangePositonName.Merge(Type.Missing);
                    oWorkSheet.Cells[indexRow, 1] = PositionName.ToUpper();
                    rangePositonName.Font.Bold = true;
                    indexRow++;
                }

                /// inserting order number
                oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                oWorkSheet.Cells[indexRow, 2] = dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
                oWorkSheet.Cells[indexRow, 3] = dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
                var DayOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
                var MonthOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
                var YearOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
                var BirthDay = string.Empty;
                if ((DayOfBirth > 0) && (MonthOfBirth > 0) && (YearOfBirth > 0))
                    BirthDay = DayOfBirth + "/" + MonthOfBirth + "/" + YearOfBirth;
                else if ((MonthOfBirth > 0) && (YearOfBirth > 0))
                    BirthDay = MonthOfBirth + "/" + YearOfBirth;
                else if (YearOfBirth > 0)
                    BirthDay = YearOfBirth.ToString();
                else
                    BirthDay = string.Empty;
                oWorkSheet.Cells[indexRow, 4] = BirthDay;
                var Sex = dr[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                    ? false
                    : bool.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
                if (Sex)
                    oWorkSheet.Cells[indexRow, 5] = "Nam";
                else
                    oWorkSheet.Cells[indexRow, 5] = "Nữ";
                /////////////////////////////////////////////
                //// Trinh do van hoa
                var Id = dr[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
                var EducationLevel = string.Empty;
                var listCEL = CandidateEducationLevelsBLL.GetById(Id);
                for (var j = 0; j < listCEL.Count; j++)
                {
                    var objCEL = listCEL[j];

                    if (j == listCEL.Count - 1)
                        EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue;
                    else
                        EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
                }
                oWorkSheet.Cells[indexRow, 6] = EducationLevel;
                oWorkSheet.Cells[indexRow, 7] = dr[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE].ToString();
                oWorkSheet.Cells[indexRow, 8] = dr[CandidatesKeys.Field_Candidate_NNGK1] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NNGK1].ToString());
                oWorkSheet.Cells[indexRow, 9] = dr[CandidatesKeys.Field_Candidate_NNGK2] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NNGK2].ToString());
                oWorkSheet.Cells[indexRow, 10] = dr[CandidatesKeys.Field_Candidate_NNGK3] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NNGK3].ToString());

                var PCGK1 = dr[CandidatesKeys.Field_Candidate_PCGK1] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_PCGK1].ToString());
                oWorkSheet.Cells[indexRow, 11] = PCGK1 == -1 ? "" : PCGK1.ToString();
                var PCGK2 = dr[CandidatesKeys.Field_Candidate_PCGK2] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_PCGK2].ToString());
                oWorkSheet.Cells[indexRow, 12] = PCGK2 == -1 ? "" : PCGK2.ToString();
                var PCGK3 = dr[CandidatesKeys.Field_Candidate_PCGK3] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_PCGK3].ToString());
                oWorkSheet.Cells[indexRow, 13] = PCGK3 == -1 ? "" : PCGK3.ToString();
                var PCTB = dr[CandidatesKeys.Field_Candidate_PCTB] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_PCTB].ToString());
                oWorkSheet.Cells[indexRow, 14] = PCTB == -1 ? "" : PCTB.ToString();

                var KNGK1 = dr[CandidatesKeys.Field_Candidate_KNGK1] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_KNGK1].ToString());
                oWorkSheet.Cells[indexRow, 15] = KNGK1 == -1 ? "" : KNGK1.ToString();
                var KNGK2 = dr[CandidatesKeys.Field_Candidate_KNGK2] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_KNGK2].ToString());
                oWorkSheet.Cells[indexRow, 16] = KNGK2 == -1 ? "" : KNGK2.ToString();
                var KNGK3 = dr[CandidatesKeys.Field_Candidate_KNGK3] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_KNGK3].ToString());
                oWorkSheet.Cells[indexRow, 17] = KNGK3 == -1 ? "" : KNGK3.ToString();
                var KNTB = dr[CandidatesKeys.Field_Candidate_KNTB] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_KNTB].ToString());
                oWorkSheet.Cells[indexRow, 18] = KNTB == -1 ? "" : KNTB.ToString();

                //double DHNNGK1 = dr[CandidatesKeys.Field_Candidate_DHNNGK1] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_DHNNGK1].ToString());
                //oWorkSheet.Cells[indexRow, 18] = DHNNGK1 == -1 ? "" : DHNNGK1.ToString();
                //double DHNNGK2 = dr[CandidatesKeys.Field_Candidate_DHNNGK2] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_DHNNGK2].ToString());
                //oWorkSheet.Cells[indexRow, 19] = DHNNGK2 == -1 ? "" : DHNNGK2.ToString();
                //double DHNNGK3 = dr[CandidatesKeys.Field_Candidate_DHNNGK3] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_DHNNGK3].ToString());
                //oWorkSheet.Cells[indexRow, 20] = DHNNGK3 == -1 ? "" : DHNNGK3.ToString();
                //double DHNNTB = dr[CandidatesKeys.Field_Candidate_DHNNTB] == DBNull.Value ? -1 : double.Parse(dr[CandidatesKeys.Field_Candidate_DHNNTB].ToString());
                //oWorkSheet.Cells[indexRow, 21] = DHNNTB == -1 ? "" : DHNNTB.ToString();

                var DHNNGK1 = dr[CandidatesKeys.Field_Candidate_DHNNGK1] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_DHNNGK1].ToString());
                oWorkSheet.Cells[indexRow, 19] = DHNNGK1 == -1 ? "" : "Đạt";
                var DHNNGK2 = dr[CandidatesKeys.Field_Candidate_DHNNGK2] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_DHNNGK2].ToString());
                oWorkSheet.Cells[indexRow, 20] = DHNNGK2 == -1 ? "" : "Đạt";
                var DHNNGK3 = dr[CandidatesKeys.Field_Candidate_DHNNGK3] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_DHNNGK3].ToString());
                oWorkSheet.Cells[indexRow, 21] = DHNNGK3 == -1 ? "" : "Đạt";
                var DHNNTB = dr[CandidatesKeys.Field_Candidate_DHNNTB] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_DHNNTB].ToString());
                oWorkSheet.Cells[indexRow, 22] = DHNNTB == -1 ? "" : "Đạt";


                var Result = dr[CandidatesKeys.FIELD_CANDIDATE_RESULT] == DBNull.Value
                    ? -1
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_RESULT].ToString());
                if (Result == 3)
                    oWorkSheet.Cells[indexRow, 23] = "Đạt";
                else
                    oWorkSheet.Cells[indexRow, 23] = "";
                oWorkSheet.Cells[indexRow, 24] = dr[CandidatesKeys.Field_Candidate_RemarkR3] == DBNull.Value
                    ? ""
                    : dr[CandidatesKeys.Field_Candidate_RemarkR3].ToString();
            }

            // release range object            
            var range = oWorkSheet.get_Range("A" + (initTitleIndex - 1), "W" + indexRow);
            range.Font.Size = 10;
            range.Font.Name = "Times New Roman";
            range.Borders.LineStyle = Constants.xlSolid;
        }

        private static void CreateHeaderAndTitleRepairingBoard(ref _Worksheet oWorkSheet, ref int initTitleIndex,
            DateTime date)
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

            var rangeHeader3 = oWorkSheet.get_Range("A3", "D3");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[3, 1] = "HỘI ĐỒNG TUYỂN DỤNG";
            rangeHeader3.Font.Size = 12;
            rangeHeader3.Font.Bold = true;

            var rangeHeader4 = oWorkSheet.get_Range("A4", "H4");
            rangeHeader4.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "KẾT QUẢ THI LÁI XE - VÒNG PHỎNG VẤN HỘI ĐỒNG";
            rangeHeader4.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader4.Font.Size = 15;
            rangeHeader4.Font.Bold = true;

            #endregion

            #region Create Title for Account List from oWorkSheet

            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            oWorkSheet.Cells[initTitleIndex, 2] = "Họ và tên lót";
            ((Range) oWorkSheet.Cells[initTitleIndex, 2]).ColumnWidth = 20;
            oWorkSheet.Cells[initTitleIndex, 3] = "Tên";
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 4] = "Năm sinh";
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 5] = "Giới tính";
            oWorkSheet.Cells[initTitleIndex, 6] = "Trình độ";
            ((Range) oWorkSheet.Cells[initTitleIndex, 6]).ColumnWidth = 35;
            oWorkSheet.Cells[initTitleIndex, 7] = "Kinh nghiệm";
            oWorkSheet.Cells[initTitleIndex, 8] = "Luật GTĐB";
            oWorkSheet.Cells[initTitleIndex, 9] = "LT Ôtô cơ bản";
            oWorkSheet.Cells[initTitleIndex, 10] = "Thực Hành";

            var rangeHeaderPC = oWorkSheet.get_Range("J" + (initTitleIndex - 1), "M" + (initTitleIndex - 1));
            oWorkSheet.Cells[initTitleIndex - 1, 10] = "PHONG CÁCH";
            rangeHeaderPC.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 11] = "GK1";
            oWorkSheet.Cells[initTitleIndex, 12] = "GK2";
            oWorkSheet.Cells[initTitleIndex, 13] = "GK3";
            oWorkSheet.Cells[initTitleIndex, 14] = "TB";

            var rangeHeaderKN = oWorkSheet.get_Range("N" + (initTitleIndex - 1), "Q" + (initTitleIndex - 1));
            oWorkSheet.Cells[initTitleIndex - 1, 14] = "KINH NGHIỆM";
            rangeHeaderKN.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 15] = "GK1";
            oWorkSheet.Cells[initTitleIndex, 16] = "GK2";
            oWorkSheet.Cells[initTitleIndex, 17] = "GK3";
            oWorkSheet.Cells[initTitleIndex, 18] = "TB";

            var rangeHeaderDHNN = oWorkSheet.get_Range("R" + (initTitleIndex - 1), "U" + (initTitleIndex - 1));
            oWorkSheet.Cells[initTitleIndex - 1, 18] = "ĐH NGHỀ NGHIỆP";
            rangeHeaderDHNN.Merge(Type.Missing);
            oWorkSheet.Cells[initTitleIndex, 19] = "GK1";
            oWorkSheet.Cells[initTitleIndex, 20] = "GK2";
            oWorkSheet.Cells[initTitleIndex, 21] = "GK3";
            oWorkSheet.Cells[initTitleIndex, 22] = "TB";

            oWorkSheet.Cells[initTitleIndex, 23] = "Kết Quả";
            oWorkSheet.Cells[initTitleIndex, 24] = "Ghi chú";
            ((Range) oWorkSheet.Cells[initTitleIndex, 23]).ColumnWidth = 40;

            var rangeHeader5 = oWorkSheet.get_Range("A1", "W8");
            rangeHeader5.Font.Size = 10;
            rangeHeader5.Font.Name = "Times New Roman";
            rangeHeader5.Font.Bold = true;

            #endregion
        }

        #endregion

        #region Export Speciality Theory

        public static string ExportSpecialityTheory(string firstName, int positionId, int result, int sessionId,
            int type, int sessionType, int typeSort, string pathName)
        {
            ///////////////////////////////////////////
            var dt = new CandidatesDAL().GetByFilter(firstName, positionId, result, sessionId, type, sessionType,
                typeSort);
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

                    oWorkSheet.Name = "KQChuyenMonAnhVan";

                    InsertDataToWorkSheetSpecialityTheory(dt, ref oWorkSheet, DateTime.Now);

                    oExcel.Visible = false;
                    oExcel.UserControl = false;
                    fileName = pathName + "\\SpecialityTheory.xls";
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

        private static void InsertDataToWorkSheetSpecialityTheory(DataTable dt, ref _Worksheet oWorkSheet, DateTime date)
        {
            var positionId = 0;
            var positionIdBefore = 0;
            var initTitleIndex = 8;
            var indexRow = initTitleIndex + 1;
            var orderNumber = 1;
            var value = string.Empty;
            CreateHeaderAndTitleSpecialityTheory(ref oWorkSheet, ref initTitleIndex, date);
            var dr0 = dt.Rows[0];

            var rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName.Merge(Type.Missing);
            var _PositionName = dr0[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                ? string.Empty
                : dr0[PositionKeys.FIELD_POSITION_NAME].ToString();
            oWorkSheet.Cells[indexRow, 1] = _PositionName.ToUpper();
            rangePositonName.Font.Bold = true;

            #region Create FirstRow

            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            oWorkSheet.Cells[indexRow, 2] = dr0[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
            oWorkSheet.Cells[indexRow, 3] = dr0[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
            var _DayOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
            var _MonthOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
            var _YearOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
            var _BirthDay = string.Empty;
            if ((_DayOfBirth > 0) && (_MonthOfBirth > 0) && (_YearOfBirth > 0))
                _BirthDay = _DayOfBirth + "/" + _MonthOfBirth + "/" + _YearOfBirth;
            else if ((_MonthOfBirth > 0) && (_YearOfBirth > 0))
                _BirthDay = _MonthOfBirth + "/" + _YearOfBirth;
            else if (_YearOfBirth > 0)
                _BirthDay = _YearOfBirth.ToString();
            else
                _BirthDay = string.Empty;
            oWorkSheet.Cells[indexRow, 4] = _BirthDay;
            var _Sex = dr0[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                ? false
                : bool.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
            if (_Sex)
                oWorkSheet.Cells[indexRow, 5] = "Nam";
            else
                oWorkSheet.Cells[indexRow, 5] = "Nữ";
            /////////////////////////////////////////////
            //// Trinh do van hoa
            var _Id = dr0[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
            var _EducationLevel = string.Empty;
            var _listCEL = CandidateEducationLevelsBLL.GetById(_Id);
            for (var i = 0; i < _listCEL.Count; i++)
            {
                var objCEL = _listCEL[i];
                _EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
            }
            oWorkSheet.Cells[indexRow, 6] = _EducationLevel;
            /////////////////////////////////////////////
            oWorkSheet.Cells[indexRow, 7] = dr0[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE].ToString();

            var NNGK1o = dr0[CandidatesKeys.Field_Candidate_NNGK1] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NNGK1].ToString());
            if (NNGK1o == 1)
                oWorkSheet.Cells[indexRow, 8] = "Đạt";
            else
                oWorkSheet.Cells[indexRow, 8] = "";
            var NNGK2o = dr0[CandidatesKeys.Field_Candidate_NNGK2] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NNGK2].ToString());

            if (NNGK2o == 1)
                oWorkSheet.Cells[indexRow, 9] = "Đạt";
            else
                oWorkSheet.Cells[indexRow, 9] = "";

            var _Result = dr0[CandidatesKeys.FIELD_CANDIDATE_RESULT] == DBNull.Value
                ? -1
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_RESULT].ToString());
            if (_Result == 1)
                oWorkSheet.Cells[indexRow, 10] = "Đạt";
            else
                oWorkSheet.Cells[indexRow, 10] = "";

            oWorkSheet.Cells[indexRow, 11] = dr0[CandidatesKeys.Field_Candidate_RemarkR1] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.Field_Candidate_RemarkR1].ToString();

            #endregion

            for (var i = 1; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                var dr_1 = dt.Rows[i - 1];

                positionId = dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());
                positionIdBefore = dr_1[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr_1[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());

                indexRow++;

                var PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[PositionKeys.FIELD_POSITION_NAME].ToString();

                if (positionId != positionIdBefore)
                {
                    rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
                    rangePositonName.Merge(Type.Missing);
                    oWorkSheet.Cells[indexRow, 1] = PositionName.ToUpper();
                    rangePositonName.Font.Bold = true;
                    indexRow++;
                }

                /// inserting order number
                oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                oWorkSheet.Cells[indexRow, 2] = dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
                oWorkSheet.Cells[indexRow, 3] = dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
                var DayOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
                var MonthOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
                var YearOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
                var BirthDay = string.Empty;
                if ((DayOfBirth > 0) && (MonthOfBirth > 0) && (YearOfBirth > 0))
                    BirthDay = DayOfBirth + "/" + MonthOfBirth + "/" + YearOfBirth;
                else if ((MonthOfBirth > 0) && (YearOfBirth > 0))
                    BirthDay = MonthOfBirth + "/" + YearOfBirth;
                else if (YearOfBirth > 0)
                    BirthDay = YearOfBirth.ToString();
                else
                    BirthDay = string.Empty;
                oWorkSheet.Cells[indexRow, 4] = BirthDay;
                var Sex = dr[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                    ? false
                    : bool.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
                if (Sex)
                    oWorkSheet.Cells[indexRow, 5] = "Nam";
                else
                    oWorkSheet.Cells[indexRow, 5] = "Nữ";
                /////////////////////////////////////////////
                //// Trinh do van hoa
                var Id = dr[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
                var EducationLevel = string.Empty;
                var listCEL = CandidateEducationLevelsBLL.GetById(Id);
                for (var j = 0; j < listCEL.Count; j++)
                {
                    var objCEL = listCEL[j];

                    if (j == listCEL.Count - 1)
                        EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue;
                    else
                        EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
                }
                oWorkSheet.Cells[indexRow, 6] = EducationLevel;
                oWorkSheet.Cells[indexRow, 7] = dr[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE].ToString();
                var NNGK1 = dr[CandidatesKeys.Field_Candidate_NNGK1] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NNGK1].ToString());
                if (NNGK1 >= 1)
                    oWorkSheet.Cells[indexRow, 8] = "Đạt";
                else
                    oWorkSheet.Cells[indexRow, 8] = "";
                var NNGK2 = dr[CandidatesKeys.Field_Candidate_NNGK2] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NNGK2].ToString());

                if (NNGK2 >= 1)
                    oWorkSheet.Cells[indexRow, 9] = "Đạt";
                else
                    oWorkSheet.Cells[indexRow, 9] = "";

                var Result = dr[CandidatesKeys.FIELD_CANDIDATE_RESULT] == DBNull.Value
                    ? -1
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_RESULT].ToString());
                if (Result >= 1)
                    oWorkSheet.Cells[indexRow, 10] = "Đạt";
                else
                    oWorkSheet.Cells[indexRow, 10] = "";
                oWorkSheet.Cells[indexRow, 11] = dr[CandidatesKeys.Field_Candidate_RemarkR1] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.Field_Candidate_RemarkR1].ToString();
            }

            // release range object            
            var range = oWorkSheet.get_Range("A" + initTitleIndex, "K" + indexRow);
            range.Font.Size = 10;
            range.Font.Name = "Times New Roman";
            range.Borders.LineStyle = Constants.xlSolid;
        }

        private static void CreateHeaderAndTitleSpecialityTheory(ref _Worksheet oWorkSheet, ref int initTitleIndex,
            DateTime date)
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

            var rangeHeader3 = oWorkSheet.get_Range("A3", "D3");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[3, 1] = "HỘI ĐỒNG TUYỂN DỤNG";
            rangeHeader3.Font.Size = 12;
            rangeHeader3.Font.Bold = true;

            var rangeHeader4 = oWorkSheet.get_Range("A4", "H4");
            rangeHeader4.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "KẾT QUẢ VÒNG THI CHUYÊN MÔN ANH VĂN";
            rangeHeader4.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader4.Font.Size = 15;
            rangeHeader4.Font.Bold = true;

            #endregion

            #region Create Title for Account List from oWorkSheet

            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            oWorkSheet.Cells[initTitleIndex, 2] = "Họ và tên lót";
            ((Range) oWorkSheet.Cells[initTitleIndex, 2]).ColumnWidth = 20;
            oWorkSheet.Cells[initTitleIndex, 3] = "Tên";
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 4] = "Năm sinh";
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 5] = "Giới tính";
            oWorkSheet.Cells[initTitleIndex, 6] = "Trình độ";
            ((Range) oWorkSheet.Cells[initTitleIndex, 6]).ColumnWidth = 35;
            oWorkSheet.Cells[initTitleIndex, 7] = "Kinh nghiệm";
            ((Range) oWorkSheet.Cells[initTitleIndex, 7]).ColumnWidth = 30;
            oWorkSheet.Cells[initTitleIndex, 8] = "Chuyên môn";
            oWorkSheet.Cells[initTitleIndex, 9] = "Anh văn";
            oWorkSheet.Cells[initTitleIndex, 10] = "Kết Quả";
            oWorkSheet.Cells[initTitleIndex, 11] = "Ghi chú";
            ((Range) oWorkSheet.Cells[initTitleIndex, 10]).ColumnWidth = 40;

            var rangeHeader5 = oWorkSheet.get_Range("A7", "K7");
            rangeHeader5.Font.Size = 10;
            rangeHeader5.Font.Name = "Times New Roman";
            rangeHeader5.Font.Bold = true;

            #endregion
        }

        #endregion

        #region Export Speciality Board

        public static string ExportSpecialityBoard(string firstName, int positionId, int result, int sessionId, int type,
            int sessionType, int typeSort, string pathName)
        {
            ///////////////////////////////////////////
            var dt = new CandidatesDAL().GetByFilter(firstName, positionId, result, sessionId, type, sessionType,
                typeSort);
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

                    oWorkSheet.Name = "KQCMAVHoiDong";

                    InsertDataToWorkSheetSpecialityBoard(dt, ref oWorkSheet, DateTime.Now);

                    oExcel.Visible = false;
                    oExcel.UserControl = false;
                    fileName = pathName + "\\SpecialityBoard.xls";
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

        private static void InsertDataToWorkSheetSpecialityBoard(DataTable dt, ref _Worksheet oWorkSheet, DateTime date)
        {
            var positionId = 0;
            var positionIdBefore = 0;
            var initTitleIndex = 8;
            var indexRow = initTitleIndex + 1;
            var orderNumber = 1;
            var value = string.Empty;
            CreateHeaderAndTitleSpecialityBoard(ref oWorkSheet, ref initTitleIndex, date);
            var dr0 = dt.Rows[0];

            var rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName.Merge(Type.Missing);
            var _PositionName = dr0[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                ? string.Empty
                : dr0[PositionKeys.FIELD_POSITION_NAME].ToString();
            oWorkSheet.Cells[indexRow, 1] = _PositionName.ToUpper();
            rangePositonName.Font.Bold = true;

            #region Create FirstRow

            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            oWorkSheet.Cells[indexRow, 2] = dr0[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
            oWorkSheet.Cells[indexRow, 3] = dr0[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
            var _DayOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
            var _MonthOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
            var _YearOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
            var _BirthDay = string.Empty;
            if ((_DayOfBirth > 0) && (_MonthOfBirth > 0) && (_YearOfBirth > 0))
                _BirthDay = _DayOfBirth + "/" + _MonthOfBirth + "/" + _YearOfBirth;
            else if ((_MonthOfBirth > 0) && (_YearOfBirth > 0))
                _BirthDay = _MonthOfBirth + "/" + _YearOfBirth;
            else if (_YearOfBirth > 0)
                _BirthDay = _YearOfBirth.ToString();
            else
                _BirthDay = string.Empty;
            oWorkSheet.Cells[indexRow, 4] = _BirthDay;
            var _Sex = dr0[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                ? false
                : bool.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
            if (_Sex)
                oWorkSheet.Cells[indexRow, 5] = "Nam";
            else
                oWorkSheet.Cells[indexRow, 5] = "Nữ";
            /////////////////////////////////////////////
            //// Trinh do van hoa
            var _Id = dr0[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
            var _EducationLevel = string.Empty;
            var _listCEL = CandidateEducationLevelsBLL.GetById(_Id);
            for (var i = 0; i < _listCEL.Count; i++)
            {
                var objCEL = _listCEL[i];
                _EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
            }
            oWorkSheet.Cells[indexRow, 6] = _EducationLevel;
            /////////////////////////////////////////////
            oWorkSheet.Cells[indexRow, 7] = dr0[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE].ToString();

            oWorkSheet.Cells[indexRow, 8] = dr0[CandidatesKeys.Field_Candidate_NNGK1] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NNGK1].ToString());

            var NNGK2o = dr0[CandidatesKeys.Field_Candidate_NNGK2] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NNGK2].ToString());

            if (NNGK2o == 1)
                oWorkSheet.Cells[indexRow, 9] = "Đạt";
            else
                oWorkSheet.Cells[indexRow, 9] = "";

            var _Result = dr0[CandidatesKeys.FIELD_CANDIDATE_RESULT] == DBNull.Value
                ? -1
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_RESULT].ToString());
            if (_Result == 2)
                oWorkSheet.Cells[indexRow, 10] = "Đạt";
            else
                oWorkSheet.Cells[indexRow, 10] = "";

            oWorkSheet.Cells[indexRow, 11] = dr0[CandidatesKeys.Field_Candidate_RemarkR2] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.Field_Candidate_RemarkR2].ToString();

            #endregion

            for (var i = 1; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                var dr_1 = dt.Rows[i - 1];

                positionId = dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());
                positionIdBefore = dr_1[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr_1[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());

                indexRow++;

                var PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[PositionKeys.FIELD_POSITION_NAME].ToString();

                if (positionId != positionIdBefore)
                {
                    rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
                    rangePositonName.Merge(Type.Missing);
                    oWorkSheet.Cells[indexRow, 1] = PositionName.ToUpper();
                    rangePositonName.Font.Bold = true;
                    indexRow++;
                }

                /// inserting order number
                oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                oWorkSheet.Cells[indexRow, 2] = dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
                oWorkSheet.Cells[indexRow, 3] = dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
                var DayOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
                var MonthOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
                var YearOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
                var BirthDay = string.Empty;
                if ((DayOfBirth > 0) && (MonthOfBirth > 0) && (YearOfBirth > 0))
                    BirthDay = DayOfBirth + "/" + MonthOfBirth + "/" + YearOfBirth;
                else if ((MonthOfBirth > 0) && (YearOfBirth > 0))
                    BirthDay = MonthOfBirth + "/" + YearOfBirth;
                else if (YearOfBirth > 0)
                    BirthDay = YearOfBirth.ToString();
                else
                    BirthDay = string.Empty;
                oWorkSheet.Cells[indexRow, 4] = BirthDay;
                var Sex = dr[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                    ? false
                    : bool.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
                if (Sex)
                    oWorkSheet.Cells[indexRow, 5] = "Nam";
                else
                    oWorkSheet.Cells[indexRow, 5] = "Nữ";
                /////////////////////////////////////////////
                //// Trinh do van hoa
                var Id = dr[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
                var EducationLevel = string.Empty;
                var listCEL = CandidateEducationLevelsBLL.GetById(Id);
                for (var j = 0; j < listCEL.Count; j++)
                {
                    var objCEL = listCEL[j];

                    if (j == listCEL.Count - 1)
                        EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue;
                    else
                        EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
                }
                oWorkSheet.Cells[indexRow, 6] = EducationLevel;
                oWorkSheet.Cells[indexRow, 7] = dr[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE].ToString();
                oWorkSheet.Cells[indexRow, 8] = dr[CandidatesKeys.Field_Candidate_NNGK1] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NNGK1].ToString());

                var NNGK2 = dr[CandidatesKeys.Field_Candidate_NNGK2] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NNGK2].ToString());

                if (NNGK2 >= 1)
                    oWorkSheet.Cells[indexRow, 9] = "Đạt";
                else
                    oWorkSheet.Cells[indexRow, 9] = "";

                var Result = dr[CandidatesKeys.FIELD_CANDIDATE_RESULT] == DBNull.Value
                    ? -1
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_RESULT].ToString());
                if (Result >= 2)
                    oWorkSheet.Cells[indexRow, 10] = "Đạt";
                else
                    oWorkSheet.Cells[indexRow, 10] = "";
                oWorkSheet.Cells[indexRow, 11] = dr[CandidatesKeys.Field_Candidate_RemarkR2] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.Field_Candidate_RemarkR2].ToString();
            }

            // release range object            
            var range = oWorkSheet.get_Range("A" + initTitleIndex, "K" + indexRow);
            range.Font.Size = 10;
            range.Font.Name = "Times New Roman";
            range.Borders.LineStyle = Constants.xlSolid;
        }

        private static void CreateHeaderAndTitleSpecialityBoard(ref _Worksheet oWorkSheet, ref int initTitleIndex,
            DateTime date)
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

            var rangeHeader3 = oWorkSheet.get_Range("A3", "D3");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[3, 1] = "HỘI ĐỒNG TUYỂN DỤNG";
            rangeHeader3.Font.Size = 12;
            rangeHeader3.Font.Bold = true;

            var rangeHeader4 = oWorkSheet.get_Range("A4", "H4");
            rangeHeader4.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "KẾT QUẢ VÒNG THI CHUYÊN MÔN ANH VĂN";
            rangeHeader4.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader4.Font.Size = 15;
            rangeHeader4.Font.Bold = true;

            #endregion

            #region Create Title for Account List from oWorkSheet

            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            oWorkSheet.Cells[initTitleIndex, 2] = "Họ và tên lót";
            ((Range) oWorkSheet.Cells[initTitleIndex, 2]).ColumnWidth = 20;
            oWorkSheet.Cells[initTitleIndex, 3] = "Tên";
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 4] = "Năm sinh";
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 5] = "Giới tính";
            oWorkSheet.Cells[initTitleIndex, 6] = "Trình độ";
            ((Range) oWorkSheet.Cells[initTitleIndex, 6]).ColumnWidth = 35;
            oWorkSheet.Cells[initTitleIndex, 7] = "Kinh nghiệm";
            ((Range) oWorkSheet.Cells[initTitleIndex, 7]).ColumnWidth = 30;
            oWorkSheet.Cells[initTitleIndex, 8] = "Chuyên môn";
            oWorkSheet.Cells[initTitleIndex, 9] = "Anh văn";
            oWorkSheet.Cells[initTitleIndex, 10] = "Kết Quả";
            oWorkSheet.Cells[initTitleIndex, 11] = "Ghi chú";
            ((Range) oWorkSheet.Cells[initTitleIndex, 10]).ColumnWidth = 40;

            var rangeHeader5 = oWorkSheet.get_Range("A7", "K7");
            rangeHeader5.Font.Size = 10;
            rangeHeader5.Font.Name = "Times New Roman";
            rangeHeader5.Font.Bold = true;

            #endregion
        }

        #endregion

        #region Export Speciality Practice

        public static string ExportSpecialityPractice(string firstName, int positionId, int result, int sessionId,
            int type, int sessionType, int typeSort, string pathName)
        {
            ///////////////////////////////////////////
            var dt = new CandidatesDAL().GetByFilter(firstName, positionId, result, sessionId, type, sessionType,
                typeSort);
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

                    oWorkSheet.Name = "KQCMAVThucHanh";

                    InsertDataToWorkSheetSpecialityPractice(dt, ref oWorkSheet, DateTime.Now);

                    oExcel.Visible = false;
                    oExcel.UserControl = false;
                    fileName = pathName + "\\SpecialityPractice.xls";
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

        private static void InsertDataToWorkSheetSpecialityPractice(DataTable dt, ref _Worksheet oWorkSheet,
            DateTime date)
        {
            var positionId = 0;
            var positionIdBefore = 0;
            var initTitleIndex = 8;
            var indexRow = initTitleIndex + 1;
            var orderNumber = 1;
            var value = string.Empty;
            CreateHeaderAndTitleSpecialityPractice(ref oWorkSheet, ref initTitleIndex, date);
            var dr0 = dt.Rows[0];

            var rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName.Merge(Type.Missing);
            var _PositionName = dr0[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                ? string.Empty
                : dr0[PositionKeys.FIELD_POSITION_NAME].ToString();
            oWorkSheet.Cells[indexRow, 1] = _PositionName.ToUpper();
            rangePositonName.Font.Bold = true;

            #region Create FirstRow

            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            oWorkSheet.Cells[indexRow, 2] = dr0[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
            oWorkSheet.Cells[indexRow, 3] = dr0[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
            var _DayOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
            var _MonthOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
            var _YearOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
            var _BirthDay = string.Empty;
            if ((_DayOfBirth > 0) && (_MonthOfBirth > 0) && (_YearOfBirth > 0))
                _BirthDay = _DayOfBirth + "/" + _MonthOfBirth + "/" + _YearOfBirth;
            else if ((_MonthOfBirth > 0) && (_YearOfBirth > 0))
                _BirthDay = _MonthOfBirth + "/" + _YearOfBirth;
            else if (_YearOfBirth > 0)
                _BirthDay = _YearOfBirth.ToString();
            else
                _BirthDay = string.Empty;
            oWorkSheet.Cells[indexRow, 4] = _BirthDay;
            var _Sex = dr0[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                ? false
                : bool.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
            if (_Sex)
                oWorkSheet.Cells[indexRow, 5] = "Nam";
            else
                oWorkSheet.Cells[indexRow, 5] = "Nữ";
            /////////////////////////////////////////////
            //// Trinh do van hoa
            var _Id = dr0[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
            var _EducationLevel = string.Empty;
            var _listCEL = CandidateEducationLevelsBLL.GetById(_Id);
            for (var i = 0; i < _listCEL.Count; i++)
            {
                var objCEL = _listCEL[i];
                _EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
            }
            oWorkSheet.Cells[indexRow, 6] = _EducationLevel;
            /////////////////////////////////////////////
            oWorkSheet.Cells[indexRow, 7] = dr0[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE].ToString();

            oWorkSheet.Cells[indexRow, 8] = dr0[CandidatesKeys.Field_Candidate_NNGK1] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NNGK1].ToString());

            var NNGK2o = dr0[CandidatesKeys.Field_Candidate_NNGK2] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NNGK2].ToString());

            if (NNGK2o == 1)
                oWorkSheet.Cells[indexRow, 9] = "Đạt";
            else
                oWorkSheet.Cells[indexRow, 9] = "";
            oWorkSheet.Cells[indexRow, 10] = dr0[CandidatesKeys.Field_Candidate_NNGK3] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NNGK3].ToString());
            var _Result = dr0[CandidatesKeys.FIELD_CANDIDATE_RESULT] == DBNull.Value
                ? -1
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_RESULT].ToString());
            if (_Result == 2)
                oWorkSheet.Cells[indexRow, 11] = "Đạt";
            else
                oWorkSheet.Cells[indexRow, 11] = "";

            oWorkSheet.Cells[indexRow, 12] = dr0[CandidatesKeys.Field_Candidate_RemarkR2] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.Field_Candidate_RemarkR2].ToString();

            #endregion

            for (var i = 1; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                var dr_1 = dt.Rows[i - 1];

                positionId = dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());
                positionIdBefore = dr_1[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr_1[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());

                indexRow++;

                var PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[PositionKeys.FIELD_POSITION_NAME].ToString();

                if (positionId != positionIdBefore)
                {
                    rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
                    rangePositonName.Merge(Type.Missing);
                    oWorkSheet.Cells[indexRow, 1] = PositionName.ToUpper();
                    rangePositonName.Font.Bold = true;
                    indexRow++;
                }

                /// inserting order number
                oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                oWorkSheet.Cells[indexRow, 2] = dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
                oWorkSheet.Cells[indexRow, 3] = dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
                var DayOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
                var MonthOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
                var YearOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
                var BirthDay = string.Empty;
                if ((DayOfBirth > 0) && (MonthOfBirth > 0) && (YearOfBirth > 0))
                    BirthDay = DayOfBirth + "/" + MonthOfBirth + "/" + YearOfBirth;
                else if ((MonthOfBirth > 0) && (YearOfBirth > 0))
                    BirthDay = MonthOfBirth + "/" + YearOfBirth;
                else if (YearOfBirth > 0)
                    BirthDay = YearOfBirth.ToString();
                else
                    BirthDay = string.Empty;
                oWorkSheet.Cells[indexRow, 4] = BirthDay;
                var Sex = dr[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                    ? false
                    : bool.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
                if (Sex)
                    oWorkSheet.Cells[indexRow, 5] = "Nam";
                else
                    oWorkSheet.Cells[indexRow, 5] = "Nữ";
                /////////////////////////////////////////////
                //// Trinh do van hoa
                var Id = dr[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
                var EducationLevel = string.Empty;
                var listCEL = CandidateEducationLevelsBLL.GetById(Id);
                for (var j = 0; j < listCEL.Count; j++)
                {
                    var objCEL = listCEL[j];

                    if (j == listCEL.Count - 1)
                        EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue;
                    else
                        EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
                }
                oWorkSheet.Cells[indexRow, 6] = EducationLevel;
                oWorkSheet.Cells[indexRow, 7] = dr[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE].ToString();
                oWorkSheet.Cells[indexRow, 8] = dr[CandidatesKeys.Field_Candidate_NNGK1] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NNGK1].ToString());

                var NNGK2 = dr[CandidatesKeys.Field_Candidate_NNGK2] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NNGK2].ToString());

                if (NNGK2 >= 1)
                    oWorkSheet.Cells[indexRow, 9] = "Đạt";
                else
                    oWorkSheet.Cells[indexRow, 9] = "";
                oWorkSheet.Cells[indexRow, 10] = dr[CandidatesKeys.Field_Candidate_NNGK3] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NNGK3].ToString());

                var Result = dr[CandidatesKeys.FIELD_CANDIDATE_RESULT] == DBNull.Value
                    ? -1
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_RESULT].ToString());
                if (Result >= 2)
                    oWorkSheet.Cells[indexRow, 11] = "Đạt";
                else
                    oWorkSheet.Cells[indexRow, 11] = "";
                oWorkSheet.Cells[indexRow, 12] = dr[CandidatesKeys.Field_Candidate_RemarkR3] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.Field_Candidate_RemarkR3].ToString();
            }

            // release range object            
            var range = oWorkSheet.get_Range("A" + initTitleIndex, "L" + indexRow);
            range.Font.Size = 10;
            range.Font.Name = "Times New Roman";
            range.Borders.LineStyle = Constants.xlSolid;
        }

        private static void CreateHeaderAndTitleSpecialityPractice(ref _Worksheet oWorkSheet, ref int initTitleIndex,
            DateTime date)
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

            var rangeHeader3 = oWorkSheet.get_Range("A3", "D3");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[3, 1] = "HỘI ĐỒNG TUYỂN DỤNG";
            rangeHeader3.Font.Size = 12;
            rangeHeader3.Font.Bold = true;

            var rangeHeader4 = oWorkSheet.get_Range("A4", "H4");
            rangeHeader4.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "KẾT QUẢ VÒNG THI CHUYÊN MÔN ANH VĂN THỰC HÀNH";
            rangeHeader4.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader4.Font.Size = 15;
            rangeHeader4.Font.Bold = true;

            #endregion

            #region Create Title for Account List from oWorkSheet

            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            oWorkSheet.Cells[initTitleIndex, 2] = "Họ và tên lót";
            ((Range) oWorkSheet.Cells[initTitleIndex, 2]).ColumnWidth = 20;
            oWorkSheet.Cells[initTitleIndex, 3] = "Tên";
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 4] = "Năm sinh";
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 5] = "Giới tính";
            oWorkSheet.Cells[initTitleIndex, 6] = "Trình độ";
            ((Range) oWorkSheet.Cells[initTitleIndex, 6]).ColumnWidth = 35;
            oWorkSheet.Cells[initTitleIndex, 7] = "Kinh nghiệm";
            ((Range) oWorkSheet.Cells[initTitleIndex, 7]).ColumnWidth = 30;
            oWorkSheet.Cells[initTitleIndex, 8] = "Chuyên môn";
            oWorkSheet.Cells[initTitleIndex, 9] = "Anh văn";
            oWorkSheet.Cells[initTitleIndex, 10] = "Thực hành";
            oWorkSheet.Cells[initTitleIndex, 11] = "Kết Quả";
            oWorkSheet.Cells[initTitleIndex, 12] = "Ghi chú";
            ((Range) oWorkSheet.Cells[initTitleIndex, 12]).ColumnWidth = 40;

            var rangeHeader5 = oWorkSheet.get_Range("A7", "L7");
            rangeHeader5.Font.Size = 10;
            rangeHeader5.Font.Name = "Times New Roman";
            rangeHeader5.Font.Bold = true;

            #endregion
        }

        #endregion

        #region Export LoadAndUnloadCargo

        public static string ExportLoadAndUnloadCargo(string firstName, int positionId, int result, int sessionId,
            int type, int sessionType, int typeSort, string pathName)
        {
            ///////////////////////////////////////////
            var dt = new CandidatesDAL().GetByFilter(firstName, positionId, result, sessionId, type, sessionType,
                typeSort);
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

                    oWorkSheet.Name = "KetQuaThiCXHHHL";

                    InsertDataToWorkSheetLoadAndUnloadCargo(dt, ref oWorkSheet, DateTime.Now);

                    oExcel.Visible = false;
                    oExcel.UserControl = false;
                    fileName = pathName + "\\LoadUnLoadCargo.xls";
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

        private static void InsertDataToWorkSheetLoadAndUnloadCargo(DataTable dt, ref _Worksheet oWorkSheet,
            DateTime date)
        {
            var positionId = 0;
            var positionIdBefore = 0;
            var initTitleIndex = 8;
            var indexRow = initTitleIndex + 1;
            var orderNumber = 1;
            var value = string.Empty;
            CreateHeaderAndTitleLoadAndUnloadCargo(ref oWorkSheet, ref initTitleIndex, date);
            var dr0 = dt.Rows[0];

            var rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangePositonName.Merge(Type.Missing);
            var _PositionName = dr0[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                ? string.Empty
                : dr0[PositionKeys.FIELD_POSITION_NAME].ToString();
            oWorkSheet.Cells[indexRow, 1] = _PositionName.ToUpper();
            rangePositonName.Font.Bold = true;

            #region Create FirstRow

            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            oWorkSheet.Cells[indexRow, 2] = dr0[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
            oWorkSheet.Cells[indexRow, 3] = dr0[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
            var _DayOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
            var _MonthOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
            var _YearOfBirth = dr0[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
            var _BirthDay = string.Empty;
            if ((_DayOfBirth > 0) && (_MonthOfBirth > 0) && (_YearOfBirth > 0))
                _BirthDay = _DayOfBirth + "/" + _MonthOfBirth + "/" + _YearOfBirth;
            else if ((_MonthOfBirth > 0) && (_YearOfBirth > 0))
                _BirthDay = _MonthOfBirth + "/" + _YearOfBirth;
            else if (_YearOfBirth > 0)
                _BirthDay = _YearOfBirth.ToString();
            else
                _BirthDay = string.Empty;
            oWorkSheet.Cells[indexRow, 4] = _BirthDay;
            var _Sex = dr0[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                ? false
                : bool.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
            if (_Sex)
                oWorkSheet.Cells[indexRow, 5] = "Nam";
            else
                oWorkSheet.Cells[indexRow, 5] = "Nữ";
            /////////////////////////////////////////////
            //// Trinh do van hoa
            var _Id = dr0[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                ? 0
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
            var _EducationLevel = string.Empty;
            var _listCEL = CandidateEducationLevelsBLL.GetById(_Id);
            for (var i = 0; i < _listCEL.Count; i++)
            {
                var objCEL = _listCEL[i];
                _EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
            }
            oWorkSheet.Cells[indexRow, 6] = _EducationLevel;
            /////////////////////////////////////////////
            oWorkSheet.Cells[indexRow, 7] = dr0[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE] == DBNull.Value
                ? string.Empty
                : dr0[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE].ToString();

            var oNNGK1 = dr0[CandidatesKeys.Field_Candidate_NNGK1] == DBNull.Value
                ? -1
                : double.Parse(dr0[CandidatesKeys.Field_Candidate_NNGK1].ToString());
            if (oNNGK1 == 1)
                oWorkSheet.Cells[indexRow, 8] = "Đạt";
            else
                oWorkSheet.Cells[indexRow, 8] = "";

            oWorkSheet.Cells[indexRow, 9] = dr0[CandidatesKeys.Field_Candidate_RemarkR1] == DBNull.Value
                ? ""
                : dr0[CandidatesKeys.Field_Candidate_RemarkR1].ToString();
            oWorkSheet.Cells[indexRow, 10] = dr0[CandidatesKeys.Field_Candidate_RemarkR2] == DBNull.Value
                ? ""
                : dr0[CandidatesKeys.Field_Candidate_RemarkR2].ToString();

            var _Result = dr0[CandidatesKeys.FIELD_CANDIDATE_RESULT] == DBNull.Value
                ? -1
                : int.Parse(dr0[CandidatesKeys.FIELD_CANDIDATE_RESULT].ToString());
            if (_Result == 1)
                oWorkSheet.Cells[indexRow, 11] = "Đạt";
            else
                oWorkSheet.Cells[indexRow, 11] = "";
            oWorkSheet.Cells[indexRow, 12] = dr0[CandidatesKeys.Field_Candidate_RemarkR3] == DBNull.Value
                ? ""
                : dr0[CandidatesKeys.Field_Candidate_RemarkR3].ToString();

            #endregion

            for (var i = 1; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                var dr_1 = dt.Rows[i - 1];

                positionId = dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());
                positionIdBefore = dr_1[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr_1[CandidatesKeys.FIELD_CANDIDATE_POSITION_ID].ToString());

                indexRow++;

                var PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[PositionKeys.FIELD_POSITION_NAME].ToString();

                if (positionId != positionIdBefore)
                {
                    rangePositonName = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
                    rangePositonName.Merge(Type.Missing);
                    oWorkSheet.Cells[indexRow, 1] = PositionName.ToUpper();
                    rangePositonName.Font.Bold = true;
                    indexRow++;
                }

                /// inserting order number
                oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                oWorkSheet.Cells[indexRow, 2] = dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_LASTNAME].ToString();
                oWorkSheet.Cells[indexRow, 3] = dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_FIRSTNAME].ToString();
                var DayOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_DATE_OF_BIRTH].ToString());
                var MonthOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_MONTH_OF_BIRTH].ToString());
                var YearOfBirth = dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_YEAR_OF_BIRTH].ToString());
                var BirthDay = string.Empty;
                if ((DayOfBirth > 0) && (MonthOfBirth > 0) && (YearOfBirth > 0))
                    BirthDay = DayOfBirth + "/" + MonthOfBirth + "/" + YearOfBirth;
                else if ((MonthOfBirth > 0) && (YearOfBirth > 0))
                    BirthDay = MonthOfBirth + "/" + YearOfBirth;
                else if (YearOfBirth > 0)
                    BirthDay = YearOfBirth.ToString();
                else
                    BirthDay = string.Empty;
                oWorkSheet.Cells[indexRow, 4] = BirthDay;
                var Sex = dr[CandidatesKeys.FIELD_CANDIDATE_SEX] == DBNull.Value
                    ? false
                    : bool.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_SEX].ToString());
                if (Sex)
                    oWorkSheet.Cells[indexRow, 5] = "Nam";
                else
                    oWorkSheet.Cells[indexRow, 5] = "Nữ";
                /////////////////////////////////////////////
                //// Trinh do van hoa
                var Id = dr[CandidatesKeys.FIELD_CANDIDATE_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_ID].ToString());
                var EducationLevel = string.Empty;
                var listCEL = CandidateEducationLevelsBLL.GetById(Id);
                for (var j = 0; j < listCEL.Count; j++)
                {
                    var objCEL = listCEL[j];

                    if (j == listCEL.Count - 1)
                        EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue;
                    else
                        EducationLevel += "- " + objCEL.EducationLevelName + " : " + objCEL.EducationLevelValue + "\n";
                }
                oWorkSheet.Cells[indexRow, 6] = EducationLevel;
                oWorkSheet.Cells[indexRow, 7] = dr[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE] == DBNull.Value
                    ? string.Empty
                    : dr[CandidatesKeys.FIELD_CANDIDATE_EXPERIENCE].ToString();

                var NNGK1 = dr[CandidatesKeys.Field_Candidate_NNGK1] == DBNull.Value
                    ? -1
                    : double.Parse(dr[CandidatesKeys.Field_Candidate_NNGK1].ToString());
                if (NNGK1 == 1)
                    oWorkSheet.Cells[indexRow, 8] = "Đạt";
                else
                    oWorkSheet.Cells[indexRow, 8] = "";

                oWorkSheet.Cells[indexRow, 9] = dr[CandidatesKeys.Field_Candidate_RemarkR1] == DBNull.Value
                    ? ""
                    : dr[CandidatesKeys.Field_Candidate_RemarkR1].ToString();
                oWorkSheet.Cells[indexRow, 10] = dr[CandidatesKeys.Field_Candidate_RemarkR2] == DBNull.Value
                    ? ""
                    : dr[CandidatesKeys.Field_Candidate_RemarkR2].ToString();
                var Result = dr[CandidatesKeys.FIELD_CANDIDATE_RESULT] == DBNull.Value
                    ? -1
                    : int.Parse(dr[CandidatesKeys.FIELD_CANDIDATE_RESULT].ToString());
                if (Result == 1)
                    oWorkSheet.Cells[indexRow, 11] = "Đạt";
                else
                    oWorkSheet.Cells[indexRow, 11] = "";
                oWorkSheet.Cells[indexRow, 12] = dr[CandidatesKeys.Field_Candidate_RemarkR3] == DBNull.Value
                    ? ""
                    : dr[CandidatesKeys.Field_Candidate_RemarkR3].ToString();
            }

            // release range object            
            var range = oWorkSheet.get_Range("A" + initTitleIndex, "L" + indexRow);
            range.Font.Size = 10;
            range.Font.Name = "Times New Roman";
            range.Borders.LineStyle = Constants.xlSolid;
        }

        private static void CreateHeaderAndTitleLoadAndUnloadCargo(ref _Worksheet oWorkSheet, ref int initTitleIndex,
            DateTime date)
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

            var rangeHeader3 = oWorkSheet.get_Range("A3", "D3");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[3, 1] = "HỘI ĐỒNG TUYỂN DỤNG";
            rangeHeader3.Font.Size = 12;
            rangeHeader3.Font.Bold = true;

            var rangeHeader4 = oWorkSheet.get_Range("A4", "H4");
            rangeHeader4.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "KẾT QUẢ THI CHẤT XẾP HÀNG HÓA HÀNH LY";
            rangeHeader4.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader4.Font.Size = 15;
            rangeHeader4.Font.Bold = true;

            #endregion

            #region Create Title for Account List from oWorkSheet

            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            oWorkSheet.Cells[initTitleIndex, 2] = "Họ và tên lót";
            ((Range) oWorkSheet.Cells[initTitleIndex, 2]).ColumnWidth = 20;
            oWorkSheet.Cells[initTitleIndex, 3] = "Tên";
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 4] = "Năm sinh";
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 5] = "Giới tính";
            oWorkSheet.Cells[initTitleIndex, 6] = "Trình độ";
            ((Range) oWorkSheet.Cells[initTitleIndex, 6]).ColumnWidth = 30;
            oWorkSheet.Cells[initTitleIndex, 7] = "Kinh nghiệm";
            ((Range) oWorkSheet.Cells[initTitleIndex, 7]).ColumnWidth = 25;
            oWorkSheet.Cells[initTitleIndex, 8] = "Sức khỏe";
            ((Range) oWorkSheet.Cells[initTitleIndex, 8]).ColumnWidth = 25;
            oWorkSheet.Cells[initTitleIndex, 9] = "Nhận xét kinh nghiệm";
            ((Range) oWorkSheet.Cells[initTitleIndex, 9]).ColumnWidth = 25;
            oWorkSheet.Cells[initTitleIndex, 10] = "Định hướng nghề nghiệp";
            ((Range) oWorkSheet.Cells[initTitleIndex, 10]).ColumnWidth = 25;
            oWorkSheet.Cells[initTitleIndex, 11] = "Kết quả";
            oWorkSheet.Cells[initTitleIndex, 12] = "Ghi chú";
            ((Range) oWorkSheet.Cells[initTitleIndex, 12]).ColumnWidth = 40;

            var rangeHeader5 = oWorkSheet.get_Range("A7", "L7");
            rangeHeader5.Font.Size = 10;
            rangeHeader5.Font.Name = "Times New Roman";
            rangeHeader5.Font.Bold = true;

            #endregion
        }

        #endregion
    }
}