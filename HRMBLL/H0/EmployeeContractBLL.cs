using System;
using System.Collections.Generic;
using System.Data;
using HRMBLL.H0.Helper;
using HRMDAL.H0;
using HRMUtil;
using HRMUtil.KeyNames.H0;

namespace HRMBLL.H0
{
    public class EmployeeContractBLL
    {
        #region private fileds

        #region New field

        #endregion

        public bool IsReplaced { get; set; }

        //-/////////////////////////////////////////////////////////////////////////////////////////////////
        // Old fields
        //-/////////////////////////////////////////////////////////////////////////////////////////////////

        public string CntRemark { get; set; }

        private string _SP = "";
        private string _SPValue = "";
        //-/////////////////////////////////////////////////////////////////////////////////////////////////
        // Old fields
        //-/////////////////////////////////////////////////////////////////////////////////////////////////

        #endregion

        #region properties

        public int ScaleOfSalaryId { get; set; }

        public string ContractTitle { get; set; }

        public string Office { get; set; }

        public string Resident { get; set; }

        public string PlaceOfIssue { get; set; }

        public DateTime DateOfIssue { get; set; }

        public string ID { get; set; }

        public string BirthPlace { get; set; }

        public DateTime Birthday { get; set; } = FormatDate.GetSQLDateMinValue;

        public string DepartmentFullName { get; set; }

        public string DepartmentName { get; set; }

        public string RootName { get; set; }

        public double Staff_BHXH { get; set; }

        public double Staff_BHYT { get; set; }

        public double Staff_BHTN { get; set; }

        public double Company_BHXH { get; set; }

        public double Company_BHYT { get; set; }

        public double Company_BHTN { get; set; }

        public DateTime CreateDate { get; set; }

        public string ScaleOfLNS { get; set; }

        public string LNSCoefficient { get; set; }

        public string ScaleOfLCB { get; set; }

        public string LCBCoefficient { get; set; }

        public int SalaryLevel { get; set; }

        public int EmployeeContractId { get; set; }

        public int PreviousEmployeeContractId { get; set; }

        public int UserId { get; set; }

        public string FullName { get; set; }

        public int ContractTypeId { get; set; }

        public string ContractTypeCode { get; set; }

        public string ContractTypeName { get; set; }

        public string ContractTypeDescription { get; set; }

        public int PositionId { get; set; }

        public string PositionName { get; set; }

        public DateTime FromDate { get; set; } = FormatDate.GetSQLDateMinValue;

        public DateTime ToDate { get; set; } = FormatDate.GetSQLDateMinValue;

        public string Description { get; set; }

        public bool Active { get; set; }

        public double Wages { get; set; }

        public int Unit { get; set; }

        public string UnitName { get; set; }

        public int LevelPosition { get; set; }

        public string ContractNo { get; set; } = "";

        public string ContractName { get; set; } = "";

        public string RepresentativeOfCompany { get; set; } = "";

        public string CompanyName { get; set; } = "";

        public string AttachFileName { get; set; } = "";

        public string WorkingName { get; set; } = "";

        public int WorkingHour { get; set; }

        public int Overtime { get; set; }

        public int SalaryType { get; set; }

        public int DecisionId { get; set; } = 0;

        /////////////////////////////////
        //// new position

        public DateTime PreviousFromDate { get; set; } = DateTime.Now;

        public DateTime PreviousToDate { get; set; } = DateTime.Now;

        public int PreviousPositionId { get; set; }

        public string PreviousPositionName { get; set; }


        ////////////////////////////////////////////
        ///// He so LCB
        public int LCB_CoefficientEmployeeId { get; set; }

        public int CoefficientNameIdLCB { get; set; }

        public string CoefficientNameLCB { get; set; }

        public int CoefficientLevelIdLCB { get; set; }

        public string CoefficientLevelNameLCB { get; set; }

        public int CoefficientValueIdLCB { get; set; }

        public double CoefficientValueLCB { get; set; }

        public double LCBWages { get; set; }

        public int LCBUnit { get; set; }

        public string LCBUnitName { get; set; }

        public int ConditionsLCB { get; set; }

        public DateTime FromDateLCB { get; set; } = DateTime.Now;

        public DateTime ToDateLCB { get; set; } = DateTime.Now;

        public double PCDH { get; set; }

        public double PCTN { get; set; }

        public double PCCV { get; set; }

        public double PCKV { get; set; }

        public double PCCVLevel { get; set; }

        public double PCKVLevel { get; set; }

        ////// phan he so LNS
        public int LNS_CoefficientEmployeeId { get; set; }

        public int CoefficientNameIdLNS { get; set; }

        public string CoefficientNameLNS { get; set; }

        public int CoefficientLevelIdLNS { get; set; }

        public string CoefficientLevelNameLNS { get; set; }

        public int CoefficientValueIdLNS { get; set; }

        public double CoefficientValueLNS { get; set; }

        public double LNSWages { get; set; }

        public int LNSUnit { get; set; }

        public string LNSUnitName { get; set; }

        public double PCTNLNS { get; set; }

        #endregion

        #region constructor

        public EmployeeContractBLL()
        {
        }

        public EmployeeContractBLL(int id, int userId, int contractTypeId, int positionId, DateTime fromDate,
            DateTime toDate, string description)
            : this(
                id, userId, string.Empty, contractTypeId, string.Empty, string.Empty, string.Empty, positionId,
                string.Empty, fromDate, toDate, description, true)
        {
        }


        public EmployeeContractBLL(int employeeContractId, int userId, string fullName, int contractTypeId,
            string contractTypeCode, string contractTypeName,
            string contractTypeDescription, int positionId, string positionName,
            DateTime fromDate, DateTime toDate, string description, bool active)
        {
            EmployeeContractId = employeeContractId;
            UserId = userId;
            FullName = fullName;
            ContractTypeId = contractTypeId;
            ContractTypeCode = contractTypeCode;
            ContractTypeName = contractTypeName;
            ContractTypeDescription = contractTypeDescription;

            PositionId = positionId;
            PositionName = positionName;

            FromDate = fromDate;
            ToDate = toDate;
            Description = description;
            Active = active;
        }

        #endregion

        #region public methods insert, update, delete

        //////////////////////////////////////////////////////////////////
        //public long Save_V1()
        //{
        //    EmployeeContractDAL objDAL = new EmployeeContractDAL();
        //    if (_EmployeeContractId <= 0)
        //    {
        //        return objDAL.Insert_V1(_UserId, 
        //                   _ContractTypeId, 
        //                   _PositionId, 
        //                   _Wages, 
        //                   _Unit, 
        //                   _FromDate, 
        //                   _ToDate, 
        //                   _Description, 
        //                   _Active, 
        //                   //_ContractNo, 
        //                   _ContractName, 
        //                   _RepresentativeOfCompany, 
        //                   _CompanyName, 
        //                   _AttachFileName, 
        //                   _WorkingName, 
        //                   _WorkingHour, 
        //                   _Overtime, 
        //                   _SalaryType, 
        //                   _ScaleOfLNS,
        //                   _LNSCoefficient,
        //                   _CreateDate,
        //                   _LNSWages,
        //                   _LNSUnit,
        //                   _PCTNLNS,
        //                   _ScaleOfLCB,
        //                   _LCBCoefficient,
        //                   _FromDateLCB,
        //                   _ToDateLCB,
        //                   _LCBWages,
        //                   _LCBUnit,
        //                   _PCTNLCB,
        //                   _PCDH,
        //                   _PCKV,
        //                   _PCKVLevel,
        //                   _PCCV,
        //                   _PCCVLevel,
        //                   _SalaryLevel,
        //                   _Staff_BHXH,
        //                   _Company_BHXH,
        //                   _Staff_BHYT,
        //                   _Company_BHYT,
        //                   _Staff_BHTN,
        //                   _Company_BHTN,
        //                   _ContractTitle);
        //    }
        //    else
        //    {
        //        return objDAL.Update_V1(_EmployeeContractId,
        //                   _UserId,
        //                   _ContractTypeId,
        //                   _PositionId,
        //                   _Wages,
        //                   _Unit,
        //                   _FromDate,
        //                   _ToDate,
        //                   _Description,
        //                   _Active,
        //                   //_ContractNo,
        //                   _ContractName,
        //                   _RepresentativeOfCompany,
        //                   _CompanyName,
        //                   _AttachFileName,
        //                   _WorkingName,
        //                   _WorkingHour,
        //                   _Overtime,
        //                   _SalaryType,
        //                   _ScaleOfLNS,
        //                   _LNSCoefficient,
        //                   _CreateDate,
        //                   _LNSWages,
        //                   _LNSUnit,
        //                   _PCTNLNS,
        //                   _ScaleOfLCB,
        //                   _LCBCoefficient,
        //                   _FromDateLCB,
        //                   _ToDateLCB,
        //                   _LCBWages,
        //                   _LCBUnit,
        //                   _PCTNLCB,
        //                   _PCDH,
        //                   _PCKV,
        //                   _PCKVLevel,
        //                   _PCCV,
        //                   _PCCVLevel,
        //                   _SalaryLevel,
        //                   _Staff_BHXH,
        //                   _Company_BHXH,
        //                   _Staff_BHYT,
        //                   _Company_BHYT,
        //                   _Staff_BHTN,
        //                   _Company_BHTN,
        //                   _ContractTitle);
        //    }
        //}
        //public static long Update_V1(int employeeContractId, int UserId, int ContractTypeId, int PositionId, double Wages, int Unit, DateTime FromDate, DateTime ToDate, string Description, bool Active,
        //                   /*string ContractNo,*/ string ContractName, string RepresentativeOfCompany, string CompanyName, string AttachFileName, string WorkingName, int WorkingHour, int Overtime, int SalaryType,
        //                    string ScaleOfLNS, string LNSCoefficient, DateTime CreateDate, double LNSWages, int LNSUnit, double LNS_PCTN,
        //                    string ScaleOfLCB, string LCBCoefficient, DateTime LCBFromDate, DateTime LCBToDate, double LCBWages, int LCBUnit, double LCB_PCTN, double PCDH, double PCKV, double PCKVLevel, double PCCV, double PCCVLevel, double SalaryLevel,
        //                    double Staff_BHXH, double Company_BHXH, double Staff_BHYT, double Company_BHYT, double Staff_BHTN, double Company_BHTN, string ContractTitle)
        //{
        //    EmployeeContractDAL objDAL = new EmployeeContractDAL();
        //    return objDAL.Update_V1(employeeContractId,
        //        UserId,
        //        ContractTypeId,
        //        PositionId,
        //        Wages,
        //        Unit,
        //        FromDate,
        //        ToDate,
        //        Description,
        //        Active,
        //        //ContractNo,
        //        ContractName,
        //        RepresentativeOfCompany,
        //        CompanyName,
        //        AttachFileName,
        //        WorkingName,
        //        WorkingHour,
        //        Overtime,
        //        SalaryType,
        //        ScaleOfLNS,
        //        LNSCoefficient,
        //        CreateDate,
        //        LNSWages,
        //        LNSUnit,
        //        LNS_PCTN,
        //        ScaleOfLCB,
        //        LCBCoefficient,
        //        LCBFromDate,
        //        LCBToDate,
        //        LCBWages,
        //        LCBUnit,
        //        LCB_PCTN,
        //        PCDH,
        //        PCKV,
        //        PCKVLevel,
        //        PCCV,
        //        PCCVLevel,
        //        SalaryLevel,
        //        Staff_BHXH,
        //        Company_BHXH,
        //        Staff_BHYT,
        //        Company_BHYT,
        //        Staff_BHTN,
        //        Company_BHTN,
        //        ContractTitle);
        //}
        ////////////////////////////////////////////////////////////////////
        //public long Save()
        //{
        //    EmployeeContractDAL objDAL = new EmployeeContractDAL();
        //    if (_EmployeeContractId <= 0)
        //    {
        //        return objDAL.Insert(_UserId, _ContractTypeId, _PositionId, _Wages, _Unit, _FromDate, _ToDate, _Description, _Active, _ContractNo, _ContractName, _RepresentativeOfCompany, _CompanyName, _AttachFileName, _WorkingName, _WorkingHour, _Overtime, _SalaryType);
        //    }
        //    else
        //    {
        //        return objDAL.Update(_EmployeeContractId, _UserId, _ContractTypeId, _PositionId, _Wages, _Unit, _FromDate, _ToDate, _Description, _Active, _ContractNo, _ContractName, _RepresentativeOfCompany, _CompanyName, _AttachFileName, _WorkingName, _WorkingHour, _Overtime, _SalaryType);
        //    }
        //}
        //public long SaveDecisions()
        //{
        //    EmployeeContractDAL objDAL = new EmployeeContractDAL();
        //    if (_EmployeeContractId <= 0)
        //    {
        //        return objDAL.InsertDecisions(_PreviousEmployeeContractId, _ContractTypeId, _DecisionId, _UserId, _PositionId, _FromDate, _ToDate, _SalaryType);
        //    }
        //    else
        //    {
        //        return objDAL.UpdateDecisions(_PreviousEmployeeContractId, _ContractTypeId, _DecisionId, _UserId, _PositionId, _FromDate, _ToDate, _SalaryType, _EmployeeContractId);
        //    }
        //}
        //public static long Update(int employeeContractId, int userId, int contractTypeId, int positionId, double wages, int unit, DateTime fromDate, DateTime toDate, string description, bool active)
        //{
        //    EmployeeContractDAL objEmployeeContractDAL = new EmployeeContractDAL();
        //    if (description == null)
        //    {
        //        description = string.Empty;
        //    }

        //    ContractTypesBLL obj = ContractTypesBLL.GetById(contractTypeId);
        //    if (!toDate.Equals(FormatDate.GetSQLDateMinValue))
        //    {
        //        if (obj != null)
        //        {
        //            toDate = fromDate.AddMonths(int.Parse(obj.ContractTypeValue.ToString()));
        //            toDate = toDate.AddDays(-1);
        //        }
        //    }
        //    return objEmployeeContractDAL.Update(employeeContractId, userId, contractTypeId, positionId, wages, unit, fromDate, toDate, description, active);            
        //}
        public static long Delete(int employeeContractId)
        {
            return new EmployeeContractDAL().Delete(employeeContractId);
        }

        public static long DeleteByIds(string employeeContractIds)
        {
            return new EmployeeContractDAL().DeleteByIds(employeeContractIds);
        }

        public static long UpdatePrintType(int employeeContractIds, bool printType)
        {
            //_SP = "Upd_H0_EmployeeContract_PrintType";
            //_SPValue = $"'{employeeContractIds}','{printType}'";
            return new EmployeeContractDAL().UpdatePrintType(employeeContractIds, printType);
        }

        public long Save()
        {
            var objDAL = new EmployeeContractDAL();
            if (EmployeeContractId <= 0)
            {
                _SP = "Ins_H0_EmployeeContract_By_UserId";
                _SPValue =
                    $"UserId: {UserId}, PositionId: {PositionId}, ContractTypeId: {ContractTypeId}, FromDate: '{FromDate}', ToDate: '{ToDate}', ContractName: N'{ContractName}', RepresentativeOfCompany: N'{RepresentativeOfCompany}', CompanyName: N'{CompanyName}', WorkingHour: {WorkingHour}, Overtime: {Overtime}, CreateDate: '{CreateDate}', SalaryLevel: {SalaryLevel}, ScaleOfSalaryId: {ScaleOfSalaryId}, ContractTitle: N'{ContractTitle}', Office: '{Office}', IsReplaced: {IsReplaced}, PreviousEmployeeContractId: {PreviousEmployeeContractId}, CntRemark: N'{CntRemark}'";
                return objDAL.Insert_V1(UserId, PositionId, ContractTypeId, FromDate, ToDate, ContractName,
                    RepresentativeOfCompany, CompanyName, WorkingHour, Overtime, CreateDate, SalaryLevel,
                    ScaleOfSalaryId, ContractTitle, Office, IsReplaced, PreviousEmployeeContractId, CntRemark);
            }
            _SP = "Upd_H0_EmployeeContract_ByUserId";
            _SPValue =
                $"EmployeeContractId: {EmployeeContractId}, UserId: {UserId}, PositionId: {PositionId}, ContractTypeId: {ContractTypeId}, FromDate: '{FromDate}', ToDate: '{ToDate}', ContractName: N'{ContractName}', RepresentativeOfCompany: N'{RepresentativeOfCompany}', CompanyName: N'{CompanyName}', WorkingHour: {WorkingHour}, Overtime: {Overtime}, SalaryLevel: {SalaryLevel}, ScaleOfSalaryId: {ScaleOfSalaryId}, ContractTitle: N'{ContractTitle}', Office: '{Office}', IsReplaced: {IsReplaced}, PreviousEmployeeContractId: {PreviousEmployeeContractId}, CntRemark: N'{CntRemark}'";
            return objDAL.Update_V1(EmployeeContractId, UserId, PositionId, ContractTypeId, FromDate, ToDate,
                ContractName, RepresentativeOfCompany, CompanyName, WorkingHour, Overtime, SalaryLevel, ScaleOfSalaryId,
                ContractTitle, Office, IsReplaced, PreviousEmployeeContractId, CntRemark);
        }

        public string ReturnSP()
        {
            return _SP;
        }

        public string ReturnSPValue()
        {
            return _SPValue;
        }

        #endregion

        #region public static methods Get

        /// <summary>
        ///     Author: Giang
        ///     Date: 6-Nov-14
        ///     Content: Get Active User Contract to DT
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllToDT()
        {
            return new EmployeeContractDAL().GetAllToDT();
        }

        public static DataTable GetByAllByUserId(int userid)
        {
            return new EmployeeContractDAL().GetByAllByUserId(userid);
        }

        public static DataTable GetAllToDT1()
        {
            return new EmployeeContractDAL().GetAllToDT1();
        }

        public static DataTable GetAll()
        {
            return new EmployeeContractDAL().GetAll();
        }

        //----------------------------------------
        public static DataTable GetByDeptIdForDatatable(string deptId)
        {
            return new EmployeeContractDAL().GetByDeptId(deptId);
        }

        public static List<EmployeeContractBLL> GetByFilter(string fullName, int departmentId, int contractType,
            string sortParameter, int typeSort)
        {
            var list =
                GenerateListEmployeeContractBLLFromDataTable(new EmployeeContractDAL().GetByFilter(fullName,
                    departmentId, contractType, typeSort));

            if (!string.IsNullOrEmpty(sortParameter))
                list.Sort(new EmployeeContractBLLComparer(sortParameter));

            return list;
        }

        public static List<EmployeeContractBLL> GetByUserId(int userId, int decisionId)
        {
            return
                GenerateListEmployeeContractBLLFromDataTable(new EmployeeContractDAL().GetByUserId(userId, decisionId));
        }

        public static DataTable GetByUserIdToDT(int userId, int decisionId)
        {
            return new EmployeeContractDAL().GetByUserId(userId, decisionId);
        }

        public static DataTable GetByUserIdsToDT(string userId)
        {
            return new EmployeeContractDAL().GetByUserIds(userId);
        }

        public static List<EmployeeContractBLL> GetByUserDateActive(int userId, DateTime date, bool active)
        {
            return
                GenerateListEmployeeContractBLLFromDataTable(new EmployeeContractDAL().GetByUserDateActive(userId, date,
                    active));
        }

        public static EmployeeContractBLL GetActiveContractByUserId(int userId)
        {
            var dt = new EmployeeContractDAL().GetActiveContractByUserId(userId);

            if (dt.Rows.Count == 1)
                return GenerateEmployeeContractBLLFromDataRow(dt.Rows[0]);
            return null;
        }

        public static DataRow DR_GetActiveContractByUserId(int userId)
        {
            var dt = new EmployeeContractDAL().GetActiveContractByUserId(userId);

            if (dt.Rows.Count == 1)
                return dt.Rows[0];
            return null;
        }

        public static DataTable DT_GetActiveContractByUserId(int userId)
        {
            return new EmployeeContractDAL().GetActiveContractByUserId(userId);
        }

        /// <summary>
        ///     Author: Giang
        ///     Date: 12/18/2014
        ///     Content: Get active contract to data row
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DataRow GetActiveContractByUserIdToDT(int userId)
        {
            var dt = new EmployeeContractDAL().GetActiveContractByUserId(userId);

            if (dt.Rows.Count == 1)
                return dt.Rows[0];
            return null;
        }

        public static DataRow GetContractById(int employeeContractId)
        {
            var dt = new EmployeeContractDAL().GetContractById(employeeContractId);

            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        public static List<EmployeeContractBLL> RemindExpiredConstracts(string fullName, string deptId,
            DateTime expireDate, int typeSort)
        {
            return
                GenerateListEmployeeContractBLLFromDataTable(new EmployeeContractDAL().RemindExpiredConstracts(
                    fullName, deptId, expireDate, typeSort));
        }

        public static DataTable RemindExpiredConstractsToDT(string fullName, string deptId, DateTime expireDate,
            int typeSort)
        {
            return new EmployeeContractDAL().RemindExpiredConstracts(fullName, deptId, expireDate, typeSort);
        }

        public static List<EmployeeContractBLL> ChangedConstracts(string fullName, int deptId, int month, int year,
            int typeSort)
        {
            return
                GenerateListEmployeeContractBLLFromDataTable(new EmployeeContractDAL().ChangedConstracts(fullName,
                    deptId, month, year, typeSort));
        }

        public static DataTable ChangedConstractsToDT(string fullName, int deptId, int month, int year, int typeSort)
        {
            return new EmployeeContractDAL().ChangedConstracts(fullName, deptId, month, year, typeSort);
        }

        public static List<EmployeeContractBLL> GetByUserIdFromToDate(int userId, DateTime fromDate, DateTime toDate)
        {
            return
                GenerateListEmployeeContractBLLFromDataTable(new EmployeeContractDAL().GetByUserFromToDate(userId,
                    fromDate, toDate));
        }

        public static EmployeeContractBLL GetById(int employeeContractId)
        {
            var list =
                GenerateListEmployeeContractBLLFromDataTable(new EmployeeContractDAL().GetById(employeeContractId));
            return list.Count == 1 ? list[0] : null;
        }

        public static List<EmployeeContractBLL> GetByDecisionId(int decisionId)
        {
            return
                GenerateListEmployeeContractBLLCoefficientFromDataTable(
                    new EmployeeContractDAL().GetByDecisionId(decisionId));
        }

        public static DataRow GetContractByUserIdId(int userId)
        {
            var dt = new EmployeeContractDAL().GetActiveContractByUserId(userId);

            if (dt.Rows.Count == 1)
                return dt.Rows[0];
            return null;
        }

        #endregion

        #region private methods

        private static List<EmployeeContractBLL> GenerateListEmployeeContractBLLFromDataTable(DataTable dt)
        {
            var list = new List<EmployeeContractBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateEmployeeContractBLLFromDataRow(dr));

            return list;
        }

        private static EmployeeContractBLL GenerateEmployeeContractBLLFromDataRow(DataRow dr)
        {
            var objBLL = new EmployeeContractBLL(
                dr[EmployeeContractKeys.Field_EmployeeContract_EmployeeContractId] == DBNull.Value
                    ? DefaultValues.UserIdMinValue
                    : (int) dr[EmployeeContractKeys.Field_EmployeeContract_EmployeeContractId],
                dr[EmployeeKeys.FIELD_EMPLOYEES_USERID] == DBNull.Value
                    ? DefaultValues.UserIdMinValue
                    : (int) dr[EmployeeKeys.FIELD_EMPLOYEES_USERID],
                dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString(),
                dr[ContractTypeKeys.Field_ContractTypes_ContractTypeId] == DBNull.Value
                    ? DefaultValues.ContractTypeIdMinValue
                    : (int) dr[ContractTypeKeys.Field_ContractTypes_ContractTypeId],
                dr[ContractTypeKeys.Field_ContractTypes_ContractTypeCode] == DBNull.Value
                    ? string.Empty
                    : dr[ContractTypeKeys.Field_ContractTypes_ContractTypeCode].ToString(),
                dr[ContractTypeKeys.Field_ContractTypes_ContractTypeName] == DBNull.Value
                    ? string.Empty
                    : dr[ContractTypeKeys.Field_ContractTypes_ContractTypeName].ToString(),
                string.Empty,
                dr[PositionKeys.FIELD_POSITION_ID] == DBNull.Value
                    ? DefaultValues.PositionIdMinValue
                    : (int) dr[PositionKeys.FIELD_POSITION_ID],
                dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[PositionKeys.FIELD_POSITION_NAME].ToString(),
                dr[EmployeeContractKeys.Field_EmployeeContract_FromDate] == DBNull.Value
                    ? DateTime.MinValue
                    : Convert.ToDateTime(dr[EmployeeContractKeys.Field_EmployeeContract_FromDate].ToString()),
                dr[EmployeeContractKeys.Field_EmployeeContract_ToDate] == DBNull.Value
                    ? DateTime.MinValue
                    : Convert.ToDateTime(dr[EmployeeContractKeys.Field_EmployeeContract_ToDate].ToString()),
                dr[EmployeeContractKeys.Field_EmployeeContract_Description] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeContractKeys.Field_EmployeeContract_Description].ToString(),
                dr[EmployeeContractKeys.Field_EmployeeContract_Active] == DBNull.Value
                    ? false
                    : Convert.ToBoolean(dr[EmployeeContractKeys.Field_EmployeeContract_Active].ToString())
            );

            objBLL.Wages = dr[EmployeeContractKeys.Field_EmployeeContract_Wages] == DBNull.Value
                ? 0
                : (double) dr[EmployeeContractKeys.Field_EmployeeContract_Wages];
            objBLL.Unit = dr[EmployeeContractKeys.Field_EmployeeContract_Unit] == DBNull.Value
                ? 0
                : (int) dr[EmployeeContractKeys.Field_EmployeeContract_Unit];
            if (objBLL.Unit == Constants.EMPLOYEE_CONTRACT_UNIT_dNGAY)
                objBLL.UnitName = Constants.EMPLOYEE_CONTRACT_UNIT_dNGAY_TEXT;
            else if (objBLL.Unit == Constants.EMPLOYEE_CONTRACT_UNIT_PERCENT)
                objBLL.UnitName = Constants.EMPLOYEE_CONTRACT_UNIT_PERCENT_TEXT;
            else
                objBLL.UnitName = "None";

            try
            {
                objBLL.LevelPosition = dr[PositionKeys.Field_Position_LevelPosition] == DBNull.Value
                    ? 0
                    : (int) dr[PositionKeys.Field_Position_LevelPosition];
            }
            catch
            {
            }

            objBLL.ContractNo = dr[EmployeeContractKeys.Field_EmployeeContract_ContractNo] == DBNull.Value
                ? string.Empty
                : dr[EmployeeContractKeys.Field_EmployeeContract_ContractNo].ToString();
            objBLL.ContractName = dr[EmployeeContractKeys.Field_EmployeeContract_ContractName] == DBNull.Value
                ? string.Empty
                : dr[EmployeeContractKeys.Field_EmployeeContract_ContractName].ToString();
            objBLL.RepresentativeOfCompany = dr[EmployeeContractKeys.Field_EmployeeContract_RepresentativeOfCompany] ==
                                             DBNull.Value
                ? string.Empty
                : dr[EmployeeContractKeys.Field_EmployeeContract_RepresentativeOfCompany].ToString();
            objBLL.CompanyName = dr[EmployeeContractKeys.Field_EmployeeContract_CompanyName] == DBNull.Value
                ? string.Empty
                : dr[EmployeeContractKeys.Field_EmployeeContract_CompanyName].ToString();
            objBLL.AttachFileName = dr[EmployeeContractKeys.Field_EmployeeContract_AttachFileName] == DBNull.Value
                ? string.Empty
                : dr[EmployeeContractKeys.Field_EmployeeContract_AttachFileName].ToString();
            objBLL.WorkingName = dr[EmployeeContractKeys.Field_EmployeeContract_WorkingName] == DBNull.Value
                ? string.Empty
                : dr[EmployeeContractKeys.Field_EmployeeContract_WorkingName].ToString();
            objBLL.WorkingHour = dr[EmployeeContractKeys.Field_EmployeeContract_WorkingHour] == DBNull.Value
                ? 0
                : int.Parse(dr[EmployeeContractKeys.Field_EmployeeContract_WorkingHour].ToString());
            objBLL.Overtime = dr[EmployeeContractKeys.Field_EmployeeContract_Overtime] == DBNull.Value
                ? 0
                : int.Parse(dr[EmployeeContractKeys.Field_EmployeeContract_Overtime].ToString());
            objBLL.SalaryType = dr[EmployeeContractKeys.Field_EmployeeContract_SalaryType] == DBNull.Value
                ? 0
                : int.Parse(dr[EmployeeContractKeys.Field_EmployeeContract_SalaryType].ToString());
            objBLL.RootName = dr[EmployeeContractKeys.Field_EmployeeContract_RootName] == DBNull.Value
                ? string.Empty
                : dr[EmployeeContractKeys.Field_EmployeeContract_RootName].ToString();
            objBLL.DepartmentName = dr[EmployeeContractKeys.Field_EmployeeContract_DepartmentName] == DBNull.Value
                ? string.Empty
                : dr[EmployeeContractKeys.Field_EmployeeContract_DepartmentName].ToString();
            objBLL.DepartmentFullName = dr[EmployeeContractKeys.Field_EmployeeContract_DepartmentFullName] ==
                                        DBNull.Value
                ? string.Empty
                : dr[EmployeeContractKeys.Field_EmployeeContract_DepartmentFullName].ToString();
            try
            {
                objBLL.Birthday = dr[EmployeeContractKeys.Field_EmployeeContract_Birthday] == DBNull.Value
                    ? DateTime.MinValue
                    : Convert.ToDateTime(dr[EmployeeContractKeys.Field_EmployeeContract_Birthday].ToString());
                objBLL.BirthPlace = dr[EmployeeContractKeys.Field_EmployeeContract_Birthplace] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeContractKeys.Field_EmployeeContract_Birthplace].ToString();
                objBLL.ID = dr[EmployeeContractKeys.Field_EmployeeContract_ID] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeContractKeys.Field_EmployeeContract_ID].ToString();
                objBLL.DateOfIssue = dr[EmployeeContractKeys.Field_EmployeeContract_DateOfIssue] == DBNull.Value
                    ? DateTime.MinValue
                    : Convert.ToDateTime(dr[EmployeeContractKeys.Field_EmployeeContract_DateOfIssue].ToString());
                objBLL.PlaceOfIssue = dr[EmployeeContractKeys.Field_EmployeeContract_PlaceOfIssue] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeContractKeys.Field_EmployeeContract_PlaceOfIssue].ToString();
                objBLL.Resident = dr[EmployeeContractKeys.Field_EmployeeContract_Resident] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeContractKeys.Field_EmployeeContract_Resident].ToString();
            }
            catch
            {
            }

            return objBLL;
        }


        private static List<EmployeeContractBLL> GenerateListEmployeeContractBLLCoefficientFromDataTable(DataTable dt)
        {
            var list = new List<EmployeeContractBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateEmployeeContractBLLCoefficientFromDataRow(dr));

            return list;
        }

        private static EmployeeContractBLL GenerateEmployeeContractBLLCoefficientFromDataRow(DataRow dr)
        {
            var objBLL = new EmployeeContractBLL();


            objBLL.PreviousEmployeeContractId =
                dr[EmployeeContractKeys.Field_EmployeeContract_PreviousEmployeeContractId] == DBNull.Value
                    ? 0
                    : int.Parse(dr[EmployeeContractKeys.Field_EmployeeContract_PreviousEmployeeContractId].ToString());
            objBLL.EmployeeContractId = dr[EmployeeContractKeys.Field_EmployeeContract_EmployeeContractId] ==
                                        DBNull.Value
                ? 0
                : int.Parse(dr[EmployeeContractKeys.Field_EmployeeContract_EmployeeContractId].ToString());
            objBLL.UserId = dr[EmployeeKeys.FIELD_EMPLOYEES_USERID] == DBNull.Value
                ? 0
                : int.Parse(dr[EmployeeKeys.FIELD_EMPLOYEES_USERID].ToString());
            objBLL.FullName = dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                ? string.Empty
                : dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();
            objBLL.PositionId = dr[PositionKeys.FIELD_POSITION_ID] == DBNull.Value
                ? 0
                : (int) dr[PositionKeys.FIELD_POSITION_ID];
            objBLL.PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                ? string.Empty
                : dr[PositionKeys.FIELD_POSITION_NAME].ToString();
            objBLL.FromDate = dr[EmployeeContractKeys.Field_EmployeeContract_FromDate] == DBNull.Value
                ? DateTime.MinValue
                : Convert.ToDateTime(dr[EmployeeContractKeys.Field_EmployeeContract_FromDate].ToString());
            objBLL.ToDate = dr[EmployeeContractKeys.Field_EmployeeContract_ToDate] == DBNull.Value
                ? DateTime.MinValue
                : Convert.ToDateTime(dr[EmployeeContractKeys.Field_EmployeeContract_ToDate].ToString());
            objBLL.SalaryType = dr[EmployeeContractKeys.Field_EmployeeContract_SalaryType] == DBNull.Value
                ? 0
                : int.Parse(dr[EmployeeContractKeys.Field_EmployeeContract_SalaryType].ToString());

            ////// phan he so LCB
            objBLL.LCB_CoefficientEmployeeId = dr["LCB_CoefficientEmployeeId"] == DBNull.Value
                ? 0
                : int.Parse(dr["LCB_CoefficientEmployeeId"].ToString());
            objBLL.CoefficientNameIdLCB = dr["CoefficientNameIdLCB"] == DBNull.Value
                ? 0
                : int.Parse(dr["CoefficientNameIdLCB"].ToString());
            objBLL.CoefficientNameLCB = dr["CoefficientNameLCB"] == DBNull.Value
                ? ""
                : dr["CoefficientNameLCB"].ToString();
            objBLL.CoefficientLevelIdLCB = dr["CoefficientLevelIdLCB"] == DBNull.Value
                ? 0
                : int.Parse(dr["CoefficientLevelIdLCB"].ToString());
            objBLL.CoefficientLevelNameLCB = dr["CoefficientLevelNameLCB"] == DBNull.Value
                ? ""
                : dr["CoefficientLevelNameLCB"].ToString();
            objBLL.CoefficientValueIdLCB = dr["CoefficientValueIdLCB"] == DBNull.Value
                ? 0
                : int.Parse(dr["CoefficientValueIdLCB"].ToString());
            objBLL.CoefficientValueLCB = dr["CoefficientValueIdLCB"] == DBNull.Value
                ? 0
                : double.Parse(dr["CoefficientValueIdLCB"].ToString());
            objBLL.LCBWages = dr["LCBWages"] == DBNull.Value ? 0 : double.Parse(dr["LCBWages"].ToString());
            objBLL.LCBUnit = dr["LCBUnit"] == DBNull.Value ? 0 : int.Parse(dr["LCBUnit"].ToString());
            objBLL.LCBUnitName = Constants.GetUnitNameById(objBLL.LCBUnit);
            objBLL.ConditionsLCB = dr["ConditionsLCB"] == DBNull.Value ? 0 : int.Parse(dr["ConditionsLCB"].ToString());
            objBLL.FromDateLCB = dr["FromDateLCB"] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr["FromDateLCB"].ToString());
            objBLL.ToDateLCB = dr["ToDateLCB"] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr["ToDateLCB"].ToString());
            objBLL.PCDH = dr["PCDH"] == DBNull.Value ? 0 : double.Parse(dr["PCDH"].ToString());
            objBLL.PCTN = dr["PCTN"] == DBNull.Value ? 0 : double.Parse(dr["PCTN"].ToString());
            objBLL.PCCV = dr["PCCV"] == DBNull.Value ? 0 : double.Parse(dr["PCCV"].ToString());
            objBLL.PCKV = dr["PCKV"] == DBNull.Value ? 0 : double.Parse(dr["PCKV"].ToString());

            ////// phan he so LNS
            objBLL.LNS_CoefficientEmployeeId = dr["LNS_CoefficientEmployeeId"] == DBNull.Value
                ? 0
                : int.Parse(dr["LNS_CoefficientEmployeeId"].ToString());
            objBLL.CoefficientNameIdLNS = dr["CoefficientNameIdLNS"] == DBNull.Value
                ? 0
                : int.Parse(dr["CoefficientNameIdLNS"].ToString());
            objBLL.CoefficientNameLNS = dr["CoefficientNameLNS"] == DBNull.Value
                ? ""
                : dr["CoefficientNameLNS"].ToString();
            objBLL.CoefficientLevelIdLNS = dr["CoefficientLevelIdLNS"] == DBNull.Value
                ? 0
                : int.Parse(dr["CoefficientLevelIdLNS"].ToString());
            objBLL.CoefficientLevelNameLNS = dr["CoefficientLevelNameLNS"] == DBNull.Value
                ? ""
                : dr["CoefficientLevelNameLNS"].ToString();
            objBLL.CoefficientValueIdLNS = dr["CoefficientValueIdLNS"] == DBNull.Value
                ? 0
                : int.Parse(dr["CoefficientValueIdLNS"].ToString());
            objBLL.CoefficientValueLNS = dr["CoefficientValueLNS"] == DBNull.Value
                ? 0
                : double.Parse(dr["CoefficientValueLNS"].ToString());
            objBLL.LNSWages = dr["LNSWages"] == DBNull.Value ? 0 : double.Parse(dr["LNSWages"].ToString());
            objBLL.LNSUnit = dr["LNSUnit"] == DBNull.Value ? 0 : int.Parse(dr["LNSUnit"].ToString());
            objBLL.LNSUnitName = Constants.GetUnitNameById(objBLL.LCBUnit);
            objBLL.PCTNLNS = dr["PCTNLNS"] == DBNull.Value ? 0 : double.Parse(dr["PCTNLNS"].ToString());

            return objBLL;
        }

        #endregion
    }
}