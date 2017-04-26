using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H1;

namespace HRMDAL.H1
{
    public class BonusTitleDAL : Dao
    {
        #region Get

        public DataTable GetByType(int type)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@BonusType", SqlDbType.Int)
                };

                param[0].Value = type;
                sproc = new StoreProcedure(BonusTitleKeys.Sp_Sel_H1_BonusTitle_ByType, param);
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
    }
}