using System;
using System.Text;

namespace HRMDAL.H0.KeyNames
{
    sealed public class EmployeeLeaveKeys
    {
        /// <summary>
        /// Some field names of LeaveForm table
        /// </summary>
        public const string FIELD_EMPLOYEE_LEAVE_ID = "EmployeeLeaveId";
        public const string FIELD_EMPLOYEE_LEAVE_TYPE_ID = "LeaveTypeId";
        public const string FIELD_EMPLOYEE_LEAVE_FROMDATE = "FromDate";
        public const string FIELD_EMPLOYEE_LEAVE_TODATE = "ToDate";
        public const string FIELD_EMPLOYEE_LEAVE_DAYS = "Days";
        public const string FIELD_EMPLOYEE_LEAVE_GROUP_ID = "GroupId";
        public const string FIELD_EMPLOYEE_LEAVE_REMARK = "Remark";                        

        /// <summary>
        /// StoreProcedure name of LeaveForm object.
        /// </summary>
        public const string SP_EMPLOYEE_LEAVE_INSERT = "Ins_H0_EmployeeLeave";
        public const string SP_EMPLOYEE_LEAVE_UPDATE = "Upd_H0_EmployeeLeave";
        public const string SP_EMPLOYEE_LEAVE_DELETE = "Del_H0_EmployeeLeave";
        public const string SP_EMPLOYEE_LEAVE_DELETE_BY_GROUPID = "Del_H0_EmployeeLeaveByGroupId";

        public const string SP_EMPLOYEE_LEAVE_GET_BY_ID = "Sel_H0_EmployeeLeave_By_Id";
        public const string SP_EMPLOYEE_LEAVE_GET_BY_USERID_DATE = "Sel_H0_EmployeeLeave_By_UserId_Date";
        public const string SP_EMPLOYEE_LEAVE_GET_BY_RECORD_TOTAL = "Sel_H0_EmployeeLeave_By_RecordTotal";
        public const string SP_EMPLOYEE_LEAVE_GET_BY_USERID_LEAVETYPE_YEAR = "Sel_H0_EmployeeLeave_By_UserId_LeaveType_Year";
        public const string SP_EMPLOYEE_LEAVE_GET_BY_USERID_LEAVETYPE_DATE = "Sel_H0_EmployeeLeave_By_UserId_LeaveType_Date";
    }
}
