using System;
using System.Collections.Generic;
using HRMBLL.H0;
using HRMUtil;
using HRMUtil.KeyNames.H1;

namespace HRMBLL.H1.Helper
{
    public static class DefaultValues
    {
        public static int CoefficientIdMinValue
        {
            get { return 0; }
        }

        public static int TimeKeepingIdMinValue
        {
            get { return 0; }
        }

        public static int UserIdMinValue
        {
            get { return 0; }
        }

        /// <summary>
        ///     Gio cong chuan trong thang
        /// </summary>
        //public static double GIO_CONG_TRONG_THANG
        //{
        //    get
        //    {
        //        return (NC_TRONG_THANG * 8);
        //    }
        //}
        public static int CONTRACT_DATE_STANDARD
        {
            get { return 15; }
        }

        //////////////////////////////////////////////

        public static int CoefficientLevelIdMinValue
        {
            get { return 0; }
        }

        public static int CoefficientValueIdMinValue
        {
            get { return 0; }
        }

        public static int CoefficientNameIdMinValue
        {
            get { return 0; }
        }

        public static int LNS_CoefficientEmployeeIdMinValue
        {
            get { return 0; }
        }

        public static int LCB_CoefficientEmployeeIdMinValue
        {
            get { return 0; }
        }


        /// <summary>
        ///     Lay Ngay cong lam viec thuc te trong thang
        /// </summary>
        public static double XQD(int month, int year)
        {
            DateTime dt;
            var daysInNow = DateTime.DaysInMonth(year, month);
            var count = 0;
            var i = 1;
            while (i <= daysInNow)
            {
                dt = new DateTime(year, month, i);
                if ((dt.DayOfWeek == DayOfWeek.Sunday) || (dt.DayOfWeek == DayOfWeek.Saturday))
                    count++;

                //if (count > 0)
                //{
                //    i = i + 7;
                //}
                //else
                //{
                switch (dt.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        i = i + 5;
                        break;
                    case DayOfWeek.Tuesday:
                        i = i + 4;
                        break;
                    case DayOfWeek.Wednesday:
                        i = i + 3;
                        break;
                    case DayOfWeek.Thursday:
                        i = i + 2;
                        break;
                    case DayOfWeek.Friday:
                        i = i + 1;
                        break;
                    default:
                        i++;
                        break;
                }
                //}
            }

            var listHoliday = HolidaysBLL.GetByDate(month, year);
            count = count + listHoliday.Count;

            return daysInNow - count;
        }

        public static double XQDSalary(int month, int year)
        {
            DateTime dt;
            var daysInNow = DateTime.DaysInMonth(year, month);
            var count = 0;
            var i = 1;
            while (i <= daysInNow)
            {
                dt = new DateTime(year, month, i);
                if (dt.DayOfWeek == DayOfWeek.Sunday)
                    count++;

                switch (dt.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        i = i + 5;
                        break;
                    case DayOfWeek.Tuesday:
                        i = i + 4;
                        break;
                    case DayOfWeek.Wednesday:
                        i = i + 3;
                        break;
                    case DayOfWeek.Thursday:
                        i = i + 2;
                        break;
                    case DayOfWeek.Friday:
                        i = i + 1;
                        break;
                    default:
                        i++;
                        break;
                }
                //}
            }

            return daysInNow - count;
        }

        public static double XQDSalaryMinusHoliday(int month, int year)
        {
            DateTime dt;
            var daysInNow = DateTime.DaysInMonth(year, month);
            var count = 0;
            var i = 1;
            while (i <= daysInNow)
            {
                dt = new DateTime(year, month, i);
                if (dt.DayOfWeek == DayOfWeek.Sunday)
                    count++;

                switch (dt.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        i = i + 5;
                        break;
                    case DayOfWeek.Tuesday:
                        i = i + 4;
                        break;
                    case DayOfWeek.Wednesday:
                        i = i + 3;
                        break;
                    case DayOfWeek.Thursday:
                        i = i + 2;
                        break;
                    case DayOfWeek.Friday:
                        i = i + 1;
                        break;
                    default:
                        i++;
                        break;
                }
                //}
            }

            var listHoliday = HolidaysBLL.GetByDate(month, year);
            count = count + listHoliday.Count;

            return daysInNow - count;
        }

        public static string HTCV(double mark)
        {
            if ((mark >= 101) && (mark <= 115))
                return "A1";
            if ((mark >= 91) && (mark <= 100))
                return "A";
            if ((mark >= 76) && (mark <= 90))
                return "B";
            if ((mark >= 66) && (mark <= 75))
                return "C";
            return "D";
        }

        public static bool IsBonus(string htcv)
        {
            if (htcv.Equals("C") & htcv.Equals("D"))
                return true;
            return false;
        }

        public static void MarkHTCV(string rank, ref int minMark, ref int maxMark)
        {
            if (rank.Equals("A1"))
            {
                minMark = 101;
                maxMark = 115;
            }
            else if (rank.Equals("A"))
            {
                minMark = 91;
                maxMark = 100;
            }
            else if (rank.Equals("B"))
            {
                minMark = 76;
                maxMark = 90;
            }
            else if (rank.Equals("C"))
            {
                minMark = 66;
                maxMark = 75;
            }
            else
            {
                minMark = 0;
                maxMark = 65;
            }
        }

        public static double K(double mark)
        {
            return mark/100;
        }

        /// <summary>
        ///     Ngay cong lam viec thuc te tu ngay A den ngay B
        /// </summary>
        public static double WorkdayByDate(DateTime fromDate, DateTime toDate)
        {
            var dtTemp = fromDate;

            double days = 0;

            while (dtTemp.Equals(toDate))
            {
                dtTemp = dtTemp.AddDays(1);
                if (dtTemp.DayOfWeek != DayOfWeek.Sunday)
                    days++;
            }
            return days;
        }

        /// <summary>
        ///     Get saturday by fromDate and toDate
        /// </summary>
        public static double GetCountSaturday(DateTime fromDate, DateTime toDate)
        {
            var dtTemp = fromDate;

            double days = 0;

            while (!dtTemp.Equals(toDate))
            {
                dtTemp = dtTemp.AddDays(1);
                if (dtTemp.DayOfWeek == DayOfWeek.Saturday)
                    days++;
            }
            return days;
        }

        /// <summary>
        ///     Get saturday by fromDate and toDate
        /// </summary>
        public static double GetCountSaturday(int month, int year)
        {
            var dtTemp = new DateTime(year, month, 1);
            var toDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            double days = 0;

            while (!dtTemp.Equals(toDate))
            {
                dtTemp = dtTemp.AddDays(1);
                if (dtTemp.DayOfWeek == DayOfWeek.Saturday)
                    days++;

                //switch (dtTemp.DayOfWeek)
                //{
                //    case DayOfWeek.Monday:                        
                //        dtTemp = dtTemp.AddDays(5);
                //        break;
                //    case DayOfWeek.Tuesday:
                //        dtTemp = dtTemp.AddDays(4);
                //        break;
                //    case DayOfWeek.Wednesday:
                //        dtTemp = dtTemp.AddDays(3);
                //        break;
                //    case DayOfWeek.Thursday:
                //        dtTemp = dtTemp.AddDays(2);
                //        break;
                //    case DayOfWeek.Friday:
                //        dtTemp = dtTemp.AddDays(1);
                //        break;
                //    default:
                //        dtTemp = dtTemp.AddDays(1);
                //        break;
                //}
            }
            return days;
        }

        public static double CalculateLeaveDay(string Day1, string Day2, string Day3, string Day4, string Day5,
            string Day6,
            string Day7, string Day8, string Day9, string Day10, string Day11, string Day12, string Day13,
            string Day14, string Day15, string Day16, string Day17, string Day18, string Day19, string Day20,
            string Day21, string Day22, string Day23, string Day24, string Day25, string Day26, string Day27,
            string Day28, string Day29, string Day30, string Day31, int leaveType)
        {
            double countLeaveDays = 0;
            var leaveTypeCode = Constants.GetSymbolTimekeeping(leaveType);
            var nuagioSymbol = "1/2" + leaveTypeCode;

            #region day

            if ((Day1 != null) && (Day1 != ""))
                if (Day1.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day1.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day2 != null) && (Day2 != ""))
                if (Day2.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day2.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day3 != null) && (Day3 != ""))
                if (Day3.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day3.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day4 != null) && (Day4 != ""))
                if (Day4.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day4.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day5 != null) && (Day5 != ""))
                if (Day5.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day5.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day6 != null) && (Day6 != ""))
                if (Day6.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day6.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day7 != null) && (Day7 != ""))
                if (Day7.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day7.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day8 != null) && (Day8 != ""))
                if (Day8.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day8.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day9 != null) && (Day9 != ""))
                if (Day9.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day9.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day10 != null) && (Day10 != ""))
                if (Day10.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day10.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day11 != null) && (Day11 != ""))
                if (Day11.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day11.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day12 != null) && (Day12 != ""))
                if (Day12.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day12.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day13 != null) && (Day13 != ""))
                if (Day13.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day13.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day14 != null) && (Day14 != ""))
                if (Day14.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day14.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day15 != null) && (Day15 != ""))
                if (Day15.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day15.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day16 != null) && (Day16 != ""))
                if (Day16.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day16.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day17 != null) && (Day17 != ""))
                if (Day17.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day17.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day18 != null) && (Day18 != ""))
                if (Day18.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day18.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day19 != null) && (Day19 != ""))
                if (Day19.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day19.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day20 != null) && (Day20 != ""))
                if (Day20.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day20.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day21 != null) && (Day21 != ""))
                if (Day21.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day21.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day22 != null) && (Day22 != ""))
                if (Day22.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day22.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day23 != null) && (Day23 != ""))
                if (Day23.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day23.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day24 != null) && (Day24 != ""))
                if (Day24.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day24.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day25 != null) && (Day25 != ""))
                if (Day25.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day25.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day26 != null) && (Day26 != ""))
                if (Day26.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day26.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day27 != null) && (Day27 != ""))
                if (Day27.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day27.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day28 != null) && (Day28 != ""))
                if (Day28.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day28.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day29 != null) && (Day29 != ""))
                if (Day29.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day29.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day30 != null) && (Day30 != ""))
                if (Day30.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day30.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day31 != null) && (Day31 != ""))
                if (Day31.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day31.Equals(leaveTypeCode))
                    countLeaveDays++;

            #endregion

            return countLeaveDays; //> 0 ? countLeaveDays : Constants.WorkdayEmployee_DefaultValue;
        }

        public static double CalculateNights(double night1, double night2, double night3, double night4, double night5,
            double night6, double night7, double night8, double night9, double night10,
            double night11, double night12, double night13, double night14, double night15, double night16,
            double night17, double night18, double night19, double night20,
            double night21, double night22, double night23, double night24, double night25, double night26,
            double night27, double night28, double night29, double night30, double night31)
        {
            double countNight = 0;

            if (night1 > 0)
                countNight++;
            if (night2 > 0)
                countNight++;
            if (night3 > 0)
                countNight++;
            if (night4 > 0)
                countNight++;
            if (night5 > 0)
                countNight++;
            if (night6 > 0)
                countNight++;
            if (night7 > 0)
                countNight++;
            if (night8 > 0)
                countNight++;
            if (night9 > 0)
                countNight++;
            if (night10 > 0)
                countNight++;

            if (night11 > 0)
                countNight++;
            if (night12 > 0)
                countNight++;
            if (night13 > 0)
                countNight++;
            if (night14 > 0)
                countNight++;
            if (night15 > 0)
                countNight++;
            if (night16 > 0)
                countNight++;
            if (night17 > 0)
                countNight++;
            if (night18 > 0)
                countNight++;
            if (night19 > 0)
                countNight++;
            if (night20 > 0)
                countNight++;

            if (night21 > 0)
                countNight++;
            if (night22 > 0)
                countNight++;
            if (night23 > 0)
                countNight++;
            if (night24 > 0)
                countNight++;
            if (night25 > 0)
                countNight++;
            if (night26 > 0)
                countNight++;
            if (night27 > 0)
                countNight++;
            if (night28 > 0)
                countNight++;
            if (night29 > 0)
                countNight++;
            if (night30 > 0)
                countNight++;
            if (night31 > 0)
                countNight++;


            return countNight;
        }

        public static double CountBy(WorkdayEmployeesBLL objWE, string leaveTypeCode)
        {
            double countLeaveDays = 0;
            //string leaveTypeCode = Constants.GetSymbolTimekeeping(leaveType);
            var nuagioSymbol = "1/2" + leaveTypeCode;

            #region day

            if ((objWE.Day1 != null) && (objWE.Day1 != ""))
                if (objWE.Day1.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day1.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day2 != null) && (objWE.Day2 != ""))
                if (objWE.Day2.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day2.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day3 != null) && (objWE.Day3 != ""))
                if (objWE.Day3.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day3.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day4 != null) && (objWE.Day4 != ""))
                if (objWE.Day4.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day4.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day5 != null) && (objWE.Day5 != ""))
                if (objWE.Day5.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day5.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day6 != null) && (objWE.Day6 != ""))
                if (objWE.Day6.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day6.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day7 != null) && (objWE.Day7 != ""))
                if (objWE.Day7.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day7.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day8 != null) && (objWE.Day8 != ""))
                if (objWE.Day8.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day8.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day9 != null) && (objWE.Day9 != ""))
                if (objWE.Day9.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day9.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day10 != null) && (objWE.Day10 != ""))
                if (objWE.Day10.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day10.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day11 != null) && (objWE.Day11 != ""))
                if (objWE.Day11.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day11.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day12 != null) && (objWE.Day12 != ""))
                if (objWE.Day12.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day12.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day13 != null) && (objWE.Day13 != ""))
                if (objWE.Day13.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day13.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day14 != null) && (objWE.Day14 != ""))
                if (objWE.Day14.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day14.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day15 != null) && (objWE.Day15 != ""))
                if (objWE.Day15.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day15.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day16 != null) && (objWE.Day16 != ""))
                if (objWE.Day16.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day16.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day17 != null) && (objWE.Day17 != ""))
                if (objWE.Day17.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day17.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day18 != null) && (objWE.Day18 != ""))
                if (objWE.Day18.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day18.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day19 != null) && (objWE.Day19 != ""))
                if (objWE.Day19.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day19.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day20 != null) && (objWE.Day20 != ""))
                if (objWE.Day20.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day20.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day21 != null) && (objWE.Day21 != ""))
                if (objWE.Day21.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day21.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day22 != null) && (objWE.Day22 != ""))
                if (objWE.Day22.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day22.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day23 != null) && (objWE.Day23 != ""))
                if (objWE.Day23.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day23.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day24 != null) && (objWE.Day24 != ""))
                if (objWE.Day24.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day24.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day25 != null) && (objWE.Day25 != ""))
                if (objWE.Day25.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day25.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day26 != null) && (objWE.Day26 != ""))
                if (objWE.Day26.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day26.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day27 != null) && (objWE.Day27 != ""))
                if (objWE.Day27.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day27.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day28 != null) && (objWE.Day28 != ""))
                if (objWE.Day28.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day28.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day29 != null) && (objWE.Day29 != ""))
                if (objWE.Day29.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day29.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day30 != null) && (objWE.Day30 != ""))
                if (objWE.Day30.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day30.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((objWE.Day31 != null) && (objWE.Day31 != ""))
                if (objWE.Day31.Contains(nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (objWE.Day31.Equals(leaveTypeCode))
                    countLeaveDays++;

            #endregion

            return countLeaveDays;
        }

        public static double CountDayByContract(WorkdayEmployeesBLLL objWE, EmployeeContractBLL objEC,
            string leaveTypeCode)
        {
            double countLeaveDays = 0;
            var nuagioSymbol = "1/2" + leaveTypeCode;

            var listLeave = new List<DateTime>();

            var fromDate = objEC.FromDate;
            var toDate = objEC.ToDate;
            var daysInMonths = DateTime.DaysInMonth(objWE.WorkdayDateL.Year, objWE.WorkdayDateL.Month);
            if (objWE.WorkdayDateL.Month == objEC.FromDate.Month)
            {
                fromDate = objEC.FromDate;
                toDate = new DateTime(objWE.WorkdayDateL.Year, objWE.WorkdayDateL.Month, daysInMonths);
            }
            else if (objWE.WorkdayDateL.Month == objEC.ToDate.Month)
            {
                fromDate = new DateTime(objWE.WorkdayDateL.Year, objWE.WorkdayDateL.Month, 1);
                toDate = objEC.ToDate;
            }

            var countDay = fromDate.Day;

            while (countDay <= toDate.Day)
            {
                #region switch 1

                switch (countDay)
                {
                    case 1:
                        if ((objWE.Day1L != null) && (objWE.Day1L != ""))
                            if (objWE.Day1L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 1));
                            }
                            else if (objWE.Day1L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 1));
                            }
                        break;
                    case 2:
                        if ((objWE.Day2L != null) && (objWE.Day2L != ""))
                            if (objWE.Day2L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 2));
                            }
                            else if (objWE.Day2L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 2));
                            }
                        break;
                    case 3:
                        if ((objWE.Day3L != null) && (objWE.Day3L != ""))
                            if (objWE.Day3L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 3));
                            }
                            else if (objWE.Day3L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 3));
                            }
                        break;
                    case 4:
                        if ((objWE.Day4L != null) && (objWE.Day4L != ""))
                            if (objWE.Day4L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 4));
                            }
                            else if (objWE.Day4L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 4));
                            }
                        break;
                    case 5:
                        if ((objWE.Day5L != null) && (objWE.Day5L != ""))
                            if (objWE.Day5L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 5));
                            }
                            else if (objWE.Day5L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 5));
                            }
                        break;
                    case 6:
                        if ((objWE.Day6L != null) && (objWE.Day6L != ""))
                            if (objWE.Day6L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 6));
                            }
                            else if (objWE.Day6L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 6));
                            }
                        break;
                    case 7:
                        if ((objWE.Day7L != null) && (objWE.Day7L != ""))
                            if (objWE.Day7L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 7));
                            }
                            else if (objWE.Day7L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 7));
                            }
                        break;
                    case 8:
                        if ((objWE.Day8L != null) && (objWE.Day8L != ""))
                            if (objWE.Day8L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 8));
                            }
                            else if (objWE.Day8L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 8));
                            }
                        break;
                    case 9:
                        if ((objWE.Day9L != null) && (objWE.Day9L != ""))
                            if (objWE.Day9L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 9));
                            }
                            else if (objWE.Day9L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 9));
                            }
                        break;
                    case 10:
                        if ((objWE.Day10L != null) && (objWE.Day10L != ""))
                            if (objWE.Day10L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 10));
                            }
                            else if (objWE.Day10L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 10));
                            }
                        break;
                    case 11:
                        if ((objWE.Day11L != null) && (objWE.Day11L != ""))
                            if (objWE.Day11L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 11));
                            }
                            else if (objWE.Day11L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 11));
                            }
                        break;
                    case 12:
                        if ((objWE.Day12L != null) && (objWE.Day12L != ""))
                            if (objWE.Day12L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 12));
                            }
                            else if (objWE.Day12L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 12));
                            }
                        break;
                    case 13:
                        if ((objWE.Day13L != null) && (objWE.Day13L != ""))
                            if (objWE.Day13L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 13));
                            }
                            else if (objWE.Day13L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 13));
                            }
                        break;
                    case 14:
                        if ((objWE.Day14L != null) && (objWE.Day14L != ""))
                            if (objWE.Day14L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 14));
                            }
                            else if (objWE.Day14L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 14));
                            }
                        break;
                    case 15:
                        if ((objWE.Day15L != null) && (objWE.Day15L != ""))
                            if (objWE.Day15L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 15));
                            }
                            else if (objWE.Day15L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 15));
                            }
                        break;
                    case 16:
                        if ((objWE.Day16L != null) && (objWE.Day16L != ""))
                            if (objWE.Day16L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 16));
                            }
                            else if (objWE.Day16L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 16));
                            }
                        break;
                    case 17:
                        if ((objWE.Day17L != null) && (objWE.Day17L != ""))
                            if (objWE.Day17L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 17));
                            }
                            else if (objWE.Day17L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 17));
                            }
                        break;
                    case 18:
                        if ((objWE.Day18L != null) && (objWE.Day18L != ""))
                            if (objWE.Day18L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 18));
                            }
                            else if (objWE.Day18L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 18));
                            }
                        break;
                    case 19:
                        if ((objWE.Day19L != null) && (objWE.Day19L != ""))
                            if (objWE.Day19L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 19));
                            }
                            else if (objWE.Day19L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 19));
                            }
                        break;
                    case 20:
                        if ((objWE.Day20L != null) && (objWE.Day20L != ""))
                            if (objWE.Day20L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 20));
                            }
                            else if (objWE.Day20L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 20));
                            }
                        break;
                    case 21:
                        if ((objWE.Day21L != null) && (objWE.Day21L != ""))
                            if (objWE.Day21L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 21));
                            }
                            else if (objWE.Day21L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 21));
                            }
                        break;
                    case 22:
                        if ((objWE.Day22L != null) && (objWE.Day22L != ""))
                            if (objWE.Day22L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 22));
                            }
                            else if (objWE.Day22L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 22));
                            }
                        break;
                    case 23:
                        if ((objWE.Day23L != null) && (objWE.Day23L != ""))
                            if (objWE.Day23L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 23));
                            }
                            else if (objWE.Day23L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 23));
                            }
                        break;
                    case 24:
                        if ((objWE.Day24L != null) && (objWE.Day24L != ""))
                            if (objWE.Day24L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 24));
                            }
                            else if (objWE.Day24L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 24));
                            }
                        break;
                    case 25:
                        if ((objWE.Day25L != null) && (objWE.Day25L != ""))
                            if (objWE.Day25L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 25));
                            }
                            else if (objWE.Day25L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 25));
                            }
                        break;
                    case 26:
                        if ((objWE.Day26L != null) && (objWE.Day26L != ""))
                            if (objWE.Day26L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 26));
                            }
                            else if (objWE.Day26L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 26));
                            }
                        break;
                    case 27:
                        if ((objWE.Day27L != null) && (objWE.Day27L != ""))
                            if (objWE.Day27L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 27));
                            }
                            else if (objWE.Day27L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 27));
                            }
                        break;
                    case 28:
                        if ((objWE.Day28L != null) && (objWE.Day28L != ""))
                            if (objWE.Day28L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 28));
                            }
                            else if (objWE.Day28L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 28));
                            }
                        break;
                    case 29:
                        if ((objWE.Day29L != null) && (objWE.Day29L != ""))
                            if (objWE.Day29L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 29));
                            }
                            else if (objWE.Day29L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 29));
                            }
                        break;
                    case 30:
                        if ((objWE.Day30L != null) && (objWE.Day30L != ""))
                            if (objWE.Day30L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 30));
                            }
                            else if (objWE.Day30L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 30));
                            }
                        break;
                    case 31:
                        if ((objWE.Day31L != null) && (objWE.Day31L != ""))
                            if (objWE.Day31L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 31));
                            }
                            else if (objWE.Day31L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 31));
                            }
                        break;
                }

                #endregion

                countDay++;
            }

            return countLeaveDays;
        }

        public static double CountDayByFromToDate(WorkdayEmployeesBLLL objWE, DateTime fromDateInput,
            DateTime toDateInput, string leaveTypeCode)
        {
            double countLeaveDays = 0;
            var nuagioSymbol = "1/2" + leaveTypeCode;

            var listLeave = new List<DateTime>();

            var fromDate = fromDateInput;
            var toDate = toDateInput;
            var daysInMonths = DateTime.DaysInMonth(objWE.WorkdayDateL.Year, objWE.WorkdayDateL.Month);
            if (objWE.WorkdayDateL.Month == fromDateInput.Month)
            {
                fromDate = fromDateInput;
                toDate = new DateTime(objWE.WorkdayDateL.Year, objWE.WorkdayDateL.Month, daysInMonths);
            }
            else if (objWE.WorkdayDateL.Month == toDateInput.Month)
            {
                fromDate = new DateTime(objWE.WorkdayDateL.Year, objWE.WorkdayDateL.Month, 1);
                toDate = toDateInput;
            }

            var countDay = fromDate.Day;

            while (countDay <= toDate.Day)
            {
                #region switch 1

                switch (countDay)
                {
                    case 1:
                        if ((objWE.Day1L != null) && (objWE.Day1L != ""))
                            if (objWE.Day1L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 1));
                            }
                            else if (objWE.Day1L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 1));
                            }
                        break;
                    case 2:
                        if ((objWE.Day2L != null) && (objWE.Day2L != ""))
                            if (objWE.Day2L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 2));
                            }
                            else if (objWE.Day2L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 2));
                            }
                        break;
                    case 3:
                        if ((objWE.Day3L != null) && (objWE.Day3L != ""))
                            if (objWE.Day3L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 3));
                            }
                            else if (objWE.Day3L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 3));
                            }
                        break;
                    case 4:
                        if ((objWE.Day4L != null) && (objWE.Day4L != ""))
                            if (objWE.Day4L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 4));
                            }
                            else if (objWE.Day4L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 4));
                            }
                        break;
                    case 5:
                        if ((objWE.Day5L != null) && (objWE.Day5L != ""))
                            if (objWE.Day5L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 5));
                            }
                            else if (objWE.Day5L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 5));
                            }
                        break;
                    case 6:
                        if ((objWE.Day6L != null) && (objWE.Day6L != ""))
                            if (objWE.Day6L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 6));
                            }
                            else if (objWE.Day6L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 6));
                            }
                        break;
                    case 7:
                        if ((objWE.Day7L != null) && (objWE.Day7L != ""))
                            if (objWE.Day7L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 7));
                            }
                            else if (objWE.Day7L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 7));
                            }
                        break;
                    case 8:
                        if ((objWE.Day8L != null) && (objWE.Day8L != ""))
                            if (objWE.Day8L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 8));
                            }
                            else if (objWE.Day8L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 8));
                            }
                        break;
                    case 9:
                        if ((objWE.Day9L != null) && (objWE.Day9L != ""))
                            if (objWE.Day9L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 9));
                            }
                            else if (objWE.Day9L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 9));
                            }
                        break;
                    case 10:
                        if ((objWE.Day10L != null) && (objWE.Day10L != ""))
                            if (objWE.Day10L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 10));
                            }
                            else if (objWE.Day10L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 10));
                            }
                        break;
                    case 11:
                        if ((objWE.Day11L != null) && (objWE.Day11L != ""))
                            if (objWE.Day11L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 11));
                            }
                            else if (objWE.Day11L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 11));
                            }
                        break;
                    case 12:
                        if ((objWE.Day12L != null) && (objWE.Day12L != ""))
                            if (objWE.Day12L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 12));
                            }
                            else if (objWE.Day12L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 12));
                            }
                        break;
                    case 13:
                        if ((objWE.Day13L != null) && (objWE.Day13L != ""))
                            if (objWE.Day13L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 13));
                            }
                            else if (objWE.Day13L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 13));
                            }
                        break;
                    case 14:
                        if ((objWE.Day14L != null) && (objWE.Day14L != ""))
                            if (objWE.Day14L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 14));
                            }
                            else if (objWE.Day14L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 14));
                            }
                        break;
                    case 15:
                        if ((objWE.Day15L != null) && (objWE.Day15L != ""))
                            if (objWE.Day15L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 15));
                            }
                            else if (objWE.Day15L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 15));
                            }
                        break;
                    case 16:
                        if ((objWE.Day16L != null) && (objWE.Day16L != ""))
                            if (objWE.Day16L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 16));
                            }
                            else if (objWE.Day16L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 16));
                            }
                        break;
                    case 17:
                        if ((objWE.Day17L != null) && (objWE.Day17L != ""))
                            if (objWE.Day17L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 17));
                            }
                            else if (objWE.Day17L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 17));
                            }
                        break;
                    case 18:
                        if ((objWE.Day18L != null) && (objWE.Day18L != ""))
                            if (objWE.Day18L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 18));
                            }
                            else if (objWE.Day18L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 18));
                            }
                        break;
                    case 19:
                        if ((objWE.Day19L != null) && (objWE.Day19L != ""))
                            if (objWE.Day19L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 19));
                            }
                            else if (objWE.Day19L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 19));
                            }
                        break;
                    case 20:
                        if ((objWE.Day20L != null) && (objWE.Day20L != ""))
                            if (objWE.Day20L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 20));
                            }
                            else if (objWE.Day20L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 20));
                            }
                        break;
                    case 21:
                        if ((objWE.Day21L != null) && (objWE.Day21L != ""))
                            if (objWE.Day21L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 21));
                            }
                            else if (objWE.Day21L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 21));
                            }
                        break;
                    case 22:
                        if ((objWE.Day22L != null) && (objWE.Day22L != ""))
                            if (objWE.Day22L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 22));
                            }
                            else if (objWE.Day22L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 22));
                            }
                        break;
                    case 23:
                        if ((objWE.Day23L != null) && (objWE.Day23L != ""))
                            if (objWE.Day23L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 23));
                            }
                            else if (objWE.Day23L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 23));
                            }
                        break;
                    case 24:
                        if ((objWE.Day24L != null) && (objWE.Day24L != ""))
                            if (objWE.Day24L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 24));
                            }
                            else if (objWE.Day24L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 24));
                            }
                        break;
                    case 25:
                        if ((objWE.Day25L != null) && (objWE.Day25L != ""))
                            if (objWE.Day25L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 25));
                            }
                            else if (objWE.Day25L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 25));
                            }
                        break;
                    case 26:
                        if ((objWE.Day26L != null) && (objWE.Day26L != ""))
                            if (objWE.Day26L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 26));
                            }
                            else if (objWE.Day26L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 26));
                            }
                        break;
                    case 27:
                        if ((objWE.Day27L != null) && (objWE.Day27L != ""))
                            if (objWE.Day27L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 27));
                            }
                            else if (objWE.Day27L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 27));
                            }
                        break;
                    case 28:
                        if ((objWE.Day28L != null) && (objWE.Day28L != ""))
                            if (objWE.Day28L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 28));
                            }
                            else if (objWE.Day28L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 28));
                            }
                        break;
                    case 29:
                        if ((objWE.Day29L != null) && (objWE.Day29L != ""))
                            if (objWE.Day29L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 29));
                            }
                            else if (objWE.Day29L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 29));
                            }
                        break;
                    case 30:
                        if ((objWE.Day30L != null) && (objWE.Day30L != ""))
                            if (objWE.Day30L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 30));
                            }
                            else if (objWE.Day30L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 30));
                            }
                        break;
                    case 31:
                        if ((objWE.Day31L != null) && (objWE.Day31L != ""))
                            if (objWE.Day31L.Contains(nuagioSymbol))
                            {
                                countLeaveDays = countLeaveDays + 0.5;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 31));
                            }
                            else if (objWE.Day31L.Equals(leaveTypeCode))
                            {
                                countLeaveDays++;
                                listLeave.Add(new DateTime(fromDate.Year, fromDate.Month, 31));
                            }
                        break;
                }

                #endregion

                countDay++;
            }

            return countLeaveDays;
        }

        public static double RealWorkdayByDate(int month, int year, bool isGioHanhChinh)
        {
            DateTime dt;
            var daysInNow = DateTime.DaysInMonth(year, month);
            var count = 0;
            var i = 1;
            while (i <= daysInNow)
            {
                dt = new DateTime(year, month, i);
                if (dt.DayOfWeek == DayOfWeek.Sunday)
                    count++;

                if (count > 0)
                    i = i + 7;
                else
                    switch (dt.DayOfWeek)
                    {
                        case DayOfWeek.Monday:
                            i = i + 6;
                            break;
                        case DayOfWeek.Tuesday:
                            i = i + 5;
                            break;
                        case DayOfWeek.Wednesday:
                            i = i + 4;
                            break;
                        case DayOfWeek.Thursday:
                            i = i + 3;
                            break;
                        case DayOfWeek.Friday:
                            i = i + 2;
                            break;
                        default:
                            i++;
                            break;
                    }
            }
            if (isGioHanhChinh)
                return daysInNow - count;
            return daysInNow;
        }


        public static double CalculateLeaveDay(int month, int year, int userid, string Day1, string Day2, string Day3,
            string Day4, string Day5, string Day6,
            string Day7, string Day8, string Day9, string Day10, string Day11, string Day12, string Day13,
            string Day14, string Day15, string Day16, string Day17, string Day18, string Day19, string Day20,
            string Day21, string Day22, string Day23, string Day24, string Day25, string Day26, string Day27,
            string Day28, string Day29, string Day30, string Day31, int leaveType)
        {
            double countLeaveDays = 0;
            var leaveTypeCode = Constants.GetSymbolTimekeeping(leaveType);
            var nuagioSymbol = "1/2" + leaveTypeCode;

            var listLeave = new List<DateTime>();

            #region day

            if ((Day1 != null) && (Day1 != ""))
                if (Day1.Contains(nuagioSymbol + "+") || Day1.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 1));
                }
                else if (Day1.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 1));
                }
            if ((Day2 != null) && (Day2 != ""))
                if (Day2.Contains(nuagioSymbol + "+") || Day2.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 2));
                }
                else if (Day2.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 2));
                }
            if ((Day3 != null) && (Day3 != ""))
                if (Day3.Contains(nuagioSymbol + "+") || Day3.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 3));
                }
                else if (Day3.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 3));
                }
            if ((Day4 != null) && (Day4 != ""))
                if (Day4.Contains(nuagioSymbol + "+") || Day4.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 4));
                }
                else if (Day4.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 4));
                }
            if ((Day5 != null) && (Day5 != ""))
                if (Day5.Contains(nuagioSymbol + "+") || Day5.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 5));
                }
                else if (Day5.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 5));
                }
            if ((Day6 != null) && (Day6 != ""))
                if (Day6.Contains(nuagioSymbol + "+") || Day6.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 6));
                }
                else if (Day6.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 6));
                }
            if ((Day7 != null) && (Day7 != ""))
                if (Day7.Contains(nuagioSymbol + "+") || Day7.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 7));
                }
                else if (Day7.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 7));
                }
            if ((Day8 != null) && (Day8 != ""))
                if (Day8.Contains(nuagioSymbol + "+") || Day8.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 8));
                }
                else if (Day8.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 8));
                }
            if ((Day9 != null) && (Day9 != ""))
                if (Day9.Contains(nuagioSymbol + "+") || Day9.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 9));
                }
                else if (Day9.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 9));
                }
            if ((Day10 != null) && (Day10 != ""))
                if (Day10.Contains(nuagioSymbol + "+") || Day10.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 10));
                }
                else if (Day10.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 10));
                }
            if ((Day11 != null) && (Day11 != ""))
                if (Day11.Contains(nuagioSymbol + "+") || Day11.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 11));
                }
                else if (Day11.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 11));
                }
            if ((Day12 != null) && (Day12 != ""))
                if (Day12.Contains(nuagioSymbol + "+") || Day12.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 12));
                }
                else if (Day12.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 12));
                }
            if ((Day13 != null) && (Day13 != ""))
                if (Day13.Contains(nuagioSymbol + "+") || Day13.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 13));
                }
                else if (Day13.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 13));
                }
            if ((Day14 != null) && (Day14 != ""))
                if (Day14.Contains(nuagioSymbol + "+") || Day14.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 14));
                }
                else if (Day14.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 14));
                }
            if ((Day15 != null) && (Day15 != ""))
                if (Day15.Contains(nuagioSymbol + "+") || Day15.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 15));
                }
                else if (Day15.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 15));
                }
            if ((Day16 != null) && (Day16 != ""))
                if (Day16.Contains(nuagioSymbol + "+") || Day16.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 16));
                }
                else if (Day16.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 16));
                }
            if ((Day17 != null) && (Day17 != ""))
                if (Day17.Contains(nuagioSymbol + "+") || Day17.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 17));
                }
                else if (Day17.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 17));
                }
            if ((Day18 != null) && (Day18 != ""))
                if (Day18.Contains(nuagioSymbol + "+") || Day18.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 18));
                }
                else if (Day18.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 18));
                }
            if ((Day19 != null) && (Day19 != ""))
                if (Day19.Contains(nuagioSymbol + "+") || Day19.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 19));
                }
                else if (Day19.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 19));
                }
            if ((Day20 != null) && (Day20 != ""))
                if (Day20.Contains(nuagioSymbol + "+") || Day20.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 20));
                }
                else if (Day20.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 20));
                }
            if ((Day21 != null) && (Day21 != ""))
                if (Day21.Contains(nuagioSymbol + "+") || Day21.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 21));
                }
                else if (Day21.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 21));
                }
            if ((Day22 != null) && (Day22 != ""))
                if (Day22.Contains(nuagioSymbol + "+") || Day22.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 22));
                }
                else if (Day22.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 22));
                }
            if ((Day23 != null) && (Day23 != ""))
                if (Day23.Contains(nuagioSymbol + "+") || Day23.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 23));
                }
                else if (Day23.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 23));
                }
            if ((Day24 != null) && (Day24 != ""))
                if (Day24.Contains(nuagioSymbol + "+") || Day24.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 24));
                }
                else if (Day24.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 24));
                }
            if ((Day25 != null) && (Day25 != ""))
                if (Day25.Contains(nuagioSymbol + "+") || Day25.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 25));
                }
                else if (Day25.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 25));
                }
            if ((Day26 != null) && (Day26 != ""))
                if (Day26.Contains(nuagioSymbol + "+") || Day26.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 26));
                }
                else if (Day26.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 26));
                }
            if ((Day27 != null) && (Day27 != ""))
                if (Day27.Contains(nuagioSymbol + "+") || Day27.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 27));
                }
                else if (Day27.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 27));
                }
            if ((Day28 != null) && (Day28 != ""))
                if (Day28.Contains(nuagioSymbol + "+") || Day28.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 28));
                }
                else if (Day28.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 28));
                }
            if ((Day29 != null) && (Day29 != ""))
                if (Day29.Contains(nuagioSymbol + "+") || Day29.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 29));
                }
                else if (Day29.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 29));
                }
            if ((Day30 != null) && (Day30 != ""))
                if (Day30.Contains(nuagioSymbol + "+") || Day30.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 30));
                }
                else if (Day30.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 30));
                }
            if ((Day31 != null) && (Day31 != ""))
                if (Day31.Contains(nuagioSymbol + "+") || Day31.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 31));
                }
                else if (Day31.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 31));
                }

            #endregion

            return countLeaveDays;
        }


        public static double CalculateLeaveDay(int month, int year, int userid, string Day1, string Day2, string Day3,
            string Day4, string Day5, string Day6,
            string Day7, string Day8, string Day9, string Day10, string Day11, string Day12, string Day13,
            string Day14, string Day15, string Day16, string Day17, string Day18, string Day19, string Day20,
            string Day21, string Day22, string Day23, string Day24, string Day25, string Day26, string Day27,
            string Day28, string Day29, string Day30, string Day31, int leaveType, bool isUpdateEmployeeLeave,
            ref double resultCalculateLeave)
        {
            double countLeaveDays = 0;
            var leaveTypeCode = Constants.GetSymbolTimekeeping(leaveType);
            var nuagioSymbol = "1/2" + leaveTypeCode;
            resultCalculateLeave = 0;
            var listLeave = new List<DateTime>();

            #region day

            if ((Day1 != null) && (Day1 != ""))
                if (Day1.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 1));
                }
                else if (Day1.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 1));
                }
            if ((Day2 != null) && (Day2 != ""))
                if (Day2.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 2));
                }
                else if (Day2.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 2));
                }
            if ((Day3 != null) && (Day3 != ""))
                if (Day3.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 3));
                }
                else if (Day3.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 3));
                }
            if ((Day4 != null) && (Day4 != ""))
                if (Day4.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 4));
                }
                else if (Day4.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 4));
                }
            if ((Day5 != null) && (Day5 != ""))
                if (Day5.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 5));
                }
                else if (Day5.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 5));
                }
            if ((Day6 != null) && (Day6 != ""))
                if (Day6.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 6));
                }
                else if (Day6.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 6));
                }
            if ((Day7 != null) && (Day7 != ""))
                if (Day7.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 7));
                }
                else if (Day7.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 7));
                }
            if ((Day8 != null) && (Day8 != ""))
                if (Day8.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 8));
                }
                else if (Day8.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 8));
                }
            if ((Day9 != null) && (Day9 != ""))
                if (Day9.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 9));
                }
                else if (Day9.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 9));
                }
            if ((Day10 != null) && (Day10 != ""))
                if (Day10.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 10));
                }
                else if (Day10.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 10));
                }
            if ((Day11 != null) && (Day11 != ""))
                if (Day11.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 11));
                }
                else if (Day11.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 11));
                }
            if ((Day12 != null) && (Day12 != ""))
                if (Day12.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 12));
                }
                else if (Day12.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 12));
                }
            if ((Day13 != null) && (Day13 != ""))
                if (Day13.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 13));
                }
                else if (Day13.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 13));
                }
            if ((Day14 != null) && (Day14 != ""))
                if (Day14.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 14));
                }
                else if (Day14.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 14));
                }
            if ((Day15 != null) && (Day15 != ""))
                if (Day15.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 15));
                }
                else if (Day15.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 15));
                }
            if ((Day16 != null) && (Day16 != ""))
                if (Day16.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 16));
                }
                else if (Day16.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 16));
                }
            if ((Day17 != null) && (Day17 != ""))
                if (Day17.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 17));
                }
                else if (Day17.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 17));
                }
            if ((Day18 != null) && (Day18 != ""))
                if (Day18.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 18));
                }
                else if (Day18.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 18));
                }
            if ((Day19 != null) && (Day19 != ""))
                if (Day19.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 19));
                }
                else if (Day19.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 19));
                }
            if ((Day20 != null) && (Day20 != ""))
                if (Day20.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 20));
                }
                else if (Day20.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 20));
                }
            if ((Day21 != null) && (Day21 != ""))
                if (Day21.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 21));
                }
                else if (Day21.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 21));
                }
            if ((Day22 != null) && (Day22 != ""))
                if (Day22.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 22));
                }
                else if (Day22.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 22));
                }
            if ((Day23 != null) && (Day23 != ""))
                if (Day23.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 23));
                }
                else if (Day23.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 23));
                }
            if ((Day24 != null) && (Day24 != ""))
                if (Day24.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 24));
                }
                else if (Day24.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 24));
                }
            if ((Day25 != null) && (Day25 != ""))
                if (Day25.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 25));
                }
                else if (Day25.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 25));
                }
            if ((Day26 != null) && (Day26 != ""))
                if (Day26.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 26));
                }
                else if (Day26.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 26));
                }
            if ((Day27 != null) && (Day27 != ""))
                if (Day27.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 27));
                }
                else if (Day27.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 27));
                }
            if ((Day28 != null) && (Day28 != ""))
                if (Day28.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 28));
                }
                else if (Day28.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 28));
                }
            if ((Day29 != null) && (Day29 != ""))
                if (Day29.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 29));
                }
                else if (Day29.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 29));
                }
            if ((Day30 != null) && (Day30 != ""))
                if (Day30.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 30));
                }
                else if (Day30.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 30));
                }
            if ((Day31 != null) && (Day31 != ""))
                if (Day31.Contains(nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 31));
                }
                else if (Day31.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 31));
                }

            #endregion

            if (countLeaveDays > 0)
            {
                DateTime fromdate = FormatDate.GetSQLDateMinValue,
                    todate = FormatDate.GetSQLDateMinValue,
                    dateTemp = FormatDate.GetSQLDateMinValue;
                var i = 0;

                while (i < listLeave.Count)
                {
                    if (i == listLeave.Count - 1)
                    {
                        fromdate = listLeave[i];
                        todate = listLeave[i];
                        i++;
                    }
                    else
                    {
                        fromdate = listLeave[i];

                        for (var j = i; j < listLeave.Count - 1; j++)
                        {
                            dateTemp = listLeave[j];
                            if (listLeave[j + 1].Day - dateTemp.Day == 1)
                            {
                                todate = listLeave[j + 1];
                                i = j + 1;
                                if (j == listLeave.Count - 2)
                                    i++;
                            }
                            else
                            {
                                todate = listLeave[j];
                                i = j + 1;
                                break;
                            }
                        }
                    }


                    double days = todate.Day - fromdate.Day;
                    if (days >= 4)
                    {
                        if (todate.DayOfWeek == DayOfWeek.Friday)
                            resultCalculateLeave += 1;
                    }
                    else
                    {
                        if (resultCalculateLeave < 0)
                            resultCalculateLeave = 0;
                    }
                }
            }
            return countLeaveDays;
        }


        public static bool IsWorking(string symbol)
        {
            if (symbol.Equals(Constants.LEAVE_TYPE_LE_TET_CODE) || symbol.Equals(Constants.LEAVE_TYPE_X_CODE)
                || symbol.Contains(Constants.LEAVE_TYPE_1_2_X_CODE) ||
                symbol.Contains(Constants.LEAVE_TYPE_NGHI_BU_CODE))
                return true;
            return false;
        }

        public static string ConvertWorking(string symbol)
        {
            var nuaX_nuaNT = Constants.LEAVE_TYPE_1_2_X_CODE + "+1/2" + Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
            var nuaX_nuaNB = Constants.LEAVE_TYPE_1_2_X_CODE + "+1/2" + Constants.LEAVE_TYPE_NGHI_BU_CODE;

            var nuaNT_nuaX = "1/2" + Constants.LEAVE_TYPE_NGHI_TUAN_CODE + "+" + Constants.LEAVE_TYPE_1_2_X_CODE;
            var nuaNB_nuaX = "1/2" + Constants.LEAVE_TYPE_NGHI_BU_CODE + "+" + Constants.LEAVE_TYPE_1_2_X_CODE;

            if (symbol.Equals(Constants.LEAVE_TYPE_X_CODE)
                || symbol.Equals(nuaX_nuaNB)
                || symbol.Equals(nuaX_nuaNT)
                || symbol.Equals(nuaNB_nuaX)
                || symbol.Equals(nuaNT_nuaX)
                || symbol.Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE)
                || symbol.Contains("1/2" + Constants.LEAVE_TYPE_HOC_SAGS_CODE + "+")
                || symbol.Equals(Constants.LEAVE_TYPE_HOC_SAGS_CODE)
                || symbol.Equals(Constants.LEAVE_TYPE_NGHI_BU_CODE)
                || symbol.Equals(Constants.LEAVE_TYPE_LE_TET_CODE)
                || symbol.Equals(Constants.LEAVE_TYPE_NGHI_MAT_CODE))
                return Constants.LEAVE_TYPE_X_CODE;
            //else if (symbol.Contains("1/2" + Constants.LEAVE_TYPE_HOC_1_CODE + "+"))
            //{
            //    return "1/2" + Constants.LEAVE_TYPE_HOC_1_CODE;
            //}
            //else if (symbol.Contains("1/2" + Constants.LEAVE_TYPE_HOC_2_CODE + "+"))
            //{
            //    return "1/2" + Constants.LEAVE_TYPE_HOC_2_CODE;
            //}
            //else if (symbol.Contains("1/2" + Constants.LEAVE_TYPE_HOC_3_CODE + "+"))
            //{
            //    return "1/2" + Constants.LEAVE_TYPE_HOC_3_CODE;
            //}
            //else if (symbol.Contains("1/2" + Constants.LEAVE_TYPE_HOC_4_CODE + "+"))
            //{
            //    return "1/2" + Constants.LEAVE_TYPE_HOC_4_CODE;
            //}
            //else if (symbol.Contains("1/2" + Constants.LEAVE_TYPE_HOC_5_CODE + "+"))
            //{
            //    return "1/2" + Constants.LEAVE_TYPE_HOC_5_CODE;
            //}
            //else if (symbol.Contains("1/2" + Constants.LEAVE_TYPE_HOC_6_CODE + "+"))
            //{
            //    return "1/2" + Constants.LEAVE_TYPE_HOC_6_CODE;
            //}
            //else if (symbol.Contains("1/2" + Constants.LEAVE_TYPE_HOC_7_CODE + "+"))
            //{
            //    return "1/2" + Constants.LEAVE_TYPE_HOC_7_CODE;
            //}
            return symbol;
        }

        public static string ConvertNghiTuan(string symbol)
        {
            if (symbol.Length > 0)
                if (symbol.Equals(Constants.LEAVE_TYPE_F_NAM_CODE))
                    return Constants.LEAVE_TYPE_F_NAM_CODE;
                else if (symbol.Equals(Constants.LEAVE_TYPE_FDB_CODE))
                    return Constants.LEAVE_TYPE_FDB_CODE;
                else if (symbol.Equals(Constants.LEAVE_TYPE_F_DI_DUONG_CODE))
                    return Constants.LEAVE_TYPE_F_DI_DUONG_CODE;
                else if (symbol.Equals(Constants.LEAVE_TYPE_TNLD_CODE))
                    return Constants.LEAVE_TYPE_TNLD_CODE;
                else if (symbol.Equals(Constants.LEAVE_TYPE_O_BAN_THAN_CODE))
                    return Constants.LEAVE_TYPE_O_BAN_THAN_CODE;
                else if (symbol.Equals(Constants.LEAVE_TYPE_O_DAI_NGAY_CODE))
                    return Constants.LEAVE_TYPE_O_DAI_NGAY_CODE;
                else if (symbol.Equals(Constants.LEAVE_TYPE_CON_OM_CODE))
                    return Constants.LEAVE_TYPE_CON_OM_CODE;
                else if (symbol.Equals(Constants.LEAVE_TYPE_KHHDS_CODE))
                    return Constants.LEAVE_TYPE_KHHDS_CODE;
                else if (symbol.Equals(Constants.LEAVE_TYPE_SAY_THAI_CODE))
                    return Constants.LEAVE_TYPE_SAY_THAI_CODE;
                else if (symbol.Equals(Constants.LEAVE_TYPE_KHAM_THAI_CODE))
                    return Constants.LEAVE_TYPE_KHAM_THAI_CODE;
                else
                    return Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
            return string.Empty;
        }

        public static string ConvertNghiTuan(string symbol, string symbolFriDay)
        {
            if (symbol.Equals(Constants.LEAVE_TYPE_NGHI_TUAN_CODE))
                return symbolFriDay;
            return symbol;
        }

        public static bool CheckIsPositiveMark(int HTCVCatalogueId)
        {
            if ((HTCVCatalogueId == Constants.HTCVCatalogueType_Section_Group_Manager_Id)
                || (HTCVCatalogueId == Constants.HTCVCatalogueType_Mediate_Id))
                return true;
            return false;
        }

        public static bool CheckMark(double mark, string minMark_maxMark)
        {
            var arr = minMark_maxMark.Split(';');
            var minMark = Math.Abs(double.Parse(arr[0]));
            var maxMark = Math.Abs(double.Parse(arr[1]));
            mark = Math.Abs(mark);
            if ((minMark == 0) && (minMark == 0))
                return true;
            if (minMark == maxMark)
                if (minMark == mark)
                    return true;
                else
                    return false;
            if ((mark >= minMark) && (mark <= maxMark))
                return true;
            return false;
        }

        public static List<HTCVEmployeeBLL> SortHTCVEmployeeBLL(List<HTCVEmployeeBLL> list)
        {
            list.Sort(new HTCVEmployeeBLLComparer(HTCVCatalogueKeys.Field_HTCVCatalogue_ParentId + " ASC"));

            var listReturn = new List<HTCVEmployeeBLL>();
            var listTemp = new List<HTCVEmployeeBLL>();
            if (list.Count > 0)
            {
                var objHC0 = HTCVCatalogueBLL.GetById(list[0].ParentId);
                var objHE0 = new HTCVEmployeeBLL();
                objHE0.HTCVCatalogueId = objHC0.HTCVCatalogueId;
                objHE0.HTCVCatalogueName = objHC0.HTCVCatalogueName;
                objHE0.MarkDate = FormatDate.GetSQLDateMinValue;
                listReturn.Add(objHE0);
                if (list.Count == 1)
                {
                    listReturn.Add(list[0]);
                }
                else
                {
                    listTemp.Add(list[0]);
                    for (var i = 1; i < list.Count; i++)
                        if (list[i - 1].ParentId == list[i].ParentId)
                        {
                            listTemp.Add(list[i]);
                        }
                        else
                        {
                            listTemp.Sort(
                                new HTCVEmployeeBLLComparer(HTCVEmployeeKeys.Field_HTCVEmployee_MarkDate + " ASC"));
                            foreach (var objSorted in listTemp)
                                listReturn.Add(objSorted);
                            listTemp.Clear();
                            var objHC0I = HTCVCatalogueBLL.GetById(list[i].ParentId);
                            var objHE0I = new HTCVEmployeeBLL();
                            objHE0I.HTCVCatalogueId = objHC0I.HTCVCatalogueId;
                            objHE0I.HTCVCatalogueName = objHC0I.HTCVCatalogueName;
                            objHE0.MarkDate = FormatDate.GetSQLDateMinValue;
                            listReturn.Add(objHE0I);
                            listTemp.Add(list[i]);
                        }
                    listTemp.Sort(new HTCVEmployeeBLLComparer(HTCVEmployeeKeys.Field_HTCVEmployee_MarkDate + " ASC"));
                    foreach (var objSorted in listTemp)
                        listReturn.Add(objSorted);
                    listTemp.Clear();
                }
            }
            // listReturn.Sort(new HTCVEmployeeBLLComparer(HTCVEmployeeKeys.Field_HTCVEmployee_MarkDate + " ASC"));
            return listReturn;
        }

        public static bool IsMediateDepartment(int departmentId)
        {
            //string mediateDeptIds = ",1,2,8,9,10,11,3,12,13,15,16,14,4,17,6,23,27,28,35,36,37,38,39,40,41,52,29,58,59,60,61,62,63,29,64,67,69";

            var mediateDeptIds = ",1,2,8,9,61,16,14,62,4,17,63,28,64,29,60,58,59,70,71,72,";

            return Util.IsContains(mediateDeptIds, departmentId);
        }

        public static bool IsNotCalculateNightTimeDepartment(int departmentId)
        {
            var mediateDeptIds = ",1,2,8,9,61,10,11,3,12,14,4,58,59,";

            return Util.IsContains(mediateDeptIds, departmentId);
        }

        public static double CalculateLeaveDayL(int month, int year, int userid, string Day1, string Day2, string Day3,
            string Day4, string Day5, string Day6,
            string Day7, string Day8, string Day9, string Day10, string Day11, string Day12, string Day13,
            string Day14, string Day15, string Day16, string Day17, string Day18, string Day19, string Day20,
            string Day21, string Day22, string Day23, string Day24, string Day25, string Day26, string Day27,
            string Day28, string Day29, string Day30, string Day31, int leaveType)
        {
            double countLeaveDays = 0;
            var leaveTypeCode = Constants.GetSymbolTimekeeping(leaveType);
            var nuagioSymbol = "1/2" + leaveTypeCode;

            var listLeave = new List<DateTime>();

            #region day

            if ((Day1 != null) && (Day1 != ""))
                if (Day1.Contains(nuagioSymbol + "+") || Day1.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 1));
                }
                else if (Day1.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 1));
                }
            if ((Day2 != null) && (Day2 != ""))
                if (Day2.Contains(nuagioSymbol + "+") || Day2.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 2));
                }
                else if (Day2.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 2));
                }
            if ((Day3 != null) && (Day3 != ""))
                if (Day3.Contains(nuagioSymbol + "+") || Day3.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 3));
                }
                else if (Day3.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 3));
                }
            if ((Day4 != null) && (Day4 != ""))
                if (Day4.Contains(nuagioSymbol + "+") || Day4.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 4));
                }
                else if (Day4.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 4));
                }
            if ((Day5 != null) && (Day5 != ""))
                if (Day5.Contains(nuagioSymbol + "+") || Day5.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 5));
                }
                else if (Day5.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 5));
                }
            if ((Day6 != null) && (Day6 != ""))
                if (Day6.Contains(nuagioSymbol + "+") || Day6.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 6));
                }
                else if (Day6.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 6));
                }
            if ((Day7 != null) && (Day7 != ""))
                if (Day7.Contains(nuagioSymbol + "+") || Day7.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 7));
                }
                else if (Day7.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 7));
                }
            if ((Day8 != null) && (Day8 != ""))
                if (Day8.Contains(nuagioSymbol + "+") || Day8.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 8));
                }
                else if (Day8.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 8));
                }
            if ((Day9 != null) && (Day9 != ""))
                if (Day9.Contains(nuagioSymbol + "+") || Day9.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 9));
                }
                else if (Day9.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 9));
                }
            if ((Day10 != null) && (Day10 != ""))
                if (Day10.Contains(nuagioSymbol + "+") || Day10.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 10));
                }
                else if (Day10.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 10));
                }
            if ((Day11 != null) && (Day11 != ""))
                if (Day11.Contains(nuagioSymbol + "+") || Day11.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 11));
                }
                else if (Day11.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 11));
                }
            if ((Day12 != null) && (Day12 != ""))
                if (Day12.Contains(nuagioSymbol + "+") || Day12.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 12));
                }
                else if (Day12.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 12));
                }
            if ((Day13 != null) && (Day13 != ""))
                if (Day13.Contains(nuagioSymbol + "+") || Day13.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 13));
                }
                else if (Day13.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 13));
                }
            if ((Day14 != null) && (Day14 != ""))
                if (Day14.Contains(nuagioSymbol + "+") || Day14.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 14));
                }
                else if (Day14.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 14));
                }
            if ((Day15 != null) && (Day15 != ""))
                if (Day15.Contains(nuagioSymbol + "+") || Day15.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 15));
                }
                else if (Day15.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 15));
                }
            if ((Day16 != null) && (Day16 != ""))
                if (Day16.Contains(nuagioSymbol + "+") || Day16.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 16));
                }
                else if (Day16.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 16));
                }
            if ((Day17 != null) && (Day17 != ""))
                if (Day17.Contains(nuagioSymbol + "+") || Day17.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 17));
                }
                else if (Day17.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 17));
                }
            if ((Day18 != null) && (Day18 != ""))
                if (Day18.Contains(nuagioSymbol + "+") || Day18.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 18));
                }
                else if (Day18.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 18));
                }
            if ((Day19 != null) && (Day19 != ""))
                if (Day19.Contains(nuagioSymbol + "+") || Day19.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 19));
                }
                else if (Day19.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 19));
                }
            if ((Day20 != null) && (Day20 != ""))
                if (Day20.Contains(nuagioSymbol + "+") || Day20.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 20));
                }
                else if (Day20.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 20));
                }
            if ((Day21 != null) && (Day21 != ""))
                if (Day21.Contains(nuagioSymbol + "+") || Day21.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 21));
                }
                else if (Day21.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 21));
                }
            if ((Day22 != null) && (Day22 != ""))
                if (Day22.Contains(nuagioSymbol + "+") || Day22.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 22));
                }
                else if (Day22.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 22));
                }
            if ((Day23 != null) && (Day23 != ""))
                if (Day23.Contains(nuagioSymbol + "+") || Day23.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 23));
                }
                else if (Day23.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 23));
                }
            if ((Day24 != null) && (Day24 != ""))
                if (Day24.Contains(nuagioSymbol + "+") || Day24.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 24));
                }
                else if (Day24.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 24));
                }
            if ((Day25 != null) && (Day25 != ""))
                if (Day25.Contains(nuagioSymbol + "+") || Day25.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 25));
                }
                else if (Day25.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 25));
                }
            if ((Day26 != null) && (Day26 != ""))
                if (Day26.Contains(nuagioSymbol + "+") || Day26.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 26));
                }
                else if (Day26.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 26));
                }
            if ((Day27 != null) && (Day27 != ""))
                if (Day27.Contains(nuagioSymbol + "+") || Day27.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 27));
                }
                else if (Day27.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 27));
                }
            if ((Day28 != null) && (Day28 != ""))
                if (Day28.Contains(nuagioSymbol + "+") || Day28.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 28));
                }
                else if (Day28.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 28));
                }
            if ((Day29 != null) && (Day29 != ""))
                if (Day29.Contains(nuagioSymbol + "+") || Day29.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 29));
                }
                else if (Day29.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 29));
                }
            if ((Day30 != null) && (Day30 != ""))
                if (Day30.Contains(nuagioSymbol + "+") || Day30.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 30));
                }
                else if (Day30.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 30));
                }
            if ((Day31 != null) && (Day31 != ""))
                if (Day31.Contains(nuagioSymbol + "+") || Day31.Contains("+" + nuagioSymbol))
                {
                    countLeaveDays = countLeaveDays + 0.5;
                    listLeave.Add(new DateTime(year, month, 31));
                }
                else if (Day31.Equals(leaveTypeCode))
                {
                    countLeaveDays++;
                    listLeave.Add(new DateTime(year, month, 31));
                }

            #endregion

            return countLeaveDays;
        }

        public static double CalculateLeaveDayL(string Day1, string Day2, string Day3, string Day4, string Day5,
            string Day6,
            string Day7, string Day8, string Day9, string Day10, string Day11, string Day12, string Day13,
            string Day14, string Day15, string Day16, string Day17, string Day18, string Day19, string Day20,
            string Day21, string Day22, string Day23, string Day24, string Day25, string Day26, string Day27,
            string Day28, string Day29, string Day30, string Day31, int leaveType)
        {
            double countLeaveDays = 0;
            var leaveTypeCode = Constants.GetSymbolTimekeeping(leaveType);
            var nuagioSymbol = "1/2" + leaveTypeCode;

            #region day

            if ((Day1 != null) && (Day1 != ""))
                if (Day1.Contains(nuagioSymbol + "+") || Day1.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day1.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day2 != null) && (Day2 != ""))
                if (Day2.Contains(nuagioSymbol + "+") || Day2.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day2.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day3 != null) && (Day3 != ""))
                if (Day3.Contains(nuagioSymbol + "+") || Day3.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day3.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day4 != null) && (Day4 != ""))
                if (Day4.Contains(nuagioSymbol + "+") || Day4.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day4.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day5 != null) && (Day5 != ""))
                if (Day5.Contains(nuagioSymbol + "+") || Day5.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day5.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day6 != null) && (Day6 != ""))
                if (Day6.Contains(nuagioSymbol + "+") || Day6.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day6.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day7 != null) && (Day7 != ""))
                if (Day7.Contains(nuagioSymbol + "+") || Day7.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day7.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day8 != null) && (Day8 != ""))
                if (Day8.Contains(nuagioSymbol + "+") || Day8.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day8.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day9 != null) && (Day9 != ""))
                if (Day9.Contains(nuagioSymbol + "+") || Day9.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day9.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day10 != null) && (Day10 != ""))
                if (Day10.Contains(nuagioSymbol + "+") || Day10.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day10.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day11 != null) && (Day11 != ""))
                if (Day11.Contains(nuagioSymbol + "+") || Day11.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day11.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day12 != null) && (Day12 != ""))
                if (Day12.Contains(nuagioSymbol + "+") || Day12.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day12.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day13 != null) && (Day13 != ""))
                if (Day13.Contains(nuagioSymbol + "+") || Day13.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day13.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day14 != null) && (Day14 != ""))
                if (Day14.Contains(nuagioSymbol + "+") || Day14.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day14.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day15 != null) && (Day15 != ""))
                if (Day15.Contains(nuagioSymbol + "+") || Day15.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day15.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day16 != null) && (Day16 != ""))
                if (Day16.Contains(nuagioSymbol + "+") || Day16.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day16.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day17 != null) && (Day17 != ""))
                if (Day17.Contains(nuagioSymbol + "+") || Day17.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day17.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day18 != null) && (Day18 != ""))
                if (Day18.Contains(nuagioSymbol + "+") || Day18.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day18.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day19 != null) && (Day19 != ""))
                if (Day19.Contains(nuagioSymbol + "+") || Day19.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day19.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day20 != null) && (Day20 != ""))
                if (Day20.Contains(nuagioSymbol + "+") || Day20.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day20.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day21 != null) && (Day21 != ""))
                if (Day21.Contains(nuagioSymbol + "+") || Day21.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day21.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day22 != null) && (Day22 != ""))
                if (Day22.Contains(nuagioSymbol + "+") || Day22.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day22.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day23 != null) && (Day23 != ""))
                if (Day23.Contains(nuagioSymbol + "+") || Day23.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day23.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day24 != null) && (Day24 != ""))
                if (Day24.Contains(nuagioSymbol + "+") || Day24.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day24.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day25 != null) && (Day25 != ""))
                if (Day25.Contains(nuagioSymbol + "+") || Day25.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day25.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day26 != null) && (Day26 != ""))
                if (Day26.Contains(nuagioSymbol + "+") || Day26.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day26.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day27 != null) && (Day27 != ""))
                if (Day27.Contains(nuagioSymbol + "+") || Day27.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day27.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day28 != null) && (Day28 != ""))
                if (Day28.Contains(nuagioSymbol + "+") || Day28.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day28.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day29 != null) && (Day29 != ""))
                if (Day29.Contains(nuagioSymbol + "+") || Day29.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day29.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day30 != null) && (Day30 != ""))
                if (Day30.Contains(nuagioSymbol + "+") || Day30.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day30.Equals(leaveTypeCode))
                    countLeaveDays++;
            if ((Day31 != null) && (Day31 != ""))
                if (Day31.Contains(nuagioSymbol + "+") || Day31.Contains("+" + nuagioSymbol))
                    countLeaveDays = countLeaveDays + 0.5;
                else if (Day31.Equals(leaveTypeCode))
                    countLeaveDays++;

            #endregion

            return countLeaveDays;
        }
    }
}