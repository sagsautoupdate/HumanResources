namespace HRMUtil.KeyNames.H0
{
    public sealed class LeaveTypeKeys
    {
        /// <summary>
        ///     Some field names of EmployeeLeaves table
        /// </summary>
        public const string Field_Leave_Type_Id = "LeaveTypeId";

        public const string Field_Leave_Type_Code = "LeaveTypeCode";
        public const string Field_Leave_Type_Name = "LeaveTypeName";
        public const string Field_Leave_Type_Availabledays = "Availabledays";
        public const string Field_Leave_Type_Visible = "Visible";
        public const string Field_Leave_Type_Type = "Type";

        /// <summary>
        ///     StoreProcedure name of EmployeeLeaves object.
        /// </summary>
        public const string Sp_Sel_H0_LeaveTypes_By_Filter = "Sel_H0_LeaveTypes_By_Filter";

        public const string Sp_Leave_Type_GetById = "Sel_H0_LeaveTypes_By_Id";
        public const string Sp_Leave_Type_GetDTAll = "Sel_H0_LeaveTypes_All";
    }
}