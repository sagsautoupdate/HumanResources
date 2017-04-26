using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H;

namespace HRMDAL.H
{
    public class BonusNamesDAL : Dao
    {
        #region Insert, Update, Delete

        public long Insert(string bonusName, string description, int type)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@BonusName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@Description", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@Type", SqlDbType.Int)
                };

                param[0].Value = bonusName;
                param[1].Value = description;
                param[2].Value = type;

                sproc = new StoreProcedure(BonusNameKeys.SP_BONUS_NAME_INSERT, param);
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

        public DataTable GetAll()
        {
            Debug.Assert(sproc == null);
            var dt = new DataTable();
            try
            {
                sproc = new StoreProcedure(BonusNameKeys.SP_BONUS_NAME_ALL, null);
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

        public DataTable GetByType(int type)
        {
            Debug.Assert(sproc == null);
            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Type", SqlDbType.Int)
                };

                param[0].Value = type;

                sproc = new StoreProcedure(BonusNameKeys.SP_BONUS_NAME_BY_TYPE, param);
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

        public DataTable GetByIds(string BonusNameIds)
        {
            Debug.Assert(sproc == null);
            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@BonusNameIds", SqlDbType.VarChar, 254)
                };

                param[0].Value = BonusNameIds;

                sproc = new StoreProcedure(BonusNameKeys.Sp_Sel_H_BonusNames_By_Ids, param);
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