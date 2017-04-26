namespace HRMUtil.KeyNames.H4
{
    public sealed class RepresentativeKeys
    {
        /// <summary>
        ///     some fields in H4_Representative tables
        /// </summary>
        public const string Field_Representative_RepresentativeId = "RepresentativeId";

        public const string Field_Representative_InvestorNo = "InvestorNo";
        public const string Field_Representative_RepresentativeName = "RepresentativeName";
        public const string Field_Representative_RepresentativeName2 = "RepresentativeName2";
        public const string Field_Representative_Stock = "Stock";
        public const string Field_Representative_StockRatio = "StockRatio";
        public const string Field_Representative_AttorneyCode = "AttorneyCode";
        public const string Field_Representative_HDQT_Vote = "HDQT_Vote";
        public const string Field_Representative_HDQT_IsValid = "HDQT_IsValid";
        public const string Field_Representative_HDQT_A = "HDQT_A";
        public const string Field_Representative_HDQT_B = "HDQT_B";
        public const string Field_Representative_HDQT_C = "HDQT_C";
        public const string Field_Representative_HDQT_D = "HDQT_D";
        public const string Field_Representative_HDQT_E = "HDQT_E";
        public const string Field_Representative_HDQT_F = "HDQT_F";
        public const string Field_Representative_HDQT_G = "HDQT_G";
        public const string Field_Representative_BKS_Vote = "BKS_Vote";
        public const string Field_Representative_BKS_IsValid = "BKS_IsValid";
        public const string Field_Representative_BKS_A = "BKS_A";
        public const string Field_Representative_BKS_B = "BKS_B";
        public const string Field_Representative_BKS_C = "BKS_C";
        public const string Field_Representative_IsOk = "IsOk";
        public const string Field_Representative_Remark = "Remark";

        /// <summary>
        ///     some store procedures H4_Representative table
        /// </summary>
        public const string Sp_Ins_H4_Representative = "Ins_H4_Representative";

        public const string Sp_Del_H4_Representative = "Del_H4_Representative";

        public const string Sp_Upd_H4_Representative_For_AttorneyCode = "Upd_H4_Representative_For_AttorneyCode";
        public const string Sp_Upd_H4_Representative_For_Check = "Upd_H4_Representative_For_Check";
        public const string Sp_Upd_H4_Representative_For_HDQT = "Upd_H4_Representative_For_HDQT";
        public const string Sp_Upd_H4_Representative_For_BKS = "Upd_H4_Representative_For_BKS";

        public const string Sp_Sel_H4_Representative_By_Filter = "Sel_H4_Representative_By_Filter";
        public const string Sp_Sel_H4_Representative_For_Total = "Sel_H4_Representative_For_Total";
        public const string Sp_Sel_H4_Representative_By_AttorneyCode = "Sel_H4_Representative_By_AttorneyCode";
        //public const string Sp_Sel_H4_Decision_By_Id = "Sel_H3_Decision_By_Id";
    }
}