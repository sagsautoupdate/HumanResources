using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H2;
using HRMUtil.KeyNames.H2;

namespace HRMBLL.H2
{
    public class EducationLevelsBLL
    {
        #region private fields

        #endregion

        #region properties

        public int Id { get; set; }

        public string Name { get; set; }

        public string Remark { get; set; }

        #endregion

        #region public methods

        public long Save()
        {
            var objDAL = new EducationLevelsDAL();
            if (Id <= 0)
                return objDAL.Insert(Name, Remark);
            return objDAL.Update(Name, Remark, Id);
        }

        public static long Update(string name, string remark, int id)
        {
            return new EducationLevelsDAL().Update(name, remark, id);
        }

        public static long Delete(int id)
        {
            return new EducationLevelsDAL().Delete(id);
        }

        #endregion

        #region public methods get

        public static List<EducationLevelsBLL> GetAll()
        {
            return GenerateListFromDataTable(new EducationLevelsDAL().GetAll());
        }

        public static List<EducationLevelsBLL> GetByFilter(string name, int orderByType)
        {
            return GenerateListFromDataTable(new EducationLevelsDAL().GetByFilter(name, orderByType));
        }

        #endregion

        #region new version

        public static DataTable GetDtAll()
        {
            return new EducationLevelsDAL().GetAll();
        }

        public static DataRow GetDRAll(string name)
        {
            var dt = new EducationLevelsDAL().GetByFilter(name, 0);
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        #endregion

        #region private methods

        private static List<EducationLevelsBLL> GenerateListFromDataTable(DataTable dt)
        {
            var list = new List<EducationLevelsBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateFromDataTable(dr));

            return list;
        }

        private static EducationLevelsBLL GenerateFromDataTable(DataRow dr)
        {
            var e = new EducationLevelsBLL();
            e.Id = dr[EducationLevelKeys.FIELD_EDUCATION_LEVEL_ID] == DBNull.Value
                ? 0
                : int.Parse(dr[EducationLevelKeys.FIELD_EDUCATION_LEVEL_ID].ToString());
            e.Name = dr[EducationLevelKeys.FIELD_EDUCATION_LEVEL_NAME] == DBNull.Value
                ? string.Empty
                : dr[EducationLevelKeys.FIELD_EDUCATION_LEVEL_NAME].ToString();
            e.Remark = dr[EducationLevelKeys.FIELD_EDUCATION_LEVEL_REMARK] == DBNull.Value
                ? string.Empty
                : dr[EducationLevelKeys.FIELD_EDUCATION_LEVEL_REMARK].ToString();

            return e;
        }

        #endregion
    }
}