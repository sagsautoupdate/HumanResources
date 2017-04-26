namespace HRMUtil.KeyNames.H0
{
    public sealed class EmployeeRelationKeys
    {
        /// <summary>
        ///     some fields in H0_EmployeeRelation tables
        /// </summary>
        public const string Field_EmployeeRelation_UserRelationId = "UserRelationId";

        public const string Field_EmployeeRelation_UserId = "UserId";
        public const string Field_EmployeeRelation_RelationTypeId = "RelationTypeId";
        public const string Field_EmployeeRelation_RFullName = "RFullName";
        public const string Field_EmployeeRelation_RDayOfBirth = "RDayOfBirth";
        public const string Field_EmployeeRelation_RMonthOfBirth = "RMonthOfBirth";
        public const string Field_EmployeeRelation_RYearOfBirth = "RYearOfBirth";
        public const string Field_EmployeeRelation_RNativePlace = "RNativePlace";
        public const string Field_EmployeeRelation_RResident = "RResident";
        public const string Field_EmployeeRelation_RLive = "RLive";
        public const string Field_EmployeeRelation_Before1975 = "Before1975";
        public const string Field_EmployeeRelation_After1975 = "After1975";
        public const string Field_EmployeeRelation_Participate = "Participate";
        public const string Field_EmployeeRelation_Died = "Died";
        public const string Field_EmployeeRelation_DiedCause = "DiedCause";
        public const string Field_EmployeeRelation_Others = "Others";

        /// <summary>
        ///     some store procedures H0_EmployeeRelation table
        /// </summary>
        public const string Sp_EmployeeRelation_Insert = "Ins_H0_EmployeeRelation";

        public const string Sp_EmployeeRelation_Update = "Upd_H0_EmployeeRelation";
        public const string Sp_EmployeeRelation_Delete = "Del_H0_EmployeeRelation";
        public const string Sp_EmployeeRelation_Get_By_Filter = "Sel_H0_EmployeeRelation_By_Filter";
    }
}