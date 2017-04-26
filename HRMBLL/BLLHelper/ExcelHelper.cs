using System.Data;
using System.Data.OleDb;

namespace HRMBLL.BLLHelper
{
    public class ExcelHelper
    {
        #region constructor

        public ExcelHelper(string fileName)
        {
            FileName = fileName;
        }

        #endregion

        #region properties

        public string FileName { get; set; }

        #endregion

        public DataTable ReadDataFromExcelToDataTable()
        {
            string strConn;
            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                      "Data Source=" + FileName + "; Extended Properties=Excel 8.0;";
            //You must use the $ after the object you reference in the spreadsheet
            var myCommand = new OleDbDataAdapter("SELECT * FROM [Sheet 1$]", strConn);
            var myDataSet = new DataSet();
            myCommand.Fill(myDataSet, "ExcelInfo");

            return myDataSet.Tables["ExcelInfo"];
        }

        public DataTable ReadDataFromExcelToDataTable(string sheetName)
        {
            string strConn;
            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                      "Data Source=" + FileName + "; Extended Properties=Excel 8.0;";
            //You must use the $ after the object you reference in the spreadsheet
            var myCommand = new OleDbDataAdapter("SELECT * FROM [" + sheetName + "$]", strConn);
            var myDataSet = new DataSet();
            myCommand.Fill(myDataSet, "ExcelInfo");

            return myDataSet.Tables["ExcelInfo"];
        }

        #region private fields

        #endregion
    }
}