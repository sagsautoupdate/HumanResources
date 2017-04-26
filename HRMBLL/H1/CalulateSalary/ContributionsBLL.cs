using System.Collections.Generic;
using HRMBLL.H0;
using HRMUtil;

namespace HRMBLL.H1.CalulateSalary
{
    public class ContributionsBLL
    {
        /// <summary>
        ///     Tinh khoan trich BHXH.
        ///     **********************************************************************************
        ///     CONG THUC CU:
        ///     (HeSoPCCV + HeSoPCDH + HeSoLCB) * DonGiaLCB * 0.05(dong 5%)
        ///     *****************
        ///     CONG THUC MOI ngay 24/06/2008
        ///     if(TS, ODN>=NCQD)thi TrBHXH = 0
        ///     else if(O,Co,KHDS >=14 )thi TrBHXH = 0
        ///     else IF X> 0 thi TrBHXH = (HeSoPCCV + HesoPCKV+ HeSoLCB) * DonGiaLCB * 0.05(dong 5%)
        ///     CONG THUC bosung ngay 27/10/2008
        ///     Hop dong thu viec khong trich cac khoan : BHXH, BHYT
        ///     **********************************************************************************
        ///     Tinh khoan trich BHYT.
        ///     CONG THUC CU: (HeSoPCCV + HeSoPCDH + HeSoLCB) * DonGiaLCB * 0.01(dong 1%)
        ///     ****************
        ///     CONG THUC MOI ngay 24/06/2008
        ///     if(TS, ODN>=NCQD)thi TrBHYT = 0
        ///     else IF X> 0 thi TrBHYT = (HeSoPCCV + HesoPCKV+ HeSoLCB) * DonGiaLCB * 0.01(dong 1%)
        ///     **********************************************************************************
        ///     Tinh Bo sung luong
        ///     CONG THUC CU : BosungLuong = TrBHXH + TrBHYT
        ///     *******************
        ///     CONG THUC MOI
        ///     Neu Ro, Ko, DC thi bo sung luong =0
        ///     **********************************************************************************
        ///     Tinh khoan trich Kinh Phi cong doan.
        ///     Neu ((Luong NS + LCB + PCCV +PCTN +PCDH) * 0.01) > 54000 thi dong 54000
        ///     Nguoc lai thi dong ((Luong NS + LCB + PCCV +PCTN +PCDH) * 0.01)
        ///     Tinh tien bo sung luong
        ///     tien bo sung luong = trich nop BHXH + BHYT (tuy theo loai hop dong va nghi om dai ngay)
        /// </summary>
        /// <returns></returns>
        public void Calculate()
        {
            if (ListEmployeeContractBLLCheck.Count > 1)
                if ((ListEmployeeContractBLLCheck[1].FromDate.Day <= 15) ||
                    CheckIsContractHaveBHXH_BHYT(ListEmployeeContractBLLCheck[0]))
                {
                    CalculateByCondition();
                }
                else
                {
                    Contributions_Log += "Hop dong: " + ListEmployeeContractBLLCheck[0].ContractTypeName +
                                         " chuyen sang hop dong: " + ListEmployeeContractBLLCheck[1].ContractTypeName;
                    Contributions_Log += "Ngay chuyen hop dong la : " + ListEmployeeContractBLLCheck[1].FromDate +
                                         " >15 nen : <br/>";
                    ValueBHXH = 0;
                    ValueBHYT = 0;
                    ValueBoSungLuong = 0;
                    Contributions_Log += "ValueBHXH=0 <br/>";
                    Contributions_Log += "ValueBHYT=0 <br/>";
                    Contributions_Log += "ValueBoSungLuong=0 <br/>";
                    CalculateDPCD();
                }
            else if (ListEmployeeContractBLLCheck.Count == 1)
                if (CheckIsContractHaveBHXH_BHYT(ListEmployeeContractBLLCheck[0]))
                {
                    Contributions_Log += "Hop dong thu viec nen : <br/>";
                    ValueBHXH = 0;
                    ValueBHYT = 0;
                    ValueBoSungLuong = 0;
                    Contributions_Log += "ValueBHXH=0 <br/>";
                    Contributions_Log += "ValueBHYT=0 <br/>";
                    Contributions_Log += "ValueBoSungLuong=0 <br/>";
                    CalculateDPCD();
                }
                else
                {
                    CalculateByCondition();
                }
        }

        private void CalculateByCondition()
        {
            if ((ObjWorkdayEmployeesBLLL.F_ThaiSanL >= ObjWorkdayEmployeesBLLL.XQDL) ||
                (ObjWorkdayEmployeesBLLL.F_OmDaiNgayL >= ObjWorkdayEmployeesBLLL.XQDL))
            {
                Contributions_Log += "TS:" + ObjWorkdayEmployeesBLLL.F_ThaiSanL + " OR ODN:" +
                                     ObjWorkdayEmployeesBLLL.F_OmDaiNgayL;
                ValueBHXH = 0;
                ValueBHYT = 0;
                ValueBHTN = 0;
                ValueBoSungLuong = 0;
                Contributions_Log += "ValueBHXH=0 <br/>";
                Contributions_Log += "ValueBHYT=0 <br/>";
                Contributions_Log += "ValueBHTN=0 <br/>";
                Contributions_Log += "ValueBoSungLuong=0 <br/>";
            }
            else if ((ObjWorkdayEmployeesBLLL.F_OmL >= 14) || (ObjWorkdayEmployeesBLLL.F_Con_OmL >= 14) ||
                     (ObjWorkdayEmployeesBLLL.F_KHHDSL >= 14))
            {
                double pccv = 0;
                double pckv = 0;
                double lcb = 0;
                ValueBHXH = 0;
                if (ObjCoefficientEmployeeFinalBLL != null)
                {
                    pccv = ObjCoefficientEmployeeFinalBLL.PCCV;
                    pckv = ObjCoefficientEmployeeFinalBLL.PCKV;
                    lcb = ObjCoefficientEmployeeFinalBLL.LCB;
                    ValueBHYT = (pccv + pckv + lcb)*DON_GIA_LCB*0.01;
                    ValueBHTN = (pccv + pckv + lcb)*DON_GIA_LCB*0.01;
                    Contributions_Log += "ValueBHYT=(PCCV:" + pccv + "+PCKV:" + pckv + "+lcb) * DonGiaLCB:" +
                                         DON_GIA_LCB + " 0.01 <br/>";
                    Contributions_Log += "ValueBHTN=(PCCV:" + pccv + "+PCKV:" + pckv + "+lcb) * DonGiaLCB:" +
                                         DON_GIA_LCB + " 0.01 <br/>";
                    if ((ObjWorkdayEmployeesBLLL.F_KoLuongCLDL >= ObjWorkdayEmployeesBLLL.XQDL) ||
                        (ObjWorkdayEmployeesBLLL.F_KoLuongKLDL >= ObjWorkdayEmployeesBLLL.XQDL)
                        || (ObjWorkdayEmployeesBLLL.F_DinhChiCongTacL >= ObjWorkdayEmployeesBLLL.XQDL))
                    {
                        ValueBoSungLuong = 0;
                        Contributions_Log += "Ro:" + ObjWorkdayEmployeesBLLL.F_KoLuongCLDL + " >= XQD:" +
                                             ObjWorkdayEmployeesBLLL.XQDL + "<br/>";
                        Contributions_Log += " hoac Ko:" + ObjWorkdayEmployeesBLLL.F_KoLuongKLDL + " >= XQD:" +
                                             ObjWorkdayEmployeesBLLL.XQDL + "<br/>";
                        Contributions_Log += " hoac DC:" + ObjWorkdayEmployeesBLLL.F_DinhChiCongTacL + " >= XQD:" +
                                             ObjWorkdayEmployeesBLLL.XQDL + "<br/>";

                        Contributions_Log += " BoSungLuong = 0 <br/>";
                    }
                    else
                    {
                        ValueBoSungLuong = ValueBHXH + ValueBHYT + ValueBHTN;
                        Contributions_Log += " TienBoSungLuong = TienBHXH:" + ValueBHXH + "+TienBHYT:" + ValueBHYT +
                                             "+TienBHTN:" + ValueBHTN + "=" + ValueBoSungLuong + "<br/>";
                    }
                }
            }
            else
            {
                CalculateByLCBCoefficient();
            }
            CalculateDPCD();
        }

        private void CalculateDPCD()
        {
            Contributions_Log +=
                "/******************** Tính tien dinh phi cong doan **************************************/<br/>";

            var totalKPCongDoan = ValueLCB + ValueLNS + ValuePCDH + ValuePCTN + ValuePCCV;

            Contributions_Log += "TongTienChiuKPCD= TienLCB:" + ValueLCB + "+TienLNS:" + ValueLNS + "+TienPCDH:" +
                                 ValuePCDH + "+TienPCTN:" + ValuePCTN + "+TienPCCV:" + ValuePCCV + "=" + totalKPCongDoan +
                                 "<br/>";
            ;

            var kpcd = totalKPCongDoan*0.01;
            Contributions_Log += "kpcd:" + totalKPCongDoan + " * 0.01 <br/>";
            //if (kpcd > Constants.MAX_KPCD)
            //{
            //    Contributions_Log += "kpcd:" + totalKPCongDoan + " > " + Constants.MAX_KPCD;
            //    _ValueKPCD = Constants.MAX_KPCD;
            //    Contributions_Log += "TienKPCD=" + Constants.MAX_KPCD + "<br/>"; ;
            //}
            //else
            //{
            //    Contributions_Log += "TienKPCD:" + kpcd + "<br/>"; ;
            //    _ValueKPCD = kpcd;
            //}
        }

        private void CalculateByLCBCoefficient()
        {
            double pccv = 0;
            double pckv = 0;
            double lcb = 0;
            if (ObjCoefficientEmployeeFinalBLL != null)
            {
                pccv = ObjCoefficientEmployeeFinalBLL.PCCV;
                pckv = ObjCoefficientEmployeeFinalBLL.PCKV;
                lcb = ObjCoefficientEmployeeFinalBLL.LCB;
                ValueBHXH = (pccv + pckv + lcb)*DON_GIA_LCB*0.05;
                Contributions_Log += "ValueBHXH=(PCCV:" + pccv + "+PCKV:" + pckv + "+lcb) * DonGiaLCB:" + DON_GIA_LCB +
                                     " 0.05 <br/>";
                ValueBHYT = (pccv + pckv + lcb)*DON_GIA_LCB*0.01;
                Contributions_Log += "ValueBHYT=(PCCV:" + pccv + "+PCKV:" + pckv + "+lcb) * DonGiaLCB:" + DON_GIA_LCB +
                                     " 0.01 <br/>";
                ValueBHTN = (pccv + pckv + lcb)*DON_GIA_LCB*0.01;
                Contributions_Log += "ValueBHTN=(PCCV:" + pccv + "+PCKV:" + pckv + "+lcb) * DonGiaLCB:" + DON_GIA_LCB +
                                     " 0.01 <br/>";
            }

            if ((ObjWorkdayEmployeesBLLL.F_KoLuongCLDL >= ObjWorkdayEmployeesBLLL.XQDL) ||
                (ObjWorkdayEmployeesBLLL.F_KoLuongKLDL >= ObjWorkdayEmployeesBLLL.XQDL)
                || (ObjWorkdayEmployeesBLLL.F_DinhChiCongTacL >= ObjWorkdayEmployeesBLLL.XQDL))
            {
                ValueBoSungLuong = 0;
                Contributions_Log += "Ro:" + ObjWorkdayEmployeesBLLL.F_KoLuongCLDL + " >= XQD:" +
                                     ObjWorkdayEmployeesBLLL.XQDL + "<br/>";
                Contributions_Log += " hoac Ko:" + ObjWorkdayEmployeesBLLL.F_KoLuongKLDL + " >= XQD:" +
                                     ObjWorkdayEmployeesBLLL.XQDL + "<br/>";
                Contributions_Log += " hoac DC:" + ObjWorkdayEmployeesBLLL.F_DinhChiCongTacL + " >= XQD:" +
                                     ObjWorkdayEmployeesBLLL.XQDL + "<br/>";

                Contributions_Log += " BoSungLuong = 0 <br/>";
            }
            else
            {
                ValueBoSungLuong = ValueBHXH + ValueBHYT + ValueBHTN;
                Contributions_Log += " TienBoSungLuong = TienBHXH:" + ValueBHXH + "+TienBHYT:" + ValueBHYT +
                                     "+TienBHTN:" + ValueBHTN + "=" + ValueBoSungLuong + "<br/>";
            }
        }

        private bool CheckIsContractHaveBHXH_BHYT(EmployeeContractBLL objEC)
        {
            if ((objEC.ContractTypeId == Constants.HopDong_HocNghe) ||
                (objEC.ContractTypeId == Constants.HopDong_ThoiVu_3T) ||
                (objEC.ContractTypeId == Constants.HopDong_ThoiVu_6T) ||
                (objEC.ContractTypeId == Constants.HopDong_ThuViec_DaiHoc) ||
                (objEC.ContractTypeId == Constants.HopDong_ThuViec_Khac))
                return true;
            return false;
        }

        #region private fields

        #endregion

        #region properties

        public WorkdayEmployeesBLLL ObjWorkdayEmployeesBLLL { get; set; }

        public CoefficientEmployeeFinalBLL ObjCoefficientEmployeeFinalBLL { get; set; }

        public List<EmployeeContractBLL> ListEmployeeContractBLLCheck { get; set; }

        public double ValueKPCD { get; set; }

        public double ValueBHYT { get; set; }

        public double ValueBHTN { get; set; }

        public double ValueBHXH { get; set; }

        public double ValueBoSungLuong { get; set; }

        /// <summary>
        ///     Don gia luong can ban
        /// </summary>
        public double DON_GIA_LCB { get; set; }

        public double ValueLNS { get; set; }

        public double ValueLCB { get; set; }

        public double ValuePCDH { get; set; }

        public double ValuePCTN { get; set; }

        public double ValuePCCV { get; set; }

        public string Contributions_Log { get; set; }

        #endregion
    }
}