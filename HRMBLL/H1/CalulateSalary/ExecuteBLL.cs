using System;
using HRMBLL.H0;
using HRMUtil;

namespace HRMBLL.H1.CalulateSalary
{
    public class ExecuteBLL
    {
        public int RunningPeriod_I_ShortTerm()
        {
            var CalculatingLog1 = string.Empty;
            var createDate = new DateTime(Year, Month, 1);
            var objCoefficientEmployeeFinalBLL = CoefficientEmployeeFinalBLL.GetByUserDate(UserId, Month, Year);
            var ListEmployeeContractBLLCheck = EmployeeContractBLL.GetByUserIdFromToDate(UserId,
                new DateTime(Year, Month, 1), new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month)));

            if (ListEmployeeContractBLLCheck.Count <= 0)
                return -1;
            ////////////////////////////////////////////////////////////////
            /// 
            /// Object tinh luong nang suat
            /// Tien luong nang suat, tien thuong
            ////////////////////////////////////////////////////////////////
            if (ObjWorkdayEmployeesBLLL.MarkL > 0)
            {
                CalculatingLog1 +=
                    "<br/>/******************** Tính lương năng suất ko phải XDTH **************************************/<br/><br/>";
                ObjProductivitySalaryBLL.ObjCoefficientEmployeeFinalBLL = objCoefficientEmployeeFinalBLL;
                ObjProductivitySalaryBLL.ObjWorkdayEmployeesBLLL = ObjWorkdayEmployeesBLLL;
                ObjProductivitySalaryBLL.DON_GIA_LCB = DonGiaLCB;
                ObjProductivitySalaryBLL.UserId = UserId;
                ObjProductivitySalaryBLL.Month = Month;
                ObjProductivitySalaryBLL.Year = Year;
                ObjProductivitySalaryBLL.ListEmployeeContractBLLCheck = ListEmployeeContractBLLCheck;
                ObjProductivitySalaryBLL.CalculateShortTermSalary();
                CalculatingLog1 += ObjProductivitySalaryBLL.LNSLog;
            }
            ////////////////////////////////////////////////////////////////
            /// run Object tinh luong co ban
            /// Tien luong co ban 
            ////////////////////////////////////////////////////////////////
            if (ObjWorkdayEmployeesBLLL.MarkL <= 0)
            {
                CalculatingLog1 +=
                    "<br/>/******************** Tính lương cơ bản nếu điểm HTCV<0 **************************************/<br/><br/>";
                ObjBasicSalaryBLL.ObjCoefficientEmployeeFinalBLL = objCoefficientEmployeeFinalBLL;
                ObjBasicSalaryBLL.ObjWorkdayEmployeesBLLL = ObjWorkdayEmployeesBLLL;
                ObjBasicSalaryBLL.DON_GIA_LCB = DonGiaLCB;
                ObjBasicSalaryBLL.UserId = UserId;
                ObjBasicSalaryBLL.Month = Month;
                ObjBasicSalaryBLL.Year = Year;
                ObjBasicSalaryBLL.ListEmployeeContractBLLCheck = ListEmployeeContractBLLCheck;
                ObjBasicSalaryBLL.Calculate();
                CalculatingLog1 += ObjBasicSalaryBLL.LCBLog;
            }
            else if (ObjWorkdayEmployeesBLLL.F_DinhChiCongTacL > 0)
            {
                CalculatingLog1 +=
                    "<br/>/******************** Tính lương cơ bản nếu có ngày đình chỉ công tác **************************************/<br/><br/>";
                ObjBasicSalaryBLL.ObjCoefficientEmployeeFinalBLL = objCoefficientEmployeeFinalBLL;
                ObjBasicSalaryBLL.ObjWorkdayEmployeesBLLL = ObjWorkdayEmployeesBLLL;
                ObjBasicSalaryBLL.DON_GIA_LCB = DonGiaLCB;
                ObjBasicSalaryBLL.UserId = UserId;
                ObjBasicSalaryBLL.Month = Month;
                ObjBasicSalaryBLL.Year = Year;
                ObjBasicSalaryBLL.ListEmployeeContractBLLCheck = ListEmployeeContractBLLCheck;
                ObjBasicSalaryBLL.CalculateDinhCongTac();
                CalculatingLog1 += ObjBasicSalaryBLL.LCBLog;
            }
            ///////////////////////////////////////////////////////////////////
            /// run Object tinh cac khoan phu cap
            /// PCTN, PCCV, PCDH, tro cap bhxh
            //////////////////////////////////////////////////////////////////
            CalculatingLog1 +=
                "<br/>/******************** Tính các khoản phụ cấp **************************************/<br/><br/>";
            ObjAllowancesBLL.ObjCoefficientEmployeeFinalBLL = objCoefficientEmployeeFinalBLL;
            ObjAllowancesBLL.ObjWorkdayEmployeesBLLL = ObjWorkdayEmployeesBLLL;
            ObjAllowancesBLL.DON_GIA_LCB = DonGiaLCB;
            ObjAllowancesBLL.CalculatePC();
            CalculatingLog1 += ObjAllowancesBLL.PCLog;
            ObjAllowancesBLL.CalculateTCBHXH();
            CalculatingLog1 +=
                "<br/>/******************** Tính tiền trợ cấp BHXH **************************************/<br/><br/>";
            CalculatingLog1 += ObjAllowancesBLL.TCBHXHLog;

            //////////////////////////////////////////////////////////////////
            /// run Object tinh tien lam Overtime
            /// Tien lam dem, tien an giua ca
            //////////////////////////////////////////////////////////////////
            CalculatingLog1 +=
                "<br/>/******************** Tính tiền ăn **************************************/<br/><br/>";
            ObjOverTimeSalaryBLL.ObjCoefficientEmployeeFinalBLL = objCoefficientEmployeeFinalBLL;
            ObjOverTimeSalaryBLL.ObjWorkdayEmployeesBLLL = ObjWorkdayEmployeesBLLL;
            ObjOverTimeSalaryBLL.DON_GIA_LCB = DonGiaLCB;
            ObjOverTimeSalaryBLL.DON_GIA_TIENAN = DonGiaTienAn;
            ObjOverTimeSalaryBLL.CalculateTienAn();
            CalculatingLog1 += ObjOverTimeSalaryBLL.TienAnLog;
            ObjOverTimeSalaryBLL.CalculateTienDem();
            CalculatingLog1 +=
                "<br/>/********************* Tính tiền làm đêm *************************************/<br/><br/>";
            CalculatingLog1 += ObjOverTimeSalaryBLL.TienDemLog;
            ///******************************************************************
            /// Insert Incomes
            ///******************************************************************
            var IsChangeContract = Constants.IsChangeContract_No_Id;
            if (ListEmployeeContractBLLCheck.Count > 1)
                IsChangeContract = Constants.IsChangeContract_Yes_Id;

            var TotalShortTerm = ObjProductivitySalaryBLL.ValueLNSSortTerm +
                                 ObjBasicSalaryBLL.ValueLCB +
                                 ObjAllowancesBLL.ValuePCCV +
                                 ObjAllowancesBLL.ValuePCTN +
                                 ObjAllowancesBLL.ValuePCDH +
                                 //ObjAllowancesBLL.ValueTCBHXH +
                                 ObjOverTimeSalaryBLL.ValueTIENDEM;
            CalculatingLog1 +=
                "<br/>/******************** Tổng tiền lương ngắn hạn **************************************/<br/><br/>";
            CalculatingLog1 += "TotalShortTerm = LNS:" + ObjProductivitySalaryBLL.ValueLNSSortTerm;
            CalculatingLog1 += " + LCB:" + ObjBasicSalaryBLL.ValueLCB;
            CalculatingLog1 += " + PCCV:" + ObjAllowancesBLL.ValuePCCV;
            CalculatingLog1 += " + PCTN:" + ObjAllowancesBLL.ValuePCTN;
            CalculatingLog1 += " + PCDH:" + ObjAllowancesBLL.ValuePCDH;
            //CalculatingLog1 += " + TCBHXH:" + ObjAllowancesBLL.ValueTCBHXH;
            CalculatingLog1 += " + TienDem:" + ObjOverTimeSalaryBLL.ValueTIENDEM + " = " + TotalShortTerm;
            var type = Constants.H1_Income_Type_Position;
            if (ObjProductivitySalaryBLL.ValueLNSSortTerm > 0)
                type = Constants.H1_Income_Type_Seasonal;

            //IncomesBLL.InsertShortTerm(
            //    UserId,
            //    (decimal)ObjProductivitySalaryBLL.ValueLNSSortTerm,
            //    (decimal)ObjBasicSalaryBLL.ValueLCB,
            //    (decimal)ObjAllowancesBLL.ValuePCCV,
            //    (decimal)ObjAllowancesBLL.ValuePCTN,
            //    (decimal)ObjAllowancesBLL.ValuePCDH,
            //    (decimal)ObjAllowancesBLL.ValueTCBHXH,
            //    (decimal)ObjOverTimeSalaryBLL.ValueTIENAN,
            //    0,
            //    (decimal)ObjOverTimeSalaryBLL.ValueTIENDEM,
            //    (decimal)TotalShortTerm,
            //    CalculatingLog1,
            //    IsChangeContract,
            //    string.Empty,
            //    new DateTime(Year, Month, 1),
            //    (decimal)ObjAllowancesBLL.ValueTCOm,
            //    (decimal)ObjAllowancesBLL.ValueTCTS1Lan,
            //    type
            //    );

            return type;
        }

        //public int RunningPeriod_II(bool isContractThoiVuHocNghe)
        //{
        //    string CalculatingLog = string.Empty;
        //    DateTime createDate = new DateTime(Year, Month, 1);
        //    CoefficientEmployeeFinalBLL objCoefficientEmployeeFinalBLL = CoefficientEmployeeFinalBLL.GetByUserDate(UserId, Month, Year);            
        //    List<EmployeeContractBLL> ListEmployeeContractBLLCheck = EmployeeContractBLL.GetByUserIdFromToDate(UserId, createDate, new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month)));
        //    if (ListEmployeeContractBLLCheck.Count <= 0)
        //    {
        //        return -1;
        //    }
        //    else
        //    {
        //        IncomesBLL objIncome = IncomesBLL.GetByUserIdAndDate(UserId, createDate.Month, createDate.Year);
        //        ////////////////////////////////////////////////////////////////
        //        /// run Object tinh luong nang suat
        //        /// Tien luong nang suat, tien thuong
        //        ////////////////////////////////////////////////////////////////
        //        if (ObjWorkdayEmployeesBLLL.MarkL > 0 && isContractThoiVuHocNghe == false && objCoefficientEmployeeFinalBLL != null)
        //        {
        //            ObjProductivitySalaryBLL.ObjCoefficientEmployeeFinalBLL = objCoefficientEmployeeFinalBLL;
        //            ObjProductivitySalaryBLL.ObjWorkdayEmployeesBLLL = ObjWorkdayEmployeesBLLL;
        //            ObjProductivitySalaryBLL.DON_GIA_LNS = DonGiaLNS;
        //            ObjProductivitySalaryBLL.DON_GIA_TIEN_THUONG = DonGiaTienThuong;
        //            ObjProductivitySalaryBLL.UserId = UserId;
        //            ObjProductivitySalaryBLL.Month = Month;
        //            ObjProductivitySalaryBLL.Year = Year;
        //            ObjProductivitySalaryBLL.ListEmployeeContractBLLCheck = ListEmployeeContractBLLCheck;
        //            ObjProductivitySalaryBLL.CalculatePerod_II();
        //            CalculatingLog += ObjProductivitySalaryBLL.Perod_II_Log;
        //            objIncome.LNS = (decimal)ObjProductivitySalaryBLL.ValueLNS;
        //            objIncome.TienThuong = (decimal)ObjProductivitySalaryBLL.ValueTienThuong;
        //        }

        //        //////////////////////////////////////////////////////////////////
        //        /// run Object tinh cac khoan phai nop               
        //        // TInh cac khoan trich
        //        // tien trich BHXH, BHYT, KPCD, TTN, Tien BoSungLuong, BHTN

        //        ObjContributionsBLL.ObjCoefficientEmployeeFinalBLL = objCoefficientEmployeeFinalBLL;
        //        ObjContributionsBLL.ObjWorkdayEmployeesBLLL = ObjWorkdayEmployeesBLLL;
        //        ObjContributionsBLL.ListEmployeeContractBLLCheck = ListEmployeeContractBLLCheck;                
        //        ObjContributionsBLL.DON_GIA_LCB = DonGiaLCB;
        //        ObjContributionsBLL.ValueLNS = (double)objIncome.LNS;
        //        ObjContributionsBLL.ValueLCB = (double)objIncome.LCBNN;
        //        ObjContributionsBLL.ValuePCDH = (double)objIncome.PCDH;
        //        ObjContributionsBLL.ValuePCTN = (double)objIncome.PCTN;
        //        ObjContributionsBLL.ValuePCCV = (double)objIncome.PCCV;
        //        ObjContributionsBLL.Calculate();
        //        CalculatingLog += "/******************** Tính cac khoan phai nop ****************/ <br/>";
        //        CalculatingLog += ObjContributionsBLL.Contributions_Log;

        //        //////////////////////////////////////////////////////////////////
        //        // tinh tong cac khoan thu nhap
        //        // total = TienLamThem + TienLamDem + Tien PCTN + Tien PCDH + Tien LNS + Tien An
        //        //********************************
        //        // Con thieu tinh tien lam them
        //        //********************************
        //        double tiendem = (double)objIncome.TienLamDem;
        //        double tienLamThem = 0;
        //        double pctn = (double)objIncome.PCTN;
        //        double pcdh = (double)objIncome.PCDH;
        //        double pccv = (double)objIncome.PCCV;
        //        double lns = (double)objIncome.LNS;
        //        double tcTS1Lan = (double)objIncome.TCTS1Lan;
        //        double pckv = 0;
        //        if (isContractThoiVuHocNghe == false)
        //        {
        //            lns = (double)ObjProductivitySalaryBLL.ValueLNS;
        //        }

        //        double lcb = (double)objIncome.LCBNN;
        //        double tienan = (double)objIncome.TienAn;
        //        double tienthuong = ObjProductivitySalaryBLL.ValueTienThuong;
        //        double tienbosungluong = ObjContributionsBLL.ValueBoSungLuong;

        //        double totalIncome = tiendem + tienLamThem + pctn + pcdh + pccv + lns + lcb + tienan + tienthuong + tienbosungluong + tcTS1Lan;
        //        CalculatingLog += "/******************** tinh tong cac khoan thu nhap ****************/ <br/>";
        //        CalculatingLog += "TienTongThuNhap=Tiendem: " + tiendem + " + pctn:" + pctn + " + pcdh:" + pcdh + " + pccv:" + pccv + " + lns:" + lns + " + lcb:" + lcb + " + tienan:" + tienan + " + tienthuong:" + tienthuong + " + tienBosungLuong:" + tienbosungluong + " + TroCapThaiSan:" + tcTS1Lan + "=" + totalIncome + "<br/>";

        //        double totalIncomeCHIUTHUE = 0;
        //        double thueThuNhap = 0;
        //        double totalPeriod_I = 0;
        //        double totalContributions = 0;

        //        /// I. Tinh tong thu nhap chiu thue.
        //        /// TN CHIU THUE= TONG TN - (ĐH + PCKV+  BHXH +BHYT +BHTN)

        //        totalIncomeCHIUTHUE = totalIncome - (pcdh + pckv + ObjContributionsBLL.ValueBHXH + ObjContributionsBLL.ValueBHYT + ObjContributionsBLL.ValueBHTN + tiendem + tienLamThem + tcTS1Lan + tienan);
        //        CalculatingLog += "/******************** Tinh tong thu nhap chiu thue ****************/ <br/>";
        //        CalculatingLog += "TienTongThuNhapChiuThue=TienTongThuNhap: " + totalIncome + " - (PCDH:" + pcdh + " + PCKV:" + pckv + " + BHXH:" + ObjContributionsBLL.ValueBHXH + " + BHYT:" + ObjContributionsBLL.ValueBHYT + " + BHTN:" + ObjContributionsBLL.ValueBHTN + " + TienDem:" + tiendem + " + TienLamThem:" + tienLamThem + " + TroCapThaiSan:" + tcTS1Lan + " + TienAn:" + tienan + ")=" + totalIncomeCHIUTHUE + "<br/>";

        //        /// II. Tinh Thue Thu Nhap
        //        /// Muc chiu thue :     
        //        ///  level 1 :   0 -> 5.000.000 : 0%
        //        ///  level 2 :   5.000.001 -> 15.000.000 : 10%     
        //        ///  level 3 :   15.000.001 -> 25.000.000 : 20%
        //        ///  level 4 :   25.000.001 -> 40.000.000 : 30%
        //        ///  level 5 :   40.000.001 tro len : 40%
        //        ///  Neu tong thu nhap nam o level 3 thi ThueThuNhap phai tinh luon level 1,2,3
        //        ///  

        //        if (totalIncomeCHIUTHUE > 0)
        //        {
        //            string TinhthueLog;                    
        //            thueThuNhap = CalculateThueThuNhap(totalIncomeCHIUTHUE, out TinhthueLog);
        //            CalculatingLog += TinhthueLog;
        //        }

        //        CalculatingLog += "<br/> /******************** Tinh tong cac khoan phai nop ****************/ <br/>";
        //        totalContributions = ObjContributionsBLL.ValueBHXH + ObjContributionsBLL.ValueBHYT + ObjContributionsBLL.ValueKPCD /*+ thueThuNhap*/ + ObjContributionsBLL.ValueBHTN;
        //        CalculatingLog += "TienTongCacKhoanNop=BHXH:" + ObjContributionsBLL.ValueBHXH + "+BHYT:" + ObjContributionsBLL.ValueBHYT + "+KPCD:" + ObjContributionsBLL.ValueKPCD + /*"+thueThuNhap:" + thueThuNhap + */"+BHTN:" + ObjContributionsBLL.ValueBHTN + "=" + totalContributions + " <br/>";

        //        totalPeriod_I = totalIncome - totalContributions;
        //        CalculatingLog += "Lương Kỳ I=TienTongThuNhap:" + totalIncome + "-TienTongCacKhoanNop:" + totalContributions + "=" + totalPeriod_I + " <br/>";
        //        ///******************************************************************
        //        /// update salary period I to Incomes table
        //        ///******************************************************************

        //        IncomesBLL.UpdateByPeriod_II(
        //            (decimal)lns,
        //            (decimal)tienbosungluong,
        //            (decimal)tienthuong,
        //            (decimal)ObjContributionsBLL.ValueBHXH,
        //            (decimal)ObjContributionsBLL.ValueBHYT,
        //            (decimal)ObjContributionsBLL.ValueKPCD,
        //            (decimal)thueThuNhap,
        //            (decimal)totalIncome,
        //            (decimal)totalIncomeCHIUTHUE,
        //            CalculatingLog,
        //            objIncome.Lock,
        //            objIncome.Remark,                    
        //            objIncome.IncomeId,
        //            objIncome.UserId,
        //            objIncome.CreateDate,
        //            (decimal)totalPeriod_I,
        //            ObjProductivitySalaryBLL.FinalConversionLNSCoefficient,
        //            ObjProductivitySalaryBLL.FinalConversionLNSCoefficientLog,
        //            (decimal)ObjContributionsBLL.ValueBHTN
        //            );

        //        return 1;
        //    }
        //}

        //public int RunningPeriod_III(long incomeId)
        //{
        //    double LNSBalance = 0;
        //    double BonusBalance = 0;
        //    string CalculatingLog3 = string.Empty;

        //    LNSBalance = UnitPriceLNSBalance * FinalConversionLNSCoefficient;
        //    CalculatingLog3 += "LNSBalance = DonGiaDuLNS:" + UnitPriceLNSBalance + " x ChiSoPBQD:" + FinalConversionLNSCoefficient + " = " + LNSBalance + "<br/>";

        //    BonusBalance = UnitPriceBonusBalance * FinalConversionLNSCoefficient;
        //    CalculatingLog3 += "BonusBalance = DonGiaDuThuong:" + UnitPriceBonusBalance + " x ChiSoPBQD:" + FinalConversionLNSCoefficient + " = " + BonusBalance + "<br/>";

        //    IncomesBLL.UpdateByPeriod_III((decimal)LNSBalance, (decimal)BonusBalance, CalculatingLog3, incomeId, UserId, new DateTime(Year, Month, 1));

        //    return 1;
        //}

        private double CalculateThueThuNhap(double tongThuNhapChiuThue, out string TinhthueLog)
        {
            /// II. Tinh Thue Thu Nhap
            /// Muc chiu thue :     
            ///  level 1 :   = 5.000.000 : 5%
            ///  level 2 :   5.000.001 -> 10.000.000 : 10%     
            ///  level 3 :   10.000.001 -> 18.000.000 : 15%
            ///  level 4 :   18.000.001 -> 32.000.000 : 20%
            ///  level 5 :   32.000.001 -> 52.000.000 : 25%
            ///  level 6 :   52.000.001 -> 80.000.000 : 30%
            ///  level 7 :   tren 80.000.000 : 35%
            ///  Neu tong thu nhap nam o level 3 thi ThueThuNhap phai tinh nhu sau :
            ///  0.75trđ + 15% TNTT(thu nhap tinh thue) tren 10 trđ.

            TinhthueLog = string.Empty;

            double returnValueThuNhapChiuThue = 0;

            double Level1_from = 0;
            double Level1_to = 5000000;

            double Level2_from = 5000001;
            double Level2_to = 10000000;

            double Level3_from = 10000001;
            double Level3_to = 18000000;

            double Level4_from = 18000001;
            double Level4_to = 32000000;

            double Level5_from = 32000001;
            double Level5_to = 52000000;

            double Level6_from = 52000001;
            double Level6_to = 80000000;

            double Level7_to = 80000000;
            /// tinh thue giam tru gia canh
            /// 
            var list = PITDeductionBLL.GetByUserDate(UserId, Month, Year);
            TinhthueLog += "Tinh Giam Tru Gia Canh <br/>";
            TinhthueLog += "Giam cho ban than 4000,000 <br/>";
            double giamTruGiaCanh = 0;
            giamTruGiaCanh = 1600000*list.Count;
            TinhthueLog += "Giam cho " + list.Count + " nguoi phu thuoc : 1600,000 x " + list.Count + "=" +
                           giamTruGiaCanh + "<br/>";
            TinhthueLog += "tongThuNhapChiuThue=" + tongThuNhapChiuThue;
            tongThuNhapChiuThue = tongThuNhapChiuThue - 4000000 - giamTruGiaCanh;
            TinhthueLog += "- 4000,000 - " + giamTruGiaCanh + "=" + tongThuNhapChiuThue;


            if ((tongThuNhapChiuThue > Level1_from) && (tongThuNhapChiuThue <= Level1_to))
            {
                TinhthueLog += "tongThuNhapChiuThue:" + tongThuNhapChiuThue + ">" + Level1_from +
                               " va tongThuNhapChiuThue:" + tongThuNhapChiuThue + "<=" + Level1_to + "<br/>";
                var thueBac1 = (tongThuNhapChiuThue - 0)*0.05;
                returnValueThuNhapChiuThue = thueBac1;
                TinhthueLog += "thueBac1 = (tongThuNhapChiuThue:" + tongThuNhapChiuThue + " - 0) * 0.05 = " + thueBac1 +
                               "<br/>";
            }
            else if ((tongThuNhapChiuThue > Level2_from) && (tongThuNhapChiuThue <= Level2_to))
            {
                TinhthueLog += "tongThuNhapChiuThue:" + tongThuNhapChiuThue + ">" + Level2_from +
                               " va tongThuNhapChiuThue:" + tongThuNhapChiuThue + "<=" + Level2_to + "<br/>";

                var thueBac1 = 5000000*0.05;
                TinhthueLog += "thueBac1 = 5000000 * 0.05 = " + thueBac1 + "<br/>";
                var thueBac2 = (tongThuNhapChiuThue - 5000000)*0.1;
                TinhthueLog += "thueBac2 = (tongThuNhapChiuThue:" + tongThuNhapChiuThue + " - 5000000) * 0.1 = " +
                               thueBac2 + "<br/>";

                returnValueThuNhapChiuThue = thueBac1 + thueBac2;
                TinhthueLog += "TienThuNhapChiuThue = thueBac1: " + thueBac1 + " + thueBac2: " + thueBac2 + " = " +
                               returnValueThuNhapChiuThue + " <br/>";
            }
            else if ((tongThuNhapChiuThue > Level3_from) && (tongThuNhapChiuThue <= Level3_to))
            {
                TinhthueLog += "tongThuNhapChiuThue:" + tongThuNhapChiuThue + ">" + Level3_from +
                               " va tongThuNhapChiuThue:" + tongThuNhapChiuThue + "<=" + Level3_to + "<br/>";

                var thueBac1 = 5000000*0.05;
                TinhthueLog += "thueBac1 = 5000000 * 0.05 = " + thueBac1 + "<br/>";
                var thueBac2 = (10000000 - 5000000)*0.1;
                TinhthueLog += "thueBac2 = (10000000 - 5000000) * 0.1 = " + thueBac2 + "<br/>";
                var thueBac3 = (tongThuNhapChiuThue - 10000000)*0.15;
                TinhthueLog += "thueBac3 = (tongThuNhapChiuThue:" + tongThuNhapChiuThue + " - 10000000) * 0.15 = " +
                               thueBac3 + "<br/>";

                returnValueThuNhapChiuThue = thueBac1 + thueBac2 + thueBac3;
                TinhthueLog += "TienThuNhapChiuThue = thueBac1: " + thueBac1 + " + thueBac2: " + thueBac2 +
                               " + thueBac3: " + thueBac3 + " = " + returnValueThuNhapChiuThue + " <br/>";
            }
            else if ((tongThuNhapChiuThue > Level4_from) && (tongThuNhapChiuThue < Level4_to))
            {
                TinhthueLog += "tongThuNhapChiuThue:" + tongThuNhapChiuThue + ">" + Level4_from +
                               " va tongThuNhapChiuThue:" + tongThuNhapChiuThue + "<=" + Level4_to + "<br/>";

                var thueBac1 = 5000000*0.05;
                TinhthueLog += "thueBac1 = 5000000 * 0.05 = " + thueBac1 + "<br/>";
                var thueBac2 = (10000000 - 5000000)*0.1;
                TinhthueLog += "thueBac2 = (10000000 - 5000000) * 0.1 = " + thueBac2 + "<br/>";
                var thueBac3 = (18000000 - 10000000)*0.15;
                TinhthueLog += "thueBac3 = (18000000 - 10000000) * 0.15 = " + thueBac3 + "<br/>";
                var thueBac4 = (tongThuNhapChiuThue - 18000000)*0.2;
                TinhthueLog += "thueBac4 = (tongThuNhapChiuThue:" + tongThuNhapChiuThue + " - 18000000) * 0.2 = " +
                               thueBac4 + "<br/>";


                returnValueThuNhapChiuThue = thueBac1 + thueBac2 + thueBac3 + thueBac4;
                TinhthueLog += "TienThuNhapChiuThue = thueBac1: " + thueBac1 + " + thueBac2: " + thueBac2 +
                               " + thueBac3: " + thueBac3 + " + thueBac4: " + thueBac4 + " = " +
                               returnValueThuNhapChiuThue + " <br/>";
            }
            else if ((tongThuNhapChiuThue > Level5_from) && (tongThuNhapChiuThue < Level5_to))
            {
                TinhthueLog += "tongThuNhapChiuThue:" + tongThuNhapChiuThue + ">" + Level5_from +
                               " va tongThuNhapChiuThue:" + tongThuNhapChiuThue + "<=" + Level5_to + "<br/>";

                var thueBac1 = 5000000*0.05;
                TinhthueLog += "thueBac1 = 5000000 * 0.05 = " + thueBac1 + "<br/>";
                var thueBac2 = (10000000 - 5000000)*0.1;
                TinhthueLog += "thueBac2 = (10000000 - 5000000) * 0.1 = " + thueBac2 + "<br/>";
                var thueBac3 = (18000000 - 10000000)*0.15;
                TinhthueLog += "thueBac3 = (18000000 - 10000000) * 0.15 = " + thueBac3 + "<br/>";
                var thueBac4 = (32000000 - 18000000)*0.2;
                TinhthueLog += "thueBac4 = (32000000 - 18000000) * 0.2 = " + thueBac4 + "<br/>";
                var thueBac5 = (tongThuNhapChiuThue - 32000000)*0.25;
                TinhthueLog += "thueBac5 = (tongThuNhapChiuThue:" + tongThuNhapChiuThue + " - 32000000) * 0.25 = " +
                               thueBac5 + "<br/>";


                returnValueThuNhapChiuThue = thueBac1 + thueBac2 + thueBac3 + thueBac4 + thueBac5;
                TinhthueLog += "TienThuNhapChiuThue = thueBac1: " + thueBac1 + " + thueBac2: " + thueBac2 +
                               " + thueBac3: " + thueBac3 + " + thueBac4: " + thueBac4 + " + thueBac5: " + thueBac5 +
                               " = " + returnValueThuNhapChiuThue + " <br/>";
            }
            else if ((tongThuNhapChiuThue > Level6_from) && (tongThuNhapChiuThue < Level6_to))
            {
                TinhthueLog += "tongThuNhapChiuThue:" + tongThuNhapChiuThue + ">" + Level6_from +
                               " va tongThuNhapChiuThue:" + tongThuNhapChiuThue + "<=" + Level6_to + "<br/>";

                var thueBac1 = 5000000*0.05;
                TinhthueLog += "thueBac1 = 5000000 * 0.05 = " + thueBac1 + "<br/>";
                var thueBac2 = (10000000 - 5000000)*0.1;
                TinhthueLog += "thueBac2 = (10000000 - 5000000) * 0.1 = " + thueBac2 + "<br/>";
                var thueBac3 = (18000000 - 10000000)*0.15;
                TinhthueLog += "thueBac3 = (18000000 - 10000000) * 0.15 = " + thueBac3 + "<br/>";
                var thueBac4 = (32000000 - 18000000)*0.2;
                TinhthueLog += "thueBac4 = (32000000 - 18000000) * 0.2 = " + thueBac4 + "<br/>";
                var thueBac5 = (52000000 - 32000000)*0.25;
                TinhthueLog += "thueBac5 = (52000000 - 32000000) * 0.25 = " + thueBac5 + "<br/>";
                var thueBac6 = (tongThuNhapChiuThue - 52000000)*0.3;
                TinhthueLog += "thueBac6 = (tongThuNhapChiuThue:" + tongThuNhapChiuThue + " - 52000000) * 0.3 = " +
                               thueBac6 + "<br/>";


                returnValueThuNhapChiuThue = thueBac1 + thueBac2 + thueBac3 + thueBac4 + thueBac5 + thueBac6;
                TinhthueLog += "TienThuNhapChiuThue = thueBac1: " + thueBac1 + " + thueBac2: " + thueBac2 +
                               " + thueBac3: " + thueBac3 + " + thueBac4: " + thueBac4 + " + thueBac5: " + thueBac5 +
                               " + thueBac6: " + thueBac6 + " = " + returnValueThuNhapChiuThue + " <br/>";
            }
            else if (tongThuNhapChiuThue > Level7_to)
            {
                TinhthueLog += "tongThuNhapChiuThue:" + tongThuNhapChiuThue + ">" + Level7_to + "<br/>";

                var thueBac1 = 5000000*0.05;
                TinhthueLog += "thueBac1 = 5000000 * 0.05 = " + thueBac1 + "<br/>";
                var thueBac2 = (10000000 - 5000000)*0.1;
                TinhthueLog += "thueBac2 = (10000000 - 5000000) * 0.1 = " + thueBac2 + "<br/>";
                var thueBac3 = (18000000 - 10000000)*0.15;
                TinhthueLog += "thueBac3 = (18000000 - 10000000) * 0.15 = " + thueBac3 + "<br/>";
                var thueBac4 = (32000000 - 18000000)*0.2;
                TinhthueLog += "thueBac4 = (32000000 - 18000000) * 0.2 = " + thueBac4 + "<br/>";
                var thueBac5 = (52000000 - 32000000)*0.25;
                TinhthueLog += "thueBac5 = (52000000 - 32000000) * 0.25 = " + thueBac5 + "<br/>";
                var thueBac6 = (80000000 - 52000000)*0.3;
                TinhthueLog += "thueBac6 = (80000000 - 52000000) * 0.3 = " + thueBac6 + "<br/>";
                var thueBac7 = (tongThuNhapChiuThue - 80000000)*0.35;
                TinhthueLog += "thueBac7 = (tongThuNhapChiuThue:" + tongThuNhapChiuThue + " - 52000000) * 0.35 = " +
                               thueBac7 + "<br/>";


                returnValueThuNhapChiuThue = thueBac1 + thueBac2 + thueBac3 + thueBac4 + thueBac5 + thueBac6 + thueBac7;
                TinhthueLog += "TienThuNhapChiuThue = thueBac1: " + thueBac1 + " + thueBac2: " + thueBac2 +
                               " + thueBac3: " + thueBac3 + " + thueBac4: " + thueBac4 + " + thueBac5: " + thueBac5 +
                               " + thueBac6: " + thueBac6 + " + thueBac7: " + thueBac7 + " = " +
                               returnValueThuNhapChiuThue + " <br/>";
            }
            return returnValueThuNhapChiuThue;
        }

        #region private fields

        private AllowancesBLL _ObjAllowancesBLL;
        private ContributionsBLL _ObjContributionsBLL;
        private OverTimeSalaryBLL _ObjOverTimeSalaryBLL;
        private ProductivitySalaryBLL _ObjProductivitySalaryBLL;
        private BasicSalaryBLL _ObjBasicSalaryBLL;


        //----------------------------------------

        #endregion

        #region properties

        private AllowancesBLL ObjAllowancesBLL
        {
            get
            {
                if (_ObjAllowancesBLL == null)
                    _ObjAllowancesBLL = new AllowancesBLL();

                return _ObjAllowancesBLL;
            }
        }

        private ContributionsBLL ObjContributionsBLL
        {
            get
            {
                if (_ObjContributionsBLL == null)
                    _ObjContributionsBLL = new ContributionsBLL();

                return _ObjContributionsBLL;
            }
        }

        private OverTimeSalaryBLL ObjOverTimeSalaryBLL
        {
            get
            {
                if (_ObjOverTimeSalaryBLL == null)
                    _ObjOverTimeSalaryBLL = new OverTimeSalaryBLL();
                return _ObjOverTimeSalaryBLL;
            }
        }

        private ProductivitySalaryBLL ObjProductivitySalaryBLL
        {
            get
            {
                if (_ObjProductivitySalaryBLL == null)
                    _ObjProductivitySalaryBLL = new ProductivitySalaryBLL();

                return _ObjProductivitySalaryBLL;
            }
        }

        private BasicSalaryBLL ObjBasicSalaryBLL
        {
            get
            {
                if (_ObjBasicSalaryBLL == null)
                    _ObjBasicSalaryBLL = new BasicSalaryBLL();

                return _ObjBasicSalaryBLL;
            }
        }

        public WorkdayEmployeesBLLL ObjWorkdayEmployeesBLLL { get; set; }

        public int UserId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public double NCTrongThang { get; set; }

        public double DonGiaLNS { get; set; }

        public double DonGiaLCB { get; set; }

        public double DonGiaTienAn { get; set; }

        public double DonGiaTienThuong { get; set; }


        public double UnitPriceLNSBalance { get; set; }

        public double UnitPriceBonusBalance { get; set; }

        //----------------------------------------
        public double FinalConversionLNSCoefficient { get; set; }

        #endregion
    }
}