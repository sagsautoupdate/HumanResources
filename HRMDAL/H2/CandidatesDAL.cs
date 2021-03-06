using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H2;

namespace HRMDAL.H2
{
    public class CandidatesDAL : Dao
    {
        #region Methods Get

        public DataTable GetAll()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure(CandidatesKeys.SP_CANDIDATE_GET_BY_ALL, null);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByFilter(string firstName, int positionId, int result, int sessionId, int type,
            int sessionType, int TypeSort)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FirstName", SqlDbType.NVarChar, 50),
                    new SqlParameter("@PositionId", SqlDbType.Int),
                    new SqlParameter("@Result", SqlDbType.Int),
                    new SqlParameter("@SessionId", SqlDbType.Int),
                    new SqlParameter("@Type", SqlDbType.Int),
                    new SqlParameter("@SessionType", SqlDbType.Int),
                    new SqlParameter("@TypeSort", SqlDbType.Int)
                };

                param[0].Value = firstName;
                param[1].Value = positionId;
                param[2].Value = result;
                param[3].Value = sessionId;
                param[4].Value = type;
                param[5].Value = sessionType;
                param[6].Value = TypeSort;

                sproc = new StoreProcedure(CandidatesKeys.SP_CANDIDATE_GET_BY_FILTER, param);
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
                    new SqlParameter("@Id", SqlDbType.Int)
                };

                param[0].Value = id;

                sproc = new StoreProcedure(CandidatesKeys.SP_CANDIDATE_GET_BY_ID, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetForTrainingByFilter(string fullName, int sessionId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@SessionId", SqlDbType.Int)
                };

                param[0].Value = fullName;
                param[1].Value = sessionId;

                sproc = new StoreProcedure("Sel_H2_Candidate_ForTraining_ByFilterV1", param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetForTrainingByFilterPreEmployee(string fullName, int sessionId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@SessionId", SqlDbType.Int)
                };

                param[0].Value = fullName;
                param[1].Value = sessionId;

                sproc = new StoreProcedure("Sel_H2_Candidate_ForTraining_ByFilter", param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable Login(string userName, string password)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserName", SqlDbType.VarChar, 50),
                    new SqlParameter("@Password", SqlDbType.VarChar, 50)
                };
                param[0].Value = userName;
                param[1].Value = password;

                sproc = new StoreProcedure(CandidatesKeys.Sp_LoginCandidate, param);
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

        #region methods inset, update , delete

        public long Insert(string lastName, string firstName, int dayOfBirth, int monthOfBirth,
            int yearOfBirth, bool sex, string experience, double height,
            string homePhone, string handPhone, string remark, int type, string reson, int result, int positionId,
            int sessionId, string health, int CreateUserId, string lastName1, string firstName1, string userName,
            string password)

        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@LastName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@FirstName", SqlDbType.NVarChar, 50),
                    new SqlParameter("@DayOfBirth", SqlDbType.Int),
                    new SqlParameter("@MonthOfBirth", SqlDbType.Int),
                    new SqlParameter("@YearOfBirth", SqlDbType.Int),
                    new SqlParameter("@Sex", SqlDbType.Bit),
                    new SqlParameter("@Experience", SqlDbType.NVarChar, 4000),
                    new SqlParameter("@Height", SqlDbType.Float),
                    new SqlParameter("@HomePhone", SqlDbType.VarChar, 50),
                    new SqlParameter("@HandPhone", SqlDbType.VarChar, 50),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@Type", SqlDbType.Int),
                    new SqlParameter("@Reson", SqlDbType.NVarChar, 100),
                    new SqlParameter("@Result", SqlDbType.Int),
                    new SqlParameter("@PositionId", SqlDbType.Int),
                    new SqlParameter("@SessionId", SqlDbType.Int),
                    new SqlParameter("@Health", SqlDbType.NVarChar, 254),
                    new SqlParameter("@CreateUserId", SqlDbType.Int),
                    new SqlParameter("@LastName1", SqlDbType.VarChar, 100),
                    new SqlParameter("@FirstName1", SqlDbType.VarChar, 50),
                    new SqlParameter("@UserName", SqlDbType.VarChar, 50),
                    new SqlParameter("@Password", SqlDbType.VarChar, 254)
                };

                param[0].Value = lastName;
                param[1].Value = firstName;
                param[2].Value = dayOfBirth;
                param[3].Value = monthOfBirth;
                param[4].Value = yearOfBirth;
                param[5].Value = sex;
                param[6].Value = experience;
                param[7].Value = height;
                param[8].Value = homePhone;
                param[9].Value = handPhone;
                param[10].Value = remark;
                param[11].Value = type;
                param[12].Value = reson;
                param[13].Value = result;
                param[14].Value = positionId;
                param[15].Value = sessionId;
                param[16].Value = health;
                param[17].Value = CreateUserId;
                param[18].Value = lastName1;
                param[19].Value = firstName1;
                param[20].Value = userName;
                param[21].Value = password;

                sproc = new StoreProcedure(CandidatesKeys.Sp_Ins_H2_Candidates, param);
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

        public long Update(string lastName, string firstName, int dayOfBirth, int monthOfBirth,
            int yearOfBirth, bool sex, string experience, double height,
            string homePhone, string handPhone, string remark, int type, string reson, int result, int positionId,
            int candidateId, int sessionId, string health, int UpdateUserId, string lastName1, string firstName1,
            string userName, string password)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@LastName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@FirstName", SqlDbType.NVarChar, 50),
                    new SqlParameter("@DayOfBirth", SqlDbType.Int),
                    new SqlParameter("@MonthOfBirth", SqlDbType.Int),
                    new SqlParameter("@YearOfBirth", SqlDbType.Int),
                    new SqlParameter("@Sex", SqlDbType.Bit),
                    new SqlParameter("@Experience", SqlDbType.NVarChar, 4000),
                    new SqlParameter("@Height", SqlDbType.Float),
                    new SqlParameter("@HomePhone", SqlDbType.VarChar, 50),
                    new SqlParameter("@HandPhone", SqlDbType.VarChar, 50),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@Type", SqlDbType.Int),
                    new SqlParameter("@Reson", SqlDbType.NVarChar, 100),
                    new SqlParameter("@Result", SqlDbType.Int),
                    new SqlParameter("@PositionId", SqlDbType.Int),
                    new SqlParameter("@CandidateId", SqlDbType.Int),
                    new SqlParameter("@SessionId", SqlDbType.Int),
                    new SqlParameter("@Health", SqlDbType.NVarChar, 254),
                    new SqlParameter("@UpdateUserId", SqlDbType.Int),
                    new SqlParameter("@LastName1", SqlDbType.VarChar, 100),
                    new SqlParameter("@FirstName1", SqlDbType.VarChar, 50),
                    new SqlParameter("@UserName", SqlDbType.VarChar, 50),
                    new SqlParameter("@Password", SqlDbType.VarChar, 254)
                };

                param[0].Value = lastName;
                param[1].Value = firstName;
                param[2].Value = dayOfBirth;
                param[3].Value = monthOfBirth;
                param[4].Value = yearOfBirth;
                param[5].Value = sex;
                param[6].Value = experience;
                param[7].Value = height;
                param[8].Value = homePhone;
                param[9].Value = handPhone;
                param[10].Value = remark;
                param[11].Value = type;
                param[12].Value = reson;
                param[13].Value = result;
                param[14].Value = positionId;
                param[15].Value = candidateId;
                param[16].Value = sessionId;
                param[17].Value = health;
                param[18].Value = UpdateUserId;
                param[19].Value = lastName1;
                param[20].Value = firstName1;
                param[21].Value = userName;
                param[22].Value = password;

                sproc = new StoreProcedure(CandidatesKeys.Sp_Upd_H2_Candidates, param);
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

        public long UpdateForLastFirstName1(string lastName1, string firstName1, int candidateId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@LastName1", SqlDbType.VarChar, 100),
                    new SqlParameter("@FirstName1", SqlDbType.VarChar, 50),
                    new SqlParameter("@CandidateId", SqlDbType.Int)
                };

                param[0].Value = lastName1;
                param[1].Value = firstName1;
                param[2].Value = candidateId;

                sproc = new StoreProcedure(CandidatesKeys.Sp_Upd_H2_CandidatesForLastFirstName1, param);
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


        public long UpdateMark_RP(
            double? NNGK1,
            double? NNGK2,
            double? NNGK3,
            double? NNTB,
            double? NHGK1,
            double? NHGK2,
            double? NHGK3,
            double? NHTB,
            double? PCGK1,
            double? PCGK2,
            double? PCGK3,
            double? PCTB,
            double? KNGK1,
            double? KNGK2,
            double? KNGK3,
            double? KNTB,
            double? DHGK1,
            double? DHGK2,
            double? DHGK3,
            double? DHTB,
            double? DHNNGK1,
            double? DHNNGK2,
            double? DHNNGK3,
            double? DHNNTB,
            int? Result,
            int? CandidateId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@NNGK1", SqlDbType.Float, 8),
                    new SqlParameter("@NNGK2", SqlDbType.Float, 8),
                    new SqlParameter("@NNGK3", SqlDbType.Float, 8),
                    new SqlParameter("@NNTB", SqlDbType.Float, 8),
                    new SqlParameter("@NHGK1", SqlDbType.Float, 8),
                    new SqlParameter("@NHGK2", SqlDbType.Float, 8),
                    new SqlParameter("@NHGK3", SqlDbType.Float, 8),
                    new SqlParameter("@NHTB", SqlDbType.Float, 8),
                    new SqlParameter("@PCGK1", SqlDbType.Float, 8),
                    new SqlParameter("@PCGK2", SqlDbType.Float, 8),
                    new SqlParameter("@PCGK3", SqlDbType.Float, 8),
                    new SqlParameter("@PCTB", SqlDbType.Float, 8),
                    new SqlParameter("@KNGK1", SqlDbType.Float, 8),
                    new SqlParameter("@KNGK2", SqlDbType.Float, 8),
                    new SqlParameter("@KNGK3", SqlDbType.Float, 8),
                    new SqlParameter("@KNTB", SqlDbType.Float, 8),
                    new SqlParameter("@DHGK1", SqlDbType.Float, 8),
                    new SqlParameter("@DHGK2", SqlDbType.Float, 8),
                    new SqlParameter("@DHGK3", SqlDbType.Float, 8),
                    new SqlParameter("@DHTB", SqlDbType.Float, 8),
                    new SqlParameter("@DHNNGK1", SqlDbType.Float, 8),
                    new SqlParameter("@DHNNGK2", SqlDbType.Float, 8),
                    new SqlParameter("@DHNNGK3", SqlDbType.Float, 8),
                    new SqlParameter("@DHNNTB", SqlDbType.Float, 8),
                    new SqlParameter("@Result", SqlDbType.Int, 4),
                    new SqlParameter("@CandidateId", SqlDbType.Int, 4)
                };

                if (NNGK1.HasValue)
                    param[0].Value = NNGK1.Value;
                else
                    param[0].Value = DBNull.Value;
                if (NNGK2.HasValue)
                    param[1].Value = NNGK2.Value;
                else
                    param[1].Value = DBNull.Value;
                if (NNGK3.HasValue)
                    param[2].Value = NNGK3.Value;
                else
                    param[2].Value = DBNull.Value;
                if (NNTB.HasValue)
                    param[3].Value = NNTB.Value;
                else
                    param[3].Value = DBNull.Value;
                if (NHGK1.HasValue)
                    param[4].Value = NHGK1.Value;
                else
                    param[4].Value = DBNull.Value;
                if (NHGK2.HasValue)
                    param[5].Value = NHGK2.Value;
                else
                    param[5].Value = DBNull.Value;
                if (NHGK3.HasValue)
                    param[6].Value = NHGK3.Value;
                else
                    param[6].Value = DBNull.Value;
                if (NHTB.HasValue)
                    param[7].Value = NHTB.Value;
                else
                    param[7].Value = DBNull.Value;
                if (PCGK1.HasValue)
                    param[8].Value = PCGK1.Value;
                else
                    param[8].Value = DBNull.Value;
                if (PCGK2.HasValue)
                    param[9].Value = PCGK2.Value;
                else
                    param[9].Value = DBNull.Value;
                if (PCGK3.HasValue)
                    param[10].Value = PCGK3.Value;
                else
                    param[10].Value = DBNull.Value;
                if (PCTB.HasValue)
                    param[11].Value = PCTB.Value;
                else
                    param[11].Value = DBNull.Value;
                if (KNGK1.HasValue)
                    param[12].Value = KNGK1.Value;
                else
                    param[12].Value = DBNull.Value;
                if (KNGK2.HasValue)
                    param[13].Value = KNGK2.Value;
                else
                    param[13].Value = DBNull.Value;
                if (KNGK3.HasValue)
                    param[14].Value = KNGK3.Value;
                else
                    param[14].Value = DBNull.Value;
                if (KNTB.HasValue)
                    param[15].Value = KNTB.Value;
                else
                    param[15].Value = DBNull.Value;

                if (DHGK1.HasValue)
                    param[16].Value = DHGK1.Value;
                else
                    param[16].Value = DBNull.Value;
                if (DHGK2.HasValue)
                    param[17].Value = DHGK2.Value;
                else
                    param[17].Value = DBNull.Value;
                if (DHGK3.HasValue)
                    param[18].Value = DHGK3.Value;
                else
                    param[18].Value = DBNull.Value;
                if (DHTB.HasValue)
                    param[19].Value = DHTB.Value;
                else
                    param[19].Value = DBNull.Value;

                if (DHNNGK1.HasValue)
                    param[20].Value = DHNNGK1.Value;
                else
                    param[20].Value = DBNull.Value;
                if (DHNNGK2.HasValue)
                    param[21].Value = DHNNGK2.Value;
                else
                    param[21].Value = DBNull.Value;
                if (DHNNGK3.HasValue)
                    param[22].Value = DHNNGK3.Value;
                else
                    param[22].Value = DBNull.Value;
                if (DHNNTB.HasValue)
                    param[23].Value = DHNNTB.Value;
                else
                    param[23].Value = DBNull.Value;
                if (Result.HasValue)
                    param[24].Value = Result.Value;
                else
                    param[24].Value = DBNull.Value;
                if (CandidateId.HasValue)
                    param[25].Value = CandidateId.Value;
                else
                    param[25].Value = DBNull.Value;
                sproc = new StoreProcedure(CandidatesKeys.Sp_Upd_H2_Candidates_Mark_RP, param);
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

        public long UpdateMark_RL(
            double? NNLRGK1,
            double? NNLRGK2,
            double? NNLRGK3,
            double? NNLRTB,
            double? NHLRGK1,
            double? NHLRGK2,
            double? NHLRGK3,
            double? NHLRTB,
            int? Result,
            int? CandidateId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@NNLRGK1", SqlDbType.Float, 8),
                    new SqlParameter("@NNLRGK2", SqlDbType.Float, 8),
                    new SqlParameter("@NNLRGK3", SqlDbType.Float, 8),
                    new SqlParameter("@NNLRTB", SqlDbType.Float, 8),
                    new SqlParameter("@NHLRGK1", SqlDbType.Float, 8),
                    new SqlParameter("@NHLRGK2", SqlDbType.Float, 8),
                    new SqlParameter("@NHLRGK3", SqlDbType.Float, 8),
                    new SqlParameter("@NHLRTB", SqlDbType.Float, 8),
                    new SqlParameter("@Result", SqlDbType.Int, 4),
                    new SqlParameter("@CandidateId", SqlDbType.Int, 4)
                };

                if (NNLRGK1.HasValue)
                    param[0].Value = NNLRGK1.Value;
                else
                    param[0].Value = DBNull.Value;
                if (NNLRGK2.HasValue)
                    param[1].Value = NNLRGK2.Value;
                else
                    param[1].Value = DBNull.Value;
                if (NNLRGK3.HasValue)
                    param[2].Value = NNLRGK3.Value;
                else
                    param[2].Value = DBNull.Value;
                if (NNLRTB.HasValue)
                    param[3].Value = NNLRTB.Value;
                else
                    param[3].Value = DBNull.Value;
                if (NHLRGK1.HasValue)
                    param[4].Value = NHLRGK1.Value;
                else
                    param[4].Value = DBNull.Value;
                if (NHLRGK2.HasValue)
                    param[5].Value = NHLRGK2.Value;
                else
                    param[5].Value = DBNull.Value;
                if (NHLRGK3.HasValue)
                    param[6].Value = NHLRGK3.Value;
                else
                    param[6].Value = DBNull.Value;
                if (NHLRTB.HasValue)
                    param[7].Value = NHLRTB.Value;
                else
                    param[7].Value = DBNull.Value;


                if (Result.HasValue)
                    param[8].Value = Result.Value;
                else
                    param[8].Value = DBNull.Value;
                if (CandidateId.HasValue)
                    param[9].Value = CandidateId.Value;
                else
                    param[9].Value = DBNull.Value;

                sproc = new StoreProcedure(CandidatesKeys.Sp_Upd_H2_Candidates_Mark_RL, param);
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


        public long UpdateMark_R1(double? DHTB, double? DHGK1, double? DHGK2, double? DHGK3, int? Result, string Remark1,
            int? CandidateId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DHTB", SqlDbType.Float, 8),
                    new SqlParameter("@DHGK1", SqlDbType.Float, 8),
                    new SqlParameter("@DHGK2", SqlDbType.Float, 8),
                    new SqlParameter("@DHGK3", SqlDbType.Float, 8),
                    new SqlParameter("@Result", SqlDbType.Int, 4),
                    new SqlParameter("@Remark1", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@CandidateId", SqlDbType.Int, 4)
                };


                if (DHTB.HasValue)
                    param[0].Value = DHTB.Value;
                else
                    param[0].Value = DBNull.Value;
                if (DHGK1.HasValue)
                    param[1].Value = DHGK1.Value;
                else
                    param[1].Value = DBNull.Value;
                if (DHGK2.HasValue)
                    param[2].Value = DHGK2.Value;
                else
                    param[2].Value = DBNull.Value;
                if (DHGK3.HasValue)
                    param[3].Value = DHGK3.Value;
                else
                    param[3].Value = DBNull.Value;
                if (Result.HasValue)
                    param[4].Value = Result.Value;
                else
                    param[4].Value = DBNull.Value;
                if (Remark1 != null)
                    param[5].Value = Remark1;
                else
                    param[5].Value = DBNull.Value;
                if (CandidateId.HasValue)
                    param[6].Value = CandidateId.Value;
                else
                    param[6].Value = DBNull.Value;
                sproc = new StoreProcedure(CandidatesKeys.Sp_Upd_H2_Candidates_Mark_R1, param);
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

        public long UpdateMark_R2(
            double? NNGK1,
            double? NNGK2,
            double? NNGK3,
            double? NNTB,
            double? NHGK1,
            double? NHGK2,
            double? NHGK3,
            double? NHTB,
            double? PCGK1,
            double? PCGK2,
            double? PCGK3,
            double? PCTB,
            double? KNGK1,
            double? KNGK2,
            double? KNGK3,
            double? KNTB,
            double? DHNNGK1,
            double? DHNNGK2,
            double? DHNNGK3,
            double? DHNNTB,
            int? Result,
            string Remark2,
            int? CandidateId,
            double? DHTB
        )
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@NNGK1", SqlDbType.Float, 8),
                    new SqlParameter("@NNGK2", SqlDbType.Float, 8),
                    new SqlParameter("@NNGK3", SqlDbType.Float, 8),
                    new SqlParameter("@NNTB", SqlDbType.Float, 8),
                    new SqlParameter("@NHGK1", SqlDbType.Float, 8),
                    new SqlParameter("@NHGK2", SqlDbType.Float, 8),
                    new SqlParameter("@NHGK3", SqlDbType.Float, 8),
                    new SqlParameter("@NHTB", SqlDbType.Float, 8),
                    new SqlParameter("@PCGK1", SqlDbType.Float, 8),
                    new SqlParameter("@PCGK2", SqlDbType.Float, 8),
                    new SqlParameter("@PCGK3", SqlDbType.Float, 8),
                    new SqlParameter("@PCTB", SqlDbType.Float, 8),
                    new SqlParameter("@KNGK1", SqlDbType.Float, 8),
                    new SqlParameter("@KNGK2", SqlDbType.Float, 8),
                    new SqlParameter("@KNGK3", SqlDbType.Float, 8),
                    new SqlParameter("@KNTB", SqlDbType.Float, 8),
                    new SqlParameter("@DHNNGK1", SqlDbType.Float, 8),
                    new SqlParameter("@DHNNGK2", SqlDbType.Float, 8),
                    new SqlParameter("@DHNNGK3", SqlDbType.Float, 8),
                    new SqlParameter("@DHNNTB", SqlDbType.Float, 8),
                    new SqlParameter("@Result", SqlDbType.Int, 4),
                    new SqlParameter("@RemarkR2", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@CandidateId", SqlDbType.Int, 4),
                    new SqlParameter("@DHTB", SqlDbType.Float, 8)
                };

                if (NNGK1.HasValue)
                    param[0].Value = NNGK1.Value;
                else
                    param[0].Value = DBNull.Value;
                if (NNGK2.HasValue)
                    param[1].Value = NNGK2.Value;
                else
                    param[1].Value = DBNull.Value;
                if (NNGK3.HasValue)
                    param[2].Value = NNGK3.Value;
                else
                    param[2].Value = DBNull.Value;
                if (NNTB.HasValue)
                    param[3].Value = NNTB.Value;
                else
                    param[3].Value = DBNull.Value;
                if (NHGK1.HasValue)
                    param[4].Value = NHGK1.Value;
                else
                    param[4].Value = DBNull.Value;
                if (NHGK2.HasValue)
                    param[5].Value = NHGK2.Value;
                else
                    param[5].Value = DBNull.Value;
                if (NHGK3.HasValue)
                    param[6].Value = NHGK3.Value;
                else
                    param[6].Value = DBNull.Value;
                if (NHTB.HasValue)
                    param[7].Value = NHTB.Value;
                else
                    param[7].Value = DBNull.Value;
                if (PCGK1.HasValue)
                    param[8].Value = PCGK1.Value;
                else
                    param[8].Value = DBNull.Value;
                if (PCGK2.HasValue)
                    param[9].Value = PCGK2.Value;
                else
                    param[9].Value = DBNull.Value;
                if (PCGK3.HasValue)
                    param[10].Value = PCGK3.Value;
                else
                    param[10].Value = DBNull.Value;
                if (PCTB.HasValue)
                    param[11].Value = PCTB.Value;
                else
                    param[11].Value = DBNull.Value;
                if (KNGK1.HasValue)
                    param[12].Value = KNGK1.Value;
                else
                    param[12].Value = DBNull.Value;
                if (KNGK2.HasValue)
                    param[13].Value = KNGK2.Value;
                else
                    param[13].Value = DBNull.Value;
                if (KNGK3.HasValue)
                    param[14].Value = KNGK3.Value;
                else
                    param[14].Value = DBNull.Value;
                if (KNTB.HasValue)
                    param[15].Value = KNTB.Value;
                else
                    param[15].Value = DBNull.Value;

                if (DHNNGK1.HasValue)
                    param[16].Value = DHNNGK1.Value;
                else
                    param[16].Value = DBNull.Value;
                if (DHNNGK2.HasValue)
                    param[17].Value = DHNNGK2.Value;
                else
                    param[17].Value = DBNull.Value;
                if (DHNNGK3.HasValue)
                    param[18].Value = DHNNGK3.Value;
                else
                    param[18].Value = DBNull.Value;
                if (DHNNTB.HasValue)
                    param[19].Value = DHNNTB.Value;
                else
                    param[19].Value = DBNull.Value;
                if (Result.HasValue)
                    param[20].Value = Result.Value;
                else
                    param[20].Value = DBNull.Value;
                param[21].Value = Remark2;
                if (CandidateId.HasValue)
                    param[22].Value = CandidateId.Value;
                else
                    param[22].Value = DBNull.Value;
                if (DHTB.HasValue)
                    param[23].Value = DHTB.Value;
                else
                    param[23].Value = DBNull.Value;
                sproc = new StoreProcedure(CandidatesKeys.Sp_Upd_H2_Candidates_Mark_R2, param);
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

        public long UpdateMark_R3(
            double? L2NNGK1,
            double? L2NNGK2,
            double? L2NNGK3,
            double? L2NNTB,
            double? L2NHGK1,
            double? L2NHGK2,
            double? L2NHGK3,
            double? L2NHTB,
            double? L2PCGK1,
            double? L2PCGK2,
            double? L2PCGK3,
            double? L2PCTB,
            double? L2KNGK1,
            double? L2KNGK2,
            double? L2KNGK3,
            double? L2KNTB,
            double? L2DHNNGK1,
            double? L2DHNNGK2,
            double? L2DHNNGK3,
            double? L2DHNNTB,
            int? Result,
            string Remark3,
            int? CandidateId
        )
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@L2NNGK1", SqlDbType.Float, 8),
                    new SqlParameter("@L2NNGK2", SqlDbType.Float, 8),
                    new SqlParameter("@L2NNGK3", SqlDbType.Float, 8),
                    new SqlParameter("@L2NNTB", SqlDbType.Float, 8),
                    new SqlParameter("@L2NHGK1", SqlDbType.Float, 8),
                    new SqlParameter("@L2NHGK2", SqlDbType.Float, 8),
                    new SqlParameter("@L2NHGK3", SqlDbType.Float, 8),
                    new SqlParameter("@L2NHTB", SqlDbType.Float, 8),
                    new SqlParameter("@L2PCGK1", SqlDbType.Float, 8),
                    new SqlParameter("@L2PCGK2", SqlDbType.Float, 8),
                    new SqlParameter("@L2PCGK3", SqlDbType.Float, 8),
                    new SqlParameter("@L2PCTB", SqlDbType.Float, 8),
                    new SqlParameter("@L2KNGK1", SqlDbType.Float, 8),
                    new SqlParameter("@L2KNGK2", SqlDbType.Float, 8),
                    new SqlParameter("@L2KNGK3", SqlDbType.Float, 8),
                    new SqlParameter("@L2KNTB", SqlDbType.Float, 8),
                    new SqlParameter("@L2DHNNGK1", SqlDbType.Float, 8),
                    new SqlParameter("@L2DHNNGK2", SqlDbType.Float, 8),
                    new SqlParameter("@L2DHNNGK3", SqlDbType.Float, 8),
                    new SqlParameter("@L2DHNNTB", SqlDbType.Float, 8),
                    new SqlParameter("@Result", SqlDbType.Int, 4),
                    new SqlParameter("@RemarkR3", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@CandidateId", SqlDbType.Int, 4)
                };

                if (L2NNGK1.HasValue)
                    param[0].Value = L2NNGK1.Value;
                else
                    param[0].Value = DBNull.Value;
                if (L2NNGK2.HasValue)
                    param[1].Value = L2NNGK2.Value;
                else
                    param[1].Value = DBNull.Value;
                if (L2NNGK3.HasValue)
                    param[2].Value = L2NNGK3.Value;
                else
                    param[2].Value = DBNull.Value;
                if (L2NNTB.HasValue)
                    param[3].Value = L2NNTB.Value;
                else
                    param[3].Value = DBNull.Value;
                if (L2NHGK1.HasValue)
                    param[4].Value = L2NHGK1.Value;
                else
                    param[4].Value = DBNull.Value;
                if (L2NHGK2.HasValue)
                    param[5].Value = L2NHGK2.Value;
                else
                    param[5].Value = DBNull.Value;
                if (L2NHGK3.HasValue)
                    param[6].Value = L2NHGK3.Value;
                else
                    param[6].Value = DBNull.Value;
                if (L2NHTB.HasValue)
                    param[7].Value = L2NHTB.Value;
                else
                    param[7].Value = DBNull.Value;
                if (L2PCGK1.HasValue)
                    param[8].Value = L2PCGK1.Value;
                else
                    param[8].Value = DBNull.Value;
                if (L2PCGK2.HasValue)
                    param[9].Value = L2PCGK2.Value;
                else
                    param[9].Value = DBNull.Value;
                if (L2PCGK3.HasValue)
                    param[10].Value = L2PCGK3.Value;
                else
                    param[10].Value = DBNull.Value;
                if (L2PCTB.HasValue)
                    param[11].Value = L2PCTB.Value;
                else
                    param[11].Value = DBNull.Value;
                if (L2KNGK1.HasValue)
                    param[12].Value = L2KNGK1.Value;
                else
                    param[12].Value = DBNull.Value;
                if (L2KNGK2.HasValue)
                    param[13].Value = L2KNGK2.Value;
                else
                    param[13].Value = DBNull.Value;
                if (L2KNGK3.HasValue)
                    param[14].Value = L2KNGK3.Value;
                else
                    param[14].Value = DBNull.Value;
                if (L2KNTB.HasValue)
                    param[15].Value = L2KNTB.Value;
                else
                    param[15].Value = DBNull.Value;

                if (L2DHNNGK1.HasValue)
                    param[16].Value = L2DHNNGK1.Value;
                else
                    param[16].Value = DBNull.Value;
                if (L2DHNNGK2.HasValue)
                    param[17].Value = L2DHNNGK2.Value;
                else
                    param[17].Value = DBNull.Value;
                if (L2DHNNGK3.HasValue)
                    param[18].Value = L2DHNNGK3.Value;
                else
                    param[18].Value = DBNull.Value;
                if (L2DHNNTB.HasValue)
                    param[19].Value = L2DHNNTB.Value;
                else
                    param[19].Value = DBNull.Value;
                if (Result.HasValue)
                    param[20].Value = Result.Value;
                else
                    param[20].Value = DBNull.Value;
                param[21].Value = Remark3;
                if (CandidateId.HasValue)
                    param[22].Value = CandidateId.Value;
                else
                    param[22].Value = DBNull.Value;
                sproc = new StoreProcedure(CandidatesKeys.Sp_Upd_H2_Candidates_Mark_R3, param);
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

        public long UpdateMark_RBoard(
            double? NNLRGK1,
            double? NNLRGK2,
            double? NNLRGK3,
            double? NNLRTB,
            double? DHGK1,
            double? NHLRGK1,
            double? NHLRGK2,
            double? NHLRGK3,
            double? NHLRTB,
            double? DHGK2,
            int? Result,
            string RemarkLR,
            int? CandidateId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@NNLRGK1", SqlDbType.Float, 8),
                    new SqlParameter("@NNLRGK2", SqlDbType.Float, 8),
                    new SqlParameter("@NNLRGK3", SqlDbType.Float, 8),
                    new SqlParameter("@NNLRTB", SqlDbType.Float, 8),
                    new SqlParameter("@DHGK1", SqlDbType.Float, 8),
                    new SqlParameter("@NHLRGK1", SqlDbType.Float, 8),
                    new SqlParameter("@NHLRGK2", SqlDbType.Float, 8),
                    new SqlParameter("@NHLRGK3", SqlDbType.Float, 8),
                    new SqlParameter("@NHLRTB", SqlDbType.Float, 8),
                    new SqlParameter("@DHGK2", SqlDbType.Float, 8),
                    new SqlParameter("@Result", SqlDbType.Int, 4),
                    new SqlParameter("@RemarkLR", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@CandidateId", SqlDbType.Int, 4)
                };

                if (NNLRGK1.HasValue)
                    param[0].Value = NNLRGK1.Value;
                else
                    param[0].Value = DBNull.Value;
                if (NNLRGK2.HasValue)
                    param[1].Value = NNLRGK2.Value;
                else
                    param[1].Value = DBNull.Value;
                if (NNLRGK3.HasValue)
                    param[2].Value = NNLRGK3.Value;
                else
                    param[2].Value = DBNull.Value;
                if (NNLRTB.HasValue)
                    param[3].Value = NNLRTB.Value;
                else
                    param[3].Value = DBNull.Value;
                if (DHGK1.HasValue)
                    param[4].Value = DHGK1.Value;
                else
                    param[4].Value = DBNull.Value;
                if (NHLRGK1.HasValue)
                    param[5].Value = NHLRGK1.Value;
                else
                    param[5].Value = DBNull.Value;
                if (NHLRGK2.HasValue)
                    param[6].Value = NHLRGK2.Value;
                else
                    param[6].Value = DBNull.Value;
                if (NHLRGK3.HasValue)
                    param[7].Value = NHLRGK3.Value;
                else
                    param[7].Value = DBNull.Value;
                if (NHLRTB.HasValue)
                    param[8].Value = NHLRTB.Value;
                else
                    param[8].Value = DBNull.Value;
                if (DHGK2.HasValue)
                    param[9].Value = DHGK2.Value;
                else
                    param[9].Value = DBNull.Value;

                if (Result.HasValue)
                    param[10].Value = Result.Value;
                else
                    param[10].Value = DBNull.Value;
                if (RemarkLR != null)
                    param[11].Value = RemarkLR;
                else
                    param[11].Value = DBNull.Value;
                if (CandidateId.HasValue)
                    param[12].Value = CandidateId.Value;
                else
                    param[12].Value = DBNull.Value;

                sproc = new StoreProcedure(CandidatesKeys.Sp_Upd_H2_Candidates_Mark_RBoard, param);
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


        public long UpdateMark_Repairing_Thoery(double? NNGK1, int? Result, string RemarkR1, int? CandidateId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@NNGK1", SqlDbType.Float, 8),
                    new SqlParameter("@Result", SqlDbType.Int, 4),
                    new SqlParameter("@RemarkR1", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@CandidateId", SqlDbType.Int, 4)
                };


                if (NNGK1.HasValue)
                    param[0].Value = NNGK1.Value;
                else
                    param[0].Value = DBNull.Value;
                if (Result.HasValue)
                    param[1].Value = Result.Value;
                else
                    param[1].Value = DBNull.Value;
                if (RemarkR1 != null)
                    param[2].Value = RemarkR1;
                else
                    param[2].Value = DBNull.Value;
                if (CandidateId.HasValue)
                    param[3].Value = CandidateId.Value;
                else
                    param[3].Value = DBNull.Value;
                sproc = new StoreProcedure(CandidatesKeys.Sp_Upd_H2_Candidates_Mark_Repairing_Thoery, param);
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

        public long UpdateMark_Repairing_Practice(double? NNGK2, int? Result, string RemarkR2, int? CandidateId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@NNGK2", SqlDbType.Float, 8),
                    new SqlParameter("@Result", SqlDbType.Int, 4),
                    new SqlParameter("@RemarkR2", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@CandidateId", SqlDbType.Int, 4)
                };


                if (NNGK2.HasValue)
                    param[0].Value = NNGK2.Value;
                else
                    param[0].Value = DBNull.Value;
                if (Result.HasValue)
                    param[1].Value = Result.Value;
                else
                    param[1].Value = DBNull.Value;
                if (RemarkR2 != null)
                    param[2].Value = RemarkR2;
                else
                    param[2].Value = DBNull.Value;
                if (CandidateId.HasValue)
                    param[3].Value = CandidateId.Value;
                else
                    param[3].Value = DBNull.Value;
                sproc = new StoreProcedure(CandidatesKeys.Sp_Upd_H2_Candidates_Mark_Repairing_Practice, param);
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

        public long UpdateMark_Repairing_Board(
            double? PCGK1,
            double? PCGK2,
            double? PCGK3,
            double? PCTB,
            double? KNGK1,
            double? KNGK2,
            double? KNGK3,
            double? KNTB,
            double? DHNNGK1,
            double? DHNNGK2,
            double? DHNNGK3,
            double? DHNNTB,
            int? Result,
            string RemarkR3,
            int? CandidateId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@PCGK1", SqlDbType.Float, 8),
                    new SqlParameter("@PCGK2", SqlDbType.Float, 8),
                    new SqlParameter("@PCGK3", SqlDbType.Float, 8),
                    new SqlParameter("@PCTB", SqlDbType.Float, 8),
                    new SqlParameter("@KNGK1", SqlDbType.Float, 8),
                    new SqlParameter("@KNGK2", SqlDbType.Float, 8),
                    new SqlParameter("@KNGK3", SqlDbType.Float, 8),
                    new SqlParameter("@KNTB", SqlDbType.Float, 8),
                    new SqlParameter("@DHNNGK1", SqlDbType.Float, 8),
                    new SqlParameter("@DHNNGK2", SqlDbType.Float, 8),
                    new SqlParameter("@DHNNGK3", SqlDbType.Float, 8),
                    new SqlParameter("@DHNNTB", SqlDbType.Float, 8),
                    new SqlParameter("@Result", SqlDbType.Int, 4),
                    new SqlParameter("@RemarkR3", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@CandidateId", SqlDbType.Int, 4)
                };


                if (PCGK1.HasValue)
                    param[0].Value = PCGK1.Value;
                else
                    param[0].Value = DBNull.Value;
                if (PCGK2.HasValue)
                    param[1].Value = PCGK2.Value;
                else
                    param[1].Value = DBNull.Value;
                if (PCGK3.HasValue)
                    param[2].Value = PCGK3.Value;
                else
                    param[2].Value = DBNull.Value;
                if (PCTB.HasValue)
                    param[3].Value = PCTB.Value;
                else
                    param[3].Value = DBNull.Value;
                if (KNGK1.HasValue)
                    param[4].Value = KNGK1.Value;
                else
                    param[4].Value = DBNull.Value;
                if (KNGK2.HasValue)
                    param[5].Value = KNGK2.Value;
                else
                    param[5].Value = DBNull.Value;
                if (KNGK3.HasValue)
                    param[6].Value = KNGK3.Value;
                else
                    param[6].Value = DBNull.Value;
                if (KNTB.HasValue)
                    param[7].Value = KNTB.Value;
                else
                    param[7].Value = DBNull.Value;

                if (DHNNGK1.HasValue)
                    param[8].Value = DHNNGK1.Value;
                else
                    param[8].Value = DBNull.Value;
                if (DHNNGK2.HasValue)
                    param[9].Value = DHNNGK2.Value;
                else
                    param[9].Value = DBNull.Value;
                if (DHNNGK3.HasValue)
                    param[10].Value = DHNNGK3.Value;
                else
                    param[10].Value = DBNull.Value;
                if (DHNNTB.HasValue)
                    param[11].Value = DHNNTB.Value;
                else
                    param[11].Value = DBNull.Value;
                if (Result.HasValue)
                    param[12].Value = Result.Value;
                else
                    param[12].Value = DBNull.Value;
                if (RemarkR3 != null)
                    param[13].Value = RemarkR3;
                else
                    param[13].Value = DBNull.Value;
                if (CandidateId.HasValue)
                    param[14].Value = CandidateId.Value;
                else
                    param[14].Value = DBNull.Value;
                sproc = new StoreProcedure(CandidatesKeys.Sp_Upd_H2_Candidates_Mark_Repairing_Board, param);
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


        public long UpdateMark_Speciality_Thoery(double? NNGK1, double? NNGK2, int? Result, string RemarkR1,
            int? CandidateId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@NNGK1", SqlDbType.Float, 8),
                    new SqlParameter("@NNGK2", SqlDbType.Float, 8),
                    new SqlParameter("@Result", SqlDbType.Int, 4),
                    new SqlParameter("@RemarkR1", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@CandidateId", SqlDbType.Int, 4)
                };


                if (NNGK1.HasValue)
                    param[0].Value = NNGK1.Value;
                else
                    param[0].Value = DBNull.Value;
                if (NNGK2.HasValue)
                    param[1].Value = NNGK2.Value;
                else
                    param[1].Value = DBNull.Value;
                if (Result.HasValue)
                    param[2].Value = Result.Value;
                else
                    param[2].Value = DBNull.Value;
                if (RemarkR1 != null)
                    param[3].Value = RemarkR1;
                else
                    param[3].Value = DBNull.Value;
                if (CandidateId.HasValue)
                    param[4].Value = CandidateId.Value;
                else
                    param[4].Value = DBNull.Value;
                sproc = new StoreProcedure(CandidatesKeys.Sp_Upd_H2_Candidates_Mark_Speciality_Thoery, param);
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

        public long UpdateMark_Speciality_Board(int? Result, string RemarkR2, int? CandidateId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Result", SqlDbType.Int, 4),
                    new SqlParameter("@RemarkR2", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@CandidateId", SqlDbType.Int, 4)
                };


                if (Result.HasValue)
                    param[0].Value = Result.Value;
                else
                    param[0].Value = DBNull.Value;
                if (RemarkR2 != null)
                    param[1].Value = RemarkR2;
                else
                    param[1].Value = DBNull.Value;
                if (CandidateId.HasValue)
                    param[2].Value = CandidateId.Value;
                else
                    param[2].Value = DBNull.Value;
                sproc = new StoreProcedure(CandidatesKeys.Sp_Upd_H2_Candidates_Mark_Speciality_Board, param);
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

        public long UpdateMark_Speciality_Practice(double? NNGK3, int? Result, string RemarkR3, int? CandidateId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@NNGK3", SqlDbType.Float, 8),
                    new SqlParameter("@Result", SqlDbType.Int, 4),
                    new SqlParameter("@RemarkR3", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@CandidateId", SqlDbType.Int, 4)
                };


                if (NNGK3.HasValue)
                    param[0].Value = (float) NNGK3.Value;
                else
                    param[0].Value = DBNull.Value;
                if (Result.HasValue)
                    param[1].Value = Result.Value;
                else
                    param[1].Value = DBNull.Value;
                if (RemarkR3 != null)
                    param[2].Value = RemarkR3;
                else
                    param[2].Value = DBNull.Value;
                if (CandidateId.HasValue)
                    param[3].Value = CandidateId.Value;
                else
                    param[3].Value = DBNull.Value;
                sproc = new StoreProcedure(CandidatesKeys.Sp_Upd_H2_Candidates_Mark_Speciality_Practice, param);
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

        public long UpdateMark_LoadAndUnloadCargo(double? NNGK1, string RemarkR1, string RemarkR2, int? Result,
            string RemarkR3, int? CandidateId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@NNGK1", SqlDbType.Float, 8),
                    new SqlParameter("@RemarkR1", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@RemarkR2", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@Result", SqlDbType.Int, 4),
                    new SqlParameter("@RemarkR3", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@CandidateId", SqlDbType.Int, 4)
                };


                if (NNGK1.HasValue)
                    param[0].Value = NNGK1.Value;
                else
                    param[0].Value = DBNull.Value;
                if (RemarkR1 != null)
                    param[1].Value = RemarkR1;
                else
                    param[1].Value = DBNull.Value;
                if (RemarkR2 != null)
                    param[2].Value = RemarkR2;
                else
                    param[2].Value = DBNull.Value;
                if (Result.HasValue)
                    param[3].Value = Result.Value;
                else
                    param[3].Value = DBNull.Value;
                if (RemarkR3 != null)
                    param[4].Value = RemarkR3;
                else
                    param[4].Value = DBNull.Value;
                if (CandidateId.HasValue)
                    param[5].Value = CandidateId.Value;
                else
                    param[5].Value = DBNull.Value;
                sproc = new StoreProcedure(CandidatesKeys.Sp_Upd_H2_Candidates_Mark_LoadUnLoadCargo, param);
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
                sproc = new StoreProcedure(CandidatesKeys.SP_CANDIDATE_DELETE, param);
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