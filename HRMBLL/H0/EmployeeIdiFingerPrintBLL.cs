using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H0;
using HRMUtil.KeyNames.H0;

namespace HRMBLL.H0
{
    public class EmployeeIdiFingerPrintBLL
    {
        #region public static Get methods

        public static List<EmployeeIdiFingerPrintBLL> GetByFilter(int userId, int fingerIndex, int rootId)
        {
            return
                GenerateListEmployeeIdiFingerPrintBLLFromDataTable(new EmployeeIdiFingerPrintDAL().GetByFilter(userId,
                    fingerIndex, rootId));
        }

        #endregion

        public static string GetFingerNameByIndex(int fingerIndex)
        {
            var valueReturn = "";
            switch (fingerIndex)
            {
                case FingerIndex0:
                    valueReturn = FingerIndex0Name;
                    break;
                case FingerIndex1:
                    valueReturn = FingerIndex1Name;
                    break;
                case FingerIndex2:
                    valueReturn = FingerIndex2Name;
                    break;
                case FingerIndex3:
                    valueReturn = FingerIndex3Name;
                    break;
                case FingerIndex4:
                    valueReturn = FingerIndex4Name;
                    break;
                case FingerIndex5:
                    valueReturn = FingerIndex5Name;
                    break;
                case FingerIndex6:
                    valueReturn = FingerIndex6Name;
                    break;
                case FingerIndex7:
                    valueReturn = FingerIndex7Name;
                    break;
                case FingerIndex8:
                    valueReturn = FingerIndex8Name;
                    break;
                case FingerIndex9:
                    valueReturn = FingerIndex9Name;
                    break;
            }
            return valueReturn;
        }

        #region private fields

        private const int FingerIndex0 = 0;
        private const string FingerIndex0Name = "Trái út";
        private const int FingerIndex1 = 1;
        private const string FingerIndex1Name = "Trái kề út";
        private const int FingerIndex2 = 2;
        private const string FingerIndex2Name = "Trái giữa";
        private const int FingerIndex3 = 3;
        private const string FingerIndex3Name = "Trái trỏ";
        private const int FingerIndex4 = 4;
        private const string FingerIndex4Name = "Trái cái";
        private const int FingerIndex5 = 5;
        private const string FingerIndex5Name = "Phải cái";
        private const int FingerIndex6 = 6;
        private const string FingerIndex6Name = "Phải trỏ";
        private const int FingerIndex7 = 7;
        private const string FingerIndex7Name = "Phải giữa";
        private const int FingerIndex8 = 8;
        private const string FingerIndex8Name = "Phải kề út";
        private const int FingerIndex9 = 9;
        private const string FingerIndex9Name = "Phải út";

        #endregion

        #region properties

        public int PK_UserFingerPrintId { get; set; }

        public int UserId { get; set; }

        public int FingerIndex { get; set; }

        public string FingerName { get; set; } = "";

        public int IndexValue { get; set; }

        public byte[] Features { get; set; }

        #endregion

        #region public methods insert, update, delete

        public long Save()
        {
            var objDAL = new EmployeeIdiFingerPrintDAL();
            if (PK_UserFingerPrintId <= 0)
                return objDAL.Insert(UserId, FingerIndex, IndexValue, Features);
            return objDAL.Update(UserId, FingerIndex, IndexValue, Features, PK_UserFingerPrintId);
        }

        public static long Delete(int pk_UserFingerPrintId)
        {
            return new EmployeeIdiFingerPrintDAL().Delete(pk_UserFingerPrintId);
        }

        public static long DeleteByUserId(int userId)
        {
            return new EmployeeIdiFingerPrintDAL().DeleteByUserId(userId);
        }

        public static long DeleteByUserIdFingerIndex(int userId, int fingerIndex)
        {
            return new EmployeeIdiFingerPrintDAL().DeleteByUserIdFingerIndex(userId, fingerIndex);
        }

        #endregion

        #region private methods

        private static List<EmployeeIdiFingerPrintBLL> GenerateListEmployeeIdiFingerPrintBLLFromDataTable(DataTable dt)
        {
            var lst = new List<EmployeeIdiFingerPrintBLL>();

            foreach (DataRow dr in dt.Rows)
                lst.Add(GenerateEmployeeIdiFingerPrintBLLFromDataRow(dr));

            return lst;
        }

        private static EmployeeIdiFingerPrintBLL GenerateEmployeeIdiFingerPrintBLLFromDataRow(DataRow dr)
        {
            var obj = new EmployeeIdiFingerPrintBLL();
            obj.PK_UserFingerPrintId = 0;
            obj.UserId = dr[EmployeeIdiFingerPrintKeys.Field_EmployeeIdiFingerPrint_UserId] == DBNull.Value
                ? 0
                : int.Parse(dr[EmployeeIdiFingerPrintKeys.Field_EmployeeIdiFingerPrint_UserId].ToString());
            obj.FingerIndex = dr[EmployeeIdiFingerPrintKeys.Field_EmployeeIdiFingerPrint_FingerIndex] == DBNull.Value
                ? 0
                : int.Parse(dr[EmployeeIdiFingerPrintKeys.Field_EmployeeIdiFingerPrint_FingerIndex].ToString());
            obj.IndexValue = dr[EmployeeIdiFingerPrintKeys.Field_EmployeeIdiFingerPrint_IndexValue] == DBNull.Value
                ? 0
                : int.Parse(dr[EmployeeIdiFingerPrintKeys.Field_EmployeeIdiFingerPrint_IndexValue].ToString());
            obj.Features = dr[EmployeeIdiFingerPrintKeys.Field_EmployeeIdiFingerPrint_Features] == DBNull.Value
                ? null
                : (byte[]) dr[EmployeeIdiFingerPrintKeys.Field_EmployeeIdiFingerPrint_Features];
            obj.FingerName = GetFingerNameByIndex(obj.FingerIndex);
            return obj;
        }

        #endregion
    }
}