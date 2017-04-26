using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H1;

namespace HRMDAL.H1
{
    public class WorkdayPrivilegeDAL : Dao
    {
        #region methods insert, update , delete

        public long Insert(int? UserId, int? DepartmentId, bool? IsInit, int? PrivilegeType)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@DepartmentId", SqlDbType.Int, 4),
                    new SqlParameter("@IsInit", SqlDbType.Bit, 1),
                    new SqlParameter("@PrivilegeType", SqlDbType.Int, 4)
                };

                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (DepartmentId.HasValue)
                    param[1].Value = DepartmentId.Value;
                else
                    param[1].Value = DBNull.Value;
                if (IsInit.HasValue)
                    param[2].Value = IsInit.Value;
                else
                    param[2].Value = DBNull.Value;
                if (PrivilegeType.HasValue)
                    param[3].Value = PrivilegeType.Value;
                else
                    param[3].Value = DBNull.Value;


                sproc = new StoreProcedure(WorkdayPrivilageKeys.Sp_Ins_H1_WorkdayPrivilege, param);
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

        public long Update(int? UserId, bool? IsInit, int? PrivilegeType, int? DepartmentId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@IsInit", SqlDbType.Bit, 1),
                    new SqlParameter("@PrivilegeType", SqlDbType.Int, 4),
                    new SqlParameter("@DepartmentId", SqlDbType.Int, 4)
                };

                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (IsInit.HasValue)
                    param[1].Value = IsInit.Value;
                else
                    param[1].Value = DBNull.Value;
                if (PrivilegeType.HasValue)
                    param[2].Value = PrivilegeType.Value;
                else
                    param[2].Value = DBNull.Value;
                if (DepartmentId.HasValue)
                    param[3].Value = DepartmentId.Value;
                else
                    param[3].Value = DBNull.Value;

                sproc = new StoreProcedure(WorkdayPrivilageKeys.Sp_Upd_H1_WorkdayPrivilege, param);
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

        public long Delete(int? UserId, int? DepartmentId, int? PrivilegeType)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@DepartmentId", SqlDbType.Int, 4),
                    new SqlParameter("@PrivilegeType", SqlDbType.Int, 4)
                };

                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (DepartmentId.HasValue)
                    param[1].Value = DepartmentId.Value;
                else
                    param[1].Value = DBNull.Value;
                if (PrivilegeType.HasValue)
                    param[2].Value = PrivilegeType.Value;
                else
                    param[2].Value = DBNull.Value;


                sproc = new StoreProcedure(WorkdayPrivilageKeys.Sp_Del_H1_WorkdayPrivilege, param);
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

        #region Methods GET      

        public DataTable GetByUserIdDeptId(int userId, int departmentId, int privilegeType)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@DepartmentId", SqlDbType.Int),
                    new SqlParameter("@PrivilegeType", SqlDbType.Int)
                };

                param[0].Value = userId;
                param[1].Value = departmentId;
                param[2].Value = privilegeType;

                sproc = new StoreProcedure(WorkdayPrivilageKeys.Sp_Sel_H1_WorkdayPrivilege_By_UserId_DeptId, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByUserId(int userId, int privilegeType)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@PrivilegeType", SqlDbType.Int)
                };

                param[0].Value = userId;
                param[1].Value = privilegeType;

                sproc = new StoreProcedure(WorkdayPrivilageKeys.Sp_Sel_H1_WorkdayPrivilege_By_UserId, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByDeptId(int departmentId, int privilegeType)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DepartmentId", SqlDbType.Int),
                    new SqlParameter("@PrivilegeType", SqlDbType.Int)
                };

                param[0].Value = departmentId;
                param[1].Value = privilegeType;

                sproc = new StoreProcedure(WorkdayPrivilageKeys.Sp_Sel_H1_WorkdayPrivilege_By_DeptId, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByDeptIdIsInit(string deptIds, bool IsInit, int privilegeType)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DeptIds", SqlDbType.VarChar, 1000),
                    new SqlParameter("@IsInit", SqlDbType.Bit),
                    new SqlParameter("@PrivilegeType", SqlDbType.Int)
                };

                param[0].Value = deptIds;
                param[1].Value = IsInit;
                param[2].Value = privilegeType;

                sproc = new StoreProcedure(WorkdayPrivilageKeys.Sp_Sel_H1_WorkdayPrivilege_By_DeptId_IsInit, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByDeptIds(string deptIds, int privilegeType, string fullName)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DeptIds", SqlDbType.VarChar, 1000),
                    new SqlParameter("@PrivilegeType", SqlDbType.Int),
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 254)
                };

                param[0].Value = deptIds;
                param[1].Value = privilegeType;
                param[2].Value = fullName;

                sproc = new StoreProcedure(WorkdayPrivilageKeys.Sp_Sel_H1_WorkdayPrivilege_By_DeptIds, param);
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