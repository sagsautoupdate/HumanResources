using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H;

namespace HRMDAL.H
{
    public class BulletinDAL : Dao
    {
        #region Get

        public DataTable GetByNumberday(int Numberday)
        {
            Debug.Assert(sproc == null);
            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Numberday", SqlDbType.Int)
                };

                param[0].Value = Numberday;

                sproc = new StoreProcedure(BulletinKeyNames.Sp_Sel_BulletinByNumberday, param);
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