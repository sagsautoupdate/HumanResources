using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H1;
using HRMUtil;
using HRMUtil.KeyNames.H0;
using HRMUtil.KeyNames.H1;

namespace HRMBLL.H1
{
    public class HTCVEmployeeBLL
    {
        #region private fields

        #endregion

        #region properties

        public long HTCVEmployeeId { get; set; }

        public int UserId { get; set; }

        public int HTCVCatalogueId { get; set; }

        public string HTCVCatalogueName { get; set; }

        public double Mark { get; set; }

        public int TypeDisplay { get; set; }

        public DateTime MarkDate { get; set; }

        public string MarkDisplay { get; set; }

        public string Remark { get; set; }

        public double MinMark { get; set; }

        public double MaxMark { get; set; }

        public int ParentId { get; set; }

        public string FullName { get; set; }

        public string PositionName { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentFullName { get; set; }

        #endregion

        #region public methods insert, update, delete

        public int Save()
        {
            var objDAL = new HTCVEmployeeDAL();

            if (HTCVEmployeeId <= 0)
                return objDAL.Insert(UserId, HTCVCatalogueId, Mark, MarkDate, Remark);
            return objDAL.Update(UserId, HTCVCatalogueId, Mark, MarkDate, Remark, HTCVEmployeeId);
        }

        public static void Update(int userId, int hTCVCatalogueId, double mark, DateTime markDate, string remark,
            long hTCVEmployeeId)
        {
            new HTCVEmployeeDAL().Update(userId, hTCVCatalogueId, mark, markDate, remark, hTCVEmployeeId);
        }

        public static void Delete(int hTCVEmployeeId)
        {
            new HTCVEmployeeDAL().Delete(hTCVEmployeeId);
        }

        public static void DeleteByIds(string hTCVEmployeeIds)
        {
            new HTCVEmployeeDAL().DeleteByIds(hTCVEmployeeIds);
        }

        public static void DeleteByUserDate(int userId, DateTime markDate)
        {
            new HTCVEmployeeDAL().DeleteDate(userId, markDate);
        }

        #endregion

        #region public methods GET

        public static List<HTCVEmployeeBLL> GetByFilter(int userid, int hTCVCatalogueId, double mark, DateTime markdate)
        {
            return
                GenerateListHTCVEmployeeBLLFromDataTable(new HTCVEmployeeDAL().GetByFilter(userid, hTCVCatalogueId, mark,
                    markdate));
        }

        public static string GetForAllRemarkByUserIdDate(int userid, int month, int year)
        {
            var dt = new HTCVEmployeeDAL().GetForAllRemarkByUserIdDate(userid, month, year);
            return dt.Rows.Count > 0 ? dt.Rows[0]["AllRemark"].ToString() : "";
        }

        #endregion

        #region private methods

        private static List<HTCVEmployeeBLL> GenerateListHTCVEmployeeBLLFromDataTable(DataTable dt)
        {
            var list = new List<HTCVEmployeeBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateHTCVEmployeeBLLFromDataRow(dr));

            return list;
        }

        private static HTCVEmployeeBLL GenerateHTCVEmployeeBLLFromDataRow(DataRow dr)
        {
            var objBLL = new HTCVEmployeeBLL();

            objBLL.HTCVEmployeeId = dr[HTCVEmployeeKeys.Field_HTCVEmployee_ID] == DBNull.Value
                ? 0
                : long.Parse(dr[HTCVEmployeeKeys.Field_HTCVEmployee_ID].ToString());
            objBLL.HTCVCatalogueId = dr[HTCVEmployeeKeys.Field_HTCVEmployee_HTCVCatalogueId] == DBNull.Value
                ? 0
                : int.Parse(dr[HTCVEmployeeKeys.Field_HTCVEmployee_HTCVCatalogueId].ToString());
            objBLL.Mark = dr[HTCVEmployeeKeys.Field_HTCVEmployee_Mark] == DBNull.Value
                ? 0
                : double.Parse(dr[HTCVEmployeeKeys.Field_HTCVEmployee_Mark].ToString());
            objBLL.MarkDate = dr[HTCVEmployeeKeys.Field_HTCVEmployee_MarkDate] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr[HTCVEmployeeKeys.Field_HTCVEmployee_MarkDate].ToString());
            objBLL.Remark = dr[HTCVEmployeeKeys.Field_HTCVEmployee_Remark] == DBNull.Value
                ? string.Empty
                : dr[HTCVEmployeeKeys.Field_HTCVEmployee_Remark].ToString();
            objBLL.TypeDisplay = dr[HTCVCatalogueKeys.Field_HTCVCatalogue_TypeDisplay] == DBNull.Value
                ? 0
                : int.Parse(dr[HTCVCatalogueKeys.Field_HTCVCatalogue_TypeDisplay].ToString());
            objBLL.UserId = dr[HTCVEmployeeKeys.Field_HTCVEmployee_UserId] == DBNull.Value
                ? 0
                : int.Parse(dr[HTCVEmployeeKeys.Field_HTCVEmployee_UserId].ToString());
            try
            {
                objBLL.HTCVCatalogueName = dr[HTCVCatalogueKeys.Field_HTCVCatalogue_Name] == DBNull.Value
                    ? string.Empty
                    : dr[HTCVCatalogueKeys.Field_HTCVCatalogue_Name].ToString();
            }
            catch
            {
            }

            objBLL.MarkDisplay = dr[HTCVCatalogueKeys.Field_HTCVCatalogue_MarkDisplay] == DBNull.Value
                ? string.Empty
                : dr[HTCVCatalogueKeys.Field_HTCVCatalogue_MarkDisplay].ToString();
            objBLL.MinMark = dr[HTCVCatalogueKeys.Field_HTCVCatalogue_MinMark] == DBNull.Value
                ? 0
                : double.Parse(dr[HTCVCatalogueKeys.Field_HTCVCatalogue_MinMark].ToString());
            objBLL.MaxMark = dr[HTCVCatalogueKeys.Field_HTCVCatalogue_MaxMark] == DBNull.Value
                ? 0
                : double.Parse(dr[HTCVCatalogueKeys.Field_HTCVCatalogue_MaxMark].ToString());
            objBLL.ParentId = dr[HTCVCatalogueKeys.Field_HTCVCatalogue_ParentId] == DBNull.Value
                ? 0
                : int.Parse(dr[HTCVCatalogueKeys.Field_HTCVCatalogue_ParentId].ToString());

            try
            {
                objBLL.FullName = dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                    ? string.Empty
                    : (string) dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME];
            }
            catch
            {
            }
            try
            {
                objBLL.PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                    ? string.Empty
                    : (string) dr[PositionKeys.FIELD_POSITION_NAME];
            }
            catch
            {
            }

            try
            {
                objBLL.DepartmentName = dr[DepartmentKeys.FIELD_DEPARTMENT_NAME] == DBNull.Value
                    ? string.Empty
                    : (string) dr[DepartmentKeys.FIELD_DEPARTMENT_NAME];
            }
            catch
            {
            }
            try
            {
                objBLL.DepartmentFullName = dr[DepartmentKeys.FIELD_DEPARTMENT_DEPARTMENTFULLNAME] == DBNull.Value
                    ? string.Empty
                    : (string) dr[DepartmentKeys.FIELD_DEPARTMENT_DEPARTMENTFULLNAME];
            }
            catch
            {
            }

            return objBLL;
        }

        #endregion
    }
}