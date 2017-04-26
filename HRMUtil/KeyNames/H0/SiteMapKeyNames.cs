namespace HRMUtil.KeyNames.H0
{
    public sealed class SiteMapKeyNames
    {
        /// <summary>
        ///     Some field names of SiteMap table
        /// </summary>
        public const string FIELD_SITE_MAP_ID = "SiteMapId";

        public const string FIELD_SITE_MAP_TITLE = "Title";
        public const string FIELD_SITE_MAP_URL = "Url";
        public const string FIELD_SITE_MAP_PARENTID = "ParentId";
        public const string FIELD_SITE_MAP_ROLEIDS = "RoleIds";


        /// <summary>
        ///     StoreProcedure name of SiteMap object.
        /// </summary>
        public const string SP_SITE_MAP_GET_BY_ROOTS = "Sel_SiteMap_By_Roots";

        public const string SP_SITE_MAP_GET_BY_PARENT_ID = "Sel_SiteMap_By_ParentId";
    }
}