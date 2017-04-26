using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace HRMDAL.Utilities
{
    public sealed class StoreProcedure
    {
        private readonly SqlConnection con;
        private readonly SqlTransaction tran;
        private SqlCommand command;

        /// <summary>
        /// </summary>
        /// <param name="sprocName"></param>
        /// <param name="parameters"></param>
        public StoreProcedure(string sprocName, SqlParameter[] parameters)
        {
            try
            {
                //con = new SqlConnection(HRMConfig.ConnectionString);
                con = HRMConfig.GetSqlConnection();
                con.Open();
                tran = con.BeginTransaction();
                command = new SqlCommand(sprocName, con, tran);
                command.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                    foreach (var param in parameters)
                        command.Parameters.Add(param);


                command.Parameters.Add(new SqlParameter("ReturnValue",
                    SqlDbType.Int,
                    4,
                    ParameterDirection.ReturnValue,
                    false,
                    0,
                    0,
                    string.Empty,
                    DataRowVersion.Default,
                    null));
            }
            catch
            {
                tran.Rollback();
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// </summary>
        public void Dispose()
        {
            var cn = command.Connection;
            Debug.Assert(cn != null);
            command.Dispose();
            command = null;
            cn.Close();
            cn.Dispose();
            con.Close();
            con.Dispose();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public long Run()
        {
            if (command == null)
                throw new ObjectDisposedException(GetType().FullName);
            if (con.State == ConnectionState.Closed)
                con.Open();
            command.ExecuteNonQuery();
            return Convert.ToInt64(command.Parameters["ReturnValue"].Value);
        }

        public void UserTracking()
        {
            var sprocName = "p_SaveHRM_ChangedLog";
        }

        public int RunInt()
        {
            if (command == null)
                throw new ObjectDisposedException(GetType().FullName);
            if (con.State == ConnectionState.Closed)
                con.Open();
            command.ExecuteNonQuery();
            return (int) command.Parameters["ReturnValue"].Value;
        }

        /// <summary>
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public int RunFill(DataTable table)
        {
            if (command == null)
                throw new ObjectDisposedException(GetType().FullName);
            var adap = new SqlDataAdapter();
            adap.SelectCommand = command;
            adap.Fill(table);
            return (int) command.Parameters["ReturnValue"].Value;
        }

        /// <summary>
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader RunReader()
        {
            if (command == null)
                throw new ObjectDisposedException(GetType().FullName);
            return command.ExecuteReader();
        }

        public void RollBack()
        {
            tran.Rollback();
        }

        public void Commit()
        {
            try
            {
                tran.Commit();
            }
            catch
            {
                tran.Dispose();
            }
            finally
            {
                con.Close();
            }
        }
    }
}