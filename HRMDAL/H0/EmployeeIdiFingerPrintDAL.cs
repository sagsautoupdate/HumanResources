using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H0;

namespace HRMDAL.H0
{
    public class EmployeeIdiFingerPrintDAL : Dao
    {
        #region get

        public DataTable GetByFilter(int? UserId, int? FingerIndex, int? RootId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@FingerIndex", SqlDbType.Int, 4),
                    new SqlParameter("@RootId", SqlDbType.Int, 4)
                };
                param[0].Value = UserId;
                param[1].Value = FingerIndex;
                param[2].Value = RootId;

                sproc = new StoreProcedure(EmployeeIdiFingerPrintKeys.Sp_Sel_H0_EmployeeIdiFingerPrintByFilter, param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return dt;
        }

        #endregion

        #region insert, update, delete

        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns>int</returns>
        public long Insert(int? UserId, int? FingerIndex, int? IndexValue, byte[] Features)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@FingerIndex", SqlDbType.Int, 4),
                    new SqlParameter("@IndexValue", SqlDbType.Int, 4),
                    new SqlParameter("@Features", SqlDbType.Binary, 4000)
                };

                param[0].Value = UserId;
                param[1].Value = FingerIndex;
                param[2].Value = IndexValue;
                param[3].Value = Features;

                sproc = new StoreProcedure(EmployeeIdiFingerPrintKeys.Sp_Ins_H0_EmployeeIdiFingerPrint, param);
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

        public long Update(int? UserId, int? FingerIndex, int? IndexValue, byte[] Features, int? PK_UserFingerPrintId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@FingerIndex", SqlDbType.Int, 4),
                    new SqlParameter("@IndexValue", SqlDbType.Int, 4),
                    new SqlParameter("@Features", SqlDbType.Binary, 4000),
                    new SqlParameter("@PK_UserFingerPrintId", SqlDbType.Int)
                };

                param[0].Value = UserId;
                param[1].Value = FingerIndex;
                param[2].Value = IndexValue;
                param[3].Value = Features;
                param[4].Value = PK_UserFingerPrintId;

                sproc = new StoreProcedure(EmployeeIdiFingerPrintKeys.Sp_Upd_H0_EmployeeIdiFingerPrint, param);
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

        public int Delete(int? PK_UserFingerPrintId)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@PK_UserFingerPrintId", SqlDbType.Int)
                };

                param[0].Value = PK_UserFingerPrintId;

                sproc = new StoreProcedure(EmployeeIdiFingerPrintKeys.Sp_Del_H0_EmployeeIdiFingerPrint, param);
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

                param[0].Value = UserId;

                sproc = new StoreProcedure(EmployeeIdiFingerPrintKeys.Sp_Del_H0_EmployeeIdiFingerPrintByUserId, param);
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

        public int DeleteByUserIdFingerIndex(int? UserId, int? FingerIndex)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@FingerIndex", SqlDbType.Int, 4)
                };

                param[0].Value = UserId;
                param[1].Value = FingerIndex;

                sproc =
                    new StoreProcedure(EmployeeIdiFingerPrintKeys.Sp_Del_H0_EmployeeIdiFingerPrintByUserIdFingerIndex,
                        param);
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
    }
}