namespace HRMUtil.KeyNames.H1
{
    public sealed class HTCVCatalogueKeys
    {
        /// <summary>
        ///     Some fields in H1_HTCVCatalogue table
        /// </summary>
        public const string Field_HTCVCatalogue_ID = "HTCVCatalogueId";

        public const string Field_HTCVCatalogue_Name = "HTCVCatalogueName";
        public const string Field_HTCVCatalogue_MarkDisplay = "MarkDisplay";
        public const string Field_HTCVCatalogue_MarkDefault = "MarkDefault";
        public const string Field_HTCVCatalogue_TypeDisplay = "TypeDisplay";

        public const string Field_HTCVCatalogue_MinMark = "MinMark";
        public const string Field_HTCVCatalogue_MaxMark = "MaxMark";
        public const string Field_HTCVCatalogue_ParentId = "ParentId";

        /// <summary>
        ///     store procedure for H1_HTCVCatalogue table
        /// </summary>
        public const string Sp_Ins_H1_HTCVCatalogue = "Ins_H1_HTCVCatalogue";

        public const string Sp_Upd_H1_HTCVCatalogue = "Upd_H1_HTCVCatalogue";
        public const string Sp_Del_H1_HTCVCatalogue = "Del_H1_HTCVCatalogue";

        public const string SP_Sel_H1_HTCVCatalogueByFilter = "Sel_H1_HTCVCatalogueByFilter";
        public const string SP_Sel_H1_HTCVCatalogueById = "Sel_H1_HTCVCatalogueById";
    }
}