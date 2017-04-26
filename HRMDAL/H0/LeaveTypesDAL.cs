using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H0;

namespace HRMDAL.H0
{
    public class LeaveTypesDAL : Dao
    {
        #region methods Get

        public DataTable GetByFilter(string leaveTypeCode, string leaveTypeName, int type)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@LeaveTypeCode", SqlDbType.VarChar, 50),
                    new SqlParameter("@LeaveTypeName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Type", SqlDbType.Int)
                };

                param[0].Value = leaveTypeCode;
                param[1].Value = leaveTypeName;
                param[2].Value = type;

                sproc = new StoreProcedure(LeaveTypeKeys.Sp_Sel_H0_LeaveTypes_By_Filter, param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }


        public DataTable GetById(int leaveTypeId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@LeaveTypeId", SqlDbType.NVarChar, 254)
                };

                param[0].Value = leaveTypeId;

                sproc = new StoreProcedure(LeaveTypeKeys.Sp_Leave_Type_GetById, param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetByName(string leaveTypeName)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@LeaveTypeName", SqlDbType.NVarChar, 50)
                };

                param[0].Value = leaveTypeName;

                sproc = new StoreProcedure("Sel_H0_LeaveTypes_By_Name", param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetAll()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure(LeaveTypeKeys.Sp_Leave_Type_GetDTAll, null);
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