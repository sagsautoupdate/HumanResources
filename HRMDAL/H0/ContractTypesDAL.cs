using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H0;

namespace HRMDAL.H0
{
    public class ContractTypesDAL : Dao
    {
        #region Insert, update, delete

        public long InsertV1(string ContractTypeCode, string ContractTypeName, string ContractFullName,
            double ContractTypeValue, string ContractDescription, int DataType)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@ContractTypeCode", SqlDbType.VarChar, 50),
                    new SqlParameter("@ContractTypeName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@ContractFullName", SqlDbType.NVarChar, 250),
                    new SqlParameter("@ContractTypeValue", SqlDbType.Float),
                    new SqlParameter("@ContractDescription", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@DataType", SqlDbType.Int, 4)
                };

                param[0].Value = ContractTypeCode;
                param[1].Value = ContractTypeName;
                param[2].Value = ContractFullName;
                param[3].Value = ContractTypeValue;
                param[4].Value = ContractDescription;
                param[5].Value = DataType;

                sproc = new StoreProcedure(ContractTypeKeys.Sp_Ins_H0_ContractTypeV1, param);
                identity = sproc.Run();
                sproc.Commit();
            }
            catch (SqlException se)
            {
                sproc.RollBack();
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return identity;
        }

        public long UpdateV1(string ContractTypeCode, string ContractTypeName, string ContractFullName,
            double ContractTypeValue, string ContractDescription, int ContractTypeId, int DataType)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@ContractTypeCode", SqlDbType.VarChar, 50),
                    new SqlParameter("@ContractTypeName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@ContractFullName", SqlDbType.NVarChar, 250),
                    new SqlParameter("@ContractTypeValue", SqlDbType.Float),
                    new SqlParameter("@ContractDescription", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@ContractTypeId", SqlDbType.Int),
                    new SqlParameter("@DataType", SqlDbType.Int)
                };

                param[0].Value = ContractTypeCode;
                param[1].Value = ContractTypeName;
                param[2].Value = ContractFullName;
                param[3].Value = ContractTypeValue;
                param[4].Value = ContractDescription;
                param[5].Value = ContractTypeId;
                param[6].Value = DataType;

                sproc = new StoreProcedure(ContractTypeKeys.Sp_Upd_H0_ContractTypeV1, param);
                identity = sproc.Run();
                sproc.Commit();
            }
            catch (SqlException se)
            {
                sproc.RollBack();
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return identity;
        }

        public long Insert(string ContractTypeCode, string ContractTypeName, double ContractTypeValue,
            string ContractDescription, int DataType)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@ContractTypeCode", SqlDbType.VarChar, 50),
                    new SqlParameter("@ContractTypeName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@ContractTypeValue", SqlDbType.Float),
                    new SqlParameter("@ContractDescription", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@DataType", SqlDbType.Int, 4)
                };

                param[0].Value = ContractTypeCode;
                param[1].Value = ContractTypeName;
                param[2].Value = ContractTypeValue;
                param[3].Value = ContractDescription;
                param[4].Value = DataType;

                sproc = new StoreProcedure(ContractTypeKeys.Sp_Ins_H0_ContractType, param);
                identity = sproc.Run();
                sproc.Commit();
            }
            catch (SqlException se)
            {
                sproc.RollBack();
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return identity;
        }

        public long Update(string ContractTypeCode, string ContractTypeName, double ContractTypeValue,
            string ContractDescription, int ContractTypeId, int DataType)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@ContractTypeCode", SqlDbType.VarChar, 50),
                    new SqlParameter("@ContractTypeName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@ContractTypeValue", SqlDbType.Float),
                    new SqlParameter("@ContractDescription", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@ContractTypeId", SqlDbType.Int),
                    new SqlParameter("@DataType", SqlDbType.Int)
                };

                param[0].Value = ContractTypeCode;
                param[1].Value = ContractTypeName;
                param[2].Value = ContractTypeValue;
                param[3].Value = ContractDescription;
                param[4].Value = ContractTypeId;
                param[5].Value = DataType;

                sproc = new StoreProcedure(ContractTypeKeys.Sp_Upd_H0_ContractType, param);
                identity = sproc.Run();
                sproc.Commit();
            }
            catch (SqlException se)
            {
                sproc.RollBack();
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return identity;
        }

        public long Delete(int contractTypeId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@ContractTypeId", SqlDbType.Int)
                };

                param[0].Value = contractTypeId;

                sproc = new StoreProcedure(ContractTypeKeys.Sp_Del_H0_ContractType, param);
                identity = sproc.Run();
                sproc.Commit();
            }
            catch (SqlException se)
            {
                sproc.RollBack();
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return identity;
        }

        #endregion

        #region get

        public DataTable GetAll(int DataType)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DataType", SqlDbType.Int)
                };

                param[0].Value = DataType;

                sproc = new StoreProcedure(ContractTypeKeys.Sp_Sel_H0_ContractTypes_All, param);
                sproc.RunFill(dt);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetByCode(string contractTypeCode)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@ContractTypeCode", SqlDbType.VarChar, 50)
                };

                param[0].Value = contractTypeCode;
                sproc = new StoreProcedure(ContractTypeKeys.Sp_Sel_H0_ContractTypes_By_Code, param);
                sproc.RunFill(dt);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetById(int contractTypeId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@ContractTypeId", SqlDbType.Int)
                };

                param[0].Value = contractTypeId;
                sproc = new StoreProcedure(ContractTypeKeys.Sp_Sel_H0_ContractTypes_By_Id, param);
                sproc.RunFill(dt);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        #endregion
    }
}