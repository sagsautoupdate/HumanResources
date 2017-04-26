using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;

namespace HRMDAL.H2
{
    public class EducationFeeDAL : Dao
    {
        public DataTable GetAll()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure("Sel_H2_EducationFee", null);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetById(int FeeId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FeeId", SqlDbType.Int)
                };

                param[0].Value = FeeId;

                sproc = new StoreProcedure("Sel_H2_EducationFee_ById", param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetBySessionIdPositionId(int SessionId, int PositionId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@SessionId", SqlDbType.Int),
                    new SqlParameter("@PositionId", SqlDbType.Int)
                };

                param[0].Value = SessionId;
                param[1].Value = PositionId;

                sproc = new StoreProcedure("Sel_H2_EducationFee_BySessionIdPositionId", param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetBySessionId(int SessionId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@SessionId", SqlDbType.Int)
                };

                param[0].Value = SessionId;

                sproc = new StoreProcedure("Sel_H2_EducationFee_BySessionId", param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public long Insert(int SessionId, int PositionId, string Fee, string FeeInVietNamese, int CreatedBy)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@SessionId", SqlDbType.Int),
                    new SqlParameter("@PositionId", SqlDbType.Int),
                    new SqlParameter("@Fee", SqlDbType.NVarChar, 4000),
                    new SqlParameter("@FeeInVietNamese", SqlDbType.NVarChar, 4000),
                    new SqlParameter("@CreatedBy", SqlDbType.Int)
                };

                param[0].Value = SessionId;
                param[1].Value = PositionId;
                param[2].Value = Fee;
                param[3].Value = FeeInVietNamese;
                param[4].Value = CreatedBy;

                sproc = new StoreProcedure("Ins_H2_EducationFee", param);
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

        public long Update(int SessionId, int PositionId, string Fee, string FeeInVietNamese, int CreatedBy, int FeeId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@SessionId", SqlDbType.Int),
                    new SqlParameter("@PositionId", SqlDbType.Int),
                    new SqlParameter("@Fee", SqlDbType.NVarChar, 4000),
                    new SqlParameter("@FeeInVietNamese", SqlDbType.NVarChar, 4000),
                    new SqlParameter("@CreatedBy", SqlDbType.Int),
                    new SqlParameter("@FeeId", SqlDbType.Int)
                };

                param[0].Value = SessionId;
                param[1].Value = PositionId;
                param[2].Value = Fee;
                param[3].Value = FeeInVietNamese;
                param[4].Value = CreatedBy;
                param[5].Value = FeeId;

                sproc = new StoreProcedure("Upd_H2_EducationFee", param);
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

        public long Delete(int FeeId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FeeId", SqlDbType.Int)
                };

                param[0].Value = FeeId;

                sproc = new StoreProcedure("Del_H2_EducationFee", param);
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
    }
}