using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H2;
using HRMUtil.KeyNames.H2;

namespace HRMBLL.H2
{
    public class CandidateEducationLevelsBLL
    {
        #region private fields

        #endregion

        #region properties

        public int Id { get; set; }

        public int CandidateId { get; set; }

        public int EducationLevelId { get; set; }

        public string EducationLevelValue { get; set; }

        public string EducationLevelName { get; set; }

        public string Remark { get; set; }

        public int LastItem { get; set; }

        #endregion

        #region public methods

        public long Save()
        {
            var objDAL = new CandidateEducationLevelsDAL();
            if (Id <= 0)
                return objDAL.Insert(CandidateId, EducationLevelId, EducationLevelValue, Remark);
            return objDAL.Update(CandidateId, EducationLevelId, EducationLevelValue, Remark, Id);
        }


        public static long Update(int candidateId, int educationLevelId, string educationLevelValue, string remark,
            int id)
        {
            return new CandidateEducationLevelsDAL().Update(candidateId, educationLevelId, educationLevelValue, remark,
                id);
        }

        public static long Delete(int id)
        {
            return new CandidateEducationLevelsDAL().Delete(id);
        }

        public static string Delete(string ids)
        {
            var arr = ids.Split(',');
            foreach (var arrItem in arr)
                if (arrItem.Length > 0)
                    Delete(int.Parse(arrItem));
            return ids;
        }

        #endregion

        #region public methods get

        public static List<CandidateEducationLevelsBLL> GetById(int candidateId)
        {
            return GenerateListFromDataTable(new CandidateEducationLevelsDAL().GetById(candidateId));
        }

        public static DataRow DR_GetById(int candidateId, int educationId)
        {
            var byCandidateIdEducationId = new CandidateEducationLevelsDAL().GetByCandidateIdEducationId(candidateId,
                educationId);
            if (byCandidateIdEducationId.Rows.Count > 0)
                return byCandidateIdEducationId.Rows[0];
            return null;
        }

        public static DataTable DT_GetById(int candidateId)
        {
            return new CandidateEducationLevelsDAL().GetById(candidateId);
        }

        #endregion

        #region private methods

        private static List<CandidateEducationLevelsBLL> GenerateListFromDataTable(DataTable dt)
        {
            var list = new List<CandidateEducationLevelsBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateFromDataTable(dr, dt.Rows.Count));

            return list;
        }

        private static CandidateEducationLevelsBLL GenerateFromDataTable(DataRow dr, int itemLastIndex)
        {
            var c = new CandidateEducationLevelsBLL();

            c.Id = dr[CandidateEducationLevelKeys.FIELD_CANDIDATE_EDUCATION_LEVEL_ID] == DBNull.Value
                ? 0
                : int.Parse(dr[CandidateEducationLevelKeys.FIELD_CANDIDATE_EDUCATION_LEVEL_ID].ToString());
            c.CandidateId = dr[CandidateEducationLevelKeys.FIELD_CANDIDATE_EDUCATION_LEVEL_CANDIDATE_ID] == DBNull.Value
                ? 0
                : int.Parse(dr[CandidateEducationLevelKeys.FIELD_CANDIDATE_EDUCATION_LEVEL_CANDIDATE_ID].ToString());
            c.EducationLevelId = dr[CandidateEducationLevelKeys.FIELD_CANDIDATE_EDUCATION_LEVEL_EDUCATION_LEVEL_ID] ==
                                 DBNull.Value
                ? 0
                : int.Parse(
                    dr[CandidateEducationLevelKeys.FIELD_CANDIDATE_EDUCATION_LEVEL_EDUCATION_LEVEL_ID].ToString());
            c.EducationLevelValue = dr[CandidateEducationLevelKeys.FIELD_CANDIDATE_EDUCATION_LEVEL_VALUE] ==
                                    DBNull.Value
                ? string.Empty
                : dr[CandidateEducationLevelKeys.FIELD_CANDIDATE_EDUCATION_LEVEL_VALUE].ToString();
            c.Remark = dr[CandidateEducationLevelKeys.FIELD_CANDIDATE_EDUCATION_LEVEL_REMARK] == DBNull.Value
                ? string.Empty
                : dr[CandidateEducationLevelKeys.FIELD_CANDIDATE_EDUCATION_LEVEL_REMARK].ToString();

            c.EducationLevelName = dr[EducationLevelKeys.FIELD_EDUCATION_LEVEL_NAME] == DBNull.Value
                ? string.Empty
                : dr[EducationLevelKeys.FIELD_EDUCATION_LEVEL_NAME].ToString();

            c.LastItem = itemLastIndex;

            return c;
        }

        #endregion
    }
}