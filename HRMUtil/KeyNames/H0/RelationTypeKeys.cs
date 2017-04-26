namespace HRMUtil.KeyNames.H0
{
    public sealed class RelationTypeKeys
    {
        /// <summary>
        ///     Some field names of RelationTypes table
        /// </summary>
        public const string FIELD_RELATION_TYPE_ID = "RelationTypeId";

        public const string FIELD_RELATION_TYPE_NAME = "RelationTypeName";
        public const string FIELD_RELATION_TYPE_DESCRIPTION = "Description";
        public const string FIELD_RELATION_TYPE_TYPE = "Type";


        /// <summary>
        ///     StoreProcedure name  of Qualification object.
        /// </summary>
        public const string SP_RELATION_TYPE_INSERT = "Ins_H0_RelationType";

        public const string SP_RELATION_TYPE_UPDATE = "Upd_H0_RelationType";
        public const string SP_RELATION_TYPE_DELETE = "Del_H0_RelationType";

        public const string SP_RELATION_TYPE_GET_ALL = "Sel_H0_RelationType_All";
        public const string SP_RELATION_TYPE_GET_BY_FILTER = "Sel_H0_RelationType_By_Filter";
        public const string SP_RELATION_TYPE_GET_BY_TYPE = "Sel_H0_RelationType_By_Name";
    }
}