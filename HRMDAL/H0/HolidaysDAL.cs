using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H0;

namespace HRMDAL.H0
{
    public class HolidaysDAL : Dao
    {
        #region Methods Get

        public DataTable GetAll()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure(HolidayKeys.SP_HOLIDAY_GETALL, null);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }


        public DataTable GetByDate(int month, int year)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int)
                };

                param[0].Value = month;
                param[1].Value = year;

                sproc = new StoreProcedure(HolidayKeys.Sp_Sel_H0_Holiday_By_Date, param);
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

        public long Insert(string holidayName, DateTime holidayDate)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@HolidayName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@HolidayDate", SqlDbType.DateTime)
                };

                param[0].Value = holidayName;
                param[1].Value = holidayDate;

                sproc = new StoreProcedure(HolidayKeys.Sp_Ins_H0_Holiday, param);
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

        public long Update(int holidayId, string holidayName, DateTime holidayDate)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@HolidayName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@HolidayDate", SqlDbType.DateTime),
                    new SqlParameter("@HolidayId", SqlDbType.Int)
                };

                param[0].Value = holidayName;
                param[1].Value = holidayDate;
                param[2].Value = holidayId;

                sproc = new StoreProcedure(HolidayKeys.Sp_Upd_H0_Holiday, param);
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

        public long Delete(int holidayId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@HolidayId", SqlDbType.Int)
                };

                param[0].Value = holidayId;

                sproc = new StoreProcedure(HolidayKeys.Sp_Del_H0_Holiday, param);
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