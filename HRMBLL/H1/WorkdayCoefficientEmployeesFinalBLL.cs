using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H1;
using HRMUtil;
using HRMUtil.KeyNames.H1;

namespace HRMBLL.H1
{
    public class WorkdayCoefficientEmployeesFinalBLL
    {
        #region private fields

        private int _UpdateUserId;

        #endregion

        #region properties

        public long WorkdayEmployeeIdFinal { get; set; }


        public int UserId { get; set; }


        public DateTime DataDate { get; set; }


        public string Day1 { get; set; }


        public string Day2 { get; set; }


        public string Day3 { get; set; }


        public string Day4 { get; set; }


        public string Day5 { get; set; }


        public string Day6 { get; set; }

        public string Day7 { get; set; }


        public string Day8 { get; set; }

        public string Day9 { get; set; }

        public string Day10 { get; set; }


        public string Day11 { get; set; }

        public string Day12 { get; set; }

        public string Day13 { get; set; }

        public string Day14 { get; set; }

        public string Day15 { get; set; }

        public string Day16 { get; set; }

        public string Day17 { get; set; }

        public string Day18 { get; set; }

        public string Day19 { get; set; }

        public string Day20 { get; set; }

        public string Day21 { get; set; }

        public string Day22 { get; set; }


        public string Day23 { get; set; }

        public string Day24 { get; set; }


        public string Day25 { get; set; }

        public string Day26 { get; set; }

        public string Day27 { get; set; }

        public string Day28 { get; set; }

        public string Day29 { get; set; }

        public string Day30 { get; set; }


        public string Day31 { get; set; }

        public double NCQD { get; set; }

        public double NCDC { get; set; }

        public double X { get; set; }

        public double OmDNBHXH { get; set; }

        public double Om { get; set; }

        public double OmDN { get; set; }

        public double KHH { get; set; }

        public double Co { get; set; }

        public double TS { get; set; }

        public double ST { get; set; }

        public double Khamthai { get; set; }

        public double TNLD { get; set; }

        public double F { get; set; }

        public double Diduong { get; set; }

        public double CTac { get; set; }

        public double Fdb { get; set; }


        public double H1 { get; set; }

        public double H2 { get; set; }

        public double H3 { get; set; }

        public double H4 { get; set; }

        public double H5 { get; set; }

        public double H6 { get; set; }


        public double H7 { get; set; }

        public double DinhChiCT { get; set; }

        public double Ro { get; set; }

        public double Ko { get; set; }

        public double LamthemNTbngay { get; set; }

        public double LamthemCNbngay { get; set; }

        public double LamthemLTbngay { get; set; }

        public double LamthemNTbdem { get; set; }

        public double LamthemCNbdem { get; set; }

        public double LamthemLTbdem { get; set; }

        public double Lamdem { get; set; }

        public double HSLNS { get; set; }

        public double HSLNSPCTN { get; set; }

        public double HSLCB { get; set; }

        public double HSPCDH { get; set; }

        public double HSPCTN { get; set; }

        public double HSPCKV { get; set; }

        public double HSPCCV { get; set; }

        public double HSK { get; set; }

        public double DTNopThue { get; set; }

        public double NguoiPThuoc { get; set; }


        public DateTime CreateDate { get; set; }

        public int CreateUserId { get; set; }

        public DateTime UpdateDate { get; set; }

        public int UpdateUserId
        {
            get { return -UpdateUserId; }
            set { _UpdateUserId = value; }
        }

        public string Remark { get; set; }

        #endregion

        #region Ins, Udp, Del

        //Giang - 08/06/2015 - Insert Bang He So
        public static long CoefficientInsert(int UserId, DateTime DataDate,
            double HSLNS, double HSLNSPCTN, double HSLCB, double HSLCB_TinhBu,
            double HSPCDH, double HSPCTN, double HSPCKV, double HSPCCV,
            DateTime CreateDate, int CreateUserId,
            double HSQDNCHL, double HSTLNS)
        {
            return new WorkdayCoefficientEmployeesFinalDAL().CoefficientInsert(UserId, DataDate,
                HSLNS, HSLNSPCTN, HSLCB, HSLCB_TinhBu, HSPCDH, HSPCTN, HSPCKV, HSPCCV, CreateDate, CreateUserId,
                HSQDNCHL, HSTLNS);
        }

        public long CoefficientUpdate(int UserId, DateTime DataDate,
            double HSLNS, double HSLNSPCTN, double HSLCB, double HSLCB_TinhBu,
            double HSPCDH, double HSPCTN, double HSPCKV, double HSPCCV,
            DateTime UpdateDate, int UpdateUserId,
            double HSQDNCHL, double HSTLNS, int WorkdayCoefficientEmployeeIdFinal)
        {
            return new WorkdayCoefficientEmployeesFinalDAL().CoefficientUpdate(UserId, DataDate,
                HSLNS, HSLNSPCTN, HSLCB, HSLCB_TinhBu, HSPCDH, HSPCTN, HSPCKV, HSPCCV, UpdateDate, UpdateUserId,
                HSQDNCHL, HSTLNS, WorkdayCoefficientEmployeeIdFinal);
        }

        public static long CoefficientInsertV1(int FDataType, DateTime FDataDate, int TDataType, DateTime TDataDate,
            int UserId)
        {
            return new WorkdayCoefficientEmployeesFinalDAL().CoefficientInsertV1(FDataType, FDataDate, TDataType,
                TDataDate, UserId);
        }

        public static long UpdateWorkingDayFinal(int UserId, DateTime DataDate, string Day1, string Day2, string Day3,
            string Day4, string Day5, string Day6, string Day7, string Day8, string Day9, string Day10,
            string Day11, string Day12, string Day13, string Day14, string Day15, string Day16, string Day17,
            string Day18, string Day19, string Day20,
            string Day21, string Day22, string Day23, string Day24, string Day25, string Day26, string Day27,
            string Day28, string Day29, string Day30, string Day31,
            double LamDem, DateTime UpdateDate, int UpdateUserId,
            double OmDNBHXH, double Om, double OmDN, double KHH, double Co, double TS, double ST, double Khamthai,
            double TNLD,
            double F, double Diduong, double CTac, double Fdb, double H1, double H2, double H3, double H4, double H5,
            double H6, double H7, double DinhChiCT, double Ro, double Ko,
            double X, double NghiTuan, double NghiBu, double NghiViec, double NghiMat, double ChuaDiLam, double HSK,
            string Remark, string RemarkHRMAdmin, double NCDC)
        {
            return new WorkdayCoefficientEmployeesFinalDAL().UpdateWorkingDayFinal(UserId, DataDate,
                Day1, Day2, Day3, Day4, Day5, Day6, Day7, Day8, Day9, Day10,
                Day11, Day12, Day13, Day14, Day15, Day16, Day17, Day18, Day19, Day20,
                Day21, Day22, Day23, Day24, Day25, Day26, Day27, Day28, Day29, Day30, Day31,
                LamDem, UpdateDate, UpdateUserId, OmDNBHXH, Om, OmDN, KHH, Co, TS, ST, Khamthai, TNLD,
                F, Diduong, CTac, Fdb, H1, H2, H3, H4, H5, H6, H7, DinhChiCT, Ro, Ko, X, NghiTuan, NghiBu, NghiViec,
                NghiMat, ChuaDiLam, HSK, Remark, RemarkHRMAdmin, NCDC);
        }

        public long Insert(int UserId, DateTime DataDate,
            string Day25, string Day26, string Day27, string Day28, string Day29, string Day30, string Day31,
            double NCQD, double NCDC, DateTime CreateDate, int CreateUserId, string Remark)
        {
            return new WorkdayCoefficientEmployeesFinalDAL().Insert(UserId, DataDate,
                Day25, Day26, Day27, Day28, Day29, Day30, Day31,
                NCQD, NCDC, CreateDate, CreateUserId, Remark);
        }

        public static long UpdateWDStatus(int UserId, DateTime DataDate, int WDStatus, string CheckRemark, int Id)
        {
            return new WorkdayCoefficientEmployeesFinalDAL().UpdateWDStatus(UserId, DataDate, WDStatus, CheckRemark, Id);
        }

        public static long UpdateCoefficientStatus(int UserId, DateTime DataDate, int WDStatus)
        {
            return new WorkdayCoefficientEmployeesFinalDAL().UpdateCoefficientStatus(UserId, DataDate, WDStatus);
        }

        public long Update(int UserId, DateTime DataDate,
            string Day1, string Day2, string Day3, string Day4, string Day5, string Day6, string Day7, string Day8,
            string Day9, string Day10,
            string Day11, string Day12, string Day13, string Day14, string Day15, string Day16, string Day17,
            string Day18, string Day19, string Day20,
            string Day21, string Day22, string Day23, string Day24, string Day25, string Day26, string Day27,
            string Day28, string Day29, string Day30, string Day31,
            double NCQD, double NCDC, double X, double OmDNBHXH, double Om, double OmDN, double KHH, double Co,
            double TS, double ST, double Khamthai, double TNLD, double F, double Diduong, double CTac, double Fdb,
            double H1, double H2, double H3, double H4, double h5, double H6, double H7, double DinhChiCT, double Ro,
            double Ko,
            double LamthemNTbngay, double LamthemCNbngay, double LamthemLTbngay, double LamthemNTbdem,
            double LamthemCNbdem, double LamthemLTbdem, double Lamdem,
            double HSK, DateTime UpdateDate, int UpdateUserId, string Remark)
        {
            return new WorkdayCoefficientEmployeesFinalDAL().Update(UserId, DataDate,
                Day1, Day2, Day3, Day4, Day5, Day6, Day7, Day8, Day9, Day10,
                Day11, Day12, Day13, Day14, Day15, Day16, Day17, Day18, Day19, Day20,
                Day21, Day22, Day23, Day24, Day25, Day26, Day27, Day28, Day29, Day30, Day31,
                NCQD, NCDC, X, OmDNBHXH, Om, OmDN, KHH, Co, TS, ST, Khamthai, TNLD, F, Diduong, CTac, Fdb,
                H1, H2, H3, H4, h5, H6, H7, DinhChiCT, Ro, Ko,
                LamthemNTbngay, LamthemCNbngay, LamthemLTbngay, LamthemNTbdem, LamthemCNbdem, LamthemLTbdem, Lamdem,
                HSK, CreateDate, CreateUserId, Remark);
        }

        public static long ImportFromExcelACV(string ACVId, DateTime DataDate, string Day1, string Day2, string Day3,
            string Day4, string Day5, string Day6, string Day7, string Day8, string Day9, string Day10,
            string Day11, string Day12, string Day13, string Day14, string Day15, string Day16, string Day17,
            string Day18, string Day19, string Day20,
            string Day21, string Day22, string Day23, string Day24, string Day25, string Day26, string Day27,
            string Day28, string Day29, string Day30, string Day31,
            double NCQD, double NCDC, double X, double OmDNBHXH, double Om, double OmDN, double KHH, double Co,
            double TS, double ST, double Khamthai, double TNLD, double F, double Diduong, double CTac, double Fdb,
            double H1, double H2, double H3, double H4, double H5, double H6, double H7, double DinhChiCT, double Ro,
            double Ko, double LamthemNTbngay, double LamthemCNbngay, double LamthemLTbngay, double LamthemNTbdem,
            double LamthemCNbdem, double LamthemLTbdem, double Lamdem,
            double HSLNS, double HSLNSPCTN, double HSLCB, double HSPCDH, double HSPCTN, double HSPCKV, double HSPCCV,
            double HSK, double DTNopThue, double NguoiPThuoc,
            DateTime CreateDate, int CreateUserId, DateTime UpdateDate, int UpdateUserId, string Remark, int UserId,
            string Contract, double BSLNgayCong, double BSLHSLNS, double BSLQHSLNS, double ThuongNgayCong,
            double ThuongHSLNS, double ThuongQHSLNS, double ATHKNgayCong, double ATHKHSLNS, double ATHKQHSLNS,
            double ThangCongLeTet)
        {
            return new WorkdayCoefficientEmployeesFinalDAL().ImportFromExcelACV(ACVId, DataDate, Day1, Day2, Day3, Day4,
                Day5, Day6, Day7, Day8, Day9, Day10,
                Day11, Day12, Day13, Day14, Day15, Day16, Day17, Day18, Day19, Day20, Day21, Day22, Day23, Day24, Day25,
                Day26, Day27, Day28, Day29, Day30, Day31,
                NCQD, NCDC, X, OmDNBHXH, Om, OmDN, KHH, Co, TS, ST, Khamthai, TNLD, F, Diduong, CTac, Fdb, H1, H2, H3,
                H4, H5, H6, H7, DinhChiCT, Ro, Ko, LamthemNTbngay, LamthemCNbngay, LamthemLTbngay, LamthemNTbdem,
                LamthemCNbdem, LamthemLTbdem, Lamdem, HSLNS, HSLNSPCTN, HSLCB, HSPCDH, HSPCTN, HSPCKV, HSPCCV, HSK,
                DTNopThue, NguoiPThuoc, CreateDate, CreateUserId, UpdateDate, UpdateUserId, Remark, UserId,
                Contract, BSLNgayCong, BSLHSLNS, BSLQHSLNS, ThuongNgayCong, ThuongHSLNS, ThuongQHSLNS, ATHKNgayCong,
                ATHKHSLNS, ATHKQHSLNS, ThangCongLeTet);
        }

        public static long UpdateByCalculateConversionCoefficient(double NCHLNS, double HSQDNCHL, double HSTLNS,
            double NCHLCB, double NANGC, DateTime DataDate, DateTime UpdateDate, int UpdateUserId, string Remark,
            int UserId,
            double BSLNgayCong, double BSLHSLNS, double BSLQHSLNS, double ThuongNgayCong, double ThuongHSLNS,
            double ThuongQHSLNS, double ATHKNgayCong, double ATHKHSLNS, double ATHKQHSLNS, double ThangCongLeTet,
            double ThangCongLeTet_TV, int CountBlank_1_15, int CountBlank_16_31, int DataType)
        {
            return new WorkdayCoefficientEmployeesFinalDAL().UpdateByCalculateConversionCoefficient(NCHLNS, HSQDNCHL,
                HSTLNS, NCHLCB, NANGC, DataDate, UpdateDate, UpdateUserId, Remark, UserId, BSLNgayCong, BSLHSLNS,
                BSLQHSLNS, ThuongNgayCong, ThuongHSLNS, ThuongQHSLNS, ATHKNgayCong, ATHKHSLNS, ATHKQHSLNS,
                ThangCongLeTet, ThangCongLeTet_TV, CountBlank_1_15, CountBlank_16_31, DataType);
        }

        public static long UpdateForContract(DateTime DataDate, string contract, int UserId)
        {
            return new WorkdayCoefficientEmployeesFinalDAL().UpdateForContract(DataDate, contract, UserId);
        }

        public static long UpdateHSLNS(DateTime DataDate, double HSLNS, int UserId)
        {
            return new WorkdayCoefficientEmployeesFinalDAL().UpdateHSLNS(DataDate, HSLNS, UserId);
        }

        public static long UpdateWorkingDayFinal(int UserId, DateTime DataDate, string Day1, string Day2, string Day3,
            string Day4, string Day5, string Day6, string Day7, string Day8, string Day9, string Day10,
            string Day11, string Day12, string Day13, string Day14, string Day15, string Day16, string Day17,
            string Day18, string Day19, string Day20,
            string Day21, string Day22, string Day23, string Day24, string Day25, string Day26, string Day27,
            string Day28, string Day29, string Day30, string Day31,
            double LamDem, DateTime UpdateDate, int UpdateUserId,
            double OmDNBHXH, double Om, double OmDN, double KHH, double Co, double TS, double ST, double Khamthai,
            double TNLD,
            double F, double Diduong, double CTac, double Fdb, double H1, double H2, double H3, double H4, double H5,
            double H6, double H7, double DinhChiCT, double Ro, double Ko,
            double X, double NghiTuan, double NghiBu, double NghiViec, double NghiMat, double ChuaDiLam, double HSK,
            string Remark, string RemarkHRMAdmin)
        {
            return new WorkdayCoefficientEmployeesFinalDAL().UpdateWorkingDayFinal(UserId, DataDate,
                Day1, Day2, Day3, Day4, Day5, Day6, Day7, Day8, Day9, Day10,
                Day11, Day12, Day13, Day14, Day15, Day16, Day17, Day18, Day19, Day20,
                Day21, Day22, Day23, Day24, Day25, Day26, Day27, Day28, Day29, Day30, Day31,
                LamDem, UpdateDate, UpdateUserId, OmDNBHXH, Om, OmDN, KHH, Co, TS, ST, Khamthai, TNLD,
                F, Diduong, CTac, Fdb, H1, H2, H3, H4, H5, H6, H7, DinhChiCT, Ro, Ko, X, NghiTuan, NghiBu, NghiViec,
                NghiMat, ChuaDiLam, HSK, Remark, RemarkHRMAdmin);
        }

        #endregion

        #region GET

        public static DataTable GetByDataDate(DateTime dataDate, int IsVCQLNC, int DataType)
        {
            return new WorkdayCoefficientEmployeesFinalDAL().GetByDataDate(dataDate, IsVCQLNC, DataType);
        }

        public static DataTable GetByDataTable_For_WorkdayFinal_LNSCoefficient(DateTime dataDate, string DeptIds,
            int UserId)
        {
            return new WorkdayCoefficientEmployeesFinalDAL().GetByDataTable_For_WorkdayFinal_LNSCoefficient(dataDate,
                DeptIds, UserId);
        }

        public static DataTable GetByDataTable_For_WorkdayFinal_LNSCoefficientV1(DateTime dataDate1, DateTime dataDate2,
            string DeptIds, int UserId)
        {
            return new WorkdayCoefficientEmployeesFinalDAL().GetByDataTable_For_WorkdayFinal_LNSCoefficientV1(
                dataDate1, dataDate2, DeptIds, UserId);
        }

        public static DataTable GetByDataDateForDetail(DateTime dataDate, int IsVCQLNC, int DataType)
        {
            return new WorkdayCoefficientEmployeesFinalDAL().GetByDataDateForDetail(dataDate, IsVCQLNC, DataType);
        }

        public static DataTable GetForExport(string DeptIds, int Month, int Year)
        {
            return new WorkdayCoefficientEmployeesFinalDAL().GetForExport(DeptIds, Month, Year);
        }

        public static DataTable GetByDataDateForTotal(DateTime dataDate, int IsVCQLNC, int DataType)
        {
            return new WorkdayCoefficientEmployeesFinalDAL().GetByDataDateForTotal(dataDate, IsVCQLNC, DataType);
        }

        public static DataTable GetDataTableByDataDate(DateTime dataDate, int isVCQLNN, string departments)
        {
            return new WorkdayCoefficientEmployeesFinalDAL().GetByDataDateV1(dataDate, isVCQLNN, departments);
        }

        public static DataTable GetDataTableByDataDate_ForComparing(DateTime dataDate, int isVCQLNN, string departments)
        {
            return new WorkdayCoefficientEmployeesFinalDAL().GetDataTableByDataDate_ForComparing(dataDate, isVCQLNN,
                departments, 0);
        }

        public static DataRow GetDataRowByDataDate_ForComparing(DateTime dataDate, int isVCQLNN, string departments,
            int UserId)
        {
            var dt = new WorkdayCoefficientEmployeesFinalDAL().GetDataTableByDataDate_ForComparing(dataDate, isVCQLNN,
                departments, UserId);
            if (dt.Rows.Count == 1)
                return dt.Rows[0];
            return null;
        }

        public static DataTable GetDataForCoefficientWorkingDay(int DataMonth, int DataYear, string departments,
            int DataTyppe)
        {
            return new WorkdayCoefficientEmployeesFinalDAL().GetDataForCoefficientWorkingDay(DataMonth, DataYear,
                departments, DataTyppe);
        }

        public static DataTable GetDataTableByDataDateForWorkingDay(int DataMonth, int DataYear, int isVCQLNN,
            string departments, int WDStatus, int DataType)
        {
            return new WorkdayCoefficientEmployeesFinalDAL().GetDataTableByDataDateForWorkingDay(0, DataMonth, DataYear,
                isVCQLNN, departments, WDStatus, DataType);
        }

        public static DataTable GetDataTableByDataDateForWorkingDay_ERROR(int DataMonth, int DataYear, int isVCQLNN,
            string departments, int WDStatus, int DataType, string isError, int UserId)
        {
            return new WorkdayCoefficientEmployeesFinalDAL().GetDataTableByDataDateForWorkingDay_ERROR(UserId, DataMonth,
                DataYear, isVCQLNN, departments, WDStatus, DataType, isError);
        }

        public static DataTable GetDataTableByDataDateForWorkingDayAll(int DataMonth, int DataYear, string departments,
            int UserId, int ToMonth, int ToYear)
        {
            return new WorkdayCoefficientEmployeesFinalDAL().GetDataTableByDataDateForWorkingDayAll(UserId, DataMonth,
                DataYear, departments, ToMonth, ToYear);
        }

        public static DataRow GetDataRowByDataDateForWorkingDay(int UserId, int DataMonth, int DataYear, int isVCQLNN,
            string departments, int WDStatus, int DataType)
        {
            var dt = new WorkdayCoefficientEmployeesFinalDAL().GetDataTableByDataDateForWorkingDay(UserId, DataMonth,
                DataYear, isVCQLNN, departments, WDStatus, DataType);
            if (dt.Rows.Count == 1)
                return dt.Rows[0];
            return null;
        }

        public static DataRow GetCountNumber(int DataMonth, int DataYear, int RootId)
        {
            var dt = new WorkdayCoefficientEmployeesFinalDAL().GetCountNumber(DataMonth, DataYear, RootId);
            if (dt.Rows.Count == 1)
                return dt.Rows[0];
            return null;
        }

        public static DataTable GetDTByUserIdDataDate(DateTime dataDate, int DataType, string DepartmentId)
        {
            return new WorkdayCoefficientEmployeesFinalDAL().GetByUserIdDataDate(0, dataDate, DataType, DepartmentId);
        }

        public static DataRow GetDRByUserIdDataDate(int UserId, DateTime dataDate, int DataType, string DepartmentId)
        {
            var dt = new WorkdayCoefficientEmployeesFinalDAL().GetByUserIdDataDate(UserId, dataDate, DataType, DepartmentId);
            if (dt.Rows.Count == 1)
                return dt.Rows[0];
            return null;
        }

        public static DataTable GetByUserIdDataDateToDT(int UserId, DateTime dataDate, int DataType)
        {
            return new WorkdayCoefficientEmployeesFinalDAL().GetByUserIdDataDate(UserId, dataDate, DataType, "");
        }

        public static DataTable GetWorkdayIncome(DateTime dataDate1, DateTime dataDate2, int UserId,
            string DepartmentIds)
        {
            return new WorkdayCoefficientEmployeesFinalDAL().GetWorkdayIncome(dataDate1, dataDate2, UserId,
                DepartmentIds);
        }

        public static DataRow GetByUserIdDataDateToDR(int UserId, DateTime dataDate, int DataType)
        {
            var dt = new WorkdayCoefficientEmployeesFinalDAL().GetByUserIdDataDate(UserId, dataDate, DataType, "");
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        public static DataTable GetDataByMonthYear(int UserId, int DataMonth, int DataYear, int DataType)
        {
            return new WorkdayCoefficientEmployeesFinalDAL().GetDataByMonthYear(UserId, DataMonth, DataYear, DataType);
        }

        public static DataRow GetDRDataByMonthYear(int UserId, int DataMonth, int DataYear, int DataType)
        {
            var dt = new WorkdayCoefficientEmployeesFinalDAL().GetDataByMonthYear(UserId, DataMonth, DataYear, DataType);
            if (dt.Rows.Count == 1)
                return dt.Rows[0];
            return null;
        }

        public static DataRow GetByUserIdDataDate(int UserId, DateTime dataDate, int DataType)
        {
            var dt = new WorkdayCoefficientEmployeesFinalDAL().GetByUserIdDataDate(UserId, dataDate, DataType, "");
            if (dt.Rows.Count == 1)
                return dt.Rows[0];
            return null;
        }

        #endregion

        #region private methods

        private static List<WorkdayCoefficientEmployeesFinalBLL> GenerateListWorkdayEmployeesBLLFinalFromDataTable(
            DataTable dt)
        {
            var list = new List<WorkdayCoefficientEmployeesFinalBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateWorkdayEmployeesBLLFinalFromDataRow(dr));

            return list;
        }

        private static WorkdayCoefficientEmployeesFinalBLL GenerateWorkdayEmployeesBLLFinalFromDataRow(DataRow dr)
        {
            var objBLL = new WorkdayCoefficientEmployeesFinalBLL();

            objBLL.WorkdayEmployeeIdFinal =
                dr[
                    WorkdayCoefficientEmployeesFinalKeys
                        .Field_WorkdayCoefficientEmployeesFinal_WorkdayCoefficientEmployeeFinalId] == DBNull.Value
                    ? 0
                    : long.Parse(
                        dr[
                            WorkdayCoefficientEmployeesFinalKeys
                                .Field_WorkdayCoefficientEmployeesFinal_WorkdayCoefficientEmployeeFinalId].ToString());
            objBLL.UserId = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_UserId] ==
                            DBNull.Value
                ? 0
                : int.Parse(
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_UserId].ToString());
            objBLL.DataDate =
                dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_DataDate] == DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_DataDate]
                            .ToString());

            //try
            //{
            //    objBLL._EmployeeCode = dr[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE] == DBNull.Value ? string.Empty : dr[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE].ToString();
            //}
            //catch { }
            //objBLL._FullName = dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value ? string.Empty : dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();
            //objBLL._DepartmentId = dr[DepartmentKeys.FIELD_DEPARTMENT_ID] == DBNull.Value ? 0 : int.Parse(dr[DepartmentKeys.FIELD_DEPARTMENT_ID].ToString());
            //objBLL._DepartmentName = dr[DepartmentKeys.FIELD_DEPARTMENT_NAME] == DBNull.Value ? string.Empty : dr[DepartmentKeys.FIELD_DEPARTMENT_NAME].ToString();
            //objBLL._DepartmentFullName = dr[DepartmentKeys.FIELD_DEPARTMENT_DEPARTMENTFULLNAME] == DBNull.Value ? string.Empty : dr[DepartmentKeys.FIELD_DEPARTMENT_DEPARTMENTFULLNAME].ToString();
            //objBLL._RootId = dr[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID] == DBNull.Value ? 0 : int.Parse(dr[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID].ToString());
            //try
            //{
            //    objBLL._RootName = dr[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME] == DBNull.Value ? string.Empty : dr[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME].ToString();
            //}
            //catch { }
            //try
            //{
            //    objBLL._PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value ? string.Empty : dr[PositionKeys.FIELD_POSITION_NAME].ToString();
            //}
            //catch { }

            #region TimeKeeping

            objBLL.Day1 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day1] ==
                          DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day1].ToString();
            objBLL.Day2 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day2] ==
                          DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day2].ToString();
            objBLL.Day3 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day3] ==
                          DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day3].ToString();
            objBLL.Day4 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day4] ==
                          DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day4].ToString();
            objBLL.Day5 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day5] ==
                          DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day5].ToString();
            objBLL.Day6 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day6] ==
                          DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day6].ToString();
            objBLL.Day7 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day7] ==
                          DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day7].ToString();
            objBLL.Day8 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day8] ==
                          DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day8].ToString();
            objBLL.Day9 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day9] ==
                          DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day9].ToString();
            objBLL.Day10 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day10] ==
                           DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day10].ToString();
            objBLL.Day11 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day11] ==
                           DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day11].ToString();
            objBLL.Day12 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day12] ==
                           DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day12].ToString();
            objBLL.Day13 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day13] ==
                           DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day13].ToString();
            objBLL.Day14 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day14] ==
                           DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day14].ToString();
            objBLL.Day15 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day15] ==
                           DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day15].ToString();
            objBLL.Day16 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day16] ==
                           DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day16].ToString();
            objBLL.Day17 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day17] ==
                           DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day17].ToString();
            objBLL.Day18 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day18] ==
                           DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day18].ToString();
            objBLL.Day19 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day19] ==
                           DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day19].ToString();
            objBLL.Day20 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day20] ==
                           DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day20].ToString();
            objBLL.Day21 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day21] ==
                           DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day21].ToString();
            objBLL.Day22 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day22] ==
                           DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day22].ToString();
            objBLL.Day23 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day23] ==
                           DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day23].ToString();
            objBLL.Day24 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day24] ==
                           DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day24].ToString();
            objBLL.Day25 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day25] ==
                           DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day25].ToString();
            objBLL.Day26 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day26] ==
                           DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day26].ToString();
            objBLL.Day27 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day27] ==
                           DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day27].ToString();
            objBLL.Day28 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day28] ==
                           DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day28].ToString();
            objBLL.Day29 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day29] ==
                           DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day29].ToString();
            objBLL.Day30 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day30] ==
                           DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day30].ToString();
            objBLL.Day31 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day31] ==
                           DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Day31].ToString();

            #endregion

            #region To Collect WorkdayEmplyee

            objBLL.NCQD = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_NCQD] ==
                          DBNull.Value
                ? 0
                : double.Parse(
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_NCQD].ToString());
            objBLL.NCDC = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_NCDC] ==
                          DBNull.Value
                ? 0
                : double.Parse(
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_NCDC].ToString());
            objBLL.X = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_X] == DBNull.Value
                ? 0
                : double.Parse(
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_X].ToString());
            objBLL.OmDNBHXH =
                dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_OmDNBHXH] == DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_OmDNBHXH]
                            .ToString());
            objBLL.Om = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Om] ==
                        DBNull.Value
                ? 0
                : double.Parse(
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Om].ToString());
            objBLL.OmDN = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_OmDN] ==
                          DBNull.Value
                ? 0
                : double.Parse(
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_OmDN].ToString());
            objBLL.KHH = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_KHH] ==
                         DBNull.Value
                ? 0
                : double.Parse(
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_KHH].ToString());
            objBLL.Co = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Co] ==
                        DBNull.Value
                ? 0
                : double.Parse(
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Co].ToString());
            objBLL.TS = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_TS] ==
                        DBNull.Value
                ? 0
                : double.Parse(
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_TS].ToString());
            objBLL.ST = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_ST] ==
                        DBNull.Value
                ? 0
                : double.Parse(
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_ST].ToString());
            objBLL.Khamthai =
                dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Khamthai] == DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Khamthai]
                            .ToString());
            objBLL.TNLD = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_TNLD] ==
                          DBNull.Value
                ? 0
                : double.Parse(
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_TNLD].ToString());
            objBLL.F = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_F] == DBNull.Value
                ? 0
                : double.Parse(
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_F].ToString());
            objBLL.Diduong = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Diduong] ==
                             DBNull.Value
                ? 0
                : double.Parse(
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Diduong].ToString());
            objBLL.CTac = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_CTac] ==
                          DBNull.Value
                ? 0
                : double.Parse(
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_CTac].ToString());
            objBLL.Fdb = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Fdb] ==
                         DBNull.Value
                ? 0
                : double.Parse(
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Fdb].ToString());
            objBLL.H1 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H1] ==
                        DBNull.Value
                ? 0
                : double.Parse(
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H1].ToString());
            objBLL.H2 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H2] ==
                        DBNull.Value
                ? 0
                : double.Parse(
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H2].ToString());
            objBLL.H3 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H3] ==
                        DBNull.Value
                ? 0
                : double.Parse(
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H3].ToString());
            objBLL.H4 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H4] ==
                        DBNull.Value
                ? 0
                : double.Parse(
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H4].ToString());
            objBLL.H5 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H5] ==
                        DBNull.Value
                ? 0
                : double.Parse(
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H5].ToString());
            objBLL.H6 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H6] ==
                        DBNull.Value
                ? 0
                : double.Parse(
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H6].ToString());
            objBLL.H7 = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H7] ==
                        DBNull.Value
                ? 0
                : double.Parse(
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_H7].ToString());
            objBLL.DinhChiCT =
                dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_DinhChiCT] ==
                DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_DinhChiCT]
                            .ToString());
            objBLL.Ro = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Ro] ==
                        DBNull.Value
                ? 0
                : double.Parse(
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Ro].ToString());
            objBLL.Ko = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Ko] ==
                        DBNull.Value
                ? 0
                : double.Parse(
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Ko].ToString());
            objBLL.LamthemNTbngay =
                dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemNTbngay] ==
                DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemNTbngay]
                            .ToString());
            objBLL.LamthemCNbngay =
                dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemCNbngay] ==
                DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemCNbngay]
                            .ToString());
            objBLL.LamthemLTbngay =
                dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemLTbngay] ==
                DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemLTbngay]
                            .ToString());
            objBLL.LamthemNTbdem =
                dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemNTbdem] ==
                DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemNTbdem]
                            .ToString());
            objBLL.LamthemCNbdem =
                dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemCNbdem] ==
                DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemCNbdem]
                            .ToString());
            objBLL.LamthemLTbdem =
                dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemLTbdem] ==
                DBNull.Value
                    ? 0
                    : double.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_LamthemLTbdem]
                            .ToString());
            objBLL.Lamdem = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Lamdem] ==
                            DBNull.Value
                ? 0
                : double.Parse(
                    dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Lamdem].ToString());

            #endregion

            //objBLL._NightTimeL = dr[WorkdayEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_NightTimeL] == DBNull.Value ? 0 : double.Parse(dr[WorkdayEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_NightTimeL].ToString());
            //objBLL._MarkL = dr[WorkdayEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_MarkL] == DBNull.Value ? 0 : double.Parse(dr[WorkdayEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_MarkL].ToString());
            //objBLL._RankL = DefaultValues.HTCV(objBLL._MarkL);//dr[WorkdayEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_RankL] == DBNull.Value ? string.Empty : dr[WorkdayEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Rank].ToString();

            objBLL.CreateDate =
                dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_CreateDate] ==
                DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_CreateDate]
                            .ToString());
            objBLL.CreateUserId =
                dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_CreateUserId] ==
                DBNull.Value
                    ? 0
                    : int.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_CreateUserId]
                            .ToString());
            //objBLL._CreateUserNameL = dr[WorkdayEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_CreateUserNameL] == DBNull.Value ? "" : dr[WorkdayEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_CreateUserName].ToString();
            //objBLL._CreateFullNameL = dr[WorkdayEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_CreateFullNameL] == DBNull.Value ? "" : dr[WorkdayEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_CreateFullName].ToString();

            objBLL.UpdateDate =
                dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_UpdateDate] ==
                DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_UpdateDate]
                            .ToString());
            objBLL._UpdateUserId =
                dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_UpdateUserId] ==
                DBNull.Value
                    ? 0
                    : int.Parse(
                        dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_UpdateUserId]
                            .ToString());
            //objBLL._UpdateUserNameL = dr[WorkdayEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_UpdateUserNameL] == DBNull.Value ? "" : dr[WorkdayEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_UpdateUserName].ToString();
            //objBLL._UpdateFullNameL = dr[WorkdayEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_UpdateFullNameL] == DBNull.Value ? "" : dr[WorkdayEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_UpdateFullName].ToString();

            //objBLL._TypeL = dr[WorkdayEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_TypeL] == DBNull.Value ? 0 : int.Parse(dr[WorkdayEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_TypeL].ToString());


            //objBLL._F_O_Co_KHHDS_TNLDL = objBLL._F_OmL + objBLL._F_Con_OmL + objBLL._F_KHHDSL + objBLL._F_TNLDL;
            //objBLL._F_Nam_Fdb_DDL = objBLL._F_NamL + objBLL._F_dbL + objBLL._F_DiDuongL;
            //objBLL._F_HocL = objBLL._F_HocSAGSL + objBLL._F_Hoc1L + objBLL._F_Hoc2L + objBLL._F_Hoc3L + objBLL._F_Hoc4L + objBLL._F_Hoc5L + objBLL._F_Hoc6L + objBLL._F_Hoc7L;
            //objBLL._F_KhacL = objBLL._F_LeL + objBLL._F_KoLuongCLDL + objBLL._F_KoLuongKLDL + objBLL._ChuaDiLamL + objBLL._F_SayThaiL;

            //objBLL._NghiTuan_NghiBuL = objBLL._NghiTuanL + objBLL._NghiBuL;

            objBLL.Remark = dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Remark] ==
                            DBNull.Value
                ? string.Empty
                : dr[WorkdayCoefficientEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_Remark].ToString();

            //objBLL._StatusL = dr[WorkdayEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_StatusL] == DBNull.Value ? 0 : int.Parse(dr[WorkdayEmployeesFinalKeys.Field_WorkdayCoefficientEmployeesFinal_StatusL].ToString());

            //try
            //{
            //    objBLL._LeaveStatus = dr["LeaveStatus"] == DBNull.Value ? 0 : int.Parse(dr["LeaveStatus"].ToString());
            //}
            //catch { }
            //try
            //{
            //    objBLL._LeaveDate = dr["LeaveDate"] == DBNull.Value ? FormatDate.GetSQLDateMinValue : Convert.ToDateTime(dr["LeaveDate"].ToString());
            //}
            //catch { }


            return objBLL;
        }

        #endregion
    }
}