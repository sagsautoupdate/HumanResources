namespace HRMUtil.KeyNames.H1
{
    public sealed class CoefficientEmployeeKeys
    {
        /// <summary>
        ///     Some field names of Coefficient table
        /// </summary>
        public const string FIELD_COEFFICIENT_EMPLOYEE_ID = "CoefficientEmployeeId";

        //public const string FIELD_COEFFICIENT_EMPLOYEE_K = "K";
        public const string FIELD_COEFFICIENT_EMPLOYEE_PCDH = "PCDH";
        public const string FIELD_COEFFICIENT_EMPLOYEE_PCTN = "PCTN";
        public const string FIELD_COEFFICIENT_EMPLOYEE_PCCV = "PCCV";
        public const string FIELD_COEFFICIENT_EMPLOYEE_PCKV = "PCKV";
        public const string FIELD_COEFFICIENT_EMPLOYEE_DESCRIPTION = "Description";
        public const string FIELD_COEFFICIENT_EMPLOYEE_ACTIVE = "Active";
        public const string FIELD_COEFFICIENT_EMPLOYEE_CREATEDATE = "CreateDate";

        public const string Field_Coefficient_Employee_TotalPCDH = "TotalPCDH";
        public const string Field_Coefficient_Employee_TotalPCTN = "TotalPCTN";
        public const string Field_Coefficient_Employee_TotalPCCV = "TotalPCCV";
        public const string Field_Coefficient_Employee_TotalPCKV = "TotalPCKV";


        public const string Field_Coeficient_Employee_LCBCoefficientValue = "LCBCoefficientValue";
        public const string Field_Coeficient_Employee_LCBWages = "LCBWages";
        public const string Field_Coeficient_Employee_LCBUnit = "LCBUnit";
        public const string Field_Coeficient_Employee_LCBActive = "LCBActive";

        public const string Field_Coeficient_Employee_LNSCoefficientValue = "LNSCoefficientValue";
        public const string Field_Coeficient_Employee_LNSWages = "LNSWages";
        public const string Field_Coeficient_Employee_LNSUnit = "LNSUnit";
        public const string Field_Coeficient_Employee_LNSActive = "LNSActive";
        public const string Field_Coeficient_Employee_LNSPCTN = "LNSPCTN";

        /// <summary>
        ///     StoreProcedure name  of Coefficient object.
        /// </summary>
        public const string SP_COEFFICIENT_EMPLOYEE_INSERT = "Ins_H1_CoefficientEmployee";

        public const string SP_COEFFICIENT_EMPLOYEE_UPDATE = "Upd_H1_CoefficientEmployee";
        public const string SP_COEFFICIENT_EMPLOYEE_DELETE = "Del_H1_CoefficientEmployee";

        //public const string SP_COEFFICIENT_EMPLOYEE_BY_FILTER = "Sel_H1_CoefficientEmployee_By_Filter";        
        //public const string SP_COEFFICIENT_EMPLOYEE_ACTIVE_BY_USERID = "Sel_H1_CoefficientEmployee_Active_By_UserId";

        public const string Sp_Sel_H1_CoefficientEmployee_By_UserId = "Sel_H1_CoefficientEmployee_By_UserId";
        public const string Sp_Sel_H1_CoefficientEmployee_By_UserId_Date = "Sel_H1_CoefficientEmployee_By_UserId_Date";

        public const string Sp_Sel_H1_CoefficientEmployee_By_UserId_DateFinal =
            "Sel_H1_CoefficientEmployee_By_UserId_DateFinal";

        public const string Sp_Sel_H1_CoefficientEmployee_By_UserIdForNew = "Sel_H1_CoefficientEmployee_By_UserIdForNew";

        ///////////////////////////////////////////////
        //public const string Sp_Sel_H1_CoefficientEmployeeAll_By_Filter = "Sel_H1_CoefficientEmployeeAll_By_Filter";
        //public const string Sp_Sel_H1_CoefficientEmployeeAllTotal_By_Filter = "Sel_H1_CoefficientEmployeeAllTotal_By_Filter";
        //public const string Sp_Sel_H1_CoefficientEmployeeAll_Active_By_UserId = "Sel_H1_CoefficientEmployeeAll_Active_By_UserId";
    }
}