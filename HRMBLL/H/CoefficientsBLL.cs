using System;
using System.Collections.Generic;
using System.Data;
using HRMBLL.H.Helper;
using HRMDAL.H;
using HRMUtil.KeyNames.H;

namespace HRMBLL.H
{
    public class CoefficientsBLL
    {
        #region constructor

        public CoefficientsBLL(long coefficientId, double value, DateTime date)
        {
            CoefficientId = coefficientId;
            Value = value;
            Date = date;
        }

        #endregion

        #region methods insert, update, delete

        public long Save()
        {
            if (CoefficientId <= 0)
            {
                var objDAL = new CoefficientsDAL();
                return objDAL.Insert(CoefficientTypeId, UserCode, Value, Date);
            }
            return -1;
        }

        #endregion

        #region public methods Get

        public static List<CoefficientsBLL> GetByUserId_Monthly(int userId, int month, int year)
        {
            var objDAL = new CoefficientsDAL();
            return GenerateListCoefficientBLLFromDataTable(objDAL.GetByUserId_Monthly(userId, month, year));
        }

        #endregion

        #region private fields

        #endregion

        #region properties

        public long CoefficientId { get; set; }

        public double Value { get; set; }

        public DateTime Date { get; set; }

        public int CoefficientTypeId { get; set; }

        public string Description { get; set; }

        public string UserCode { get; set; }

        public string CoefficientName { get; set; }

        #endregion

        #region private methods, generate helper methods

        private static List<CoefficientsBLL> GenerateListCoefficientBLLFromDataTable(DataTable dt)
        {
            var list = new List<CoefficientsBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateCoefficientBLLFromDataRow(dr));

            return list;
        }

        private static CoefficientsBLL GenerateCoefficientBLLFromDataRow(DataRow dr)
        {
            var objCoefficientsBLL = new CoefficientsBLL(
                dr[CoefficientKeys.FIELD_COEFFICIENT_ID] == DBNull.Value
                    ? DefaultValues.CoefficientIdMinValue
                    : Convert.ToInt64(dr[CoefficientKeys.FIELD_COEFFICIENT_ID].ToString()),
                dr[CoefficientKeys.FIELD_COEFFICIENT_VALUE] == DBNull.Value
                    ? 0
                    : Convert.ToDouble(dr[CoefficientKeys.FIELD_COEFFICIENT_VALUE].ToString()),
                dr[CoefficientKeys.FIELD_COEFFICIENT_DATE] == DBNull.Value
                    ? DateTime.MinValue
                    : Convert.ToDateTime(dr[CoefficientKeys.FIELD_COEFFICIENT_DATE].ToString())
            );

            objCoefficientsBLL.CoefficientName = dr[CoefficientTypeKeys.FIELD_COEFFICIENT_TYPE_NAME] == DBNull.Value
                ? string.Empty
                : (string) dr[CoefficientTypeKeys.FIELD_COEFFICIENT_TYPE_NAME];
            objCoefficientsBLL.Description = dr[CoefficientTypeKeys.FIELD_COEFFICIENT_TYPE_DESCRIPTION] == DBNull.Value
                ? string.Empty
                : (string) dr[CoefficientTypeKeys.FIELD_COEFFICIENT_TYPE_DESCRIPTION];
            return objCoefficientsBLL;
        }

        #endregion
    }
}