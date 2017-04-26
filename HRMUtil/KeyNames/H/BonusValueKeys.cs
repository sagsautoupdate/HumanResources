namespace HRMUtil.KeyNames.H
{
    public sealed class BonusValueKeys
    {
        /// <summary>
        ///     Some field names of BonusValue table
        /// </summary>
        public const string FIELD_BONUS_VALUE_ID = "BonusValueId";

        public const string FIELD_BONUS_VALUE_VALUE = "BonusValue";
        public const string FIELD_BONUS_VALUE_YEAR = "BonusYear";

        /// <summary>
        ///     StoreProcedure name of BonusValue object.
        /// </summary>
        public const string SP_BONUS_VALUE_INSERT = "Ins_H_BonusValue";

        public const string SP_BONUS_VALUE_GET_BY_USERID_YEAR = "Sel_H_BonusValue_By_UserId_Year";
        public const string SP_BONUS_VALUE_GET_BY_FILTER = "Sel_H_BonusValue_By_Filter";
        public const string Sp_Sel_H_BonusValue_By_YearBonusNamesIds = "Sel_H_BonusValue_By_YearBonusNamesIds";
        public const string Sp_Sel_H_BonusValue_StatisticTotalBy_Filter = "Sel_H_BonusValue_StatisticTotalBy_Filter";
    }
}