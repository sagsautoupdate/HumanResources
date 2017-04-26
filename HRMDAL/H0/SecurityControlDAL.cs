using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;

namespace HRMDAL.H0
{
    public class SecurityControlDAL : Dao
    {
        public DataTable GetOneById(int Id)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                param[0].Value = Id;

                sproc = new StoreProcedure("Sel_H0_SecurityControl_By_Id", param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }
            return dt;
        }

        public DataTable GetAll()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure("Sel_H0_SecurityControl", null);
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

        public DataTable GetAllForExport()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure("Sel_H0_SecurityControl_ForExport", null);
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

        public DataTable GetAllForExport_Employee()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure("Sel_H0_SecurityControl_ForExport_Employee", null);
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

        public DataTable GetAllForExport_Candidate()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure("Sel_H0_SecurityControl_ForExport_Candidate", null);
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

        public DataTable GetAllHistory(int UserId)
        {
            Debug.Assert(sproc == null);
            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                param[0].Value = UserId;

                sproc = new StoreProcedure("Sel_H0_SecurityControl_UserId_All", param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }
            return dt;
        }

        public long Insert(int UserId, string CurrentSCI,
            DateTime Period, string Area1, string Area2, string Area3,
            string Area4, string Area5, string Area6, string Remark, int CreateBy, DateTime StartDate)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@CurrentSCI", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Period", SqlDbType.DateTime),
                    new SqlParameter("@Area1", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Area2", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Area3", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Area4", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Area5", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Area6", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Remark", SqlDbType.NText),
                    new SqlParameter("@CreateUserId", SqlDbType.Int),
                    new SqlParameter("@StartDate", SqlDbType.DateTime)
                };

                param[0].Value = UserId;
                param[1].Value = CurrentSCI;
                param[2].Value = Period;
                param[3].Value = Area1;
                param[4].Value = Area2;
                param[5].Value = Area3;
                param[6].Value = Area4;
                param[7].Value = Area5;
                param[8].Value = Area6;
                param[9].Value = Remark;
                param[10].Value = CreateBy;
                param[11].Value = StartDate;

                sproc = new StoreProcedure("Ins_H0_SecurityControl", param);
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

        public long Update(int UserId, string CurrentSCI,
            DateTime Period, string Area1, string Area2, string Area3,
            string Area4, string Area5, string Area6, string Remark, int SecurityControlId, int UpdateBy,
            DateTime StartDate)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@CurrentSCI", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Period", SqlDbType.DateTime),
                    new SqlParameter("@Area1", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Area2", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Area3", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Area4", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Area5", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Area6", SqlDbType.NVarChar, 50),
                    new SqlParameter("@Remark", SqlDbType.NText),
                    new SqlParameter("@SecurityControlId", SqlDbType.Int),
                    new SqlParameter("@UpdateUserId", SqlDbType.Int),
                    new SqlParameter("@StartDate", SqlDbType.DateTime)
                };

                param[0].Value = UserId;
                param[1].Value = CurrentSCI;
                param[2].Value = Period;
                param[3].Value = Area1;
                param[4].Value = Area2;
                param[5].Value = Area3;
                param[6].Value = Area4;
                param[7].Value = Area5;
                param[8].Value = Area6;
                param[9].Value = Remark;
                param[10].Value = SecurityControlId;
                param[11].Value = UpdateBy;
                param[12].Value = StartDate;

                sproc = new StoreProcedure("Upd_H0_SecurityControl", param);
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

        public long UpdateLostCard(int SecurityControlId, int Active, int IsLost, string RemarkLost, int UpdateUserId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@SecurityControlId", SqlDbType.Int),
                    new SqlParameter("@Active", SqlDbType.Int),
                    new SqlParameter("@IsLost", SqlDbType.Int),
                    new SqlParameter("@LastUpdateBy", SqlDbType.Int),
                    new SqlParameter("@RemarkLost", SqlDbType.NText)
                };

                param[0].Value = SecurityControlId;
                param[1].Value = Active;
                param[2].Value = IsLost;
                param[3].Value = UpdateUserId;
                param[4].Value = RemarkLost;

                sproc = new StoreProcedure("Upd_H0_SecurityControl_Lost", param);
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
    }
}