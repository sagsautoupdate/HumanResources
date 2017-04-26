using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H;
using HRMUtil;
using HRMUtil.KeyNames.H;

namespace HRMBLL.H
{
    public class CommandLogBLL
    {
        #region methods insert, update, delete

        public long Save()
        {
            return new CommandLogDAL().Insert(CommandTypeId, DataName, UserId, OldValues, NewValues, CommandLogDate,
                ModuleId);
        }

        #endregion

        #region private fields

        #endregion

        #region properties

        public long CommandLogId { get; set; }

        public int CommandTypeId { get; set; }

        public int UserId { get; set; }

        public string OldValues { get; set; }

        public string NewValues { get; set; }

        public DateTime CommandLogDate { get; set; }

        public int ModuleId { get; set; }


        public string FullName { get; set; }

        public string DataName { get; set; }

        public string CommandTypeName { get; set; }

        #endregion

        #region constructor

        public CommandLogBLL()
        {
        }

        public CommandLogBLL(long commandLogId, int commandTypeId, string dataName, int userId, string oldValues,
            string newValues, DateTime commandLogDate)
        {
            CommandLogId = commandLogId;
            CommandTypeId = commandTypeId;
            DataName = dataName;
            UserId = userId;
            OldValues = oldValues;
            NewValues = newValues;
            CommandLogDate = commandLogDate;
        }

        #endregion

        #region public static method Get

        public static List<CommandLogBLL> GetByFilter(string dataName, int commandTypeId, int userId, int day, int month,
            int year, int moduleId)
        {
            return
                GenerateListCommandLogBLLFromDataTable(new CommandLogDAL().GetByFilter(dataName, commandTypeId, userId,
                    day, month, year, moduleId));
        }

        public static List<CommandLogBLL> GetByDateName(string dataName)
        {
            return GenerateListCommandLogBLLFromDataTable(new CommandLogDAL().GetByDataName(dataName));
        }

        public static List<CommandLogBLL> GetByFromToDate(int CommandTypeId, DateTime FromDate, DateTime ToDate,
            int ModuleId)
        {
            return
                GenerateListCommandLogBLLFromDataTable(new CommandLogDAL().GetByFromToDateModule(CommandTypeId, FromDate,
                    ToDate, ModuleId));
        }

        #endregion

        #region private static methods

        private static List<CommandLogBLL> GenerateListCommandLogBLLFromDataTable(DataTable dt)
        {
            var list = new List<CommandLogBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateListCommandLogBLLFromDataRow(dr));

            return list;
        }

        private static CommandLogBLL GenerateListCommandLogBLLFromDataRow(DataRow dr)
        {
            var objBLL = new CommandLogBLL(
                dr[CommandLogKeyNames.Field_Command_Log_Id] == DBNull.Value
                    ? 0
                    : long.Parse(dr[CommandLogKeyNames.Field_Command_Log_Id].ToString()),
                dr[CommandLogKeyNames.Field_Command_Log_CommandTypeId] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CommandLogKeyNames.Field_Command_Log_CommandTypeId].ToString()),
                dr[CommandLogKeyNames.Field_Command_Log_DataName] == DBNull.Value
                    ? string.Empty
                    : dr[CommandLogKeyNames.Field_Command_Log_DataName].ToString(),
                dr[CommandLogKeyNames.Field_Command_Log_UserId] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CommandLogKeyNames.Field_Command_Log_UserId].ToString()),
                dr[CommandLogKeyNames.Field_Command_Log_OldValues] == DBNull.Value
                    ? string.Empty
                    : dr[CommandLogKeyNames.Field_Command_Log_OldValues].ToString(),
                dr[CommandLogKeyNames.Field_Command_Log_NewValues] == DBNull.Value
                    ? string.Empty
                    : dr[CommandLogKeyNames.Field_Command_Log_NewValues].ToString(),
                dr[CommandLogKeyNames.Field_Command_Log_CommandLogDate] == DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(dr[CommandLogKeyNames.Field_Command_Log_CommandLogDate].ToString())
            );

            objBLL.ModuleId = dr[CommandLogKeyNames.Field_CommandLog_ModuleId] == DBNull.Value
                ? 0
                : int.Parse(dr[CommandLogKeyNames.Field_CommandLog_ModuleId].ToString());

            objBLL.FullName = dr[CommandLogKeyNames.Field_Command_Log_FullName] == DBNull.Value
                ? string.Empty
                : dr[CommandLogKeyNames.Field_Command_Log_FullName].ToString();
            objBLL.CommandTypeName = Constants.GetlCommandTypeNameById(objBLL.CommandTypeId);

            return objBLL;
        }

        #endregion
    }
}