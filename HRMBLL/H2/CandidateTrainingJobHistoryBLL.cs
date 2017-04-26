using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H2;
using HRMUtil.KeyNames.H2;

namespace HRMBLL.H2
{
    public class CandidateTrainingJobHistoryBLL
    {
        public int CandidateId { get; set; }

        public string Training_Job { get; set; } = "";

        public string Year { get; set; } = "";

        public string School_Position { get; set; } = "";

        public string Major_Salary { get; set; } = "";

        public string GraduateYear_LeaveReason { get; set; } = "";

        public string Experience { get; set; } = "";

        public string Type { get; set; } = "";

        public int CandidateTrainingJobHistoryId { get; set; }

        public int LastItem { get; set; }

        public static long Insert(int CandidateId, string Training_Job, string Year, string School_Position,
            string Major_Salary, string GraduateYear_LeaveReason, string Experience, string Type)
        {
            return new CandidateTrainingJobHistoryDAL().Insert(CandidateId, Training_Job, Year, School_Position,
                Major_Salary, GraduateYear_LeaveReason, Experience, Type);
        }

        public static long Update(int CandidateId, string Training_Job, string Year, string School_Position,
            string Major_Salary, string GraduateYear_LeaveReason, string Experience, string Type,
            int CandidateTrainingJobHistoryId)
        {
            return new CandidateTrainingJobHistoryDAL().Update(CandidateId, Training_Job, Year, School_Position,
                Major_Salary, GraduateYear_LeaveReason, Experience, Type, CandidateTrainingJobHistoryId);
        }

        public static string Delete(string ids)
        {
            var arr = ids.Split(',');
            foreach (var arrItem in arr)
                if (arrItem.Length > 0)
                    Delete(int.Parse(arrItem));
            return ids;
        }

        public static long Delete(int CandidateTrainingJobHistoryId)
        {
            return new CandidateTrainingJobHistoryDAL().Delete(CandidateTrainingJobHistoryId);
        }

        public static List<CandidateTrainingJobHistoryBLL> GetByCandidateId_Type(int CandidateId, string Type)
        {
            return
                GenerateListFromDataTable(new CandidateTrainingJobHistoryDAL().GetByCandidateId_Type(CandidateId, Type));
        }

        public static DataTable GetDTByCandidateId_Type(int CandidateId, string Type)
        {
            return new CandidateTrainingJobHistoryDAL().GetByCandidateId_Type(CandidateId, Type);
        }

        public static List<CandidateTrainingJobHistoryBLL> GetById(int id)
        {
            return GenerateListFromDataTable(new CandidateTrainingJobHistoryDAL().GetById(id));
        }

        #region private methods

        private static List<CandidateTrainingJobHistoryBLL> GenerateListFromDataTable(DataTable dt)
        {
            var list = new List<CandidateTrainingJobHistoryBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateFromDataTable(dr, dt.Rows.Count));

            return list;
        }

        private static CandidateTrainingJobHistoryBLL GenerateFromDataTable(DataRow dr, int itemLastIndex)
        {
            var c = new CandidateTrainingJobHistoryBLL();

            c.CandidateId = dr[CandidateTrainingJobHistoryKeys.Filed_CandidateTrainingJobHistory_CandidateId] ==
                            DBNull.Value
                ? 0
                : int.Parse(dr[CandidateTrainingJobHistoryKeys.Filed_CandidateTrainingJobHistory_CandidateId].ToString());
            c.Training_Job = dr[CandidateTrainingJobHistoryKeys.Filed_CandidateTrainingJobHistory_Training_Job] ==
                             DBNull.Value
                ? ""
                : dr[CandidateTrainingJobHistoryKeys.Filed_CandidateTrainingJobHistory_Training_Job].ToString();
            c.Year = dr[CandidateTrainingJobHistoryKeys.Filed_CandidateTrainingJobHistory_Year] == DBNull.Value
                ? ""
                : dr[CandidateTrainingJobHistoryKeys.Filed_CandidateTrainingJobHistory_Year].ToString();
            c.School_Position = dr[CandidateTrainingJobHistoryKeys.Filed_CandidateTrainingJobHistory_School_Position] ==
                                DBNull.Value
                ? ""
                : dr[CandidateTrainingJobHistoryKeys.Filed_CandidateTrainingJobHistory_School_Position].ToString();
            c.Major_Salary = dr[CandidateTrainingJobHistoryKeys.Filed_CandidateTrainingJobHistory_Major_Salary] ==
                             DBNull.Value
                ? ""
                : dr[CandidateTrainingJobHistoryKeys.Filed_CandidateTrainingJobHistory_Major_Salary].ToString();
            c.GraduateYear_LeaveReason =
                dr[CandidateTrainingJobHistoryKeys.Filed_CandidateTrainingJobHistory_GraduateYear_LeaveReason] ==
                DBNull.Value
                    ? ""
                    : dr[CandidateTrainingJobHistoryKeys.Filed_CandidateTrainingJobHistory_GraduateYear_LeaveReason]
                        .ToString();
            c.Experience = dr[CandidateTrainingJobHistoryKeys.Filed_CandidateTrainingJobHistory_Experience] ==
                           DBNull.Value
                ? ""
                : dr[CandidateTrainingJobHistoryKeys.Filed_CandidateTrainingJobHistory_Experience].ToString();
            c.Type = dr[CandidateTrainingJobHistoryKeys.Filed_CandidateTrainingJobHistory_Type] == DBNull.Value
                ? ""
                : dr[CandidateTrainingJobHistoryKeys.Filed_CandidateTrainingJobHistory_Type].ToString();
            c.CandidateTrainingJobHistoryId =
                dr[CandidateTrainingJobHistoryKeys.Filed_CandidateTrainingJobHistory_CandidateTrainingJobHistoryId] ==
                DBNull.Value
                    ? 0
                    : int.Parse(
                        dr[
                            CandidateTrainingJobHistoryKeys
                                .Filed_CandidateTrainingJobHistory_CandidateTrainingJobHistoryId].ToString());


            c.LastItem = itemLastIndex;

            return c;
        }

        #endregion
    }
}