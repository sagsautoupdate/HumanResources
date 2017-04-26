namespace HRMUtil.KeyNames.H0
{
    public sealed class DepartmentEmployeeKeys
    {
        #region some store procedures of Department table

        public const string SP_DEPARTMENT_EMPLOYEE_INSERT = "Ins_H0_DepartmentEmployee";
        public const string SP_DEPARTMENT_EMPLOYEE_INSERTS = "Ins_H0_DepartmentEmployees";
        public const string SP_DEPARTMENT_EMPLOYEE_UPDATE = "Upd_H0_DepartmentEmployee";
        public const string SP_DEPARTMENT_EMPLOYEE_DELETE = "Del_H0_DepartmentEmployee";
        public const string Sp_Del_H0_DepartmentEmployee = "Del_H0_DepartmentEmployees";

        public const string Sp_Upd_H0_DepartmentEmployee = "Upd_H0_DepartmentEmployee";

        public const string SP_DEPARTMENT_EMPLOYEE_GET_BY_DEPARTMENT_ID = "Sel_H0_DepartmentEmployee_By_DeptId";
        public const string SP_DEPARTMENT_EMPLOYEE_GET_BY_USER_ID = "Sel_H0_DepartmentEmployee_By_UserId";
        public const string SP_DEPARTMENT_EMPLOYEE_GET_BY_ALL = "Sel_H0_DepartmentEmployee_By_All";
        public const string Sp_Sel_H0_DepartmentEmployee_By_Root = "Sel_H0_DepartmentEmployee_By_Root";

        public const string Sp_Sel_H0_DepartmentEmployee_By_Root_LeaveDate =
            "Sel_H0_DepartmentEmployee_By_Root_LeaveDate";

        #endregion
    }
}