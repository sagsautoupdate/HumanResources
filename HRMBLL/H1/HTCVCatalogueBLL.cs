using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H1;
using HRMUtil;
using HRMUtil.KeyNames.H1;

namespace HRMBLL.H1
{
    public class HTCVCatalogueBLL
    {
        #region private fields

        #endregion

        #region properties

        public int HTCVCatalogueId { get; set; }

        public string HTCVCatalogueName { get; set; }

        public string MarkDisplay { get; set; }

        public double MarkDefault { get; set; }

        public int TypeDisplay { get; set; }

        public string TypeDisplayName { get; set; }

        public double MinMark { get; set; }

        public double MaxMark { get; set; }

        public double ParentId { get; set; }

        #endregion

        #region public methods insert, update, delete

        public int Save()
        {
            var objDAL = new HTCVCatalogueDAL();

            if (HTCVCatalogueId <= 0)
                return objDAL.Insert(HTCVCatalogueName, MarkDisplay, MarkDefault, TypeDisplay);
            return objDAL.Update(HTCVCatalogueName, MarkDisplay, MarkDefault, TypeDisplay, HTCVCatalogueId);
        }

        public static void Update(string hTCVCatalogueName, string markDisplay, double markDefault, int typeDisplay,
            int hTCVCatalogueId)
        {
            new HTCVCatalogueDAL().Update(hTCVCatalogueName, markDisplay, markDefault, typeDisplay, hTCVCatalogueId);
        }

        public static void Delete(int hTCVCatalogueId)
        {
            new HTCVCatalogueDAL().Delete(hTCVCatalogueId);
        }

        #endregion

        #region public methods GET

        public static List<HTCVCatalogueBLL> GetByFilter(string hTCVCatalogueName, string markDisplay,
            double markDefault, int typeDisplay)
        {
            return
                GenerateListHTCVCatalogueBLLFromDataTable(new HTCVCatalogueDAL().GetByFilter(hTCVCatalogueName,
                    markDisplay, markDefault, typeDisplay));
        }

        public static HTCVCatalogueBLL GetById(int hTCVCatalogueId)
        {
            var list = GenerateListHTCVCatalogueBLLFromDataTable(new HTCVCatalogueDAL().GetById(hTCVCatalogueId));
            if (list.Count == 1)
                return list[0];
            return null;
        }

        #endregion

        #region private methods

        private static List<HTCVCatalogueBLL> GenerateListHTCVCatalogueBLLFromDataTable(DataTable dt)
        {
            var list = new List<HTCVCatalogueBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateHTCVCatalogueBLLFromDataRow(dr));

            return list;
        }

        private static HTCVCatalogueBLL GenerateHTCVCatalogueBLLFromDataRow(DataRow dr)
        {
            var objBLL = new HTCVCatalogueBLL();

            objBLL.HTCVCatalogueId = dr[HTCVCatalogueKeys.Field_HTCVCatalogue_ID] == DBNull.Value
                ? 0
                : int.Parse(dr[HTCVCatalogueKeys.Field_HTCVCatalogue_ID].ToString());
            objBLL.HTCVCatalogueName = dr[HTCVCatalogueKeys.Field_HTCVCatalogue_Name] == DBNull.Value
                ? string.Empty
                : dr[HTCVCatalogueKeys.Field_HTCVCatalogue_Name].ToString();
            objBLL.MarkDisplay = dr[HTCVCatalogueKeys.Field_HTCVCatalogue_MarkDisplay] == DBNull.Value
                ? string.Empty
                : dr[HTCVCatalogueKeys.Field_HTCVCatalogue_MarkDisplay].ToString();
            objBLL.MarkDefault = dr[HTCVCatalogueKeys.Field_HTCVCatalogue_MarkDefault] == DBNull.Value
                ? 0
                : double.Parse(dr[HTCVCatalogueKeys.Field_HTCVCatalogue_MarkDefault].ToString());
            objBLL.TypeDisplay = dr[HTCVCatalogueKeys.Field_HTCVCatalogue_TypeDisplay] == DBNull.Value
                ? 0
                : int.Parse(dr[HTCVCatalogueKeys.Field_HTCVCatalogue_TypeDisplay].ToString());
            objBLL.TypeDisplayName = Constants.GetHTCVCatalogueTypeName(objBLL.TypeDisplay);

            objBLL.MinMark = dr[HTCVCatalogueKeys.Field_HTCVCatalogue_MinMark] == DBNull.Value
                ? 0
                : double.Parse(dr[HTCVCatalogueKeys.Field_HTCVCatalogue_MinMark].ToString());
            objBLL.MaxMark = dr[HTCVCatalogueKeys.Field_HTCVCatalogue_MaxMark] == DBNull.Value
                ? 0
                : double.Parse(dr[HTCVCatalogueKeys.Field_HTCVCatalogue_MaxMark].ToString());
            objBLL.ParentId = dr[HTCVCatalogueKeys.Field_HTCVCatalogue_ParentId] == DBNull.Value
                ? 0
                : int.Parse(dr[HTCVCatalogueKeys.Field_HTCVCatalogue_ParentId].ToString());

            return objBLL;
        }

        #endregion
    }
}