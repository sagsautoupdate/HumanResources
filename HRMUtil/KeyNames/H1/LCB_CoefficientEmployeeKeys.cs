namespace HRMUtil.KeyNames.H1
{
    public sealed class LCB_CoefficientEmployeeKeys
    {
        /// <summary>
        ///     Some fields in H1_LCB_CoefficientEmployees table
        /// </summary>
        public const string Field_LCB_CoefficientEmployees_LCB_CoefficientEmployeeId = "LCB_CoefficientEmployeeId";

        public const string Field_LCB_CoefficientEmployees_FromDate = "FromDate";
        public const string Field_LCB_CoefficientEmployees_ToDate = "ToDate";

        public const string Field_LCB_CoefficientEmployees_LCB_CoefficientEmployeeDescription =
            "LCB_CoefficientEmployeeDescription";

        public const string Field_LCB_CoefficientEmployees_Active = "Active";
        public const string Field_LCB_CoefficientEmployees_LCBWages = "LCBWages";
        public const string Field_LCB_CoefficientEmployees_LCBUnit = "LCBUnit";
        public const string Field_LCB_CoefficientEmployees_PCDH = "PCDH";
        public const string Field_LCB_CoefficientEmployees_PCTN = "PCTN";
        public const string Field_LCB_CoefficientEmployees_PCCV = "PCCV";
        public const string Field_LCB_CoefficientEmployees_PCKV = "PCKV";


        public const string Field_LCB_Coefficeint_TotalLCB = "TotalLCB";
        public const string Field_LCB_Coefficeint_TotalLCBP = "TotalLCBP";


        /// <summary>
        ///     some store procedurces for H1_LCB_CoefficientLevels table
        /// </summary>
        public const string Sp_Ins_H1_LCB_CoefficientEmployee = "Ins_H1_LCB_CoefficientEmployee";

        public const string Sp_Upd_H1_LCB_CoefficientEmployee = "Upd_H1_LCB_CoefficientEmployee";
        public const string SP_LCB_COEFFICIENT_EMPLOYEE_DELETE = "Del_H1_LCB_CoefficientEmployee";

        public const string Sp_Del_H1_LCB_CoefficientEmployeeByEmployeeContractId =
            "Del_H1_LCB_CoefficientEmployeeByEmployeeContractId";

        public const string Sp_Sel_H1_LCB_CoefficientEmployee_By_UserId = "Sel_H1_LCB_CoefficientEmployee_By_UserId";

        public const string Sp_Sel_H1_LCB_CoefficientEmployee_By_EmployeeContractId =
            "Sel_H1_LCB_CoefficientEmployee_By_EmployeeContractId";

        //public const string SP_LCB_COEFFICIENT_EMPLOYEE_GET_CURRENT_BY_FILTER = "Sel_H1_LCB_CoefficientEmployee_Active_By_Filter";

        //public const string Sp_Sel_H1_LCB_CoefficientEmployee_By_UserId_Date = "Sel_H1_LCB_CoefficientEmployee_By_UserId_Date";
        //public const string Sel_H1_LCB_CoefficientEmployee_By_UserId_ToDate_ContractTypeId = "Sel_H1_LCB_CoefficientEmployee_By_UserId_ToDate_ContractTypeId";

        public const string Sp_Sel_H1_LCB_CoefficientEmployee_By_Remind = "Sel_H1_LCB_CoefficientEmployee_By_Remind";
        public const string Sp_Sel_H1_LCB_CoefficientEmployee_By_Changed = "Sel_H1_LCB_CoefficientEmployee_By_Changed";

        public const string Sp_Sel_H1_LCB_CoefficientEmployee_By_UserIdForNew =
            "Sel_H1_LCB_CoefficientEmployee_By_UserIdForNew";

        public const string Sp_Sel_H1_LCB_CoefficientEmployee_By_UserId_FromToDateFinal =
            "Sel_H1_LCB_CoefficientEmployee_By_UserId_FromToDateFinal";
    }
}