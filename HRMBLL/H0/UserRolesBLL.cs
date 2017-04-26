using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H0;
using HRMUtil;
using HRMUtil.KeyNames.H0;

namespace HRMBLL.H0
{
    public class UserRolesBLL
    {
        #region private fields

        #endregion

        #region properties

        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string UserCode { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public int RoleId { get; set; }

        public string RoleName { get; set; } = string.Empty;

        public int RoleLevel { get; set; }

        public string Description { get; set; } = string.Empty;

        #endregion

        #region public methods insert, update, delete

        public static long Insert(int userId, int roleId)
        {
            return new RoleUsersDAL().Insert(userId, roleId);
        }

        public static long UpdateByRole(int userId, int roleId)
        {
            return new RoleUsersDAL().UpdateByRole(userId, roleId);
        }

        public static long UpdateByUser(int userId, int roleId)
        {
            return new RoleUsersDAL().UpdateByUser(userId, roleId);
        }

        public static long Delete(int userId, int roleId)
        {
            return new RoleUsersDAL().Delete(userId, roleId);
        }

        public static long DeleteByUser(int userId)
        {
            return new RoleUsersDAL().DeleteByUser(userId);
        }

        public static long DeleteByRole(int roleId)
        {
            return new RoleUsersDAL().DeleteByRole(roleId);
        }

        #endregion

        #region public method GET

        public static List<UserRolesBLL> GetByUserId(int userId)
        {
            return GenerateUserRolesBLLFromDataTable(new RoleUsersDAL().GetByUserId(userId));
        }

        public static DataTable GetWinForm(int roleId)
        {
            return new RoleUsersDAL().GetWinForm(roleId);
        }

        public static List<UserRolesBLL> GetRoleByRoleType(int roleType)
        {
            return GenerateUserRolesBLLFromDataTable(new RoleUsersDAL().GetRoleByRoleType(roleType));
        }

        public static List<UserRolesBLL> GetByRoleId(int roleId)
        {
            return GenerateUserRolesBLLFromDataTable(new RoleUsersDAL().GetByRoleId(roleId));
        }

        public static List<UserRolesBLL> GetByFilter(string fullName, string roleIds, string departmentIds)
        {
            return GenerateUserRolesBLLFromDataTable(new RoleUsersDAL().GetByFilter(fullName, roleIds, departmentIds));
        }

        public static List<UserRolesBLL> GetByTimeKeeping(int departmentId, int currentUserId, int typeTimeKeeping)
        {
            return
                GenerateUserRolesBLLFromDataTable(new RoleUsersDAL().GetByTimeKeeping(departmentId, currentUserId,
                    typeTimeKeeping));
        }

        public static bool IsAdmin(int userId)
        {
            var list =
                GenerateUserRolesBLLFromDataTable(new RoleUsersDAL().GetByUserId_RoleId(userId,
                    Constants.ROLE_ADMINISTRATORS_ID));

            if (list.Count > 0)
                return true;
            return false;
        }

        public static bool IsHRManager(int userId)
        {
            var list =
                GenerateUserRolesBLLFromDataTable(new RoleUsersDAL().GetByUserId_RoleId(userId,
                    Constants.ROLE_HR_MANAGER_ID));

            if (list.Count > 0)
                return true;
            return false;
        }

        public static bool IsHRAdmin(int userId)
        {
            var list =
                GenerateUserRolesBLLFromDataTable(new RoleUsersDAL().GetByUserId_RoleId(userId,
                    Constants.ROLE_HR_ADMINISTRATORS_ID));

            if (list.Count > 0)
                return true;
            return false;
        }

        public static bool IsHRMember(int userId)
        {
            var list =
                GenerateUserRolesBLLFromDataTable(new RoleUsersDAL().GetByUserId_RoleId(userId,
                    Constants.ROLE_HR_Member_ID));

            if (list.Count > 0)
                return true;
            return false;
        }

        public static bool IsFinancialAccounting(int userId)
        {
            var list =
                GenerateUserRolesBLLFromDataTable(new RoleUsersDAL().GetByUserId_RoleId(userId,
                    Constants.ROLE_FINANCIAL_ADMINISTRATORS_ID));

            if (list.Count > 0)
                return true;
            return false;
        }

        public static bool IsTimeKeepingManager(int userId)
        {
            var list =
                GenerateUserRolesBLLFromDataTable(new RoleUsersDAL().GetByUserId_RoleId(userId,
                    Constants.Role_TimeKeepingManager));

            if (list.Count > 0)
                return true;
            return false;
        }

        public static bool IsTimeKeepingLeader(int userId)
        {
            var list =
                GenerateUserRolesBLLFromDataTable(new RoleUsersDAL().GetByUserId_RoleId(userId,
                    Constants.Role_TimeKeepingLeader));

            if (list.Count > 0)
                return true;
            return false;
        }

        public static bool IsTimeKeepingSuperVisor(int userId)
        {
            var list =
                GenerateUserRolesBLLFromDataTable(new RoleUsersDAL().GetByUserId_RoleId(userId,
                    Constants.Role_TimeKeepingSupperVisor));

            if (list.Count > 0)
                return true;
            return false;
        }

        public static bool IsTimeKeepingGroup(int userId)
        {
            var list =
                GenerateUserRolesBLLFromDataTable(new RoleUsersDAL().GetByUserId_RoleId(userId,
                    Constants.Role_TimeKeepingGroup));

            if (list.Count > 0)
                return true;
            return false;
        }

        public static bool IsTrainingManager(int userId)
        {
            var list =
                GenerateUserRolesBLLFromDataTable(new RoleUsersDAL().GetByUserId_RoleId(userId,
                    Constants.Role_Type_TraningManager_Id));

            if (list.Count > 0)
                return true;
            return false;
        }

        public static bool IsQAManager(int userId)
        {
            var list =
                GenerateUserRolesBLLFromDataTable(new RoleUsersDAL().GetByUserId_RoleId(userId,
                    Constants.Role_Type_QAManager_Id));

            if (list.Count > 0)
                return true;
            return false;
        }

        #endregion

        #region private methods

        private static List<UserRolesBLL> GenerateUserRolesBLLFromDataTable(DataTable dt)
        {
            var list = new List<UserRolesBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateUserRolesBLLFromDataRow(dr));

            return list;
        }

        private static UserRolesBLL GenerateUserRolesBLLFromDataRow(DataRow dr)
        {
            var objBLL = new UserRolesBLL();
            try
            {
                objBLL.UserId = dr[EmployeeKeys.FIELD_EMPLOYEES_USERID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[EmployeeKeys.FIELD_EMPLOYEES_USERID].ToString().Trim());
            }
            catch
            {
            }
            try
            {
                objBLL.UserName = dr[EmployeeKeys.FIELD_EMPLOYEES_USERNAME] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeKeys.FIELD_EMPLOYEES_USERNAME].ToString();
            }
            catch
            {
            }
            try
            {
                objBLL.FullName = dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();
            }
            catch
            {
            }
            try
            {
                objBLL.UserCode = dr[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE].ToString();
            }
            catch
            {
            }
            try
            {
                objBLL.RoleId = dr[RoleKeys.FIELD_ROLE_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[RoleKeys.FIELD_ROLE_ID].ToString());
            }
            catch
            {
            }
            try
            {
                objBLL.RoleName = dr[RoleKeys.FIELD_ROLE_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[RoleKeys.FIELD_ROLE_NAME].ToString();
            }
            catch
            {
            }
            try
            {
                objBLL.RoleLevel = dr[RoleKeys.FIELD_ROLE_LEVEL] == DBNull.Value
                    ? 0
                    : int.Parse(dr[RoleKeys.FIELD_ROLE_LEVEL].ToString());
            }
            catch
            {
            }
            try
            {
                objBLL.Description = dr[RoleKeys.FIELD_ROLE_DESCRIPTION] == DBNull.Value
                    ? string.Empty
                    : dr[RoleKeys.FIELD_ROLE_DESCRIPTION].ToString();
            }
            catch
            {
            }
            return objBLL;
        }

        #endregion
    }
}