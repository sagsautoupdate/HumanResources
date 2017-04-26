using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H1;

namespace HRMDAL.H1
{
    public class CoefficientValuesDAL : Dao
    {
        #region insert, update, delete

        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns>int</returns>
        public long Insert(string coefficientName,
            double value_Level_1, double conditions1, double value_Level_2, double conditions2, double value_Level_3,
            double conditions3,
            double value_Level_4, double conditions4, double value_Level_5, double conditions5, double value_Level_6,
            double conditions6,
            double value_Level_7, double conditions7, double value_Level_8, double conditions8, double value_Level_9,
            double conditions9,
            double value_Level_10, double conditions10, double value_Level_11, double conditions11,
            double value_Level_12, double conditions12,
            string coefficientNameDescription, int type, int SalaryRegulationId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@CoefficientName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@Value_Level_1", SqlDbType.Float),
                    new SqlParameter("@Conditions_1", SqlDbType.Float),
                    new SqlParameter("@Value_Level_2", SqlDbType.Float),
                    new SqlParameter("@Conditions_2", SqlDbType.Float),
                    new SqlParameter("@Value_Level_3", SqlDbType.Float),
                    new SqlParameter("@Conditions_3", SqlDbType.Float),
                    new SqlParameter("@Value_Level_4", SqlDbType.Float),
                    new SqlParameter("@Conditions_4", SqlDbType.Float),
                    new SqlParameter("@Value_Level_5", SqlDbType.Float),
                    new SqlParameter("@Conditions_5", SqlDbType.Float),
                    new SqlParameter("@Value_Level_6", SqlDbType.Float),
                    new SqlParameter("@Conditions_6", SqlDbType.Float),
                    new SqlParameter("@Value_Level_7", SqlDbType.Float),
                    new SqlParameter("@Conditions_7", SqlDbType.Float),
                    new SqlParameter("@Value_Level_8", SqlDbType.Float),
                    new SqlParameter("@Conditions_8", SqlDbType.Float),
                    new SqlParameter("@Value_Level_9", SqlDbType.Float),
                    new SqlParameter("@Conditions_9", SqlDbType.Float),
                    new SqlParameter("@Value_Level_10", SqlDbType.Float),
                    new SqlParameter("@Conditions_10", SqlDbType.Float),
                    new SqlParameter("@Value_Level_11", SqlDbType.Float),
                    new SqlParameter("@Conditions_11", SqlDbType.Float),
                    new SqlParameter("@Value_Level_12", SqlDbType.Float),
                    new SqlParameter("@Conditions_12", SqlDbType.Float),
                    new SqlParameter("@CoefficientNameDescription", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@Type", SqlDbType.Int),
                    new SqlParameter("@SalaryRegulationId", SqlDbType.Int)
                };

                param[0].Value = coefficientName;

                param[1].Value = value_Level_1;
                param[2].Value = conditions1;
                param[3].Value = value_Level_2;
                param[4].Value = conditions2;
                param[5].Value = value_Level_3;
                param[6].Value = conditions3;
                param[7].Value = value_Level_4;
                param[8].Value = conditions4;
                param[9].Value = value_Level_5;
                param[10].Value = conditions5;
                param[11].Value = value_Level_6;
                param[12].Value = conditions6;
                param[13].Value = value_Level_7;
                param[14].Value = conditions7;
                param[15].Value = value_Level_8;
                param[16].Value = conditions8;
                param[17].Value = value_Level_9;
                param[18].Value = conditions9;
                param[19].Value = value_Level_10;
                param[20].Value = conditions10;
                param[21].Value = value_Level_11;
                param[22].Value = conditions11;
                param[23].Value = value_Level_12;
                param[24].Value = conditions12;

                param[25].Value = coefficientNameDescription;
                param[26].Value = type;
                param[27].Value = SalaryRegulationId;

                sproc = new StoreProcedure(CoefficientValueKeys.Sp_Ins_H1_CoefficientValue, param);
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

        public long Update(string coefficientName,
            double value_Level_1, double conditions1, double value_Level_2, double conditions2, double value_Level_3,
            double conditions3,
            double value_Level_4, double conditions4, double value_Level_5, double conditions5, double value_Level_6,
            double conditions6,
            double value_Level_7, double conditions7, double value_Level_8, double conditions8, double value_Level_9,
            double conditions9,
            double value_Level_10, double conditions10, double value_Level_11, double conditions11,
            double value_Level_12, double conditions12,
            string coefficientNameDescription, int type, int SalaryRegulationId, int coefficientNameId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@CoefficientName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@Value_Level_1", SqlDbType.Float),
                    new SqlParameter("@Conditions_1", SqlDbType.Float),
                    new SqlParameter("@Value_Level_2", SqlDbType.Float),
                    new SqlParameter("@Conditions_2", SqlDbType.Float),
                    new SqlParameter("@Value_Level_3", SqlDbType.Float),
                    new SqlParameter("@Conditions_3", SqlDbType.Float),
                    new SqlParameter("@Value_Level_4", SqlDbType.Float),
                    new SqlParameter("@Conditions_4", SqlDbType.Float),
                    new SqlParameter("@Value_Level_5", SqlDbType.Float),
                    new SqlParameter("@Conditions_5", SqlDbType.Float),
                    new SqlParameter("@Value_Level_6", SqlDbType.Float),
                    new SqlParameter("@Conditions_6", SqlDbType.Float),
                    new SqlParameter("@Value_Level_7", SqlDbType.Float),
                    new SqlParameter("@Conditions_7", SqlDbType.Float),
                    new SqlParameter("@Value_Level_8", SqlDbType.Float),
                    new SqlParameter("@Conditions_8", SqlDbType.Float),
                    new SqlParameter("@Value_Level_9", SqlDbType.Float),
                    new SqlParameter("@Conditions_9", SqlDbType.Float),
                    new SqlParameter("@Value_Level_10", SqlDbType.Float),
                    new SqlParameter("@Conditions_10", SqlDbType.Float),
                    new SqlParameter("@Value_Level_11", SqlDbType.Float),
                    new SqlParameter("@Conditions_11", SqlDbType.Float),
                    new SqlParameter("@Value_Level_12", SqlDbType.Float),
                    new SqlParameter("@Conditions_12", SqlDbType.Float),
                    new SqlParameter("@CoefficientNameDescription", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@Type", SqlDbType.Int),
                    new SqlParameter("@SalaryRegulationId", SqlDbType.Int),
                    new SqlParameter("@CoefficientNameId", SqlDbType.Int)
                };

                param[0].Value = coefficientName;

                param[1].Value = value_Level_1;
                param[2].Value = conditions1;
                param[3].Value = value_Level_2;
                param[4].Value = conditions2;
                param[5].Value = value_Level_3;
                param[6].Value = conditions3;
                param[7].Value = value_Level_4;
                param[8].Value = conditions4;
                param[9].Value = value_Level_5;
                param[10].Value = conditions5;
                param[11].Value = value_Level_6;
                param[12].Value = conditions6;
                param[13].Value = value_Level_7;
                param[14].Value = conditions7;
                param[15].Value = value_Level_8;
                param[16].Value = conditions8;
                param[17].Value = value_Level_9;
                param[18].Value = conditions9;
                param[19].Value = value_Level_10;
                param[20].Value = conditions10;
                param[21].Value = value_Level_11;
                param[22].Value = conditions11;
                param[23].Value = value_Level_12;
                param[24].Value = conditions12;

                param[25].Value = coefficientNameDescription;
                param[26].Value = type;
                param[27].Value = SalaryRegulationId;
                param[28].Value = coefficientNameId;

                sproc = new StoreProcedure(CoefficientValueKeys.Sp_Upd_H1_CoefficientValue, param);
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

        public int DeleteByNameId(int coefficientNameId)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@CoefficientNameId", SqlDbType.Int)
                };

                param[0].Value = coefficientNameId;

                sproc = new StoreProcedure(CoefficientValueKeys.SP_COEFFICIENT_VALUE_DELETE_BY_NAME_ID, param);
                sproc.Run();
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

        #region private methods

        #endregion

        #region Get

        public DataTable GetByFilter(int type, string coefficientName, int SalaryRegulationId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Type", SqlDbType.Int),
                    new SqlParameter("@CoefficientName", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@SalaryRegulationId", SqlDbType.Int)
                };

                param[0].Value = type;
                param[1].Value = coefficientName;
                param[2].Value = SalaryRegulationId;

                sproc = new StoreProcedure(CoefficientValueKeys.Sp_Sel_H1_CoefficientValues_By_Filter, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        /// <summary>
        ///     Author: Giang
        ///     Date: 12/2/2014
        ///     Content: Lay Coefficient ID theo type & regulation
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable GetAllToDT(int type, int regulationid)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Type", SqlDbType.Int),
                    new SqlParameter("@RegulationId", SqlDbType.Int)
                };

                param[0].Value = type;
                param[1].Value = regulationid;

                sproc = new StoreProcedure(CoefficientValueKeys.SP_COEFFICIENT_VALUE_GET_BY_TYPE_AND_REGULATIONID, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetAll(int type)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Type", SqlDbType.Int)
                };

                param[0].Value = type;


                sproc = new StoreProcedure(CoefficientValueKeys.SP_COEFFICIENT_VALUE_GETALL, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByName_Level(int coefficientNameId, int coefficientLevelId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@CoefficientNameId", SqlDbType.Int),
                    new SqlParameter("@CoefficientLevelId", SqlDbType.Int)
                };

                param[0].Value = coefficientNameId;
                param[1].Value = coefficientLevelId;

                sproc = new StoreProcedure(CoefficientValueKeys.SP_COEFFICIENT_VALUE_GET_BY_NAME_LEVEL, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByNameId(int coefficientNameId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@CoefficientNameId", SqlDbType.Int)
                };

                param[0].Value = coefficientNameId;

                sproc = new StoreProcedure(CoefficientValueKeys.SP_COEFFICIENT_VALUE_GET_BY_NAME, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        #endregion
    }
}