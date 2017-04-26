namespace HRMUtil.KeyNames.H1
{
    public sealed class LNS_CoefficientEmployeeKeys
    {
        /// <summary>
        ///     Some fields in H1_LNS_CoefficientEmployee
        /// </summary>
        public const string FIELD_LNS_COEFFICIENT_EMPLOYEE_ID = "LNS_CoefficientEmployeeId";

        public const string FIELD_LNS_COEFFICIENT_EMPLOYEE_DESCRIPTION = "LNS_CoefficientEmployeeDescription";
        public const string FIELD_LNS_COEFFICIENT_EMPLOYEE_CREATEDATE = "CreateDate";
        public const string FIELD_LNS_COEFFICIENT_EMPLOYEE_PCTN = "PCTN";
        public const string FIELD_LNS_COEFFICIENT_EMPLOYEE_ACTIVE = "Active";

        public const string FIELD_LNS_COEFFICIENT_EMPLOYEE_LNSWages = "LNSWages";
        public const string FIELD_LNS_COEFFICIENT_EMPLOYEE_LNSUnit = "LNSUnit";

        public const string Field_LNS_Coefficeint_TotalLNS = "TotalLNS";
        public const string Field_LNS_Coefficeint_TotalLNSP = "TotalLNSP";

        /////////////////////////////////////////////////////////
        /// <summary>
        ///     cac field co he so da duoc tinh ra he so cuoi cung
        /// </summary>
        public const string Field_LNS_Coefficeint_LNS = "LNS";

        public const string Field_LNS_Coefficeint_LNSPCTN = "LNSPCTN";
        /////////////////////////////////////////////////////////
        /// <summary>
        ///     some store procedurces for H1_LNS_CoefficientLevels
        /// </summary>
        public const string Sp_Ins_H1_LNS_CoefficientEmployee = "Ins_H1_LNS_CoefficientEmployee";

        public const string Sp_Upd_H1_LNS_CoefficientEmployee = "Upd_H1_LNS_CoefficientEmployee";
        public const string SP_LNS_COEFFICIENT_EMPLOYEE_DELETE = "Del_H1_LNS_CoefficientEmployee";

        public const string Sp_Del_H1_LNS_CoefficientEmployeeByEmployeeContractId =
            "Del_H1_LNS_CoefficientEmployeeByEmployeeContractId";

        public const string SP_Sel_H1_LNS_CoefficientEmployee_By_UserId = "Sel_H1_LNS_CoefficientEmployee_By_UserId";

        public const string Sp_Sel_H1_LNS_CoefficientEmployee_By_UserId_Date =
            "Sel_H1_LNS_CoefficientEmployee_By_UserId_Date";

        public const string Sp_Sel_H1_LNS_CoefficientEmployee_By_EmployeeContractId =
            "Sel_H1_LNS_CoefficientEmployee_By_EmployeeContractId";

        public const string Sp_Sel_H1_LNS_CoefficientEmployee_By_UserIdForNew =
            "Sel_H1_LNS_CoefficientEmployee_By_UserIdForNew";
    }
}