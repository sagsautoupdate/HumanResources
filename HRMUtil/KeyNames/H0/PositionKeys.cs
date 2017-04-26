namespace HRMUtil.KeyNames.H0
{
    public sealed class PositionKeys
    {
        #region some field names of Position table

        public const string FIELD_POSITION_ID = "PositionId";
        public const string FIELD_POSITION_NAME = "PositionName";
        public const string FIELD_POSITION_DESCRIPTION = "Description";
        public const string Field_Position_LevelPosition = "LevelPosition";
        public const string Field_Position_DepartmentId = "DepartmentId";

        #endregion

        #region some store procedures of Position table

        //public const string SP_POSITION_IS_RECRUITMENT = "Sel_H0_Positions_IsRecruitment";

        //public const string Sp_Sel_H0_Psotion_By_Ids = "Sel_H0_Psotion_By_Ids";
        public const string Sp_Sel_H0_Positions_All = "Sel_H0_Positions_All";
        public const string Sp_Sel_H0_Positions_AllView = "Sel_H0_Positions_AllView";
        public const string Sp_Sel_H0_PositionsV1 = "Sel_H0_PositionsV1";
        public const string Sp_Sel_H0_Positions_ByFilter = "Sel_H0_Positions_ByFilter";
        public const string Sp_Sel_H0_PositionsLeader_ByFilter = "Sel_H0_PositionsLeader_ByFilter";
        public const string Sp_Sel_H0_Positions_ByDepartmentId_LevelPosition = "Sel_H0_Positions_ByDepartmentId";

        public const string Sp_Ins_H0_Position = "Ins_H0_Position";
        public const string Sp_Upd_H0_Position = "Upd_H0_Position";
        public const string SP_POSITION_DELETE = "Del_H0_Position";

        public const string Sp_Ins_H0_PositionV1 = "Ins_H0_Position_V1";
        public const string Sp_Upd_H0_PositionV1 = "Upd_H0_Position_V1";

        #endregion
    }
}