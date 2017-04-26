using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;

namespace HRMDAL.H1
{
    public class LNSEmployeeCoefficientDAL : Dao
    {
        public DataTable Get_ById(int UserId, int LNSCoefficientEmployeesId, int Active)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@LNSCoefficientEmployeesId", SqlDbType.Int),
                    new SqlParameter("@Active", SqlDbType.Int)
                };

                param[0].Value = UserId;
                param[1].Value = LNSCoefficientEmployeesId;
                param[2].Value = Active;

                sproc = new StoreProcedure("Sel_H1_LNSEmployeeCoefficient_By_Id", param);
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

        public DataTable Get_ByFilter(int UserId, int LNSCoefficientEmployeesId, string DepartmentIds)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@LNSCoefficientEmployeesId", SqlDbType.Int),
                    new SqlParameter("@DepartmentIds", SqlDbType.NVarChar, 4000)
                };

                param[0].Value = UserId;
                param[1].Value = LNSCoefficientEmployeesId;
                param[2].Value = DepartmentIds;

                sproc = new StoreProcedure("Sel_H1_LNSEmployeeCoefficient_By_Filter", param);
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

        public long Save(int LNSCoefficientEmployeesId,
            int UserId,
            int ScaleOfSalaryId,
            int CoefficientLevel,
            double Ratio,
            double TheoreticalValue,
            double ActualValue,
            DateTime FromDate,
            DateTime ToDate,
            DateTime CreatedDate,
            int Active,
            string Remark)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@LNSCoefficientEmployeesId", SqlDbType.Int),
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@ScaleOfSalaryId", SqlDbType.Int),
                    new SqlParameter("@CoefficientLevel", SqlDbType.Int),
                    new SqlParameter("@Ratio", SqlDbType.Float),
                    new SqlParameter("@TheoreticalValue", SqlDbType.Float),
                    new SqlParameter("@ActualValue", SqlDbType.Float),
                    new SqlParameter("@FromDate", SqlDbType.DateTime),
                    new SqlParameter("@ToDate", SqlDbType.DateTime),
                    new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@Active", SqlDbType.Int),
                    new SqlParameter("@Remark", SqlDbType.NText)
                };

                param[0].Value = LNSCoefficientEmployeesId;
                param[1].Value = UserId;
                param[2].Value = ScaleOfSalaryId;
                param[3].Value = CoefficientLevel;
                param[4].Value = Ratio;
                param[5].Value = TheoreticalValue;
                param[6].Value = ActualValue;
                param[7].Value = FromDate;
                param[8].Value = ToDate;
                param[9].Value = CreatedDate;
                param[10].Value = Active;
                param[11].Value = Remark;

                sproc = new StoreProcedure("Save_H1_LNS_CoefficientEmployees", param);
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
    }
}