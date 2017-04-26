using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H;

namespace HRMDAL.H
{
    public class TimeKeepingDAL : Dao
    {
        #region insert, update, delete

        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns>int</returns>
        public long Insert(int timeKeepingTypeId, string userCode, double value, DateTime timeKeepingDate)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@TimeKeepingTypeId", SqlDbType.Int),
                    new SqlParameter("@UserCode", SqlDbType.VarChar, 50),
                    new SqlParameter("@Value", SqlDbType.Float),
                    new SqlParameter("@TimeKeepingDate", SqlDbType.DateTime)
                };
                param[0].Value = timeKeepingTypeId;
                param[1].Value = userCode;
                param[2].Value = value;
                param[3].Value = timeKeepingDate;

                sproc = new StoreProcedure(TimeKeepingKeys.SP_TIME_KEEPING_INSERT, param);
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

        #region methods Get

        public DataTable GetByUserId_Monthly(int userId, int month, int year, int type)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int),
                    new SqlParameter("@Type", SqlDbType.Int)
                };

                param[0].Value = userId;
                param[1].Value = month;
                param[2].Value = year;
                param[3].Value = type;

                sproc = new StoreProcedure(TimeKeepingKeys.SP_TIME_KEEPING_GETALL_BY_USERID_MONTHLY, param);
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