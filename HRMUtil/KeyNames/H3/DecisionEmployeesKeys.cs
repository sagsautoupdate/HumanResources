namespace HRMUtil.KeyNames.H3
{
    public sealed class DecisionEmployeesKeys
    {
        /// <summary>
        ///     some fields in H3_DecisionEmployees tables
        /// </summary>
        public const string Field_DecisionEmployees_DecisionEmployeeId = "DecisionEmployeeId";

        public const string Field_DecisionEmployees_DecisionId = "DecisionId";
        public const string Field_DecisionEmployees_UserId = "UserId";
        public const string Field_DecisionEmployees_RootId = "RootId";
        public const string Field_DecisionEmployees_PositionId = "PositionId";
        public const string Field_DecisionEmployees_FromDate = "FromDate";
        public const string Field_DecisionEmployees_ToDate = "ToDate";
        public const string Field_DecisionEmployees_Level = "Level";
        public const string Field_DecisionEmployees_Form = "Form";
        public const string Field_DecisionEmployees_Title = "Title";
        public const string Field_DecisionEmployees_KeyPosition = "KeyPosition";

        /// <summary>
        ///     some store procedures H3_DecisionEmployees table
        /// </summary>
        public const string Sp_Ins_H3_DecisionEmployees = "Ins_H3_DecisionEmployees_By_UserId";

        public const string Sp_Upd_H3_DecisionEmployees = "Upd_H3_DecisionEmployees_By_UserId";
        public const string Sp_Del_H3_DecisionEmployees = "Del_H3_DecisionEmployees";
        public const string Sp_Del_H3_DecisionEmployees_By_DecisionId = "Del_H3_DecisionEmployees_By_DecisionId";
        public const string Sp_Del_H3_DecisionEmployees_By_Ids = "Del_H3_DecisionEmployees_By_Ids";

        public const string Sp_Sel_H3_DecisionEmployees_By_DecisionId = "Sel_H3_DecisionEmployees_By_DecisionId";
        public const string Sp_Sel_H3_DecisionEmployees_By_DeptId = "Sel_H3_EmployeeDecision_By_DeptId";
    }
}