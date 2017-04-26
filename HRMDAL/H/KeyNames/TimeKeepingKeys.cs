using System;
using System.Text;


namespace HRMDAL.H.KeyNames
{
    public sealed class TimeKeepingKeys
    {
        /// <summary>
        /// Some field names of Coefficient table
        /// </summary>
        public const string FIELD_TIME_KEEPING_ID = "TimeKeepingId";
        public const string FIELD_TIME_KEEPING_VALUE = "Value";
        public const string FIELD_TIME_KEEPING_DATE = "TimeKeepingDate";
        public const string FIELD_TIME_KEEPING_LOCK = "Lock";

        /// <summary>
        /// Some StoreProcedure names  of Coefficient table.
        /// </summary>
        public const string SP_TIME_KEEPING_GETALL_BY_USERID_MONTHLY = "Sel_H_TimeKeeping_ByUserId_Monthly";

        public const string SP_TIME_KEEPING_INSERT = "Ins_H_TimeKeeping";
    }
}
