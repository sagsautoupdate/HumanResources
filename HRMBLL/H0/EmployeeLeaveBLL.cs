using System;
using System.Collections.Generic;
using System.Data;
using HRMBLL.BLLHelper;
using HRMDAL.H0;
using HRMUtil.KeyNames.H0;

namespace HRMBLL.H0
{
    public class EmployeeLeaveBLL
    {
        #region private fields

        private string _SP = "";
        private string _SPValue = "";

        #endregion

        #region properties

        public long EmployeeLeaveId { get; set; }

        public int UserId { get; set; }

        public int LeaveTypeId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public double Days { get; set; }

        public long GroupId { get; set; }

        public string Remark { get; set; }

        public string LeaveTypeName { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string DepartmentName { get; set; } = string.Empty;

        public string DepartmentFullName { get; set; }

        public string RootName { get; set; }

        #endregion

        #region constructor

        public EmployeeLeaveBLL()
        {
        }

        public EmployeeLeaveBLL(long employeeLeaveId, int userId, int leaveTypeId, DateTime fromDate, DateTime toDate,
            string remark)
        {
            FullName = string.Empty;
            DepartmentName = string.Empty;
            EmployeeLeaveId = employeeLeaveId;
            UserId = userId;
            LeaveTypeId = leaveTypeId;
            FromDate = fromDate;
            ToDate = toDate;
            Remark = remark;
        }

        public EmployeeLeaveBLL(long employeeLeaveId, int userId, int leaveTypeId, DateTime fromDate, DateTime toDate,
            double days, string remark)
        {
            FullName = string.Empty;
            DepartmentName = string.Empty;
            EmployeeLeaveId = employeeLeaveId;
            UserId = userId;
            LeaveTypeId = leaveTypeId;
            FromDate = fromDate;
            ToDate = toDate;
            Days = days;
            GroupId = 0L;
            Remark = remark;
        }

        #endregion

        #region public methods Get

        public static DataTable GetDTByDeptId_Date(string deptIds, int month, int year, int leaveCode)
        {
            var objDAL = new EmployeeLeaveDAL();
            return objDAL.GetByDeptId_Date(deptIds, month, year, leaveCode);
        }

        public static DataTable GetDTByUserId_Date(int userId, int month, int year)
        {
            var objDAL = new EmployeeLeaveDAL();
            return objDAL.GetByUserId_Date(userId, month, year);
        }

        public static DataTable GetByUserId_LeaveTypeId_DateToDT(int userId, int leaveTypeId, int month, int year)
        {
            var objDAL = new EmployeeLeaveDAL();
            return objDAL.GetByUserId_LeaveType_Date(userId, leaveTypeId, month, year);
        }

        public static DataTable GetByUserIds_Date(int userId, int empLeaveId)
        {
            var objDAL = new EmployeeLeaveDAL();
            return objDAL.GetByUserIds_Date(userId, empLeaveId);
        }

        public static DataTable DT_GetByDeptId_Total(string deptIds, int Month, int Year)
        {
            return new EmployeeLeaveDAL().GetByDeptId_Total(deptIds, Month, Year);
        }

        public static DataRow DR_GetEmpLeaveDetail(int month, int year, int userId)
        {
            var table = new EmployeeLeaveDAL().GetEmpLeaveDetail(month, year, userId);
            if (table.Rows.Count > 0)
                return table.Rows[0];
            return null;
        }

        public static List<EmployeeLeaveBLL> GetByFilter(string fullName, string departmentIds, int? leaveTypeId,
            int? month, int? year)
        {
            return
                GenerateListEmployeeLeaveBLLFromDataTable(new EmployeeLeaveDAL().GetByFilter(fullName, departmentIds,
                    leaveTypeId, month, year));
        }

        public static List<EmployeeLeaveBLL> GetByUserId_Date(int userId, int month, int year)
        {
            var objDAL = new EmployeeLeaveDAL();
            return GenerateListEmployeeLeaveBLLFromDataTable(objDAL.GetByUserId_Date(userId, month, year));
        }

        public static List<EmployeeLeaveBLL> GetByUserId_Year_LeaveTypeId(int userId, int leaveTypeId, int year,
            long groupId)
        {
            var objDAL = new EmployeeLeaveDAL();
            return
                GenerateListEmployeeLeaveBLLFromDataTable(objDAL.GetByUserId_LeaveType_Year(userId, leaveTypeId, year,
                    groupId));
        }

        public static List<EmployeeLeaveBLL> GetByUserId_LeaveTypeId_Date(int userId, int leaveTypeId, int month,
            int year)
        {
            var objDAL = new EmployeeLeaveDAL();
            return
                GenerateListEmployeeLeaveBLLFromDataTable(objDAL.GetByUserId_LeaveType_Date(userId, leaveTypeId, month,
                    year));
        }

        public static List<EmployeeLeaveBLL> GetAllById(long employeeLeaveId)
        {
            var objDAL = new EmployeeLeaveDAL();
            return GenerateListEmployeeLeaveBLLFromDataTable(objDAL.GetAllById(employeeLeaveId));
        }

        public static List<EmployeeLeaveBLL> GetByDeptId_Date(string deptIds, int month, int year)
        {
            var objDAL = new EmployeeLeaveDAL();
            return GenerateListEmployeeLeaveBLLFromDataTable(objDAL.GetByDeptId_Date(deptIds, month, year, 0));
        }

        public static EmployeeLeaveBLL GetById(long employeeLeaveId)
        {
            var list = GenerateListEmployeeLeaveBLLFromDataTable(new EmployeeLeaveDAL().GetById(employeeLeaveId));
            if (list.Count == 1)
                return list[0];
            return null;
        }

        public static DataRow GetDRById(long employeeLeaveId)
        {
            var table = new EmployeeLeaveDAL().GetById(employeeLeaveId);
            if (table.Rows.Count > 0)
                return table.Rows[0];
            return null;
        }

        public static EmployeeLeaveBLL GetByRecordTotal(int userId, int month, int year, int leaveTypeId, long groupId)
        {
            var objDAL = new EmployeeLeaveDAL();
            var objBLL = new EmployeeLeaveBLL();
            var list =
                GenerateListEmployeeLeaveBLLFromDataTable(objDAL.GetByRecordTotal(userId, month, year, leaveTypeId,
                    groupId));
            foreach (var bll in list)
            {
                objBLL.Days = objBLL.Days + bll.Days;
                objBLL.LeaveTypeId = bll.LeaveTypeId;
            }
            return objBLL;
        }

        public static DataTable DT_GetByFilter(string fullName, string departmentIds, int? leaveTypeId, int? month,
            int? year)
        {
            return new EmployeeLeaveDAL().GetByFilter(fullName, departmentIds, leaveTypeId, month, year);
        }

        public static DataTable DT_GetByUserId_Date(int userId, int month, int year)
        {
            var objDAL = new EmployeeLeaveDAL();
            return objDAL.GetByUserId_Date(userId, month, year);
        }

        public static DataTable DT_GetByUserIds_Date(string userId, int month, int year, string empLeaveId)
        {
            var objDAL = new EmployeeLeaveDAL();
            return objDAL.GetByUserIds_DateV1(userId, month, year, empLeaveId);
        }

        public static DataTable DT_GetByUserIds(int userId, int empLeaveId)
        {
            var objDAL = new EmployeeLeaveDAL();
            return objDAL.GetByUserIds_Date(userId, empLeaveId);
        }

        public static DataTable DT_GetByUserId_LeaveTypeId_Date(int userId, int leaveTypeId, int month, int year)
        {
            var objDAL = new EmployeeLeaveDAL();
            return objDAL.GetByUserId_LeaveType_Date(userId, leaveTypeId, month, year);
        }

        public static DataTable DT_GetByDeptId_Date(string deptIds, int month, int year, int leaveCode)
        {
            var objDAL = new EmployeeLeaveDAL();
            return objDAL.GetByDeptId_Date(deptIds, month, year, leaveCode);
        }

        //public static DataTable DT_GetByDeptId_Date_Include_Total(string deptIds, int Month, int Year, int leaveTypeId)
        //{
        //    EmployeeLeaveDAL objDAL = new EmployeeLeaveDAL();
        //    return (objDAL.DT_GetByDeptId_Date_Include_Total(deptIds, Month, Year, leaveTypeId));
        //}
        public static DataTable DT_GetByDeptId_Date_Include_Total(string deptIds, int month, int year)
        {
            var objDAL = new EmployeeLeaveDAL();
            return objDAL.DT_GetByDeptId_Date_Include_Total(deptIds, month, year);
        }

        public static DataTable DT_GetByDeptId_DateForExporting(string deptIds, int month, int year, int leaveCode)
        {
            var objDAL = new EmployeeLeaveDAL();
            return objDAL.GetDTByDeptId_DateForExporting(deptIds, month, year, leaveCode);
        }

        public static DataRow DR_GetByUserId_Date(int userId, int month, int year)
        {
            var dt = new EmployeeLeaveDAL().GetByUserId_Date(userId, month, year);
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        public static DataRow DR_GetByUserId_LeaveTypeId_Date(int userId, int leaveTypeId, DateTime fromDate,
            DateTime toDate)
        {
            var dt = new EmployeeLeaveDAL().GetByUserId_LeaveType_DateV1(userId, leaveTypeId, fromDate, toDate);
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        public static DataRow GetMaxNP()
        {
            var dt = new EmployeeLeaveDAL().GetMaxNP();
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        #endregion

        #region public methods Insert, Update, Delete

        public string ReturnSP()
        {
            return _SP;
        }

        public string ReturnSPValue()
        {
            return _SPValue;
        }

        public long Save()
        {
            var objDAL = new EmployeeLeaveDAL();
            var objLeaveTypesBLL = LeaveTypesBLL.GetById(LeaveTypeId);
            Days = Utilities.GetLeaveDaysV1(objLeaveTypesBLL, FromDate, ToDate);
            if (EmployeeLeaveId <= 0)
            {
                _SP = $"Ins_H0_EmployeeLeaveV1";
                _SPValue =
                    $"LeaveTypeId: {LeaveTypeId}, UserId: {UserId}, FromDate: '{FromDate}', ToDate: '{ToDate}', Days: {Days}, GroupId: {GroupId}, Remark: N'{Remark}', Status: {4}";
                return objDAL.Insert(UserId, LeaveTypeId, FromDate, ToDate, Days, GroupId, Remark);
            }
            _SP = $"Upd_H0_EmployeeLeave";
            _SPValue =
                $"LeaveTypeId: {LeaveTypeId}, UserId: {UserId}, FromDate: '{FromDate}', ToDate: '{ToDate}', Days: {Days}, GroupId: {GroupId}, Remark: N'{Remark}', EmployeeLeaveId: {EmployeeLeaveId}, Status: {0}";
            return objDAL.Update(UserId, LeaveTypeId, FromDate, ToDate, Days, GroupId, Remark, EmployeeLeaveId);
        }

        public void InsertWithMonths(LeaveTypesBLL objLeaveTypesBLL)
        {
            var list = Utilities.AnalyseLeaveDate(objLeaveTypesBLL, FromDate, ToDate);

            Days = Utilities.GetLeaveDays(objLeaveTypesBLL, FromDate, ToDate);
            var objDAL = new EmployeeLeaveDAL();
            long returnId = 0;

            returnId = objDAL.Insert(UserId, LeaveTypeId, FromDate, ToDate, Days, GroupId, Remark);
            if (list.Count > 1)
                foreach (var obj in list)
                    if (obj.Days > 0)
                        new EmployeeLeaveDAL().Insert(UserId, LeaveTypeId, obj.StartTime, obj.EndTime, obj.Days,
                            returnId, Remark);
        }

        public static void Update(int userId, int leaveTypeId, DateTime fromDate, DateTime toDate, double days,
            long groupId, string remark, long employeeLeaveId)
        {
            new EmployeeLeaveDAL().DeleteByGroupId(employeeLeaveId);


            var listOld = GetAllById(employeeLeaveId);
            foreach (var objOld in listOld)
            {
                //UpdateWorkdayEmployee(objOld.UserId, objOld.FromDate, objOld.ToDate, Constants.LEAVE_TYPE_X);
            }

            var objLeaveTypesBLL = LeaveTypesBLL.GetById(leaveTypeId);
            //days = days;// Utilities.GetLeaveDays(objLeaveTypesBLL, fromDate, toDate);
            new EmployeeLeaveDAL().Update(userId, leaveTypeId, fromDate, toDate, days, groupId, remark, employeeLeaveId);
            var list = Utilities.AnalyseLeaveDate(objLeaveTypesBLL, fromDate, toDate);
            if (list.Count > 1)
                foreach (var obj in list)
                    if (obj.Days > 0)
                        new EmployeeLeaveDAL().Insert(userId, leaveTypeId, obj.StartTime, obj.EndTime, obj.Days,
                            employeeLeaveId, remark);
        }

        public static void UpdateNoWorkdayEmployee(int userId, int leaveTypeId, DateTime fromDate, DateTime toDate,
            double days, long groupId, string remark, long employeeLeaveId)
        {
            new EmployeeLeaveDAL().DeleteByGroupId(employeeLeaveId);

            var objLeaveTypesBLL = LeaveTypesBLL.GetById(leaveTypeId);
            days = Utilities.GetLeaveDays(objLeaveTypesBLL, fromDate, toDate);
            new EmployeeLeaveDAL().Update(userId, leaveTypeId, fromDate, toDate, days, groupId, remark, employeeLeaveId);
            var list = Utilities.AnalyseLeaveDate(objLeaveTypesBLL, fromDate, toDate);
            if (list.Count > 1)
                foreach (var obj in list)
                    if (obj.Days > 0)
                        new EmployeeLeaveDAL().Insert(userId, leaveTypeId, obj.StartTime, obj.EndTime, obj.Days,
                            employeeLeaveId, remark);
        }

        public static void Delete(long employeeLeaveId)
        {
            var list = GetAllById(employeeLeaveId);
            foreach (var obj in list)
                new EmployeeLeaveDAL().Delete(obj.EmployeeLeaveId);
        }

        public static void DeleteOne(long employeeLeaveId)
        {
            //List<EmployeeLeaveBLL> list = GetAllById(employeeLeaveId);
            //foreach (EmployeeLeaveBLL obj in list)
            //{
            //    new EmployeeLeaveDAL().Delete(obj.EmployeeLeaveId);
            //    //UpdateWorkdayEmployee(obj.UserId, obj.FromDate, obj.ToDate, Constants.LEAVE_TYPE_X);
            //}
            new EmployeeLeaveDAL().Delete(employeeLeaveId);
        }

        public static void DeleteByMonthLeaveType(int month, int leaveType)
        {
            new EmployeeLeaveDAL().DeleteByMonthLeaveType(month, leaveType);
        }

        #endregion

        #region private methods, generate helper methods

        private static List<EmployeeLeaveBLL> GenerateListEmployeeLeaveBLLFromDataTable(DataTable dt)
        {
            var list = new List<EmployeeLeaveBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateEmployeeLeaveBLLFromDataRow(dr));

            return list;
        }

        private static EmployeeLeaveBLL GenerateEmployeeLeaveBLLFromDataRow(DataRow dr)
        {
            var objBLL = new EmployeeLeaveBLL(
                dr[EmployeeLeaveKeys.FIELD_EMPLOYEE_LEAVE_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[EmployeeLeaveKeys.FIELD_EMPLOYEE_LEAVE_ID].ToString()),
                dr[EmployeeKeys.FIELD_EMPLOYEES_USERID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[EmployeeKeys.FIELD_EMPLOYEES_USERID].ToString()),
                dr[LeaveTypeKeys.Field_Leave_Type_Id] == DBNull.Value
                    ? 0
                    : int.Parse(dr[LeaveTypeKeys.Field_Leave_Type_Id].ToString()),
                dr[EmployeeLeaveKeys.FIELD_EMPLOYEE_LEAVE_FROMDATE] == DBNull.Value
                    ? DateTime.MinValue
                    : Convert.ToDateTime(dr[EmployeeLeaveKeys.FIELD_EMPLOYEE_LEAVE_FROMDATE]),
                dr[EmployeeLeaveKeys.FIELD_EMPLOYEE_LEAVE_TODATE] == DBNull.Value
                    ? DateTime.MinValue
                    : Convert.ToDateTime(dr[EmployeeLeaveKeys.FIELD_EMPLOYEE_LEAVE_TODATE]),
                dr[EmployeeLeaveKeys.FIELD_EMPLOYEE_LEAVE_REMARK] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeLeaveKeys.FIELD_EMPLOYEE_LEAVE_REMARK]
            );

            objBLL.LeaveTypeId = dr[LeaveTypeKeys.Field_Leave_Type_Id] == DBNull.Value
                ? 0
                : (int) dr[LeaveTypeKeys.Field_Leave_Type_Id];
            objBLL.LeaveTypeName = dr[LeaveTypeKeys.Field_Leave_Type_Name] == DBNull.Value
                ? string.Empty
                : (string) dr[LeaveTypeKeys.Field_Leave_Type_Name];
            objBLL.Days = dr[EmployeeLeaveKeys.FIELD_EMPLOYEE_LEAVE_DAYS] == DBNull.Value
                ? 0
                : int.Parse(dr[EmployeeLeaveKeys.FIELD_EMPLOYEE_LEAVE_DAYS].ToString());

            try
            {
                objBLL.FullName = dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME];
            }
            catch
            {
            }
            try
            {
                objBLL.DepartmentName = dr[DepartmentKeys.FIELD_DEPARTMENT_NAME] == DBNull.Value
                    ? string.Empty
                    : (string) dr[DepartmentKeys.FIELD_DEPARTMENT_NAME];
            }
            catch
            {
            }
            try
            {
                objBLL.DepartmentName = dr[DepartmentKeys.FIELD_DEPARTMENT_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[DepartmentKeys.FIELD_DEPARTMENT_NAME].ToString();
            }
            catch
            {
            }
            try
            {
                objBLL.DepartmentFullName = dr[DepartmentKeys.FIELD_DEPARTMENT_DEPARTMENTFULLNAME] == DBNull.Value
                    ? string.Empty
                    : dr[DepartmentKeys.FIELD_DEPARTMENT_DEPARTMENTFULLNAME].ToString();
            }
            catch
            {
            }
            try
            {
                objBLL.RootName = dr[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME].ToString();
            }
            catch
            {
            }
            return objBLL;
        }

        //private static void UpdateWorkdayEmployee(int userId, DateTime fromDate, DateTime toDate, int leaveTypeId)
        //{
        //    string symbolTimekeeping = Constants.GetSymbolTimekeeping(leaveTypeId);
        //    WorkdayEmployeesBLL objWE = WorkdayEmployeesBLL.GetByUserId_Month_Year(userId, fromDate.Month, fromDate.Year, Constants.WorkdayEmployees_Status_TimeKeeping_No);

        //    if (objWE != null)
        //    {
        //        DateTime dateTemp = fromDate;
        //        while(dateTemp.CompareTo(toDate) <= 0)
        //        {
        //            switch (dateTemp.Day)
        //            {
        //                case 1:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day1 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day1 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 2:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day2 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day2 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 3:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day3 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day3 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 4:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day4 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day4 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 5:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day5 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day5 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 6:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day6 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day6 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 7:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day7 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day7 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 8:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day8 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day8 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 9:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day9 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day9 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 10:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day10 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day10 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 11:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day11 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day11 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 12:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day12 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day12 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 13:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day13 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day13 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 14:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day14 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day14 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 15:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day15 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day15 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 16:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day16 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day16 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 17:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day17 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day17 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 18:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day18 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day18 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 19:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day19 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day19 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 20:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day20 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day20 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 21:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day21 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day21 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 22:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day22 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day22 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 23:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day23 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day23 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 24:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day24 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day24 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 25:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day25 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day25 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 26:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day26 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day26 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 27:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day27 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day27 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 28:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day28 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day28 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 29:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day29 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day29 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 30:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day30 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day30 = symbolTimekeeping;
        //                    }
        //                    break;
        //                case 31:
        //                    if (dateTemp.DayOfWeek == DayOfWeek.Sunday)
        //                    {
        //                        objWE.Day31 = Constants.LEAVE_TYPE_NGHI_TUAN_CODE;
        //                    }
        //                    else
        //                    {
        //                        objWE.Day31 = symbolTimekeeping;
        //                    }
        //                    break;
        //            }
        //            dateTemp = dateTemp.AddDays(1);
        //        }
        //        ////////////////////////////////////////////////
        //        objWE.F_Om = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_O_BAN_THAN);

        //        objWE.F_OmDaiNgay = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_O_DAI_NGAY);

        //        objWE.F_ThaiSan = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_THAI_SAN);

        //        objWE.F_TNLD = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_TNLD);

        //        objWE.F_Nam = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_F_NAM);

        //        objWE.F_db = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_FDB);

        //        objWE.F_KoLuongCLD = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO);

        //        objWE.F_KoLuongKLD = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO);

        //        objWE.F_DiDuong = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_F_DI_DUONG);

        //        objWE.F_CongTac = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_F_CONG_TAC);

        //        objWE.F_Hoc1 = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_HOC_1);
        //        objWE.F_Hoc2 = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_HOC_2);
        //        objWE.F_Hoc3 = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_HOC_3);
        //        objWE.F_Hoc4 = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_HOC_4);
        //        objWE.F_Hoc5 = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_HOC_5);
        //        objWE.F_Hoc6 = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_HOC_6);
        //        objWE.F_Hoc7 = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_HOC_7);

        //        objWE.F_Con_Om = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_CON_OM);

        //        objWE.F_KHHDS = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_KHHDS);

        //        objWE.F_SayThai = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_SAY_THAI);

        //        objWE.F_KhamThai = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_KHAM_THAI);

        //        objWE.F_ConChet = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_CON_CHET_SAU_KHI_SINH);

        //        objWE.F_DinhChiCongTac = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_DINH_CHI_CONG_TAC);

        //        objWE.F_TamHoanHD = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_TAM_HOAN_HOP_DONG);

        //        objWE.F_HoiHop = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_HOI_HOP);

        //        objWE.F_Le = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_LE_TET);

        //        objWE.NghiTuan = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_NGHI_TUAN);

        //        objWE.NghiBu = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_NGHI_BU);

        //        objWE.NghiViec = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_NGHI_VIEC);

        //        double x = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_X);

        //        double nuax = HRMBLL.H1.Helper.DefaultValues.CalculateLeaveDay(objWE.Day1, objWE.Day2, objWE.Day3, objWE.Day4, objWE.Day5, objWE.Day6, objWE.Day7, objWE.Day8, objWE.Day9, objWE.Day10, objWE.Day11, objWE.Day12, objWE.Day13,
        //            objWE.Day14, objWE.Day15, objWE.Day16, objWE.Day17, objWE.Day18, objWE.Day19, objWE.Day20, objWE.Day21, objWE.Day22, objWE.Day23, objWE.Day24, objWE.Day25, objWE.Day26,
        //            objWE.Day27, objWE.Day28, objWE.Day29, objWE.Day30, objWE.Day31, Constants.LEAVE_TYPE_1_2_X);

        //        if (nuax > 0)
        //        {
        //            objWE.NC_LamViec = x + (nuax / 2);
        //        }
        //        else
        //        {
        //            objWE.NC_LamViec = x;
        //        }

        //        objWE.UpdateByDate_UserId();
        //        ///////////////////////////////////////////////
        //    }
        //}

        #endregion
    }
}