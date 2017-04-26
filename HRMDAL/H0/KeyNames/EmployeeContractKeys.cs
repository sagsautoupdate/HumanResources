using System;
using System.Text;

namespace HRMDAL.H0.KeyNames
{
    public sealed class EmployeeContractKeys
    {
        /// <summary>
        /// Some field names of EmployeeQualificationContract table
        /// </summary>
        /// 
        public const string FIELD_EMPLOYEE_CONTRACT_ID = "EmployeeContractId";
        public const string FIELD_EMPLOYEE_CONTRACT_WAGES = "Wages";
        public const string FIELD_EMPLOYEE_CONTRACT_UNIT = "Unit";
        public const string FIELD_EMPLOYEE_CONTRACT_FROMDATE = "FromDate";
        public const string FIELD_EMPLOYEE_CONTRACT_TODATE = "ToDate";
        public const string FIELD_EMPLOYEE_CONTRACT_DESCRIPTION = "Description";
        public const string FIELD_EMPLOYEE_CONTRACT_ACTIVE = "Active";

        /// <summary>
        /// StoreProcedure name of EmployeeQualificationContract object.
        /// </summary>
        public const string SP_EMPLOYEE_CONTRACT_INSERT = "Ins_H0_EmployeeContract";
        public const string SP_EMPLOYEE_CONTRACT_UPDATE = "Upd_H0_EmployeeContract";
        public const string SP_EMPLOYEE_CONTRACT_DELETE = "Del_H0_EmployeeContract";

        public const string SP_EMPLOYEE_CONTRACT_GET_BY_FILTER = "Sel_H0_EmployeeContractByFilter";
        public const string SP_EMPLOYEE_CONTRACT_GET_BY_USERID = "Sel_H0_EmployeeContractByUserId";
        public const string SP_EMPLOYEE_CONTRACT_GET_BY_USERID_POSITIONID_CONTRACTTYPEID = "Sel_H0_EmployeeContractByUserIdPositionIdContractTypeId";
        public const string SP_EMPLOYEE_CONTRACT_GET_ACTVIE_CONTRACT_BY_USERID = "Sel_H0_EmployeeContract_Active_By_UserId";
        
    }
}
