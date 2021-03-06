using System;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace HRMUtil
{
    public class FormatDate
    {
        public static DateTime GetSQLDateMinValue
        {
            get
            {
                var MinValue = (DateTime) SqlDateTime.MinValue;
                MinValue.AddYears(30);
                return MinValue;
            }
        }

        public static List<Unit> GetYears()
        {
            var list = new List<Unit>();
            for (var i = 2005; i <= DateTime.Now.Year + 5; i++)
                list.Add(new Unit(i, i.ToString()));
            return list;
        }

        public static string FormatVNDate(DateTime date)
        {
            if (date.CompareTo(GetSQLDateMinValue) > 0)
                return date.ToString("dd/MM/yyyy");
            return string.Empty;
        }

        public static DateTime FormatUSDate(string dt)
        {
            var d = dt.Split('/');
            if (d.Length == 3)
            {
                var year = int.Parse(d[2].Trim().Substring(0, 4));
                var month = int.Parse(d[1].Trim());
                var day = int.Parse(d[0].Trim());
                var date = DateTime.Now;
                try
                {
                    date = new DateTime(year, month, day);
                }
                catch
                {
                    if (month > 12)
                        date = new DateTime(year, day, month);
                }

                return date;
            }
            return GetSQLDateMinValue;
        }

        public static DateTime FormatUSDate(string dt, int sqlFormat)
        {
            var d = dt.Split('/');
            if (d.Length == 3)
            {
                var year = GetSQLDateMinValue.Year;
                var month = GetSQLDateMinValue.Month;
                var day = GetSQLDateMinValue.Day;
                if (sqlFormat == 111)
                {
                    year = int.Parse(d[0].Trim().Substring(0, 4));
                    month = int.Parse(d[1].Trim());
                    day = int.Parse(d[2].Trim());
                }
                var date = DateTime.Now;
                try
                {
                    date = new DateTime(year, month, day);
                }
                catch
                {
                    if (month > 12)
                        date = new DateTime(year, day, month);
                }

                return date;
            }
            return GetSQLDateMinValue;
        }

        public static string GetVNDateNow()
        {
            var dayOfWeek = string.Empty;

            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    dayOfWeek = "Chủ Nhật";
                    break;
                case DayOfWeek.Monday:
                    dayOfWeek = "Thứ Hai";
                    break;
                case DayOfWeek.Tuesday:
                    dayOfWeek = "Thứ Ba";
                    break;
                case DayOfWeek.Wednesday:
                    dayOfWeek = "Thứ Tư";
                    break;
                case DayOfWeek.Thursday:
                    dayOfWeek = "Thứ Năm";
                    break;
                case DayOfWeek.Friday:
                    dayOfWeek = "Thứ Sáu";
                    break;
                case DayOfWeek.Saturday:
                    dayOfWeek = "Thứ Bảy";
                    break;
            }
            return dayOfWeek;
        }

        public static string GetVNDayOfWeek(DateTime dt)
        {
            var dayOfWeek = string.Empty;

            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    dayOfWeek = "CN";
                    break;
                case DayOfWeek.Monday:
                    dayOfWeek = "Hai";
                    break;
                case DayOfWeek.Tuesday:
                    dayOfWeek = "Ba";
                    break;
                case DayOfWeek.Wednesday:
                    dayOfWeek = "Tư";
                    break;
                case DayOfWeek.Thursday:
                    dayOfWeek = "Năm";
                    break;
                case DayOfWeek.Friday:
                    dayOfWeek = "Sáu";
                    break;
                case DayOfWeek.Saturday:
                    dayOfWeek = "Bảy";
                    break;
            }
            return dayOfWeek;
        }

        public static double GetDays(int leaveTypeId, DateTime fromDate, DateTime toDate)
        {
            var days = 0;
            var tempFromDate = fromDate;
            if ((tempFromDate.DayOfWeek != DayOfWeek.Saturday) && (tempFromDate.DayOfWeek != DayOfWeek.Sunday))
                days++;
            while (tempFromDate.CompareTo(toDate) < 0)
            {
                tempFromDate = tempFromDate.AddDays(1);
                if ((tempFromDate.DayOfWeek != DayOfWeek.Saturday) && (tempFromDate.DayOfWeek != DayOfWeek.Sunday))
                    days++;
            }
            return days;
        }
    }
}