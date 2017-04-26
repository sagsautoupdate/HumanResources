using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H1;
using HRMUtil;
using HRMUtil.KeyNames.H0;
using HRMUtil.KeyNames.H1;

namespace HRMBLL.H1
{
    public class IncomeEmployeesBLL
    {
        //#region methods get
        public static DataTable GetDataTableByFilter(DateTime DataDate, int DataType, int isVCQLNN, string DepartmentIds)
        {
            return new IncomeEmployeesDAL().GetByFilter(DataDate, DataType, isVCQLNN, DepartmentIds);
        }

        public static List<IncomeEmployeesBLL> GetByDataDate(DateTime dataDate, int isVCQLNN, string departments)
        {
            return
                GenerateListIncomeEmployeesBLLFromDataTable(new IncomeEmployeesDAL().GetByDataDate(dataDate, isVCQLNN,
                    departments));
        }

        public static DataTable GetDataTableByDataDate(DateTime dataDate, int isVCQLNN, string departments)
        {
            return new IncomeEmployeesDAL().GetByDataDate(dataDate, isVCQLNN, departments);
        }

        /// <summary>
        ///     Author: Giang
        ///     Date: 20-Oct-14
        ///     Content: Lay thong tin luong cua 1 nhan vien tra ve DT
        /// </summary>
        /// <param name="DataDate"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public static DataTable GetDataTableByUserId(DateTime dataDate, int UserId)
        {
            return new IncomeEmployeesDAL().GetByUserId(dataDate, UserId);
        }

        #region private fields

        public static int DataType_ImportSalary = 2; ////


        //private decimal _RealIncome;

        private string _EmployeeCode;
        //private string _UserName;
        private string _FullName;
        //private int _PositionId;
        //private string _PositionName;
        //private int _DepartmentId;
        //private string _DepartmentName;
        //private string _DepartmentFullName;

        //private decimal _TotalLNS;
        //private decimal _TotalLCBNN;
        //private decimal _TotalPCCV;
        //private decimal _TotalPCTN;
        //private decimal _TotalPCDH;
        //private decimal _TotalTCBHXH;
        //private decimal _TotalTCOm;
        //private decimal _TotalTCTS1Lan;
        //private decimal _TotalTienAn;
        //private decimal _TotalBoSungLuong;
        //private decimal _TotalTienThemGio;
        //private decimal _TotalTienLamDem;
        //private decimal _TotalTienThuong;
        //private decimal _TotalTrBHXH;
        //private decimal _TotalTrBHYT;
        //private decimal _TotalTrBHTN;
        //private decimal _TotalTrDoanPhi;
        //private decimal _TotalThueThuNhap;
        //private decimal _TotalTotalIncome;
        //private decimal _TotalTotalIncomeForTax;
        //private decimal _TotalTotalShortTerm;
        //private decimal _TotalTotalPeriod_I;
        //private decimal _TotalTotalPeriod_II;
        //private decimal _TotalLNSBalance;
        //private decimal _TotalBonusBalance;
        //private decimal _TotalTotalContributions;
        //private decimal _TotalRealIncome;

        #endregion

        #region properties

        public long IncomeEmployeesId { get; set; }

        public DateTime DataDate { get; set; }

        public int UserId { get; set; }

        public decimal LNS { get; set; }

        public decimal LCB { get; set; }

        public decimal PCCV { get; set; }

        public decimal PCTN { get; set; }

        public decimal TienAnGiuaCa { get; set; }

        public decimal BoSungLuong { get; set; }

        public decimal TienThemGio { get; set; }

        public decimal TienLamDem { get; set; }

        public decimal TotalIncome { get; set; }

        public decimal TotalIncomeForTax { get; set; }

        public decimal BHXH { get; set; }

        public decimal BHYT { get; set; }

        public decimal BHTN { get; set; }

        public decimal DoanPhiCD { get; set; }

        public decimal ThueThuNhap { get; set; }

        public decimal ThucLinh { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreateUserId { get; set; }

        public DateTime UpdateDate { get; set; }

        public int UpdateUserId { get; set; }

        public bool Lock { get; set; }

        public int DataType { get; set; }

        public string Remark { get; set; }


        //public decimal TotalContributions
        //{
        //    get { return _TotalContributions; }
        //    set { _TotalContributions = value; }
        //}


        //public string UserName
        //{
        //    get
        //    {
        //        if (String.IsNullOrEmpty(_UserName))
        //            return string.Empty;
        //        else
        //            return _UserName;
        //    }
        //    set { _UserName = value; }
        //}

        public string EmployeeCode
        {
            get
            {
                if (string.IsNullOrEmpty(_EmployeeCode))
                    return string.Empty;
                return _EmployeeCode;
            }
            set { _EmployeeCode = value; }
        }

        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(_FullName))
                    return string.Empty;
                return _FullName;
            }
            set { _FullName = value; }
        }

        //public int DepartmentId
        //{
        //    get { return _DepartmentId; }
        //    set { _DepartmentId = value; }
        //}

        //public string DepartmentName
        //{
        //    get { return _DepartmentName; }
        //    set { _DepartmentName = value; }
        //}
        //public string DepartmentFullName
        //{
        //    get { return _DepartmentFullName; }
        //    set { _DepartmentFullName = value; }
        //}

        public int RootId { get; set; }

        public string RootName { get; set; }

        //public int PositionId
        //{
        //    get { return _PositionId; }
        //    set { _PositionId = value; }
        //}

        //public string PositionName
        //{
        //    get { return _PositionName; }
        //    set { _PositionName = value; }
        //}


        //public decimal TotalLNS
        //{
        //    get { return _TotalLNS; }
        //    set { _TotalLNS = value; }
        //}

        //public decimal TotalLCBNN
        //{
        //    get { return _TotalLCBNN; }
        //    set { _TotalLCBNN = value; }
        //}

        //public decimal TotalPCCV
        //{
        //    get { return _TotalPCCV; }
        //    set { _TotalPCCV = value; }
        //}

        //public decimal TotalPCTN
        //{
        //    get { return _TotalPCTN; }
        //    set { _TotalPCTN = value; }
        //}

        //public decimal TotalPCDH
        //{
        //    get { return _TotalPCDH; }
        //    set { _TotalPCDH = value; }
        //}

        //public decimal TotalTCBHXH
        //{
        //    get { return _TotalTCBHXH; }
        //    set { _TotalTCBHXH = value; }
        //}

        //public decimal TotalTienAn
        //{
        //    get { return _TotalTienAn; }
        //    set { _TotalTienAn = value; }
        //}

        //public decimal TotalBoSungLuong
        //{
        //    get { return _TotalBoSungLuong; }
        //    set { _TotalBoSungLuong = value; }
        //}

        //public decimal TotalTienThemGio
        //{
        //    get { return _TotalTienThemGio; }
        //    set { _TotalTienThemGio = value; }
        //}

        //public decimal TotalTienLamDem
        //{
        //    get { return _TotalTienLamDem; }
        //    set { _TotalTienLamDem = value; }
        //}

        //public decimal TotalTienThuong
        //{
        //    get { return _TotalTienThuong; }
        //    set { _TotalTienThuong = value; }
        //}

        //public decimal TotalTrBHXH
        //{
        //    get { return _TotalTrBHXH; }
        //    set { _TotalTrBHXH = value; }
        //}

        //public decimal TotalTrBHYT
        //{
        //    get { return _TotalTrBHYT; }
        //    set { _TotalTrBHYT = value; }
        //}
        //public decimal TotalTrBHTN
        //{
        //    get { return _TotalTrBHTN; }
        //    set { _TotalTrBHTN = value; }
        //}
        //public decimal TotalTrDoanPhi
        //{
        //    get { return _TotalTrDoanPhi; }
        //    set { _TotalTrDoanPhi = value; }
        //}

        //public decimal TotalThueThuNhap
        //{
        //    get { return _TotalThueThuNhap; }
        //    set { _TotalThueThuNhap = value; }
        //}

        //public decimal TotalTotalIncome
        //{
        //    get { return _TotalTotalIncome; }
        //    set { _TotalTotalIncome = value; }
        //}

        //public decimal TotalTotalIncomeForTax
        //{
        //    get { return _TotalTotalIncomeForTax; }
        //    set { _TotalTotalIncomeForTax = value; }
        //}

        //public decimal TotalTotalShortTerm
        //{
        //    get { return _TotalTotalShortTerm; }
        //    set { _TotalTotalShortTerm = value; }
        //}

        //public decimal TotalTotalPeriod_I
        //{
        //    get { return _TotalTotalPeriod_I; }
        //    set { _TotalTotalPeriod_I = value; }
        //}

        //public decimal TotalTotalPeriod_II
        //{
        //    get { return _TotalTotalPeriod_II; }
        //    set { _TotalTotalPeriod_II = value; }
        //}

        //public decimal TotalLNSBalance
        //{
        //    get { return _TotalLNSBalance; }
        //    set { _TotalLNSBalance = value; }
        //}

        //public decimal TotalBonusBalance
        //{
        //    get { return _TotalBonusBalance; }
        //    set { _TotalBonusBalance = value; }
        //}

        //public decimal TotalTotalContributions
        //{
        //    get { return _TotalTotalContributions; }
        //    set { _TotalTotalContributions = value; }
        //}

        //public decimal TotalRealIncome
        //{
        //    get { return _TotalRealIncome; }
        //    set { _TotalRealIncome = value; }
        //}

        #endregion

        #region methods insert, update, delete

        public static long Insert(DateTime DataDate, int UserId, decimal LNS, decimal DGGC_LCB, decimal LCB,
            decimal PCCV, decimal PCTN,
            decimal TienAnGiuaCa, decimal BoSungLuong, decimal TienThemGio_BNgay, decimal TienThemGio_BNgayChiuThue,
            decimal TienThemGio_BDem,
            decimal TienThemGio_BDemChiuThue, decimal TienThemGio, decimal TienLamDem, decimal TotalIncome,
            decimal TotalIncomeForTax,
            decimal BHXH, decimal BHYT, decimal BHTN, decimal DoanPhiCD, decimal LuongChiuThue, decimal ThueThuNhap,
            decimal TruocThucLinh,
            decimal DongGop, decimal ThucLinh, DateTime CreateDate, int CreateUserId, int DataType, string Remark,
            int IsVCQLNC)
        {
            return new IncomeEmployeesDAL().Insert(DataDate, UserId, LNS, DGGC_LCB, LCB, PCCV, PCTN, TienAnGiuaCa,
                BoSungLuong, TienThemGio_BNgay, TienThemGio_BNgayChiuThue, TienThemGio_BDem,
                TienThemGio_BDemChiuThue, TienThemGio, TienLamDem, TotalIncome, TotalIncomeForTax,
                BHXH, BHYT, BHTN, DoanPhiCD, LuongChiuThue, ThueThuNhap, TruocThucLinh,
                DongGop, ThucLinh, CreateDate, CreateUserId, DataType, Remark, IsVCQLNC);
        }

        public static long Update(DateTime DataDate, int UserId, decimal LNS, decimal DGGC_LCB, decimal LCB,
            decimal PCCV, decimal PCTN,
            decimal TienAnGiuaCa, decimal BoSungLuong, decimal TienThemGio_BNgay, decimal TienThemGio_BNgayChiuThue,
            decimal TienThemGio_BDem,
            decimal TienThemGio_BDemChiuThue, decimal TienThemGio, decimal TienLamDem, decimal TotalIncome,
            decimal TotalIncomeForTax,
            decimal BHXH, decimal BHYT, decimal BHTN, decimal DoanPhiCD, decimal LuongChiuThue, decimal ThueThuNhap,
            decimal TruocThucLinh,
            decimal DongGop, decimal ThucLinh, DateTime CreateDate, int CreateUserId, DateTime UpdateDate,
            int UpdateUserId, bool Lock,
            int DataType, string Remark, int IsVCQLNC)
        {
            return new IncomeEmployeesDAL().Update(DataDate, UserId, LNS, DGGC_LCB, LCB, PCCV, PCTN, TienAnGiuaCa,
                BoSungLuong, TienThemGio_BNgay, TienThemGio_BNgayChiuThue, TienThemGio_BDem,
                TienThemGio_BDemChiuThue, TienThemGio, TienLamDem, TotalIncome, TotalIncomeForTax,
                BHXH, BHYT, BHTN, DoanPhiCD, LuongChiuThue, ThueThuNhap, TruocThucLinh,
                DongGop, ThucLinh, UpdateDate, UpdateUserId, DataType, Remark, IsVCQLNC);
        }


        public static long ImportFromExcelACV(DateTime DataDate, string ACVId,
            decimal LNS, decimal LCB, decimal PCCV, decimal PCTN, decimal TienAnGiuaCa, decimal BoSungLuong,
            decimal TienThemGio, decimal TienLamDem,
            decimal TotalIncome, decimal TotalIncomeForTax,
            decimal BHXH, decimal BHYT, decimal BHTN, decimal DoanPhiCD, decimal ThueThuNhap,
            decimal TruocThucLinh, decimal DongGop, decimal Luong, decimal TNTHieuQuaCongViec, decimal ThucLinh,
            DateTime CreateDate, int CreateUserId, DateTime UpdateDate, int UpdateUserId,
            bool Lock, int DataType, string Remark, int UserId)
        {
            return new IncomeEmployeesDAL().ImportFromExcelACV(DataDate, ACVId,
                LNS, LCB, PCCV, PCTN, TienAnGiuaCa, BoSungLuong, TienThemGio, TienLamDem,
                TotalIncome, TotalIncomeForTax,
                BHXH, BHYT, BHTN, DoanPhiCD, ThueThuNhap,
                TruocThucLinh, DongGop, Luong, TNTHieuQuaCongViec, ThucLinh,
                CreateDate, CreateUserId, UpdateDate, UpdateUserId,
                Lock, DataType, Remark, UserId);
        }

        #endregion

        //public static List<IncomesBLL> GetByFilter1(int rootId, int month, int year, int type)
        //{
        //    List<IncomesBLL> list = GenerateListIncomesBLLFromDataTable(new IncomeEmployeesDAL().GetByFilter1(rootId, month, year, type));
        //    return list;
        //}

        //public static IncomesBLL GetByUserIdAndDate(int userId, int month, int year)
        //{
        //    List<IncomesBLL> list = GenerateListIncomesBLLFromDataTable(new IncomeEmployeesDAL().GetByUserIdAndDate(userId, month, year));
        //    if (list.Count == 1)
        //    {
        //        return list[0];
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //public static List<IncomesBLL> GetByUserIdAndDate1(int userId, int month, int year)
        //{
        //    return GenerateListIncomesBLLFromDataTable(new IncomeEmployeesDAL().GetByUserIdAndDate(userId, month, year));
        //}

        //public static IncomesBLL GetByRootDateForTotal(int rootId, int month, int year)
        //{
        //    List<IncomesBLL> list = GenerateListIncomesBLLFromDataTableForTotal(new IncomeEmployeesDAL().GetByRootDateForTotal(rootId, month, year));

        //    return list.Count == 1 ? list[0] : null;
        //}

        //#endregion

        #region private methods

        private static List<IncomeEmployeesBLL> GenerateListIncomeEmployeesBLLFromDataTable(DataTable dt)
        {
            var list = new List<IncomeEmployeesBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateIncomeEmployeesBLLFromDataRow(dr));

            return list;
        }

        private static IncomeEmployeesBLL GenerateIncomeEmployeesBLLFromDataRow(DataRow dr)
        {
            var objBLL = new IncomeEmployeesBLL();

            //#region infor for employees


            try
            {
                objBLL.EmployeeCode = dr[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE].ToString();
            }
            catch
            {
            }
            //try
            //{
            //    objBLL.UserName = dr[EmployeeKeys.FIELD_EMPLOYEES_USERNAME] == DBNull.Value ? string.Empty : dr[EmployeeKeys.FIELD_EMPLOYEES_USERNAME].ToString();
            //}
            //catch { }
            try
            {
                objBLL.FullName = dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();
            }
            catch
            {
            }
            //try
            //{
            //    objBLL.PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value ? string.Empty : dr[PositionKeys.FIELD_POSITION_NAME].ToString();
            //}
            //catch { }
            //try
            //{
            //    objBLL.DepartmentName = dr[DepartmentKeys.FIELD_DEPARTMENT_NAME] == DBNull.Value ? string.Empty : dr[DepartmentKeys.FIELD_DEPARTMENT_NAME].ToString();
            //}
            //catch { }
            //try
            //{
            //    objBLL.DepartmentFullName = dr[DepartmentKeys.FIELD_DEPARTMENT_DEPARTMENTFULLNAME] == DBNull.Value ? string.Empty : dr[DepartmentKeys.FIELD_DEPARTMENT_DEPARTMENTFULLNAME].ToString();
            //}
            //catch { }
            try
            {
                objBLL.RootId = dr[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID] == DBNull.Value
                    ? 0
                    : Convert.ToInt32(dr[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID].ToString());
            }
            catch
            {
            }
            try
            {
                objBLL.RootName = dr[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME].ToString();
            }
            catch
            {
            }

            //#endregion

            objBLL.IncomeEmployeesId = dr[IncomeEmployeesKeys.Field_IncomeEmployees_IncomeEmployeesId] == DBNull.Value
                ? 0
                : long.Parse(dr[IncomeEmployeesKeys.Field_IncomeEmployees_IncomeEmployeesId].ToString());
            objBLL.UserId = dr[IncomeEmployeesKeys.Field_IncomeEmployees_UserId] == DBNull.Value
                ? 0
                : Convert.ToInt32(dr[IncomeEmployeesKeys.Field_IncomeEmployees_UserId].ToString());
            objBLL.DataDate = dr[IncomeEmployeesKeys.Field_IncomeEmployees_DataDate] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr[IncomeEmployeesKeys.Field_IncomeEmployees_DataDate].ToString());

            objBLL.LNS = dr[IncomeEmployeesKeys.Field_IncomeEmployees_LNS] == DBNull.Value
                ? 0
                : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_LNS].ToString());
            objBLL.LCB = dr[IncomeEmployeesKeys.Field_IncomeEmployees_LCB] == DBNull.Value
                ? 0
                : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_LCB].ToString());
            objBLL.PCCV = dr[IncomeEmployeesKeys.Field_IncomeEmployees_PCCV] == DBNull.Value
                ? 0
                : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_PCCV].ToString());
            objBLL.PCTN = dr[IncomeEmployeesKeys.Field_IncomeEmployees_PCTN] == DBNull.Value
                ? 0
                : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_PCTN].ToString());
            objBLL.TienAnGiuaCa = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TienAnGiuaCa] == DBNull.Value
                ? 0
                : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TienAnGiuaCa].ToString());
            objBLL.BoSungLuong = dr[IncomeEmployeesKeys.Field_IncomeEmployees_BoSungLuong] == DBNull.Value
                ? 0
                : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_BoSungLuong].ToString());
            objBLL.TienThemGio = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TienThemGio] == DBNull.Value
                ? 0
                : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TienThemGio].ToString());
            objBLL.TienLamDem = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TienLamDem] == DBNull.Value
                ? 0
                : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TienLamDem].ToString());

            objBLL.TotalIncome = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalIncome] == DBNull.Value
                ? 0
                : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalIncome].ToString());
            objBLL.TotalIncomeForTax = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalIncomeForTax] == DBNull.Value
                ? 0
                : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalIncomeForTax].ToString());

            objBLL.BHXH = dr[IncomeEmployeesKeys.Field_IncomeEmployees_BHXH] == DBNull.Value
                ? 0
                : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_BHXH].ToString());
            objBLL.BHYT = dr[IncomeEmployeesKeys.Field_IncomeEmployees_BHYT] == DBNull.Value
                ? 0
                : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_BHYT].ToString());
            objBLL.BHTN = dr[IncomeEmployeesKeys.Field_IncomeEmployees_BHTN] == DBNull.Value
                ? 0
                : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_BHTN].ToString());
            objBLL.DoanPhiCD = dr[IncomeEmployeesKeys.Field_IncomeEmployees_DoanPhiCD] == DBNull.Value
                ? 0
                : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_DoanPhiCD].ToString());
            objBLL.ThueThuNhap = dr[IncomeEmployeesKeys.Field_IncomeEmployees_ThueThuNhap] == DBNull.Value
                ? 0
                : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_ThueThuNhap].ToString());


            //objBLL.TotalContributions = objBLL.TrBHXH + objBLL.TrBHYT + objBLL.TrDoanPhi + objBLL.ThueThuNhap;
            //objBLL.RealIncome = objBLL.TotalIncome - (objBLL.TotalContributions);


            objBLL.CreateDate = dr[IncomeEmployeesKeys.Field_IncomeEmployees_CreateDate] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr[IncomeEmployeesKeys.Field_IncomeEmployees_CreateDate].ToString());
            objBLL.CreateUserId = dr[IncomeEmployeesKeys.Field_IncomeEmployees_CreateUserId] == DBNull.Value
                ? 0
                : int.Parse(dr[IncomeEmployeesKeys.Field_IncomeEmployees_CreateUserId].ToString());
            objBLL.UpdateDate = dr[IncomeEmployeesKeys.Field_IncomeEmployees_UpdateDate] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr[IncomeEmployeesKeys.Field_IncomeEmployees_UpdateDate].ToString());
            objBLL.UpdateUserId = dr[IncomeEmployeesKeys.Field_IncomeEmployees_UpdateUserId] == DBNull.Value
                ? 0
                : int.Parse(dr[IncomeEmployeesKeys.Field_IncomeEmployees_UpdateUserId].ToString());

            objBLL.Lock = dr[IncomeEmployeesKeys.Field_IncomeEmployees_Lock] == DBNull.Value
                ? false
                : Convert.ToBoolean(dr[IncomeEmployeesKeys.Field_IncomeEmployees_Lock].ToString());
            objBLL.DataType = dr[IncomeEmployeesKeys.Field_IncomeEmployees_DataType] == DBNull.Value
                ? 0
                : int.Parse(dr[IncomeEmployeesKeys.Field_IncomeEmployees_DataType].ToString());
            objBLL.Remark = dr[IncomeEmployeesKeys.Field_IncomeEmployees_Remark] == DBNull.Value
                ? string.Empty
                : dr[IncomeEmployeesKeys.Field_IncomeEmployees_Remark].ToString();

            return objBLL;
        }

        //private static List<IncomesBLL> GenerateListIncomesBLLFromDataTableForTotal(DataTable dt)
        //{
        //    List<IncomesBLL> list = new List<IncomesBLL>();

        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        list.Add(GenerateIncomesBLLFromDataRowForTotal(dr));
        //    }

        //    return list;
        //}


        //private static IncomesBLL GenerateIncomesBLLFromDataRowForTotal(DataRow dr)
        //{
        //    IncomesBLL objBLL = new IncomesBLL();


        //    objBLL.TotalLNS = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalLNS] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalLNS].ToString());
        //    objBLL.TotalLCBNN = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalLCBNN] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalLCBNN].ToString());
        //    objBLL.TotalPCCV = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalPCCV] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalPCCV].ToString());
        //    objBLL.TotalPCTN = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalPCTN] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalPCTN].ToString());
        //    objBLL.TotalPCDH = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalPCDH] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalPCDH].ToString());

        //    objBLL.TotalTCBHXH = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTCBHXH] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTCBHXH].ToString());
        //    objBLL.TotalTCOm = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTCOm] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTCOm].ToString());
        //    objBLL.TotalTCTS1Lan = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTCTS1Lan] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTCTS1Lan].ToString());

        //    objBLL.TotalTienAn = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTienAn] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTienAn].ToString());
        //    objBLL.TotalBoSungLuong = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalBoSungLuong] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalBoSungLuong].ToString());

        //    objBLL.TotalTienThemGio = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTienThemGio] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTienThemGio].ToString());
        //    objBLL.TotalTienLamDem = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTienLamDem] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTienLamDem].ToString());
        //    objBLL.TotalTienThuong = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTienThuong] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTienThuong].ToString());

        //    objBLL.TotalTrBHXH = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTrBHXH] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTrBHXH].ToString());
        //    objBLL.TotalTrBHYT = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTrBHYT] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTrBHYT].ToString());
        //    objBLL.TotalTrBHTN = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTrBHTN] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTrBHTN].ToString());
        //    objBLL.TotalTrDoanPhi = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTrDoanPhi] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTrDoanPhi].ToString());
        //    objBLL.TotalThueThuNhap = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalThueThuNhap] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalThueThuNhap].ToString());

        //    objBLL.TotalTotalIncome = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTotalIncome] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTotalIncome].ToString());
        //    objBLL.TotalTotalIncomeForTax = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTotalIncomeForTax] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTotalIncomeForTax].ToString());

        //    //objBLL.TotalTotalContributions = objBLL.TrBHXH + objBLL.TrBHYT + objBLL.TrDoanPhi + objBLL.ThueThuNhap;

        //    objBLL.TotalTotalShortTerm = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTotalShortTerm] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTotalShortTerm].ToString());
        //    objBLL.TotalTotalPeriod_I = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTotalPeriod_I] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTotalPeriod_I].ToString());
        //    objBLL.TotalTotalPeriod_II = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTotalPeriod_II] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalTotalPeriod_II].ToString());


        //    objBLL.TotalLNSBalance = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalLNSBalance] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalLNSBalance].ToString());
        //    objBLL.TotalBonusBalance = dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalBonusBalance] == DBNull.Value ? 0 : Convert.ToDecimal(dr[IncomeEmployeesKeys.Field_IncomeEmployees_TotalBonusBalance].ToString());


        //    return objBLL;
        //}

        #endregion
    }
}