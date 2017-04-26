using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;

namespace HRMDAL.H2
{
    public class CandidateContractionsDAL : Dao
    {
        public DataTable GetAll()
        {
            Debug.Assert(sproc == null);
            var table = new DataTable();
            try
            {
                sproc = new StoreProcedure("Sel_H2_CandidateContraction_GetAll", null);
                sproc.RunFill(table);
                sproc.Dispose();
            }
            catch (SqlException exception)
            {
                throw new HRMException(exception.Message, exception.Number);
            }
            return table;
        }

        public DataTable GetById(int id)
        {
            Debug.Assert(sproc == null);
            var table = new DataTable();
            try
            {
                SqlParameter[] parameters = {new SqlParameter("@CandidateContractId", SqlDbType.Int)};
                parameters[0].Value = id;
                sproc = new StoreProcedure("Sel_H2_CandidateContraction_GetById", parameters);
                sproc.RunFill(table);
                sproc.Dispose();
            }
            catch (SqlException exception)
            {
                throw new HRMException(exception.Message, exception.Number);
            }
            return table;
        }

        public long Insert(int CandidateId, DateTime FromDate, DateTime ToDate, string Remark,
            int EducationHighestLevelId, int PositionIdFee, int SessionId)
        {
            Debug.Assert(sproc == null);
            var num = 0L;
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@CandidateId", SqlDbType.Int),
                    new SqlParameter("@FromDate", SqlDbType.DateTime), new SqlParameter("@ToDate", SqlDbType.DateTime),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 0xfa0),
                    new SqlParameter("@EducationHighestLevelId", SqlDbType.Int),
                    new SqlParameter("@PositionIdFee", SqlDbType.Int), new SqlParameter("@SessionId", SqlDbType.Int)
                };
                parameters[0].Value = CandidateId;
                parameters[1].Value = FromDate;
                parameters[2].Value = ToDate;
                parameters[3].Value = Remark;
                parameters[4].Value = EducationHighestLevelId;
                parameters[5].Value = PositionIdFee;
                parameters[6].Value = SessionId;
                sproc = new StoreProcedure("Ins_H2_CandidateContract_By_CandidateId", parameters);
                num = sproc.RunInt();
                sproc.Commit();
            }
            catch (SqlException exception)
            {
                sproc.RollBack();
                throw new HRMException(exception.Message, exception.Number);
            }
            finally
            {
                sproc.Dispose();
            }
            return num;
        }

        public long Update(int CandidateContractId, DateTime FromDate, DateTime ToDate, string Remark,
            int EducationHighestLevelId, int PositionIdFee, int SessionId)
        {
            Debug.Assert(sproc == null);
            var num = 0L;
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@CandidateContractId", SqlDbType.Int),
                    new SqlParameter("@FromDate", SqlDbType.DateTime), new SqlParameter("@ToDate", SqlDbType.DateTime),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 0xfa0),
                    new SqlParameter("@EducationHighestLevelId", SqlDbType.Int),
                    new SqlParameter("@PositionIdFee", SqlDbType.Int), new SqlParameter("@SessionId", SqlDbType.Int)
                };
                parameters[0].Value = CandidateContractId;
                parameters[1].Value = FromDate;
                parameters[2].Value = ToDate;
                parameters[3].Value = Remark;
                parameters[4].Value = EducationHighestLevelId;
                parameters[5].Value = PositionIdFee;
                parameters[6].Value = SessionId;
                sproc = new StoreProcedure("Upd_H2_CandidateContract_By_CandidateId", parameters);
                num = sproc.RunInt();
                sproc.Commit();
            }
            catch (SqlException exception)
            {
                sproc.RollBack();
                throw new HRMException(exception.Message, exception.Number);
            }
            finally
            {
                sproc.Dispose();
            }
            return num;
        }
    }
}