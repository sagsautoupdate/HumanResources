using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H0;
using HRMUtil.KeyNames.H0;

namespace HRMBLL.H0
{
    public class LeaveTypesBLL
    {
        #region constructor

        public LeaveTypesBLL(int leaveTypeId, string leaveTypeName, int availabledays, bool visible)
        {
            LeaveTypeId = leaveTypeId;
            LeaveTypeName = leaveTypeName;
            Availabledays = availabledays;
            Visible = visible;
        }

        #endregion

        #region private fields

        #endregion

        #region properties

        public int LeaveTypeId { get; set; }

        public string LeaveTypeCode { get; set; }

        public string LeaveTypeName { get; set; }

        public int Availabledays { get; set; }

        public int Type { get; set; }

        public bool Visible { get; set; }

        #endregion

        #region methods GET

        public static List<LeaveTypesBLL> GetByFilter(string leaveTypeCode, string leaveTypeName, int type)
        {
            var objDAL = new LeaveTypesDAL();
            return GenerateListLeaveTypesBLLFromDataTable(objDAL.GetByFilter(leaveTypeCode, leaveTypeName, type));
        }

        public static LeaveTypesBLL GetById(int leaveTypeId)
        {
            var objDAL = new LeaveTypesDAL();

            var list = new List<LeaveTypesBLL>();

            list = GenerateListLeaveTypesBLLFromDataTable(objDAL.GetById(leaveTypeId));
            if (list.Count == 1)
                return list[0];
            return null;
        }

        public static LeaveTypesBLL GetByName(string leaveTypeName)
        {
            var objDAL = new LeaveTypesDAL();

            var list = new List<LeaveTypesBLL>();

            list = GenerateListLeaveTypesBLLFromDataTable(objDAL.GetByName(leaveTypeName));
            if (list.Count == 1)
                return list[0];
            return null;
        }

        public static DataTable GetDtAll()
        {
            return new LeaveTypesDAL().GetAll();
        }

        #endregion

        #region private methods

        private static List<LeaveTypesBLL> GenerateListLeaveTypesBLLFromDataTable(DataTable dt)
        {
            var list = new List<LeaveTypesBLL>();
            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateLeaveTypesBLLFromDataRow(dr));
            return list;
        }


        private static LeaveTypesBLL GenerateLeaveTypesBLLFromDataRow(DataRow dr)
        {
            var objBLL = new LeaveTypesBLL(
                dr[LeaveTypeKeys.Field_Leave_Type_Id] == DBNull.Value ? 0 : (int) dr[LeaveTypeKeys.Field_Leave_Type_Id],
                dr[LeaveTypeKeys.Field_Leave_Type_Name] == DBNull.Value
                    ? string.Empty
                    : (string) dr[LeaveTypeKeys.Field_Leave_Type_Name],
                dr[LeaveTypeKeys.Field_Leave_Type_Availabledays] == DBNull.Value
                    ? 0
                    : (int) dr[LeaveTypeKeys.Field_Leave_Type_Availabledays],
                dr[LeaveTypeKeys.Field_Leave_Type_Visible] == DBNull.Value
                    ? false
                    : Convert.ToBoolean(dr[LeaveTypeKeys.Field_Leave_Type_Visible].ToString())
            );

            objBLL.LeaveTypeCode = dr[LeaveTypeKeys.Field_Leave_Type_Code] == DBNull.Value
                ? string.Empty
                : (string) dr[LeaveTypeKeys.Field_Leave_Type_Code];
            objBLL.Type = dr[LeaveTypeKeys.Field_Leave_Type_Type] == DBNull.Value
                ? 0
                : int.Parse(dr[LeaveTypeKeys.Field_Leave_Type_Type].ToString());

            return objBLL;
        }

        #endregion
    }
}