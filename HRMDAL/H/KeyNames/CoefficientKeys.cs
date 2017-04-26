using System;
using System.Text;


namespace HRMDAL.H.KeyNames
{
    public sealed class CoefficientKeys
    {
        /// <summary>
        /// Some field names of Coefficient table
        /// </summary>
        public const string FIELD_COEFFICIENT_ID = "CoefficientId";
        public const string FIELD_COEFFICIENT_VALUE = "Value";
        public const string FIELD_COEFFICIENT_DATE = "Date";
       
        /// <summary>
        /// Some StoreProcedure names  of Coefficient table.
        /// </summary>
        public const string SP_COEFFICIENT_GETALL_BY_USERID_MONTHLY = "Sel_H_Coefficient_ByUserId_Monthly";

        public const string SP_COEFFICIENT_INSERT = "Ins_H_Coefficient";
        
    }
}
