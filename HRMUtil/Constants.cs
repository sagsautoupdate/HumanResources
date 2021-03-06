using System;
using System.Collections.Generic;

namespace HRMUtil
{
    public class Constants
    {
        #region Relation Employee

        public const int RelationType_Child_Id = 24;

        #endregion

        #region EmployeeTimeBill

        public static double LunchTime = 1;

        #endregion

        public static int DataType_Run = 1; ////
        public static int DataType_Import = 2; ////

        #region Export

        public static int EXPORT_DATA_SUCCESSFUL = 1;
        public static string EXPORT_DATA_SUCCESSFUL_MSG = "Trích xuất dữ liệu thành công";
        public static string EXPORT_DATA_NOINFOR_MSG = "Không có dữ liệu";
        public static int EXPORT_DATA_ERROR = -1;
        public static int EXPORT_DATA_NOINFOR = 0;

        public static string ACV_NAME = "TỔNG CÔNG TY CẢNG HÀNG KHÔNG VIỆT NAM";
        public static string SAGS_NAME = "CÔNG TY CỔ PHẦN PVMĐ SÀI GÒN";

        #endregion

        #region Session Variables

        public static string KEY_USER_CURRENT = "Constant.KeyUserCurrent";
        public static string KEY_IS_ADMINISTRATOR = "Constant.KeyAdministrator";
        public static string KEY_IS_HR_MANAGER = "Constant.KeyIsHRManager";
        public static string KEY_IS_HR_MEMBER = "Constant.KeyIsHRMember";
        public static string KEY_IS_HR_ADMINISTRATOR = "Constant.KeyIsHRAdministrator";
        public static string KEY_IS_FINANCIAL_ACCOUNTING = "Constant.KeyIsFinancial_Accounting";

        public static string KeyIsTimeKeepingLeader = "Constant.KeyIsTimeKeepingLeader";
        public static string KeyIsTimeKeepingManager = "Constant.KeyIsTimeKeepingManager";
        public static string KeyIsTimeKeepingSuppervisor = "Constant.KeyIsTimeKeepingSuppervisor";
        public static string KeyIsTimeKeepingGroup = "Constant.KeyIsTimeKeepingGroup";

        public static string KeyIsTrainingManager = "Constant.KeyIsTrainingManager";
        public static string KeyIsQAManager = "Constant.KeyIsQAManager";

        #endregion

        #region Role

        public const int ROLE_ADMINISTRATORS_ID = 1;
        public const int ROLE_HR_ADMINISTRATORS_ID = 2;
        public const int ROLE_HR_MANAGER_ID = 7;
        public const int ROLE_HR_Member_ID = 8;
        public const int ROLE_FINANCIAL_ADMINISTRATORS_ID = 3;

        public const int Role_Type_TimeKeeping_Id = 2;

        public const int Role_Type_TraningManager_Id = 13;
        public const int Role_Type_QAManager_Id = 14;

        #endregion

        #region Role TimeKeeping

        public const int Role_TimeKeepingManager = 9;
        public const int Role_TimeKeepingLeader = 4;
        public const int Role_TimeKeepingSupperVisor = 5;
        public const int Role_TimeKeepingGroup = 11;
        public const int ROLE_GUEST_ID = 0;

        #endregion

        #region Unit For Contract

        public const int EMPLOYEE_CONTRACT_UNIT_dNGAY = 1;
        public const int EMPLOYEE_CONTRACT_UNIT_PERCENT = 2;
        public const string EMPLOYEE_CONTRACT_UNIT_PERCENT_TEXT = "%";
        public const string EMPLOYEE_CONTRACT_UNIT_dNGAY_TEXT = "đ/ngày";

        public static List<Unit> GetAllUnit()
        {
            var list = new List<Unit>();
            list.Add(new Unit(0, "None"));
            list.Add(new Unit(1, "đ/ngày"));
            list.Add(new Unit(2, "%"));

            return list;
        }

        public static string GetUnitNameById(int Id)
        {
            if (Id == EMPLOYEE_CONTRACT_UNIT_dNGAY)
                return EMPLOYEE_CONTRACT_UNIT_dNGAY_TEXT;
            if (Id == EMPLOYEE_CONTRACT_UNIT_PERCENT)
                return EMPLOYEE_CONTRACT_UNIT_PERCENT_TEXT;
            return "";
        }

        #endregion

        #region WorkdayEmployee

        public const int WorkdayEmployee_DefaultMonth = 5;
        public const double WorkdayEmployee_DefaultValue = -1;

        public const int LEAVE_TYPE_O_BAN_THAN = 1;
        public const int LEAVE_TYPE_O_DAI_NGAY = 2;
        public const int LEAVE_TYPE_THAI_SAN = 3;
        public const int LEAVE_TYPE_TNLD = 4;
        public const int LEAVE_TYPE_F_NAM = 5;
        public const int LEAVE_TYPE_FDB = 6;
        public const int LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO = 7;
        public const int LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO = 8;
        public const int LEAVE_TYPE_F_DI_DUONG = 9;
        public const int LEAVE_TYPE_F_CONG_TAC = 10;
        public const int LEAVE_TYPE_HOC_1 = 11;
        public const int LEAVE_TYPE_HOC_2 = 12;
        public const int LEAVE_TYPE_HOC_3 = 13;
        public const int LEAVE_TYPE_HOC_4 = 14;
        public const int LEAVE_TYPE_HOC_5 = 15;
        public const int LEAVE_TYPE_HOC_6 = 16;
        public const int LEAVE_TYPE_HOC_7 = 17;
        public const int LEAVE_TYPE_CON_OM = 18;
        public const int LEAVE_TYPE_KHHDS = 19;
        public const int LEAVE_TYPE_SAY_THAI = 20;
        public const int LEAVE_TYPE_KHAM_THAI = 21;
        public const int LEAVE_TYPE_CON_CHET_SAU_KHI_SINH = 22;
        public const int LEAVE_TYPE_DINH_CHI_CONG_TAC = 23;
        public const int LEAVE_TYPE_TAM_HOAN_HOP_DONG = 24;
        public const int LEAVE_TYPE_HOI_HOP = 25;
        public const int LEAVE_TYPE_LE_TET = 26;
        public const int LEAVE_TYPE_NGHI_BU = 27;
        public const int LEAVE_TYPE_NGHI_TUAN = 28;
        public const int LEAVE_TYPE_X = 29;
        public const int LEAVE_TYPE_1_2_X = 30;
        public const int LEAVE_TYPE_NGHI_VIEC = 31;
        public const int LEAVE_TYPE_CHUA_DI_LAM = 32;
        public const int LEAVE_TYPE_HOC_SAGS = 33;

        public const int LEAVE_TYPE_O_DAI_NGAY_HUONG_BHXH = 34;
        public const int LEAVE_TYPE_NUOI_CON_SO_SINH = 35;
        public const int LEAVE_TYPE_NGHI_MAT = 36;

        public const string LEAVE_TYPE_O_BAN_THAN_CODE = "O";
        public const string LEAVE_TYPE_O_DAI_NGAY_CODE = "OD";
        public const string LEAVE_TYPE_THAI_SAN_CODE = "TS";
        public const string LEAVE_TYPE_TNLD_CODE = "TNLD";
        public const string LEAVE_TYPE_F_NAM_CODE = "F";
        public const string LEAVE_TYPE_FDB_CODE = "Fdb";
        public const string LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO_CODE = "Ro";
        public const string LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO_CODE = "Ko";
        public const string LEAVE_TYPE_F_DI_DUONG_CODE = "DD";
        public const string LEAVE_TYPE_F_CONG_TAC_CODE = "CT";
        public const string LEAVE_TYPE_HOC_1_CODE = "H1";
        public const string LEAVE_TYPE_HOC_2_CODE = "H2";
        public const string LEAVE_TYPE_HOC_3_CODE = "H3";
        public const string LEAVE_TYPE_HOC_4_CODE = "H4";
        public const string LEAVE_TYPE_HOC_5_CODE = "H5";
        public const string LEAVE_TYPE_HOC_6_CODE = "H6";
        public const string LEAVE_TYPE_HOC_7_CODE = "H7";
        public const string LEAVE_TYPE_CON_OM_CODE = "Co";
        public const string LEAVE_TYPE_KHHDS_CODE = "KHH";
        public const string LEAVE_TYPE_SAY_THAI_CODE = "ST";
        public const string LEAVE_TYPE_KHAM_THAI_CODE = "KT";
        public const string LEAVE_TYPE_CON_CHET_SAU_KHI_SINH_CODE = "CC";
        public const string LEAVE_TYPE_DINH_CHI_CONG_TAC_CODE = "DC";
        public const string LEAVE_TYPE_TAM_HOAN_HOP_DONG_CODE = "NK";
        public const string LEAVE_TYPE_HOI_HOP_CODE = "HH";
        public const string LEAVE_TYPE_LE_TET_CODE = "LE";
        public const string LEAVE_TYPE_NGHI_BU_CODE = "NB";
        public const string LEAVE_TYPE_NGHI_TUAN_CODE = "NT";
        public const string LEAVE_TYPE_X_CODE = "X";
        public const string LEAVE_TYPE_1_2_X_CODE = "1/2X";
        public const string LEAVE_TYPE_NGHI_VIEC_CODE = "NV";
        public const string LEAVE_TYPE_CHUA_DI_LAM_CODE = "-";
        public const string LEAVE_TYPE_HOC_SAGS_CODE = "Ho";

        public const string LEAVE_TYPE_O_DAI_NGAY_HUONG_BHXH_CODE = "OBH";
        public const string LEAVE_TYPE_NUOI_CON_SO_SINH_CODE = "CN";
        public const string LEAVE_TYPE_NGHI_MAT_CODE = "NM";

        public const string LEAVE_TYPE_O_BAN_THAN_NAME = "Nghỉ ốm";
        public const string LEAVE_TYPE_O_DAI_NGAY_NAME = "Nghỉ ốm dài ngày";
        public const string LEAVE_TYPE_THAI_SAN_NAME = "Nghỉ sinh con";
        public const string LEAVE_TYPE_TNLD_NAME = "Nghỉ do tai nạn lao động";
        public const string LEAVE_TYPE_F_NAM_NAME = "Nghỉ phép";
        public const string LEAVE_TYPE_FDB_NAME = "Nghỉ việc riêng hưởng nguyên lương";
        public const string LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO_NAME = "Nghỉ không lương";
        public const string LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO_NAME = "Nghỉ không lý do";
        public const string LEAVE_TYPE_F_DI_DUONG_NAME = "Nghỉ đi đường";
        public const string LEAVE_TYPE_F_CONG_TAC_NAME = "Nghỉ công tác";
        public const string LEAVE_TYPE_HOC_1_NAME = "Nghỉ đi học nhóm 1";
        public const string LEAVE_TYPE_HOC_2_NAME = "Nghỉ đi học nhóm 2";
        public const string LEAVE_TYPE_HOC_3_NAME = "Nghỉ đi học nhóm 3";
        public const string LEAVE_TYPE_HOC_4_NAME = "Nghỉ đi học nhóm 4";
        public const string LEAVE_TYPE_HOC_5_NAME = "Nghỉ đi học nhóm 5";
        public const string LEAVE_TYPE_HOC_6_NAME = "Nghỉ đi học nhóm 6";
        public const string LEAVE_TYPE_HOC_7_NAME = "Nghỉ đi học nhóm 7";
        public const string LEAVE_TYPE_CON_OM_NAME = "Nghỉ con ốm ";
        public const string LEAVE_TYPE_KHHDS_NAME = "Nghỉ thực hiện KHH dân số";
        public const string LEAVE_TYPE_SAY_THAI_NAME = "Nghỉ do sẩy thai";
        public const string LEAVE_TYPE_KHAM_THAI_NAME = "Nghỉ do khám thai";
        public const string LEAVE_TYPE_CON_CHET_SAU_KHI_SINH_NAME = "Nghỉ do con chết sau khi sinh";
        public const string LEAVE_TYPE_DINH_CHI_CONG_TAC_NAME = "Đình chỉ công tác";
        public const string LEAVE_TYPE_TAM_HOAN_HOP_DONG_NAME = "Nghỉ do ngừng việc, hoãn HĐLĐ";
        public const string LEAVE_TYPE_HOI_HOP_NAME = "Nghỉ hội họp";
        public const string LEAVE_TYPE_LE_TET_NAME = "Nghỉ lễ tết";
        public const string LEAVE_TYPE_NGHI_BU_NAME = "Nghỉ bù";
        public const string LEAVE_TYPE_NGHI_TUAN_NAME = "Nghỉ tuần";
        public const string LEAVE_TYPE_X_NAME = "Ngày làm việc 8 giờ/ngày";
        public const string LEAVE_TYPE_1_2_X_NAME = "Ngày làm việc 4 giờ/ngày";
        public const string LEAVE_TYPE_NGHI_VIEC_NAME = "Nghỉ việc";
        public const string LEAVE_TYPE_CHUA_DI_LAM_NAME = "Không chấm công";
        public const string LEAVE_TYPE_HOC_SAGS_NAME = "Học hưởng nguyên lương như X";

        public const string LEAVE_TYPE_O_DAI_NGAY_HUONG_BHXH_NAME = "Ốm dài ngày hưởng BHXH";
        public const string LEAVE_TYPE_NUOI_CON_SO_SINH_NAME = "Nghỉ nuôi con, nuôi sơ sinh";
        public const string LEAVE_TYPE_NGHI_MAT_NAME = "Nghỉ mát";

        public static string GetSymbolTimekeeping(int leaveType)
        {
            var valueReturn = string.Empty;

            switch (leaveType)
            {
                case LEAVE_TYPE_O_BAN_THAN:
                    valueReturn = LEAVE_TYPE_O_BAN_THAN_CODE;
                    break;
                case LEAVE_TYPE_O_DAI_NGAY:
                    valueReturn = LEAVE_TYPE_O_DAI_NGAY_CODE;
                    break;
                case LEAVE_TYPE_THAI_SAN:
                    valueReturn = LEAVE_TYPE_THAI_SAN_CODE;
                    break;
                case LEAVE_TYPE_TNLD:
                    valueReturn = LEAVE_TYPE_TNLD_CODE;
                    break;
                case LEAVE_TYPE_F_NAM:
                    valueReturn = LEAVE_TYPE_F_NAM_CODE;
                    break;
                case LEAVE_TYPE_FDB:
                    valueReturn = LEAVE_TYPE_FDB_CODE;
                    break;
                case LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO:
                    valueReturn = LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO_CODE;
                    break;
                case LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO:
                    valueReturn = LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO_CODE;
                    break;
                case LEAVE_TYPE_F_DI_DUONG:
                    valueReturn = LEAVE_TYPE_F_DI_DUONG_CODE;
                    break;
                case LEAVE_TYPE_F_CONG_TAC:
                    valueReturn = LEAVE_TYPE_F_CONG_TAC_CODE;
                    break;
                case LEAVE_TYPE_HOC_1:
                    valueReturn = LEAVE_TYPE_HOC_1_CODE;
                    break;
                case LEAVE_TYPE_HOC_2:
                    valueReturn = LEAVE_TYPE_HOC_2_CODE;
                    break;
                case LEAVE_TYPE_HOC_3:
                    valueReturn = LEAVE_TYPE_HOC_3_CODE;
                    break;
                case LEAVE_TYPE_HOC_4:
                    valueReturn = LEAVE_TYPE_HOC_4_CODE;
                    break;
                case LEAVE_TYPE_HOC_5:
                    valueReturn = LEAVE_TYPE_HOC_5_CODE;
                    break;
                case LEAVE_TYPE_HOC_6:
                    valueReturn = LEAVE_TYPE_HOC_6_CODE;
                    break;
                case LEAVE_TYPE_HOC_7:
                    valueReturn = LEAVE_TYPE_HOC_7_CODE;
                    break;
                case LEAVE_TYPE_CON_OM:
                    valueReturn = LEAVE_TYPE_CON_OM_CODE;
                    break;
                case LEAVE_TYPE_KHHDS:
                    valueReturn = LEAVE_TYPE_KHHDS_CODE;
                    break;
                case LEAVE_TYPE_SAY_THAI:
                    valueReturn = LEAVE_TYPE_SAY_THAI_CODE;
                    break;
                case LEAVE_TYPE_KHAM_THAI:
                    valueReturn = LEAVE_TYPE_KHAM_THAI_CODE;
                    break;
                case LEAVE_TYPE_CON_CHET_SAU_KHI_SINH:
                    valueReturn = LEAVE_TYPE_CON_CHET_SAU_KHI_SINH_CODE;
                    break;
                case LEAVE_TYPE_DINH_CHI_CONG_TAC:
                    valueReturn = LEAVE_TYPE_DINH_CHI_CONG_TAC_CODE;
                    break;
                case LEAVE_TYPE_TAM_HOAN_HOP_DONG:
                    valueReturn = LEAVE_TYPE_TAM_HOAN_HOP_DONG_CODE;
                    break;
                case LEAVE_TYPE_HOI_HOP:
                    valueReturn = LEAVE_TYPE_HOI_HOP_CODE;
                    break;
                case LEAVE_TYPE_LE_TET:
                    valueReturn = LEAVE_TYPE_LE_TET_CODE;
                    break;
                case LEAVE_TYPE_NGHI_BU:
                    valueReturn = LEAVE_TYPE_NGHI_BU_CODE;
                    break;
                case LEAVE_TYPE_NGHI_TUAN:
                    valueReturn = LEAVE_TYPE_NGHI_TUAN_CODE;
                    break;
                case LEAVE_TYPE_X:
                    valueReturn = LEAVE_TYPE_X_CODE;
                    break;
                case LEAVE_TYPE_1_2_X:
                    valueReturn = LEAVE_TYPE_1_2_X_CODE;
                    break;
                case LEAVE_TYPE_NGHI_VIEC:
                    valueReturn = LEAVE_TYPE_NGHI_VIEC_CODE;
                    break;
                case LEAVE_TYPE_CHUA_DI_LAM:
                    valueReturn = LEAVE_TYPE_CHUA_DI_LAM_CODE;
                    break;
                case LEAVE_TYPE_HOC_SAGS:
                    valueReturn = LEAVE_TYPE_HOC_SAGS_CODE;
                    break;
                case LEAVE_TYPE_O_DAI_NGAY_HUONG_BHXH:
                    valueReturn = LEAVE_TYPE_O_DAI_NGAY_HUONG_BHXH_CODE;
                    break;
                case LEAVE_TYPE_NUOI_CON_SO_SINH:
                    valueReturn = LEAVE_TYPE_NUOI_CON_SO_SINH_CODE;
                    break;
                case LEAVE_TYPE_NGHI_MAT:
                    valueReturn = LEAVE_TYPE_NGHI_MAT_CODE;
                    break;
            }

            return valueReturn;
        }

        public static string GetTimekeepingName(string leaveTypeCode)
        {
            var valueReturn = string.Empty;

            switch (leaveTypeCode)
            {
                case LEAVE_TYPE_O_BAN_THAN_CODE:
                    valueReturn = LEAVE_TYPE_O_BAN_THAN_NAME;
                    break;
                case LEAVE_TYPE_O_DAI_NGAY_CODE:
                    valueReturn = LEAVE_TYPE_O_DAI_NGAY_NAME;
                    break;
                case LEAVE_TYPE_THAI_SAN_CODE:
                    valueReturn = LEAVE_TYPE_THAI_SAN_NAME;
                    break;
                case LEAVE_TYPE_TNLD_CODE:
                    valueReturn = LEAVE_TYPE_TNLD_NAME;
                    break;
                case LEAVE_TYPE_F_NAM_CODE:
                    valueReturn = LEAVE_TYPE_F_NAM_NAME;
                    break;
                case LEAVE_TYPE_FDB_CODE:
                    valueReturn = LEAVE_TYPE_FDB_NAME;
                    break;
                case LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO_CODE:
                    valueReturn = LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO_NAME;
                    break;
                case LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO_CODE:
                    valueReturn = LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO_NAME;
                    break;
                case LEAVE_TYPE_F_DI_DUONG_CODE:
                    valueReturn = LEAVE_TYPE_F_DI_DUONG_NAME;
                    break;
                case LEAVE_TYPE_F_CONG_TAC_CODE:
                    valueReturn = LEAVE_TYPE_F_CONG_TAC_NAME;
                    break;
                case LEAVE_TYPE_HOC_1_CODE:
                    valueReturn = LEAVE_TYPE_HOC_1_NAME;
                    break;
                case LEAVE_TYPE_HOC_2_CODE:
                    valueReturn = LEAVE_TYPE_HOC_2_NAME;
                    break;
                case LEAVE_TYPE_HOC_3_CODE:
                    valueReturn = LEAVE_TYPE_HOC_3_NAME;
                    break;
                case LEAVE_TYPE_HOC_4_CODE:
                    valueReturn = LEAVE_TYPE_HOC_4_NAME;
                    break;
                case LEAVE_TYPE_HOC_5_CODE:
                    valueReturn = LEAVE_TYPE_HOC_5_NAME;
                    break;
                case LEAVE_TYPE_HOC_6_CODE:
                    valueReturn = LEAVE_TYPE_HOC_6_NAME;
                    break;
                case LEAVE_TYPE_HOC_7_CODE:
                    valueReturn = LEAVE_TYPE_HOC_7_NAME;
                    break;
                case LEAVE_TYPE_CON_OM_CODE:
                    valueReturn = LEAVE_TYPE_CON_OM_NAME;
                    break;
                case LEAVE_TYPE_KHHDS_CODE:
                    valueReturn = LEAVE_TYPE_KHHDS_NAME;
                    break;
                case LEAVE_TYPE_SAY_THAI_CODE:
                    valueReturn = LEAVE_TYPE_SAY_THAI_NAME;
                    break;
                case LEAVE_TYPE_KHAM_THAI_CODE:
                    valueReturn = LEAVE_TYPE_KHAM_THAI_NAME;
                    break;
                case LEAVE_TYPE_CON_CHET_SAU_KHI_SINH_CODE:
                    valueReturn = LEAVE_TYPE_CON_CHET_SAU_KHI_SINH_NAME;
                    break;
                case LEAVE_TYPE_DINH_CHI_CONG_TAC_CODE:
                    valueReturn = LEAVE_TYPE_DINH_CHI_CONG_TAC_NAME;
                    break;
                case LEAVE_TYPE_TAM_HOAN_HOP_DONG_CODE:
                    valueReturn = LEAVE_TYPE_TAM_HOAN_HOP_DONG_NAME;
                    break;
                case LEAVE_TYPE_HOI_HOP_CODE:
                    valueReturn = LEAVE_TYPE_HOI_HOP_NAME;
                    break;
                case LEAVE_TYPE_LE_TET_CODE:
                    valueReturn = LEAVE_TYPE_LE_TET_NAME;
                    break;
                case LEAVE_TYPE_NGHI_BU_CODE:
                    valueReturn = LEAVE_TYPE_NGHI_BU_NAME;
                    break;
                case LEAVE_TYPE_NGHI_TUAN_CODE:
                    valueReturn = LEAVE_TYPE_NGHI_TUAN_NAME;
                    break;
                case LEAVE_TYPE_X_CODE:
                    valueReturn = LEAVE_TYPE_X_NAME;
                    break;
                case LEAVE_TYPE_1_2_X_CODE:
                    valueReturn = LEAVE_TYPE_1_2_X_NAME;
                    break;
                case LEAVE_TYPE_NGHI_VIEC_CODE:
                    valueReturn = LEAVE_TYPE_NGHI_VIEC_NAME;
                    break;
                case LEAVE_TYPE_CHUA_DI_LAM_CODE:
                    valueReturn = LEAVE_TYPE_CHUA_DI_LAM_NAME;
                    break;
                case LEAVE_TYPE_HOC_SAGS_CODE:
                    valueReturn = LEAVE_TYPE_HOC_SAGS_NAME;
                    break;
                case LEAVE_TYPE_O_DAI_NGAY_HUONG_BHXH_CODE:
                    valueReturn = LEAVE_TYPE_O_DAI_NGAY_HUONG_BHXH_NAME;
                    break;
                case LEAVE_TYPE_NUOI_CON_SO_SINH_CODE:
                    valueReturn = LEAVE_TYPE_NUOI_CON_SO_SINH_NAME;
                    break;
                case LEAVE_TYPE_NGHI_MAT_CODE:
                    valueReturn = LEAVE_TYPE_NGHI_MAT_NAME;
                    break;
            }

            return valueReturn;
        }

        public static bool CheckCode(string leaveTypeCode)
        {
            var valueReturn = false;

            switch (leaveTypeCode)
            {
                case LEAVE_TYPE_O_BAN_THAN_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_O_DAI_NGAY_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_THAI_SAN_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_TNLD_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_F_NAM_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_FDB_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_F_DI_DUONG_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_F_CONG_TAC_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_HOC_1_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_HOC_2_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_HOC_3_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_HOC_4_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_HOC_5_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_HOC_6_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_HOC_7_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_CON_OM_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_KHHDS_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_SAY_THAI_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_KHAM_THAI_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_CON_CHET_SAU_KHI_SINH_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_DINH_CHI_CONG_TAC_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_TAM_HOAN_HOP_DONG_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_HOI_HOP_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_LE_TET_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_NGHI_BU_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_NGHI_TUAN_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_X_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_1_2_X_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_NGHI_VIEC_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_CHUA_DI_LAM_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_HOC_SAGS_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_O_DAI_NGAY_HUONG_BHXH_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_NUOI_CON_SO_SINH_CODE:
                    valueReturn = true;
                    break;
                case LEAVE_TYPE_NGHI_MAT_CODE:
                    valueReturn = true;
                    break;
            }

            return valueReturn;
        }

        public static string GetTimekeepingName1(string leaveTypeCode)
        {
            var valueReturn = string.Empty;
            var arr = leaveTypeCode.Split('+');
            if (arr.Length > 1)
            {
                if (arr[0].Substring(3, 1) == "X")
                    valueReturn = GetTimekeepingName(arr[0]);
                else
                    valueReturn = GetTimekeepingName(arr[0].Substring(3, arr[0].Length - 3)) + "(4 giờ/ngày)";
                if (arr[1].Substring(3, 1) == "X")
                    valueReturn += "<br>" + GetTimekeepingName(arr[1]);
                else
                    valueReturn += "<br>" + GetTimekeepingName(arr[1].Substring(3, arr[1].Length - 3)) + "(4 giờ/ngày)";
            }
            else
            {
                switch (leaveTypeCode)
                {
                    case LEAVE_TYPE_O_BAN_THAN_CODE:
                        valueReturn = LEAVE_TYPE_O_BAN_THAN_NAME;
                        break;
                    case LEAVE_TYPE_O_DAI_NGAY_CODE:
                        valueReturn = LEAVE_TYPE_O_DAI_NGAY_NAME;
                        break;
                    case LEAVE_TYPE_THAI_SAN_CODE:
                        valueReturn = LEAVE_TYPE_THAI_SAN_NAME;
                        break;
                    case LEAVE_TYPE_TNLD_CODE:
                        valueReturn = LEAVE_TYPE_TNLD_NAME;
                        break;
                    case LEAVE_TYPE_F_NAM_CODE:
                        valueReturn = LEAVE_TYPE_F_NAM_NAME;
                        break;
                    case LEAVE_TYPE_FDB_CODE:
                        valueReturn = LEAVE_TYPE_FDB_NAME;
                        break;
                    case LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO_CODE:
                        valueReturn = LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO_NAME;
                        break;
                    case LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO_CODE:
                        valueReturn = LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO_NAME;
                        break;
                    case LEAVE_TYPE_F_DI_DUONG_CODE:
                        valueReturn = LEAVE_TYPE_F_DI_DUONG_NAME;
                        break;
                    case LEAVE_TYPE_F_CONG_TAC_CODE:
                        valueReturn = LEAVE_TYPE_F_CONG_TAC_NAME;
                        break;
                    case LEAVE_TYPE_HOC_1_CODE:
                        valueReturn = LEAVE_TYPE_HOC_1_NAME;
                        break;
                    case LEAVE_TYPE_HOC_2_CODE:
                        valueReturn = LEAVE_TYPE_HOC_2_NAME;
                        break;
                    case LEAVE_TYPE_HOC_3_CODE:
                        valueReturn = LEAVE_TYPE_HOC_3_NAME;
                        break;
                    case LEAVE_TYPE_HOC_4_CODE:
                        valueReturn = LEAVE_TYPE_HOC_4_NAME;
                        break;
                    case LEAVE_TYPE_HOC_5_CODE:
                        valueReturn = LEAVE_TYPE_HOC_5_NAME;
                        break;
                    case LEAVE_TYPE_HOC_6_CODE:
                        valueReturn = LEAVE_TYPE_HOC_6_NAME;
                        break;
                    case LEAVE_TYPE_HOC_7_CODE:
                        valueReturn = LEAVE_TYPE_HOC_7_NAME;
                        break;
                    case LEAVE_TYPE_CON_OM_CODE:
                        valueReturn = LEAVE_TYPE_CON_OM_NAME;
                        break;
                    case LEAVE_TYPE_KHHDS_CODE:
                        valueReturn = LEAVE_TYPE_KHHDS_NAME;
                        break;
                    case LEAVE_TYPE_SAY_THAI_CODE:
                        valueReturn = LEAVE_TYPE_SAY_THAI_NAME;
                        break;
                    case LEAVE_TYPE_KHAM_THAI_CODE:
                        valueReturn = LEAVE_TYPE_KHAM_THAI_NAME;
                        break;
                    case LEAVE_TYPE_CON_CHET_SAU_KHI_SINH_CODE:
                        valueReturn = LEAVE_TYPE_CON_CHET_SAU_KHI_SINH_NAME;
                        break;
                    case LEAVE_TYPE_DINH_CHI_CONG_TAC_CODE:
                        valueReturn = LEAVE_TYPE_DINH_CHI_CONG_TAC_NAME;
                        break;
                    case LEAVE_TYPE_TAM_HOAN_HOP_DONG_CODE:
                        valueReturn = LEAVE_TYPE_TAM_HOAN_HOP_DONG_NAME;
                        break;
                    case LEAVE_TYPE_HOI_HOP_CODE:
                        valueReturn = LEAVE_TYPE_HOI_HOP_NAME;
                        break;
                    case LEAVE_TYPE_LE_TET_CODE:
                        valueReturn = LEAVE_TYPE_LE_TET_NAME;
                        break;
                    case LEAVE_TYPE_NGHI_BU_CODE:
                        valueReturn = LEAVE_TYPE_NGHI_BU_NAME;
                        break;
                    case LEAVE_TYPE_NGHI_TUAN_CODE:
                        valueReturn = LEAVE_TYPE_NGHI_TUAN_NAME;
                        break;
                    case LEAVE_TYPE_X_CODE:
                        valueReturn = LEAVE_TYPE_X_NAME;
                        break;
                    case LEAVE_TYPE_1_2_X_CODE:
                        valueReturn = LEAVE_TYPE_1_2_X_NAME;
                        break;
                    case LEAVE_TYPE_NGHI_VIEC_CODE:
                        valueReturn = LEAVE_TYPE_NGHI_VIEC_NAME;
                        break;
                    case LEAVE_TYPE_CHUA_DI_LAM_CODE:
                        valueReturn = LEAVE_TYPE_CHUA_DI_LAM_NAME;
                        break;
                    case LEAVE_TYPE_HOC_SAGS_CODE:
                        valueReturn = LEAVE_TYPE_HOC_SAGS_NAME;
                        break;
                    case LEAVE_TYPE_O_DAI_NGAY_HUONG_BHXH_CODE:
                        valueReturn = LEAVE_TYPE_O_DAI_NGAY_HUONG_BHXH_NAME;
                        break;
                    case LEAVE_TYPE_NUOI_CON_SO_SINH_CODE:
                        valueReturn = LEAVE_TYPE_NUOI_CON_SO_SINH_NAME;
                        break;
                    case LEAVE_TYPE_NGHI_MAT_CODE:
                        valueReturn = LEAVE_TYPE_NGHI_MAT_NAME;
                        break;
                }
            }
            return valueReturn;
        }

        public static List<RType> GetAllLeaveCode()
        {
            var list = new List<RType>();

            list.Add(new RType(0, "<All>"));
            list.Add(new RType(LEAVE_TYPE_O_BAN_THAN, LEAVE_TYPE_O_BAN_THAN_CODE));
            list.Add(new RType(LEAVE_TYPE_O_DAI_NGAY, LEAVE_TYPE_O_DAI_NGAY_CODE));
            list.Add(new RType(LEAVE_TYPE_THAI_SAN, LEAVE_TYPE_THAI_SAN_CODE));
            list.Add(new RType(LEAVE_TYPE_TNLD, LEAVE_TYPE_TNLD_CODE));
            list.Add(new RType(LEAVE_TYPE_F_NAM, LEAVE_TYPE_F_NAM_CODE));
            list.Add(new RType(LEAVE_TYPE_FDB, LEAVE_TYPE_FDB_CODE));
            list.Add(new RType(LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO, LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO_CODE));
            list.Add(new RType(LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO, LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO_CODE));
            list.Add(new RType(LEAVE_TYPE_F_DI_DUONG, LEAVE_TYPE_F_DI_DUONG_CODE));
            list.Add(new RType(LEAVE_TYPE_F_CONG_TAC, LEAVE_TYPE_F_CONG_TAC_CODE));
            list.Add(new RType(LEAVE_TYPE_HOC_1, LEAVE_TYPE_HOC_1_CODE));
            list.Add(new RType(LEAVE_TYPE_HOC_2, LEAVE_TYPE_HOC_2_CODE));
            list.Add(new RType(LEAVE_TYPE_HOC_3, LEAVE_TYPE_HOC_3_CODE));
            list.Add(new RType(LEAVE_TYPE_HOC_4, LEAVE_TYPE_HOC_4_CODE));
            list.Add(new RType(LEAVE_TYPE_HOC_5, LEAVE_TYPE_HOC_5_CODE));
            list.Add(new RType(LEAVE_TYPE_HOC_6, LEAVE_TYPE_HOC_6_CODE));
            list.Add(new RType(LEAVE_TYPE_HOC_7, LEAVE_TYPE_HOC_7_CODE));
            list.Add(new RType(LEAVE_TYPE_CON_OM, LEAVE_TYPE_CON_OM_CODE));
            list.Add(new RType(LEAVE_TYPE_KHHDS, LEAVE_TYPE_KHHDS_CODE));
            list.Add(new RType(LEAVE_TYPE_SAY_THAI, LEAVE_TYPE_SAY_THAI_CODE));
            list.Add(new RType(LEAVE_TYPE_KHAM_THAI, LEAVE_TYPE_KHAM_THAI_CODE));
            list.Add(new RType(LEAVE_TYPE_CON_CHET_SAU_KHI_SINH, LEAVE_TYPE_CON_CHET_SAU_KHI_SINH_CODE));
            list.Add(new RType(LEAVE_TYPE_DINH_CHI_CONG_TAC, LEAVE_TYPE_DINH_CHI_CONG_TAC_CODE));
            list.Add(new RType(LEAVE_TYPE_TAM_HOAN_HOP_DONG, LEAVE_TYPE_TAM_HOAN_HOP_DONG_CODE));
            list.Add(new RType(LEAVE_TYPE_HOI_HOP, LEAVE_TYPE_HOI_HOP_CODE));
            list.Add(new RType(LEAVE_TYPE_LE_TET, LEAVE_TYPE_LE_TET_CODE));
            list.Add(new RType(LEAVE_TYPE_NGHI_BU, LEAVE_TYPE_NGHI_BU_CODE));
            list.Add(new RType(LEAVE_TYPE_NGHI_TUAN, LEAVE_TYPE_NGHI_TUAN_CODE));
            list.Add(new RType(LEAVE_TYPE_X, LEAVE_TYPE_X_CODE));
            list.Add(new RType(LEAVE_TYPE_1_2_X, LEAVE_TYPE_1_2_X_CODE));
            list.Add(new RType(LEAVE_TYPE_NGHI_VIEC, LEAVE_TYPE_NGHI_VIEC_CODE));
            list.Add(new RType(LEAVE_TYPE_CHUA_DI_LAM, LEAVE_TYPE_CHUA_DI_LAM_CODE));
            list.Add(new RType(LEAVE_TYPE_HOC_SAGS, LEAVE_TYPE_HOC_SAGS_CODE));
            list.Add(new RType(LEAVE_TYPE_O_DAI_NGAY_HUONG_BHXH, LEAVE_TYPE_O_DAI_NGAY_HUONG_BHXH_CODE));
            list.Add(new RType(LEAVE_TYPE_NUOI_CON_SO_SINH, LEAVE_TYPE_NUOI_CON_SO_SINH_CODE));
            list.Add(new RType(LEAVE_TYPE_NGHI_MAT, LEAVE_TYPE_NGHI_MAT_CODE));

            return list;
        }

        public const int TimeKeepingType_NghiKhac_Id = 3;
        public const int TimeKeepingType_NgayCongLamViec_Id = 4;
        public const int TimeKeepingType_NghiPhepGianDoan_Id = 2;
        public const int TimeKeepingType_NghiPhepLienTuc_Id = 1;

        public const int TimeKeepingType_Init_OfficeHours_Id = 1;
        public const int TimeKeepingType_Init_Shift_Id = 2;

        public static List<RType> GetTimeKeepingType()
        {
            var list = new List<RType>();

            list.Add(new RType(0, ""));
            list.Add(new RType(TimeKeepingType_NghiKhac_Id, "Nghỉ khác"));
            list.Add(new RType(TimeKeepingType_NgayCongLamViec_Id, "Ngày công làm việc"));
            list.Add(new RType(TimeKeepingType_NghiPhepGianDoan_Id, "Nghỉ phép gián đoạn"));
            list.Add(new RType(TimeKeepingType_NghiPhepLienTuc_Id, "Nghỉ phép liên tục"));

            return list;
        }

        public const int TimeKeepingType_NuaGio_X_Id = 1;
        public const int TimeKeepingType_NuaGio_NT_Id = 2;
        public const int TimeKeepingType_NuaGio_NB_Id = 3;

        public static List<RType> GetTimeKeepingTypeNuaGio(bool showX)
        {
            var list = new List<RType>();

            list.Add(new RType(0, ""));
            if (showX)
                list.Add(new RType(TimeKeepingType_NuaGio_X_Id, "1/2...+1/2X)"));
            list.Add(new RType(TimeKeepingType_NuaGio_NT_Id, "1/2...+1/2NT)"));
            list.Add(new RType(TimeKeepingType_NuaGio_NB_Id, "1/2...+1/2NB)"));

            return list;
        }

        public const int WorkdayEmployees_Status_TimeKeeping_Yes = 1;
        public const int WorkdayEmployees_Status_TimeKeeping_No = 0;

        #endregion

        #region WorkdayPrivilege

        public const int WorkdayPrivilege_TimeKeeping = 1;
        public const int WorkdayPrivilege_CV = 2;

        #endregion

        #region ScaleOfSalary

        public const int ScaleOfSalary_Value1_Id = 1;
        public const int ScaleOfSalary_Value2_Id = 2;
        public const int ScaleOfSalary_Value3_Id = 3;

        public const string ScaleOfSalary_Value1_Name = "Mức 1";
        public const string ScaleOfSalary_Value2_Name = "Mức 2";
        public const string ScaleOfSalary_Value3_Name = "Mức 3";

        public static List<Unit> GetScaleOfSalary_Level()
        {
            var list = new List<Unit>();

            list.Add(new Unit(ScaleOfSalary_Value1_Id, ScaleOfSalary_Value1_Name));
            list.Add(new Unit(ScaleOfSalary_Value2_Id, ScaleOfSalary_Value2_Name));
            list.Add(new Unit(ScaleOfSalary_Value3_Id, ScaleOfSalary_Value3_Name));

            return list;
        }

        #endregion

        #region HTCV

        /// <summary>
        ///     Danh muc cac nhom bang tieu chi danh gia muc do hoan thanh cong viec
        /// </summary>
        public const int HTCVCatalogueType_CustomerServices_BaggageHandling_Cargo_ServiceCharge_Id = 1;

        public const int HTCVCatalogueType_Sanitation_OddJob_DecorativePlants_Id = 2;
        public const int HTCVCatalogueType_RampHandling_Id = 3;
        public const int HTCVCatalogueType_Technical_Id = 4;
        public const int HTCVCatalogueType_Section_Group_Manager_Id = 5;
        public const int HTCVCatalogueType_Mediate_Id = 6;
        public const int HTCVCatalogueType_FlightingOperation_Id = 7;

        public const string HTCVCatalogueType_CustomerServices_BaggageHandling_Cargo_ServiceCharge_Name =
            "Nhân viên PVHK, Hành Lý, Hàng Hóa và Thu Dịch Vụ";

        public const string HTCVCatalogueType_Sanitation_OddJob_DecorativePlants_Name =
            "Nhân viên Vệ Sinh-Tạp Vụ-Cây Cảnh";

        public const string HTCVCatalogueType_RampHandling_Name = "Nhân viên Lái Xe Vận Hành Trang Thiết Bị";
        public const string HTCVCatalogueType_Technical_Name = "Nhân viên Kỹ Thuật";
        public const string HTCVCatalogueType_Section_Group_Manager_Name = "Cán Bộ Cấp Tổ, Đội hoặc Tương Đương";
        public const string HTCVCatalogueType_Mediate_Name = "Nhóm Lao Động Gián Tiếp";
        public const string HTCVCatalogueType_FlightingOperation_Name = "Nhóm Nhân Viên Phục Vụ Hoạt Động Bay";

        public static List<RType> GetAllHTCVCatalogueType()
        {
            var list = new List<RType>();

            list.Add(new RType(HTCVCatalogueType_CustomerServices_BaggageHandling_Cargo_ServiceCharge_Id,
                HTCVCatalogueType_CustomerServices_BaggageHandling_Cargo_ServiceCharge_Name));
            list.Add(new RType(HTCVCatalogueType_Sanitation_OddJob_DecorativePlants_Id,
                HTCVCatalogueType_Sanitation_OddJob_DecorativePlants_Name));
            list.Add(new RType(HTCVCatalogueType_RampHandling_Id, HTCVCatalogueType_RampHandling_Name));
            list.Add(new RType(HTCVCatalogueType_Technical_Id, HTCVCatalogueType_Technical_Name));
            list.Add(new RType(HTCVCatalogueType_Section_Group_Manager_Id, HTCVCatalogueType_Section_Group_Manager_Name));
            list.Add(new RType(HTCVCatalogueType_Mediate_Id, HTCVCatalogueType_Mediate_Name));
            list.Add(new RType(HTCVCatalogueType_FlightingOperation_Id, HTCVCatalogueType_FlightingOperation_Name));

            return list;
        }

        public static string GetHTCVCatalogueTypeName(int Id)
        {
            if (Id == HTCVCatalogueType_CustomerServices_BaggageHandling_Cargo_ServiceCharge_Id)
                return HTCVCatalogueType_CustomerServices_BaggageHandling_Cargo_ServiceCharge_Name;
            if (Id == HTCVCatalogueType_Sanitation_OddJob_DecorativePlants_Id)
                return HTCVCatalogueType_Sanitation_OddJob_DecorativePlants_Name;
            if (Id == HTCVCatalogueType_RampHandling_Id)
                return HTCVCatalogueType_RampHandling_Name;
            if (Id == HTCVCatalogueType_Technical_Id)
                return HTCVCatalogueType_Technical_Name;
            if (Id == HTCVCatalogueType_Section_Group_Manager_Id)
                return HTCVCatalogueType_Section_Group_Manager_Name;
            if (Id == HTCVCatalogueType_Mediate_Id)
                return HTCVCatalogueType_Mediate_Name;
            if (Id == HTCVCatalogueType_FlightingOperation_Id)
                return HTCVCatalogueType_FlightingOperation_Name;
            return "";
        }

        #endregion

        #region CV

        public const string RELATION_CHA_ME_ANH_CHI_EM_NAME = "Cha mẹ, anh, chị, em ruột";
        public const string RELATION_BEN_NOI_NAME = "Bên nội";
        public const string RELATION_BEN_NGOAI_NAME = "Bên ngoại";
        public const string RELATION_BEN_VO_CHONG_NAME = "Bên vợ (chồng)";
        public const string RELATION_NGUOI_THAN_KHAC_NAME = "Những người thân khác";

        public const int RELATION_CHA_ME_ANH_CHI_EM_ID = 0;
        public const int RELATION_BEN_NOI_ID = 1;
        public const int RELATION_BEN_NGOAI_ID = 2;
        public const int RELATION_BEN_VO_CHONG_ID = 3;
        public const int RELATION_NGUOI_THAN_KHAC_ID = 4;

        public static List<RType> GetAllRTypeN()
        {
            var list = new List<RType>();

            list.Add(new RType(-1, "Tất cả"));
            list.Add(new RType(RELATION_CHA_ME_ANH_CHI_EM_ID, RELATION_CHA_ME_ANH_CHI_EM_NAME));
            list.Add(new RType(RELATION_BEN_NOI_ID, RELATION_BEN_NOI_NAME));
            list.Add(new RType(RELATION_BEN_NGOAI_ID, RELATION_BEN_NGOAI_NAME));
            list.Add(new RType(RELATION_BEN_VO_CHONG_ID, RELATION_BEN_VO_CHONG_NAME));
            list.Add(new RType(RELATION_NGUOI_THAN_KHAC_ID, RELATION_NGUOI_THAN_KHAC_NAME));

            return list;
        }

        public static List<RType> GetAllRType()
        {
            var list = new List<RType>();

            list.Add(new RType(RELATION_CHA_ME_ANH_CHI_EM_ID, RELATION_CHA_ME_ANH_CHI_EM_NAME));
            list.Add(new RType(RELATION_BEN_NOI_ID, RELATION_BEN_NOI_NAME));
            list.Add(new RType(RELATION_BEN_NGOAI_ID, RELATION_BEN_NGOAI_NAME));
            list.Add(new RType(RELATION_BEN_VO_CHONG_ID, RELATION_BEN_VO_CHONG_NAME));
            list.Add(new RType(RELATION_NGUOI_THAN_KHAC_ID, RELATION_NGUOI_THAN_KHAC_NAME));
            return list;
        }

        public static string GetRTypeName(int rTypeId)
        {
            var name = string.Empty;
            var list = GetAllRType();
            foreach (var obj in list)
                if (obj.RTypeId == rTypeId)
                    name = obj.RTypeName;
            return name;
        }

        public static List<RType> RELATION_CHA_ME_ANH_CHI_EM()
        {
            var list = new List<RType>();

            list.Add(new RType(-1, "Tất cả"));
            list.Add(new RType(RELATION_CHA_RUOT, RELATION_CHA_RUOT_NAME));
            list.Add(new RType(RELATION_ME_RUOT, RELATION_ME_RUOT_NAME));
            list.Add(new RType(RELATION_ANH_RUOT, RELATION_ANH_RUOT_NAME));
            list.Add(new RType(RELATION_CHI_RUOT, RELATION_CHI_RUOT_NAME));
            list.Add(new RType(RELATION_EM_RUOT, RELATION_EM_RUOT_NAME));
            list.Add(new RType(RELATION_CHAU_RUOT, RELATION_CHAU_RUOT_NAME));

            return list;
        }

        public static List<RType> RELATION_BEN_NOI()
        {
            var list = new List<RType>();

            list.Add(new RType(-1, "Tất cả"));
            list.Add(new RType(RELATION_BA_NOI, RELATION_BA_NOI_NAME));
            list.Add(new RType(RELATION_ONG_NOI, RELATION_ONG_NOI_NAME));
            list.Add(new RType(RELATION_CO_RUOT, RELATION_CO_RUOT_NAME));
            list.Add(new RType(RELATION_CHU_RUOT, RELATION_CHU_RUOT_NAME));
            list.Add(new RType(RELATION_BAC_RUOTT1, RELATION_BAC_RUOTT1_NAME));
            list.Add(new RType(RELATION_DUONGT1, RELATION_DUONGT1_NAME));

            return list;
        }

        public static List<RType> RELATION_BEN_NGOAI()
        {
            var list = new List<RType>();

            list.Add(new RType(-1, "Tất cả"));
            list.Add(new RType(RELATION_ONG_NGOAI, RELATION_ONG_NGOAI_NAME));
            list.Add(new RType(RELATION_BA_NGOAI, RELATION_BA_NGOAI_NAME));
            list.Add(new RType(RELATION_CAU_RUOT, RELATION_CAU_RUOT_NAME));
            list.Add(new RType(RELATION_DI_RUOT, RELATION_DI_RUOT_NAME));
            list.Add(new RType(RELATION_DUONGT2, RELATION_DUONGT2_NAME));
            list.Add(new RType(RELATION_BAC_RUOTT2, RELATION_BAC_RUOTT2_NAME));

            return list;
        }

        public static List<RType> RELATION_BEN_VO_CHONG()
        {
            var list = new List<RType>();

            list.Add(new RType(-1, "Tất cả"));
            list.Add(new RType(RELATION_CHA_VO, RELATION_CHA_VO_NAME));
            list.Add(new RType(RELATION_CHA_CHONG, RELATION_CHA_CHONG_NAME));
            list.Add(new RType(RELATION_ME_CHONG, RELATION_ME_CHONG_NAME));
            list.Add(new RType(RELATION_ME_VO, RELATION_ME_VO_NAME));
            list.Add(new RType(RELATION_VO, RELATION_VO_NAME));
            list.Add(new RType(RELATION_CHONG, RELATION_CHONG_NAME));
            list.Add(new RType(RELATION_CON_RUOT, RELATION_CON_RUOT_NAME));
            list.Add(new RType(RELATION_CHI_VO, RELATION_CHI_VO_NAME));
            list.Add(new RType(RELATION_ANH_VO, RELATION_ANH_VO_NAME));
            list.Add(new RType(RELATION_EM_VO, RELATION_EM_VO_NAME));
            list.Add(new RType(RELATION_BAC_RUOTT2, RELATION_BAC_RUOTT2_NAME));
            list.Add(new RType(RELATION_CHI_CHONG, RELATION_CHI_CHONG_NAME));
            list.Add(new RType(RELATION_ANH_CHONG, ELATION_ANH_CHONG_NAME));
            list.Add(new RType(RELATION_EM_CHONG, RELATION_EM_CHONG_NAME));
            list.Add(new RType(RELATION_CON_NUOI, RELATION_CON_NUOI_NAME));
            list.Add(new RType(RELATION_CON_CHONG, RELATION_CON_CHONG_NAME));
            list.Add(new RType(RELATION_CON_VO, RELATION_CON_VO_NAME));
            list.Add(new RType(RELATION_CHAU_RUOT, RELATION_CHAU_RUOT_NAME));

            return list;
        }

        public static List<RType> RELATION_NGUOI_THAN_KHAC()
        {
            var list = new List<RType>();

            list.Add(new RType(-1, "Tất cả"));
            list.Add(new RType(RELATION_ME_NUOI, RELATION_ME_NUOI_NAME));
            list.Add(new RType(RELATION_CHA_NUOI, RELATION_CHA_NUOI_NAME));

            return list;
        }

        //-///////////////////////////////////////////////////////////////
        public const int RELATION_CHA_RUOT = 1;
        public const int RELATION_ME_RUOT = 2;
        public const int RELATION_ANH_RUOT = 3;
        public const int RELATION_CHI_RUOT = 4;
        public const int RELATION_EM_RUOT = 5;
        public const int RELATION_BA_NOI = 6;
        public const int RELATION_ONG_NOI = 7;
        public const int RELATION_CO_RUOT = 8;
        public const int RELATION_CHU_RUOT = 9;
        public const int RELATION_BAC_RUOTT1 = 10;
        public const int RELATION_DUONGT1 = 11;
        public const int RELATION_ONG_NGOAI = 12;
        public const int RELATION_BA_NGOAI = 13;
        public const int RELATION_CAU_RUOT = 14;
        public const int RELATION_DI_RUOT = 15;
        public const int RELATION_DUONGT2 = 16;
        public const int RELATION_CHA_VO = 17;
        public const int RELATION_CHA_CHONG = 18;
        public const int RELATION_ME_CHONG = 19;
        public const int RELATION_ME_VO = 20;
        public const int RELATION_CHA_NUOI = 21;
        public const int RELATION_VO = 22;
        public const int RELATION_CHONG = 23;
        public const int RELATION_CON_RUOT = 24;
        public const int RELATION_CHI_VO = 25;
        public const int RELATION_ANH_VO = 26;
        public const int RELATION_EM_VO = 27;
        public const int RELATION_BAC_RUOTT2 = 28;
        public const int RELATION_CHI_CHONG = 29;
        public const int RELATION_ANH_CHONG = 30;
        public const int RELATION_EM_CHONG = 31;
        public const int RELATION_ME_NUOI = 32;
        public const int RELATION_CON_NUOI = 33;
        public const int RELATION_CON_CHONG = 35;
        public const int RELATION_CON_VO = 36;
        public const int RELATION_CHAU_RUOT = 37;

        public const string RELATION_CHA_RUOT_NAME = "Cha ruột";
        public const string RELATION_ME_RUOT_NAME = "Mẹ ruột";
        public const string RELATION_ANH_RUOT_NAME = "Anh ruột";
        public const string RELATION_CHI_RUOT_NAME = "Chị ruột";
        public const string RELATION_EM_RUOT_NAME = "Em ruột";
        public const string RELATION_BA_NOI_NAME = "Bà nội";
        public const string RELATION_ONG_NOI_NAME = "Ông nội";
        public const string RELATION_CO_RUOT_NAME = "Cô ruột";
        public const string RELATION_CHU_RUOT_NAME = "Chú ruột";
        public const string RELATION_BAC_RUOTT1_NAME = "Bác ruột";
        public const string RELATION_DUONGT1_NAME = "Dượng ";
        public const string RELATION_ONG_NGOAI_NAME = "Ông ngoại";
        public const string RELATION_BA_NGOAI_NAME = "Bà ngoại";
        public const string RELATION_CAU_RUOT_NAME = "Cậu ruột";
        public const string RELATION_DI_RUOT_NAME = "Dì ruột";
        public const string RELATION_DUONGT2_NAME = "Dượng";
        public const string RELATION_CHA_VO_NAME = "Cha vợ";
        public const string RELATION_CHA_CHONG_NAME = "Cha chồng";
        public const string RELATION_ME_CHONG_NAME = "Mẹ chồng";
        public const string RELATION_ME_VO_NAME = "Mẹ vợ";
        public const string RELATION_CHA_NUOI_NAME = "Cha nuôi";
        public const string RELATION_VO_NAME = "Vợ";
        public const string RELATION_CHONG_NAME = "Chồng";
        public const string RELATION_CON_RUOT_NAME = "Con ruột";
        public const string RELATION_CHI_VO_NAME = "Chị vợ";
        public const string RELATION_ANH_VO_NAME = "Anh vợ";
        public const string RELATION_EM_VO_NAME = "Em vợ";
        public const string RELATION_BAC_RUOTT2_NAME = "Bác ruột";
        public const string RELATION_CHI_CHONG_NAME = "Chi Chồng";
        public const string ELATION_ANH_CHONG_NAME = "Anh Chồng";
        public const string RELATION_EM_CHONG_NAME = "Em Chồng";
        public const string RELATION_ME_NUOI_NAME = "Mẹ Nuôi";
        public const string RELATION_CON_NUOI_NAME = "Con nuôi";
        public const string RELATION_CON_CHONG_NAME = "Con chồng";
        public const string RELATION_CON_VO_NAME = "Con vợ";
        public const string RELATION_CHAU_RUOT_NAME = "Cháu ruột";

        public static List<RType> GetAllRTypeN_Detail()
        {
            var list = new List<RType>();

            list.Add(new RType(-1, "Tất cả"));
            list.Add(new RType(RELATION_CHA_RUOT, RELATION_CHA_RUOT_NAME));
            list.Add(new RType(RELATION_ME_RUOT, RELATION_ME_RUOT_NAME));
            list.Add(new RType(RELATION_ANH_RUOT, RELATION_ANH_RUOT_NAME));
            list.Add(new RType(RELATION_CHI_RUOT, RELATION_CHI_RUOT_NAME));
            list.Add(new RType(RELATION_EM_RUOT, RELATION_EM_RUOT_NAME));
            list.Add(new RType(RELATION_BA_NOI, RELATION_BA_NOI_NAME));
            list.Add(new RType(RELATION_ONG_NOI, RELATION_ONG_NOI_NAME));
            list.Add(new RType(RELATION_CO_RUOT, RELATION_CO_RUOT_NAME));
            list.Add(new RType(RELATION_CHU_RUOT, RELATION_CHU_RUOT_NAME));
            list.Add(new RType(RELATION_BAC_RUOTT1, RELATION_BAC_RUOTT1_NAME));
            list.Add(new RType(RELATION_DUONGT1, RELATION_DUONGT1_NAME));
            list.Add(new RType(RELATION_ONG_NGOAI, RELATION_ONG_NGOAI_NAME));
            list.Add(new RType(RELATION_BA_NGOAI, RELATION_BA_NGOAI_NAME));
            list.Add(new RType(RELATION_CAU_RUOT, RELATION_CAU_RUOT_NAME));
            list.Add(new RType(RELATION_DI_RUOT, RELATION_DI_RUOT_NAME));
            list.Add(new RType(RELATION_DUONGT2, RELATION_DUONGT2_NAME));
            list.Add(new RType(RELATION_CHA_VO, RELATION_CHA_VO_NAME));
            list.Add(new RType(RELATION_CHA_CHONG, RELATION_CHA_CHONG_NAME));
            list.Add(new RType(RELATION_ME_CHONG, RELATION_ME_CHONG_NAME));
            list.Add(new RType(RELATION_ME_VO, RELATION_ME_VO_NAME));
            list.Add(new RType(RELATION_CHA_NUOI, RELATION_CHA_NUOI_NAME));
            list.Add(new RType(RELATION_VO, RELATION_VO_NAME));
            list.Add(new RType(RELATION_CHONG, RELATION_CHONG_NAME));
            list.Add(new RType(RELATION_CON_RUOT, RELATION_CON_RUOT_NAME));
            list.Add(new RType(RELATION_CHI_VO, RELATION_CHI_VO_NAME));
            list.Add(new RType(RELATION_ANH_VO, RELATION_ANH_VO_NAME));
            list.Add(new RType(RELATION_EM_VO, RELATION_EM_VO_NAME));
            list.Add(new RType(RELATION_BAC_RUOTT2, RELATION_BAC_RUOTT2_NAME));
            list.Add(new RType(RELATION_CHI_CHONG, RELATION_CHI_CHONG_NAME));
            list.Add(new RType(RELATION_ANH_CHONG, ELATION_ANH_CHONG_NAME));
            list.Add(new RType(RELATION_EM_CHONG, RELATION_EM_CHONG_NAME));
            list.Add(new RType(RELATION_ME_NUOI, RELATION_ME_NUOI_NAME));
            list.Add(new RType(RELATION_CON_NUOI, RELATION_CON_NUOI_NAME));
            list.Add(new RType(RELATION_CON_CHONG, RELATION_CON_CHONG_NAME));
            list.Add(new RType(RELATION_CON_VO, RELATION_CON_VO_NAME));
            list.Add(new RType(RELATION_CHAU_RUOT, RELATION_CHAU_RUOT_NAME));

            return list;
        }

        public static List<RType> GetAllRType_Detail()
        {
            var list = new List<RType>();

            list.Add(new RType(RELATION_CHA_RUOT, RELATION_CHA_RUOT_NAME));
            list.Add(new RType(RELATION_ME_RUOT, RELATION_ME_RUOT_NAME));
            list.Add(new RType(RELATION_ANH_RUOT, RELATION_ANH_RUOT_NAME));
            list.Add(new RType(RELATION_CHI_RUOT, RELATION_CHI_RUOT_NAME));
            list.Add(new RType(RELATION_EM_RUOT, RELATION_EM_RUOT_NAME));
            list.Add(new RType(RELATION_BA_NOI, RELATION_BA_NOI_NAME));
            list.Add(new RType(RELATION_ONG_NOI, RELATION_ONG_NOI_NAME));
            list.Add(new RType(RELATION_CO_RUOT, RELATION_CO_RUOT_NAME));
            list.Add(new RType(RELATION_CHU_RUOT, RELATION_CHU_RUOT_NAME));
            list.Add(new RType(RELATION_BAC_RUOTT1, RELATION_BAC_RUOTT1_NAME));
            list.Add(new RType(RELATION_DUONGT1, RELATION_DUONGT1_NAME));
            list.Add(new RType(RELATION_ONG_NGOAI, RELATION_ONG_NGOAI_NAME));
            list.Add(new RType(RELATION_BA_NGOAI, RELATION_BA_NGOAI_NAME));
            list.Add(new RType(RELATION_CAU_RUOT, RELATION_CAU_RUOT_NAME));
            list.Add(new RType(RELATION_DI_RUOT, RELATION_DI_RUOT_NAME));
            list.Add(new RType(RELATION_DUONGT2, RELATION_DUONGT2_NAME));
            list.Add(new RType(RELATION_CHA_VO, RELATION_CHA_VO_NAME));
            list.Add(new RType(RELATION_CHA_CHONG, RELATION_CHA_CHONG_NAME));
            list.Add(new RType(RELATION_ME_CHONG, RELATION_ME_CHONG_NAME));
            list.Add(new RType(RELATION_ME_VO, RELATION_ME_VO_NAME));
            list.Add(new RType(RELATION_CHA_NUOI, RELATION_CHA_NUOI_NAME));
            list.Add(new RType(RELATION_VO, RELATION_VO_NAME));
            list.Add(new RType(RELATION_CHONG, RELATION_CHONG_NAME));
            list.Add(new RType(RELATION_CON_RUOT, RELATION_CON_RUOT_NAME));
            list.Add(new RType(RELATION_CHI_VO, RELATION_CHI_VO_NAME));
            list.Add(new RType(RELATION_ANH_VO, RELATION_ANH_VO_NAME));
            list.Add(new RType(RELATION_EM_VO, RELATION_EM_VO_NAME));
            list.Add(new RType(RELATION_BAC_RUOTT2, RELATION_BAC_RUOTT2_NAME));
            list.Add(new RType(RELATION_CHI_CHONG, RELATION_CHI_CHONG_NAME));
            list.Add(new RType(RELATION_ANH_CHONG, ELATION_ANH_CHONG_NAME));
            list.Add(new RType(RELATION_EM_CHONG, RELATION_EM_CHONG_NAME));
            list.Add(new RType(RELATION_ME_NUOI, RELATION_ME_NUOI_NAME));
            list.Add(new RType(RELATION_CON_NUOI, RELATION_CON_NUOI_NAME));
            list.Add(new RType(RELATION_CON_CHONG, RELATION_CON_CHONG_NAME));
            list.Add(new RType(RELATION_CON_VO, RELATION_CON_VO_NAME));
            list.Add(new RType(RELATION_CHAU_RUOT, RELATION_CHAU_RUOT_NAME));

            return list;
        }

        public static string GetRTypeName_Detail(int rTypeId)
        {
            var name = string.Empty;
            var list = GetAllRType_Detail();
            foreach (var obj in list)
                if (obj.RTypeId == rTypeId)
                    name = obj.RTypeName;
            return name;
        }

        /// <summary>
        /// </summary>
        public const int JOB_HISTORY_SELF = 1;

        public const int JOB_HISTORY_REWARD = 2;
        public const int JOB_HISTORY_BREACH_OF_DISCIPLINE = 3;

        #endregion

        #region Cac don gia de tinh luong

        public const int UnitPricesType_Fixed_Id = 1;
        public const int UnitPricesType_UnFixed_Id = 2;
        public const int UnitPricesType_UnFixed_Balance_Id = 3;
        public const string UnitPricesType_Fixed_Name = "Đơn giá cố định";
        public const string UnitPricesType_UnFixed_Name = "Đơn giá đầu đen từ quỹ lương";
        public const string UnitPricesType_UnFixed_Balance_Name = "Đơn giá từ quỹ lương thưởng dư";

        public static string GetUnitPriceTypeName(int UnitPriceTypeId)
        {
            if (UnitPriceTypeId == UnitPricesType_Fixed_Id)
                return UnitPricesType_Fixed_Name;
            if (UnitPriceTypeId == UnitPricesType_UnFixed_Id)
                return UnitPricesType_UnFixed_Name;
            if (UnitPriceTypeId == UnitPricesType_UnFixed_Balance_Id)
                return UnitPricesType_UnFixed_Balance_Name;
            return string.Empty;
        }

        public static double DG_LNS = 3230000;
        public static double DG_TLTTCLCB = 1050000;
        public static double DG_TLTTCKPN = 1150000;
        public static double DG_AnGiuaCa = 680000;
        public static double DG_GTGC = 3600000;
        public static double DG_GTCN = 9000000;

        public static int H1_Income_Type_Seasonal = 1;
        public static int H1_Income_Type_Position = 2;

        #endregion

        #region Loai hop Dong

        public const int ContractType_DataType_Contract_Id = 1;
        public const int ContractType_DataType_Decision_Id = 2;

        public const int HopDong_XÐTH_12T = 14;
        public const int HopDong_XÐTH_24T = 7;
        public const int HopDong_XÐTH_36T = 15;
        public const int HopDong_KoXÐTH = 13;
        public const int HopDong_TapNghe_DaiHoc = 12;
        public const int HopDong_TapNghe_TrungCap = 11;
        public const int HopDong_TapNghe_SoCap = 9;
        public const int HopDong_TapNghe_ConLai = 5;
        public const int HopDong_ThuViec_DaiHoc = 10;
        public const int HopDong_ThuViec_Khac = 6;
        public const int HopDong_HocNghe = 1;
        public const int HopDong_ThoiVu_3T = 8;
        public const int HopDong_ThoiVu_6T = 16;

        #endregion

        #region Recruitment

        public const string TITLE_SESSION_CATEGORY = "DANH MỤC CÁC ĐỢT TUYỂN DỤNG";
        public const string TITLE_EDUCATION_LEVEL_CATEGORY = "DANH MỤC TRÌNH ĐỘ VĂN HÓA";
        public const string TITLE_RECRUITMENT_LIST = "DANH SÁCH TUYỂN DỤNG";
        public const string TITLE_RECRUITMENT_RESULT = "DANH SÁCH KẾT QUẢ TUYỂN DỤNG";
        public static string CANDIDATE_TITLE_ADD_NEW = "THÊM MỚI ỨNG CỬ VIÊN";
        public static string CANDIDATE_TITLE_EXPORT_EXCEL = "EXPORT EXCEL";

        public const string ALL_ITEM_NAME = "Tât cả";
        public const int ALL_ITEM_ID = 0;
        public const string DATA_EXPORTED_PATH = @"~\App_Data\ExportedExcels";

        public static int CANDIDATE_NO_OK_ID = 0;
        public static int CANDIDATE_OK_ID = 1;
        public static int NO_CANDIDATE_ID = 2;
        public static int CANDIDATE_ONEMORE_ID = 3;
        public static string CANDIDATE_NO_OK_NAME = "Không đạt";
        public static string CANDIDATE_OK_NAME = "Đạt";
        public static string NO_CANDIDATE_NAME = "Xét tuyển";
        public static string CANDIDATE_ONEMORE_NAME = "Hồ sơ xét thêm";


        public static int SESSION_TYPE_CS = 1;
        public static int SESSION_TYPE_DRIVING = 2;
        public static int SESSION_TYPE_UNSKILLED_LABOUR = 3;
        public static int SESSION_TYPE_GROUND_HANDLING_EQUIPMENT = 4;
        public static int SESSION_TYPE_SPECIALITY_ENGLISH = 5;
        public static int SESSION_TYPE_LoadAndUnloadCargo = 6;
        public static string SESSION_TYPE_CS_NAME = "Vi tính - Anh văn - Ngoại hình - Hội đồng";
        public static string SESSION_TYPE_DRIVING_NAME = "Lái xe";
        public static string SESSION_TYPE_UNSKILLED_LABOUR_NAME = "Xét tuyển lao động phổ thông";
        public static string SESSION_TYPE_GROUND_HANDLING_EQUIPMENT_NAME = "Sữa chữa trang thiết bị mặt đất";
        public static string SESSION_TYPE_SPECIALITY_ENGLISH_NAME = "Chuyên Môn -  Anh văn - Hội đồng";
        public static string SESSION_TYPE_LoadAndUnloadCargo_Name = "Chất xếp hàng hóa - hành lý";

        //public static List<RType> GetAllCandidateType_ItemAll()
        //{
        //    List<RType> list = new List<RType>();

        //    list.Add(new RType(-1, " "));
        //    list.Add(new RType(CANDIDATE_NO_OK_ID, CANDIDATE_NO_OK_NAME));
        //    list.Add(new RType(CANDIDATE_OK_ID, CANDIDATE_OK_NAME));

        //    return list;
        //}

        public static List<RType> GetAllCandidateType()
        {
            var list = new List<RType>();

            list.Add(new RType(-1, ""));
            list.Add(new RType(CANDIDATE_NO_OK_ID, CANDIDATE_NO_OK_NAME));
            list.Add(new RType(CANDIDATE_OK_ID, CANDIDATE_OK_NAME));
            list.Add(new RType(NO_CANDIDATE_ID, NO_CANDIDATE_NAME));
            list.Add(new RType(CANDIDATE_ONEMORE_ID, CANDIDATE_ONEMORE_NAME));

            return list;
        }

        public static List<RType> GetAllSessionType()
        {
            var list = new List<RType>();

            list.Add(new RType(SESSION_TYPE_CS, SESSION_TYPE_CS_NAME));
            list.Add(new RType(SESSION_TYPE_DRIVING, SESSION_TYPE_DRIVING_NAME));
            list.Add(new RType(SESSION_TYPE_UNSKILLED_LABOUR, SESSION_TYPE_UNSKILLED_LABOUR_NAME));
            list.Add(new RType(SESSION_TYPE_GROUND_HANDLING_EQUIPMENT, SESSION_TYPE_GROUND_HANDLING_EQUIPMENT_NAME));
            list.Add(new RType(SESSION_TYPE_SPECIALITY_ENGLISH, SESSION_TYPE_SPECIALITY_ENGLISH_NAME));
            list.Add(new RType(SESSION_TYPE_LoadAndUnloadCargo, SESSION_TYPE_LoadAndUnloadCargo_Name));

            return list;
        }

        public static string GetNameBySessionType(int sessionType)
        {
            if (SESSION_TYPE_CS == sessionType)
                return SESSION_TYPE_CS_NAME;
            if (SESSION_TYPE_DRIVING == sessionType)
                return SESSION_TYPE_DRIVING_NAME;
            if (SESSION_TYPE_UNSKILLED_LABOUR == sessionType)
                return SESSION_TYPE_UNSKILLED_LABOUR_NAME;
            if (SESSION_TYPE_GROUND_HANDLING_EQUIPMENT == sessionType)
                return SESSION_TYPE_GROUND_HANDLING_EQUIPMENT_NAME;
            if (SESSION_TYPE_SPECIALITY_ENGLISH == sessionType)
                return SESSION_TYPE_SPECIALITY_ENGLISH_NAME;
            if (SESSION_TYPE_LoadAndUnloadCargo == sessionType)
                return SESSION_TYPE_LoadAndUnloadCargo_Name;
            return string.Empty;
        }

        #endregion

        #region Decision

        public const int Decision_Appointment_Id = 1;
        public const int Decision_Change_Position_Id = 2;
        public const int Decision_Change_Department_Id = 3;
        public const int Decision_Leave_Job_Id = 4;
        public const int Decision_Reward_Id = 5;
        public const int Decision_BreachOfDiscipline_Id = 6;


        public const int Status_Leave_Job_Id = 1;
        public const int Status_Training_Id = 3;
        public const int Status_Pause_Job_Id = 11;

        #endregion

        #region IsChangeContract

        public const int IsChangeContract_No_Id = 0;
        public const int IsChangeContract_Yes_Id = 1;

        #endregion

        #region Employee

        public const int Employee_Status_Leave_Id = 0;
        public const int Employee_Status_Working_Id = 1;

        public const string Employee_Status_Leave_Name = "Nghỉ việc";
        public const string Employee_Status_Working_Name = "Làm việc";

        public static List<RType> GetAllEmployeeStatus()
        {
            var list = new List<RType>();
            list.Add(new RType(Employee_Status_Leave_Id, Employee_Status_Leave_Name));
            list.Add(new RType(Employee_Status_Working_Id, Employee_Status_Working_Name));
            return list;
        }

        public static string GetStatusNameById(int Id)
        {
            if (Id == Employee_Status_Leave_Id)
                return Employee_Status_Leave_Name;
            if (Id == Employee_Status_Working_Id)
                return Employee_Status_Working_Name;
            return string.Empty;
        }

        #endregion

        #region constant For CommandLog

        /// <summary>
        /// </summary>
        public const int CommandLog_Form_AccountBankId = 1;

        public const int CommandLog_Form_UpdateEmployeeGeneralInforId = 2;
        public const int CommandLog_Form_UpdateEmployeeDetailInforId = 3;
        public const int CommandLog_Form_WorkDay_Id = 4;

        /// <summary>
        ///     Constants for Command
        /// </summary>
        public const int CommandLog_Insert_Id = 1;

        public const string CommandLog_Insert_Name = "Thêm mới";
        public const int CommandLog_Update_Id = 2;
        public const string CommandLog_Update_Name = "Cập nhật";
        public const int CommandLog_Delete_Id = 3;
        public const string CommandLog_Delete_Name = "Xóa";

        public static List<RType> GetAllCommandType()
        {
            var list = new List<RType>();
            list.Add(new RType(CommandLog_Insert_Id, CommandLog_Insert_Name));
            list.Add(new RType(CommandLog_Update_Id, CommandLog_Update_Name));
            list.Add(new RType(CommandLog_Delete_Id, CommandLog_Delete_Name));
            return list;
        }

        public static string GetlCommandTypeNameById(int commandTypeId)
        {
            var list = GetAllCommandType();
            var strReturn = string.Empty;
            foreach (var obj in list)
                if (obj.RTypeId == commandTypeId)
                {
                    strReturn = obj.RTypeName;
                    break;
                }

            return strReturn;
        }

        #endregion

        #region SalaryRegulation

        public const int SalaryRegulation_BasicSalaryType_Id = 1;
        public const string SalaryRegulation_BasicSalaryType_Name = "Quy chế lương cơ bản";
        public const int SalaryRegulation_ProductivitySalaryType_Id = 2;
        public const string SalaryRegulation_ProductivitySalaryType_Name = "Quy chế lương năng suất";

        public const int IsVCQLNN_ALl_Id = 0;
        public const string IsVCQLNN_ALl_Name = "Viên chức quản lý và Cán bộ nhân viên";
        public const int IsVCQLNN_Id = 1;
        public const string IsVCQLNN_Name = "Viên chức quản lý nhà nước";
        public const int IsVCQLNN_Not_Id = 2;
        public const string IsVCQLNN_Not_Name = "Cán bộ nhân viên Công ty";

        public static List<RType> GetAllSalaryRegulationType()
        {
            var list = new List<RType>();
            list.Add(new RType(SalaryRegulation_BasicSalaryType_Id, SalaryRegulation_BasicSalaryType_Name));
            list.Add(new RType(SalaryRegulation_ProductivitySalaryType_Id, SalaryRegulation_ProductivitySalaryType_Name));
            return list;
        }

        public static string GetSalaryRegulationTypeName(int Id)
        {
            if (Id == SalaryRegulation_BasicSalaryType_Id)
                return SalaryRegulation_BasicSalaryType_Name;
            if (Id == SalaryRegulation_ProductivitySalaryType_Id)
                return SalaryRegulation_ProductivitySalaryType_Name;

            return "";
        }

        public static List<Unit> GetAllVCQLNN(bool isNone)
        {
            var list = new List<Unit>();
            if (isNone)
                list.Add(new Unit(IsVCQLNN_ALl_Id, IsVCQLNN_ALl_Name));
            list.Add(new Unit(IsVCQLNN_Id, IsVCQLNN_Name));
            list.Add(new Unit(IsVCQLNN_Not_Id, IsVCQLNN_Not_Name));
            return list;
        }

        public static string GetVCQLNN_NameById(int Id)
        {
            if (Id == IsVCQLNN_ALl_Id)
                return IsVCQLNN_ALl_Name;
            if (Id == IsVCQLNN_Id)
                return IsVCQLNN_Name;
            if (Id == IsVCQLNN_Not_Id)
                return IsVCQLNN_Not_Name;
            return "";
        }

        public static List<Unit> GetAllYears()
        {
            var list = new List<Unit>();
            for (var i = DateTime.Now.Year - 1; i <= DateTime.Now.Year; i++)
                list.Add(new Unit(i, i.ToString()));
            return list;
        }

        public static List<Unit> GetAllMonths()
        {
            var list = new List<Unit>();
            for (var i = 1; i <= 12; i++)
                list.Add(new Unit(i, i.ToString()));
            return list;
        }

        #endregion

        #region LevelPosition

        public static int LevelPosition_VicePresident_Id = -1;
        public static string LevelPosition_VicePresident_Name = "Phó Tổng Giám Đốc";
        public static int LevelPosition_President_Id = -2;
        public static string LevelPosition_President_Name = "Tổng Giám Đốc";
        public static int LevelPosition_Chairman_Of_The_Board_Id = -3;
        public static string LevelPosition_Chairman_Of_The_Board_Name = "Chủ tịch HĐQT";
        public static int LevelPosition_Chairman_Of_The_Board_Name_And_President_Id = -4;
        public static string LevelPosition_Chairman_Of_The_Board_Name_And_President_Name = "Chủ tịch HĐQT & TGĐ";

        public static int LevelPosition_Director_Id = 1;
        public static string LevelPosition_Director_Name = "Giám Đốc";
        public static int LevelPosition_DeputyDirector_Id = 2;
        public static string LevelPosition_DeputyDirector_Name = "Phó Giám Đốc";
        public static int LevelPosition_Manager_Id = 3;
        public static string LevelPosition_Manager_Name = "Trưởng phòng";
        public static int LevelPosition_DeputyManager_Id = 4;
        public static string LevelPosition_DeputyManager_Name = "Phó phòng";
        public static int LevelPosition_SessionManager_Id = 5;
        public static string LevelPosition_SessionManager_Name = "Đội trưởng";
        public static int LevelPosition_SessionDeputyManager_Id = 6;
        public static string LevelPosition_SessionDeputyManager_Name = "Đội phó";
        public static int LevelPosition_TeamManager_Id = 7;
        public static string LevelPosition_TeamManager_Name = "Tổ trưởng";
        public static int LevelPosition_TeamDeputyManager_Id = 8;
        public static string LevelPosition_TeamDeputyManager_Name = "Tổ phó";
        public static int LevelPosition_ShiftManager_Id = 9;
        public static string LevelPosition_ShiftManager_Name = "Ca trưởng";
        public static int LevelPosition_ShiftDeputyManager_Id = 10;
        public static string LevelPosition_ShiftDeputyManager_Name = "Ca phó";
        public static int LevelPosition_Staff_Id = 20;
        public static string LevelPosition_Staff_Name = "Nhân viên";

        public static List<Unit> GetAllLevelPosition()
        {
            var list = new List<Unit>();
            list.Add(new Unit(LevelPosition_VicePresident_Id, LevelPosition_VicePresident_Name));
            list.Add(new Unit(LevelPosition_President_Id, LevelPosition_President_Name));
            list.Add(new Unit(LevelPosition_Chairman_Of_The_Board_Id, LevelPosition_Chairman_Of_The_Board_Name));
            list.Add(new Unit(LevelPosition_Chairman_Of_The_Board_Name_And_President_Id,
                LevelPosition_Chairman_Of_The_Board_Name_And_President_Name));

            list.Add(new Unit(LevelPosition_Director_Id, LevelPosition_Director_Name));
            list.Add(new Unit(LevelPosition_DeputyDirector_Id, LevelPosition_DeputyDirector_Name));
            list.Add(new Unit(LevelPosition_Manager_Id, LevelPosition_Manager_Name));
            list.Add(new Unit(LevelPosition_DeputyManager_Id, LevelPosition_DeputyManager_Name));
            list.Add(new Unit(LevelPosition_SessionManager_Id, LevelPosition_SessionManager_Name));
            list.Add(new Unit(LevelPosition_SessionDeputyManager_Id, LevelPosition_SessionDeputyManager_Name));
            list.Add(new Unit(LevelPosition_TeamManager_Id, LevelPosition_TeamManager_Name));
            list.Add(new Unit(LevelPosition_TeamDeputyManager_Id, LevelPosition_TeamDeputyManager_Name));
            list.Add(new Unit(LevelPosition_ShiftManager_Id, LevelPosition_ShiftManager_Name));
            list.Add(new Unit(LevelPosition_ShiftDeputyManager_Id, LevelPosition_ShiftDeputyManager_Name));
            list.Add(new Unit(LevelPosition_Staff_Id, LevelPosition_Staff_Name));

            return list;
        }

        public static string GetLevelPositionNameById(int Id)
        {
            if (LevelPosition_Director_Id == Id)
                return LevelPosition_Director_Name;
            if (LevelPosition_DeputyDirector_Id == Id)
                return LevelPosition_DeputyDirector_Name;
            if (LevelPosition_Manager_Id == Id)
                return LevelPosition_Manager_Name;
            if (LevelPosition_DeputyManager_Id == Id)
                return LevelPosition_DeputyManager_Name;
            if (LevelPosition_SessionManager_Id == Id)
                return LevelPosition_SessionManager_Name;
            if (LevelPosition_SessionDeputyManager_Id == Id)
                return LevelPosition_SessionDeputyManager_Name;
            if (LevelPosition_TeamManager_Id == Id)
                return LevelPosition_TeamManager_Name;
            if (LevelPosition_TeamDeputyManager_Id == Id)
                return LevelPosition_TeamDeputyManager_Name;
            if (LevelPosition_ShiftManager_Id == Id)
                return LevelPosition_ShiftManager_Name;
            if (LevelPosition_ShiftDeputyManager_Id == Id)
                return LevelPosition_ShiftDeputyManager_Name;
            if (LevelPosition_Staff_Id == Id)
                return LevelPosition_Staff_Name;
            return "";
        }

        #endregion

        #region SecurityControl

        public static string So1Do_Code = "1";
        public static string So1Do_Name = "1 đỏ";
        public static string So1Do_Description = "Số 1 màu đỏ: KV tàu bay chuyên cơ và KV số 1 màu đen";

        public static string So4Do_Code = "4";
        public static string So4Do_Name = "4 đỏ";

        public static string So4Do_Description =
            "Số 4 màu đỏ: KV nhà khách chuyên cơ trong thời gian có chuyến bay chuyên cơ cũng như không có chuyến bay chuyên cơ";

        public static string So1Den_Code = "1d";
        public static string So1Den_Name = "1";

        public static string So1Den_Description =
            "Số 1 màu đen: KV sân đỗ tàu bay, đường hạ cất cánh, đường lăn, lề và dải bảo hiểm, đài kiểm soát KL; KV phục vụ kỹ thuật sân đỗ";

        public static string So1DenA_Code = "1dA";
        public static string So1DenA_Name = "1A";
        public static string So1DenA_Description = "Số 1A: KV sân đỗ TB";
        public static string So1DenB_Code = "1dB";
        public static string So1DenB_Name = "1B";

        public static string So1DenB_Description =
            "Số 1B: KV sân đỗ TB, đường hạ cất cánh, đường lăn, lề và dải bảo hiểm";

        public static string So1DenC_Code = "1dC";
        public static string So1DenC_Name = "1C";
        public static string So1DenC_Description = "Số 1C: Đài kiểm soát KL";
        public static string So1DenD_Code = "1dD";
        public static string So1DenD_Name = "1D";
        public static string So1DenD_Description = "Số 1D: KV sân đỗ TB, phục vụ kỹ thuật sân đỗ";
        public static string So1DenE_Code = "1dE";
        public static string So1DenE_Name = "1E";
        public static string So1DenE_Description = "Số 1E: KV bãi xe trong KV ĐKSKL";

        public static string So2Den_Code = "2d";
        public static string So2Den_Name = "2";
        public static string So2Den_Description = "Số 2: KV hạn chế, cách ly ga đi QT";
        public static string So2DenA_Code = "2dA";
        public static string So2DenA_Name = "2A";
        public static string So2DenA_Description = "Số 2A: KV cách ly ga đi QT";
        public static string So2DenB_Code = "2dB";
        public static string So2DenB_Name = "2B";
        public static string So2DenB_Description = "Số 2B: KV từ kiểm tra Hải quan đến kiểm tra ANHK ga đi QT";
        public static string So2DenC_Code = "2dC";
        public static string So2DenC_Name = "2C";
        public static string So2DenC_Description = "Số 2C: KV làm thủ tục HK, HL ga đi QT";

        public static string So3Den_Code = "3d";
        public static string So3Den_Name = "3";
        public static string So3Den_Description = "Số 3: KV hạn chế, cách ly ga đi QN";
        public static string So3DenA_Code = "3dA";
        public static string So3DenA_Name = "3A";
        public static string So3DenA_Description = "Số 3A: KV cách ly ga đi QN";
        public static string So3DenB_Code = "3dB";
        public static string So3DenB_Name = "3B";
        public static string So3DenB_Description = "Số 3B: KV từ chân cầu thang đến điểm kiểm tra ANHK ga đi QN";
        public static string So3DenC_Code = "3dC";
        public static string So3DenC_Name = "3C";
        public static string So3DenC_Description = "Số 3C: KV làm thủ tục HK, HL ga đi QN";

        public static string So4Den_Code = "4d";
        public static string So4Den_Name = "4";

        public static string So4Den_Description =
            "Số 4 màu đen: KV nhà khách chuyên cơ trong thời gian không có chuyến bay chuyên cơ";

        public static string So5Den_Code = "5d";
        public static string So5Den_Name = "5";
        public static string So5Den_Description = "Số 5 màu đen: KV hạn chế ga đến QN và QT";
        public static string So5DenA_Code = "5dA";
        public static string So5DenA_Name = "5A";
        public static string So5DenA_Description = "Số 5A: KV nhận HL ga đến QT";
        public static string So5DenB_Code = "5dB";
        public static string So5DenB_Name = "5B";
        public static string So5DenB_Description = "Số 5B: KV nhận HL ga đến QN";
        public static string So5DenC_Code = "5dC";
        public static string So5DenC_Name = "5C";
        public static string So5DenC_Description = "Số 5C: KV làm thủ tục nhập cảnh và chuyển tiếp ga đến QT";
        public static string So5DenD_Code = "5dD";
        public static string So5DenD_Name = "5D";
        public static string So5DenD_Description = "Số 5D: KV sau Hải quan ga đến QT";
        public static string So5DenE_Code = "5dE";
        public static string So5DenE_Name = "5E";
        public static string So5DenE_Description = "Số 5E: KV Văn phòng HHK";

        public static string So6Den_Code = "6d";
        public static string So6Den_Name = "6";

        public static string So6Den_Description =
            "Số 6 màu đen: KV phân loại, chất xếp HL ký gửi, hàng hóa sau khi đã được kiểm tra soi chiếu an ninh";

        public static string So6DenA_Code = "6dA";
        public static string So6DenA_Name = "6A";
        public static string So6DenA_Description = "Số 6A: KV xử lý, phân loại HL ký gửi";
        public static string So6DenB_Code = "6dB";
        public static string So6DenB_Name = "6B";
        public static string So6DenB_Description = "Số 6B: KV xử lý, phân loại hàng hóa QT";
        public static string So6DenC_Code = "6dC";
        public static string So6DenC_Name = "6C";
        public static string So6DenC_Description = "Số 6C: KV xử lý, phân loại hàng hóa QN";

        public static List<SecurityControl> GetAllSecurityControl()
        {
            var list = new List<SecurityControl>();
            list.Add(new SecurityControl(So1Do_Code, So1Do_Name, So1Do_Description));

            list.Add(new SecurityControl(So1Den_Code, So1Den_Name, So1Den_Description));
            list.Add(new SecurityControl(So1DenA_Code, So1DenA_Name, So1DenA_Description));
            list.Add(new SecurityControl(So1DenB_Code, So1DenB_Name, So1DenB_Description));
            list.Add(new SecurityControl(So1DenC_Code, So1DenC_Name, So1DenC_Description));
            list.Add(new SecurityControl(So1DenD_Code, So1DenD_Name, So1DenD_Description));
            list.Add(new SecurityControl(So1DenE_Code, So1DenE_Name, So1DenE_Description));

            list.Add(new SecurityControl(So2Den_Code, So2Den_Name, So2Den_Description));
            list.Add(new SecurityControl(So2DenA_Code, So2DenA_Name, So2DenA_Description));
            list.Add(new SecurityControl(So2DenB_Code, So2DenB_Name, So2DenB_Description));
            list.Add(new SecurityControl(So2DenC_Code, So2DenC_Name, So2DenC_Description));

            list.Add(new SecurityControl(So3Den_Code, So3Den_Name, So3Den_Description));
            list.Add(new SecurityControl(So3DenA_Code, So3DenA_Name, So3DenA_Description));
            list.Add(new SecurityControl(So3DenB_Code, So3DenB_Name, So3DenB_Description));
            list.Add(new SecurityControl(So3DenC_Code, So3DenC_Name, So3DenC_Description));

            list.Add(new SecurityControl(So4Do_Code, So4Do_Name, So4Do_Description));

            list.Add(new SecurityControl(So4Den_Code, So4Den_Name, So4Den_Description));

            list.Add(new SecurityControl(So5Den_Code, So5Den_Name, So5Den_Description));
            list.Add(new SecurityControl(So5DenA_Code, So5DenA_Name, So5DenA_Description));
            list.Add(new SecurityControl(So5DenB_Code, So5DenB_Name, So5DenB_Description));
            list.Add(new SecurityControl(So5DenC_Code, So5DenC_Name, So5DenC_Description));
            list.Add(new SecurityControl(So5DenD_Code, So5DenD_Name, So5DenD_Description));
            list.Add(new SecurityControl(So5DenE_Code, So5DenE_Name, So5DenE_Description));

            list.Add(new SecurityControl(So6Den_Code, So6Den_Name, So6Den_Description));
            list.Add(new SecurityControl(So6DenA_Code, So6DenA_Name, So6DenA_Description));
            list.Add(new SecurityControl(So6DenB_Code, So6DenB_Name, So6DenB_Description));
            list.Add(new SecurityControl(So6DenC_Code, So6DenC_Name, So6DenC_Description));

            return list;
        }

        public static string GetCodeBySecurityControlName(string SCCode)
        {
            var Return = "";
            if (SCCode.CompareTo(So1Do_Name) == 1)
                Return = So1Do_Code;
            else if (SCCode.CompareTo(So4Do_Name) == 1)
                Return = So4Do_Code;
            else if (SCCode.CompareTo(So1Den_Code) == 1)
                Return = So1Den_Code;
            else if (SCCode.CompareTo(So1DenA_Code) == 1)
                Return = So1DenA_Code;
            else if (SCCode.CompareTo(So1DenB_Code) == 1)
                Return = So1DenB_Code;
            else if (SCCode.CompareTo(So1DenC_Code) == 1)
                Return = So1DenC_Code;
            else if (SCCode.CompareTo(So1DenD_Code) == 1)
                Return = So1DenD_Code;
            else if (SCCode.CompareTo(So1DenE_Code) == 1)
                Return = So1DenE_Code;
            else if (SCCode.CompareTo(So2Den_Code) == 1)
                Return = So2Den_Code;
            else if (SCCode.CompareTo(So2DenA_Code) == 1)
                Return = So2DenA_Code;
            else if (SCCode.CompareTo(So2DenB_Code) == 1)
                Return = So2DenB_Code;
            else if (SCCode.CompareTo(So2DenC_Code) == 1)
                Return = So2DenC_Code;
            else if (SCCode.CompareTo(So3Den_Code) == 1)
                Return = So3Den_Code;
            else if (SCCode.CompareTo(So3DenA_Code) == 1)
                Return = So3DenA_Code;
            else if (SCCode.CompareTo(So3DenB_Code) == 1)
                Return = So3DenB_Code;
            else if (SCCode.CompareTo(So3DenC_Code) == 1)
                Return = So3DenC_Code;
            else if (SCCode.CompareTo(So4Den_Code) == 1)
                Return = So4Den_Code;
            else if (SCCode.CompareTo(So5Den_Code) == 1)
                Return = So5Den_Code;
            else if (SCCode.CompareTo(So5DenA_Code) == 1)
                Return = So5DenA_Code;
            else if (SCCode.CompareTo(So5DenB_Code) == 1)
                Return = So5DenB_Code;
            else if (SCCode.CompareTo(So5DenC_Code) == 1)
                Return = So5DenC_Code;
            else if (SCCode.CompareTo(So5DenD_Code) == 1)
                Return = So5DenD_Code;
            else if (SCCode.CompareTo(So5DenE_Code) == 1)
                Return = So5DenE_Code;
            else if (SCCode.CompareTo(So6Den_Code) == 1)
                Return = So6Den_Code;
            else if (SCCode.CompareTo(So6DenA_Code) == 1)
                Return = So6DenA_Code;
            else if (SCCode.CompareTo(So6DenB_Code) == 1)
                Return = So6DenB_Code;
            else if (SCCode.CompareTo(So6DenC_Code) == 1)
                Return = So6DenC_Code;
            else
                Return = "";
            return Return;
        }

        public static string GetNameBySecurityControlCode(string SCCode)
        {
            var Return = "";
            switch (SCCode)
            {
                case "1":
                    Return = So1Do_Name;
                    break;
                case "4":
                    Return = So4Do_Name;
                    break;
                case "1d":
                    Return = So1Den_Name;
                    break;
                case "1dA":
                    Return = So1DenA_Name;
                    break;
                case "1dB":
                    Return = So1DenB_Name;
                    break;
                case "1dC":
                    Return = So1DenC_Name;
                    break;
                case "1dD":
                    Return = So1DenD_Name;
                    break;
                case "1dE":
                    Return = So1DenE_Name;
                    break;
                case "2d":
                    Return = So2Den_Name;
                    break;
                case "2dA":
                    Return = So2DenA_Name;
                    break;
                case "2dB":
                    Return = So2DenB_Name;
                    break;
                case "2dC":
                    Return = So2DenC_Name;
                    break;
                case "3d":
                    Return = So3Den_Name;
                    break;
                case "3dA":
                    Return = So3DenA_Name;
                    break;
                case "3dB":
                    Return = So3DenB_Name;
                    break;
                case "3dC":
                    Return = So3DenC_Name;
                    break;
                case "4d":
                    Return = So4Den_Name;
                    break;
                case "5d":
                    Return = So5Den_Name;
                    break;
                case "5dA":
                    Return = So5DenA_Name;
                    break;
                case "5dB":
                    Return = So5DenB_Name;
                    break;
                case "5dC":
                    Return = So5DenC_Name;
                    break;
                case "5dD":
                    Return = So5DenD_Name;
                    break;
                case "5dE":
                    Return = So5DenE_Name;
                    break;
                case "6d":
                    Return = So6Den_Name;
                    break;
                case "6dA":
                    Return = So6DenA_Name;
                    break;
                case "6dB":
                    Return = So6DenB_Name;
                    break;
                case "6dC":
                    Return = So6DenC_Name;
                    break;

                default:
                    Return = "";
                    break;
            }
            return Return;
        }

        public static string GetDescriptionBySecurityControlCode(string SCCode)
        {
            var Return = "";
            switch (SCCode)
            {
                case "1":
                    Return = So1Do_Description;
                    break;
                case "4":
                    Return = So4Do_Description;
                    break;
                case "1d":
                    Return = So1Den_Description;
                    break;
                case "1dA":
                    Return = So1DenA_Description;
                    break;
                case "1dB":
                    Return = So1DenB_Description;
                    break;
                case "1dC":
                    Return = So1DenC_Description;
                    break;
                case "1dD":
                    Return = So1DenD_Description;
                    break;
                case "1dE":
                    Return = So1DenE_Description;
                    break;
                case "2d":
                    Return = So2Den_Description;
                    break;
                case "2dA":
                    Return = So2DenA_Description;
                    break;
                case "2dB":
                    Return = So2DenB_Description;
                    break;
                case "2dC":
                    Return = So2DenC_Description;
                    break;
                case "3d":
                    Return = So3Den_Description;
                    break;
                case "3dA":
                    Return = So3DenA_Description;
                    break;
                case "3dB":
                    Return = So3DenB_Description;
                    break;
                case "3dC":
                    Return = So3DenC_Description;
                    break;
                case "4d":
                    Return = So4Den_Description;
                    break;
                case "5d":
                    Return = So5Den_Description;
                    break;
                case "5dA":
                    Return = So5DenA_Description;
                    break;
                case "5dB":
                    Return = So5DenB_Description;
                    break;
                case "5dC":
                    Return = So5DenC_Description;
                    break;
                case "5dD":
                    Return = So5DenD_Description;
                    break;
                case "5dE":
                    Return = So5DenE_Description;
                    break;
                case "6d":
                    Return = So6Den_Description;
                    break;
                case "6dA":
                    Return = So6DenA_Description;
                    break;
                case "6dB":
                    Return = So6DenB_Description;
                    break;
                case "6dC":
                    Return = So6DenC_Description;
                    break;

                default:
                    Return = "";
                    break;
            }
            return Return;
        }

        #endregion

        #region

        public static List<Unit> ListScaleOfSalaryLevel()
        {
            var _list = new List<Unit>();
            _list.Add(new Unit(1, "Mức 1"));
            _list.Add(new Unit(2, "Mức 2"));
            _list.Add(new Unit(3, "Mức 3"));
            return _list;
        }

        public static List<Unit> ListCustomPercent()
        {
            var _list = new List<Unit>();
            _list.Add(new Unit(0.7, "0.7"));
            _list.Add(new Unit(0.9, "0.7"));
            _list.Add(new Unit(1, ""));
            return _list;
        }

        #endregion
        public static string ERROR_LEAVE_LEAVEWD_NAME = "PHEP";
        public static string ERROR_NCQD_NCDC_NAME = "NCQD vs NCDC";
        public static string ERROR_NGHITUAN_NAME = "NGHI TUAN";
        public static string ERROR_WORKING_DAYS_NAME = "CHUA CHAM CONG hoac CHUA DI LAM";

        public static int ERROR_LEAVE_LEAVEWD_ID = 2;
        public static int ERROR_NCQD_NCDC_ID = 1;
        public static int ERROR_NGHITUAN_ID = 0;
        public static int ERROR_WORKING_DAYS_ID = 3;
    }

    #region Class RType

    public class RType
    {
        public RType(int rType, string rTypeName)
        {
            RTypeId = rType;
            RTypeName = rTypeName;
        }

        public RType(int rType, string rTypeName, string rTypeURL)
        {
            RTypeId = rType;
            RTypeName = rTypeName;
            RTypeURL = rTypeURL;
        }

        public int RTypeId { get; set; }

        public string RTypeName { get; set; }

        public string RTypeURL { get; set; }
    }

    #endregion

    #region Class EmpLeaveType

    public class EmpLeaveType
    {
        public EmpLeaveType(int EmpId, int EmpLeaveId)
        {
            this.EmpId = EmpId;
            this.EmpLeaveId = EmpLeaveId;
        }

        public int EmpId { get; set; }

        public int EmpLeaveId { get; set; }
    }

    #endregion

    #region SecurityControl

    public class SecurityControl
    {
        public SecurityControl(string SCCode, string SCName, string AreaDescription)
        {
            this.SCCode = SCCode;
            this.SCName = SCName;
            this.AreaDescription = AreaDescription;
        }

        public string AreaDescription { get; set; }

        public string SCCode { get; set; }

        public string SCName { get; set; }
    }

    #endregion
}