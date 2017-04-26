using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H1;

namespace HRMDAL.H1
{
    public class BonusEmployeesDAL : Dao
    {
        #region Get

        public DataTable GetByFilter(int bonusYear, DateTime payDate, int bonusNameId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@BonusYear", SqlDbType.Int),
                    new SqlParameter("@PayDate", SqlDbType.DateTime),
                    new SqlParameter("@BonusNameId", SqlDbType.Int)
                };

                param[0].Value = bonusYear;
                param[1].Value = payDate;
                param[2].Value = bonusNameId;
                sproc = new StoreProcedure(BonusEmployeesKeys.Sp_Sel_H1_BonusEmployees_By_Filter, param);
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