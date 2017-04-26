namespace HRMUtil.KeyNames.H1
{
    public sealed class HTCVEmployeeKeys
    {
        /// <summary>
        ///     Some fields in H1_HTCVEmployee table
        /// </summary>
        public const string Field_HTCVEmployee_ID = "HTCVEmployeeId";

        public const string Field_HTCVEmployee_UserId = "UserId";
        public const string Field_HTCVEmployee_HTCVCatalogueId = "HTCVCatalogueId";
        public const string Field_HTCVEmployee_Mark = "Mark";
        public const string Field_HTCVEmployee_MarkDate = "MarkDate";
        public const string Field_HTCVEmployee_Remark = "Remark";

        /// <summary>
        ///     store procedure for H1_HTCVEmployee table
        /// </summary>
        public const string Sp_Ins_H1_HTCVEmployee = "Ins_H1_HTCVEmployee";

        public const string Sp_Upd_H1_HTCVEmployee = "Upd_H1_HTCVEmployee";
        public const string Sp_Del_H1_HTCVEmployee = "Del_H1_HTCVEmployee";
        public const string Sp_Del_H1_HTCVEmployeeByIds = "Del_H1_HTCVEmployeeByIds";
        public const string Sp_Del_H1_HTCVEmployeeByUserDate = "Del_H1_HTCVEmployeeByUserDate";

        public const string Sp_Sel_H1_HTCVEmployeeByFilter = "Sel_H1_HTCVEmployeeByFilter";

        public const string Sp_Sel_H1_HTCVEmployeeForAllRemarkByUserIdDate =
            "Sel_H1_HTCVEmployeeForAllRemarkByUserIdDate";
    }
}