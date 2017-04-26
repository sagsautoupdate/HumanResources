using System;
using System.Data;
using HRMDAL.H;

namespace HRMBLL.H
{
    public class AdministrationBLL
    {
        //History
        public static DataTable GetAll()
        {
            return new AdministrationDAL().GetAll();
        }

        public static long Save(int HistoryId, int UserId, string FullName, string IPAddress, string MACAddress,
            string ServerName, string InfluencedTable,
            string StoredProcedures, string ChangedContent, DateTime ChangedDate, string OldContent)
        {
            return new AdministrationDAL().Save(HistoryId, UserId, FullName,
                IPAddress, MACAddress, ServerName, InfluencedTable,
                StoredProcedures, ChangedContent, ChangedDate, OldContent);
        }

        //Columns
        public static DataTable GetAllTranslatedColumn()
        {
            return new AdministrationDAL().GetAllTranslatedColumn();
        }

        public static DataRow GetAllTranslatedColumn_ByName(string colName)
        {
            var oneByUserName = new AdministrationDAL().GetAllTranslatedColumn_ByName(colName);
            if (oneByUserName.Rows.Count > 0)
                return oneByUserName.Rows[0];
            return null;
        }

        public static long Save_HRM_Table_TranslatedColumn(int colId, string colName, string colNameInVI)
        {
            return new AdministrationDAL().Save_HRM_Table_TranslatedColumn(colId, colName, colNameInVI);
        }

        public static DataTable GetExportData(string tableName, string columnName, string condition)
        {
            return new AdministrationDAL().GetExportData(tableName, columnName, condition);
        }

        public static DataTable GetExportTable(string tableName)
        {
            return new AdministrationDAL().GetExportTable(tableName);
        }

        //Software management
        public static DataRow GetSoftwareInfo_ByName(string softwareName)
        {
            var oneByUserName = new AdministrationDAL().GetSoftwareInfo_ByName(softwareName);
            if (oneByUserName.Rows.Count > 0)
                return oneByUserName.Rows[0];
            return null;
        }

        public static long Save_HRM_SoftwareManagement(int SoftwareId, string SoftwareName, string SoftwareGuid,
            string LastestVersion, bool IsAllowToRun)
        {
            return new AdministrationDAL().Save_HRM_SoftwareManagement(SoftwareId, SoftwareName, SoftwareGuid,
                LastestVersion, IsAllowToRun);
        }

        public static long Save_HRM_SoftwareManagement_IsAllowToRun(string SoftwareName, bool IsAllowToRun)
        {
            return new AdministrationDAL().Save_HRM_SoftwareManagement_IsAllowToRun(SoftwareName, IsAllowToRun);
        }
    }
}