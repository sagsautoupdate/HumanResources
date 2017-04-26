using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H0;

namespace HRMDAL.H0
{
    public class EmployeeJobHistoryDAL : Dao
    {
        #region Insert, Update, Delete

        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns>int</returns>
        public long Insert(int? UserId,
            int? FromYear,
            int? ToYear,
            string Infor,
            int? Type)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@FromYear", SqlDbType.Int, 4),
                    new SqlParameter("@ToYear", SqlDbType.Int, 4),
                    new SqlParameter("@Infor", SqlDbType.NVarChar, 2000),
                    new SqlParameter("@Type", SqlDbType.Int, 4)
                };

                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (FromYear.HasValue)
                    param[1].Value = FromYear.Value;
                else
                    param[1].Value = DBNull.Value;
                if (ToYear.HasValue)
                    param[2].Value = ToYear.Value;
                else
                    param[2].Value = DBNull.Value;
                if (Infor == null)
                    param[3].Value = DBNull.Value;
                else
                    param[3].Value = Infor;
                if (Type.HasValue)
                    param[4].Value = Type.Value;
                else
                    param[4].Value = DBNull.Value;

                sproc = new StoreProcedure(EmployeeJobHistoryKeys.Sp_EmployeeJobHistory_Insert, param);
                identity = sproc.Run();
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

        public long Update(
            int? UserId,
            int? FromYear,
            int? ToYear,
            string Infor,
            int? Type,
            long? JobHistoryId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@FromYear", SqlDbType.Int, 4),
                    new SqlParameter("@ToYear", SqlDbType.Int, 4),
                    new SqlParameter("@Infor", SqlDbType.NVarChar, 2000),
                    new SqlParameter("@Type", SqlDbType.Int, 4),
                    new SqlParameter("@JobHistoryId", SqlDbType.BigInt, 8)
                };

                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (FromYear.HasValue)
                    param[1].Value = FromYear.Value;
                else
                    param[1].Value = DBNull.Value;
                if (ToYear.HasValue)
                    param[2].Value = ToYear.Value;
                else
                    param[2].Value = DBNull.Value;
                if (Infor == null)
                    param[3].Value = DBNull.Value;
                else
                    param[3].Value = Infor;
                if (Type.HasValue)
                    param[4].Value = Type.Value;
                else
                    param[4].Value = DBNull.Value;
                if (JobHistoryId.HasValue)
                    param[5].Value = JobHistoryId.Value;
                else
                    param[5].Value = DBNull.Value;

                sproc = new StoreProcedure(EmployeeJobHistoryKeys.Sp_EmployeeJobHistory_Update, param);
                identity = sproc.Run();
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


        public long Delete(long? JobHistoryId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@JobHistoryId", SqlDbType.BigInt)
                };

                if (JobHistoryId.HasValue)
                    param[0].Value = JobHistoryId.Value;
                else
                    param[0].Value = DBNull.Value;

                sproc = new StoreProcedure(EmployeeJobHistoryKeys.Sp_EmployeeJobHistory_Delete, param);
                identity = sproc.Run();
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

        #region Get

        public DataTable GetOne(int JobHistoryId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@JobHistoryId", SqlDbType.Int, 4)
                };

                param[0].Value = JobHistoryId;

                sproc = new StoreProcedure("Sel_H0_EmployeeJobHistoryById", param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetByFilter(int? Type, int? UserId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Type", SqlDbType.Int, 4),
                    new SqlParameter("@UserId", SqlDbType.Int, 4)
                };

                if (Type.HasValue)
                    param[0].Value = Type.Value;
                else
                    param[0].Value = DBNull.Value;
                if (UserId.HasValue)
                    param[1].Value = UserId.Value;
                else
                    param[1].Value = DBNull.Value;

                sproc = new StoreProcedure(EmployeeJobHistoryKeys.Sp_EmployeeJobHistory_Get_By_Filter, param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        #endregion
    }
}