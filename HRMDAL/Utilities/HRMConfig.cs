using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using HRMDAL.Properties;
using HRMUtil;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using AESm;

namespace HRMDAL.Utilities
{
    public class HRMConfig
    {
        public static string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
        private static SqlConnection sqlConnection;
        //public static string ConnectionString
        //{

        //    get
        //    {
        //        string mConnectionString = global::HRMDAL.Properties.Settings.Default.s;
        //        if (mConnectionString == null || mConnectionString.Length == 0)
        //            throw new HRMException("HRMConnectionString is missed....");
        //        else
        //            return mConnectionString;
        //    }

        //}
        
        public static SqlConnection GetSqlConnection()
        {
            var strCnnString = "";
            if ((sqlConnection != null) && (sqlConnection.State == ConnectionState.Open))
                return sqlConnection;
            if (ConnectionString == null)
                strCnnString = Settings.Default.s;
            else
            {
                strCnnString = AES.Decrypt(ConnectionString); // clsEncDec.Decrypt(ConnectionString);
            }
            sqlConnection = new SqlConnection(strCnnString);
            return sqlConnection;
        }
    }
    public enum ExitCode
    {
        Success = 0,
        UnknownError = 1
    }
}