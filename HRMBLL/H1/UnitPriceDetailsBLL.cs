using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H1;
using HRMUtil;
using HRMUtil.KeyNames.H0;
using HRMUtil.KeyNames.H1;

namespace HRMBLL.H1
{
    public class UnitPriceDetailsBLL
    {
        #region private fields

        #endregion

        #region properties

        public int UnitPriceDetailId { get; set; }

        public double UnitPriceLNS { get; set; }

        public double UnitPriceBonus { get; set; }

        public double UnitPriceLCB { get; set; }

        public double UnitPriceLuch { get; set; }

        public int UnitPriceDeptId { get; set; }

        public double UnitPriceLNSBalance { get; set; }

        public double UnitPriceBonusBalance { get; set; }

        public double IndexBalance { get; set; }

        public DateTime CreateDate { get; set; }

        public string Remark { get; set; }

        public int UnitPriceType { get; set; }

        public string RootName { get; set; }

        public string UnitPriceTypeName { get; set; }

        #endregion

        #region public methods insert, update, delete

        public int Save()
        {
            var objDAL = new UnitPriceDetailsDAL();

            if (UnitPriceDetailId <= 0)
                return objDAL.Insert(UnitPriceLNS, UnitPriceBonus, UnitPriceLCB, UnitPriceLuch, UnitPriceDeptId,
                    CreateDate, Remark, UnitPriceType);
            return objDAL.Update(UnitPriceLNS, UnitPriceBonus, UnitPriceLCB, UnitPriceLuch, UnitPriceDeptId, CreateDate,
                Remark, UnitPriceType, UnitPriceDetailId);
        }

        public static void Update(double unitPriceLNS, double unitPriceBonus, double unitPriceLCB, double unitPriceLuch,
            int unitPriceDeptId, DateTime createDate, string remark, int unitPriceType, int unitPriceDetailId,
            string rootName, string unitPriceTypeName, double indexBalance, double unitPriceLNSBalance,
            double unitPriceBonusBalance)
        {
            new UnitPriceDetailsDAL().Update(unitPriceLNS, unitPriceBonus, unitPriceLCB, unitPriceLuch, unitPriceDeptId,
                createDate, remark, unitPriceType, unitPriceDetailId);
        }

        public static void Update(double unitPriceLNS, double unitPriceBonus, double unitPriceLCB, double unitPriceLuch,
            int unitPriceDeptId, DateTime createDate, string remark, int unitPriceType, int unitPriceDetailId,
            string rootName, string unitPriceTypeName)
        {
            new UnitPriceDetailsDAL().Update(unitPriceLNS, unitPriceBonus, unitPriceLCB, unitPriceLuch, unitPriceDeptId,
                createDate, remark, unitPriceType, unitPriceDetailId);
        }

        public static void Delete(int unitPriceDetailId)
        {
            new UnitPriceDetailsDAL().Delete(unitPriceDetailId);
        }

        #endregion

        #region public methods GET

        public static List<UnitPriceDetailsBLL> GetByFilter(int unitPriceType, int month, int year, int unitPriceDeptId)
        {
            return
                GenerateListUnitPriceDetailsBLLFromDataTable(new UnitPriceDetailsDAL().GetByFilter(unitPriceType, month,
                    year, unitPriceDeptId));
        }

        public static List<UnitPriceDetailsBLL> GetByDate(int Month, int Year)
        {
            return GenerateListUnitPriceDetailsBLLFromDataTable(new UnitPriceDetailsDAL().GetByDate(Month, Year));
        }

        #endregion

        #region private methods

        private static List<UnitPriceDetailsBLL> GenerateListUnitPriceDetailsBLLFromDataTable(DataTable dt)
        {
            var list = new List<UnitPriceDetailsBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateUnitPriceDetailsBLLFromDataRow(dr));

            return list;
        }

        private static UnitPriceDetailsBLL GenerateUnitPriceDetailsBLLFromDataRow(DataRow dr)
        {
            var objBLL = new UnitPriceDetailsBLL();
            objBLL.UnitPriceDetailId = dr[UnitPriceDetailKeys.Field_Unit_Price_Detail_Id] == DBNull.Value
                ? 0
                : int.Parse(dr[UnitPriceDetailKeys.Field_Unit_Price_Detail_Id].ToString());
            objBLL.UnitPriceLNS = dr[UnitPriceDetailKeys.Field_Unit_Price_Detail_UnitPriceLNS] == DBNull.Value
                ? 0
                : double.Parse(dr[UnitPriceDetailKeys.Field_Unit_Price_Detail_UnitPriceLNS].ToString());
            objBLL.UnitPriceBonus = dr[UnitPriceDetailKeys.Field_Unit_Price_Detail_UnitPriceBonus] == DBNull.Value
                ? 0
                : double.Parse(dr[UnitPriceDetailKeys.Field_Unit_Price_Detail_UnitPriceBonus].ToString());
            objBLL.UnitPriceLCB = dr[UnitPriceDetailKeys.Field_Unit_Price_Detail_UnitPriceLCB] == DBNull.Value
                ? 0
                : double.Parse(dr[UnitPriceDetailKeys.Field_Unit_Price_Detail_UnitPriceLCB].ToString());
            objBLL.UnitPriceLuch = dr[UnitPriceDetailKeys.Field_Unit_Price_Detail_UnitPriceLuch] == DBNull.Value
                ? 0
                : double.Parse(dr[UnitPriceDetailKeys.Field_Unit_Price_Detail_UnitPriceLuch].ToString());
            objBLL.UnitPriceDeptId = dr[UnitPriceDetailKeys.Field_Unit_Price_Detail_UnitPriceDeptId] == DBNull.Value
                ? 0
                : int.Parse(dr[UnitPriceDetailKeys.Field_Unit_Price_Detail_UnitPriceDeptId].ToString());

            objBLL.UnitPriceLNSBalance = dr[UnitPriceDetailKeys.Field_Unit_Price_Detail_UnitPriceLNSBalance] ==
                                         DBNull.Value
                ? 0
                : double.Parse(dr[UnitPriceDetailKeys.Field_Unit_Price_Detail_UnitPriceLNSBalance].ToString());
            objBLL.UnitPriceBonusBalance = dr[UnitPriceDetailKeys.Field_Unit_Price_Detail_UnitPriceBonusBalance] ==
                                           DBNull.Value
                ? 0
                : double.Parse(dr[UnitPriceDetailKeys.Field_Unit_Price_Detail_UnitPriceBonusBalance].ToString());
            objBLL.IndexBalance = dr[UnitPriceDetailKeys.Field_Unit_Price_Detail_IndexBalance] == DBNull.Value
                ? 0
                : double.Parse(dr[UnitPriceDetailKeys.Field_Unit_Price_Detail_IndexBalance].ToString());

            objBLL.CreateDate = dr[UnitPriceDetailKeys.Field_Unit_Price_Detail_CreateDate] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr[UnitPriceDetailKeys.Field_Unit_Price_Detail_CreateDate].ToString());
            objBLL.Remark = dr[UnitPriceDetailKeys.Field_Unit_Price_Detail_Remark] == DBNull.Value
                ? string.Empty
                : dr[UnitPriceDetailKeys.Field_Unit_Price_Detail_Remark].ToString();
            objBLL.UnitPriceType = dr[UnitPriceDetailKeys.Field_Unit_Price_Detail_UnitPriceType] == DBNull.Value
                ? 0
                : int.Parse(dr[UnitPriceDetailKeys.Field_Unit_Price_Detail_UnitPriceType].ToString());
            objBLL.RootName = dr[DepartmentKeys.FIELD_DEPARTMENT_NAME] == DBNull.Value
                ? string.Empty
                : dr[DepartmentKeys.FIELD_DEPARTMENT_NAME].ToString();
            objBLL.UnitPriceTypeName = Constants.GetUnitPriceTypeName(objBLL.UnitPriceType);
            return objBLL;
        }

        #endregion
    }
}