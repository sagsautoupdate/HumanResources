namespace HRMUtil.KeyNames.H0
{
    public sealed class RoleKeys
    {
        /// <summary>
        ///     Some field names of Qualification table
        /// </summary>
        public const string FIELD_ROLE_ID = "RoleId";

        public const string FIELD_ROLE_NAME = "RoleName";
        public const string FIELD_ROLE_LEVEL = "RoleLevel";
        public const string FIELD_ROLE_DESCRIPTION = "Description";


        /// <summary>
        ///     StoreProcedure name  of Qualification object.
        /// </summary>
        public const string SP_ROLE_GET_BY_USERID = "Sel_H0_UserRoles_By_UserId";

        public const string SP_ROLE_GET_BY_USERID_ROLEID = "Sel_H0_UserRoles_By_UserId_RoleId";
        public const string Sp_Sel_H0_UserRoles_By_RoleId = "Sel_H0_UserRoles_By_RoleId";
        public const string Sp_Sel_H0_UserRoles_By_TimeKeeping = "Sel_H0_UserRoles_By_TimeKeeping";
        public const string Sp_Sel_H0_Roles_By_RoleType = "Sel_H0_Roles_By_RoleType";
        public const string Sp_Sel_H0_UserRoles_By_Filter = "Sel_H0_UserRoles_By_Filter";


        public const string Sp_Ins_H0_UserRole = "Ins_H0_UserRole";
        public const string Sp_Upd_H0_UserRoleByRole = "Upd_H0_UserRoleByRole";
        public const string Sp_Upd_H0_UserRoleByUser = "Upd_H0_UserRoleByUser";
        public const string Sp_Del_H0_UserRole = "Del_H0_UserRole";
        public const string Sp_Del_H0_UserRoleByRole = "Del_H0_UserRoleByRole";
        public const string Sp_Del_H0_UserRoleByUser = "Del_H0_UserRoleByUser";
    }
}