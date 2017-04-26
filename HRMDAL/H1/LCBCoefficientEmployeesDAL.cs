using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H1;

namespace HRMDAL.H1
{
    public class LCBCoefficientEmployeesDAL : Dao
    {
        #region insert, update, delete

        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns>int</returns>
        public long Insert(int EmployeeContractId, int UserId, int CoefficientValueId, double LCBwages, int LCBUnit,
            DateTime FromDate, DateTime ToDate, string Description, double PCDH, double PCTN, double PCCV, double PCKV)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@EmployeeContractId", SqlDbType.Int),
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@CoefficientValueId", SqlDbType.Int),
                    new SqlParameter("@LCBWages", SqlDbType.Float),
                    new SqlParameter("@LCBUnit", SqlDbType.Int),
                    new SqlParameter("@FromDate", SqlDbType.DateTime),
                    new SqlParameter("@ToDate", SqlDbType.DateTime),
                    new SqlParameter("@LCB_CoefficientEmployeeDescription", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@PCDH", SqlDbType.Float),
                    new SqlParameter("@PCTN", SqlDbType.Float),
                    new SqlParameter("@PCCV", SqlDbType.Float),
                    new SqlParameter("@PCKV", SqlDbType.Float)
                };
                param[0].Value = EmployeeContractId;
                param[1].Value = UserId;
                param[2].Value = CoefficientValueId;
                param[3].Value = LCBwages;
                param[4].Value = LCBUnit;
                param[5].Value = FromDate;
                param[6].Value = ToDate;
                param[7].Value = Description;
                param[8].Value = PCDH;
                param[9].Value = PCTN;
                param[10].Value = PCCV;
                param[11].Value = PCKV;

                sproc = new StoreProcedure(LCB_CoefficientEmployeeKeys.Sp_Ins_H1_LCB_CoefficientEmployee, param);
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

        public int Update(int LCB_CoefficientEmployeeId, int UserId, int EmployeeContractId, int CoefficientValueId,
            double LCBwages, int LCBUnit, DateTime FromDate, DateTime ToDate, string Description, bool Active,
            double PCDH, double PCTN, double PCCV, double PCKV)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@EmployeeContractId", SqlDbType.Int),
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@CoefficientValueId", SqlDbType.Int),
                    new SqlParameter("@LCBWages", SqlDbType.Float),
                    new SqlParameter("@LCBUnit", SqlDbType.Int),
                    new SqlParameter("@FromDate", SqlDbType.DateTime),
                    new SqlParameter("@ToDate", SqlDbType.DateTime),
                    new SqlParameter("@LCB_CoefficientEmployeeDescription", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@Active", SqlDbType.Bit),
                    new SqlParameter("@PCDH", SqlDbType.Float),
                    new SqlParameter("@PCTN", SqlDbType.Float),
                    new SqlParameter("@PCCV", SqlDbType.Float),
                    new SqlParameter("@PCKV", SqlDbType.Float),
                    new SqlParameter("@LCB_CoefficientEmployeeId", SqlDbType.Int)
                };
                param[0].Value = EmployeeContractId;
                param[1].Value = UserId;
                param[2].Value = CoefficientValueId;
                param[3].Value = LCBwages;
                param[4].Value = LCBUnit;
                param[5].Value = FromDate;
                param[6].Value = ToDate;
                param[7].Value = Description;
                param[8].Value = Active;
                param[9].Value = PCDH;
                param[10].Value = PCTN;
                param[11].Value = PCCV;
                param[12].Value = PCKV;
                param[13].Value = LCB_CoefficientEmployeeId;

                sproc = new StoreProcedure(LCB_CoefficientEmployeeKeys.Sp_Upd_H1_LCB_CoefficientEmployee, param);
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

        public int Delete(int lCB_CoefficientEmployeeId)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@LCB_CoefficientEmployeeId", SqlDbType.Int)
                };

                param[0].Value = lCB_CoefficientEmployeeId;

                sproc = new StoreProcedure(LCB_CoefficientEmployeeKeys.SP_LCB_COEFFICIENT_EMPLOYEE_DELETE, param);

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

        public int DeleteByEmployeeContractId(int EmployeeContractId)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@EmployeeContractId", SqlDbType.Int)
                };

                param[0].Value = EmployeeContractId;

                sproc =
                    new StoreProcedure(
                        LCB_CoefficientEmployeeKeys.Sp_Del_H1_LCB_CoefficientEmployeeByEmployeeContractId, param);

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

        public DataTable GetByUserId(int userId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                param[0].Value = userId;

                sproc = new StoreProcedure(LCB_CoefficientEmployeeKeys.Sp_Sel_H1_LCB_CoefficientEmployee_By_UserId,
                    param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByEmployeeContractId(int EmployeeContractId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("EmployeeContractId", SqlDbType.Int)
                };

                param[0].Value = EmployeeContractId;

                sproc =
                    new StoreProcedure(
                        LCB_CoefficientEmployeeKeys.Sp_Sel_H1_LCB_CoefficientEmployee_By_EmployeeContractId, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByUserIdForNew(int UserId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                param[0].Value = UserId;

                sproc = new StoreProcedure(
                    LCB_CoefficientEmployeeKeys.Sp_Sel_H1_LCB_CoefficientEmployee_By_UserIdForNew, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        //public DataTable GetCurrentByUserIdDate(int userId, DateTime fromDate)
        //{

        //    Debug.Assert(sproc == null);
        //    DataTable datatable = new DataTable();
        //    try
        //    {
        //        SqlParameter[] param = { 
        //            new SqlParameter("@UserId", SqlDbType.Int),
        //            new SqlParameter("@FromDate", SqlDbType.DateTime)
        //        };

        //        param[0].Value = userId;
        //        param[1].Value = fromDate;

        //        sproc = new StoreProcedure(LCB_CoefficientEmployeeKeys.Sp_Sel_H1_LCB_CoefficientEmployee_By_UserId_Date, param);
        //        sproc.RunFill(datatable);
        //        sproc.Dispose();
        //    }
        //    catch (SqlException se)
        //    {
        //        throw new HRMException(se.Message, se.Number);
        //    }

        //    return datatable;

        //}

        //public DataTable GetByUserIdToDateContractType(int userId, DateTime toDate, int contractTypeId)
        //{

        //    Debug.Assert(sproc == null);
        //    DataTable datatable = new DataTable();
        //    try
        //    {
        //        SqlParameter[] param = { 
        //            new SqlParameter("@UserId", SqlDbType.Int),
        //            new SqlParameter("@ToDate", SqlDbType.DateTime),
        //            new SqlParameter("@ContractTypeId", SqlDbType.Int)
        //        };

        //        param[0].Value = userId;
        //        param[1].Value = toDate;
        //        param[2].Value = contractTypeId;

        //        sproc = new StoreProcedure(LCB_CoefficientEmployeeKeys.Sel_H1_LCB_CoefficientEmployee_By_UserId_ToDate_ContractTypeId, param);
        //        sproc.RunFill(datatable);
        //        sproc.Dispose();
        //    }
        //    catch (SqlException se)
        //    {
        //        throw new HRMException(se.Message, se.Number);
        //    }

        //    return datatable;

        //}

        public DataTable RemindLCBCoefficient(string fullName, int deptId, int day, int month, int year)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@DepartmentId", SqlDbType.Int),
                    new SqlParameter("@Day", SqlDbType.Int),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int)
                };

                param[0].Value = fullName;
                param[1].Value = deptId;
                param[2].Value = day;
                param[3].Value = month;
                param[4].Value = year;

                sproc = new StoreProcedure(LCB_CoefficientEmployeeKeys.Sp_Sel_H1_LCB_CoefficientEmployee_By_Remind,
                    param);
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

        public DataTable ChangedLCBCoefficient(string fullName, int deptId, int day, int month, int year)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@DepartmentId", SqlDbType.Int),
                    new SqlParameter("@Day", SqlDbType.Int),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int)
                };

                param[0].Value = fullName;
                param[1].Value = deptId;
                param[2].Value = day;
                param[3].Value = month;
                param[4].Value = year;

                sproc = new StoreProcedure(LCB_CoefficientEmployeeKeys.Sp_Sel_H1_LCB_CoefficientEmployee_By_Changed,
                    param);
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

        public DataTable GetByUserIdFromToDateFinal(int UserId, DateTime FromDate, DateTime ToDate, int XQD)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@FromDate", SqlDbType.DateTime),
                    new SqlParameter("@ToDate", SqlDbType.DateTime),
                    new SqlParameter("@XQD", SqlDbType.Int)
                };

                param[0].Value = UserId;
                param[1].Value = FromDate;
                param[2].Value = ToDate;
                param[3].Value = XQD;

                sproc =
                    new StoreProcedure(
                        LCB_CoefficientEmployeeKeys.Sp_Sel_H1_LCB_CoefficientEmployee_By_UserId_FromToDateFinal, param);
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