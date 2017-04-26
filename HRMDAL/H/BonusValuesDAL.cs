using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H;

namespace HRMDAL.H
{
    public class BonusValuesDAL : Dao
    {
        #region methods Insert, Update, Delete

        public long Insert(int bonusNameId, string userCode, double value, int year)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@BonusNameId", SqlDbType.NVarChar, 100),
                    new SqlParameter("@UserCode", SqlDbType.VarChar, 50),
                    new SqlParameter("@BonusValue", SqlDbType.Float),
                    new SqlParameter("@BonusYear", SqlDbType.Int)
                };

                param[0].Value = bonusNameId;
                param[1].Value = userCode;
                param[2].Value = value;
                param[3].Value = year;

                sproc = new StoreProcedure(BonusValueKeys.SP_BONUS_VALUE_INSERT, param);
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

        #region methods Get

        public DataTable GetByFilter(string fullName, int departmentId, int year, int bonusNameId)
        {
            Debug.Assert(sproc == null);
            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@DepartmentId", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int),
                    new SqlParameter("@BonusNameId", SqlDbType.Int)
                };

                param[0].Value = fullName;
                param[1].Value = departmentId;
                param[2].Value = year;
                param[3].Value = bonusNameId;

                sproc = new StoreProcedure(BonusValueKeys.SP_BONUS_VALUE_GET_BY_FILTER, param);
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

        public DataTable GetByUserId_Year(int userId, int year, int type)
        {
            Debug.Assert(sproc == null);
            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int),
                    new SqlParameter("@Type", SqlDbType.Int)
                };

                param[0].Value = userId;
                param[1].Value = year;
                param[2].Value = type;

                sproc = new StoreProcedure(BonusValueKeys.SP_BONUS_VALUE_GET_BY_USERID_YEAR, param);
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

        public DataTable GetByYearBonusNameIdsUserId(int Year, string BonusNameIds, int UserId)
        {
            Debug.Assert(sproc == null);
            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Year", SqlDbType.Int),
                    new SqlParameter("@BonusNameIds", SqlDbType.VarChar, 1000),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                param[0].Value = Year;
                param[1].Value = BonusNameIds;
                param[2].Value = UserId;


                sproc = new StoreProcedure(BonusValueKeys.Sp_Sel_H_BonusValue_By_YearBonusNamesIds, param);
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


        public DataTable GetStatisticTotalByFilter(int departmentId, int year, int bonusNameId)
        {
            Debug.Assert(sproc == null);
            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DepartmentId", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int),
                    new SqlParameter("@BonusNameId", SqlDbType.Int)
                };

                param[0].Value = departmentId;
                param[1].Value = year;
                param[2].Value = bonusNameId;

                sproc = new StoreProcedure(BonusValueKeys.Sp_Sel_H_BonusValue_StatisticTotalBy_Filter, param);
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

        #endregion
    }
}