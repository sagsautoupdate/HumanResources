using System;
using System.Data;
using System.Data.OleDb;

namespace HRMUtil
{
    public class ExcelFile
    {
        /// <summary>
        ///     Connection string of excel sheet 2003
        /// </summary>
        private static string _xlsConString =
            "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0";

        /// <summary>
        ///     Connection string of excel sheet 2007 or Above
        /// </summary>
        private static readonly string _xlsxConString =
            "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0";

        /// <summary>
        ///     Current Connection String
        /// </summary>
        private static string _connString = string.Empty;

        private static void SetConnectionString(string ExcelSheetPath)
        {
            //int count = ExcelSheetPath.Split('.').Length;
            //string ext = ExcelSheetPath.Split('.')[count - 1];
            //_connString = string.Format(_xlsConString, ExcelSheetPath);
            //if (ext.ToLower().Equals("xlsx"))
            _connString = string.Format(_xlsxConString, ExcelSheetPath);
        }

        /// <summary>
        ///     Get Names of all Sheets in Excel
        /// </summary>
        /// <param name="ExcelSheetPath">Excel Sheet file Path ex.C:\Excel.xlsx</param>
        /// <returns>Data Table contain Name of All sheets</returns>
        public static DataTable GetAllSheetsName(string ExcelSheetPath)
        {
            // Create the connection object 
            SetConnectionString(ExcelSheetPath);
            var oledbConn = new OleDbConnection(_connString);
            try
            {
                // Open connection 
                oledbConn.Open();

                // Create OleDbCommand object and select data from worksheet Sheet1 

                var dt = oledbConn.GetSchema("Tables");
                var bindDT = new DataTable();
                var dc = new DataColumn("Sheet Name");
                dc.ColumnName = "SheetName";
                bindDT.Columns.Add(dc);
                foreach (DataRow dr in dt.Rows)
                {
                    var drow = bindDT.NewRow();
                    drow[dc] = dr["TABLE_NAME"].ToString();
                    bindDT.Rows.Add(drow);
                }

                // return the data to the data table 
                return bindDT;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Close connection 
                oledbConn.Close();
            }
        }

        public static DataTable GetDataTableExcel(string strFileName, string Table)
        {
            //System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OleDb.12.0; Data Source = " + strFileName + "; Extended Properties = \"Excel 12.0 Xml;HDR=Yes;IMEX=1\";");
            SetConnectionString(strFileName);
            var conn = new OleDbConnection(_connString);
            conn.Open();
            var strQuery = "SELECT * FROM [" + Table + "]";
            var adapter = new OleDbDataAdapter(strQuery, conn);
            var ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];
        }
    }
}