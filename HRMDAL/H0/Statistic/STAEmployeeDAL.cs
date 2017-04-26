using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;

namespace HRMDAL.H0.Statistic
{
    public class STAEmployeeDAL : Dao
    {
        #region method Get

        //////////////////////////////////////////////////////////////////
        public DataTable StatisticSexMarriage()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure("Statistic_H0_Employees_For_Sex_Marriage", null);
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

        public DataTable StatisticLeave(DateTime FromDate, DateTime ToDate, int IsStatistic)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FromDate", SqlDbType.DateTime),
                    new SqlParameter("@ToDate", SqlDbType.DateTime),
                    new SqlParameter("@IsStatistic", SqlDbType.Int)
                };

                param[0].Value = FromDate;
                param[1].Value = ToDate;
                param[2].Value = IsStatistic;

                sproc = new StoreProcedure("Statistic_H0_Employees_For_Leave", param);
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

        public DataTable StatisticJoin(DateTime FromDate, DateTime ToDate, int IsStatistic)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FromDate", SqlDbType.DateTime),
                    new SqlParameter("@ToDate", SqlDbType.DateTime),
                    new SqlParameter("@IsStatistic", SqlDbType.Int)
                };

                param[0].Value = FromDate;
                param[1].Value = ToDate;
                param[2].Value = IsStatistic;

                sproc = new StoreProcedure("Statistic_H0_Employees_For_Join", param);
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

        public DataTable StatisticSexContractType()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure("Statistic_H0_Employees_For_ContractType", null);
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

        public DataTable StatisticEducationLevel()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure("Statistic_H0_Employees_For_EducationLevel", null);
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


        public DataTable GetListEducationLevel(int rootId, int educationLevelId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@RootId", SqlDbType.Int),
                    new SqlParameter("@EducationLevelId", SqlDbType.Int)
                };

                param[0].Value = rootId;
                param[1].Value = educationLevelId;

                sproc = new StoreProcedure("Statistic_H0_Employees_For_EducationLevelByRootEducationLevelId", param);
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

        public DataTable StatisticSeniority(int RootId, string Operator, int CountYear, int TypeCompare)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@RootId", SqlDbType.Int),
                    new SqlParameter("@Operator", SqlDbType.VarChar, 10),
                    new SqlParameter("@CountYear", SqlDbType.Int),
                    new SqlParameter("@TypeCompare", SqlDbType.Int)
                };

                param[0].Value = RootId;
                param[1].Value = Operator;
                param[2].Value = CountYear;
                param[3].Value = TypeCompare;

                sproc = new StoreProcedure("Statistic_H0_Employees_For_Seniority", param);
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

        public DataTable StatisticHumanResource(DateTime FromDateA, DateTime ToDateA, DateTime FromDateB,
            DateTime ToDateB)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FromDateA", SqlDbType.DateTime),
                    new SqlParameter("@ToDateA", SqlDbType.DateTime),
                    new SqlParameter("@FromDateB", SqlDbType.DateTime),
                    new SqlParameter("@ToDateB", SqlDbType.DateTime)
                };

                param[0].Value = FromDateA;
                param[1].Value = ToDateA;
                param[2].Value = FromDateB;
                param[3].Value = ToDateB;

                sproc = new StoreProcedure("Statistic_HumanResource", param);
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

        public DataTable StatisticTotalEmployees(DateTime FromDate, DateTime ToDate, int IsStatistic)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FromDate", SqlDbType.DateTime),
                    new SqlParameter("@ToDate", SqlDbType.DateTime),
                    new SqlParameter("@IsStatistic", SqlDbType.Int)
                };

                param[0].Value = FromDate;
                param[1].Value = ToDate;
                param[2].Value = IsStatistic;

                sproc = new StoreProcedure("Statistic_H0_Employees_For_TotalEmployees", param);
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