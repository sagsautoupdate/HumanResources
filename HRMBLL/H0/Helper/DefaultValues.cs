using System;
using System.Data.SqlTypes;

namespace HRMBLL.H0.Helper
{
    public static class DefaultValues
    {
        public static int UserIdMinValue
        {
            get { return 0; }
        }

        public static DateTime DateTimeMinValue
        {
            get
            {
                var MinValue = (DateTime) SqlDateTime.MinValue;
                MinValue.AddYears(30);
                return MinValue;
            }
        }

        public static int StatusMinValue
        {
            get { return 0; }
        }

        public static bool MarriageDefaultValue
        {
            get { return false; }
        }

        public static int StandardLeaveMinValue
        {
            get { return 0; }
        }

        public static int ContractTypeIdMinValue
        {
            get { return 0; }
        }

        public static int DecisionTypeIdMinValue
        {
            get { return 0; }
        }

        public static int DepartmentIdMinValue
        {
            get { return 0; }
        }

        public static int PositionIdMinValue
        {
            get { return 0; }
        }

        public static int QualificationIdMinValue
        {
            get { return 0; }
        }

        #region constanst for EmployeeLeave

        public static int CONST_EMPLOYEE_LEAVE_O_CO_KHHDS = 1;
        public static int CONST_EMPLOYEE_LEAVE_O_DAI_NGAY = 2;
        public static int CONST_EMPLOYEE_LEAVE_THAI_SAN = 3;
        public static int CONST_EMPLOYEE_LEAVE_TNLD = 4;
        public static int CONST_EMPLOYEE_LEAVE_F_NAM = 5;
        public static int CONST_EMPLOYEE_LEAVE_Fdb = 6;
        public static int CONST_EMPLOYEE_LEAVE_Ko_LUONG_CLD = 7;
        public static int CONST_EMPLOYEE_LEAVE_Ko_LUONG_KLD = 8;
        public static int CONST_EMPLOYEE_LEAVE_F_DI_DUONG = 9;
        public static int CONST_EMPLOYEE_LEAVE_F_CONG_TAC = 10;
        public static int CONST_EMPLOYEE_LEAVE_HOC_1 = 12;
        public static int CONST_EMPLOYEE_LEAVE_HOC_2 = 13;
        public static int CONST_EMPLOYEE_LEAVE_HOC_3 = 14;
        public static int CONST_EMPLOYEE_LEAVE_HOC_4 = 15;
        public static int CONST_EMPLOYEE_LEAVE_HOC_5 = 16;
        public static int CONST_EMPLOYEE_LEAVE_HOC_6 = 17;
        public static int CONST_EMPLOYEE_LEAVE_HOC_7 = 18;

        #endregion
    }
}