using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H1;
using HRMUtil;
using HRMUtil.KeyNames.H1;

namespace HRMBLL.H1
{
    public class PITDeductionBLL
    {
        #region private fields

        #endregion

        #region properties

        public int PITDeductionId { get; set; }

        public int UserId { get; set; }

        public long UserRelationId { get; set; }

        public string TaxNumber { get; set; } = string.Empty;

        public string Id_Passport { get; set; } = string.Empty;

        public int FromMonth { get; set; }

        public int FromYear { get; set; }

        public int ToMonth { get; set; }

        public int ToYear { get; set; }

        public DateTime CreateDate { get; set; } = FormatDate.GetSQLDateMinValue;

        public int CreateUser { get; set; }

        public DateTime UpdateDate { get; set; } = FormatDate.GetSQLDateMinValue;

        public int UpdateUser { get; set; }

        public string DepartmentFullName { get; set; }

        public string FullName { get; set; }

        public string DepartmentCode { get; set; }

        public string RFullName { get; set; }

        public string RelationTypeName { get; set; }

        public string RootName { get; set; }

        #endregion

        #region public methods insert, update, delete

        public int Save()
        {
            var objDAL = new PITDeductionDAL();

            if (PITDeductionId <= 0)
                return objDAL.Insert(UserId, UserRelationId, TaxNumber, Id_Passport, FromMonth, FromYear, ToMonth,
                    ToYear, CreateDate, CreateUser, UpdateDate, UpdateUser);
            return objDAL.Update(UserId, UserRelationId, TaxNumber, Id_Passport, FromMonth, FromYear, ToMonth, ToYear,
                UpdateDate, UpdateUser, PITDeductionId);
        }

        public static void Update(int userId, long userRelationId, string taxNumber, string id_Passport, int fromMonth,
            int fromYear, int toMonth, int toYear, DateTime updateDate, int updateUser, int pITDeductionId)
        {
            new PITDeductionDAL().Update(userId, userRelationId, taxNumber, id_Passport, fromMonth, fromYear, toMonth,
                toYear, updateDate, updateUser, pITDeductionId);
        }

        public static void Delete(int pITDeductionId)
        {
            new PITDeductionDAL().Delete(pITDeductionId);
        }

        #endregion

        #region public methods GET

        public static List<PITDeductionBLL> GetByFilter(string fullName, int rootId)
        {
            return GenerateListPITDeductionBLLFromDataTable(new PITDeductionDAL().GetByFilter(fullName, rootId));
        }

        public static DataTable GetByDeptId(string fullName, string DepIds)
        {
            return new PITDeductionDAL().GetByDeptId(fullName, DepIds);
        }

        public static PITDeductionBLL GetByUserIdUserRelationId(int userId, long userRelationId)
        {
            var list =
                GenerateListPITDeductionBLLFromDataTable(new PITDeductionDAL().GetByUserIdUserRelationId(userId,
                    userRelationId));
            return list.Count != 1 ? null : list[0];
        }

        public static List<PITDeductionBLL> GetByUserDate(int userId, int month, int year)
        {
            return GenerateListPITDeductionBLLFromDataTable(new PITDeductionDAL().GetByUserDate(userId, month, year));
        }

        #endregion

        #region private methods

        private static List<PITDeductionBLL> GenerateListPITDeductionBLLFromDataTable(DataTable dt)
        {
            var list = new List<PITDeductionBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GeneratePITDeductionBLLFromDataRow(dr));

            return list;
        }

        private static PITDeductionBLL GeneratePITDeductionBLLFromDataRow(DataRow dr)
        {
            var objBLL = new PITDeductionBLL();

            try
            {
                objBLL.PITDeductionId = dr[PITDeductionKeys.Field_PITDeduction_Id] == DBNull.Value
                    ? 0
                    : int.Parse(dr[PITDeductionKeys.Field_PITDeduction_Id].ToString());
            }
            catch
            {
            }
            try
            {
                objBLL.UserId = dr[PITDeductionKeys.Field_PITDeduction_UserId] == DBNull.Value
                    ? 0
                    : int.Parse(dr[PITDeductionKeys.Field_PITDeduction_UserId].ToString());
            }
            catch
            {
            }
            try
            {
                objBLL.UserRelationId = dr[PITDeductionKeys.Field_PITDeduction_UserRelationId] == DBNull.Value
                    ? 0
                    : long.Parse(dr[PITDeductionKeys.Field_PITDeduction_UserRelationId].ToString());
            }
            catch
            {
            }
            try
            {
                objBLL.TaxNumber = dr[PITDeductionKeys.Field_PITDeduction_TaxNumber] == DBNull.Value
                    ? string.Empty
                    : dr[PITDeductionKeys.Field_PITDeduction_TaxNumber].ToString();
            }
            catch
            {
            }
            try
            {
                objBLL.Id_Passport = dr[PITDeductionKeys.Field_PITDeduction_Id_Passport] == DBNull.Value
                    ? string.Empty
                    : dr[PITDeductionKeys.Field_PITDeduction_Id_Passport].ToString();
            }
            catch
            {
            }
            try
            {
                objBLL.FromMonth = dr[PITDeductionKeys.Field_PITDeduction_FromMonth] == DBNull.Value
                    ? 0
                    : int.Parse(dr[PITDeductionKeys.Field_PITDeduction_FromMonth].ToString());
            }
            catch
            {
            }
            try
            {
                objBLL.FromYear = dr[PITDeductionKeys.Field_PITDeduction_FromYear] == DBNull.Value
                    ? 0
                    : int.Parse(dr[PITDeductionKeys.Field_PITDeduction_FromYear].ToString());
            }
            catch
            {
            }
            try
            {
                objBLL.ToMonth = dr[PITDeductionKeys.Field_PITDeduction_ToMonth] == DBNull.Value
                    ? 0
                    : int.Parse(dr[PITDeductionKeys.Field_PITDeduction_ToMonth].ToString());
            }
            catch
            {
            }
            try
            {
                objBLL.ToYear = dr[PITDeductionKeys.Field_PITDeduction_ToYear] == DBNull.Value
                    ? 0
                    : int.Parse(dr[PITDeductionKeys.Field_PITDeduction_ToYear].ToString());
            }
            catch
            {
            }
            try
            {
                objBLL.CreateDate = dr[PITDeductionKeys.Field_PITDeduction_CreateDate] == DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(dr[PITDeductionKeys.Field_PITDeduction_CreateDate].ToString());
            }
            catch
            {
            }
            try
            {
                objBLL.CreateUser = dr[PITDeductionKeys.Field_PITDeduction_CreateUser] == DBNull.Value
                    ? 0
                    : int.Parse(dr[PITDeductionKeys.Field_PITDeduction_CreateUser].ToString());
            }
            catch
            {
            }
            try
            {
                objBLL.UpdateDate = dr[PITDeductionKeys.Field_PITDeduction_UpdateDate] == DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(dr[PITDeductionKeys.Field_PITDeduction_UpdateUser].ToString());
            }
            catch
            {
            }
            try
            {
                objBLL.UpdateUser = dr[PITDeductionKeys.Field_PITDeduction_UpdateUser] == DBNull.Value
                    ? 0
                    : int.Parse(dr[PITDeductionKeys.Field_PITDeduction_UpdateUser].ToString());
            }
            catch
            {
            }
            try
            {
                objBLL.DepartmentFullName = dr[PITDeductionKeys.Field_PITDeduction_DepartmentFullName] == DBNull.Value
                    ? string.Empty
                    : dr[PITDeductionKeys.Field_PITDeduction_DepartmentFullName].ToString();
            }
            catch
            {
            }
            try
            {
                objBLL.FullName = dr[PITDeductionKeys.Field_PITDeduction_FullName] == DBNull.Value
                    ? string.Empty
                    : dr[PITDeductionKeys.Field_PITDeduction_FullName].ToString();
            }
            catch
            {
            }
            try
            {
                objBLL.DepartmentCode = dr[PITDeductionKeys.Field_PITDeduction_DepartmentCode] == DBNull.Value
                    ? string.Empty
                    : dr[PITDeductionKeys.Field_PITDeduction_DepartmentCode].ToString();
            }
            catch
            {
            }
            try
            {
                objBLL.RFullName = dr[PITDeductionKeys.Field_PITDeduction_RFullName] == DBNull.Value
                    ? string.Empty
                    : dr[PITDeductionKeys.Field_PITDeduction_RFullName].ToString();
            }
            catch
            {
            }
            try
            {
                objBLL.RelationTypeName = dr[PITDeductionKeys.Field_PITDeduction_RelationTypeName] == DBNull.Value
                    ? string.Empty
                    : dr[PITDeductionKeys.Field_PITDeduction_RelationTypeName].ToString();
            }
            catch
            {
            }
            try
            {
                objBLL.RootName = dr[PITDeductionKeys.Field_PITDeduction_RootName] == DBNull.Value
                    ? string.Empty
                    : dr[PITDeductionKeys.Field_PITDeduction_RootName].ToString();
            }
            catch
            {
            }

            return objBLL;
        }

        #endregion
    }
}