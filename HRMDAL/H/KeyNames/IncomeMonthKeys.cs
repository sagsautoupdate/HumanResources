using System;
using System.Text;

namespace HRMDAL.H.KeyNames
{
    public sealed class IncomeMonthKeys
    {
        /// <summary>
        /// Some field names of Coefficient table
        /// </summary>
        public const string FIELD_INCOME_MONTH_ID = "IncomeMonthId";
        public const string FIELD_INCOME_MONTH_VALUE = "Value";
        public const string FIELD_INCOME_MONTH_LK = "Lk";
        public const string FIELD_INCOME_MONTH_DATE = "Date";
        public const string FIELD_INCOME_MONTH_LOCK = "Lock";


        /// <summary>
        /// Some StoreProcedure names  of Coefficient table.
        /// </summary>
        public const string SP_INCOME_MONTH_GET_BY_USERID_MONTHLY = "Sel_H_IncomeMonth_ByUserId_Monthly";

        public const string SP_INCOME_MONTH_INSERT = "Ins_H_IncomeMonth";
    }
}
