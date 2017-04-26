using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H1;

namespace HRMDAL.H1
{
    /// <summary>
    ///     Author: Giang
    ///     Content: DAL Coefficient Type
    /// </summary>
    public class CoefficientTypeDAL : Dao
    {
        #region Get

        public DataTable GetCoefficientTypeId_By_CoefficientName(int DataType, int SalaryRegulationId,
            string CoefficientName)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DataType", SqlDbType.Int),
                    new SqlParameter("@SalaryRegulationId", SqlDbType.Int),
                    new SqlParameter("@CoefficientName", SqlDbType.NVarChar, 100)
                };

                param[0].Value = DataType;
                param[1].Value = SalaryRegulationId;
                param[2].Value = CoefficientName;

                sproc = new StoreProcedure(CoefficientTypeKeys.Sp_Sel_H1_CoefficientTypeId_By_Name, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetCoefficientType_By_Name(string CoefficientName)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@CoefficientName", SqlDbType.NVarChar, 100)
                };

                param[0].Value = CoefficientName;

                sproc = new StoreProcedure(CoefficientTypeKeys.Sp_Sel_H1_CoefficientType_By_Name, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetAll(int type, int salaryregulationid)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DataType", SqlDbType.Int),
                    new SqlParameter("@SalaryRegulationId", SqlDbType.Int)
                };

                param[0].Value = type;
                param[1].Value = salaryregulationid;

                sproc = new StoreProcedure(CoefficientTypeKeys.Sp_Sel_H1_CoefficientType_All, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetById_DataType(int CoefficientTypeId, int DataType)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@CoefficientTypeId", SqlDbType.Int),
                    new SqlParameter("@DataType", SqlDbType.Int)
                };

                param[0].Value = CoefficientTypeId;
                param[1].Value = DataType;

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

        public DataTable GetValue_By_ID_Level(int CoefficientTypeId, int Level, int Type, int SalaryRegulationId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@CoefficientTypeId", SqlDbType.Int),
                    new SqlParameter("@Level", SqlDbType.Int),
                    new SqlParameter("@Type", SqlDbType.Int),
                    new SqlParameter("@SalaryRegulationId", SqlDbType.Int)
                };

                param[0].Value = CoefficientTypeId;
                param[1].Value = Level;
                param[2].Value = Type;
                param[3].Value = SalaryRegulationId;

                sproc = new StoreProcedure(CoefficientTypeKeys.Sp_Sel_H1_CoefficientType_Value, param);
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

        #region Insert, Update, Delete

        public long Insert(string coefficientName,
            double value_1, double month_1, double value_2, double month_2, double value_3, double month_3,
            double value_4, double month_4, double value_5, double month_5, double value_6, double month_6,
            double value_7, double month_7, double value_8, double month_8, double value_9, double month_9,
            double value_10, double month_10, double value_11, double month_11, double value_12, double month_12,
            int dataType, int salaryRegulationId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@CoefficientName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@Value_1", SqlDbType.Float),
                    new SqlParameter("@Month_1", SqlDbType.Float),
                    new SqlParameter("@Value_2", SqlDbType.Float),
                    new SqlParameter("@Month_2", SqlDbType.Float),
                    new SqlParameter("@Value_3", SqlDbType.Float),
                    new SqlParameter("@Month_3", SqlDbType.Float),
                    new SqlParameter("@Value_4", SqlDbType.Float),
                    new SqlParameter("@Month_4", SqlDbType.Float),
                    new SqlParameter("@Value_5", SqlDbType.Float),
                    new SqlParameter("@Month_5", SqlDbType.Float),
                    new SqlParameter("@Value_6", SqlDbType.Float),
                    new SqlParameter("@Month_6", SqlDbType.Float),
                    new SqlParameter("@Value_7", SqlDbType.Float),
                    new SqlParameter("@Month_7", SqlDbType.Float),
                    new SqlParameter("@Value_8", SqlDbType.Float),
                    new SqlParameter("@Month_8", SqlDbType.Float),
                    new SqlParameter("@Value_9", SqlDbType.Float),
                    new SqlParameter("@Month_9", SqlDbType.Float),
                    new SqlParameter("@Value_10", SqlDbType.Float),
                    new SqlParameter("@Month_10", SqlDbType.Float),
                    new SqlParameter("@Value_11", SqlDbType.Float),
                    new SqlParameter("@Month_11", SqlDbType.Float),
                    new SqlParameter("@Value_12", SqlDbType.Float),
                    new SqlParameter("@Month_12", SqlDbType.Float),
                    new SqlParameter("@DataType", SqlDbType.Int),
                    new SqlParameter("@SalaryRegulationId", SqlDbType.Int)
                };

                param[0].Value = coefficientName;

                param[1].Value = value_1;
                param[2].Value = month_1;
                param[3].Value = value_2;
                param[4].Value = month_2;
                param[5].Value = value_3;
                param[6].Value = month_3;
                param[7].Value = value_4;
                param[8].Value = month_4;
                param[9].Value = value_5;
                param[10].Value = month_5;
                param[11].Value = value_6;
                param[12].Value = month_6;
                param[13].Value = value_7;
                param[14].Value = month_7;
                param[15].Value = value_8;
                param[16].Value = month_8;
                param[17].Value = value_9;
                param[18].Value = month_9;
                param[19].Value = value_10;
                param[20].Value = month_10;
                param[21].Value = value_11;
                param[22].Value = month_11;
                param[23].Value = value_12;
                param[24].Value = month_12;

                param[25].Value = dataType;
                param[26].Value = salaryRegulationId;

                sproc = new StoreProcedure(CoefficientTypeKeys.Ins_H1_CoefficientType, param);
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
            double value_1, double month_1, double value_2, double month_2, double value_3, double month_3,
            double value_4, double month_4, double value_5, double month_5, double value_6, double month_6,
            double value_7, double month_7, double value_8, double month_8, double value_9, double month_9,
            double value_10, double month_10, double value_11, double month_11, double value_12, double month_12,
            int dataType, int salaryRegulationId, int coefficientTypeId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@CoefficientName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@Value_1", SqlDbType.Float),
                    new SqlParameter("@Month_1", SqlDbType.Float),
                    new SqlParameter("@Value_2", SqlDbType.Float),
                    new SqlParameter("@Month_2", SqlDbType.Float),
                    new SqlParameter("@Value_3", SqlDbType.Float),
                    new SqlParameter("@Month_3", SqlDbType.Float),
                    new SqlParameter("@Value_4", SqlDbType.Float),
                    new SqlParameter("@Month_4", SqlDbType.Float),
                    new SqlParameter("@Value_5", SqlDbType.Float),
                    new SqlParameter("@Month_5", SqlDbType.Float),
                    new SqlParameter("@Value_6", SqlDbType.Float),
                    new SqlParameter("@Month_6", SqlDbType.Float),
                    new SqlParameter("@Value_7", SqlDbType.Float),
                    new SqlParameter("@Month_7", SqlDbType.Float),
                    new SqlParameter("@Value_8", SqlDbType.Float),
                    new SqlParameter("@Month_8", SqlDbType.Float),
                    new SqlParameter("@Value_9", SqlDbType.Float),
                    new SqlParameter("@Month_9", SqlDbType.Float),
                    new SqlParameter("@Value_10", SqlDbType.Float),
                    new SqlParameter("@Month_10", SqlDbType.Float),
                    new SqlParameter("@Value_11", SqlDbType.Float),
                    new SqlParameter("@Month_11", SqlDbType.Float),
                    new SqlParameter("@Value_12", SqlDbType.Float),
                    new SqlParameter("@Month_12", SqlDbType.Float),
                    new SqlParameter("@DataType", SqlDbType.Int),
                    new SqlParameter("@SalaryRegulationId", SqlDbType.Int),
                    new SqlParameter("@CoefficientTypeId", SqlDbType.Int)
                };

                param[0].Value = coefficientName;

                param[1].Value = value_1;
                param[2].Value = month_1;
                param[3].Value = value_2;
                param[4].Value = month_2;
                param[5].Value = value_3;
                param[6].Value = month_3;
                param[7].Value = value_4;
                param[8].Value = month_4;
                param[9].Value = value_5;
                param[10].Value = month_5;
                param[11].Value = value_6;
                param[12].Value = month_6;
                param[13].Value = value_7;
                param[14].Value = month_7;
                param[15].Value = value_8;
                param[16].Value = month_8;
                param[17].Value = value_9;
                param[18].Value = month_9;
                param[19].Value = value_10;
                param[20].Value = month_10;
                param[21].Value = value_11;
                param[22].Value = month_11;
                param[23].Value = value_12;
                param[24].Value = month_12;

                param[25].Value = dataType;
                param[26].Value = salaryRegulationId;
                param[27].Value = coefficientTypeId;

                sproc = new StoreProcedure(CoefficientTypeKeys.Upd_H1_CoefficientType, param);
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

        public int Delete(int id)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@CoefficientTypeId", SqlDbType.Int)
                };

                param[0].Value = id;

                sproc = new StoreProcedure(CoefficientTypeKeys.Del_H1_CoefficientType_By_Id, param);
                sproc.Run();
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            return identity;
        }

        #endregion
    }
}