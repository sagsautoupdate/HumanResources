using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H0;

namespace HRMDAL.H0
{
    public class EmployeeContractDAL : Dao
    {
        #region insert, update, delete

        /// <summary>
        ///     Author: Giang
        ///     Date: 12/16/2014
        ///     Content: Insert V1 (Bo sung vai field)
        /// </summary>
        /// <returns></returns>
        public long Insert_V1(int UserId, int PositionId, int ContractTypeId, DateTime FromDate, DateTime ToDate,
            string ContractName, string RepresentativeOfCompany, string CompanyName, int WorkingHour, int Overtime,
            DateTime CreateDate,
            int SalaryLevel, int ScaleOfSalaryId, string ContractTitle, string Office, bool IsReplaced,
            int PreviousEmployeeContractId, string CntRemark)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@PositionId", SqlDbType.Int),
                    new SqlParameter("@ContractTypeId", SqlDbType.Int),
                    new SqlParameter("@FromDate", SqlDbType.DateTime),
                    new SqlParameter("@ToDate", SqlDbType.DateTime),
                    new SqlParameter("@ContractName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@RepresentativeOfCompany", SqlDbType.NVarChar, 100),
                    new SqlParameter("@CompanyName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@WorkingHour", SqlDbType.Int, 4),
                    new SqlParameter("@Overtime", SqlDbType.Int, 4),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime),
                    new SqlParameter("@SalaryLevel", SqlDbType.Int),
                    new SqlParameter("@ScaleOfSalaryId", SqlDbType.Int),
                    new SqlParameter("@Title", SqlDbType.NVarChar, 250),
                    new SqlParameter("@Office", SqlDbType.NVarChar, 250),
                    new SqlParameter("@IsReplaced", SqlDbType.Bit),
                    new SqlParameter("@PreviousEmployeeContractId", SqlDbType.Int),
                    new SqlParameter("@CntRemark", SqlDbType.NText)
                };

                param[0].Value = UserId;
                param[1].Value = PositionId;
                param[2].Value = ContractTypeId;
                param[3].Value = FromDate;
                param[4].Value = ToDate;
                param[5].Value = ContractName;
                param[6].Value = RepresentativeOfCompany;
                param[7].Value = CompanyName;
                param[8].Value = WorkingHour;
                param[9].Value = Overtime;
                param[10].Value = CreateDate;
                param[11].Value = SalaryLevel;
                param[12].Value = ScaleOfSalaryId;
                param[13].Value = ContractTitle;
                param[14].Value = Office;
                if (IsReplaced == false)
                {
                    param[15].Value = false;
                    param[16].Value = DBNull.Value;
                }
                else
                {
                    param[15].Value = IsReplaced;
                    param[16].Value = PreviousEmployeeContractId;
                }
                param[17].Value = CntRemark;
                sproc = new StoreProcedure(EmployeeContractKeys.Sp_Ins_H0_EmployeeContract_ByUserId, param);
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

        public long Update_V1(int employeeContractId, int UserId, int PositionId, int ContractTypeId, DateTime FromDate,
            DateTime ToDate,
            string ContractName, string RepresentativeOfCompany, string CompanyName, int WorkingHour, int Overtime,
            int SalaryLevel, int ScaleOfSalaryId, string ContractTitle, string Office, bool IsReplaced,
            int PreviousEmployeeContractId, string CntRemark)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@EmployeeContractId", SqlDbType.Int),
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@PositionId", SqlDbType.Int),
                    new SqlParameter("@ContractTypeId", SqlDbType.Int),
                    new SqlParameter("@FromDate", SqlDbType.DateTime),
                    new SqlParameter("@ToDate", SqlDbType.DateTime),
                    new SqlParameter("@ContractName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@RepresentativeOfCompany", SqlDbType.NVarChar, 100),
                    new SqlParameter("@CompanyName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@WorkingHour", SqlDbType.Int, 4),
                    new SqlParameter("@Overtime", SqlDbType.Int, 4),
                    new SqlParameter("@SalaryLevel", SqlDbType.Int),
                    new SqlParameter("@ScaleOfSalaryId", SqlDbType.Int),
                    new SqlParameter("@Title", SqlDbType.NVarChar, 250),
                    new SqlParameter("@Office", SqlDbType.NVarChar, 250),
                    new SqlParameter("@IsReplaced", SqlDbType.Bit),
                    new SqlParameter("@PreviousEmployeeContractId", SqlDbType.Int),
                    new SqlParameter("@CntRemark", SqlDbType.NText)
                };

                param[0].Value = employeeContractId;
                param[1].Value = UserId;
                param[2].Value = PositionId;
                param[3].Value = ContractTypeId;
                param[4].Value = FromDate;
                param[5].Value = ToDate;
                param[6].Value = ContractName;
                param[7].Value = RepresentativeOfCompany;
                param[8].Value = CompanyName;
                param[9].Value = WorkingHour;
                param[10].Value = Overtime;
                param[11].Value = SalaryLevel;
                param[12].Value = ScaleOfSalaryId;
                param[13].Value = ContractTitle;
                param[14].Value = Office;
                if (IsReplaced == false)
                {
                    param[15].Value = DBNull.Value;
                    param[16].Value = DBNull.Value;
                }
                else
                {
                    param[15].Value = IsReplaced;
                    param[16].Value = PreviousEmployeeContractId;
                }
                param[17].Value = CntRemark;
                sproc = new StoreProcedure(EmployeeContractKeys.Sp_Upd_H0_EmployeeContract_ByUserId, param);
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

        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns>int</returns>
        public long Insert(int UserId, int ContractTypeId, int PositionId, double Wages, int Unit, DateTime FromDate,
            DateTime ToDate, string Description, bool Active,
            string ContractNo, string ContractName, string RepresentativeOfCompany, string CompanyName,
            string AttachFileName, string WorkingName, int WorkingHour, int Overtime, int SalaryType)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@ContractTypeId", SqlDbType.Int),
                    new SqlParameter("@PositionId", SqlDbType.Int),
                    new SqlParameter("@Wages", SqlDbType.Float),
                    new SqlParameter("@Unit", SqlDbType.Int),
                    new SqlParameter("@FromDate", SqlDbType.DateTime),
                    new SqlParameter("@ToDate", SqlDbType.DateTime),
                    new SqlParameter("@Description", SqlDbType.NVarChar),
                    new SqlParameter("@Active", SqlDbType.Bit),
                    new SqlParameter("@ContractNo", SqlDbType.VarChar, 100),
                    new SqlParameter("@ContractName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@RepresentativeOfCompany", SqlDbType.NVarChar, 100),
                    new SqlParameter("@CompanyName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@AttachFileName", SqlDbType.VarChar, 100),
                    new SqlParameter("@WorkingName", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@WorkingHour", SqlDbType.Int, 4),
                    new SqlParameter("@Overtime", SqlDbType.Int, 4),
                    new SqlParameter("@SalaryType", SqlDbType.Int, 4)
                };

                param[0].Value = UserId;
                param[1].Value = ContractTypeId;
                param[2].Value = PositionId;
                param[3].Value = Wages;
                param[4].Value = Unit;
                param[5].Value = FromDate;
                param[6].Value = ToDate;
                param[7].Value = Description;
                param[8].Value = Active;
                param[9].Value = ContractNo;
                param[10].Value = ContractName;
                param[11].Value = RepresentativeOfCompany;
                param[12].Value = CompanyName;
                param[13].Value = AttachFileName;
                param[14].Value = WorkingName;
                param[15].Value = WorkingHour;
                param[16].Value = Overtime;
                param[17].Value = SalaryType;

                sproc = new StoreProcedure(EmployeeContractKeys.Sp_Ins_H0_EmployeeContract, param);
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

        public long Update(int EmployeeContractId, int UserId, int ContractTypeId, int PositionId, double Wages,
            int Unit, DateTime FromDate, DateTime ToDate, string Description, bool Active,
            string ContractNo, string ContractName, string RepresentativeOfCompany, string CompanyName,
            string AttachFileName, string WorkingName, int WorkingHour, int Overtime, int SalaryType)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =

                {
                    new SqlParameter("@EmployeeContractId", SqlDbType.Int),
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@ContractTypeId", SqlDbType.Int),
                    new SqlParameter("@PositionId", SqlDbType.Int),
                    new SqlParameter("@Wages", SqlDbType.Float),
                    new SqlParameter("@Unit", SqlDbType.Int),
                    new SqlParameter("@FromDate", SqlDbType.DateTime),
                    new SqlParameter("@ToDate", SqlDbType.DateTime),
                    new SqlParameter("@Description", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@Active", SqlDbType.Bit),
                    new SqlParameter("@ContractNo", SqlDbType.VarChar, 100),
                    new SqlParameter("@ContractName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@RepresentativeOfCompany", SqlDbType.NVarChar, 100),
                    new SqlParameter("@CompanyName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@AttachFileName", SqlDbType.VarChar, 100),
                    new SqlParameter("@WorkingName", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@WorkingHour", SqlDbType.Int, 4),
                    new SqlParameter("@Overtime", SqlDbType.Int, 4),
                    new SqlParameter("@SalaryType", SqlDbType.Int, 4)
                };

                param[0].Value = EmployeeContractId;
                param[1].Value = UserId;
                param[2].Value = ContractTypeId;
                param[3].Value = PositionId;
                param[4].Value = Wages;
                param[5].Value = Unit;
                param[6].Value = FromDate;
                param[7].Value = ToDate;
                param[8].Value = Description;
                param[9].Value = Active;
                param[10].Value = ContractNo;
                param[11].Value = ContractName;
                param[12].Value = RepresentativeOfCompany;
                param[13].Value = CompanyName;
                param[14].Value = AttachFileName;
                param[15].Value = WorkingName;
                param[16].Value = WorkingHour;
                param[17].Value = Overtime;
                param[18].Value = SalaryType;

                sproc = new StoreProcedure(EmployeeContractKeys.Sp_Upd_H0_EmployeeContract, param);
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


        public long InsertDecisions(int PreviousEmployeeContractId, int ContractTypeId, int DecisionId, int UserId,
            int PositionId, DateTime FromDate, DateTime ToDate, int SalaryType)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@PreviousEmployeeContractId", SqlDbType.Int),
                    new SqlParameter("@ContractTypeId", SqlDbType.Int),
                    new SqlParameter("@DecisionId", SqlDbType.Int),
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@PositionId", SqlDbType.Int),
                    new SqlParameter("@FromDate", SqlDbType.DateTime),
                    new SqlParameter("@ToDate", SqlDbType.DateTime),
                    new SqlParameter("@SalaryType", SqlDbType.Int, 4)
                };

                param[0].Value = PreviousEmployeeContractId;
                param[1].Value = ContractTypeId;
                param[2].Value = DecisionId;
                param[3].Value = UserId;
                param[4].Value = PositionId;
                param[5].Value = FromDate;
                param[6].Value = ToDate;
                param[7].Value = SalaryType;

                sproc = new StoreProcedure(EmployeeContractKeys.Sp_Ins_H0_EmployeeContractDecisions, param);
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

        public long UpdateDecisions(int PreviousEmployeeContractId, int ContractTypeId, int DecisionId, int UserId,
            int PositionId, DateTime FromDate, DateTime ToDate, int SalaryType, int EmployeeContractId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@PreviousEmployeeContractId", SqlDbType.Int),
                    new SqlParameter("@ContractTypeId", SqlDbType.Int),
                    new SqlParameter("@DecisionId", SqlDbType.Int),
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@PositionId", SqlDbType.Int),
                    new SqlParameter("@FromDate", SqlDbType.DateTime),
                    new SqlParameter("@ToDate", SqlDbType.DateTime),
                    new SqlParameter("@SalaryType", SqlDbType.Int, 4),
                    new SqlParameter("@EmployeeContractId", SqlDbType.Int, 4)
                };

                param[0].Value = PreviousEmployeeContractId;
                param[1].Value = ContractTypeId;
                param[2].Value = DecisionId;
                param[3].Value = UserId;
                param[4].Value = PositionId;
                param[5].Value = FromDate;
                param[6].Value = ToDate;
                param[7].Value = SalaryType;
                param[8].Value = EmployeeContractId;


                sproc = new StoreProcedure(EmployeeContractKeys.Sp_Upd_H0_EmployeeContractDecisions, param);
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

        public int Delete(int employeeContractId)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@EmployeeContractId", SqlDbType.Int)
                };

                param[0].Value = employeeContractId;

                sproc = new StoreProcedure(EmployeeContractKeys.Sp_Del_H0_EmployeeContract, param);
                sproc.Run();
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

        public int DeleteByIds(string employeeContractIds)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@EmployeeContractIds", SqlDbType.VarChar, 254)
                };

                param[0].Value = employeeContractIds;

                sproc = new StoreProcedure(EmployeeContractKeys.Sp_Del_H0_EmployeeContractByIds, param);
                sproc.Run();
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

        public int UpdatePrintType(int employeeContractIds, bool printType)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@EmployeeContractId", SqlDbType.Int),
                    new SqlParameter("@PrintType", SqlDbType.Bit)
                };

                param[0].Value = employeeContractIds;
                param[1].Value = printType;

                sproc = new StoreProcedure("Upd_H0_EmployeeContract_PrintType", param);
                sproc.Run();
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

        #region get

        public DataTable GetByDeptId(string deptId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DeptId", SqlDbType.VarChar, 1000)
                };
                param[0].Value = deptId;
                sproc = new StoreProcedure(EmployeeContractKeys.SP_EMPLOYEE_CONTRACT_GET_BY_DEPT_ID, param);
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

        public DataTable GetAllToDT()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure(EmployeeContractKeys.SP_EMPLOYEE_CONTRACT_GETALL, null);
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

        public DataTable GetAllToDT1()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure(EmployeeContractKeys.SP_EMPLOYEE_CONTRACT_GETALL_INC_INACTIVE, null);
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

        public DataTable GetAll()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure("Sel_H0_All_EmployeeContract_Exp", null);
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

        public DataTable GetByFilter(string fullName, int departmentId, int contractType, int typeSort)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@DepartmentId", SqlDbType.Int, 4),
                    new SqlParameter("@ContractTypeId", SqlDbType.Int, 4),
                    new SqlParameter("@TypeSort", SqlDbType.Int, 4)
                };
                param[0].Value = fullName;
                param[1].Value = departmentId;
                param[2].Value = contractType;
                param[3].Value = typeSort;

                sproc = new StoreProcedure(EmployeeContractKeys.Sp_Sel_H0_EmployeeContractByFilter, param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return dt;
        }

        public DataTable GetByAllByUserId(int UserId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4)
                };
                param[0].Value = UserId;
                sproc = new StoreProcedure("Sel_H0_EmployeeContractByAll", param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return dt;
        }

        public DataTable GetByUserIds(string UserId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.NVarChar, 4000)
                };
                param[0].Value = UserId;
                sproc = new StoreProcedure("Sel_H0_EmployeeContractByUserIds", param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return dt;
        }

        public DataTable GetByUserId(int UserId, int DecisionId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@DecisionId", SqlDbType.Int, 4)
                };
                param[0].Value = UserId;
                param[1].Value = DecisionId;
                sproc = new StoreProcedure(EmployeeContractKeys.Sp_Sel_H0_EmployeeContractByUserId, param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return dt;
        }

        public DataTable GetActiveContractByUserId(int userId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4)
                };
                param[0].Value = userId;
                sproc = new StoreProcedure(EmployeeContractKeys.Sp_Sel_H0_EmployeeContract_Active_By_UserId, param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return dt;
        }

        public DataTable GetByUserDateActive(int userId, DateTime date, bool active)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@Date", SqlDbType.DateTime),
                    new SqlParameter("@Active", SqlDbType.Bit)
                };

                param[0].Value = userId;
                param[1].Value = date;
                param[2].Value = active;

                sproc = new StoreProcedure(EmployeeContractKeys.Sp_H0_EmployeeContract_By_UserId_Date_Active, param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return dt;
        }

        public DataTable RemindExpiredConstracts(string fullName, string deptId, DateTime expireDate, int typeSort)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@DepartmentId", SqlDbType.VarChar, 254),
                    new SqlParameter("@ExpireDate", SqlDbType.DateTime),
                    new SqlParameter("@TypeSort", SqlDbType.Int, 4)
                };

                param[0].Value = fullName;
                param[1].Value = deptId;
                param[2].Value = expireDate;
                param[3].Value = typeSort;

                sproc = new StoreProcedure(EmployeeContractKeys.Sp_Sel_H0_EmployeeContractByRemind, param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return dt;
        }

        public DataTable ChangedConstracts(string fullName, int deptId, int month, int year, int typeSort)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@DepartmentId", SqlDbType.Int),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int),
                    new SqlParameter("@TypeSort", SqlDbType.Int)
                };

                param[0].Value = fullName;
                param[1].Value = deptId;
                param[2].Value = month;
                param[3].Value = year;
                param[4].Value = typeSort;

                sproc = new StoreProcedure(EmployeeContractKeys.Sp_Sel_H0_EmployeeContractByChanged, param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return dt;
        }

        public DataTable GetByUserFromToDate(int UserId, DateTime FromDate, DateTime ToDate)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@FromDate", SqlDbType.DateTime),
                    new SqlParameter("@ToDate", SqlDbType.DateTime)
                };

                param[0].Value = UserId;
                param[1].Value = FromDate;
                param[2].Value = ToDate;

                sproc = new StoreProcedure(EmployeeContractKeys.Sp_Sel_H0_EmployeeContract_By_UserId_FromToDate, param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return dt;
        }

        public DataTable GetById(int EmployeeContractId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@EmployeeContractId", SqlDbType.Int, 4)
                };
                param[0].Value = EmployeeContractId;
                sproc = new StoreProcedure(EmployeeContractKeys.Sp_Sel_H0_EmployeeContractById, param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return dt;
        }

        /// <summary>
        ///     Author: Giang
        ///     Date: 12/18/2014
        ///     Content: Get contract from employeecontract table
        /// </summary>
        /// <param name="EmployeeContractId"></param>
        /// <returns></returns>
        public DataTable GetContractById(int EmployeeContractId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@EmployeeContractId", SqlDbType.Int, 4)
                };
                param[0].Value = EmployeeContractId;
                sproc = new StoreProcedure(EmployeeContractKeys.Sp_Sel_H0_EmployeeContractById, param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return dt;
        }

        public DataTable GetByDecisionId(int DecisionId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DecisionId", SqlDbType.Int, 4)
                };
                param[0].Value = DecisionId;
                sproc = new StoreProcedure(EmployeeContractKeys.Sp_Sel_H0_EmployeeContractByDecisionId, param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return dt;
        }

        #endregion
    }
}