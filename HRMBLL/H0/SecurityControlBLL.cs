using System;
using System.Data;
using HRMDAL.H0;

namespace HRMBLL.H0
{
    public class SecurityControlBLL
    {
        #region Private Fields

        public string Area1 { get; set; }

        public string Area2 { get; set; }

        public string Area3 { get; set; }

        public string Area4 { get; set; }

        public string Area5 { get; set; }

        public string Area6 { get; set; }

        public string CurrentSCI { get; set; }

        public DateTime Period { get; set; }

        public string PreviousSCI { get; set; }

        public string Remark { get; set; }

        public int SecurityControlId { get; set; }

        public int UserId { get; set; }

        #endregion

        #region Public Get

        public static DataRow GetOneById(int UserId)
        {
            var dt = new SecurityControlDAL().GetOneById(UserId);
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        public static DataTable GetAll()
        {
            return new SecurityControlDAL().GetAll();
        }

        public static DataTable GetAllForExport()
        {
            return new SecurityControlDAL().GetAllForExport();
        }

        public static DataTable GetAllForExport_Employee()
        {
            return new SecurityControlDAL().GetAllForExport_Employee();
        }

        public static DataTable GetAllForExport_Candidate()
        {
            return new SecurityControlDAL().GetAllForExport_Candidate();
        }

        public static DataTable GetAllHistory(int UserId)
        {
            return new SecurityControlDAL().GetAllHistory(UserId);
        }

        #endregion

        #region Public Ins, Upd, Del

        public static long Insert(int UserId, string CurrentSCI, DateTime Period, string Area1, string Area2,
            string Area3, string Area4, string Area5, string Area6, string Remark, int CreateBy, DateTime StartDate)
        {
            return new SecurityControlDAL().Insert(UserId, CurrentSCI, Period, Area1, Area2, Area3, Area4, Area5, Area6,
                Remark, CreateBy, StartDate);
        }

        public static long Update(int UserId, string CurrentSCI, DateTime Period, string Area1, string Area2,
            string Area3, string Area4, string Area5, string Area6, string Remark, int SecurityControlId, int UpdateBy,
            DateTime StartDate)
        {
            return new SecurityControlDAL().Update(UserId, CurrentSCI, Period, Area1, Area2, Area3, Area4, Area5, Area6,
                Remark, SecurityControlId, UpdateBy, StartDate);
        }

        public static long UpdateLostCard(int SecurityControlId, int Active, int IsLost, string RemarkLost,
            int UpdateUserId)
        {
            return new SecurityControlDAL().UpdateLostCard(SecurityControlId, Active, IsLost, RemarkLost, UpdateUserId);
        }

        #endregion
    }
}