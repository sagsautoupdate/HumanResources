using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H2;

namespace HRMDAL.H2
{
    public class CandidateTrainingJobHistoryDAL : Dao
    {
        #region Methods Get

        //public DataTable GetAll()
        //{

        //    Debug.Assert(sproc == null);
        //    DataTable datatable = new DataTable();
        //    try
        //    {
        //        sproc = new StoreProcedure(EducationLevelKeys.SP_EDUCATION_LEVEL_GET_ALL, null);
        //        sproc.RunFill(datatable);
        //        sproc.Dispose();
        //    }
        //    catch (SqlException se)
        //    {
        //        throw new HRMException(se.Message, se.Number);
        //    }

        //    return datatable;

        //}

        public DataTable GetByCandidateId_Type(int CandidateId, string Type)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@CandidateId", SqlDbType.Int),
                    new SqlParameter("@Type", SqlDbType.VarChar, 10)
                };

                param[0].Value = CandidateId;
                param[1].Value = Type;

                sproc =
                    new StoreProcedure(
                        CandidateTrainingJobHistoryKeys.Sp_Sel_H2_CandidateTrainingJobHistory_By_CandidateId_Type, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetById(int id)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@CandidateTrainingJobHistoryId", SqlDbType.Int)
                };

                param[0].Value = id;

                sproc = new StoreProcedure("Sel_H2_CandidateTrainingJobHistory_By_Id", param);
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

        #region methods inset, update , delete

        public long Insert(int CandidateId, string Training_Job, string Year, string School_Position,
            string Major_Salary, string GraduateYear_LeaveReason, string Experience, string Type)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@CandidateId", SqlDbType.Int),
                    new SqlParameter("@Training_Job", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Year", SqlDbType.VarChar, 50),
                    new SqlParameter("@School_Position", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Major_Salary", SqlDbType.NVarChar, 254),
                    new SqlParameter("@GraduateYear_LeaveReason", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Experience", SqlDbType.NText),
                    new SqlParameter("@Type", SqlDbType.VarChar, 10)
                };


                param[0].Value = CandidateId;
                param[1].Value = Training_Job;
                param[2].Value = Year;
                param[3].Value = School_Position;
                param[4].Value = Major_Salary;
                param[5].Value = GraduateYear_LeaveReason;
                param[6].Value = Experience;
                param[7].Value = Type;

                sproc = new StoreProcedure(CandidateTrainingJobHistoryKeys.Sp_Ins_H2_CandidateTrainingJobHistory, param);
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

        public long Update(int CandidateId, string Training_Job, string Year, string School_Position,
            string Major_Salary, string GraduateYear_LeaveReason, string Experience, string Type,
            int CandidateTrainingJobHistoryId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@CandidateId", SqlDbType.Int),
                    new SqlParameter("@Training_Job", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Year", SqlDbType.VarChar, 50),
                    new SqlParameter("@School_Position", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Major_Salary", SqlDbType.NVarChar, 254),
                    new SqlParameter("@GraduateYear_LeaveReason", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Experience", SqlDbType.NText),
                    new SqlParameter("@Type", SqlDbType.VarChar, 10),
                    new SqlParameter("@CandidateTrainingJobHistoryId", SqlDbType.Int)
                };


                param[0].Value = CandidateId;
                param[1].Value = Training_Job;
                param[2].Value = Year;
                param[3].Value = School_Position;
                param[4].Value = Major_Salary;
                param[5].Value = GraduateYear_LeaveReason;
                param[6].Value = Experience;
                param[7].Value = Type;
                param[8].Value = CandidateTrainingJobHistoryId;

                sproc = new StoreProcedure(CandidateTrainingJobHistoryKeys.Sp_Upd_H2_CandidateTrainingJobHistory, param);
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

        public long Delete(int CandidateTrainingJobHistoryId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@CandidateTrainingJobHistoryId", SqlDbType.Int)
                };

                param[0].Value = CandidateTrainingJobHistoryId;
                sproc = new StoreProcedure(CandidateTrainingJobHistoryKeys.Sp_Del_H2_CandidateTrainingJobHistory, param);
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