namespace HRMBLL.H1.CalulateSalary
{
    public class AllowancesBLL
    {
        public string PCLog { set; get; } = string.Empty;

        public string TCBHXHLog { set; get; } = string.Empty;

        /// <summary>
        ///     tinh khoan phu cap trach nhiem
        ///     DonGiaLCB * HeSoPCTN
        ///     tinh khoan phu cap doc hai
        ///     (DonGiaLCB * HeSoPCHD * NC_LamViecThucTe)/ NgayCongQDTrongThang
        ///     tinh khoan phu cap chuc vu
        ///     DonGiaLCB * HesoPCCV
        /// </summary>
        /// <returns></returns>
        public void CalculatePC()
        {
            if (ObjCoefficientEmployeeFinalBLL != null)
            {
                ValuePCCV = DON_GIA_LCB*ObjCoefficientEmployeeFinalBLL.PCCV;
                PCLog += "PCCV = DonGiaLCB:" + DON_GIA_LCB + " x HeSoPCCV:" + ObjCoefficientEmployeeFinalBLL.PCCV + ")=" +
                         ValuePCCV + "<br/>";

                ValuePCTN = DON_GIA_LCB*ObjCoefficientEmployeeFinalBLL.PCTN;
                PCLog += "PCTN = DonGiaLCB:" + DON_GIA_LCB + " x HeSoPCTN:" + ObjCoefficientEmployeeFinalBLL.PCTN + ")=" +
                         ValuePCTN + "<br/>";

                ValuePCDH = DON_GIA_LCB*ObjCoefficientEmployeeFinalBLL.PCDH*ObjWorkdayEmployeesBLLL.XL/
                            ObjWorkdayEmployeesBLLL.XQDL;
                PCLog += "PCDH = (DonGiaLCB:" + DON_GIA_LCB + " x HeSoPCDH:" + ObjCoefficientEmployeeFinalBLL.PCTN +
                         " x X:" + ObjWorkdayEmployeesBLLL.XL + ")/XQD:" + ObjWorkdayEmployeesBLLL.XQDL + ")=" +
                         ValuePCDH + "<br/>";
            }
        }

        /// Tinh tro cap BHXH
        /// ((DonGiaLCB * HeSoLCB)/XQD) * NC_OM * 0.75
        public void CalculateTCBHXH()
        {
            if ((ObjWorkdayEmployeesBLLL.F_OmL > 0) ||
                (ObjWorkdayEmployeesBLLL.F_Con_OmL > 0) ||
                (ObjWorkdayEmployeesBLLL.F_KHHDSL > 0) ||
                (ObjWorkdayEmployeesBLLL.F_ThaiSanL > 0))
                if (ObjCoefficientEmployeeFinalBLL != null)
                {
                    var tCBHXH_1_Ngay = DON_GIA_LCB*ObjCoefficientEmployeeFinalBLL.LCB/26;
                    TCBHXHLog += "TCBHXH_1_Ngay = (DonGiaLCB:" + DON_GIA_LCB + " x HeSoLCB:" +
                                 ObjCoefficientEmployeeFinalBLL.LCB + ")/26" + tCBHXH_1_Ngay + "<br/>";

                    if (ObjWorkdayEmployeesBLLL.F_OmL > 0)
                        if (ObjWorkdayEmployeesBLLL.F_OmL == ObjWorkdayEmployeesBLLL.XQDL)
                        {
                            TCBHXHLog += "TCBHXHTemp = TCBHXHTemp :" + ValueTCBHXH;
                            ValueTCBHXH = ValueTCBHXH + DON_GIA_LCB*ObjCoefficientEmployeeFinalBLL.LCB*0.75;
                            TCBHXHLog += " + (DonGiaLCB:" + DON_GIA_LCB + " x HeSoLCB:" +
                                         ObjCoefficientEmployeeFinalBLL.LCB + " x 0.75)=" + ValueTCBHXH + "<br/>";
                        }
                        else
                        {
                            TCBHXHLog += "TCBHXHTemp = TCBHXHTemp :" + ValueTCBHXH;
                            ValueTCBHXH = ValueTCBHXH + tCBHXH_1_Ngay*ObjWorkdayEmployeesBLLL.F_OmL*0.75;
                            TCBHXHLog += " + (TCBHXH_1_Ngay:" + tCBHXH_1_Ngay + " x Ô:" + ObjWorkdayEmployeesBLLL.F_OmL +
                                         " x 0.75)=" + ValueTCBHXH + "<br/>";
                        }
                    if (ObjWorkdayEmployeesBLLL.F_Con_OmL > 0)
                        if (ObjWorkdayEmployeesBLLL.F_Con_OmL == ObjWorkdayEmployeesBLLL.XQDL)
                        {
                            TCBHXHLog += "TCBHXHTemp = TCBHXHTemp :" + ValueTCBHXH;
                            ValueTCBHXH = ValueTCBHXH + DON_GIA_LCB*ObjCoefficientEmployeeFinalBLL.LCB*0.75;
                            TCBHXHLog += " + (DonGiaLCB:" + DON_GIA_LCB + " x HeSoLCB:" +
                                         ObjCoefficientEmployeeFinalBLL.LCB + " x 0.75)=" + ValueTCBHXH + "<br/>";
                        }
                        else
                        {
                            TCBHXHLog += "TCBHXHTemp = TCBHXHTemp :" + ValueTCBHXH;
                            ValueTCBHXH = ValueTCBHXH + tCBHXH_1_Ngay*ObjWorkdayEmployeesBLLL.F_Con_OmL*0.75;
                            TCBHXHLog += " + (TCBHXH_1_Ngay:" + tCBHXH_1_Ngay + " x Co:" +
                                         ObjWorkdayEmployeesBLLL.F_Con_OmL + " x 0.75)=" + ValueTCBHXH + "<br/>";
                        }
                    if (ObjWorkdayEmployeesBLLL.F_KHHDSL > 0)
                        if (ObjWorkdayEmployeesBLLL.F_KHHDSL == ObjWorkdayEmployeesBLLL.XQDL)
                        {
                            TCBHXHLog += "TCBHXHTemp = TCBHXHTemp :" + ValueTCBHXH;
                            ValueTCBHXH = ValueTCBHXH + DON_GIA_LCB*ObjCoefficientEmployeeFinalBLL.LCB*0.75;
                            TCBHXHLog += " + (DonGiaLCB:" + DON_GIA_LCB + " x HeSoLCB:" +
                                         ObjCoefficientEmployeeFinalBLL.LCB + " x 0.75)=" + ValueTCBHXH + "<br/>";
                        }
                        else
                        {
                            TCBHXHLog += "TCBHXHTemp = TCBHXHTemp :" + ValueTCBHXH;
                            ValueTCBHXH = ValueTCBHXH + tCBHXH_1_Ngay*ObjWorkdayEmployeesBLLL.F_KHHDSL*0.75;
                            TCBHXHLog += " + (TCBHXH_1_Ngay:" + tCBHXH_1_Ngay + " x KHH:" +
                                         ObjWorkdayEmployeesBLLL.F_KHHDSL + " x 0.75)=" + ValueTCBHXH + "<br/>";
                        }
                    if (ObjWorkdayEmployeesBLLL.F_ThaiSanL > 0)
                        if (ObjWorkdayEmployeesBLLL.F_ThaiSanL == ObjWorkdayEmployeesBLLL.XQDL)
                        {
                            TCBHXHLog += "TCBHXHTemp = TCBHXHTemp :" + ValueTCBHXH;
                            ValueTCBHXH = ValueTCBHXH + DON_GIA_LCB*ObjCoefficientEmployeeFinalBLL.LCB;
                            TCBHXHLog += " + (DonGiaLCB:" + DON_GIA_LCB + " x HeSoLCB:" +
                                         ObjCoefficientEmployeeFinalBLL.LCB + ")=" + ValueTCBHXH + "<br/>";
                        }
                        else
                        {
                            TCBHXHLog += "TCBHXHTemp = TCBHXHTemp :" + ValueTCBHXH;
                            ValueTCBHXH = ValueTCBHXH + tCBHXH_1_Ngay*ObjWorkdayEmployeesBLLL.F_ThaiSanL;
                            TCBHXHLog += " + (TCBHXH_1_Ngay:" + tCBHXH_1_Ngay + " x TS:" +
                                         ObjWorkdayEmployeesBLLL.F_ThaiSanL + ")=" + ValueTCBHXH + "<br/>";
                        }
                    if (ObjWorkdayEmployeesBLLL.F_SayThaiL > 0)
                        if (ObjWorkdayEmployeesBLLL.F_SayThaiL == ObjWorkdayEmployeesBLLL.XQDL)
                        {
                            TCBHXHLog += "TCBHXHTemp = TCBHXHTemp :" + ValueTCBHXH;
                            ValueTCBHXH = ValueTCBHXH + DON_GIA_LCB*ObjCoefficientEmployeeFinalBLL.LCB;
                            TCBHXHLog += " + (DonGiaLCB:" + DON_GIA_LCB + " x HeSoLCB:" +
                                         ObjCoefficientEmployeeFinalBLL.LCB + " =" + ValueTCBHXH + "<br/>";
                        }
                        else
                        {
                            TCBHXHLog += "TCBHXHTemp = TCBHXHTemp :" + ValueTCBHXH;
                            ValueTCBHXH = ValueTCBHXH + tCBHXH_1_Ngay*ObjWorkdayEmployeesBLLL.F_SayThaiL;
                            TCBHXHLog += " + (TCBHXH_1_Ngay:" + tCBHXH_1_Ngay + " x ST:" +
                                         ObjWorkdayEmployeesBLLL.F_SayThaiL + ")=" + ValueTCBHXH + "<br/>";
                        }
                    if (ObjWorkdayEmployeesBLLL.F_KhamThaiL > 0)
                        if (ObjWorkdayEmployeesBLLL.F_KhamThaiL == ObjWorkdayEmployeesBLLL.XQDL)
                        {
                            TCBHXHLog += "TCBHXHTemp = TCBHXHTemp :" + ValueTCBHXH;
                            ValueTCBHXH = ValueTCBHXH + DON_GIA_LCB*ObjCoefficientEmployeeFinalBLL.LCB;
                            TCBHXHLog += " + (DonGiaLCB:" + DON_GIA_LCB + " x HeSoLCB:" +
                                         ObjCoefficientEmployeeFinalBLL.LCB + " =" + ValueTCBHXH + "<br/>";
                        }
                        else
                        {
                            TCBHXHLog += "TCBHXHTemp = TCBHXHTemp :" + ValueTCBHXH;
                            ValueTCBHXH = ValueTCBHXH + tCBHXH_1_Ngay*ObjWorkdayEmployeesBLLL.F_KhamThaiL;
                            TCBHXHLog += " + (TCBHXH_1_Ngay:" + tCBHXH_1_Ngay + " x KT:" +
                                         ObjWorkdayEmployeesBLLL.F_KhamThaiL + ")=" + ValueTCBHXH + "<br/>";
                        }
                    if (ObjWorkdayEmployeesBLLL.F_ConChetL > 0)
                        if (ObjWorkdayEmployeesBLLL.F_ConChetL == ObjWorkdayEmployeesBLLL.XQDL)
                        {
                            TCBHXHLog += "TCBHXHTemp = TCBHXHTemp :" + ValueTCBHXH;
                            ValueTCBHXH = ValueTCBHXH + DON_GIA_LCB*ObjCoefficientEmployeeFinalBLL.LCB;
                            TCBHXHLog += " + (DonGiaLCB:" + DON_GIA_LCB + " x HeSoLCB:" +
                                         ObjCoefficientEmployeeFinalBLL.LCB + " =" + ValueTCBHXH + "<br/>";
                        }
                        else
                        {
                            TCBHXHLog += "TCBHXHTemp = TCBHXHTemp :" + ValueTCBHXH;
                            ValueTCBHXH = ValueTCBHXH + tCBHXH_1_Ngay*ObjWorkdayEmployeesBLLL.F_ConChetL;
                            TCBHXHLog += " + (TCBHXH_1_Ngay:" + tCBHXH_1_Ngay + " x CC:" +
                                         ObjWorkdayEmployeesBLLL.F_ConChetL + ")=" + ValueTCBHXH + "<br/>";
                        }

                    ValueTCOm = ValueTCBHXH;
                    TCBHXHLog += "TroCapOm = TCBHXHTemp = " + ValueTCBHXH + "<br/>";
                }

            var objpa = PregnantAllownceBLL.GetByUserAllownceDate(ObjWorkdayEmployeesBLLL.UserId,
                ObjWorkdayEmployeesBLLL.WorkdayDateL);
            if (objpa != null)
            {
                ValueTCTS1Lan = objpa.AllownceValue;
                TCBHXHLog += "TroCapThaiSan1Lan = " + ValueTCTS1Lan + "<br/>";
                ValueTCBHXH = 0;
                ValueTCBHXH = ValueTCOm + ValueTCTS1Lan;
                TCBHXHLog += "TroCapBHXH = TroCapOm:" + ValueTCOm + " + TroCapThaiSan1Lan:" + ValueTCTS1Lan + " = " +
                             ValueTCBHXH + "<br/>";
            }
            else
            {
                ValueTCTS1Lan = 0;
                TCBHXHLog += "TroCapThaiSan1Lan = 0 <br/>";
                ValueTCBHXH = ValueTCOm;
                TCBHXHLog += "TroCapBHXH = TroCapOm:" + ValueTCOm + "<br/>";
            }
        } //end method

        #region private fields

        #endregion

        #region properties

        public WorkdayEmployeesBLLL ObjWorkdayEmployeesBLLL { get; set; }

        public CoefficientEmployeeFinalBLL ObjCoefficientEmployeeFinalBLL { get; set; }

        public double ValuePCDH { get; set; }

        public double ValuePCTN { get; set; }

        public double ValuePCCV { get; set; }

        public double ValueTCBHXH { get; set; }

        public double ValueTCOm { get; set; }

        public double ValueTCTS1Lan { get; set; }

        /// <summary>
        ///     Don gia luong can ban
        /// </summary>
        public double DON_GIA_LCB { get; set; }

        #endregion
    }
}