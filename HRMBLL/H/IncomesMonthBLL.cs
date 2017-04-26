using System;
using System.Collections.Generic;
using System.Data;
using HRMBLL.H.Helper;
using HRMDAL.H;
using HRMUtil.KeyNames.H;

namespace HRMBLL.H
{
    public class IncomesMonthBLL
    {
        #region constructor

        public IncomesMonthBLL(long incomeMonthId, double value, DateTime date)
        {
            IncomeMonthId = incomeMonthId;
            Value = value;
            Date = date;
        }

        #endregion

        #region methods insert, update, delete

        public long Save()
        {
            if (IncomeMonthId <= 0)
            {
                var objDAL = new IncomesMonthDAL();
                return objDAL.Insert(IncomeTypeId, Type, UserCode, Value, Date);
            }
            return -1;
        }

        #endregion

        #region private fields

        #endregion

        #region properties

        public long IncomeMonthId { get; set; }

        public double Value { get; set; }

        public DateTime Date { get; set; }

        public int IncomeTypeId { get; set; }

        public string Description { get; set; }

        public string UserCode { get; set; }

        public bool Type { get; set; }

        private string IncomeName { get; set; }

        #endregion

        #region public methods Get

        public static List<IncomesMonthBLL> GetByUserId_Monthly(int userId, int month, int year, bool type)
        {
            var objDAL = new IncomesMonthDAL();
            return GenerateListIncomesMonthBLLFromDataTable(objDAL.GetByUserId_Monthly(userId, month, year, type));
        }

        public static List<IncomesMonthBLL> GetAllByUserId_Monthly(int userId, int month, int year)
        {
            var objDAL = new IncomesMonthDAL();
            return GenerateListIncomesMonthBLLFromDataTable(objDAL.GetAllByUserId_Monthly(userId, month, year));
        }

        #endregion

        #region private methods, generate helper methods

        private static List<IncomesMonthBLL> GenerateListIncomesMonthBLLFromDataTable(DataTable dt)
        {
            var list = new List<IncomesMonthBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateIncomesMonthBLLFromDataRow(dr));

            return list;
        }

        private static IncomesMonthBLL GenerateIncomesMonthBLLFromDataRow(DataRow dr)
        {
            var objBLL = new IncomesMonthBLL(
                dr[IncomeMonthKeys.FIELD_INCOME_MONTH_ID] == DBNull.Value
                    ? DefaultValues.IncomeMonthIdMinValue
                    : Convert.ToInt64(dr[IncomeMonthKeys.FIELD_INCOME_MONTH_ID].ToString()),
                dr[IncomeMonthKeys.FIELD_INCOME_MONTH_VALUE] == DBNull.Value
                    ? 0
                    : Convert.ToDouble(dr[IncomeMonthKeys.FIELD_INCOME_MONTH_VALUE].ToString()),
                dr[IncomeMonthKeys.FIELD_INCOME_MONTH_DATE] == DBNull.Value
                    ? DateTime.MinValue
                    : Convert.ToDateTime(dr[IncomeMonthKeys.FIELD_INCOME_MONTH_DATE].ToString())
            );

            objBLL.IncomeName = dr[IncomeTypeKeys.FIELD_INCOME_TYPE_NAME] == DBNull.Value
                ? string.Empty
                : (string) dr[IncomeTypeKeys.FIELD_INCOME_TYPE_NAME];
            objBLL.Description = dr[IncomeTypeKeys.FIELD_INCOME_TYPE_DESCRIPTION] == DBNull.Value
                ? string.Empty
                : (string) dr[IncomeTypeKeys.FIELD_INCOME_TYPE_DESCRIPTION];

            try
            {
                objBLL.IncomeTypeId = dr[IncomeTypeKeys.FIELD_INCOME_TYPE_ID] == DBNull.Value
                    ? DefaultValues.IncomeMonthIdMinValue
                    : int.Parse(dr[IncomeTypeKeys.FIELD_INCOME_TYPE_ID].ToString());
            }
            catch
            {
            }

            return objBLL;
        }

        #endregion
    }
}