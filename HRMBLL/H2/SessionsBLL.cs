using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H2;
using HRMUtil;
using HRMUtil.KeyNames.H2;

namespace HRMBLL.H2
{
    public class SessionsBLL
    {
        #region private fields

        #endregion

        #region properties

        public int Id { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string Name { get; set; }

        public string Remark { get; set; }

        public int SessionType { get; set; }

        public string SessionTypeName { get; set; }

        public int PositionId { get; set; }

        public string PositionName { get; set; }

        #endregion

        #region public methods

        public static DataTable DT_GetAll()
        {
            return new SessionsDAL().GetAll();
        }

        public long Save()
        {
            var objDAL = new SessionsDAL();
            if (Id <= 0)
                return objDAL.Insert(FromDate, ToDate, Name, Remark, SessionType);
            return objDAL.Update(Name, FromDate, ToDate, Remark, SessionType, Id);
        }

        public static long Update(string name, string fromDate, string toDate, string remark, int sessionType, int id)
        {
            return new SessionsDAL().Update(name, FormatDate.FormatUSDate(fromDate), FormatDate.FormatUSDate(toDate),
                remark, sessionType, id);
        }

        public static long Delete(int id)
        {
            return new SessionsDAL().Delete(id);
        }

        public static List<SessionsBLL> GetAll()
        {
            return GenerateListFromDataTable(new SessionsDAL().GetAll());
        }


        public static List<SessionsBLL> GetSessionIsOpen()
        {
            return GenerateListFromDataTable(new SessionsDAL().GetSessionIsOpen());
        }

        public static List<SessionsBLL> GetPositionBySessionId(int sessionId)
        {
            return GenerateListFromDataTable(new SessionsDAL().GetSessionPositionBySessionId(sessionId));
        }

        public static List<SessionsBLL> GetIsActive()
        {
            return GenerateListFromDataTable(new SessionsDAL().GetIsActive());
        }

        #endregion

        #region Insert SessionPosition

        public static long InsertSessionPosition(int PositionId, int SessionId, string Remark)
        {
            return new SessionsDAL().InsertSessionPosition(PositionId, SessionId, Remark);
        }

        public static long DeleteBySessionId(int SessionId)
        {
            return new SessionsDAL().DeleteBySessionId(SessionId);
        }

        #endregion

        #region private methods

        private static List<SessionsBLL> GenerateListFromDataTable(DataTable dt)
        {
            var list = new List<SessionsBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateFromDataTable(dr));

            return list;
        }


        private static SessionsBLL GenerateFromDataTable(DataRow dr)
        {
            var s = new SessionsBLL();
            s.Id = dr[SessionKeys.FIELD_SESSION_ID] == DBNull.Value
                ? 0
                : int.Parse(dr[SessionKeys.FIELD_SESSION_ID].ToString());
            s.Name = dr[SessionKeys.FIELD_SESSION_SESSION_NAME] == DBNull.Value
                ? string.Empty
                : dr[SessionKeys.FIELD_SESSION_SESSION_NAME].ToString();
            s.FromDate = dr[SessionKeys.FIELD_SESSION_FROM_DATE] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr[SessionKeys.FIELD_SESSION_FROM_DATE].ToString());
            s.ToDate = dr[SessionKeys.FIELD_SESSION_TO_DATE] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr[SessionKeys.FIELD_SESSION_TO_DATE].ToString());
            s.Remark = dr[SessionKeys.FIELD_SESSION_REMARK] == DBNull.Value
                ? string.Empty
                : dr[SessionKeys.FIELD_SESSION_REMARK].ToString();
            s.SessionType = dr[SessionKeys.FIELD_SESSION_SESSION_TYPE] == DBNull.Value
                ? 0
                : int.Parse(dr[SessionKeys.FIELD_SESSION_SESSION_TYPE].ToString());
            s.SessionTypeName = Constants.GetNameBySessionType(s.SessionType);
            try
            {
                s.PositionId = dr[SessionKeys.FIELD_SESSION_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[SessionKeys.FIELD_SESSION_POSITION_ID].ToString());
                s.PositionName = dr[SessionKeys.FIELD_SESSION_POSITION_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[SessionKeys.FIELD_SESSION_POSITION_NAME].ToString();
            }
            catch
            {
            }

            return s;
        }

        #endregion
    }
}