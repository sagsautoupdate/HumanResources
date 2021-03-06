using System;
using System.Data;
using HRMDAL.H1;

namespace HRMBLL.H1
{
    public class WageFundsBLL
    {
        #region private fields

        #endregion

        #region properties

        public int WageFundId { get; set; }


        public DateTime WageFundDate { get; set; }

        public decimal DGLNS { get; set; }

        public double LNSCoefficientTotal { get; set; }

        public decimal LNSWageFund { get; set; }

        public decimal TLTTCLCB { get; set; }

        public decimal TLTTCKPN { get; set; }

        public decimal DGAnGiuaCa { get; set; }

        public decimal GTGC { get; set; }

        public decimal GTCN { get; set; }

        public bool IsOK { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        #endregion

        #region public methods insert, update, delete

        public static void Insert(DateTime WageFundDate, decimal DGLNS, double LNSCoefficientTotal, decimal LNSWageFund,
            decimal TLTTCLCB, decimal TLTTCKPN, decimal DGAnGiuaCa, decimal GTGC, decimal GTCN, bool IsOK)
        {
            new WageFundsDAL().Insert(WageFundDate, DGLNS, LNSCoefficientTotal, LNSWageFund, TLTTCLCB, TLTTCKPN,
                DGAnGiuaCa, GTGC, GTCN, IsOK);
        }

        public static void Update(DateTime WageFundDate, decimal DGLNS, double LNSCoefficientTotal, decimal LNSWageFund,
            decimal TLTTCLCB, decimal TLTTCKPN, decimal DGAnGiuaCa, decimal GTGC, decimal GTCN, bool IsOK,
            int WageFundId)
        {
            new WageFundsDAL().Update(WageFundDate, DGLNS, LNSCoefficientTotal, LNSWageFund, TLTTCLCB, TLTTCKPN,
                DGAnGiuaCa, GTGC, GTCN, IsOK, WageFundId);
        }

        //public static void UpdateByOriginalWageFund(int rootId, decimal lNSOriginalWageFund, decimal lNSShortTermWageFund, decimal lNSNoKWageFund, decimal bonusOriginalWageFund, decimal bonusShortTermWageFund, decimal bonusNoKWageFund, double finalLNSCoefficientNoKTotal, int apportionmentType, string remark, DateTime createDate, int wageFundId, string rootName)
        //{
        //    //new WageFundsDAL().UpdateByOriginalWageFund(lNSOriginalWageFund, bonusOriginalWageFund, createDate, wageFundId, (decimal)Constants.DonGiaLCB, (decimal)Constants.MAX_TIENAN, "Đơn giá được tính từ quỹ lương");

        //}
        //public static void UpdateByShortTerm(int rootId, DateTime createDate)
        //{
        //    new WageFundsDAL().UpdateByShortTerm(rootId, createDate);
        //}
        //public static void UpdateByNoKWageFund(DateTime createDate)
        //{
        //    new WageFundsDAL().UpdateByNoKWageFund(createDate);
        //}

        //public static void UpdateByTotalPeriod_II(int rootId, DateTime createDate)
        //{
        //    new WageFundsDAL().UpdateByTotalPeriod_II(rootId, createDate);
        //}

        //public static void Delete(int wageFundId)
        //{
        //    new WageFundsDAL().Delete(wageFundId);
        //}

        public static void DeleteByDate(int month, int year)
        {
            new WageFundsDAL().DeleteByDate(month, year);
        }

        #endregion

        #region public methods GET

        public static DataTable GetAll()
        {
            return new WageFundsDAL().GetAll();
        }

        public static DataTable GetByDate(int month, int year)
        {
            return new WageFundsDAL().GetByDate(month, year);
        }

        //public static List<WageFundsBLL> GetTotalLNSCoefficientByDate(DateTime createDate, bool ApportionmentType)
        //{
        //    return GenerateListWageFundTotalLNSCoefficientBLLFromDataRow(new LNSCoefficientEmployeesDAL().GetTotalByDate(createDate, (int)DefaultValues.XQDSalary(createDate.Month, createDate.Year), ApportionmentType));
        //}

        #endregion

        //#region private methods

        //private static List<WageFundsBLL> GenerateListWageFundsBLLFromDataTable(DataTable dt)
        //{
        //    List<WageFundsBLL> list = new List<WageFundsBLL>();

        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        list.Add(GenerateWageFundsBLLFromDataRow(dr));
        //    }

        //    return list;
        //}

        //private static List<WageFundsBLL> GenerateListWageFundTotalLNSCoefficientBLLFromDataRow(DataTable dt)
        //{
        //    List<WageFundsBLL> list = new List<WageFundsBLL>();

        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        list.Add(GenerateTotalLNSCoefficientBLLFromDataRow(dr));
        //    }

        //    return list;
        //}

        //private static WageFundsBLL GenerateWageFundsBLLFromDataRow(DataRow dr)
        //{

        //    WageFundsBLL objBLL = new WageFundsBLL();

        //    objBLL._WageFundId = dr[WageFundKeys.Field_Wage_Fund_ID] == DBNull.Value ? 0 : Convert.ToInt32(dr[WageFundKeys.Field_Wage_Fund_ID].ToString());
        //    objBLL._RootId = dr[WageFundKeys.Field_Wage_Fund_RootId] == DBNull.Value ? 0 : Convert.ToInt32(dr[WageFundKeys.Field_Wage_Fund_RootId].ToString());
        //    try
        //    {
        //        objBLL._RootName = dr[WageFundKeys.Field_Wage_Fund_RootName] == DBNull.Value ? string.Empty : dr[WageFundKeys.Field_Wage_Fund_RootName].ToString();
        //    }
        //    catch { }
        //    //objBLL._LNSOriginalWageFund = dr[WageFundKeys.Field_Wage_Fund_LNSOriginalWageFund] == DBNull.Value ? 0 : Convert.ToDecimal(dr[WageFundKeys.Field_Wage_Fund_LNSOriginalWageFund].ToString());
        //    //objBLL._LNSShortTermWageFund = dr[WageFundKeys.Field_Wage_Fund_LNSShortTermWageFund] == DBNull.Value ? 0 : Convert.ToDecimal(dr[WageFundKeys.Field_Wage_Fund_LNSShortTermWageFund].ToString());
        //    //objBLL._LNSNoKWageFund = dr[WageFundKeys.Field_Wage_Fund_LNSNoKWageFund] == DBNull.Value ? 0 : Convert.ToDecimal(dr[WageFundKeys.Field_Wage_Fund_LNSNoKWageFund].ToString());
        //    //objBLL._BonusOriginalWageFund = dr[WageFundKeys.Field_Wage_Fund_BonusOriginalWageFund] == DBNull.Value ? 0 : Convert.ToDecimal(dr[WageFundKeys.Field_Wage_Fund_BonusOriginalWageFund].ToString());            
        //    //objBLL._LNSCoefficientNoKTotal = dr[WageFundKeys.Field_Wage_Fund_LNSCoefficientNoKTotal] == DBNull.Value ? 0 : Convert.ToDouble(dr[WageFundKeys.Field_Wage_Fund_LNSCoefficientNoKTotal].ToString());
        //    //objBLL._LNSPCTNCoefficientNoKTotal = dr[WageFundKeys.Field_Wage_Fund_LNSPCTNCoefficientNoKTotal] == DBNull.Value ? 0 : Convert.ToDouble(dr[WageFundKeys.Field_Wage_Fund_LNSPCTNCoefficientNoKTotal].ToString());
        //    //objBLL._FinalLNSCoefficientNoKTotal = objBLL._LNSCoefficientNoKTotal + objBLL._LNSPCTNCoefficientNoKTotal;
        //    //objBLL._LNSKWageFund = dr[WageFundKeys.Field_Wage_Fund_LNSKWageFund] == DBNull.Value ? 0 : Convert.ToDecimal(dr[WageFundKeys.Field_Wage_Fund_LNSKWageFund].ToString());
        //    //objBLL._BonusKWageFund = dr[WageFundKeys.Field_Wage_Fund_BonusKWageFund] == DBNull.Value ? 0 : Convert.ToDecimal(dr[WageFundKeys.Field_Wage_Fund_BonusKWageFund].ToString());
        //    //objBLL._LNSBalanceWageFund = dr[WageFundKeys.Field_Wage_Fund_LNSBalanceWageFund] == DBNull.Value ? 0 : Convert.ToDecimal(dr[WageFundKeys.Field_Wage_Fund_LNSBalanceWageFund].ToString());
        //    //objBLL._BonusBalanceWageFund = dr[WageFundKeys.Field_Wage_Fund_BonusBalanceWageFund] == DBNull.Value ? 0 : Convert.ToDecimal(dr[WageFundKeys.Field_Wage_Fund_BonusBalanceWageFund].ToString());
        //    //objBLL._ApportionmentType = dr[WageFundKeys.Field_Wage_Fund_ApportionmentType] == DBNull.Value ? 0 : int.Parse(dr[WageFundKeys.Field_Wage_Fund_ApportionmentType].ToString());
        //    //objBLL._Remark = dr[WageFundKeys.Field_Wage_Fund_Remark] == DBNull.Value ? string.Empty : dr[WageFundKeys.Field_Wage_Fund_Remark].ToString();
        //    //objBLL._CreateDate = dr[WageFundKeys.Field_Wage_Fund_CreateDate] == DBNull.Value ? FormatDate.GetSQLDateMinValue : Convert.ToDateTime(dr[WageFundKeys.Field_Wage_Fund_CreateDate].ToString());

        //    return objBLL;
        //}

        //private static WageFundsBLL GenerateTotalLNSCoefficientBLLFromDataRow(DataRow dr)
        //{

        //    WageFundsBLL objBLL = new WageFundsBLL();

        //    objBLL._RootId = dr[WageFundKeys.Field_Wage_Fund_RootId] == DBNull.Value ? 0 : Convert.ToInt32(dr[WageFundKeys.Field_Wage_Fund_RootId].ToString());
        //    objBLL._RootName = dr[WageFundKeys.Field_Wage_Fund_RootName] == DBNull.Value ? string.Empty : dr[WageFundKeys.Field_Wage_Fund_RootName].ToString();
        //    //objBLL._LNSCoefficientNoKTotal = dr[WageFundKeys.Field_Wage_Fund_LNSCoefficientNoKTotal] == DBNull.Value ? 0 : Convert.ToDouble(dr[WageFundKeys.Field_Wage_Fund_LNSCoefficientNoKTotal].ToString());
        //    //objBLL._LNSPCTNCoefficientNoKTotal = dr[WageFundKeys.Field_Wage_Fund_LNSPCTNCoefficientNoKTotal] == DBNull.Value ? 0 : Convert.ToDouble(dr[WageFundKeys.Field_Wage_Fund_LNSPCTNCoefficientNoKTotal].ToString());
        //    objBLL._FinalLNSCoefficientNoKTotal = objBLL._LNSCoefficientNoKTotal + objBLL._LNSPCTNCoefficientNoKTotal;
        //    return objBLL;
        //}

        //#endregion
    }
}