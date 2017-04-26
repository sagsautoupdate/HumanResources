using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H;

namespace HRMDAL.H
{
    public class EmployeeIncomeDAL : Dao
    {
        #region methods insert, update, delete

        public long Insert(string userCode, double total_Inc, double total_Cntr, double total_Real, double total_Inc_LK,
            double total_Cntr_LK, DateTime date)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserCode", SqlDbType.VarChar, 50),
                    new SqlParameter("@Total_Inc", SqlDbType.Money),
                    new SqlParameter("@Total_Cntr", SqlDbType.Money),
                    new SqlParameter("@Total_Real", SqlDbType.Money),
                    new SqlParameter("@Total_Inc_LK", SqlDbType.Money),
                    new SqlParameter("@Total_Cntr_LK", SqlDbType.Money),
                    new SqlParameter("@Date", SqlDbType.DateTime)
                };
                param[0].Value = userCode;
                param[1].Value = total_Inc;
                param[2].Value = total_Cntr;
                param[3].Value = total_Real;
                param[4].Value = total_Inc_LK;
                param[5].Value = total_Cntr_LK;
                param[6].Value = date;

                sproc = new StoreProcedure(EmployeeIncomeKeys.Sp_Ins_EmployeeIncome, param);
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

        #region methods GET

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

                sproc = new StoreProcedure(EmployeeIncomeKeys.SP_EMPLOYEE_INCOME_GET_BY_USERID_MONTHLY, param);
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

        public DataTable GetByFilter(string fullName, int departmentId, int month, int year)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@DepartmentId", SqlDbType.Int),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int)
                };

                param[0].Value = fullName;
                param[1].Value = departmentId;
                param[2].Value = month;
                param[3].Value = year;

                sproc = new StoreProcedure(EmployeeIncomeKeys.SP_EMPLOYEE_INCOME_GET_BY_FILTER, param);
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

        public DataTable GetStatisticTotalByFilter(int departmentId, int month, int year)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DepartmentId", SqlDbType.Int),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int)
                };

                param[0].Value = departmentId;
                param[1].Value = month;
                param[2].Value = year;

                sproc = new StoreProcedure(EmployeeIncomeKeys.SP_Sel_H_EmployeeIncomeForStatisticTotalByFilter, param);
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