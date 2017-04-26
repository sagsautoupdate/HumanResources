namespace HRMUtil.KeyNames.H
{
    public sealed class BonusNameKeys
    {
        /// <summary>
        ///     Some field names of BonusName table
        /// </summary>
        public const string FIELD_BONUS_NAME_ID = "BonusNameId";

        public const string FIELD_BONUS_NAME_NAME = "BonusName";
        public const string FIELD_BONUS_NAME_DESCRIPTION = "Description";
        public const string FIELD_BONUS_NAME_TYPE = "Type";
        public const string FIELD_BONUS_NAME_VISIBLE = "Visible";

        /// <summary>
        ///     StoreProcedure name of BonusName object.
        /// </summary>
        public const string SP_BONUS_NAME_INSERT = "Ins_H_BonusName";

        public const string SP_BONUS_NAME_BY_TYPE = "Sel_H_BonusNames_By_Type";
        public const string SP_BONUS_NAME_ALL = "Sel_H_BonusNames_All";
        public const string Sp_Sel_H_BonusNames_By_Ids = "Sel_H_BonusNames_By_Ids";
    }
}