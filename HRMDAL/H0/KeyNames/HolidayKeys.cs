using System;
using System.Text;

namespace HRMDAL.H0.KeyNames
{
    public sealed class HolidayKeys
    {
        /// <summary>
        /// Some field names of Holidays table
        /// </summary>
        public const string FIELD_HOLIDAYS_ID = "HolidayId";
        public const string FIELD_HOLIDAYS_NAME = "HolidayName";
        public const string FIELD_HOLIDAYS_DATE = "HolidayDate";        

        /// <summary>
        /// StoreProcedure name  of Holidays object.
        /// </summary>
        /// 

        public const string SP_HOLIDAYS_GETALL = "Sel_H0_Holidays_All";
        public const string SP_HOLIDAYS_GET_BY_YEAR = "Sel_H0_Holidays_By_Year";

        public const string SP_HOLIDAYS_INSERT = "Ins_H0_Holidays";
        public const string SP_HOLIDAYS_UPDATE = "Upd_H0_Holidays";
        public const string SP_HOLIDAYS_DELETE = "Del_H0_Holidays";
    }
}
