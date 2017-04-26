using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H1;

namespace HRMDAL.H1
{
    public class HTCVEmployeeDAL : Dao
    {
        #region insert, update, delete

        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns>int</returns>
        public int Insert(int? UserId, int? HTCVCatalogueId, double? Mark, DateTime? MarkDate, string Remark)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@HTCVCatalogueId", SqlDbType.Int, 4),
                    new SqlParameter("@Mark", SqlDbType.Float, 8),
                    new SqlParameter("@MarkDate", SqlDbType.DateTime, 8),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 1000)
                };
                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (HTCVCatalogueId.HasValue)
                    param[1].Value = HTCVCatalogueId.Value;
                else
                    param[1].Value = DBNull.Value;
                if (Mark.HasValue)
                    param[2].Value = Mark.Value;
                else
                    param[2].Value = DBNull.Value;
                if (MarkDate.HasValue)
                    param[3].Value = MarkDate.Value;
                else
                    param[3].Value = DBNull.Value;
                if (Remark == null)
                    param[4].Value = DBNull.Value;
                else
                    param[4].Value = Remark;

                sproc = new StoreProcedure(HTCVEmployeeKeys.Sp_Ins_H1_HTCVEmployee, param);
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

        public int Update(int? UserId, int? HTCVCatalogueId, double? Mark, DateTime? MarkDate, string Remark,
            long? HTCVEmployeeId)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@HTCVCatalogueId", SqlDbType.Int, 4),
                    new SqlParameter("@Mark", SqlDbType.Float, 8),
                    new SqlParameter("@MarkDate", SqlDbType.DateTime, 8),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@HTCVEmployeeId", SqlDbType.BigInt, 8)
                };

                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (HTCVCatalogueId.HasValue)
                    param[1].Value = HTCVCatalogueId.Value;
                else
                    param[1].Value = DBNull.Value;
                if (Mark.HasValue)
                    param[2].Value = Mark.Value;
                else
                    param[2].Value = DBNull.Value;
                if (MarkDate.HasValue)
                    param[3].Value = MarkDate.Value;
                else
                    param[3].Value = DBNull.Value;
                if (Remark == null)
                    param[4].Value = DBNull.Value;
                else
                    param[4].Value = Remark;
                if (HTCVEmployeeId.HasValue)
                    param[5].Value = HTCVEmployeeId.Value;
                else
                    param[5].Value = DBNull.Value;

                sproc = new StoreProcedure(HTCVEmployeeKeys.Sp_Upd_H1_HTCVEmployee, param);
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

        public int Delete(int? HTCVEmployeeId)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@HTCVEmployeeId", SqlDbType.Int, 4)
                };

                if (HTCVEmployeeId.HasValue)
                    param[0].Value = HTCVEmployeeId.Value;
                else
                    param[0].Value = DBNull.Value;

                sproc = new StoreProcedure(HTCVEmployeeKeys.Sp_Del_H1_HTCVEmployee, param);
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

        public int DeleteByIds(string HTCVEmployeeIds)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@HTCVEmployeeIds", SqlDbType.VarChar, 1000)
                };

                if (HTCVEmployeeIds == null)
                    param[0].Value = DBNull.Value;
                else
                    param[0].Value = HTCVEmployeeIds;

                sproc = new StoreProcedure(HTCVEmployeeKeys.Sp_Del_H1_HTCVEmployeeByIds, param);
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


        public int DeleteDate(int? UserId, DateTime? MarkDate)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@MarkDate", SqlDbType.DateTime, 8)
                };

                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (MarkDate.HasValue)
                    param[1].Value = MarkDate.Value;
                else
                    param[1].Value = DBNull.Value;
                sproc = new StoreProcedure(HTCVEmployeeKeys.Sp_Del_H1_HTCVEmployeeByUserDate, param);
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

        public DataTable GetByFilter(int? userid, int? HTCVCatalogueId, double? Mark, DateTime? Markdate)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@userid", SqlDbType.Int, 4),
                    new SqlParameter("@HTCVCatalogueId", SqlDbType.Int, 4),
                    new SqlParameter("@Mark", SqlDbType.Float, 8),
                    new SqlParameter("@Markdate", SqlDbType.DateTime, 8)
                };
                if (userid.HasValue)
                    param[0].Value = userid.Value;
                else
                    param[0].Value = DBNull.Value;
                if (HTCVCatalogueId.HasValue)
                    param[1].Value = HTCVCatalogueId.Value;
                else
                    param[1].Value = DBNull.Value;
                if (Mark.HasValue)
                    param[2].Value = Mark.Value;
                else
                    param[2].Value = DBNull.Value;
                if (Markdate.HasValue)
                    param[3].Value = Markdate.Value;
                else
                    param[3].Value = DBNull.Value;

                sproc = new StoreProcedure(HTCVEmployeeKeys.Sp_Sel_H1_HTCVEmployeeByFilter, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetForAllRemarkByUserIdDate(int? Userid, int? Month, int? Year)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Userid", SqlDbType.Int, 4),
                    new SqlParameter("@Month", SqlDbType.Int, 4),
                    new SqlParameter("@Year", SqlDbType.Int, 4)
                };
                if (Userid.HasValue)
                    param[0].Value = Userid.Value;
                else
                    param[0].Value = DBNull.Value;
                if (Month.HasValue)
                    param[1].Value = Month.Value;
                else
                    param[1].Value = DBNull.Value;
                if (Year.HasValue)
                    param[2].Value = (double) Year.Value;
                else
                    param[2].Value = DBNull.Value;


                sproc = new StoreProcedure(HTCVEmployeeKeys.Sp_Sel_H1_HTCVEmployeeForAllRemarkByUserIdDate, param);
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