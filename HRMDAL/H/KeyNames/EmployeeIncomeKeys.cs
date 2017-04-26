using System;
using System.Text;

namespace HRMDAL.H.KeyNames
{
    public sealed class EmployeeIncomeKeys
    {
        /// <summary>
        /// Some field names of EmployeeIncome table
        /// </summary>
        public const string FIELD_EMPLOYEE_INCOME_ID = "EmployeeIncomeId";
        public const string FIELD_EMPLOYEE_INCOME_USER_ID = "UserId";
        public const string FIELD_EMPLOYEE_INCOME_DATE = "Date";
        public const string FIELD_EMPLOYEE_INCOME_TOTAL_INC = "Total_Inc";
        public const string FIELD_EMPLOYEE_INCOME_TOTAL_CNTR = "Total_Cntr";
        public const string FIELD_EMPLOYEE_INCOME_TOTAL_INC_LK = "Total_Inc_LK";
        public const string FIELD_EMPLOYEE_INCOME_TOTAL_CNTR_LK = "Total_Cntr_LK";
        public const string FIELD_EMPLOYEE_INCOME_REAL_INCOME = "RealIncome";

        public const string FIELD_EMPLOYEE_INCOME_ACCOUNT_NO = "AccountNo";
        public const string FIELD_EMPLOYEE_INCOME_CARD_NO = "CardNo";
        public const string FIELD_EMPLOYEE_INCOME_DEPARTMENT_NAME = "DepartmentName";
        public const string FIELD_EMPLOYEE_INCOME_DEPARTMENT_ID = "DepartmentId";

        /// <summary>
        /// Some StoreProcedure names of EmployeeIncome table.
        /// </summary>
        public const string SP_EMPLOYEE_INCOME_GET_BY_USERID_MONTHLY = "Sel_H_EmployeeIncomeByUserId_Monthly";
        public const string SP_EMPLOYEE_INCOME_GET_BY_FILTER = "Sel_H_EmployeeIncomeByFilter";

        public const string SP_EMPLOYEE_INCOME_INSERT = "Ins_EmployeeIncome";
    }
}
