using System;

namespace HRMUtil
{
    public class LeaveDate
    {
        public LeaveDate(DateTime startTime, DateTime endTime, double days)
        {
            StartTime = startTime;
            EndTime = endTime;
            Days = days;
        }


        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public double Days { get; set; }
    }
}