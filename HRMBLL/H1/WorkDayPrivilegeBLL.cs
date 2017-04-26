using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H1;
using HRMUtil;
using HRMUtil.KeyNames.H1;

namespace HRMBLL.H1
{
    public class WorkDayPrivilegeBLL
    {
        #region private fields

        #endregion

        #region properties

        public int UserId { get; set; }

        public int DepartmentId { get; set; }

        public int PrivilegeType { get; set; }

        public bool IsInit { get; set; }

        public string EmployeeCode { get; set; }

        public string FullName { get; set; }

        public string DepartmentFullName { get; set; }

        #endregion

        #region methods insert, update , delete

        public static long Insert(int userId, int departmentId, bool isInit, int privilegeType)
        {
            return new WorkdayPrivilegeDAL().Insert(userId, departmentId, isInit, privilegeType);
        }

        public static long Update(int userId, bool isInit, int privilegeType, int departmentId)
        {
            return new WorkdayPrivilegeDAL().Update(userId, isInit, privilegeType, departmentId);
        }

        public static long Delete(int userId, int departmentId, int privilegeType)
        {
            return new WorkdayPrivilegeDAL().Delete(userId, departmentId, privilegeType);
        }

        #endregion

        #region public method GET

        public static bool IsPrivilegeTimeKeeping(int userId, int departmentId, int privilegeType)
        {
            var list =
                GenerateWorkDayPrivilegeBLLFromDataTable(new WorkdayPrivilegeDAL().GetByUserIdDeptId(userId,
                    departmentId, privilegeType));

            if (list.Count == 1)
                return true;
            return false;
        }

        public static WorkDayPrivilegeBLL GetByUserIdDeptId(int userId, int departmentId, int privilegeType)
        {
            var list =
                GenerateWorkDayPrivilegeBLLFromDataTable(new WorkdayPrivilegeDAL().GetByUserIdDeptId(userId,
                    departmentId, privilegeType));

            if (list.Count == 1)
                return list[0];
            return null;
        }

        public static List<WorkDayPrivilegeBLL> GetByUserId(int userId, int privilegeType)
        {
            return GenerateWorkDayPrivilegeBLLFromDataTable(new WorkdayPrivilegeDAL().GetByUserId(userId, privilegeType));
        }

        public static string GetDepartmentIDsByUserId(int userId, int privilegeType)
        {
            var list =
                GenerateWorkDayPrivilegeBLLFromDataTable(new WorkdayPrivilegeDAL().GetByUserId(userId, privilegeType));

            var deptIds = string.Empty;
            foreach (var obj in list)
                deptIds += obj.DepartmentId + ",";

            if (deptIds.Length > 0)
                deptIds = Util.RejectLastComma(deptIds);

            return deptIds;
        }

        public static string GetUserIDsByDeptId(int deptId, int privilegeType)
        {
            var list =
                GenerateWorkDayPrivilegeBLLFromDataTable(new WorkdayPrivilegeDAL().GetByDeptId(deptId, privilegeType));

            var userIds = ",";
            foreach (var obj in list)
                userIds += obj.UserId + ",";

            return userIds;
        }

        public static string GetUserIdsByDeptIdIsInit(string deptIds, bool isInit, int privilegeType)
        {
            var list =
                GenerateWorkDayPrivilegeBLLFromDataTable(new WorkdayPrivilegeDAL().GetByDeptIdIsInit(deptIds, isInit,
                    privilegeType));

            var userIds = ",";
            foreach (var obj in list)
                userIds += obj.UserId + ",";

            //if (deptIds.Length > 0)
            //    deptIds = Util.RejectLastComma(deptIds);

            return userIds;
        }

        public static List<WorkDayPrivilegeBLL> GetUserIdsByDeptIdIsInit_(string deptIds, bool isInit, int privilegeType)
        {
            return
                GenerateWorkDayPrivilegeBLLFromDataTable(new WorkdayPrivilegeDAL().GetByDeptIdIsInit(deptIds, isInit,
                    privilegeType));
        }

        public static List<WorkDayPrivilegeBLL> GetByDeptId(int deptId, int privilegeType)
        {
            return GenerateWorkDayPrivilegeBLLFromDataTable(new WorkdayPrivilegeDAL().GetByDeptId(deptId, privilegeType));
        }

        public static List<WorkDayPrivilegeBLL> GetByDeptIds(string deptIds, int privilegeType, string fullName)
        {
            return
                GenerateWorkDayPrivilegeBLLFromDataTable(new WorkdayPrivilegeDAL().GetByDeptIds(deptIds, privilegeType,
                    fullName));
        }

        #endregion

        #region private methods

        private static List<WorkDayPrivilegeBLL> GenerateWorkDayPrivilegeBLLFromDataTable(DataTable dt)
        {
            var list = new List<WorkDayPrivilegeBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateWorkDayPrivilegeBLLFromDataRow(dr));

            return list;
        }

        private static WorkDayPrivilegeBLL GenerateWorkDayPrivilegeBLLFromDataRow(DataRow dr)
        {
            var objBLL = new WorkDayPrivilegeBLL();
            objBLL.UserId = dr[WorkdayPrivilageKeys.Field_Workday_Privilege_UserId] == DBNull.Value
                ? 0
                : int.Parse(dr[WorkdayPrivilageKeys.Field_Workday_Privilege_UserId].ToString().Trim());
            objBLL.IsInit = dr[WorkdayPrivilageKeys.Field_Workday_Privilege_IsInit] == DBNull.Value
                ? false
                : Convert.ToBoolean(dr[WorkdayPrivilageKeys.Field_Workday_Privilege_IsInit].ToString());

            try
            {
                objBLL.DepartmentId = dr[WorkdayPrivilageKeys.Field_Workday_Privilege_DepartmentId] == DBNull.Value
                    ? 0
                    : int.Parse(dr[WorkdayPrivilageKeys.Field_Workday_Privilege_DepartmentId].ToString());
            }
            catch
            {
            }

            try
            {
                objBLL.PrivilegeType = dr[WorkdayPrivilageKeys.Field_Workday_Privilege_PrivilegeType] == DBNull.Value
                    ? 0
                    : int.Parse(dr[WorkdayPrivilageKeys.Field_Workday_Privilege_PrivilegeType].ToString());
            }
            catch
            {
            }

            try
            {
                objBLL.EmployeeCode = dr[WorkdayPrivilageKeys.Field_Workday_Privilege_EmployeeCode] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayPrivilageKeys.Field_Workday_Privilege_EmployeeCode].ToString();
            }
            catch
            {
            }
            try
            {
                objBLL.FullName = dr[WorkdayPrivilageKeys.Field_Workday_Privilege_FullName] == DBNull.Value
                    ? string.Empty
                    : dr[WorkdayPrivilageKeys.Field_Workday_Privilege_FullName].ToString();
            }
            catch
            {
            }
            try
            {
                objBLL.DepartmentFullName = dr[WorkdayPrivilageKeys.Field_Workday_Privilege_DepartmentFullName] ==
                                            DBNull.Value
                    ? string.Empty
                    : dr[WorkdayPrivilageKeys.Field_Workday_Privilege_DepartmentFullName].ToString();
            }
            catch
            {
            }

            return objBLL;
        }

        #endregion
    }
}