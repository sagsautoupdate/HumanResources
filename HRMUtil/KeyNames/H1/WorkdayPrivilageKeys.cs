namespace HRMUtil.KeyNames.H1
{
    public sealed class WorkdayPrivilageKeys
    {
        /// <summary>
        ///     Some fields in H1_WorkdayPrivilege table
        /// </summary>
        public const string Field_Workday_Privilege_UserId = "UserId";

        public const string Field_Workday_Privilege_DepartmentId = "DepartmentId";
        public const string Field_Workday_Privilege_IsInit = "IsInit";
        public const string Field_Workday_Privilege_PrivilegeType = "PrivilegeType";

        public const string Field_Workday_Privilege_EmployeeCode = "EmployeeCode";
        public const string Field_Workday_Privilege_FullName = "FullName";
        public const string Field_Workday_Privilege_DepartmentFullName = "DepartmentFullName";

        /// <summary>
        ///     Some Sproc for H1_WorkdayPrivilege table
        /// </summary>
        public const string Sp_Sel_H1_WorkdayPrivilege_By_UserId = "Sel_H1_WorkdayPrivilege_By_UserId";

        public const string Sp_Sel_H1_WorkdayPrivilege_By_UserId_DeptId = "Sel_H1_WorkdayPrivilege_By_UserId_DeptId";
        public const string Sp_Sel_H1_WorkdayPrivilege_By_DeptId = "Sel_H1_WorkdayPrivilege_By_DeptId";
        public const string Sp_Sel_H1_WorkdayPrivilege_By_DeptId_IsInit = "Sel_H1_WorkdayPrivilege_By_DeptId_IsInit";

        public const string Sp_Sel_H1_WorkdayPrivilege_By_DeptIds = "Sel_H1_WorkdayPrivilege_By_DeptIds";


        public const string Sp_Ins_H1_WorkdayPrivilege = "Ins_H1_WorkdayPrivilege";
        public const string Sp_Upd_H1_WorkdayPrivilege = "Upd_H1_WorkdayPrivilege";
        public const string Sp_Del_H1_WorkdayPrivilege = "Del_H1_WorkdayPrivilege";
    }
}