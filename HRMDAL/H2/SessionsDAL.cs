using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H2;

namespace HRMDAL.H2
{
    public class SessionsDAL : Dao
    {
        #region Methods Get

        public DataTable GetAll()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure(SessionKeys.SP_SESSION_GET_ALL, null);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetById(int sessionId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@SessionId", SqlDbType.Int)
                };

                param[0].Value = sessionId;

                sproc = new StoreProcedure(SessionKeys.SP_SESSION_GET_BY_ID, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetSessionPositionBySessionId(int sessionId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@SessionId", SqlDbType.Int)
                };

                param[0].Value = sessionId;

                sproc = new StoreProcedure(SessionKeys.SP_SESSION_POSITION_GET_BY_SESSIONID, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetIsActive()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure(SessionKeys.SP_SESSION_GET_IS_ACTIVE, null);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetSessionIsOpen()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure(SessionKeys.Sp_Sel_H2_SessionPosition_By_IsOpen, null);
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

        public long Insert(DateTime fromDate, DateTime toDate, string sessionName, string remark, int sessionType)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FromDate", SqlDbType.DateTime),
                    new SqlParameter("@ToDate", SqlDbType.DateTime),
                    new SqlParameter("@Name", SqlDbType.NVarChar, 100),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 254),
                    new SqlParameter("@SessionType", SqlDbType.Int)
                };

                param[0].Value = fromDate;
                param[1].Value = toDate;
                param[2].Value = sessionName;
                param[3].Value = remark;
                param[4].Value = sessionType;

                sproc = new StoreProcedure(SessionKeys.SP_SESSION_INSERT, param);
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

        public long Update(string name, DateTime fromDate, DateTime toDate, string remark, int sessionType,
            int sessionId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Name", SqlDbType.NVarChar),
                    new SqlParameter("@FromDate", SqlDbType.DateTime),
                    new SqlParameter("@ToDate", SqlDbType.DateTime),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 254),
                    new SqlParameter("@SessionType", SqlDbType.Int),
                    new SqlParameter("@SessionId", SqlDbType.Int)
                };

                param[0].Value = name;
                param[1].Value = fromDate;
                param[2].Value = toDate;
                param[3].Value = remark == null ? string.Empty : remark;
                param[4].Value = sessionType;
                param[5].Value = sessionId;

                sproc = new StoreProcedure(SessionKeys.SP_SESSION_UPDATE, param);
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

        public long Delete(int sessionId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@SessionId", SqlDbType.Int)
                };

                param[0].Value = sessionId;
                sproc = new StoreProcedure(SessionKeys.SP_SESSION_DELETE, param);
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

        #region Insert SessionPosition

        public long InsertSessionPosition(int PositionId, int SessionId, string Remark)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@PositionId", SqlDbType.Int),
                    new SqlParameter("@SessionId", SqlDbType.Int),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 1000)
                };

                param[0].Value = PositionId;
                param[1].Value = SessionId;
                param[2].Value = Remark;

                sproc = new StoreProcedure(SessionKeys.Sp_Ins_H2_SessionPosition, param);
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

        public long DeleteBySessionId(int sessionId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@SessionId", SqlDbType.Int)
                };

                param[0].Value = sessionId;
                sproc = new StoreProcedure(SessionKeys.Sp_Del_H2_SessionPositionBySessionId, param);
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