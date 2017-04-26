using System;
using System.Text;

namespace HRMDAL.H1.KeyNames
{
    sealed public class CoefficientValueKeys
    {
        /// <summary>
        /// Some fields in H1_LNS_CoefficientValues 
        /// </summary>
        public const string FIELD_COEFFICIENT_VALUE_ID = "CoefficientValueId";
        public const string FIELD_COEFFICIENT_VALUE = "CoefficientValue";
        public const string FIELD_COEFFICIENT_CONDITIONS = "Conditions";

        /// <summary>
        /// some store procedurces for H1_LNS_CoefficientValues
        /// </summary>
        public const string SP_COEFFICIENT_VALUE_INSERT = "Ins_H1_CoefficientValue";
        public const string SP_COEFFICIENT_VALUE_UPDATE = "Upd_H1_CoefficientValue";
        public const string SP_COEFFICIENT_VALUE_DELETE_BY_NAME_ID = "Del_H1_CoefficientValue_By_NameId";

        public const string SP_COEFFICIENT_VALUE_GETALL = "Sel_H1_CoefficientValues_All";
        public const string SP_COEFFICIENT_VALUE_GET_BY_FILTER = "Sel_H1_CoefficientValues_By_Filter";        
        public const string SP_COEFFICIENT_VALUE_GET_BY_NAME_LEVEL = "Sel_H1_CoefficientValues_By_Name_Level";
        public const string SP_COEFFICIENT_VALUE_GET_BY_NAME = "Sel_H1_CoefficientValues_By_NameId";
    }
}
