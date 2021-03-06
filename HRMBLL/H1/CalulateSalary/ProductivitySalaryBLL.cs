using System;
using System.Collections.Generic;
using HRMBLL.H0;
using HRMBLL.H1.Helper;
using HRMUtil;

namespace HRMBLL.H1.CalulateSalary
{
    public class ProductivitySalaryBLL
    {
        private string TienThuongLog = string.Empty;

        public string Perod_II_Log { get; set; } = string.Empty;

        public double FinalConversionLNSCoefficient { get; set; }

        public string FinalConversionLNSCoefficientLog { get; set; } = string.Empty;

        //// Tien Thuong 
        //// cong thuc giong LNS , don gia thay doi moi thang, phai them noi dinh nghia no
        /// <summary>
        ///     Tinh luong nang suat
        /// </summary>
        /// <returns></returns>
        public void CalculatePerod_II()
        {
            #region TINH LUONG NANG SUAT VA TIEN THUONG

            var K = DefaultValues.K(ObjWorkdayEmployeesBLLL.MarkL);

            if (ListEmployeeContractBLLCheck.Count > 1)
            {
                IsChangeContract = 1;

                if (CheckContractXDTH(ListEmployeeContractBLLCheck[0]) &&
                    CheckContractXDTH(ListEmployeeContractBLLCheck[1]))
                {
                    ValueLNS = CalculateOnlyOneContractXDTHInMonth(K);
                    ValueTienThuong = CalculateBonusOnlyOneContractInMonth(K);
                }
                else
                {
                    ////////////////////////////////////////////////////////////////////////////////////////////////
                    ///////// Tinh luong cho truong hop lien quan den nhieu hop dong trong 1 thang                                        
                    for (var i = 0; i < ListEmployeeContractBLLCheck.Count; i++)
                    {
                        var objOldContract = ListEmployeeContractBLLCheck[i];
                        if (!CheckContractShortTerm(objOldContract))
                        {
                            ValueLNS += CalculateMultiContractXDTHInMonth(objOldContract, i, K);
                            ValueTienThuong += CalculateBonusMultiContractsInMonth(objOldContract, i, K);
                        }
                    }
                    LNSLog += "LNS = " + ValueLNS;
                    TienThuongLog += "TienThuong = " + ValueTienThuong;
                }
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            //// Tinh luong cho truong hop chi co lien quan toi 1 hop dong trong 1 thang

            else
            {
                ValueLNS = CalculateOnlyOneContractXDTHInMonth(K);
                ValueTienThuong = CalculateBonusOnlyOneContractInMonth(K);
            }

            #endregion

            Perod_II_Log +=
                "<br/>/************* TINH LUONG NANG SUAT **************************************************/<br/>";
            Perod_II_Log += LNSLog;
            Perod_II_Log +=
                "<br/>/************* TINH TIEN THUONG **************************************************/<br/>";
            Perod_II_Log += TienThuongLog;
        }

        public void CalculateShortTermSalary()
        {
            #region TINH LUONG NANG SUAT NGAN HAN

            var K = DefaultValues.K(ObjWorkdayEmployeesBLLL.MarkL);

            if (ListEmployeeContractBLLCheck.Count > 1)
            {
                ////////////////////////////////////////////////////////////////////////////////////////////////
                ///////// Tinh luong cho truong hop lien quan den nhieu hop dong trong 1 thang
                LNSLog += "Trường hợp này có chuyển đổi hợp đồng trong tháng :<br/>";
                for (var i = 0; i < ListEmployeeContractBLLCheck.Count; i++)
                {
                    var objOldContract = ListEmployeeContractBLLCheck[i];
                    ValueLNSSortTerm += CalculateMultiContractKoPhaiXDTHInMonth(objOldContract, i, K);
                }
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            //// Tinh luong cho truong hop chi co lien quan toi 1 hop dong trong 1 thang

            else
            {
                /*Tinh luong truong hop khong phai hop dong xac dinh thoi han
                 * hop dong hoc nghe khong tinh he so HTCV (Lienntb, ngay 20/5/2008 chay luong thang 5)
                 */
                LNSLog += "Trường hợp này chỉ có 1 hợp đồng trong tháng :<br/>";
                IsChangeContract = 0;
                ValueLNSSortTerm = CalculateOnlyOneContractKoPhaiXDTHInMonth(ListEmployeeContractBLLCheck[0], K);
            }
            LNSLog += "LNS = " + ValueLNSSortTerm + "<br/>";

            #endregion
        }

        #region private fields

        #endregion

        #region properties

        public WorkdayEmployeesBLLL ObjWorkdayEmployeesBLLL { get; set; }

        public CoefficientEmployeeFinalBLL ObjCoefficientEmployeeFinalBLL { get; set; }

        public List<EmployeeContractBLL> ListEmployeeContractBLLCheck { get; set; }

        public double ValueLNS { get; set; }

        public double ValueLNSSortTerm { get; set; }

        public double ValueTienThuong { get; set; }

        /// <summary>
        ///     Don gia luong nang suat
        /// </summary>
        public double DON_GIA_LNS { get; set; }

        /// <summary>
        ///     Don gia luong can ban
        /// </summary>
        public double DON_GIA_LCB { get; set; }

        /// <summary>
        ///     Don gia luong can ban
        /// </summary>
        public double DON_GIA_TIEN_THUONG { get; set; }

        public int UserId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public string LNSLog { get; set; } = string.Empty;

        public int IsChangeContract { get; set; }

        public string Remark { get; set; }

        public double HeSoLNS { get; set; }

        #endregion

        #region tinh luong dot II

        #region Tinh Luong Nang Suat

        private double CalculateOnlyOneContractXDTHInMonth(double K)
        {
            double LNSValueReturn = 0;
            if (ObjWorkdayEmployeesBLLL.XL == ObjWorkdayEmployeesBLLL.XQDL)
            {
                // Nếu ngày công lam viec = ngày công QĐ trong tháng
                // lns = heso_LNS * heso_K * DonGia

                LNSLog += " X = XQĐ = " + ObjWorkdayEmployeesBLLL.XL + "<br/>";

                var finalHeSoLNS = ObjCoefficientEmployeeFinalBLL.LNS + ObjCoefficientEmployeeFinalBLL.LNSPCTN;
                LNSLog += " finalHeSoLNS = HeSoLNS:" + ObjCoefficientEmployeeFinalBLL.LNS + " + HeSoLNSPCTN:" +
                          ObjCoefficientEmployeeFinalBLL.LNSPCTN + "<br/>";

                LNSValueReturn = finalHeSoLNS*DON_GIA_LNS;
                LNSLog += "LNSTemp= finalHeSoLNS:" + finalHeSoLNS + " * DonGiaLNS:" + DON_GIA_LNS + "=" + LNSValueReturn +
                          "<br/>";
                LNSLog += "LNS= LNSTemp:" + LNSValueReturn;
                LNSValueReturn = LNSValueReturn*K;
                LNSLog += "* K:" + K + "=" + LNSValueReturn + "<br/>";

                //--------------------------------------
                FinalConversionLNSCoefficient = finalHeSoLNS*K*ObjWorkdayEmployeesBLLL.XL;
                FinalConversionLNSCoefficientLog = "ChiSoPBQD = finalHeSoLNS:" + finalHeSoLNS + " x K:" + K + " x X:" +
                                                   ObjWorkdayEmployeesBLLL.XL + " = " + FinalConversionLNSCoefficient;
                //--------------------------------------
            }
            else
            {
                var finalHeSoLNS = ObjCoefficientEmployeeFinalBLL.LNS + ObjCoefficientEmployeeFinalBLL.LNSPCTN;
                LNSLog += " finalHeSoLNS = HeSoLNS:" + ObjCoefficientEmployeeFinalBLL.LNS + " + HeSoLNSPCTN:" +
                          ObjCoefficientEmployeeFinalBLL.LNSPCTN + "=" + finalHeSoLNS + "<br/>";

                var luongMotNgay = finalHeSoLNS*DON_GIA_LNS/ObjWorkdayEmployeesBLLL.XQDL;
                LNSLog += "luongMotNgay= (finalHeSoLNS:" + finalHeSoLNS + " * DonGiaLNS:" + DON_GIA_LNS + ")/XQD:" +
                          ObjWorkdayEmployeesBLLL.XQDL + "=" + luongMotNgay + "<br/>";
                LNSLog += " LNSTemp = 0 <br/>";

                //--------------------------------------
                double ChiSoPBQDTemp = 0;
                double HSLNS_K = 0;
                HSLNS_K = finalHeSoLNS*K;
                FinalConversionLNSCoefficientLog = "HSLNS_K = finalHeSoLNS:" + finalHeSoLNS + " x K:" + K + " = " +
                                                   HSLNS_K + "<br/>";
                //--------------------------------------

                #region Tinh luong cho cac truong hop nghi phep

                if (ObjWorkdayEmployeesBLLL.F_OmL > 0)
                {
                    // Neu nghi tu 30,40,50 : 75% Luong Nang suat, khong tien thuong
                    // huong 50% Luong nang suat trong thoi han 3 thang
                    /// lien quan phan Om dai ngay. phan tinh luong ngay om nay lien quan toi che do BHXH
                    /// 0 -> 15 nam : 30 Om/nam
                    /// 15 - > 25 nam :  40 Om/Nam
                    /// 
                    LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                    LNSValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_OmL*0.75;
                    LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "Ô:" + ObjWorkdayEmployeesBLLL.F_OmL +
                              ")= " + LNSValueReturn + "<br/>";
                    //--------------------------------------
                    FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                    ChiSoPBQDTemp += HSLNS_K*ObjWorkdayEmployeesBLLL.F_OmL*0.75;
                    FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x Ô:" +
                                                        ObjWorkdayEmployeesBLLL.F_OmL + " x 0.75) = " + ChiSoPBQDTemp +
                                                        "<br/>";
                    //--------------------------------------
                }
                if (ObjWorkdayEmployeesBLLL.F_Con_OmL > 0)
                {
                    LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                    LNSValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_Con_OmL*0.75;
                    LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "Co:" + ObjWorkdayEmployeesBLLL.F_Con_OmL +
                              ")= " + LNSValueReturn + "<br/>";
                    //--------------------------------------
                    FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                    ChiSoPBQDTemp += HSLNS_K*ObjWorkdayEmployeesBLLL.F_Con_OmL*0.75;
                    FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x Co:" +
                                                        ObjWorkdayEmployeesBLLL.F_Con_OmL + " x 0.75) = " +
                                                        ChiSoPBQDTemp + "<br/>";
                    //--------------------------------------
                }
                if (ObjWorkdayEmployeesBLLL.F_KHHDSL > 0)
                {
                    LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                    LNSValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_KHHDSL*0.75;
                    LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "KHH:" + ObjWorkdayEmployeesBLLL.F_KHHDSL +
                              ")= " + LNSValueReturn + "<br/>";
                    //--------------------------------------
                    FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                    ChiSoPBQDTemp += HSLNS_K*ObjWorkdayEmployeesBLLL.F_KHHDSL*0.75;
                    FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x KHH:" +
                                                        ObjWorkdayEmployeesBLLL.F_KHHDSL + " x 0.75) = " + ChiSoPBQDTemp +
                                                        "<br/>";
                    //--------------------------------------
                }
                if (ObjWorkdayEmployeesBLLL.F_SayThaiL > 0)
                {
                    LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                    LNSValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_SayThaiL;
                    LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "ST:" + ObjWorkdayEmployeesBLLL.F_SayThaiL +
                              ")= " + LNSValueReturn + "<br/>";
                    //--------------------------------------
                    FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                    ChiSoPBQDTemp += HSLNS_K*ObjWorkdayEmployeesBLLL.F_SayThaiL;
                    FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x ST:" +
                                                        ObjWorkdayEmployeesBLLL.F_SayThaiL + ") = " + ChiSoPBQDTemp +
                                                        "<br/>";
                    //--------------------------------------
                }
                if (ObjWorkdayEmployeesBLLL.F_ThaiSanL > 0)
                {
                    // hưởng 100% lương năng suất, ko thương
                    LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                    LNSValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_ThaiSanL;
                    LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "TS:" + ObjWorkdayEmployeesBLLL.F_ThaiSanL +
                              ")= " + LNSValueReturn + "<br/>";
                    //--------------------------------------
                    FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                    ChiSoPBQDTemp += HSLNS_K*ObjWorkdayEmployeesBLLL.F_ThaiSanL;
                    FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x TS:" +
                                                        ObjWorkdayEmployeesBLLL.F_ThaiSanL + ") = " + ChiSoPBQDTemp +
                                                        "<br/>";
                    //--------------------------------------
                }
                if (ObjWorkdayEmployeesBLLL.F_KhamThaiL > 0)
                {
                    // hưởng 100% lương năng suất, ko thương
                    LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                    LNSValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_KhamThaiL;
                    LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "KT:" + ObjWorkdayEmployeesBLLL.F_KhamThaiL +
                              ")= " + LNSValueReturn + "<br/>";
                    //--------------------------------------
                    FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                    ChiSoPBQDTemp += HSLNS_K*ObjWorkdayEmployeesBLLL.F_KhamThaiL;
                    FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x KT:" +
                                                        ObjWorkdayEmployeesBLLL.F_KhamThaiL + ") = " + ChiSoPBQDTemp +
                                                        "<br/>";
                    //--------------------------------------
                }
                if (ObjWorkdayEmployeesBLLL.F_TNLDL > 0)
                {
                    // hưởng 75% lương năng suất, không có thưởng
                    LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                    LNSValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_TNLDL*0.75;
                    LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "TNLD:" + ObjWorkdayEmployeesBLLL.F_TNLDL +
                              ")= " + LNSValueReturn + "<br/>";
                    //--------------------------------------
                    FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                    ChiSoPBQDTemp += HSLNS_K*ObjWorkdayEmployeesBLLL.F_TNLDL*0.75;
                    FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x TNLD:" +
                                                        ObjWorkdayEmployeesBLLL.F_TNLDL + " x 0.75) = " + ChiSoPBQDTemp +
                                                        "<br/>";
                    //--------------------------------------
                }

                //// Nhóm 1: không liên tục 1 tuần / tháng hoặc cộng dồn dưới 1 tháng/Quý
                ////hưởng 100% lương năng suất, 100% thưởng
                if (ObjWorkdayEmployeesBLLL.F_Hoc1L > 0)
                {
                    LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                    LNSValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_Hoc1L;
                    LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "Ho1:" + ObjWorkdayEmployeesBLLL.F_Hoc1L +
                              ")= " + LNSValueReturn + "<br/>";
                    //--------------------------------------
                    FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                    ChiSoPBQDTemp += HSLNS_K*ObjWorkdayEmployeesBLLL.F_Hoc1L;
                    FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x Ho1:" +
                                                        ObjWorkdayEmployeesBLLL.F_Hoc1L + ") = " + ChiSoPBQDTemp +
                                                        "<br/>";
                    //--------------------------------------
                }
                //// Nhóm 2: liên tục từ 1 tháng đến 2 tháng/năm 
                ////hưởng 100% lương năng suất, 75 % thưởng
                if (ObjWorkdayEmployeesBLLL.F_Hoc2L > 0)
                {
                    LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                    LNSValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_Hoc2L;
                    LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "Ho2:" + ObjWorkdayEmployeesBLLL.F_Hoc2L +
                              ")= " + LNSValueReturn + "<br/>";
                    //--------------------------------------
                    FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                    ChiSoPBQDTemp += HSLNS_K*ObjWorkdayEmployeesBLLL.F_Hoc2L;
                    FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x Ho2:" +
                                                        ObjWorkdayEmployeesBLLL.F_Hoc2L + ") = " + ChiSoPBQDTemp +
                                                        "<br/>";
                    //--------------------------------------
                }
                //// Nhóm 3: liên tục trên 2 tháng đến dưới 12 tháng/năm (trong nước)
                ////hưởng 75% lương năng suất, 50% tiền thưởng
                if (ObjWorkdayEmployeesBLLL.F_Hoc3L > 0)
                {
                    LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                    LNSValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_Hoc3L*0.75;
                    LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "Ho3:" + ObjWorkdayEmployeesBLLL.F_Hoc3L +
                              ")= " + LNSValueReturn + "<br/>";
                    //--------------------------------------
                    FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                    ChiSoPBQDTemp += HSLNS_K*ObjWorkdayEmployeesBLLL.F_Hoc3L*0.75;
                    FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x Ho3:" +
                                                        ObjWorkdayEmployeesBLLL.F_Hoc3L + " x 0.75) = " + ChiSoPBQDTemp +
                                                        "<br/>";
                    //--------------------------------------
                }
                //// Nhóm 4: liên tục trên 2 tháng đến dưới 12 tháng/năm (ngoài nước)
                ////hưởng 50% lương năng suất, 50% tiền thưởng
                if (ObjWorkdayEmployeesBLLL.F_Hoc4L > 0)
                {
                    LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                    LNSValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_Hoc4L*0.50;
                    LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "Ho4:" + ObjWorkdayEmployeesBLLL.F_Hoc4L +
                              ")= " + LNSValueReturn + "<br/>";
                    //--------------------------------------
                    FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                    ChiSoPBQDTemp += HSLNS_K*ObjWorkdayEmployeesBLLL.F_Hoc4L*0.50;
                    FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x Ho4:" +
                                                        ObjWorkdayEmployeesBLLL.F_Hoc4L + " x 0.50) = " + ChiSoPBQDTemp +
                                                        "<br/>";
                    //--------------------------------------
                }
                //// Nhóm 5: liên tục từ 12 tháng/năm trở lên (trong nước)
                ////hưởng 50% lương năng suất, ko tiền thưởng
                if (ObjWorkdayEmployeesBLLL.F_Hoc5L > 0)
                {
                    LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                    LNSValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_Hoc5L*0.50;
                    LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "Ho5:" + ObjWorkdayEmployeesBLLL.F_Hoc5L +
                              ")= " + LNSValueReturn + "<br/>";
                    //--------------------------------------
                    FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                    ChiSoPBQDTemp += HSLNS_K*ObjWorkdayEmployeesBLLL.F_Hoc5L*0.50;
                    FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x Ho5:" +
                                                        ObjWorkdayEmployeesBLLL.F_Hoc5L + " x 0.50) = " + ChiSoPBQDTemp +
                                                        "<br/>";
                    //--------------------------------------
                }
                //// Nhóm 6: liên tục từ 12 tháng/năm trở lên (ngoài nước)
                ////hưởng lương CB, ko lương năng suất, ko tiền thưởng
                //// Nhóm 7: tự đi học theo nguyện vọng
                ////ko hưởng lương, thưởng

                if (ObjWorkdayEmployeesBLLL.F_NamL > 0)
                {
                    // nghi phep nam duoc huong 50% luong nang suat, ko thuong
                    LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                    LNSValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_NamL*0.50;
                    LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "F:" + ObjWorkdayEmployeesBLLL.F_NamL +
                              " x 0.50)= " + LNSValueReturn + "<br/>";
                    //--------------------------------------
                    FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                    ChiSoPBQDTemp += HSLNS_K*ObjWorkdayEmployeesBLLL.F_NamL*0.50;
                    FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x F:" +
                                                        ObjWorkdayEmployeesBLLL.F_NamL + " x 0.50) = " + ChiSoPBQDTemp +
                                                        "<br/>";
                    //--------------------------------------
                }
                if (ObjWorkdayEmployeesBLLL.F_dbL > 0)
                {
                    // phep dac biet : nghỉ có việc riêng (cưới hỏi, ma chay), hưởng nguyên lương
                    // 100% luong, 100% thuong
                    LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                    LNSValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_dbL;
                    LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "Fdb:" + ObjWorkdayEmployeesBLLL.F_dbL +
                              ")= " + LNSValueReturn + "<br/>";
                    //--------------------------------------
                    FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                    ChiSoPBQDTemp += HSLNS_K*ObjWorkdayEmployeesBLLL.F_dbL;
                    FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x Fdb:" +
                                                        ObjWorkdayEmployeesBLLL.F_dbL + ") = " + ChiSoPBQDTemp + "<br/>";
                    //--------------------------------------
                }
                if (ObjWorkdayEmployeesBLLL.F_CongTacL > 0)
                {
                    // ngay cong di cong tac huong 100 LNS, thuong
                    LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                    LNSValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_CongTacL;
                    LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "CT:" + ObjWorkdayEmployeesBLLL.F_CongTacL +
                              ")= " + LNSValueReturn + "<br/>";
                    //--------------------------------------
                    FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                    ChiSoPBQDTemp += HSLNS_K*ObjWorkdayEmployeesBLLL.F_CongTacL;
                    FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x CT:" +
                                                        ObjWorkdayEmployeesBLLL.F_CongTacL + ") = " + ChiSoPBQDTemp +
                                                        "<br/>";
                    //--------------------------------------
                }

                if (ObjWorkdayEmployeesBLLL.F_LeL > 0)
                {
                    // ngay cong di cong tac huong 100 LNS, thuong
                    LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                    LNSValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.F_LeL;
                    LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "LE:" + ObjWorkdayEmployeesBLLL.F_LeL +
                              ")= " + LNSValueReturn + "<br/>";
                    //--------------------------------------
                    FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                    ChiSoPBQDTemp += HSLNS_K*ObjWorkdayEmployeesBLLL.F_LeL;
                    FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x LE:" +
                                                        ObjWorkdayEmployeesBLLL.F_LeL + ") = " + ChiSoPBQDTemp + "<br/>";
                    //--------------------------------------
                }

                if (ObjWorkdayEmployeesBLLL.XL > 0)
                {
                    LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                    LNSValueReturn += luongMotNgay*ObjWorkdayEmployeesBLLL.XL;
                    LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "X:" + ObjWorkdayEmployeesBLLL.XL + ")= " +
                              LNSValueReturn + "<br/>";
                    //--------------------------------------
                    FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                    ChiSoPBQDTemp += HSLNS_K*ObjWorkdayEmployeesBLLL.XL;
                    FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x X:" + ObjWorkdayEmployeesBLLL.XL +
                                                        ") = " + ChiSoPBQDTemp + "<br/>";
                    //--------------------------------------
                }

                #endregion

                LNSValueReturn = LNSValueReturn*K;
                LNSLog += " LNS = LNSTemp x K :" + K + " = " + LNSValueReturn + "<br/>";

                //--------------------------------------
                FinalConversionLNSCoefficient = ChiSoPBQDTemp;
                FinalConversionLNSCoefficientLog += "ChiSoPBQD = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                //--------------------------------------
            }
            return LNSValueReturn;
        }

        private double CalculateMultiContractXDTHInMonth(EmployeeContractBLL objEC, int i, double K)
        {
            double LNSValueReturn = 0;

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

            var ObjCE = LNSCoefficientEmployeeBLL.GetByUserIdDate(objEC.UserId, objEC.FromDate);

            var finalHeSoLNS = ObjCE.LNS + ObjCE.LNSPCTN;

            LNSLog += " finalHeSoLNS = HeSoLNS:" + ObjCE.LNS + " + HeSoLNSPCTN:" + ObjCE.LNSPCTN + " = " + finalHeSoLNS +
                      "<br/>";

            LNSLog += "HĐ thứ " + i + " : " + objEC.ContractTypeCode + "<br/>";

            var luongMotNgay = finalHeSoLNS*DON_GIA_LNS/ObjWorkdayEmployeesBLLL.XQDL;

            LNSLog += "luongMotNgay = (finalHeSoLNS: " + finalHeSoLNS + " * DonGiaLNS : " + DON_GIA_LNS + ")/XQD:" +
                      ObjWorkdayEmployeesBLLL.XQDL + " = " + luongMotNgay + "<br/>";

            //--------------------------------------
            double ChiSoPBQDTemp = 0;
            double HSLNS_K = 0;
            HSLNS_K = finalHeSoLNS*K;
            FinalConversionLNSCoefficientLog = "HSLNS_K = finalHeSoLNS:" + finalHeSoLNS + " x K:" + K + " = " + HSLNS_K +
                                               "<br/>";
            FinalConversionLNSCoefficientLog += "HĐ thứ " + i + " : " + objEC.ContractTypeCode + "<br/>";
            //--------------------------------------

            #region Tinh luong nang suat cho cac truong hop nghi phep

            if (f_OmL > 0)
            {
                LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                LNSValueReturn += luongMotNgay*f_OmL*0.75;
                LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "Ô:" + f_OmL + ")= " + LNSValueReturn + "<br/>";
                //--------------------------------------
                FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                ChiSoPBQDTemp += HSLNS_K*f_OmL*0.75;
                FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x Ô:" + f_OmL + " x 0.75) = " +
                                                    ChiSoPBQDTemp + "<br/>";
                //--------------------------------------
            }
            if (f_Con_OmL > 0)
            {
                LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                LNSValueReturn += luongMotNgay*f_Con_OmL*0.75;
                LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "Co:" + f_Con_OmL + ")= " + LNSValueReturn +
                          "<br/>";
                //--------------------------------------
                FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                ChiSoPBQDTemp += HSLNS_K*f_Con_OmL*0.75;
                FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x Co:" + f_Con_OmL + " x 0.75) = " +
                                                    ChiSoPBQDTemp + "<br/>";
                //--------------------------------------
            }
            if (f_KHHDSL > 0)
            {
                LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                LNSValueReturn += luongMotNgay*f_KHHDSL*0.75;
                LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "KHH:" + f_KHHDSL + ")= " + LNSValueReturn +
                          "<br/>";
                //--------------------------------------
                FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                ChiSoPBQDTemp += HSLNS_K*f_KHHDSL*0.75;
                FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x KHH:" + f_KHHDSL + " x 0.75) = " +
                                                    ChiSoPBQDTemp + "<br/>";
                //--------------------------------------
            }
            if (f_SayThaiL > 0)
            {
                LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                LNSValueReturn += luongMotNgay*f_SayThaiL;
                LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "ST:" + f_SayThaiL + ")= " + LNSValueReturn +
                          "<br/>";
                //--------------------------------------
                FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                ChiSoPBQDTemp += HSLNS_K*f_SayThaiL;
                FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x ST:" + f_SayThaiL + ") = " +
                                                    ChiSoPBQDTemp + "<br/>";
                //--------------------------------------
            }
            if (f_ThaiSanL > 0)
            {
                // hưởng 100% lương năng suất, ko thương
                LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                LNSValueReturn += luongMotNgay*f_ThaiSanL;
                LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "TS:" + f_ThaiSanL + ")= " + LNSValueReturn +
                          "<br/>";
                //--------------------------------------
                FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                ChiSoPBQDTemp += HSLNS_K*f_ThaiSanL;
                FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x TS:" + f_ThaiSanL + ") = " +
                                                    ChiSoPBQDTemp + "<br/>";
                //--------------------------------------
            }
            if (f_TNLDL > 0)
            {
                // hưởng 75% lương năng suất, không có thưởng
                LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                LNSValueReturn += luongMotNgay*f_TNLDL*0.75;
                LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "TNLD:" + f_ThaiSanL + ")= " + LNSValueReturn +
                          "<br/>";
                //--------------------------------------
                FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                ChiSoPBQDTemp += HSLNS_K*f_TNLDL*0.75;
                FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x TNLD:" + f_TNLDL + " x 0.75) = " +
                                                    ChiSoPBQDTemp + "<br/>";
                //--------------------------------------
            }

            //// Nhóm 1: không liên tục 1 tuần / tháng hoặc cộng dồn dưới 1 tháng/Quý
            ////hưởng 100% lương năng suất, 100% thưởng
            if (f_Hoc1L > 0)
            {
                LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                LNSValueReturn += luongMotNgay*f_Hoc1L;
                LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "Ho1:" + f_Hoc1L + ")= " + LNSValueReturn +
                          "<br/>";
                //--------------------------------------
                FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                ChiSoPBQDTemp += HSLNS_K*f_Hoc1L;
                FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x TNLD:" + f_Hoc1L + ") = " +
                                                    ChiSoPBQDTemp + "<br/>";
                //--------------------------------------
            }
            //// Nhóm 2: liên tục từ 1 tháng đến 2 tháng/năm 
            ////hưởng 100% lương năng suất, 75 % thưởng
            if (f_Hoc2L > 0)
            {
                LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                LNSValueReturn += luongMotNgay*f_Hoc2L;
                LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "Ho2:" + f_Hoc2L + ")= " + LNSValueReturn +
                          "<br/>";
                //--------------------------------------
                FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                ChiSoPBQDTemp += HSLNS_K*f_Hoc2L;
                FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x Ho2:" + f_Hoc2L + ") = " +
                                                    ChiSoPBQDTemp + "<br/>";
                //--------------------------------------
            }
            //// Nhóm 3: liên tục trên 2 tháng đến dưới 12 tháng/năm (trong nước)
            ////hưởng 75% lương năng suất, 50% tiền thưởng
            if (f_Hoc3L > 0)
            {
                LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                LNSValueReturn += luongMotNgay*f_Hoc3L*0.75;
                LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "Ho3:" + f_Hoc3L + " x 0.75)= " +
                          LNSValueReturn + "<br/>";
                //--------------------------------------
                FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                ChiSoPBQDTemp += HSLNS_K*f_Hoc3L*0.75;
                FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x Ho3:" + f_Hoc3L + " x 0.75) = " +
                                                    ChiSoPBQDTemp + "<br/>";
                //--------------------------------------
            }
            //// Nhóm 4: liên tục trên 2 tháng đến dưới 12 tháng/năm (ngoài nước)
            ////hưởng 50% lương năng suất, 50% tiền thưởng
            if (f_Hoc4L > 0)
            {
                LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                LNSValueReturn += luongMotNgay*f_Hoc4L*0.50;
                LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "Ho4:" + f_Hoc4L + " x 0.50)= " +
                          LNSValueReturn + "<br/>";
                //--------------------------------------
                FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                ChiSoPBQDTemp += HSLNS_K*f_Hoc4L*0.50;
                FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x Ho4:" + f_Hoc4L + " x 0.50) = " +
                                                    ChiSoPBQDTemp + "<br/>";
                //--------------------------------------
            }
            //// Nhóm 5: liên tục từ 12 tháng/năm trở lên (trong nước)
            ////hưởng 50% lương năng suất, ko tiền thưởng
            if (f_Hoc5L > 0)
            {
                LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                LNSValueReturn += luongMotNgay*f_Hoc5L*0.50;
                LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "Ho5:" + f_Hoc5L + " x 0.50)= " +
                          LNSValueReturn + "<br/>";
                //--------------------------------------
                FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                ChiSoPBQDTemp += HSLNS_K*f_Hoc5L*0.50;
                FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x Ho5:" + f_Hoc5L + " x 0.50) = " +
                                                    ChiSoPBQDTemp + "<br/>";
                //--------------------------------------
            }
            //// Nhóm 6: liên tục từ 12 tháng/năm trở lên (ngoài nước)
            ////hưởng lương CB, ko lương năng suất, ko tiền thưởng
            //// Nhóm 7: tự đi học theo nguyện vọng
            ////ko hưởng lương, thưởng


            if (f_NamL > 0)
            {
                // nghi phep nam duoc huong 50% luong nang suat, ko thuong
                LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                LNSValueReturn += luongMotNgay*f_NamL*0.50;
                LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "F:" + f_NamL + " x 0.50)= " + LNSValueReturn +
                          "<br/>";
                //--------------------------------------
                FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                ChiSoPBQDTemp += HSLNS_K*f_NamL*0.50;
                FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x F:" + f_NamL + " x 0.50) = " +
                                                    ChiSoPBQDTemp + "<br/>";
                //--------------------------------------
            }
            if (f_dbL > 0)
            {
                // phep dac biet : nghỉ có việc riêng (cưới hỏi, ma chay), hưởng nguyên lương
                // 100% luong, 100% thuong
                LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                LNSValueReturn += luongMotNgay*f_dbL;
                LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "Fdb:" + f_dbL + " x 0.50)= " + LNSValueReturn +
                          "<br/>";
                //--------------------------------------
                FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                ChiSoPBQDTemp += HSLNS_K*f_dbL*0.50;
                FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x Fdb:" + f_dbL + " x 0.50) = " +
                                                    ChiSoPBQDTemp + "<br/>";
                //--------------------------------------
            }
            if (f_CongTacL > 0)
            {
                // ngay cong di cong tac huong 100 LNS, thuong                    
                LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                LNSValueReturn += luongMotNgay*f_CongTacL;
                LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "CT:" + f_CongTacL + ")= " + LNSValueReturn +
                          "<br/>";
                //--------------------------------------
                FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                ChiSoPBQDTemp += HSLNS_K*f_CongTacL;
                FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x Fdb:" + f_CongTacL + ") = " +
                                                    ChiSoPBQDTemp + "<br/>";
                //--------------------------------------
            }

            if (f_LeL > 0)
            {
                // ngay cong di cong tac huong 100 LNS, thuong
                LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                LNSValueReturn += luongMotNgay*f_LeL;
                LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "LE:" + f_LeL + ")= " + LNSValueReturn +
                          "<br/>";
                //--------------------------------------
                FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                ChiSoPBQDTemp += HSLNS_K*f_LeL;
                FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x LE:" + f_LeL + ") = " + ChiSoPBQDTemp +
                                                    "<br/>";
                //--------------------------------------
            }

            if (XL > 0)
            {
                LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                LNSValueReturn += luongMotNgay*XL;
                LNSLog += " + (luongMotNgay : " + luongMotNgay + " x " + "X:" + XL + ")= " + LNSValueReturn + "<br/>";
                //--------------------------------------
                FinalConversionLNSCoefficientLog += "ChiSoPBQDTemp = ChiSoPBQDTemp:" + ChiSoPBQDTemp;
                ChiSoPBQDTemp += HSLNS_K*XL;
                FinalConversionLNSCoefficientLog += " + (HSLNS_K:" + HSLNS_K + " x X:" + XL + ") = " + ChiSoPBQDTemp +
                                                    "<br/>";
                //--------------------------------------
            }

            LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
            LNSValueReturn = LNSValueReturn*K*(objEC.Wages/100);
            LNSLog += " x K:" + K + " x (MucHuong:" + objEC.Wages + "/100)=" + LNSValueReturn + "<br/>";


            //--------------------------------------
            FinalConversionLNSCoefficientLog += "FinalConversionLNSCoefficient = FinalConversionLNSCoefficient:" +
                                                FinalConversionLNSCoefficient;
            FinalConversionLNSCoefficient += ChiSoPBQDTemp*(objEC.Wages/100);
            FinalConversionLNSCoefficientLog += " + (ChiSoPBQDTemp:" + ChiSoPBQDTemp + " x (" + objEC.Wages +
                                                "/100)) = " + ChiSoPBQDTemp + "<br/>";
            //--------------------------------------

            #endregion

            return LNSValueReturn;
        }

        #endregion

        #region Tinh Tien Thuong

        /*
         * Tien thuong chi duoc tinh nguoi do di lam tron thang ngay nghi phep nam trong quy che
         * va hop dong phai la XDTH 12, 36 thang va KoXDTH
         */

        private double CalculateBonusOnlyOneContractInMonth(double K)
        {
            double valueTienThuongMotNgay = 0;
            double valueTienThuongReturn = 0;
            if (DefaultValues.IsBonus(DefaultValues.HTCV(ObjWorkdayEmployeesBLLL.MarkL)))
                if (CheckContractXDTH(ListEmployeeContractBLLCheck[0]))
                    if (ObjWorkdayEmployeesBLLL.XL == ObjWorkdayEmployeesBLLL.XQDL)
                    {
                        // Nếu ngày công lam viec = ngày công QĐ trong tháng
                        // lns = heso_LNS * heso_K * DonGia

                        LNSLog += " X = XQĐ = " + ObjWorkdayEmployeesBLLL.XL + "<br/>";

                        var finalHeSoLNS = ObjCoefficientEmployeeFinalBLL.LNS + ObjCoefficientEmployeeFinalBLL.LNSPCTN;
                        LNSLog += " finalHeSoLNS = HeSoLNS:" + ObjCoefficientEmployeeFinalBLL.LNS + " + HeSoLNSPCTN:" +
                                  ObjCoefficientEmployeeFinalBLL.LNSPCTN + "<br/>";

                        ValueTienThuong = finalHeSoLNS*DON_GIA_TIEN_THUONG;
                        TienThuongLog += "TienThuongTemp= finalHeSoLNS:" + finalHeSoLNS + " * DonGiaTienThuong:" +
                                         DON_GIA_TIEN_THUONG + "=" + ValueTienThuong + "<br/>";
                        TienThuongLog += "TienThuong= TienThuongTemp:" + ValueTienThuong;
                        ValueTienThuong = ValueTienThuong*K;
                        TienThuongLog += "* K:" + K + "=" + ValueTienThuong + "<br/>";
                        //--------------------------------------
                        FinalConversionLNSCoefficient = finalHeSoLNS*K*ObjWorkdayEmployeesBLLL.XL;
                        FinalConversionLNSCoefficientLog = "ChiSoPBQD = finalHeSoLNS:" + finalHeSoLNS + " x K:" + K +
                                                           " x X:" + ObjWorkdayEmployeesBLLL.XL + " = " +
                                                           FinalConversionLNSCoefficient;
                        //--------------------------------------
                    }
                    else
                    {
                        if (ObjWorkdayEmployeesBLLL.ChuaDiLamL <= 0)
                        {
                            var finalHeSoLNS = ObjCoefficientEmployeeFinalBLL.LNS +
                                               ObjCoefficientEmployeeFinalBLL.LNSPCTN;
                            TienThuongLog += " finalHeSoLNS = HeSoLNS:" + ObjCoefficientEmployeeFinalBLL.LNS + "+" +
                                             ObjCoefficientEmployeeFinalBLL.LNSPCTN + "=" + finalHeSoLNS + "<br/>";

                            valueTienThuongMotNgay = finalHeSoLNS*DON_GIA_TIEN_THUONG/ObjWorkdayEmployeesBLLL.XQDL;
                            TienThuongLog += "TienThuongMotNgay = (finalHeSoLNS: " + finalHeSoLNS +
                                             " * DonGiaTienThuong : " + DON_GIA_TIEN_THUONG + ")/XQD:" +
                                             ObjWorkdayEmployeesBLLL.XQDL + " = " + valueTienThuongMotNgay + "<br/>";
                            TienThuongLog += " TienThuongTemp = 0 <br/>";

                            #region Tinh Luong cho cac truong hop nghi phep

                            if (ObjWorkdayEmployeesBLLL.XL > 0)
                            {
                                TienThuongLog += "TienThuongTemp = TienThuongTemp:" + valueTienThuongReturn;
                                valueTienThuongReturn += valueTienThuongMotNgay*ObjWorkdayEmployeesBLLL.XL;
                                TienThuongLog += " + (TienThuongMotNgay : " + valueTienThuongMotNgay + " x " + "X:" +
                                                 ObjWorkdayEmployeesBLLL.XL + ")= " + valueTienThuongReturn + "<br/>";
                            }

                            if (ObjWorkdayEmployeesBLLL.F_LeL > 0)
                            {
                                TienThuongLog += "TienThuongTemp = TienThuongTemp:" + valueTienThuongReturn;
                                valueTienThuongReturn += valueTienThuongMotNgay*ObjWorkdayEmployeesBLLL.F_LeL;
                                TienThuongLog += " + (TienThuongMotNgay : " + valueTienThuongMotNgay + " x " + "LE:" +
                                                 ObjWorkdayEmployeesBLLL.F_LeL + ")= " + valueTienThuongReturn + "<br/>";
                            }
                            if (ObjWorkdayEmployeesBLLL.F_CongTacL > 0)
                            {
                                TienThuongLog += "TienThuongTemp = TienThuongTemp:" + valueTienThuongReturn;
                                valueTienThuongReturn += valueTienThuongMotNgay*ObjWorkdayEmployeesBLLL.F_CongTacL;
                                TienThuongLog += " + (TienThuongMotNgay : " + valueTienThuongMotNgay + " x " + "CT:" +
                                                 ObjWorkdayEmployeesBLLL.F_CongTacL + ")= " + valueTienThuongReturn +
                                                 "<br/>";
                            }
                            if (ObjWorkdayEmployeesBLLL.F_dbL > 0)
                            {
                                TienThuongLog += "TienThuongTemp = TienThuongTemp:" + valueTienThuongReturn;
                                valueTienThuongReturn += valueTienThuongMotNgay*ObjWorkdayEmployeesBLLL.F_dbL;
                                TienThuongLog += " + (TienThuongMotNgay : " + valueTienThuongMotNgay + " x " + "Fdb:" +
                                                 ObjWorkdayEmployeesBLLL.F_dbL + ")= " + valueTienThuongReturn + "<br/>";
                            }
                            if (ObjWorkdayEmployeesBLLL.F_HoiHopL > 0)
                            {
                                TienThuongLog += "TienThuongTemp = TienThuongTemp:" + valueTienThuongReturn;
                                valueTienThuongReturn += valueTienThuongMotNgay*ObjWorkdayEmployeesBLLL.F_HoiHopL;
                                TienThuongLog += " + (TienThuongMotNgay : " + valueTienThuongMotNgay + " x " + "HH:" +
                                                 ObjWorkdayEmployeesBLLL.F_HoiHopL + ")= " + valueTienThuongReturn +
                                                 "<br/>";
                            }
                            if (ObjWorkdayEmployeesBLLL.F_Hoc1L > 0)
                            {
                                TienThuongLog += "TienThuongTemp = TienThuongTemp:" + valueTienThuongReturn;
                                valueTienThuongReturn += valueTienThuongMotNgay*ObjWorkdayEmployeesBLLL.F_Hoc1L;
                                TienThuongLog += " + (TienThuongMotNgay : " + valueTienThuongMotNgay + " x " + "Ho1:" +
                                                 ObjWorkdayEmployeesBLLL.F_Hoc1L + ")= " + valueTienThuongReturn +
                                                 "<br/>";
                            }
                            if (ObjWorkdayEmployeesBLLL.F_Hoc2L > 0)
                            {
                                TienThuongLog += "TienThuongTemp = TienThuongTemp:" + valueTienThuongReturn;
                                valueTienThuongReturn += valueTienThuongMotNgay*ObjWorkdayEmployeesBLLL.F_Hoc2L*0.75;
                                TienThuongLog += " + (TienThuongMotNgay : " + valueTienThuongMotNgay + " x " + "Ho2:" +
                                                 ObjWorkdayEmployeesBLLL.F_Hoc2L + ")= " + valueTienThuongReturn +
                                                 "<br/>";
                            }
                            if (ObjWorkdayEmployeesBLLL.F_Hoc3L > 0)
                            {
                                TienThuongLog += "TienThuongTemp = TienThuongTemp:" + valueTienThuongReturn;
                                valueTienThuongReturn += valueTienThuongMotNgay*ObjWorkdayEmployeesBLLL.F_Hoc3L*0.50;
                                TienThuongLog += " + (TienThuongMotNgay : " + valueTienThuongMotNgay + " x " + "Ho3:" +
                                                 ObjWorkdayEmployeesBLLL.F_Hoc3L + ")= " + valueTienThuongReturn +
                                                 "<br/>";
                            }
                            if (ObjWorkdayEmployeesBLLL.F_Hoc4L > 0)
                            {
                                TienThuongLog += "TienThuongTemp = TienThuongTemp:" + valueTienThuongReturn;
                                valueTienThuongReturn += valueTienThuongMotNgay*ObjWorkdayEmployeesBLLL.F_Hoc4L*0.50;
                                TienThuongLog += " + (TienThuongMotNgay : " + valueTienThuongMotNgay + " x " + "Ho4:" +
                                                 ObjWorkdayEmployeesBLLL.F_Hoc4L + ")= " + valueTienThuongReturn +
                                                 "<br/>";
                            }

                            #endregion

                            valueTienThuongReturn = valueTienThuongReturn*K;
                            TienThuongLog += " TienThuong = TienThuongTemp x K :" + K + " = " + valueTienThuongReturn +
                                             "<br/>";
                        }
                    }
            return valueTienThuongReturn;
        }

        private double CalculateBonusMultiContractsInMonth(EmployeeContractBLL objEC, int i, double K)
        {
            double valueTienThuongMotNgay = 0;
            double valueTienThuongReturn = 0;

            var contractType = objEC.ContractTypeId;
            if (DefaultValues.IsBonus(DefaultValues.HTCV(ObjWorkdayEmployeesBLLL.MarkL)))
                if ((contractType == Constants.HopDong_XÐTH_12T) || (contractType == Constants.HopDong_XÐTH_24T)
                    || (contractType == Constants.HopDong_XÐTH_36T) || (contractType == Constants.HopDong_KoXÐTH))
                {
                    #region declare variable

                    double f_OmL = Constants.WorkdayEmployee_DefaultValue,
                        f_OmDaiNgayL = Constants.WorkdayEmployee_DefaultValue;
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

                    double f_Con_OmL = Constants.WorkdayEmployee_DefaultValue,
                        f_KHHDSL = Constants.WorkdayEmployee_DefaultValue;
                    double f_SayThaiL = Constants.WorkdayEmployee_DefaultValue,
                        f_KhamThaiL = Constants.WorkdayEmployee_DefaultValue,
                        f_ConChetL = Constants.WorkdayEmployee_DefaultValue;
                    double f_DinhChiCongTacL = Constants.WorkdayEmployee_DefaultValue,
                        f_TamHoanHDL = Constants.WorkdayEmployee_DefaultValue,
                        f_HoiHopL = Constants.WorkdayEmployee_DefaultValue;
                    double XL = 0, f_LeL = Constants.WorkdayEmployee_DefaultValue;
                    var chuaDiLamL = Constants.WorkdayEmployee_DefaultValue;

                    #endregion

                    #region Calculate days

                    f_OmL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                        Constants.LEAVE_TYPE_O_BAN_THAN_CODE);

                    f_OmDaiNgayL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                        Constants.LEAVE_TYPE_O_DAI_NGAY_CODE);


                    f_ThaiSanL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                        Constants.LEAVE_TYPE_THAI_SAN_CODE);

                    f_TNLDL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                        Constants.LEAVE_TYPE_TNLD_CODE);

                    f_NamL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                        Constants.LEAVE_TYPE_F_NAM_CODE);

                    f_dbL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                        Constants.LEAVE_TYPE_FDB_CODE);

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

                    f_Hoc1L = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                        Constants.LEAVE_TYPE_HOC_1_CODE);
                    f_Hoc2L = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                        Constants.LEAVE_TYPE_HOC_2_CODE);
                    f_Hoc3L = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                        Constants.LEAVE_TYPE_HOC_3_CODE);
                    f_Hoc4L = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                        Constants.LEAVE_TYPE_HOC_4_CODE);
                    f_Hoc5L = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                        Constants.LEAVE_TYPE_HOC_5_CODE);
                    f_Hoc6L = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                        Constants.LEAVE_TYPE_HOC_6_CODE);
                    f_Hoc7L = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                        Constants.LEAVE_TYPE_HOC_7_CODE);

                    f_Con_OmL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                        Constants.LEAVE_TYPE_CON_OM_CODE);

                    f_KHHDSL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                        Constants.LEAVE_TYPE_KHHDS_CODE);

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

                    f_LeL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                        Constants.LEAVE_TYPE_LE_TET_CODE);

                    XL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC, Constants.LEAVE_TYPE_X_CODE);

                    chuaDiLamL = DefaultValues.CountDayByContract(ObjWorkdayEmployeesBLLL, objEC,
                        Constants.LEAVE_TYPE_CHUA_DI_LAM_CODE);

                    #endregion

                    if (chuaDiLamL <= 0)
                    {
                        var ObjCE = LNSCoefficientEmployeeBLL.GetByUserIdDate(objEC.UserId, objEC.FromDate);

                        var finalHeSoLNS = ObjCE.LNS + ObjCE.LNSPCTN;
                        TienThuongLog += " finalHeSoLNS = HeSoLNS:" + ObjCE.LNS + "+" + ObjCE.LNSPCTN + "=" +
                                         finalHeSoLNS + "<br/>";

                        TienThuongLog += "HĐ thứ : " + i + " : " + objEC.ContractTypeCode + "<br/>";

                        #region Tinh Tien thuong cho tung truong hop

                        valueTienThuongMotNgay = finalHeSoLNS*DON_GIA_TIEN_THUONG/ObjWorkdayEmployeesBLLL.XQDL;

                        TienThuongLog += " TienThuongMotNgay = (finalHeSoLNS: " + finalHeSoLNS + " x DonGiaTienThuong:" +
                                         DON_GIA_TIEN_THUONG + ")/XQD:" + ObjWorkdayEmployeesBLLL.XQDL + " = " +
                                         valueTienThuongMotNgay + "<br/>";
                        TienThuongLog += " TienThuongTemp = 0 <br/>";

                        if (XL > 0)
                        {
                            TienThuongLog += "TienThuongTemp = TienThuongTemp:" + valueTienThuongReturn;
                            valueTienThuongReturn += valueTienThuongMotNgay*XL;
                            TienThuongLog += " + (TienThuongMotNgay : " + valueTienThuongMotNgay + " x " + "X:" + XL +
                                             ")= " + valueTienThuongReturn + "<br/>";
                        }

                        if (f_LeL > 0)
                        {
                            TienThuongLog += "TienThuongTemp = TienThuongTemp:" + valueTienThuongReturn;
                            valueTienThuongReturn += valueTienThuongMotNgay*f_LeL;
                            TienThuongLog += " + (TienThuongMotNgay : " + valueTienThuongMotNgay + " x " + "LE:" + f_LeL +
                                             ")= " + valueTienThuongReturn + "<br/>";
                        }
                        if (f_CongTacL > 0)
                        {
                            TienThuongLog += "TienThuongTemp = TienThuongTemp:" + valueTienThuongReturn;
                            valueTienThuongReturn += valueTienThuongMotNgay*f_CongTacL;
                            TienThuongLog += " + (TienThuongMotNgay : " + valueTienThuongMotNgay + " x " + "CT:" +
                                             f_CongTacL + ")= " + valueTienThuongReturn + "<br/>";
                        }
                        if (f_dbL > 0)
                        {
                            TienThuongLog += "TienThuongTemp = TienThuongTemp:" + valueTienThuongReturn;
                            valueTienThuongReturn += valueTienThuongMotNgay*f_dbL;
                            TienThuongLog += " + (TienThuongMotNgay : " + valueTienThuongMotNgay + " x " + "Fdb:" +
                                             f_dbL + ")= " + valueTienThuongReturn + "<br/>";
                        }
                        if (f_HoiHopL > 0)
                        {
                            TienThuongLog += "TienThuongTemp = TienThuongTemp:" + valueTienThuongReturn;
                            valueTienThuongReturn += valueTienThuongMotNgay*f_HoiHopL;
                            TienThuongLog += " + (TienThuongMotNgay : " + valueTienThuongMotNgay + " x " + "HH:" +
                                             f_HoiHopL + ")= " + valueTienThuongReturn + "<br/>";
                        }
                        if (f_Hoc1L > 0)
                        {
                            TienThuongLog += "TienThuongTemp = TienThuongTemp:" + valueTienThuongReturn;
                            valueTienThuongReturn += valueTienThuongMotNgay*f_Hoc1L;
                            TienThuongLog += " + (TienThuongMotNgay : " + valueTienThuongMotNgay + " x " + "Ho1:" +
                                             f_Hoc1L + ")= " + valueTienThuongReturn + "<br/>";
                        }
                        if (f_Hoc2L > 0)
                        {
                            TienThuongLog += "TienThuongTemp = TienThuongTemp:" + valueTienThuongReturn;
                            valueTienThuongReturn += valueTienThuongMotNgay*f_Hoc2L*0.75;
                            TienThuongLog += " + (TienThuongMotNgay : " + valueTienThuongMotNgay + " x " + "Ho2:" +
                                             f_Hoc2L + " x 0.75)= " + valueTienThuongReturn + "<br/>";
                        }
                        if (f_Hoc3L > 0)
                        {
                            TienThuongLog += "TienThuongTemp = TienThuongTemp:" + valueTienThuongReturn;
                            valueTienThuongReturn += valueTienThuongMotNgay*f_Hoc3L*0.50;
                            TienThuongLog += " + (TienThuongMotNgay : " + valueTienThuongMotNgay + " x " + "Ho3:" +
                                             f_Hoc3L + " x 0.50)= " + valueTienThuongReturn + "<br/>";
                        }
                        if (f_Hoc4L > 0)
                        {
                            TienThuongLog += "TienThuongTemp = TienThuongTemp:" + valueTienThuongReturn;
                            valueTienThuongReturn += valueTienThuongMotNgay*f_Hoc4L*0.50;
                            TienThuongLog += " + (TienThuongMotNgay : " + valueTienThuongMotNgay + " x " + "Ho4:" +
                                             f_Hoc4L + " x 0.50)= " + valueTienThuongReturn + "<br/>";
                        }

                        #endregion
                    }
                }
            return valueTienThuongReturn;
        }

        #endregion

        private bool CheckContractXDTH(EmployeeContractBLL objEC)
        {
            if ((objEC.ContractTypeId == Constants.HopDong_XÐTH_12T) ||
                (objEC.ContractTypeId == Constants.HopDong_XÐTH_24T) ||
                (objEC.ContractTypeId == Constants.HopDong_XÐTH_36T) ||
                (objEC.ContractTypeId == Constants.HopDong_KoXÐTH))
                return true;
            return false;
        }

        #endregion

        #region Tinh Luong nang suat ngan han cho cac truong hop ko phai dong xdth

        private bool CheckContractShortTerm(EmployeeContractBLL objEC)
        {
            if ((objEC.ContractTypeId == Constants.HopDong_HocNghe) ||
                (objEC.ContractTypeId == Constants.HopDong_ThoiVu_3T) ||
                (objEC.ContractTypeId == Constants.HopDong_ThoiVu_6T))
                return true;
            return false;
        }

        private double CalculateOnlyOneContractKoPhaiXDTHInMonth(EmployeeContractBLL objEC, double K)
        {
            double LNSValueReturn = 0;

            if (CheckContractShortTerm(objEC))
            {
                LNSLog += "Hợp Đồng : " + objEC.ContractTypeCode + "<br/>";
                LNSLog += "LNS =0 <br/>";

                if (ObjWorkdayEmployeesBLLL.XL == ObjWorkdayEmployeesBLLL.XQDL)
                {
                    LNSLog += "X=XQD-><br/>";
                    LNSValueReturn = objEC.Wages*ObjWorkdayEmployeesBLLL.XL*K;
                    LNSLog += "LNS = ĐG:" + objEC.Wages + " x X:" + ObjWorkdayEmployeesBLLL.XL + " * K:" + K + "=" +
                              LNSValueReturn + "<br/>";
                }
                else
                {
                    #region Tinh cho cac truong hop nghi phep

                    LNSLog += "X!=XQD-><br/>";
                    if (ObjWorkdayEmployeesBLLL.XL > 0)
                    {
                        LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                        LNSValueReturn += objEC.Wages*ObjWorkdayEmployeesBLLL.XL;
                        LNSLog += "LNS = + ( ĐG : " + objEC.Wages + " x X:" + ObjWorkdayEmployeesBLLL.XL + ")=" +
                                  LNSValueReturn + "<br/>";
                    }
                    if (ObjWorkdayEmployeesBLLL.F_Hoc1L > 0)
                    {
                        LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                        LNSValueReturn += objEC.Wages*ObjWorkdayEmployeesBLLL.F_Hoc1L;
                        LNSLog += "LNS = + ( ĐG : " + objEC.Wages + " x Ho1:" + ObjWorkdayEmployeesBLLL.F_Hoc1L + ")=" +
                                  LNSValueReturn + "<br/>";
                    }
                    if (ObjWorkdayEmployeesBLLL.F_Hoc2L > 0)
                    {
                        LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                        LNSValueReturn += objEC.Wages*ObjWorkdayEmployeesBLLL.F_Hoc2L;
                        LNSLog += "LNS = + ( ĐG : " + objEC.Wages + " x Ho2:" + ObjWorkdayEmployeesBLLL.F_Hoc2L + ")=" +
                                  LNSValueReturn + "<br/>";
                    }
                    if (ObjWorkdayEmployeesBLLL.F_Hoc3L > 0)
                    {
                        LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                        LNSValueReturn += objEC.Wages*ObjWorkdayEmployeesBLLL.F_Hoc3L;
                        LNSLog += "LNS = + ( ĐG : " + objEC.Wages + " x Ho3:" + ObjWorkdayEmployeesBLLL.F_Hoc3L + ")=" +
                                  LNSValueReturn + "<br/>";
                    }
                    if (ObjWorkdayEmployeesBLLL.F_Hoc4L > 0)
                    {
                        LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                        LNSValueReturn += objEC.Wages*ObjWorkdayEmployeesBLLL.F_Hoc4L;
                        LNSLog += "LNS = + ( ĐG : " + objEC.Wages + " x Ho4:" + ObjWorkdayEmployeesBLLL.F_Hoc4L + ")=" +
                                  LNSValueReturn + "<br/>";
                    }
                    if (ObjWorkdayEmployeesBLLL.F_Hoc5L > 0)
                    {
                        LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                        LNSValueReturn += objEC.Wages*ObjWorkdayEmployeesBLLL.F_Hoc5L;
                        LNSLog += "LNS = + ( ĐG : " + objEC.Wages + " x Ho5:" + ObjWorkdayEmployeesBLLL.F_Hoc5L + ")=" +
                                  LNSValueReturn + "<br/>";
                    }
                    if (ObjWorkdayEmployeesBLLL.F_Hoc6L > 0)
                    {
                        LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                        LNSValueReturn += objEC.Wages*ObjWorkdayEmployeesBLLL.F_Hoc6L;
                        LNSLog += "LNS = + ( ĐG : " + objEC.Wages + " x Ho6:" + ObjWorkdayEmployeesBLLL.F_Hoc6L + ")=" +
                                  LNSValueReturn + "<br/>";
                    }
                    if (ObjWorkdayEmployeesBLLL.F_Hoc7L > 0)
                    {
                        LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                        LNSValueReturn += objEC.Wages*ObjWorkdayEmployeesBLLL.F_Hoc7L;
                        LNSLog += "LNS = + ( ĐG : " + objEC.Wages + " x Ho7:" + ObjWorkdayEmployeesBLLL.F_Hoc7L + ")=" +
                                  LNSValueReturn + "<br/>";
                    }
                    if (objEC.ContractTypeId != Constants.HopDong_HocNghe)
                    {
                        LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                        LNSValueReturn = LNSValueReturn*K;
                        LNSLog += " x  K : " + K + "=" + LNSValueReturn + "<br/>";
                    }
                    else
                    {
                        LNSLog += "Hợp đồng học nghề không tính hệ số K.";
                    }

                    #endregion
                }
            }
            return LNSValueReturn;
        }

        private double CalculateMultiContractKoPhaiXDTHInMonth(EmployeeContractBLL objEC, int i, double K)
        {
            double LNSValueReturn = 0;

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

            if (CheckContractShortTerm(objEC))
            {
                LNSLog += i + ".Hợp Đồng : " + objEC.ContractTypeCode + "<br/>";
                LNSLog += "LNSTemp =0 <br/>";

                if (XL > 0)
                {
                    LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                    LNSValueReturn += objEC.Wages*XL;
                    LNSLog += " + (ĐG: " + objEC.Wages + " x X:" + XL + ")=" + LNSValueReturn + "<br/>";
                }
                if (f_Hoc1L > 0)
                {
                    LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                    LNSValueReturn += objEC.Wages*f_Hoc1L;
                    LNSLog += " + (ĐG: " + objEC.Wages + " x Ho1:" + f_Hoc1L + ")=" + LNSValueReturn + "<br/>";
                }
                if (f_Hoc2L > 0)
                {
                    LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                    LNSValueReturn += objEC.Wages*f_Hoc2L;
                    LNSLog += " + (ĐG: " + objEC.Wages + " x Ho2:" + f_Hoc2L + ")=" + LNSValueReturn + "<br/>";
                }
                if (f_Hoc3L > 0)
                {
                    LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                    LNSValueReturn += objEC.Wages*f_Hoc3L;
                    LNSLog += " + (ĐG: " + objEC.Wages + " x Ho3:" + f_Hoc3L + ")=" + LNSValueReturn + "<br/>";
                }
                if (f_Hoc4L > 0)
                {
                    LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                    LNSValueReturn += objEC.Wages*f_Hoc4L;
                    LNSLog += " + (ĐG: " + objEC.Wages + " x Ho4:" + f_Hoc4L + ")=" + LNSValueReturn + "<br/>";
                }
                if (f_Hoc5L > 0)
                {
                    LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                    LNSValueReturn += objEC.Wages*f_Hoc5L;
                    LNSLog += " + (ĐG: " + objEC.Wages + " x Ho5:" + f_Hoc5L + ")=" + LNSValueReturn + "<br/>";
                }
                if (f_Hoc6L > 0)
                {
                    LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                    LNSValueReturn += objEC.Wages*f_Hoc6L;
                    LNSLog += " + (ĐG: " + objEC.Wages + " x Ho6:" + f_Hoc6L + ")=" + LNSValueReturn + "<br/>";
                }
                if (f_Hoc7L > 0)
                {
                    LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                    LNSValueReturn += objEC.Wages*f_Hoc7L;
                    LNSLog += " + (ĐG: " + objEC.Wages + " x Ho7:" + f_Hoc7L + ")=" + LNSValueReturn + "<br/>";
                }
                if (objEC.ContractTypeId != Constants.HopDong_HocNghe)
                {
                    LNSLog += "LNSTemp = LNSTemp:" + LNSValueReturn;
                    LNSValueReturn = LNSValueReturn*K;
                    LNSLog += " x K:" + K + "=" + LNSValueReturn;
                }
                else
                {
                    LNSLog += "Hợp đồng học nghề không tính hệ số K.";
                }
            }

            return LNSValueReturn;
        }

        #endregion
    }


    public class TempLNS
    {
        public double HeSoLNS { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
    }
}