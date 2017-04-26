using System;
using System.Text;

namespace HRMDAL.H1.KeyNames
{
    sealed public class LNS_CoefficientEmployeeKeys
    {
        /// <summary>
        /// Some fields in H1_LNS_CoefficientEmployee
        /// </summary>
        public const string FIELD_LNS_COEFFICIENT_EMPLOYEE_ID = "LNS_CoefficientEmployeeId";
        public const string FIELD_LNS_COEFFICIENT_EMPLOYEE_DESCRIPTION = "LNS_CoefficientEmployeeDescription";
        public const string FIELD_LNS_COEFFICIENT_EMPLOYEE_CREATEDATE = "CreateDate";
        public const string FIELD_LNS_COEFFICIENT_EMPLOYEE_PCTN = "PCTN";
        public const string FIELD_LNS_COEFFICIENT_EMPLOYEE_ACTIVE = "Active";

        /// <summary>
        /// some store procedurces for H1_LNS_CoefficientLevels
        /// </summary>
        public const string SP_LNS_COEFFICIENT_EMPLOYEE_INSERT = "Ins_H1_LNS_CoefficientEmployee";
        public const string SP_LNS_COEFFICIENT_EMPLOYEE_UPDATE = "Upd_H1_LNS_CoefficientEmployee";
        public const string SP_LNS_COEFFICIENT_EMPLOYEE_DELETE = "Del_H1_LNS_CoefficientEmployee";

        public const string SP_LNS_COEFFICIENT_EMPLOYEE_GET_BY_USERID = "Sel_H1_LNS_CoefficientEmployee_By_UserId";
        public const string SP_LNS_COEFFICIENT_EMPLOYEE_GET_CURRENT_BY_USERID = "Sel_H1_LNS_CoefficientEmployee_Active_By_UserId";
        
    }
}
