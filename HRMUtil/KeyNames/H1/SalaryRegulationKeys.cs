namespace HRMUtil.KeyNames.H1
{
    public sealed class SalaryRegulationKeys
    {
        /// <summary>
        ///     Some fields in H1_SalaryRegulation table
        /// </summary>
        public const string Field_SalaryRegulation_SalaryRegulationId = "SalaryRegulationId";

        public const string Field_SalaryRegulation_SalaryRegulationName = "SalaryRegulationName";
        public const string Field_SalaryRegulation_BeginingDate = "BeginingDate";
        public const string Field_SalaryRegulation_Description = "Description";
        public const string Field_SalaryRegulation_InUse = "InUse";
        public const string Field_SalaryRegulation_TypeId = "TypeId";

        /// <summary>
        ///     store procedure for H1_SalaryRegulation table
        /// </summary>
        public const string Sp_Ins_H1_SalaryRegulation = "Ins_H1_SalaryRegulation";

        public const string Sp_Upd_H1_SalaryRegulation = "Upd_H1_SalaryRegulation";
        public const string Sp_Del_H1_SalaryRegulation = "Del_H1_SalaryRegulation";
        public const string Sp_Sel_H1_SalaryRegulationByFilter = "Sel_H1_SalaryRegulationByFilter";
        public const string Sp_Sel_H1_SalaryRegulationByInUse = "Sel_H1_SalaryRegulationByInUse";
        //Giang
        public const string Sp_Sel_H1_SalaryRegulationByFilterV1 = "Sel_H1_SalaryRegulationByFilterV1";
    }
}