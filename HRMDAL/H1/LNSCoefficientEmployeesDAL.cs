using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H1;

namespace HRMDAL.H1
{
    public class LNSCoefficientEmployeesDAL : Dao
    {
        #region insert, update, delete

        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns>int</returns>
        public long Insert(int employeeContractId, int userId, int coefficientValueId, double pCTN, double lNSWage,
            int lNSUnit, string description, DateTime createDate)
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
                    new SqlParameter("@PCTN", SqlDbType.Float),
                    new SqlParameter("@LNSWages", SqlDbType.Float),
                    new SqlParameter("@LNSUnit", SqlDbType.Int),
                    new SqlParameter("@LNS_CoefficientEmployeeDescription", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime)
                };
                param[0].Value = employeeContractId;
                param[1].Value = userId;
                param[2].Value = coefficientValueId;
                param[3].Value = pCTN;
                param[4].Value = lNSWage;
                param[5].Value = lNSUnit;
                param[6].Value = description;
                param[7].Value = createDate;

                sproc = new StoreProcedure(LNS_CoefficientEmployeeKeys.Sp_Ins_H1_LNS_CoefficientEmployee, param);
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

        public int Update(int lNS_CoefficientEmployeeId, int userId, int employeeContractId, int coefficientValueId,
            double pCTN, double lNSWage, int lNSUnit, string description, DateTime createDate, bool active)
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
                    new SqlParameter("@PCTN", SqlDbType.Float),
                    new SqlParameter("@LNSWages", SqlDbType.Float),
                    new SqlParameter("@LNSUnit", SqlDbType.Int),
                    new SqlParameter("@LNS_CoefficientEmployeeDescription", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime),
                    new SqlParameter("@LNS_CoefficientEmployeeId", SqlDbType.Int),
                    new SqlParameter("@Active", SqlDbType.Bit)
                };
                param[0].Value = employeeContractId;
                param[1].Value = userId;
                param[2].Value = coefficientValueId;
                param[3].Value = pCTN;
                param[4].Value = lNSWage;
                param[5].Value = lNSUnit;
                param[6].Value = description;
                param[7].Value = createDate;
                param[8].Value = lNS_CoefficientEmployeeId;
                param[9].Value = active;

                sproc = new StoreProcedure(LNS_CoefficientEmployeeKeys.Sp_Upd_H1_LNS_CoefficientEmployee, param);
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

        public int Delete(int lNS_CoefficientEmployeeId)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@LNS_CoefficientEmployeeId", SqlDbType.Int)
                };

                param[0].Value = lNS_CoefficientEmployeeId;

                sproc = new StoreProcedure(LNS_CoefficientEmployeeKeys.SP_LNS_COEFFICIENT_EMPLOYEE_DELETE, param);

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
                        LNS_CoefficientEmployeeKeys.Sp_Del_H1_LNS_CoefficientEmployeeByEmployeeContractId, param);

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

        public DataTable GetByUserId(int UserId, int InUse)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@InUse", SqlDbType.Int)
                };

                param[0].Value = UserId;
                param[1].Value = InUse;

                sproc = new StoreProcedure(LNS_CoefficientEmployeeKeys.SP_Sel_H1_LNS_CoefficientEmployee_By_UserId,
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
                    new SqlParameter("@EmployeeContractId", SqlDbType.Int)
                };

                param[0].Value = EmployeeContractId;

                sproc =
                    new StoreProcedure(
                        LNS_CoefficientEmployeeKeys.Sp_Sel_H1_LNS_CoefficientEmployee_By_EmployeeContractId, param);
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
                    LNS_CoefficientEmployeeKeys.Sp_Sel_H1_LNS_CoefficientEmployee_By_UserIdForNew, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByUserIdDate(int userId, DateTime createDate, int XQD)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime),
                    new SqlParameter("@XQD", SqlDbType.Int)
                };

                param[0].Value = userId;
                param[1].Value = createDate;
                param[2].Value = XQD;

                sproc = new StoreProcedure(
                    LNS_CoefficientEmployeeKeys.Sp_Sel_H1_LNS_CoefficientEmployee_By_UserId_Date, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        //public DataTable GetTotalByDate(DateTime createDate, int XQD, bool ApportionmentType)
        //{

        //    Debug.Assert(sproc == null);
        //    DataTable datatable = new DataTable();
        //    try
        //    {
        //        SqlParameter[] param = { 

        //            new SqlParameter("@CreateDate", SqlDbType.SmallDateTime),
        //            new SqlParameter("@XQD", SqlDbType.Int),
        //            new SqlParameter("@ApportionmentType", SqlDbType.Bit)
        //        };

        //        param[0].Value = createDate;
        //        param[1].Value = XQD;
        //        param[2].Value = ApportionmentType;

        //        sproc = new StoreProcedure(LNS_CoefficientEmployeeKeys.Sp_Sel_H1_LNS_CoefficientEmployee_Total_By_Date, param);
        //        sproc.RunFill(datatable);
        //        sproc.Dispose();
        //    }
        //    catch (SqlException se)
        //    {
        //        throw new HRMException(se.Message, se.Number);
        //    }

        //    return datatable;

        //}

        #endregion
    }
}