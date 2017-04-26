using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H1;

namespace HRMDAL.H1
{
    public class CoefficientLevelsDAL : Dao
    {
        #region Get

        public DataTable GetAll(int type)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Type", SqlDbType.Int)
                };
                param[0].Value = type;
                sproc = new StoreProcedure(CoefficientLevelKeys.Sp_Sel_H1_CoefficientLevels_All, param);
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