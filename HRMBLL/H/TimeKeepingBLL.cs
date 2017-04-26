using System;
using System.Collections.Generic;
using System.Data;
using HRMBLL.H.Helper;
using HRMDAL.H;
using HRMUtil.KeyNames.H;

namespace HRMBLL.H
{
    public class TimeKeepingBLL
    {
        #region constructor

        public TimeKeepingBLL(long timeKeepingId, double value, DateTime timeKeepingDate)
        {
            TimeKeepingId = timeKeepingId;
            Value = value;
            TimeKeepingDate = timeKeepingDate;
        }

        #endregion

        #region methods insert, update, delete

        public long Save()
        {
            if (TimeKeepingId <= 0)
            {
                var objDAL = new TimeKeepingDAL();
                return objDAL.Insert(TimeKeepingTypeId, UserCode, Value, TimeKeepingDate);
            }
            return -1;
        }

        #endregion

        #region public methods Get

        public static List<TimeKeepingBLL> GetByUserId_Monthly(int userId, int month, int year, int type)
        {
            var objDAL = new TimeKeepingDAL();
            return GenerateListTimeKeepingBLLFromDataTable(objDAL.GetByUserId_Monthly(userId, month, year, type));
        }

        #endregion

        #region private fields

        #endregion

        #region properties

        public long TimeKeepingId { get; set; }

        public double Value { get; set; }

        public DateTime TimeKeepingDate { get; set; }

        public int TimeKeepingTypeId { get; set; }

        public string Description { get; set; }

        public string UserCode { get; set; }

        public string TimeKeepingCode { get; set; }

        #endregion

        #region private methods, generate helper methods

        private static List<TimeKeepingBLL> GenerateListTimeKeepingBLLFromDataTable(DataTable dt)
        {
            var list = new List<TimeKeepingBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateTimeKeepingBLLFromDataRow(dr));

            return list;
        }

        private static TimeKeepingBLL GenerateTimeKeepingBLLFromDataRow(DataRow dr)
        {
            var objBLL = new TimeKeepingBLL(
                dr[TimeKeepingKeys.FIELD_TIME_KEEPING_ID] == DBNull.Value
                    ? DefaultValues.TimeKeepingIdMinValue
                    : Convert.ToInt64(dr[TimeKeepingKeys.FIELD_TIME_KEEPING_ID].ToString()),
                dr[TimeKeepingKeys.FIELD_TIME_KEEPING_VALUE] == DBNull.Value
                    ? 0
                    : Convert.ToDouble(dr[TimeKeepingKeys.FIELD_TIME_KEEPING_VALUE].ToString()),
                dr[TimeKeepingKeys.FIELD_TIME_KEEPING_DATE] == DBNull.Value
                    ? DateTime.MinValue
                    : Convert.ToDateTime(dr[TimeKeepingKeys.FIELD_TIME_KEEPING_DATE].ToString())
            );

            objBLL.TimeKeepingCode = dr[TimeKeepingTypeKeys.FIELD_TIME_KEEPING_TYPE_CODE] == DBNull.Value
                ? string.Empty
                : (string) dr[TimeKeepingTypeKeys.FIELD_TIME_KEEPING_TYPE_CODE];
            objBLL.Description = dr[TimeKeepingTypeKeys.FIELD_TIME_KEEPING_TYPE_DESCRIPTION] == DBNull.Value
                ? string.Empty
                : (string) dr[TimeKeepingTypeKeys.FIELD_TIME_KEEPING_TYPE_DESCRIPTION];
            return objBLL;
        }

        #endregion
    }
}