using System;
using System.Text;

namespace HRMDAL.H1.KeyNames
{
    sealed public class WorkdayEmployeeKeys
    {
        /// <summary>
        /// Some fields in H1_WorkdayEmployees table
        /// </summary>
        public const string FIELD_WORKDAY_EMPLOYEE_ID = "WorkdayEmployeeId";
        public const string FIELD_WORKDAY_EMPLOYEE_CREATE_DATE = "CreateDate";
        public const string FIELD_WORKDAY_EMPLOYEE_NC_LAM_VIEC = "NC_LamViec";
        public const string FIELD_WORKDAY_EMPLOYEE_F_OM_KHHDS = "F_OmKHHDS";
        public const string FIELD_WORKDAY_EMPLOYEE_F_OM_DaiNgay = "F_OmDaiNgay";
        public const string FIELD_WORKDAY_EMPLOYEE_F_THAI_SAN = "F_ThaiSan";
        public const string FIELD_WORKDAY_EMPLOYEE_F_TNLD = "F_TNLD";
        public const string FIELD_WORKDAY_EMPLOYEE_F_NAM = "F_Nam";
        public const string FIELD_WORKDAY_EMPLOYEE_F_DAC_BIET = "F_db";
        public const string FIELD_WORKDAY_EMPLOYEE_F_KO_LUONG_KLD = "F_KoLuongKLD";
        public const string FIELD_WORKDAY_EMPLOYEE_F_KO_LUONG_CLD = "F_KoLuongCLD";
        public const string FIELD_WORKDAY_EMPLOYEE_F_DI_DUONG = "F_DiDuong";
        public const string FIELD_WORKDAY_EMPLOYEE_F_CONG_TAC = "F_CongTac";
        public const string FIELD_WORKDAY_EMPLOYEE_F_LE = "F_Le";
        public const string FIELD_WORKDAY_EMPLOYEE_F_HOC1 = "F_Hoc1";
        public const string FIELD_WORKDAY_EMPLOYEE_F_HOC2 = "F_Hoc2";
        public const string FIELD_WORKDAY_EMPLOYEE_F_HOC3 = "F_Hoc3";
        public const string FIELD_WORKDAY_EMPLOYEE_F_HOC4 = "F_Hoc4";
        public const string FIELD_WORKDAY_EMPLOYEE_F_HOC5 = "F_Hoc5";
        public const string FIELD_WORKDAY_EMPLOYEE_F_HOC6 = "F_Hoc6";
        public const string FIELD_WORKDAY_EMPLOYEE_F_HOC7 = "F_Hoc7";
        public const string FIELD_WORKDAY_EMPLOYEE_GC_LT_BAN_NGAY = "GC_LTBanNgay";
        public const string FIELD_WORKDAY_EMPLOYEE_GC_LT_BAN_DEM = "GC_LTBanDem";
        public const string FIELD_WORKDAY_EMPLOYEE_GC_LAM_DEM = "GC_LamDem";
        public const string FIELD_WORKDAY_EMPLOYEE_HTCV = "HTCV";

        /// <summary>
        /// store procedure for 
        /// </summary>
        /// 
        public const string SP_WORKDAY_EMPLOYEE_GET_BY_FILTER = "Sel_H1_WorkdayEmployee_By_Filter";
        public const string SP_WORKDAY_EMPLOYEE_GET_BY_ID = "Sel_H1_WorkdayEmployee_By_Id";
        public const string SP_WORKDAY_EMPLOYEE_GET_BY_USERID_MONTH_YEAR = "Sel_H1_WorkdayEmployee_By_UserId_Mont_Year";
        
        /// <summary>
        /// 
        /// </summary>
        public const string SP_WORKDAY_EMPLOYEE_INSERT = "Ins_H1_WorkdayEmployee";
        public const string SP_WORKDAY_EMPLOYEE_INSERT_BY_DATE = "Ins_H1_WorkdayEmployee_By_Date";
        public const string SP_WORKDAY_EMPLOYEE_UPDATE = "Upd_H1_WorkdayEmployee";
        public const string SP_WORKDAY_EMPLOYEE_UPDATE_BY_STUDYING = "Upd_H1_WorkdayEmployeeByStudying";
        public const string SP_WORKDAY_EMPLOYEE_UPDATE_BY_USERID_LEAVETYPE_DATE = "Upd_H1_WorkdayEmployee_By_UserId_LeaveType_Date";
        public const string SP_WORKDAY_EMPLOYEE_DELETE = "Del_H1_WorkdayEmployee";
        public const string SP_WORKDAY_EMPLOYEE_DELETE_BY_DATE = "Del_H1_WorkdayEmployee_By_Date";
    }
}
