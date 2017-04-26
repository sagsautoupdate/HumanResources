using System;
using System.Collections.Generic;
using System.Data;
using HRMBLL.H1.Helper;
using HRMDAL.H1;
using HRMUtil;
using HRMUtil.KeyNames.H0;
using HRMUtil.KeyNames.H1;

namespace HRMBLL.H1
{
    public class LNSCoefficientEmployeeBLL
    {
        #region private fields

        /// <summary>
        ///     ////////////
        ///     cac he so da duoc tinh ra he so cuoi cung bao gom muc huong, 2 he so trong thang
        /// </summary>
        private double _LNS;

        #endregion

        public double LNS
        {
            get { return _LNS; }
            set { _LNS = value; }
        }

        public double LNSPCTN { get; set; }

        #region properties

        public int LNS_CoefficientEmployeeId { get; set; }

        public int EmployeeContractId { get; set; }

        public int UserId { get; set; }

        public string ContractTypeName { get; set; }

        public int CoefficientValueId { get; set; }

        public string LNS_CoefficientEmployeeDescription { get; set; }

        public DateTime CreateDate { get; set; }

        public double LNSWages { get; set; }

        public int LNSUnit { get; set; }

        public string LNSUnitName { get; set; }


        public int CoefficientNameId { get; set; }

        public string CoefficientName { get; set; }


        public int CoefficientLevelId { get; set; }

        public string CoefficientLevelName { get; set; }

        public double CoefficientValue { get; set; }

        public bool Active { get; set; }

        public int PositionId { get; set; }

        public string PositionName { get; set; }

        //public double Wages
        //{
        //    get { return _Wages; }
        //    set { _Wages = value; }
        //}

        //public int Unit
        //{
        //    get { return _Unit; }
        //    set { _Unit = value; }
        //}

        //public string UnitName
        //{
        //    get { return _UnitName; }
        //    set { _UnitName = value; }
        //}

        public double PCTN { get; set; }

        public int SalaryRegulationId { get; set; }

        public int CoefficientNameType { get; set; }

        #endregion

        #region public methods insert, update, delete

        public long Save()
        {
            var objDAL = new LNSCoefficientEmployeesDAL();

            if (LNS_CoefficientEmployeeId <= 0)
                return objDAL.Insert(EmployeeContractId, UserId, CoefficientValueId, PCTN, LNSWages, LNSUnit,
                    LNS_CoefficientEmployeeDescription, CreateDate);
            return objDAL.Update(LNS_CoefficientEmployeeId, UserId, EmployeeContractId, CoefficientValueId, PCTN,
                LNSWages, LNSUnit, LNS_CoefficientEmployeeDescription, CreateDate, Active);
        }


        public static int Update(int lNS_CoefficientEmployeeId, int userId, int employeeContractId,
            string contractTypeName, string positionName, int coefficientNameId, int coefficientLevelId,
            string coefficientValue, double lNSWages, int lNSUnit, DateTime createDate,
            string lNS_CoefficientEmployeeDescription, bool active, double pCTN)
        {
            var objDAL = new LNSCoefficientEmployeesDAL();

            var objBLL = CoefficientValuesBLL.GetByName_Level(coefficientNameId, coefficientLevelId);
            var coefficientValueId = 0;
            if (objBLL != null)
                coefficientValueId = objBLL.CoefficientValueId;

            if (lNS_CoefficientEmployeeDescription == null)
                lNS_CoefficientEmployeeDescription = string.Empty;

            var successful = objDAL.Update(lNS_CoefficientEmployeeId, userId, employeeContractId, coefficientValueId,
                pCTN, lNSWages, lNSUnit, lNS_CoefficientEmployeeDescription, createDate, active);


            return successful;
        }

        public static int Delete(int lNS_CoefficientEmployeeId)
        {
            var objDAL = new LNSCoefficientEmployeesDAL();
            return objDAL.Delete(lNS_CoefficientEmployeeId);
        }

        public static int DeleteByEmployeeContractId(int employeeContractId)
        {
            return new LNSCoefficientEmployeesDAL().DeleteByEmployeeContractId(employeeContractId);
        }

        #endregion

        #region public methods GET

        public static List<LNSCoefficientEmployeeBLL> GetByUserId(int userId, int InUse)
        {
            return
                GenerateListLNS_CoefficientEmployeeBLLFromDataTable(new LNSCoefficientEmployeesDAL().GetByUserId(
                    userId, InUse));
        }

        /// <summary>
        ///     Author: Giang
        ///     Date: 22-Oct-14
        ///     Contents: Lay LNS tra ve DT
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="InUse"></param>
        /// <returns></returns>
        public static DataTable GetByUserIdToDT(int userId, int InUse)
        {
            return new LNSCoefficientEmployeesDAL().GetByUserId(userId, InUse);
        }

        public static LNSCoefficientEmployeeBLL GetByEmployeeContractId(int employeeContractId)
        {
            var list =
                GenerateListLNS_CoefficientEmployeeBLLFromDataTable(
                    new LNSCoefficientEmployeesDAL().GetByEmployeeContractId(employeeContractId));

            if (list.Count > 0)
                return list[list.Count - 1];
            return null;
        }

        public static LNSCoefficientEmployeeBLL GetByUserIdForNew(int userId)
        {
            var list =
                GenerateListLNS_CoefficientEmployeeBLLFromDataTable(
                    new LNSCoefficientEmployeesDAL().GetByUserIdForNew(userId));

            if (list.Count == 1)
                return list[0];
            return null;
        }

        public static LNSCoefficientEmployeeBLL GetByUserIdDate(int userId, DateTime createDate)
        {
            var XQD = int.Parse(DefaultValues.XQDSalary(createDate.Month, createDate.Year).ToString());
            var list =
                GenerateFinalListLNS_CoefficientEmployeeBLLFromDataTable(
                    new LNSCoefficientEmployeesDAL().GetByUserIdDate(userId, createDate, XQD));
            if (list.Count == 1)
                return list[0];
            return null;
        }

        #endregion

        #region private methods

        private static List<LNSCoefficientEmployeeBLL> GenerateListLNS_CoefficientEmployeeBLLFromDataTable(DataTable dt)
        {
            var list = new List<LNSCoefficientEmployeeBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateLNS_CoefficientEmployeeBLLFromDataRow(dr));

            return list;
        }

        private static LNSCoefficientEmployeeBLL GenerateLNS_CoefficientEmployeeBLLFromDataRow(DataRow dr)
        {
            var objBLL = new LNSCoefficientEmployeeBLL();
            objBLL.LNS_CoefficientEmployeeId = dr[LNS_CoefficientEmployeeKeys.FIELD_LNS_COEFFICIENT_EMPLOYEE_ID] ==
                                               DBNull.Value
                ? DefaultValues.LNS_CoefficientEmployeeIdMinValue
                : int.Parse(dr[LNS_CoefficientEmployeeKeys.FIELD_LNS_COEFFICIENT_EMPLOYEE_ID].ToString());
            objBLL.EmployeeContractId = dr[EmployeeContractKeys.Field_EmployeeContract_EmployeeContractId] ==
                                        DBNull.Value
                ? 0
                : int.Parse(dr[EmployeeContractKeys.Field_EmployeeContract_EmployeeContractId].ToString());
            objBLL.CoefficientValueId = dr[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE_ID] == DBNull.Value
                ? 0
                : int.Parse(dr[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE_ID].ToString());
            objBLL.LNS_CoefficientEmployeeDescription =
                dr[LNS_CoefficientEmployeeKeys.FIELD_LNS_COEFFICIENT_EMPLOYEE_DESCRIPTION] == DBNull.Value
                    ? string.Empty
                    : dr[LNS_CoefficientEmployeeKeys.FIELD_LNS_COEFFICIENT_EMPLOYEE_DESCRIPTION].ToString();

            objBLL.UserId = dr[EmployeeKeys.FIELD_EMPLOYEES_USERID] == DBNull.Value
                ? 0
                : int.Parse(dr[EmployeeKeys.FIELD_EMPLOYEES_USERID].ToString());

            objBLL.LNSWages = dr[LNS_CoefficientEmployeeKeys.FIELD_LNS_COEFFICIENT_EMPLOYEE_LNSWages] == DBNull.Value
                ? 0
                : double.Parse(dr[LNS_CoefficientEmployeeKeys.FIELD_LNS_COEFFICIENT_EMPLOYEE_LNSWages].ToString());
            objBLL.LNSUnit = dr[LNS_CoefficientEmployeeKeys.FIELD_LNS_COEFFICIENT_EMPLOYEE_LNSUnit] == DBNull.Value
                ? 0
                : int.Parse(dr[LNS_CoefficientEmployeeKeys.FIELD_LNS_COEFFICIENT_EMPLOYEE_LNSUnit].ToString());


            objBLL.CoefficientNameId = dr[CoefficientNameKeys.Field_CoefficientNames_CoefficientNameId] == DBNull.Value
                ? DefaultValues.CoefficientNameIdMinValue
                : int.Parse(dr[CoefficientNameKeys.Field_CoefficientNames_CoefficientNameId].ToString());
            objBLL.CoefficientName = dr[CoefficientNameKeys.Field_CoefficientNames_CoefficientName] == DBNull.Value
                ? string.Empty
                : dr[CoefficientNameKeys.Field_CoefficientNames_CoefficientName].ToString();

            objBLL.CoefficientLevelId = dr[CoefficientLevelKeys.FIELD_COEFFICIENT_LEVEL_ID] == DBNull.Value
                ? DefaultValues.CoefficientNameIdMinValue
                : int.Parse(dr[CoefficientLevelKeys.FIELD_COEFFICIENT_LEVEL_ID].ToString());
            objBLL.CoefficientLevelName = dr[CoefficientLevelKeys.FIELD_COEFFICIENT_LEVEL_NAME] == DBNull.Value
                ? string.Empty
                : dr[CoefficientLevelKeys.FIELD_COEFFICIENT_LEVEL_NAME].ToString();

            objBLL.CoefficientValue = dr[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE] == DBNull.Value
                ? 0
                : double.Parse(dr[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE].ToString());

            objBLL.CreateDate = dr[LNS_CoefficientEmployeeKeys.FIELD_LNS_COEFFICIENT_EMPLOYEE_CREATEDATE] ==
                                DBNull.Value
                ? DateTime.MinValue
                : Convert.ToDateTime(
                    dr[LNS_CoefficientEmployeeKeys.FIELD_LNS_COEFFICIENT_EMPLOYEE_CREATEDATE].ToString());

            objBLL.Active = dr[LNS_CoefficientEmployeeKeys.FIELD_LNS_COEFFICIENT_EMPLOYEE_ACTIVE] == DBNull.Value
                ? false
                : Convert.ToBoolean(dr[LNS_CoefficientEmployeeKeys.FIELD_LNS_COEFFICIENT_EMPLOYEE_ACTIVE].ToString());

            //objBLL.PositionId = dr[PositionKeys.FIELD_POSITION_ID] == DBNull.Value ? 0 : int.Parse(dr[PositionKeys.FIELD_POSITION_ID].ToString());
            //objBLL.PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value ? string.Empty : dr[PositionKeys.FIELD_POSITION_NAME].ToString();
            //objBLL.ContractTypeName = dr[ContractTypeKeys.Field_ContractTypes_ContractTypeName] == DBNull.Value ? string.Empty : dr[ContractTypeKeys.Field_ContractTypes_ContractTypeName].ToString();            
            objBLL.PCTN = dr[LNS_CoefficientEmployeeKeys.FIELD_LNS_COEFFICIENT_EMPLOYEE_PCTN] == DBNull.Value
                ? 0
                : (double) dr[LNS_CoefficientEmployeeKeys.FIELD_LNS_COEFFICIENT_EMPLOYEE_PCTN];

            objBLL.LNSWages = dr[LNS_CoefficientEmployeeKeys.FIELD_LNS_COEFFICIENT_EMPLOYEE_LNSWages] == DBNull.Value
                ? 0
                : (double) dr[LNS_CoefficientEmployeeKeys.FIELD_LNS_COEFFICIENT_EMPLOYEE_LNSWages];
            objBLL.LNSUnit = dr[LNS_CoefficientEmployeeKeys.FIELD_LNS_COEFFICIENT_EMPLOYEE_LNSUnit] == DBNull.Value
                ? 0
                : (int) dr[LNS_CoefficientEmployeeKeys.FIELD_LNS_COEFFICIENT_EMPLOYEE_LNSUnit];

            objBLL.LNSUnitName = Constants.GetUnitNameById(objBLL.LNSUnit);

            try
            {
                objBLL.SalaryRegulationId = dr[SalaryRegulationKeys.Field_SalaryRegulation_SalaryRegulationId] ==
                                            DBNull.Value
                    ? 0
                    : (int) dr[SalaryRegulationKeys.Field_SalaryRegulation_SalaryRegulationId];
            }
            catch
            {
            }
            try
            {
                objBLL.CoefficientNameType = dr["Type"] == DBNull.Value ? 0 : (int) dr["Type"];
            }
            catch
            {
            }


            return objBLL;
        }


        private static List<LNSCoefficientEmployeeBLL> GenerateFinalListLNS_CoefficientEmployeeBLLFromDataTable(
            DataTable dt)
        {
            var list = new List<LNSCoefficientEmployeeBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateFinalLNS_CoefficientEmployeeBLLFromDataRow(dr));

            return list;
        }

        private static LNSCoefficientEmployeeBLL GenerateFinalLNS_CoefficientEmployeeBLLFromDataRow(DataRow dr)
        {
            var objBLL = new LNSCoefficientEmployeeBLL();

            objBLL.UserId = dr[EmployeeKeys.FIELD_EMPLOYEES_USERID] == DBNull.Value
                ? 0
                : int.Parse(dr[EmployeeKeys.FIELD_EMPLOYEES_USERID].ToString());
            objBLL.LNS = dr[LNS_CoefficientEmployeeKeys.Field_LNS_Coefficeint_LNS] == DBNull.Value
                ? 0
                : double.Parse(dr[LNS_CoefficientEmployeeKeys.Field_LNS_Coefficeint_LNS].ToString());
            objBLL.LNSPCTN = dr[LNS_CoefficientEmployeeKeys.Field_LNS_Coefficeint_LNSPCTN] == DBNull.Value
                ? 0
                : double.Parse(dr[LNS_CoefficientEmployeeKeys.Field_LNS_Coefficeint_LNSPCTN].ToString());
            return objBLL;
        }

        #endregion
    }
}