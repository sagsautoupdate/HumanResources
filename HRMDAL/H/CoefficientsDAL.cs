using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H;

namespace HRMDAL.H
{
    public class CoefficientsDAL : Dao
    {
        #region insert, update, delete

        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns>int</returns>
        public long Insert(int CoefficientTypeId, string userCode, double value, DateTime date)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@CoefficientTypeId", SqlDbType.NVarChar, 50),
                    new SqlParameter("@UserCode", SqlDbType.VarChar, 50),
                    new SqlParameter("@Value", SqlDbType.Float),
                    new SqlParameter("@Date", SqlDbType.DateTime)
                };
                param[0].Value = CoefficientTypeId;
                param[1].Value = userCode;
                param[2].Value = value;
                param[3].Value = date;

                sproc = new StoreProcedure(CoefficientKeys.SP_COEFFICIENT_INSERT, param);
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

        #region methods get

        public DataTable GetByUserId_Monthly(int userId, int month, int year)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int)
                };

                param[0].Value = userId;
                param[1].Value = month;
                param[2].Value = year;

                sproc = new StoreProcedure(CoefficientKeys.SP_COEFFICIENT_GETALL_BY_USERID_MONTHLY, param);
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
    }
}