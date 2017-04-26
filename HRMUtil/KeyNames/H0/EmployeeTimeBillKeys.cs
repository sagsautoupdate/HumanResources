namespace HRMUtil.KeyNames.H0
{
    public sealed class EmployeeTimeBillKeys
    {
        /// <summary>
        ///     some fields in H0_EmployeeTimeBill tables
        /// </summary>
        public const string Field_EmployeeTimeBill_UserTimeBillId = "UserTimeBillId";

        public const string Field_EmployeeTimeBill_WorkDate = "WorkDate";
        public const string Field_EmployeeTimeBill_UserId = "UserId";
        public const string Field_EmployeeTimeBill_TimeIn = "TimeIn";
        public const string Field_EmployeeTimeBill_TimeOut = "TimeOut";
        public const string Field_EmployeeTimeBill_TotalMinutes = "TotalMinutes";
        public const string Field_EmployeeTimeBill_TotalHours = "TotalHours";
        public const string Field_EmployeeTimeBill_Status = "Status";
        public const string Field_EmployeeTimeBill_OverTime = "OverTime";


        /// <summary>
        ///     some store procedures H0_EmployeeTimeBill table
        /// </summary>
        public const string Sp_Ins_H0_EmployeeTimeBill = "Ins_H0_EmployeeTimeBill";

        public const string Sp_Upd_H0_EmployeeTimeBill = "Upd_H0_EmployeeTimeBill";
        public const string Sp_Del_H0_EmployeeTimeBill = "Del_H0_EmployeeTimeBill";
        public const string Sp_Sel_H0_EmployeeTimeBillByUserStatus = "Sel_H0_EmployeeTimeBillByUserStatus";

        public const string Sp_Sel_H0_EmployeeTimeBillForDistinctWorkdateByUserId =
            "Sel_H0_EmployeeTimeBillForDistinctWorkdateByUserId";

        public const string Sp_Sel_H0_EmployeeTimeBillByFilter = "Sel_H0_EmployeeTimeBillByFilter";
        public const string Sp_Sel_H0_EmployeeTimeBillByFilterForHo = "Sel_H0_EmployeeTimeBillByFilterForHo";

        public const string Sp_Sel_H0_EmployeeTimeBillByUserDateForWorkingAndHo =
            "Sel_H0_EmployeeTimeBillByUserDateForWorkingAndHo";

        public const string Sp_Sel_H0_EmployeeTimeBillForDistinctWorkdateByUserIdFromToWorkDate =
            "Sel_H0_EmployeeTimeBillForDistinctWorkdateByUserIdFromToWorkDate";

        public const string Sp_Sel_H0_EmployeeTimeBillByRootDate = "Sel_H0_EmployeeTimeBillByRootDate";
        public const string Sp_Sel_DateTimeNow = "Sel_DateTimeNow";
    }
}