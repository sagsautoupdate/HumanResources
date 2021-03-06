using System;
using System.Collections.Generic;
using System.Data;
using HRMBLL.H0.Helper;
using HRMDAL.H0;
using HRMUtil.KeyNames.H0;

namespace HRMBLL.H0
{
    public class ContractTypesBLL
    {
        #region private fields

        #endregion

        #region properties

        public string ContractFullName { get; set; }

        public int ContractTypeId { get; set; }

        public string ContractTypeCode { get; set; }

        public string ContractTypeName { get; set; }

        public string ContractTypeDescription { get; set; }

        public double ContractTypeValue { get; set; }

        public int DataType { get; set; }

        #endregion

        #region constructor

        public ContractTypesBLL()
        {
            var objDAL = new ContractTypesDAL();
            objDAL.Insert("", "", 0, "", 1);
        }

        public ContractTypesBLL(int contractTypeId, string contractTypeCode, string contractTypeName,
            string contractTypeDescription)
        {
            ContractTypeId = contractTypeId;
            ContractTypeCode = contractTypeCode;
            ContractTypeName = contractTypeName;
            ContractTypeDescription = contractTypeDescription;
        }

        #endregion

        #region methods insert, update, delete

        public long Save()
        {
            var objDAL = new ContractTypesDAL();
            if (ContractTypeId <= 0)
                return objDAL.Insert(ContractTypeCode, ContractTypeName, ContractTypeValue, ContractTypeDescription,
                    DataType);
            return objDAL.Update(ContractTypeCode, ContractTypeName, ContractTypeValue, ContractTypeDescription,
                ContractTypeId, DataType);
        }

        public long SaveV1()
        {
            var objDAL = new ContractTypesDAL();
            if (ContractTypeId <= 0)
                return objDAL.InsertV1(ContractTypeCode, ContractTypeName, ContractFullName, ContractTypeValue,
                    ContractTypeDescription, DataType);
            return objDAL.UpdateV1(ContractTypeCode, ContractTypeName, ContractFullName, ContractTypeValue,
                ContractTypeDescription, ContractTypeId, DataType);
        }

        public static long UpdateV1(string contractTypeCode, string contractTypeName, string contractFullName,
            double contractTypeValue, string contractTypeDescription, int contractTypeId, int dataType)
        {
            var objDAL = new ContractTypesDAL();
            if (contractTypeDescription == null)
                contractTypeDescription = string.Empty;
            return objDAL.UpdateV1(contractTypeCode, contractTypeName, contractFullName, contractTypeValue,
                contractTypeDescription, contractTypeId, dataType);
        }

        public static long Update(string contractTypeCode, string contractTypeName, double contractTypeValue,
            string contractTypeDescription, int contractTypeId, int dataType)
        {
            var objDAL = new ContractTypesDAL();
            if (contractTypeDescription == null)
                contractTypeDescription = string.Empty;
            return objDAL.Update(contractTypeCode, contractTypeName, contractTypeValue, contractTypeDescription,
                contractTypeId, dataType);
        }

        public static long Delete(int contractTypeId)
        {
            var objDAL = new ContractTypesDAL();
            return objDAL.Delete(contractTypeId);
        }

        #endregion

        #region public static method Get

        /// <summary>
        ///     Giang
        ///     12/11/2014
        ///     Get Contract Types to DT
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public static DataTable GetAllToDT(int dataType)
        {
            return new ContractTypesDAL().GetAll(dataType);
        }

        public static DataRow GetByCode(string Code)
        {
            var dt = new ContractTypesDAL().GetByCode(Code);
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        public static List<ContractTypesBLL> GetAll(int dataType)
        {
            return GenerateListContractTypesBLLFromDataTable(new ContractTypesDAL().GetAll(dataType));
        }

        public static List<ContractTypesBLL> GetAll_N(int dataType)
        {
            return GenerateListContractTypesBLLFromDataTable_N(new ContractTypesDAL().GetAll(dataType));
        }

        public static ContractTypesBLL GetById(int contractTypeId)
        {
            var list = new List<ContractTypesBLL>();
            list = GenerateListContractTypesBLLFromDataTable(new ContractTypesDAL().GetById(contractTypeId));
            if (list.Count == 1)
                return list[0];
            return null;
        }

        #endregion

        #region private static methods

        private static List<ContractTypesBLL> GenerateListContractTypesBLLFromDataTable_N(DataTable dt)
        {
            var list = new List<ContractTypesBLL>();

            list.Add(new ContractTypesBLL(0, string.Empty, " ", string.Empty));

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateContractTypesBLLFromDataRow(dr));

            return list;
        }

        private static List<ContractTypesBLL> GenerateListContractTypesBLLFromDataTable(DataTable dt)
        {
            var list = new List<ContractTypesBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateContractTypesBLLFromDataRow(dr));

            return list;
        }

        private static ContractTypesBLL GenerateContractTypesBLLFromDataRow(DataRow dr)
        {
            var objBLL = new ContractTypesBLL(
                dr[ContractTypeKeys.Field_ContractTypes_ContractTypeId] == DBNull.Value
                    ? DefaultValues.ContractTypeIdMinValue
                    : int.Parse(dr[ContractTypeKeys.Field_ContractTypes_ContractTypeId].ToString()),
                dr[ContractTypeKeys.Field_ContractTypes_ContractTypeCode] == DBNull.Value
                    ? string.Empty
                    : dr[ContractTypeKeys.Field_ContractTypes_ContractTypeCode].ToString(),
                dr[ContractTypeKeys.Field_ContractTypes_ContractTypeName] == DBNull.Value
                    ? string.Empty
                    : dr[ContractTypeKeys.Field_ContractTypes_ContractTypeName].ToString(),
                dr[ContractTypeKeys.Field_ContractTypes_ContractDescription] == DBNull.Value
                    ? string.Empty
                    : dr[ContractTypeKeys.Field_ContractTypes_ContractDescription].ToString()
            );

            objBLL.ContractFullName = dr[ContractTypeKeys.Field_ContractTypes_ContractFullName] == DBNull.Value
                ? string.Empty
                : dr[ContractTypeKeys.Field_ContractTypes_ContractFullName].ToString();

            objBLL.ContractTypeValue = dr[ContractTypeKeys.Field_ContractTypes_ContractTypeValue] == DBNull.Value
                ? 0
                : double.Parse(dr[ContractTypeKeys.Field_ContractTypes_ContractTypeValue].ToString());
            objBLL.DataType = dr[ContractTypeKeys.Field_ContractTypes_DataType] == DBNull.Value
                ? 0
                : int.Parse(dr[ContractTypeKeys.Field_ContractTypes_DataType].ToString());

            return objBLL;
        }

        #endregion
    }
}