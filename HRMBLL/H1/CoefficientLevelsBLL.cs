using System;
using System.Collections.Generic;
using System.Data;
using HRMBLL.H1.Helper;
using HRMDAL.H1;
using HRMUtil.KeyNames.H1;

namespace HRMBLL.H1
{
    public class CoefficientLevelsBLL
    {
        #region Constructor

        public CoefficientLevelsBLL(int coefficientLevelId, string coefficientLevelName)
        {
            CoefficientLevelId = coefficientLevelId;
            CoefficientLevelName = coefficientLevelName;
        }

        #endregion

        #region private fields

        #endregion

        #region properties

        public int CoefficientLevelId { get; set; }

        public string CoefficientLevelName { get; set; }

        #endregion

        #region public methods GET

        public static List<CoefficientLevelsBLL> GetAll(int type)
        {
            var objDAL = new CoefficientLevelsDAL();

            return GenerateListCoefficientLevelsBLLFromDataTable(objDAL.GetAll(type));
        }

        /// <summary>
        ///     Author: Giang
        ///     Date: 12/2/2014
        ///     Content: Tra ve DT
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable GetAllToDT(int type)
        {
            var objDAL = new CoefficientLevelsDAL();

            return objDAL.GetAll(type);
        }

        #endregion

        #region private methods

        private static List<CoefficientLevelsBLL> GenerateListCoefficientLevelsBLLFromDataTable(DataTable dt)
        {
            var list = new List<CoefficientLevelsBLL>();

            var objNone = new CoefficientLevelsBLL(0, "");

            list.Add(objNone);
            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateCoefficientLevelsBLLFromDataRow(dr));

            return list;
        }

        private static CoefficientLevelsBLL GenerateCoefficientLevelsBLLFromDataRow(DataRow dr)
        {
            var obj = new CoefficientLevelsBLL(
                dr[CoefficientLevelKeys.FIELD_COEFFICIENT_LEVEL_ID] == DBNull.Value
                    ? DefaultValues.CoefficientLevelIdMinValue
                    : int.Parse(dr[CoefficientLevelKeys.FIELD_COEFFICIENT_LEVEL_ID].ToString()),
                dr[CoefficientLevelKeys.FIELD_COEFFICIENT_LEVEL_NAME] == DBNull.Value
                    ? string.Empty
                    : (string) dr[CoefficientLevelKeys.FIELD_COEFFICIENT_LEVEL_NAME]);

            return obj;
        }

        #endregion
    }
}