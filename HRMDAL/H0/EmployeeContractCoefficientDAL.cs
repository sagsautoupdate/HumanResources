using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H0;

namespace HRMDAL.H0
{
    public class EmployeeContractCoefficientDAL : Dao
    {
        #region methods inset, update , delete

        public long Insert(int EmployeeContractId, int LCB_CoefficientEmployeeId, int LNS_CoefficientEmployeeId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@EmployeeContractId", SqlDbType.Int),
                    new SqlParameter("@LCB_CoefficientEmployeeId", SqlDbType.Int),
                    new SqlParameter("@LNS_CoefficientEmployeeId", SqlDbType.Int)
                };

                param[0].Value = EmployeeContractId;
                param[1].Value = LCB_CoefficientEmployeeId;
                param[2].Value = LNS_CoefficientEmployeeId;

                sproc = new StoreProcedure(EmployeeContractCoefficientKeys.Sp_Ins_H0_EmployeeContractCoefficient, param);
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


        public long Delete(int EmployeeContractId, int LCB_CoefficientEmployeeId, int LNS_CoefficientEmployeeId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@EmployeeContractId", SqlDbType.Int),
                    new SqlParameter("@LCB_CoefficientEmployeeId", SqlDbType.Int),
                    new SqlParameter("@LNS_CoefficientEmployeeId", SqlDbType.Int)
                };

                param[0].Value = EmployeeContractId;
                param[1].Value = LCB_CoefficientEmployeeId;
                param[2].Value = LNS_CoefficientEmployeeId;

                sproc = new StoreProcedure(EmployeeContractCoefficientKeys.SP_Del_H0_EmployeeContractCoefficient, param);
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