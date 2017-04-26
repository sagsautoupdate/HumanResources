using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H1;

namespace HRMDAL.H1
{
    public class CoefficientNamePositionsDAL : Dao
    {
        #region method GET

        public DataTable GetByNameId(int coefficientNameId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@CoefficientNameId", SqlDbType.Int)
                };

                param[0].Value = coefficientNameId;

                sproc = new StoreProcedure(CoefficientNamePositionKeys.SP_COEFFICIENT_NAME_POSITION_GET_BY_NAME_ID,
                    param);
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

        public DataTable GetByPositionId(int positionId, int type)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@PositionId", SqlDbType.Int),
                    new SqlParameter("@Type", SqlDbType.Int)
                };

                param[0].Value = positionId;
                param[1].Value = type;

                sproc = new StoreProcedure(CoefficientNamePositionKeys.SP_COEFFICIENT_NAME_POSITION_GET_BY_POSITION_ID,
                    param);
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

        #endregion
    }
}