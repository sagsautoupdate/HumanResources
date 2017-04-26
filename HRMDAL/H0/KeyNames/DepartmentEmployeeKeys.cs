using System;
using System.Text;

namespace HRMDAL.H0.KeyNames
{
    public sealed class DepartmentEmployeeKeys
    {
        #region some store procedures of Department table

        public const string SP_DEPARTMENT_EMPLOYEE_INSERT = "Ins_H0_DepartmentEmployee";
        public const string SP_DEPARTMENT_EMPLOYEE_INSERTS = "Ins_H0_DepartmentEmployees";
        public const string SP_DEPARTMENT_EMPLOYEE_UPDATE = "Upd_H0_DepartmentEmployee";
        public const string SP_DEPARTMENT_EMPLOYEE_DELETE = "Del_H0_DepartmentEmployee";

        public const string SP_DEPARTMENT_EMPLOYEE_GET_BY_DEPARTMENT_ID = "Sel_H0_DepartmentEmployee_By_DeptId";
        public const string SP_DEPARTMENT_EMPLOYEE_GET_BY_USER_ID = "Sel_H0_DepartmentEmployee_By_UserId";
        public const string SP_DEPARTMENT_EMPLOYEE_GET_BY_ALL = "Sel_H0_DepartmentEmployee_By_All";
        
        #endregion
    }
}
