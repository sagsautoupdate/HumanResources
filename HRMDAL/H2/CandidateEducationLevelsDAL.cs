using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H2;

namespace HRMDAL.H2
{
    public class CandidateEducationLevelsDAL : Dao
    {
        #region Methods Get

        //public DataTable GetAllIsNull()
        //{

        //    Debug.Assert(sproc == null);
        //    DataTable datatable = new DataTable();
        //    try
        //    {
        //        sproc = new StoreProcedure(CandidateEducationLevelKeys.SP_CANDIDATE_EDUCATION_LEVEL_GET_IS_NULL, null);
        //        sproc.RunFill(datatable);
        //        sproc.Dispose();
        //    }
        //    catch (SqlException se)
        //    {
        //        throw new HRMException(se.Message, se.Number);
        //    }

        //    return datatable;

        //}

        public DataTable GetById(int candidateId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@CandidateId", SqlDbType.Int)
                };

                param[0].Value = candidateId;

                sproc = new StoreProcedure(CandidateEducationLevelKeys.SP_CANDIDATE_EDUCATION_LEVEL_GET_BY_ID, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByCandidateIdEducationId(int candidateId, int eduId)
        {
            Debug.Assert(sproc == null);
            var table = new DataTable();
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@CandidateId", SqlDbType.Int),
                    new SqlParameter("@EducationLevelId", SqlDbType.Int)
                };
                parameters[0].Value = candidateId;
                parameters[1].Value = eduId;
                sproc = new StoreProcedure("Sel_H2_CandidateEducationLevel_Id_EduId", parameters);
                sproc.RunFill(table);
                sproc.Dispose();
            }
            catch (SqlException exception)
            {
                throw new HRMException(exception.Message, exception.Number);
            }
            return table;
        }

        #endregion

        #region methods inset, update , delete

        public long Insert(int candidateId, int educationLevelId, string educationLevelValue, string remark)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@CandidateId", SqlDbType.Int),
                    new SqlParameter("@EducationLevelId", SqlDbType.Int),
                    new SqlParameter("@EducationLevelValue", SqlDbType.NVarChar, 100),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 1000)
                };

                param[0].Value = candidateId;
                param[1].Value = educationLevelId;
                param[2].Value = educationLevelValue;
                param[3].Value = remark;

                sproc = new StoreProcedure(CandidateEducationLevelKeys.SP_CANDIDATE_EDUCATION_LEVEL_INSERT, param);
                identity = sproc.RunInt();
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

        public long Update(int candidateId, int educationLevelId, string educationLevelValue, string remark, int id)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@CandidateId", SqlDbType.Int),
                    new SqlParameter("@EducationLevelId", SqlDbType.Int),
                    new SqlParameter("@EducationLevelValue", SqlDbType.NVarChar, 100),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@Id", SqlDbType.Int)
                };

                param[0].Value = candidateId;
                param[1].Value = educationLevelId;
                param[2].Value = educationLevelValue;
                param[3].Value = remark == null ? string.Empty : remark;
                param[4].Value = id;

                sproc = new StoreProcedure(CandidateEducationLevelKeys.SP_CANDIDATE_EDUCATION_LEVEL_UPDATE, param);
                identity = sproc.RunInt();
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

        public long Delete(int id)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Id", SqlDbType.Int)
                };

                param[0].Value = id;
                sproc = new StoreProcedure(CandidateEducationLevelKeys.SP_CANDIDATE_EDUCATION_LEVEL_DELETE, param);
                identity = sproc.RunInt();
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