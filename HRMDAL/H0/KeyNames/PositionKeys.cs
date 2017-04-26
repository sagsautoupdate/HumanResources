using System;
using System.Text;
namespace HRMDAL.H0.KeyNames
{
    public sealed class PositionKeys
    {

        #region some field names of Position table

        public const string FIELD_POSITION_ID = "PositionId";
        public const string FIELD_POSITION_NAME = "PositionName";
        public const string FIELD_POSITION_DESCRIPTION = "Description";

        #endregion

        #region some store procedures of Position table

        public const string SP_POSITION_IS_RECRUITMENT = "Sel_H0_Positions_IsRecruitment";
        public const string SP_POSITION_GETALL = "Sel_H0_Positions_All";
        public const string SP_POSITION_GET_BY_IDs = "Sel_H0_Psotion_By_Ids";        
        public const string SP_POSITION_INSERT = "Ins_H0_Position";
        public const string SP_POSITION_UPDATE = "Upd_H0_Position";
        public const string SP_POSITION_DELETE = "Del_H0_Position";

        #endregion

    }
}
