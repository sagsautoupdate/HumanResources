using System;
using System.Text;

namespace HRMDAL.H1.KeyNames
{
    sealed public class UnitPriceKeys
    {
        /// <summary>
        /// Some fields in H1_WorkdayEmployees table
        /// </summary>
        public const string FIELD_UNIT_PRICE_ID = "UnitPriceId";
        public const string FIELD_UNIT_PRICE_NAME = "UnitPriceName";

        /// <summary>
        /// store procedure for 
        /// </summary>
        /// 

        public const string SP_UNIT_PRICE_GET_ALL = "Sel_H1_UnitPrice_All";
        //public const string SP_WORKDAY_EMPLOYEE_GET_BY_ID = "Sel_H1_WorkdayEmployee_By_Id";
        //public const string SP_WORKDAY_EMPLOYEE_GET_BY_USERID_MONTH_YEAR = "Sel_H1_WorkdayEmployee_By_UserId_Mont_Year";
    }
}
