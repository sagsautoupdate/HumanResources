using System;
using System.Data.SqlTypes;

namespace HRMBLL.BLLHelper
{
    public static class DefaultValues
    {
        public static DateTime GetBirthdayMinValue()
        {
            var MinValue = (DateTime) SqlDateTime.MinValue;
            MinValue.AddYears(30);
            return MinValue;
        }

        public static DateTime GetJoinDateMinValue()
        {
            var MinValue = (DateTime) SqlDateTime.MinValue;
            MinValue.AddYears(30);
            return MinValue;
        }

        public static DateTime GetSQLDateMinValue()
        {
            var MinValue = (DateTime) SqlDateTime.MinValue;
            MinValue.AddYears(30);
            return MinValue;
        }

        public static int GetStandardLeaveMinValue()
        {
            return 12;
        }
    }
}