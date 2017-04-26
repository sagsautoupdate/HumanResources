using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H0;

namespace HRMDAL.H0
{
    public class SiteMapDAL : Dao
    {
        #region get

        public DataTable GetAllRoots()
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                sproc = new StoreProcedure(SiteMapKeyNames.SP_SITE_MAP_GET_BY_ROOTS, null);
                sproc.RunFill(dt);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetByParentId(int parentId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@ParentId", SqlDbType.Int)
                };

                param[0].Value = parentId;

                sproc = new StoreProcedure(SiteMapKeyNames.SP_SITE_MAP_GET_BY_PARENT_ID, param);
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