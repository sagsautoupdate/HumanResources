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
    public class LCBCoefficientEmployeeBLL
    {
        #region private fields

        #endregion

        #region properties

        public int LCB_CoefficientEmployeeId { get; set; }

        public int UserId { get; set; }

        public string FullName { get; set; }

        public int CoefficientValueId { get; set; }

        public int Conditions { get; set; }

        public string LCB_CoefficientEmployeeDescription { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public double LCBWages { get; set; }

        public int LCBUnit { get; set; }

        public string LCBUnitName { get; set; }

        public int CoefficientNameId { get; set; }

        public string CoefficientName { get; set; }

        public int EmployeeContractId { get; set; }

        public string ContractTypeName { get; set; }

        public string ContractTypeCode { get; set; }

        public int ContractTypeId { get; set; }

        public int CoefficientLevelId { get; set; }

        public string CoefficientLevelName { get; set; }

        public double CoefficientValue { get; set; }

        public double LCB { get; set; }

        public bool Active { get; set; }

        public int PositionId { get; set; }

        public string PositionName { get; set; }

        public int SalaryRegulationId { get; set; }

        public double PCDH { get; set; }

        public double PCTN { get; set; }

        public double PCCV { get; set; }

        public double PCKV { get; set; }

        #endregion

        #region Constructor

        public LCBCoefficientEmployeeBLL()
        {
        }

        public LCBCoefficientEmployeeBLL(int LCB_CoefficientEmployeeId, int employeeContractId, int coefficientValueId,
            DateTime fromDate, DateTime toDate, string LCB_CoefficientEmployeeDescription)
        {
            this.LCB_CoefficientEmployeeId = LCB_CoefficientEmployeeId;
            EmployeeContractId = employeeContractId;
            CoefficientValueId = coefficientValueId;
            this.LCB_CoefficientEmployeeDescription = LCB_CoefficientEmployeeDescription;
            FromDate = fromDate;
            ToDate = toDate;
        }

        #endregion

        #region public methods insert, update, delete

        public long Save()
        {
            var objDAL = new LCBCoefficientEmployeesDAL();

            if (LCB_CoefficientEmployeeId <= 0)
                return objDAL.Insert(EmployeeContractId, UserId, CoefficientValueId, LCBWages, LCBUnit, FromDate, ToDate,
                    LCB_CoefficientEmployeeDescription, PCDH, PCTN, PCCV, PCKV);
            return objDAL.Update(LCB_CoefficientEmployeeId, UserId, EmployeeContractId, CoefficientValueId, LCBWages,
                LCBUnit, FromDate, ToDate, LCB_CoefficientEmployeeDescription, Active, PCDH, PCTN, PCCV, PCKV);
        }


        //public static long Update(int lCB_CoefficientEmployeeId, int userId, int employeeContractId, string positionName, int coefficientNameId, int coefficientLevelId, string coefficientValue, double lCBWages, int lCBUnit, DateTime fromDate, DateTime toDate, string lCB_CoefficientEmployeeDescription, bool active, string contractTypeName, string conditions, int contractTypeId)
        //{

        //    LCBCoefficientEmployeesDAL objDAL = new LCBCoefficientEmployeesDAL();

        //    CoefficientValuesBLL objBLL = CoefficientValuesBLL.GetByName_Level(coefficientNameId, coefficientLevelId);
        //    int coefficientValueId = 0;
        //    if (objBLL != null)
        //    {
        //        coefficientValueId = objBLL.CoefficientValueId;
        //    }
        //    if (lCB_CoefficientEmployeeDescription == null)
        //        lCB_CoefficientEmployeeDescription = string.Empty;
        //    int successful = objDAL.Update(lCB_CoefficientEmployeeId, userId, employeeContractId, coefficientValueId, lCBWages, lCBUnit, fromDate, toDate, lCB_CoefficientEmployeeDescription, active);            

        //    return successful;
        //}

        public static long Delete(int lCB_CoefficientEmployeeId)
        {
            var objDAL = new LCBCoefficientEmployeesDAL();
            return objDAL.Delete(lCB_CoefficientEmployeeId);
        }

        public static long DeleteByEmployeeContractId(int employeeContractId)
        {
            return new LCBCoefficientEmployeesDAL().DeleteByEmployeeContractId(employeeContractId);
        }

        #endregion

        #region public methods GET

        public static List<LCBCoefficientEmployeeBLL> GetByUserId(int userId)
        {
            return
                GenerateListLCB_CoefficientEmployeeBLLFromDataTable(new LCBCoefficientEmployeesDAL().GetByUserId(userId));
        }

        public static DataTable GetByUserIdToDT(int userId)
        {
            return new LCBCoefficientEmployeesDAL().GetByUserId(userId);
        }

        public static List<LCBCoefficientEmployeeBLL> RemindLCBCoefficient(string fullName, int deptId, int day,
            int month, int year)
        {
            return
                GenerateListLCB_CoefficientEmployeeBLLFromDataTable(
                    new LCBCoefficientEmployeesDAL().RemindLCBCoefficient(fullName, deptId, day, month, year));
        }

        public static List<LCBCoefficientEmployeeBLL> ChangedLCBCoefficient(string fullName, int deptId, int day,
            int month, int year)
        {
            return
                GenerateListLCB_CoefficientEmployeeBLLFromDataTable(
                    new LCBCoefficientEmployeesDAL().ChangedLCBCoefficient(fullName, deptId, day, month, year));
        }

        public static LCBCoefficientEmployeeBLL GetByUserIdFromToDateFinal(int userId, DateTime fromDate,
            DateTime toDate, int XQD)
        {
            var list =
                GenerateListLCB_CoefficientEmployeeBLLFinalFromDataTable(
                    new LCBCoefficientEmployeesDAL().GetByUserIdFromToDateFinal(userId, fromDate, toDate, XQD));
            if (list.Count == 1)
                return list[0];
            return null;
        }

        public static LCBCoefficientEmployeeBLL GetByEmployeeContractId(int employeeContractId)
        {
            var list =
                GenerateListLCB_CoefficientEmployeeBLLFromDataTable(
                    new LCBCoefficientEmployeesDAL().GetByEmployeeContractId(employeeContractId));

            //if (list.Count == 1)
            //{
            //    return list[0];
            //}
            //else
            //{
            //    return null;
            //}
            if (list.Count > 0)
                return list[list.Count - 1];
            return null;
        }

        public static LCBCoefficientEmployeeBLL GetByUserIdForNew(int userId)
        {
            var list =
                GenerateListLCB_CoefficientEmployeeBLLFromDataTable(
                    new LCBCoefficientEmployeesDAL().GetByUserIdForNew(userId));
            if (list.Count == 1)
                return list[0];
            return null;
        }

        //public static LCBCoefficientEmployeeBLL GetCurrentByUserIdDate(int userId, DateTime fromDate)
        //{

        //    List<LCBCoefficientEmployeeBLL> list = GenerateListLCB_CoefficientEmployeeBLLFromDataTable(new LCBCoefficientEmployeesDAL().GetCurrentByUserIdDate(userId, fromDate));

        //    if (list.Count == 1)
        //    {
        //        return list[0];
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //public static LCBCoefficientEmployeeBLL GetByUserIdToDateContractType(int userId, DateTime fromDate, int contractTypeId)
        //{

        //    List<LCBCoefficientEmployeeBLL> list = GenerateListLCB_CoefficientEmployeeBLLFromDataTable(new LCBCoefficientEmployeesDAL().GetByUserIdToDateContractType(userId, fromDate, contractTypeId));

        //    if (list.Count == 1)
        //    {
        //        return list[0];
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        #endregion

        #region private methods

        private static List<LCBCoefficientEmployeeBLL> GenerateListLCB_CoefficientEmployeeBLLFromDataTable(DataTable dt)
        {
            var list = new List<LCBCoefficientEmployeeBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateLCB_CoefficientEmployeeBLLFromDataRow(dr));

            return list;
        }

        private static LCBCoefficientEmployeeBLL GenerateLCB_CoefficientEmployeeBLLFromDataRow(DataRow dr)
        {
            var objBLL = new LCBCoefficientEmployeeBLL(
                dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_LCB_CoefficientEmployeeId] == DBNull.Value
                    ? DefaultValues.LCB_CoefficientEmployeeIdMinValue
                    : int.Parse(
                        dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_LCB_CoefficientEmployeeId]
                            .ToString()),
                dr[EmployeeContractKeys.Field_EmployeeContract_EmployeeContractId] == DBNull.Value
                    ? DefaultValues.UserIdMinValue
                    : int.Parse(dr[EmployeeContractKeys.Field_EmployeeContract_EmployeeContractId].ToString()),
                dr[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE_ID] == DBNull.Value
                    ? DefaultValues.CoefficientValueIdMinValue
                    : int.Parse(dr[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE_ID].ToString()),
                dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_FromDate] == DBNull.Value
                    ? DateTime.MinValue
                    : Convert.ToDateTime(
                        dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_FromDate].ToString()),
                dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_ToDate] == DBNull.Value
                    ? DateTime.MinValue
                    : Convert.ToDateTime(
                        dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_ToDate].ToString()),
                dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_LCB_CoefficientEmployeeDescription] ==
                DBNull.Value
                    ? string.Empty
                    : dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_LCB_CoefficientEmployeeDescription]
                        .ToString()
            );

            objBLL.UserId = dr[EmployeeKeys.FIELD_EMPLOYEES_USERID] == DBNull.Value
                ? 0
                : int.Parse(dr[EmployeeKeys.FIELD_EMPLOYEES_USERID].ToString());
            try
            {
                objBLL.FullName = dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();
            }
            catch
            {
            }

            objBLL.LCBWages = dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_LCBWages] == DBNull.Value
                ? 0
                : double.Parse(dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_LCBWages].ToString());
            objBLL.LCBUnit = dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_LCBUnit] == DBNull.Value
                ? 0
                : int.Parse(dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_LCBUnit].ToString());
            objBLL.LCBUnitName = Constants.GetUnitNameById(objBLL.LCBUnit);

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
            objBLL.Conditions = dr[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS] == DBNull.Value
                ? 0
                : int.Parse(dr[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS].ToString());

            objBLL.Active = dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_Active] == DBNull.Value
                ? false
                : Convert.ToBoolean(dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_Active].ToString());

            //objBLL.PositionId = dr[PositionKeys.FIELD_POSITION_ID] == DBNull.Value ? 0 : int.Parse(dr[PositionKeys.FIELD_POSITION_ID].ToString());
            //objBLL.PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value ? string.Empty : dr[PositionKeys.FIELD_POSITION_NAME].ToString();
            //objBLL.ContractTypeName = dr[ContractTypeKeys.Field_ContractTypes_ContractTypeName] == DBNull.Value ? string.Empty : dr[ContractTypeKeys.Field_ContractTypes_ContractTypeName].ToString();
            //objBLL.ContractTypeCode = dr[ContractTypeKeys.Field_ContractTypes_ContractTypeCode] == DBNull.Value ? string.Empty : dr[ContractTypeKeys.Field_ContractTypes_ContractTypeCode].ToString();
            //objBLL.ContractTypeId = dr[ContractTypeKeys.Field_ContractTypes_ContractTypeId] == DBNull.Value ? 0 : int.Parse(dr[ContractTypeKeys.Field_ContractTypes_ContractTypeId].ToString());
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

            objBLL.PCDH = dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_PCDH] == DBNull.Value
                ? 0
                : Convert.ToDouble(dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_PCDH].ToString());
            objBLL.PCTN = dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_PCTN] == DBNull.Value
                ? 0
                : Convert.ToDouble(dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_PCTN].ToString());
            objBLL.PCCV = dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_PCCV] == DBNull.Value
                ? 0
                : Convert.ToDouble(dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_PCCV].ToString());
            objBLL.PCKV = dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_PCKV] == DBNull.Value
                ? 0
                : Convert.ToDouble(dr[LCB_CoefficientEmployeeKeys.Field_LCB_CoefficientEmployees_PCKV].ToString());


            return objBLL;
        }

        private static List<LCBCoefficientEmployeeBLL> GenerateListLCB_CoefficientEmployeeBLLFinalFromDataTable(
            DataTable dt)
        {
            var list = new List<LCBCoefficientEmployeeBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateLCB_CoefficientEmployeeBLLFinalFromDataRow(dr));

            return list;
        }

        private static LCBCoefficientEmployeeBLL GenerateLCB_CoefficientEmployeeBLLFinalFromDataRow(DataRow dr)
        {
            var objBLL = new LCBCoefficientEmployeeBLL();

            objBLL.UserId = dr[EmployeeKeys.FIELD_EMPLOYEES_USERID] == DBNull.Value
                ? 0
                : int.Parse(dr[EmployeeKeys.FIELD_EMPLOYEES_USERID].ToString());

            objBLL.LCB = dr[CoefficientValueKeys.Field_Coefficient_LCB] == DBNull.Value
                ? 0
                : double.Parse(dr[CoefficientValueKeys.Field_Coefficient_LCB].ToString());

            return objBLL;
        }

        #endregion
    }
}