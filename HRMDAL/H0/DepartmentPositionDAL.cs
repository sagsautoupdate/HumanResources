using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H0;

namespace HRMDAL.H0
{
    public class DepartmentPositionDAL : Dao
    {
        #region Methods Get

        public DataTable GetByFilter(int DepartmentId, int PositionId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DepartmentId", SqlDbType.Int, 4),
                    new SqlParameter("@PositionId", SqlDbType.Int, 4)
                };

                param[0].Value = DepartmentId;
                param[1].Value = PositionId;

                sproc = new StoreProcedure(DepartmentPositionKeys.Sp_Sel_H0_DepartmentPositionByFilter, param);
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

        #region methods inset, update , delete

        public long Insert(int DepartmentId, int PositionId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DepartmentId", SqlDbType.Int, 4),
                    new SqlParameter("@PositionId", SqlDbType.Int, 4)
                };

                param[0].Value = DepartmentId;
                param[1].Value = PositionId;

                sproc = new StoreProcedure(DepartmentPositionKeys.Sp_Ins_H0_DepartmentPosition, param);
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

        public long Update(int DepartmentId, int PositionId, int DeptPositionId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DepartmentId", SqlDbType.Int, 4),
                    new SqlParameter("@PositionId", SqlDbType.Int, 4),
                    new SqlParameter("@DeptPositionId", SqlDbType.Int, 4)
                };

                param[0].Value = DepartmentId;
                param[1].Value = PositionId;
                param[2].Value = DeptPositionId;

                sproc = new StoreProcedure(DepartmentPositionKeys.Sp_Upd_H0_DepartmentPosition, param);
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

        public long Delete(int DeptPositionId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DeptPositionId", SqlDbType.Int)
                };

                param[0].Value = DeptPositionId;

                sproc = new StoreProcedure(DepartmentPositionKeys.Sp_Del_H0_DepartmentPositionById, param);
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
    }
}