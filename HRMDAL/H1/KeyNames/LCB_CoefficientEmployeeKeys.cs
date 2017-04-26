using System;
using System.Text;

namespace HRMDAL.H1.KeyNames
{
    sealed public class LCB_CoefficientEmployeeKeys
    {
        /// <summary>
        /// Some fields in H1_LCB_CoefficientEmployees table
        /// </summary>
        public const string FIELD_LCB_COEFFICIENT_EMPLOYEE_ID = "LCB_CoefficientEmployeeId";
        public const string FIELD_LCB_COEFFICIENT_EMPLOYEE_FROM_DATE = "FromDate";
        public const string FIELD_LCB_COEFFICIENT_EMPLOYEE_TO_DATE = "ToDate";
        public const string FIELD_LCB_COEFFICIENT_EMPLOYEE_DESCRIPTION = "LCB_CoefficientEmployeeDescription";
        public const string FIELD_LCB_COEFFICIENT_EMPLOYEE_ACTIVE = "Active";

        /// <summary>
        /// some store procedurces for H1_LCB_CoefficientLevels table
        /// </summary>
        public const string SP_LCB_COEFFICIENT_EMPLOYEE_INSERT = "Ins_H1_LCB_CoefficientEmployee";
        public const string SP_LCB_COEFFICIENT_EMPLOYEE_UPDATE = "Upd_H1_LCB_CoefficientEmployee";
        public const string SP_LCB_COEFFICIENT_EMPLOYEE_DELETE = "Del_H1_LCB_CoefficientEmployee";

        public const string SP_LCB_COEFFICIENT_EMPLOYEE_GET_BY_USERID = "Sel_H1_LCB_CoefficientEmployee_By_UserId";
        public const string SP_LCB_COEFFICIENT_EMPLOYEE_GET_CURRENT_BY_USERID = "Sel_H1_LCB_CoefficientEmployee_Active_By_UserId";
        
    }
}
