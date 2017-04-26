using System;
using System.Collections.Generic;
using System.Data;
using HRMBLL.H1.Helper;
using HRMDAL.H1;
using HRMUtil.KeyNames.H1;

namespace HRMBLL.H1
{
    public class CoefficientValuesBLL
    {
        #region private fields

        /// <summary>
        ///     //////////
        /// </summary>
        private string _CoefficientName;

        #endregion

        #region properties

        public int CoefficientValueId { get; set; }

        public int CoefficientNameId { get; set; }

        public int CoefficientLevelId { get; set; }

        public double CoefficientValue { get; set; }

        public double Conditions { get; set; }

        public string CoefficientName
        {
            get { return _CoefficientName; }
            set { _CoefficientName = value; }
        }

        public string Description { get; set; }

        public int SalaryRegulationId { get; set; }

        public int Type { get; set; }

        /// <summary>
        /// </summary>

        #endregion

        #region Constructor
        public CoefficientValuesBLL(int coefficientValueId, int coefficientNameId, int coefficientLevelId,
            double coefficientValue)
        {
            CoefficientValueId = coefficientValueId;
            CoefficientNameId = coefficientNameId;
            CoefficientLevelId = coefficientLevelId;
            CoefficientValue = coefficientValue;
        }

        #endregion

        #region public method Insert, Update, Delete

        public static long Insert(string coefficientName,
            double value_Level_1, double conditions1, double value_Level_2, double conditions2, double value_Level_3,
            double conditions3,
            double value_Level_4, double conditions4, double value_Level_5, double conditions5, double value_Level_6,
            double conditions6,
            double value_Level_7, double conditions7, double value_Level_8, double conditions8, double value_Level_9,
            double conditions9,
            double value_Level_10, double conditions10, double value_Level_11, double conditions11,
            double value_Level_12, double conditions12,
            string coefficientNameDescription, int type, int salaryRegulation)
        {
            var objDAL = new CoefficientValuesDAL();
            return objDAL.Insert(coefficientName,
                value_Level_1, conditions1, value_Level_2, conditions2, value_Level_3, conditions3,
                value_Level_4, conditions4, value_Level_5, conditions5, value_Level_6, conditions6,
                value_Level_7, conditions7, value_Level_8, conditions8, value_Level_9, conditions9,
                value_Level_10, conditions10, value_Level_11, conditions11, value_Level_12, conditions12,
                coefficientNameDescription, type, salaryRegulation);
        }

        public static long Update(string coefficientName,
            double value_Level_1, double conditions1, double value_Level_2, double conditions2, double value_Level_3,
            double conditions3,
            double value_Level_4, double conditions4, double value_Level_5, double conditions5, double value_Level_6,
            double conditions6,
            double value_Level_7, double conditions7, double value_Level_8, double conditions8, double value_Level_9,
            double conditions9,
            double value_Level_10, double conditions10, double value_Level_11, double conditions11,
            double value_Level_12, double conditions12,
            string coefficientNameDescription, int type, int salaryRegulation, int coefficientNameId)
        {
            var objDAL = new CoefficientValuesDAL();
            return objDAL.Update(coefficientName,
                value_Level_1, conditions1, value_Level_2, conditions2, value_Level_3, conditions3,
                value_Level_4, conditions4, value_Level_5, conditions5, value_Level_6, conditions6,
                value_Level_7, conditions7, value_Level_8, conditions8, value_Level_9, conditions9,
                value_Level_10, conditions10, value_Level_11, conditions11, value_Level_12, conditions12,
                coefficientNameDescription, type, salaryRegulation, coefficientNameId);
        }

        public static long DeleteByNameId(int coefficientNameId)
        {
            var objDAL = new CoefficientValuesDAL();
            return objDAL.DeleteByNameId(coefficientNameId);
        }

        #endregion

        #region public method Get

        public static List<CoefficientValuesBLL> GetAll(int type)
        {
            var objDAL = new CoefficientValuesDAL();
            return GenerateListCoefficientValuesBLLFromDataTable(objDAL.GetAll(type));
        }

        public static CoefficientValuesBLL GetByName_Level(int coefficientNameId, int coefficientLevelId)
        {
            var objDAL = new CoefficientValuesDAL();

            var dt = objDAL.GetByName_Level(coefficientNameId, coefficientLevelId);

            if (dt.Rows.Count > 0)
                return GenerateCoefficientValuesBLLFromDataRow(dt.Rows[0]);
            return null;
        }

        public static List<CoefficientValuesBLL> GetByNameId(int coefficientNameId)
        {
            var objDAL = new CoefficientValuesDAL();
            return GenerateListCoefficientValuesBLLFromDataTable(objDAL.GetByNameId(coefficientNameId));
        }

        #endregion

        #region private method

        private static List<CoefficientValuesBLL> GenerateListCoefficientValuesBLLFromDataTable(DataTable dt)
        {
            var list = new List<CoefficientValuesBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateCoefficientValuesBLLFromDataRow(dr));

            return list;
        }

        private static CoefficientValuesBLL GenerateCoefficientValuesBLLFromDataRow(DataRow dr)
        {
            var objBLL = new CoefficientValuesBLL(
                dr[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE_ID] == DBNull.Value
                    ? DefaultValues.CoefficientValueIdMinValue
                    : int.Parse(dr[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE_ID].ToString()),
                dr[CoefficientNameKeys.Field_CoefficientNames_CoefficientNameId] == DBNull.Value
                    ? DefaultValues.CoefficientNameIdMinValue
                    : int.Parse(dr[CoefficientNameKeys.Field_CoefficientNames_CoefficientNameId].ToString()),
                dr[CoefficientLevelKeys.FIELD_COEFFICIENT_LEVEL_ID] == DBNull.Value
                    ? DefaultValues.CoefficientLevelIdMinValue
                    : int.Parse(dr[CoefficientLevelKeys.FIELD_COEFFICIENT_LEVEL_ID].ToString()),
                dr[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE] == DBNull.Value
                    ? 0
                    : double.Parse(dr[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE].ToString()));

            objBLL.Conditions = dr[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS] == DBNull.Value
                ? 0
                : double.Parse(dr[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS].ToString());

            objBLL.CoefficientName = dr[CoefficientNameKeys.Field_CoefficientNames_CoefficientName] == DBNull.Value
                ? string.Empty
                : dr[CoefficientNameKeys.Field_CoefficientNames_CoefficientName].ToString();
            objBLL.Description = dr[CoefficientNameKeys.Field_CoefficientNames_CoefficientNameDescription] ==
                                 DBNull.Value
                ? string.Empty
                : dr[CoefficientNameKeys.Field_CoefficientNames_CoefficientNameDescription].ToString();
            objBLL.SalaryRegulationId = dr[CoefficientNameKeys.Field_CoefficientNames_SalaryRegulationId] ==
                                        DBNull.Value
                ? 0
                : int.Parse(dr[CoefficientNameKeys.Field_CoefficientNames_SalaryRegulationId].ToString());
            objBLL.Type = dr[CoefficientNameKeys.Field_CoefficientNames_Type] == DBNull.Value
                ? 0
                : int.Parse(dr[CoefficientNameKeys.Field_CoefficientNames_Type].ToString());
            return objBLL;
        }

        #endregion
    }
}