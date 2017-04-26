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

namespace HHumanResources.Forms.Workingday.Export
{
    public class CoefficientEmployeeFinalExport
    {
        public static _Application ExportCoefficientForApproval(string fileName, RadGridView rgwList, DateTime dataDate,
            string fullName, BackgroundWorker backgroundWorkerExport)
        {
            if (rgwList.RowCount > 0)
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

                    oWorkSheet.Name = "TỔNG HỢP BẢNG HỆ SỐ NGÀY CÔNG";

                    InsertDataToWorkSheetForApproval(ref oWorkSheet, rgwList, dataDate, fullName, backgroundWorkerExport);

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

        private static void InsertDataToWorkSheetForApproval(ref _Worksheet oWorkSheet, RadGridView rgwList,
            DateTime date, string fullName, BackgroundWorker backgroundWorkerExport)
        {
            var deparmentId = 0;
            var departmentIdBefore = 0;

            var initTitleIndex = 7;

            var indexRow = initTitleIndex + 1;
            var orderNumber = 1;
            var previousIndexRow = 0;
            var UserId = 0;

            decimal LCB = 0;
            decimal PCCV = 0;
            decimal PCTN = 0;
            decimal PCKV = 0;
            decimal HSLTNCD = 0;
            decimal LNS = 0;
            decimal K = 0;
            decimal X = 0;
            decimal OmCo = 0;
            decimal ThaiSan = 0;
            decimal TNLD = 0;
            decimal FDiDuong = 0;
            decimal Khac = 0;
            decimal LamThem = 0;
            decimal LamDem = 0;
            decimal DTNopThue = 0;
            decimal NguoiPThuoc = 0;

            decimal TotalLCB = 0;
            decimal TotalPCCV = 0;
            decimal TotalPCTN = 0;
            decimal TotalPCKV = 0;
            decimal TotalHSLTNCD = 0;
            decimal TotalLNS = 0;
            decimal TotalK = 0;
            decimal TotalX = 0;
            decimal TotalOmCo = 0;
            decimal TotalThaiSan = 0;
            decimal TotalTNLD = 0;
            decimal TotalFDiDuong = 0;
            decimal TotalKhac = 0;
            decimal TotalLamThem = 0;
            decimal TotalLamDem = 0;
            decimal TotalDTNopThue = 0;
            decimal TotalNguoiPThuoc = 0;


            decimal TotalAllLCB = 0;
            decimal TotalAllPCCV = 0;
            decimal TotalAllPCTN = 0;
            decimal TotalAllPCKV = 0;
            decimal TotalAllHSLTNCD = 0;
            decimal TotalAllLNS = 0;
            decimal TotalAllK = 0;
            decimal TotalAllX = 0;
            decimal TotalAllOmCo = 0;
            decimal TotalAllThaiSan = 0;
            decimal TotalAllTNLD = 0;
            decimal TotalAllFDiDuong = 0;
            decimal TotalAllKhac = 0;
            decimal TotalAllLamThem = 0;
            decimal TotalAllLamDem = 0;
            decimal TotalAllDTNopThue = 0;
            decimal TotalAllNguoiPThuoc = 0;

            CreateHeaderAndTitle(ref oWorkSheet, ref initTitleIndex, date);


            var dr = rgwList.Rows[0];

            previousIndexRow = indexRow;
            var rangeDept = oWorkSheet.get_Range("A" + indexRow, "C" + indexRow);
            rangeDept.Merge(Type.Missing);
            oWorkSheet.Cells[indexRow, 1] =
                dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_RootName].Value ==
                null
                    ? string.Empty
                    : dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_RootName]
                        .Value.ToString().ToUpper();
            rangeDept = oWorkSheet.get_Range("A" + indexRow, "R" + indexRow);
            rangeDept.Font.Bold = true;

            indexRow++;
            oWorkSheet.Cells[indexRow, 1] = orderNumber++;

            UserId =
                dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_UserId].Value ==
                null
                    ? 0
                    : Convert.ToInt32(
                        dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_UserId]
                            .Value.ToString());
            oWorkSheet.Cells[indexRow, 2] = "'" + UserId.ToString(StringFormat.FormatUserId);
            oWorkSheet.Cells[indexRow, 3] =
                dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_FullName].Value ==
                null
                    ? string.Empty
                    : dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_FullName]
                        .Value.ToString();
            oWorkSheet.Cells[indexRow, 4] =
                dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_PositionName].Value ==
                null
                    ? string.Empty
                    : dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_PositionName]
                        .Value.ToString();

            LCB = dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSLCB].Value ==
                  null
                ? 0
                : Convert.ToDecimal(
                    dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSLCB].Value
                        .ToString());
            oWorkSheet.Cells[indexRow, 5] = LCB == 0 ? "-" : LCB.ToString(StringFormat.FormatCoefficient);
            TotalLCB = TotalLCB + LCB;
            PCCV = dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSPCCV].Value ==
                   null
                ? 0
                : Convert.ToDecimal(
                    dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSPCCV].Value
                        .ToString());
            oWorkSheet.Cells[indexRow, 6] = PCCV == 0 ? "-" : PCCV.ToString(StringFormat.FormatCoefficient);
            TotalPCCV = TotalPCCV + PCCV;
            PCTN = dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSPCTN].Value ==
                   null
                ? 0
                : Convert.ToDecimal(
                    dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSPCTN].Value
                        .ToString());
            oWorkSheet.Cells[indexRow, 7] = PCTN == 0 ? "-" : PCTN.ToString(StringFormat.FormatCoefficient);
            TotalPCTN = TotalPCTN + PCTN;
            PCKV = dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSPCKV].Value ==
                   null
                ? 0
                : Convert.ToDecimal(
                    dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSPCKV].Value
                        .ToString());
            oWorkSheet.Cells[indexRow, 8] = PCKV == 0 ? "-" : PCKV.ToString(StringFormat.FormatCoefficient);
            TotalPCKV = TotalPCKV + PCKV;
            HSLTNCD =
                dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSLNSPCTN].Value ==
                null
                    ? 0
                    : Convert.ToDecimal(
                        dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSLNSPCTN]
                            .Value.ToString());
            oWorkSheet.Cells[indexRow, 9] = HSLTNCD == 0 ? "-" : HSLTNCD.ToString(StringFormat.FormatCoefficient);
            TotalHSLTNCD = TotalHSLTNCD + HSLTNCD;
            LNS = dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSLNS].Value ==
                  null
                ? 0
                : Convert.ToDecimal(
                    dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSLNS].Value
                        .ToString());
            oWorkSheet.Cells[indexRow, 10] = LNS == 0 ? "-" : LNS.ToString(StringFormat.FormatCoefficient);
            TotalLNS = TotalLNS + LNS;
            K = dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSK].Value == null
                ? 0
                : Convert.ToDecimal(
                    dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSK].Value
                        .ToString());
            oWorkSheet.Cells[indexRow, 11] = K == 0 ? "-" : K.ToString(StringFormat.FormatCoefficient);
            TotalK = TotalK + K;

            X = dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_X].Value == null
                ? 0
                : Convert.ToDecimal(
                    dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_X].Value
                        .ToString());
            oWorkSheet.Cells[indexRow, 12] = X == 0 ? "-" : X.ToString(StringFormat.FormatCoefficient);
            TotalX = TotalX + X;
            OmCo = dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_OmCo].Value ==
                   null
                ? 0
                : Convert.ToDecimal(
                    dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_OmCo].Value
                        .ToString());
            oWorkSheet.Cells[indexRow, 13] = OmCo == 0 ? "-" : OmCo.ToString(StringFormat.FormatCoefficient);
            TotalOmCo = TotalOmCo + OmCo;
            ThaiSan =
                dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_ThaiSan].Value ==
                null
                    ? 0
                    : Convert.ToDecimal(
                        dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_ThaiSan]
                            .Value.ToString());
            oWorkSheet.Cells[indexRow, 14] = ThaiSan == 0 ? "-" : ThaiSan.ToString(StringFormat.FormatCoefficient);
            TotalThaiSan = TotalThaiSan + ThaiSan;
            TNLD = dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_TNLD].Value ==
                   null
                ? 0
                : Convert.ToDecimal(
                    dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_TNLD].Value
                        .ToString());
            oWorkSheet.Cells[indexRow, 15] = TNLD == 0 ? "-" : TNLD.ToString(StringFormat.FormatCoefficient);
            TotalTNLD = TotalTNLD + TNLD;
            FDiDuong =
                dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_FDiDuong].Value ==
                null
                    ? 0
                    : Convert.ToDecimal(
                        dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_FDiDuong]
                            .Value.ToString());
            oWorkSheet.Cells[indexRow, 16] = FDiDuong == 0 ? "-" : FDiDuong.ToString(StringFormat.FormatCoefficient);
            TotalFDiDuong = TotalFDiDuong + FDiDuong;
            Khac = dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Khac].Value ==
                   null
                ? 0
                : Convert.ToDecimal(
                    dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Khac].Value
                        .ToString());
            oWorkSheet.Cells[indexRow, 17] = Khac == 0 ? "-" : Khac.ToString(StringFormat.FormatCoefficient);
            TotalKhac = TotalKhac + Khac;

            LamThem =
                dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamThem].Value ==
                null
                    ? 0
                    : Convert.ToDecimal(
                        dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamThem]
                            .Value.ToString());
            oWorkSheet.Cells[indexRow, 18] = LamThem == 0 ? "-" : LamThem.ToString(StringFormat.FormatCoefficient);
            TotalLamThem = TotalLamThem + LamThem;
            LamDem =
                dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Lamdem].Value ==
                null
                    ? 0
                    : Convert.ToDecimal(
                        dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Lamdem]
                            .Value.ToString());
            oWorkSheet.Cells[indexRow, 19] = LamDem == 0 ? "-" : LamDem.ToString(StringFormat.FormatCoefficient);
            TotalLamDem = TotalLamDem + LamDem;
            DTNopThue =
                dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_DTNopThue].Value ==
                null
                    ? 0
                    : Convert.ToDecimal(
                        dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_DTNopThue]
                            .Value.ToString());
            oWorkSheet.Cells[indexRow, 20] = DTNopThue == 0 ? "-" : DTNopThue.ToString(StringFormat.FormatCoefficient);
            TotalDTNopThue = TotalDTNopThue + DTNopThue;
            NguoiPThuoc =
                dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_NguoiPThuoc].Value ==
                null
                    ? 0
                    : Convert.ToDecimal(
                        dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_NguoiPThuoc
                        ].Value.ToString());
            oWorkSheet.Cells[indexRow, 21] = NguoiPThuoc == 0
                ? "-"
                : NguoiPThuoc.ToString(StringFormat.FormatCoefficient);
            TotalNguoiPThuoc = TotalNguoiPThuoc + NguoiPThuoc;

            TotalAllLCB = TotalAllLCB + LCB;
            TotalAllPCCV = TotalAllPCCV + PCCV;
            TotalAllPCTN = TotalAllPCTN + PCTN;
            TotalAllPCKV = TotalAllPCKV + PCKV;
            TotalAllHSLTNCD = TotalAllHSLTNCD + HSLTNCD;
            TotalAllLNS = TotalAllLNS + LNS;
            TotalAllK = TotalAllK + K;
            TotalAllX = TotalAllX + X;
            TotalAllOmCo = TotalAllOmCo + OmCo;
            TotalAllThaiSan = TotalAllThaiSan + ThaiSan;
            TotalAllTNLD = TotalAllTNLD + TNLD;
            TotalAllFDiDuong = TotalAllFDiDuong + FDiDuong;
            TotalAllKhac = TotalAllKhac + Khac;
            TotalAllLamThem = TotalAllLamThem + LamThem;
            TotalAllLamDem = TotalAllLamDem + LamDem;
            TotalAllDTNopThue = TotalAllDTNopThue + DTNopThue;
            TotalAllNguoiPThuoc = TotalAllNguoiPThuoc + NguoiPThuoc;


            LCB = 0;
            PCCV = 0;
            PCTN = 0;
            PCKV = 0;
            HSLTNCD = 0;
            LNS = 0;
            K = 0;
            X = 0;
            OmCo = 0;
            ThaiSan = 0;
            TNLD = 0;
            FDiDuong = 0;
            Khac = 0;
            LamThem = 0;
            LamDem = 0;
            DTNopThue = 0;
            NguoiPThuoc = 0;

            for (var i = 1; i < rgwList.RowCount; i++)
            {
                dr = rgwList.Rows[i];

                var userId =
                    dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_UserId].Value ==
                    null
                        ? 0
                        : Convert.ToInt32(
                            dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_UserId]
                                .Value.ToString());
                if (userId > 0)
                {
                    var dr_1 = rgwList.Rows[i - 1];
                    var rootId =
                        dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_RootId]
                            .Value == null
                            ? 0
                            : int.Parse(
                                dr.Cells[
                                        WorkdayCoefficientEmployeesFinalKeys
                                            .Field_WorkdayCoefficientEmployeesFinal_RootId]
                                    .Value.ToString());
                    var rootId_1 =
                        dr_1.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_RootId]
                            .Value == null
                            ? 0
                            : int.Parse(
                                dr_1.Cells[
                                        WorkdayCoefficientEmployeesFinalKeys
                                            .Field_WorkdayCoefficientEmployeesFinal_RootId]
                                    .Value.ToString());

                    indexRow++;

                    if ((i < rgwList.RowCount - 1) && (i > 1))
                    {
                        deparmentId = rootId;
                        departmentIdBefore = rootId_1;
                    }

                    if (deparmentId != departmentIdBefore)
                    {
                        oWorkSheet.Cells[previousIndexRow, 5] = TotalLCB == 0
                            ? "-"
                            : TotalLCB.ToString(StringFormat.FormatCoefficient);
                        oWorkSheet.Cells[previousIndexRow, 6] = TotalPCCV == 0
                            ? "-"
                            : TotalPCCV.ToString(StringFormat.FormatCoefficient);
                        oWorkSheet.Cells[previousIndexRow, 7] = TotalPCTN == 0
                            ? "-"
                            : TotalPCTN.ToString(StringFormat.FormatCoefficient);
                        oWorkSheet.Cells[previousIndexRow, 8] = TotalPCKV == 0
                            ? "-"
                            : TotalPCKV.ToString(StringFormat.FormatCoefficient);
                        oWorkSheet.Cells[previousIndexRow, 9] = TotalHSLTNCD == 0
                            ? "-"
                            : TotalHSLTNCD.ToString(StringFormat.FormatCoefficient);
                        oWorkSheet.Cells[previousIndexRow, 10] = TotalLNS == 0
                            ? "-"
                            : TotalLNS.ToString(StringFormat.FormatCoefficient);
                        oWorkSheet.Cells[previousIndexRow, 11] = TotalK == 0
                            ? "-"
                            : TotalK.ToString(StringFormat.FormatCoefficient);
                        oWorkSheet.Cells[previousIndexRow, 12] = TotalX == 0
                            ? "-"
                            : TotalX.ToString(StringFormat.FormatCoefficient);
                        oWorkSheet.Cells[previousIndexRow, 13] = TotalOmCo == 0
                            ? "-"
                            : TotalOmCo.ToString(StringFormat.FormatCoefficient);
                        oWorkSheet.Cells[previousIndexRow, 14] = TotalThaiSan == 0
                            ? "-"
                            : TotalThaiSan.ToString(StringFormat.FormatCoefficient);
                        oWorkSheet.Cells[previousIndexRow, 15] = TotalTNLD == 0
                            ? "-"
                            : TotalTNLD.ToString(StringFormat.FormatCoefficient);
                        oWorkSheet.Cells[previousIndexRow, 16] = TotalFDiDuong == 0
                            ? "-"
                            : TotalFDiDuong.ToString(StringFormat.FormatCoefficient);
                        oWorkSheet.Cells[previousIndexRow, 17] = TotalKhac == 0
                            ? "-"
                            : TotalKhac.ToString(StringFormat.FormatCoefficient);
                        oWorkSheet.Cells[previousIndexRow, 18] = TotalLamThem == 0
                            ? "-"
                            : TotalLamThem.ToString(StringFormat.FormatCoefficient);
                        oWorkSheet.Cells[previousIndexRow, 19] = TotalLamDem == 0
                            ? "-"
                            : TotalLamDem.ToString(StringFormat.FormatCoefficient);
                        oWorkSheet.Cells[previousIndexRow, 20] = TotalDTNopThue == 0
                            ? "-"
                            : TotalDTNopThue.ToString(StringFormat.FormatCoefficient);
                        oWorkSheet.Cells[previousIndexRow, 21] = TotalNguoiPThuoc == 0
                            ? "-"
                            : TotalNguoiPThuoc.ToString(StringFormat.FormatCoefficient);


                        previousIndexRow = indexRow;

                        TotalLCB = 0;
                        TotalPCCV = 0;
                        TotalPCTN = 0;
                        TotalPCKV = 0;
                        TotalHSLTNCD = 0;
                        TotalLNS = 0;
                        TotalK = 0;
                        TotalX = 0;
                        TotalOmCo = 0;
                        TotalThaiSan = 0;
                        TotalTNLD = 0;
                        TotalFDiDuong = 0;
                        TotalKhac = 0;
                        TotalLamThem = 0;
                        TotalLamDem = 0;
                        TotalDTNopThue = 0;
                        TotalNguoiPThuoc = 0;

                        orderNumber = 1;
                        rangeDept = oWorkSheet.get_Range("A" + indexRow, "C" + indexRow);
                        rangeDept.Merge(Type.Missing);
                        oWorkSheet.Cells[indexRow, 1] =
                            dr.Cells[
                                    WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_RootName
                                ]
                                .Value == null
                                ? string.Empty
                                : dr.Cells[
                                    WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_RootName
                                ].Value.ToString().ToUpper();
                        rangeDept = oWorkSheet.get_Range("A" + indexRow, "U" + indexRow);
                        rangeDept.Font.Bold = true;
                        indexRow++;
                    }


                    /// inserting order number
                    oWorkSheet.Cells[indexRow, 1] = orderNumber++;

                    UserId =
                        dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_UserId]
                            .Value == null
                            ? 0
                            : Convert.ToInt32(
                                dr.Cells[
                                        WorkdayCoefficientEmployeesFinalKeys
                                            .Field_WorkdayCoefficientEmployeesFinal_UserId]
                                    .Value.ToString());
                    oWorkSheet.Cells[indexRow, 2] = "'" + UserId.ToString(StringFormat.FormatUserId);
                    oWorkSheet.Cells[indexRow, 3] =
                        dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_FullName]
                            .Value == null
                            ? string.Empty
                            : dr.Cells[
                                    WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_FullName
                                ]
                                .Value.ToString();
                    oWorkSheet.Cells[indexRow, 4] =
                        dr.Cells[
                                WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_PositionName
                            ]
                            .Value == null
                            ? string.Empty
                            : dr.Cells[
                                WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_PositionName
                            ].Value.ToString();

                    LCB =
                        dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSLCB]
                            .Value == null
                            ? 0
                            : Convert.ToDecimal(
                                dr.Cells[
                                        WorkdayCoefficientEmployeesFinalKeys
                                            .Field_WorkdayCoefficientEmployeesFinal_HSLCB]
                                    .Value.ToString());
                    oWorkSheet.Cells[indexRow, 5] = LCB == 0 ? "-" : LCB.ToString(StringFormat.FormatCoefficient);
                    TotalLCB = TotalLCB + LCB;
                    PCCV =
                        dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSPCCV]
                            .Value == null
                            ? 0
                            : Convert.ToDecimal(
                                dr.Cells[
                                        WorkdayCoefficientEmployeesFinalKeys
                                            .Field_WorkdayCoefficientEmployeesFinal_HSPCCV]
                                    .Value.ToString());
                    oWorkSheet.Cells[indexRow, 6] = PCCV == 0 ? "-" : PCCV.ToString(StringFormat.FormatCoefficient);
                    TotalPCCV = TotalPCCV + PCCV;
                    PCTN =
                        dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSPCTN]
                            .Value == null
                            ? 0
                            : Convert.ToDecimal(
                                dr.Cells[
                                        WorkdayCoefficientEmployeesFinalKeys
                                            .Field_WorkdayCoefficientEmployeesFinal_HSPCTN]
                                    .Value.ToString());
                    oWorkSheet.Cells[indexRow, 7] = PCTN == 0 ? "-" : PCTN.ToString(StringFormat.FormatCoefficient);
                    TotalPCTN = TotalPCTN + PCTN;
                    PCKV =
                        dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSPCKV]
                            .Value == null
                            ? 0
                            : Convert.ToDecimal(
                                dr.Cells[
                                        WorkdayCoefficientEmployeesFinalKeys
                                            .Field_WorkdayCoefficientEmployeesFinal_HSPCKV]
                                    .Value.ToString());
                    oWorkSheet.Cells[indexRow, 8] = PCKV == 0 ? "-" : PCKV.ToString(StringFormat.FormatCoefficient);
                    TotalPCKV = TotalPCKV + PCKV;
                    HSLTNCD =
                        dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSLNSPCTN]
                            .Value == null
                            ? 0
                            : Convert.ToDecimal(
                                dr.Cells[
                                    WorkdayCoefficientEmployeesFinalKeys
                                        .Field_WorkdayCoefficientEmployeesFinal_HSLNSPCTN].Value.ToString());
                    oWorkSheet.Cells[indexRow, 9] = HSLTNCD == 0
                        ? "-"
                        : HSLTNCD.ToString(StringFormat.FormatCoefficient);
                    TotalHSLTNCD = TotalHSLTNCD + HSLTNCD;
                    LNS =
                        dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSLNS]
                            .Value == null
                            ? 0
                            : Convert.ToDecimal(
                                dr.Cells[
                                        WorkdayCoefficientEmployeesFinalKeys
                                            .Field_WorkdayCoefficientEmployeesFinal_HSLNS]
                                    .Value.ToString());
                    oWorkSheet.Cells[indexRow, 10] = LNS == 0 ? "-" : LNS.ToString(StringFormat.FormatCoefficient);
                    TotalLNS = TotalLNS + LNS;
                    K =
                        dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSK].Value ==
                        null
                            ? 0
                            : Convert.ToDecimal(
                                dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_HSK
                                ].Value.ToString());
                    oWorkSheet.Cells[indexRow, 11] = K == 0 ? "-" : K.ToString(StringFormat.FormatCoefficient);
                    TotalK = TotalK + K;

                    X = dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_X].Value ==
                        null
                        ? 0
                        : Convert.ToDecimal(
                            dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_X]
                                .Value.ToString());
                    oWorkSheet.Cells[indexRow, 12] = X == 0 ? "-" : X.ToString(StringFormat.FormatCoefficient);
                    TotalX = TotalX + X;
                    OmCo =
                        dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_OmCo].Value ==
                        null
                            ? 0
                            : Convert.ToDecimal(
                                dr.Cells[
                                        WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_OmCo
                                    ]
                                    .Value.ToString());
                    oWorkSheet.Cells[indexRow, 13] = OmCo == 0 ? "-" : OmCo.ToString(StringFormat.FormatCoefficient);
                    TotalOmCo = TotalOmCo + OmCo;
                    ThaiSan =
                        dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_ThaiSan]
                            .Value == null
                            ? 0
                            : Convert.ToDecimal(
                                dr.Cells[
                                        WorkdayCoefficientEmployeesFinalKeys
                                            .Field_WorkdayCoefficientEmployeesFinal_ThaiSan]
                                    .Value.ToString());
                    oWorkSheet.Cells[indexRow, 14] = ThaiSan == 0
                        ? "-"
                        : ThaiSan.ToString(StringFormat.FormatCoefficient);
                    TotalThaiSan = TotalThaiSan + ThaiSan;
                    TNLD =
                        dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_TNLD].Value ==
                        null
                            ? 0
                            : Convert.ToDecimal(
                                dr.Cells[
                                        WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_TNLD
                                    ]
                                    .Value.ToString());
                    oWorkSheet.Cells[indexRow, 15] = TNLD == 0 ? "-" : TNLD.ToString(StringFormat.FormatCoefficient);
                    TotalTNLD = TotalTNLD + TNLD;
                    FDiDuong =
                        dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_FDiDuong]
                            .Value == null
                            ? 0
                            : Convert.ToDecimal(
                                dr.Cells[
                                    WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_FDiDuong
                                ].Value.ToString());
                    oWorkSheet.Cells[indexRow, 16] = FDiDuong == 0
                        ? "-"
                        : FDiDuong.ToString(StringFormat.FormatCoefficient);
                    TotalFDiDuong = TotalFDiDuong + FDiDuong;
                    Khac =
                        dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Khac].Value ==
                        null
                            ? 0
                            : Convert.ToDecimal(
                                dr.Cells[
                                        WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Khac
                                    ]
                                    .Value.ToString());
                    oWorkSheet.Cells[indexRow, 17] = Khac == 0 ? "-" : Khac.ToString(StringFormat.FormatCoefficient);
                    TotalKhac = TotalKhac + Khac;

                    LamThem =
                        dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamThem]
                            .Value == null
                            ? 0
                            : Convert.ToDecimal(
                                dr.Cells[
                                        WorkdayCoefficientEmployeesFinalKeys
                                            .Field_WorkdayCoefficientEmployeesFinal_LamThem]
                                    .Value.ToString());
                    oWorkSheet.Cells[indexRow, 18] = LamThem == 0
                        ? "-"
                        : LamThem.ToString(StringFormat.FormatCoefficient);
                    TotalLamThem = TotalLamThem + LamThem;
                    LamDem =
                        dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Lamdem]
                            .Value == null
                            ? 0
                            : Convert.ToDecimal(
                                dr.Cells[
                                        WorkdayCoefficientEmployeesFinalKeys
                                            .Field_WorkdayCoefficientEmployeesFinal_Lamdem]
                                    .Value.ToString());
                    oWorkSheet.Cells[indexRow, 19] = LamDem == 0 ? "-" : LamDem.ToString(StringFormat.FormatCoefficient);
                    TotalLamDem = TotalLamDem + LamDem;
                    DTNopThue =
                        dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_DTNopThue]
                            .Value == null
                            ? 0
                            : Convert.ToDecimal(
                                dr.Cells[
                                    WorkdayCoefficientEmployeesFinalKeys
                                        .Field_WorkdayCoefficientEmployeesFinal_DTNopThue].Value.ToString());
                    oWorkSheet.Cells[indexRow, 20] = DTNopThue == 0
                        ? "-"
                        : DTNopThue.ToString(StringFormat.FormatCoefficient);
                    TotalDTNopThue = TotalDTNopThue + DTNopThue;
                    NguoiPThuoc =
                        dr.Cells[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_NguoiPThuoc
                        ].Value == null
                            ? 0
                            : Convert.ToDecimal(
                                dr.Cells[
                                    WorkdayCoefficientEmployeesFinalKeys
                                        .Field_WorkdayCoefficientEmployeesFinal_NguoiPThuoc].Value.ToString());
                    oWorkSheet.Cells[indexRow, 21] = NguoiPThuoc == 0
                        ? "-"
                        : NguoiPThuoc.ToString(StringFormat.FormatCoefficient);
                    TotalNguoiPThuoc = TotalNguoiPThuoc + NguoiPThuoc;

                    TotalAllLCB = TotalAllLCB + LCB;
                    TotalAllPCCV = TotalAllPCCV + PCCV;
                    TotalAllPCTN = TotalAllPCTN + PCTN;
                    TotalAllPCKV = TotalAllPCKV + PCKV;
                    TotalAllHSLTNCD = TotalAllHSLTNCD + HSLTNCD;
                    TotalAllLNS = TotalAllLNS + LNS;
                    TotalAllK = TotalAllK + K;
                    TotalAllX = TotalAllX + X;
                    TotalAllOmCo = TotalAllOmCo + OmCo;
                    TotalAllThaiSan = TotalAllThaiSan + ThaiSan;
                    TotalAllTNLD = TotalAllTNLD + TNLD;
                    TotalAllFDiDuong = TotalAllFDiDuong + FDiDuong;
                    TotalAllKhac = TotalAllKhac + Khac;
                    TotalAllLamThem = TotalAllLamThem + LamThem;
                    TotalAllLamDem = TotalAllLamDem + LamDem;
                    TotalAllDTNopThue = TotalAllDTNopThue + DTNopThue;
                    TotalAllNguoiPThuoc = TotalAllNguoiPThuoc + NguoiPThuoc;
                }

                backgroundWorkerExport.ReportProgress(i + 1, fullName);
                Thread.Sleep(10);
            }


            oWorkSheet.Cells[previousIndexRow, 5] = TotalLCB == 0
                ? "-"
                : TotalLCB.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[previousIndexRow, 6] = TotalPCCV == 0
                ? "-"
                : TotalPCCV.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[previousIndexRow, 7] = TotalPCTN == 0
                ? "-"
                : TotalPCTN.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[previousIndexRow, 8] = TotalPCKV == 0
                ? "-"
                : TotalPCKV.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[previousIndexRow, 9] = TotalHSLTNCD == 0
                ? "-"
                : TotalHSLTNCD.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[previousIndexRow, 10] = TotalLNS == 0
                ? "-"
                : TotalLNS.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[previousIndexRow, 11] = TotalK == 0 ? "-" : TotalK.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[previousIndexRow, 12] = TotalX == 0 ? "-" : TotalX.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[previousIndexRow, 13] = TotalOmCo == 0
                ? "-"
                : TotalOmCo.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[previousIndexRow, 14] = TotalThaiSan == 0
                ? "-"
                : TotalThaiSan.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[previousIndexRow, 15] = TotalTNLD == 0
                ? "-"
                : TotalTNLD.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[previousIndexRow, 16] = TotalFDiDuong == 0
                ? "-"
                : TotalFDiDuong.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[previousIndexRow, 17] = TotalKhac == 0
                ? "-"
                : TotalKhac.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[previousIndexRow, 18] = TotalLamThem == 0
                ? "-"
                : TotalLamThem.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[previousIndexRow, 19] = TotalLamDem == 0
                ? "-"
                : TotalLamDem.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[previousIndexRow, 20] = TotalDTNopThue == 0
                ? "-"
                : TotalDTNopThue.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[previousIndexRow, 21] = TotalNguoiPThuoc == 0
                ? "-"
                : TotalNguoiPThuoc.ToString(StringFormat.FormatCoefficient);


            indexRow = indexRow + 1;
            oWorkSheet.Cells[indexRow, 1] = rgwList.RowCount;
            oWorkSheet.Cells[indexRow, 2] = "TỔNG CỘNG";
            var totalRange = oWorkSheet.Range[oWorkSheet.Cells[indexRow, 2], oWorkSheet.Cells[indexRow, 3]];
            totalRange.Merge(Type.Missing);
            totalRange = oWorkSheet.Range[oWorkSheet.Cells[indexRow, 1], oWorkSheet.Cells[indexRow, 21]];
            totalRange.Font.Bold = true;


            oWorkSheet.Cells[indexRow, 5] = TotalAllLCB == 0
                ? "-"
                : TotalAllLCB.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[indexRow, 6] = TotalAllPCCV == 0
                ? "-"
                : TotalAllPCCV.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[indexRow, 7] = TotalAllPCTN == 0
                ? "-"
                : TotalAllPCTN.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[indexRow, 8] = TotalAllPCKV == 0
                ? "-"
                : TotalAllPCKV.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[indexRow, 9] = TotalAllHSLTNCD == 0
                ? "-"
                : TotalAllHSLTNCD.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[indexRow, 10] = TotalAllLNS == 0
                ? "-"
                : TotalAllLNS.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[indexRow, 11] = TotalAllK == 0 ? "-" : TotalAllK.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[indexRow, 12] = TotalAllX == 0 ? "-" : TotalAllX.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[indexRow, 13] = TotalAllOmCo == 0
                ? "-"
                : TotalAllOmCo.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[indexRow, 14] = TotalAllThaiSan == 0
                ? "-"
                : TotalAllThaiSan.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[indexRow, 15] = TotalAllTNLD == 0
                ? "-"
                : TotalAllTNLD.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[indexRow, 16] = TotalAllFDiDuong == 0
                ? "-"
                : TotalAllFDiDuong.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[indexRow, 17] = TotalAllKhac == 0
                ? "-"
                : TotalAllKhac.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[indexRow, 18] = TotalAllLamThem == 0
                ? "-"
                : TotalAllLamThem.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[indexRow, 19] = TotalAllLamDem == 0
                ? "-"
                : TotalAllLamDem.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[indexRow, 20] = TotalAllDTNopThue == 0
                ? "-"
                : TotalAllDTNopThue.ToString(StringFormat.FormatCoefficient);
            oWorkSheet.Cells[indexRow, 21] = TotalAllNguoiPThuoc == 0
                ? "-"
                : TotalAllNguoiPThuoc.ToString(StringFormat.FormatCoefficient);


            var rangeAll = oWorkSheet.get_Range("A6", "U" + indexRow);
            rangeAll.Borders.LineStyle = Constants.xlSolid;
            rangeAll.Borders.Weight = 2;
            rangeAll.Font.Size = 8;
            rangeAll.Font.Name = "Times New Roman";


            indexRow = indexRow + 2;
            oWorkSheet.Cells[indexRow, 3] = "NGƯỜI LẬP BẢNG";
            oWorkSheet.Cells[indexRow, 6] = "P.TỔ CHỨC HÀNH CHÍNH";
            oWorkSheet.Cells[indexRow, 12] = "P.TÀI CHÍNH KẾ TOÁN";
            oWorkSheet.Cells[indexRow, 16] = "GIÁM DỐC";
            var rangeFooter1 = oWorkSheet.Range[oWorkSheet.Cells[indexRow, 1], oWorkSheet.Cells[indexRow, 18]];
            rangeFooter1.Font.Name = "Times New Roman";
            rangeFooter1.Font.Size = 9;
            rangeFooter1.Font.Bold = true;
        }

        private static void CreateHeaderAndTitle(ref _Worksheet oWorkSheet, ref int initTitleIndex, DateTime date)
        {
            var rangeHeader1 = oWorkSheet.get_Range("A1", "D1");
            rangeHeader1.Merge(Type.Missing);
            oWorkSheet.Cells[1, 1] = HRMUtil.Constants.ACV_NAME;
            rangeHeader1.Font.Size = 12;
            rangeHeader1.Font.Name = "Times New Roman";
            rangeHeader1.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            var rangeHeader2 = oWorkSheet.get_Range("A2", "D2");
            rangeHeader2.Merge(Type.Missing);
            oWorkSheet.Cells[2, 1] = HRMUtil.Constants.SAGS_NAME;
            rangeHeader2.Font.Size = 12;
            rangeHeader2.Font.Name = "Times New Roman";
            rangeHeader2.Font.Bold = true;
            rangeHeader2.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            var rangeHeader3 = oWorkSheet.get_Range("A4", "R4");
            rangeHeader3.Merge(Type.Missing);
            oWorkSheet.Cells[4, 1] = "BẢNG TỔNG HỢP HỆ SỐ - NGÀY CÔNG THÁNG " + date.Month + " NĂM " + date.Year;
            rangeHeader3.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            rangeHeader3.Font.Size = 15;
            rangeHeader3.Font.Name = "Times New Roman";
            rangeHeader3.Font.Bold = true;
            rangeHeader3.HorizontalAlignment = XlHAlign.xlHAlignCenter;


            oWorkSheet.Cells[initTitleIndex - 1, 5] = "Hệ số";
            oWorkSheet.Cells[initTitleIndex - 1, 12] = "Ngày công";
            oWorkSheet.Cells[initTitleIndex - 1, 18] = "Giờ công";
            oWorkSheet.Cells[initTitleIndex - 1, 20] = "Giảm trừ GC";


            /// inserting title
            oWorkSheet.Cells[initTitleIndex, 1] = "STT";
            ((Range) oWorkSheet.Cells[initTitleIndex, 1]).ColumnWidth = 7;
            oWorkSheet.Cells[initTitleIndex, 2] = "Mã NV";
            ((Range) oWorkSheet.Cells[initTitleIndex, 2]).ColumnWidth = 7;
            oWorkSheet.Cells[initTitleIndex, 3] = "Họ và tên";
            ((Range) oWorkSheet.Cells[initTitleIndex, 3]).ColumnWidth = 25;
            oWorkSheet.Cells[initTitleIndex, 4] = "Chức vụ - Chức danh";
            ((Range) oWorkSheet.Cells[initTitleIndex, 4]).ColumnWidth = 15;
            oWorkSheet.Cells[initTitleIndex, 5] = "HSLCB";
            ((Range) oWorkSheet.Cells[initTitleIndex, 5]).ColumnWidth = 7;
            oWorkSheet.Cells[initTitleIndex, 6] = "PCCV";
            ((Range) oWorkSheet.Cells[initTitleIndex, 6]).ColumnWidth = 7;
            oWorkSheet.Cells[initTitleIndex, 7] = "PCTN";
            ((Range) oWorkSheet.Cells[initTitleIndex, 7]).ColumnWidth = 7;
            oWorkSheet.Cells[initTitleIndex, 8] = "PCKV";
            ((Range) oWorkSheet.Cells[initTitleIndex, 8]).ColumnWidth = 7;
            oWorkSheet.Cells[initTitleIndex, 9] = "HSLTN CD";
            ((Range) oWorkSheet.Cells[initTitleIndex, 9]).ColumnWidth = 7;
            oWorkSheet.Cells[initTitleIndex, 10] = "LCD";
            ((Range) oWorkSheet.Cells[initTitleIndex, 10]).ColumnWidth = 7;
            oWorkSheet.Cells[initTitleIndex, 11] = "HS K";
            ((Range) oWorkSheet.Cells[initTitleIndex, 11]).ColumnWidth = 7;
            oWorkSheet.Cells[initTitleIndex, 12] = "Làm việc";
            ((Range) oWorkSheet.Cells[initTitleIndex, 12]).ColumnWidth = 10;
            oWorkSheet.Cells[initTitleIndex, 13] = "Ốm,Co";
            ((Range) oWorkSheet.Cells[initTitleIndex, 13]).ColumnWidth = 7;
            oWorkSheet.Cells[initTitleIndex, 14] = "Thai sản";
            ((Range) oWorkSheet.Cells[initTitleIndex, 14]).ColumnWidth = 7;
            oWorkSheet.Cells[initTitleIndex, 15] = "TNLĐ";
            ((Range) oWorkSheet.Cells[initTitleIndex, 15]).ColumnWidth = 7;
            oWorkSheet.Cells[initTitleIndex, 16] = "F,Đi đường";
            ((Range) oWorkSheet.Cells[initTitleIndex, 16]).ColumnWidth = 7;
            oWorkSheet.Cells[initTitleIndex, 17] = "Khác";
            ((Range) oWorkSheet.Cells[initTitleIndex, 17]).ColumnWidth = 7;
            oWorkSheet.Cells[initTitleIndex, 18] = "Làm thêm";
            ((Range) oWorkSheet.Cells[initTitleIndex, 18]).ColumnWidth = 7;
            oWorkSheet.Cells[initTitleIndex, 19] = "Làm đêm";
            ((Range) oWorkSheet.Cells[initTitleIndex, 19]).ColumnWidth = 7;
            oWorkSheet.Cells[initTitleIndex, 20] = "ĐT nộp thuế";
            ((Range) oWorkSheet.Cells[initTitleIndex, 20]).ColumnWidth = 7;
            oWorkSheet.Cells[initTitleIndex, 21] = "Người P.Thuộc";
            ((Range) oWorkSheet.Cells[initTitleIndex, 21]).ColumnWidth = 7;
            /// format title
            var oTitleRange = oWorkSheet.get_Range("A6", "U7");
            oTitleRange.Font.Bold = true;
            oTitleRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;

            oTitleRange = oWorkSheet.get_Range("E6", "J6");
            oTitleRange.Merge(Type.Missing);
            oTitleRange = oWorkSheet.get_Range("L6", "Q6");
            oTitleRange.Merge(Type.Missing);
            oTitleRange = oWorkSheet.get_Range("R6", "S6");
            oTitleRange.Merge(Type.Missing);
            oTitleRange = oWorkSheet.get_Range("T6", "U6");
            oTitleRange.Merge(Type.Missing);

            oTitleRange = oWorkSheet.get_Range("A6", "A7");
            oTitleRange.Merge(Type.Missing);
            oTitleRange = oWorkSheet.get_Range("B6", "B7");
            oTitleRange.Merge(Type.Missing);
            oTitleRange = oWorkSheet.get_Range("C6", "C7");
            oTitleRange.Merge(Type.Missing);
            oTitleRange = oWorkSheet.get_Range("D6", "D7");
            oTitleRange.Merge(Type.Missing);
        }
    }
}