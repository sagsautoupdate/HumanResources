using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H1;
using HRMUtil.KeyNames.H0;
using HRMUtil.KeyNames.H1;

namespace HRMBLL.H1
{
    public class CoefficientNamePositionsBLL
    {
        #region Constructor

        public CoefficientNamePositionsBLL(int coefficientNamePositionId, int coefficientNameId, int positionId)
        {
            CoefficientNamePositionId = coefficientNamePositionId;
            CoefficientNameId = coefficientNameId;
            PositionId = positionId;
        }

        #endregion

        #region private fields

        #endregion

        #region properties

        public int CoefficientNamePositionId { get; set; }

        public int CoefficientNameId { get; set; }

        public int PositionId { get; set; }

        public string PositionName { get; set; }

        #endregion

        #region public method Get

        public static List<CoefficientNamePositionsBLL> GetByNameId(int coefficientNameId)
        {
            var objDAL = new CoefficientNamePositionsDAL();
            return GenerateListCoefficientNamePositionsBLLFromDataTable(objDAL.GetByNameId(coefficientNameId));
        }

        public static CoefficientNamePositionsBLL GetByPositionId(int positionId, int type)
        {
            var objDAL = new CoefficientNamePositionsDAL();
            var dt = objDAL.GetByPositionId(positionId, type);
            if (dt.Rows.Count == 1)
                return GenerateCoefficientNamePositionsBLLFromDataRow(dt.Rows[0]);
            return null;
        }

        #endregion

        #region private methods

        private static List<CoefficientNamePositionsBLL> GenerateListCoefficientNamePositionsBLLFromDataTable(
            DataTable dt)
        {
            var list = new List<CoefficientNamePositionsBLL>();
            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateCoefficientNamePositionsBLLFromDataRow(dr));

            return list;
        }

        private static CoefficientNamePositionsBLL GenerateCoefficientNamePositionsBLLFromDataRow(DataRow dr)
        {
            var objBLL = new CoefficientNamePositionsBLL(
                dr[CoefficientNamePositionKeys.FIELD_COEFFICIENT_NAME_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CoefficientNamePositionKeys.FIELD_COEFFICIENT_NAME_POSITION_ID].ToString()),
                dr[CoefficientNameKeys.Field_CoefficientNames_CoefficientNameId] == DBNull.Value
                    ? 0
                    : int.Parse(dr[CoefficientNameKeys.Field_CoefficientNames_CoefficientNameId].ToString()),
                dr[PositionKeys.FIELD_POSITION_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[PositionKeys.FIELD_POSITION_ID].ToString())
            );

            objBLL.PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                ? string.Empty
                : dr[PositionKeys.FIELD_POSITION_NAME].ToString();

            return objBLL;
        }

        #endregion
    }
}