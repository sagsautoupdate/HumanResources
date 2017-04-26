using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;

namespace HRMDAL.H
{
    public class AdministrationDAL : Dao
    {
        public DataTable GetExportTable(string tableName)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@TableName", SqlDbType.NVarChar, 4000)
                };
                param[0].Value = tableName;

                sproc = new StoreProcedure("Sel_GetExportTable", param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetExportData(string tableName, string columnName, string condition)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@TableName", SqlDbType.NVarChar, 4000),
                    new SqlParameter("@ColumnsName", SqlDbType.NVarChar, 4000),
                    new SqlParameter("@Condition", SqlDbType.NVarChar, 4000)
                };
                param[0].Value = tableName;
                param[1].Value = columnName;
                param[2].Value = condition;

                sproc = new StoreProcedure("Sel_GetExportData", param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public long Save_HRM_SoftwareManagement_IsAllowToRun(string SoftwareName, bool IsAllowToRun)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@SoftwareName", SqlDbType.NVarChar, 4000),
                    new SqlParameter("@IsAllowToRun", SqlDbType.Bit)
                };

                param[0].Value = SoftwareName;
                param[1].Value = IsAllowToRun;

                sproc = new StoreProcedure("Upd_SaveHRM_SoftwareManagement_IsAllowToRun", param);
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

        public long Save_HRM_SoftwareManagement(int SoftwareId, string SoftwareName, string SoftwareGuid,
            string LastestVersion, bool IsAllowToRun)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@SoftwareId", SqlDbType.Int),
                    new SqlParameter("@SoftwareName", SqlDbType.NVarChar, 4000),
                    new SqlParameter("@SoftwareGuid", SqlDbType.NVarChar, 4000),
                    new SqlParameter("@LastestVersion", SqlDbType.NVarChar, 4000),
                    new SqlParameter("@IsAllowToRun", SqlDbType.Bit)
                };

                param[0].Value = SoftwareId;
                param[1].Value = SoftwareName;
                param[2].Value = SoftwareGuid;
                param[3].Value = LastestVersion;
                param[4].Value = IsAllowToRun;

                sproc = new StoreProcedure("p_SaveHRM_SoftwareManagement", param);
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

        public long Save_HRM_Table_TranslatedColumn(int colId, string colName, string colNameInVI)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@ColumnId", SqlDbType.Int),
                    new SqlParameter("@ColumnName", SqlDbType.NVarChar, 4000),
                    new SqlParameter("@ColumnNameInVI", SqlDbType.NVarChar, 4000)
                };

                param[0].Value = colId;
                param[1].Value = colName;
                param[2].Value = colNameInVI;


                sproc = new StoreProcedure("p_SaveHRM_Table_TranslatedColumn", param);
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

        public DataTable GetSoftwareInfo_ByName(string colName)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@SoftwareName", SqlDbType.NVarChar, 4000)
                };
                param[0].Value = colName;

                sproc = new StoreProcedure("Sel_H_GetHRM_SoftwareManagement", param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetAllTranslatedColumn_ByName(string colName)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@ColumnName", SqlDbType.NVarChar, 4000)
                };
                param[0].Value = colName;

                sproc = new StoreProcedure("Sel_H_GetHRM_Table_TranslatedColumn_ByName", param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetAllTranslatedColumn()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure("Sel_H_GetHRM_Table_TranslatedColumn", null);
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

        public DataTable GetAll()
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                sproc = new StoreProcedure("Sel_H_GetHRM_ChangedLog", null);
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

        public long Save(int HistoryId, int UserId, string FullName, string IPAddress, string MACAddress,
            string ServerName,
            string InfluencedTable, string StoredProcedures, string ChangedContent, DateTime ChangedDate,
            string OldContent)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@HistoryId", SqlDbType.Int),
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 4000),
                    new SqlParameter("@IPAddress", SqlDbType.NVarChar, 4000),
                    new SqlParameter("@MACAddress", SqlDbType.NVarChar, 4000),
                    new SqlParameter("@ServerName", SqlDbType.NText),
                    new SqlParameter("@InfluencedTable", SqlDbType.NVarChar, 4000),
                    new SqlParameter("@StoredProcedures", SqlDbType.NText),
                    new SqlParameter("@ChangedContent", SqlDbType.NText),
                    new SqlParameter("@ChangedDate", SqlDbType.DateTime),
                    new SqlParameter("@OldContent", SqlDbType.NText)
                };
                param[0].Value = HistoryId;
                param[1].Value = UserId;
                param[2].Value = FullName;
                param[3].Value = IPAddress;
                param[4].Value = MACAddress;
                param[5].Value = ServerName;
                param[6].Value = InfluencedTable;
                param[7].Value = StoredProcedures;
                param[8].Value = ChangedContent;
                param[9].Value = ChangedDate;
                param[10].Value = OldContent;

                sproc = new StoreProcedure("p_SaveHRM_ChangedLog", param);
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
    }
}