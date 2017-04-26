using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H1;

namespace HRMDAL.H1
{
    public class PITDeductionDAL : Dao
    {
        #region insert, update, delete

        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns>int</returns>
        public int Insert(int? UserId, long? UserRelationId, string TaxNumber, string Id_Passport, int? FromMonth,
            int? FromYear, int? ToMonth, int? ToYear, DateTime? CreateDate, int? CreateUser, DateTime? UpdateDate,
            int? UpdateUser)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@UserRelationId", SqlDbType.BigInt, 8),
                    new SqlParameter("@TaxNumber", SqlDbType.VarChar, 50),
                    new SqlParameter("@Id_Passport", SqlDbType.VarChar, 50),
                    new SqlParameter("@FromMonth", SqlDbType.Int, 4),
                    new SqlParameter("@FromYear", SqlDbType.Int, 4),
                    new SqlParameter("@ToMonth", SqlDbType.Int, 4),
                    new SqlParameter("@ToYear", SqlDbType.Int, 4),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime, 8),
                    new SqlParameter("@CreateUser", SqlDbType.Int, 4),
                    new SqlParameter("@UpdateDate", SqlDbType.DateTime, 8),
                    new SqlParameter("@UpdateUser", SqlDbType.Int, 4)
                };

                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (UserRelationId.HasValue)
                    param[1].Value = UserRelationId.Value;
                else
                    param[1].Value = DBNull.Value;
                if (TaxNumber == null)
                    param[2].Value = DBNull.Value;
                else
                    param[2].Value = TaxNumber;
                if (Id_Passport == null)
                    param[3].Value = DBNull.Value;
                else
                    param[3].Value = Id_Passport;
                if (FromMonth.HasValue)
                    param[4].Value = FromMonth.Value;
                else
                    param[4].Value = DBNull.Value;
                if (FromYear.HasValue)
                    param[5].Value = FromYear.Value;
                else
                    param[5].Value = DBNull.Value;
                if (ToMonth.HasValue)
                    param[6].Value = ToMonth.Value;
                else
                    param[6].Value = DBNull.Value;
                if (ToYear.HasValue)
                    param[7].Value = ToYear.Value;
                else
                    param[7].Value = DBNull.Value;
                if (CreateDate.HasValue)
                    param[8].Value = CreateDate.Value;
                else
                    param[8].Value = DBNull.Value;
                if (CreateUser.HasValue)
                    param[9].Value = CreateUser.Value;
                else
                    param[9].Value = DBNull.Value;
                if (UpdateDate.HasValue)
                    param[10].Value = UpdateDate.Value;
                else
                    param[10].Value = DBNull.Value;
                if (UpdateUser.HasValue)
                    param[11].Value = UpdateUser.Value;
                else
                    param[11].Value = DBNull.Value;

                sproc = new StoreProcedure(PITDeductionKeys.Sp_Ins_H1_PITDeduction, param);
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

        public int Update(int? UserId, long? UserRelationId, string TaxNumber, string Id_Passport, int? FromMonth,
            int? FromYear, int? ToMonth, int? ToYear, DateTime? UpdateDate, int? UpdateUser, int? PITDeductionId)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@UserRelationId", SqlDbType.BigInt, 8),
                    new SqlParameter("@TaxNumber", SqlDbType.VarChar, 50),
                    new SqlParameter("@Id_Passport", SqlDbType.VarChar, 50),
                    new SqlParameter("@FromMonth", SqlDbType.Int, 4),
                    new SqlParameter("@FromYear", SqlDbType.Int, 4),
                    new SqlParameter("@ToMonth", SqlDbType.Int, 4),
                    new SqlParameter("@ToYear", SqlDbType.Int, 4),
                    new SqlParameter("@UpdateDate", SqlDbType.DateTime, 8),
                    new SqlParameter("@UpdateUser", SqlDbType.Int, 4),
                    new SqlParameter("@PITDeductionId", SqlDbType.Int, 4)
                };

                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (UserRelationId.HasValue)
                    param[1].Value = UserRelationId.Value;
                else
                    param[1].Value = DBNull.Value;
                if (TaxNumber == null)
                    param[2].Value = DBNull.Value;
                else
                    param[2].Value = TaxNumber;
                if (Id_Passport == null)
                    param[3].Value = DBNull.Value;
                else
                    param[3].Value = Id_Passport;
                if (FromMonth.HasValue)
                    param[4].Value = FromMonth.Value;
                else
                    param[4].Value = DBNull.Value;
                if (FromYear.HasValue)
                    param[5].Value = FromYear.Value;
                else
                    param[5].Value = DBNull.Value;
                if (ToMonth.HasValue)
                    param[6].Value = ToMonth.Value;
                else
                    param[6].Value = DBNull.Value;
                if (ToYear.HasValue)
                    param[7].Value = ToYear.Value;
                else
                    param[7].Value = DBNull.Value;
                if (UpdateDate.HasValue)
                    param[8].Value = UpdateDate.Value;
                else
                    param[8].Value = DBNull.Value;
                if (UpdateUser.HasValue)
                    param[9].Value = UpdateUser.Value;
                else
                    param[9].Value = DBNull.Value;
                if (PITDeductionId.HasValue)
                    param[10].Value = PITDeductionId.Value;
                else
                    param[10].Value = DBNull.Value;

                sproc = new StoreProcedure(PITDeductionKeys.Sp_Upd_H1_PITDeduction, param);
                sproc.Run();

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

        public int Delete(int? PITDeductionId)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@PITDeductionId", SqlDbType.Int, 4)
                };

                if (PITDeductionId.HasValue)
                    param[0].Value = PITDeductionId.Value;
                else
                    param[0].Value = DBNull.Value;

                sproc = new StoreProcedure(PITDeductionKeys.Sp_Del_H1_PITDeduction, param);
                sproc.Run();
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

        #region Methods GET

        public DataTable GetByFilter(string FullName, int? RootId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@RootId", SqlDbType.Int, 4)
                };
                if (FullName == null)
                    param[0].Value = DBNull.Value;
                else
                    param[0].Value = FullName;

                if (RootId.HasValue)
                    param[1].Value = RootId.Value;
                else
                    param[1].Value = DBNull.Value;

                sproc = new StoreProcedure(PITDeductionKeys.Sp_Sel_H1_PITDeduction_By_Filter, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByDeptId(string FullName, string DeptIds)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@DepartmentIds", SqlDbType.VarChar, 5000)
                };
                if (FullName == null)
                    param[0].Value = DBNull.Value;
                else
                    param[0].Value = FullName;

                if (DeptIds == null)
                    param[1].Value = DBNull.Value;
                else
                    param[1].Value = DeptIds;

                sproc = new StoreProcedure(PITDeductionKeys.Sp_Sel_H1_PITDeduction_By_DeptId, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByUserIdUserRelationId(int? UserId, long? UserRelationId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@UserRelationId", SqlDbType.BigInt)
                };
                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (UserRelationId.HasValue)
                    param[1].Value = (int) UserRelationId.Value;
                else
                    param[1].Value = DBNull.Value;

                sproc = new StoreProcedure(PITDeductionKeys.Sp_Sel_H1_PITDeduction_By_UserRelation, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByUserDate(int? UserId, int? Month, int? Year)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@Month", SqlDbType.Int, 4),
                    new SqlParameter("@Year", SqlDbType.Int, 4)
                };
                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (Month.HasValue)
                    param[1].Value = Month.Value;
                else
                    param[1].Value = DBNull.Value;
                if (Year.HasValue)
                    param[2].Value = Year.Value;
                else
                    param[2].Value = DBNull.Value;

                sproc = new StoreProcedure(PITDeductionKeys.Sp_Sel_H1_PITDeduction_By_UserDate, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        #endregion
    }
}