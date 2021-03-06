using System.Collections.Generic;
using HRMBLL.H0;
using HRMBLL.H1.Helper;
using HRMUtil;

namespace HRMBLL.H1.CalulateSalary
{
    public class BasicSalaryBLL
    {
        public string LCBLog { get; set; } = string.Empty;

        public void Calculate()
        {
            if (ListEmployeeContractBLLCheck.Count > 1)
            {
                LCBLog += "Trường hợp này có chuyển đổi hợp đồng trong tháng :<br/>";
                for (var i = 0; i < ListEmployeeContractBLLCheck.Count; i++)
                {
                    var objOldContract = ListEmployeeContractBLLCheck[i];
                    ValueLCB += CalculateMultiContract(objOldContract, i + 1);
                }
            }
            else
            {
                LCBLog += "Trường hợp này chỉ có 1 hợp đồng trong tháng :<br/>";
                ValueLCB = CalculateOnlyOneContract(ListEmployeeContractBLLCheck[0]);
            }
            LCBLog += "LCB = " + ValueLCB;
        }

        public void CalculateDinhCongTac()
        {
            if (ListEmployeeContractBLLCheck.Count > 1)
            {
                LCBLog += "Trường hợp này có chuyển đổi hợp đồng trong tháng :<br/>";
                for (var i = 0; i < ListEmployeeContractBLLCheck.Count; i++)
                {
                    var objOldContract = ListEmployeeContractBLLCheck[i];
                    ValueLCB += CalculateMultiContractDinhChiCongTac(objOldContract, i + 1);
                }
            }
            else
            {
                LCBLog += "Trường hợp này chỉ có 1 hợp đồng trong tháng :<br/>";
                ValueLCB = CalculateOnlyOneContractDinhChiCongTac(ListEmployeeContractBLLCheck[0]);
            }
            LCBLog += "LCB = " + ValueLCB;
        }

        #region private fields

        #endregion

        #region properties

        public WorkdayEmployeesBLLL ObjWorkdayEmployeesBLLL { get; set; }

        public CoefficientEmployeeFinalBLL ObjCoefficientEmployeeFinalBLL { get; set; }

        public List<EmployeeContractBLL> ListEmployeeContractBLLCheck { get; set; }


        public double ValueLCB { get; set; }

        /// <summary>
        ///     Don gia luong can ban
        /// </summary>
        public double DON_GIA_LCB { get; set; }

        public int UserId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        #endregion

        #region Tinh Luong Co Ban

        private bool CheckContractXDTH(EmployeeContractBLL objEC)
        {
            if ((objEC.ContractTypeId == Constants.HopDong_XÐTH_12T) ||
                (objEC.ContractTypeId == Constants.HopDong_XÐTH_24T) ||
                (objEC.ContractTypeId == Constants.HopDong_XÐTH_36T) ||
                (objEC.ContractTypeId == Constants.HopDong_KoXÐTH) ||
                (objEC.ContractTypeId == Constants.HopDong_ThoiVu_6T))
                return true;
            return false;
        }

        private double CalculateOnlyOneContract(EmployeeContractBLL objEC)
        {
            double LCBValueReturn = 0;

            if (CheckContractXDTH(objEC))
            {
                LCBLog += "Hợp Đồng : " + objEC.ContractTypeCode + "<br/>";
                var finalHeSo = ObjCoefficientEmployeeFinalBLL.LCB;
                LCBLog += "finalHeSo = HeSoLCB:" + ObjCoefficientEmployeeFinalBLL.LCB + "=" + finalHeSo + "<br/>";
                LCBLog += "LCB =0 <br/>";
                if (ObjWorkdayEmployeesBLLL.XL == ObjWorkdayEmployeesBLLL.XQDL)
                {
                    LCBLog += "X=XQD-><br/>";
                    LCBValueReturn = finalHeSo*DON_GIA_LCB;
                    LCBLog += "LCB=finalHeSo:" + finalHeSo + " x DonGiaLCB:" + DON_GIA_LCB + "=" + LCBValueReturn +
                              "<br/>";
                }
                else
                {
                    LCBLog += "X!=XQD-><br/>";
                    var luongMotNgay = finalHeSo*DON_GIA_LCB/ObjWorkdayEmployeesBLLL.XQDL;
                    LCBLog += "luongMotNgay = (finalHeSo:" + finalHeSo + " x DonGiaLCB:" + DON_GIA_LCB + ")/XQD:" +
                              ObjWorkdayEmployeesBLLL.XQDL + "=" + luongMotNgay + "<br/>";

                    if (ObjWorkdayEmployeesBLLL.F_OmL > 0)
                    {
                        LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                        LCBValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_OmL*0.75;
                        LCBLog += " + (luongMotNgay : " + luongMotNgay + " x Ô:" + ObjWorkdayEmployeesBLLL.F_OmL +
                                  " x 0.75)=" + LCBValueReturn + "<br/>";
                    }
                    if (ObjWorkdayEmployeesBLLL.F_Con_OmL > 0)
                    {
                        LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                        LCBValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_Con_OmL*0.75;
                        LCBLog += " + (luongMotNgay : " + luongMotNgay + " x Co:" + ObjWorkdayEmployeesBLLL.F_Con_OmL +
                                  " x 0.75)=" + LCBValueReturn + "<br/>";
                    }
                    if (ObjWorkdayEmployeesBLLL.F_KHHDSL > 0)
                    {
                        LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                        LCBValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_KHHDSL*0.75;
                        LCBLog += " + (luongMotNgay : " + luongMotNgay + " x KHH:" + ObjWorkdayEmployeesBLLL.F_KHHDSL +
                                  " x 0.75)=" + LCBValueReturn + "<br/>";
                    }
                    if (ObjWorkdayEmployeesBLLL.F_ThaiSanL > 0)
                    {
                        LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                        LCBValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_ThaiSanL;
                        LCBLog += " + (luongMotNgay : " + luongMotNgay + " x TS:" + ObjWorkdayEmployeesBLLL.F_ThaiSanL +
                                  ")=" + LCBValueReturn + "<br/>";
                    }
                    if (ObjWorkdayEmployeesBLLL.F_TNLDL > 0)
                    {
                        LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                        LCBValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_TNLDL;
                        LCBLog += " + (luongMotNgay : " + luongMotNgay + " x TBLD:" + ObjWorkdayEmployeesBLLL.F_TNLDL +
                                  ")=" + LCBValueReturn + "<br/>";
                    }
                    if (ObjWorkdayEmployeesBLLL.F_DiDuongL > 0)
                    {
                        LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                        LCBValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_DiDuongL;
                        LCBLog += " + (luongMotNgay : " + luongMotNgay + " x DD:" + ObjWorkdayEmployeesBLLL.F_DiDuongL +
                                  ")=" + LCBValueReturn + "<br/>";
                    }
                    if (ObjWorkdayEmployeesBLLL.F_Hoc1L > 0)
                    {
                        LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                        LCBValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_Hoc1L;
                        LCBLog += " + (luongMotNgay : " + luongMotNgay + " x Ho1:" + ObjWorkdayEmployeesBLLL.F_Hoc1L +
                                  ")=" + LCBValueReturn + "<br/>";
                    }
                    if (ObjWorkdayEmployeesBLLL.F_Hoc2L > 0)
                    {
                        LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                        LCBValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_Hoc2L;
                        LCBLog += " + (luongMotNgay : " + luongMotNgay + " x Ho2:" + ObjWorkdayEmployeesBLLL.F_Hoc2L +
                                  ")=" + LCBValueReturn + "<br/>";
                    }
                    if (ObjWorkdayEmployeesBLLL.F_Hoc3L > 0)
                    {
                        LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                        LCBValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_Hoc3L;
                        LCBLog += " + (luongMotNgay : " + luongMotNgay + " x Ho3:" + ObjWorkdayEmployeesBLLL.F_Hoc3L +
                                  ")=" + LCBValueReturn + "<br/>";
                    }
                    if (ObjWorkdayEmployeesBLLL.F_Hoc4L > 0)
                    {
                        LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                        LCBValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_Hoc4L;
                        LCBLog += " + (luongMotNgay : " + luongMotNgay + " x Ho4:" + ObjWorkdayEmployeesBLLL.F_Hoc4L +
                                  ")=" + LCBValueReturn + "<br/>";
                    }
                    if (ObjWorkdayEmployeesBLLL.F_Hoc5L > 0)
                    {
                        LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                        LCBValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_Hoc5L;
                        LCBLog += " + (luongMotNgay : " + luongMotNgay + " x Ho5:" + ObjWorkdayEmployeesBLLL.F_Hoc5L +
                                  ")=" + LCBValueReturn + "<br/>";
                    }
                    if (ObjWorkdayEmployeesBLLL.F_Hoc6L > 0)
                    {
                        LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                        LCBValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_Hoc6L;
                        LCBLog += " + (luongMotNgay : " + luongMotNgay + " x Ho6:" + ObjWorkdayEmployeesBLLL.F_Hoc6L +
                                  ")=" + LCBValueReturn + "<br/>";
                    }
                    if (ObjWorkdayEmployeesBLLL.F_NamL > 0)
                    {
                        LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                        LCBValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_NamL;
                        LCBLog += " + (luongMotNgay : " + luongMotNgay + " x F:" + ObjWorkdayEmployeesBLLL.F_NamL + ")=" +
                                  LCBValueReturn + "<br/>";
                    }
                    if (ObjWorkdayEmployeesBLLL.F_dbL > 0)
                    {
                        LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                        LCBValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_dbL;
                        LCBLog += " + (luongMotNgay : " + luongMotNgay + " x Fdb:" + ObjWorkdayEmployeesBLLL.F_dbL +
                                  ")=" + LCBValueReturn + "<br/>";
                    }
                    if (ObjWorkdayEmployeesBLLL.F_CongTacL > 0)
                    {
                        LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                        LCBValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_CongTacL;
                        LCBLog += " + (luongMotNgay : " + luongMotNgay + " x CT:" + ObjWorkdayEmployeesBLLL.F_CongTacL +
                                  ")=" + LCBValueReturn + "<br/>";
                    }
                    if (ObjWorkdayEmployeesBLLL.F_LeL > 0)
                    {
                        LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                        LCBValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_LeL;
                        LCBLog += " + (luongMotNgay : " + luongMotNgay + " x LE:" + ObjWorkdayEmployeesBLLL.F_LeL + ")=" +
                                  LCBValueReturn + "<br/>";
                    }

                    if (ObjWorkdayEmployeesBLLL.F_DinhChiCongTacL > 0)
                    {
                        LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                        LCBValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_DinhChiCongTacL*0.50;
                        LCBLog += " + (luongMotNgay : " + luongMotNgay + " x DC:" +
                                  ObjWorkdayEmployeesBLLL.F_DinhChiCongTacL + " x 0.50 ) = " + LCBValueReturn + "<br/>";
                    }
                    if (ObjWorkdayEmployeesBLLL.XL > 0)
                    {
                        LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                        LCBValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.XL;
                        LCBLog += " + (luongMotNgay : " + luongMotNgay + " x X:" + ObjWorkdayEmployeesBLLL.XL + ")=" +
                                  LCBValueReturn + "<br/>";
                    }
                }
            }

            return LCBValueReturn;
        }

        private double CalculateOnlyOneContractDinhChiCongTac(EmployeeContractBLL objEC)
        {
            double LCBValueReturn = 0;

            if (CheckContractXDTH(objEC))
            {
                LCBLog += "Hợp Đồng : " + objEC.ContractTypeCode + "<br/>";
                var finalHeSo = ObjCoefficientEmployeeFinalBLL.LCB;
                LCBLog += "finalHeSo = HeSoLCB:" + ObjCoefficientEmployeeFinalBLL.LCB + "=" + finalHeSo + "<br/>";
                LCBLog += "LCB =0 <br/>";

                LCBLog += "X!=XQD-><br/>";
                var luongMotNgay = finalHeSo*DON_GIA_LCB/ObjWorkdayEmployeesBLLL.XQDL;
                LCBLog += "luongMotNgay = (finalHeSo:" + finalHeSo + " x DonGiaLCB:" + DON_GIA_LCB + ")/XQD:" +
                          ObjWorkdayEmployeesBLLL.XQDL + "=" + luongMotNgay + "<br/>";

                if (ObjWorkdayEmployeesBLLL.F_DinhChiCongTacL > 0)
                {
                    LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                    LCBValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_DinhChiCongTacL*0.50;
                    LCBLog += " + (luongMotNgay : " + luongMotNgay + " x DC:" +
                              ObjWorkdayEmployeesBLLL.F_DinhChiCongTacL + " x 0.50 ) = " + LCBValueReturn + "<br/>";
                }
            }

            return LCBValueReturn;
        }

        private double CalculateMultiContract(EmployeeContractBLL objEC, int i)
        {
            double LCBValueReturn = 0;

            #region declare variable

            double f_OmL = Constants.WorkdayEmployee_DefaultValue, f_OmDaiNgayL = Constants.WorkdayEmployee_DefaultValue;
            double f_ThaiSanL = Constants.WorkdayEmployee_DefaultValue,
                f_TNLDL = Constants.WorkdayEmployee_DefaultValue,
                f_NamL = Constants.WorkdayEmployee_DefaultValue;
            double f_dbL = Constants.WorkdayEmployee_DefaultValue,
                f_KoLuongCLDL = Constants.WorkdayEmployee_DefaultValue,
                f_KoLuongKLDL = Constants.WorkdayEmployee_DefaultValue;
            double f_DiDuongL = Constants.WorkdayEmployee_DefaultValue,
                f_CongTacL = Constants.WorkdayEmployee_DefaultValue;

            double f_HocSAGSL = Constants.WorkdayEmployee_DefaultValue,
                f_Hoc1L = Constants.WorkdayEmployee_DefaultValue,
                f_Hoc2L = Constants.WorkdayEmployee_DefaultValue,
                f_Hoc3L = Constants.WorkdayEmployee_DefaultValue;
            double f_Hoc4L = Constants.WorkdayEmployee_DefaultValue,
                f_Hoc5L = Constants.WorkdayEmployee_DefaultValue,
                f_Hoc6L = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc7L = Constants.WorkdayEmployee_DefaultValue;

            double f_Con_OmL = Constants.WorkdayEmployee_DefaultValue, f_KHHDSL = Constants.WorkdayEmployee_DefaultValue;
            double f_SayThaiL = Constants.WorkdayEmployee_DefaultValue,
                f_KhamThaiL = Constants.WorkdayEmployee_DefaultValue,
                f_ConChetL = Constants.WorkdayEmployee_DefaultValue;
            double f_DinhChiCongTacL = Constants.WorkdayEmployee_DefaultValue,
                f_TamHoanHDL = Constants.WorkdayEmployee_DefaultValue,
                f_HoiHopL = Constants.WorkdayEmployee_DefaultValue;
            double XL = 0, f_LeL = Constants.WorkdayEmployee_DefaultValue;

            #endregion

            #region Calculate days

            f_OmL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_O_BAN_THAN_CODE);

            f_OmDaiNgayL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_O_DAI_NGAY_CODE);


            f_ThaiSanL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_THAI_SAN_CODE);

            f_TNLDL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC, Constants.LEAVE_TYPE_TNLD_CODE);

            f_NamL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC, Constants.LEAVE_TYPE_F_NAM_CODE);

            f_dbL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC, Constants.LEAVE_TYPE_FDB_CODE);

            f_KoLuongCLDL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO_CODE);

            f_KoLuongKLDL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO_CODE);

            f_DiDuongL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_F_DI_DUONG_CODE);

            f_CongTacL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_F_CONG_TAC_CODE);

            f_HocSAGSL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_HOC_SAGS_CODE);

            f_Hoc1L = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC, Constants.LEAVE_TYPE_HOC_1_CODE);
            f_Hoc2L = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC, Constants.LEAVE_TYPE_HOC_2_CODE);
            f_Hoc3L = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC, Constants.LEAVE_TYPE_HOC_3_CODE);
            f_Hoc4L = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC, Constants.LEAVE_TYPE_HOC_4_CODE);
            f_Hoc5L = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC, Constants.LEAVE_TYPE_HOC_5_CODE);
            f_Hoc6L = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC, Constants.LEAVE_TYPE_HOC_6_CODE);
            f_Hoc7L = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC, Constants.LEAVE_TYPE_HOC_7_CODE);

            f_Con_OmL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_CON_OM_CODE);

            f_KHHDSL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC, Constants.LEAVE_TYPE_KHHDS_CODE);

            f_SayThaiL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_SAY_THAI_CODE);

            f_KhamThaiL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_KHAM_THAI_CODE);

            f_ConChetL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_CON_CHET_SAU_KHI_SINH_CODE);

            f_DinhChiCongTacL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_DINH_CHI_CONG_TAC_CODE);

            f_TamHoanHDL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_TAM_HOAN_HOP_DONG_CODE);

            f_HoiHopL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_HOI_HOP_CODE);

            f_LeL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC, Constants.LEAVE_TYPE_LE_TET_CODE);

            XL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC, Constants.LEAVE_TYPE_X_CODE);

            #endregion

            if (CheckContractXDTH(objEC))
            {
                LCBLog += i + ".Hợp Đồng : " + objEC.ContractTypeCode + "<br/>";
                var finalHeSo = ObjCoefficientEmployeeFinalBLL.LCB;
                LCBLog += "finalHeSo = HeSoLCB:" + ObjCoefficientEmployeeFinalBLL.LCB + "=" + finalHeSo + "<br/>";
                LCBLog += "LCB =0 <br/>";

                var luongMotNgay = finalHeSo*DON_GIA_LCB/ObjWorkdayEmployeesBLLL.XQDL;
                LCBLog += "luongMotNgay = (finalHeSo:" + finalHeSo + " x DonGiaLCB:" + DON_GIA_LCB + ")/XQD:" +
                          ObjWorkdayEmployeesBLLL.XQDL + "=" + luongMotNgay + "<br/>";

                if (f_OmL > 0)
                {
                    LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                    LCBValueReturn += luongMotNgay*f_OmL*0.75;
                    LCBLog += " + (luongMotNgay : " + luongMotNgay + " x Ô:" + f_OmL + " x 0.75)=" + LCBValueReturn +
                              "<br/>";
                }
                if (f_Con_OmL > 0)
                {
                    LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                    LCBValueReturn += luongMotNgay*f_Con_OmL*0.75;
                    LCBLog += " + (luongMotNgay : " + luongMotNgay + " x Co:" + f_Con_OmL + " x 0.75)=" + LCBValueReturn +
                              "<br/>";
                }
                if (f_KHHDSL > 0)
                {
                    LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                    LCBValueReturn += luongMotNgay*f_KHHDSL*0.75;
                    LCBLog += " + (luongMotNgay : " + luongMotNgay + " x Co:" + f_Con_OmL + " x 0.75)=" + LCBValueReturn +
                              "<br/>";
                }
                if (f_ThaiSanL > 0)
                {
                    LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                    LCBValueReturn += luongMotNgay*f_ThaiSanL;
                    LCBLog += " + (luongMotNgay : " + luongMotNgay + " x TS:" + f_ThaiSanL + ")=" + LCBValueReturn +
                              "<br/>";
                }
                if (f_TNLDL > 0)
                {
                    LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                    LCBValueReturn += luongMotNgay*f_TNLDL;
                    LCBLog += " + (luongMotNgay : " + luongMotNgay + " x TNLD:" + f_TNLDL + ")=" + LCBValueReturn +
                              "<br/>";
                }
                if (f_DiDuongL > 0)
                {
                    LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                    LCBValueReturn += luongMotNgay*f_DiDuongL;
                    LCBLog += " + (luongMotNgay : " + luongMotNgay + " x DD:" + f_DiDuongL + ")=" + LCBValueReturn +
                              "<br/>";
                }
                if (f_Hoc1L > 0)
                {
                    LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                    LCBValueReturn += luongMotNgay*f_Hoc1L;
                    LCBLog += " + (luongMotNgay : " + luongMotNgay + " x Ho1:" + f_Hoc1L + ")=" + LCBValueReturn +
                              "<br/>";
                }
                if (f_Hoc2L > 0)
                {
                    LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                    LCBValueReturn += luongMotNgay*f_Hoc2L;
                    LCBLog += " + (luongMotNgay : " + luongMotNgay + " x Ho2:" + f_Hoc2L + ")=" + LCBValueReturn +
                              "<br/>";
                }
                if (f_Hoc3L > 0)
                {
                    LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                    LCBValueReturn += luongMotNgay*f_Hoc3L;
                    LCBLog += " + (luongMotNgay : " + luongMotNgay + " x Ho3:" + f_Hoc3L + ")=" + LCBValueReturn +
                              "<br/>";
                }
                if (f_Hoc4L > 0)
                {
                    LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                    LCBValueReturn += luongMotNgay*f_Hoc4L;
                    LCBLog += " + (luongMotNgay : " + luongMotNgay + " x Ho4:" + f_Hoc4L + ")=" + LCBValueReturn +
                              "<br/>";
                }
                if (f_Hoc5L > 0)
                {
                    LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                    LCBValueReturn += luongMotNgay*f_Hoc5L;
                    LCBLog += " + (luongMotNgay : " + luongMotNgay + " x Ho5:" + f_Hoc5L + ")=" + LCBValueReturn +
                              "<br/>";
                }
                if (f_Hoc6L > 0)
                {
                    LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                    LCBValueReturn += luongMotNgay*f_Hoc6L;
                    LCBLog += " + (luongMotNgay : " + luongMotNgay + " x Ho6:" + f_Hoc6L + ")=" + LCBValueReturn +
                              "<br/>";
                }
                if (f_NamL > 0)
                {
                    LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                    LCBValueReturn += luongMotNgay*f_NamL;
                    LCBLog += " + (luongMotNgay : " + luongMotNgay + " x F:" + f_NamL + ")=" + LCBValueReturn + "<br/>";
                }
                if (f_dbL > 0)
                {
                    LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                    LCBValueReturn += luongMotNgay*f_dbL;
                    LCBLog += " + (luongMotNgay : " + luongMotNgay + " x Fdb:" + f_dbL + ")=" + LCBValueReturn + "<br/>";
                }
                if (f_CongTacL > 0)
                {
                    LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                    LCBValueReturn += luongMotNgay*f_CongTacL;
                    LCBLog += " + (luongMotNgay : " + luongMotNgay + " x CT:" + f_CongTacL + ")=" + LCBValueReturn +
                              "<br/>";
                }
                if (f_LeL > 0)
                {
                    LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                    LCBValueReturn += luongMotNgay*f_LeL;
                    LCBLog += " + (luongMotNgay : " + luongMotNgay + " x LE:" + f_LeL + ")=" + LCBValueReturn + "<br/>";
                }
                if (f_DinhChiCongTacL > 0)
                {
                    LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                    LCBValueReturn += luongMotNgay*f_DinhChiCongTacL*0.50;
                    LCBLog += " + (luongMotNgay : " + luongMotNgay + " x DC:" + f_DinhChiCongTacL + " x 0.50 )=" +
                              LCBValueReturn + "<br/>";
                }
                if (XL > 0)
                {
                    LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                    LCBValueReturn += luongMotNgay*XL;
                    LCBLog += " + (luongMotNgay : " + luongMotNgay + " x X:" + XL + ")=" + LCBValueReturn + "<br/>";
                }
            }

            return LCBValueReturn;
        }

        private double CalculateMultiContractDinhChiCongTac(EmployeeContractBLL objEC, int i)
        {
            double LCBValueReturn = 0;

            #region declare variable

            double f_OmL = Constants.WorkdayEmployee_DefaultValue, f_OmDaiNgayL = Constants.WorkdayEmployee_DefaultValue;
            double f_ThaiSanL = Constants.WorkdayEmployee_DefaultValue,
                f_TNLDL = Constants.WorkdayEmployee_DefaultValue,
                f_NamL = Constants.WorkdayEmployee_DefaultValue;
            double f_dbL = Constants.WorkdayEmployee_DefaultValue,
                f_KoLuongCLDL = Constants.WorkdayEmployee_DefaultValue,
                f_KoLuongKLDL = Constants.WorkdayEmployee_DefaultValue;
            double f_DiDuongL = Constants.WorkdayEmployee_DefaultValue,
                f_CongTacL = Constants.WorkdayEmployee_DefaultValue;

            double f_HocSAGSL = Constants.WorkdayEmployee_DefaultValue,
                f_Hoc1L = Constants.WorkdayEmployee_DefaultValue,
                f_Hoc2L = Constants.WorkdayEmployee_DefaultValue,
                f_Hoc3L = Constants.WorkdayEmployee_DefaultValue;
            double f_Hoc4L = Constants.WorkdayEmployee_DefaultValue,
                f_Hoc5L = Constants.WorkdayEmployee_DefaultValue,
                f_Hoc6L = Constants.WorkdayEmployee_DefaultValue;
            var f_Hoc7L = Constants.WorkdayEmployee_DefaultValue;

            double f_Con_OmL = Constants.WorkdayEmployee_DefaultValue, f_KHHDSL = Constants.WorkdayEmployee_DefaultValue;
            double f_SayThaiL = Constants.WorkdayEmployee_DefaultValue,
                f_KhamThaiL = Constants.WorkdayEmployee_DefaultValue,
                f_ConChetL = Constants.WorkdayEmployee_DefaultValue;
            double f_DinhChiCongTacL = Constants.WorkdayEmployee_DefaultValue,
                f_TamHoanHDL = Constants.WorkdayEmployee_DefaultValue,
                f_HoiHopL = Constants.WorkdayEmployee_DefaultValue;
            double XL = 0, f_LeL = Constants.WorkdayEmployee_DefaultValue;

            #endregion

            #region Calculate days

            f_OmL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_O_BAN_THAN_CODE);

            f_OmDaiNgayL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_O_DAI_NGAY_CODE);


            f_ThaiSanL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_THAI_SAN_CODE);

            f_TNLDL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC, Constants.LEAVE_TYPE_TNLD_CODE);

            f_NamL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC, Constants.LEAVE_TYPE_F_NAM_CODE);

            f_dbL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC, Constants.LEAVE_TYPE_FDB_CODE);

            f_KoLuongCLDL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_NGHI_KO_LUONG_CO_LYDO_CODE);

            f_KoLuongKLDL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_NGHI_KO_LUONG_KO_LYDO_CODE);

            f_DiDuongL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_F_DI_DUONG_CODE);

            f_CongTacL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_F_CONG_TAC_CODE);

            f_HocSAGSL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_HOC_SAGS_CODE);

            f_Hoc1L = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC, Constants.LEAVE_TYPE_HOC_1_CODE);
            f_Hoc2L = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC, Constants.LEAVE_TYPE_HOC_2_CODE);
            f_Hoc3L = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC, Constants.LEAVE_TYPE_HOC_3_CODE);
            f_Hoc4L = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC, Constants.LEAVE_TYPE_HOC_4_CODE);
            f_Hoc5L = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC, Constants.LEAVE_TYPE_HOC_5_CODE);
            f_Hoc6L = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC, Constants.LEAVE_TYPE_HOC_6_CODE);
            f_Hoc7L = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC, Constants.LEAVE_TYPE_HOC_7_CODE);

            f_Con_OmL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_CON_OM_CODE);

            f_KHHDSL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC, Constants.LEAVE_TYPE_KHHDS_CODE);

            f_SayThaiL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_SAY_THAI_CODE);

            f_KhamThaiL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_KHAM_THAI_CODE);

            f_ConChetL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_CON_CHET_SAU_KHI_SINH_CODE);

            f_DinhChiCongTacL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_DINH_CHI_CONG_TAC_CODE);

            f_TamHoanHDL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_TAM_HOAN_HOP_DONG_CODE);

            f_HoiHopL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                Constants.LEAVE_TYPE_HOI_HOP_CODE);

            f_LeL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC, Constants.LEAVE_TYPE_LE_TET_CODE);

            XL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC, Constants.LEAVE_TYPE_X_CODE);

            #endregion

            if (CheckContractXDTH(objEC))
            {
                LCBLog += i + ".Hợp Đồng : " + objEC.ContractTypeCode + "<br/>";
                var finalHeSo = ObjCoefficientEmployeeFinalBLL.LCB;
                LCBLog += "finalHeSo = HeSoLCB:" + ObjCoefficientEmployeeFinalBLL.LCB + "=" + finalHeSo + "<br/>";
                LCBLog += "LCB =0 <br/>";

                var luongMotNgay = finalHeSo*DON_GIA_LCB/ObjWorkdayEmployeesBLLL.XQDL;
                LCBLog += "luongMotNgay = (finalHeSo:" + finalHeSo + " x DonGiaLCB:" + DON_GIA_LCB + ")/XQD:" +
                          ObjWorkdayEmployeesBLLL.XQDL + "=" + luongMotNgay + "<br/>";


                if (f_DinhChiCongTacL > 0)
                {
                    LCBLog += "LCBTemp = LCBTemp:" + LCBValueReturn;
                    LCBValueReturn += luongMotNgay*f_DinhChiCongTacL*0.50;
                    LCBLog += " + (luongMotNgay : " + luongMotNgay + " x DC:" + f_DinhChiCongTacL + " x 0.50 )=" +
                              LCBValueReturn + "<br/>";
                }
            }

            return LCBValueReturn;
        }

        #endregion
    }
}