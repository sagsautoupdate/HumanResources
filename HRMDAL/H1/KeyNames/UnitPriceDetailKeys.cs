using System;
using System.Text;

namespace HRMDAL.H1.KeyNames
{
    sealed public class UnitPriceDetailKeys
    {
        /// <summary>
        /// Some fields in H1_WorkdayEmployees table
        /// </summary>
        public const string FIELD_UNIT_PRICE_DETAIL_ID = "UnitPriceDetailId";
        public const string FIELD_UNIT_PRICE_DETAIL_VALUE = "UnitPriceValue";
        public const string FIELD_UNIT_PRICE_DETAIL_CREATE_DATE = "CreateDate";
        public const string FIELD_UNIT_PRICE_DETAIL_ACTIVE = "Active";

        /// <summary>
        /// store procedure for 
        /// </summary>
        /// 

        public const string SP_FIELD_UNIT_PRICE_DETAIL_INSERT = "Ins_H1_UnitPriceDetail";
        public const string SP_FIELD_UNIT_PRICE_DETAIL_UPDATE = "Upd_H1_UnitPriceDetail";
        public const string SP_FIELD_UNIT_PRICE_DETAIL_DELETE = "Del_H1_UnitPriceDetail";

        public const string SP_FIELD_UNIT_PRICE_DETAIL_GET_All = "Sel_H1_UnitPriceDetail_All";
        //public const string SP_WORKDAY_EMPLOYEE_GET_BY_ID = "Sel_H1_WorkdayEmployee_By_Id";
        //public const string SP_WORKDAY_EMPLOYEE_GET_BY_USERID_MONTH_YEAR = "Sel_H1_WorkdayEmployee_By_UserId_Mont_Year";
    }
}
