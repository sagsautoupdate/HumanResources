using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using HRMBLL.H0;
using HRMBLL.H1;
using HRMBLL.H1.Helper;
using HRMUtil;
using Telerik.WinControls.UI;

namespace HumanResources.Forms.Workingday.Helper
{
    public class Checker
    {
        private static string _SP = string.Empty;
        private static string _SPValue = string.Empty;

        public static double GetDataByDR(DataRow Dr, string DataType, int Month, int Year)
        {
            var DataDate = new DateTime(Year, Month, 1);


            var Day1 = Dr["Day1"].ToString();
            var Day2 = Dr["Day2"].ToString();
            var Day3 = Dr["Day3"].ToString();
            var Day4 = Dr["Day4"].ToString();
            var Day5 = Dr["Day5"].ToString();
            var Day6 = Dr["Day6"].ToString();
            var Day7 = Dr["Day7"].ToString();
            var Day8 = Dr["Day8"].ToString();
            var Day9 = Dr["Day9"].ToString();
            var Day10 = Dr["Day10"].ToString();
            var Day11 = Dr["Day11"].ToString();
            var Day12 = Dr["Day12"].ToString();
            var Day13 = Dr["Day13"].ToString();
            var Day14 = Dr["Day14"].ToString();
            var Day15 = Dr["Day15"].ToString();
            var Day16 = Dr["Day16"].ToString();
            var Day17 = Dr["Day17"].ToString();
            var Day18 = Dr["Day18"].ToString();
            var Day19 = Dr["Day19"].ToString();
            var Day20 = Dr["Day20"].ToString();
            var Day21 = Dr["Day21"].ToString();
            var Day22 = Dr["Day22"].ToString();
            var Day23 = Dr["Day23"].ToString();
            var Day24 = Dr["Day24"].ToString();
            var Day25 = Dr["Day25"].ToString();
            var Day26 = Dr["Day26"].ToString();
            var Day27 = Dr["Day27"].ToString();
            var Day28 = Dr["Day28"].ToString();
            var Day29 = Dr["Day29"].ToString();
            var Day30 = Dr["Day30"].ToString();
            var Day31 = Dr["Day31"].ToString();
            var f_Om = Constants.WorkdayEmployee_DefaultValue;
            var f_OmDaiNgay = Constants.WorkdayEmployee_DefaultValue;
            var f_ThaiSan = Constants.WorkdayEmployee_DefaultValue;
            var f_TNLD = Constants.WorkdayEmployee_DefaultValue;
            var f_Nam = Constants.WorkdayEmployee_DefaultValue;
            var f_db = Constants.WorkdayEmployee_DefaultValue;
            var f_KoLuongCLD = Constants.WorkdayEmployee_DefaultValue;
            var f_KoLuongKLD = Constants.WorkdayEmployee_DefaultValue;
            var f_DiDuong = Constants.WorkdayEmployee_DefaultValue;
            var f_CongTac = Constants.WorkdayEmployee_DefaultValue;

            var f_HocSAGS = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc1 = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc2 = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc3 = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc4 = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc5 = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc6 = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc7 = Constants.WorkdayEmployee_DefaultValue;

            var f_Con_Om = Constants.WorkdayEmployee_DefaultValue;
            var f_KHHDS = Constants.WorkdayEmployee_DefaultValue;
            var f_SayThai = Constants.WorkdayEmployee_DefaultValue;
            var f_KhamThai = Constants.WorkdayEmployee_DefaultValue;
            var f_ConChet = Constants.WorkdayEmployee_DefaultValue;
            var f_DinhChiCongTac = Constants.WorkdayEmployee_DefaultValue;
            var f_TamHoanHD = Constants.WorkdayEmployee_DefaultValue;
            var f_HoiHop = Constants.WorkdayEmployee_DefaultValue;
            var f_Le = Constants.WorkdayEmployee_DefaultValue;
            var nghiTuan = Constants.WorkdayEmployee_DefaultValue;
            var nghiBu = Constants.WorkdayEmployee_DefaultValue;
            var nghiMat = Constants.WorkdayEmployee_DefaultValue;
            var nghiViec = Constants.WorkdayEmployee_DefaultValue;
            var chuaDiLam = Constants.WorkdayEmployee_DefaultValue;
            var f_OmDNBHXH = Constants.WorkdayEmployee_DefaultValue;


            f_OmDNBHXH = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_O_DAI_NGAY_HUONG_BHXH);

            f_Om = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5, Day6,
                Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_O_BAN_THAN);

            f_OmDaiNgay = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_O_DAI_NGAY);

            f_KHHDS = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_KHHDS);

            f_Con_Om = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_CON_OM);

            f_ThaiSan = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_THAI_SAN);

            f_SayThai = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_SAY_THAI);

            f_KhamThai = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_KHAM_THAI);

            f_TNLD = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_TNLD);

            f_Nam = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5, Day6,
                Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_F_NAM);

            f_DiDuong = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_F_DI_DUONG);

            f_CongTac = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_F_CONG_TAC);

            f_db = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5, Day6,
                Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_FDB);

            f_Hoc1 = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_1);

            f_Hoc2 = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_2);

            f_Hoc3 = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_3);

            f_Hoc4 = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_4);

            f_Hoc5 = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_5);

            f_Hoc6 = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_6);

            f_Hoc7 = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_7);

            f_DinhChiCongTac = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_DINH_CHI_CONG_TAC);

            f_KoLuongCLD = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO);

            f_KoLuongKLD = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO);


            f_HocSAGS = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_SAGS);

            f_ConChet = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_CON_CHET_SAU_KHI_SINH);

            f_TamHoanHD = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_TAM_HOAN_HOP_DONG);
            f_HoiHop = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOI_HOP);
            f_Le = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5, Day6,
                Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_LE_TET);

            nghiTuan = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_TUAN);

            nghiBu = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_BU);

            nghiMat = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_MAT);

            nghiViec = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_VIEC);

            chuaDiLam = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_CHUA_DI_LAM);

            var X = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5, Day6,
                Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_X);


            var TotalLeave = f_OmDNBHXH + f_Om + f_OmDaiNgay + f_KHHDS + f_Con_Om + f_ThaiSan + f_SayThai + f_KhamThai +
                             f_TNLD + f_Nam + f_DiDuong + f_CongTac + f_db + f_Hoc1 + f_Hoc2 + f_Hoc3 + f_Hoc4 + f_Hoc5 +
                             f_Hoc6 + f_Hoc7 + f_DinhChiCongTac + f_KoLuongCLD + f_KoLuongKLD + nghiBu;

            if (DataType == "X")
                return X;
            return TotalLeave;
        }


        public static double GetDataByGridDR(GridViewRowInfo Dr, string DataType, int Month, int Year)
        {
            var DataDate = new DateTime(Year, Month, 1);


            var Day1 = Dr.Cells["Day1"].Value.ToString();
            var Day2 = Dr.Cells["Day2"].Value.ToString();
            var Day3 = Dr.Cells["Day3"].Value.ToString();
            var Day4 = Dr.Cells["Day4"].Value.ToString();
            var Day5 = Dr.Cells["Day5"].Value.ToString();
            var Day6 = Dr.Cells["Day6"].Value.ToString();
            var Day7 = Dr.Cells["Day7"].Value.ToString();
            var Day8 = Dr.Cells["Day8"].Value.ToString();
            var Day9 = Dr.Cells["Day9"].Value.ToString();
            var Day10 = Dr.Cells["Day10"].Value.ToString();
            var Day11 = Dr.Cells["Day11"].Value.ToString();
            var Day12 = Dr.Cells["Day12"].Value.ToString();
            var Day13 = Dr.Cells["Day13"].Value.ToString();
            var Day14 = Dr.Cells["Day14"].Value.ToString();
            var Day15 = Dr.Cells["Day15"].Value.ToString();
            var Day16 = Dr.Cells["Day16"].Value.ToString();
            var Day17 = Dr.Cells["Day17"].Value.ToString();
            var Day18 = Dr.Cells["Day18"].Value.ToString();
            var Day19 = Dr.Cells["Day19"].Value.ToString();
            var Day20 = Dr.Cells["Day20"].Value.ToString();
            var Day21 = Dr.Cells["Day21"].Value.ToString();
            var Day22 = Dr.Cells["Day22"].Value.ToString();
            var Day23 = Dr.Cells["Day23"].Value.ToString();
            var Day24 = Dr.Cells["Day24"].Value.ToString();
            var Day25 = Dr.Cells["Day25"].Value.ToString();
            var Day26 = Dr.Cells["Day26"].Value.ToString();
            var Day27 = Dr.Cells["Day27"].Value.ToString();
            var Day28 = Dr.Cells["Day28"].Value.ToString();
            var Day29 = Dr.Cells["Day29"].Value.ToString();
            var Day30 = Dr.Cells["Day30"].Value.ToString();
            var Day31 = Dr.Cells["Day31"].Value.ToString();
            var f_Om = Constants.WorkdayEmployee_DefaultValue;
            var f_OmDaiNgay = Constants.WorkdayEmployee_DefaultValue;
            var f_ThaiSan = Constants.WorkdayEmployee_DefaultValue;
            var f_TNLD = Constants.WorkdayEmployee_DefaultValue;
            var f_Nam = Constants.WorkdayEmployee_DefaultValue;
            var f_db = Constants.WorkdayEmployee_DefaultValue;
            var f_KoLuongCLD = Constants.WorkdayEmployee_DefaultValue;
            var f_KoLuongKLD = Constants.WorkdayEmployee_DefaultValue;
            var f_DiDuong = Constants.WorkdayEmployee_DefaultValue;
            var f_CongTac = Constants.WorkdayEmployee_DefaultValue;

            var f_HocSAGS = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc1 = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc2 = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc3 = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc4 = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc5 = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc6 = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc7 = Constants.WorkdayEmployee_DefaultValue;

            var f_Con_Om = Constants.WorkdayEmployee_DefaultValue;
            var f_KHHDS = Constants.WorkdayEmployee_DefaultValue;
            var f_SayThai = Constants.WorkdayEmployee_DefaultValue;
            var f_KhamThai = Constants.WorkdayEmployee_DefaultValue;
            var f_ConChet = Constants.WorkdayEmployee_DefaultValue;
            var f_DinhChiCongTac = Constants.WorkdayEmployee_DefaultValue;
            var f_TamHoanHD = Constants.WorkdayEmployee_DefaultValue;
            var f_HoiHop = Constants.WorkdayEmployee_DefaultValue;
            var f_Le = Constants.WorkdayEmployee_DefaultValue;
            var nghiTuan = Constants.WorkdayEmployee_DefaultValue;
            var nghiBu = Constants.WorkdayEmployee_DefaultValue;
            var nghiMat = Constants.WorkdayEmployee_DefaultValue;
            var nghiViec = Constants.WorkdayEmployee_DefaultValue;
            var chuaDiLam = Constants.WorkdayEmployee_DefaultValue;
            var f_OmDNBHXH = Constants.WorkdayEmployee_DefaultValue;


            f_OmDNBHXH = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_O_DAI_NGAY_HUONG_BHXH);

            f_Om = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5, Day6,
                Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_O_BAN_THAN);

            f_OmDaiNgay = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_O_DAI_NGAY);

            f_KHHDS = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_KHHDS);

            f_Con_Om = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_CON_OM);

            f_ThaiSan = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_THAI_SAN);

            f_SayThai = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_SAY_THAI);

            f_KhamThai = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_KHAM_THAI);

            f_TNLD = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_TNLD);

            f_Nam = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5, Day6,
                Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_F_NAM);

            f_DiDuong = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_F_DI_DUONG);

            f_CongTac = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_F_CONG_TAC);

            f_db = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5, Day6,
                Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_FDB);

            f_Hoc1 = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_1);

            f_Hoc2 = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_2);

            f_Hoc3 = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_3);

            f_Hoc4 = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_4);

            f_Hoc5 = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_5);

            f_Hoc6 = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_6);

            f_Hoc7 = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_7);

            f_DinhChiCongTac = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_DINH_CHI_CONG_TAC);

            f_KoLuongCLD = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO);

            f_KoLuongKLD = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4,
                Day5, Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO);


            f_HocSAGS = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOC_SAGS);

            f_ConChet = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_CON_CHET_SAU_KHI_SINH);

            f_TamHoanHD = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_TAM_HOAN_HOP_DONG);
            f_HoiHop = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_HOI_HOP);
            f_Le = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5, Day6,
                Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_LE_TET);

            nghiTuan = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_TUAN);

            nghiBu = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_BU);

            nghiMat = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_MAT);

            nghiViec = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_NGHI_VIEC);

            chuaDiLam = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5,
                Day6, Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_CHUA_DI_LAM);

            var X = DefaultValues.CalculateLeaveDay(DataDate.Month, DataDate.Year, 0, Day1, Day2, Day3, Day4, Day5, Day6,
                Day7, Day8, Day9, Day10, Day11, Day12, Day13,
                Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25, Day26,
                Day27, Day28, Day29, Day30, Day31, Constants.LEAVE_TYPE_X);


            var TotalLeave = f_OmDNBHXH + f_Om + f_OmDaiNgay + f_KHHDS + f_Con_Om + f_ThaiSan + f_SayThai + f_KhamThai +
                             f_TNLD + f_Nam + f_DiDuong + f_CongTac + f_db + f_Hoc1 + f_Hoc2 + f_Hoc3 + f_Hoc4 + f_Hoc5 +
                             f_Hoc6 + f_Hoc7 + f_DinhChiCongTac + f_KoLuongCLD + f_KoLuongKLD + nghiBu;

            if (DataType == "X")
                return X;
            return TotalLeave;
        }

        public static int GetTotalLeaveOnLeaveDayTable(int UserId, int Month, int Year)
        {
            var Total = 0;
            var DT = EmployeeLeaveBLL.GetDTByUserId_Date(UserId, Month, Year);
            foreach (DataRow DL in DT.Rows)
                Total += int.Parse(DL["Days"].ToString());
            return Total;
        }

        public static List<DateTime> GetDateRange(DateTime StartingDate, DateTime EndingDate)
        {
            if (StartingDate > EndingDate)
                return null;
            var rv = new List<DateTime>();
            var tmpDate = StartingDate;
            do
            {
                rv.Add(tmpDate);
                tmpDate = tmpDate.AddDays(1);
            } while (tmpDate <= EndingDate);
            return rv;
        }

        public static bool LeaveDay_WorkingDay_Comparer(int Month, int Year, int UserId, GridViewRowInfo grvDR)
        {
            var result = false;
            var DataDate = new DateTime(Year, Month, 1);

            if (grvDR != null)
            {
                var LeaveRow = EmployeeLeaveBLL.GetDTByUserId_Date(UserId, Month, Year);
                if (LeaveRow.Rows.Count > 0)
                {
                    var Count = 0;
                    var EmpLeave_Leave = GetTotalLeaveOnLeaveDayTable(UserId, Month, Year);
                    var EmpLeave_WD = GetDataByGridDR(grvDR, "TotalLeave", Month, Year);
                    if (EmpLeave_Leave - EmpLeave_WD == 0)
                        foreach (DataRow LeaveDR in LeaveRow.Rows)
                            foreach (
                                var Date in
                                GetDateRange(Convert.ToDateTime(LeaveDR["FromDate"]),
                                    Convert.ToDateTime(LeaveDR["ToDate"])))
                            {
                                var LeaveCode =
                                    Constants.GetSymbolTimekeeping(int.Parse(LeaveDR["LeaveTypeId"].ToString()));

                                if ((grvDR.Cells["Day1"].Value != null) ||
                                    (grvDR.Cells["Day1"].Value.ToString() != string.Empty))
                                    if ((LeaveCode != "DD") || (LeaveCode != "CT") || (LeaveCode != "Ko"))
                                        if (
                                            Convert.ToBoolean(
                                                EmployeesBLL.GetDataRowEmployeeById(UserId)["Direct"].ToString()))
                                        {
                                            if (grvDR.Cells["Day" + Date.Day].Value.ToString() == LeaveCode)
                                                Count++;
                                            else
                                                Count--;
                                        }

                                        else
                                        {
                                            if ((Date.DayOfWeek != DayOfWeek.Sunday) &&
                                                (Date.DayOfWeek != DayOfWeek.Saturday))
                                                if (grvDR.Cells["Day" + Date.Day].Value.ToString() == LeaveCode)
                                                    Count++;
                                                else
                                                    Count--;
                                        }
                            }
                    else
                        result = false;
                    if (Count == EmpLeave_Leave)
                        result = true;
                    else
                        result = false;
                }

                else
                {
                    if ((GetDataByGridDR(grvDR, "TotalLeave", Month, Year) > 0) ||
                        (Convert.ToDouble(grvDR.Cells["X"].Value) != Convert.ToDouble(grvDR.Cells["NCQD"].Value)) ||
                        (Convert.ToDouble(grvDR.Cells["NCDC"].Value) != Convert.ToDouble(grvDR.Cells["NCQD"].Value)))
                        result = false;
                    else
                        result = true;
                }
            }

            else
            {
                result = false;
            }
            return result;
        }

        public static void LeaveDay_WorkingDay_Checker(int Month, int Year, int UserId, GridViewRowInfo grvDR)
        {
            var DataDate = new DateTime(Year, Month, 1);

            var _ERROR = string.Empty;
            if (grvDR != null)
            {
                var LeaveRow = EmployeeLeaveBLL.GetDTByUserId_Date(UserId, Month, Year);
                var WorkdayCoefficientEmployeeIdFinal =
                    Convert.ToInt32(grvDR.Cells["WorkdayCoefficientEmployeeIdFinal"].Value);
                if (LeaveRow.Rows.Count > 0)
                {
                    var Count = 0;
                    var EmpLeave_Leave = GetTotalLeaveOnLeaveDayTable(UserId, Month, Year);
                    var EmpLeave_WD = GetDataByGridDR(grvDR, "TotalLeave", Month, Year);
                    var NTTotal = 0;
                    try
                    {
                        NTTotal = Convert.ToInt32(grvDR.Cells["NghiTuan"].Value);
                    }
                    catch
                    {
                    }
                    var NTCount = 0;
                    try
                    {
                        NTCount = CountCompareGridRow(grvDR, "NT");
                    }
                    catch
                    {
                    }

                    if (EmpLeave_Leave - EmpLeave_WD == 0)
                    {
                        if ((GetDataByGridDR(grvDR, "TotalLeave", Month, Year) +
                             GetDataByGridDR(grvDR, "X", Month, Year) < Convert.ToDouble(grvDR.Cells["NCDC"].Value)) ||
                            (GetDataByGridDR(grvDR, "TotalLeave", Month, Year) +
                             GetDataByGridDR(grvDR, "X", Month, Year) > Convert.ToDouble(grvDR.Cells["NCDC"].Value)) ||
                            (GetDataByGridDR(grvDR, "TotalLeave", Month, Year) +
                             GetDataByGridDR(grvDR, "X", Month, Year) + Convert.ToDouble(grvDR.Cells["NCDC"].Value))
                                .Equals(Convert.ToDouble(grvDR.Cells["NCQD"].Value)))
                        {
                            _ERROR = WriteERROR(_ERROR, -1);
                            _SP = $"Upd_H1_WorkdayCoefficientEmployeesFinal_WDStatus";
                            _SPValue =
                                $"UserId: {UserId}, DataDate: '{DataDate}', WDStatus: {2}, CheckRemark: {_ERROR}, WorkdayCoefficientEmployeeIdFinal: {WorkdayCoefficientEmployeeIdFinal}";
                            Utilities.Utilities.SaveHRMLog("H1_WorkdayCoefficientEmployeesFinal", _SP, _SPValue,
                                string.Empty);
                            WorkdayCoefficientEmployeesFinalBLL.UpdateWDStatus(UserId, DataDate, 2, _ERROR,
                                WorkdayCoefficientEmployeeIdFinal);
                        }
                        else
                        {
                            foreach (DataRow LeaveDR in LeaveRow.Rows)
                                foreach (
                                    var Date in
                                    GetDateRange(Convert.ToDateTime(LeaveDR["FromDate"]),
                                        Convert.ToDateTime(LeaveDR["ToDate"])))
                                {
                                    var LeaveCode =
                                        Constants.GetSymbolTimekeeping(int.Parse(LeaveDR["LeaveTypeId"].ToString()));
                                    if ((LeaveCode != "DD") || (LeaveCode != "CT") || (LeaveCode != "Ko") ||
                                        (LeaveCode != "Ho") || (LeaveCode != "H1") || (LeaveCode != "H2") ||
                                        (LeaveCode != "H3") || (LeaveCode != "H4") || (LeaveCode != "H5") ||
                                        (LeaveCode != "H6") || (LeaveCode != "H7") || (LeaveCode != "DC"))
                                        if (
                                            Convert.ToBoolean(
                                                EmployeesBLL.GetDataRowEmployeeById(UserId)["Direct"].ToString()))
                                        {
                                            if (grvDR.Cells["Day" + Date.Day].Value.ToString() == LeaveCode)
                                                Count++;
                                            else
                                                Count--;
                                        }
                                        else
                                        {
                                            if ((Date.DayOfWeek != DayOfWeek.Sunday) &&
                                                (Date.DayOfWeek != DayOfWeek.Saturday))
                                                if (grvDR.Cells["Day" + Date.Day].Value.ToString() == LeaveCode)
                                                    Count++;
                                                else
                                                    Count--;
                                        }
                                }
                        }
                    }
                    else
                    {
                        _ERROR = WriteERROR(_ERROR, Constants.ERROR_LEAVE_LEAVEWD_ID);
                        _SP = $"Upd_H1_WorkdayCoefficientEmployeesFinal_WDStatus";
                        _SPValue =
                            $"UserId: {UserId}, DateDate: '{DataDate}', WDStatus: {2}, CheckRemark: {_ERROR}, WorkdayCoefficientEmployeeIdFinal: {WorkdayCoefficientEmployeeIdFinal}";
                        Utilities.Utilities.SaveHRMLog("H1_WorkdayCoefficientEmployeesFinal", _SP, _SPValue,
                            string.Empty);
                        WorkdayCoefficientEmployeesFinalBLL.UpdateWDStatus(UserId, DataDate, 2,
                            _ERROR, WorkdayCoefficientEmployeeIdFinal);
                    }

                    if (Count == EmpLeave_Leave)
                    {
                        _ERROR = WriteERROR(_ERROR, -1);
                        _SP = $"Upd_H1_WorkdayCoefficientEmployeesFinal_WDStatus";
                        _SPValue =
                            $"UserId: {UserId}, DataDate: '{DataDate}', WDStatus: {1}, CheckRemark: {_ERROR}, WorkdayCoefficientEmployeeIdFinal: {WorkdayCoefficientEmployeeIdFinal}";
                        Utilities.Utilities.SaveHRMLog("H1_WorkdayCoefficientEmployeesFinal", _SP, _SPValue,
                            string.Empty);
                        WorkdayCoefficientEmployeesFinalBLL.UpdateWDStatus(UserId, DataDate, 1, _ERROR,
                            WorkdayCoefficientEmployeeIdFinal);
                    }
                    else
                    {
                        _ERROR = WriteERROR(_ERROR, Constants.ERROR_LEAVE_LEAVEWD_ID);
                        _SP = $"Upd_H1_WorkdayCoefficientEmployeesFinal_WDStatus";
                        _SPValue =
                            $"UserId: {UserId}, DataDate: '{DataDate}', WDStatus: {2}, CheckRemark: {_ERROR}, WorkdayCoefficientEmployeeIdFinal: {WorkdayCoefficientEmployeeIdFinal}";
                        Utilities.Utilities.SaveHRMLog("H1_WorkdayCoefficientEmployeesFinal", _SP, _SPValue,
                            string.Empty);
                        WorkdayCoefficientEmployeesFinalBLL.UpdateWDStatus(UserId, DataDate, 2,
                            _ERROR, WorkdayCoefficientEmployeeIdFinal);
                    }
                    var DaysInMonth = DateTime.DaysInMonth(Year, Month);
                    for (var ii = 1; ii <= DaysInMonth; ii++)
                        if ((grvDR.Cells["Day" + ii].Value != null) ||
                            (grvDR.Cells["Day" + ii].Value.ToString() != string.Empty) ||
                            (grvDR.Cells["Day" + ii].Value.ToString() != "--"))
                        {
                        }
                        else
                        {
                            _ERROR = WriteERROR(_ERROR, Constants.ERROR_WORKING_DAYS_ID);
                            _SP = $"Upd_H1_WorkdayCoefficientEmployeesFinal_WDStatus";
                            _SPValue =
                                $"UserId: {UserId}, DataDate: '{DataDate}', WDStatus: {2}, CheckRemark: {_ERROR}, WorkdayCoefficientEmployeeIdFinal: {WorkdayCoefficientEmployeeIdFinal}";
                            Utilities.Utilities.SaveHRMLog("H1_WorkdayCoefficientEmployeesFinal", _SP,
                                _SPValue, string.Empty);
                            WorkdayCoefficientEmployeesFinalBLL.UpdateWDStatus(UserId, DataDate, 2,
                                _ERROR,
                                WorkdayCoefficientEmployeeIdFinal);
                        }
                }

                else
                {
                    if ((GetDataByGridDR(grvDR, "TotalLeave", Month, Year) + Convert.ToDouble(grvDR.Cells["X"].Value) !=
                         Convert.ToDouble(grvDR.Cells["NCDC"].Value)) ||
                        (GetDataByGridDR(grvDR, "TotalLeave", Month, Year) > 0))
                    {
                        _ERROR = WriteERROR(_ERROR, Constants.ERROR_LEAVE_LEAVEWD_ID);
                        _SP = $"Upd_H1_WorkdayCoefficientEmployeesFinal_WDStatus";
                        _SPValue =
                            $"UserId: {UserId}, DataDate: '{DataDate}', WDStatus: {2}, CheckRemark: {_ERROR}, WorkdayCoefficientEmployeeIdFinal: {WorkdayCoefficientEmployeeIdFinal}";
                        Utilities.Utilities.SaveHRMLog("H1_WorkdayCoefficientEmployeesFinal", _SP, _SPValue,
                            string.Empty);
                        WorkdayCoefficientEmployeesFinalBLL.UpdateWDStatus(UserId, DataDate, 2,
                            _ERROR, WorkdayCoefficientEmployeeIdFinal);
                    }
                    else
                    {
                        _ERROR = WriteERROR(_ERROR, Constants.ERROR_LEAVE_LEAVEWD_ID);
                        _SP = $"Upd_H1_WorkdayCoefficientEmployeesFinal_WDStatus";
                        _SPValue =
                            $"UserId: {UserId}, DataDate: '{DataDate}', WDStatus: {1}, CheckRemark: {_ERROR}, WorkdayCoefficientEmployeeIdFinal: {WorkdayCoefficientEmployeeIdFinal}";
                        Utilities.Utilities.SaveHRMLog("H1_WorkdayCoefficientEmployeesFinal", _SP, _SPValue,
                            string.Empty);
                        WorkdayCoefficientEmployeesFinalBLL.UpdateWDStatus(UserId, DataDate, 1, _ERROR,
                            WorkdayCoefficientEmployeeIdFinal);
                    }
                }
            }

            else
            {
                _ERROR = WriteERROR(_ERROR, Constants.ERROR_WORKING_DAYS_ID);
                var WorkdayCoefficientEmployeeIdFinal =
                    Convert.ToInt32(grvDR.Cells["WorkdayCoefficientEmployeeIdFinal"].Value);
                _SP = $"Upd_H1_WorkdayCoefficientEmployeesFinal_WDStatus";
                _SPValue =
                    $"UserId: {UserId}, DataDate: '{DataDate}', WDStatus: {2}, CheckRemark: {_ERROR}, WorkdayCoefficientEmployeeIdFinal: {WorkdayCoefficientEmployeeIdFinal}";
                Utilities.Utilities.SaveHRMLog("H1_WorkdayCoefficientEmployeesFinal", _SP, _SPValue, string.Empty);
                WorkdayCoefficientEmployeesFinalBLL.UpdateWDStatus(UserId, DataDate, 2,
                    _ERROR, WorkdayCoefficientEmployeeIdFinal);
            }
        }

        public static int CountCompare(DataRow dr, string strCompare)
        {
            var ReturnValue = 0;
            for (var i = 1; i <= 31; i++)
                if (dr["Day" + i].Equals(strCompare))
                    ReturnValue++;
            return ReturnValue;
        }

        public static int CountCompareGridRow(GridViewRowInfo dr, string strCompare)
        {
            var ReturnValue = 0;
            for (var i = 1; i <= 31; i++)
                if (dr.Cells["Day" + i].Value.ToString().Trim().ToLower().Equals(strCompare.Trim().ToLower()))
                    ReturnValue++;
            return ReturnValue;
        }

        public static string WriteERROR(string error, int addstr)
        {
            var strReturn = string.Empty;
            if (error.Length == 0)
                strReturn = addstr.ToString();
            else
                strReturn = error + "," + Environment.NewLine + addstr;
            return strReturn;
        }

        public static string WriteStatus(string error, string addstr)
        {
            var strReturn = string.Empty;
            if (error.Length == 0)
                strReturn = addstr;
            else
                strReturn = error + "," + Environment.NewLine + addstr;
            return strReturn;
        }

        public static List<DateTime> getSundays(int Month, int Year)
        {
            var lstSundays = new List<DateTime>();
            var intMonth = Month;
            var intYear = Year;
            var ci = new CultureInfo("en-US");
            for (var i = 1; i <= ci.Calendar.GetDaysInMonth(intYear, intMonth); i++)
                if (new DateTime(intYear, intMonth, i).DayOfWeek == DayOfWeek.Sunday)
                    lstSundays.Add(new DateTime(intYear, intMonth, i));
            return lstSundays;
        }

        public static List<DateTime> getSaturdays(int Month, int Year)
        {
            var lstSundays = new List<DateTime>();
            var intMonth = Month;
            var intYear = Year;
            var ci = new CultureInfo("en-US");
            for (var i = 1; i <= ci.Calendar.GetDaysInMonth(intYear, intMonth); i++)
                if (new DateTime(intYear, intMonth, i).DayOfWeek == DayOfWeek.Saturday)
                    lstSundays.Add(new DateTime(intYear, intMonth, i));
            return lstSundays;
        }

        public static List<DateTime> getWeekend(int Month, int Year, int DirectWorking)
        {
            var lstSundays = new List<DateTime>();
            var intMonth = Month;
            var intYear = Year;
            var ci = new CultureInfo("en-US");
            for (var i = 1; i <= ci.Calendar.GetDaysInMonth(intYear, intMonth); i++)
                if (DirectWorking == 0)
                {
                    if ((new DateTime(intYear, intMonth, i).DayOfWeek == DayOfWeek.Saturday) ||
                        (new DateTime(intYear, intMonth, i).DayOfWeek == DayOfWeek.Sunday))
                        lstSundays.Add(new DateTime(intYear, intMonth, i));
                }
                else
                {
                    if (DirectWorking == 1)
                        if (new DateTime(intYear, intMonth, i).DayOfWeek == DayOfWeek.Sunday)
                            lstSundays.Add(new DateTime(intYear, intMonth, i));
                }
            return lstSundays;
        }
    }
}