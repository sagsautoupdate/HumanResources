using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H0;
using HRMUtil.KeyNames.H0;

namespace HRMBLL.H0
{
    public class EmployeeJobHistoryBLL
    {
        #region private fields

        private string _SP = "";
        private string _SPValue = "";

        #endregion

        #region properties

        public long JobHistoryId { get; set; }

        public int UserId { get; set; }

        public int FromYear { get; set; }

        public int ToYear { get; set; }

        public string Infor { get; set; } = string.Empty;

        public int Type { get; set; }

        public int LastItem { get; set; }

        #endregion

        #region public methods Get

        public static List<EmployeeJobHistoryBLL> GetByFilter(int? type, int? userId)
        {
            return GenerateListEmployeeJobHistoryBLLFromDataTable(new EmployeeJobHistoryDAL().GetByFilter(type, userId));
        }

        public static DataRow GetOne(int JobHistoryId)
        {
            var dt = new EmployeeJobHistoryDAL().GetOne(JobHistoryId);
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        /// <summary>
        ///     Author: Giang
        ///     Date: 17-Oct-14
        ///     Content: Lay job history tra ve DT
        /// </summary>
        /// <returns></returns>
        public static DataTable GetByFilterToDT(int? type, int? userId)
        {
            return new EmployeeJobHistoryDAL().GetByFilter(type, userId);
        }

        public static DataRow GetByFilterToDR(int? type, int? userId)
        {
            var dt = new EmployeeJobHistoryDAL().GetByFilter(type, userId);
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        #endregion

        #region public methods Insert, Update, Delete

        public string ReturnSP()
        {
            return _SP;
        }

        public string ReturnSPValue()
        {
            return _SPValue;
        }

        public long Save()
        {
            var objDAL = new EmployeeJobHistoryDAL();
            if (JobHistoryId <= 0)
            {
                _SP = $"{EmployeeJobHistoryKeys.Sp_EmployeeJobHistory_Insert}";
                _SPValue = $"UserId: {UserId}, FromYear: {FromYear}, ToYear: {ToYear}, Infor: N'{Infor}', Type: {Type}";
                return objDAL.Insert(UserId, FromYear, ToYear, Infor, Type);
            }
            _SP = $"{EmployeeJobHistoryKeys.Sp_EmployeeJobHistory_Update}";
            _SPValue =
                $"UserId: {UserId}, FromYear: {FromYear}, ToYear: {ToYear}, Infor: N'{Infor}', Type: {Type}, JobHistoryId: {JobHistoryId}";
            return objDAL.Update(UserId, FromYear, ToYear, Infor, Type, JobHistoryId);
        }

        public static void Update(
            int? userId,
            int? fromYear,
            int? toYear,
            string infor,
            int? type,
            long? jobHistoryId)
        {
            new EmployeeJobHistoryDAL().Update(userId, fromYear, toYear, infor, type, jobHistoryId);
        }

        public static void Delete(long? jobHistoryId)
        {
            new EmployeeJobHistoryDAL().Delete(jobHistoryId);
        }

        public static string Delete(string jobHistoryIds)
        {
            var arr = jobHistoryIds.Split(',');
            foreach (var arrItem in arr)
                if (arrItem.Length > 0)
                    Delete(int.Parse(arrItem));
            return jobHistoryIds;
        }

        #endregion

        #region private methods, generate helper methods

        private static List<EmployeeJobHistoryBLL> GenerateListEmployeeJobHistoryBLLFromDataTable(DataTable dt)
        {
            var list = new List<EmployeeJobHistoryBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateEmployeeJobHistoryBLLFromDataRow(dr, dt.Rows.Count));

            return list;
        }

        private static EmployeeJobHistoryBLL GenerateEmployeeJobHistoryBLLFromDataRow(DataRow dr)
        {
            var objBLL = new EmployeeJobHistoryBLL();

            objBLL.JobHistoryId = dr[EmployeeJobHistoryKeys.Field_EmployeeJobHistory_Id] == DBNull.Value
                ? 0
                : (long) dr[EmployeeJobHistoryKeys.Field_EmployeeJobHistory_Id];
            objBLL.UserId = dr[EmployeeJobHistoryKeys.Field_EmployeeJobHistory_UserId] == DBNull.Value
                ? 0
                : (int) dr[EmployeeJobHistoryKeys.Field_EmployeeJobHistory_UserId];
            objBLL.FromYear = dr[EmployeeJobHistoryKeys.Field_EmployeeJobHistory_FromYear] == DBNull.Value
                ? 0
                : (int) dr[EmployeeJobHistoryKeys.Field_EmployeeJobHistory_FromYear];
            objBLL.ToYear = dr[EmployeeJobHistoryKeys.Field_EmployeeJobHistory_ToYear] == DBNull.Value
                ? 0
                : (int) dr[EmployeeJobHistoryKeys.Field_EmployeeJobHistory_ToYear];
            objBLL.Infor = dr[EmployeeJobHistoryKeys.Field_EmployeeJobHistory_Infor] == DBNull.Value
                ? string.Empty
                : dr[EmployeeJobHistoryKeys.Field_EmployeeJobHistory_Infor].ToString();
            try
            {
                objBLL.Type = dr[EmployeeJobHistoryKeys.Field_EmployeeJobHistory_Type] == DBNull.Value
                    ? 0
                    : (int) dr[EmployeeJobHistoryKeys.Field_EmployeeJobHistory_Type];
            }
            catch
            {
            }

            return objBLL;
        }

        private static EmployeeJobHistoryBLL GenerateEmployeeJobHistoryBLLFromDataRow(DataRow dr, int itemLastIndex)
        {
            var objBLL = new EmployeeJobHistoryBLL();

            objBLL.JobHistoryId = dr[EmployeeJobHistoryKeys.Field_EmployeeJobHistory_Id] == DBNull.Value
                ? 0
                : (long) dr[EmployeeJobHistoryKeys.Field_EmployeeJobHistory_Id];
            objBLL.UserId = dr[EmployeeJobHistoryKeys.Field_EmployeeJobHistory_UserId] == DBNull.Value
                ? 0
                : (int) dr[EmployeeJobHistoryKeys.Field_EmployeeJobHistory_UserId];
            objBLL.FromYear = dr[EmployeeJobHistoryKeys.Field_EmployeeJobHistory_FromYear] == DBNull.Value
                ? 0
                : (int) dr[EmployeeJobHistoryKeys.Field_EmployeeJobHistory_FromYear];
            objBLL.ToYear = dr[EmployeeJobHistoryKeys.Field_EmployeeJobHistory_ToYear] == DBNull.Value
                ? 0
                : (int) dr[EmployeeJobHistoryKeys.Field_EmployeeJobHistory_ToYear];
            objBLL.Infor = dr[EmployeeJobHistoryKeys.Field_EmployeeJobHistory_Infor] == DBNull.Value
                ? string.Empty
                : dr[EmployeeJobHistoryKeys.Field_EmployeeJobHistory_Infor].ToString();
            try
            {
                objBLL.Type = dr[EmployeeJobHistoryKeys.Field_EmployeeJobHistory_Type] == DBNull.Value
                    ? 0
                    : (int) dr[EmployeeJobHistoryKeys.Field_EmployeeJobHistory_Type];
            }
            catch
            {
            }

            objBLL.LastItem = itemLastIndex;

            return objBLL;
        }

        #endregion
    }
}