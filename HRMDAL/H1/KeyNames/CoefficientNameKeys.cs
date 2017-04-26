using System;
using System.Text;

namespace HRMDAL.H1.KeyNames
{
    sealed public class CoefficientNameKeys
    {
        /// <summary>
        /// Some fields in H1_LNS_CoefficientNames 
        /// </summary>
        /// 
        public const string FIELD_COEFFICIENT_NAME_ID = "CoefficientNameId";
        public const string FIELD_COEFFICIENT_NAME = "CoefficientName";
        public const string FIELD_COEFFICIENT_NAME_DESCRIPTION = "CoefficientNameDescription";

        /// <summary>
        /// some store procedurces for H1_LNS_CoefficientNames
        /// </summary>
        /// 
        //public const string SP_COEFFICIENT_NAME_INSERT = "Ins_H1_CoefficientName";
        //public const string SP_COEFFICIENT_NAME_UPDATE = "Upd_H1_CoefficientName";
        //public const string SP_COEFFICIENT_NAME_DELETE = "Del_H1_CoefficientName";

        public const string SP_COEFFICIENT_NAME_GETALL = "Sel_H1_CoefficientNames_All";

        public const int CONST_LNS_COEFFICIENT_NAME_TYPE = 0;
        public const int CONST_LCB_COEFFICIENT_NAME_TYPE = 1;
    }
}
