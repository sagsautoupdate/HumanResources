using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Locations
{
    public class HRMConfig
    {
        public static string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
        private static SqlConnection sqlConnection;
        public static StoreProcedure sproc;
        private static List<string> _listConnectedServer;
        private static List<string> _listIsAdminServer;

        public void Dispose()
        {
            if (sproc != null)
            {
                sproc.Dispose();
                sproc = null;
            }
        }

        public static SqlConnection GetSqlConnection()
        {
            var strCnnString = string.Empty;
            if ((sqlConnection != null) && (sqlConnection.State == ConnectionState.Open))
                return sqlConnection;
            if (ConnectionString == null)
                strCnnString = string.Empty;
            else
                strCnnString = clsEncDec.Decrypt(ConnectionString);
            sqlConnection = new SqlConnection(strCnnString);
            return sqlConnection;
        }


        public static int ParrellLogin(string userName, string password)
        {
            var count = 0;
            _listConnectedServer = new List<string>();
            foreach (ConnectionStringSettings css in ConfigurationManager.ConnectionStrings)
            {
                var name = css.Name;
                var connectionString = css.ConnectionString;
                if (!name.Contains("LocalSqlServer"))
                {
                    ConnectionString = connectionString;
                    var dr = LoginNew(userName, password);
                    if (dr != null)
                    {
                        count++;
                        _listConnectedServer.Add(name);
                    }
                }
            }
            return count;
        }

        public static int ParrellCheckingRole(string userName)
        {
            var count = 0;
            _listIsAdminServer = new List<string>();
            foreach (ConnectionStringSettings css in ConfigurationManager.ConnectionStrings)
            {
                var name = css.Name;
                var connectionString = css.ConnectionString;
                if (!name.Contains("LocalSqlServer"))
                {
                    ConnectionString = connectionString;
                    var dt = GetOneByUserName(userName);
                    if (dt.Rows.Count == 1)
                        if (IsAdmin(Convert.ToInt32(dt.Rows[0]["UserId"])))
                        {
                            count++;
                            _listIsAdminServer.Add(name);
                        }
                }
            }
            return count;
        }

        public static List<string> GetCouldBeConnectedServerName(string userName, string password)
        {
            _listConnectedServer = new List<string>();
            foreach (ConnectionStringSettings css in ConfigurationManager.ConnectionStrings)
            {
                var name = css.Name;
                var connectionString = css.ConnectionString;
                var defaultGateway = NetworkInterfaces.GetDefaultInterface().GetIPProperties().GatewayAddresses.FirstOrDefault().Address.ToString();
                switch (defaultGateway)
                {
                    case "10.10.0.1":
                    {
                            if (!name.Contains("LocalSqlServer"))
                            {
                                ConnectionString = connectionString;
                                if (checkConnection(connectionString) == true)
                                {
                                    DataRow dr = null;
                                    try
                                    {
                                        dr = LoginNew(userName, password);
                                    }
                                    catch
                                    {
                                        dr = null;
                                    }
                                    if (dr != null)
                                    {
                                        _listConnectedServer.Add(name);
                                    }
                                }
                            }
                        }
                        break;
                    case "10.10.77.1":
                        {
                            if (!name.Contains("LocalSqlServer"))
                            {
                                ConnectionString = connectionString;
                                if (checkConnection(connectionString) == true)
                                {
                                    DataRow dr = null;
                                    try
                                    {
                                        dr = LoginNew(userName, password);
                                    }
                                    catch
                                    {
                                        dr = null;
                                    }
                                    if (dr != null)
                                    {
                                        _listConnectedServer.Add(name);
                                    }
                                }
                            }
                        }
                        break;
                    case "172.16.234.1":
                    {
                            if (!name.Contains("LocalSqlServer") && !name.Contains("Server_SAGS") && !name.Contains("Server_CXR"))
                            {
                                ConnectionString = connectionString;
                                if (checkConnection(connectionString) == true)
                                {
                                    DataRow dr = null;
                                    try
                                    {
                                        dr = LoginNew(userName, password);
                                    }
                                    catch
                                    {
                                        dr = null;
                                    }
                                    if (dr != null)
                                    {
                                        _listConnectedServer.Add(name);
                                    }
                                }
                            }
                        }
                        break;
                    case "172.16.112.1":
                        {
                            if (!name.Contains("LocalSqlServer") && !name.Contains("Server_SAGS") && !name.Contains("Server_DAD"))
                            {
                                ConnectionString = connectionString;
                                if (checkConnection(connectionString) == true)
                                {
                                    DataRow dr = null;
                                    try
                                    {
                                        dr = LoginNew(userName, password);
                                    }
                                    catch
                                    {
                                        dr = null;
                                    }
                                    if (dr != null)
                                    {
                                        _listConnectedServer.Add(name);
                                    }
                                }
                            }
                        }
                        break;
                }
                
            }
            return _listConnectedServer;
        }

        public static bool checkConnection(string connectionString)
        {
            SqlConnection conn = new SqlConnection(clsEncDec.Decrypt(connectionString));
            try
            {
                conn.Open();
                return true;
            }
            catch (Exception ex) { return false; }
        }

        public static List<string> GetIsAdminServerName()
        {
            return _listIsAdminServer;
        }

        public static int LocationCount(string userName, string password)
        {
            return ParrellLogin(userName, password);
        }

        public static int RoleCount(string userName)
        {
            return ParrellCheckingRole(userName);
        }

        public static int TestPing(string Server)
        {
            var Result = -1;

            if (string.IsNullOrEmpty(Server))
                Result = (int) ExitCode.UnknownError;
            else
                try
                {
                    var pingSender = new Ping();
                    var options = new PingOptions
                    {
                        DontFragment = true
                    };


                    const string data = "Test";
                    var buffer = Encoding.ASCII.GetBytes(data);
                    const int timeout = 120;
                    var reply = pingSender.Send(Server, timeout, buffer, options);
                    if (reply.Status == IPStatus.Success)
                        Result = (int) ExitCode.Success;
                }
                catch
                {
                }
            return Result;
        }

        public static List<string> GetAllNames()
        {
            var List = new List<string>();
            foreach (ConnectionStringSettings css in ConfigurationManager.ConnectionStrings)
            {
                var name = css.Name;
                if (name.Contains("Server_"))
                    List.Add(name);
            }
            return List;
        }

        public static string GetValueByName(string key)
        {
            var ReturnValue = string.Empty;

            foreach (ConnectionStringSettings css in ConfigurationManager.ConnectionStrings)
            {
                var name = css.Name;
                var connectionString = css.ConnectionString;
                if (name == key)
                    ReturnValue = connectionString;
            }
            return ReturnValue;
        }

        public static string GetServerByKey(string key)
        {
            var Value = clsEncDec.Decrypt(GetValueByName(key));

            var Start = Value.IndexOf("=");
            var End = Value.IndexOf(";");

            return Value.Substring(Start + 1, End - Start - 1);
        }

        public static string GetServerByKeyWithoutDecrypt(string key)
        {
            var Value = clsEncDec.Decrypt(key);

            var Start = Value.IndexOf("=");
            var End = Value.IndexOf(";");

            return Value.Substring(Start + 1, End - Start - 1);
        }

        public static List<string> GetAllActiveServer()
        {
            var ListSucceeded = new List<string>();
            foreach (var item in GetAllNames())
            {
                var Server = GetServerByKey(item);
                if ((TestPing(Server) == 0) && (item.Equals("ConnectionString") == false))
                    ListSucceeded.Add(item);
            }
            return ListSucceeded;
        }

        public static int CountAllActiveServer()
        {
            return GetAllActiveServer().Count;
        }

        public static DataTable GetOneByUserName(string userName)
        {
            var datatable = new DataTable();
            try
            {
                var param =
                    new[]
                    {
                        new SqlParameter("@UserName", SqlDbType.VarChar, 50)
                    };
                param[0].Value = userName;
                sproc = new StoreProcedure("Sel_H0_Employee_By_UserName", param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
            }
            finally
            {
                sproc.Dispose();
            }

            return datatable;
        }

        public static DataTable GetOne(int userId)
        {
            var datatable = new DataTable();
            try
            {
                var param =
                    new[]
                    {
                        new SqlParameter("@UserId", SqlDbType.Int, 4)
                    };
                param[0].Value = userId;
                sproc = new StoreProcedure("Sel_H0_Employee_By_Id", param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
            }
            finally
            {
                sproc.Dispose();
            }

            return datatable;
        }

        public static DataRow GetEmployeeById(int userId)
        {
            var one = GetOne(userId);
            if (one.Rows.Count > 0)
                return one.Rows[0];
            return null;
        }

        public static DataRow LoginNew(string userName, string password)
        {
            var dt = Login(userName, password);

            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        public static DataTable Login(string userName, string password)
        {
            var datatable = new DataTable();
            try
            {
                var param =
                    new[]
                    {
                        new SqlParameter("@UserName", SqlDbType.VarChar, 50),
                        new SqlParameter("@Password", SqlDbType.VarChar, 50)
                    };
                param[0].Value = userName;
                param[1].Value = password;

                sproc = new StoreProcedure("Login", param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
            }
            finally
            {
                sproc.Dispose();
            }

            return datatable;
        }

        public static bool IsAdmin(int userId)
        {
            var list =
                GetByUserId_RoleId(userId, 1);

            if (list.Rows.Count > 0)
                return true;
            return false;
        }

        public static DataTable GetByUserId_RoleId(int userId, int roleId)
        {
            var dt = new DataTable();
            try
            {
                var param =
                    new[]
                    {
                        new SqlParameter("@UserId", SqlDbType.Int),
                        new SqlParameter("@RoleId", SqlDbType.Int)
                    };

                param[0].Value = userId;
                param[1].Value = roleId;

                sproc = new StoreProcedure("Sel_H0_UserRoles_By_UserId_RoleId", param);
                sproc.RunFill(dt);
            }
            catch (SqlException se)
            {
            }
            finally
            {
                sproc.Dispose();
            }
            return dt;
        }
    }

    public enum ExitCode
    {
        Success = 0,
        UnknownError = 1
    }

    public static class NetworkInterfaces
    {
        public static NetworkInterface GetDefaultInterface()
        {
            var interfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            foreach (var intf in interfaces)
            {
                if (intf.OperationalStatus != OperationalStatus.Up)
                {
                    continue;
                }
                if (intf.NetworkInterfaceType == NetworkInterfaceType.Loopback)
                {
                    continue;
                }

                var properties = intf.GetIPProperties();
                if (properties == null)
                {
                    continue;
                }
                var gateways = properties.GatewayAddresses;
                if ((gateways == null) || (gateways.Count == 0))
                {
                    continue;
                }
                var addresses = properties.UnicastAddresses;
                if ((addresses == null) || (addresses.Count == 0))
                {
                    continue;
                }
                return intf;
            }
            return null;
        }

        public static IPAddress GetDefaultIPV4Address(NetworkInterface intf)
        {
            if (intf == null)
            {
                return null;
            }
            foreach (var address in intf.GetIPProperties().UnicastAddresses)
            {
                if (address.Address.AddressFamily != AddressFamily.InterNetwork)
                {
                    continue;
                }
                if (address.IsTransient)
                {
                    continue;
                }
                return address.Address;
            }
            return null;
        }
    }
}