using System;
using System.Text;

namespace HRMDAL.H0.KeyNames
{
    sealed public class EmployeeKeys
    {

        #region Some field names of Business table
        /// <summary>
        /// Some field names of Business table
        /// </summary>
        /// 

        public const string FIELD_EMPLOYEES_USERID = "UserId";
        public const string FIELD_EMPLOYEES_USERNAME = "UserName";
        public const string FIELD_EMPLOYEES_EMPLOYEE_CODE = "EmployeeCode";
        public const string FIELD_EMPLOYEES_PASSWORD = "Password";
        public const string FIELD_EMPLOYEES_JOINDATE = "JoinDate";
        public const string FIELD_EMPLOYEES_WORKING_PHONE = "WorkingPhone";
        public const string FIELD_EMPLOYEES_HOME_PHONE = "HomePhone";
        public const string FIELD_EMPLOYEES_HAND_PHONE = "HandPhone";
        public const string FIELD_EMPLOYEES_MARRIAGE = "Marriage";
        public const string FIELD_EMPLOYEES_HEALTH_INSURANCE_NO = "HealthInsuranceNo";
        public const string FIELD_EMPLOYEES_HEALTH_INSURANCE_ADDRESS = "HealthInsuranceAddress";
        public const string FIELD_EMPLOYEES_SOCIAL_INSURANCE_NO = "SocialInsuranceNo";
        public const string FIELD_EMPLOYEES_ACCOUNT_NO = "AccountNo";
        public const string FIELD_EMPLOYEES_ATM_NO = "CardNo";
        public const string FIELD_EMPLOYEES_BANK_NAME = "BankName";
        public const string FIELD_EMPLOYEES_STATUS = "Status";                  
                
        
        public const string FIELD_EMPLOYEES_FULLNAME = "FullName";
        public const string FIELD_EMPLOYEES_OTHERNAMES = "OtherNames";
        public const string FIELD_EMPLOYEES_NORMALNAMES = "NormalNames";
        public const string FIELD_EMPLOYEES_SEX = "Sex";
        public const string FIELD_EMPLOYEES_BIRTHDAY = "Birthday";
        public const string FIELD_EMPLOYEES_BIRTHPLACE = "BirthPlace";
        public const string FIELD_EMPLOYEES_NATIVEPLACE = "NativePlace";
        public const string FIELD_EMPLOYEES_RESIDENT = "Resident";
        public const string FIELD_EMPLOYEES_LIVE = "Live";
        public const string FIELD_EMPLOYEES_IDCARD = "IdCard";
        public const string FIELD_EMPLOYEES_DATE_OF_ISSUE = "DateOfIssue";
        public const string FIELD_EMPLOYEES_PLACE_OF_ISSUE = "PlaceOfIssue";
        public const string FIELD_EMPLOYEES_NATION = "Nation";
        public const string FIELD_EMPLOYEES_NATIONALITY = "Nationality";
        public const string FIELD_EMPLOYEES_RELIGION = "Religion";
        public const string FIELD_EMPLOYEES_ORIGIN = "Origin";
        public const string FIELD_EMPLOYEES_DATE_JOIN_PARTY = "DateJoinParty";
        public const string FIELD_EMPLOYEES_PLACE_JOIN_PARTY = "PlaceJoinParty";
        public const string FIELD_EMPLOYEES_DATE_JOIN_CYU = "DateJoinCYU";
        public const string FIELD_EMPLOYEES_PLACE_JOIN_CYU = "PlaceJoinCYU";
        public const string FIELD_EMPLOYEES_DATE_OF_ENLISTED = "DateOfEnlisted";
        public const string FIELD_EMPLOYEES_DATE_OF_DEMOBILIZED = "DateOfDemobilized";
        public const string FIELD_EMPLOYEES_ARMYRANK = "ArmyRank";
        public const string FIELD_EMPLOYEES_WORKED_COMPANY = "WorkedCompany";
        

        #endregion

        #region StoreProcedure name  of Business object.

        /// <summary>
        /// StoreProcedure name  of Business object.
        /// </summary>
        /// 
        public const string SP_EMPLOYEES_DEPT_POSITION_GET_BY_DEPT_ID = "Sel_H0_EmployeeDeptPositionByDeptId";
        public const string SP_EMPLOYEES_DEPT_POSITION_GET_BY_FILTER = "Sel_H0_EmployeeDeptPositionByFilter";



        public const string SP_EMPLOYEES_GETALL = "Sel_H0_Employee_By_All";
        public const string SP_EMPLOYEES_GETONE = "Sel_H0_Employee_By_Id";
        public const string SP_EMPLOYEES_GET_BY_ROOT_ID = "Sel_H0_Employee_By_RootId";
        public const string SP_EMPLOYEES_GET_BY_DEPT_ID = "Sel_H0_Employee_By_DeptId";
        public const string SP_EMPLOYEES_GET_BY_DEPT_IDS = "Sel_H0_Employee_By_DeptIds";
        public const string SP_EMPLOYEES_GET_BY_FILTER = "Sel_H0_Employee_By_Filter";
        public const string SP_EMPLOYEES_GET_BY_IDs = "Sel_H0_Employee_By_Ids";

        public const string SP_EMPLOYEES_GET_BY_USERCODE_IS_NULL = "Sel_H0_Employee_By_UserCode_Is_Null";


        public const string SP_EMPLOYEES_INSERT = "Ins_H0_Employee";
        public const string SP_EMPLOYEES_INSERT_BY_IMPORTING = "Ins_H0_EmployeeByImporting";
        public const string SP_EMPLOYEES_UPDATE_INFOR_GENERAL = "Upd_H0_Employee_InforGeneral";
        public const string SP_EMPLOYEES_UPDATE_INFOR_DETAIL = "Upd_H0_Employee_InforDetail";
        public const string SP_EMPLOYEES_UPDATE_EMPLOYEE_CODE = "Upd_H0_Employee_EmployeeCode";
        public const string SP_EMPLOYEES_DELETE = "Del_H0_Employee";        
        public const string SP_EMPLOYEES_UPDATE_CHANGE_PASSWORD = "ChangePassword";        
        public const string SP_EMPLOYEES_LOGIN = "Login";
        public const string SP_EMPLOYEES_CHECK_NEW_EMPLOYEE = "Chk_H_UserCode";


        #endregion

        #region constant values

        public static string CONSTANT_NAME_MA_NHAN_VIEN = "Mã nhân viên";
        public static string CONSTANT_NAME_HO_VA_TEN = "Họ và tên";

        #endregion

    }
}
