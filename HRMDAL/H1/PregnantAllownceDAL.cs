using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H1;

namespace HRMDAL.H1
{
    public class PregnantAllownceDAL : Dao
    {
        #region insert, update, delete

        public int Insert(int? UserId, DateTime? AllownceDate, double? AllownceValue, int? IsCount)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@AllownceDate", SqlDbType.DateTime, 8),
                    new SqlParameter("@AllownceValue", SqlDbType.Float, 8),
                    new SqlParameter("@IsCount", SqlDbType.Int, 4)
                };
                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (AllownceDate.HasValue)
                    param[1].Value = AllownceDate.Value;
                else
                    param[1].Value = DBNull.Value;
                if (AllownceValue.HasValue)
                    param[2].Value = AllownceValue.Value;
                else
                    param[2].Value = DBNull.Value;
                if (IsCount.HasValue)
                    param[3].Value = IsCount.Value;
                else
                    param[3].Value = DBNull.Value;

                sproc = new StoreProcedure(PregnantAllownceKeys.Sp_Ins_H1_PregnantAllownce, param);
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

        public int Update(int? UserId, DateTime? AllownceDate, double? AllownceValue, int? IsCount,
            int? PregnantAllownceId)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@AllownceDate", SqlDbType.DateTime, 8),
                    new SqlParameter("@AllownceValue", SqlDbType.Float, 8),
                    new SqlParameter("@IsCount", SqlDbType.Int, 4),
                    new SqlParameter("@PregnantAllownceId", SqlDbType.Int, 4)
                };
                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (AllownceDate.HasValue)
                    param[1].Value = AllownceDate.Value;
                else
                    param[1].Value = DBNull.Value;
                if (AllownceValue.HasValue)
                    param[2].Value = AllownceValue.Value;
                else
                    param[2].Value = DBNull.Value;
                if (IsCount.HasValue)
                    param[3].Value = IsCount.Value;
                else
                    param[3].Value = DBNull.Value;
                if (PregnantAllownceId.HasValue)
                    param[4].Value = PregnantAllownceId.Value;
                else
                    param[4].Value = DBNull.Value;

                sproc = new StoreProcedure(PregnantAllownceKeys.Sp_Upd_H1_PregnantAllownce, param);
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

        public int Delete(int? PregnantAllownceId)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@PregnantAllownceId", SqlDbType.Int, 4)
                };

                if (PregnantAllownceId.HasValue)
                    param[0].Value = PregnantAllownceId.Value;
                else
                    param[0].Value = DBNull.Value;

                sproc = new StoreProcedure(PregnantAllownceKeys.Sp_Del_H1_PregnantAllownce, param);
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

        public int DeleteByUserId(int? UserId)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4)
                };

                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;

                sproc = new StoreProcedure(PregnantAllownceKeys.Sp_Del_H1_PregnantAllownce_By_UserId, param);
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

        public DataTable GetByFilter(string FullName, int? RootId, DateTime? AllownceDate)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@RootId", SqlDbType.Int, 4),
                    new SqlParameter("@AllownceDate", SqlDbType.DateTime, 8)
                };


                param[0].Value = FullName;

                if (RootId.HasValue)
                    param[1].Value = RootId.Value;
                else
                    param[1].Value = DBNull.Value;

                if (!AllownceDate.Equals(FormatDate.GetSQLDateMinValue))
                    param[2].Value = AllownceDate.Value;
                else
                    param[2].Value = DBNull.Value;

                sproc = new StoreProcedure(PregnantAllownceKeys.Sp_Sel_H1_PregnantAllownce_By_Filter, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByUserId(int? UserId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4)
                };
                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;

                sproc = new StoreProcedure(PregnantAllownceKeys.Sp_Sel_H1_PregnantAllownce_By_UserId, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetById(int? PregnantAllownceId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@PregnantAllownceId", SqlDbType.Int, 4)
                };
                if (PregnantAllownceId.HasValue)
                    param[0].Value = PregnantAllownceId.Value;
                else
                    param[0].Value = DBNull.Value;

                sproc = new StoreProcedure(PregnantAllownceKeys.Sp_Sel_H1_PregnantAllownce_By_Id, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByUserAllownceDate(int? UserId, DateTime? AllownceDate)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@AllownceDate", SqlDbType.DateTime, 8)
                };

                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (AllownceDate.HasValue)
                    param[1].Value = AllownceDate.Value;
                else
                    param[1].Value = DBNull.Value;


                sproc = new StoreProcedure(PregnantAllownceKeys.Sp_Sel_H1_PregnantAllownce_By_UserId_Date, param);
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