using System;
using System.Collections.Generic;
using System.Data;
using HRMBLL.H1.Helper;
using HRMDAL.H0;
using HRMUtil;
using HRMUtil.KeyNames.H0;

namespace HRMBLL.H0
{
    public class EmployeeTimeBillBLL
    {
        #region private fields

        #endregion

        #region properties

        public int UserTimeBillId { get; set; }

        public DateTime WorkDate { get; set; }

        public int UserId { get; set; }

        public DateTime TimeIn { get; set; }

        public DateTime TimeOut { get; set; }

        public double TotalMinutes { get; set; }

        public double TotalHours { get; set; }

        public int Status { get; set; }

        public string FullName { get; set; }

        public int OverTime { get; set; }

        #endregion

        #region public methods insert, update, delete

        public long Save()
        {
            var objDAL = new EmployeeTimeBillDAL();
            if (UserTimeBillId <= 0)
                return objDAL.Insert(WorkDate, UserId, TimeIn, TimeOut, TotalMinutes, TotalHours, Status);
            return objDAL.Update(WorkDate, UserId, TimeIn, TimeOut, TotalMinutes, TotalHours, Status, UserTimeBillId);
        }

        public static long Delete(int userTimeBillId)
        {
            return new EmployeeTimeBillDAL().Delete(userTimeBillId);
        }

        #endregion

        #region public static Get methods

        public static void GetTimeKeeping(int userId, int departmentId, int status, int day, int month, int year,
            ref string lnkX, ref string lbNight, ref string lbDaytime)
        {
            var lst =
                GenerateListEmployeeTimeBillBLLFromDataTable(new EmployeeTimeBillDAL().GetByFilter(userId, status, day,
                    month, year));
            var IsMediateDepartment = DefaultValues.IsMediateDepartment(departmentId);
            TimeSpan tempNightHours;
            double nightTime = 0;
            double totalHour = 0;

            foreach (var obj in lst)
            {
                var tempTotalHours = obj.TimeOut - obj.TimeIn;
                if (tempTotalHours.TotalHours >= 0)
                    totalHour = totalHour + tempTotalHours.TotalHours;

                var dtStartNight = new DateTime(obj.TimeIn.Year, obj.TimeIn.Month, obj.TimeIn.Day, 21, 0, 0);
                tempNightHours = obj.TimeOut - dtStartNight;
                if (tempNightHours.TotalHours >= 0)
                    nightTime = nightTime + tempNightHours.TotalHours;

                var dtEndNight = new DateTime(obj.TimeOut.Year, obj.TimeOut.Month, obj.TimeOut.Day, 6, 0, 0);
                tempNightHours = dtEndNight - obj.TimeIn;
                if (tempNightHours.TotalHours >= 0)
                    nightTime = nightTime + tempNightHours.TotalHours;
            }
            if ((totalHour > 12) && IsMediateDepartment)
                totalHour = 0;
            else if ((totalHour > 12) && !IsMediateDepartment)
                totalHour = 8;
            if (nightTime >= 8)
                nightTime = 3;
            if (lst.Count == 0)
            {
                lnkX = "-";
                lbDaytime = string.Empty;
                lbNight = string.Empty;
            }
            else if ((totalHour > 5.5) && (totalHour <= 12))
            {
                lnkX = "X";
                lbDaytime = totalHour <= 0 ? "" : totalHour.ToString(StringFormat.FormatCurrencyFinal);
                lbNight = nightTime <= 0 ? "" : nightTime.ToString(StringFormat.FormatCurrencyFinal);
            }
            else if ((totalHour > 1) && (totalHour <= 4))
            {
                lnkX = "1/2X+1/2NT";
                lbDaytime = totalHour <= 0 ? "" : totalHour.ToString(StringFormat.FormatCurrencyFinal);
                lbNight = nightTime <= 0 ? "" : nightTime.ToString(StringFormat.FormatCurrencyFinal);
            }

            /////////////////////////////////////////////////////////////////
            //if(lst.Count == 1)
            //{

            //    EmployeeTimeBillBLL obj = lst[0];
            //    TimeSpan tempTotalHours = obj.TimeOut - obj.TimeIn;
            //    double totalHour = tempTotalHours.TotalHours;
            //    if(totalHour>12)
            //    {
            //        lnkX= string.Empty;
            //        lbNight = string.Empty;
            //        lbDaytime = string.Empty;                    
            //    }
            //    else if (totalHour > 6 && totalHour <= 12)
            //    {
            //        lbDaytime = totalHour.ToString(StringFormat.FormatCurrencyFinal);
            //        lnkX = "X";
            //        DateTime dtStartNight = new DateTime(obj.TimeIn.Year, obj.TimeIn.Month, obj.TimeIn.Day, 21, 0, 0);
            //        tempNightHours = obj.TimeOut - dtStartNight;
            //        nightTime = tempNightHours.TotalHours;
            //        if (nightTime > 0)
            //        {
            //            c
            //        }
            //        else
            //        {
            //            DateTime dtEndNight = new DateTime(obj.TimeOut.Year, obj.TimeOut.Month, obj.TimeOut.Day, 6, 0, 0);
            //            tempNightHours = dtEndNight - obj.TimeIn;
            //            nightTime = tempNightHours.TotalHours;
            //            if (nightTime > 0)
            //            {
            //                lbNight = nightTime.ToString(StringFormat.FormatCurrencyFinal);
            //            }
            //        }                   
            //    }
            //    else if (totalHour > 1 && totalHour <= 4)
            //    {
            //        lbDaytime = totalHour.ToString(StringFormat.FormatCurrencyFinal);
            //        lnkX = "1/2X+1/2NT";
            //        DateTime dtStartNight = new DateTime(obj.TimeIn.Year, obj.TimeIn.Month, obj.TimeIn.Day, 21, 0, 0);
            //        tempNightHours = obj.TimeOut - dtStartNight;
            //        nightTime = tempNightHours.TotalHours;
            //        if (nightTime > 0)
            //        {
            //            lbNight = nightTime.ToString(StringFormat.FormatCurrencyFinal);
            //        }
            //        else
            //        {
            //            DateTime dtEndNight = new DateTime(obj.TimeOut.Year, obj.TimeOut.Month, obj.TimeOut.Day, 6, 0, 0);
            //            tempNightHours = dtEndNight - obj.TimeIn;
            //            nightTime = tempNightHours.TotalHours;
            //            if (nightTime > 0)
            //            {
            //                lbNight = nightTime.ToString(StringFormat.FormatCurrencyFinal);
            //            }
            //        }  
            //    }              
            //}
        }

        public static EmployeeTimeBillBLL GetByUserStatus(int userId, int status)
        {
            var list =
                GenerateListEmployeeTimeBillBLLFromDataTable(new EmployeeTimeBillDAL().GetByUserStatus(userId, status));
            return list.Count == 1 ? list[0] : null;
        }

        public static List<string> GetDistinctWorkDareByUserId(int userId, int month, int year)
        {
            var dt = new EmployeeTimeBillDAL().GetForDistinctWorkdateByUserId(userId, month, year);
            var lst = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                var workdate = dr[EmployeeTimeBillKeys.Field_EmployeeTimeBill_WorkDate] == DBNull.Value
                    ? ""
                    : dr[EmployeeTimeBillKeys.Field_EmployeeTimeBill_WorkDate].ToString();
                lst.Add(workdate);
            }
            return lst;
        }

        public static List<EmployeeTimeBillBLL> GetByFilter(int userId, int status, int day, int month, int year)
        {
            return
                GenerateListEmployeeTimeBillBLLFromDataTable(new EmployeeTimeBillDAL().GetByFilter(userId, status, day,
                    month, year));
        }

        public static List<EmployeeTimeBillBLL> GetByFilterByHo(int userId, int status, int day, int month, int year)
        {
            return
                GenerateListEmployeeTimeBillBLLFromDataTable(new EmployeeTimeBillDAL().GetByFilterByHo(userId, status,
                    day, month, year));
        }

        public static List<EmployeeTimeBillBLL> GetByUserDateForWorkingHo(int userId, int day, int month, int year)
        {
            return
                GenerateListEmployeeTimeBillBLLFromDataTable(new EmployeeTimeBillDAL().GetByUserDateForWorkingHo(
                    userId, day, month, year));
        }

        public static List<EmployeeTimeBillBLL> GetByDateRootId(int rootId, DateTime inputDate)
        {
            return
                GenerateListEmployeeTimeBillBLLFromDataTable(new EmployeeTimeBillDAL().GetByDateRootId(rootId, inputDate));
        }

        public static List<string> GetForDistinctWorkdateByFromToWorkDate(int userId, DateTime fromDate, DateTime toDate)
        {
            var dt = new EmployeeTimeBillDAL().GetForDistinctWorkdateByFromToWorkDate(userId, fromDate, toDate);
            var lst = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                var workdate = dr[EmployeeTimeBillKeys.Field_EmployeeTimeBill_WorkDate] == DBNull.Value
                    ? ""
                    : dr[EmployeeTimeBillKeys.Field_EmployeeTimeBill_WorkDate].ToString();
                lst.Add(workdate);
            }
            return lst;
        }

        public static DateTime GetByDateRootId()
        {
            var dateTime = DateTime.Now;
            var dt = new EmployeeTimeBillDAL().GetDateTimeNowFromServer();
            if (dt.Rows.Count == 1)
            {
                var dr = dt.Rows[0];
                dateTime = dr["ServerTime"] == DBNull.Value
                    ? DateTime.Now
                    : Convert.ToDateTime(dr["ServerTime"].ToString());
            }
            return dateTime;
        }

        #endregion

        #region private methods

        //private static List<EmployeeTimeBillBLL> GenerateListEmployeeTimeBillBLLFromDataTableCheckTimeWork(DataTable dt)
        //{
        //    List<EmployeeTimeBillBLL> lst = new List<EmployeeTimeBillBLL>();

        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        EmployeeTimeBillBLL obj = GenerateEmployeeTimeBillBLLFromDataRow(dr);
        //        if (obj.Status == 1 || obj.Status == 0)
        //        {
        //            lst.Add(obj);
        //        }
        //        else if (obj.Status == 2)
        //        {
        //            if (obj.TotalHours > 4)
        //                lst.Add(obj);
        //        }
        //    }

        //    return lst;
        //}

        private static List<EmployeeTimeBillBLL> GenerateListEmployeeTimeBillBLLFromDataTable(DataTable dt)
        {
            var lst = new List<EmployeeTimeBillBLL>();

            foreach (DataRow dr in dt.Rows)
                lst.Add(GenerateEmployeeTimeBillBLLFromDataRow(dr));

            return lst;
        }

        private static EmployeeTimeBillBLL GenerateEmployeeTimeBillBLLFromDataRow(DataRow dr)
        {
            var obj = new EmployeeTimeBillBLL();
            try
            {
                obj.UserTimeBillId = dr[EmployeeTimeBillKeys.Field_EmployeeTimeBill_UserTimeBillId] == DBNull.Value
                    ? 0
                    : int.Parse(dr[EmployeeTimeBillKeys.Field_EmployeeTimeBill_UserTimeBillId].ToString());
            }
            catch
            {
            }
            obj.WorkDate = dr[EmployeeTimeBillKeys.Field_EmployeeTimeBill_WorkDate] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr[EmployeeTimeBillKeys.Field_EmployeeTimeBill_WorkDate].ToString());
            obj.UserId = dr[EmployeeTimeBillKeys.Field_EmployeeTimeBill_UserId] == DBNull.Value
                ? 0
                : int.Parse(dr[EmployeeTimeBillKeys.Field_EmployeeTimeBill_UserId].ToString());
            obj.TimeIn = dr[EmployeeTimeBillKeys.Field_EmployeeTimeBill_TimeIn] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr[EmployeeTimeBillKeys.Field_EmployeeTimeBill_TimeIn].ToString());
            obj.TimeOut = dr[EmployeeTimeBillKeys.Field_EmployeeTimeBill_TimeOut] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr[EmployeeTimeBillKeys.Field_EmployeeTimeBill_TimeOut].ToString());
            try
            {
                obj.TotalMinutes = dr[EmployeeTimeBillKeys.Field_EmployeeTimeBill_TotalMinutes] == DBNull.Value
                    ? 0
                    : double.Parse(dr[EmployeeTimeBillKeys.Field_EmployeeTimeBill_TotalMinutes].ToString());
            }
            catch
            {
            }
            try
            {
                obj.TotalHours = dr[EmployeeTimeBillKeys.Field_EmployeeTimeBill_TotalHours] == DBNull.Value
                    ? 0
                    : double.Parse(dr[EmployeeTimeBillKeys.Field_EmployeeTimeBill_TotalHours].ToString());
            }
            catch
            {
            }
            try
            {
                obj.Status = dr[EmployeeTimeBillKeys.Field_EmployeeTimeBill_Status] == DBNull.Value
                    ? 0
                    : int.Parse(dr[EmployeeTimeBillKeys.Field_EmployeeTimeBill_Status].ToString());
            }
            catch
            {
            }
            try
            {
                obj.FullName = dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                    ? ""
                    : dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();
            }
            catch
            {
            }
            try
            {
                obj.OverTime = dr[EmployeeTimeBillKeys.Field_EmployeeTimeBill_OverTime] == DBNull.Value
                    ? 0
                    : int.Parse(dr[EmployeeTimeBillKeys.Field_EmployeeTimeBill_OverTime].ToString());
            }
            catch
            {
            }
            return obj;
        }

        #endregion
    }
}