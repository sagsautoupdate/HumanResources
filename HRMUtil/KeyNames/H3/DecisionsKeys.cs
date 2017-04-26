namespace HRMUtil.KeyNames.H3
{
    public sealed class DecisionsKeys
    {
        /// <summary>
        ///     some fields in H3_Decisions tables
        /// </summary>
        public const string Field_Decisions_DecisionId = "DecisionId";

        public const string Field_Decisions_DecisionTypeId = "DecisionTypeId";
        public const string Field_Decisions_DecisionNo = "DecisionNo";
        public const string Field_Decisions_DecisionDate = "DecisionDate";
        public const string Field_Decisions_DecisionName = "DecisionName";
        public const string Field_Decisions_BringOutDept = "BringOutDept";
        public const string Field_Decisions_SignUser = "SignUser";
        public const string Field_Decisions_Remark = "Remark";

        /// <summary>
        ///     some store procedures H3_Decisions table
        /// </summary>
        public const string Sp_Ins_H3_Decisions = "Ins_H3_Decisions";

        public const string Sp_Upd_H3_Decisions = "Upd_H3_Decisions";
        public const string Sp_Del_H3_Decisions = "Del_H3_Decisions";

        public const string Sp_Sel_H3_Decisions_By_Filter = "Sel_H3_Decisions_By_Filter";
        public const string Sp_Sel_H3_Decision_By_Id = "Sel_H3_Decision_By_Id";
    }
}