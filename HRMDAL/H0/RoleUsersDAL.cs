using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H0;

namespace HRMDAL.H0
{
    public class RoleUsersDAL : Dao
    {
        #region get

        public DataTable GetByUserId(int userId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                param[0].Value = userId;

                sproc = new StoreProcedure(RoleKeys.SP_ROLE_GET_BY_USERID, param);
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

        public DataTable GetWinForm(int roleId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@RoleFormId", SqlDbType.Int)
                };

                param[0].Value = roleId;

                sproc = new StoreProcedure("Sel_H_GetRoles_WinForms", param);
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

        public DataTable GetByRoleId(int roleId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@RoleId", SqlDbType.Int)
                };

                param[0].Value = roleId;

                sproc = new StoreProcedure(RoleKeys.Sp_Sel_H0_UserRoles_By_RoleId, param);
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

        public DataTable GetByUserId_RoleId(int userId, int roleId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@RoleId", SqlDbType.Int)
                };

                param[0].Value = userId;
                param[1].Value = roleId;

                sproc = new StoreProcedure(RoleKeys.SP_ROLE_GET_BY_USERID_ROLEID, param);
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

        public DataTable GetByTimeKeeping(int departmentId, int currentUserId, int typeTimeKeeping)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DepartmentId", SqlDbType.Int),
                    new SqlParameter("@CurrentUserId", SqlDbType.Int),
                    new SqlParameter("@TypeTimeKeeping", SqlDbType.Int)
                };

                param[0].Value = departmentId;
                param[1].Value = currentUserId;
                param[2].Value = typeTimeKeeping;

                sproc = new StoreProcedure(RoleKeys.Sp_Sel_H0_UserRoles_By_TimeKeeping, param);
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

        public DataTable GetRoleByRoleType(int roleType)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@RoleType", SqlDbType.Int)
                };

                param[0].Value = roleType;

                sproc = new StoreProcedure(RoleKeys.Sp_Sel_H0_Roles_By_RoleType, param);
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

        public DataTable GetByFilter(string FullName, string RoleIds, string DepartmentIds)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName", SqlDbType.VarChar, 100),
                    new SqlParameter("@RoleIds", SqlDbType.VarChar, 254),
                    new SqlParameter("@DepartmentIds", SqlDbType.VarChar, 254)
                };

                param[0].Value = FullName;
                param[1].Value = RoleIds;
                param[2].Value = DepartmentIds;

                sproc = new StoreProcedure(RoleKeys.Sp_Sel_H0_UserRoles_By_Filter, param);
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

        #endregion

        #region methods insert, update , delete

        public long Insert(int? UserId, int? RoleId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@RoleId", SqlDbType.Int, 4)
                };

                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (RoleId.HasValue)
                    param[1].Value = RoleId.Value;
                else
                    param[1].Value = DBNull.Value;


                sproc = new StoreProcedure(RoleKeys.Sp_Ins_H0_UserRole, param);
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

        public long UpdateByRole(int? UserId, int? RoleId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@RoleId", SqlDbType.Int, 4)
                };

                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (RoleId.HasValue)
                    param[1].Value = RoleId.Value;
                else
                    param[1].Value = DBNull.Value;


                sproc = new StoreProcedure(RoleKeys.Sp_Upd_H0_UserRoleByRole, param);
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

        public long UpdateByUser(int? UserId, int? RoleId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@RoleId", SqlDbType.Int, 4),
                    new SqlParameter("@UserId", SqlDbType.Int, 4)
                };

                if (RoleId.HasValue)
                    param[0].Value = RoleId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (UserId.HasValue)
                    param[1].Value = UserId.Value;
                else
                    param[1].Value = DBNull.Value;

                sproc = new StoreProcedure(RoleKeys.Sp_Upd_H0_UserRoleByUser, param);
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

        public long Delete(int? UserId, int? RoleId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@RoleId", SqlDbType.Int, 4)
                };

                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (RoleId.HasValue)
                    param[1].Value = RoleId.Value;
                else
                    param[1].Value = DBNull.Value;


                sproc = new StoreProcedure(RoleKeys.Sp_Del_H0_UserRole, param);
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

        public long DeleteByUser(int? UserId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4)
                };

                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;


                sproc = new StoreProcedure(RoleKeys.Sp_Del_H0_UserRoleByUser, param);
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

        public long DeleteByRole(int? RoleId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@RoleId", SqlDbType.Int, 4)
                };

                if (RoleId.HasValue)
                    param[0].Value = RoleId.Value;
                else
                    param[0].Value = DBNull.Value;


                sproc = new StoreProcedure(RoleKeys.Sp_Del_H0_UserRoleByRole, param);
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