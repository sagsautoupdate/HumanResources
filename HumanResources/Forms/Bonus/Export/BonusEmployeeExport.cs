using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading;
using Excel;
using HRMUtil;
using HRMUtil.KeyNames.H1;
using Telerik.WinControls.UI;
using Constants = Excel.Constants;
using Range = Excel.Range;

namespace HumanResources.Forms.Bonus.Export
{
    public class BonusEmployeeExport
    {
        public static _Application ExportBonusForBank(string fileName, RadGridView rgwListIncome, DateTime dataDate,
            string fullName, BackgroundWorker backgroundWorkerExport)
        {
            if (rgwListIncome.RowCount > 0)
            {
                _Application oExcel;
                _Workbook oWorkBook;
                _Worksheet oWorkSheet;
                _Worksheet oWorkSheetCash;


                try
                {
                    GC.Collect();
                    oExcel = oExcel = new Application();
                    oExcel.Visible = false;


                    oWorkBook = oExcel.Workbooks.Add(Missing.Value);

                    oWorkSheet = (_Worksheet) oWorkBook.Worksheets[1];
                    oWorkSheet.Name = "QUA TÀI KHOẢN";

                    oWorkSheetCash = (_Worksheet) oWorkBook.Worksheets.Add();
                    oWorkSheetCash.Name = "QUA TIỀN MẶT";

                    InsertDataToWorkSheetForBank(ref oWorkSheet, ref oWorkSheetCash, rgwListIncome, dataDate, fullName,
                        backgroundWorkerExport);

                    oExcel.Visible = false;
                    oExcel.UserControl = false;

                    return oExcel;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return null;
        }

        private static void InsertDataToWorkSheetForBank(ref _Worksheet oWorkSheet, ref _Worksheet oWorkSheetCash,
            RadGridView rgwListIncome, DateTime date, string fullName, BackgroundWorker backgroundWorkerExport)
        {
            var deparmentId = 0;
            var departmentIdBefore = 0;
            decimal totalAllRealIncome = 0;
            decimal totalAllRealIncomeCash = 0;
            var initTitleIndex = 6;

            var indexRow = initTitleIndex + 1;
            var indexRowCash = initTitleIndex + 1;
            var orderNumber = 1;
            var orderNumberCash = 1;
            CreateHeaderAndTitleForAccount(ref oWorkSheet, ref initTitleIndex, date);
            CreateHeaderAndTitleForCash(ref oWorkSheetCash, ref initTitleIndex, date);


            var dr = rgwListIncome.Rows[0];


            var rangeDept = oWorkSheet.Range["A" + indexRow, "E" + indexRow];
            rangeDept.Merge(Type.Missing);
            oWorkSheet.Cells[indexRow, 1] =
                dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootName].Value == null
                    ? string.Empty
                    : dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootName].Value.ToString()
                        .ToUpper();
            rangeDept.Font.Bold = true;

            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            oWorkSheet.Cells[indexRow, 2] =
                dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_FullName2].Value == null
                    ? string.Empty
                    : dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_FullName2].Value.ToString();

            var Account = oWorkSheet.get_Range("C" + indexRow, "C" + 3);
            Account.NumberFormat = "@";
            oWorkSheet.Cells[indexRow, 3] = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_AccountNo].Value == null
                ? string.Empty
                : dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_AccountNo]
                    .Value
                    .ToString();

            var Card = oWorkSheet.get_Range("D" + indexRow, "D" + 4);
            Card.NumberFormat = "@";
            oWorkSheet.Cells[indexRow, 4] = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_CardNo].Value == null
                ? string.Empty
                : dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_CardNo]
                    .Value
                    .ToString();
            
            var oThucLinh = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_ThucLinh].Value == null
                ? 0
                : Convert.ToDecimal(
                    dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_ThucLinh].Value.ToString());
            oWorkSheet.Cells[indexRow, 5] = oThucLinh.ToString("#,###");
            oWorkSheet.Cells[indexRow, 6] =
                dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_FullName].Value == null
                    ? string.Empty
                    : dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_FullName].Value.ToString();
            totalAllRealIncome = totalAllRealIncome + oThucLinh;
            var rIdCard = oWorkSheet.get_Range("G" + indexRow, "G" + 7);
            rIdCard.NumberFormat = "@";
            rIdCard.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            oWorkSheet.Cells[indexRow, 7] = dr.Cells["IdCard"].Value == null
                ? string.Empty
                : dr.Cells["IdCard"].Value.ToString();

            for (var i = 1; i < rgwListIncome.RowCount; i++)
            {
                dr = rgwListIncome.Rows[i];

                var userId = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_UserId].Value == null
                    ? 0
                    : Convert.ToInt32(
                        dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_UserId].Value.ToString());
                if (userId > 0)
                {
                    var accountNo = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_AccountNo].Value ==
                                    null
                        ? string.Empty
                        : dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_AccountNo].Value.ToString();
                    var realIncome = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_ThucLinh].Value ==
                                     null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_ThucLinh].Value.ToString());
                    if ((accountNo != string.Empty) && (accountNo.Trim().Length > 0) && (realIncome > 0))
                    {
                        var dr_1 = rgwListIncome.Rows[i - 1];
                        var rootId = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootId].Value ==
                                     null
                            ? 0
                            : int.Parse(
                                dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootId].Value.ToString());
                        var rootId_1 =
                            dr_1.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootId].Value == null
                                ? 0
                                : int.Parse(
                                    dr_1.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootId].Value
                                        .ToString());

                        indexRow++;

                        if ((i < rgwListIncome.RowCount - 1) && (i > 1))
                        {
                            deparmentId = rootId;
                            departmentIdBefore = rootId_1;
                        }

                        if (deparmentId != departmentIdBefore)
                        {
                            rangeDept = oWorkSheet.Range["A" + indexRow, "E" + indexRow];
                            rangeDept.Merge(Type.Missing);
                            oWorkSheet.Cells[indexRow, 1] =
                                dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootName].Value == null
                                    ? string.Empty
                                    : dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootName].Value
                                        .ToString().ToUpper();
                            rangeDept.Font.Bold = true;
                            indexRow++;
                        }

                        /// inserting order number
                        oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                        oWorkSheet.Cells[indexRow, 2] =
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_FullName2].Value == null
                                ? string.Empty
                                : dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_FullName2].Value
                                    .ToString();

                        var Account1 = oWorkSheet.get_Range("C" + indexRow, "C" + 3);
                        Account1.NumberFormat = "@";
                        oWorkSheet.Cells[indexRow, 3] = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_AccountNo]
                                                            .Value == null
                            ? string.Empty
                            : dr.Cells[
                                    IncomeEmployeesKeys.Field_IncomeEmployees_AccountNo]
                                .Value.ToString();

                        var Card1 = oWorkSheet.get_Range("D" + indexRow, "D" + 4);
                        Card1.NumberFormat = "@";
                        oWorkSheet.Cells[indexRow, 4] = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_CardNo]
                                                            .Value == null
                            ? string.Empty
                            : dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_CardNo]
                                .Value.ToString();
                        
                        oWorkSheet.Cells[indexRow, 5] = realIncome.ToString("#,###");
                        oWorkSheet.Cells[indexRow, 6] =
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_FullName].Value == null
                                ? string.Empty
                                : dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_FullName].Value
                                    .ToString();
                        var rIdCard1 = oWorkSheet.get_Range("G" + indexRow, "G" + 7);
                        rIdCard1.NumberFormat = "@";
                        rIdCard1.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        oWorkSheet.Cells[indexRow, 7] = dr.Cells["IdCard"].Value == null
                            ? string.Empty
                            : dr.Cells["IdCard"].Value.ToString();
                        totalAllRealIncome = totalAllRealIncome + realIncome;
                    }
                    else
                    {
                        oWorkSheetCash.Cells[indexRowCash, 1] = orderNumberCash++;

                        oWorkSheetCash.Cells[indexRowCash, 2] =
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_FullName2].Value == null
                                ? string.Empty
                                : dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_FullName2].Value
                                    .ToString();
                        oWorkSheetCash.Cells[indexRowCash, 3] =
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootName].Value == null
                                ? string.Empty
                                : dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootName].Value
                                    .ToString();
                        oWorkSheetCash.Cells[indexRowCash, 4] = realIncome.ToString("#,###");
                        oWorkSheetCash.Cells[indexRowCash, 5] =
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_FullName].Value == null
                                ? string.Empty
                                : dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_FullName].Value
                                    .ToString();
                        totalAllRealIncomeCash = totalAllRealIncomeCash + realIncome;
                        indexRowCash++;
                    }
                }

                backgroundWorkerExport.ReportProgress(i + 1, fullName);
                Thread.Sleep(10);
            }


            indexRow = indexRow + 1;
            oWorkSheet.Cells[indexRow, 1] = "TỔNG CỘNG";
            var totalRange = oWorkSheet.Range[oWorkSheet.Cells[indexRow, 1], oWorkSheet.Cells[indexRow, 2]];
            totalRange.Merge(Type.Missing);
            totalRange.Font.Bold = true;
            totalRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            oWorkSheet.Cells[indexRow, 5] = totalAllRealIncome.ToString("#,###.00");
            ((Range) oWorkSheet.Cells[indexRow, 5]).Font.Bold = true;
            var rangeAll = oWorkSheet.Range["A6", "E" + indexRow];
            rangeAll.Borders.LineStyle = Constants.xlSolid;
            rangeAll.Borders.Weight = 2;
            rangeAll.Font.Size = 12;


            oWorkSheetCash.Cells[indexRowCash, 1] = "TỔNG CỘNG";
            var totalRangeCash =
                oWorkSheetCash.Range[oWorkSheetCash.Cells[indexRowCash, 1], oWorkSheetCash.Cells[indexRowCash, 2]];
            totalRangeCash.Merge(Type.Missing);
            totalRangeCash.Font.Bold = true;
            totalRangeCash.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            oWorkSheetCash.Cells[indexRowCash, 4] = totalAllRealIncomeCash.ToString("#,###.00");
            ((Range) oWorkSheetCash.Cells[indexRowCash, 4]).Font.Bold = true;
            var rangeAllCash = oWorkSheetCash.Range["A6", "E" + indexRowCash];
            rangeAllCash.Borders.LineStyle = Constants.xlSolid;
            rangeAllCash.Borders.Weight = 2;
            rangeAllCash.Font.Size = 12;


            indexRow = indexRow + 2;
            var rangeFooter1 = oWorkSheet.Range[oWorkSheet.Cells[indexRow, 4], oWorkSheet.Cells[indexRow, 5]];
            rangeFooter1.Merge(Type.Missing);
            oWorkSheet.Cells[indexRow, 4] = "Ngày       tháng      năm " + DateTime.Now.Year;
            rangeFooter1.Font.Name = "Times New Roman";
            rangeFooter1.Font.Size = 12;
            rangeFooter1.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            indexRow = indexRow + 1;
            var rangeFooter2 = oWorkSheet.Range[oWorkSheet.Cells[indexRow, 4], oWorkSheet.Cells[indexRow, 5]];
            rangeFooter2.Merge(Type.Missing);
            oWorkSheet.Cells[indexRow, 4] = "TỔNG GIÁM ĐỐC";
            rangeFooter2.Font.Name = "Times New Roman";
            rangeFooter2.Font.Size = 12;
            rangeFooter2.Font.Bold = true;
            rangeFooter2.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            indexRow = indexRow + 5;
            var rangeFooter3 = oWorkSheet.Range[oWorkSheet.Cells[indexRow, 4], oWorkSheet.Cells[indexRow, 5]];
            rangeFooter3.Merge(Type.Missing);
            oWorkSheet.Cells[indexRow, 4] = "NGUYỄN ĐÌNH HÙNG";
            rangeFooter3.Font.Name = "Times New Roman";
            rangeFooter3.Font.Size = 12;
            rangeFooter3.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        }

        private static void CreateHeaderAndTitleForAccount(ref _Worksheet oWorkSheet, ref int initTitleIndex,
            DateTime date)
        {
            var rangeHeader2 = oWorkSheet.Range["A1", "C1"];
            rangeHeader2.Merge(Type.Missing);
            oWorkSheet.Cells[1, 1] = HRMUtil.Constants.SAGS_NAME;
            rangeHeader2.Font.Size = 12;
            rangeHeader2.Font.Bold = true;

            var rangeHeader3 = oWorkSheet.Range["A3", "E3"];
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[3, 1] = "DANH SÁCH CB.CNV NHẬN TIỀN THƯỞNG QUA THẺ ATM";
            rangeHeader3.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader3.Font.Size = 15;
            rangeHeader3.Font.Bold = true;

            var rangeHeader4 = oWorkSheet.Range["A4", "E4"];
            rangeHeader4.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "THỰC CHI NGÀY " + FormatDate.FormatVNDate(date);
            rangeHeader4.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader4.Font.Size = 12;
            rangeHeader4.Font.Bold = true;


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
            oWorkSheet.Cells[initTitleIndex, 6] = "HỌ VÀ TÊN CÓ DẤU";
            ((Range) oWorkSheet.Cells[initTitleIndex, 6]).ColumnWidth = 17;
            oWorkSheet.Cells[initTitleIndex, 7] = "CMND";
            ((Range)oWorkSheet.Cells[initTitleIndex, 7]).ColumnWidth = 17;
            /// format title
            var oTitleRange = oWorkSheet.Range[oWorkSheet.Cells[initTitleIndex, 1], oWorkSheet.Cells[initTitleIndex, 5]];
            oTitleRange.Font.Bold = true;

            oTitleRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        }

        private static void CreateHeaderAndTitleForCash(ref _Worksheet oWorkSheetCash, ref int initTitleIndex,
            DateTime date)
        {
            var rangeHeader2 = oWorkSheetCash.Range["A1", "C1"];
            rangeHeader2.Merge(Type.Missing);
            oWorkSheetCash.Cells[1, 1] = HRMUtil.Constants.SAGS_NAME;
            rangeHeader2.Font.Size = 12;
            rangeHeader2.Font.Bold = true;

            var rangeHeader3 = oWorkSheetCash.Range["A3", "E3"];
            rangeHeader3.Merge(Type.Missing);
            oWorkSheetCash.Cells[3, 1] = "DANH SÁCH CB.CNV NHẬN TIỀN THƯỞNG QUA TIỀN MẶT";
            rangeHeader3.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader3.Font.Size = 15;
            rangeHeader3.Font.Bold = true;

            var rangeHeader4 = oWorkSheetCash.Range["A4", "E4"];
            rangeHeader4.Merge(Type.Missing);
            oWorkSheetCash.Cells[4, 1] = "THỰC CHI NGÀY " + FormatDate.FormatVNDate(date);
            rangeHeader4.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader4.Font.Size = 12;
            rangeHeader4.Font.Bold = true;


            /// inserting title
            oWorkSheetCash.Cells[initTitleIndex, 1] = "STT";
            oWorkSheetCash.Cells[initTitleIndex, 2] = "HỌ VÀ TÊN";
            ((Range) oWorkSheetCash.Cells[initTitleIndex, 2]).ColumnWidth = 30;
            oWorkSheetCash.Cells[initTitleIndex, 3] = "PHÒNG BAN";
            ((Range) oWorkSheetCash.Cells[initTitleIndex, 3]).ColumnWidth = 20;
            oWorkSheetCash.Cells[initTitleIndex, 4] = "SỐ TIỀN";
            ((Range) oWorkSheetCash.Cells[initTitleIndex, 4]).ColumnWidth = 23;
            oWorkSheetCash.Cells[initTitleIndex, 5] = "HỌ VÀ TÊN CÓ DẤU";
            ((Range) oWorkSheetCash.Cells[initTitleIndex, 5]).ColumnWidth = 17;
            /// format title
            var oTitleRange =
                oWorkSheetCash.Range[oWorkSheetCash.Cells[initTitleIndex, 1], oWorkSheetCash.Cells[initTitleIndex, 5]];
            oTitleRange.Font.Bold = true;

            oTitleRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        }


        public static _Application ExportQuyThuongForApproval(RadGridView rgwListIncome, DateTime dataDate,
            string fullName, BackgroundWorker backgroundWorkerExport)
        {
            if (rgwListIncome.RowCount > 0)
            {
                _Application oExcel;
                _Workbook oWorkBook;
                _Worksheet oWorkSheet;

                try
                {
                    GC.Collect();
                    oExcel = oExcel = new Application();
                    oExcel.Visible = false;


                    oWorkBook = oExcel.Workbooks.Add(Missing.Value);
                    oWorkSheet = (_Worksheet) oWorkBook.Worksheets[1];

                    oWorkSheet.Name = "TỔNG HỢP BẢNG THƯỞNG";

                    InsertDataToWorkSheetQuyThuongForApproval(ref oWorkSheet, rgwListIncome, dataDate, fullName,
                        backgroundWorkerExport);

                    oExcel.Visible = false;
                    oExcel.UserControl = false;

                    return oExcel;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return null;
        }

        private static void InsertDataToWorkSheetQuyThuongForApproval(ref _Worksheet oWorkSheet,
            RadGridView rgwListIncome, DateTime date, string fullName, BackgroundWorker backgroundWorkerExport)
        {
            var deparmentId = 0;
            var departmentIdBefore = 0;

            var initTitleIndex = 7;

            var indexRow = initTitleIndex + 1;
            var orderNumber = 1;
            var previousIndexRow = 0;
            var UserId = 0;

            decimal LCD = 0;
            decimal K = 0;
            decimal X = 0;
            decimal OmCoKHH = 0;
            decimal TS = 0;
            decimal TNLD = 0;
            decimal FDiDuong = 0;
            decimal Khac = 0;
            decimal HSLNSThuong = 0;
            var SoThangLamViec = 0;
            decimal BonusValue = 0;
            decimal ThueThuNhap = 0;
            decimal ThucLinh = 0;

            decimal TotalLCD = 0;
            decimal TotalK = 0;
            decimal TotalX = 0;
            decimal TotalOmCoKHH = 0;
            decimal TotalTS = 0;
            decimal TotalTNLD = 0;
            decimal TotalFDiDuong = 0;
            decimal TotalKhac = 0;
            decimal TotalHSLNSThuong = 0;
            var TotalSoThangLamViec = 0;
            decimal TotalBonusValue = 0;
            decimal TotalThueThuNhap = 0;
            decimal TotalThucLinh = 0;

            decimal TotalAllLCD = 0;
            decimal TotalAllK = 0;
            decimal TotalAllX = 0;
            decimal TotalAllOmCoKHH = 0;
            decimal TotalAllTS = 0;
            decimal TotalAllTNLD = 0;
            decimal TotalAllFDiDuong = 0;
            decimal TotalAllKhac = 0;
            decimal TotalAllHSLNSThuong = 0;
            var TotalAllSoThangLamViec = 0;
            decimal TotalAllBonusValue = 0;
            decimal TotalAllThueThuNhap = 0;
            decimal TotalAllThucLinh = 0;

            CreateHeaderAndTitleForQuyThuong(ref oWorkSheet, ref initTitleIndex, date);


            var dr = rgwListIncome.Rows[0];

            previousIndexRow = indexRow;
            var rangeDept = oWorkSheet.Range["A" + indexRow, "C" + indexRow];
            rangeDept.Merge(Type.Missing);
            oWorkSheet.Cells[indexRow, 1] =
                dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootName].Value == null
                    ? string.Empty
                    : dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootName].Value.ToString()
                        .ToUpper();
            rangeDept = oWorkSheet.Range["A" + indexRow, "T" + indexRow];
            rangeDept.Font.Bold = true;

            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;

            UserId = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_UserId].Value == null
                ? 0
                : Convert.ToInt32(
                    dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_UserId].Value.ToString());
            oWorkSheet.Cells[indexRow, 2] = "'" + UserId.ToString(StringFormat.FormatUserId);
            oWorkSheet.Cells[indexRow, 3] =
                dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_FullName].Value == null
                    ? string.Empty
                    : dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_FullName].Value.ToString();
            oWorkSheet.Cells[indexRow, 4] =
                dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_PositionName].Value == null
                    ? string.Empty
                    : dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_PositionName].Value.ToString();


            LCD = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_HSLNS].Value == null
                ? 0
                : Convert.ToDecimal(
                    dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_HSLNS].Value.ToString());
            oWorkSheet.Cells[indexRow, 5] = LCD.ToString(StringFormat.FormatCoefficient);
            TotalLCD = TotalLCD + LCD;

            K = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_HSK].Value == null
                ? 0
                : Convert.ToDecimal(
                    dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_HSK].Value.ToString());
            oWorkSheet.Cells[indexRow, 6] = K.ToString(StringFormat.FormatCoefficient);
            TotalK = TotalK + K;

            X = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_NC_X].Value == null
                ? 0
                : Convert.ToDecimal(
                    dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_NC_X].Value.ToString());
            oWorkSheet.Cells[indexRow, 7] = X.ToString("#,###");
            TotalX = TotalX + X;
            OmCoKHH = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_NC_OmCoKHH].Value == null
                ? 0
                : Convert.ToDecimal(
                    dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_NC_OmCoKHH].Value.ToString());
            oWorkSheet.Cells[indexRow, 8] = OmCoKHH.ToString("#,###");
            TotalOmCoKHH = TotalOmCoKHH + OmCoKHH;
            TS = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_NC_ThaiSan].Value == null
                ? 0
                : Convert.ToDecimal(
                    dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_NC_ThaiSan].Value.ToString());
            oWorkSheet.Cells[indexRow, 9] = TS.ToString("#,###");
            TotalTS = TotalTS + TS;
            TNLD = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_NC_TNLD].Value == null
                ? 0
                : Convert.ToDecimal(
                    dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_NC_TNLD].Value.ToString());
            oWorkSheet.Cells[indexRow, 10] = TNLD.ToString("#,###");
            TotalTNLD = TotalTNLD + TNLD;
            FDiDuong = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_NC_FDiDuong].Value == null
                ? 0
                : Convert.ToDecimal(
                    dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_NC_FDiDuong].Value.ToString());
            oWorkSheet.Cells[indexRow, 11] = FDiDuong.ToString("#,###");
            TotalFDiDuong = TotalFDiDuong + FDiDuong;
            Khac = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_NC_Khac].Value == null
                ? 0
                : Convert.ToDecimal(
                    dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_NC_Khac].Value.ToString());
            oWorkSheet.Cells[indexRow, 12] = Khac.ToString("#,###");
            TotalKhac = TotalKhac + Khac;


            HSLNSThuong = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_HSLNSThuong].Value == null
                ? 0
                : Convert.ToDecimal(
                    dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_HSLNSThuong].Value.ToString());
            oWorkSheet.Cells[indexRow, 13] = HSLNSThuong.ToString(StringFormat.FormatCoefficient);
            TotalHSLNSThuong = TotalHSLNSThuong + HSLNSThuong;

            SoThangLamViec = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_MonthNumber].Value == null
                ? 0
                : Convert.ToInt32(
                    dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_MonthNumber].Value.ToString());
            oWorkSheet.Cells[indexRow, 14] = SoThangLamViec.ToString("#,###");
            TotalSoThangLamViec = TotalSoThangLamViec + SoThangLamViec;

            BonusValue = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_BonusValue].Value == null
                ? 0
                : Convert.ToDecimal(
                    dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_BonusValue].Value.ToString());
            oWorkSheet.Cells[indexRow, 15] = BonusValue.ToString("#,###");
            TotalBonusValue = TotalBonusValue + BonusValue;
            ThueThuNhap = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_ThueTamTinh].Value == null
                ? 0
                : Convert.ToDecimal(
                    dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_ThueTamTinh].Value.ToString());
            oWorkSheet.Cells[indexRow, 16] = ThueThuNhap.ToString("#,###");
            TotalThueThuNhap = TotalThueThuNhap + ThueThuNhap;


            ThucLinh = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_ThucLinh].Value == null
                ? 0
                : Convert.ToDecimal(
                    dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_ThucLinh].Value.ToString());
            oWorkSheet.Cells[indexRow, 17] = ThucLinh.ToString("#,###");
            TotalThucLinh = TotalThucLinh + ThucLinh;

            TotalAllLCD = TotalAllLCD + LCD;
            TotalAllK = TotalAllK + K;
            TotalAllX = TotalAllX + X;
            TotalAllOmCoKHH = TotalAllOmCoKHH + OmCoKHH;
            TotalAllTS = TotalAllTS + TS;
            TotalAllTNLD = TotalAllTNLD + TNLD;
            TotalAllFDiDuong = TotalAllFDiDuong + FDiDuong;
            TotalAllKhac = TotalAllKhac + Khac;
            TotalAllHSLNSThuong = TotalAllHSLNSThuong + HSLNSThuong;
            TotalAllSoThangLamViec = TotalAllSoThangLamViec + SoThangLamViec;
            TotalAllBonusValue = TotalAllBonusValue + BonusValue;
            TotalAllThueThuNhap = TotalAllThueThuNhap + ThueThuNhap;
            TotalAllThucLinh = TotalAllThucLinh + ThucLinh;


            LCD = 0;
            K = 0;
            X = 0;
            OmCoKHH = 0;
            TS = 0;
            TNLD = 0;
            FDiDuong = 0;
            Khac = 0;
            HSLNSThuong = 0;
            BonusValue = 0;
            ThueThuNhap = 0;
            ThucLinh = 0;


            for (var i = 1; i < rgwListIncome.RowCount; i++)
            {
                dr = rgwListIncome.Rows[i];

                var userId = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_UserId].Value == null
                    ? 0
                    : Convert.ToInt32(
                        dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_UserId].Value.ToString());
                if (userId > 0)
                {
                    var dr_1 = rgwListIncome.Rows[i - 1];
                    var rootId = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootId].Value == null
                        ? 0
                        : int.Parse(
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootId].Value.ToString());
                    var rootId_1 = dr_1.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootId].Value ==
                                   null
                        ? 0
                        : int.Parse(
                            dr_1.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootId].Value.ToString());
                    indexRow++;

                    if ((i < rgwListIncome.RowCount - 1) && (i > 1))
                    {
                        deparmentId = rootId;
                        departmentIdBefore = rootId_1;
                    }

                    if (deparmentId != departmentIdBefore)
                    {
                        oWorkSheet.Cells[previousIndexRow, 5] = TotalLCD.ToString(StringFormat.FormatCoefficient);
                        oWorkSheet.Cells[previousIndexRow, 6] = TotalK.ToString(StringFormat.FormatCoefficient);
                        oWorkSheet.Cells[previousIndexRow, 7] = TotalX.ToString("#,###");
                        oWorkSheet.Cells[previousIndexRow, 8] = TotalOmCoKHH.ToString("#,###");
                        oWorkSheet.Cells[previousIndexRow, 9] = TotalTS.ToString("#,###");
                        oWorkSheet.Cells[previousIndexRow, 10] = TotalTNLD.ToString("#,###");
                        oWorkSheet.Cells[previousIndexRow, 11] = TotalFDiDuong.ToString("#,###");
                        oWorkSheet.Cells[previousIndexRow, 12] = TotalKhac.ToString("#,###");
                        oWorkSheet.Cells[previousIndexRow, 13] = TotalHSLNSThuong.ToString("#,###");
                        oWorkSheet.Cells[previousIndexRow, 14] = TotalSoThangLamViec.ToString("#,###");
                        oWorkSheet.Cells[previousIndexRow, 15] = TotalBonusValue.ToString("#,###");
                        oWorkSheet.Cells[previousIndexRow, 16] = TotalThueThuNhap.ToString("#,###");
                        oWorkSheet.Cells[previousIndexRow, 17] = TotalThucLinh.ToString("#,###");

                        previousIndexRow = indexRow;


                        TotalLCD = 0;
                        TotalK = 0;
                        TotalX = 0;
                        TotalOmCoKHH = 0;
                        TotalTS = 0;
                        TotalTNLD = 0;
                        TotalFDiDuong = 0;
                        TotalKhac = 0;
                        TotalHSLNSThuong = 0;
                        TotalBonusValue = 0;
                        TotalThueThuNhap = 0;
                        TotalThucLinh = 0;


                        rangeDept = oWorkSheet.Range["A" + indexRow, "C" + indexRow];
                        rangeDept.Merge(Type.Missing);
                        oWorkSheet.Cells[indexRow, 1] =
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootName].Value == null
                                ? string.Empty
                                : dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootName].Value
                                    .ToString().ToUpper();
                        rangeDept = oWorkSheet.Range["A" + indexRow, "T" + indexRow];
                        rangeDept.Font.Bold = true;
                        indexRow++;
                    }


                    /// inserting order number
                    oWorkSheet.Cells[indexRow, 1] = orderNumber++;

                    UserId = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_UserId].Value == null
                        ? 0
                        : Convert.ToInt32(
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_UserId].Value.ToString());
                    oWorkSheet.Cells[indexRow, 2] = "'" + UserId.ToString(StringFormat.FormatUserId);
                    oWorkSheet.Cells[indexRow, 3] =
                        dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_FullName].Value == null
                            ? string.Empty
                            : dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_FullName].Value.ToString();
                    oWorkSheet.Cells[indexRow, 4] =
                        dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_PositionName].Value == null
                            ? string.Empty
                            : dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_PositionName].Value
                                .ToString();

                    LCD = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_HSLNS].Value == null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_HSLNS].Value.ToString());
                    oWorkSheet.Cells[indexRow, 5] = LCD.ToString(StringFormat.FormatCoefficient);
                    TotalLCD = TotalLCD + LCD;
                    K = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_HSK].Value == null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_HSK].Value.ToString());
                    oWorkSheet.Cells[indexRow, 6] = K.ToString(StringFormat.FormatCoefficient);
                    TotalK = TotalK + K;

                    X = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_NC_X].Value == null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_NC_X].Value.ToString());
                    oWorkSheet.Cells[indexRow, 7] = X.ToString("#,###");
                    TotalX = TotalX + X;
                    OmCoKHH = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_NC_OmCoKHH].Value == null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_NC_OmCoKHH].Value.ToString());
                    oWorkSheet.Cells[indexRow, 8] = OmCoKHH.ToString("#,###");
                    TotalOmCoKHH = TotalOmCoKHH + OmCoKHH;
                    TS = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_NC_ThaiSan].Value == null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_NC_ThaiSan].Value.ToString());
                    oWorkSheet.Cells[indexRow, 9] = TS.ToString("#,###");
                    TotalTS = TotalTS + TS;
                    TNLD = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_NC_TNLD].Value == null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_NC_TNLD].Value.ToString());
                    oWorkSheet.Cells[indexRow, 10] = TNLD.ToString("#,###");
                    TotalTNLD = TotalTNLD + TNLD;
                    FDiDuong = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_NC_FDiDuong].Value ==
                               null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_NC_FDiDuong].Value.ToString
                                ());
                    oWorkSheet.Cells[indexRow, 11] = FDiDuong.ToString("#,###");
                    TotalFDiDuong = TotalFDiDuong + FDiDuong;
                    Khac = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_NC_Khac].Value == null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_NC_Khac].Value.ToString());
                    oWorkSheet.Cells[indexRow, 12] = Khac.ToString("#,###");
                    TotalKhac = TotalKhac + Khac;


                    HSLNSThuong = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_HSLNSThuong].Value ==
                                  null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_HSLNSThuong].Value.ToString
                                ());
                    oWorkSheet.Cells[indexRow, 13] = HSLNSThuong.ToString(StringFormat.FormatCoefficient);
                    TotalHSLNSThuong = TotalHSLNSThuong + HSLNSThuong;
                    SoThangLamViec =
                        dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_MonthNumber].Value == null
                            ? 0
                            : Convert.ToInt32(
                                dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_MonthNumber].Value
                                    .ToString());
                    oWorkSheet.Cells[indexRow, 14] = SoThangLamViec.ToString("#,###");
                    TotalSoThangLamViec = TotalSoThangLamViec + SoThangLamViec;

                    BonusValue = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_BonusValue].Value ==
                                 null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_BonusValue].Value.ToString());
                    oWorkSheet.Cells[indexRow, 15] = BonusValue.ToString("#,###");
                    TotalBonusValue = TotalBonusValue + BonusValue;
                    ThueThuNhap = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_ThueTamTinh].Value ==
                                  null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_ThueTamTinh].Value.ToString
                                ());
                    oWorkSheet.Cells[indexRow, 16] = ThueThuNhap.ToString("#,###");
                    TotalThueThuNhap = TotalThueThuNhap + ThueThuNhap;


                    ThucLinh = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_ThucLinh].Value == null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_ThucLinh].Value.ToString());
                    oWorkSheet.Cells[indexRow, 17] = ThucLinh.ToString("#,###");
                    TotalThucLinh = TotalThucLinh + ThucLinh;

                    TotalAllLCD = TotalAllLCD + LCD;
                    TotalAllK = TotalAllK + K;
                    TotalAllX = TotalAllX + X;
                    TotalAllOmCoKHH = TotalAllOmCoKHH + OmCoKHH;
                    TotalAllTS = TotalAllTS + TS;
                    TotalAllTNLD = TotalAllTNLD + TNLD;
                    TotalAllFDiDuong = TotalAllFDiDuong + FDiDuong;
                    TotalAllKhac = TotalAllKhac + Khac;
                    TotalAllHSLNSThuong = TotalAllHSLNSThuong + HSLNSThuong;
                    TotalAllSoThangLamViec = TotalAllSoThangLamViec + SoThangLamViec;
                    TotalAllBonusValue = TotalAllBonusValue + BonusValue;
                    TotalAllThueThuNhap = TotalAllThueThuNhap + ThueThuNhap;
                    TotalAllThucLinh = TotalAllThucLinh + ThucLinh;
                }
                backgroundWorkerExport.ReportProgress(i + 1, fullName);
                Thread.Sleep(10);
            }


            oWorkSheet.Cells[previousIndexRow, 5] = TotalLCD.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[previousIndexRow, 6] = TotalK.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[previousIndexRow, 7] = TotalX.ToString("#,###");
            oWorkSheet.Cells[previousIndexRow, 8] = TotalOmCoKHH.ToString("#,###");
            oWorkSheet.Cells[previousIndexRow, 9] = TotalTS.ToString("#,###");
            oWorkSheet.Cells[previousIndexRow, 10] = TotalTNLD.ToString("#,###");
            oWorkSheet.Cells[previousIndexRow, 11] = TotalFDiDuong.ToString("#,###");
            oWorkSheet.Cells[previousIndexRow, 12] = TotalKhac.ToString("#,###");
            oWorkSheet.Cells[previousIndexRow, 13] = TotalHSLNSThuong.ToString("#,###");
            oWorkSheet.Cells[previousIndexRow, 14] = TotalSoThangLamViec.ToString("#,###");
            oWorkSheet.Cells[previousIndexRow, 15] = TotalBonusValue.ToString("#,###");
            oWorkSheet.Cells[previousIndexRow, 16] = TotalThueThuNhap.ToString("#,###");
            oWorkSheet.Cells[previousIndexRow, 17] = TotalThucLinh.ToString("#,###");


            indexRow = indexRow + 1;
            oWorkSheet.Cells[indexRow, 1] = rgwListIncome.RowCount;
            oWorkSheet.Cells[indexRow, 2] = "TỔNG CỘNG";
            var totalRange = oWorkSheet.Range[oWorkSheet.Cells[indexRow, 2], oWorkSheet.Cells[indexRow, 3]];
            totalRange.Merge(Type.Missing);
            totalRange = oWorkSheet.Range[oWorkSheet.Cells[indexRow, 1], oWorkSheet.Cells[indexRow, 20]];
            totalRange.Font.Bold = true;


            oWorkSheet.Cells[indexRow, 5] = TotalAllLCD.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[indexRow, 6] = TotalAllK.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[indexRow, 7] = TotalAllX.ToString("#,###");
            oWorkSheet.Cells[indexRow, 8] = TotalAllOmCoKHH.ToString("#,###");
            oWorkSheet.Cells[indexRow, 9] = TotalAllTS.ToString("#,###");
            oWorkSheet.Cells[indexRow, 10] = TotalAllTNLD.ToString("#,###");
            oWorkSheet.Cells[indexRow, 11] = TotalAllFDiDuong.ToString("#,###");
            oWorkSheet.Cells[indexRow, 12] = TotalAllKhac.ToString("#,###");
            oWorkSheet.Cells[indexRow, 13] = TotalAllHSLNSThuong.ToString("#,###");
            oWorkSheet.Cells[indexRow, 14] = TotalAllSoThangLamViec.ToString("#,###");
            oWorkSheet.Cells[indexRow, 15] = TotalAllBonusValue.ToString("#,###");
            oWorkSheet.Cells[indexRow, 16] = TotalAllThueThuNhap.ToString("#,###");
            oWorkSheet.Cells[indexRow, 17] = TotalAllThucLinh.ToString("#,###");

            ((Range) oWorkSheet.Cells[indexRow, 5]).Font.Bold = true;
            var rangeAll = oWorkSheet.Range["A6", "Q" + indexRow];
            rangeAll.Borders.LineStyle = Constants.xlSolid;
            rangeAll.Borders.Weight = 2;
            rangeAll.Font.Size = 8;
            rangeAll.Font.Name = "Times New Roman";


            indexRow = indexRow + 2;
            oWorkSheet.Cells[indexRow, 3] = "NGƯỜI LẬP BẢNG";
            oWorkSheet.Cells[indexRow, 5] = "P.TỔ CHỨC HÀNH CHÍNH";
            oWorkSheet.Cells[indexRow, 9] = "P.TÀI CHÍNH KẾ TOÁN";
            oWorkSheet.Cells[indexRow, 14] = "GIÁM DỐC";
            var rangeFooter1 = oWorkSheet.Range[oWorkSheet.Cells[indexRow, 1], oWorkSheet.Cells[indexRow, 20]];
            rangeFooter1.Font.Name = "Times New Roman";
            rangeFooter1.Font.Size = 9;
            rangeFooter1.Font.Bold = true;
        }

        private static void CreateHeaderAndTitleForQuyThuong(ref _Worksheet oWorkSheet, ref int initTitleIndex,
            DateTime date)
        {
            var rangeHeader1 = oWorkSheet.Range["A1", "D1"];
            rangeHeader1.Merge(Type.Missing);
            oWorkSheet.Cells[1, 1] = HRMUtil.Constants.ACV_NAME;
            rangeHeader1.Font.Size = 12;
            rangeHeader1.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            var rangeHeader2 = oWorkSheet.Range["A2", "D2"];
            rangeHeader2.Merge(Type.Missing);
            oWorkSheet.Cells[2, 1] = HRMUtil.Constants.SAGS_NAME;
            rangeHeader2.Font.Size = 12;
            rangeHeader2.Font.Bold = true;
            rangeHeader2.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            var rangeHeader3 = oWorkSheet.Range["A4", "Q4"];
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "BẢNG TIỀN THƯỞNG " + date.Month + " NĂM " + date.Year;
            rangeHeader3.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader3.Font.Size = 15;
            rangeHeader3.Font.Bold = true;
            rangeHeader3.HorizontalAlignment = XlHAlign.xlHAlignCenter;


            oWorkSheet.Cells[initTitleIndex - 1, 5] = "Hệ Số";
            oWorkSheet.Cells[initTitleIndex - 1, 7] = "Ngày Công";
            oWorkSheet.Cells[initTitleIndex - 1, 13] = "Tiền Thưởng";

            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            oWorkSheet.Cells[initTitleIndex, 2] = "Mã NV";
            oWorkSheet.Cells[initTitleIndex, 3] = "Họ và tên";
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 30;
            oWorkSheet.Cells[initTitleIndex, 4] = "Chức danh";
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).ColumnWidth = 15;
            oWorkSheet.Cells[initTitleIndex, 5] = "LNS";
            oWorkSheet.Cells[initTitleIndex, 6] = "K";
            oWorkSheet.Cells[initTitleIndex, 7] = "Làm việc";
            oWorkSheet.Cells[initTitleIndex, 8] = "OmCoKHH";
            oWorkSheet.Cells[initTitleIndex, 9] = "NC_ThaiSan";
            oWorkSheet.Cells[initTitleIndex, 10] = "NC_TNLD";
            oWorkSheet.Cells[initTitleIndex, 11] = "NC_FDiDuong";
            oWorkSheet.Cells[initTitleIndex, 12] = "NC_Khac";
            oWorkSheet.Cells[initTitleIndex, 13] = "HSL tính thưởng";
            oWorkSheet.Cells[initTitleIndex, 14] = "Số thánh làm việc";
            oWorkSheet.Cells[initTitleIndex, 15] = "Tiền thưởng";
            oWorkSheet.Cells[initTitleIndex, 16] = "Thuế tạm tính";
            oWorkSheet.Cells[initTitleIndex, 17] = "Thực lĩnh";

            /// format title
            var oTitleRange = oWorkSheet.Range["A6", "Q7"];
            oTitleRange.Font.Bold = true;
            oTitleRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            oTitleRange = oWorkSheet.Range["E6", "F6"];
            oTitleRange.Merge(Type.Missing);
            oTitleRange = oWorkSheet.Range["G6", "L6"];
            oTitleRange.Merge(Type.Missing);
            oTitleRange = oWorkSheet.Range["M6", "Q6"];
            oTitleRange.Merge(Type.Missing);

            oTitleRange = oWorkSheet.Range["A6", "A7"];
            oTitleRange.Merge(Type.Missing);
            oTitleRange = oWorkSheet.Range["B6", "B7"];
            oTitleRange.Merge(Type.Missing);
            oTitleRange = oWorkSheet.Range["C6", "C7"];
            oTitleRange.Merge(Type.Missing);
            oTitleRange = oWorkSheet.Range["D6", "D7"];
            oTitleRange.Merge(Type.Missing);
        }


        public static _Application ExportBSDTForApproval(RadGridView rgwListIncome, DateTime dataDate, int bonusYear,
            string fullName, BackgroundWorker backgroundWorkerExport)
        {
            if (rgwListIncome.RowCount > 0)
            {
                _Application oExcel;
                _Workbook oWorkBook;
                _Worksheet oWorkSheet;

                try
                {
                    GC.Collect();
                    oExcel = oExcel = new Application();
                    oExcel.Visible = false;


                    oWorkBook = oExcel.Workbooks.Add(Missing.Value);
                    oWorkSheet = (_Worksheet) oWorkBook.Worksheets[1];

                    oWorkSheet.Name = "TỔNG HỢP BẢNG BSDT";

                    InsertDataToWorkSheetBSDTForApproval(ref oWorkSheet, rgwListIncome, dataDate, bonusYear, fullName,
                        backgroundWorkerExport);

                    oExcel.Visible = false;
                    oExcel.UserControl = false;

                    return oExcel;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return null;
        }

        private static void InsertDataToWorkSheetBSDTForApproval(ref _Worksheet oWorkSheet, RadGridView rgwListIncome,
            DateTime date, int bonusYear, string fullName, BackgroundWorker backgroundWorkerExport)
        {
            var deparmentId = 0;
            var departmentIdBefore = 0;

            var initTitleIndex = 5;

            var indexRow = initTitleIndex + 1;
            var orderNumber = 1;
            var previousIndexRow = 0;
            var UserId = 0;

            decimal K = 0;
            decimal HSLNS_BSDT = 0;
            decimal BonusValue = 0;
            decimal ThueThuNhap = 0;
            decimal ThucLinh = 0;

            decimal TotalK = 0;
            decimal TotalHSLNS_BSDT = 0;
            decimal TotalBonusValue = 0;
            decimal TotalThueThuNhap = 0;
            decimal TotalThucLinh = 0;

            decimal TotalAllK = 0;
            decimal TotalAllHSLNS_BSDT = 0;
            decimal TotalAllBonusValue = 0;
            decimal TotalAllThueThuNhap = 0;
            decimal TotalAllThucLinh = 0;

            CreateHeaderAndTitleForBSDT(ref oWorkSheet, ref initTitleIndex, date, bonusYear);


            var dr = rgwListIncome.Rows[0];

            previousIndexRow = indexRow;
            var rangeDept = oWorkSheet.Range["A" + indexRow, "C" + indexRow];
            rangeDept.Merge(Type.Missing);
            oWorkSheet.Cells[indexRow, 1] =
                dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootName].Value == null
                    ? string.Empty
                    : dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootName].Value.ToString()
                        .ToUpper();
            rangeDept = oWorkSheet.Range["A" + indexRow, "T" + indexRow];
            rangeDept.Font.Bold = true;

            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;

            UserId = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_UserId].Value == null
                ? 0
                : Convert.ToInt32(
                    dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_UserId].Value.ToString());
            oWorkSheet.Cells[indexRow, 2] = "'" + UserId.ToString(StringFormat.FormatUserId);
            oWorkSheet.Cells[indexRow, 3] =
                dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_FullName].Value == null
                    ? string.Empty
                    : dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_FullName].Value.ToString();
            oWorkSheet.Cells[indexRow, 4] =
                dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_PositionName].Value == null
                    ? string.Empty
                    : dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_PositionName].Value.ToString();

            try
            {
                HSLNS_BSDT = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_HSLNSThuong].Value == null
                    ? 0
                    : Convert.ToDecimal(
                        dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_HSLNSThuong].Value);
                oWorkSheet.Cells[indexRow, 5] = HSLNS_BSDT.ToString(StringFormat.FormatCoefficient3Digit);
                TotalHSLNS_BSDT = TotalHSLNS_BSDT + HSLNS_BSDT;
            }
            catch
            {
            }

            try
            {
                K = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_HSK].Value == null
                    ? 0
                    : Convert.ToDecimal(dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_HSK].Value);
                oWorkSheet.Cells[indexRow, 6] = K.ToString(StringFormat.FormatCoefficient3Digit);
                TotalK = TotalK + K;
            }
            catch
            {
            }

            BonusValue = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_BonusValue].Value == null
                ? 0
                : Convert.ToDecimal(dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_BonusValue].Value);
            oWorkSheet.Cells[indexRow, 7] = BonusValue.ToString("#,###");
            TotalBonusValue = TotalBonusValue + BonusValue;

            ThueThuNhap = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_ThueTamTinh].Value == null
                ? 0
                : Convert.ToDecimal(dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_ThueTamTinh].Value);
            oWorkSheet.Cells[indexRow, 8] = ThueThuNhap.ToString("#,###");
            TotalThueThuNhap = TotalThueThuNhap + ThueThuNhap;


            ThucLinh = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_ThucLinh].Value == null
                ? 0
                : Convert.ToDecimal(dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_ThucLinh].Value);
            oWorkSheet.Cells[indexRow, 9] = ThucLinh.ToString("#,###");
            TotalThucLinh = TotalThucLinh + ThucLinh;


            TotalAllK = TotalAllK + K;
            TotalAllHSLNS_BSDT = TotalAllHSLNS_BSDT + HSLNS_BSDT;

            TotalAllBonusValue = TotalAllBonusValue + BonusValue;
            TotalAllThueThuNhap = TotalAllThueThuNhap + ThueThuNhap;
            TotalAllThucLinh = TotalAllThucLinh + ThucLinh;


            K = 0;
            HSLNS_BSDT = 0;
            BonusValue = 0;
            ThueThuNhap = 0;
            ThucLinh = 0;


            for (var i = 1; i < rgwListIncome.RowCount; i++)
            {
                dr = rgwListIncome.Rows[i];

                var userId = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_UserId].Value == null
                    ? 0
                    : Convert.ToInt32(
                        dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_UserId].Value.ToString());
                if (userId > 0)
                {
                    var dr_1 = rgwListIncome.Rows[i - 1];
                    var rootId = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootId].Value == null
                        ? 0
                        : int.Parse(
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootId].Value.ToString());
                    var rootId_1 = dr_1.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootId].Value ==
                                   null
                        ? 0
                        : int.Parse(
                            dr_1.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootId].Value.ToString());
                    indexRow++;

                    if ((i < rgwListIncome.RowCount - 1) && (i > 1))
                    {
                        deparmentId = rootId;
                        departmentIdBefore = rootId_1;
                    }

                    if (deparmentId != departmentIdBefore)
                    {
                        oWorkSheet.Cells[previousIndexRow, 5] =
                            TotalHSLNS_BSDT.ToString(StringFormat.FormatCoefficient3Digit);
                        oWorkSheet.Cells[previousIndexRow, 6] = TotalK.ToString(StringFormat.FormatCoefficient3Digit);
                        oWorkSheet.Cells[previousIndexRow, 7] = TotalBonusValue.ToString("#,###");
                        oWorkSheet.Cells[previousIndexRow, 8] = TotalThueThuNhap.ToString("#,###");
                        oWorkSheet.Cells[previousIndexRow, 9] = TotalThucLinh.ToString("#,###");

                        previousIndexRow = indexRow;


                        TotalK = 0;
                        TotalHSLNS_BSDT = 0;
                        TotalBonusValue = 0;
                        TotalThueThuNhap = 0;
                        TotalThucLinh = 0;


                        rangeDept = oWorkSheet.Range["A" + indexRow, "C" + indexRow];
                        rangeDept.Merge(Type.Missing);
                        oWorkSheet.Cells[indexRow, 1] =
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootName].Value == null
                                ? string.Empty
                                : dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootName].Value
                                    .ToString().ToUpper();
                        rangeDept = oWorkSheet.Range["A" + indexRow, "T" + indexRow];
                        rangeDept.Font.Bold = true;
                        indexRow++;
                    }


                    /// inserting order number
                    oWorkSheet.Cells[indexRow, 1] = orderNumber++;

                    UserId = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_UserId].Value == null
                        ? 0
                        : Convert.ToInt32(
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_UserId].Value.ToString());
                    oWorkSheet.Cells[indexRow, 2] = "'" + UserId.ToString(StringFormat.FormatUserId);
                    oWorkSheet.Cells[indexRow, 3] =
                        dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_FullName].Value == null
                            ? string.Empty
                            : dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_FullName].Value.ToString();
                    oWorkSheet.Cells[indexRow, 4] =
                        dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_PositionName].Value == null
                            ? string.Empty
                            : dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_PositionName].Value
                                .ToString();

                    try
                    {
                        HSLNS_BSDT =
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_HSLNSThuong].Value == null
                                ? 0
                                : Convert.ToDecimal(
                                    dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_HSLNSThuong].Value);
                        oWorkSheet.Cells[indexRow, 5] = HSLNS_BSDT.ToString(StringFormat.FormatCoefficient3Digit);
                        TotalHSLNS_BSDT = TotalHSLNS_BSDT + HSLNS_BSDT;
                    }
                    catch
                    {
                    }

                    try
                    {
                        K = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_HSK].Value == null
                            ? 0
                            : Convert.ToDecimal(
                                dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_HSK].Value);
                        oWorkSheet.Cells[indexRow, 6] = K.ToString(StringFormat.FormatCoefficient3Digit);
                        TotalK = TotalK + K;
                    }
                    catch
                    {
                    }

                    BonusValue = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_BonusValue].Value ==
                                 null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_BonusValue].Value);
                    oWorkSheet.Cells[indexRow, 7] = BonusValue.ToString("#,###");
                    TotalBonusValue = TotalBonusValue + BonusValue;

                    ThueThuNhap = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_ThueTamTinh].Value ==
                                  null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_ThueTamTinh].Value);
                    oWorkSheet.Cells[indexRow, 8] = ThueThuNhap.ToString("#,###");
                    TotalThueThuNhap = TotalThueThuNhap + ThueThuNhap;


                    ThucLinh = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_ThucLinh].Value == null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_ThucLinh].Value);
                    oWorkSheet.Cells[indexRow, 9] = ThucLinh.ToString("#,###");
                    TotalThucLinh = TotalThucLinh + ThucLinh;

                    TotalAllK = TotalAllK + K;
                    TotalAllHSLNS_BSDT = TotalAllHSLNS_BSDT + HSLNS_BSDT;
                    TotalAllBonusValue = TotalAllBonusValue + BonusValue;
                    TotalAllThueThuNhap = TotalAllThueThuNhap + ThueThuNhap;
                    TotalAllThucLinh = TotalAllThucLinh + ThucLinh;
                }
                backgroundWorkerExport.ReportProgress(i + 1, fullName);
                Thread.Sleep(10);
            }


            oWorkSheet.Cells[previousIndexRow, 5] = TotalHSLNS_BSDT.ToString(StringFormat.FormatCoefficient3Digit);
            oWorkSheet.Cells[previousIndexRow, 6] = TotalK.ToString(StringFormat.FormatCoefficient3Digit);

            oWorkSheet.Cells[previousIndexRow, 7] = TotalBonusValue.ToString("#,###");
            oWorkSheet.Cells[previousIndexRow, 8] = TotalThueThuNhap.ToString("#,###");
            oWorkSheet.Cells[previousIndexRow, 9] = TotalThucLinh.ToString("#,###");


            indexRow = indexRow + 1;
            oWorkSheet.Cells[indexRow, 1] = rgwListIncome.RowCount;
            oWorkSheet.Cells[indexRow, 2] = "TỔNG CỘNG";
            var totalRange = oWorkSheet.Range[oWorkSheet.Cells[indexRow, 2], oWorkSheet.Cells[indexRow, 3]];
            totalRange.Merge(Type.Missing);
            totalRange = oWorkSheet.Range[oWorkSheet.Cells[indexRow, 1], oWorkSheet.Cells[indexRow, 20]];
            totalRange.Font.Bold = true;


            oWorkSheet.Cells[indexRow, 5] = TotalAllHSLNS_BSDT.ToString(StringFormat.FormatCoefficient3Digit);
            oWorkSheet.Cells[indexRow, 6] = TotalAllK.ToString(StringFormat.FormatCoefficient3Digit);
            oWorkSheet.Cells[indexRow, 7] = TotalAllBonusValue.ToString("#,###");
            oWorkSheet.Cells[indexRow, 8] = TotalAllThueThuNhap.ToString("#,###");
            oWorkSheet.Cells[indexRow, 9] = TotalAllThucLinh.ToString("#,###");

            ((Range) oWorkSheet.Cells[indexRow, 5]).Font.Bold = true;
            var rangeAll = oWorkSheet.Range["A5", "I" + indexRow];
            rangeAll.Borders.LineStyle = Constants.xlSolid;
            rangeAll.Borders.Weight = 2;
            rangeAll.Font.Size = 8;
            rangeAll.Font.Name = "Times New Roman";


            indexRow = indexRow + 2;
            oWorkSheet.Cells[indexRow, 1] = "NGƯỜI LẬP BẢNG";
            oWorkSheet.Cells[indexRow, 3] = "P.TỔ CHỨC HÀNH CHÍNH";
            oWorkSheet.Cells[indexRow, 4] = "P.TÀI CHÍNH KẾ TOÁN";
            oWorkSheet.Cells[indexRow, 7] = "TỔNG GIÁM DỐC";
            var rangeFooter1 = oWorkSheet.Range[oWorkSheet.Cells[indexRow, 1], oWorkSheet.Cells[indexRow, 9]];
            rangeFooter1.Font.Name = "Times New Roman";
            rangeFooter1.Font.Size = 9;
            rangeFooter1.Font.Bold = true;
        }

        private static void CreateHeaderAndTitleForBSDT(ref _Worksheet oWorkSheet, ref int initTitleIndex, DateTime date,
            int bonusYear)
        {
            var rangeHeader2 = oWorkSheet.Range["A1", "D1"];
            rangeHeader2.Merge(Type.Missing);
            oWorkSheet.Cells[1, 1] = HRMUtil.Constants.SAGS_NAME;
            rangeHeader2.Font.Size = 12;
            rangeHeader2.Font.Bold = true;
            rangeHeader2.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            var rangeHeader3 = oWorkSheet.Range["A3", "I3"];
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[3, 1] = "BẢNG TIỀN THƯỞNG BSDT NĂM " + bonusYear + " THỰC CHI NGÀY " +
                                     FormatDate.FormatVNDate(date);
            rangeHeader3.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader3.Font.Size = 15;
            rangeHeader3.Font.Bold = true;
            rangeHeader3.HorizontalAlignment = XlHAlign.xlHAlignCenter;


            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            oWorkSheet.Cells[initTitleIndex, 2] = "Mã NV";
            oWorkSheet.Cells[initTitleIndex, 3] = "Họ và tên";
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 30;
            oWorkSheet.Cells[initTitleIndex, 4] = "Chức danh";
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).ColumnWidth = 15;
            oWorkSheet.Cells[initTitleIndex, 5] = "HSLNS Tính BSDT";
            oWorkSheet.Cells[initTitleIndex, 6] = "K";
            oWorkSheet.Cells[initTitleIndex, 7] = "Tiền thưởng";
            oWorkSheet.Cells[initTitleIndex, 8] = "Thuế tạm tính";
            oWorkSheet.Cells[initTitleIndex, 9] = "Thực lĩnh";

            /// format title
            var oTitleRange = oWorkSheet.Range["A5", "I5"];
            oTitleRange.Font.Bold = true;
            oTitleRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        }


        public static _Application ExportLeTetForApproval(RadGridView rgwListIncome, DateTime dataDate, int bonusYear,
            string fullName, BackgroundWorker backgroundWorkerExport)
        {
            if (rgwListIncome.RowCount > 0)
            {
                _Application oExcel;
                _Workbook oWorkBook;
                _Worksheet oWorkSheet;

                try
                {
                    GC.Collect();
                    oExcel = oExcel = new Application();
                    oExcel.Visible = false;


                    oWorkBook = oExcel.Workbooks.Add(Missing.Value);
                    oWorkSheet = (_Worksheet) oWorkBook.Worksheets[1];

                    oWorkSheet.Name = "TỔNG HỢP BẢNG BSDT";

                    InsertDataToWorkSheetLeTetForApproval(ref oWorkSheet, rgwListIncome, dataDate, bonusYear, fullName,
                        backgroundWorkerExport);

                    oExcel.Visible = false;
                    oExcel.UserControl = false;

                    return oExcel;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return null;
        }

        private static void InsertDataToWorkSheetLeTetForApproval(ref _Worksheet oWorkSheet, RadGridView rgwListIncome,
            DateTime date, int bonusYear, string fullName, BackgroundWorker backgroundWorkerExport)
        {
            var deparmentId = 0;
            var departmentIdBefore = 0;

            var initTitleIndex = 5;

            var indexRow = initTitleIndex + 1;
            var orderNumber = 1;
            var previousIndexRow = 0;
            var UserId = 0;

            decimal ThangLamViec = 0;
            decimal TienThuong = 0;
            decimal ThueTNCN = 0;
            decimal ThucLinh = 0;

            decimal TotalThangLamViec = 0;
            decimal TotalTienThuong = 0;
            decimal TotalThueTNCN = 0;
            decimal TotalThucLinh = 0;

            decimal TotalAllThangLamViec = 0;
            decimal TotalAllTienThuong = 0;
            decimal TotalAllThueTNCN = 0;
            decimal TotalAllThucLinh = 0;

            CreateHeaderAndTitleForLeTet(ref oWorkSheet, ref initTitleIndex, date, bonusYear);


            var dr = rgwListIncome.Rows[0];

            previousIndexRow = indexRow;
            var rangeDept = oWorkSheet.Range["A" + indexRow, "C" + indexRow];
            rangeDept.Merge(Type.Missing);
            oWorkSheet.Cells[indexRow, 1] =
                dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootName].Value == null
                    ? string.Empty
                    : dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootName].Value.ToString()
                        .ToUpper();
            rangeDept = oWorkSheet.Range["A" + indexRow, "T" + indexRow];
            rangeDept.Font.Bold = true;

            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;

            UserId = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_UserId].Value == null
                ? 0
                : Convert.ToInt32(
                    dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_UserId].Value.ToString());
            oWorkSheet.Cells[indexRow, 2] = "'" + UserId.ToString(StringFormat.FormatUserId);
            oWorkSheet.Cells[indexRow, 3] =
                dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_FullName].Value == null
                    ? string.Empty
                    : dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_FullName].Value.ToString();
            oWorkSheet.Cells[indexRow, 4] =
                dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_PositionName].Value == null
                    ? string.Empty
                    : dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_PositionName].Value.ToString();


            ThangLamViec = dr.Cells["MonthNumber_Total"].Value == null
                ? 0
                : Convert.ToDecimal(dr.Cells["MonthNumber_Total"].Value);
            oWorkSheet.Cells[indexRow, 5] = ThangLamViec.ToString(StringFormat.FormatCoefficient3Digit);
            TotalThangLamViec = TotalThangLamViec + ThangLamViec;

            TienThuong = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_BonusValue].Value == null
                ? 0
                : Convert.ToDecimal(dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_BonusValue].Value);
            oWorkSheet.Cells[indexRow, 6] = TienThuong.ToString("#,###");
            TotalTienThuong = TotalTienThuong + TienThuong;

            ThueTNCN = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_ThueTamTinh].Value == null
                ? 0
                : Convert.ToDecimal(dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_ThueTamTinh].Value);
            oWorkSheet.Cells[indexRow, 7] = ThueTNCN.ToString("#,###");
            TotalThueTNCN = TotalThueTNCN + ThueTNCN;

            ThucLinh = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_ThucLinh].Value == null
                ? 0
                : Convert.ToDecimal(dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_ThucLinh].Value);
            oWorkSheet.Cells[indexRow, 8] = ThucLinh.ToString("#,###");
            TotalThucLinh = TotalThucLinh + ThucLinh;


            TotalAllThangLamViec = TotalAllThangLamViec + ThangLamViec;
            TotalAllTienThuong = TotalAllTienThuong + TienThuong;
            TotalAllThueTNCN = TotalAllThueTNCN + ThueTNCN;
            TotalAllThucLinh = TotalAllThucLinh + ThucLinh;


            ThangLamViec = 0;
            TienThuong = 0;
            ThueTNCN = 0;
            ThucLinh = 0;


            for (var i = 1; i < rgwListIncome.RowCount; i++)
            {
                dr = rgwListIncome.Rows[i];

                var userId = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_UserId].Value == null
                    ? 0
                    : Convert.ToInt32(
                        dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_UserId].Value.ToString());
                if (userId > 0)
                {
                    var dr_1 = rgwListIncome.Rows[i - 1];
                    var rootId = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootId].Value == null
                        ? 0
                        : int.Parse(
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootId].Value.ToString());
                    var rootId_1 = dr_1.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootId].Value ==
                                   null
                        ? 0
                        : int.Parse(
                            dr_1.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootId].Value.ToString());
                    indexRow++;

                    if ((i < rgwListIncome.RowCount - 1) && (i > 1))
                    {
                        deparmentId = rootId;
                        departmentIdBefore = rootId_1;
                    }

                    if (deparmentId != departmentIdBefore)
                    {
                        oWorkSheet.Cells[previousIndexRow, 5] =
                            TotalThangLamViec.ToString(StringFormat.FormatCoefficient3Digit);
                        oWorkSheet.Cells[previousIndexRow, 6] = TotalTienThuong.ToString("#,###");
                        oWorkSheet.Cells[previousIndexRow, 7] = TotalThueTNCN.ToString("#,###");
                        oWorkSheet.Cells[previousIndexRow, 8] = TotalThucLinh.ToString("#,###");

                        previousIndexRow = indexRow;


                        TotalThangLamViec = 0;
                        TotalTienThuong = 0;
                        TotalThueTNCN = 0;
                        TotalThucLinh = 0;


                        rangeDept = oWorkSheet.Range["A" + indexRow, "C" + indexRow];
                        rangeDept.Merge(Type.Missing);
                        oWorkSheet.Cells[indexRow, 1] =
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootName].Value == null
                                ? string.Empty
                                : dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_RootName].Value
                                    .ToString().ToUpper();
                        rangeDept = oWorkSheet.Range["A" + indexRow, "T" + indexRow];
                        rangeDept.Font.Bold = true;
                        indexRow++;
                    }


                    /// inserting order number
                    oWorkSheet.Cells[indexRow, 1] = orderNumber++;

                    UserId = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_UserId].Value == null
                        ? 0
                        : Convert.ToInt32(
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_UserId].Value.ToString());
                    oWorkSheet.Cells[indexRow, 2] = "'" + UserId.ToString(StringFormat.FormatUserId);
                    oWorkSheet.Cells[indexRow, 3] =
                        dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_FullName].Value == null
                            ? string.Empty
                            : dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_FullName].Value.ToString();
                    oWorkSheet.Cells[indexRow, 4] =
                        dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_PositionName].Value == null
                            ? string.Empty
                            : dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_PositionName].Value
                                .ToString();

                    ThangLamViec =
                        dr.Cells["MonthNumber_Total"].Value == null
                            ? 0
                            : Convert.ToDecimal(
                                dr.Cells["MonthNumber_Total"].Value);
                    oWorkSheet.Cells[indexRow, 5] = ThangLamViec.ToString(StringFormat.FormatCoefficient3Digit);
                    TotalThangLamViec = TotalThangLamViec + ThangLamViec;

                    TienThuong = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_BonusValue].Value ==
                                 null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_BonusValue].Value);
                    oWorkSheet.Cells[indexRow, 6] = TienThuong.ToString("#,###");
                    TotalTienThuong = TotalTienThuong + TienThuong;

                    ThueTNCN = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_ThueTamTinh].Value ==
                               null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_ThueTamTinh].Value);
                    oWorkSheet.Cells[indexRow, 7] = ThueTNCN.ToString("#,###");
                    TotalThueTNCN = TotalThueTNCN + ThueTNCN;

                    ThucLinh = dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_ThucLinh].Value == null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[BonusEmployeeConditionKeys.Field_BonusEmployeeCondition_ThucLinh].Value);
                    oWorkSheet.Cells[indexRow, 8] = ThucLinh.ToString("#,###");
                    TotalThucLinh = TotalThucLinh + ThucLinh;


                    TotalAllThangLamViec = TotalAllThangLamViec + ThangLamViec;

                    TotalAllTienThuong = TotalAllTienThuong + TienThuong;
                    TotalAllThueTNCN = TotalAllThueTNCN + ThueTNCN;

                    TotalAllThucLinh = TotalAllThucLinh + ThucLinh;
                }
                backgroundWorkerExport.ReportProgress(i + 1, fullName);
                Thread.Sleep(10);
            }


            oWorkSheet.Cells[previousIndexRow, 5] = TotalThangLamViec.ToString(StringFormat.FormatCoefficient3Digit);
            oWorkSheet.Cells[previousIndexRow, 6] = TotalTienThuong.ToString("#,###");
            oWorkSheet.Cells[previousIndexRow, 7] = TotalThueTNCN.ToString("#,###");
            oWorkSheet.Cells[previousIndexRow, 8] = TotalThucLinh.ToString("#,###");


            indexRow = indexRow + 1;
            oWorkSheet.Cells[indexRow, 1] = rgwListIncome.RowCount;
            oWorkSheet.Cells[indexRow, 2] = "TỔNG CỘNG";
            var totalRange = oWorkSheet.Range[oWorkSheet.Cells[indexRow, 2], oWorkSheet.Cells[indexRow, 3]];
            totalRange.Merge(Type.Missing);
            totalRange = oWorkSheet.Range[oWorkSheet.Cells[indexRow, 1], oWorkSheet.Cells[indexRow, 20]];
            totalRange.Font.Bold = true;


            oWorkSheet.Cells[indexRow, 5] = TotalAllThangLamViec.ToString(StringFormat.FormatCoefficient3Digit);
            oWorkSheet.Cells[indexRow, 6] = TotalAllTienThuong.ToString(StringFormat.FormatCoefficient3Digit);
            oWorkSheet.Cells[indexRow, 7] = TotalAllThueTNCN.ToString(StringFormat.FormatCoefficient3Digit);
            oWorkSheet.Cells[indexRow, 8] = TotalAllThucLinh.ToString("#,###");

            ((Range) oWorkSheet.Cells[indexRow, 5]).Font.Bold = true;
            var rangeAll = oWorkSheet.Range["A5", "I" + indexRow];
            rangeAll.Borders.LineStyle = Constants.xlSolid;
            rangeAll.Borders.Weight = 2;
            rangeAll.Font.Size = 8;
            rangeAll.Font.Name = "Times New Roman";


            indexRow = indexRow + 2;
            oWorkSheet.Cells[indexRow, 1] = "NGƯỜI LẬP BẢNG";
            oWorkSheet.Cells[indexRow, 3] = "P.TỔ CHỨC HÀNH CHÍNH";
            oWorkSheet.Cells[indexRow, 4] = "P.TÀI CHÍNH KẾ TOÁN";
            oWorkSheet.Cells[indexRow, 7] = "TỔNG GIÁM DỐC";
            var rangeFooter1 = oWorkSheet.Range[oWorkSheet.Cells[indexRow, 1], oWorkSheet.Cells[indexRow, 9]];
            rangeFooter1.Font.Name = "Times New Roman";
            rangeFooter1.Font.Size = 9;
            rangeFooter1.Font.Bold = true;
        }

        private static void CreateHeaderAndTitleForLeTet(ref _Worksheet oWorkSheet, ref int initTitleIndex,
            DateTime date, int bonusYear)
        {
            var rangeHeader2 = oWorkSheet.Range["A1", "D1"];
            rangeHeader2.Merge(Type.Missing);
            oWorkSheet.Cells[1, 1] = HRMUtil.Constants.SAGS_NAME;
            rangeHeader2.Font.Size = 12;
            rangeHeader2.Font.Bold = true;
            rangeHeader2.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            var rangeHeader3 = oWorkSheet.Range["A3", "I3"];
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[3, 1] = "BẢNG TIỀN THƯỞNG LỄ, TẾT " + bonusYear + " THỰC CHI NGÀY " +
                                     FormatDate.FormatVNDate(date);
            rangeHeader3.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader3.Font.Size = 15;
            rangeHeader3.Font.Bold = true;
            rangeHeader3.HorizontalAlignment = XlHAlign.xlHAlignCenter;


            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            oWorkSheet.Cells[initTitleIndex, 2] = "Mã NV";
            oWorkSheet.Cells[initTitleIndex, 3] = "Họ và tên";
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 30;
            oWorkSheet.Cells[initTitleIndex, 4] = "Chức danh";
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).ColumnWidth = 15;
            oWorkSheet.Cells[initTitleIndex, 5] = "Số tháng làm việc";

            oWorkSheet.Cells[initTitleIndex, 6] = "Tiền thưởng";
            oWorkSheet.Cells[initTitleIndex, 7] = "Thuế TNCN";

            oWorkSheet.Cells[initTitleIndex, 8] = "Thực lĩnh";

            /// format title
            var oTitleRange = oWorkSheet.Range["A5", "I5"];
            oTitleRange.Font.Bold = true;
            oTitleRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        }
    }
}