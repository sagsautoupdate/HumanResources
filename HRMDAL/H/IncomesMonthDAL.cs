using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H;

namespace HRMDAL.H
{
    public class IncomesMonthDAL : Dao
    {
        #region insert, update, delete

        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns>int</returns>
        public long Insert(int incomeTypeId, bool type, string userCode, double value, DateTime date)
        {
            long identity = 0;
            Debug.Assert(sproc == null);

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@IncomeTypeId", SqlDbType.Int),
                    new SqlParameter("@Type", SqlDbType.Bit),
                    new SqlParameter("@UserCode", SqlDbType.VarChar, 50),
                    new SqlParameter("@Value", SqlDbType.Float),
                    new SqlParameter("@Date", SqlDbType.DateTime)
                };
                param[0].Value = incomeTypeId;
                param[1].Value = type;
                param[2].Value = userCode;
                param[3].Value = value;
                param[4].Value = date;

                sproc = new StoreProcedure(IncomeMonthKeys.Sp_Ins_H_IncomeMonth, param);
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

        public DataTable GetByUserId_Monthly(int userId, int month, int year, bool type)
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
                    new SqlParameter("@Type", SqlDbType.Bit)
                };

                param[0].Value = userId;
                param[1].Value = month;
                param[2].Value = year;
                param[3].Value = type;

                sproc = new StoreProcedure(IncomeMonthKeys.SP_INCOME_MONTH_GET_BY_USERID_MONTHLY, param);
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

        public DataTable GetAllByUserId_Monthly(int userId, int month, int year)
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

                sproc = new StoreProcedure(IncomeMonthKeys.Sp_Sel_H_IncomeMonthAll_ByUserId_Monthly, param);
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