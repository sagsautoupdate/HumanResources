namespace HRMUtil.KeyNames.H1
{
    public sealed class PITDeductionKeys
    {
        /// <summary>
        ///     Some fields in H1_PITDeduction table
        /// </summary>
        public const string Field_PITDeduction_Id = "PITDeductionId";

        public const string Field_PITDeduction_UserId = "UserId";
        public const string Field_PITDeduction_UserRelationId = "UserRelationId";
        public const string Field_PITDeduction_TaxNumber = "TaxNumber";
        public const string Field_PITDeduction_Id_Passport = "Id_Passport";
        public const string Field_PITDeduction_FromMonth = "FromMonth";
        public const string Field_PITDeduction_FromYear = "FromYear";
        public const string Field_PITDeduction_ToMonth = "ToMonth";
        public const string Field_PITDeduction_ToYear = "ToYear";

        public const string Field_PITDeduction_CreateDate = "CreateDate";
        public const string Field_PITDeduction_CreateUser = "CreateUser";
        public const string Field_PITDeduction_UpdateDate = "UpdateDate";
        public const string Field_PITDeduction_UpdateUser = "UpdateUser";


        public const string Field_PITDeduction_DepartmentFullName = "DepartmentFullName";
        public const string Field_PITDeduction_FullName = "FullName";
        public const string Field_PITDeduction_DepartmentCode = "DepartmentCode";
        public const string Field_PITDeduction_RFullName = "RFullName";
        public const string Field_PITDeduction_RelationTypeName = "RelationTypeName";
        public const string Field_PITDeduction_RootName = "RootName";

        /// <summary>
        ///     store procedure for H1_PITDeduction table
        /// </summary>
        public const string Sp_Ins_H1_PITDeduction = "Ins_H1_PITDeduction";

        public const string Sp_Upd_H1_PITDeduction = "Upd_H1_PITDeduction";
        public const string Sp_Del_H1_PITDeduction = "Del_H1_PITDeduction";

        public const string Sp_Sel_H1_PITDeduction_By_Filter = "Sel_H1_PITDeduction_By_Filter";
        public const string Sp_Sel_H1_PITDeduction_By_DeptId = "Sel_H1_PITDeduction_By_DeptId";
        public const string Sp_Sel_H1_PITDeduction_By_UserRelation = "Sel_H1_PITDeduction_By_UserRelation";
        public const string Sp_Sel_H1_PITDeduction_By_UserDate = "Sel_H1_PITDeduction_By_UserDate";
    }
}