using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H0;

namespace HRMDAL.H0
{
    public class EmployeeSubContractDAL : Dao
    {
        #region GET

        public DataTable GetAll()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure(EmployeeSubContractKeys.Sp_Sel_H1_ScaleOfSalaries_ByAll, null);
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

        public DataTable GetAllDistinct()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure("Sel_H0_SubContract_By_AllDistinct", null);
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

        public DataTable GetBySubContractId(int subContractId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@EmployeeContractId", SqlDbType.Int, 4)
                };
                param[0].Value = subContractId;
                sproc = new StoreProcedure(EmployeeSubContractKeys.Sp_Sel_H0_EmployeeSubContract_By_Id, param);
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

        public DataTable GetBySubContractUserId(int UserId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4)
                };
                param[0].Value = UserId;
                sproc = new StoreProcedure(EmployeeSubContractKeys.Sp_Sel_H0_EmployeeSubContract_By_UserId, param);
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

        public DataTable GetBySubContractUserIdByActive(string UserId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.NVarChar, 4000)
                };
                param[0].Value = UserId;
                sproc = new StoreProcedure(EmployeeSubContractKeys.Sp_Sel_H0_EmployeeSubContract_By_UserId_Active, param);
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

        public DataTable GetDetail(int userid, int subId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@SubId", SqlDbType.Int, 4)
                };
                param[0].Value = userid;
                param[1].Value = subId;
                sproc = new StoreProcedure("Sel_H0_SubContract_Detail", param);
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

        public DataTable GetDuration(int userid, int subId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@SubId", SqlDbType.Int, 4)
                };
                param[0].Value = userid;
                param[1].Value = subId;
                sproc = new StoreProcedure("Sel_H0_SubContract_Duration", param);
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

        #region INSERT, UPDATE, DELETE

        public long Insert(int EmployeeContractId, int UserId, DateTime CreatedDate, DateTime FromDate, DateTime ToDate,
            int PositionId, int ScaleOfSalaryId, int Value, string Detail, string Duration, int SubContractTypeId)
        {
            var num = 0L;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@EmployeeContractId", SqlDbType.Int),
                    new SqlParameter("@UserId", SqlDbType.Int), new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@FromDate", SqlDbType.DateTime), new SqlParameter("@ToDate", SqlDbType.DateTime),
                    new SqlParameter("@PositionId", SqlDbType.Int), new SqlParameter("@ScaleOfSalaryId", SqlDbType.Int),
                    new SqlParameter("@Value", SqlDbType.Int), new SqlParameter("@Detail", SqlDbType.NText),
                    new SqlParameter("@Duration", SqlDbType.NText),
                    new SqlParameter("@SubContractTypeId", SqlDbType.Int)
                };
                parameters[0].Value = EmployeeContractId;
                parameters[1].Value = UserId;
                parameters[2].Value = CreatedDate;
                parameters[3].Value = FromDate;
                parameters[4].Value = ToDate;
                parameters[5].Value = PositionId;
                parameters[6].Value = ScaleOfSalaryId;
                parameters[7].Value = Value;
                parameters[8].Value = Detail;
                parameters[9].Value = Duration;
                parameters[10].Value = SubContractTypeId;
                sproc = new StoreProcedure("Ins_H0_EmployeeSubContract", parameters);
                num = sproc.Run();
                sproc.Commit();
            }
            catch (SqlException exception)
            {
                sproc.RollBack();
                throw new HRMException(exception.Message, exception.Number);
            }
            finally
            {
                sproc.Dispose();
            }
            return num;
        }

        public long Update(int EmployeeSubContractId, int UserId, DateTime FromDate, DateTime ToDate, int PositionId,
            int ScaleOfSalaryId, int Value, string Detail, string Duration, int SubContractTypeId)
        {
            var num = 0L;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@EmployeeSubContractId", SqlDbType.Int),
                    new SqlParameter("@UserId", SqlDbType.Int), new SqlParameter("@FromDate", SqlDbType.DateTime),
                    new SqlParameter("@ToDate", SqlDbType.DateTime), new SqlParameter("@PositionId", SqlDbType.Int),
                    new SqlParameter("@ScaleOfSalaryId", SqlDbType.Int), new SqlParameter("@Value", SqlDbType.Int),
                    new SqlParameter("@Detail", SqlDbType.NText), new SqlParameter("@Duration", SqlDbType.NText),
                    new SqlParameter("@SubContractTypeId", SqlDbType.Int)
                };
                parameters[0].Value = EmployeeSubContractId;
                parameters[1].Value = UserId;
                parameters[2].Value = FromDate;
                parameters[3].Value = ToDate;
                parameters[4].Value = PositionId;
                parameters[5].Value = ScaleOfSalaryId;
                parameters[6].Value = Value;
                parameters[7].Value = Detail;
                parameters[8].Value = Duration;
                parameters[9].Value = SubContractTypeId;
                sproc = new StoreProcedure("Upd_H0_EmployeeSubContract_By_UserId", parameters);
                num = sproc.Run();
                sproc.Commit();
            }
            catch (SqlException exception)
            {
                sproc.RollBack();
                throw new HRMException(exception.Message, exception.Number);
            }
            finally
            {
                sproc.Dispose();
            }
            return num;
        }

        public long UpdateActive(int EmployeeSubContractId, bool Status)
        {
            var num = 0L;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@EmployeeSubContractId", SqlDbType.Int),
                    new SqlParameter("@Status", SqlDbType.Bit)
                };
                parameters[0].Value = EmployeeSubContractId;
                parameters[1].Value = Status;

                sproc = new StoreProcedure("Upd_H0_EmployeeSubContract_Active", parameters);
                num = sproc.Run();
                sproc.Commit();
            }
            catch (SqlException exception)
            {
                sproc.RollBack();
                throw new HRMException(exception.Message, exception.Number);
            }
            finally
            {
                sproc.Dispose();
            }
            return num;
        }

        #endregion
    }
}