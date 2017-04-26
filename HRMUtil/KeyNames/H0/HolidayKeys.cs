namespace HRMUtil.KeyNames.H0
{
    public sealed class HolidayKeys
    {
        /// <summary>
        ///     Some field names of Holidays table
        /// </summary>
        public const string FIELD_HOLIDAY_ID = "HolidayId";

        public const string FIELD_HOLIDAY_NAME = "HolidayName";
        public const string FIELD_HOLIDAY_DATE = "HolidayDate";

        /// <summary>
        ///     StoreProcedure name  of Holidays object.
        /// </summary>
        public const string SP_HOLIDAY_GETALL = "Sel_H0_Holiday_By_All";

        public const string Sp_Sel_H0_Holiday_By_Date = "Sel_H0_Holiday_By_Date";

        public const string Sp_Ins_H0_Holiday = "Ins_H0_Holiday";
        public const string Sp_Upd_H0_Holiday = "Upd_H0_Holiday";
        public const string Sp_Del_H0_Holiday = "Del_H0_Holiday";
    }
}