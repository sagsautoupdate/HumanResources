using System;
using System.Collections.Generic;
using System.Data;
using HRMBLL.H0;
using HRMBLL.H1.Helper;
using HRMDAL.H1;
using HRMUtil;
using HRMUtil.KeyNames.H0;
using HRMUtil.KeyNames.H1;

namespace HRMBLL.H1
{
    public class WorkdayEmployeesBLLL
    {
        #region private fields

        #region TimeKeeping

        #endregion

        #region To Collect WorkdayEmplyee

        #endregion

        #endregion

        #region properties

        public long WorkdayEmployeeIdL { get; set; }

        public int UserId { get; set; }

        public DateTime WorkdayDateL { get; set; }

        public int TypeL { get; set; }

        public string EmployeeCode { get; set; }

        public string FullName { get; set; }

        public int RootId { get; set; }

        public string RootName { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentFullName { get; set; }

        public string PositionName { get; set; }

        #region TimeKeeping

        public string Day1L { get; set; } = string.Empty;

        public string Day2L { get; set; } = string.Empty;

        public string Day3L { get; set; } = string.Empty;

        public string Day4L { get; set; } = string.Empty;

        public string Day5L { get; set; } = string.Empty;

        public string Day6L { get; set; } = string.Empty;

        public string Day7L { get; set; } = string.Empty;

        public string Day8L { get; set; } = string.Empty;

        public string Day9L { get; set; } = string.Empty;

        public string Day10L { get; set; } = string.Empty;

        public string Day11L { get; set; } = string.Empty;

        public string Day12L { get; set; } = string.Empty;

        public string Day13L { get; set; } = string.Empty;

        public string Day14L { get; set; } = string.Empty;

        public string Day15L { get; set; } = string.Empty;

        public string Day16L { get; set; } = string.Empty;

        public string Day17L { get; set; } = string.Empty;

        public string Day18L { get; set; } = string.Empty;

        public string Day19L { get; set; } = string.Empty;

        public string Day20L { get; set; } = string.Empty;

        public string Day21L { get; set; } = string.Empty;

        public string Day22L { get; set; } = string.Empty;

        public string Day23L { get; set; } = string.Empty;

        public string Day24L { get; set; } = string.Empty;

        public string Day25L { get; set; } = string.Empty;

        public string Day26L { get; set; } = string.Empty;

        public string Day27L { get; set; } = string.Empty;

        public string Day28L { get; set; } = string.Empty;

        public string Day29L { get; set; } = string.Empty;

        public string Day30L { get; set; } = string.Empty;

        public string Day31L { get; set; } = string.Empty;

        #endregion

        #region To Collect WorkdayEmplyee

        public double XQDL { get; set; }

        public double XL { get; set; }

        public double F_OmL { get; set; }

        public double F_OmDaiNgayL { get; set; }

        public double F_ThaiSanL { get; set; }

        public double F_TNLDL { get; set; }

        public double F_NamL { get; set; }

        public double F_dbL { get; set; }

        public double F_KoLuongCLDL { get; set; }

        public double F_KoLuongKLDL { get; set; }

        public double F_DiDuongL { get; set; }

        public double F_CongTacL { get; set; }

        public double F_HocSAGSL { get; set; }

        public double F_Hoc1L { get; set; }

        public double F_Hoc2L { get; set; }

        public double F_Hoc3L { get; set; }

        public double F_Hoc4L { get; set; }

        public double F_Hoc5L { get; set; }

        public double F_Hoc6L { get; set; }

        public double F_Hoc7L { get; set; }

        public double F_Con_OmL { get; set; }

        public double F_KHHDSL { get; set; }

        public double F_SayThaiL { get; set; }

        public double F_KhamThaiL { get; set; }

        public double F_ConChetL { get; set; }

        public double F_DinhChiCongTacL { get; set; }

        public double F_TamHoanHDL { get; set; }

        public double F_HoiHopL { get; set; }

        public double F_LeL { get; set; }

        public double NghiTuanL { get; set; }

        public double NghiBuL { get; set; }

        public double NghiViecL { get; set; }

        public double ChuaDiLamL { get; set; }

        public int LeaveStatus { get; set; }

        public DateTime LeaveDate { get; set; }

        #endregion

        public double F_O_Co_KHHDS_TNLDL { get; set; }

        public double F_Nam_Fdb_DDL { get; set; }

        public double F_HocL { get; set; }

        public double F_KhacL { get; set; }

        public double NghiTuan_NghiBuL { get; set; }

        public double NightTimeL { get; set; }

        public double MarkL { get; set; }

        public string RankL { get; set; }

        public DateTime CreateDateL { get; set; }

        public int CreateUserL { get; set; }

        public string CreateUserNameL { get; set; }

        public string CreateFullNameL { get; set; }

        public DateTime LastUpdateL { get; set; }

        public int UpdateUserL { get; set; }

        public string UpdateUserNameL { get; set; }

        public string UpdateFullNameL { get; set; }

        public string RemarkL { get; set; }

        public int StatusL { get; set; }

        #endregion

        #region public methods insert, update, delete

        public static long InsertByDate_DeptId(string deptIds, DateTime workdayDateL, int createUserL,
            bool isOfficeHours, int statusL)
        {
            var dayInMonth = DateTime.DaysInMonth(workdayDateL.Year, workdayDateL.Month);
            string day25L = null;
            string day26L = null;
            string day27L = null;
            string day28L = null;
            string day29L = null;
            string day30L = null;
            string day31L = null;
            if (dayInMonth == 25)
            {
                day25L = "X";
            }
            else if (dayInMonth == 26)
            {
                day25L = "X";
                day26L = "X";
            }
            else if (dayInMonth == 27)
            {
                day25L = "X";
                day26L = "X";
                day27L = "X";
            }
            else if (dayInMonth == 28)
            {
                day25L = "X";
                day26L = "X";
                day27L = "X";
                day28L = "X";
            }
            else if (dayInMonth == 29)
            {
                day25L = "X";
                day26L = "X";
                day27L = "X";
                day28L = "X";
                day29L = "X";
            }
            else if (dayInMonth == 30)
            {
                day25L = "X";
                day26L = "X";
                day27L = "X";
                day28L = "X";
                day29L = "X";
                day30L = "X";
            }
            else
            {
                day25L = "X";
                day26L = "X";
                day27L = "X";
                day28L = "X";
                day29L = "X";
                day30L = "X";
                day31L = "X";
            }
            var XQDL = DefaultValues.XQDSalary(workdayDateL.Month, workdayDateL.Year);
            var typeL = 1;
            if (!isOfficeHours)
                typeL = 0;


            new WorkdayEmployeesDALL().InsertByDate_DeptId(day25L, day26L, day27L, day28L, day29L, day30L, day31L,
                deptIds, workdayDateL, createUserL, XQDL, XQDL, typeL, statusL);

            var listW = GetByFilter(string.Empty, deptIds, workdayDateL.Month, workdayDateL.Year, 1);

            foreach (var objWE in listW)
                if (isOfficeHours)
                {
                    UpdateWorkdayEmployeeByOfficeHours(objWE);
                }
                else
                {
                    var x = DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L,
                        objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L,
                        objWE.Day12L, objWE.Day13L,
                        objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L,
                        objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                        objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_X);

                    objWE.XL = x;
                    objWE.UpdateByDate_UserId();
                }

            return 0;
        }

        public static long InsertByDate_UserId(int userId, DateTime workdayDateL, int createUserL, bool isOfficeHours,
            int statusL)
        {
            var daysInMonth = DateTime.DaysInMonth(workdayDateL.Year, workdayDateL.Month);
            string day25L = null;
            string day26L = null;
            string day27L = null;
            string day28L = null;
            string day29L = null;
            string day30L = null;
            string day31L = null;
            if (daysInMonth == 25)
            {
                day25L = "X";
            }
            else if (daysInMonth == 26)
            {
                day25L = "X";
                day26L = "X";
            }
            else if (daysInMonth == 27)
            {
                day25L = "X";
                day26L = "X";
                day27L = "X";
            }
            else if (daysInMonth == 28)
            {
                day25L = "X";
                day26L = "X";
                day27L = "X";
                day28L = "X";
            }
            else if (daysInMonth == 29)
            {
                day25L = "X";
                day26L = "X";
                day27L = "X";
                day28L = "X";
                day29L = "X";
            }
            else if (daysInMonth == 30)
            {
                day25L = "X";
                day26L = "X";
                day27L = "X";
                day28L = "X";
                day29L = "X";
                day30L = "X";
            }
            else
            {
                day25L = "X";
                day26L = "X";
                day27L = "X";
                day28L = "X";
                day29L = "X";
                day30L = "X";
                day31L = "X";
            }
            var XQDL = DefaultValues.XQDSalary(workdayDateL.Month, workdayDateL.Year);
            var typeL = 1;
            if (!isOfficeHours)
                typeL = 0;

            return new WorkdayEmployeesDALL().InsertByDate_UserId(userId, workdayDateL, day25L, day26L, day27L, day28L,
                day29L, day30L, day31L, createUserL, XQDL, XQDL, typeL, statusL);
        }

        public static long UpdateByDate_UserId(
            int userId,
            DateTime workdayDate,
            string day1L, string day2L, string day3L, string day4L, string day5L, string day6L, string day7L,
            string day8L, string day9L, string day10L,
            string day11L, string day12L, string day13L, string day14L, string day15L, string day16L, string day17L,
            string day18L, string day19L, string day20L,
            string day21L, string day22L, string day23L, string day24L, string day25L, string day26L, string day27L,
            string day28L, string day29L, string day30L, string day31L,
            double XL,
            double f_OmL, double f_OmDaiNgayL, double f_ThaiSanL, double f_TNLDL, double f_NamL,
            double f_dbL, double f_KoLuongCLDL, double f_KoLuongKLDL, double f_DiDuongL, double f_CongTacL,
            double f_HocSAGS, double f_Hoc1L, double f_Hoc2L, double f_Hoc3L, double f_Hoc4L, double f_Hoc5L,
            double f_Hoc6L, double f_Hoc7L,
            double f_Con_OmL, double f_KHHDSL, double f_SayThaiL, double f_KhamThaiL, double f_ConChetL,
            double f_DinhChiCongTacL,
            double f_TamHoanHDL, double f_HoiHopL, double f_LeL, double nghiTuanL, double nghiBuL, double nghiViecL,
            double chuaDiLamL,
            double nightTimeL, double markL, string rankL, DateTime lastUpdateL, double updateUserL, string remarkL
        )
        {
            return new WorkdayEmployeesDALL().UpdateByDate_UserId(
                userId, workdayDate,
                day1L, day2L, day3L, day4L, day5L, day6L, day7L, day8L, day9L, day10L,
                day11L, day12L, day13L, day14L, day15L, day16L, day17L, day18L, day19L, day20L,
                day21L, day22L, day23L, day24L, day25L, day26L, day27L, day28L, day29L, day30L, day31L,
                XL,
                f_OmL, f_OmDaiNgayL, f_ThaiSanL, f_TNLDL, f_NamL,
                f_dbL, f_KoLuongCLDL, f_KoLuongKLDL, f_DiDuongL, f_CongTacL,
                f_HocSAGS, f_Hoc1L, f_Hoc2L, f_Hoc3L, f_Hoc4L, f_Hoc5L, f_Hoc6L, f_Hoc7L,
                f_Con_OmL, f_KHHDSL, f_SayThaiL, f_KhamThaiL, f_ConChetL, f_DinhChiCongTacL,
                f_TamHoanHDL, f_HoiHopL, f_LeL, nghiTuanL, nghiBuL, nghiViecL, chuaDiLamL,
                nightTimeL, markL, rankL, lastUpdateL, updateUserL, remarkL
            );
        }

        public long UpdateByDate_UserId()
        {
            return UpdateByDate_UserId(UserId, WorkdayDateL,
                Day1L, Day2L, Day3L, Day4L, Day5L, Day6L, Day7L, Day8L, Day9L, Day10L,
                Day11L, Day12L, Day13L, Day14L, Day15L, Day16L, Day17L, Day18L, Day19L, Day20L,
                Day21L, Day22L, Day23L, Day24L, Day25L, Day26L, Day27L, Day28L, Day29L, Day30L, Day31L,
                MarkL, NightTimeL, LastUpdateL, UpdateUserL, RemarkL);
        }

        public static long UpdateByDate_UserId(
            int userId,
            DateTime workdayDate,
            string day1L, string day2L, string day3L, string day4L, string day5L, string day6L, string day7L,
            string day8L, string day9L, string day10L,
            string day11L, string day12L, string day13L, string day14L, string day15L, string day16L, string day17L,
            string day18L, string day19L, string day20L,
            string day21L, string day22L, string day23L, string day24L, string day25L, string day26L, string day27L,
            string day28L, string day29L, string day30L, string day31L,
            double mark, double nightTime, DateTime lastUpdateL, double updateUserL, string remarkL
        )
        {
            double XL = Constants.WorkdayEmployee_DefaultValue,
                f_OmL = Constants.WorkdayEmployee_DefaultValue,
                f_OmDaiNgayL = Constants.WorkdayEmployee_DefaultValue;
            double f_ThaiSanL = Constants.WorkdayEmployee_DefaultValue,
                f_TNLDL = Constants.WorkdayEmployee_DefaultValue,
                f_NamL = Constants.WorkdayEmployee_DefaultValue;
            double f_dbL = Constants.WorkdayEmployee_DefaultValue,
                f_KoLuongCLDL = Constants.WorkdayEmployee_DefaultValue,
                f_KoLuongKLDL = Constants.WorkdayEmployee_DefaultValue;
            double f_DiDuongL = Constants.WorkdayEmployee_DefaultValue,
                f_CongTacL = Constants.WorkdayEmployee_DefaultValue;

            double f_HocSAGSL = Constants.WorkdayEmployee_DefaultValue,
                f_Hoc1L = Constants.WorkdayEmployee_DefaultValue,
                f_Hoc2L = Constants.WorkdayEmployee_DefaultValue,
                f_Hoc3L = Constants.WorkdayEmployee_DefaultValue;
            double f_Hoc4L = Constants.WorkdayEmployee_DefaultValue,
                f_Hoc5L = Constants.WorkdayEmployee_DefaultValue,
                f_Hoc6L = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc7L = Constants.WorkdayEmployee_DefaultValue;

            double f_Con_OmL = Constants.WorkdayEmployee_DefaultValue, f_KHHDSL = Constants.WorkdayEmployee_DefaultValue;
            double f_SayThaiL = Constants.WorkdayEmployee_DefaultValue,
                f_KhamThaiL = Constants.WorkdayEmployee_DefaultValue,
                f_ConChetL = Constants.WorkdayEmployee_DefaultValue;
            double f_DinhChiCongTacL = Constants.WorkdayEmployee_DefaultValue,
                f_TamHoanHDL = Constants.WorkdayEmployee_DefaultValue,
                f_HoiHopL = Constants.WorkdayEmployee_DefaultValue;
            double f_LeL = Constants.WorkdayEmployee_DefaultValue,
                nghiTuanL = Constants.WorkdayEmployee_DefaultValue,
                nghiBuL = Constants.WorkdayEmployee_DefaultValue;
            var nghiViecL = Constants.WorkdayEmployee_DefaultValue;
            var chuaDiLamL = Constants.WorkdayEmployee_DefaultValue;


            f_OmL = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L, day3L,
                day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_O_BAN_THAN);

            f_OmDaiNgayL = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L,
                day3L, day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_O_DAI_NGAY);

            f_ThaiSanL = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L,
                day3L, day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_THAI_SAN);

            f_TNLDL = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L, day3L,
                day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_TNLD);

            f_NamL = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L, day3L,
                day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_F_NAM);

            f_dbL = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L, day3L,
                day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_FDB);

            f_KoLuongCLDL = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L,
                day3L, day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO);

            f_KoLuongKLDL = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L,
                day3L, day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO);

            f_DiDuongL = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L,
                day3L, day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_F_DI_DUONG);

            f_CongTacL = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L,
                day3L, day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_F_CONG_TAC);

            f_HocSAGSL = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L,
                day3L, day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_HOC_SAGS);
            f_Hoc1L = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L, day3L,
                day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_HOC_1);
            f_Hoc2L = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L, day3L,
                day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_HOC_2);
            f_Hoc3L = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L, day3L,
                day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_HOC_3);
            f_Hoc4L = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L, day3L,
                day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_HOC_4);
            f_Hoc5L = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L, day3L,
                day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_HOC_5);
            f_Hoc6L = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L, day3L,
                day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_HOC_6);
            f_Hoc7L = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L, day3L,
                day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_HOC_7);
            f_Con_OmL = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L,
                day3L, day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_CON_OM);
            f_KHHDSL = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L, day3L,
                day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_KHHDS);
            f_SayThaiL = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L,
                day3L, day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_SAY_THAI);
            f_KhamThaiL = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L,
                day3L, day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_KHAM_THAI);
            f_ConChetL = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L,
                day3L, day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_CON_CHET_SAU_KHI_SINH);
            f_DinhChiCongTacL = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L,
                day2L, day3L, day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_DINH_CHI_CONG_TAC);
            f_TamHoanHDL = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L,
                day3L, day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_TAM_HOAN_HOP_DONG);
            f_HoiHopL = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L,
                day3L, day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_HOI_HOP);

            f_LeL = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L, day3L,
                day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_LE_TET);

            nghiTuanL = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L,
                day3L, day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_NGHI_TUAN);

            nghiBuL = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L, day3L,
                day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_NGHI_BU);

            nghiViecL = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L,
                day3L, day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_NGHI_VIEC);

            chuaDiLamL = DefaultValues.CalculateLeaveDayL(workdayDate.Month, workdayDate.Year, userId, day1L, day2L,
                day3L, day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L, Constants.LEAVE_TYPE_CHUA_DI_LAM);

            var hoc = f_HocSAGSL + f_Hoc1L + f_Hoc2L + f_Hoc3L + f_Hoc4L + f_Hoc5L + f_Hoc6L + f_Hoc7L;
            var om = f_OmL + f_OmDaiNgayL + f_Con_OmL;
            var thaisan = f_ThaiSanL + f_SayThaiL + f_KhamThaiL + f_ConChetL + f_KHHDSL;
            var phep = f_NamL + f_dbL + f_TNLDL + f_HoiHopL + f_CongTacL + f_DiDuongL;
            var khac = f_KoLuongCLDL + f_KoLuongKLDL + f_DinhChiCongTacL + f_TamHoanHDL + chuaDiLamL + nghiViecL;

            var totalLeave = hoc + om + thaisan + phep + khac;
            var XQDL = DefaultValues.XQDSalary(workdayDate.Month, workdayDate.Year);

            var daysInNow = DateTime.DaysInMonth(workdayDate.Year, workdayDate.Month);

            XL = daysInNow - (nghiTuanL + totalLeave);

            var hTCV = DefaultValues.HTCV(mark);

            return UpdateByDate_UserId(userId, workdayDate,
                day1L, day2L, day3L, day4L, day5L, day6L, day7L, day8L, day9L, day10L, day11L, day12L, day13L,
                day14L, day15L, day16L, day17L, day18L, day19L, day20L, day21L, day22L, day23L, day24L, day25L, day26L,
                day27L, day28L, day29L, day30L, day31L,
                XL, f_OmL, f_OmDaiNgayL, f_ThaiSanL, f_TNLDL, f_NamL, f_dbL, f_KoLuongCLDL,
                f_KoLuongKLDL, f_DiDuongL, f_CongTacL, f_HocSAGSL, f_Hoc1L, f_Hoc2L, f_Hoc3L, f_Hoc4L, f_Hoc5L, f_Hoc6L,
                f_Hoc7L, f_Con_OmL,
                f_KHHDSL, f_SayThaiL, f_KhamThaiL, f_ConChetL, f_DinhChiCongTacL, f_TamHoanHDL, f_HoiHopL,
                f_LeL, nghiTuanL, nghiBuL, nghiViecL, chuaDiLamL, nightTime, mark, hTCV, lastUpdateL, updateUserL,
                remarkL
            );
        }

        public static void UpdateWorkdayByLeave(int userId, DateTime fromDate, DateTime toDate, int leaveTypeId,
            int status)
        {
            var symbolTimekeeping = Constants.GetSymbolTimekeeping(leaveTypeId);
            var objWE = GetByUserId_Month_Year(userId, fromDate.Month, fromDate.Year, status);

            if (objWE != null)
            {
                var dateTemp = fromDate;

                #region while

                while (dateTemp.CompareTo(toDate) <= 0)
                {
                    switch (dateTemp.Day)
                    {
                        case 1:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day1L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day1L = symbolTimekeeping;
                            break;
                        case 2:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day2L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day2L = symbolTimekeeping;
                            break;
                        case 3:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day3L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day3L = symbolTimekeeping;
                            break;
                        case 4:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day4L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day4L = symbolTimekeeping;
                            break;
                        case 5:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day5L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day5L = symbolTimekeeping;
                            break;
                        case 6:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day6L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day6L = symbolTimekeeping;
                            break;
                        case 7:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day7L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day7L = symbolTimekeeping;
                            break;
                        case 8:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day8L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day8L = symbolTimekeeping;
                            break;
                        case 9:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day9L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day9L = symbolTimekeeping;
                            break;
                        case 10:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day10L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day10L = symbolTimekeeping;
                            break;
                        case 11:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day11L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day11L = symbolTimekeeping;
                            break;
                        case 12:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day12L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day12L = symbolTimekeeping;
                            break;
                        case 13:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day13L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day13L = symbolTimekeeping;
                            break;
                        case 14:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day14L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day14L = symbolTimekeeping;
                            break;
                        case 15:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day15L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day15L = symbolTimekeeping;
                            break;
                        case 16:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day16L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day16L = symbolTimekeeping;
                            break;
                        case 17:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day17L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day17L = symbolTimekeeping;
                            break;
                        case 18:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day18L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day18L = symbolTimekeeping;
                            break;
                        case 19:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day19L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day19L = symbolTimekeeping;
                            break;
                        case 20:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day20L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day20L = symbolTimekeeping;
                            break;
                        case 21:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day21L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day21L = symbolTimekeeping;
                            break;
                        case 22:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day22L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day22L = symbolTimekeeping;
                            break;
                        case 23:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day23L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day23L = symbolTimekeeping;
                            break;
                        case 24:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day24L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day24L = symbolTimekeeping;
                            break;
                        case 25:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day25L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day25L = symbolTimekeeping;
                            break;
                        case 26:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day26L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day26L = symbolTimekeeping;
                            break;
                        case 27:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day27L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day27L = symbolTimekeeping;
                            break;
                        case 28:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day28L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day28L = symbolTimekeeping;
                            break;
                        case 29:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day29L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day29L = symbolTimekeeping;
                            break;
                        case 30:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day30L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day30L = symbolTimekeeping;
                            break;
                        case 31:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day31L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day31L = symbolTimekeeping;
                            break;
                    }
                    dateTemp = dateTemp.AddDays(1);
                }

                #endregion

                ////////////////////////////////////////////////
                //objWE.F_OmL = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_O_BAN_THAN);

                //objWE.F_OmDaiNgayL = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_O_DAI_NGAY);

                //objWE.F_ThaiSanL = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_THAI_SAN);

                //objWE.F_TNLDL = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_TNLD);

                //objWE.F_NamL = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_F_NAM);

                //objWE.F_dbL = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_FDB);

                //objWE.F_KoLuongCLDL = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO);

                //objWE.F_KoLuongKLDL = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO);

                //objWE.F_DiDuongL = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_F_DI_DUONG);

                //objWE.F_CongTacL = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_F_CONG_TAC);

                //objWE.F_Hoc1L = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_HOC_1);
                //objWE.F_Hoc2L = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_HOC_2);
                //objWE.F_Hoc3L = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_HOC_3);
                //objWE.F_Hoc4L = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_HOC_4);
                //objWE.F_Hoc5L = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_HOC_5);
                //objWE.F_Hoc6L = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_HOC_6);
                //objWE.F_Hoc7L = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_HOC_7);

                //objWE.F_Con_OmL = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_CON_OM);

                //objWE.F_KHHDSL = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_KHHDS);

                //objWE.F_SayThaiL = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_SAY_THAI);

                //objWE.F_KhamThaiL = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_KHAM_THAI);

                //objWE.F_ConChetL = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_CON_CHET_SAU_KHI_SINH);

                //objWE.F_DinhChiCongTacL = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_DINH_CHI_CONG_TAC);

                //objWE.F_TamHoanHDL = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_TAM_HOAN_HOP_DONG);

                //objWE.F_HoiHopL = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_HOI_HOP);

                //objWE.F_LeL = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_LE_TET);

                //objWE.NghiTuanL = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_NGHI_TUAN);

                //objWE.NghiBuL = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_NGHI_BU);

                //objWE.NghiViecL = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_NGHI_VIEC);

                //objWE.XL = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDayL(objWE.Day1L, objWE.Day2L, objWE.Day3L, objWE.Day4L, objWE.Day5L, objWE.Day6L, objWE.Day7L, objWE.Day8L, objWE.Day9L, objWE.Day10L, objWE.Day11L, objWE.Day12L, objWE.Day13L,
                //    objWE.Day14L, objWE.Day15L, objWE.Day16L, objWE.Day17L, objWE.Day18L, objWE.Day19L, objWE.Day20L, objWE.Day21L, objWE.Day22L, objWE.Day23L, objWE.Day24L, objWE.Day25L, objWE.Day26L,
                //    objWE.Day27L, objWE.Day28L, objWE.Day29L, objWE.Day30L, objWE.Day31L, Constants.LEAVE_TYPE_X);               

                objWE.UpdateByDate_UserId();
                ///////////////////////////////////////////////
            }
        }

        public static long Update_By_MarkHTCV(int userId, double markL, DateTime lastUpdateL, int updateUserL,
            int monthL, int yearL)
        {
            return new WorkdayEmployeesDALL().Update_By_MarkHTCV(userId, markL, lastUpdateL, updateUserL, monthL, yearL);
        }

        public static long DeleteByDeptIdsDate(string deptIds, int month, int year)
        {
            return new WorkdayEmployeesDALL().DeleteByDeptIdsDate(deptIds, month, year);
        }

        #endregion

        #region public methods GET

        public static List<WorkdayEmployeesBLLL> GetByFilter(string fullName, string departmentIds, int month, int year,
            int typeSort)
        {
            return
                GenerateListWorkdayEmployeesBLLLFromDataTable(new WorkdayEmployeesDALL().GetByFilter(fullName,
                    departmentIds, month, year, typeSort));
        }

        public static WorkdayEmployeesBLLL GetById(long workdayEmployeeIdL)
        {
            var list =
                GenerateListWorkdayEmployeesBLLLFromDataTable(new WorkdayEmployeesDALL().GetById(workdayEmployeeIdL));

            if (list.Count > 0)
                return list[0];
            return null;
        }


        public static WorkdayEmployeesBLLL GetByUserId_Month_Year(int userId, int month, int year, int statusL)
        {
            var list =
                GenerateListWorkdayEmployeesBLLLFromDataTable(new WorkdayEmployeesDALL().GetByUserId_Month_Year(userId,
                    month, year, statusL));

            if (list.Count > 0)
                return list[0];
            return null;
        }

        public static List<WorkdayEmployeesBLLL> GetByRoot(int rootId, int month, int year)
        {
            return
                GenerateListWorkdayEmployeesBLLLFromDataTable(new WorkdayEmployeesDALL().GetByRootId(rootId, month, year));
        }

        #endregion

        #region private methods

        private static List<WorkdayEmployeesBLLL> GenerateListWorkdayEmployeesBLLLFromDataTable(DataTable dt)
        {
            var list = new List<WorkdayEmployeesBLLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateWorkdayEmployeesBLLLFromDataRow(dr));

            return list;
        }

        private static WorkdayEmployeesBLLL GenerateWorkdayEmployeesBLLLFromDataRow(DataRow dr)
        {
            var objBLL = new WorkdayEmployeesBLLL();

            objBLL.WorkdayEmployeeIdL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_IDL] == DBNull.Value
                ? 0
                : int.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_IDL].ToString());
            objBLL.UserId = dr[EmployeeKeys.FIELD_EMPLOYEES_USERID] == DBNull.Value
                ? 0
                : int.Parse(dr[EmployeeKeys.FIELD_EMPLOYEES_USERID].ToString());
            objBLL.WorkdayDateL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_WorkdayDateL] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_WorkdayDateL].ToString());

            try
            {
                objBLL.EmployeeCode = dr[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE].ToString();
            }
            catch
            {
            }
            objBLL.FullName = dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                ? string.Empty
                : dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();
            objBLL.DepartmentId = dr[DepartmentKeys.FIELD_DEPARTMENT_ID] == DBNull.Value
                ? 0
                : int.Parse(dr[DepartmentKeys.FIELD_DEPARTMENT_ID].ToString());
            objBLL.DepartmentName = dr[DepartmentKeys.FIELD_DEPARTMENT_NAME] == DBNull.Value
                ? string.Empty
                : dr[DepartmentKeys.FIELD_DEPARTMENT_NAME].ToString();
            objBLL.DepartmentFullName = dr[DepartmentKeys.FIELD_DEPARTMENT_DEPARTMENTFULLNAME] == DBNull.Value
                ? string.Empty
                : dr[DepartmentKeys.FIELD_DEPARTMENT_DEPARTMENTFULLNAME].ToString();
            objBLL.RootId = dr[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID] == DBNull.Value
                ? 0
                : int.Parse(dr[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID].ToString());
            try
            {
                objBLL.RootName = dr[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME].ToString();
            }
            catch
            {
            }
            try
            {
                objBLL.PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[PositionKeys.FIELD_POSITION_NAME].ToString();
            }
            catch
            {
            }

            #region TimeKeeping

            objBLL.Day1L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day1L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day1L].ToString();
            objBLL.Day2L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day2L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day2L].ToString();
            objBLL.Day3L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day3L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day3L].ToString();
            objBLL.Day4L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day4L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day4L].ToString();
            objBLL.Day5L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day5L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day5L].ToString();
            objBLL.Day6L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day6L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day6L].ToString();
            objBLL.Day7L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day7L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day7L].ToString();
            objBLL.Day8L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day8L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day8L].ToString();
            objBLL.Day9L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day9L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day9L].ToString();
            objBLL.Day10L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day10L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day10L].ToString();
            objBLL.Day11L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day11L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day11L].ToString();
            objBLL.Day12L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day12L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day12L].ToString();
            objBLL.Day13L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day13L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day13L].ToString();
            objBLL.Day14L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day14L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day14L].ToString();
            objBLL.Day15L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day15L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day15L].ToString();
            objBLL.Day16L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day16L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day16L].ToString();
            objBLL.Day17L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day17L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day17L].ToString();
            objBLL.Day18L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day18L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day18L].ToString();
            objBLL.Day19L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day19L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day19L].ToString();
            objBLL.Day20L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day20L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day20L].ToString();
            objBLL.Day21L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day21L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day21L].ToString();
            objBLL.Day22L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day22L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day22L].ToString();
            objBLL.Day23L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day23L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day23L].ToString();
            objBLL.Day24L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day24L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day24L].ToString();
            objBLL.Day25L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day25L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day25L].ToString();
            objBLL.Day26L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day26L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day26L].ToString();
            objBLL.Day27L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day27L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day27L].ToString();
            objBLL.Day28L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day28L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day28L].ToString();
            objBLL.Day29L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day29L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day29L].ToString();
            objBLL.Day30L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day30L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day30L].ToString();
            objBLL.Day31L = dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day31L] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_Workday_Employee_Day31L].ToString();

            #endregion

            #region To Collect WorkdayEmplyee

            objBLL.XQDL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_XQDL] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_XQDL].ToString());

            objBLL.XL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_XL] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_XL].ToString());

            objBLL.F_OmL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_OmL] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_OmL].ToString());
            objBLL.F_OmDaiNgayL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_OM_DaiNgayL] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_OM_DaiNgayL].ToString());
            objBLL.F_ThaiSanL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_ThaiSanL] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_ThaiSanL].ToString());
            objBLL.F_TNLDL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_TNLDL] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_TNLDL].ToString());
            objBLL.F_NamL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_NamL] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_NamL].ToString());

            objBLL.F_dbL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_dbL] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_dbL].ToString());
            objBLL.F_KoLuongCLDL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KoLuongCLDL] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KoLuongCLDL].ToString());
            objBLL.F_KoLuongKLDL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KoLuongKLDL] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KoLuongKLDL].ToString());
            objBLL.F_DiDuongL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_DiDuongL] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_DiDuongL].ToString());
            objBLL.F_CongTacL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_CongTacL] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_CongTacL].ToString());

            objBLL.F_HocSAGSL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_HocSAGSL] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_HocSAGSL].ToString());
            objBLL.F_Hoc1L = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc1L] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc1L].ToString());
            objBLL.F_Hoc2L = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc2L] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc2L].ToString());
            objBLL.F_Hoc3L = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc3L] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc3L].ToString());
            objBLL.F_Hoc4L = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc4L] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc4L].ToString());
            objBLL.F_Hoc5L = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc5L] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc5L].ToString());
            objBLL.F_Hoc6L = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc6L] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc6L].ToString());
            objBLL.F_Hoc7L = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc7L] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Hoc7L].ToString());


            objBLL.F_Con_OmL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Con_OmL] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_Con_OmL].ToString());
            objBLL.F_KHHDSL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KHHDSL] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KHHDSL].ToString());
            objBLL.F_SayThaiL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_SayThaiL] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_SayThaiL].ToString());
            objBLL.F_KhamThaiL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KhamThaiL] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_KhamThaiL].ToString());
            objBLL.F_ConChetL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_ConChetL] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_ConChetL].ToString());
            objBLL.F_DinhChiCongTacL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_DinhChiCongTacL] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_DinhChiCongTacL].ToString());

            objBLL.F_TamHoanHDL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_TamHoanHDL] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_TamHoanHDL].ToString());
            objBLL.F_HoiHopL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_HoiHopL] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_HoiHopL].ToString());
            objBLL.F_LeL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_LEL] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_LEL].ToString());
            objBLL.NghiTuanL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_NghiTuanL] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_NghiTuanL].ToString());
            objBLL.NghiBuL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_NghiBuL] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_NghiBuL].ToString());
            objBLL.NghiViecL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_NghiViecL] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_NghiViecL].ToString());
            objBLL.ChuaDiLamL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_ChuaDilam] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_F_ChuaDilam].ToString());

            #endregion

            objBLL.NightTimeL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_NightTimeL] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_NightTimeL].ToString());
            objBLL.MarkL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_MarkL] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_MarkL].ToString());
            objBLL.RankL = DefaultValues.HTCV(objBLL.MarkL);
            //dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_RankL] == DBNull.Value ? string.Empty : dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_RankL].ToString();

            objBLL.CreateDateL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_CreateDateL] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_CreateDateL].ToString());
            objBLL.CreateUserL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_CreateUserL] == DBNull.Value
                ? 0
                : int.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_CreateUserL].ToString());
            objBLL.CreateUserNameL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_CreateUserNameL] == DBNull.Value
                ? ""
                : dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_CreateUserNameL].ToString();
            objBLL.CreateFullNameL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_CreateFullNameL] == DBNull.Value
                ? ""
                : dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_CreateFullNameL].ToString();

            objBLL.LastUpdateL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_LastUpdateL] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_LastUpdateL].ToString());
            objBLL.UpdateUserL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_UpdateUserL] == DBNull.Value
                ? 0
                : int.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_UpdateUserL].ToString());
            objBLL.UpdateUserNameL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_UpdateUserNameL] == DBNull.Value
                ? ""
                : dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_UpdateUserNameL].ToString();
            objBLL.UpdateFullNameL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_UpdateFullNameL] == DBNull.Value
                ? ""
                : dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_UpdateFullNameL].ToString();

            objBLL.TypeL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_TypeL] == DBNull.Value
                ? 0
                : int.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_TypeL].ToString());


            objBLL.F_O_Co_KHHDS_TNLDL = objBLL.F_OmL + objBLL.F_Con_OmL + objBLL.F_KHHDSL + objBLL.F_TNLDL;
            objBLL.F_Nam_Fdb_DDL = objBLL.F_NamL + objBLL.F_dbL + objBLL.F_DiDuongL;
            objBLL.F_HocL = objBLL.F_HocSAGSL + objBLL.F_Hoc1L + objBLL.F_Hoc2L + objBLL.F_Hoc3L + objBLL.F_Hoc4L +
                            objBLL.F_Hoc5L + objBLL.F_Hoc6L + objBLL.F_Hoc7L;
            objBLL.F_KhacL = objBLL.F_LeL + objBLL.F_KoLuongCLDL + objBLL.F_KoLuongKLDL + objBLL.ChuaDiLamL +
                             objBLL.F_SayThaiL;

            objBLL.NghiTuan_NghiBuL = objBLL.NghiTuanL + objBLL.NghiBuL;

            objBLL.RemarkL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_RemarkL] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_RemarkL].ToString();

            objBLL.StatusL = dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_StatusL] == DBNull.Value
                ? 0
                : int.Parse(dr[WorkdayEmployeeLKeys.Field_WorkdayEmployee_StatusL].ToString());

            try
            {
                objBLL.LeaveStatus = dr["LeaveStatus"] == DBNull.Value ? 0 : int.Parse(dr["LeaveStatus"].ToString());
            }
            catch
            {
            }
            try
            {
                objBLL.LeaveDate = dr["LeaveDate"] == DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(dr["LeaveDate"].ToString());
            }
            catch
            {
            }


            return objBLL;
        }


        public static void UpdateWorkdayEmployeeByOfficeHours(WorkdayEmployeesBLLL objWE)
        {
            if (objWE != null)
            {
                DateTime dt;
                var daysInNow = DateTime.DaysInMonth(objWE.WorkdayDateL.Year, objWE.WorkdayDateL.Month);
                var i = 1;
                while (i <= daysInNow)
                {
                    dt = new DateTime(objWE.WorkdayDateL.Year, objWE.WorkdayDateL.Month, i);
                    switch (i)
                    {
                        case 1:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day1L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 2:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day2L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 3:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day3L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 4:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day4L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 5:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day5L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 6:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day6L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 7:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day7L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 8:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day8L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 9:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day9L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 10:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day10L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 11:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day11L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 12:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day12L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 13:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day13L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 14:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day14L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 15:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day15L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 16:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day16L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 17:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day17L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 18:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day18L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 19:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day19L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 20:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day20L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 21:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day21L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 22:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day22L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 23:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day23L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 24:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day24L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 25:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day25L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 26:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day26L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 27:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day27L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 28:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day28L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 29:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day29L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 30:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day30L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 31:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day31L = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                    }

                    i++;
                }
                ////////////////////////////////////////////////                                           
                objWE.UpdateByDate_UserId();
                ///////////////////////////////////////////////
            }
        }

        private static void UpdateWorkdayEmployeeByHoliday(WorkdayEmployeesBLLL objWE)
        {
            if (objWE != null)
            {
                var listHoliday = HolidaysBLL.GetByDate(objWE.WorkdayDateL.Month, objWE.WorkdayDateL.Year);

                foreach (var obj in listHoliday)
                    switch (obj.HolidayDate.Day)
                    {
                        case 1:
                            objWE.Day1L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 2:
                            objWE.Day2L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 3:
                            objWE.Day3L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 4:
                            objWE.Day4L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 5:
                            objWE.Day5L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 6:
                            objWE.Day6L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 7:
                            objWE.Day7L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 8:
                            objWE.Day8L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 9:
                            objWE.Day9L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 10:
                            objWE.Day10L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 11:
                            objWE.Day11L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 12:
                            objWE.Day12L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 13:
                            objWE.Day13L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 14:
                            objWE.Day14L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 15:
                            objWE.Day15L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 16:
                            objWE.Day16L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 17:
                            objWE.Day17L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 18:
                            objWE.Day18L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 19:
                            objWE.Day19L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 20:
                            objWE.Day20L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 21:
                            objWE.Day21L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 22:
                            objWE.Day22L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 23:
                            objWE.Day23L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 24:
                            objWE.Day24L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 25:
                            objWE.Day25L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 26:
                            objWE.Day26L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 27:
                            objWE.Day27L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 28:
                            objWE.Day28L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 29:
                            objWE.Day29L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 30:
                            objWE.Day30L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 31:
                            objWE.Day31L = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                    }

                objWE.UpdateByDate_UserId();
                ///////////////////////////////////////////////
            }
        }

        #endregion
    }
}