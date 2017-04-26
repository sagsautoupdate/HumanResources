using System;
using System.Text;

namespace HRMDAL.H0.KeyNames
{
    public sealed class ContractTypeKeys
    {

        /// <summary>
        /// Some field names of ContractTypes table
        /// </summary>
        public const string FIELD_CONTRACT_TYPE_ID = "ContractTypeId";
        public const string FIELD_CONTRACT_TYPE_CODE = "ContractTypeCode";
        public const string FIELD_CONTRACT_TYPE_NAME = "ContractTypeName";
        public const string FIELD_CONTRACT_TYPE_DESCRIPTION = "ContractDescription";
        public const string FIELD_CONTRACT_TYPE_VALUE = "ContractTypeValue";

        /// <summary>
        /// StoreProcedure name  of ContractTypes object.
        /// </summary>
        public const string SP_CONTRACT_TYPE_GETALL = "Sel_H0_ContractTypes_All";
        public const string SP_CONTRACT_TYPE_GET_BY_ID = "Sel_H0_ContractTypes_By_Id";

        public const string SP_CONTRACT_TYPE_INSERT = "Ins_H0_ContractType";
        public const string SP_CONTRACT_TYPE_UPDATE = "Upd_H0_ContractType";
        public const string SP_CONTRACT_TYPE_DELETE = "Del_H0_ContractType";
        
    }
}
