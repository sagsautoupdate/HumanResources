using System;
using System.Text;

namespace HRMDAL.H1.KeyNames
{
    sealed public class CoefficientEmployeeKeys
    {

        /// <summary>
        /// Some field names of Coefficient table
        /// </summary>
        public const string FIELD_COEFFICIENT_EMPLOYEE_ID = "CoefficientEmployeeId";
        public const string FIELD_COEFFICIENT_EMPLOYEE_K = "K";
        public const string FIELD_COEFFICIENT_EMPLOYEE_PCDH = "PCDH";
        public const string FIELD_COEFFICIENT_EMPLOYEE_PCTN = "PCTN";
        public const string FIELD_COEFFICIENT_EMPLOYEE_PCCV = "PCCV";
        public const string FIELD_COEFFICIENT_EMPLOYEE_PCKV = "PCKV";
        public const string FIELD_COEFFICIENT_EMPLOYEE_DESCRIPTION = "Description";
        public const string FIELD_COEFFICIENT_EMPLOYEE_ACTIVE = "Active";
        public const string FIELD_COEFFICIENT_EMPLOYEE_CREATEDATE = "CreateDate";


        /// <summary>
        /// StoreProcedure name  of Coefficient object.
        /// </summary>
        public const string SP_COEFFICIENT_EMPLOYEE_INSERT = "Ins_H1_CoefficientEmployee";
        public const string SP_COEFFICIENT_EMPLOYEE_UPDATE = "Upd_H1_CoefficientEmployee";
        public const string SP_COEFFICIENT_EMPLOYEE_DELETE = "Del_H1_CoefficientEmployee";

        public const string SP_COEFFICIENT_EMPLOYEE_BY_FILTER = "Sel_H1_CoefficientEmployee_By_Filter";
        public const string SP_COEFFICIENT_EMPLOYEE_BY_USERID = "Sel_H1_CoefficientEmployee_By_UserId";
        public const string SP_COEFFICIENT_EMPLOYEE_ACTIVE_BY_USERID = "Sel_H1_CoefficientEmployee_Active_By_UserId";

    }
}
