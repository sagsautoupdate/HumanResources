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
    public class CoefficientEmployeesBLL
    {
        #region private fields

        #endregion

        #region properties

        public double TotalLCB { get; set; }

        public double TotalLNS { get; set; }

        public double TotalLCBP { get; set; }

        public double TotalLNSP { get; set; }

        public double TotalPCDH { get; set; }

        public double TotalPCTN { get; set; }

        public double TotalPCCV { get; set; }

        public double TotalPCKV { get; set; }

        #region properties

        public int CoefficientEmployeeId { get; set; }

        public double K { get; set; }

        public double LCB { get; set; }

        public double LNS { get; set; }

        public double PCDH { get; set; }

        public double PCTN { get; set; }

        public double PCCV { get; set; }

        public double PCKV { get; set; }

        public double LNSPCTN { get; set; }

        public string Description { get; set; }

        public bool ACTIVE { get; set; }

        public bool LOCK { get; set; }

        public DateTime CREATEDATE { get; set; }


        public string ContractTypeName { get; set; }

        public string ContractTypeCode { get; set; }


        public double Wages { get; set; }

        public int Unit { get; set; }

        public string UnitName { get; set; }

        public int UserId { get; set; }

        public string UserCode { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string PositionName { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentFullName { get; set; }

        public string RootName { get; set; }

        /// <summary>
        /// </summary>
        public double LNSWages { get; set; }

        public int LNSUnit { get; set; }

        public string LNSUnitName { get; set; }

        #endregion

        /// <summary>
        /// </summary>
        public double LCBWages { get; set; }

        public int LCBUnit { get; set; }

        public string LCBUnitName { get; set; }

        #endregion

        #region constructor

        public CoefficientEmployeesBLL()
        {
        }

        public CoefficientEmployeesBLL(int coefficientEmployeeId, int userId,
            double pCDH, double pCTN, double pCCV, double pCKV, string description, DateTime createDate, bool active)
        {
            CoefficientEmployeeId = coefficientEmployeeId;
            UserId = userId;
            PCDH = pCDH;
            PCTN = pCTN;
            PCCV = pCCV;
            PCKV = pCKV;
            Description = description;
            CREATEDATE = createDate;
            ACTIVE = active;
        }


        public CoefficientEmployeesBLL(int userId, string userName, string userCode, string fullName,
            string deparmentName, string positionName)
        {
            UserId = userId;
            UserName = userName;
            UserCode = userCode;
            PositionName = positionName;
            FullName = fullName;
            DepartmentName = deparmentName;
        }

        #endregion

        #region public methods : insert, update, delete

        public long Save()
        {
            var objDAL = new CoefficientEmployeesDAL();
            if (CoefficientEmployeeId <= 0)
                return objDAL.Insert(UserId, PCDH, PCTN, PCCV, PCKV, Description, ACTIVE, CREATEDATE);
            return objDAL.Update(UserId, PCDH, PCTN, PCCV, PCKV, Description, ACTIVE, CREATEDATE, CoefficientEmployeeId);
        }

        public static long Update(int coefficientEmployeeId, int userId, double lCB, double lNS, double pCDH,
            double pCTN,
            double pCCV, double pCKV, string description, bool active, DateTime createDate)
        {
            var objDAL = new CoefficientEmployeesDAL();

            if (description == null)
                description = string.Empty;
            return objDAL.Update(userId, pCDH, pCTN, pCCV, pCKV, description, active, createDate, coefficientEmployeeId);
        }

        public static long Delete(int coefficientEmployeeId)
        {
            var objDAL = new CoefficientEmployeesDAL();
            return objDAL.Delete(coefficientEmployeeId);
        }

        #endregion

        #region public static methods : GET

        //public static List<CoefficientEmployeesBLL> AllCoefficientEmployeeGetByFilter(string fullName, int departmentId, int LNSwages, string LNSstrOperator, int LCBwages, string LCBstrOperator, string sortParameter)
        //{
        //    List<CoefficientEmployeesBLL> list = GenerateListCoefficientBLLFromDataTableAll(new CoefficientEmployeesDAL().AllCoefficientEmployeeGetByFilter(fullName, departmentId, LNSstrOperator,LNSwages, LCBstrOperator, LCBwages));
        //    if (!String.IsNullOrEmpty(sortParameter))
        //        list.Sort(new CoefficientEmployeesBLLComparer(sortParameter));
        //    return list;
        //}
        //public static CoefficientEmployeesBLL AllCoefficientEmployeeGetTotalByFilter(string fullName, int departmentId, int LNSwages, string LNSstrOperator, int LCBwages, string LCBstrOperator, string sortParameter)
        //{
        //    List<CoefficientEmployeesBLL> list = GenerateListCoefficientBLLFromDataTableAllTotal(new CoefficientEmployeesDAL().AllCoefficientEmployeeGetTotalByFilter(fullName, departmentId, LNSstrOperator, LNSwages, LCBstrOperator, LCBwages));
        //    if (list.Count == 1)
        //    {
        //        return list[0];
        //    }
        //    else
        //    {
        //        return null;
        //    }            
        //}
        //public static CoefficientEmployeesBLL AllCoefficientEmployeeGetByUserId(int userId)
        //{
        //    List<CoefficientEmployeesBLL> list = GenerateListCoefficientBLLFromDataTableAll(new CoefficientEmployeesDAL().AllCoefficientEmployeeGetByUserId(userId));
        //    if (list.Count == 1)
        //    {
        //        return list[0];
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}


        public static List<CoefficientEmployeesBLL> GetByUserId(int userId)
        {
            return GenerateListCoefficientBLLFromDataTable(new CoefficientEmployeesDAL().GetByUserId(userId));
        }

        public static List<CoefficientEmployeesBLL> GetByUserIdDate(int userId, DateTime createDate)
        {
            return
                GenerateListCoefficientBLLFromDataTable(new CoefficientEmployeesDAL().GetByUserIdDate(userId, createDate));
        }

        public static CoefficientEmployeesBLL GetByUserIdDateFinal(int userId, DateTime createDate, int XQD)
        {
            var list =
                GenerateListCoefficientBLLFinalFromDataTable(new CoefficientEmployeesDAL().GetByUserIdDateFinal(userId,
                    createDate, XQD));

            if (list.Count == 1)
                return list[0];
            return null;
        }

        public static CoefficientEmployeesBLL GetByUserIdForNew(int userId)
        {
            var list =
                GenerateListCoefficientBLLFinalFromDataTable(new CoefficientEmployeesDAL().GetByUserIdForNew(userId));

            if (list.Count == 1)
                return list[0];
            return null;
        }

        #endregion

        #region private methods, generate helper methods                                     

        private static List<CoefficientEmployeesBLL> GenerateListCoefficientBLLFromDataTable(DataTable dt)
        {
            var list = new List<CoefficientEmployeesBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateCoefficientBLLFromDataRow(dr));

            return list;
        }

        private static List<CoefficientEmployeesBLL> GenerateListCoefficientBLLFinalFromDataTable(DataTable dt)
        {
            var list = new List<CoefficientEmployeesBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateCoefficientBLLFinalFromDataRow(dr));

            return list;
        }

        private static List<CoefficientEmployeesBLL> GenerateListCoefficientBLLFromDataTableAll(DataTable dt)
        {
            var list = new List<CoefficientEmployeesBLL>();
            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateCoefficientBLLFromDataRowAll(dr));

            return list;
        }

        private static List<CoefficientEmployeesBLL> GenerateListCoefficientBLLFromDataTableAllTotal(DataTable dt)
        {
            var list = new List<CoefficientEmployeesBLL>();
            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateCoefficientBLLFromDataRowAllTotal(dr));

            return list;
        }

        private static CoefficientEmployeesBLL GenerateCoefficientBLLFromDataRow(DataRow dr)
        {
            var objBLL = new CoefficientEmployeesBLL(
                dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_ID].ToString()),
                dr[EmployeeKeys.FIELD_EMPLOYEES_USERID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[EmployeeKeys.FIELD_EMPLOYEES_USERID].ToString()),
                dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_PCDH] == DBNull.Value
                    ? 0
                    : Convert.ToDouble(dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_PCDH].ToString()),
                dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_PCTN] == DBNull.Value
                    ? 0
                    : Convert.ToDouble(dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_PCTN].ToString()),
                dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_PCCV] == DBNull.Value
                    ? 0
                    : Convert.ToDouble(dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_PCCV].ToString()),
                dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_PCKV] == DBNull.Value
                    ? 0
                    : Convert.ToDouble(dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_PCKV].ToString()),
                dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_DESCRIPTION] == DBNull.Value
                    ? string.Empty
                    : dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_DESCRIPTION].ToString(),
                dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_CREATEDATE] == DBNull.Value
                    ? DateTime.MinValue
                    : Convert.ToDateTime(dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_CREATEDATE].ToString()),
                dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_ACTIVE] == DBNull.Value
                    ? false
                    : Convert.ToBoolean(dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_ACTIVE].ToString())
            );
            return objBLL;
        }

        private static CoefficientEmployeesBLL GenerateCoefficientBLLFinalFromDataRow(DataRow dr)
        {
            var objBLL = new CoefficientEmployeesBLL();
            objBLL.UserId = dr[EmployeeKeys.FIELD_EMPLOYEES_USERID] == DBNull.Value
                ? 0
                : int.Parse(dr[EmployeeKeys.FIELD_EMPLOYEES_USERID].ToString());
            objBLL.PCDH = dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_PCDH] == DBNull.Value
                ? 0
                : Convert.ToDouble(dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_PCDH].ToString());
            objBLL.PCTN = dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_PCTN] == DBNull.Value
                ? 0
                : Convert.ToDouble(dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_PCTN].ToString());
            objBLL.PCCV = dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_PCCV] == DBNull.Value
                ? 0
                : Convert.ToDouble(dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_PCCV].ToString());
            objBLL.PCKV = dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_PCKV] == DBNull.Value
                ? 0
                : Convert.ToDouble(dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_PCKV].ToString());
            return objBLL;
        }

        private static CoefficientEmployeesBLL GenerateCoefficientBLLFromDataRowAll(DataRow dr)
        {
            var objBLL = new CoefficientEmployeesBLL();
            try
            {
                objBLL.UserId = dr[EmployeeKeys.FIELD_EMPLOYEES_USERID] == DBNull.Value
                    ? DefaultValues.UserIdMinValue
                    : Convert.ToInt32(dr[EmployeeKeys.FIELD_EMPLOYEES_USERID].ToString());
            }
            catch
            {
            }
            try
            {
                objBLL.UserCode = dr[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE].ToString();
            }
            catch
            {
            }
            try
            {
                objBLL.FullName = dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();
            }
            catch
            {
            }
            try
            {
                objBLL.PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[PositionKeys.FIELD_POSITION_NAME].ToString();
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

            try
            {
                objBLL.Wages = dr[EmployeeContractKeys.Field_EmployeeContract_Wages] == DBNull.Value
                    ? 0
                    : double.Parse(dr[EmployeeContractKeys.Field_EmployeeContract_Wages].ToString());
            }
            catch
            {
            }
            try
            {
                objBLL.Unit = dr[EmployeeContractKeys.Field_EmployeeContract_Unit] == DBNull.Value
                    ? 0
                    : int.Parse(dr[EmployeeContractKeys.Field_EmployeeContract_Unit].ToString());
                objBLL.UnitName = Constants.GetUnitNameById(objBLL.Unit);
            }
            catch
            {
            }
            try
            {
                objBLL.ContractTypeName = dr[ContractTypeKeys.Field_ContractTypes_ContractTypeName] == DBNull.Value
                    ? string.Empty
                    : dr[ContractTypeKeys.Field_ContractTypes_ContractTypeName].ToString();
            }
            catch
            {
            }
            try
            {
                objBLL.ContractTypeCode = dr[ContractTypeKeys.Field_ContractTypes_ContractTypeCode] == DBNull.Value
                    ? string.Empty
                    : dr[ContractTypeKeys.Field_ContractTypes_ContractTypeCode].ToString();
            }
            catch
            {
            }


            objBLL.CoefficientEmployeeId = dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_ID] == DBNull.Value
                ? 0
                : int.Parse(dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_ID].ToString());
            objBLL.PCDH = dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_PCDH] == DBNull.Value
                ? 0
                : Convert.ToDouble(dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_PCDH].ToString());
            objBLL.PCTN = dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_PCTN] == DBNull.Value
                ? 0
                : Convert.ToDouble(dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_PCTN].ToString());
            objBLL.PCCV = dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_PCCV] == DBNull.Value
                ? 0
                : Convert.ToDouble(dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_PCCV].ToString());
            objBLL.PCKV = dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_PCKV] == DBNull.Value
                ? 0
                : Convert.ToDouble(dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_PCKV].ToString());
            objBLL.Description = dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_DESCRIPTION] == DBNull.Value
                ? string.Empty
                : dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_DESCRIPTION].ToString();
            objBLL.CREATEDATE = dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_CREATEDATE] == DBNull.Value
                ? DateTime.MinValue
                : Convert.ToDateTime(dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_CREATEDATE].ToString());
            objBLL.ACTIVE = dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_ACTIVE] == DBNull.Value
                ? false
                : Convert.ToBoolean(dr[CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_ACTIVE].ToString());

            try
            {
                objBLL.LNS = dr[CoefficientEmployeeKeys.Field_Coeficient_Employee_LNSCoefficientValue] == DBNull.Value
                    ? 0
                    : double.Parse(dr[CoefficientEmployeeKeys.Field_Coeficient_Employee_LNSCoefficientValue].ToString());
            }
            catch
            {
            }
            try
            {
                objBLL.LNSWages = dr[CoefficientEmployeeKeys.Field_Coeficient_Employee_LNSWages] == DBNull.Value
                    ? 0
                    : double.Parse(dr[CoefficientEmployeeKeys.Field_Coeficient_Employee_LNSWages].ToString());
            }
            catch
            {
            }
            try
            {
                objBLL.LNSUnit = dr[CoefficientEmployeeKeys.Field_Coeficient_Employee_LNSUnit] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CoefficientEmployeeKeys.Field_Coeficient_Employee_LNSUnit].ToString());
            }
            catch
            {
            }
            try
            {
                objBLL.LNSUnitName = Constants.GetUnitNameById(objBLL.LNSUnit);
            }
            catch
            {
            }
            try
            {
                objBLL.LNSPCTN = dr[CoefficientEmployeeKeys.Field_Coeficient_Employee_LNSPCTN] == DBNull.Value
                    ? 0
                    : double.Parse(dr[CoefficientEmployeeKeys.Field_Coeficient_Employee_LNSPCTN].ToString());
            }
            catch
            {
            }
            try
            {
                objBLL.LCB = dr[CoefficientEmployeeKeys.Field_Coeficient_Employee_LCBCoefficientValue] == DBNull.Value
                    ? 0
                    : double.Parse(dr[CoefficientEmployeeKeys.Field_Coeficient_Employee_LCBCoefficientValue].ToString());
            }
            catch
            {
            }
            try
            {
                objBLL.LCBWages = dr[CoefficientEmployeeKeys.Field_Coeficient_Employee_LCBWages] == DBNull.Value
                    ? 0
                    : double.Parse(dr[CoefficientEmployeeKeys.Field_Coeficient_Employee_LCBWages].ToString());
            }
            catch
            {
            }
            try
            {
                objBLL.LCBUnit = dr[CoefficientEmployeeKeys.Field_Coeficient_Employee_LCBUnit] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CoefficientEmployeeKeys.Field_Coeficient_Employee_LCBUnit].ToString());
            }
            catch
            {
            }
            try
            {
                objBLL.LCBUnitName = Constants.GetUnitNameById(objBLL.LCBUnit);
            }
            catch
            {
            }


            return objBLL;
        }

        private static CoefficientEmployeesBLL GenerateCoefficientBLLFromDataRowAllTotal(DataRow dr)
        {
            var objBLL = new CoefficientEmployeesBLL();

            objBLL.TotalLNS = dr[LNS_CoefficientEmployeeKeys.Field_LNS_Coefficeint_TotalLNS] == DBNull.Value
                ? 0
                : double.Parse(dr[LNS_CoefficientEmployeeKeys.Field_LNS_Coefficeint_TotalLNS].ToString());
            objBLL.TotalLNSP = dr[LNS_CoefficientEmployeeKeys.Field_LNS_Coefficeint_TotalLNSP] == DBNull.Value
                ? 0
                : double.Parse(dr[LNS_CoefficientEmployeeKeys.Field_LNS_Coefficeint_TotalLNSP].ToString());

            objBLL.TotalLCB = dr[LCB_CoefficientEmployeeKeys.Field_LCB_Coefficeint_TotalLCB] == DBNull.Value
                ? 0
                : double.Parse(dr[LCB_CoefficientEmployeeKeys.Field_LCB_Coefficeint_TotalLCB].ToString());
            objBLL.TotalLCBP = dr[LCB_CoefficientEmployeeKeys.Field_LCB_Coefficeint_TotalLCBP] == DBNull.Value
                ? 0
                : double.Parse(dr[LCB_CoefficientEmployeeKeys.Field_LCB_Coefficeint_TotalLCBP].ToString());

            objBLL.TotalPCDH = dr[CoefficientEmployeeKeys.Field_Coefficient_Employee_TotalPCDH] == DBNull.Value
                ? 0
                : double.Parse(dr[CoefficientEmployeeKeys.Field_Coefficient_Employee_TotalPCDH].ToString());
            objBLL.TotalPCTN = dr[CoefficientEmployeeKeys.Field_Coefficient_Employee_TotalPCTN] == DBNull.Value
                ? 0
                : double.Parse(dr[CoefficientEmployeeKeys.Field_Coefficient_Employee_TotalPCTN].ToString());
            objBLL.TotalPCKV = dr[CoefficientEmployeeKeys.Field_Coefficient_Employee_TotalPCKV] == DBNull.Value
                ? 0
                : double.Parse(dr[CoefficientEmployeeKeys.Field_Coefficient_Employee_TotalPCKV].ToString());
            objBLL.TotalPCCV = dr[CoefficientEmployeeKeys.Field_Coefficient_Employee_TotalPCCV] == DBNull.Value
                ? 0
                : double.Parse(dr[CoefficientEmployeeKeys.Field_Coefficient_Employee_TotalPCCV].ToString());


            return objBLL;
        }

        #endregion
    }
}