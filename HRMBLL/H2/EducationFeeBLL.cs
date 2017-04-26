using System;
using System.Data;
using HRMDAL.H2;

namespace HRMBLL.H2
{
    public class EducationFeeBLL
    {
        #region Private Fields

        public DateTime CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Fee { get; set; }

        public int FeeId { get; set; }

        public string FeeInVietNamese { get; set; }

        public int SessionId { get; set; }

        #endregion

        #region Get

        public static DataTable GetAll()
        {
            return new EducationFeeDAL().GetAll();
        }

        public static DataRow GetById(int FeeId)
        {
            var dt = new EducationFeeDAL().GetById(FeeId);
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        public static DataTable GetBySessionId(int SessionId)
        {
            var dt = new EducationFeeDAL().GetBySessionId(SessionId);
            return dt;
        }

        public static DataRow GetBySessionIdPositionId(int SessionId, int PositionId)
        {
            var dt = new EducationFeeDAL().GetBySessionIdPositionId(SessionId, PositionId);
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        #endregion

        #region Ins-Upd-Del

        public static long Insert(int SessionId, int PositionId, string Fee, string FeeInVietNamese, int CreatedBy)
        {
            return new EducationFeeDAL().Insert(SessionId, PositionId, Fee, FeeInVietNamese, CreatedBy);
        }

        public static long Update(int SessionId, int PositionId, string Fee, string FeeInVietNamese, int CreatedBy,
            int FeeId)
        {
            return new EducationFeeDAL().Update(SessionId, PositionId, Fee, FeeInVietNamese, CreatedBy, FeeId);
        }

        public static long Delete(int FeeId)
        {
            return new EducationFeeDAL().Delete(FeeId);
        }

        #endregion
    }
}