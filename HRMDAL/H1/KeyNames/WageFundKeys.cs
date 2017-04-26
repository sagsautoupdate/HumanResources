using System;
using System.Text;

namespace HRMDAL.H1.KeyNames
{
    sealed public class WageFundKeys
    {
        /// <summary>
        /// Some fields in H1_WorkdayEmployees table
        /// </summary>
        public const string FIELD_WAGE_FUND_ID = "WageFundId";
        public const string FIELD_WAGE_FUND_VALUE = "WageFundValue";
        public const string FIELD_WAGE_FUND_TOTAL_COEFICIENT_VALUES = "TotalCoefficientValues";        
        public const string FIELD_WAGE_FUND_CREATE_DATE = "CreateDate";        

        /// <summary>
        /// store procedure for 
        /// </summary>
        /// 
        public const string SP_FIELD_WAGE_FUND_INSERT = "Ins_H1_WageFund";
        public const string SP_FIELD_WAGE_FUND_UPDATE = "Upd_H1_WageFund";
        public const string SP_FIELD_WAGE_FUND_DELETE = "Del_H1_WageFund";

        public const string SP_FIELD_WAGE_FUND_GET_BY_ALL = "Sel_H1_WageFund_By_All";
        //public const string SP_WORKDAY_EMPLOYEE_GET_BY_ID = "Sel_H1_WorkdayEmployee_By_Id";
        //public const string SP_WORKDAY_EMPLOYEE_GET_BY_USERID_MONTH_YEAR = "Sel_H1_WorkdayEmployee_By_UserId_Mont_Year";
        
    }
}
