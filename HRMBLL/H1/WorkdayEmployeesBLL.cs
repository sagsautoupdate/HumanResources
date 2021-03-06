using System;
using System.Collections.Generic;
using System.Data;
using HRMBLL.BLLHelper;
using HRMBLL.H0;
using HRMDAL.H1;
using HRMUtil;
using HRMUtil.KeyNames.H0;
using HRMUtil.KeyNames.H1;
using DefaultValues = HRMBLL.H1.Helper.DefaultValues;

namespace HRMBLL.H1
{
    public class WorkdayEmployeesBLL
    {
        #region private fields

        #region TimeKeeping

        #endregion

        #region To work nights

        #endregion

        #region To Collect WorkdayEmplyee

        #endregion

        #endregion

        #region properties

        public long WorkdayEmployeeId { get; set; }

        public int UserId { get; set; }

        public string EmployeeCode { get; set; }

        public string FullName { get; set; }

        public int RootId { get; set; }

        public string RootName { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentFullName { get; set; }

        public DateTime WorkdayDate { get; set; }

        public string PositionName { get; set; }

        public int Type { get; set; }

        public double TotalLeaves { get; set; }

        #region TimeKeeping

        public string Day1 { get; set; } = string.Empty;

        public string Day2 { get; set; } = string.Empty;

        public string Day3 { get; set; } = string.Empty;

        public string Day4 { get; set; } = string.Empty;

        public string Day5 { get; set; } = string.Empty;

        public string Day6 { get; set; } = string.Empty;

        public string Day7 { get; set; } = string.Empty;

        public string Day8 { get; set; } = string.Empty;

        public string Day9 { get; set; } = string.Empty;

        public string Day10 { get; set; } = string.Empty;

        public string Day11 { get; set; } = string.Empty;

        public string Day12 { get; set; } = string.Empty;

        public string Day13 { get; set; } = string.Empty;

        public string Day14 { get; set; } = string.Empty;

        public string Day15 { get; set; } = string.Empty;

        public string Day16 { get; set; } = string.Empty;

        public string Day17 { get; set; } = string.Empty;

        public string Day18 { get; set; } = string.Empty;

        public string Day19 { get; set; } = string.Empty;

        public string Day20 { get; set; } = string.Empty;

        public string Day21 { get; set; } = string.Empty;

        public string Day22 { get; set; } = string.Empty;

        public string Day23 { get; set; } = string.Empty;

        public string Day24 { get; set; } = string.Empty;

        public string Day25 { get; set; } = string.Empty;

        public string Day26 { get; set; } = string.Empty;

        public string Day27 { get; set; } = string.Empty;

        public string Day28 { get; set; } = string.Empty;

        public string Day29 { get; set; } = string.Empty;

        public string Day30 { get; set; } = string.Empty;

        public string Day31 { get; set; } = string.Empty;

        #endregion

        #region To work nights

        public double Night1 { get; set; }

        public double Night2 { get; set; }

        public double Night3 { get; set; }

        public double Night4 { get; set; }

        public double Night5 { get; set; }

        public double Night6 { get; set; }

        public double Night7 { get; set; }

        public double Night8 { get; set; }

        public double Night9 { get; set; }

        public double Night10 { get; set; }

        public double Night11 { get; set; }

        public double Night12 { get; set; }

        public double Night13 { get; set; }

        public double Night14 { get; set; }

        public double Night15 { get; set; }

        public double Night16 { get; set; }

        public double Night17 { get; set; }

        public double Night18 { get; set; }

        public double Night19 { get; set; }

        public double Night20 { get; set; }

        public double Night21 { get; set; }

        public double Night22 { get; set; }

        public double Night23 { get; set; }

        public double Night24 { get; set; }

        public double Night25 { get; set; }

        public double Night26 { get; set; }

        public double Night27 { get; set; }

        public double Night28 { get; set; }

        public double Night29 { get; set; }

        public double Night30 { get; set; }

        public double Night31 { get; set; }

        public double CountNight { get; set; }

        public double GC_LamDem { get; set; }

        #endregion

        #region To Collect WorkdayEmplyee

        public double XQD { get; set; }

        public double NC_LamViec { get; set; }

        public double CountX { get; set; }

        public double Count1_2X { get; set; }


        public double F_Om { get; set; }

        public double F_Con_Om { get; set; }

        public double F_KHHDS { get; set; }

        public double F_TNLD { get; set; }

        public double F_O_Co_KHHDS_TNLD { get; set; }


        public double F_Nam { get; set; }

        public double F_db { get; set; }

        public double F_DiDuong { get; set; }

        public double F_Nam_Fdb_DD { get; set; }


        public double F_ThaiSan { get; set; }

        public double F_CongTac { get; set; }

        public double F_HocSAGS { get; set; }

        public double F_Hoc1 { get; set; }

        public double F_Hoc2 { get; set; }

        public double F_Hoc3 { get; set; }

        public double F_Hoc4 { get; set; }

        public double F_Hoc5 { get; set; }

        public double F_Hoc6 { get; set; }

        public double F_Hoc7 { get; set; }

        public double F_Hoc { get; set; }


        public double F_Le { get; set; }

        public double F_KoLuongCLD { get; set; }

        public double F_KoLuongKLD { get; set; }

        public double F_Khac { get; set; }


        public double NghiTuan { get; set; }

        public double NghiBu { get; set; }

        public double NghiMat { get; set; }

        public double NghiTuan_NghiBu { get; set; }


        public double F_OmDaiNgay { get; set; }

        public double F_SayThai { get; set; }

        public double F_KhamThai { get; set; }

        public double F_ConChet { get; set; }

        public double F_DinhChiCongTac { get; set; }

        public double F_TamHoanHD { get; set; }

        public double F_HoiHop { get; set; }

        public double NghiViec { get; set; }

        public double CongDu { get; set; }

        public double OldCongDu { get; set; }

        public double FinalCongDu { get; set; }


        public double Mark { get; set; }

        public string HTCV { get; set; }

        #endregion

        public DateTime CreateDate { get; set; }

        public int CreateUser { get; set; }

        public string CreateUserName { get; set; }

        public string CreateFullName { get; set; }

        public DateTime LastUpdate { get; set; }

        public int UpdateUser { get; set; }

        public string UpdateUserName { get; set; }

        public string UpdateFullName { get; set; }

        public int Status { get; set; }

        public string ReadUserIds { get; set; }

        public string WriteUserIds { get; set; }

        public string Remark { get; set; }

        #endregion

        #region Constructor

        //public WorkdayEmployeesBLL(long workdayEmployeeId, int userId, DateTime createDate, double nC_LamViec, double f_OmKHHDS, double f_OmDaiNgay, double f_ThaiSan, double f_TNLD, double f_Nam,
        //    double f_db, double f_KoLuongKLD, double f_KoLuongCLD, double f_DiDuong, double f_CongTac, double f_Le, double f_Hoc1, double f_Hoc2,
        //    double f_Hoc3, double f_Hoc4, double f_Hoc5, double f_Hoc6, double f_Hoc7, double gC_LTBanNgay, double gC_LTBanDem, double gC_LamDem)
        //{
        //    _WorkdayEmployeeId = workdayEmployeeId;
        //    _UserId = userId;
        //    _CreateDate = createDate;
        //    _NC_LamViec = nC_LamViec;
        //    _F_OmKHHDS = f_OmKHHDS;
        //    _F_OmDaiNgay = f_OmDaiNgay;
        //    _F_ThaiSan = f_ThaiSan;
        //    _F_TNLD = f_TNLD;
        //    _F_Nam = f_Nam;
        //    _F_db = f_db;
        //    _F_KoLuongKLD = f_KoLuongKLD;
        //    _F_KoLuongCLD = f_KoLuongCLD;
        //    _F_DiDuong = f_DiDuong;
        //    _F_CongTac = f_CongTac;
        //    _F_Le = f_Le;
        //    _F_Hoc1 = f_Hoc1;
        //    _F_Hoc2 = f_Hoc2;
        //    _F_Hoc3 = f_Hoc3;
        //    _F_Hoc4 = f_Hoc4;
        //    _F_Hoc5 = f_Hoc5;
        //    _F_Hoc6 = f_Hoc6;
        //    _F_Hoc7 = f_Hoc7;
        //    _GC_LTBanNgay = gC_LTBanNgay;
        //    _GC_LTBanDem = gC_LTBanDem;
        //    _GC_LamDem = gC_LamDem;
        //}

        #endregion

        #region public methods insert, update, delete

        public static long InsertByDate_DeptId(string deptIds, DateTime workdayDate, int createUser, int status,
            string writeUserIds, string readUserIds, bool isOfficeHours)
        {
            var nC_LamViec = DateTime.DaysInMonth(workdayDate.Year, workdayDate.Month);
            string day25 = null;
            string day26 = null;
            string day27 = null;
            string day28 = null;
            string day29 = null;
            string day30 = null;
            string day31 = null;
            if (nC_LamViec == 25)
            {
                if (isOfficeHours)
                    day25 = "X";
                else
                    day25 = "-";
            }
            else if (nC_LamViec == 26)
            {
                if (isOfficeHours)
                {
                    day25 = "X";
                    day26 = "X";
                }
                else
                {
                    day25 = "-";
                    day26 = "-";
                }
            }
            else if (nC_LamViec == 27)
            {
                if (isOfficeHours)
                {
                    day25 = "X";
                    day26 = "X";
                    day27 = "X";
                }
                else
                {
                    day25 = "-";
                    day26 = "-";
                    day27 = "-";
                }
            }
            else if (nC_LamViec == 28)
            {
                if (isOfficeHours)
                {
                    day25 = "X";
                    day26 = "X";
                    day27 = "X";
                    day28 = "X";
                }
                else
                {
                    day25 = "-";
                    day26 = "-";
                    day27 = "-";
                    day28 = "-";
                }
            }
            else if (nC_LamViec == 29)
            {
                if (isOfficeHours)
                {
                    day25 = "X";
                    day26 = "X";
                    day27 = "X";
                    day28 = "X";
                    day29 = "X";
                }
                else
                {
                    day25 = "-";
                    day26 = "-";
                    day27 = "-";
                    day28 = "-";
                    day29 = "-";
                }
            }
            else if (nC_LamViec == 30)
            {
                if (isOfficeHours)
                {
                    day25 = "X";
                    day26 = "X";
                    day27 = "X";
                    day28 = "X";
                    day29 = "X";
                    day30 = "X";
                }
                else
                {
                    day25 = "-";
                    day26 = "-";
                    day27 = "-";
                    day28 = "-";
                    day29 = "-";
                    day30 = "-";
                }
            }
            else
            {
                if (isOfficeHours)
                {
                    day25 = "X";
                    day26 = "X";
                    day27 = "X";
                    day28 = "X";
                    day29 = "X";
                    day30 = "X";
                    day31 = "X";
                }
                else
                {
                    day25 = "-";
                    day26 = "-";
                    day27 = "-";
                    day28 = "-";
                    day29 = "-";
                    day30 = "-";
                    day31 = "-";
                }
            }
            var XQD = DefaultValues.XQD(workdayDate.Month, workdayDate.Year);
            var type = Constants.TimeKeepingType_Init_OfficeHours_Id;
            if (!isOfficeHours)
            {
                type = Constants.TimeKeepingType_Init_Shift_Id;
                XQD = DefaultValues.XQDSalaryMinusHoliday(workdayDate.Month, workdayDate.Year);
            }


            new WorkdayEmployeesDAL().InsertByDate_DeptIds(day25, day26, day27, day28, day29, day30, day31, deptIds,
                workdayDate, createUser, status, writeUserIds, readUserIds, XQD, type, isOfficeHours);


            var listW = GetByFilter(string.Empty, deptIds, workdayDate.Month, workdayDate.Year, -1, 0, 1);

            foreach (var objWE in listW)
            {
                if (isOfficeHours)
                {
                    UpdateWorkdayEmployeeByOfficeHours(objWE);
                }
                else
                {
                    var x = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5,
                        objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                        objWE.Day13,
                        objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                        objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                        objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_X);

                    objWE.NC_LamViec = x;
                    objWE.UpdateByDate_UserId();
                }

                UpdateWorkdayEmployeeByHoliday(objWE);
            }

            /// insert vao bang cong huong luong
            /// 

            WorkdayEmployeesBLLL.InsertByDate_DeptId(deptIds, workdayDate, createUser, isOfficeHours, status);

            return 0;
        }

        public static long InsertByDate_UserId(int userId, DateTime workdayDate, int createUser, int status,
            string writeUserIds, string readUserIds, bool isOfficeHours)
        {
            var objDAL = new WorkdayEmployeesDAL();
            //double days = DefaultValues.WorkdayByDate(date);
            var nC_LamViec = DateTime.DaysInMonth(workdayDate.Year, workdayDate.Month);
            string day25 = null;
            string day26 = null;
            string day27 = null;
            string day28 = null;
            string day29 = null;
            string day30 = null;
            string day31 = null;
            if (nC_LamViec == 25)
            {
                if (isOfficeHours)
                    day25 = "X";
                else
                    day25 = "-";
            }
            else if (nC_LamViec == 26)
            {
                if (isOfficeHours)
                {
                    day25 = "X";
                    day26 = "X";
                }
                else
                {
                    day25 = "-";
                    day26 = "-";
                }
            }
            else if (nC_LamViec == 27)
            {
                if (isOfficeHours)
                {
                    day25 = "X";
                    day26 = "X";
                    day27 = "X";
                }
                else
                {
                    day25 = "-";
                    day26 = "-";
                    day27 = "-";
                }
            }
            else if (nC_LamViec == 28)
            {
                if (isOfficeHours)
                {
                    day25 = "X";
                    day26 = "X";
                    day27 = "X";
                    day28 = "X";
                }
                else
                {
                    day25 = "-";
                    day26 = "-";
                    day27 = "-";
                    day28 = "-";
                }
            }
            else if (nC_LamViec == 29)
            {
                if (isOfficeHours)
                {
                    day25 = "X";
                    day26 = "X";
                    day27 = "X";
                    day28 = "X";
                    day29 = "X";
                }
                else
                {
                    day25 = "-";
                    day26 = "-";
                    day27 = "-";
                    day28 = "-";
                    day29 = "-";
                }
            }
            else if (nC_LamViec == 30)
            {
                if (isOfficeHours)
                {
                    day25 = "X";
                    day26 = "X";
                    day27 = "X";
                    day28 = "X";
                    day29 = "X";
                    day30 = "X";
                }
                else
                {
                    day25 = "-";
                    day26 = "-";
                    day27 = "-";
                    day28 = "-";
                    day29 = "-";
                    day30 = "-";
                }
            }
            else
            {
                if (isOfficeHours)
                {
                    day25 = "X";
                    day26 = "X";
                    day27 = "X";
                    day28 = "X";
                    day29 = "X";
                    day30 = "X";
                    day31 = "X";
                }
                else
                {
                    day25 = "-";
                    day26 = "-";
                    day27 = "-";
                    day28 = "-";
                    day29 = "-";
                    day30 = "-";
                    day31 = "-";
                }
            }
            //double XQD = DefaultValues.XQD(workdayDate.Month, workdayDate.Year);
            //int type = 1;
            //if (!isOfficeHours)
            //{
            //    type = 0;
            //}

            var XQD = DefaultValues.XQD(workdayDate.Month, workdayDate.Year);
            var type = Constants.TimeKeepingType_Init_OfficeHours_Id;
            if (!isOfficeHours)
            {
                type = Constants.TimeKeepingType_Init_Shift_Id;
                XQD = DefaultValues.XQDSalaryMinusHoliday(workdayDate.Month, workdayDate.Year);
            }

            return objDAL.InsertByDate_UserId(userId, workdayDate, day25, day26, day27, day28, day29, day30, day31,
                createUser, status, writeUserIds, readUserIds, XQD, type, isOfficeHours);
        }

        public static void InsertUpdateWorkdayEmployeeByFromDateToDate(int userId, DateTime fromDate, DateTime toDate,
            int leaveTypeId, int currentUserId)
        {
            var objLeaveTypesBLL = LeaveTypesBLL.GetById(leaveTypeId);

            var list = Utilities.AnalyseLeaveDate(objLeaveTypesBLL, fromDate, toDate);

            var status = 0;
            var writeUserIds = "," + currentUserId + ",";
            var readUserIds = "," + currentUserId + ",";
            var isOfficeHours = false;
            var listDept = DepartmentEmployeeBLL.GetByUserId(userId);
            var deptId = 0;
            if (listDept.Count > 0)
                deptId = listDept[0].DepartmentId;
            if (DefaultValues.IsMediateDepartment(deptId))
                isOfficeHours = true;
            else
                isOfficeHours = false;

            if (list.Count > 1)
            {
                foreach (var obj in list)
                    if (obj.Days > 0)
                    {
                        var workdayDate = new DateTime(obj.StartTime.Year, obj.StartTime.Month, 1);
                        InsertByDate_UserId(userId, workdayDate, currentUserId, status, writeUserIds, readUserIds,
                            isOfficeHours);
                        UpdateWorkdayByLeave(userId, obj.StartTime, obj.EndTime, leaveTypeId,
                            Constants.WorkdayEmployees_Status_TimeKeeping_No);

                        WorkdayEmployeesBLLL.InsertByDate_UserId(userId, workdayDate, currentUserId, isOfficeHours,
                            status);
                        WorkdayEmployeesBLLL.UpdateWorkdayByLeave(userId, obj.StartTime, obj.EndTime, leaveTypeId,
                            Constants.WorkdayEmployees_Status_TimeKeeping_No);
                    }
            }
            else
            {
                var workdayDate = new DateTime(fromDate.Year, fromDate.Month, 1);
                InsertByDate_UserId(userId, workdayDate, currentUserId, status, writeUserIds, readUserIds, isOfficeHours);
                UpdateWorkdayByLeave(userId, fromDate, toDate, leaveTypeId,
                    Constants.WorkdayEmployees_Status_TimeKeeping_No);

                WorkdayEmployeesBLLL.InsertByDate_UserId(userId, workdayDate, currentUserId, isOfficeHours, status);
                UpdateWorkdayByLeave(userId, fromDate, toDate, leaveTypeId,
                    Constants.WorkdayEmployees_Status_TimeKeeping_No);
            }
        }

        public static long UpdateByDate_UserId(
            int userId,
            DateTime workdayDate,
            string day1, string day2, string day3, string day4, string day5, string day6, string day7, string day8,
            string day9, string day10,
            string day11, string day12, string day13, string day14, string day15, string day16, string day17,
            string day18, string day19, string day20,
            string day21, string day22, string day23, string day24, string day25, string day26, string day27,
            string day28, string day29, string day30, string day31,
            double nC_LamViec,
            double f_Om, double f_OmDaiNgay, double f_ThaiSan, double f_TNLD, double f_Nam, double f_db,
            double f_KoLuongCLD, double f_KoLuongKLD,
            double f_DiDuong, double f_CongTac, double f_HocSAGS, double f_Hoc1, double f_Hoc2, double f_Hoc3,
            double f_Hoc4, double f_Hoc5, double f_Hoc6, double f_Hoc7,
            double f_Con_Om, double f_KHHDS, double f_SayThai, double f_KhamThai, double f_ConChet,
            double f_DinhChiCongTac, double f_TamHoanHD, double f_HoiHop, double f_Le,
            double nghiTuan, double nghiBu, double nghiViec, double nghiMat, double congDu, double gC_LamDem,
            double mark, string hTCV,
            DateTime lastUpdate, int updateUser, int status,
            double night1, double night2, double night3, double night4, double night5, double night6, double night7,
            double night8, double night9, double night10,
            double night11, double night12, double night13, double night14, double night15, double night16,
            double night17, double night18, double night19, double night20,
            double night21, double night22, double night23, double night24, double night25, double night26,
            double night27, double night28, double night29, double night30, double night31, string remark
        )
        {
            return new WorkdayEmployeesDAL().UpdateByDate_UserId(userId, workdayDate,
                day1, day2, day3, day4, day5, day6, day7, day8, day9, day10,
                day11, day12, day13, day14, day15, day16, day17, day18, day19, day20,
                day21, day22, day23, day24, day25, day26, day27, day28, day29, day30, day31,
                nC_LamViec, f_Om, f_OmDaiNgay, f_ThaiSan, f_TNLD, f_Nam, f_db, f_KoLuongCLD,
                f_KoLuongKLD, f_DiDuong, f_CongTac, f_HocSAGS, f_Hoc1, f_Hoc2, f_Hoc3, f_Hoc4, f_Hoc5, f_Hoc6, f_Hoc7,
                f_Con_Om,
                f_KHHDS, f_SayThai, f_KhamThai, f_ConChet, f_DinhChiCongTac, f_TamHoanHD, f_HoiHop,
                f_Le, nghiTuan, nghiBu, nghiViec, nghiMat, congDu,
                gC_LamDem, mark, hTCV,
                lastUpdate, updateUser, status,
                night1, night2, night3, night4, night5, night6, night7, night8, night9, night10,
                night11, night12, night13, night14, night15, night16, night17, night18, night19, night20,
                night21, night22, night23, night24, night25, night26, night27, night28, night29, night30, night31,
                remark
            );
        }

        public long UpdateByDate_UserId()
        {
            return new WorkdayEmployeesDAL().UpdateByDate_UserId(UserId, WorkdayDate,
                Day1, Day2, Day3, Day4, Day5, Day6, Day7, Day8, Day9, Day10,
                Day11, Day12, Day13, Day14, Day15, Day16, Day17, Day18, Day19, Day20,
                Day21, Day22, Day23, Day24, Day25, Day26, Day27, Day28, Day29, Day30, Day31,
                NC_LamViec, F_Om, F_OmDaiNgay, F_ThaiSan, F_TNLD, F_Nam, F_db, F_KoLuongCLD,
                F_KoLuongKLD, F_DiDuong, F_CongTac, F_HocSAGS, F_Hoc1, F_Hoc2, F_Hoc3, F_Hoc4, F_Hoc5, F_Hoc6, F_Hoc7,
                F_Con_Om,
                F_KHHDS, F_SayThai, F_KhamThai, F_ConChet, F_DinhChiCongTac, F_TamHoanHD, F_HoiHop,
                F_Le, NghiTuan, NghiBu, NghiViec, NghiMat, CongDu,
                GC_LamDem, Mark, HTCV,
                LastUpdate, UpdateUser, Status,
                Night1, Night2, Night3, Night4, Night5, Night6, Night7, Night8, Night9, Night10,
                Night11, Night12, Night13, Night14, Night15, Night16, Night17, Night18, Night19, Night20,
                Night21, Night22, Night23, Night24, Night25, Night26, Night27, Night28, Night29, Night30, Night31,
                Remark
            );
        }

        public static long UpdateByDate_UserId(
            int userId,
            DateTime workdayDate, double XQD,
            string day1, string day2, string day3, string day4, string day5, string day6, string day7, string day8,
            string day9, string day10,
            string day11, string day12, string day13, string day14, string day15, string day16, string day17,
            string day18, string day19, string day20,
            string day21, string day22, string day23, string day24, string day25, string day26, string day27,
            string day28, string day29, string day30, string day31,
            DateTime lastUpdate, int updateUser, int status, double mark,
            double night1, double night2, double night3, double night4, double night5, double night6, double night7,
            double night8, double night9, double night10,
            double night11, double night12, double night13, double night14, double night15, double night16,
            double night17, double night18, double night19, double night20,
            double night21, double night22, double night23, double night24, double night25, double night26,
            double night27, double night28, double night29, double night30, double night31,
            string remark, int typeInitWorkdayEmployee
        )
        {
            #region Calculate Timekeeping

            double nC_LamViec = Constants.WorkdayEmployee_DefaultValue,
                f_Om = Constants.WorkdayEmployee_DefaultValue,
                f_OmDaiNgay = Constants.WorkdayEmployee_DefaultValue;
            double f_ThaiSan = Constants.WorkdayEmployee_DefaultValue,
                f_TNLD = Constants.WorkdayEmployee_DefaultValue,
                f_Nam = Constants.WorkdayEmployee_DefaultValue;
            double f_db = Constants.WorkdayEmployee_DefaultValue,
                f_KoLuongCLD = Constants.WorkdayEmployee_DefaultValue,
                f_KoLuongKLD = Constants.WorkdayEmployee_DefaultValue;
            double f_DiDuong = Constants.WorkdayEmployee_DefaultValue,
                f_CongTac = Constants.WorkdayEmployee_DefaultValue;

            double f_HocSAGS = Constants.WorkdayEmployee_DefaultValue,
                f_Hoc1 = Constants.WorkdayEmployee_DefaultValue,
                f_Hoc2 = Constants.WorkdayEmployee_DefaultValue,
                f_Hoc3 = Constants.WorkdayEmployee_DefaultValue;
            double f_Hoc4 = Constants.WorkdayEmployee_DefaultValue,
                f_Hoc5 = Constants.WorkdayEmployee_DefaultValue,
                f_Hoc6 = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc7 = Constants.WorkdayEmployee_DefaultValue;

            double f_Con_Om = Constants.WorkdayEmployee_DefaultValue, f_KHHDS = Constants.WorkdayEmployee_DefaultValue;
            double f_SayThai = Constants.WorkdayEmployee_DefaultValue,
                f_KhamThai = Constants.WorkdayEmployee_DefaultValue,
                f_ConChet = Constants.WorkdayEmployee_DefaultValue;
            double f_DinhChiCongTac = Constants.WorkdayEmployee_DefaultValue,
                f_TamHoanHD = Constants.WorkdayEmployee_DefaultValue,
                f_HoiHop = Constants.WorkdayEmployee_DefaultValue;
            double f_Le = Constants.WorkdayEmployee_DefaultValue,
                nghiTuan = Constants.WorkdayEmployee_DefaultValue,
                nghiBu = Constants.WorkdayEmployee_DefaultValue,
                nghiMat = Constants.WorkdayEmployee_DefaultValue;
            double nghiViec = Constants.WorkdayEmployee_DefaultValue, congDu = Constants.WorkdayEmployee_DefaultValue;
            var gC_LamDem = Constants.WorkdayEmployee_DefaultValue;

            #endregion

            #region Calculate Timekeeping

            f_Om = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3, day4,
                day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_O_BAN_THAN);

            f_OmDaiNgay = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3,
                day4, day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_O_DAI_NGAY);

            f_ThaiSan = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3,
                day4, day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_THAI_SAN);

            f_TNLD = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3, day4,
                day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_TNLD);

            f_Nam = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3, day4,
                day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_F_NAM);

            f_db = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3, day4,
                day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_FDB);

            f_KoLuongCLD = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3,
                day4, day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO);

            f_KoLuongKLD = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3,
                day4, day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO);

            f_DiDuong = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3,
                day4, day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_F_DI_DUONG);

            f_CongTac = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3,
                day4, day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_F_CONG_TAC);

            f_HocSAGS = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3,
                day4, day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_HOC_SAGS);
            f_Hoc1 = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3, day4,
                day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_HOC_1);
            f_Hoc2 = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3, day4,
                day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_HOC_2);
            f_Hoc3 = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3, day4,
                day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_HOC_3);
            f_Hoc4 = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3, day4,
                day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_HOC_4);
            f_Hoc5 = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3, day4,
                day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_HOC_5);
            f_Hoc6 = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3, day4,
                day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_HOC_6);
            f_Hoc7 = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3, day4,
                day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_HOC_7);
            f_Con_Om = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3,
                day4, day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_CON_OM);
            f_KHHDS = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3,
                day4, day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_KHHDS);
            f_SayThai = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3,
                day4, day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_SAY_THAI);
            f_KhamThai = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3,
                day4, day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_KHAM_THAI);
            f_ConChet = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3,
                day4, day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_CON_CHET_SAU_KHI_SINH);
            f_DinhChiCongTac = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2,
                day3, day4, day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_DINH_CHI_CONG_TAC);
            f_TamHoanHD = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3,
                day4, day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_TAM_HOAN_HOP_DONG);
            f_HoiHop = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3,
                day4, day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_HOI_HOP);
            f_Le = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3, day4,
                day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_LE_TET);

            nghiTuan = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3,
                day4, day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_NGHI_TUAN);

            nghiBu = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3, day4,
                day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_NGHI_BU);

            nghiMat = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3,
                day4, day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_NGHI_MAT);

            nghiViec = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3,
                day4, day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_NGHI_VIEC);

            var x = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3, day4,
                day5, day6, day7, day8, day9, day10, day11, day12, day13,
                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
                day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_X);

            //double nuax = DefaultValues.CalculateLeaveDay(workdayDate.Month, workdayDate.Year, userId, day1, day2, day3, day4, day5, day6, day7, day8, day9, day10, day11, day12, day13,
            //       day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26,
            //       day27, day28, day29, day30, day31, Constants.LEAVE_TYPE_1_2_X);

            #endregion

            //if (nuax > 0)
            //{
            //    nC_LamViec = x + (nuax / 2);
            //}
            //else
            //{
            nC_LamViec = x;
            //}

            var hoc = f_HocSAGS + f_Hoc1 + f_Hoc2 + f_Hoc3 + f_Hoc4 + f_Hoc5 + f_Hoc6 + f_Hoc7;
            var om = f_Om + f_Con_Om + f_OmDaiNgay + f_TNLD;
            var thaisan = f_ThaiSan + f_KHHDS + f_KhamThai + f_SayThai + f_ConChet;
            var FFdbDD = f_Nam + f_db + f_DiDuong;
            var CtHoiHop = f_CongTac + f_HoiHop;
            var khac = f_KoLuongCLD + f_KoLuongKLD + f_TamHoanHD + f_DinhChiCongTac + nghiMat;
            var total = nC_LamViec + hoc + om + thaisan + FFdbDD + CtHoiHop + khac;

            // Tinh cong du
            var countSaturday = DefaultValues.GetCountSaturday(workdayDate.Month, workdayDate.Year);
            congDu = total - XQD;

            gC_LamDem = night1 + night2 + night3 + night4 + night5 + night6 + night7 + night8 + night9 + night10 +
                        night11 + night12 + night13 + night14 + night15 + night16 + night17 + night18 + night19 +
                        night20 + night21 + night22 + night23 + night24 + night25 + night26 + night27 + night28 +
                        night29 + night30 + night31;

            var hTCV = DefaultValues.HTCV(mark);

            UpdateByDate_UserId(userId, workdayDate,
                day1, day2, day3, day4, day5, day6, day7, day8, day9, day10,
                day11, day12, day13, day14, day15, day16, day17, day18, day19, day20,
                day21, day22, day23, day24, day25, day26, day27, day28, day29, day30, day31,
                nC_LamViec, f_Om, f_OmDaiNgay, f_ThaiSan, f_TNLD, f_Nam, f_db, f_KoLuongCLD,
                f_KoLuongKLD, f_DiDuong, f_CongTac, f_HocSAGS, f_Hoc1, f_Hoc2, f_Hoc3, f_Hoc4, f_Hoc5, f_Hoc6, f_Hoc7,
                f_Con_Om,
                f_KHHDS, f_SayThai, f_KhamThai, f_ConChet, f_DinhChiCongTac, f_TamHoanHD, f_HoiHop,
                f_Le, nghiTuan, nghiBu, nghiViec, nghiMat, congDu,
                gC_LamDem, mark, hTCV,
                DateTime.Now, updateUser, status,
                night1, night2, night3, night4, night5, night6, night7, night8, night9, night10,
                night11, night12, night13, night14, night15, night16, night17, night18, night19, night20,
                night21, night22, night23, night24, night25, night26, night27, night28, night29, night30, night31,
                remark
            );

            long success = 1;

            if (f_ThaiSan == XQD)
            {
                var tsCode = Constants.LEAVE_TYPE_THAI_SAN_CODE;
                var dayinMonths = DateTime.DaysInMonth(workdayDate.Year, workdayDate.Month);
                if (dayinMonths == 27)
                    WorkdayEmployeesBLLL.UpdateByDate_UserId(userId, workdayDate,
                        tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode,
                        tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode,
                        tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, "", "", "", "", mark, gC_LamDem,
                        DateTime.Now, updateUser, remark);
                else if (dayinMonths == 28)
                    WorkdayEmployeesBLLL.UpdateByDate_UserId(userId, workdayDate,
                        tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode,
                        tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode,
                        tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, "", "", "", mark, gC_LamDem,
                        DateTime.Now, updateUser, remark);
                else if (dayinMonths == 29)
                    WorkdayEmployeesBLLL.UpdateByDate_UserId(userId, workdayDate,
                        tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode,
                        tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode,
                        tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, "", "", mark, gC_LamDem,
                        DateTime.Now, updateUser, remark);
                else if (dayinMonths == 30)
                    WorkdayEmployeesBLLL.UpdateByDate_UserId(userId, workdayDate,
                        tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode,
                        tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode,
                        tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, "", mark,
                        gC_LamDem, DateTime.Now, updateUser, remark);
                else
                    WorkdayEmployeesBLLL.UpdateByDate_UserId(userId, workdayDate,
                        tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode,
                        tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode,
                        tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, tsCode, mark,
                        gC_LamDem, DateTime.Now, updateUser, remark);


                var objTS = WorkdayEmployeesBLLL.GetByUserId_Month_Year(userId, workdayDate.Month, workdayDate.Year,
                    Constants.WorkdayEmployees_Status_TimeKeeping_Yes);
                WorkdayEmployeesBLLL.UpdateWorkdayEmployeeByOfficeHours(objTS);

                success = -1;
            }

            return success;
        }

        public static void UpdateWorkdayByLeave(int userId, DateTime fromDate, DateTime toDate, int leaveTypeId,
            int status)
        {
            var symbolTimekeeping = Constants.GetSymbolTimekeeping(leaveTypeId);
            var objWE = GetByUserId_Month_Year(userId, fromDate.Month, fromDate.Year, status);

            if (objWE != null)
            {
                var dateTemp = fromDate;
                while (dateTemp.CompareTo(toDate) <= 0)
                {
                    switch (dateTemp.Day)
                    {
                        case 1:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day1 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day1 = symbolTimekeeping;
                            break;
                        case 2:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day2 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day2 = symbolTimekeeping;
                            break;
                        case 3:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day3 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day3 = symbolTimekeeping;
                            break;
                        case 4:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day4 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day4 = symbolTimekeeping;
                            break;
                        case 5:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day5 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day5 = symbolTimekeeping;
                            break;
                        case 6:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day6 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day6 = symbolTimekeeping;
                            break;
                        case 7:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day7 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day7 = symbolTimekeeping;
                            break;
                        case 8:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day8 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day8 = symbolTimekeeping;
                            break;
                        case 9:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day9 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day9 = symbolTimekeeping;
                            break;
                        case 10:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day10 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day10 = symbolTimekeeping;
                            break;
                        case 11:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day11 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day11 = symbolTimekeeping;
                            break;
                        case 12:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day12 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day12 = symbolTimekeeping;
                            break;
                        case 13:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day13 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day13 = symbolTimekeeping;
                            break;
                        case 14:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day14 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day14 = symbolTimekeeping;
                            break;
                        case 15:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day15 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day15 = symbolTimekeeping;
                            break;
                        case 16:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day16 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day16 = symbolTimekeeping;
                            break;
                        case 17:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day17 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day17 = symbolTimekeeping;
                            break;
                        case 18:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day18 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day18 = symbolTimekeeping;
                            break;
                        case 19:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day19 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day19 = symbolTimekeeping;
                            break;
                        case 20:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day20 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day20 = symbolTimekeeping;
                            break;
                        case 21:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day21 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day21 = symbolTimekeeping;
                            break;
                        case 22:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day22 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day22 = symbolTimekeeping;
                            break;
                        case 23:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day23 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day23 = symbolTimekeeping;
                            break;
                        case 24:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day24 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day24 = symbolTimekeeping;
                            break;
                        case 25:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day25 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day25 = symbolTimekeeping;
                            break;
                        case 26:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day26 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day26 = symbolTimekeeping;
                            break;
                        case 27:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day27 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day27 = symbolTimekeeping;
                            break;
                        case 28:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day28 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day28 = symbolTimekeeping;
                            break;
                        case 29:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day29 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day29 = symbolTimekeeping;
                            break;
                        case 30:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day30 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day30 = symbolTimekeeping;
                            break;
                        case 31:
                            if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
                                objWE.Day31 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day31 = symbolTimekeeping;
                            break;
                    }
                    dateTemp = dateTemp.AddDays(1);
                }
                ////////////////////////////////////////////////
                objWE.F_Om = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5,
                    objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_O_BAN_THAN);

                objWE.F_OmDaiNgay = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_O_DAI_NGAY);

                objWE.F_ThaiSan = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_THAI_SAN);

                objWE.F_TNLD = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_TNLD);

                objWE.F_Nam = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5,
                    objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_F_NAM);

                objWE.F_db = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5,
                    objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_FDB);

                objWE.F_KoLuongCLD = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31,
                    Constants.LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO);

                objWE.F_KoLuongKLD = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31,
                    Constants.LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO);

                objWE.F_DiDuong = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_F_DI_DUONG);

                objWE.F_CongTac = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_F_CONG_TAC);

                objWE.F_Hoc1 = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_HOC_1);
                objWE.F_Hoc2 = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_HOC_2);
                objWE.F_Hoc3 = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_HOC_3);
                objWE.F_Hoc4 = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_HOC_4);
                objWE.F_Hoc5 = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_HOC_5);
                objWE.F_Hoc6 = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_HOC_6);
                objWE.F_Hoc7 = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_HOC_7);

                objWE.F_Con_Om = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_CON_OM);

                objWE.F_KHHDS = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_KHHDS);

                objWE.F_SayThai = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_SAY_THAI);

                objWE.F_KhamThai = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_KHAM_THAI);

                objWE.F_ConChet = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31,
                    Constants.LEAVE_TYPE_CON_CHET_SAU_KHI_SINH);

                objWE.F_DinhChiCongTac = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31,
                    Constants.LEAVE_TYPE_DINH_CHI_CONG_TAC);

                objWE.F_TamHoanHD = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31,
                    Constants.LEAVE_TYPE_TAM_HOAN_HOP_DONG);

                objWE.F_HoiHop = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_HOI_HOP);

                objWE.F_Le = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5,
                    objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_LE_TET);

                objWE.NghiTuan = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_NGHI_TUAN);

                objWE.NghiBu = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_NGHI_BU);

                objWE.NghiViec = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_NGHI_VIEC);

                var x = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5,
                    objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_X);

                var nuax = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5,
                    objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_1_2_X);

                if (nuax > 0)
                    objWE.NC_LamViec = x + nuax/2;
                else
                    objWE.NC_LamViec = x;

                objWE.UpdateByDate_UserId();
                ///////////////////////////////////////////////
            }
        }

        public static long Update_By_Sends(string DeptIds, int? Month, int? Year, string ReadUserIds,
            string WriteUserIds, int? Status)
        {
            return new WorkdayEmployeesDAL().Update_By_Sends(DeptIds, Month, Year, ReadUserIds, WriteUserIds, Status);
        }

        public static long Update_By_MarkHTCV(int userId, double mark, DateTime lastUpdate, int updateUser, int month,
            int year)
        {
            return new WorkdayEmployeesDAL().Update_By_MarkHTCV(userId, mark, lastUpdate, updateUser, month, year);
        }

        public static long Update_ForCongDu(int userId, double congDu, int month, int year)
        {
            return new WorkdayEmployeesDAL().Update_ForCongDu(userId, congDu, month, year);
        }

        public static long Update_By_Send_UserId(string readUserIds, string writeUserIds, int month, int year,
            int userId)
        {
            return new WorkdayEmployeesDAL().Update_By_Send_UserId(readUserIds, writeUserIds, month, year, userId);
        }

        public static long Update_By_Send_Read(string readUserIds, int month, int year, int userId)
        {
            return new WorkdayEmployeesDAL().Update_By_Send_Read(readUserIds, month, year, userId);
        }

        public static long Update_By_Send_Write(string writeUserIds, int month, int year, int userId)
        {
            return new WorkdayEmployeesDAL().Update_By_Send_Write(writeUserIds, month, year, userId);
        }

        public static long Update_By_Sends_Read(string readUserIds, int month, int year, string deptIds)
        {
            return new WorkdayEmployeesDAL().Update_By_Sends_Read(readUserIds, month, year, deptIds);
        }

        public static long Update_By_Sends_Write(string writeUserIds, int month, int year, string deptIds)
        {
            return new WorkdayEmployeesDAL().Update_By_Sends_Write(writeUserIds, month, year, deptIds);
        }

        public static long Update_By_Sharing_Read(string readUserIds, int month, int year, int userId)
        {
            return new WorkdayEmployeesDAL().Update_By_Sharing_Read(readUserIds, month, year, userId);
        }

        public static long Update_By_Sharing_Write(string writeUserIds, int month, int year, int userId)
        {
            return new WorkdayEmployeesDAL().Update_By_Sharing_Write(writeUserIds, month, year, userId);
        }

        public static long Update_By_Sharings_Read(string readUserIds, int month, int year, string deptIds)
        {
            return new WorkdayEmployeesDAL().Update_By_Sharings_Read(readUserIds, month, year, deptIds);
        }

        public static long Update_By_Sharings_Write(string writeUserIds, int month, int year, string deptIds)
        {
            return new WorkdayEmployeesDAL().Update_By_Sharings_Write(writeUserIds, month, year, deptIds);
        }

        public static long DeleteByDeptIdsDate(string deptIds, int month, int year)
        {
            return new WorkdayEmployeesDAL().DeleteByDeptIdsDate(deptIds, month, year);
        }

        public static long Delete(long workdayEmployeeId)
        {
            return new WorkdayEmployeesDAL().Delete(workdayEmployeeId);
        }

        #endregion

        #region public methods GET

        public static List<WorkdayEmployeesBLL> GetByFilter(string fullName, string departmentIds, int month, int year,
            int status, int receivedUserId, int typeSort)
        {
            var objDAL = new WorkdayEmployeesDAL();
            return
                GenerateListWorkdayEmployeesBLLFromDataTable(objDAL.GetAllByFilter(fullName, departmentIds, month, year,
                    status, receivedUserId, typeSort));
        }

        public static DataTable GetDTByFilter(string fullName, string departmentIds, int month, int year)
        {
            var objDAL = new WorkdayEmployeesDAL();
            return objDAL.GetAllDT(fullName, departmentIds, month, year);
        }

        public static List<WorkdayEmployeesBLL> GetByFilterByNotUser(string fullName, string departmentIds, int month,
            int year, int status, int receivedUserId, int typeSort, int notUserId)
        {
            return
                GenerateListWorkdayEmployeesBLLFromDataTableByNotUserId(
                    new WorkdayEmployeesDAL().GetAllByFilter(fullName, departmentIds, month, year, status,
                        receivedUserId, typeSort), notUserId);
        }

        public static WorkdayEmployeesBLL GetById(long workdayEmployeeId)
        {
            var objDAL = new WorkdayEmployeesDAL();
            var list = GenerateListWorkdayEmployeesBLLFromDataTable(objDAL.GetById(workdayEmployeeId));

            if (list.Count > 0)
                return list[0];
            return null;
        }

        public static List<WorkdayEmployeesBLL> GetById2(long workdayEmployeeId)
        {
            return GenerateListWorkdayEmployeesBLLFromDataTable(new WorkdayEmployeesDAL().GetById(workdayEmployeeId));
        }

        public static WorkdayEmployeesBLL GetByUserId_Month_Year(int userId, int month, int year, int status)
        {
            var objDAL = new WorkdayEmployeesDAL();
            var list =
                GenerateListWorkdayEmployeesBLLFromDataTable(objDAL.GetByUserId_Month_Year(userId, month, year, status));

            if (list.Count > 0)
                return list[0];
            return null;
        }

        public static WorkdayEmployeesBLL GetByUserId_Month_Year1(int userId, int month, int year)
        {
            var objDAL = new WorkdayEmployeesDAL();
            var list = GenerateListWorkdayEmployeesBLLFromDataTable(objDAL.GetByUserId_Month_Year1(userId, month, year));

            if (list.Count > 0)
                return list[0];
            return null;
        }

        public static DataTable GetDTByUserId_Month_Year(int userId, int month, int year)
        {
            return new WorkdayEmployeesDAL().GetByUserId_Month_Year1(userId, month, year);
        }

        public static List<WorkdayEmployeesBLL> GetByStatistic1(string deptIds, int month, int year, int debit,
            string hTCV, int workdayType)
        {
            return
                GenerateListWorkdayEmployeesBLLFromDataTable(new WorkdayEmployeesDAL().GetByStatistic1(deptIds, month,
                    year, debit, hTCV, workdayType));
        }

        public static List<WorkdayEmployeesBLL> GetByStatisticLeave(string fullName, string deptIds, string leaveCode,
            int month, int year)
        {
            return
                GenerateListWorkdayEmployeesBLLFromDataTable(new WorkdayEmployeesDAL().GetByStatisticLeave(fullName,
                    deptIds, leaveCode, month, year));
        }

        public static List<WorkdayEmployeesBLL> GetAllByFilterHTCV(string fullName, string deptIds, int minMark,
            int maxMark, int month, int year)
        {
            return
                GenerateListWorkdayEmployeesBLLFromDataTable(new WorkdayEmployeesDAL().GetAllByFilterHTCV(fullName,
                    deptIds, minMark, maxMark, month, year));
        }

        public static List<WorkdayEmployeesBLL> GetByRoot(int rootId, int month, int year)
        {
            return
                GenerateListWorkdayEmployeesBLLFromDataTable(new WorkdayEmployeesDAL().GetByRootId(rootId, month, year));
        }

        public static DataTable CheckSend(int receivedUserId, int rootId, int month, int year)
        {
            var dtGet = new WorkdayEmployeesDAL().CheckSend(receivedUserId, rootId, month, year);
            //DataTable dt = new DataTable();
            //dt.Columns.Add(new DataColumn("STT", typeof(int)));
            //dt.Columns.Add(new DataColumn("Phòng", typeof(string)));
            //dt.Columns.Add(new DataColumn("Gửi", typeof(bool)));

            //int stt = 0;
            //for (int i = 0; i < dtGet.Rows.Count; i++)
            //{
            //    DataRow dr = dtGet.Rows[i];
            //    stt = i + 1;
            //    DataRow drnew = dt.NewRow();
            //    drnew["STT"] = stt;
            //    drnew["Phòng"] = dr["DepartmentName"];
            //    drnew["Gửi"] = dr["SEND"];
            //    dt.Rows.Add(drnew);
            //}

            return dtGet;
        }

        public static DataTable GetByDeptIdsToDT(string deptIds, int rootId, string fullname, string sortParameter)
        {
            var list = new WorkdayEmployeesDAL().GetByDeptIds(deptIds, rootId, fullname, 0);

            return list;
        }

        #endregion

        #region private methods

        private static List<WorkdayEmployeesBLL> GenerateListWorkdayEmployeesBLLFromDataTableByNotUserId(DataTable dt,
            int notUserId)
        {
            var list = new List<WorkdayEmployeesBLL>();

            foreach (DataRow dr in dt.Rows)
            {
                var obj = GenerateWorkdayEmployeesBLLFromDataRow(dr);
                if (obj.UserId != notUserId)
                    list.Add(obj);
            }

            return list;
        }

        private static List<WorkdayEmployeesBLL> GenerateListWorkdayEmployeesBLLFromDataTable(DataTable dt)
        {
            var list = new List<WorkdayEmployeesBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateWorkdayEmployeesBLLFromDataRow(dr));

            return list;
        }

        private static WorkdayEmployeesBLL GenerateWorkdayEmployeesBLLFromDataRow(DataRow dr)
        {
            var objBLL = new WorkdayEmployeesBLL();

            objBLL.WorkdayEmployeeId = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_ID] == DBNull.Value
                ? 0
                : int.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_ID].ToString());
            objBLL.UserId = dr[EmployeeKeys.FIELD_EMPLOYEES_USERID] == DBNull.Value
                ? 0
                : int.Parse(dr[EmployeeKeys.FIELD_EMPLOYEES_USERID].ToString());
            objBLL.Type = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_Type] == DBNull.Value
                ? 0
                : int.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_Type].ToString());
            try
            {
                objBLL.EmployeeCode = dr[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE].ToString();
            }
            catch
            {
            }

            try
            {
                objBLL.FullName = dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();
            }
            catch
            {
            }

            objBLL.WorkdayDate = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_WorkdayDate] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_WorkdayDate].ToString());
            try
            {
                objBLL.DepartmentId = dr[DepartmentKeys.FIELD_DEPARTMENT_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[DepartmentKeys.FIELD_DEPARTMENT_ID].ToString());
            }
            catch
            {
            }
            try
            {
                objBLL.DepartmentName = dr[DepartmentKeys.FIELD_DEPARTMENT_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[DepartmentKeys.FIELD_DEPARTMENT_NAME].ToString();
            }
            catch
            {
            }

            try
            {
                objBLL.DepartmentFullName = dr[DepartmentKeys.FIELD_DEPARTMENT_DEPARTMENTFULLNAME] == DBNull.Value
                    ? string.Empty
                    : dr[DepartmentKeys.FIELD_DEPARTMENT_DEPARTMENTFULLNAME].ToString();
            }
            catch
            {
            }

            try
            {
                objBLL.RootId = dr[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID].ToString());
            }
            catch
            {
            }
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

            objBLL.Day1 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day1] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day1].ToString();
            objBLL.Day2 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day2] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day2].ToString();
            objBLL.Day3 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day3] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day3].ToString();
            objBLL.Day4 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day4] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day4].ToString();
            objBLL.Day5 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day5] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day5].ToString();
            objBLL.Day6 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day6] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day6].ToString();
            objBLL.Day7 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day7] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day7].ToString();
            objBLL.Day8 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day8] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day8].ToString();
            objBLL.Day9 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day9] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day9].ToString();
            objBLL.Day10 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day10] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day10].ToString();
            objBLL.Day11 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day11] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day11].ToString();
            objBLL.Day12 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day12] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day12].ToString();
            objBLL.Day13 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day13] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day13].ToString();
            objBLL.Day14 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day14] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day14].ToString();
            objBLL.Day15 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day15] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day15].ToString();
            objBLL.Day16 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day16] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day16].ToString();
            objBLL.Day17 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day17] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day17].ToString();
            objBLL.Day18 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day18] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day18].ToString();
            objBLL.Day19 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day19] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day19].ToString();
            objBLL.Day20 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day20] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day20].ToString();
            objBLL.Day21 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day21] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day21].ToString();
            objBLL.Day22 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day22] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day22].ToString();
            objBLL.Day23 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day23] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day23].ToString();
            objBLL.Day24 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day24] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day24].ToString();
            objBLL.Day25 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day25] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day25].ToString();
            objBLL.Day26 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day26] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day26].ToString();
            objBLL.Day27 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day27] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day27].ToString();
            objBLL.Day28 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day28] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day28].ToString();
            objBLL.Day29 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day29] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day29].ToString();
            objBLL.Day30 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day30] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day30].ToString();
            objBLL.Day31 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day31] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_Workday_Employee_Day31].ToString();

            #endregion

            #region To Work nights

            objBLL.Night1 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night1] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night1].ToString());
            objBLL.Night2 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night2] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night2].ToString());
            objBLL.Night3 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night3] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night3].ToString());
            objBLL.Night4 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night4] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night4].ToString());
            objBLL.Night5 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night5] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night5].ToString());
            objBLL.Night6 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night6] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night6].ToString());
            objBLL.Night7 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night7] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night7].ToString());
            objBLL.Night8 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night8] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night8].ToString());
            objBLL.Night9 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night9] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night9].ToString());
            objBLL.Night10 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night10] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night10].ToString());

            objBLL.Night11 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night11] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night11].ToString());
            objBLL.Night12 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night12] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night12].ToString());
            objBLL.Night13 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night13] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night13].ToString());
            objBLL.Night14 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night14] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night14].ToString());
            objBLL.Night15 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night15] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night15].ToString());
            objBLL.Night16 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night16] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night16].ToString());
            objBLL.Night17 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night17] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night17].ToString());
            objBLL.Night18 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night18] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night18].ToString());
            objBLL.Night19 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night19] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night19].ToString());
            objBLL.Night20 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night20] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night20].ToString());

            objBLL.Night21 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night21] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night21].ToString());
            objBLL.Night22 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night22] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night22].ToString());
            objBLL.Night23 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night23] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night23].ToString());
            objBLL.Night24 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night24] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night24].ToString());
            objBLL.Night25 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night25] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night25].ToString());
            objBLL.Night26 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night26] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night26].ToString());
            objBLL.Night27 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night27] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night27].ToString());
            objBLL.Night28 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night28] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night28].ToString());
            objBLL.Night29 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night29] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night29].ToString());
            objBLL.Night30 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night30] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night30].ToString());
            objBLL.Night31 = dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night31] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_Workday_Employee_Night31].ToString());

            objBLL.CountNight = DefaultValues.CalculateNights(objBLL.Night1, objBLL.Night2, objBLL.Night3, objBLL.Night4,
                objBLL.Night5, objBLL.Night6, objBLL.Night7, objBLL.Night8, objBLL.Night9, objBLL.Night10,
                objBLL.Night11, objBLL.Night12, objBLL.Night13, objBLL.Night14, objBLL.Night15, objBLL.Night16,
                objBLL.Night17, objBLL.Night18, objBLL.Night19, objBLL.Night20,
                objBLL.Night21, objBLL.Night22, objBLL.Night23, objBLL.Night24, objBLL.Night25, objBLL.Night26,
                objBLL.Night27, objBLL.Night28, objBLL.Night29, objBLL.Night30, objBLL.Night31);

            objBLL.GC_LamDem = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_GC_LamDem] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_GC_LamDem].ToString());

            #endregion

            #region To Collect WorkdayEmplyee

            objBLL.XQD = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_XQD] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_XQD].ToString());
            objBLL.NC_LamViec = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_NC_LAM_VIEC] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_NC_LAM_VIEC].ToString());

            objBLL.CountX = DefaultValues.CountBy(objBLL, Constants.LEAVE_TYPE_X_CODE);
            objBLL.Count1_2X = DefaultValues.CountBy(objBLL, Constants.LEAVE_TYPE_1_2_X_CODE);

            objBLL.F_Om = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_OM] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_OM].ToString());
            objBLL.F_Con_Om = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_Con_Om] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_Con_Om].ToString());
            objBLL.F_KHHDS = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_KHHDS] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_KHHDS].ToString());
            objBLL.F_TNLD = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_TNLD] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_TNLD].ToString());
            objBLL.F_O_Co_KHHDS_TNLD = objBLL.F_Om + objBLL.F_Con_Om + objBLL.F_KHHDS + objBLL.F_TNLD;


            objBLL.F_Nam = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_NAM] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_NAM].ToString());
            objBLL.F_db = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_DAC_BIET] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_DAC_BIET].ToString());
            objBLL.F_DiDuong = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_DI_DUONG] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_DI_DUONG].ToString());

            objBLL.F_Nam_Fdb_DD = objBLL.F_Nam + objBLL.F_db + objBLL.F_DiDuong;

            objBLL.F_ThaiSan = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_THAI_SAN] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_THAI_SAN].ToString());

            objBLL.F_CongTac = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_CONG_TAC] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_CONG_TAC].ToString());

            objBLL.F_HocSAGS = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HocSAGS] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HocSAGS].ToString());
            objBLL.F_Hoc1 = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC1] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC1].ToString());
            objBLL.F_Hoc2 = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC2] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC2].ToString());
            objBLL.F_Hoc3 = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC3] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC3].ToString());
            objBLL.F_Hoc4 = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC4] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC4].ToString());
            objBLL.F_Hoc5 = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC5] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC5].ToString());
            objBLL.F_Hoc6 = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC6] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC6].ToString());
            objBLL.F_Hoc7 = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC7] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HOC7].ToString());
            objBLL.F_Hoc = objBLL.F_HocSAGS + objBLL.F_Hoc1 + objBLL.F_Hoc2 + objBLL.F_Hoc3 + objBLL.F_Hoc4 +
                           objBLL.F_Hoc5 + objBLL.F_Hoc6 + objBLL.F_Hoc7;


            objBLL.F_Le = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_LE] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_LE].ToString());
            objBLL.F_KoLuongCLD = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_KO_LUONG_CLD] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_KO_LUONG_CLD].ToString());
            objBLL.F_KoLuongKLD = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_KO_LUONG_KLD] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_KO_LUONG_KLD].ToString());
            objBLL.F_SayThai = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_SayThai] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_SayThai].ToString());
            objBLL.NghiMat = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_NghiMat] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_NghiMat].ToString());
            objBLL.F_Khac = objBLL.F_Le + objBLL.F_KoLuongCLD + objBLL.F_KoLuongKLD + objBLL.F_SayThai + objBLL.NghiMat;


            objBLL.NghiTuan = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_NghiTuan] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_NghiTuan].ToString());
            objBLL.NghiBu = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_NghiBu] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_NghiBu].ToString());
            objBLL.NghiTuan_NghiBu = objBLL.NghiTuan + objBLL.NghiBu + objBLL.Count1_2X/2;


            objBLL.F_OmDaiNgay = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_OM_DaiNgay] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_OM_DaiNgay].ToString());
            objBLL.F_KhamThai = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_KhamThai] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_KhamThai].ToString());
            objBLL.F_ConChet = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_ConChet] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_ConChet].ToString());
            objBLL.F_DinhChiCongTac = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_DinhChiCongTac] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_DinhChiCongTac].ToString());
            objBLL.F_TamHoanHD = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_TamHoanHD] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_TamHoanHD].ToString());
            objBLL.F_HoiHop = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HoiHop] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_F_HoiHop].ToString());

            objBLL.NghiViec = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_NghiViec] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_NghiViec].ToString());


            objBLL.CongDu = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_CongDu] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_CongDu].ToString());
            objBLL.OldCongDu = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_OldCongDu] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_OldCongDu].ToString());
            objBLL.FinalCongDu = objBLL.OldCongDu + objBLL.CongDu;

            objBLL.Mark = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_Mark] == DBNull.Value
                ? 0
                : double.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_Mark].ToString());
            if (((objBLL.WorkdayDate.Month == 3) && (objBLL.WorkdayDate.Year == 2008)) ||
                ((objBLL.WorkdayDate.Month == 4) && (objBLL.WorkdayDate.Year == 2008)))
                objBLL.HTCV = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_HTCV] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_HTCV].ToString();
            else
                objBLL.HTCV = DefaultValues.HTCV(objBLL.Mark);

            objBLL.TotalLeaves = objBLL.F_O_Co_KHHDS_TNLD + objBLL.F_Nam_Fdb_DD + objBLL.F_Hoc + objBLL.F_CongTac +
                                 objBLL.F_ThaiSan + objBLL.F_KoLuongCLD + objBLL.F_KoLuongKLD + objBLL.F_OmDaiNgay +
                                 objBLL.F_SayThai + objBLL.F_KhamThai + objBLL.F_ConChet;

            #endregion

            objBLL.CreateDate = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_CREATE_DATE] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_CREATE_DATE].ToString());

            objBLL.CreateUser = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_UserId] == DBNull.Value
                ? 0
                : int.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_UserId].ToString());
            try
            {
                objBLL.CreateUserName = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_CreateUserName] == DBNull.Value
                    ? ""
                    : dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_CreateUserName].ToString();
            }
            catch
            {
            }
            try
            {
                objBLL.CreateFullName = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_CreateFullName] == DBNull.Value
                    ? ""
                    : dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_CreateFullName].ToString();
            }
            catch
            {
            }
            objBLL.LastUpdate = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_LastUpdate] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_LastUpdate].ToString());

            objBLL.UpdateUser = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_UpdateUser] == DBNull.Value
                ? 0
                : int.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_UpdateUser].ToString());
            try
            {
                objBLL.UpdateUserName = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_UpdateUserName] == DBNull.Value
                    ? ""
                    : dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_UpdateUserName].ToString();
            }
            catch
            {
            }
            try
            {
                objBLL.UpdateFullName = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_UpdateFullName] == DBNull.Value
                    ? ""
                    : dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_UpdateFullName].ToString();
            }
            catch
            {
            }
            objBLL.ReadUserIds = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_ReadUserIds] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_ReadUserIds].ToString();
            objBLL.WriteUserIds = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_WriteUserIds] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_WriteUserIds].ToString();

            objBLL.Status = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_Status] == DBNull.Value
                ? 0
                : int.Parse(dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_Status].ToString());

            objBLL.Remark = dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_Remark] == DBNull.Value
                ? string.Empty
                : dr[WorkdayEmployeeKeys.Field_WorkdayEmployee_Remark].ToString();

            return objBLL;
        }

        private static void UpdateWorkdayEmployee(int userId, DateTime fromDate, DateTime toDate, int leaveTypeId,
            int status)
        {
            var symbolTimekeeping = Constants.GetSymbolTimekeeping(leaveTypeId);
            var objWE = GetByUserId_Month_Year(userId, fromDate.Month, fromDate.Year, status);

            if (objWE != null)
            {
                var dateTemp = fromDate;
                while (dateTemp.CompareTo(toDate) <= 0)
                {
                    switch (dateTemp.Day)
                    {
                        case 1:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day1 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day1 = symbolTimekeeping;
                            break;
                        case 2:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day2 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day2 = symbolTimekeeping;
                            break;
                        case 3:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day3 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day3 = symbolTimekeeping;
                            break;
                        case 4:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day4 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day4 = symbolTimekeeping;
                            break;
                        case 5:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day5 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day5 = symbolTimekeeping;
                            break;
                        case 6:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day6 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day6 = symbolTimekeeping;
                            break;
                        case 7:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day7 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day7 = symbolTimekeeping;
                            break;
                        case 8:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day8 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day8 = symbolTimekeeping;
                            break;
                        case 9:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day9 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day9 = symbolTimekeeping;
                            break;
                        case 10:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day10 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day10 = symbolTimekeeping;
                            break;
                        case 11:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day11 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day11 = symbolTimekeeping;
                            break;
                        case 12:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day12 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day12 = symbolTimekeeping;
                            break;
                        case 13:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day13 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day13 = symbolTimekeeping;
                            break;
                        case 14:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day14 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day14 = symbolTimekeeping;
                            break;
                        case 15:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day15 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day15 = symbolTimekeeping;
                            break;
                        case 16:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day16 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day16 = symbolTimekeeping;
                            break;
                        case 17:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day17 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day17 = symbolTimekeeping;
                            break;
                        case 18:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day18 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day18 = symbolTimekeeping;
                            break;
                        case 19:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day19 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day19 = symbolTimekeeping;
                            break;
                        case 20:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day20 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day20 = symbolTimekeeping;
                            break;
                        case 21:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day21 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day21 = symbolTimekeeping;
                            break;
                        case 22:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day22 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day22 = symbolTimekeeping;
                            break;
                        case 23:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day23 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day23 = symbolTimekeeping;
                            break;
                        case 24:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day24 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day24 = symbolTimekeeping;
                            break;
                        case 25:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day25 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day25 = symbolTimekeeping;
                            break;
                        case 26:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day26 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day26 = symbolTimekeeping;
                            break;
                        case 27:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day27 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day27 = symbolTimekeeping;
                            break;
                        case 28:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day28 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day28 = symbolTimekeeping;
                            break;
                        case 29:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day29 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day29 = symbolTimekeeping;
                            break;
                        case 30:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day30 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day30 = symbolTimekeeping;
                            break;
                        case 31:
                            if ((dateTemp.DayOfWeek == DayOfWeek.Sunday) || (dateTemp.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day31 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            else
                                objWE.Day31 = symbolTimekeeping;
                            break;
                    }
                    dateTemp = dateTemp.AddDays(1);
                }
                ////////////////////////////////////////////////
                objWE.F_Om = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5,
                    objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_O_BAN_THAN);

                objWE.F_OmDaiNgay = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_O_DAI_NGAY);

                objWE.F_ThaiSan = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_THAI_SAN);

                objWE.F_TNLD = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_TNLD);

                objWE.F_Nam = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5,
                    objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_F_NAM);

                objWE.F_db = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5,
                    objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_FDB);

                objWE.F_KoLuongCLD = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31,
                    Constants.LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO);

                objWE.F_KoLuongKLD = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31,
                    Constants.LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO);

                objWE.F_DiDuong = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_F_DI_DUONG);

                objWE.F_CongTac = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_F_CONG_TAC);

                objWE.F_Hoc1 = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_HOC_1);
                objWE.F_Hoc2 = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_HOC_2);
                objWE.F_Hoc3 = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_HOC_3);
                objWE.F_Hoc4 = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_HOC_4);
                objWE.F_Hoc5 = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_HOC_5);
                objWE.F_Hoc6 = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_HOC_6);
                objWE.F_Hoc7 = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_HOC_7);

                objWE.F_Con_Om = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_CON_OM);

                objWE.F_KHHDS = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_KHHDS);

                objWE.F_SayThai = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_SAY_THAI);

                objWE.F_KhamThai = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_KHAM_THAI);

                objWE.F_ConChet = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31,
                    Constants.LEAVE_TYPE_CON_CHET_SAU_KHI_SINH);

                objWE.F_DinhChiCongTac = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31,
                    Constants.LEAVE_TYPE_DINH_CHI_CONG_TAC);

                objWE.F_TamHoanHD = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31,
                    Constants.LEAVE_TYPE_TAM_HOAN_HOP_DONG);

                objWE.F_HoiHop = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_HOI_HOP);

                objWE.F_Le = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5,
                    objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_LE_TET);

                objWE.NghiTuan = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_NGHI_TUAN);

                objWE.NghiBu = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_NGHI_BU);

                objWE.NghiViec = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_NGHI_VIEC);

                var x = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5,
                    objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_X);

                var nuax = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5,
                    objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_1_2_X);

                if (nuax > 0)
                    objWE.NC_LamViec = x + nuax/2;
                else
                    objWE.NC_LamViec = x;

                var hoc = objWE.F_Hoc1 + objWE.F_Hoc2 + objWE.F_Hoc3 + objWE.F_Hoc4 + objWE.F_Hoc5 + objWE.F_Hoc6 +
                          objWE.F_Hoc7;
                var om = objWE.F_Om + objWE.F_Con_Om + objWE.F_OmDaiNgay + objWE.F_TNLD;
                var thaisan = objWE.F_ThaiSan + objWE.F_KHHDS + objWE.F_KhamThai + objWE.F_SayThai + objWE.F_ConChet;
                var FFdbDD = objWE.F_Nam + objWE.F_db + objWE.F_DiDuong;
                var CtHoiHop = objWE.F_CongTac + objWE.F_HoiHop;
                var khac = objWE.F_KoLuongCLD + objWE.F_KoLuongKLD + objWE.F_TamHoanHD + objWE.F_DinhChiCongTac;

                var total = objWE.NC_LamViec + hoc + om + thaisan + FFdbDD + CtHoiHop + khac;

                //double xQD = HRMBLL.H1.Helper.DefaultValues.XQD(objWE.WorkdayDate.Month, objWE.WorkdayDate.Year);                

                objWE.CongDu = total - objWE.XQD;

                var totalNghiPhep = hoc + om + thaisan + FFdbDD + CtHoiHop + khac; // +objWE.F_Le; 
                //objWE.XL = objWE.XQDL - totalNghiPhep;

                objWE.UpdateByDate_UserId();
                ///////////////////////////////////////////////
            }
        }

        private static void UpdateWorkdayEmployeeByOfficeHours(WorkdayEmployeesBLL objWE)
        {
            //WorkdayEmployeesBLL objWE = WorkdayEmployeesBLL.GetByUserId_Month_Year(userId, month, year);

            if (objWE != null)
            {
                DateTime dt;
                var daysInNow = DateTime.DaysInMonth(objWE.WorkdayDate.Year, objWE.WorkdayDate.Month);
                var i = 1;
                while (i <= daysInNow)
                {
                    dt = new DateTime(objWE.WorkdayDate.Year, objWE.WorkdayDate.Month, i);
                    switch (i)
                    {
                        case 1:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day1 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 2:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day2 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 3:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day3 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 4:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day4 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 5:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day5 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 6:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day6 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 7:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day7 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 8:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day8 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 9:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day9 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 10:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day10 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 11:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day11 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 12:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day12 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 13:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day13 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 14:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day14 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 15:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day15 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 16:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day16 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 17:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day17 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 18:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day18 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 19:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day19 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 20:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day20 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 21:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day21 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 22:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day22 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 23:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day23 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 24:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day24 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 25:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day25 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 26:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day26 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 27:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day27 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 28:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day28 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 29:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day29 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 30:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day30 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                        case 31:
                            if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                                objWE.Day31 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
                            break;
                    }

                    i++;
                }
                ////////////////////////////////////////////////                

                objWE.NghiTuan = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4,
                    objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12,
                    objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_NGHI_TUAN);

                var x = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5,
                    objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_X);

                var nuax = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5,
                    objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_1_2_X);

                if (nuax > 0)
                    objWE.NC_LamViec = x + nuax/2;
                else
                    objWE.NC_LamViec = x;


                var hoc = objWE.F_Hoc1 + objWE.F_Hoc2 + objWE.F_Hoc3 + objWE.F_Hoc4 + objWE.F_Hoc5 + objWE.F_Hoc6 +
                          objWE.F_Hoc7;
                var om = objWE.F_Om + objWE.F_Con_Om + objWE.F_OmDaiNgay + objWE.F_TNLD;
                var thaisan = objWE.F_ThaiSan + objWE.F_KHHDS + objWE.F_KhamThai + objWE.F_SayThai + objWE.F_ConChet;
                var FFdbDD = objWE.F_Nam + objWE.F_db + objWE.F_DiDuong;
                var CtHoiHop = objWE.F_CongTac + objWE.F_HoiHop;
                var khac = objWE.F_KoLuongCLD + objWE.F_KoLuongKLD + objWE.F_TamHoanHD + objWE.F_DinhChiCongTac;

                var total = objWE.NC_LamViec + hoc + om + thaisan + FFdbDD + CtHoiHop + khac;

                //double xQD = HRMBLL.H1.Helper.DefaultValues.XQD(objWE.WorkdayDate.Month, objWE.WorkdayDate.Year);

                objWE.CongDu = total - objWE.XQD;

                var totalNghiPhep = hoc + om + thaisan + FFdbDD + CtHoiHop + khac;
                //objWE.XL = objWE.XQDL - totalNghiPhep;

                objWE.UpdateByDate_UserId();
                ///////////////////////////////////////////////
            }
        }

        private static void UpdateWorkdayEmployeeByHoliday(WorkdayEmployeesBLL objWE)
        {
            if (objWE != null)
            {
                var listHoliday = HolidaysBLL.GetByDate(objWE.WorkdayDate.Month, objWE.WorkdayDate.Year);

                foreach (var obj in listHoliday)
                    switch (obj.HolidayDate.Day)
                    {
                        case 1:
                            objWE.Day1 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 2:
                            objWE.Day2 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 3:
                            objWE.Day3 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 4:
                            objWE.Day4 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 5:
                            objWE.Day5 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 6:
                            objWE.Day6 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 7:
                            objWE.Day7 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 8:
                            objWE.Day8 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 9:
                            objWE.Day9 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 10:
                            objWE.Day10 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 11:
                            objWE.Day11 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 12:
                            objWE.Day12 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 13:
                            objWE.Day13 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 14:
                            objWE.Day14 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 15:
                            objWE.Day15 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 16:
                            objWE.Day16 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 17:
                            objWE.Day17 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 18:
                            objWE.Day18 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 19:
                            objWE.Day19 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 20:
                            objWE.Day20 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 21:
                            objWE.Day21 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 22:
                            objWE.Day22 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 23:
                            objWE.Day23 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 24:
                            objWE.Day24 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 25:
                            objWE.Day25 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 26:
                            objWE.Day26 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 27:
                            objWE.Day27 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 28:
                            objWE.Day28 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 29:
                            objWE.Day29 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 30:
                            objWE.Day30 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                        case 31:
                            objWE.Day31 = Constants.LEAVE_TYPE_LE_TET_CODE;
                            break;
                    }
                ////////////////////////////////////////////////                

                objWE.F_Le = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5,
                    objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_LE_TET);

                var x = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5,
                    objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_X);

                var nuax = DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5,
                    objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
                    objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20,
                    objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
                    objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_1_2_X);

                if (nuax > 0)
                    objWE.NC_LamViec = x + nuax/2;
                else
                    objWE.NC_LamViec = x;


                var hoc = objWE.F_Hoc1 + objWE.F_Hoc2 + objWE.F_Hoc3 + objWE.F_Hoc4 + objWE.F_Hoc5 + objWE.F_Hoc6 +
                          objWE.F_Hoc7;
                var om = objWE.F_Om + objWE.F_Con_Om + objWE.F_OmDaiNgay + objWE.F_TNLD;
                var thaisan = objWE.F_ThaiSan + objWE.F_KHHDS + objWE.F_KhamThai + objWE.F_SayThai + objWE.F_ConChet;
                var FFdbDD = objWE.F_Nam + objWE.F_db + objWE.F_DiDuong;
                var CtHoiHop = objWE.F_CongTac + objWE.F_HoiHop;
                var khac = objWE.F_KoLuongCLD + objWE.F_KoLuongKLD + objWE.F_TamHoanHD + objWE.F_DinhChiCongTac +
                           objWE.NghiMat;

                var total = objWE.NC_LamViec + hoc + om + thaisan + FFdbDD + CtHoiHop + khac;

                //double xQD = HRMBLL.H1.Helper.DefaultValues.XQD(objWE.WorkdayDate.Month, objWE.WorkdayDate.Year);

                objWE.CongDu = total - objWE.XQD;

                var totalNghiPhep = hoc + om + thaisan + FFdbDD + CtHoiHop + khac;
                //objWE.XL = objWE.XQDL - totalNghiPhep;

                objWE.UpdateByDate_UserId();
                ///////////////////////////////////////////////
            }
        }

        #endregion
    }
}