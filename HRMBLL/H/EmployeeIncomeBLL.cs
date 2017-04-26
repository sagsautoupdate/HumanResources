using System;
using System.Collections.Generic;
using System.Data;
using HRMBLL.H.Helper;
using HRMDAL.H;
using HRMUtil.KeyNames.H;
using HRMUtil.KeyNames.H0;

namespace HRMBLL.H
{
    public class EmployeeIncomeBLL
    {
        #region constructor

        public EmployeeIncomeBLL(int employeeIncomeId, int userId, DateTime date, decimal total_Inc, decimal total_Cntr,
            decimal total_Inc_LK, decimal total_Cntr_LK)
        {
            EmployeeIncomeId = employeeIncomeId;
            UserId = userId;
            Date = date;
            Total_Inc = total_Inc;
            Total_Cntr = total_Cntr;
            Total_Inc_LK = total_Inc_LK;
            Total_Cntr_LK = total_Cntr_LK;
        }

        #endregion

        #region Import total

        public static long InsertByImporting(string userCode, double total_Inc, double total_Cntr, double total_Real,
            double total_Inc_LK, double total_Cntr_LK, DateTime date)
        {
            var objDAL = new EmployeeIncomeDAL();
            return objDAL.Insert(userCode, total_Inc, total_Cntr, total_Real, total_Inc_LK, total_Cntr_LK, date);
        }

        #endregion

        #region private fields

        private string _UserName;
        private string _EmployeeCode;

        private string _FullName;

        /// <summary>
        ///     //////////////////
        /// </summary>
        private decimal _LNS;

        #endregion

        #region properties

        public int EmployeeIncomeId { get; set; }

        public int UserId { get; set; }

        public string UserName
        {
            get
            {
                if (string.IsNullOrEmpty(_UserName))
                    return string.Empty;
                return _UserName;
            }
            set { _UserName = value; }
        }

        public string EmployeeCode
        {
            get
            {
                if (string.IsNullOrEmpty(_EmployeeCode))
                    return string.Empty;
                return _EmployeeCode;
            }
            set { _EmployeeCode = value; }
        }

        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(_FullName))
                    return string.Empty;
                return _FullName;
            }
            set { _FullName = value; }
        }

        public DateTime Date { get; set; }

        public decimal Total_Inc { get; set; }

        public decimal Total_Cntr { get; set; }

        public decimal Total_Real { get; set; }

        public decimal Total_Inc_LK { get; set; }

        public decimal Total_Cntr_LK { get; set; }

        public decimal RealIncome { get; set; }

        public string AccountNo { get; set; }

        public string CardNo { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public decimal LNS
        {
            get { return _LNS; }
            set { _LNS = value; }
        }

        public decimal TienAn { get; set; }

        public decimal BoSungLuong { get; set; }

        public decimal TienThuong { get; set; }

        public decimal ThueThuNhap { get; set; }

        #endregion

        #region public methods Get

        public static EmployeeIncomeBLL GetByUserId_Monthly(int userId, int month, int year)
        {
            var objDAL = new EmployeeIncomeDAL();

            var dt = objDAL.GetByUserId_Monthly(userId, month, year);
            if (dt.Rows.Count > 0)
                return GenerateEmployeeIncomeBLLFromDataRow(dt.Rows[0]);
            return null;
        }

        public static List<EmployeeIncomeBLL> GetByFilter(string fullName, int departmentId, int month, int year)
        {
            return
                GenerateListEmployeeIncomeBLLFromDataTable(new EmployeeIncomeDAL().GetByFilter(fullName, departmentId,
                    month, year));
        }

        public static List<EmployeeIncomeBLL> GetAllByFilter(string fullName, int departmentId, int month, int year)
        {
            return
                GenerateAllListEmployeeIncomeBLLFromDataTable(new EmployeeIncomeDAL().GetByFilter(fullName, departmentId,
                    month, year));
        }


        public static DataTable GetStatisticTotalByFilter(int departmentId, int month, int year)
        {
            return new EmployeeIncomeDAL().GetStatisticTotalByFilter(departmentId, month, year);
        }

        #endregion

        #region private methods, generate helper methods

        private static List<EmployeeIncomeBLL> GenerateAllListEmployeeIncomeBLLFromDataTable(DataTable dt)
        {
            var list = new List<EmployeeIncomeBLL>();

            foreach (DataRow dr in dt.Rows)
            {
                var obj = GenerateEmployeeIncomeBLLFromDataRow(dr);
                var listIM = IncomesMonthBLL.GetAllByUserId_Monthly(obj.UserId, obj.Date.Month, obj.Date.Year);
                foreach (var objIM in listIM)
                    switch (objIM.IncomeTypeId)
                    {
                        case 1:
                            obj.LNS = (decimal) objIM.Value;
                            break;
                        case 7:
                            obj.TienAn = (decimal) objIM.Value;
                            break;
                        case 8:
                            obj.BoSungLuong = (decimal) objIM.Value;
                            break;
                        case 11:
                            obj.TienThuong = (decimal) objIM.Value;
                            break;
                        case 18:
                            obj.ThueThuNhap = (decimal) objIM.Value;
                            break;
                    }
                list.Add(obj);
            }

            return list;
        }

        private static List<EmployeeIncomeBLL> GenerateListEmployeeIncomeBLLFromDataTable(DataTable dt)
        {
            var list = new List<EmployeeIncomeBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateEmployeeIncomeBLLFromDataRow(dr));

            return list;
        }

        private static EmployeeIncomeBLL GenerateEmployeeIncomeBLLFromDataRow(DataRow dr)
        {
            var objBLL = new EmployeeIncomeBLL(
                dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_ID] == DBNull.Value
                    ? DefaultValues.EmployeeIncomeIdMinValue
                    : Convert.ToInt32(dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_ID].ToString()),
                dr[EmployeeKeys.FIELD_EMPLOYEES_USERID] == DBNull.Value
                    ? DefaultValues.UserIdMinValue
                    : Convert.ToInt32(dr[EmployeeKeys.FIELD_EMPLOYEES_USERID].ToString()),
                dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_DATE] == DBNull.Value
                    ? DateTime.MinValue
                    : Convert.ToDateTime(dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_DATE].ToString()),
                dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_TOTAL_INC] == DBNull.Value
                    ? 0
                    : Convert.ToDecimal(dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_TOTAL_INC].ToString()),
                dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_TOTAL_CNTR] == DBNull.Value
                    ? 0
                    : Convert.ToDecimal(dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_TOTAL_CNTR].ToString()),
                dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_TOTAL_INC_LK] == DBNull.Value
                    ? 0
                    : Convert.ToDecimal(dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_TOTAL_INC_LK].ToString()),
                dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_TOTAL_CNTR_LK] == DBNull.Value
                    ? 0
                    : Convert.ToDecimal(dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_TOTAL_CNTR_LK].ToString())
            );

            objBLL.Total_Real = dr[EmployeeIncomeKeys.Field_Employee_Income_Total_Real] == DBNull.Value
                ? 0
                : Convert.ToDecimal(dr[EmployeeIncomeKeys.Field_Employee_Income_Total_Real].ToString());

            objBLL.UserName = dr[EmployeeKeys.FIELD_EMPLOYEES_USERNAME] == DBNull.Value
                ? string.Empty
                : dr[EmployeeKeys.FIELD_EMPLOYEES_USERNAME].ToString();
            objBLL.FullName = dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                ? string.Empty
                : dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();
            objBLL.EmployeeCode = dr[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE] == DBNull.Value
                ? string.Empty
                : dr[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE].ToString();


            objBLL.RealIncome = dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_REAL_INCOME] == DBNull.Value
                ? 0
                : Convert.ToDecimal(dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_REAL_INCOME].ToString());

            objBLL.AccountNo = dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_ACCOUNT_NO] == DBNull.Value
                ? string.Empty
                : dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_ACCOUNT_NO].ToString();
            objBLL.CardNo = dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_CARD_NO] == DBNull.Value
                ? string.Empty
                : dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_CARD_NO].ToString();

            objBLL.DepartmentId = dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_DEPARTMENT_ID] == DBNull.Value
                ? 0
                : int.Parse(dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_DEPARTMENT_ID].ToString());
            objBLL.DepartmentName = dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_DEPARTMENT_NAME] == DBNull.Value
                ? string.Empty
                : dr[EmployeeIncomeKeys.FIELD_EMPLOYEE_INCOME_DEPARTMENT_NAME].ToString();

            return objBLL;
        }

        #endregion
    }
}