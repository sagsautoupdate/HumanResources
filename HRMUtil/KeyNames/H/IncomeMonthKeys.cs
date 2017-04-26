namespace HRMUtil.KeyNames.H
{
    public sealed class IncomeMonthKeys
    {
        /// <summary>
        ///     Some field names of Coefficient table
        /// </summary>
        public const string FIELD_INCOME_MONTH_ID = "IncomeMonthId";

        public const string FIELD_INCOME_MONTH_VALUE = "Value";
        public const string FIELD_INCOME_MONTH_LK = "Lk";
        public const string FIELD_INCOME_MONTH_DATE = "Date";
        public const string FIELD_INCOME_MONTH_LOCK = "Lock";


        /// <summary>
        ///     Some StoreProcedure names  of Coefficient table.
        /// </summary>
        public const string SP_INCOME_MONTH_GET_BY_USERID_MONTHLY = "Sel_H_IncomeMonth_ByUserId_Monthly";

        public const string Sp_Sel_H_IncomeMonthAll_ByUserId_Monthly = "Sel_H_IncomeMonthAll_ByUserId_Monthly";

        public const string Sp_Ins_H_IncomeMonth = "Ins_H_IncomeMonth";
    }
}