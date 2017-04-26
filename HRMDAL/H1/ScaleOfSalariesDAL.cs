using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H1;

namespace HRMDAL.H1
{
    public class ScaleOfSalariesDAL : Dao
    {
        #region Get

        public DataTable GetAll()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure(ScaleOfSalariesKeys.Sp_Sel_H1_ScaleOfSalaries_ByAll, null);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }
            return datatable;
        }

        public DataTable GetAllWithFilter(int Active)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Active", SqlDbType.Int)
                };

                param[0].Value = Active;

                sproc = new StoreProcedure("Sel_H1_ScaleOfSalaries_ByAll_Filter", param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }
            return datatable;
        }

        public DataTable GetOne(int ScaleOfSalaryId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@ScaleOfSalaryId", SqlDbType.Int)
                };

                param[0].Value = ScaleOfSalaryId;

                sproc = new StoreProcedure(ScaleOfSalariesKeys.Sp_Sel_H1_ScaleOfSalaries_GetOne, param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }
            return datatable;
        }

        public DataTable GetByName(string PositionName)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@PositionName", SqlDbType.NVarChar, 4000)
                };

                param[0].Value = PositionName;

                sproc = new StoreProcedure(ScaleOfSalariesKeys.Sp_Sel_H1_ScaleOfSalaries_GetByName, param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }
            return datatable;
        }

        #endregion

        #region Insert, Update, Delete

        public long Insert_Coefficient(string PositionName, string Code, double Value1, double Value2, double Value3,
            string JobDescription, DateTime AppliedDate)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@PositionName", SqlDbType.NVarChar, 5000),
                    new SqlParameter("@Code", SqlDbType.VarChar, 10),
                    new SqlParameter("@Value1", SqlDbType.Float),
                    new SqlParameter("@Value2", SqlDbType.Float),
                    new SqlParameter("@Value3", SqlDbType.Float),
                    new SqlParameter("@JobDescription", SqlDbType.NText),
                    new SqlParameter("@AppliedDate", SqlDbType.DateTime)
                };

                param[0].Value = PositionName;
                param[1].Value = Code;
                param[2].Value = Value1;
                param[3].Value = Value2;
                param[4].Value = Value3;
                param[5].Value = JobDescription;
                param[6].Value = AppliedDate;

                sproc = new StoreProcedure("Ins_H1_SOS_Coefficient", param);
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

        public long Update_Coefficient(int ScaleOfSalaryId, string PositionName, string Code, double Value1,
            double Value2,
            double Value3, string JobDescription, DateTime AppliedDate)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@ScaleOfSalaryId", SqlDbType.Int),
                    new SqlParameter("@PositionName", SqlDbType.NVarChar, 5000),
                    new SqlParameter("@Code", SqlDbType.VarChar, 10),
                    new SqlParameter("@Value1", SqlDbType.Float),
                    new SqlParameter("@Value2", SqlDbType.Float),
                    new SqlParameter("@Value3", SqlDbType.Float),
                    new SqlParameter("@JobDescription", SqlDbType.NText),
                    new SqlParameter("@AppliedDate", SqlDbType.DateTime)
                };

                param[0].Value = ScaleOfSalaryId;
                param[1].Value = PositionName;
                param[2].Value = Code;
                param[3].Value = Value1;
                param[4].Value = Value2;
                param[5].Value = Value3;
                param[6].Value = JobDescription;
                param[7].Value = AppliedDate;

                sproc = new StoreProcedure("Upd_H1_SOS_Coefficient", param);
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

        public long Insert(string PositionName, string Code, double Value1, double Value2, double Value3,
            string JobDescription, DateTime AppliedDate)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@PositionName", SqlDbType.NVarChar, 5000),
                    new SqlParameter("@Code", SqlDbType.VarChar, 10),
                    new SqlParameter("@Value1", SqlDbType.Float),
                    new SqlParameter("@Value2", SqlDbType.Float),
                    new SqlParameter("@Value3", SqlDbType.Float),
                    new SqlParameter("@JobDescription", SqlDbType.NText),
                    new SqlParameter("@AppliedDate", SqlDbType.DateTime)
                };

                param[0].Value = PositionName;
                param[1].Value = Code;
                param[2].Value = Value1;
                param[3].Value = Value2;
                param[4].Value = Value3;
                param[5].Value = JobDescription;
                param[6].Value = AppliedDate;

                sproc = new StoreProcedure(ScaleOfSalariesKeys.Sp_Ins_H1_ScaleOfSalaries, param);
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

        public long Update(int ScaleOfSalaryId, string PositionName, string Code, double Value1, double Value2,
            double Value3, string JobDescription, DateTime AppliedDate)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@ScaleOfSalaryId", SqlDbType.Int),
                    new SqlParameter("@PositionName", SqlDbType.NVarChar, 5000),
                    new SqlParameter("@Code", SqlDbType.VarChar, 10),
                    new SqlParameter("@Value1", SqlDbType.Float),
                    new SqlParameter("@Value2", SqlDbType.Float),
                    new SqlParameter("@Value3", SqlDbType.Float),
                    new SqlParameter("@JobDescription", SqlDbType.NText),
                    new SqlParameter("@AppliedDate", SqlDbType.DateTime)
                };

                param[0].Value = ScaleOfSalaryId;
                param[1].Value = PositionName;
                param[2].Value = Code;
                param[3].Value = Value1;
                param[4].Value = Value2;
                param[5].Value = Value3;
                param[6].Value = JobDescription;
                param[7].Value = AppliedDate;

                sproc = new StoreProcedure(ScaleOfSalariesKeys.Sp_Upd_H1_ScaleOfSalaries, param);
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

        public long Delete(int ScaleOfSalaryId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@ScaleOfSalaryId", SqlDbType.Int)
                };

                param[0].Value = ScaleOfSalaryId;

                sproc = new StoreProcedure(ScaleOfSalariesKeys.Sp_Del_H1_ScaleOfSalaries, param);
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
    }
}