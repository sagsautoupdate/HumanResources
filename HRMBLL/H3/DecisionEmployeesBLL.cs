using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H3;

namespace HRMBLL.H3
{
    public class DecisionEmployeesBLL
    {
        #region private filed

        public int DecisionGroupId { get; set; }

        public int DecisionEmployeeId { get; set; }

        public int DecisionId { get; set; }

        public int UserId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int RootId { get; set; }

        public int PositionId { get; set; }

        public int PositionId1 { get; set; }

        public int PositionPeriod { get; set; }

        public string Reason { get; set; }

        public string Place { get; set; }

        public string EducationName { get; set; }

        public string Cost { get; set; }

        public string Basis { get; set; }

        public string StClause { get; set; }

        public string NdClause { get; set; }

        public string RdClause { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedUserId { get; set; }

        public string StoragePlace { get; set; }

        #endregion

        //#region properties

        //public int DecisionId
        //{
        //    get
        //    {
        //        return _DecisionId;
        //    }
        //    set
        //    {
        //        _DecisionId = value;
        //    }
        //}


        //public int DecisionEmployeeId
        //{
        //    get
        //    {
        //        return _DecisionEmployeeId;
        //    }
        //    set
        //    {
        //        _DecisionEmployeeId = value;
        //    }
        //}

        //public int UserId
        //{
        //    get
        //    {
        //        return _UserId;
        //    }
        //    set
        //    {
        //        _UserId = value;
        //    }
        //}

        //public string FullName
        //{
        //    get
        //    {
        //        return _FullName;
        //    }
        //    set
        //    {
        //        _FullName = value;
        //    }
        //}

        //public int PositionId
        //{
        //    get
        //    {
        //        return _PositionId;
        //    }
        //    set
        //    {
        //        _PositionId = value;
        //    }
        //}
        //public int RootId
        //{
        //    get
        //    {
        //        return _RootId;
        //    }
        //    set
        //    {
        //        _RootId = value;
        //    }
        //}
        //public System.DateTime FromDate
        //{
        //    get
        //    {
        //        return _FromDate;
        //    }
        //    set
        //    {
        //        _FromDate = value;
        //    }
        //}

        //public System.DateTime ToDate
        //{
        //    get
        //    {
        //        return _ToDate;
        //    }
        //    set
        //    {
        //        _ToDate = value;
        //    }
        //}

        //public string Level
        //{
        //    get
        //    {
        //        return _Level;
        //    }
        //    set
        //    {
        //        _Level = value;
        //    }
        //}

        //public string Form
        //{
        //    get
        //    {
        //        return _Form;
        //    }
        //    set
        //    {
        //        _Form = value;
        //    }
        //}

        //public string Title
        //{
        //    get
        //    {
        //        return _Title;
        //    }
        //    set
        //    {
        //        _Title = value;
        //    }
        //}

        //public int KeyPosition
        //{
        //    get
        //    {
        //        return _KeyPosition;
        //    }
        //    set
        //    {
        //        _KeyPosition = value;
        //    }
        //}

        //#endregion

        #region public method Get

        public static DataTable GetByDecisionId(int decisionId)
        {
            return new DecisionEmployeesDAL().GetByDecisionId(decisionId);
        }

        public static DataRow GetDRByDecisionId(int decisionId)
        {
            var dt = new DecisionEmployeesDAL().GetByDecisionId(decisionId);
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        public static DataTable GetByDeptIdForDatatable(string deptId)
        {
            return new DecisionEmployeesDAL().GetByDeptId(deptId);
        }

        #endregion

        #region methods Insert, update, delete

        public long Save()
        {
            var objDAL = new DecisionEmployeesDAL();

            if (DecisionEmployeeId <= 0)
                return objDAL.Insert(DecisionId, UserId, FromDate, ToDate, RootId, PositionId, PositionId1,
                    PositionPeriod, Reason, Place, EducationName, Cost, Basis, StClause, NdClause, RdClause, CreatedDate,
                    CreatedUserId, StoragePlace, DecisionGroupId);
            return objDAL.Update(DecisionId, UserId, FromDate, ToDate, RootId, PositionId, PositionId1, PositionPeriod,
                Reason, Place, EducationName, Cost, Basis, StClause, NdClause, RdClause, CreatedDate, CreatedUserId,
                DecisionEmployeeId, StoragePlace, DecisionGroupId);
        }

        public static void InsertByList(List<DecisionEmployeesBLL> list)
        {
            var objDAL = new DecisionEmployeesDAL();

            foreach (var obj in list)
                if (obj.DecisionEmployeeId <= 0)
                {
                    //objDAL.Insert(obj.DecisionId, obj.UserId, obj.PositionId, obj.RootId, obj.FromDate, obj.ToDate, obj.Level, obj.Form, obj.Title, obj.KeyPosition);
                }
        }

        //public static void Update(int decisionId, int userId, int positionId, int rootId, DateTime fromDate, DateTime toDate, string level, string form, string title, int keyPosition, long decisionEmployeeId)
        //{
        //    new DecisionEmployeesDAL().Update(_DecisionId, _UserId, _FromDate, _ToDate, _RootId, _PositionId, _PositionId1, _PositionPeriod, _Reason, _Place, _EducationName, _Cost, _Basis, _stClause, _ndClause, _rdClause, _CreatedDate, _CreatedUserId, _DecisionEmployeeId);
        //}

        public static void Delete(int decisionEmployeeId)
        {
            new DecisionEmployeesDAL().Delete(decisionEmployeeId);
        }

        public static void DeleteByDecisionId(int decisionId)
        {
            new DecisionEmployeesDAL().DeleteByDecisionId(decisionId);
        }

        public static void DeleteByIds(string decisionIds)
        {
            new DecisionEmployeesDAL().DeleteByIds(decisionIds);
        }

        #endregion

        #region private methods

        //private static List<DecisionEmployeesBLL> GenerateListDecisionEmployeesBLLFromDataTable(DataTable dt)
        //{
        //    List<DecisionEmployeesBLL> list = new List<DecisionEmployeesBLL>();

        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        list.Add(GenerateDecisionEmployeesBLLFromDataRow(dr));
        //    }

        //    return list;
        //}

        //private static DecisionEmployeesBLL GenerateDecisionEmployeesBLLFromDataRow(DataRow dr)
        //{

        //    DecisionEmployeesBLL objBLL = new DecisionEmployeesBLL();

        //    objBLL._DecisionEmployeeId = dr[DecisionEmployeesKeys.Field_DecisionEmployees_DecisionEmployeeId] == DBNull.Value ? 0 : long.Parse(dr[DecisionEmployeesKeys.Field_DecisionEmployees_DecisionEmployeeId].ToString());
        //    objBLL._DecisionId = dr[DecisionEmployeesKeys.Field_DecisionEmployees_UserId] == DBNull.Value ? 0 : int.Parse(dr[DecisionEmployeesKeys.Field_DecisionEmployees_UserId].ToString());
        //    objBLL._RootId = dr[DecisionEmployeesKeys.Field_DecisionEmployees_RootId] == DBNull.Value ? 0 : int.Parse(dr[DecisionEmployeesKeys.Field_DecisionEmployees_RootId].ToString());
        //    objBLL._UserId = dr[DecisionEmployeesKeys.Field_DecisionEmployees_UserId] == DBNull.Value ? 0 : int.Parse(dr[DecisionEmployeesKeys.Field_DecisionEmployees_UserId].ToString());
        //    objBLL._FullName = dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value ? string.Empty : dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();
        //    objBLL._PositionId = dr[DecisionEmployeesKeys.Field_DecisionEmployees_PositionId] == DBNull.Value ? 0 : int.Parse(dr[DecisionEmployeesKeys.Field_DecisionEmployees_PositionId].ToString());
        //    objBLL._FromDate = dr[DecisionEmployeesKeys.Field_DecisionEmployees_FromDate] == DBNull.Value ? FormatDate.GetSQLDateMinValue : Convert.ToDateTime(dr[DecisionEmployeesKeys.Field_DecisionEmployees_FromDate].ToString());
        //    objBLL._ToDate = dr[DecisionEmployeesKeys.Field_DecisionEmployees_ToDate] == DBNull.Value ? FormatDate.GetSQLDateMinValue : Convert.ToDateTime(dr[DecisionEmployeesKeys.Field_DecisionEmployees_ToDate].ToString());
        //    objBLL._Level = dr[DecisionEmployeesKeys.Field_DecisionEmployees_Level] == DBNull.Value ? string.Empty : dr[DecisionEmployeesKeys.Field_DecisionEmployees_Level].ToString();
        //    objBLL._Form = dr[DecisionEmployeesKeys.Field_DecisionEmployees_Form] == DBNull.Value ? string.Empty : dr[DecisionEmployeesKeys.Field_DecisionEmployees_Form].ToString();
        //    objBLL._Title = dr[DecisionEmployeesKeys.Field_DecisionEmployees_Title] == DBNull.Value ? string.Empty : dr[DecisionEmployeesKeys.Field_DecisionEmployees_Title].ToString();
        //    objBLL._KeyPosition = dr[DecisionEmployeesKeys.Field_DecisionEmployees_KeyPosition] == DBNull.Value ? 0 : int.Parse(dr[DecisionEmployeesKeys.Field_DecisionEmployees_KeyPosition].ToString());

        //    return objBLL;

        //}

        #endregion
    }
}