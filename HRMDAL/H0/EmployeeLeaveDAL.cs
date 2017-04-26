using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H0;

namespace HRMDAL.H0
{
    public class EmployeeLeaveDAL : Dao
    {
        #region Insert, Update, Delete

        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns>int</returns>
        public long Insert(int userId, int leaveTypeId, DateTime fromDate, DateTime toDate, double days, long groupId,
            string remark)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@LeaveTypeId", SqlDbType.Int),
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@FromDate", SqlDbType.DateTime),
                    new SqlParameter("@ToDate", SqlDbType.DateTime),
                    new SqlParameter("@Days", SqlDbType.Float),
                    new SqlParameter("@GroupId", SqlDbType.BigInt),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@Status", SqlDbType.Int)
                };

                param[0].Value = leaveTypeId;
                param[1].Value = userId;
                param[2].Value = fromDate;
                param[3].Value = toDate;
                param[4].Value = days;
                param[5].Value = groupId;
                param[6].Value = remark == null ? string.Empty : remark;
                param[7].Value = 4;

                sproc = new StoreProcedure("Ins_H0_EmployeeLeaveV1", param);
                identity = sproc.Run();
                sproc.Commit();
            }
            catch (SqlException se)
            {
                sproc.RollBack();
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }
            return identity;
        }

        public long Update(int userId, int leaveTypeId, DateTime fromDate, DateTime toDate, double days, long groupId,
            string remark, long employeeLeaveId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@LeaveTypeId", SqlDbType.Int),
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@FromDate", SqlDbType.DateTime),
                    new SqlParameter("@ToDate", SqlDbType.DateTime),
                    new SqlParameter("@Days", SqlDbType.Float),
                    new SqlParameter("@GroupId", SqlDbType.BigInt),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@EmployeeLeaveId", SqlDbType.BigInt)
                };

                param[0].Value = leaveTypeId;
                param[1].Value = userId;
                param[2].Value = fromDate;
                param[3].Value = toDate;
                param[4].Value = days;
                param[5].Value = groupId;
                param[6].Value = remark == null ? string.Empty : remark;
                param[7].Value = employeeLeaveId;

                sproc = new StoreProcedure(EmployeeLeaveKeys.SP_EMPLOYEE_LEAVE_UPDATE, param);
                identity = sproc.Run();
                sproc.Commit();
            }
            catch (SqlException se)
            {
                sproc.RollBack();
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return identity;
        }

        public long DeleteByGroupId(long groupId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@GroupId", SqlDbType.BigInt)
                };

                param[0].Value = groupId;

                sproc = new StoreProcedure(EmployeeLeaveKeys.SP_EMPLOYEE_LEAVE_DELETE_BY_GROUPID, param);
                identity = sproc.Run();
                sproc.Commit();
            }
            catch (SqlException se)
            {
                sproc.RollBack();
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return identity;
        }


        public long Delete(long employeeLeaveId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@EmployeeLeaveId", SqlDbType.BigInt)
                };

                param[0].Value = employeeLeaveId;

                sproc = new StoreProcedure(EmployeeLeaveKeys.SP_EMPLOYEE_LEAVE_DELETE, param);
                identity = sproc.Run();
                sproc.Commit();
            }
            catch (SqlException se)
            {
                sproc.RollBack();
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return identity;
        }

        public long DeleteByMonthLeaveType(int month, int leaveType)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@LeaveType", SqlDbType.Int)
                };

                param[0].Value = month;
                param[1].Value = leaveType;

                sproc = new StoreProcedure(EmployeeLeaveKeys.Sp_Del_H0_EmployeeLeave_By_Month_LeaveType, param);
                identity = sproc.Run();
                sproc.Commit();
            }
            catch (SqlException se)
            {
                sproc.RollBack();
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return identity;
        }

        #endregion

        #region Get

        public DataTable GetByUserId_LeaveType_Date(int userId, int leaveTypeId, int month, int year)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@LeaveTypeId", SqlDbType.Int),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int)
                };

                param[0].Value = userId;
                param[1].Value = leaveTypeId;
                param[2].Value = month;
                param[3].Value = year;


                sproc = new StoreProcedure(EmployeeLeaveKeys.SP_EMPLOYEE_LEAVE_GET_BY_USERID_LEAVETYPE_DATE, param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetByUserIds_Date(int userId, int empLeaveId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@EmployeeLeaveId", SqlDbType.Int)
                };

                param[0].Value = userId;
                param[1].Value = empLeaveId;

                sproc = new StoreProcedure(EmployeeLeaveKeys.SP_EMPLOYEE_LEAVE_GET_BY_IDs, param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetByDeptId_Total(string deptIds, int Month, int Year)
        {
            Debug.Assert(sproc == null);
            var table = new DataTable();
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@DeptId", SqlDbType.VarChar, 0xbb8),
                    new SqlParameter("@Month", SqlDbType.Int), new SqlParameter("@Year", SqlDbType.Int)
                };
                parameters[0].Value = deptIds;
                parameters[1].Value = Month;
                parameters[2].Value = Year;
                sproc = new StoreProcedure("Sel_H0_EmployeeLeave_By_DeptId_Include_Total", parameters);
                sproc.RunFill(table);
            }
            catch (SqlException exception)
            {
                throw new HRMException(exception.Message, exception.Number);
            }
            return table;
        }

        public DataTable GetEmpLeaveDetail(int month, int year, int userId)
        {
            Debug.Assert(sproc == null);
            var table = new DataTable();
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int), new SqlParameter("@UserId", SqlDbType.Int)
                };
                parameters[0].Value = month;
                parameters[1].Value = year;
                parameters[2].Value = userId;
                sproc = new StoreProcedure("Sel_H0_EmployeeLeave_By_Detail", parameters);
                sproc.RunFill(table);
            }
            catch (SqlException exception)
            {
                throw new HRMException(exception.Message, exception.Number);
            }
            return table;
        }

        public DataTable GetByFilter(string FullName, string DepartmentIds, int? LeaveTypeId, int? Month, int? Year)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@DepartmentIds", SqlDbType.VarChar, 1000),
                    new SqlParameter("@LeaveTypeId", SqlDbType.Int, 4),
                    new SqlParameter("@Date", SqlDbType.SmallDateTime)
                };

                if (FullName == null)
                    param[0].Value = DBNull.Value;
                else
                    param[0].Value = FullName;
                if (DepartmentIds != null)
                    param[1].Value = DepartmentIds;
                else
                    param[1].Value = DBNull.Value;
                if (LeaveTypeId.HasValue)
                    param[2].Value = LeaveTypeId.Value;
                else
                    param[2].Value = DBNull.Value;
                if ((Month > 0) && (Year > 0))
                {
                    var date = new DateTime((int) Year, (int) Month, 1);
                    param[3].Value = date;
                }
                else
                {
                    param[3].Value = DBNull.Value;
                }
                //if ((Year.HasValue == true))
                //{
                //    param[4].Value = ((int)(Year.Value));
                //}
                //else
                //{
                //    param[4].Value = System.DBNull.Value;
                //}

                sproc = new StoreProcedure(EmployeeLeaveKeys.SP_EMPLOYEE_LEAVE_GET_BY_FILTER, param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetById(long employeeLeaveId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@EmployeeLeaveId", SqlDbType.Int)
                };

                param[0].Value = employeeLeaveId;

                sproc = new StoreProcedure(EmployeeLeaveKeys.SP_EMPLOYEE_LEAVE_GET_BY_ID, param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetAllById(long employeeLeaveId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@EmployeeLeaveId", SqlDbType.Int)
                };

                param[0].Value = employeeLeaveId;

                sproc = new StoreProcedure(EmployeeLeaveKeys.Sp_EmployeeLeave_All_By_Id, param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetByUserId_Date(int userId, int month, int year)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int)
                };

                param[0].Value = userId;
                param[1].Value = month;
                param[2].Value = year;

                sproc = new StoreProcedure(EmployeeLeaveKeys.SP_EMPLOYEE_LEAVE_GET_BY_USERID_DATE, param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetByUserIds_DateV1(string userId, int month, int year, string empLeaveId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.NVarChar, 4000),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int),
                    new SqlParameter("@EmployeeLeaveId", SqlDbType.NVarChar, 4000)
                };

                param[0].Value = userId;
                param[1].Value = month;
                param[2].Value = year;
                param[3].Value = empLeaveId;

                sproc = new StoreProcedure("Sel_H0_EmployeeLeave_By_UserIds_Date", param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetByRecordTotal(int userId, int month, int year, int leaveTypeId, long groupId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int),
                    new SqlParameter("@LeaveTypeId", SqlDbType.Int),
                    new SqlParameter("@GroupId", SqlDbType.BigInt)
                };

                param[0].Value = userId;
                param[1].Value = month;
                param[2].Value = year;
                param[3].Value = leaveTypeId;
                param[4].Value = groupId;

                sproc = new StoreProcedure(EmployeeLeaveKeys.SP_EMPLOYEE_LEAVE_GET_BY_RECORD_TOTAL, param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetByUserId_LeaveType_Year(int userId, int leaveTypeId, int year, long groupId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@LeaveTypeId", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int),
                    new SqlParameter("@GroupId", SqlDbType.BigInt)
                };

                param[0].Value = userId;
                param[1].Value = leaveTypeId;
                param[2].Value = year;
                param[3].Value = groupId;

                sproc = new StoreProcedure(EmployeeLeaveKeys.SP_EMPLOYEE_LEAVE_GET_BY_USERID_LEAVETYPE_YEAR, param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetByUserId_LeaveType_DateV1(int userId, int leaveTypeId, DateTime fromDate, DateTime toDate)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@LeaveTypeId", SqlDbType.Int),
                    new SqlParameter("@FromDate", SqlDbType.DateTime),
                    new SqlParameter("@ToDate", SqlDbType.DateTime)
                };

                param[0].Value = userId;
                param[1].Value = leaveTypeId;
                param[2].Value = fromDate;
                param[3].Value = toDate;


                sproc = new StoreProcedure("Sel_H0_EmployeeLeave_By_UserId_LeaveType_DateV1", param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetByDeptId_Date(string deptIds, int month, int year, int leaveCode)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DeptIds", SqlDbType.VarChar, 1000),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int),
                    new SqlParameter("@LeaveCodeId", SqlDbType.Int)
                };

                param[0].Value = deptIds;
                param[1].Value = month;
                param[2].Value = year;
                param[3].Value = leaveCode;

                sproc = new StoreProcedure(EmployeeLeaveKeys.Sp_Sel_H0_EmployeeLeave_By_DeptId_Date, param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable DT_GetByDeptId_Date_Include_Total(string deptIds, int month, int year)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DeptId", SqlDbType.VarChar, 3000),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int)
                };

                param[0].Value = deptIds;
                param[1].Value = month;
                param[2].Value = year;

                sproc = new StoreProcedure(EmployeeLeaveKeys.SP_EMPLOYEELEAVE_BY_DEPTID_INCLUDE_TOTALV1, param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        //public DataTable DT_GetByDeptId_Date_Include_Total(string deptIds, int Month, int Year, int leaveTypeId)
        //{

        //    Debug.Assert(sproc == null);

        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        SqlParameter[] param =
        //        {
        //            new SqlParameter("@DeptId",SqlDbType.VarChar, 3000),
        //            new SqlParameter("@Month",SqlDbType.Int),
        //            new SqlParameter("@Year",SqlDbType.Int),
        //            new SqlParameter("@LeaveTypeId",SqlDbType.Int),

        //        };

        //        param[0].Value = deptIds;
        //        param[1].Value = Month;
        //        param[2].Value = Year;
        //        param[3].Value = leaveTypeId;

        //        sproc = new StoreProcedure(EmployeeLeaveKeys.SP_EMPLOYEELEAVE_BY_DEPTID_INCLUDE_TOTAL, param);
        //        sproc.RunFill(dt);
        //    }
        //    catch (SqlException se)
        //    {
        //        throw new HRMException(se.Message, se.Number);
        //    }

        //    return dt;

        //}
        public DataTable GetDTByDeptId_DateForExporting(string deptIds, int month, int year, int leaveCode)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DeptIds", SqlDbType.VarChar, 1000),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int),
                    new SqlParameter("@LeaveCodeId", SqlDbType.Int)
                };

                param[0].Value = deptIds;
                param[1].Value = month;
                param[2].Value = year;
                param[3].Value = leaveCode;

                sproc = new StoreProcedure("Sel_H0_EmployeeLeave_By_DeptId_Date_For_Exporting", param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetMaxNP()
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                sproc = new StoreProcedure("Sel_H0_Employee_Leave_By_MaxLeaveNumber", null);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        #endregion
    }
}