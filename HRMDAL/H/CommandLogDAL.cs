using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H;

namespace HRMDAL.H
{
    public class CommandLogDAL : Dao
    {
        #region Insert, update, delete

        public long Insert(int commandTypeId, string dataName, int userId, string oldValues, string newValues,
            DateTime commandLogDate, int ModuleId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@CommandTypeId", SqlDbType.Int),
                    new SqlParameter("@DataName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@OldValues", SqlDbType.NText),
                    new SqlParameter("@NewValues", SqlDbType.NText),
                    new SqlParameter("@CommandLogDate", SqlDbType.DateTime),
                    new SqlParameter("@ModuleId", SqlDbType.Int)
                };

                param[0].Value = commandTypeId;
                param[1].Value = dataName;
                param[2].Value = userId;
                param[3].Value = oldValues;
                param[4].Value = newValues;
                param[5].Value = commandLogDate;
                param[6].Value = ModuleId;

                sproc = new StoreProcedure(CommandLogKeyNames.Sp_Ins_CommandLog, param);
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

        public DataTable GetByFilter(string dataName, int commandTypeId, int userId, int Day, int Month, int Year,
            int ModuleId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DataName", SqlDbType.NVarChar),
                    new SqlParameter("@CommandTypeId", SqlDbType.Int),
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@Day", SqlDbType.Int),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int),
                    new SqlParameter("@ModuleId", SqlDbType.Int)
                };

                param[0].Value = dataName;
                param[1].Value = commandTypeId;
                param[2].Value = userId;
                param[3].Value = Day;
                param[4].Value = Month;
                param[5].Value = Year;
                param[6].Value = ModuleId;

                sproc = new StoreProcedure(CommandLogKeyNames.Sp_Sel_CommandLog_By_Filter, param);
                sproc.RunFill(dt);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetByFromToDateModule(int CommandTypeId, DateTime FromDate, DateTime ToDate, int ModuleId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@CommandTypeId", SqlDbType.Int),
                    new SqlParameter("@FromDate", SqlDbType.DateTime),
                    new SqlParameter("@ToDate", SqlDbType.DateTime),
                    new SqlParameter("@ModuleId", SqlDbType.Int)
                };

                param[0].Value = CommandTypeId;
                param[1].Value = FromDate;
                param[2].Value = ToDate;
                param[3].Value = ModuleId;

                sproc = new StoreProcedure(CommandLogKeyNames.Sp_Sel_CommandLog_By_FromToDateModule, param);
                sproc.RunFill(dt);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetByDataName(string dataName)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DataName", SqlDbType.NVarChar)
                };

                param[0].Value = dataName;

                sproc = new StoreProcedure(CommandLogKeyNames.Sp_Sel_CommandLog_By_DataName, param);
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