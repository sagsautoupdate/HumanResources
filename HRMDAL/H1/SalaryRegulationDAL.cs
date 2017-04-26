using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H1;

namespace HRMDAL.H1
{
    public class SalaryRegulationDAL : Dao
    {
        #region insert, update, delete

        public int Insert(string SalaryRegulationName, DateTime? BeginingDate, string Description, bool? InUse,
            int? TypeId)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@SalaryRegulationName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@BeginingDate", SqlDbType.DateTime, 8),
                    new SqlParameter("@Description", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@InUse", SqlDbType.Bit, 1),
                    new SqlParameter("@TypeId", SqlDbType.Int, 4)
                };
                if (SalaryRegulationName == null)
                    param[0].Value = DBNull.Value;
                else
                    param[0].Value = SalaryRegulationName;

                if (BeginingDate.HasValue)
                    param[1].Value = BeginingDate.Value;
                else
                    param[1].Value = DBNull.Value;
                if (Description == null)
                    param[2].Value = DBNull.Value;
                else
                    param[2].Value = Description;
                if (InUse.HasValue)
                    param[3].Value = InUse.Value;
                else
                    param[3].Value = DBNull.Value;
                if (TypeId.HasValue)
                    param[4].Value = TypeId.Value;
                else
                    param[4].Value = DBNull.Value;
                sproc = new StoreProcedure(SalaryRegulationKeys.Sp_Ins_H1_SalaryRegulation, param);
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

        public int Update(string SalaryRegulationName, DateTime? BeginingDate, string Description, bool? InUse,
            int? TypeId, int? SalaryRegulationId)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@SalaryRegulationName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@BeginingDate", SqlDbType.DateTime, 8),
                    new SqlParameter("@Description", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@InUse", SqlDbType.Bit, 1),
                    new SqlParameter("@TypeId", SqlDbType.Int, 4),
                    new SqlParameter("@SalaryRegulationId", SqlDbType.Int, 4)
                };
                if (SalaryRegulationName == null)
                    param[0].Value = DBNull.Value;
                else
                    param[0].Value = SalaryRegulationName;

                if (BeginingDate.HasValue)
                    param[1].Value = BeginingDate.Value;
                else
                    param[1].Value = DBNull.Value;
                if (Description == null)
                    param[2].Value = DBNull.Value;
                else
                    param[2].Value = Description;
                if (InUse.HasValue)
                    param[3].Value = InUse.Value;
                else
                    param[3].Value = DBNull.Value;
                if (TypeId.HasValue)
                    param[4].Value = TypeId.Value;
                else
                    param[4].Value = DBNull.Value;
                if (SalaryRegulationId.HasValue)
                    param[5].Value = SalaryRegulationId.Value;
                else
                    param[5].Value = DBNull.Value;
                sproc = new StoreProcedure(SalaryRegulationKeys.Sp_Upd_H1_SalaryRegulation, param);
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

        public int Delete(int? SalaryRegulationId)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@SalaryRegulationId", SqlDbType.Int, 4)
                };

                if (SalaryRegulationId.HasValue)
                    param[0].Value = SalaryRegulationId.Value;
                else
                    param[0].Value = DBNull.Value;

                sproc = new StoreProcedure(SalaryRegulationKeys.Sp_Del_H1_SalaryRegulation, param);
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

        public DataTable GetByInUse(bool? InUse)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@InUse", SqlDbType.Int, 4)
                };

                if (InUse.HasValue)
                    param[0].Value = InUse.Value;
                else
                    param[0].Value = DBNull.Value;

                sproc = new StoreProcedure(SalaryRegulationKeys.Sp_Sel_H1_SalaryRegulationByInUse, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByFilter(int? TypeId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@TypeId", SqlDbType.Int, 4)
                };

                if (TypeId.HasValue)
                    param[0].Value = TypeId.Value;
                else
                    param[0].Value = DBNull.Value;

                sproc = new StoreProcedure(SalaryRegulationKeys.Sp_Sel_H1_SalaryRegulationByFilter, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByFilterV1(int? TypeId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@TypeId", SqlDbType.Int, 4)
                };

                if (TypeId.HasValue)
                    param[0].Value = TypeId.Value;
                else
                    param[0].Value = DBNull.Value;

                sproc = new StoreProcedure(SalaryRegulationKeys.Sp_Sel_H1_SalaryRegulationByFilterV1, param);
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