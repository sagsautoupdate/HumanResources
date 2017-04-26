using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H0;
using HRMUtil.KeyNames.H0;

namespace HRMBLL.H0
{
    public class SiteMapBLL
    {
        #region constructor

        public SiteMapBLL(int siteMapId, string title, int parentId)
        {
            SiteMapId = siteMapId;
            Title = title;
            ParentId = parentId;
        }

        #endregion

        #region private fields

        #endregion

        #region properties

        public int SiteMapId { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public int ParentId { get; set; }

        public string RoleIds { get; set; }

        #endregion

        #region methods insert, update, delete

        #endregion

        #region public static method Get

        public static List<SiteMapBLL> GetAllRoots()
        {
            return GenerateListSiteMapBLLFromDataTable(new SiteMapDAL().GetAllRoots());
        }

        public static List<SiteMapBLL> GetByParentId(int parentId)
        {
            return GenerateListSiteMapBLLFromDataTable(new SiteMapDAL().GetByParentId(parentId));
        }

        #endregion

        #region private static methods

        private static List<SiteMapBLL> GenerateListSiteMapBLLFromDataTable(DataTable dt)
        {
            var list = new List<SiteMapBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateSiteMapBLLFromDataRow(dr));

            return list;
        }

        private static SiteMapBLL GenerateSiteMapBLLFromDataRow(DataRow dr)
        {
            var objBLL = new SiteMapBLL(
                dr[SiteMapKeyNames.FIELD_SITE_MAP_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[SiteMapKeyNames.FIELD_SITE_MAP_ID].ToString()),
                dr[SiteMapKeyNames.FIELD_SITE_MAP_TITLE] == DBNull.Value
                    ? string.Empty
                    : dr[SiteMapKeyNames.FIELD_SITE_MAP_TITLE].ToString(),
                dr[SiteMapKeyNames.FIELD_SITE_MAP_PARENTID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[SiteMapKeyNames.FIELD_SITE_MAP_PARENTID].ToString()));

            objBLL.Url = dr[SiteMapKeyNames.FIELD_SITE_MAP_URL] == DBNull.Value
                ? string.Empty
                : dr[SiteMapKeyNames.FIELD_SITE_MAP_URL].ToString();
            objBLL.RoleIds = dr[SiteMapKeyNames.FIELD_SITE_MAP_ROLEIDS] == DBNull.Value
                ? string.Empty
                : dr[SiteMapKeyNames.FIELD_SITE_MAP_ROLEIDS].ToString();

            return objBLL;
        }

        #endregion
    }
}