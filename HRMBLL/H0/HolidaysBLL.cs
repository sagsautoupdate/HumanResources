using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H0;
using HRMUtil.KeyNames.H0;

namespace HRMBLL.H0
{
    public class HolidaysBLL
    {
        #region constructors

        public HolidaysBLL(int holidayId, string holidayName, DateTime holidayDate)
        {
            HolidayId = holidayId;
            HolidayName = holidayName;
            HolidayDate = holidayDate;
        }

        #endregion

        #region private fields

        #endregion

        #region properties

        public int HolidayId { get; set; }

        public string HolidayName { get; set; }

        public DateTime HolidayDate { get; set; }

        #endregion

        #region public methods insert, update, delete

        public long Save()
        {
            var objDAL = new HolidaysDAL();
            if (HolidayId <= 0)
                return objDAL.Insert(HolidayName, HolidayDate);
            return objDAL.Update(HolidayId, HolidayName, HolidayDate);
        }

        public static long Update(int holidayId, string holidayName, DateTime holidayDate)
        {
            var objDAL = new HolidaysDAL();
            return objDAL.Update(holidayId, holidayName, holidayDate);
        }

        public static long Delete(int holidayId)
        {
            var objDAL = new HolidaysDAL();
            return objDAL.Delete(holidayId);
        }

        #endregion

        #region public static Get methods

        public static List<HolidaysBLL> GetAll()
        {
            var objDAL = new HolidaysDAL();
            return GenerateListHolidayFromDataTable(objDAL.GetAll());
        }

        public static List<HolidaysBLL> GetByDate(int month, int year)
        {
            var objDAL = new HolidaysDAL();
            return GenerateListHolidayFromDataTable(objDAL.GetByDate(month, year));
        }

        public static DataTable GetByDateToDT(int month, int year)
        {
            var objDAL = new HolidaysDAL();
            return objDAL.GetByDate(month, year);
        }

        #endregion

        #region private methods       

        private static List<HolidaysBLL> GenerateListHolidayFromDataTable(DataTable dt)
        {
            var list = new List<HolidaysBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateHolidayFromDataRow(dr));

            return list;
        }

        private static HolidaysBLL GenerateHolidayFromDataRow(DataRow dr)
        {
            var objBLL = new HolidaysBLL(
                dr[HolidayKeys.FIELD_HOLIDAY_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[HolidayKeys.FIELD_HOLIDAY_ID].ToString()),
                dr[HolidayKeys.FIELD_HOLIDAY_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[HolidayKeys.FIELD_HOLIDAY_NAME].ToString(),
                dr[HolidayKeys.FIELD_HOLIDAY_NAME] == DBNull.Value
                    ? DateTime.MinValue
                    : Convert.ToDateTime(dr[HolidayKeys.FIELD_HOLIDAY_DATE].ToString()));

            return objBLL;
        }

        #endregion
    }
}