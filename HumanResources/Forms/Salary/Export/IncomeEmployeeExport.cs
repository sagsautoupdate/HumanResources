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

namespace HumanResources.Forms.Salary.Export
{
    public class IncomeEmployeeExport
    {
        public static _Application ExportSalaryForBank(string fileName, RadGridView rgwListIncome, DateTime dataDate,
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
            var initTitleIndex = 7;

            var indexRow = initTitleIndex + 1;
            var indexRowCash = initTitleIndex + 1;
            var orderNumber = 1;
            var orderNumberCash = 1;
            CreateHeaderAndTitleForAccount(ref oWorkSheet, ref initTitleIndex, date);
            CreateHeaderAndTitleForCash(ref oWorkSheetCash, ref initTitleIndex, date);


            var dr = rgwListIncome.Rows[0];


            var rangeDept = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
            rangeDept.Merge(Type.Missing);
            oWorkSheet.Cells[indexRow, 1] = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_RootName].Value == null
                ? string.Empty
                : dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_RootName].Value.ToString().ToUpper();
            rangeDept.Font.Bold = true;

            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;
            oWorkSheet.Cells[indexRow, 2] = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_FullName2].Value == null
                ? string.Empty
                : dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_FullName2].Value.ToString();
            oWorkSheet.Cells[indexRow, 3] = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_AccountNo].Value == null
                ? string.Empty
                : dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_AccountNo]
                    .Value
                    .ToString();
            var Account = oWorkSheet.get_Range("C" + indexRow, "C" + 3);
            Account.NumberFormat = "@";

            oWorkSheet.Cells[indexRow, 4] = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_CardNo].Value == null
                ? string.Empty
                : dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_CardNo]
                    .Value
                    .ToString();
            var Card = oWorkSheet.get_Range("D" + indexRow, "D" + 4);
            Card.NumberFormat = "@";

            var oThucLinh = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_ThucLinh].Value == null
                ? 0
                : Convert.ToDecimal(dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_ThucLinh].Value.ToString());
            oWorkSheet.Cells[indexRow, 5] = oThucLinh.ToString("#,###");
            oWorkSheet.Cells[indexRow, 6] = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_FullName].Value == null
                ? string.Empty
                : dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_FullName].Value.ToString();
            var rIdCard = oWorkSheet.get_Range("G" + indexRow, "G" + 7);
            rIdCard.NumberFormat = "@";
            rIdCard.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            oWorkSheet.Cells[indexRow, 7] = dr.Cells["IdCard"].Value == null
                ? string.Empty
                : dr.Cells["IdCard"].Value.ToString();
            totalAllRealIncome = totalAllRealIncome + oThucLinh;


            for (var i = 1; i < rgwListIncome.RowCount; i++)
            {
                dr = rgwListIncome.Rows[i];

                var userId = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_UserId].Value == null
                    ? 0
                    : Convert.ToInt32(dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_UserId].Value.ToString());
                if (userId > 0)
                {
                    var accountNo = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_AccountNo].Value == null
                        ? string.Empty
                        : dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_AccountNo].Value.ToString();
                    var realIncome = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_ThucLinh].Value == null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_ThucLinh].Value.ToString());
                    if (accountNo != string.Empty && accountNo.Trim().Length > 0 && realIncome > 0)
                    {
                        var dr_1 = rgwListIncome.Rows[i - 1];
                        var rootId = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_RootId].Value == null
                            ? 0
                            : int.Parse(dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_RootId].Value.ToString());
                        var rootId_1 = dr_1.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_RootId].Value == null
                            ? 0
                            : int.Parse(dr_1.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_RootId].Value.ToString());

                        indexRow++;

                        if (i < rgwListIncome.RowCount - 1 && i > 1)
                        {
                            deparmentId = rootId;
                            departmentIdBefore = rootId_1;
                        }

                        if (deparmentId != departmentIdBefore)
                        {
                            rangeDept = oWorkSheet.get_Range("A" + indexRow, "E" + indexRow);
                            rangeDept.Merge(Type.Missing);
                            oWorkSheet.Cells[indexRow, 1] =
                                dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_RootName].Value == null
                                    ? string.Empty
                                    : dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_RootName]
                                        .Value.ToString()
                                        .ToUpper();
                            rangeDept.Font.Bold = true;
                            indexRow++;
                        }

                        /// inserting order number
                        oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                        oWorkSheet.Cells[indexRow, 2] =
                            dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_FullName2].Value == null
                                ? string.Empty
                                : dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_FullName2].Value.ToString();
                        oWorkSheet.Cells[indexRow, 3] = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_AccountNo]
                                                            .Value == null
                            ? string.Empty
                            : dr.Cells[
                                    IncomeEmployeesKeys.Field_IncomeEmployees_AccountNo]
                                .Value.ToString();
                        var Account1 = oWorkSheet.get_Range("C" + indexRow, "C" + 3);
                        Account1.NumberFormat = "@";

                        oWorkSheet.Cells[indexRow, 4] = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_CardNo]
                                                            .Value == null
                            ? string.Empty
                            : dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_CardNo]
                                .Value.ToString();
                        var Card1 = oWorkSheet.get_Range("D" + indexRow, "D" + 4);
                        Card1.NumberFormat = "@";

                        oWorkSheet.Cells[indexRow, 5] = realIncome.ToString("#,###");
                        oWorkSheet.Cells[indexRow, 6] =
                            dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_FullName].Value == null
                                ? string.Empty
                                : dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_FullName].Value.ToString();
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
                            dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_FullName2].Value == null
                                ? string.Empty
                                : dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_FullName2].Value.ToString();
                        oWorkSheetCash.Cells[indexRowCash, 3] =
                            dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_RootName].Value == null
                                ? string.Empty
                                : dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_RootName].Value.ToString();
                        oWorkSheetCash.Cells[indexRowCash, 4] = realIncome.ToString("#,###");
                        oWorkSheetCash.Cells[indexRowCash, 5] =
                            dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_FullName].Value == null
                                ? string.Empty
                                : dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_FullName].Value.ToString();
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
            oWorkSheet.Cells[indexRow, 5] = totalAllRealIncome.ToString("#,###");
            ((Range) oWorkSheet.Cells[indexRow, 5]).Font.Bold = true;
            var rangeAll = oWorkSheet.get_Range("A7", "G" + indexRow);
            rangeAll.Borders.LineStyle = Constants.xlSolid;
            rangeAll.Borders.Weight = 2;
            rangeAll.Font.Size = 12;


            oWorkSheetCash.Cells[indexRowCash, 1] = "TỔNG CỘNG";
            var totalRangeCash =
                oWorkSheetCash.Range[oWorkSheetCash.Cells[indexRowCash, 1], oWorkSheetCash.Cells[indexRowCash, 2]];
            totalRangeCash.Merge(Type.Missing);
            totalRangeCash.Font.Bold = true;
            totalRangeCash.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            oWorkSheetCash.Cells[indexRowCash, 4] = totalAllRealIncomeCash.ToString("#,###");
            ((Range) oWorkSheetCash.Cells[indexRowCash, 4]).Font.Bold = true;
            var rangeAllCash = oWorkSheetCash.get_Range("A7", "G" + indexRowCash);
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
            //var rangeHeader1 = oWorkSheet.get_Range("A1", "C1");
            //rangeHeader1.Merge(Type.Missing);
            //oWorkSheet.Cells[1, 1] = HRMUtil.Constants.ACV_NAME;
            //rangeHeader1.Font.Size = 12;

            var rangeHeader2 = oWorkSheet.get_Range("A1", "C1");
            rangeHeader2.Merge(Type.Missing);
            oWorkSheet.Cells[1, 1] = HRMUtil.Constants.SAGS_NAME;
            rangeHeader2.Font.Size = 12;
            rangeHeader2.Font.Bold = true;

            var rangeHeader3 = oWorkSheet.get_Range("A4", "E4");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "DANH SÁCH CB.CNV NHẬN LƯƠNG QUA THẺ ATM";
            rangeHeader3.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader3.Font.Size = 15;
            rangeHeader3.Font.Bold = true;

            var rangeHeader4 = oWorkSheet.get_Range("A5", "E5");
            rangeHeader4.Merge(Type.Missing);
            oWorkSheet.Cells[5, 1] = "THÁNG " + date.Month + " / " + date.Year;
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
            ((Range) oWorkSheet.Cells[initTitleIndex, 7]).ColumnWidth = 17;
            /// format title
            var oTitleRange = oWorkSheet.Range[oWorkSheet.Cells[initTitleIndex, 1],
                oWorkSheet.Cells[initTitleIndex, 5]];
            oTitleRange.Font.Bold = true;

            oTitleRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        }

        private static void CreateHeaderAndTitleForCash(ref _Worksheet oWorkSheetCash, ref int initTitleIndex,
            DateTime date)
        {
            //var rangeHeader1 = oWorkSheetCash.get_Range("A1", "C1");
            //rangeHeader1.Merge(Type.Missing);
            //oWorkSheetCash.Cells[1, 1] = HRMUtil.Constants.ACV_NAME;
            //rangeHeader1.Font.Size = 12;

            var rangeHeader2 = oWorkSheetCash.get_Range("A1", "C1");
            rangeHeader2.Merge(Type.Missing);
            oWorkSheetCash.Cells[2, 1] = HRMUtil.Constants.SAGS_NAME;
            rangeHeader2.Font.Size = 15;
            rangeHeader2.Font.Bold = true;

            var rangeHeader3 = oWorkSheetCash.get_Range("A4", "E4");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheetCash.Cells[4, 1] = "DANH SÁCH CB.CNV NHẬN LƯƠNG QUA TIỀN MẶT";
            rangeHeader3.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader3.Font.Size = 15;
            rangeHeader3.Font.Bold = true;

            var rangeHeader4 = oWorkSheetCash.get_Range("A5", "E5");
            rangeHeader4.Merge(Type.Missing);
            oWorkSheetCash.Cells[5, 1] = "THÁNG " + date.Month + " / " + date.Year;
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


        public static _Application ExportSalaryForApproval(RadGridView rgwListIncome, DateTime dataDate,
            string fullName,
            BackgroundWorker backgroundWorkerExport)
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

                    oWorkSheet.Name = "TỔNG HỢP BẢNG LƯƠNG";

                    InsertDataToWorkSheetForApproval(ref oWorkSheet, rgwListIncome, dataDate, fullName,
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

        private static void InsertDataToWorkSheetForApproval(ref _Worksheet oWorkSheet, RadGridView rgwListIncome,
            DateTime date, string fullName, BackgroundWorker backgroundWorkerExport)
        {
            var deparmentId = 0;
            var departmentIdBefore = 0;

            var initTitleIndex = 7;

            var indexRow = initTitleIndex + 1;
            var orderNumber = 1;
            var previousIndexRow = 0;
            var UserId = 0;


            decimal Luong = 0;
            decimal TNTHieuQuaCongViec = 0;
            decimal PCCV = 0;
            decimal PCTN = 0;
            decimal TienAnGiuaCa = 0;
            decimal BoSungLuong = 0;
            decimal TienThemGio = 0;
            decimal TienLamDem = 0;
            decimal TotalIncome = 0;
            decimal BHXH = 0;
            decimal BHYT = 0;
            decimal BHTN = 0;
            decimal DoanPhiCD = 0;
            decimal ThueThuNhap = 0;
            decimal TruocThucLinh = 0;
            decimal DongGop = 0;
            decimal ThucLinh = 0;


            decimal TotalLuong = 0;
            decimal TotalTNTHieuQuaCongViec = 0;

            decimal TotalPCCV = 0;
            decimal TotalPCTN = 0;
            decimal TotalTienAnGiuaCa = 0;
            decimal TotalBoSungLuong = 0;
            decimal TotalTienThemGio = 0;
            decimal TotalTienLamDem = 0;
            decimal TotalTotalIncome = 0;
            decimal TotalBHXH = 0;
            decimal TotalBHYT = 0;
            decimal TotalBHTN = 0;
            decimal TotalDoanPhiCD = 0;
            decimal TotalThueThuNhap = 0;
            decimal TotalTruocThucLinh = 0;
            decimal TotalDongGop = 0;
            decimal TotalThucLinh = 0;


            decimal TotalAllLuong = 0;
            decimal TotalAllTNTHieuQuaCongViec = 0;

            decimal TotalAllPCCV = 0;
            decimal TotalAllPCTN = 0;
            decimal TotalAllTienAnGiuaCa = 0;
            decimal TotalAllBoSungLuong = 0;
            decimal TotalAllTienThemGio = 0;
            decimal TotalAllTienLamDem = 0;
            decimal TotalAllTotalIncome = 0;
            decimal TotalAllBHXH = 0;
            decimal TotalAllBHYT = 0;
            decimal TotalAllBHTN = 0;
            decimal TotalAllDoanPhiCD = 0;
            decimal TotalAllThueThuNhap = 0;
            decimal TotalAllTruocThucLinh = 0;
            decimal TotalAllDongGop = 0;
            decimal TotalAllThucLinh = 0;

            CreateHeaderAndTitle(ref oWorkSheet, ref initTitleIndex, date);


            var dr = rgwListIncome.Rows[0];

            previousIndexRow = indexRow;
            var rangeDept = oWorkSheet.get_Range("A" + indexRow, "C" + indexRow);
            rangeDept.Merge(Type.Missing);
            oWorkSheet.Cells[indexRow, 1] = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_RootName].Value == null
                ? string.Empty
                : dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_RootName].Value.ToString().ToUpper();
            rangeDept = oWorkSheet.get_Range("A" + indexRow, "T" + indexRow);
            rangeDept.Font.Bold = true;

            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;

            UserId = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_UserId].Value == null
                ? 0
                : Convert.ToInt32(dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_UserId].Value.ToString());
            oWorkSheet.Cells[indexRow, 2] = "'" + UserId.ToString(StringFormat.FormatUserId);
            oWorkSheet.Cells[indexRow, 3] = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_FullName].Value == null
                ? string.Empty
                : dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_FullName].Value.ToString();


            Luong = dr.Cells["Luong"].Value == null ? 0 : Convert.ToDecimal(dr.Cells["Luong"].Value.ToString());
            oWorkSheet.Cells[indexRow, 4] = Luong.ToString("#,###");
            TotalLuong = TotalLuong + Luong;

            TNTHieuQuaCongViec = dr.Cells["TNTHieuQuaCongViec"].Value == null
                ? 0
                : Convert.ToDecimal(dr.Cells["TNTHieuQuaCongViec"].Value.ToString());
            oWorkSheet.Cells[indexRow, 5] = TNTHieuQuaCongViec.ToString("#,###");
            TotalTNTHieuQuaCongViec = TotalTNTHieuQuaCongViec + TNTHieuQuaCongViec;

            PCCV = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_PCCV].Value == null
                ? 0
                : Convert.ToDecimal(dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_PCCV].Value.ToString());
            oWorkSheet.Cells[indexRow, 6] = PCCV.ToString("#,###");
            TotalPCCV = TotalPCCV + PCCV;

            PCTN = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_PCTN].Value == null
                ? 0
                : Convert.ToDecimal(dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_PCTN].Value.ToString());
            oWorkSheet.Cells[indexRow, 7] = PCTN.ToString("#,###");
            TotalPCTN = TotalPCTN + PCTN;

            TienAnGiuaCa = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_TienAnGiuaCa].Value == null
                ? 0
                : Convert.ToDecimal(dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_TienAnGiuaCa].Value.ToString());
            oWorkSheet.Cells[indexRow, 8] = TienAnGiuaCa.ToString("#,###");
            TotalTienAnGiuaCa = TotalTienAnGiuaCa + TienAnGiuaCa;

            BoSungLuong = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_BoSungLuong].Value == null
                ? 0
                : Convert.ToDecimal(dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_BoSungLuong].Value.ToString());
            oWorkSheet.Cells[indexRow, 9] = BoSungLuong.ToString("#,###");
            TotalBoSungLuong = TotalBoSungLuong + BoSungLuong;

            TienThemGio = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_TienThemGio].Value == null
                ? 0
                : Convert.ToDecimal(dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_TienThemGio].Value.ToString());
            oWorkSheet.Cells[indexRow, 10] = TienThemGio.ToString("#,###");
            TotalTienThemGio = TotalTienThemGio + TienThemGio;

            TienLamDem = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_TienLamDem].Value == null
                ? 0
                : Convert.ToDecimal(dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_TienLamDem].Value.ToString());
            oWorkSheet.Cells[indexRow, 11] = TienLamDem.ToString("#,###");
            TotalTienLamDem = TotalTienLamDem + TienLamDem;

            TotalIncome = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_TotalIncome].Value == null
                ? 0
                : Convert.ToDecimal(dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_TotalIncome].Value.ToString());
            oWorkSheet.Cells[indexRow, 12] = TotalIncome.ToString("#,###");
            TotalTotalIncome = TotalTotalIncome + TotalIncome;

            BHXH = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_BHXH].Value == null
                ? 0
                : Convert.ToDecimal(dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_BHXH].Value.ToString());
            oWorkSheet.Cells[indexRow, 13] = BHXH.ToString("#,###");
            TotalBHXH = TotalBHXH + BHXH;

            BHYT = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_BHYT].Value == null
                ? 0
                : Convert.ToDecimal(dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_BHYT].Value.ToString());
            oWorkSheet.Cells[indexRow, 14] = BHYT.ToString("#,###");
            TotalBHYT = TotalBHYT + BHYT;

            BHTN = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_BHTN].Value == null
                ? 0
                : Convert.ToDecimal(dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_BHTN].Value.ToString());
            oWorkSheet.Cells[indexRow, 15] = BHTN.ToString("#,###");
            TotalBHTN = TotalBHTN + BHTN;

            DoanPhiCD = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_DoanPhiCD].Value == null
                ? 0
                : Convert.ToDecimal(dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_DoanPhiCD].Value.ToString());
            oWorkSheet.Cells[indexRow, 16] = DoanPhiCD.ToString("#,###");
            TotalDoanPhiCD = TotalDoanPhiCD + DoanPhiCD;

            ThueThuNhap = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_ThueThuNhap].Value == null
                ? 0
                : Convert.ToDecimal(dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_ThueThuNhap].Value.ToString());
            oWorkSheet.Cells[indexRow, 17] = ThueThuNhap.ToString("#,###");
            TotalThueThuNhap = TotalThueThuNhap + ThueThuNhap;

            TruocThucLinh = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_TruocThucLinh].Value == null
                ? 0
                : Convert.ToDecimal(dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_TruocThucLinh].Value.ToString());
            oWorkSheet.Cells[indexRow, 18] = TruocThucLinh.ToString("#,###");
            TotalTruocThucLinh = TotalTruocThucLinh + TruocThucLinh;

            DongGop = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_DongGop].Value == null
                ? 0
                : Convert.ToDecimal(dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_DongGop].Value.ToString());
            oWorkSheet.Cells[indexRow, 19] = DongGop.ToString("#,###");
            TotalDongGop = TotalDongGop + DongGop;

            ThucLinh = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_ThucLinh].Value == null
                ? 0
                : Convert.ToDecimal(dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_ThucLinh].Value.ToString());
            oWorkSheet.Cells[indexRow, 20] = ThucLinh.ToString("#,###");
            TotalThucLinh = TotalThucLinh + ThucLinh;

            TotalAllLuong = TotalAllLuong + Luong;
            TotalAllTNTHieuQuaCongViec = TotalAllTNTHieuQuaCongViec + TNTHieuQuaCongViec;
            TotalAllPCCV = TotalAllPCCV + PCCV;
            TotalAllPCTN = TotalAllPCTN + PCTN;
            TotalAllTienAnGiuaCa = TotalAllTienAnGiuaCa + TienAnGiuaCa;
            TotalAllBoSungLuong = TotalAllBoSungLuong + BoSungLuong;
            TotalAllTienThemGio = TotalAllTienThemGio + TienThemGio;
            TotalAllTienLamDem = TotalAllTienLamDem + TienLamDem;
            TotalAllTotalIncome = TotalAllTotalIncome + TotalIncome;
            TotalAllBHXH = TotalAllBHXH + BHXH;
            TotalAllBHYT = TotalAllBHYT + BHYT;
            TotalAllBHTN = TotalAllBHTN + BHTN;
            TotalAllDoanPhiCD = TotalAllDoanPhiCD + DoanPhiCD;
            TotalAllThueThuNhap = TotalAllThueThuNhap + ThueThuNhap;
            TotalAllTruocThucLinh = TotalAllTruocThucLinh + TruocThucLinh;
            TotalAllDongGop = TotalAllDongGop + DongGop;
            TotalAllThucLinh = TotalAllThucLinh + ThucLinh;


            Luong = 0;
            TNTHieuQuaCongViec = 0;
            PCCV = 0;
            PCTN = 0;
            TienAnGiuaCa = 0;
            BoSungLuong = 0;
            TienThemGio = 0;
            TienLamDem = 0;
            TotalIncome = 0;
            BHXH = 0;
            BHYT = 0;
            BHTN = 0;
            DoanPhiCD = 0;
            ThueThuNhap = 0;
            TruocThucLinh = 0;
            DongGop = 0;
            ThucLinh = 0;
            for (var i = 1; i < rgwListIncome.RowCount; i++)
            {
                dr = rgwListIncome.Rows[i];

                var userId = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_UserId].Value == null
                    ? 0
                    : Convert.ToInt32(dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_UserId].Value.ToString());
                if (userId > 0)
                {
                    var dr_1 = rgwListIncome.Rows[i - 1];
                    var rootId = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_RootId].Value == null
                        ? 0
                        : int.Parse(dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_RootId].Value.ToString());
                    var rootId_1 = dr_1.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_RootId].Value == null
                        ? 0
                        : int.Parse(dr_1.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_RootId].Value.ToString());

                    indexRow++;

                    if (i < rgwListIncome.RowCount - 1 && i > 1)
                    {
                        deparmentId = rootId;
                        departmentIdBefore = rootId_1;
                    }

                    if (deparmentId != departmentIdBefore)
                    {
                        oWorkSheet.Cells[previousIndexRow, 4] = TotalLuong.ToString("#,###");
                        oWorkSheet.Cells[previousIndexRow, 5] = TotalTNTHieuQuaCongViec.ToString("#,###");

                        oWorkSheet.Cells[previousIndexRow, 6] = TotalPCCV.ToString("#,###");
                        oWorkSheet.Cells[previousIndexRow, 7] = TotalPCTN.ToString("#,###");
                        oWorkSheet.Cells[previousIndexRow, 8] = TotalTienAnGiuaCa.ToString("#,###");
                        oWorkSheet.Cells[previousIndexRow, 9] = TotalBoSungLuong.ToString("#,###");
                        oWorkSheet.Cells[previousIndexRow, 10] = TotalTienThemGio.ToString("#,###");
                        oWorkSheet.Cells[previousIndexRow, 11] = TotalTienLamDem.ToString("#,###");

                        oWorkSheet.Cells[previousIndexRow, 12] = TotalTotalIncome.ToString("#,###");

                        oWorkSheet.Cells[previousIndexRow, 13] = TotalBHXH.ToString("#,###");
                        oWorkSheet.Cells[previousIndexRow, 14] = TotalBHYT.ToString("#,###");
                        oWorkSheet.Cells[previousIndexRow, 15] = TotalBHTN.ToString("#,###");
                        oWorkSheet.Cells[previousIndexRow, 16] = TotalDoanPhiCD.ToString("#,###");
                        oWorkSheet.Cells[previousIndexRow, 17] = TotalThueThuNhap.ToString("#,###");
                        oWorkSheet.Cells[previousIndexRow, 18] = TotalTruocThucLinh.ToString("#,###");
                        oWorkSheet.Cells[previousIndexRow, 19] = TotalDongGop.ToString("#,###");
                        oWorkSheet.Cells[previousIndexRow, 20] = TotalThucLinh.ToString("#,###");


                        previousIndexRow = indexRow;


                        TotalLuong = 0;
                        TotalTNTHieuQuaCongViec = 0;

                        TotalPCCV = 0;
                        TotalPCTN = 0;
                        TotalTienAnGiuaCa = 0;
                        TotalBoSungLuong = 0;
                        TotalTienThemGio = 0;
                        TotalTienLamDem = 0;
                        TotalTotalIncome = 0;
                        TotalBHXH = 0;
                        TotalBHYT = 0;
                        TotalBHTN = 0;
                        TotalDoanPhiCD = 0;
                        TotalThueThuNhap = 0;
                        TotalTruocThucLinh = 0;
                        TotalDongGop = 0;
                        TotalThucLinh = 0;

                        orderNumber = 1;
                        rangeDept = oWorkSheet.get_Range("A" + indexRow, "C" + indexRow);
                        rangeDept.Merge(Type.Missing);
                        oWorkSheet.Cells[indexRow, 1] =
                            dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_RootName].Value == null
                                ? string.Empty
                                : dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_RootName]
                                    .Value.ToString()
                                    .ToUpper();
                        rangeDept = oWorkSheet.get_Range("A" + indexRow, "T" + indexRow);
                        rangeDept.Font.Bold = true;
                        indexRow++;
                    }


                    /// inserting order number
                    oWorkSheet.Cells[indexRow, 1] = orderNumber++;
                    UserId = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_UserId].Value == null
                        ? 0
                        : Convert.ToInt32(dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_UserId].Value.ToString());
                    oWorkSheet.Cells[indexRow, 2] = "'" + UserId.ToString("00000#");
                    oWorkSheet.Cells[indexRow, 3] =
                        dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_FullName].Value == null
                            ? string.Empty
                            : dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_FullName].Value.ToString();


                    Luong = dr.Cells["Luong"].Value == null ? 0 : Convert.ToDecimal(dr.Cells["Luong"].Value.ToString());
                    oWorkSheet.Cells[indexRow, 4] = Luong.ToString("#,###");
                    TotalLuong = TotalLuong + Luong;

                    TNTHieuQuaCongViec = dr.Cells["TNTHieuQuaCongViec"].Value == null
                        ? 0
                        : Convert.ToDecimal(dr.Cells["TNTHieuQuaCongViec"].Value.ToString());
                    oWorkSheet.Cells[indexRow, 5] = TNTHieuQuaCongViec.ToString("#,###");
                    TotalTNTHieuQuaCongViec = TotalTNTHieuQuaCongViec + TNTHieuQuaCongViec;

                    PCCV = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_PCCV].Value == null
                        ? 0
                        : Convert.ToDecimal(dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_PCCV].Value.ToString());
                    oWorkSheet.Cells[indexRow, 6] = PCCV.ToString("#,###");
                    TotalPCCV = TotalPCCV + PCCV;

                    PCTN = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_PCTN].Value == null
                        ? 0
                        : Convert.ToDecimal(dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_PCTN].Value.ToString());
                    oWorkSheet.Cells[indexRow, 7] = PCTN.ToString("#,###");
                    TotalPCTN = TotalPCTN + PCTN;

                    TienAnGiuaCa = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_TienAnGiuaCa].Value == null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_TienAnGiuaCa].Value.ToString());
                    oWorkSheet.Cells[indexRow, 8] = TienAnGiuaCa.ToString("#,###");
                    TotalTienAnGiuaCa = TotalTienAnGiuaCa + TienAnGiuaCa;

                    BoSungLuong = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_BoSungLuong].Value == null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_BoSungLuong].Value.ToString());
                    oWorkSheet.Cells[indexRow, 9] = BoSungLuong.ToString("#,###");
                    TotalBoSungLuong = TotalBoSungLuong + BoSungLuong;

                    TienThemGio = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_TienThemGio].Value == null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_TienThemGio].Value.ToString());
                    oWorkSheet.Cells[indexRow, 10] = TienThemGio.ToString("#,###");
                    TotalTienThemGio = TotalTienThemGio + TienThemGio;

                    TienLamDem = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_TienLamDem].Value == null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_TienLamDem].Value.ToString());
                    oWorkSheet.Cells[indexRow, 11] = TienLamDem.ToString("#,###");
                    TotalTienLamDem = TotalTienLamDem + TienLamDem;

                    TotalIncome = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_TotalIncome].Value == null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_TotalIncome].Value.ToString());
                    oWorkSheet.Cells[indexRow, 12] = TotalIncome.ToString("#,###");
                    TotalTotalIncome = TotalTotalIncome + TotalIncome;

                    BHXH = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_BHXH].Value == null
                        ? 0
                        : Convert.ToDecimal(dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_BHXH].Value.ToString());
                    oWorkSheet.Cells[indexRow, 13] = BHXH.ToString("#,###");
                    TotalBHXH = TotalBHXH + BHXH;

                    BHYT = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_BHYT].Value == null
                        ? 0
                        : Convert.ToDecimal(dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_BHYT].Value.ToString());
                    oWorkSheet.Cells[indexRow, 14] = BHYT.ToString("#,###");
                    TotalBHYT = TotalBHYT + BHYT;

                    BHTN = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_BHTN].Value == null
                        ? 0
                        : Convert.ToDecimal(dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_BHTN].Value.ToString());
                    oWorkSheet.Cells[indexRow, 15] = BHTN.ToString("#,###");
                    TotalBHTN = TotalBHTN + BHTN;

                    DoanPhiCD = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_DoanPhiCD].Value == null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_DoanPhiCD].Value.ToString());
                    oWorkSheet.Cells[indexRow, 16] = DoanPhiCD.ToString("#,###");
                    TotalDoanPhiCD = TotalDoanPhiCD + DoanPhiCD;

                    ThueThuNhap = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_ThueThuNhap].Value == null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_ThueThuNhap].Value.ToString());
                    oWorkSheet.Cells[indexRow, 17] = ThueThuNhap.ToString("#,###");
                    TotalThueThuNhap = TotalThueThuNhap + ThueThuNhap;


                    TruocThucLinh = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_TruocThucLinh].Value == null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_TruocThucLinh].Value.ToString());
                    oWorkSheet.Cells[indexRow, 18] = TruocThucLinh.ToString("#,###");
                    TotalTruocThucLinh = TotalTruocThucLinh + TruocThucLinh;

                    DongGop = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_DongGop].Value == null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_DongGop].Value.ToString());
                    oWorkSheet.Cells[indexRow, 19] = DongGop.ToString("#,###");
                    TotalDongGop = TotalDongGop + DongGop;

                    ThucLinh = dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_ThucLinh].Value == null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[IncomeEmployeesKeys.Field_IncomeEmployees_ThucLinh].Value.ToString());
                    oWorkSheet.Cells[indexRow, 20] = ThucLinh.ToString("#,###");
                    TotalThucLinh = TotalThucLinh + ThucLinh;

                    TotalAllLuong = TotalAllLuong + Luong;
                    TotalAllTNTHieuQuaCongViec = TotalAllTNTHieuQuaCongViec + TNTHieuQuaCongViec;
                    TotalAllPCCV = TotalAllPCCV + PCCV;
                    TotalAllPCTN = TotalAllPCTN + PCTN;
                    TotalAllTienAnGiuaCa = TotalAllTienAnGiuaCa + TienAnGiuaCa;
                    TotalAllBoSungLuong = TotalAllBoSungLuong + BoSungLuong;
                    TotalAllTienThemGio = TotalAllTienThemGio + TienThemGio;
                    TotalAllTienLamDem = TotalAllTienLamDem + TienLamDem;
                    TotalAllTotalIncome = TotalAllTotalIncome + TotalIncome;
                    TotalAllBHXH = TotalAllBHXH + BHXH;
                    TotalAllBHYT = TotalAllBHYT + BHYT;
                    TotalAllBHTN = TotalAllBHTN + BHTN;
                    TotalAllDoanPhiCD = TotalAllDoanPhiCD + DoanPhiCD;
                    TotalAllThueThuNhap = TotalAllThueThuNhap + ThueThuNhap;
                    TotalAllTruocThucLinh = TotalAllTruocThucLinh + TruocThucLinh;
                    TotalAllDongGop = TotalAllDongGop + DongGop;
                    TotalAllThucLinh = TotalAllThucLinh + ThucLinh;
                }
                backgroundWorkerExport.ReportProgress(i + 1, fullName);
                Thread.Sleep(10);
            }


            oWorkSheet.Cells[previousIndexRow, 4] = TotalLuong.ToString("#,###");
            oWorkSheet.Cells[previousIndexRow, 5] = TotalTNTHieuQuaCongViec.ToString("#,###");

            oWorkSheet.Cells[previousIndexRow, 6] = TotalPCCV.ToString("#,###");
            oWorkSheet.Cells[previousIndexRow, 7] = TotalPCTN.ToString("#,###");
            oWorkSheet.Cells[previousIndexRow, 8] = TotalTienAnGiuaCa.ToString("#,###");
            oWorkSheet.Cells[previousIndexRow, 9] = TotalBoSungLuong.ToString("#,###");
            oWorkSheet.Cells[previousIndexRow, 10] = TotalTienThemGio.ToString("#,###");
            oWorkSheet.Cells[previousIndexRow, 11] = TotalTienLamDem.ToString("#,###");

            oWorkSheet.Cells[previousIndexRow, 12] = TotalTotalIncome.ToString("#,###");

            oWorkSheet.Cells[previousIndexRow, 13] = TotalBHXH.ToString("#,###");
            oWorkSheet.Cells[previousIndexRow, 14] = TotalBHYT.ToString("#,###");
            oWorkSheet.Cells[previousIndexRow, 15] = TotalBHTN.ToString("#,###");
            oWorkSheet.Cells[previousIndexRow, 16] = TotalDoanPhiCD.ToString("#,###");
            oWorkSheet.Cells[previousIndexRow, 17] = TotalThueThuNhap.ToString("#,###");

            oWorkSheet.Cells[previousIndexRow, 18] = TotalTruocThucLinh.ToString("#,###");
            oWorkSheet.Cells[previousIndexRow, 19] = TotalDongGop.ToString("#,###");
            oWorkSheet.Cells[previousIndexRow, 20] = TotalThucLinh.ToString("#,###");

            indexRow = indexRow + 1;
            oWorkSheet.Cells[indexRow, 1] = rgwListIncome.RowCount - 1;
            oWorkSheet.Cells[indexRow, 2] = "TỔNG CỘNG";
            var totalRange = oWorkSheet.Range[oWorkSheet.Cells[indexRow, 2], oWorkSheet.Cells[indexRow, 3]];
            totalRange.Merge(Type.Missing);
            totalRange = oWorkSheet.Range[oWorkSheet.Cells[indexRow, 1], oWorkSheet.Cells[indexRow, 20]];
            totalRange.Font.Bold = true;


            oWorkSheet.Cells[indexRow, 4] = TotalAllLuong.ToString("#,###");
            oWorkSheet.Cells[indexRow, 5] = TotalAllTNTHieuQuaCongViec.ToString("#,###");
            oWorkSheet.Cells[indexRow, 6] = TotalAllPCCV.ToString("#,###");
            oWorkSheet.Cells[indexRow, 7] = TotalAllPCTN.ToString("#,###");
            oWorkSheet.Cells[indexRow, 8] = TotalAllTienAnGiuaCa.ToString("#,###");
            oWorkSheet.Cells[indexRow, 9] = TotalAllBoSungLuong.ToString("#,###");
            oWorkSheet.Cells[indexRow, 10] = TotalAllTienThemGio.ToString("#,###");
            oWorkSheet.Cells[indexRow, 11] = TotalAllTienLamDem.ToString("#,###");
            oWorkSheet.Cells[indexRow, 12] = TotalAllTotalIncome.ToString("#,###");
            oWorkSheet.Cells[indexRow, 13] = TotalAllBHXH.ToString("#,###");
            oWorkSheet.Cells[indexRow, 14] = TotalAllBHYT.ToString("#,###");
            oWorkSheet.Cells[indexRow, 15] = TotalAllBHTN.ToString("#,###");
            oWorkSheet.Cells[indexRow, 16] = TotalAllDoanPhiCD.ToString("#,###");
            oWorkSheet.Cells[indexRow, 17] = TotalAllThueThuNhap.ToString("#,###");
            oWorkSheet.Cells[indexRow, 18] = TotalAllTruocThucLinh.ToString("#,###");
            oWorkSheet.Cells[indexRow, 19] = TotalAllDongGop.ToString("#,###");
            oWorkSheet.Cells[indexRow, 20] = TotalAllThucLinh.ToString("#,###");

            ((Range) oWorkSheet.Cells[indexRow, 5]).Font.Bold = true;

            var rangeAll = oWorkSheet.get_Range("A6", "T" + indexRow);
            rangeAll.Borders.LineStyle = Constants.xlSolid;
            rangeAll.Borders.Weight = 2;
            rangeAll.Font.Size = 8;
            rangeAll.Font.Name = "Times New Roman";


            indexRow = indexRow + 2;
            oWorkSheet.Cells[indexRow, 3] = "NGƯỜI LẬP BẢNG";
            oWorkSheet.Cells[indexRow, 6] = "P.TỔ CHỨC HÀNH CHÍNH";
            oWorkSheet.Cells[indexRow, 12] = "P.TÀI CHÍNH KẾ TOÁN";
            oWorkSheet.Cells[indexRow, 16] = "TỔNG GIÁM ĐỐC";
            var rangeFooter1 = oWorkSheet.Range[oWorkSheet.Cells[indexRow, 1], oWorkSheet.Cells[indexRow, 20]];
            rangeFooter1.Font.Name = "Times New Roman";
            rangeFooter1.Font.Size = 9;
            rangeFooter1.Font.Bold = true;
        }

        private static void CreateHeaderAndTitle(ref _Worksheet oWorkSheet, ref int initTitleIndex, DateTime date)
        {
            var rangeHeader2 = oWorkSheet.get_Range("A2", "D2");
            rangeHeader2.Merge(Type.Missing);
            oWorkSheet.Cells[2, 1] = HRMUtil.Constants.SAGS_NAME;
            rangeHeader2.Font.Size = 12;
            rangeHeader2.Font.Bold = true;
            rangeHeader2.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            var rangeHeader3 = oWorkSheet.get_Range("A4", "R4");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "BẢNG TIỀN LƯƠNG THÁNG " + date.Month + " NĂM " + date.Year;
            rangeHeader3.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader3.Font.Size = 15;
            rangeHeader3.Font.Bold = true;
            rangeHeader3.HorizontalAlignment = XlHAlign.xlHAlignCenter;


            oWorkSheet.Cells[initTitleIndex - 1, 6] = "Các khoản phục cấp";
            oWorkSheet.Cells[initTitleIndex - 1, 13] = "Các khoản phải nộp";


            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            oWorkSheet.Cells[initTitleIndex, 2] = "Mã NV";
            oWorkSheet.Cells[initTitleIndex, 3] = "Họ và tên";
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 30;
            oWorkSheet.Cells[initTitleIndex, 4] = "Lương";
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).ColumnWidth = 15;
            oWorkSheet.Cells[initTitleIndex, 5] = "Thu nhập theo hiệu quả công việc";
            ((Range) oWorkSheet.Cells[initTitleIndex, 5]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 6] = "PCCV";
            ((Range) oWorkSheet.Cells[initTitleIndex, 6]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 7] = "PCTN";
            ((Range) oWorkSheet.Cells[initTitleIndex, 7]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 8] = "Ăn giữa ca";
            ((Range) oWorkSheet.Cells[initTitleIndex, 8]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 9] = "Bổ sung lương";
            ((Range) oWorkSheet.Cells[initTitleIndex, 9]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 10] = "Tiền thêm giờ";
            ((Range) oWorkSheet.Cells[initTitleIndex, 10]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 11] = "Tiền làm đêm";
            ((Range) oWorkSheet.Cells[initTitleIndex, 11]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 12] = "Tổng";
            ((Range) oWorkSheet.Cells[initTitleIndex, 12]).ColumnWidth = 15;
            oWorkSheet.Cells[initTitleIndex, 13] = "BHXH";
            ((Range) oWorkSheet.Cells[initTitleIndex, 13]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 14] = "BHYT";
            ((Range) oWorkSheet.Cells[initTitleIndex, 14]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 15] = "BHTN";
            ((Range) oWorkSheet.Cells[initTitleIndex, 15]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 16] = "ĐPCĐ";
            ((Range) oWorkSheet.Cells[initTitleIndex, 16]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 17] = "TTN tạm tính";
            ((Range) oWorkSheet.Cells[initTitleIndex, 17]).ColumnWidth = 10;

            oWorkSheet.Cells[initTitleIndex, 18] = "Thực lĩnh";
            ((Range) oWorkSheet.Cells[initTitleIndex, 18]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 19] = "Đóng góp một ngày lương";
            ((Range) oWorkSheet.Cells[initTitleIndex, 18]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 20] = "Thực lĩnh cuối cùng";
            ((Range) oWorkSheet.Cells[initTitleIndex, 18]).ColumnWidth = 10;
            /// format title
            var oTitleRange = oWorkSheet.get_Range("A6", "T7");
            oTitleRange.Font.Bold = true;
            oTitleRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            oTitleRange = oWorkSheet.get_Range("F6", "G6");
            oTitleRange.Merge(Type.Missing);
            oTitleRange = oWorkSheet.get_Range("M6", "Q6");
            oTitleRange.Merge(Type.Missing);

            oTitleRange = oWorkSheet.get_Range("A6", "A7");
            oTitleRange.Merge(Type.Missing);
            oTitleRange = oWorkSheet.get_Range("B6", "B7");
            oTitleRange.Merge(Type.Missing);
            oTitleRange = oWorkSheet.get_Range("C6", "C7");
            oTitleRange.Merge(Type.Missing);
            oTitleRange = oWorkSheet.get_Range("D6", "D7");
            oTitleRange.Merge(Type.Missing);
            oTitleRange = oWorkSheet.get_Range("E6", "E7");
            oTitleRange.Merge(Type.Missing);
            oTitleRange = oWorkSheet.get_Range("H6", "H7");
            oTitleRange.Merge(Type.Missing);
            oTitleRange = oWorkSheet.get_Range("I6", "I7");
            oTitleRange.Merge(Type.Missing);
            oTitleRange = oWorkSheet.get_Range("J6", "J7");
            oTitleRange.Merge(Type.Missing);
            oTitleRange = oWorkSheet.get_Range("K6", "K7");
            oTitleRange.Merge(Type.Missing);
            oTitleRange = oWorkSheet.get_Range("L6", "L7");
            oTitleRange.Merge(Type.Missing);
            oTitleRange = oWorkSheet.get_Range("R6", "R7");
            oTitleRange.Merge(Type.Missing);
            oTitleRange = oWorkSheet.get_Range("S6", "S7");
            oTitleRange.Merge(Type.Missing);
            oTitleRange = oWorkSheet.get_Range("T6", "T7");
            oTitleRange.Merge(Type.Missing);
        }
    }
}