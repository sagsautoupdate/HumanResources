using System;
using System.Text;

namespace HRMDAL.H2.KeyNames
{
    public sealed class SessionKeys
    {
        /// <summary>
        ///  some fields in H2_Session tables
        /// </summary>
        public const string FIELD_SESSION_ID = "SessionId";
        public const string FIELD_SESSION_FROM_DATE = "FromDate";
        public const string FIELD_SESSION_TO_DATE = "ToDate";
        public const string FIELD_SESSION_SESSION_NAME = "Name";
        public const string FIELD_SESSION_REMARK = "Remark";
        public const string FIELD_SESSION_IS_ACTIVE = "IsActive";


        public const string FIELD_SESSION_POSITION_ID = "PositionId";
        public const string FIELD_SESSION_POSITION_NAME = "PositionName";

        /// <summary>
        /// some store procedures H2_Session table
        /// </summary>
        public const string SP_SESSION_INSERT = "Ins_H2_Session";
        public const string SP_SESSION_UPDATE = "Upd_H2_Session";
        public const string SP_SESSION_DELETE = "Del_H2_Session";
        public const string SP_SESSION_GET_ALL = "Sel_H2_Session_All";
        public const string SP_SESSION_GET_IS_ACTIVE = "Sel_H2_Session_IsActive";        
        public const string SP_SESSION_GET_BY_ID = "Sel_H2_Session_By_Id";
        public const string SP_SESSION_POSITION_GET_IS_RECRUITMENT = "Sel_H2_SessionPositionIsRecruitment";
        public const string SP_SESSION_POSITION_GET_BY_SESSIONID = "Sel_H2_SessionPosition_By_SessionId";
        
    }
}
