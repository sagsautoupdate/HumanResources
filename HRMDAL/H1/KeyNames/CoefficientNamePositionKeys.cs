using System;
using System.Text;

namespace HRMDAL.H1.KeyNames
{
    sealed public class CoefficientNamePositionKeys
    {
        /// <summary>
        /// Some fields in H1_LNS_CoefficientNamePosition 
        /// </summary>
        public const string FIELD_COEFFICIENT_NAME_POSITION_ID = "CoefficientNamePositionId";        

        /// <summary>
        /// some store procedurces for H1_LNS_CoefficientNamePosition
        /// </summary>
        public const string SP_COEFFICIENT_NAME_POSITION_GET_BY_NAME_ID = "Sel_H1_CoefficientNamePositions_By_NameId";
        public const string SP_COEFFICIENT_NAME_POSITION_GET_BY_POSITION_ID = "Sel_H1_CoefficientNamePositions_By_PositionId";
    }
}
