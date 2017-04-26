using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H3;

namespace HRMDAL.H3
{
    public class DecisionEmployeesDAL : Dao
    {
        #region Methods Get

        public DataTable GetByDecisionId(int? DecisionId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DecisionId", SqlDbType.Int, 4)
                };

                if (DecisionId.HasValue)
                    param[0].Value = DecisionId.Value;
                else
                    param[0].Value = DBNull.Value;
                sproc = new StoreProcedure(DecisionEmployeesKeys.Sp_Sel_H3_DecisionEmployees_By_DecisionId, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByDeptId(string deptId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DeptId", SqlDbType.VarChar, 1000)
                };
                param[0].Value = deptId;
                sproc = new StoreProcedure(DecisionEmployeesKeys.Sp_Sel_H3_DecisionEmployees_By_DeptId, param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return datatable;
        }

        #endregion

        #region methods inset, update , delete

        public long Insert(int? DecisionId, int? UserId, DateTime? FromDate, DateTime? ToDate, int? RootId,
            int? PositionId, int? PositionId1, int? PositionPeriod, string Reason, string Place, string EducationName,
            string Cost, string Basis, string stClause, string ndClause, string rdClause, DateTime CreatedDate,
            int? CreatedUserId, string StoragePlace, int? DecisionGroupId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DecisionId", SqlDbType.Int, 4),
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@FromDate", SqlDbType.DateTime),
                    new SqlParameter("@ToDate", SqlDbType.DateTime),
                    new SqlParameter("@RootId", SqlDbType.Int, 4),
                    new SqlParameter("@PositionId", SqlDbType.Int, 4),
                    new SqlParameter("@PositionId1", SqlDbType.Int, 4),
                    new SqlParameter("@PositionPeriod", SqlDbType.Int, 4),
                    new SqlParameter("@Reason", SqlDbType.NText),
                    new SqlParameter("@Place", SqlDbType.NText),
                    new SqlParameter("@EducationName", SqlDbType.NText),
                    new SqlParameter("@Cost", SqlDbType.NText),
                    new SqlParameter("@Basis", SqlDbType.NText),
                    new SqlParameter("@1stClause", SqlDbType.NText),
                    new SqlParameter("@2ndClause", SqlDbType.NText),
                    new SqlParameter("@3rdClause", SqlDbType.NText),
                    new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@CreatedUserId", SqlDbType.Int, 4),
                    new SqlParameter("@StoragePlace", SqlDbType.NText),
                    new SqlParameter("@DecisionGroupId", SqlDbType.Int, 4)
                };
                if (DecisionId.HasValue)
                    param[0].Value = DecisionId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (UserId.HasValue)
                    param[1].Value = UserId.Value;
                else
                    param[1].Value = DBNull.Value;
                if (FromDate.HasValue)
                    if (FromDate.Value.ToString("dd/MM/yyyy") == "01/01/0001")
                        param[2].Value = FormatDate.GetSQLDateMinValue;
                    else
                        param[2].Value = FromDate.Value;
                else
                    param[2].Value = FormatDate.GetSQLDateMinValue;
                if (ToDate.HasValue)
                    if (ToDate.Value.ToString("dd/MM/yyyy") == "01/01/0001")
                        param[3].Value = FormatDate.GetSQLDateMinValue;
                    else
                        param[3].Value = ToDate.Value;
                else
                    param[3].Value = FormatDate.GetSQLDateMinValue;
                if (RootId.HasValue)
                    param[4].Value = RootId.Value;
                else
                    param[4].Value = DBNull.Value;
                if (PositionId.HasValue)
                    param[5].Value = PositionId.Value;
                else
                    param[5].Value = DBNull.Value;
                if (PositionId1.HasValue)
                    param[6].Value = PositionId1.Value;
                else
                    param[6].Value = DBNull.Value;
                if (PositionPeriod.HasValue)
                    param[7].Value = PositionPeriod.Value;
                else
                    param[7].Value = DBNull.Value;
                if (Reason == null)
                    param[8].Value = DBNull.Value;
                else
                    param[8].Value = Reason;
                if (Place == null)
                    param[9].Value = DBNull.Value;
                else
                    param[9].Value = Place;
                if (EducationName == null)
                    param[10].Value = DBNull.Value;
                else
                    param[10].Value = EducationName;
                if (Cost == null)
                    param[11].Value = DBNull.Value;
                else
                    param[11].Value = Cost;
                if (Basis == null)
                    param[12].Value = DBNull.Value;
                else
                    param[12].Value = Basis;
                if (stClause == null)
                    param[13].Value = DBNull.Value;
                else
                    param[13].Value = stClause;
                if (ndClause == null)
                    param[14].Value = DBNull.Value;
                else
                    param[14].Value = ndClause;
                if (rdClause == null)
                    param[15].Value = DBNull.Value;
                else
                    param[15].Value = rdClause;
                if (CreatedDate == null)
                    param[16].Value = DBNull.Value;
                else
                    param[16].Value = CreatedDate;
                if (CreatedUserId.HasValue)
                    param[17].Value = CreatedUserId.Value;
                else
                    param[17].Value = DBNull.Value;
                if (StoragePlace == null)
                    param[18].Value = DBNull.Value;
                else
                    param[18].Value = StoragePlace;
                if (DecisionGroupId.HasValue)
                    param[19].Value = DecisionGroupId.Value;
                else
                    param[19].Value = DBNull.Value;

                sproc = new StoreProcedure(DecisionEmployeesKeys.Sp_Ins_H3_DecisionEmployees, param);
                identity = sproc.RunInt();
                sproc.Commit();
            }
            catch (SqlException se)
            {
                sproc.RollBack();
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return identity;
        }

        public long Update(int? DecisionId, int? UserId, DateTime? FromDate, DateTime? ToDate, int? RootId,
            int? PositionId, int? PositionId1, int? PositionPeriod, string Reason, string Place, string EducationName,
            string Cost, string Basis, string stClause, string ndClause, string rdClause, DateTime CreatedDate,
            int? CreatedUserId, int DecisionEmployeeId, string StoragePlace, int? DecisionGroupId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DecisionId", SqlDbType.Int, 4),
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@FromDate", SqlDbType.DateTime),
                    new SqlParameter("@ToDate", SqlDbType.DateTime),
                    new SqlParameter("@RootId", SqlDbType.Int, 4),
                    new SqlParameter("@PositionId", SqlDbType.Int, 4),
                    new SqlParameter("@PositionId1", SqlDbType.Int, 4),
                    new SqlParameter("@PositionPeriod", SqlDbType.Int, 4),
                    new SqlParameter("@Reason", SqlDbType.NText),
                    new SqlParameter("@Place", SqlDbType.NText),
                    new SqlParameter("@EducationName", SqlDbType.NText),
                    new SqlParameter("@Cost", SqlDbType.NText),
                    new SqlParameter("@Basis", SqlDbType.NText),
                    new SqlParameter("@1stClause", SqlDbType.NText),
                    new SqlParameter("@2ndClause", SqlDbType.NText),
                    new SqlParameter("@3rdClause", SqlDbType.NText),
                    new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@CreatedUserId", SqlDbType.Int, 4),
                    new SqlParameter("@DecisionEmployeeId", SqlDbType.Int, 4),
                    new SqlParameter("@StoragePlace", SqlDbType.NText),
                    new SqlParameter("@DecisionGroupId", SqlDbType.Int, 4)
                };
                if (DecisionId.HasValue)
                    param[0].Value = DecisionId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (UserId.HasValue)
                    param[1].Value = UserId.Value;
                else
                    param[1].Value = DBNull.Value;
                if (FromDate.HasValue)
                    if (FromDate.Value.ToString("dd/MM/yyyy") == "01/01/0001")
                        param[2].Value = FormatDate.GetSQLDateMinValue;
                    else
                        param[2].Value = FromDate.Value;
                else
                    param[2].Value = FormatDate.GetSQLDateMinValue;
                if (ToDate.HasValue)
                    if (ToDate.Value.ToString("dd/MM/yyyy") == "01/01/0001")
                        param[3].Value = FormatDate.GetSQLDateMinValue;
                    else
                        param[3].Value = ToDate.Value;
                if (RootId.HasValue)
                    param[4].Value = RootId.Value;
                else
                    param[4].Value = DBNull.Value;
                if (PositionId.HasValue)
                    param[5].Value = PositionId.Value;
                else
                    param[5].Value = DBNull.Value;
                if (PositionId1.HasValue)
                    param[6].Value = PositionId1.Value;
                else
                    param[6].Value = DBNull.Value;
                if (PositionPeriod.HasValue)
                    param[7].Value = PositionPeriod.Value;
                else
                    param[7].Value = DBNull.Value;
                if (Reason == null)
                    param[8].Value = DBNull.Value;
                else
                    param[8].Value = Reason;
                if (Place == null)
                    param[9].Value = DBNull.Value;
                else
                    param[9].Value = Place;
                if (EducationName == null)
                    param[10].Value = DBNull.Value;
                else
                    param[10].Value = EducationName;
                if (Cost == null)
                    param[11].Value = DBNull.Value;
                else
                    param[11].Value = Cost;
                if (Basis == null)
                    param[12].Value = DBNull.Value;
                else
                    param[12].Value = Basis;
                if (stClause == null)
                    param[13].Value = DBNull.Value;
                else
                    param[13].Value = stClause;
                if (ndClause == null)
                    param[14].Value = DBNull.Value;
                else
                    param[14].Value = ndClause;
                if (rdClause == null)
                    param[15].Value = DBNull.Value;
                else
                    param[15].Value = rdClause;
                if (CreatedDate == null)
                    param[16].Value = DBNull.Value;
                else
                    param[16].Value = CreatedDate;
                if (CreatedUserId.HasValue)
                    param[17].Value = CreatedUserId.Value;
                else
                    param[17].Value = DBNull.Value;
                param[18].Value = DecisionEmployeeId;
                if (StoragePlace == null)
                    param[19].Value = DBNull.Value;
                else
                    param[19].Value = StoragePlace;
                if (DecisionGroupId.HasValue)
                    param[20].Value = DecisionGroupId.Value;
                else
                    param[20].Value = DBNull.Value;

                sproc = new StoreProcedure(DecisionEmployeesKeys.Sp_Upd_H3_DecisionEmployees, param);
                identity = sproc.RunInt();
                sproc.Commit();
            }
            catch (SqlException se)
            {
                sproc.RollBack();
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return identity;
        }

        public long Delete(int? DecisionEmployeeId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DecisionEmployeeId", SqlDbType.BigInt, 8)
                };

                param[0].Value = DecisionEmployeeId;
                sproc = new StoreProcedure(DecisionEmployeesKeys.Sp_Del_H3_DecisionEmployees, param);
                identity = sproc.RunInt();
                sproc.Commit();
            }
            catch (SqlException se)
            {
                sproc.RollBack();
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return identity;
        }

        public long DeleteByDecisionId(int? DecisionId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DecisionId", SqlDbType.Int, 4)
                };

                param[0].Value = DecisionId;
                sproc = new StoreProcedure(DecisionEmployeesKeys.Sp_Del_H3_DecisionEmployees_By_DecisionId, param);
                identity = sproc.RunInt();
                sproc.Commit();
            }
            catch (SqlException se)
            {
                sproc.RollBack();
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return identity;
        }

        public long DeleteByIds(string DecisionEmployeeIds)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DecisionEmployeeIds", SqlDbType.VarChar, 1000)
                };

                param[0].Value = DecisionEmployeeIds;
                sproc = new StoreProcedure(DecisionEmployeesKeys.Sp_Del_H3_DecisionEmployees_By_Ids, param);
                identity = sproc.RunInt();
                sproc.Commit();
            }
            catch (SqlException se)
            {
                sproc.RollBack();
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return identity;
        }

        #endregion
    }
}