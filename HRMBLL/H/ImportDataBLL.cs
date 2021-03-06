using System;
using System.Data;
using HRMBLL.BLLHelper;
using HRMBLL.H0;
using HRMUtil.KeyNames.H;

namespace HRMBLL.H
{
    public class ImportDataBLL
    {
        #region Constructor

        public ImportDataBLL(string fileName, DateTime date)
        {
            FileName = fileName;
            MonthSalary = date;
        }

        #endregion

        public int Salary()
        {
            var helper = new ExcelHelper(FileName);
            var dt = helper.ReadDataFromExcelToDataTable();
            var countRowSuccessfull = 0;
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                // this value must be imported firstly
                var valueMaNhanVien = dr[MaNhanVien].ToString().Trim();
                if (valueMaNhanVien.Trim().Length > 0)
                {
                    //ImportEmployee(dr);

                    // this value must be imported after ImportEmployee method
                    ImportCoefficient(dr);
                    ImportTimeKeeping(dr);
                    ImportIncomeMonth(dr);
                    //// this value must be imported finally
                    ImportEmployeeTotalIncome(dr);
                    countRowSuccessfull++;
                }
            }

            return countRowSuccessfull;
        }

        public int SalaryKhoan()
        {
            var helper = new ExcelHelper(FileName);
            var dt = helper.ReadDataFromExcelToDataTable();
            var countRowSuccessfull = 0;
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                // this value must be imported firstly
                var valueMaNhanVien = dr[MaNhanVien].ToString().Trim();
                if (valueMaNhanVien.Trim().Length > 0)
                {
                    //ImportEmployee(dr);

                    // this value must be imported after ImportEmployee method
                    ImportCoefficient(dr);
                    ImportTimeKeeping(dr);
                    ImportIncomeMonthKhoan(dr);
                    //// this value must be imported finally
                    ImportEmployeeTotalIncome(dr);
                    countRowSuccessfull++;
                }
            }

            return countRowSuccessfull;
        }

        public int UpdateSalary()
        {
            var helper = new ExcelHelper(FileName);
            var dt = helper.ReadDataFromExcelToDataTable();
            var countRowSuccessfull = 0;
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];
                // this value must be imported firstly
                var valueMaNhanVien = dr[MaNhanVien].ToString().Trim();
                if (valueMaNhanVien.Trim().Length > 0)
                {
                    UpdateSomeField(dr);
                    countRowSuccessfull++;
                }
            }

            return countRowSuccessfull;
        }

        private void UpdateSomeField(DataRow dr)
        {
            var employeeCode = dr[MaNhanVien].ToString().Trim();

            var valueLuong_NangSuat =
                Convert.ToDouble(dr[Luong_NangSuat].ToString().Trim().Length > 0
                    ? dr[Luong_NangSuat].ToString().Trim()
                    : "0");
            var valueLuong_CoBan =
                Convert.ToDouble(dr[Luong_CoBan].ToString().Trim().Length > 0 ? dr[Luong_CoBan].ToString().Trim() : "0");
            // luong
            var objBLL = new IncomesMonthBLL(0, valueLuong_NangSuat, MonthSalary);
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_LNS;
            objBLL.UserCode = employeeCode;
            objBLL.Save();

            objBLL.Value = valueLuong_CoBan;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_LCB;
            objBLL.Save();

            var valueCKPN_DoanPhiCD =
                Convert.ToDouble(dr[CKPN_DoanPhiCD].ToString().Trim().Length > 0
                    ? dr[CKPN_DoanPhiCD].ToString().Trim()
                    : "0");
            var valueCKPN_ThueThuNhap =
                Convert.ToDouble(dr[CKPN_ThueThuNhap].ToString().Trim().Length > 0
                    ? dr[CKPN_ThueThuNhap].ToString().Trim()
                    : "0");
            objBLL.Value = valueCKPN_DoanPhiCD;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_TRICH_DOAN_PHI;
            objBLL.Save();

            objBLL.Value = valueCKPN_ThueThuNhap;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_TTN;
            objBLL.Save();

            _TotalIncome =
                Convert.ToDouble(dr[TongCong].ToString().Trim().Length > 0 ? dr[TongCong].ToString().Trim() : "0");
            var value_ThucLinh =
                Convert.ToDouble(dr[ThucLinh].ToString().Trim().Length > 0 ? dr[ThucLinh].ToString().Trim() : "0");
            // trong sproc chi update totalIncome va value_ThucLinh neu da co gia tri
            EmployeeIncomeBLL.InsertByImporting(employeeCode, _TotalIncome, 0, value_ThucLinh, 0, 0, MonthSalary);
        }

        #region import some informations for employee

        private void ImportEmployee(DataRow dr)
        {
            var employeeCode = dr["Mã nhân viên"].ToString().Trim();
            var employeeFullName = dr["Họ và tên"].ToString().Trim();
            var departmentName = dr["Đơn vị - phòng ban"].ToString().Trim();
            var positionName = dr["Chức vụ"].ToString().Trim();
            if ((employeeFullName != null) || (employeeFullName.Trim().Length > 0))
                EmployeesBLL.InsertByImporting(employeeCode, employeeFullName, departmentName, positionName);
        }

        #endregion

        private void ImportCoefficient(DataRow dr)
        {
            var employeeCode = dr[MaNhanVien].ToString().Trim();

            var valueLCB =
                Convert.ToDouble(dr[HeSo_LCB].ToString().Trim().Length > 0 ? dr[HeSo_LCB].ToString().Trim() : "0");
            var valuePCCV =
                Convert.ToDouble(dr[HeSo_PCCV].ToString().Trim().Length > 0 ? dr[HeSo_PCCV].ToString().Trim() : "0");
            var valuePCTN =
                Convert.ToDouble(dr[HeSo_PCTN].ToString().Trim().Length > 0 ? dr[HeSo_PCTN].ToString().Trim() : "0");
            var valuePCKV =
                Convert.ToDouble(dr[HeSo_PCKV].ToString().Trim().Length > 0 ? dr[HeSo_PCKV].ToString().Trim() : "0");
            var valuePCDH =
                Convert.ToDouble(dr[HeSo_PCDH].ToString().Trim().Length > 0 ? dr[HeSo_PCDH].ToString().Trim() : "0");
            var valueLNS =
                Convert.ToDouble(dr[HeSo_LNS].ToString().Trim().Length > 0 ? dr[HeSo_LNS].ToString().Trim() : "0");
            var valueK = Convert.ToDouble(dr[HeSo_K].ToString().Trim().Length > 0 ? dr[HeSo_K].ToString().Trim() : "0");

            var objBLL = new CoefficientsBLL(0, valueLCB, MonthSalary);
            objBLL.UserCode = employeeCode;
            objBLL.CoefficientTypeId = CoefficientTypeKeys.CONST_LCB;
            objBLL.Save();

            objBLL.Value = valuePCCV;
            objBLL.CoefficientTypeId = CoefficientTypeKeys.CONST_PCCV;
            objBLL.Save();

            objBLL.Value = valuePCTN;
            objBLL.CoefficientTypeId = CoefficientTypeKeys.CONST_PCTN;
            objBLL.Save();

            objBLL.Value = valuePCKV;
            objBLL.CoefficientTypeId = CoefficientTypeKeys.CONST_PCKV;
            objBLL.Save();

            objBLL.Value = valuePCDH;
            objBLL.CoefficientTypeId = CoefficientTypeKeys.CONST_PCDH;
            objBLL.Save();

            objBLL.Value = valueLNS;
            objBLL.CoefficientTypeId = CoefficientTypeKeys.CONST_LNS;
            objBLL.Save();

            objBLL.Value = valueK;
            objBLL.CoefficientTypeId = CoefficientTypeKeys.CONST_K;
            objBLL.Save();
        }

        private void ImportTimeKeeping(DataRow dr)
        {
            var employeeCode = dr[MaNhanVien].ToString().Trim();

            ////// Ngay Cong 
            var valueNC_LamViec =
                Convert.ToDouble(dr[NC_LamViec].ToString().Trim().Length > 0 ? dr[NC_LamViec].ToString().Trim() : "0");
            var valueNC_Om_Co =
                Convert.ToDouble(dr[NC_Om_Co].ToString().Trim().Length > 0 ? dr[NC_Om_Co].ToString().Trim() : "0");
            var valueNC_ThaiSan =
                Convert.ToDouble(dr[NC_ThaiSan].ToString().Trim().Length > 0 ? dr[NC_ThaiSan].ToString().Trim() : "0");
            var valueNC_TNLD =
                Convert.ToDouble(dr[NC_TNLD].ToString().Trim().Length > 0 ? dr[NC_TNLD].ToString().Trim() : "0");
            var valueNC_PhepDiDuong =
                Convert.ToDouble(dr[NC_Phep_DiDuong].ToString().Trim().Length > 0
                    ? dr[NC_Phep_DiDuong].ToString().Trim()
                    : "0");
            double valueGiamTruCaNhan = 1;
            //Convert.ToDouble(dr[GiamTruCaNhan].ToString().Trim().Length > 0 ? dr[GiamTruCaNhan].ToString().Trim() : "0");
            var valueGiamTruNguoiPhuThuoc =
                Convert.ToDouble(dr[GiamTruNguoiPhuThuoc].ToString().Trim().Length > 0
                    ? dr[GiamTruNguoiPhuThuoc].ToString().Trim()
                    : "0");
            var valueNC_Khac =
                Convert.ToDouble(dr[NC_Khac].ToString().Trim().Length > 0 ? dr[NC_Khac].ToString().Trim() : "0");

            var objBLL = new TimeKeepingBLL(0, valueNC_LamViec, MonthSalary);
            objBLL.TimeKeepingTypeId = TimeKeepingTypeKeys.CONST_NC_LAM_VIEC;
            objBLL.UserCode = employeeCode;
            objBLL.Save();

            objBLL.Value = valueNC_Om_Co;
            objBLL.TimeKeepingTypeId = TimeKeepingTypeKeys.CONST_O_CO_KHH_DS;
            objBLL.Save();

            objBLL.Value = valueNC_ThaiSan;
            objBLL.TimeKeepingTypeId = TimeKeepingTypeKeys.CONST_THAI_SAN;
            objBLL.Save();

            objBLL.Value = valueNC_TNLD;
            objBLL.TimeKeepingTypeId = TimeKeepingTypeKeys.CONST_TNLD;
            objBLL.Save();

            objBLL.Value = valueNC_PhepDiDuong;
            objBLL.TimeKeepingTypeId = TimeKeepingTypeKeys.CONST_PHE_DI_DUONG;
            objBLL.Save();

            objBLL.Value = valueGiamTruCaNhan;
            objBLL.TimeKeepingTypeId = TimeKeepingTypeKeys.CONST_DOI_TUONG_NOP_THUE;
            objBLL.Save();

            objBLL.Value = valueGiamTruNguoiPhuThuoc;
            objBLL.TimeKeepingTypeId = TimeKeepingTypeKeys.CONST_NGUOI_PHU_THU;
            objBLL.Save();

            objBLL.Value = valueNC_Khac;
            objBLL.TimeKeepingTypeId = TimeKeepingTypeKeys.CONST_KHAC;
            objBLL.Save();

            ///// Gio Cong
            //double valueLamDem = Convert.ToDouble(dr["Làm thêm ban ngày"].ToString().Trim().Length > 0 ? dr["Làm thêm ban ngày"].ToString().Trim() : "0");
            var valueGC_LamThem =
                Convert.ToDouble(dr[GC_LamThem].ToString().Trim().Length > 0 ? dr[GC_LamThem].ToString().Trim() : "0");
            var valueGC_LamDem =
                Convert.ToDouble(dr[GC_LamDem].ToString().Trim().Length > 0 ? dr[GC_LamDem].ToString().Trim() : "0");

            //objBLL.Value = valueLamDem;
            //objBLL.TimeKeepingTypeId = TimeKeepingTypeKeys.CONST_LAM_DEM;
            //objBLL.Save();

            objBLL.Value = valueGC_LamThem;
            objBLL.TimeKeepingTypeId = TimeKeepingTypeKeys.CONST_LAM_THEM_BAN_NGAY;
            objBLL.Save();

            objBLL.Value = valueGC_LamDem;
            objBLL.TimeKeepingTypeId = TimeKeepingTypeKeys.CONST_LAM_THEM_BAN_DEM;
            objBLL.Save();
        }

        private void ImportIncomeMonth(DataRow dr)
        {
            /// Income
            /// Cac khoan thu nhap
            /// 

            var employeeCode = dr[MaNhanVien].ToString().Trim();

            // luong
            var valueLuong_NangSuat =
                Convert.ToDouble(dr[Luong_NangSuat].ToString().Trim().Length > 0
                    ? dr[Luong_NangSuat].ToString().Trim()
                    : "0");
            double valueLuong_CoBan = 0;
            // Convert.ToDouble(dr[Luong_CoBan].ToString().Trim().Length > 0 ? dr[Luong_CoBan].ToString().Trim() : "0");
            // cac khoan phu cap
            var valueCKPC_PCCV =
                Convert.ToDouble(dr[CKPC_PCCV].ToString().Trim().Length > 0 ? dr[CKPC_PCCV].ToString().Trim() : "0");
            var valueCKPC_PCTN =
                Convert.ToDouble(dr[CKPC_PCTN].ToString().Trim().Length > 0 ? dr[CKPC_PCTN].ToString().Trim() : "0");
            var valueCKPC_PCDH =
                Convert.ToDouble(dr[CKPC_PCDH].ToString().Trim().Length > 0 ? dr[CKPC_PCDH].ToString().Trim() : "0");
            // cac khoan tro cap
            var valueCKTC_TroCapBHXH =
                Convert.ToDouble(dr[CKTC_TroCapBHXH].ToString().Trim().Length > 0
                    ? dr[CKTC_TroCapBHXH].ToString().Trim()
                    : "0");
            var valueCKTC_TienAnGiuaCa =
                Convert.ToDouble(dr[CKTC_TienAnGiuaCa].ToString().Trim().Length > 0
                    ? dr[CKTC_TienAnGiuaCa].ToString().Trim()
                    : "0");
            var valueCKTC_BoSungLuong =
                Convert.ToDouble(dr[CKTC_BoSungLuong].ToString().Trim().Length > 0
                    ? dr[CKTC_BoSungLuong].ToString().Trim()
                    : "0");
            double valueCKTC_TienThemGio = 0;
            // Convert.ToDouble(dr[CKTC_TienThemGio].ToString().Trim().Length > 0 ? dr[CKTC_TienThemGio].ToString().Trim() : "0");
            var valueCKTC_TienLamDem =
                Convert.ToDouble(dr[CKTC_TienLamDem].ToString().Trim().Length > 0
                    ? dr[CKTC_TienLamDem].ToString().Trim()
                    : "0");
            //double valueTienThuong = Convert.ToDouble(dr["Tiền thưởng"].ToString().Trim().Length > 0 ? dr["Tiền thưởng"].ToString().Trim() : "0");

            // luong
            var objBLL = new IncomesMonthBLL(0, valueLuong_NangSuat, MonthSalary);
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_LNS;
            objBLL.UserCode = employeeCode;
            objBLL.Save();

            objBLL.Value = valueLuong_CoBan;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_LCB;
            objBLL.Save();
            // cac khoan phuc cap
            objBLL.Value = valueCKPC_PCCV;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_PCCV;
            objBLL.Save();

            objBLL.Value = valueCKPC_PCTN;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_PCTN;
            objBLL.Save();

            objBLL.Value = valueCKPC_PCDH;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_PCDH;
            objBLL.Save();

            // cac khoan tro cap
            objBLL.Value = valueCKTC_TroCapBHXH;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_TROCAP_BHXH;
            objBLL.Save();

            objBLL.Value = valueCKTC_TienAnGiuaCa;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_TIEN_AN;
            objBLL.Save();

            objBLL.Value = valueCKTC_BoSungLuong;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_BO_SUNG_LUONG;
            objBLL.Save();

            objBLL.Value = valueCKTC_TienThemGio;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_TIEN_THEM_GIO;
            objBLL.Save();

            objBLL.Value = valueCKTC_TienLamDem;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_TIEN_LAM_DEM;
            objBLL.Save();

            //objBLL.Value = valueTienThuong;
            //objBLL.IncomeTypeId = IncomeTypeKeys.CONST_TIEN_THUONG;
            //objBLL.Save();

            /// Contribution
            ///// Cac khoan phai nop
            var valueCKPN_BHXH =
                Convert.ToDouble(dr[CKPN_BHXH].ToString().Trim().Length > 0 ? dr[CKPN_BHXH].ToString().Trim() : "0");
            var valueCKPN_BHYT =
                Convert.ToDouble(dr[CKPN_BHYT].ToString().Trim().Length > 0 ? dr[CKPN_BHYT].ToString().Trim() : "0");
            var valueCKPN_DoanPhiCD =
                Convert.ToDouble(dr[CKPN_DoanPhiCD].ToString().Trim().Length > 0
                    ? dr[CKPN_DoanPhiCD].ToString().Trim()
                    : "0");
            var valueCKPN_ThueThuNhap =
                Convert.ToDouble(dr[CKPN_ThueThuNhap].ToString().Trim().Length > 0
                    ? dr[CKPN_ThueThuNhap].ToString().Trim()
                    : "0");
            //double valueCKPN_ThueThuNhapGianNop = Convert.ToDouble(dr[CKPN_SoQuyetToanThueTNCN].ToString().Trim().Length > 0 ? dr[CKPN_SoQuyetToanThueTNCN].ToString().Trim() : "0");
            var valueCKPN_BHThatNghiep =
                Convert.ToDouble(dr[CKPN_BHThatNghiep].ToString().Trim().Length > 0
                    ? dr[CKPN_BHThatNghiep].ToString().Trim()
                    : "0");

            objBLL.Value = valueCKPN_BHXH;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_TRICH_BHXH;
            objBLL.Save();

            objBLL.Value = valueCKPN_BHYT;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_TRICH_BHYT;
            objBLL.Save();

            objBLL.Value = valueCKPN_DoanPhiCD;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_TRICH_DOAN_PHI;
            objBLL.Save();

            objBLL.Value = valueCKPN_ThueThuNhap;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_TTN;
            objBLL.Save();

            //objBLL.Value = valueCKPN_ThueThuNhapGianNop;
            //objBLL.IncomeTypeId = IncomeTypeKeys.CONST_TTNGianNop;
            //objBLL.Save();

            objBLL.Value = valueCKPN_BHThatNghiep;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_BHTN;
            objBLL.Save();

            _TotalContribution = valueCKPN_BHXH + valueCKPN_BHYT + valueCKPN_DoanPhiCD + valueCKPN_ThueThuNhap +
                                 valueCKPN_BHThatNghiep; // +valueCKPN_ThueThuNhapGianNop;
        }

        private void ImportEmployeeTotalIncome(DataRow dr)
        {
            var employeeCode = dr[MaNhanVien].ToString().Trim();
            _TotalIncome =
                Convert.ToDouble(dr[TongCong].ToString().Trim().Length > 0 ? dr[TongCong].ToString().Trim() : "0");
            var value_ThucLinh =
                Convert.ToDouble(dr[ThucLinh].ToString().Trim().Length > 0 ? dr[ThucLinh].ToString().Trim() : "0");
            EmployeeIncomeBLL.InsertByImporting(employeeCode, _TotalIncome, _TotalContribution, value_ThucLinh, 0, 0,
                MonthSalary);
        }

        private void ImportIncomeMonthKhoan(DataRow dr)
        {
            /// Income
            /// Cac khoan thu nhap
            /// 

            var employeeCode = dr[MaNhanVien].ToString().Trim();

            // luong
            var valueLuong_Khoan =
                Convert.ToDouble(dr[Luong_Khoan].ToString().Trim().Length > 0 ? dr[Luong_Khoan].ToString().Trim() : "0");
            var valueLuong_CoBan =
                Convert.ToDouble(dr[Luong_CoBan].ToString().Trim().Length > 0 ? dr[Luong_CoBan].ToString().Trim() : "0");
            // cac khoan phu cap
            var valueCKPC_PCCV =
                Convert.ToDouble(dr[CKPC_PCCV].ToString().Trim().Length > 0 ? dr[CKPC_PCCV].ToString().Trim() : "0");
            var valueCKPC_PCTN =
                Convert.ToDouble(dr[CKPC_PCTN].ToString().Trim().Length > 0 ? dr[CKPC_PCTN].ToString().Trim() : "0");
            var valueCKPC_PCDH =
                Convert.ToDouble(dr[CKPC_PCDH].ToString().Trim().Length > 0 ? dr[CKPC_PCDH].ToString().Trim() : "0");
            // cac khoan tro cap
            var valueCKTC_TroCapBHXH =
                Convert.ToDouble(dr[CKTC_TroCapBHXH].ToString().Trim().Length > 0
                    ? dr[CKTC_TroCapBHXH].ToString().Trim()
                    : "0");
            var valueCKTC_TienAnGiuaCa =
                Convert.ToDouble(dr[CKTC_TienAnGiuaCa].ToString().Trim().Length > 0
                    ? dr[CKTC_TienAnGiuaCa].ToString().Trim()
                    : "0");
            var valueCKTC_BoSungLuong =
                Convert.ToDouble(dr[CKTC_BoSungLuong].ToString().Trim().Length > 0
                    ? dr[CKTC_BoSungLuong].ToString().Trim()
                    : "0");
            var valueCKTC_TienThemGio =
                Convert.ToDouble(dr[CKTC_TienThemGio].ToString().Trim().Length > 0
                    ? dr[CKTC_TienThemGio].ToString().Trim()
                    : "0");
            var valueCKTC_TienLamDem =
                Convert.ToDouble(dr[CKTC_TienLamDem].ToString().Trim().Length > 0
                    ? dr[CKTC_TienLamDem].ToString().Trim()
                    : "0");
            //double valueTienThuong = Convert.ToDouble(dr["Tiền thưởng"].ToString().Trim().Length > 0 ? dr["Tiền thưởng"].ToString().Trim() : "0");

            // luong
            var objBLL = new IncomesMonthBLL(0, valueLuong_Khoan, MonthSalary);
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_LươngKhoan;
            objBLL.UserCode = employeeCode;
            objBLL.Save();

            objBLL.Value = valueLuong_CoBan;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_LCB;
            objBLL.Save();
            // cac khoan phuc cap
            objBLL.Value = valueCKPC_PCCV;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_PCCV;
            objBLL.Save();

            objBLL.Value = valueCKPC_PCTN;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_PCTN;
            objBLL.Save();

            objBLL.Value = valueCKPC_PCDH;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_PCDH;
            objBLL.Save();

            // cac khoan tro cap
            objBLL.Value = valueCKTC_TroCapBHXH;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_TROCAP_BHXH;
            objBLL.Save();

            objBLL.Value = valueCKTC_TienAnGiuaCa;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_TIEN_AN;
            objBLL.Save();

            objBLL.Value = valueCKTC_BoSungLuong;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_BO_SUNG_LUONG;
            objBLL.Save();

            objBLL.Value = valueCKTC_TienThemGio;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_TIEN_THEM_GIO;
            objBLL.Save();

            objBLL.Value = valueCKTC_TienLamDem;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_TIEN_LAM_DEM;
            objBLL.Save();

            //objBLL.Value = valueTienThuong;
            //objBLL.IncomeTypeId = IncomeTypeKeys.CONST_TIEN_THUONG;
            //objBLL.Save();

            /// Contribution
            ///// Cac khoan phai nop
            var valueCKPN_BHXH =
                Convert.ToDouble(dr[CKPN_BHXH].ToString().Trim().Length > 0 ? dr[CKPN_BHXH].ToString().Trim() : "0");
            var valueCKPN_BHYT =
                Convert.ToDouble(dr[CKPN_BHYT].ToString().Trim().Length > 0 ? dr[CKPN_BHYT].ToString().Trim() : "0");
            var valueCKPN_DoanPhiCD =
                Convert.ToDouble(dr[CKPN_DoanPhiCD].ToString().Trim().Length > 0
                    ? dr[CKPN_DoanPhiCD].ToString().Trim()
                    : "0");
            var valueCKPN_ThueThuNhap =
                Convert.ToDouble(dr[CKPN_ThueThuNhap].ToString().Trim().Length > 0
                    ? dr[CKPN_ThueThuNhap].ToString().Trim()
                    : "0");
            //double valueCKPN_ThueThuNhapGianNop = Convert.ToDouble(dr[CKPN_SoQuyetToanThueTNCN].ToString().Trim().Length > 0 ? dr[CKPN_SoQuyetToanThueTNCN].ToString().Trim() : "0");
            var valueCKPN_BHThatNghiep =
                Convert.ToDouble(dr[CKPN_BHThatNghiep].ToString().Trim().Length > 0
                    ? dr[CKPN_BHThatNghiep].ToString().Trim()
                    : "0");

            objBLL.Value = valueCKPN_BHXH;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_TRICH_BHXH;
            objBLL.Save();

            objBLL.Value = valueCKPN_BHYT;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_TRICH_BHYT;
            objBLL.Save();

            objBLL.Value = valueCKPN_DoanPhiCD;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_TRICH_DOAN_PHI;
            objBLL.Save();

            objBLL.Value = valueCKPN_ThueThuNhap;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_TTN;
            objBLL.Save();

            //objBLL.Value = valueCKPN_ThueThuNhapGianNop;
            //objBLL.IncomeTypeId = IncomeTypeKeys.CONST_TTNGianNop;
            //objBLL.Save();


            objBLL.Value = valueCKPN_BHThatNghiep;
            objBLL.IncomeTypeId = IncomeTypeKeys.CONST_BHTN;
            objBLL.Save();

            _TotalContribution = valueCKPN_BHXH + valueCKPN_BHYT + valueCKPN_DoanPhiCD + valueCKPN_ThueThuNhap +
                                 valueCKPN_BHThatNghiep;
        }

        #region private fields

        //private DataTable _ExcelData;

        private double _TotalIncome;
        private double _TotalContribution;


        private readonly string MaNhanVien = "Mã NV";

        /// <summary>
        ///     Tong hop he so tinh luong
        /// </summary>
        private readonly string HeSo_LCB = "Hệ số LCB";

        private readonly string HeSo_PCCV = "Hệ số PCCV";
        private readonly string HeSo_PCTN = "Hệ số PCTN";
        private readonly string HeSo_PCKV = "Hệ số PCKV";
        private readonly string HeSo_PCDH = "Hệ số PCĐH";
        private readonly string HeSo_LNS = "Hệ số LNS";
        private readonly string HeSo_K = "Hệ số K";

        /// <summary>
        ///     Tong hop ngay cong
        /// </summary>
        private readonly string NC_LamViec = "Làm việc";

        private readonly string NC_Om_Co = "Ốm, Co";
        private readonly string NC_ThaiSan = "Thai sản";
        private readonly string NC_TNLD = "TNLĐ";
        private readonly string NC_Phep_DiDuong = "Phép, đi đường";
        private readonly string NC_Khac = "Khác";

        /// <summary>
        ///     Thue Thu Nhap Ca Nhan
        /// </summary>
        private string GiamTruCaNhan = "Giảm trừ cá nhân";

        private readonly string GiamTruNguoiPhuThuoc = "Giảm trừ người phụ thuộc";

        /// <summary>
        ///     Tong hop gio cong
        /// </summary>
        private readonly string GC_LamThem = "Làm thêm";

        private readonly string GC_LamDem = "Làm đêm";

        /// <summary>
        ///     Luong
        /// </summary>
        private readonly string Luong_NangSuat = "Lương năng suất";

        private readonly string Luong_CoBan = "Lương cơ bản";
        private readonly string Luong_Khoan = "Lương khoán";

        /// <summary>
        ///     Cac khoan phu cap
        /// </summary>
        private readonly string CKPC_PCCV = "PCCV";

        private readonly string CKPC_PCTN = "PCTN";
        private readonly string CKPC_PCDH = "PCĐH";


        /// <summary>
        ///     Cac khoan tro cap
        /// </summary>
        private readonly string CKTC_TroCapBHXH = "Trợ cấp BHXH";

        private readonly string CKTC_TienAnGiuaCa = "Tiền ăn giữa ca";
        private readonly string CKTC_BoSungLuong = "Bổ sung lương";
        private readonly string CKTC_TienThemGio = "Tiền thêm giờ";
        private readonly string CKTC_TienLamDem = "Tiền làm đêm";

        /// <summary>
        ///     Cac khoan phai nop
        /// </summary>
        private readonly string CKPN_BHXH = "BHXH";

        private readonly string CKPN_BHYT = "BHYT";
        private readonly string CKPN_BHThatNghiep = "BH thất nghiệp";
        private readonly string CKPN_DoanPhiCD = "Đoàn phí CĐ";
        //private string CKPN_SoQuyetToanThueTNCN = "Số quyết toán thuế TNCN";
        private readonly string CKPN_ThueThuNhap = "Thuế thu nhập";

        private readonly string TongCong = "Tổng cộng";
        private readonly string ThucLinh = "Thực lĩnh";

        #endregion

        #region properties

        public string FileName { get; set; }

        //private DataTable ExcelData
        //{
        //    get { return _ExcelData; }
        //    set { _ExcelData = value; }
        //}
        public DateTime MonthSalary { get; set; }

        #endregion
    }
}