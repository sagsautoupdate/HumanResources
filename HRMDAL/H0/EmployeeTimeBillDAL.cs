using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H0;

namespace HRMDAL.H0
{
    public class EmployeeTimeBillDAL : Dao
    {
        #region Insert, Update, Delete

        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns>int</returns>
        public long Insert(DateTime? WorkDate, int? UserId, DateTime? TimeIn, DateTime? TimeOut, double? TotalMinutes,
            double? TotalHours, int? Status)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@WorkDate", SqlDbType.DateTime, 8),
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@TimeIn", SqlDbType.DateTime, 8),
                    new SqlParameter("@TimeOut", SqlDbType.DateTime, 8),
                    new SqlParameter("@TotalMinutes", SqlDbType.Float, 8),
                    new SqlParameter("@TotalHours", SqlDbType.Float, 8),
                    new SqlParameter("@Status", SqlDbType.Int, 4)
                };

                if (WorkDate.HasValue)
                    param[0].Value = WorkDate.Value;
                else
                    param[0].Value = DBNull.Value;
                if (UserId.HasValue)
                    param[1].Value = UserId.Value;
                else
                    param[1].Value = DBNull.Value;
                if (TimeIn.HasValue)
                    param[2].Value = TimeIn.Value;
                else
                    param[2].Value = DBNull.Value;
                if (TimeOut.HasValue)
                    param[3].Value = TimeOut.Value;
                else
                    param[3].Value = DBNull.Value;
                if (TotalMinutes.HasValue)
                    param[4].Value = TotalMinutes.Value;
                else
                    param[4].Value = DBNull.Value;
                if (TotalHours.HasValue)
                    param[5].Value = TotalHours.Value;
                else
                    param[5].Value = DBNull.Value;
                if (Status.HasValue)
                    param[6].Value = Status.Value;
                else
                    param[6].Value = DBNull.Value;

                sproc = new StoreProcedure(EmployeeTimeBillKeys.Sp_Ins_H0_EmployeeTimeBill, param);
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

        public long Update(DateTime? WorkDate, int? UserId, DateTime? TimeIn, DateTime? TimeOut, double? TotalMinutes,
            double? TotalHours, int? Status, int? UserTimeBillId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@WorkDate", SqlDbType.DateTime, 8),
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@TimeIn", SqlDbType.DateTime, 8),
                    new SqlParameter("@TimeOut", SqlDbType.DateTime, 8),
                    new SqlParameter("@TotalMinutes", SqlDbType.Float, 8),
                    new SqlParameter("@TotalHours", SqlDbType.Float, 8),
                    new SqlParameter("@Status", SqlDbType.Int, 4),
                    new SqlParameter("@UserTimeBillId", SqlDbType.Int, 4)
                };

                if (WorkDate.HasValue)
                    param[0].Value = WorkDate.Value;
                else
                    param[0].Value = DBNull.Value;
                if (UserId.HasValue)
                    param[1].Value = UserId.Value;
                else
                    param[1].Value = DBNull.Value;
                if (TimeIn.HasValue)
                    param[2].Value = TimeIn.Value;
                else
                    param[2].Value = DBNull.Value;
                if (TimeOut.HasValue)
                    param[3].Value = TimeOut.Value;
                else
                    param[3].Value = DBNull.Value;

                if (TotalMinutes.HasValue)
                    param[4].Value = TotalMinutes.Value;
                else
                    param[4].Value = DBNull.Value;
                if (TotalHours.HasValue)
                    param[5].Value = TotalHours.Value;
                else
                    param[5].Value = DBNull.Value;
                if (Status.HasValue)
                    param[6].Value = Status.Value;
                else
                    param[6].Value = DBNull.Value;
                if (UserTimeBillId.HasValue)
                    param[7].Value = UserTimeBillId.Value;
                else
                    param[7].Value = DBNull.Value;
                sproc = new StoreProcedure(EmployeeTimeBillKeys.Sp_Upd_H0_EmployeeTimeBill, param);
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


        public long Delete(int? UserTimeBillId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserTimeBillId", SqlDbType.Int, 4)
                };

                if (UserTimeBillId.HasValue)
                    param[0].Value = UserTimeBillId.Value;
                else
                    param[0].Value = DBNull.Value;

                sproc = new StoreProcedure(EmployeeTimeBillKeys.Sp_Del_H0_EmployeeTimeBill, param);
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

        #region Get

        public DataTable GetByUserStatus(int? UserId, int? Status)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@Status", SqlDbType.Int, 4)
                };

                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (Status.HasValue)
                    param[1].Value = Status.Value;
                else
                    param[1].Value = DBNull.Value;

                sproc = new StoreProcedure(EmployeeTimeBillKeys.Sp_Sel_H0_EmployeeTimeBillByUserStatus, param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetForDistinctWorkdateByUserId(int? UserId, int? Month, int? Year)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@Month", SqlDbType.Int, 4),
                    new SqlParameter("@Year", SqlDbType.Int, 4)
                };

                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (Month.HasValue)
                    param[1].Value = Month.Value;
                else
                    param[1].Value = DBNull.Value;

                if (Year.HasValue)
                    param[2].Value = Year.Value;
                else
                    param[2].Value = DBNull.Value;

                sproc = new StoreProcedure(EmployeeTimeBillKeys.Sp_Sel_H0_EmployeeTimeBillForDistinctWorkdateByUserId,
                    param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetByFilter(int? UserId, int? Status, int? Day, int? Month, int? Year)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@Status", SqlDbType.Int, 4),
                    new SqlParameter("@Day", SqlDbType.Int, 4),
                    new SqlParameter("@Month", SqlDbType.Int, 4),
                    new SqlParameter("@Year", SqlDbType.Int, 4)
                };

                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (Status.HasValue)
                    param[1].Value = Status.Value;
                else
                    param[1].Value = DBNull.Value;

                if (Day.HasValue)
                    param[2].Value = Day.Value;
                else
                    param[2].Value = DBNull.Value;

                if (Month.HasValue)
                    param[3].Value = Month.Value;
                else
                    param[3].Value = DBNull.Value;

                if (Year.HasValue)
                    param[4].Value = Year.Value;
                else
                    param[4].Value = DBNull.Value;

                sproc = new StoreProcedure(EmployeeTimeBillKeys.Sp_Sel_H0_EmployeeTimeBillByFilter, param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetByFilterByHo(int? UserId, int? Status, int? Day, int? Month, int? Year)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@Status", SqlDbType.Int, 4),
                    new SqlParameter("@Day", SqlDbType.Int, 4),
                    new SqlParameter("@Month", SqlDbType.Int, 4),
                    new SqlParameter("@Year", SqlDbType.Int, 4)
                };

                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (Status.HasValue)
                    param[1].Value = Status.Value;
                else
                    param[1].Value = DBNull.Value;

                if (Day.HasValue)
                    param[2].Value = Day.Value;
                else
                    param[2].Value = DBNull.Value;

                if (Month.HasValue)
                    param[3].Value = Month.Value;
                else
                    param[3].Value = DBNull.Value;

                if (Year.HasValue)
                    param[4].Value = Year.Value;
                else
                    param[4].Value = DBNull.Value;

                sproc = new StoreProcedure(EmployeeTimeBillKeys.Sp_Sel_H0_EmployeeTimeBillByFilterForHo, param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetByUserDateForWorkingHo(int? UserId, int? Day, int? Month, int? Year)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@Day", SqlDbType.Int, 4),
                    new SqlParameter("@Month", SqlDbType.Int, 4),
                    new SqlParameter("@Year", SqlDbType.Int, 4)
                };

                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;

                if (Day.HasValue)
                    param[1].Value = Day.Value;
                else
                    param[1].Value = DBNull.Value;

                if (Month.HasValue)
                    param[2].Value = Month.Value;
                else
                    param[2].Value = DBNull.Value;

                if (Year.HasValue)
                    param[3].Value = Year.Value;
                else
                    param[3].Value = DBNull.Value;

                sproc = new StoreProcedure(EmployeeTimeBillKeys.Sp_Sel_H0_EmployeeTimeBillByUserDateForWorkingAndHo,
                    param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetForDistinctWorkdateByFromToWorkDate(int? UserId, DateTime FromDate, DateTime ToDate)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@FromDate", SqlDbType.DateTime, 8),
                    new SqlParameter("@ToDate", SqlDbType.DateTime, 8)
                };

                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;

                param[1].Value = FromDate;
                param[2].Value = ToDate;

                sproc =
                    new StoreProcedure(
                        EmployeeTimeBillKeys.Sp_Sel_H0_EmployeeTimeBillForDistinctWorkdateByUserIdFromToWorkDate, param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetByDateRootId(int? RootId, DateTime InputDate)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@RootId", SqlDbType.Int, 4),
                    new SqlParameter("@InputDate", SqlDbType.DateTime, 8)
                };

                if (RootId.HasValue)
                    param[0].Value = RootId.Value;
                else
                    param[0].Value = DBNull.Value;

                param[1].Value = InputDate;

                sproc = new StoreProcedure(EmployeeTimeBillKeys.Sp_Sel_H0_EmployeeTimeBillByRootDate, param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetDateTimeNowFromServer()
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                sproc = new StoreProcedure(EmployeeTimeBillKeys.Sp_Sel_DateTimeNow, null);
                sproc.RunFill(dt);
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