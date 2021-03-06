using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H0;

namespace HRMDAL.H0
{
    public class EmployeesDAL : Dao
    {
        #region method Get

        //////////////////////////////////////////////////////////////////
        public DataTable GetEmployeeDeptPositionByDeptId(int deptId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DepartmentId", SqlDbType.Int, 4)
                };
                param[0].Value = deptId;
                sproc = new StoreProcedure(EmployeeKeys.SP_EMPLOYEES_DEPT_POSITION_GET_BY_DEPT_ID, param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return datatable;
        }

        public DataTable GetEmployeeJobDescription(int userid)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4)
                };
                param[0].Value = userid;
                sproc = new StoreProcedure(EmployeeKeys.SP_EMPLOYEES_JOBDESCRIPTION_BY_USERID, param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return datatable;
        }

        public DataTable GetEmployeeDeptPositionByFilter(int deptId, string fullName)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@DepartmentId", SqlDbType.Int, 4)
                };
                param[0].Value = fullName;
                param[1].Value = deptId;
                sproc = new StoreProcedure(EmployeeKeys.SP_EMPLOYEES_DEPT_POSITION_GET_BY_FILTER, param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return datatable;
        }

        /// <summary>
        ///     Author: Giang
        ///     Date: 21-Oct-14
        ///     Content: Tim nhan vien theo ten or ma nv
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public DataTable GetEmployeeForDatatableByNameOrCode(string fullName)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 100)
                };
                param[0].Value = fullName;
                sproc = new StoreProcedure(EmployeeKeys.SP_EMPLOYEES_BY_NAME_OR_CODE, param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return datatable;
        }

        public DataTable GetEmployeeLeaveJobByFilter(int deptId, string fullName)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@DepartmentId", SqlDbType.Int, 4)
                };
                param[0].Value = fullName;
                param[1].Value = deptId;
                sproc = new StoreProcedure(EmployeeKeys.Sp_Sel_H0_Employee_LeaveJob_By_Filter, param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return datatable;
        }

        //////////////////////////////////////////////////////////////////
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public DataTable GetAll(int status)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Status", SqlDbType.Int)
                };
                param[0].Value = status;

                sproc = new StoreProcedure(EmployeeKeys.SP_EMPLOYEES_GETALL, param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }
            return datatable;
        }

        public DataTable GetAllExp()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure("Sel_H0_All_Employees_Exp", null);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }
            return datatable;
        }

        public DataTable GetAll2()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure("Sel_H0_Employee_By_All2", null);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }
            return datatable;
        }

        public DataTable GetForContract()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure("Sel_H0_EmployeeContract_FromView", null);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }
            return datatable;
        }

        public DataTable GetByRootId(int rootId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@RootId", SqlDbType.Int, 4)
                };
                param[0].Value = rootId;
                sproc = new StoreProcedure(EmployeeKeys.SP_EMPLOYEES_GET_BY_ROOT_ID, param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return datatable;
        }

        public DataTable GetByDeptIdForDatatableForLeaveEmp(string DepartmentIds)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            ;
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DepartmentIds", SqlDbType.VarChar, 254)
                };
                param[0].Value = DepartmentIds;

                sproc = new StoreProcedure(EmployeeKeys.SP_EMPLOYEES_LEAVE_GET_BY_DEPT_IDV1, param);
                sproc.RunFill(dt);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetByDeptId(int deptId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DeptId", SqlDbType.Int, 4)
                };
                param[0].Value = deptId;
                sproc = new StoreProcedure(EmployeeKeys.SP_EMPLOYEES_GET_BY_DEPT_ID, param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return datatable;
        }

        /// <summary>
        ///     Author: Giang
        ///     Date: 23-Oct-14
        ///     Content: Lay nhan vien nghi viec
        /// </summary>
        /// <param name="deptIds"></param>
        /// <param name="rootId"></param>
        /// <param name="fullname"></param>
        /// <param name="TypeSort"></param>
        /// <returns></returns>
        public DataTable GetByDeptIds(string deptIds, int rootId, string fullname, int TypeSort)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DeptIds", SqlDbType.VarChar, 254),
                    new SqlParameter("@RootId", SqlDbType.Int),
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@TypeSort", SqlDbType.Int)
                };

                param[0].Value = deptIds;
                param[1].Value = rootId;
                param[2].Value = fullname;
                param[3].Value = TypeSort;

                sproc = new StoreProcedure(EmployeeKeys.Sp_Sel_H0_Employee_By_DeptIds, param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return datatable;
        }

        public DataTable GetOne(int userId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4)
                };
                param[0].Value = userId;
                sproc = new StoreProcedure(EmployeeKeys.SP_EMPLOYEES_GETONE, param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return datatable;
        }

        public DataTable GetOneByUserName(string userName)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserName", SqlDbType.VarChar, 50)
                };
                param[0].Value = userName;
                sproc = new StoreProcedure("Sel_H0_Employee_By_UserName", param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return datatable;
        }

        public DataTable GetOneByUserName2(string userName)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserName", SqlDbType.VarChar, 50)
                };
                param[0].Value = userName;
                sproc = new StoreProcedure("Sel_H0_Employee_By_UserName2", param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return datatable;
        }

        public DataTable GetUserByFilter(int deptId, string fullName)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@DepartmentId", SqlDbType.Int, 4)
                };
                param[0].Value = fullName;
                param[1].Value = deptId;
                sproc = new StoreProcedure(EmployeeKeys.SP_EMPLOYEES_GET_BY_FILTER, param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return datatable;
        }

        public DataTable GetByIds(string userIds)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserIds", SqlDbType.VarChar, 1000)
                };
                param[0].Value = userIds;
                sproc = new StoreProcedure(EmployeeKeys.SP_EMPLOYEES_GET_BY_IDs, param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return datatable;
        }

        public DataTable Login(string userName, string password)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserName", SqlDbType.VarChar, 50),
                    new SqlParameter("@Password", SqlDbType.VarChar, 50)
                };
                param[0].Value = userName;
                param[1].Value = password;

                sproc = new StoreProcedure(EmployeeKeys.SP_EMPLOYEES_LOGIN, param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return datatable;
        }

        public DataTable GetByUserCodeIsNull()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure(EmployeeKeys.SP_EMPLOYEES_GET_BY_USERCODE_IS_NULL, null);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }
            return datatable;
        }

        public DataTable GetByFilterAccountBank(string fullName, int rootId, string accountNo, string CardNo,
            int IsExists)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@RootId", SqlDbType.Int, 4),
                    new SqlParameter("@AccountNo", SqlDbType.VarChar, 50),
                    new SqlParameter("@CardNo", SqlDbType.VarChar, 50),
                    new SqlParameter("@IsExists", SqlDbType.Int)
                };

                param[0].Value = fullName;
                param[1].Value = rootId;
                param[2].Value = accountNo;
                param[3].Value = CardNo;
                param[4].Value = IsExists;

                sproc = new StoreProcedure(EmployeeKeys.Sp_Sel_H0_Employee_By_FilterAccountBank, param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return datatable;
        }

        public DataTable GetByStatus(string fullname, string status)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Status", SqlDbType.NVarChar, 254)
                };

                param[0].Value = fullname;
                param[1].Value = status;

                sproc = new StoreProcedure(EmployeeKeys.Sp_Sel_H0_Employee_By_Status, param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return datatable;
        }

        public DataTable GetByMoreSearch(int? UserId, string UserName, string EmployeeCode, string FullName,
            int? Status, int? MonthOfLeave, int? YearOfLeave,
            int? MonthOfBirth, int? YearOfBirth, int? MonthOfJoinDate,
            int? YearOfJoinDate, int? MonthOfJoinCompanyDate, int? YearOfJoinCompanyDate,
            int? RootId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@UserName", SqlDbType.VarChar, 50),
                    new SqlParameter("@EmployeeCode", SqlDbType.VarChar, 50),
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@Status", SqlDbType.Int, 4),
                    new SqlParameter("@MonthOfLeave", SqlDbType.Int, 4),
                    new SqlParameter("@YearOfLeave", SqlDbType.Int, 4),
                    new SqlParameter("@MonthOfBirth", SqlDbType.Int, 4),
                    new SqlParameter("@YearOfBirth", SqlDbType.Int, 4),
                    new SqlParameter("@MonthOfJoinDate", SqlDbType.Int, 4),
                    new SqlParameter("@YearOfJoinDate", SqlDbType.Int, 4),
                    new SqlParameter("@MonthOfJoinCompanyDate", SqlDbType.Int, 4),
                    new SqlParameter("@YearOfJoinCompanyDate", SqlDbType.Int, 4),
                    new SqlParameter("@RootId", SqlDbType.Int, 4)
                };

                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (UserName == null)
                    param[1].Value = DBNull.Value;
                else
                    param[1].Value = UserName;
                if (EmployeeCode == null)
                    param[2].Value = DBNull.Value;
                else
                    param[2].Value = EmployeeCode;
                if (FullName == null)
                    param[3].Value = DBNull.Value;
                else
                    param[3].Value = FullName;
                if (Status.HasValue)
                    param[4].Value = Status.Value;
                else
                    param[4].Value = DBNull.Value;
                if (MonthOfLeave.HasValue)
                    param[5].Value = MonthOfLeave.Value;
                else
                    param[5].Value = DBNull.Value;
                if (YearOfLeave.HasValue)
                    param[6].Value = YearOfLeave.Value;
                else
                    param[6].Value = DBNull.Value;
                if (MonthOfBirth.HasValue)
                    param[7].Value = MonthOfBirth.Value;
                else
                    param[7].Value = DBNull.Value;
                if (YearOfBirth.HasValue)
                    param[8].Value = YearOfBirth.Value;
                else
                    param[8].Value = DBNull.Value;
                if (MonthOfJoinDate.HasValue)
                    param[9].Value = MonthOfJoinDate.Value;
                else
                    param[9].Value = DBNull.Value;
                if (YearOfJoinDate.HasValue)
                    param[10].Value = YearOfJoinDate.Value;
                else
                    param[10].Value = DBNull.Value;
                if (MonthOfJoinCompanyDate.HasValue)
                    param[11].Value = MonthOfJoinCompanyDate.Value;
                else
                    param[11].Value = DBNull.Value;
                if (YearOfJoinCompanyDate.HasValue)
                    param[12].Value = YearOfJoinCompanyDate.Value;
                else
                    param[12].Value = DBNull.Value;
                if (RootId.HasValue)
                    param[13].Value = RootId.Value;
                else
                    param[13].Value = DBNull.Value;

                sproc = new StoreProcedure(EmployeeKeys.Sp_Sel_H0_Employee_By_MoreSearch, param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return datatable;
        }

        /// <summary>
        ///     Dung them vao ngay 26MAR10
        /// </summary>
        /// <returns></returns>
        public DataTable GetTodayBirthdayEmployees()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param = {};
                sproc = new StoreProcedure(EmployeeKeys.SP_EMPLOYEES_GET_TODAY_BIRTHDAY_LIST, param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return datatable;
        }

        public DataTable GetComboBoxDataByName(string cbName)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@TypeName", SqlDbType.VarChar, 50)
                };
                param[0].Value = cbName;
                sproc = new StoreProcedure(EmployeeKeys.Sp_Sel_H0_Types, param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return datatable;
        }

        public DataTable GetByEmployeeCode(string employeeCode)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@EmployeeCode", SqlDbType.VarChar, 50)
                };
                param[0].Value = employeeCode;
                sproc = new StoreProcedure(EmployeeKeys.Sp_Sel_H0_Employee_By_EmployeeCode, param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return datatable;
        }

        public DataTable GetByStocks(string deptIds, int rootId, string fullname, int TypeSort)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DeptIds", SqlDbType.VarChar, 254),
                    new SqlParameter("@RootId", SqlDbType.Int),
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@TypeSort", SqlDbType.Int)
                };

                param[0].Value = deptIds;
                param[1].Value = rootId;
                param[2].Value = fullname;
                param[3].Value = TypeSort;

                sproc = new StoreProcedure(EmployeeKeys.Sp_Sel_H0_EmployeeByStock, param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return datatable;
        }

        #endregion

        #region methods insert, update, delete

        public long UpdateEmployeePartyInfo(DateTime DateJoinParty, DateTime DateJoinPartyOfficial, string PartyNumber,
            string PlaceJoinParty, int UserId)
        {
            var num = 0L;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@DateJoinParty", SqlDbType.DateTime),
                    new SqlParameter("@DateJoinPartyOfficial", SqlDbType.DateTime),
                    new SqlParameter("@PartyNumber", SqlDbType.VarChar, 50),
                    new SqlParameter("@PlaceJoinParty", SqlDbType.NVarChar, 0xfe),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };
                parameters[0].Value = DateJoinParty;
                parameters[1].Value = DateJoinPartyOfficial;
                parameters[2].Value = PartyNumber;
                parameters[3].Value = PlaceJoinParty;
                parameters[4].Value = UserId;
                sproc = new StoreProcedure("Upd_H0_Employee_Party_Info", parameters);
                num = sproc.Run();
                sproc.Commit();
            }
            catch (SqlException exception)
            {
                sproc.RollBack();
                throw new HRMException(exception.Message, exception.Number);
            }
            finally
            {
                sproc.Dispose();
            }
            return num;
        }

        public long InsertByImporting(string userCode, string fullName, string departmentName, string positionName)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserCode", SqlDbType.VarChar, 50),
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@DepartmentName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@PositionName", SqlDbType.NVarChar, 100)
                };
                param[0].Value = userCode;
                param[1].Value = fullName;
                param[2].Value = departmentName;
                param[3].Value = positionName;


                sproc = new StoreProcedure(EmployeeKeys.SP_EMPLOYEES_INSERT_BY_IMPORTING, param);
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

        #region draft

        public long InsertDefault(string FullName, string fullName2, DateTime BirthDay, bool Sex, string HandPhone,
            string HomePhone, int Status, int DepartmentId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@FullName2", SqlDbType.NVarChar, 100),
                    new SqlParameter("@BirthDay", SqlDbType.DateTime),
                    new SqlParameter("@Sex", SqlDbType.Bit),
                    new SqlParameter("@HandPhone", SqlDbType.VarChar, 50),
                    new SqlParameter("@HomePhone", SqlDbType.VarChar, 50),
                    new SqlParameter("@Status", SqlDbType.Int),
                    new SqlParameter("@DepartmentId", SqlDbType.Int)
                };

                param[0].Value = FullName;
                param[1].Value = fullName2;
                param[2].Value = BirthDay;
                param[3].Value = Sex;
                param[4].Value = HandPhone;
                param[5].Value = HomePhone;
                param[6].Value = Status;
                param[7].Value = DepartmentId;

                sproc = new StoreProcedure(EmployeeKeys.Sp_Ins_H0_Employees_Default, param);
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

        public long UpdateInforGeneral(int sex, bool marriage, DateTime? joinDate, DateTime? joinCompanyDate,
            string workingPhone, string healthInsuranceNo, string healthInsuranceAddress, string socialInsuranceNo,
            int status, DateTime? leaveDate, int userId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Sex", SqlDbType.Int),
                    new SqlParameter("@Marriage", SqlDbType.Bit),
                    new SqlParameter("@JoinDate", SqlDbType.DateTime),
                    new SqlParameter("@JoinCompanyDate", SqlDbType.DateTime),
                    new SqlParameter("@WorkingPhone", SqlDbType.VarChar, 50),
                    new SqlParameter("@HealthInsuranceNo", SqlDbType.VarChar, 50),
                    new SqlParameter("@HealthInsuranceAddress", SqlDbType.NVarChar, 100),
                    new SqlParameter("@SocialInsuranceNo", SqlDbType.VarChar, 50),
                    new SqlParameter("@Status", SqlDbType.Int),
                    new SqlParameter("@LeaveDate", SqlDbType.SmallDateTime),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };
                param[0].Value = sex;
                param[1].Value = marriage;

                if (joinDate.HasValue)
                    if (joinDate.Equals(FormatDate.GetSQLDateMinValue))
                        param[2].Value = DBNull.Value;
                    else
                        param[2].Value = joinDate.Value;
                else
                    param[2].Value = DBNull.Value;
                if (joinCompanyDate.HasValue)
                    if (joinCompanyDate.Equals(FormatDate.GetSQLDateMinValue))
                        param[3].Value = DBNull.Value;
                    else
                        param[3].Value = joinCompanyDate.Value;
                else
                    param[3].Value = DBNull.Value;

                param[4].Value = workingPhone;
                param[5].Value = healthInsuranceNo;
                param[6].Value = healthInsuranceAddress;
                param[7].Value = socialInsuranceNo;
                param[8].Value = status;

                if (leaveDate.HasValue)
                    if (leaveDate.Equals(FormatDate.GetSQLDateMinValue))
                        param[9].Value = DBNull.Value;
                    else
                        param[9].Value = leaveDate.Value;
                else
                    param[9].Value = DBNull.Value;

                param[10].Value = userId;


                sproc = new StoreProcedure(EmployeeKeys.Sp_Upd_H0_Employee_InforGeneral, param);
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

        public long UpdateInforDetail(string fullName, string otherNames, string normalNames, int sex,
            DateTime birthday, string birthPlace, string nativePlace, string resident, string live, string handPhone,
            string homePhone,
            string idCard, DateTime dateOfIssue, string placeOfIssue, string nation, string nationality,
            string religion, string origin, DateTime dateJoinParty, string placeJoinParty, DateTime dateJoinCYU,
            string placeJoinCYU,
            DateTime dateOfEnlisted, DateTime dateOfDemobilized, string armyRank, string workedCompany, int userId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@OtherNames", SqlDbType.NVarChar, 100),
                    new SqlParameter("@NormalNames", SqlDbType.NVarChar, 100),
                    new SqlParameter("@Sex", SqlDbType.Int),
                    new SqlParameter("@Birthday", SqlDbType.DateTime),
                    new SqlParameter("@BirthPlace", SqlDbType.NVarChar, 254),
                    new SqlParameter("@NativePlace", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Resident", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Live", SqlDbType.NVarChar, 254),
                    new SqlParameter("@HandPhone", SqlDbType.VarChar, 50),
                    new SqlParameter("@HomePhone", SqlDbType.VarChar, 50),
                    new SqlParameter("@IdCard", SqlDbType.VarChar, 50),
                    new SqlParameter("@DateOfIssue", SqlDbType.DateTime),
                    new SqlParameter("@PlaceOfIssue", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Nation", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Nationality", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Religion", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Origin", SqlDbType.NVarChar, 254),
                    new SqlParameter("@DateJoinParty", SqlDbType.DateTime),
                    new SqlParameter("@PlaceJoinParty", SqlDbType.NVarChar, 254),
                    new SqlParameter("@DateJoinCYU", SqlDbType.DateTime),
                    new SqlParameter("@PlaceJoinCYU", SqlDbType.NVarChar, 254),
                    new SqlParameter("@DateOfEnlisted", SqlDbType.DateTime),
                    new SqlParameter("@DateOfDemobilized", SqlDbType.DateTime),
                    new SqlParameter("@ArmyRank", SqlDbType.NVarChar, 254),
                    new SqlParameter("@WorkedCompany", SqlDbType.NText),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                param[0].Value = fullName;
                param[1].Value = otherNames;
                param[2].Value = normalNames;
                param[3].Value = sex;
                param[4].Value = birthday;
                param[5].Value = birthPlace;
                param[6].Value = nativePlace;
                param[7].Value = resident;
                param[8].Value = live;
                param[9].Value = handPhone;
                param[10].Value = homePhone;
                param[11].Value = idCard;
                param[12].Value = dateOfIssue;
                param[13].Value = placeOfIssue;
                param[14].Value = nation;
                param[15].Value = nationality;
                param[16].Value = religion;
                param[17].Value = origin;
                param[18].Value = dateJoinParty;
                param[19].Value = placeJoinParty;
                param[20].Value = dateJoinCYU;
                param[21].Value = placeJoinCYU;
                param[22].Value = dateOfEnlisted;
                param[23].Value = dateOfDemobilized;
                param[24].Value = armyRank;
                param[25].Value = workedCompany;
                param[26].Value = userId;


                sproc = new StoreProcedure(EmployeeKeys.SP_EMPLOYEES_UPDATE_INFOR_DETAIL, param);
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

        //----------------------
        /// <summary>
        ///     Author: Giang
        ///     Date: 28-Oct-14
        ///     Content: DAL cho Update Rad thong tin
        /// </summary>
        /// <returns></returns>
        public long UpdateRadThongTin(string fullName, string otherNames, string normalNames, int sex,
            DateTime birthday, string birthPlace, string nativePlace, string origin, string idCard,
            DateTime dateOfIssue, string placeOfIssue, string nation, string nationality, int marriage,
            string religion, DateTime joinDate, DateTime joinCompanyDate, int userId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@OtherNames", SqlDbType.NVarChar, 100),
                    new SqlParameter("@NormalNames", SqlDbType.NVarChar, 100),
                    new SqlParameter("@Sex", SqlDbType.Int),
                    new SqlParameter("@Birthday", SqlDbType.DateTime),
                    new SqlParameter("@BirthPlace", SqlDbType.NVarChar, 254),
                    new SqlParameter("@NativePlace", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Origin", SqlDbType.NVarChar, 254),
                    new SqlParameter("@IdCard", SqlDbType.VarChar, 50),
                    new SqlParameter("@DateOfIssue", SqlDbType.DateTime),
                    new SqlParameter("@PlaceOfIssue", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Nation", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Nationality", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Marriage", SqlDbType.Int),
                    new SqlParameter("@Religion", SqlDbType.NVarChar, 50),
                    new SqlParameter("@JoinDate", SqlDbType.DateTime),
                    new SqlParameter("@JoinCompanyDate", SqlDbType.DateTime),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                param[0].Value = fullName;
                param[1].Value = otherNames;
                param[2].Value = normalNames;
                param[3].Value = sex;
                param[4].Value = birthday;
                param[5].Value = birthPlace;
                param[6].Value = nativePlace;
                param[7].Value = origin;
                param[8].Value = idCard;
                param[9].Value = dateOfIssue;
                param[10].Value = placeOfIssue;
                param[11].Value = nation;
                param[12].Value = nationality;
                param[13].Value = marriage;
                param[14].Value = religion;
                param[15].Value = joinDate;
                param[16].Value = joinCompanyDate;
                param[17].Value = userId;


                sproc = new StoreProcedure(EmployeeKeys.SP_EMPLOYEES_UPDATE_RAD_THONGTIN, param);
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

        public long UpdateDirectWorking(int directWorking, int userId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DirectWorking", SqlDbType.Int),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                param[0].Value = directWorking;
                param[1].Value = userId;


                sproc = new StoreProcedure("Upd_H0_Employee_DirectWorking", param);
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

        //-----------------------
        /// <summary>
        ///     Author: Giang
        ///     Date: 29-Oct-14
        ///     Content: DAL cho Update Rad Lien He
        /// </summary>
        /// <returns></returns>
        public long UpdateHighestEducationLevel(int level, int userid)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@HighestLevel", SqlDbType.Int),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                param[0].Value = level;
                param[1].Value = userid;

                sproc = new StoreProcedure(EmployeeKeys.SP_EMPLOYEES_HIGHESTLEVEL, param);
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

        //------------------------------------------------\
        public long UpdateRadLienHe(string workphone, string cellphone, string homephone, string resident, string live,
            int userid)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@WorkingPhone", SqlDbType.VarChar, 50),
                    new SqlParameter("@HandPhone", SqlDbType.VarChar, 50),
                    new SqlParameter("@HomePhone", SqlDbType.VarChar, 50),
                    new SqlParameter("@Resident", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Live", SqlDbType.NVarChar, 254),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                param[0].Value = workphone;
                param[1].Value = cellphone;
                param[2].Value = homephone;
                param[3].Value = resident;
                param[4].Value = live;
                param[5].Value = userid;

                sproc = new StoreProcedure(EmployeeKeys.SP_EMPLOYEES_UPDATE_RAD_LIENHE, param);
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

        //-----------------------------------------
        /// <summary>
        ///     Author: Giang
        ///     Date: 29-Oct-14
        ///     Content: DAL cho Update Rad Chinh Tri
        /// </summary>
        /// <returns></returns>
        public long UpdateRadChinhTri(DateTime dateJoinParty, string placeJoinParty, DateTime dateJoinCYU,
            string placeJoinCYU,
            DateTime dateOfEnlisted, DateTime dateOfDemobilized, string armyRank, int userId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DateJoinParty", SqlDbType.DateTime),
                    new SqlParameter("@PlaceJoinParty", SqlDbType.NVarChar, 254),
                    new SqlParameter("@DateJoinCYU", SqlDbType.DateTime),
                    new SqlParameter("@PlaceJoinCYU", SqlDbType.NVarChar, 254),
                    new SqlParameter("@DateOfEnlisted", SqlDbType.DateTime),
                    new SqlParameter("@DateOfDemobilized", SqlDbType.DateTime),
                    new SqlParameter("@ArmyRank", SqlDbType.NVarChar, 254),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                param[0].Value = dateJoinParty;
                param[1].Value = placeJoinParty;
                param[2].Value = dateJoinCYU;
                param[3].Value = placeJoinCYU;
                param[4].Value = dateOfEnlisted;
                param[5].Value = dateOfDemobilized;
                param[6].Value = armyRank;
                param[7].Value = userId;


                sproc = new StoreProcedure(EmployeeKeys.SP_EMPLOYEES_UPDATE_RAD_CHINHTRI, param);
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

        //--------------------------------------------------------
        /// <summary>
        ///     Author: Giang
        ///     Date: 20/11/2014
        ///     Content: Update Rad Cong Viec
        /// </summary>
        /// <param name="status"></param>
        /// <param name="taxCode"></param>
        /// <param name="accountno"></param>
        /// <param name="atmno"></param>
        /// <param name="bank"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public long UpdateRadCongViec(int status, string taxCode, string AccountNo, string CardNo, string bankname,
            string ins, string bhxh, int userId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Status", SqlDbType.Int),
                    new SqlParameter("@TaxCode", SqlDbType.VarChar, 50),
                    new SqlParameter("@AccountNo", SqlDbType.VarChar, 50),
                    new SqlParameter("@ATMNo", SqlDbType.VarChar, 50),
                    new SqlParameter("@BankName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Insurance", SqlDbType.VarChar, 50),
                    new SqlParameter("@BHXH", SqlDbType.VarChar, 50),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                param[0].Value = status;
                param[1].Value = taxCode;
                param[2].Value = AccountNo;
                param[3].Value = CardNo;
                param[4].Value = bankname;
                param[5].Value = ins;
                param[6].Value = bhxh;
                param[7].Value = userId;


                sproc = new StoreProcedure(EmployeeKeys.SP_EMPLOYEES_UPDATE_RAD_CONGVIEC, param);
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

        //-------------------------------------------------------------
        public long UpdateEmployeeCode(string employeeCode, int userId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@EmployeeCode", SqlDbType.VarChar, 50),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                param[0].Value = employeeCode;
                param[1].Value = userId;

                sproc = new StoreProcedure(EmployeeKeys.SP_EMPLOYEES_UPDATE_EMPLOYEE_CODE, param);
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

        public long UpdateAccountBank(string AccountNo, string CardNo, string BankName, int userId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@AccountNo", SqlDbType.VarChar, 50),
                    new SqlParameter("@CardNo", SqlDbType.VarChar, 50),
                    new SqlParameter("@BankName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                param[0].Value = AccountNo;
                param[1].Value = CardNo;
                param[2].Value = BankName;
                param[3].Value = userId;

                sproc = new StoreProcedure(EmployeeKeys.Sp_Upd_H0_Employee_AccountBank, param);
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

        public long UpdateStatus(int employeeCode, int userId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Status", SqlDbType.VarChar, 50),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                param[0].Value = employeeCode;
                param[1].Value = userId;

                sproc = new StoreProcedure(EmployeeKeys.Sp_Upd_H0_Employee_Status, param);
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

        public long UpdateFullName2(string fullName2, int userId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName2", SqlDbType.VarChar, 100),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                param[0].Value = fullName2;
                param[1].Value = userId;

                sproc = new StoreProcedure(EmployeeKeys.Sp_Upd_H0_EmployeeForFullName2, param);
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

        public long UpdateTaxCode(string taxCode, int userId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@TaxCode", SqlDbType.VarChar, 50),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                param[0].Value = taxCode;
                param[1].Value = userId;

                sproc = new StoreProcedure(EmployeeKeys.Sp_Upd_H0_Employee_TaxCode, param);
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

        public long UpdatePassword(string Password, int UserId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Password", SqlDbType.VarChar, 1000),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                param[0].Value = Password;
                param[1].Value = UserId;

                sproc = new StoreProcedure(EmployeeKeys.Sp_Upd_H0_Employee_Password, param);
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

        #region other methods

        public long ChangePassword(string userName, string oldPassword, string newPassword)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserName", SqlDbType.VarChar, 50),
                    new SqlParameter("@OldPassword", SqlDbType.VarChar, 50),
                    new SqlParameter("@NewPassword", SqlDbType.NVarChar, 100)
                };
                param[0].Value = userName;
                param[1].Value = oldPassword;
                param[2].Value = newPassword;


                sproc = new StoreProcedure(EmployeeKeys.SP_EMPLOYEES_UPDATE_CHANGE_PASSWORD, param);
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

        public long Delete(int userId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int)
                };
                param[0].Value = userId;

                sproc = new StoreProcedure(EmployeeKeys.SP_EMPLOYEES_DELETE, param);
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

        public long CheckNewEmployee(string userCode)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserCode", SqlDbType.VarChar)
                };
                param[0].Value = userCode;

                sproc = new StoreProcedure(EmployeeKeys.SP_EMPLOYEES_CHECK_NEW_EMPLOYEE, param);
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

        public long CheckEmployeeExistence(string userName)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserName", SqlDbType.VarChar, 50)
                };
                param[0].Value = userName;

                sproc = new StoreProcedure("Sel_H0_EmployeeExistence", param);
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

        #region Update New Version

        public long UpdateGeneralInfo(string employeeCode, string fullName, int sex, DateTime? birthday,
            string birthPlace, string nativePlace, string nativeCity,
            string idCard, DateTime? dateOfIssue, string placeOfIssue,
            string passportNo, DateTime? passportDateOfIssue, string passportPlaceOfIssue,
            DateTime? passportDateOfExpiry,
            bool marriage, string origin, string nation, string religion, string nationality, string workedCompany,
            int userId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@EmployeeCode", SqlDbType.VarChar, 50),
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@Sex", SqlDbType.Int),
                    new SqlParameter("@Birthday", SqlDbType.DateTime),
                    new SqlParameter("@BirthPlace", SqlDbType.NVarChar, 254),
                    new SqlParameter("@NativePlace", SqlDbType.NVarChar, 254),
                    new SqlParameter("@NativeCity", SqlDbType.NVarChar, 254),
                    new SqlParameter("@IdCard", SqlDbType.VarChar, 50),
                    new SqlParameter("@DateOfIssue", SqlDbType.DateTime),
                    new SqlParameter("@PlaceOfIssue", SqlDbType.NVarChar, 254),
                    new SqlParameter("@PassportNo", SqlDbType.VarChar, 50),
                    new SqlParameter("@PassportDateOfIssue", SqlDbType.DateTime),
                    new SqlParameter("@PassportPlaceOfIssue", SqlDbType.NVarChar, 254),
                    new SqlParameter("@PassportDateOfExpiry", SqlDbType.DateTime),
                    new SqlParameter("@Marriage", SqlDbType.Bit),
                    new SqlParameter("@Origin", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Nation", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Religion", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Nationality", SqlDbType.NVarChar, 254),
                    new SqlParameter("WorkedCompany", SqlDbType.NText),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };
                param[0].Value = employeeCode;
                param[1].Value = fullName;
                param[2].Value = sex;

                if (birthday.HasValue)
                    if (birthday.Equals(FormatDate.GetSQLDateMinValue))
                        param[3].Value = DBNull.Value;
                    else
                        param[3].Value = birthday.Value;
                else
                    param[3].Value = DBNull.Value;

                param[4].Value = birthPlace;
                param[5].Value = nativePlace;
                param[6].Value = nativeCity;
                param[7].Value = idCard;

                if (dateOfIssue.HasValue)
                    if (dateOfIssue.Equals(FormatDate.GetSQLDateMinValue))
                        param[8].Value = DBNull.Value;
                    else
                        param[8].Value = dateOfIssue.Value;
                else
                    param[8].Value = DBNull.Value;

                param[9].Value = placeOfIssue;
                param[10].Value = passportNo;

                if (passportDateOfIssue.HasValue)
                    if (passportDateOfIssue.Equals(FormatDate.GetSQLDateMinValue))
                        param[11].Value = DBNull.Value;
                    else
                        param[11].Value = passportDateOfIssue.Value;
                else
                    param[11].Value = DBNull.Value;

                param[12].Value = passportPlaceOfIssue;

                if (passportDateOfExpiry.HasValue)
                    if (passportDateOfExpiry.Equals(FormatDate.GetSQLDateMinValue))
                        param[13].Value = DBNull.Value;
                    else
                        param[13].Value = passportDateOfExpiry.Value;
                else
                    param[13].Value = DBNull.Value;


                param[14].Value = marriage;
                param[15].Value = origin;
                param[16].Value = nation;
                param[17].Value = religion;
                param[18].Value = nationality;
                param[19].Value = workedCompany;
                param[20].Value = userId;


                sproc = new StoreProcedure(EmployeeKeys.Sp_Upd_H0_Employee_GeneralInfo, param);
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

        public long UpdateContactInfo(string WorkingPhone, string HomePhone, string HandPhone, string Resident,
            string Live, string OtherPhone, string WorkingEmail, string OtherName, string YahooId, string SkypeId,
            string MsnId, string Guarantor, string GuarantorWorkingPlace, string GuarantorPhone, string GuarantorAddress,
            string ResidentCity, string ResidentCountry, string CurrentCity, string CurrentCountry,
            string UrgentContactName, string UrgentContactRelation, string UrgentContactCellPhone,
            string UrgentContactHomePhone, string UrgentContactEmail, string UrgentContactAddress, int UserId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@WorkingPhone", SqlDbType.VarChar, 50),
                    new SqlParameter("@HomePhone", SqlDbType.VarChar, 50),
                    new SqlParameter("@HandPhone", SqlDbType.VarChar, 50),
                    new SqlParameter("@Resident", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Live", SqlDbType.NVarChar, 254),
                    new SqlParameter("@OtherPhone", SqlDbType.VarChar, 50),
                    new SqlParameter("@WorkingEmail", SqlDbType.VarChar, 50),
                    new SqlParameter("@OtherNames", SqlDbType.NVarChar, 254),
                    new SqlParameter("@YahooId", SqlDbType.VarChar, 50),
                    new SqlParameter("@SkypeId", SqlDbType.VarChar, 50),
                    new SqlParameter("@MsnId", SqlDbType.VarChar, 50),
                    new SqlParameter("@Guarantor", SqlDbType.NVarChar, 254),
                    new SqlParameter("@GuarantorWorkingPlace", SqlDbType.NVarChar, 254),
                    new SqlParameter("@GuarantorPhone", SqlDbType.VarChar, 50),
                    new SqlParameter("@GuarantorAddress", SqlDbType.NVarChar, 254),
                    new SqlParameter("@ResidentCity", SqlDbType.NVarChar, 50),
                    new SqlParameter("@ResidentCountry", SqlDbType.NVarChar, 50),
                    new SqlParameter("@CurrentCity", SqlDbType.NVarChar, 50),
                    new SqlParameter("@CurrentCountry", SqlDbType.NVarChar, 50),
                    new SqlParameter("@UrgentContactName", SqlDbType.NVarChar, 50),
                    new SqlParameter("@UrgentContactRelation", SqlDbType.NVarChar, 50),
                    new SqlParameter("@UrgentContactCellPhone", SqlDbType.VarChar, 50),
                    new SqlParameter("@UrgentContactHomePhone", SqlDbType.VarChar, 50),
                    new SqlParameter("@UrgentContactEmail", SqlDbType.NVarChar, 50),
                    new SqlParameter("@UrgentContactAddress", SqlDbType.NVarChar, 254),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };
                param[0].Value = WorkingPhone;
                param[1].Value = HomePhone;
                param[2].Value = HandPhone;
                param[3].Value = Resident;
                param[4].Value = Live;
                param[5].Value = OtherPhone;
                param[6].Value = WorkingEmail;
                param[7].Value = OtherName;
                param[8].Value = YahooId;
                param[9].Value = SkypeId;
                param[10].Value = MsnId;

                param[11].Value = Guarantor;
                param[12].Value = GuarantorWorkingPlace;
                param[13].Value = GuarantorPhone;
                param[14].Value = GuarantorAddress;
                param[15].Value = ResidentCity;
                param[16].Value = ResidentCountry;
                param[17].Value = CurrentCity;
                param[18].Value = CurrentCountry;
                param[19].Value = UrgentContactName;
                param[20].Value = UrgentContactRelation;

                param[21].Value = UrgentContactCellPhone;
                param[22].Value = UrgentContactHomePhone;
                param[23].Value = UrgentContactEmail;
                param[24].Value = UrgentContactAddress;
                param[25].Value = UserId;


                sproc = new StoreProcedure(EmployeeKeys.Sp_Upd_H0_Employee_ContactInfo, param);
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

        public long UpdateWorkingInfo(string TaxCode, string AccountNo, string BankName, string SocialInsuranceNo,
            string HealthInsuranceNo, DateTime LeaveDate, string UserName, DateTime SocialInsuranceRegDate,
            float SocialInsurancePercentage, int SocialInsuranceProvinceCode, DateTime HealthInsuranceDateOfExpiry,
            string HealthInsuranceRegHospital, int Status, int UserId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@TaxCode", SqlDbType.VarChar, 50),
                    new SqlParameter("@AccountNo", SqlDbType.VarChar, 50),
                    new SqlParameter("@BankName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@SocialInsuranceNo", SqlDbType.VarChar, 50),
                    new SqlParameter("@HealthInsuranceNo", SqlDbType.VarChar, 50),
                    new SqlParameter("@LeaveDate", SqlDbType.SmallDateTime),
                    new SqlParameter("@UserName", SqlDbType.VarChar, 50),
                    new SqlParameter("@SocialInsuranceRegDate", SqlDbType.DateTime),
                    new SqlParameter("@SocialInsurancePercentage", SqlDbType.Float),
                    new SqlParameter("@SocialInsuranceProvinceCode", SqlDbType.Int),
                    new SqlParameter("@HealthInsuranceDateOfExpiry", SqlDbType.DateTime),
                    new SqlParameter("@HealthInsuranceRegHospital", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Status", SqlDbType.Int),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };
                param[0].Value = TaxCode;
                param[1].Value = AccountNo;
                param[2].Value = BankName;
                param[3].Value = SocialInsuranceNo;
                param[4].Value = HealthInsuranceNo;
                param[5].Value = LeaveDate;
                param[6].Value = UserName;
                param[7].Value = SocialInsuranceRegDate;
                param[8].Value = SocialInsurancePercentage;
                param[9].Value = SocialInsuranceProvinceCode;

                param[10].Value = HealthInsuranceDateOfExpiry;
                param[11].Value = HealthInsuranceRegHospital;
                param[12].Value = Status;
                param[13].Value = UserId;


                sproc = new StoreProcedure(EmployeeKeys.Sp_Upd_H0_Employee_WorkingInfo, param);
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


        public long UpdateOtherInfo(DateTime DateJoinParty, string PlaceJoinParty, string PartyPosition,
            DateTime DateJoinCYU, string PlaceJoinCYU, string CYUPosition, DateTime DateOfEnlisted,
            DateTime DateOfDemobilized, string ArmyRank, string ArmyPosition, string ArmyBranch, string ArmyUnit,
            string DemobilizationReason, int IsWarInvalid, DateTime RevolutionJoinDate, string WarInvalidLevel,
            int WarInvalidPercentage, int IsEntitledToTreatment, string BloodGroup, string Height, string Weight,
            string HealthInformation, string HealthRemarks, string SicknessRemarks, int IsHandicaped,
            string PersonalTarget, string Hobbies, string Strengths, string Weaknesses, int UserId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DateJoinParty", SqlDbType.DateTime),
                    new SqlParameter("@PlaceJoinParty", SqlDbType.NVarChar, 50),
                    new SqlParameter("@PartyPosition", SqlDbType.NVarChar, 254),
                    new SqlParameter("@DateJoinCYU", SqlDbType.DateTime),
                    new SqlParameter("@PlaceJoinCYU", SqlDbType.NVarChar, 50),
                    new SqlParameter("@CYUPosition", SqlDbType.NVarChar, 254),
                    new SqlParameter("@DateOfEnlisted", SqlDbType.DateTime),
                    new SqlParameter("@DateOfDemobilized", SqlDbType.DateTime),
                    new SqlParameter("@ArmyRank", SqlDbType.NVarChar, 254),
                    new SqlParameter("@ArmyPosition", SqlDbType.NVarChar, 254),
                    new SqlParameter("@ArmyBranch", SqlDbType.NVarChar, 254),
                    new SqlParameter("@ArmyUnit", SqlDbType.NVarChar, 254),
                    new SqlParameter("@DemobilizationReason", SqlDbType.NVarChar, 254),
                    new SqlParameter("@IsWarInvalid", SqlDbType.Int),
                    new SqlParameter("@RevolutionJoinDate", SqlDbType.DateTime),
                    new SqlParameter("@WarInvalidLevel", SqlDbType.NVarChar, 254),
                    new SqlParameter("@WarInvalidPercentage", SqlDbType.Int),
                    new SqlParameter("@IsEntitledToTreatment", SqlDbType.Int),
                    new SqlParameter("@BloodGroup", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Height", SqlDbType.VarChar, 50),
                    new SqlParameter("@Weight", SqlDbType.VarChar, 50),
                    new SqlParameter("@HealthInformation", SqlDbType.NVarChar, 254),
                    new SqlParameter("@HealthRemarks", SqlDbType.NVarChar, 254),
                    new SqlParameter("@SicknessRemarks", SqlDbType.NVarChar, 254),
                    new SqlParameter("@IsHandicaped", SqlDbType.Int),
                    new SqlParameter("@PersonalTarget", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@Hobbies", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@Strengths", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@Weaknesses", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };
                param[0].Value = DateJoinParty;
                param[1].Value = PlaceJoinParty;
                param[2].Value = PartyPosition;
                param[3].Value = DateJoinCYU;
                param[4].Value = PlaceJoinCYU;
                param[5].Value = CYUPosition;
                param[6].Value = DateOfEnlisted;
                param[7].Value = DateOfDemobilized;
                param[8].Value = ArmyRank;
                param[9].Value = ArmyPosition;

                param[10].Value = ArmyBranch;
                param[11].Value = ArmyUnit;
                param[12].Value = DemobilizationReason;
                param[13].Value = IsWarInvalid;
                param[14].Value = RevolutionJoinDate;
                param[15].Value = WarInvalidLevel;
                param[16].Value = WarInvalidPercentage;
                param[17].Value = IsEntitledToTreatment;
                param[18].Value = BloodGroup;
                param[19].Value = Height;

                param[20].Value = Weight;
                param[21].Value = HealthInformation;
                param[22].Value = HealthRemarks;
                param[23].Value = SicknessRemarks;
                param[24].Value = IsHandicaped;
                param[25].Value = PersonalTarget;
                param[26].Value = Hobbies;
                param[27].Value = Strengths;
                param[28].Value = Weaknesses;
                param[29].Value = UserId;


                sproc = new StoreProcedure(EmployeeKeys.Sp_Upd_H0_Employee_OtherInfo, param);
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


        public long UpdateStock(string HandPhone, string HomePhone, string Resident, string Live, string IdCard,
            DateTime DateOfIssue, string PlaceOfIssue,
            int InvestorNo, int SeniorStock, int UndertakingYear, int UnderTakingStock, int SeniorStockBought,
            int UnderTakingStockBought, int UpdatedUserid,
            DateTime UpdatedDate, bool ConfirmStocks, int UserId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@HandPhone", SqlDbType.VarChar, 50),
                    new SqlParameter("@HomePhone", SqlDbType.VarChar, 50),
                    new SqlParameter("@Resident", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Live", SqlDbType.NVarChar, 254),
                    new SqlParameter("@IdCard", SqlDbType.VarChar, 50),
                    new SqlParameter("@DateOfIssue", SqlDbType.DateTime),
                    new SqlParameter("@PlaceOfIssue", SqlDbType.NVarChar, 254),
                    new SqlParameter("@InvestorNo", SqlDbType.Int),
                    new SqlParameter("@SeniorStock", SqlDbType.Int),
                    new SqlParameter("@UndertakingYear", SqlDbType.Int),
                    new SqlParameter("@UnderTakingStock", SqlDbType.Int),
                    new SqlParameter("@SeniorStockBought", SqlDbType.Int),
                    new SqlParameter("@UnderTakingStockBought", SqlDbType.Int),
                    new SqlParameter("@UpdatedUserid", SqlDbType.Int),
                    new SqlParameter("@UpdatedDate", SqlDbType.DateTime),
                    new SqlParameter("@ConfirmStocks", SqlDbType.Bit),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                param[0].Value = HandPhone;
                param[1].Value = HomePhone;
                param[2].Value = Resident;
                param[3].Value = Live;
                param[4].Value = IdCard;
                param[5].Value = DateOfIssue;
                param[6].Value = PlaceOfIssue;
                param[7].Value = InvestorNo;
                param[8].Value = SeniorStock;
                param[9].Value = UndertakingYear;
                param[10].Value = UnderTakingStock;
                param[11].Value = SeniorStockBought;
                param[12].Value = UnderTakingStockBought;
                param[13].Value = UpdatedUserid;
                param[14].Value = UpdatedDate;
                param[15].Value = ConfirmStocks;
                param[16].Value = UserId;


                sproc = new StoreProcedure(EmployeeKeys.Sp_Upd_EmployeeStock, param);
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

        public long UpdateStock2(int SeniorStockRegistered, int UnderTakingStockRegistered, int UserId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@SeniorStockRegistered", SqlDbType.Int),
                    new SqlParameter("@UnderTakingStockRegistered", SqlDbType.Int),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                param[0].Value = SeniorStockRegistered;
                param[1].Value = UnderTakingStockRegistered;
                param[2].Value = UserId;


                sproc = new StoreProcedure(EmployeeKeys.Sp_Upd_EmployeeStock2, param);
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

        #region for combo boxes

        public DataTable GetComboBoxItemId(int userId, int typeId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@ComboBoxTypeId", SqlDbType.Int)
                };
                param[0].Value = userId;
                param[1].Value = typeId;
                sproc = new StoreProcedure(EmployeeKeys.Sp_Sel_H0_ItemIdByUserIdType, param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return datatable;
        }

        public long SaveComboBoxItemId(int userId, int typeId, int id)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@ComboBoxTypeId", SqlDbType.Int),
                    new SqlParameter("@ItemId", SqlDbType.Int)
                };
                param[0].Value = userId;
                param[1].Value = typeId;
                param[2].Value = id;

                sproc = new StoreProcedure(EmployeeKeys.Sp_Upd_H0_ItemIdByUserIdType, param);
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


        public long AddRemoveUpdateComboBoxItem(int id, int typeId, string typeName, string description, string remark,
            string mode)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Id", SqlDbType.Int),
                    new SqlParameter("@TypeId", SqlDbType.Int),
                    new SqlParameter("@TypeName", SqlDbType.VarChar, 50),
                    new SqlParameter("@Description", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Mode", SqlDbType.VarChar, 3)
                };
                param[0].Value = id;
                param[1].Value = typeId;
                param[2].Value = typeName;
                param[3].Value = description;
                param[4].Value = remark;
                param[5].Value = mode;

                sproc = new StoreProcedure(EmployeeKeys.Sp_H0_AddRemoveUpdateComboBoxItem, param);
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
    }
}