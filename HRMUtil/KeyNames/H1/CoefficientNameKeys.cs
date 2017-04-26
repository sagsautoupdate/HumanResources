namespace HRMUtil.KeyNames.H1
{
    public sealed class CoefficientNameKeys
    {
        /// <summary>
        ///     Some fields in H1_LNS_CoefficientNames
        /// </summary>
        public const string Field_CoefficientNames_CoefficientNameId = "CoefficientNameId";

        public const string Field_CoefficientNames_CoefficientName = "CoefficientName";
        public const string Field_CoefficientNames_CoefficientNameDescription = "CoefficientNameDescription";
        public const string Field_CoefficientNames_SalaryRegulationId = "SalaryRegulationId";
        public const string Field_CoefficientNames_Type = "Type";

        /// <summary>
        ///     some store procedurces for H1_LNS_CoefficientNames
        /// </summary>
        //public const string SP_COEFFICIENT_NAME_INSERT = "Ins_H1_CoefficientName";
        //public const string SP_COEFFICIENT_NAME_UPDATE = "Upd_H1_CoefficientName";
        //public const string SP_COEFFICIENT_NAME_DELETE = "Del_H1_CoefficientName";
        public const string Sp_Sel_H1_CoefficientNames_All = "Sel_H1_CoefficientNames_All";

        public const string Sp_Sel_H1_CoefficientNamesByInUseTypeId = "Sel_H1_CoefficientNamesByInUseTypeId";

        public const int CONST_LNS_COEFFICIENT_NAME_POSITION_TYPE = 0;
        public const int CONST_LCB_COEFFICIENT_NAME_TYPE = 1;
        public const int CONST_LNS_COEFFICIENT_NAME_FIXEDRATE_TYPE = 2;
    }
}