using System;
using System.Text;

namespace HRMDAL.H0.KeyNames
{
    public sealed class RoleKeys
    {
        /// <summary>
        /// Some field names of Qualification table
        /// </summary>
        public const string FIELD_ROLE_ID = "RoleId";
        public const string FIELD_ROLE_NAME = "RoleName";
        public const string FIELD_ROLE_LEVEL = "RoleLevel";
        public const string FIELD_ROLE_DESCRIPTION = "Description";


        /// <summary>
        /// StoreProcedure name  of Qualification object.
        /// </summary>
        public const string SP_ROLE_GET_BY_USERID = "Sel_H0_UserRoles_By_UserId";
        public const string SP_ROLE_GET_BY_USERID_ROLEID = "Sel_H0_UserRoles_By_UserId_RoleId";   
    }
}
