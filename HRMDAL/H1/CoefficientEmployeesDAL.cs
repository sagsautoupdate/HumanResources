using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H1;

namespace HRMDAL.H1
{
    public class CoefficientEmployeesDAL : Dao
    {
        #region Insert, Update, Delete

        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns>int</returns>
        public long Insert(int userId, double pCDH, double pCTN, double pCCV, double pCKV,
            string description, bool active, DateTime createDate)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@PCDH", SqlDbType.Float),
                    new SqlParameter("@PCTN", SqlDbType.Float),
                    new SqlParameter("@PCCV", SqlDbType.Float),
                    new SqlParameter("@PCKV", SqlDbType.Float),
                    new SqlParameter("@Description", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@Active", SqlDbType.Bit),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime)
                };
                param[0].Value = userId;
                param[1].Value = pCDH;
                param[2].Value = pCTN;
                param[3].Value = pCCV;
                param[4].Value = pCKV;
                param[5].Value = description;
                param[6].Value = active;
                param[7].Value = createDate;

                sproc = new StoreProcedure(CoefficientEmployeeKeys.SP_COEFFICIENT_EMPLOYEE_INSERT, param);
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

        /// <summary>
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="k"></param>
        /// <param name="lCB"></param>
        /// <param name="lNS"></param>
        /// <param name="pCDH"></param>
        /// <param name="pCTN"></param>
        /// <param name="pCCV"></param>
        /// <param name="pCKV"></param>
        /// <param name="description"></param>
        /// <param name="active"></param>
        /// <param name="lOCK"></param>
        /// <param name="createDate"></param>
        /// <returns></returns>
        public long Update(int userId, double pCDH, double pCTN, double pCCV, double pCKV,
            string description, bool active, DateTime createDate, int coefficientEmployeeId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@PCDH", SqlDbType.Float),
                    new SqlParameter("@PCTN", SqlDbType.Float),
                    new SqlParameter("@PCCV", SqlDbType.Float),
                    new SqlParameter("@PCKV", SqlDbType.Float),
                    new SqlParameter("@Description", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@Active", SqlDbType.Bit),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime),
                    new SqlParameter("@CoefficientEmployeeId", SqlDbType.Int)
                };

                param[0].Value = userId;
                param[1].Value = pCDH;
                param[2].Value = pCTN;
                param[3].Value = pCCV;
                param[4].Value = pCKV;
                param[5].Value = description;
                param[6].Value = active;
                param[7].Value = createDate;
                param[8].Value = coefficientEmployeeId;

                sproc = new StoreProcedure(CoefficientEmployeeKeys.SP_COEFFICIENT_EMPLOYEE_UPDATE, param);
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


        public int Delete(int coefficientEmployeeId)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@CoefficientEmployeeId", SqlDbType.Int)
                };

                param[0].Value = coefficientEmployeeId;

                sproc = new StoreProcedure(CoefficientEmployeeKeys.SP_COEFFICIENT_EMPLOYEE_DELETE, param);
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

        #region Get

        public DataTable GetByUserId(int userId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            ;
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int)
                };
                param[0].Value = userId;

                sproc = new StoreProcedure(CoefficientEmployeeKeys.Sp_Sel_H1_CoefficientEmployee_By_UserId, param);
                sproc.RunFill(dt);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetByUserIdDate(int userId, DateTime createDate)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            ;
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime)
                };

                param[0].Value = userId;
                param[1].Value = createDate;

                sproc = new StoreProcedure(CoefficientEmployeeKeys.Sp_Sel_H1_CoefficientEmployee_By_UserId_Date, param);
                sproc.RunFill(dt);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetByUserIdDateFinal(int userId, DateTime createDate, int XQD)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            ;
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

                sproc = new StoreProcedure(CoefficientEmployeeKeys.Sp_Sel_H1_CoefficientEmployee_By_UserId_DateFinal,
                    param);
                sproc.RunFill(dt);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }


        public DataTable GetByUserIdForNew(int UserId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            ;
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4)
                };

                param[0].Value = UserId;

                sproc = new StoreProcedure(CoefficientEmployeeKeys.Sp_Sel_H1_CoefficientEmployee_By_UserIdForNew, param);
                sproc.RunFill(dt);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            return dt;
        }

        //public DataTable GetCurrentByUserId(int userId)
        //{

        //    Debug.Assert(sproc == null);

        //    DataTable dt = new DataTable(); ;
        //    try
        //    {
        //        SqlParameter[] param = 
        //        {
        //            new SqlParameter("@UserId",SqlDbType.Int)                    
        //        };
        //        param[0].Value = userId;

        //        sproc = new StoreProcedure(CoefficientEmployeeKeys.SP_COEFFICIENT_EMPLOYEE_ACTIVE_BY_USERID, param);
        //        sproc.RunFill(dt);
        //        sproc.Dispose();
        //    }
        //    catch (SqlException se)
        //    {
        //        throw new HRMException(se.Message, se.Number);
        //    }

        //    return dt;

        //}


        //public DataTable AllCoefficientEmployeeGetByFilter(string fullName, int rootId, string LNSOperator, int LNSWages, string LCBOperator, int LCBWages)
        //{

        //    Debug.Assert(sproc == null);
        //    DataTable dt = new DataTable(); ;
        //    try
        //    {
        //        SqlParameter[] param = 
        //        {
        //            new SqlParameter("@FullName",SqlDbType.NVarChar, 100),
        //            new SqlParameter("@RootId",SqlDbType.Int),
        //            new SqlParameter("@LNSOperator",SqlDbType.VarChar, 2),
        //            new SqlParameter("@LNSWages",SqlDbType.Int),
        //            new SqlParameter("@LCBOperator",SqlDbType.VarChar, 2),
        //            new SqlParameter("@LCBWages",SqlDbType.Int)
        //        };

        //        param[0].Value = fullName;
        //        param[1].Value = rootId;
        //        param[2].Value = LNSOperator;
        //        param[3].Value = LNSWages;
        //        param[4].Value = LCBOperator;
        //        param[5].Value = LCBWages;

        //        sproc = new StoreProcedure(CoefficientEmployeeKeys.Sp_Sel_H1_CoefficientEmployeeAll_By_Filter, param);
        //        sproc.RunFill(dt);
        //        sproc.Dispose();
        //    }
        //    catch (SqlException se)
        //    {
        //        throw new HRMException(se.Message, se.Number);
        //    }

        //    return dt;
        //}

        //public DataTable AllCoefficientEmployeeGetTotalByFilter(string fullName, int rootId, string LNSOperator, int LNSWages, string LCBOperator, int LCBWages)
        //{

        //    Debug.Assert(sproc == null);
        //    DataTable dt = new DataTable(); ;
        //    try
        //    {
        //        SqlParameter[] param = 
        //        {
        //            new SqlParameter("@FullName",SqlDbType.NVarChar, 100),
        //            new SqlParameter("@RootId",SqlDbType.Int),
        //            new SqlParameter("@LNSOperator",SqlDbType.VarChar, 2),
        //            new SqlParameter("@LNSWages",SqlDbType.Int),
        //            new SqlParameter("@LCBOperator",SqlDbType.VarChar, 2),
        //            new SqlParameter("@LCBWages",SqlDbType.Int)
        //        };

        //        param[0].Value = fullName;
        //        param[1].Value = rootId;
        //        param[2].Value = LNSOperator;
        //        param[3].Value = LNSWages;
        //        param[4].Value = LCBOperator;
        //        param[5].Value = LCBWages;

        //        sproc = new StoreProcedure(CoefficientEmployeeKeys.Sp_Sel_H1_CoefficientEmployeeAllTotal_By_Filter, param);
        //        sproc.RunFill(dt);
        //        sproc.Dispose();
        //    }
        //    catch (SqlException se)
        //    {
        //        throw new HRMException(se.Message, se.Number);
        //    }

        //    return dt;
        //}

        //public DataTable AllCoefficientEmployeeGetByUserId(int userId)
        //{

        //    Debug.Assert(sproc == null);
        //    DataTable dt = new DataTable(); ;
        //    try
        //    {
        //        SqlParameter[] param = 
        //        {
        //            new SqlParameter("@UserId",SqlDbType.Int)
        //        };

        //        param[0].Value = userId;

        //        sproc = new StoreProcedure(CoefficientEmployeeKeys.Sp_Sel_H1_CoefficientEmployeeAll_Active_By_UserId, param);
        //        sproc.RunFill(dt);
        //        sproc.Dispose();
        //    }
        //    catch (SqlException se)
        //    {
        //        throw new HRMException(se.Message, se.Number);
        //    }

        //    return dt;
        //}

        #endregion
    }
}