namespace HRMUtil.KeyNames.H1
{
    public sealed class CoefficientValueKeys
    {
        /// <summary>
        ///     Some fields in H1_LNS_CoefficientValues
        /// </summary>
        public const string FIELD_COEFFICIENT_VALUE_ID = "CoefficientValueId";

        public const string FIELD_COEFFICIENT_VALUE = "CoefficientValue";
        public const string FIELD_COEFFICIENT_CONDITIONS = "Conditions";
        public const string Field_Coefficient_LCB = "LCB";

        /// <summary>
        ///     some store procedurces for H1_LNS_CoefficientValues
        /// </summary>
        public const string Sp_Ins_H1_CoefficientValue = "Ins_H1_CoefficientValue";

        public const string Sp_Upd_H1_CoefficientValue = "Upd_H1_CoefficientValue";
        public const string SP_COEFFICIENT_VALUE_DELETE_BY_NAME_ID = "Del_H1_CoefficientValue_By_NameId";

        /// <summary>
        ///     Author: Giang
        ///     Content:
        /// </summary>
        public const string SP_COEFFICIENT_VALUE_GET_BY_TYPE_AND_REGULATIONID =
            "Sel_H1_CoefficientValues_By_Name_And_Regulation";

        public const string SP_COEFFICIENT_VALUE_GET_BY_ID_DATATYPE = "Sel_H1_CoefficientValues_By_Id_DataType";
        //------------------
        public const string SP_COEFFICIENT_VALUE_GETALL = "Sel_H1_CoefficientValues_All";
        public const string Sp_Sel_H1_CoefficientValues_By_Filter = "Sel_H1_CoefficientValues_By_Filter";
        public const string SP_COEFFICIENT_VALUE_GET_BY_NAME_LEVEL = "Sel_H1_CoefficientValues_By_Name_Level";
        public const string SP_COEFFICIENT_VALUE_GET_BY_NAME = "Sel_H1_CoefficientValues_By_NameId";
    }
}