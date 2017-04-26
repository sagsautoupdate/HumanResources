using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H1;
using HRMUtil;
using HRMUtil.KeyNames.H0;
using HRMUtil.KeyNames.H1;

namespace HRMBLL.H1
{
    public class CoefficientEmployeeFinalBLL
    {
        #region private field

        #endregion

        #region properties

        public long CoefficientEmployeeFinalId { get; set; }

        public int UserId { get; set; }

        public DateTime CreateDate { get; set; }

        public double LNS { get; set; }

        public double LNSPCTN { get; set; }

        public double LCB { get; set; }

        public double PCDH { get; set; }

        public double PCTN { get; set; }

        public double PCKV { get; set; }

        public double PCCV { get; set; }

        public double K { get; set; }

        public double Special { get; set; }

        public string Remark { get; set; }

        public DateTime UpdateDate { get; set; }

        public int CreateUserId { get; set; }

        public int UpdateUserId { get; set; }


        public string FullName { get; set; }

        public int PositionId { get; set; }

        public string PositionName { get; set; }

        public int ContractTypeId { get; set; }

        public string ContractTypeCode { get; set; }

        public string ContractTypeName { get; set; }


        public double TotalLNS { get; set; }

        public double TotalLNSPCTN { get; set; }

        public double TotalLCB { get; set; }

        public double TotalPCDH { get; set; }

        public double TotalPCTN { get; set; }

        public double TotalPCKV { get; set; }

        public double TotalPCCV { get; set; }

        #endregion

        #region public methods : insert, update, delete

        public long Save()
        {
            var objDAL = new CoefficientEmployeeFinalDAL();
            if (CoefficientEmployeeFinalId <= 0)
                return objDAL.Insert(UserId, CreateDate, LNS, LNSPCTN, LCB, PCDH, PCTN, PCKV, PCCV);
            return 0;
            //objDAL.Update(UserId, PCDH, PCTN, PCCV, PCKV, Description, ACTIVE, CREATEDATE, CoefficientEmployeeId);
        }

        public static long Insert(int userId, DateTime createDate, double lNS, double lNSPCTN, double lCB, double pCDH,
            double pCTN, double pCKV, double pCCV)
        {
            return new CoefficientEmployeeFinalDAL().Insert(userId, createDate, lNS, lNSPCTN, lCB, pCDH, pCTN, pCKV,
                pCCV);
        }

        public static long ImportFromExcelACV(string aCVId, DateTime dataDate, double lNS, double lNSPCTN, double lCB,
            double pCDH, double pCTN, double pCKV, double pCCV, double k, double dtNopThue, double nguoiPThuoc,
            string remark, DateTime createDate, DateTime updateDate, int createUserId, int updateUserId)
        {
            return new CoefficientEmployeeFinalDAL().ImportFromExcelACV(aCVId, dataDate, lNS, lNSPCTN, lCB, pCDH, pCTN,
                pCKV, pCCV, k, dtNopThue, nguoiPThuoc, remark, createDate, updateDate, createUserId, updateUserId);
        }

        public static long UpdateForLNS(int userId, DateTime createDate, double lNS, double lNSPCTN)
        {
            return new CoefficientEmployeeFinalDAL().UpdateForLNS(userId, createDate, lNS, lNSPCTN);
        }

        public static long UpdateForLCB(int userId, DateTime createDate, double lCB)
        {
            return new CoefficientEmployeeFinalDAL().UpdateForLCB(userId, createDate, lCB);
        }

        public static long UpdateForOther(int userId, DateTime createDate, double pCDH, double pCTN, double pCKV,
            double pCCV)
        {
            return new CoefficientEmployeeFinalDAL().UpdateForOther(userId, createDate, pCDH, pCTN, pCKV, pCCV);
        }

        public static long UpdateForSpecial(int userId, DateTime createDate, double lCB, double lNS, double lNSPCTN,
            double pCDH, double pCTN, double pCKV, double pCCV)
        {
            return new CoefficientEmployeeFinalDAL().UpdateForSpecial(userId, createDate, lCB, lNS, lNSPCTN, pCDH, pCTN,
                pCKV, pCCV);
        }

        #endregion

        #region public methods GET

        public static List<CoefficientEmployeeFinalBLL> GetByFilter(string fullName, int rootId, int month, int year)
        {
            return
                GenerateListCoefficientEmployeeFinalBLLFromDataTable(
                    new CoefficientEmployeeFinalDAL().GetByFilter(fullName, rootId, month, year));
        }

        public static CoefficientEmployeeFinalBLL GetByRootDateForTotal(int rootId, int month, int year)
        {
            var list =
                GenerateListCoefficientEmployeeFinalBLLFromDataTableForTotal(
                    new CoefficientEmployeeFinalDAL().GetByRootDateForTotal(rootId, month, year));
            if (list.Count == 1)
                return list[0];
            return null;
        }

        public static CoefficientEmployeeFinalBLL GetByUserDate(int userId, int month, int year)
        {
            var list =
                GenerateListCoefficientEmployeeFinalBLLFromDataTable(
                    new CoefficientEmployeeFinalDAL().GetByUserDate(userId, month, year));
            if (list.Count == 1)
                return list[0];
            return null;
        }

        public static DataTable GetCoefficientWorkdayByDataDate(DateTime dataDate)
        {
            return new CoefficientEmployeeFinalDAL().GetCoefficientWorkdayByDataDate(dataDate);
        }

        #endregion

        #region private methods

        private static List<CoefficientEmployeeFinalBLL> GenerateListCoefficientEmployeeFinalBLLFromDataTable(
            DataTable dt)
        {
            var list = new List<CoefficientEmployeeFinalBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateCoefficientEmployeeFinalBLLFromDataRow(dr));

            return list;
        }

        private static CoefficientEmployeeFinalBLL GenerateCoefficientEmployeeFinalBLLFromDataRow(DataRow dr)
        {
            var objBLL = new CoefficientEmployeeFinalBLL();

            objBLL.CoefficientEmployeeFinalId = dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_Id] ==
                                                DBNull.Value
                ? 0
                : long.Parse(dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_Id].ToString());
            objBLL.UserId = dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_UserId] == DBNull.Value
                ? 0
                : int.Parse(dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_UserId].ToString());
            objBLL.FullName = dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                ? string.Empty
                : dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();
            objBLL.CreateDate = dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_CreateDate] ==
                                DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(
                    dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_CreateDate].ToString());

            objBLL.LNS = dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_LNS] == DBNull.Value
                ? 0
                : double.Parse(dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_LNS].ToString());
            objBLL.LNSPCTN = dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_LNSPCTN] == DBNull.Value
                ? 0
                : double.Parse(dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_LNSPCTN].ToString());
            objBLL.LCB = dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_LCB] == DBNull.Value
                ? 0
                : double.Parse(dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_LCB].ToString());
            objBLL.PCDH = dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_PCDH] == DBNull.Value
                ? 0
                : double.Parse(dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_PCDH].ToString());
            objBLL.PCTN = dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_PCTN] == DBNull.Value
                ? 0
                : double.Parse(dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_PCTN].ToString());
            objBLL.PCKV = dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_PCKV] == DBNull.Value
                ? 0
                : double.Parse(dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_PCKV].ToString());
            objBLL.PCCV = dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_PCCV] == DBNull.Value
                ? 0
                : double.Parse(dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_PCCV].ToString());
            objBLL.Special = dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_Special] == DBNull.Value
                ? 0
                : int.Parse(dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_Special].ToString());
            objBLL.Remark = dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_Remark] == DBNull.Value
                ? string.Empty
                : dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_Special].ToString();

            objBLL.PositionId = dr[PositionKeys.FIELD_POSITION_ID] == DBNull.Value
                ? 0
                : int.Parse(dr[PositionKeys.FIELD_POSITION_ID].ToString());
            objBLL.PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                ? string.Empty
                : dr[PositionKeys.FIELD_POSITION_NAME].ToString();
            objBLL.ContractTypeId = dr[ContractTypeKeys.Field_ContractTypes_ContractTypeId] == DBNull.Value
                ? 0
                : int.Parse(dr[ContractTypeKeys.Field_ContractTypes_ContractTypeId].ToString());
            objBLL.ContractTypeCode = dr[ContractTypeKeys.Field_ContractTypes_ContractTypeCode] == DBNull.Value
                ? string.Empty
                : dr[ContractTypeKeys.Field_ContractTypes_ContractTypeCode].ToString();
            objBLL.ContractTypeName = dr[ContractTypeKeys.Field_ContractTypes_ContractTypeName] == DBNull.Value
                ? string.Empty
                : dr[ContractTypeKeys.Field_ContractTypes_ContractTypeName].ToString();

            return objBLL;
        }

        private static List<CoefficientEmployeeFinalBLL> GenerateListCoefficientEmployeeFinalBLLFromDataTableForTotal(
            DataTable dt)
        {
            var list = new List<CoefficientEmployeeFinalBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateCoefficientEmployeeFinalBLLFromDataRowForTotal(dr));

            return list;
        }

        private static CoefficientEmployeeFinalBLL GenerateCoefficientEmployeeFinalBLLFromDataRowForTotal(DataRow dr)
        {
            var objBLL = new CoefficientEmployeeFinalBLL();

            objBLL.TotalLNS = dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_TotalLNS] == DBNull.Value
                ? 0
                : double.Parse(dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_TotalLNS].ToString());
            objBLL.TotalLNSPCTN = dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_TotalLNSPCTN] ==
                                  DBNull.Value
                ? 0
                : double.Parse(dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_TotalLNSPCTN].ToString());
            objBLL.TotalLCB = dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_TotalLCB] == DBNull.Value
                ? 0
                : double.Parse(dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_TotalLCB].ToString());
            objBLL.TotalPCDH = dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_TotalPCDH] == DBNull.Value
                ? 0
                : double.Parse(dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_TotalPCDH].ToString());
            objBLL.TotalPCTN = dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_TotalPCTN] == DBNull.Value
                ? 0
                : double.Parse(dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_TotalPCTN].ToString());
            objBLL.TotalPCKV = dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_TotalPCKV] == DBNull.Value
                ? 0
                : double.Parse(dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_TotalPCKV].ToString());
            objBLL.TotalPCCV = dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_TotalPCCV] == DBNull.Value
                ? 0
                : double.Parse(dr[CoefficientEmployeeFinalKeys.Field_CoefficientEmployeeFinal_TotalPCCV].ToString());

            return objBLL;
        }

        #endregion
    }
}