namespace HRMUtil.KeyNames.H0
{
    public sealed class ContractTypeKeys
    {
        /// <summary>
        ///     Some field names of H0_ContractTypes table
        /// </summary>
        public const string Field_ContractTypes_ContractTypeId = "ContractTypeId";

        public const string Field_ContractTypes_ContractFullName = "ContractFullName";
        public const string Field_ContractTypes_ContractTypeCode = "ContractTypeCode";
        public const string Field_ContractTypes_ContractTypeName = "ContractTypeName";
        public const string Field_ContractTypes_ContractDescription = "ContractDescription";
        public const string Field_ContractTypes_ContractTypeValue = "ContractTypeValue";
        public const string Field_ContractTypes_DataType = "DataType";


        /// <summary>
        ///     StoreProcedure name  of ContractTypes object.
        /// </summary>
        public const string Sp_Sel_H0_ContractTypes_All = "Sel_H0_ContractTypes_All";

        public const string Sp_Sel_H0_ContractTypes_By_Id = "Sel_H0_ContractTypes_By_Id";

        public const string Sp_Ins_H0_ContractType = "Ins_H0_ContractType";
        public const string Sp_Upd_H0_ContractType = "Upd_H0_ContractType";
        public const string Sp_Del_H0_ContractType = "Del_H0_ContractType";

        public const string Sp_Ins_H0_ContractTypeV1 = "Ins_H0_ContractTypeV1";
        public const string Sp_Upd_H0_ContractTypeV1 = "Upd_H0_ContractTypeV1";
        public const string Sp_Sel_H0_ContractTypes_By_Code = "Sel_H0_ContractTypes_By_Code";
    }
}