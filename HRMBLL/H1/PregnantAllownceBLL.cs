using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H1;
using HRMUtil;
using HRMUtil.KeyNames.H0;
using HRMUtil.KeyNames.H1;

namespace HRMBLL.H1
{
    public class PregnantAllownceBLL
    {
        #region private fields

        #endregion

        #region properties

        public int PregnantAllownceId { get; set; }

        public int UserId { get; set; }

        public DateTime AllownceDate { get; set; } = FormatDate.GetSQLDateMinValue;

        public double AllownceValue { get; set; }

        public int IsCount { get; set; }

        public int RootId { get; set; }

        public string FullName { get; set; } = string.Empty;

        #endregion

        #region public methods insert, update, delete

        public int Save()
        {
            var objDAL = new PregnantAllownceDAL();

            if (PregnantAllownceId <= 0)
                return objDAL.Insert(UserId, AllownceDate, AllownceValue, IsCount);
            return objDAL.Update(UserId, AllownceDate, AllownceValue, IsCount, PregnantAllownceId);
        }

        public static void Update(int userId, DateTime allownceDate, double allownceValue, int isCount,
            int pregnantAllownceId)
        {
            new PregnantAllownceDAL().Update(userId, allownceDate, allownceValue, isCount, pregnantAllownceId);
        }

        public static void Delete(int pregnantAllownceId)
        {
            new PregnantAllownceDAL().Delete(pregnantAllownceId);
        }

        public static void DeleteByUserId(int userId)
        {
            new PregnantAllownceDAL().DeleteByUserId(userId);
        }

        #endregion

        #region public methods GET

        public static List<PregnantAllownceBLL> GetByFilter(string fullName, int rootId, DateTime allownceDate)
        {
            return
                GenerateListPregnantAllownceBLLFromDataTable(new PregnantAllownceDAL().GetByFilter(fullName, rootId,
                    allownceDate));
        }

        public static PregnantAllownceBLL GetById(int pregnantAllownceId)
        {
            var list =
                GenerateListPregnantAllownceBLLFromDataTable(new PregnantAllownceDAL().GetById(pregnantAllownceId));
            if (list.Count == 1)
                return list[0];
            return null;
        }

        public static PregnantAllownceBLL GetByUserAllownceDate(int userId, DateTime allownceDate)
        {
            var list =
                GenerateListPregnantAllownceBLLFromDataTable(new PregnantAllownceDAL().GetByUserAllownceDate(userId,
                    allownceDate));
            if (list.Count == 1)
                return list[0];
            return null;
        }

        public static List<PregnantAllownceBLL> GetByUserId(int userId)
        {
            return GenerateListPregnantAllownceBLLFromDataTable(new PregnantAllownceDAL().GetByUserId(userId));
        }

        #endregion

        #region private methods

        private static List<PregnantAllownceBLL> GenerateListPregnantAllownceBLLFromDataTable(DataTable dt)
        {
            var list = new List<PregnantAllownceBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GeneratePregnantAllownceBLLFromDataRow(dr));

            return list;
        }

        private static PregnantAllownceBLL GeneratePregnantAllownceBLLFromDataRow(DataRow dr)
        {
            var objBLL = new PregnantAllownceBLL();

            objBLL.PregnantAllownceId = dr[PregnantAllownceKeys.Field_PregnantAllownce_PregnantAllownceId] ==
                                        DBNull.Value
                ? 0
                : int.Parse(dr[PregnantAllownceKeys.Field_PregnantAllownce_PregnantAllownceId].ToString());
            objBLL.UserId = dr[PregnantAllownceKeys.Field_PregnantAllownce_UserId] == DBNull.Value
                ? 0
                : int.Parse(dr[PregnantAllownceKeys.Field_PregnantAllownce_UserId].ToString());
            objBLL.AllownceDate = dr[PregnantAllownceKeys.Field_PregnantAllownce_AllownceDate] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr[PregnantAllownceKeys.Field_PregnantAllownce_AllownceDate].ToString());
            objBLL.AllownceValue = dr[PregnantAllownceKeys.Field_PregnantAllownce_AllownceValue] == DBNull.Value
                ? 0
                : double.Parse(dr[PregnantAllownceKeys.Field_PregnantAllownce_AllownceValue].ToString());
            objBLL.IsCount = dr[PregnantAllownceKeys.Field_PregnantAllownce_IsCount] == DBNull.Value
                ? 0
                : int.Parse(dr[PregnantAllownceKeys.Field_PregnantAllownce_IsCount].ToString());

            try
            {
                objBLL.FullName = dr[PregnantAllownceKeys.Field_PregnantAllownce_FullName] == DBNull.Value
                    ? string.Empty
                    : dr[PregnantAllownceKeys.Field_PregnantAllownce_FullName].ToString();
            }
            catch
            {
            }
            try
            {
                objBLL.RootId = dr[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID].ToString());
            }
            catch
            {
            }
            return objBLL;
        }

        #endregion
    }
}