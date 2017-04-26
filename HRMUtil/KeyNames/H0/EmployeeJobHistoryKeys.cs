namespace HRMUtil.KeyNames.H0
{
    public sealed class EmployeeJobHistoryKeys
    {
        /// <summary>
        ///     Some field names of EmployeeJobHistory table
        /// </summary>
        public const string Field_EmployeeJobHistory_Id = "JobHistoryId";

        public const string Field_EmployeeJobHistory_UserId = "UserId";
        public const string Field_EmployeeJobHistory_FromYear = "FromYear";
        public const string Field_EmployeeJobHistory_ToYear = "ToYear";
        public const string Field_EmployeeJobHistory_Infor = "Infor";
        public const string Field_EmployeeJobHistory_Type = "Type";

        /// <summary>
        ///     StoreProcedure name  of EmployeeJobHistory object.
        /// </summary>
        public const string Sp_EmployeeJobHistory_Get_By_Filter = "Sel_H0_EmployeeJobHistoryByFilter";

        public const string Sp_EmployeeJobHistory_Insert = "Ins_H0_EmployeeJobHistory";
        public const string Sp_EmployeeJobHistory_Update = "Upd_H0_EmployeeJobHistory";
        public const string Sp_EmployeeJobHistory_Delete = "Del_H0_EmployeeJobHistory";
    }
}