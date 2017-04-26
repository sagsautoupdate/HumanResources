namespace HRMUtil.KeyNames.H
{
    public sealed class CommandLogKeyNames
    {
        /// <summary>
        ///     Some field names of CommandLog table
        /// </summary>
        public const string Field_Command_Log_Id = "CommandLogId";

        public const string Field_Command_Log_CommandTypeId = "CommandTypeId";
        public const string Field_Command_Log_CommandTypeName = "CommandTypeName";
        public const string Field_Command_Log_DataName = "DataName";
        public const string Field_Command_Log_UserId = "UserId";
        public const string Field_Command_Log_FullName = "FullName";
        public const string Field_Command_Log_OldValues = "OldValues";
        public const string Field_Command_Log_NewValues = "NewValues";
        public const string Field_Command_Log_CommandLogDate = "CommandLogDate";
        public const string Field_CommandLog_ModuleId = "ModuleId";


        /// <summary>
        ///     StoreProcedure name of CommandLog object.
        /// </summary>
        public const string Sp_Sel_CommandLog_By_Filter = "Sel_CommandLog_By_Filter";

        public const string Sp_Sel_CommandLog_By_DataName = "Sel_CommandLog_By_DataName";
        public const string Sp_Sel_CommandLog_By_FromToDateModule = "Sel_CommandLog_By_FromToDateModule";

        public const string Sp_Ins_CommandLog = "Ins_CommandLog";
    }
}