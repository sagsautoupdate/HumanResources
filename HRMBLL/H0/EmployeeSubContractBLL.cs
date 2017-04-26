using System;
using System.Data;
using HRMDAL.H0;

namespace HRMBLL.H0
{
    public class EmployeeSubContractBLL
    {
        #region Variables

        public int PositionId { get; set; }

        public int EmployeeSubContractId { get; set; }

        public int EmployeeContractId { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string Detail { get; set; }

        public bool Active { get; set; }

        public string Duration { get; set; }

        #endregion

        #region Constructors

        public EmployeeSubContractBLL()
        {
        }

        public EmployeeSubContractBLL(int employeeSubContractId, int employeeContractId, int UserId)
        {
            EmployeeSubContractId = employeeSubContractId;
            EmployeeContractId = employeeContractId;
            this.UserId = UserId;
        }

        #endregion

        #region GET

        public static DataRow DR_GetAllBySubContractId(int id)
        {
            var bySubContractId = new EmployeeSubContractDAL().GetBySubContractId(id);
            if (bySubContractId.Rows.Count > 0)
                return bySubContractId.Rows[0];
            return null;
        }

        public static DataTable GetAllBySubContractId(int id)
        {
            return new EmployeeSubContractDAL().GetBySubContractId(id);
        }

        public static DataTable GetAll()
        {
            return new EmployeeSubContractDAL().GetAll();
        }

        public static DataTable GetAllDistinct()
        {
            return new EmployeeSubContractDAL().GetAllDistinct();
        }

        public static DataTable GetAllByUserIdDT(int id)
        {
            return new EmployeeSubContractDAL().GetBySubContractUserId(id);
        }

        public static DataRow GetAllByUserId(int id)
        {
            var dt = new EmployeeSubContractDAL().GetBySubContractUserId(id);

            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        public static DataRow GetAllByUserIdByActive(string id)
        {
            var dt = new EmployeeSubContractDAL().GetBySubContractUserIdByActive(id);

            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        public static DataTable GetAllActiveByUserId(string ids)
        {
            return new EmployeeSubContractDAL().GetBySubContractUserIdByActive(ids);
        }

        public static DataTable GetDetail(int userid, int subId)
        {
            return new EmployeeSubContractDAL().GetDetail(userid, subId);
        }

        public static DataTable GetDuration(int userid, int subId)
        {
            return new EmployeeSubContractDAL().GetDuration(userid, subId);
        }

        #endregion

        #region Insert, Update, Delete

        //public long Save()
        //{
        //    EmployeeSubContractDAL objDAL = new EmployeeSubContractDAL();
        //    if (_EmployeeSubContractId <= 0)
        //    {
        //        return objDAL.Insert(_EmployeeContractId, _UserId, _CreatedDate, _FromDate, _ToDate, _Detail, _PositionId);
        //    }
        //    else
        //    {
        //        return objDAL.Update(_EmployeeSubContractId, _EmployeeContractId, _UserId, _CreatedDate, _FromDate, _ToDate, _Detail, _Active, _PositionId);
        //    }
        //}
        public static long Insert(int EmployeeContractId, int UserId, DateTime CreatedDate, DateTime FromDate,
            DateTime ToDate, int PositionId, int ScaleOfSalaryId, int Value, string Detail, string Duration,
            int SubContractTypeId)
        {
            return new EmployeeSubContractDAL().Insert(EmployeeContractId, UserId, CreatedDate, FromDate, ToDate,
                PositionId, ScaleOfSalaryId, Value, Detail, Duration, SubContractTypeId);
        }

        public static long Update(int EmployeeSubContractId, int UserId, DateTime FromDate, DateTime ToDate,
            int PositionId, int ScaleOfSalaryId, int Value, string Detail, string Duration, int SubContractTypeId)
        {
            return new EmployeeSubContractDAL().Update(EmployeeSubContractId, UserId, FromDate, ToDate, PositionId,
                ScaleOfSalaryId, Value, Detail, Duration, SubContractTypeId);
        }

        public static long UpdateActive(int EmployeeSubContractId, bool Status)
        {
            return new EmployeeSubContractDAL().UpdateActive(EmployeeSubContractId, Status);
        }

        #endregion
    }
}