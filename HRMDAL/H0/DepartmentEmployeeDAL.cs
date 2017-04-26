using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H0;

namespace HRMDAL.H0
{
    public class DepartmentEmployeeDAL : Dao
    {
        #region methods inset, update , delete

        public long Insert(int departmentId, int userId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DepartmentId", SqlDbType.Int),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                param[0].Value = departmentId;
                param[1].Value = userId;

                sproc = new StoreProcedure(DepartmentEmployeeKeys.SP_DEPARTMENT_EMPLOYEE_INSERT, param);
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

        public long Insert(int departmentId, string userIds)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DepartmentId", SqlDbType.Int),
                    new SqlParameter("@UserIds", SqlDbType.VarChar, 1000)
                };

                param[0].Value = departmentId;
                param[1].Value = userIds;

                sproc = new StoreProcedure(DepartmentEmployeeKeys.SP_DEPARTMENT_EMPLOYEE_INSERTS, param);
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

        public long Update(int departmentId, string departmentName)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DepartmentId", SqlDbType.Int),
                    new SqlParameter("@DepartmentName", SqlDbType.NVarChar, 254)
                };

                param[0].Value = departmentId;
                param[1].Value = departmentName;

                sproc = new StoreProcedure(DepartmentKeys.SP_DEPARTMENT_UPDATE, param);
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

        public long Delete(int departmentId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DepartmentId", SqlDbType.Int)
                };

                param[0].Value = departmentId;

                sproc = new StoreProcedure(DepartmentKeys.SP_DEPARTMENT_DELETE, param);
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

        public long DeleteDeptIdUserIds(int departmentId, string userIds)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DepartmentId", SqlDbType.Int),
                    new SqlParameter("@UserIds", SqlDbType.VarChar, 1000)
                };

                param[0].Value = departmentId;
                param[1].Value = userIds;

                sproc = new StoreProcedure(DepartmentEmployeeKeys.Sp_Del_H0_DepartmentEmployee, param);
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

        public long Update(int departmentId, int userId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DepartmentId", SqlDbType.Int),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                param[0].Value = departmentId;
                param[1].Value = userId;

                sproc = new StoreProcedure(DepartmentEmployeeKeys.Sp_Upd_H0_DepartmentEmployee, param);
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

        #region Methods Get        

        public DataTable GetByDeptId(int departmentId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DepartmentId", SqlDbType.Int)
                };
                param[0].Value = departmentId;
                sproc = new StoreProcedure(DepartmentEmployeeKeys.SP_DEPARTMENT_EMPLOYEE_GET_BY_DEPARTMENT_ID, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByRoot(int RootId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@RootId", SqlDbType.Int)
                };
                param[0].Value = RootId;
                sproc = new StoreProcedure(DepartmentEmployeeKeys.Sp_Sel_H0_DepartmentEmployee_By_Root, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByRootLeaveDate(int RootId, DateTime LeaveDate)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@RootId", SqlDbType.Int),
                    new SqlParameter("@LeaveDate", SqlDbType.SmallDateTime, 4)
                };
                param[0].Value = RootId;
                param[1].Value = LeaveDate;
                sproc = new StoreProcedure(DepartmentEmployeeKeys.Sp_Sel_H0_DepartmentEmployee_By_Root_LeaveDate, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

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
                sproc = new StoreProcedure(DepartmentEmployeeKeys.SP_DEPARTMENT_EMPLOYEE_GET_BY_USER_ID, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByStatus(int status)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Status", SqlDbType.Int)
                };
                param[0].Value = status;
                sproc = new StoreProcedure("Sel_H0_DepartmentEmployee_By_Status", param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetAll()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure(DepartmentEmployeeKeys.SP_DEPARTMENT_EMPLOYEE_GET_BY_ALL, null);
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