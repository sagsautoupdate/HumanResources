using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H0;
using HRMUtil.KeyNames.H0;

namespace HRMBLL.H0
{
    public class EmployeeRelationBLL
    {
        #region private fields

        private string _SP = "";
        private string _SPValue = "";

        #endregion

        #region properties

        public long UserRelationId { get; set; }

        public int UserId { get; set; }

        public int RelationTypeId { get; set; }

        public string RFullName { get; set; } = string.Empty;

        public int RDayOfBirth { get; set; }

        public int RMonthOfBirth { get; set; }

        public int RYearOfBirth { get; set; }

        public string RNativePlace { get; set; } = string.Empty;

        public string RResident { get; set; } = string.Empty;

        public string RLive { get; set; } = string.Empty;

        public string Before1975 { get; set; } = string.Empty;

        public string After1975 { get; set; } = string.Empty;

        public string Participate { get; set; } = string.Empty;

        public bool Died { get; set; }

        public string DiedCause { get; set; } = string.Empty;

        public string Others { get; set; } = string.Empty;

        public string RelationTypeName { get; set; }

        public int Type { get; set; }

        #endregion

        #region public methods Get

        public static List<EmployeeRelationBLL> GetByFilter(int? relationTypeId, int? userId, int? type)
        {
            return
                GenerateListEmployeeRelationBLLFromDataTable(new EmployeeRelationDAL().GetByFilter(relationTypeId,
                    userId, type));
        }

        public static DataTable GetByFilterDT(int? relationTypeId, int? userId, int? type)
        {
            return new EmployeeRelationDAL().GetByFilter(relationTypeId, userId, type);
        }

        public static DataRow GetByFilterDR(int? relationTypeId, int? userId, int? type)
        {
            var dt = new EmployeeRelationDAL().GetByFilter(relationTypeId, userId, type);

            if (dt.Rows.Count == 1)
                return dt.Rows[0];
            return null;
        }

        public static DataRow GetByUserRelationId(int UserRelationId)
        {
            var dt = new EmployeeRelationDAL().GetByUserRelationId(UserRelationId);

            if (dt.Rows.Count == 1)
                return dt.Rows[0];
            return null;
        }

        #endregion

        #region public methods Insert, Update, Delete

        public string ReturnSP()
        {
            return _SP;
        }

        public string ReturnSPValue()
        {
            return _SPValue;
        }

        public long Save()
        {
            var objDAL = new EmployeeRelationDAL();
            if (UserRelationId <= 0)
            {
                _SP = $"{EmployeeRelationKeys.Sp_EmployeeRelation_Insert}";
                _SPValue =
                    $"{UserId},{RelationTypeId},N'{RFullName}',{RDayOfBirth},{RMonthOfBirth},{RYearOfBirth},N'{RNativePlace}',N'{RResident}',N'{RLive}',N'{Before1975}',N'{After1975}',N'{Participate}','{Died}',N'{DiedCause}',N'{Others}'";

                return objDAL.Insert(UserId, RelationTypeId, RFullName, RDayOfBirth, RMonthOfBirth, RYearOfBirth,
                    RNativePlace,
                    RResident, RLive, Before1975, After1975, Participate, Died, DiedCause, Others);
            }
            _SP = $"{EmployeeRelationKeys.Sp_EmployeeRelation_Update}";
            _SPValue =
                $"{UserId},{RelationTypeId},N'{RFullName}',{RDayOfBirth},{RMonthOfBirth},{RYearOfBirth},N'{RNativePlace}',N'{RResident}',N'{RLive}',N'{Before1975}',N'{After1975}',N'{Participate}','{Died}',N'{DiedCause}',N'{Others}', {UserRelationId}";

            return objDAL.Update(UserId, RelationTypeId, RFullName, RDayOfBirth, RMonthOfBirth, RYearOfBirth,
                RNativePlace,
                RResident, RLive, Before1975, After1975, Participate, Died, DiedCause, Others, UserRelationId);
        }

        public static void Update(int? userId,
            int? relationTypeId,
            string rFullName,
            int? rDayOfBirth,
            int? rMonthOfBirth,
            int? rYearOfBirth,
            string rNativePlace,
            string rResident,
            string rLive,
            string before1975,
            string after1975,
            string participate,
            bool? died,
            string diedCause,
            string others,
            long? userRelationId)
        {
            new EmployeeRelationDAL().Update(userId, relationTypeId, rFullName, rDayOfBirth, rMonthOfBirth, rYearOfBirth,
                rNativePlace,
                rResident, rLive, before1975, after1975, participate, died, diedCause, others, userRelationId);
        }

        public static void Delete(long? userRelationId)
        {
            new EmployeeRelationDAL().Delete(userRelationId);
        }

        #endregion

        #region private methods, generate helper methods

        private static List<EmployeeRelationBLL> GenerateListEmployeeRelationBLLFromDataTable(DataTable dt)
        {
            var list = new List<EmployeeRelationBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateEmployeeRelationBLLFromDataRow(dr));

            return list;
        }

        private static EmployeeRelationBLL GenerateEmployeeRelationBLLFromDataRow(DataRow dr)
        {
            var objBLL = new EmployeeRelationBLL();

            objBLL.UserRelationId = dr[EmployeeRelationKeys.Field_EmployeeRelation_UserRelationId] == DBNull.Value
                ? 0
                : (long) dr[EmployeeRelationKeys.Field_EmployeeRelation_UserRelationId];
            objBLL.UserId = dr[EmployeeRelationKeys.Field_EmployeeRelation_UserId] == DBNull.Value
                ? 0
                : (int) dr[EmployeeRelationKeys.Field_EmployeeRelation_UserId];
            objBLL.RelationTypeId = dr[EmployeeRelationKeys.Field_EmployeeRelation_RelationTypeId] == DBNull.Value
                ? 0
                : (int) dr[EmployeeRelationKeys.Field_EmployeeRelation_RelationTypeId];
            objBLL.RFullName = dr[EmployeeRelationKeys.Field_EmployeeRelation_RFullName] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeRelationKeys.Field_EmployeeRelation_RFullName];
            objBLL.RDayOfBirth = dr[EmployeeRelationKeys.Field_EmployeeRelation_RDayOfBirth] == DBNull.Value
                ? 0
                : (int) dr[EmployeeRelationKeys.Field_EmployeeRelation_RDayOfBirth];
            objBLL.RMonthOfBirth = dr[EmployeeRelationKeys.Field_EmployeeRelation_RMonthOfBirth] == DBNull.Value
                ? 0
                : (int) dr[EmployeeRelationKeys.Field_EmployeeRelation_RMonthOfBirth];
            objBLL.RYearOfBirth = dr[EmployeeRelationKeys.Field_EmployeeRelation_RYearOfBirth] == DBNull.Value
                ? 0
                : (int) dr[EmployeeRelationKeys.Field_EmployeeRelation_RYearOfBirth];
            objBLL.RNativePlace = dr[EmployeeRelationKeys.Field_EmployeeRelation_RNativePlace] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeRelationKeys.Field_EmployeeRelation_RNativePlace];
            objBLL.RResident = dr[EmployeeRelationKeys.Field_EmployeeRelation_RResident] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeRelationKeys.Field_EmployeeRelation_RResident];
            objBLL.RLive = dr[EmployeeRelationKeys.Field_EmployeeRelation_RLive] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeRelationKeys.Field_EmployeeRelation_RLive];
            objBLL.Before1975 = dr[EmployeeRelationKeys.Field_EmployeeRelation_Before1975] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeRelationKeys.Field_EmployeeRelation_Before1975];
            objBLL.After1975 = dr[EmployeeRelationKeys.Field_EmployeeRelation_After1975] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeRelationKeys.Field_EmployeeRelation_After1975];
            objBLL.Participate = dr[EmployeeRelationKeys.Field_EmployeeRelation_Participate] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeRelationKeys.Field_EmployeeRelation_Participate];
            objBLL.Died = dr[EmployeeRelationKeys.Field_EmployeeRelation_Died] == DBNull.Value
                ? false
                : Convert.ToBoolean(dr[EmployeeRelationKeys.Field_EmployeeRelation_Died]);
            objBLL.DiedCause = dr[EmployeeRelationKeys.Field_EmployeeRelation_DiedCause] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeRelationKeys.Field_EmployeeRelation_DiedCause];
            objBLL.Others = dr[EmployeeRelationKeys.Field_EmployeeRelation_Others] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeRelationKeys.Field_EmployeeRelation_Others];

            try
            {
                objBLL.RelationTypeName = dr[RelationTypeKeys.FIELD_RELATION_TYPE_NAME] == DBNull.Value
                    ? string.Empty
                    : (string) dr[RelationTypeKeys.FIELD_RELATION_TYPE_NAME];
            }
            catch
            {
            }
            try
            {
                objBLL.Type = dr[RelationTypeKeys.FIELD_RELATION_TYPE_TYPE] == DBNull.Value
                    ? -1
                    : (int) dr[RelationTypeKeys.FIELD_RELATION_TYPE_TYPE];
            }
            catch
            {
            }

            return objBLL;
        }

        #endregion
    }
}