using System;
using System.Collections.Generic;
using System.Data;
using HRMBLL.BLLHelper;
using HRMBLL.H;
using HRMDAL.H0;
using HRMUtil;
using HRMUtil.KeyNames.H0;

namespace HRMBLL.H0
{
    public class EmployeesBLL
    {
        #region private fields

        #endregion

        #region new private fields

        #endregion

        #region Properties Gerneral

        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string EmployeeCode { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public DateTime JoinDate { get; set; } = FormatDate.GetSQLDateMinValue;

        public DateTime JoinCompanyDate { get; set; } = FormatDate.GetSQLDateMinValue;

        public bool Marriage { get; set; }

        public string WorkingPhone { get; set; } = string.Empty;

        public string HandPhone { get; set; } = string.Empty;

        public string HomePhone { get; set; } = string.Empty;

        public string AccountNo { get; set; } = string.Empty;

        public string ATMNo { get; set; } = string.Empty;

        public string BankName { get; set; } = string.Empty;

        public string HealthInsuranceNo { get; set; } = string.Empty;

        public string HealthInsuranceAddress { get; set; } = string.Empty;

        public string SocialInsuranceNo { get; set; } = string.Empty;


        public int Status { get; set; }

        public DateTime LeaveDate { get; set; } = FormatDate.GetSQLDateMinValue;

        public string TaxCode { get; set; } = string.Empty;

        #endregion

        #region CV

        public string FullName { get; set; } = string.Empty;

        public string FullName2 { get; set; } = string.Empty;

        public string OtherName { get; set; } = string.Empty;

        public string NormalNames { get; set; } = string.Empty;

        public int Sex { get; set; }

        public string SexName { get; set; } = "";

        public DateTime Birthday { get; set; } = FormatDate.GetSQLDateMinValue;

        public int DayOfBirth { get; set; }

        public int MonthOfBirth { get; set; }

        public int YearOfBirth { get; set; }

        public string BirthPlace { get; set; } = string.Empty;

        public string NativePlace { get; set; } = string.Empty;

        public string Resident { get; set; } = string.Empty;

        public string Live { get; set; } = string.Empty;

        public string IdCard { get; set; } = string.Empty;

        public DateTime DateOfIssue { get; set; } = FormatDate.GetSQLDateMinValue;

        public string PlaceOfIssue { get; set; } = string.Empty;

        public string Nation { get; set; } = string.Empty;

        public string Nationality { get; set; } = string.Empty;

        public string Religion { get; set; } = string.Empty;

        public string Origin { get; set; } = string.Empty;

        public DateTime DateJoinParty { get; set; } = FormatDate.GetSQLDateMinValue;

        public string PlaceJoinParty { get; set; } = string.Empty;

        public DateTime DateJoinCYU { get; set; } = FormatDate.GetSQLDateMinValue;

        public string PlaceJoinCYU { get; set; } = string.Empty;

        public DateTime DateOfEnlisted { get; set; } = FormatDate.GetSQLDateMinValue;

        public DateTime DateOfDemobilized { get; set; } = FormatDate.GetSQLDateMinValue;

        public string ArmyRank { get; set; } = string.Empty;

        public string WorkedCompany { get; set; } = string.Empty;

        #endregion

        #region other

        public int PositionId { get; set; } = 0;

        public string PositionName { get; set; } = string.Empty;

        public int LevelPosition { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentCode { get; set; }

        public string DepartmentName { get; set; } = string.Empty;

        public string DepartmentFullName { get; set; } = string.Empty;

        public int RootId { get; set; }

        public string RootName { get; set; } = string.Empty;

        public int ParentId { get; set; }

        public int DepartmentLevel { get; set; }

        #endregion

        #region new fields

        public string NativeCity { get; set; } = string.Empty;

        public string PassportNo { get; set; } = string.Empty;

        public DateTime PassportDateOfIssue { get; set; } = FormatDate.GetSQLDateMinValue;

        public string PassportPlaceOfIssue { get; set; } = string.Empty;

        public DateTime PassportDateOfExpiry { get; set; } = FormatDate.GetSQLDateMinValue;

        public string OtherPhone { get; set; } = string.Empty;

        public string WorkingEmail { get; set; } = string.Empty;

        public string PersonalEmail { get; set; } = string.Empty;

        public string OtherEmail { get; set; } = string.Empty;

        public string YahooId { get; set; } = string.Empty;

        public string SkypeId { get; set; } = string.Empty;

        public string MsnId { get; set; } = string.Empty;

        public string Guarantor { get; set; } = string.Empty;

        public string GuarantorWorkingPlace { get; set; } = string.Empty;

        public string ResidentCity { get; set; } = string.Empty;

        public string ResidentCountry { get; set; } = string.Empty;

        public string CurrentCity { get; set; } = string.Empty;

        public string CurrentCountry { get; set; } = string.Empty;

        public string UrgentContactName { get; set; } = string.Empty;

        public string UrgentContactRelation { get; set; } = string.Empty;

        public string UrgentContactCellPhone { get; set; } = string.Empty;

        public string UrgentContactHomePhone { get; set; } = string.Empty;

        public string UrgentContactEmail { get; set; } = string.Empty;

        public string UrgentContactAddress { get; set; } = string.Empty;

        public DateTime SocialInsuranceRegDate { get; set; } = FormatDate.GetSQLDateMinValue;

        public float SocialInsurancePercentage { get; set; }

        public int SocialInsuranceProvinceCode { get; set; }

        public DateTime HealthInsuranceDateOfExpiry { get; set; } = FormatDate.GetSQLDateMinValue;

        public string HealthInsuranceRegHospital { get; set; } = string.Empty;

        public string PartyPosition { get; set; } = string.Empty;

        public string CYUPosition { get; set; } = string.Empty;

        public string ArmyPosition { get; set; } = string.Empty;

        public string ArmyBranch { get; set; } = string.Empty;

        public int IsWarInvalid { get; set; }

        public DateTime RevolutionJoinDate { get; set; } = FormatDate.GetSQLDateMinValue;

        public string WarInvalidLevel { get; set; } = string.Empty;

        public int WarInvalidPercentage { get; set; }

        public int IsEntitledToTreatment { get; set; }

        public string BloodGroup { get; set; } = string.Empty;

        public string Height { get; set; } = string.Empty;

        public string Weight { get; set; } = string.Empty;

        public string HealthInformation { get; set; } = string.Empty;

        public string HealthRemarks { get; set; } = string.Empty;

        public string SicknessRemarks { get; set; } = string.Empty;

        public int IsHandicaped { get; set; }

        public string PersonalTarget { get; set; } = string.Empty;

        public string Hobbies { get; set; } = string.Empty;

        public string Strengths { get; set; } = string.Empty;

        public string Weaknesses { get; set; } = string.Empty;

        public string DemobilizationReason { get; set; } = string.Empty;

        public string ArmyUnit { get; set; } = string.Empty;

        public string PersonalStatus { get; set; } = string.Empty;

        public string GuarantorPhone { get; set; } = string.Empty;

        public string GuarantorAddress { get; set; } = string.Empty;

        #endregion

        #region Constructors

        public EmployeesBLL(int userId, string fullName)
        {
            UserId = userId;
            FullName = fullName;
        }

        public EmployeesBLL(int userId, string userName, string employeeCode, string password, string fullName,
            DateTime birthday)
        {
            UserId = userId;
            UserName = userName;
            EmployeeCode = employeeCode;
            Password = password;
            FullName = fullName;
            Birthday = birthday;
        }

        #endregion

        #region public methods Get

        public static DataRow DR_GetEmployeeByUserName(string userName)
        {
            var oneByUserName = new EmployeesDAL().GetOneByUserName(userName);
            if (oneByUserName.Rows.Count > 0)
                return oneByUserName.Rows[0];
            return null;
        }

        public static DataTable DT_GetByDeptIds(string deptIds, int rootId, string fullname, string sortParameter)
        {
            return new EmployeesDAL().GetByDeptIds(deptIds, rootId, fullname, 0);
        }

        public static DataRow DR_GetEmployeeById(int userId)
        {
            var one = new EmployeesDAL().GetOne(userId);
            if (one.Rows.Count > 0)
                return one.Rows[0];
            return null;
        }

        public static DataTable DT_GetAll(int status)
        {
            return new EmployeesDAL().GetAll(status);
        }

        /////////////////////////////////////////////////////////////////////
        public static List<EmployeesBLL> GetEmployeeDeptPositionByDeptId(int deptId)
        {
            return GenerateListEmployeesFromDataTable(new EmployeesDAL().GetEmployeeDeptPositionByDeptId(deptId));
        }

        public static List<EmployeesBLL> GetEmployeeDeptPositionByFilter(int deptId, string fullName)
        {
            return
                GenerateListEmployeesFromDataTable(new EmployeesDAL().GetEmployeeDeptPositionByFilter(deptId, fullName));
        }

        /// <summary>
        ///     Author: Giang
        ///     Date: 11/13/2014
        ///     Content: Lấy job description theo userid
        /// </summary>
        /// <returns></returns>
        public static DataTable GetEmployeeJobDescription(int userid)
        {
            return new EmployeesDAL().GetEmployeeJobDescription(userid);
        }

        public static List<EmployeesBLL> GetEmployeeLeaveJobByFilter(int deptId, string fullName, string sortParameter)
        {
            var list =
                GenerateListEmployeesFromDataTable(new EmployeesDAL().GetEmployeeLeaveJobByFilter(deptId, fullName));

            if (!string.IsNullOrEmpty(sortParameter))
                list.Sort(new EmployeesBLLComparer(sortParameter));

            return list;
        }

        public static DataTable GetEmployeeLeaveJobByFilterToDT(int deptId, string fullName)
        {
            var list = new EmployeesDAL().GetEmployeeLeaveJobByFilter(deptId, fullName);

            //if (!String.IsNullOrEmpty(sortParameter))
            //list.Sort(new EmployeesBLLComparer(sortParameter));

            return list;
        }

        ////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Author: giangtvt
        ///     Date: 23-Oct-14
        ///     Content: lay du lieu dept tra ve datatable cho nv nghi viec
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public static DataTable GetByDeptIdForDatatableForLeaveEmp(string departments)
        {
            return new EmployeesDAL().GetByDeptIdForDatatableForLeaveEmp(departments);
        }

        /// <summary>
        ///     Author: Giang
        ///     Date: 11/13/2014
        ///     Content: Get All to DT
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllToDT()
        {
            return new EmployeesDAL().GetAll2();
        }

        public static List<EmployeesBLL> GetAll(int status)
        {
            return GenerateListEmployeesFromDataTable(new EmployeesDAL().GetAll(status));
        }

        public static DataTable GetAllExp()
        {
            return new EmployeesDAL().GetAllExp();
        }

        public static DataTable GetAllDT(int status)
        {
            return new EmployeesDAL().GetAll(status);
        }

        public static List<EmployeesBLL> GetAll2()
        {
            return GenerateListEmployeesFromDataTable(new EmployeesDAL().GetAll2());
        }

        public static List<EmployeesBLL> GetByRootId(int rootId)
        {
            return GenerateListEmployeesFromDataTable(new EmployeesDAL().GetByRootId(rootId));
        }

        public static List<EmployeesBLL> GetByDeptId(int deptId)
        {
            return GenerateListEmployeesFromDataTable(new EmployeesDAL().GetByDeptId(deptId));
        }

        public static DataTable GetDTByDeptId(int deptId)
        {
            return new EmployeesDAL().GetByDeptId(deptId);
        }

        public static List<EmployeesBLL> GetByDeptIds(string deptIds, int rootId, string fullname, string sortParameter)
        {
            var list = GenerateListEmployeesFromDataTable(new EmployeesDAL().GetByDeptIds(deptIds, rootId, fullname, 0));

            if (!string.IsNullOrEmpty(sortParameter))
                list.Sort(new EmployeesBLLComparer(sortParameter));

            return list;
        }

        public static DataTable GetByDeptIdsToDT(string deptIds, int rootId, string fullname, string sortParameter)
        {
            var list = new EmployeesDAL().GetByDeptIds(deptIds, rootId, fullname, 0);

            return list;
        }

        public static List<EmployeesBLL> GetByIds(string userIds)
        {
            return GenerateListEmployeesFromDataTable(new EmployeesDAL().GetByIds(userIds));
        }

        /// <summary>
        ///     Author: Giang
        ///     Date: 17-Oct-14
        ///     Content: Lay employee tra ve DT
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public static DataTable GetByIdsToDT(string userIds)
        {
            return new EmployeesDAL().GetByIds(userIds);
        }

        public static EmployeesBLL GetEmployeeById(int userId)
        {
            var objEmployeesDAL = new EmployeesDAL();
            var dt = objEmployeesDAL.GetOne(userId);
            if (dt.Rows.Count > 0)
                return GenerateEmployeeFromDataRow(dt.Rows[0]);
            return null;
        }

        public static DataRow GetDataRowEmployeeById(int userId)
        {
            //EmployeesDAL objEmployeesDAL = new EmployeesDAL();
            var dt = new EmployeesDAL().GetOne(userId);
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        public static DataRow GetDataRowEmployeeByUserName(string userName)
        {
            //EmployeesDAL objEmployeesDAL = new EmployeesDAL();
            var dt = new EmployeesDAL().GetOneByUserName(userName);
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        public static DataRow GetDataRowEmployeeByUserName2(string userName)
        {
            //EmployeesDAL objEmployeesDAL = new EmployeesDAL();
            var dt = new EmployeesDAL().GetOneByUserName2(userName);
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        public static List<EmployeesBLL> GetEmployeesByFilter(int deptId, string fullName)
        {
            return GenerateListEmployeesFromDataTable(new EmployeesDAL().GetUserByFilter(deptId, fullName));
        }

        public static List<EmployeesBLL> GetByUserCodeIsNull()
        {
            return GenerateListEmployeesFromDataTable(new EmployeesDAL().GetByUserCodeIsNull());
        }

        public static List<EmployeesBLL> GetByFilterAccountBank(string fullName, int rootId, string accountNo,
            string CardNo, int IsExists, string sortParameter)
        {
            var list =
                GenerateListEmployeesFromDataTable(new EmployeesDAL().GetByFilterAccountBank(fullName, rootId, accountNo,
                    CardNo, IsExists));
            if (!string.IsNullOrEmpty(sortParameter))
                list.Sort(new EmployeesBLLComparer(sortParameter));

            return list;
        }

        public static List<EmployeesBLL> GetByStatus(string fullName, string status)
        {
            return GenerateListEmployeesFromDataTable(new EmployeesDAL().GetByStatus(fullName, status));
        }

        public static DataTable GetDTByStatus(string fullName, string status)
        {
            return new EmployeesDAL().GetByStatus(fullName, status);
        }

        public static List<EmployeesBLL> GetByMoreSearch(int userId, string userName, string employeeCode,
            string fullName,
            int status, int monthOfLeave, int YearOfLeave,
            int monthOfBirth, int yearOfBirth, int MonthOfJoinDate,
            int yearOfJoinDate, int monthOfJoinCompanyDate, int YearOfJoinCompanyDate,
            int rootId)
        {
            return
                GenerateListEmployeesFromDataTable(new EmployeesDAL().GetByMoreSearch(userId, userName, employeeCode,
                    fullName,
                    status, monthOfLeave, YearOfLeave,
                    monthOfBirth, yearOfBirth, MonthOfJoinDate,
                    yearOfJoinDate, monthOfJoinCompanyDate, YearOfJoinCompanyDate, rootId));
        }


        /// <summary>
        ///     Dung them vao ngay 26MAR10
        /// </summary>
        /// <returns></returns>
        public static List<EmployeesBLL> GetTodayBirthdayEmployees()
        {
            return GenerateListEmployeesFromDataTable(new EmployeesDAL().GetTodayBirthdayEmployees());
        }

        public static EmployeesBLL GetByEmployeeCode(string employeeCode)
        {
            var objEmployeesDAL = new EmployeesDAL();
            var dt = objEmployeesDAL.GetByEmployeeCode(employeeCode);
            if (dt.Rows.Count > 0)
                return GenerateEmployeeFromDataRow(dt.Rows[0]);
            return null;
        }

        public static DataTable GetByStocks(string deptIds, int rootId, string fullname, int typeSort)
        {
            return new EmployeesDAL().GetByStocks(deptIds, rootId, fullname, typeSort);
        }

        #endregion

        #region public method Insert, update, delete

        public static long UpdateEmployeePartyInfo(DateTime DateJoinParty, DateTime DateJoinPartyOfficial,
            string PartyNumber, string PlaceJoinParty, int UserId)
        {
            return new EmployeesDAL().UpdateEmployeePartyInfo(DateJoinParty, DateJoinPartyOfficial, PartyNumber,
                PlaceJoinParty, UserId);
        }

        /// <summary>
        ///     Author: Giang
        ///     Date: 11/20/2014
        ///     Content: Update Rad Cong Viec
        /// </summary>
        /// <returns></returns>
        public static long UpdateRadCongViec(int status, string TaxCode, string TKNH, string SoTK, string bankname,
            string ins, string bhxh, int userId)
        {
            return new EmployeesDAL().UpdateRadCongViec(status, TaxCode, TKNH, SoTK, bankname, ins, bhxh, userId);
        }

        /// <summary>
        ///     Author: Giang
        ///     Date: 28-Oct-14
        ///     Content: Update Rad Thong tin chung
        /// </summary>
        /// <returns></returns>
        public static long UpdateRadThongTin(string fullName, string otherNames, string normalNames, int sex,
            DateTime birthday, string birthPlace, string nativePlace, string origin, string idCard, DateTime dateOfIssue,
            string placeOfIssue, string nation, string nationality, int marriage,
            string religion, DateTime joinDate, DateTime joinCompanyDate, int userId)
        {
            var idReturn = new EmployeesDAL().UpdateRadThongTin(fullName, otherNames, normalNames, sex,
                birthday, birthPlace, nativePlace, origin, idCard, dateOfIssue, placeOfIssue,
                nation, nationality, marriage, religion, joinDate, joinCompanyDate, userId);

            //CommandLogBLL objCL = new CommandLogBLL();
            //objCL.DataName = userId.ToString();
            //objCL.UserId = updateUserId;
            //objCL.ModuleId = Constants.CommandLog_Form_UpdateEmployeeDetailInforId;
            //objCL.CommandTypeId = Constants.CommandLog_Update_Id;
            //objCL.OldValues = "birthday:" + birthday.ToString() + "; birthPlace: " + birthPlace + "; nativePlace: " + nativePlace;
            //objCL.NewValues = "TK:" + accountNo + "; ATM: " + ATMNo + "; NH: " + bankName;
            //objCL.CommandLogDate = DateTime.Now;
            //objCL.Save();

            return idReturn;
        }

        public static long UpdateDirectWorking(int directWorking, int userId)
        {
            var idReturn = new EmployeesDAL().UpdateDirectWorking(directWorking, userId);

            //CommandLogBLL objCL = new CommandLogBLL();
            //objCL.DataName = userId.ToString();
            //objCL.UserId = updateUserId;
            //objCL.ModuleId = Constants.CommandLog_Form_UpdateEmployeeDetailInforId;
            //objCL.CommandTypeId = Constants.CommandLog_Update_Id;
            //objCL.OldValues = "birthday:" + birthday.ToString() + "; birthPlace: " + birthPlace + "; nativePlace: " + nativePlace;
            //objCL.NewValues = "TK:" + accountNo + "; ATM: " + ATMNo + "; NH: " + bankName;
            //objCL.CommandLogDate = DateTime.Now;
            //objCL.Save();

            return idReturn;
        }

        //------------------------------------------------------------------------------
        /// <summary>
        ///     Author: Giang
        ///     Date: 29-Oct-14
        ///     Content: Update Rad Thong tin lien he
        /// </summary>
        /// <returns></returns>
        public static long UpdateRadLienHe(string workphone, string cellphone, string homephone, string resident,
            string live, int userid)
        {
            var idReturn = new EmployeesDAL().UpdateRadLienHe(workphone, cellphone, homephone, resident, live, userid);

            //CommandLogBLL objCL = new CommandLogBLL();
            //objCL.DataName = userId.ToString();
            //objCL.UserId = updateUserId;
            //objCL.ModuleId = Constants.CommandLog_Form_UpdateEmployeeDetailInforId;
            //objCL.CommandTypeId = Constants.CommandLog_Update_Id;
            //objCL.OldValues = "birthday:" + birthday.ToString() + "; birthPlace: " + birthPlace + "; nativePlace: " + nativePlace;
            //objCL.NewValues = "TK:" + accountNo + "; ATM: " + ATMNo + "; NH: " + bankName;
            //objCL.CommandLogDate = DateTime.Now;
            //objCL.Save();

            return idReturn;
        }

        //-------------------------------------------------------------------------------------
        public static long UpdateHighestEducationLevel(int level, int userid)
        {
            var idReturn = new EmployeesDAL().UpdateHighestEducationLevel(level, userid);

            //CommandLogBLL objCL = new CommandLogBLL();
            //objCL.DataName = userId.ToString();
            //objCL.UserId = updateUserId;
            //objCL.ModuleId = Constants.CommandLog_Form_UpdateEmployeeDetailInforId;
            //objCL.CommandTypeId = Constants.CommandLog_Update_Id;
            //objCL.OldValues = "birthday:" + birthday.ToString() + "; birthPlace: " + birthPlace + "; nativePlace: " + nativePlace;
            //objCL.NewValues = "TK:" + accountNo + "; ATM: " + ATMNo + "; NH: " + bankName;
            //objCL.CommandLogDate = DateTime.Now;
            //objCL.Save();

            return idReturn;
        }

        //-----------------------------------------------
        /// <summary>
        ///     Author: Giang
        ///     Date: 31-Oct-14
        ///     Content: Update Rad Thong tin khac (chinh tri)
        /// </summary>
        /// <returns></returns>
        public static long UpdateRadChinhTri(DateTime dateJoinParty, string placeJoinParty, DateTime dateJoinCYU,
            string placeJoinCYU,
            DateTime dateOfEnlisted, DateTime dateOfDemobilized, string armyRank, int userId)
        {
            var idReturn = new EmployeesDAL().UpdateRadChinhTri(dateJoinParty, placeJoinParty, dateJoinCYU, placeJoinCYU,
                dateOfEnlisted, dateOfDemobilized, armyRank, userId);

            //CommandLogBLL objCL = new CommandLogBLL();
            //objCL.DataName = userId.ToString();
            //objCL.UserId = updateUserId;
            //objCL.ModuleId = Constants.CommandLog_Form_UpdateEmployeeDetailInforId;
            //objCL.CommandTypeId = Constants.CommandLog_Update_Id;
            //objCL.OldValues = "birthday:" + birthday.ToString() + "; birthPlace: " + birthPlace + "; nativePlace: " + nativePlace;
            //objCL.NewValues = "TK:" + accountNo + "; ATM: " + ATMNo + "; NH: " + bankName;
            //objCL.CommandLogDate = DateTime.Now;
            //objCL.Save();

            return idReturn;
        }

        //---------------------------------------------------------------------------------------------
        public static long InsertDefault(string fullName, string fullName2, DateTime birthDay, bool sex,
            string handPhone, string homePhone, int status, int departmentId)
        {
            return new EmployeesDAL().InsertDefault(fullName, fullName2, birthDay, sex, handPhone, homePhone, status,
                departmentId);
        }

        public static long UpdateInforGeneral(int sex, bool marriage, DateTime? joinDate, DateTime? joinCompanyDate,
            string workingPhone, string healthInsuranceNo, string healthInsuranceAddress, string socialInsuranceNo,
            int status, DateTime? leaveDate, int userId)
        {
            return new EmployeesDAL().UpdateInforGeneral(sex, marriage, joinDate, joinCompanyDate,
                workingPhone, healthInsuranceNo, healthInsuranceAddress, socialInsuranceNo,
                status, leaveDate, userId);
        }

        public static long UpdateInforDetail(string fullName, string otherNames, string normalNames, int sex,
            DateTime birthday, string birthPlace, string nativePlace, string resident, string live, string handPhone,
            string homePhone,
            string idCard, DateTime dateOfIssue, string placeOfIssue, string nation, string nationality,
            string religion, string origin, DateTime dateJoinParty, string placeJoinParty, DateTime dateJoinCYU,
            string placeJoinCYU,
            DateTime dateOfEnlisted, DateTime dateOfDemobilized, string armyRank, string workedCompany, int userId,
            int updateUserId)
        {
            var idReturn = new EmployeesDAL().UpdateInforDetail(fullName, otherNames, normalNames, sex,
                birthday, birthPlace, nativePlace, resident, live, handPhone, homePhone, idCard, dateOfIssue,
                placeOfIssue,
                nation, nationality, religion, origin, dateJoinParty, placeJoinParty, dateJoinCYU, placeJoinCYU,
                dateOfEnlisted, dateOfDemobilized, armyRank, workedCompany, userId);

            //CommandLogBLL objCL = new CommandLogBLL();
            //objCL.DataName = userId.ToString();
            //objCL.UserId = updateUserId;
            //objCL.ModuleId = Constants.CommandLog_Form_UpdateEmployeeDetailInforId;
            //objCL.CommandTypeId = Constants.CommandLog_Update_Id;
            //objCL.OldValues = "birthday:" + birthday.ToString() + "; birthPlace: " + birthPlace + "; nativePlace: " + nativePlace;
            //objCL.NewValues = "TK:" + accountNo + "; ATM: " + ATMNo + "; NH: " + bankName;
            //objCL.CommandLogDate = DateTime.Now;
            //objCL.Save();

            return idReturn;
        }

        public static long InsertByImporting(string userCode, string fullName, string departmentName,
            string positionName)
        {
            var objDAL = new EmployeesDAL();
            return objDAL.InsertByImporting(userCode, fullName, departmentName, positionName);
        }

        public static long Delete(int userId)
        {
            return new EmployeesDAL().Delete(userId);
        }

        public static long UpdateEmployeeCode(string employeeCode, int userId)
        {
            return new EmployeesDAL().UpdateEmployeeCode(employeeCode, userId);
        }

        public static long UpdateAccountBank(string accountNo, string ATMNo, string bankName, int userId,
            string positionName, int UpdatedUser)
        {
            long returnId = 0;
            var obj = GetEmployeeById(userId);
            returnId = new EmployeesDAL().UpdateAccountBank(accountNo, ATMNo, bankName, userId);
            var objCL = new CommandLogBLL();
            objCL.DataName = userId.ToString();
            objCL.UserId = UpdatedUser;
            objCL.ModuleId = Constants.CommandLog_Form_AccountBankId;
            objCL.CommandTypeId = Constants.CommandLog_Update_Id;
            objCL.OldValues = "TK:" + obj.AccountNo + "; ATM: " + obj.ATMNo + "; NH: " + obj.BankName;
            objCL.NewValues = "TK:" + accountNo + "; ATM: " + ATMNo + "; NH: " + bankName;
            objCL.CommandLogDate = DateTime.Now;
            objCL.Save();
            return returnId;
        }

        public static long UpdateStatus(int status, int userId)
        {
            return new EmployeesDAL().UpdateStatus(status, userId);
        }

        public static long UpdateFullName2(string fullName, int userId)
        {
            return new EmployeesDAL().UpdateFullName2(fullName, userId);
        }

        public static long UpdateTaxCode(string taxCode, int userId)
        {
            return new EmployeesDAL().UpdateTaxCode(taxCode, userId);
        }

        public static long UpdatePassword(string password, int userId)
        {
            return new EmployeesDAL().UpdatePassword(password, userId);
        }


        public static long UpdateStock(string HandPhone, string HomePhone, string Resident, string Live, string IdCard,
            DateTime DateOfIssue, string PlaceOfIssue,
            int InvestorNo, int SeniorStock, int UndertakingYear, int UnderTakingStock, int SeniorStockBought,
            int UnderTakingStockBought, int UpdatedUserid,
            DateTime UpdatedDate, bool ConfirmStocks, int UserId)
        {
            return new EmployeesDAL().UpdateStock(HandPhone, HomePhone, Resident, Live, IdCard, DateOfIssue,
                PlaceOfIssue,
                InvestorNo, SeniorStock, UndertakingYear, UnderTakingStock, SeniorStockBought, UnderTakingStockBought,
                UpdatedUserid,
                UpdatedDate, ConfirmStocks, UserId);
        }

        public static long UpdateStock2(int SeniorStockRegistered, int UnderTakingStockRegistered, int UserId)
        {
            return new EmployeesDAL().UpdateStock2(SeniorStockRegistered, UnderTakingStockRegistered, UserId);
        }

        #region Update New Version

        public long UpdateGeneralInfo()
        {
            var idReturn = new EmployeesDAL().UpdateGeneralInfo(EmployeeCode, FullName, Sex, Birthday, BirthPlace,
                NativePlace, NativeCity, IdCard, DateOfIssue, PlaceOfIssue, PassportNo, PassportDateOfIssue,
                PassportPlaceOfIssue, PassportDateOfExpiry, Marriage, Origin, Nation, Religion, Nationality,
                WorkedCompany, UserId);

            return idReturn;
        }

        public long UpdateContactInfo()
        {
            var idReturn = new EmployeesDAL().UpdateContactInfo(WorkingPhone, HomePhone, HandPhone, Resident, Live,
                OtherPhone, WorkingEmail, OtherName, YahooId, SkypeId, MsnId, Guarantor, GuarantorWorkingPlace,
                GuarantorPhone, GuarantorAddress, ResidentCity, ResidentCountry, CurrentCity, CurrentCountry,
                UrgentContactName, UrgentContactRelation, UrgentContactCellPhone, UrgentContactHomePhone,
                UrgentContactEmail, UrgentContactAddress, UserId);
            return idReturn;
        }

        public long UpdateWorkingInfo()
        {
            var idReturn = new EmployeesDAL().UpdateWorkingInfo(TaxCode, AccountNo, BankName, SocialInsuranceNo,
                HealthInsuranceNo, LeaveDate, UserName, SocialInsuranceRegDate, SocialInsurancePercentage,
                SocialInsuranceProvinceCode, HealthInsuranceDateOfExpiry, HealthInsuranceRegHospital, Status, UserId);
            return idReturn;
        }

        public long UpdateOtherInfo()
        {
            var idReturn = new EmployeesDAL().UpdateOtherInfo(DateJoinParty, PlaceJoinParty, PartyPosition, DateJoinCYU,
                PlaceJoinCYU, CYUPosition, DateOfEnlisted, DateOfDemobilized, ArmyRank, ArmyPosition, ArmyBranch,
                ArmyUnit, DemobilizationReason, IsWarInvalid, RevolutionJoinDate, WarInvalidLevel, WarInvalidPercentage,
                IsEntitledToTreatment, BloodGroup, Height, Weight, HealthInformation, HealthRemarks, SicknessRemarks,
                IsHandicaped, PersonalTarget, Hobbies, Strengths, Weaknesses, UserId);
            return idReturn;
        }

        #endregion

        #endregion

        #region other methods

        public static long ChangePassword(string userName, string oldPassword, string newPassword)
        {
            var objDAL = new EmployeesDAL();
            return objDAL.ChangePassword(userName, oldPassword, newPassword);
        }

        public static EmployeesBLL Login(string userName, string password)
        {
            var objDAL = new EmployeesDAL();

            var dt = objDAL.Login(userName, password);

            if (dt.Rows.Count > 0)
                return GenerateEmployeeFromDataRow(dt.Rows[0]);
            return null;
        }

        public static DataRow LoginNew(string userName, string password)
        {
            var objDAL = new EmployeesDAL();

            var dt = objDAL.Login(userName, password);

            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }


        public static int CheckNewEmployee(string fileName, string sheetName, out List<EmployeesBLL> list)
        {
            int returnValue = 1, countColumn = 0;
            var helper = new ExcelHelper(fileName);
            list = new List<EmployeesBLL>();

            try
            {
                var dt = helper.ReadDataFromExcelToDataTable(sheetName);
                for (var i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName.Equals("Mã nhân viên"))
                        countColumn++;
                    else if (dt.Columns[i].ColumnName.Equals("Họ và tên"))
                        countColumn++;
                    if (countColumn == 2)
                        break;
                }
                if (countColumn < 2)
                {
                    returnValue = -1;
                }
                else
                {
                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        var dr = dt.Rows[i];
                        var userCode = dr["Mã nhân viên"].ToString().Trim();
                        var fullname = dr["Họ và tên"].ToString().Trim();
                        var isNew = new EmployeesDAL().CheckNewEmployee(userCode);


                        if (isNew == 1)
                            list.Add(new EmployeesBLL(0, "", userCode, "", fullname, FormatDate.GetSQLDateMinValue));
                    }
                    list.Sort();
                }
            }
            catch
            {
                returnValue = -2;
            }

            return returnValue;
        }

        public static long CheckEmployeeExistence(string userName)
        {
            var returnValue = new EmployeesDAL().CheckEmployeeExistence(userName);

            return returnValue;
        }

        #endregion

        #region private methods, generate helper methods

        private static List<EmployeesBLL> GenerateListEmployeesFromDataTable(DataTable dt)
        {
            var lstEmployees = new List<EmployeesBLL>();

            foreach (DataRow dr in dt.Rows)
                lstEmployees.Add(GenerateEmployeeFromDataRow(dr));

            return lstEmployees;
        }

        private static EmployeesBLL GenerateEmployeeFromDataRow(DataRow dr)
        {
            var objEmployeesBLL = new EmployeesBLL(
                dr[EmployeeKeys.FIELD_EMPLOYEES_USERID] == DBNull.Value
                    ? 0
                    : (int) dr[EmployeeKeys.FIELD_EMPLOYEES_USERID],
                dr[EmployeeKeys.FIELD_EMPLOYEES_USERNAME] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_USERNAME],
                dr[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE],
                dr[EmployeeKeys.FIELD_EMPLOYEES_PASSWORD] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_PASSWORD],
                dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME],
                dr[EmployeeKeys.FIELD_EMPLOYEES_BIRTHDAY] == DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : (DateTime) dr[EmployeeKeys.FIELD_EMPLOYEES_BIRTHDAY]
            );

            /// General
            objEmployeesBLL.Marriage = dr[EmployeeKeys.FIELD_EMPLOYEES_MARRIAGE] == DBNull.Value
                ? false
                : (bool) dr[EmployeeKeys.FIELD_EMPLOYEES_MARRIAGE];
            objEmployeesBLL.Sex = dr[EmployeeKeys.FIELD_EMPLOYEES_SEX] == DBNull.Value
                ? 2
                : (int) dr[EmployeeKeys.FIELD_EMPLOYEES_SEX];
            objEmployeesBLL.JoinDate = dr[EmployeeKeys.FIELD_EMPLOYEES_JOINDATE] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : (DateTime) dr[EmployeeKeys.FIELD_EMPLOYEES_JOINDATE];
            objEmployeesBLL.JoinCompanyDate = dr[EmployeeKeys.Field_Employees_JoinCompanyDate] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : (DateTime) dr[EmployeeKeys.Field_Employees_JoinCompanyDate];
            objEmployeesBLL.WorkingPhone = dr[EmployeeKeys.FIELD_EMPLOYEES_WORKING_PHONE] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_WORKING_PHONE];
            objEmployeesBLL.HandPhone = dr[EmployeeKeys.FIELD_EMPLOYEES_HAND_PHONE] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_HAND_PHONE];
            objEmployeesBLL.HomePhone = dr[EmployeeKeys.FIELD_EMPLOYEES_HOME_PHONE] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_HOME_PHONE];
            objEmployeesBLL.AccountNo = dr[EmployeeKeys.FIELD_EMPLOYEES_ACCOUNT_NO] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_ACCOUNT_NO];
            objEmployeesBLL.ATMNo = dr[EmployeeKeys.FIELD_EMPLOYEES_ATM_NO] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_ATM_NO];
            objEmployeesBLL.BankName = dr[EmployeeKeys.FIELD_EMPLOYEES_BANK_NAME] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_BANK_NAME];
            objEmployeesBLL.HealthInsuranceNo = dr[EmployeeKeys.FIELD_EMPLOYEES_HEALTH_INSURANCE_NO] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_HEALTH_INSURANCE_NO];
            objEmployeesBLL.HealthInsuranceAddress = dr[EmployeeKeys.FIELD_EMPLOYEES_HEALTH_INSURANCE_ADDRESS] ==
                                                     DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_HEALTH_INSURANCE_ADDRESS];
            objEmployeesBLL.SocialInsuranceNo = dr[EmployeeKeys.FIELD_EMPLOYEES_SOCIAL_INSURANCE_NO] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_SOCIAL_INSURANCE_NO];
            objEmployeesBLL.Status = dr[EmployeeKeys.FIELD_EMPLOYEES_STATUS] == DBNull.Value
                ? 0
                : (int) dr[EmployeeKeys.FIELD_EMPLOYEES_STATUS];

            objEmployeesBLL.LeaveDate = dr[EmployeeKeys.FIELD_EMPLOYEES_LeaveDate] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : (DateTime) dr[EmployeeKeys.FIELD_EMPLOYEES_LeaveDate];

            /// CV
            /// 
            objEmployeesBLL.FullName = dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME];
            objEmployeesBLL.FullName2 = dr[EmployeeKeys.Field_Employees_FullName2] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeKeys.Field_Employees_FullName2];
            objEmployeesBLL.OtherName = dr[EmployeeKeys.FIELD_EMPLOYEES_OTHERNAMES] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_OTHERNAMES];
            objEmployeesBLL.NormalNames = dr[EmployeeKeys.FIELD_EMPLOYEES_NORMALNAMES] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_NORMALNAMES];
            objEmployeesBLL.Sex = dr[EmployeeKeys.FIELD_EMPLOYEES_SEX] == DBNull.Value
                ? -1
                : (int) dr[EmployeeKeys.FIELD_EMPLOYEES_SEX];
            if (objEmployeesBLL.Sex == 1)
                objEmployeesBLL.SexName = "Nam";
            else if (objEmployeesBLL.Sex == 0)
                objEmployeesBLL.SexName = "Nữ";
            else
                objEmployeesBLL.SexName = "";
            objEmployeesBLL.Birthday = dr[EmployeeKeys.FIELD_EMPLOYEES_BIRTHDAY] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr[EmployeeKeys.FIELD_EMPLOYEES_BIRTHDAY].ToString());

            objEmployeesBLL.DayOfBirth = dr[EmployeeKeys.Field_Employees_DayOfBirth] == DBNull.Value
                ? 0
                : (int) dr[EmployeeKeys.Field_Employees_DayOfBirth];
            objEmployeesBLL.MonthOfBirth = dr[EmployeeKeys.Field_Employees_MonthOfBirth] == DBNull.Value
                ? 0
                : (int) dr[EmployeeKeys.Field_Employees_MonthOfBirth];
            objEmployeesBLL.YearOfBirth = dr[EmployeeKeys.Field_Employees_YearOfBirth] == DBNull.Value
                ? 0
                : (int) dr[EmployeeKeys.Field_Employees_YearOfBirth];

            objEmployeesBLL.BirthPlace = dr[EmployeeKeys.FIELD_EMPLOYEES_BIRTHPLACE] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_BIRTHPLACE];
            objEmployeesBLL.NativePlace = dr[EmployeeKeys.FIELD_EMPLOYEES_NATIVEPLACE] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_NATIVEPLACE];
            objEmployeesBLL.Resident = dr[EmployeeKeys.FIELD_EMPLOYEES_RESIDENT] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_RESIDENT];
            objEmployeesBLL.Live = dr[EmployeeKeys.FIELD_EMPLOYEES_LIVE] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_LIVE];
            objEmployeesBLL.IdCard = dr[EmployeeKeys.FIELD_EMPLOYEES_IDCARD] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_IDCARD];
            objEmployeesBLL.DateOfIssue = dr[EmployeeKeys.FIELD_EMPLOYEES_DATE_OF_ISSUE] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr[EmployeeKeys.FIELD_EMPLOYEES_DATE_OF_ISSUE].ToString());
            objEmployeesBLL.PlaceOfIssue = dr[EmployeeKeys.FIELD_EMPLOYEES_PLACE_OF_ISSUE] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_PLACE_OF_ISSUE];
            objEmployeesBLL.Nation = dr[EmployeeKeys.FIELD_EMPLOYEES_NATION] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_NATION];
            objEmployeesBLL.Nationality = dr[EmployeeKeys.FIELD_EMPLOYEES_NATIONALITY] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_NATIONALITY];
            objEmployeesBLL.Religion = dr[EmployeeKeys.FIELD_EMPLOYEES_RELIGION] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_RELIGION];
            objEmployeesBLL.Origin = dr[EmployeeKeys.FIELD_EMPLOYEES_ORIGIN] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_ORIGIN];
            objEmployeesBLL.DateJoinParty = dr[EmployeeKeys.FIELD_EMPLOYEES_DATE_JOIN_PARTY] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr[EmployeeKeys.FIELD_EMPLOYEES_DATE_JOIN_PARTY]);
            objEmployeesBLL.PlaceJoinParty = dr[EmployeeKeys.FIELD_EMPLOYEES_PLACE_JOIN_PARTY] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_PLACE_JOIN_PARTY];
            objEmployeesBLL.DateOfEnlisted = dr[EmployeeKeys.FIELD_EMPLOYEES_DATE_OF_ENLISTED] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr[EmployeeKeys.FIELD_EMPLOYEES_DATE_OF_ENLISTED].ToString());
            objEmployeesBLL.DateOfDemobilized = dr[EmployeeKeys.FIELD_EMPLOYEES_DATE_OF_DEMOBILIZED] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr[EmployeeKeys.FIELD_EMPLOYEES_DATE_OF_DEMOBILIZED].ToString());
            objEmployeesBLL.ArmyRank = dr[EmployeeKeys.FIELD_EMPLOYEES_ARMYRANK] == DBNull.Value
                ? string.Empty
                : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_ARMYRANK];

            //
            try
            {
                objEmployeesBLL.DateJoinCYU = dr[EmployeeKeys.FIELD_EMPLOYEES_DATE_JOIN_CYU] == DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(dr[EmployeeKeys.FIELD_EMPLOYEES_DATE_JOIN_CYU]);
                objEmployeesBLL.PlaceJoinCYU = dr[EmployeeKeys.FIELD_EMPLOYEES_PLACE_JOIN_CYU] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_PLACE_JOIN_CYU];
            }
            catch
            {
            }

            try
            {
                objEmployeesBLL.WorkedCompany = dr[EmployeeKeys.FIELD_EMPLOYEES_WORKED_COMPANY] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_WORKED_COMPANY];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                    ? string.Empty
                    : (string) dr[PositionKeys.FIELD_POSITION_NAME];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.LevelPosition = dr[PositionKeys.Field_Position_LevelPosition] == DBNull.Value
                    ? 0
                    : (int) dr[PositionKeys.Field_Position_LevelPosition];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.DepartmentCode = dr[DepartmentKeys.FIELD_DEPARTMENT_CODE] == DBNull.Value
                    ? string.Empty
                    : (string) dr[DepartmentKeys.FIELD_DEPARTMENT_CODE];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.DepartmentName = dr[DepartmentKeys.FIELD_DEPARTMENT_NAME] == DBNull.Value
                    ? string.Empty
                    : (string) dr[DepartmentKeys.FIELD_DEPARTMENT_NAME];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.DepartmentFullName = dr[DepartmentKeys.FIELD_DEPARTMENT_DEPARTMENTFULLNAME] ==
                                                     DBNull.Value
                    ? string.Empty
                    : (string) dr[DepartmentKeys.FIELD_DEPARTMENT_DEPARTMENTFULLNAME];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.DepartmentId = dr[DepartmentKeys.FIELD_DEPARTMENT_ID] == DBNull.Value
                    ? 0
                    : (int) dr[DepartmentKeys.FIELD_DEPARTMENT_ID];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.RootId = dr[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID] == DBNull.Value
                    ? 0
                    : (int) dr[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.RootName = dr[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME] == DBNull.Value
                    ? string.Empty
                    : (string) dr[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.ParentId = dr[DepartmentKeys.FIELD_DEPARTMENT_PARENT_ID] == DBNull.Value
                    ? 0
                    : (int) dr[DepartmentKeys.FIELD_DEPARTMENT_PARENT_ID];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.DepartmentLevel = dr[DepartmentKeys.FIELD_DEPARTMENT_LEVEL] == DBNull.Value
                    ? 0
                    : (int) dr[DepartmentKeys.FIELD_DEPARTMENT_LEVEL];
            }
            catch
            {
            }

            try
            {
                objEmployeesBLL.TaxCode = dr[EmployeeKeys.Field_Employees_TaxCode] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_TaxCode];
            }
            catch
            {
            }

            #region new fields

            try
            {
                objEmployeesBLL.NativeCity = dr[EmployeeKeys.Field_Employees_NativeCity] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_NativeCity];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.PassportNo = dr[EmployeeKeys.Field_Employees_PassportNo] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_PassportNo];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.PassportDateOfIssue = dr[EmployeeKeys.Field_Employees_PassportDateOfIssue] ==
                                                      DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(dr[EmployeeKeys.Field_Employees_PassportDateOfIssue]);
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.PassportPlaceOfIssue = dr[EmployeeKeys.Field_Employees_PassportPlaceOfIssue] ==
                                                       DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_PassportPlaceOfIssue];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.PassportDateOfExpiry = dr[EmployeeKeys.Field_Employees_PassportDateOfExpiry] ==
                                                       DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(dr[EmployeeKeys.Field_Employees_PassportDateOfExpiry]);
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.OtherPhone = dr[EmployeeKeys.Field_Employees_OtherPhone] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_OtherPhone];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.WorkingEmail = dr[EmployeeKeys.Field_Employees_WorkingEmail] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_WorkingEmail];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.PersonalEmail = dr[EmployeeKeys.Field_Employees_PersonalEmail] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_PersonalEmail];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.OtherEmail = dr[EmployeeKeys.Field_Employees_OtherEmail] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_OtherEmail];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.YahooId = dr[EmployeeKeys.Field_Employees_YahooId] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_YahooId];
            }
            catch
            {
            }

            try
            {
                objEmployeesBLL.SkypeId = dr[EmployeeKeys.Field_Employees_SkypeId] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_SkypeId];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.MsnId = dr[EmployeeKeys.Field_Employees_MsnId] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_MsnId];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.Guarantor = dr[EmployeeKeys.Field_Employees_Guarantor] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_Guarantor];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.GuarantorWorkingPlace = dr[EmployeeKeys.Field_Employees_GuarantorWorkingPlace] ==
                                                        DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_GuarantorWorkingPlace];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.ResidentCity = dr[EmployeeKeys.Field_Employees_ResidentCity] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_ResidentCity];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.ResidentCountry = dr[EmployeeKeys.Field_Employees_ResidentCountry] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_ResidentCountry];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.CurrentCity = dr[EmployeeKeys.Field_Employees_CurrentCity] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_CurrentCity];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.CurrentCountry = dr[EmployeeKeys.Field_Employees_CurrentCountry] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_CurrentCountry];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.UrgentContactName = dr[EmployeeKeys.Field_Employees_UrgentContactName] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_UrgentContactName];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.UrgentContactRelation = dr[EmployeeKeys.Field_Employees_UrgentContactRelation] ==
                                                        DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_UrgentContactRelation];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.UrgentContactCellPhone = dr[EmployeeKeys.Field_Employees_UrgentContactCellPhone] ==
                                                         DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_UrgentContactCellPhone];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.UrgentContactHomePhone = dr[EmployeeKeys.Field_Employees_UrgentContactHomePhone] ==
                                                         DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_UrgentContactHomePhone];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.UrgentContactEmail = dr[EmployeeKeys.Field_Employees_UrgentContactEmail] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_UrgentContactEmail];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.UrgentContactAddress = dr[EmployeeKeys.Field_Employees_UrgentContactAddress] ==
                                                       DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_UrgentContactAddress];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.SocialInsuranceRegDate = dr[EmployeeKeys.Field_Employees_SocialInsuranceRegDate] ==
                                                         DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(dr[EmployeeKeys.Field_Employees_SocialInsuranceRegDate]);
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.SocialInsurancePercentage =
                    dr[EmployeeKeys.Field_Employees_SocialInsurancePercentage] == DBNull.Value
                        ? 0
                        : (float) dr[EmployeeKeys.Field_Employees_SocialInsurancePercentage];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.SocialInsuranceProvinceCode =
                    dr[EmployeeKeys.Field_Employees_SocialInsuranceProvinceCode] == DBNull.Value
                        ? 0
                        : (int) dr[EmployeeKeys.Field_Employees_SocialInsuranceProvinceCode];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.HealthInsuranceDateOfExpiry =
                    dr[EmployeeKeys.Field_Employees_HealthInsuranceDateOfExpiry] == DBNull.Value
                        ? FormatDate.GetSQLDateMinValue
                        : Convert.ToDateTime(dr[EmployeeKeys.Field_Employees_HealthInsuranceDateOfExpiry]);
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.HealthInsuranceRegHospital =
                    dr[EmployeeKeys.Field_Employees_HealthInsuranceRegHospital] == DBNull.Value
                        ? string.Empty
                        : (string) dr[EmployeeKeys.Field_Employees_HealthInsuranceRegHospital];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.PartyPosition = dr[EmployeeKeys.Field_Employees_PartyPosition] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_PartyPosition];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.CYUPosition = dr[EmployeeKeys.Field_Employees_CYUPosition] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_CYUPosition];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.ArmyPosition = dr[EmployeeKeys.Field_Employees_ArmyPosition] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_ArmyPosition];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.ArmyBranch = dr[EmployeeKeys.Field_Employees_ArmyBranch] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_ArmyBranch];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.IsWarInvalid = dr[EmployeeKeys.Field_Employees_IsWarInvalid] == DBNull.Value
                    ? 0
                    : (int) dr[EmployeeKeys.Field_Employees_IsWarInvalid];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.RevolutionJoinDate = dr[EmployeeKeys.Field_Employees_RevolutionJoinDate] == DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(dr[EmployeeKeys.Field_Employees_RevolutionJoinDate]);
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.WarInvalidLevel = dr[EmployeeKeys.Field_Employees_WarInvalidLevel] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_WarInvalidLevel];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.WarInvalidPercentage = dr[EmployeeKeys.Field_Employees_WarInvalidPercentage] ==
                                                       DBNull.Value
                    ? 0
                    : (int) dr[EmployeeKeys.Field_Employees_WarInvalidPercentage];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.IsEntitledToTreatment = dr[EmployeeKeys.Field_Employees_IsEntitledToTreatment] ==
                                                        DBNull.Value
                    ? 0
                    : (int) dr[EmployeeKeys.Field_Employees_IsEntitledToTreatment];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.BloodGroup = dr[EmployeeKeys.Field_Employees_BloodGroup] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_BloodGroup];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.Height = dr[EmployeeKeys.Field_Employees_Height] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_Height];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.Weight = dr[EmployeeKeys.Field_Employees_Weight] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_Weight];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.HealthInformation = dr[EmployeeKeys.Field_Employees_HealthInformation] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_HealthInformation];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.HealthRemarks = dr[EmployeeKeys.Field_Employees_HealthRemarks] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_HealthRemarks];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.SicknessRemarks = dr[EmployeeKeys.Field_Employees_SicknessRemarks] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_SicknessRemarks];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.IsHandicaped = dr[EmployeeKeys.Field_Employees_IsHandicaped] == DBNull.Value
                    ? 0
                    : (int) dr[EmployeeKeys.Field_Employees_IsHandicaped];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.PersonalTarget = dr[EmployeeKeys.Field_Employees_PersonalTarget] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_PersonalTarget];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.Hobbies = dr[EmployeeKeys.Field_Employees_Hobbies] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_Hobbies];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.Strengths = dr[EmployeeKeys.Field_Employees_Strengths] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_Strengths];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.Weaknesses = dr[EmployeeKeys.Field_Employees_Weaknesses] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_Weaknesses];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.DemobilizationReason = dr[EmployeeKeys.Field_Employees_DemobilizationReason] ==
                                                       DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_DemobilizationReason];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.ArmyUnit = dr[EmployeeKeys.Field_Employees_ArmyUnit] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_ArmyUnit];
            }
            catch
            {
            }
            try
            {
                objEmployeesBLL.PersonalStatus = dr[EmployeeKeys.Field_Employees_PersonalStatus] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_PersonalStatus];
            }
            catch
            {
            }

            try
            {
                objEmployeesBLL.GuarantorPhone = dr[EmployeeKeys.Field_Employees_GuarantorPhone] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_GuarantorPhone];
            }
            catch
            {
            }

            try
            {
                objEmployeesBLL.GuarantorAddress = dr[EmployeeKeys.Field_Employees_GuarantorAddress] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.Field_Employees_GuarantorAddress];
            }
            catch
            {
            }

            #endregion

            return objEmployeesBLL;
        }

        #endregion

        #region for combo boxes

        public static DataTable GetComboBoxData(string cbName)
        {
            return new EmployeesDAL().GetComboBoxDataByName(cbName);
        }


        public int GetComboBoxItemId(int typeId)
        {
            try
            {
                var dt = new EmployeesDAL().GetComboBoxItemId(UserId, typeId);
                return int.Parse(dt.Rows[0]["ItemId"].ToString());
            }
            catch
            {
                return -1;
            }
        }

        public long SaveComboBoxItemId(int typeId, int id)
        {
            var idReturn = new EmployeesDAL().SaveComboBoxItemId(UserId, typeId, id);
            return idReturn;
        }

        public static long AddRemoveUpdateComboBoxItem(int id, int typeId, string typeName, string description,
            string remark, string mode)
        {
            var idReturn = new EmployeesDAL().AddRemoveUpdateComboBoxItem(id, typeId, typeName, description, remark,
                mode);
            return idReturn;
        }

        #endregion
    }
}