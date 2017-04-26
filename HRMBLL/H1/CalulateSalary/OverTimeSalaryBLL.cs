using HRMBLL.H1.Helper;

namespace HRMBLL.H1.CalulateSalary
{
    public class OverTimeSalaryBLL
    {
        public string TienAnLog { get; set; } = string.Empty;

        public string TienDemLog { get; set; } = string.Empty;

        /// <summary>
        ///     TInh tien an giua ca
        ///     (tien an trong 1 thang * NC_LamViecThucTe)/NgayCongQDTrongThang
        /// </summary>
        /// <returns></returns>
        public void CalculateTienAn()
        {
            var tienAnMotNgay = DON_GIA_TIENAN/ObjWorkdayEmployeesBLLL.XQDL;
            TienAnLog += "tienAnMotNgay = DonGiaTienAn:" + DON_GIA_TIENAN + "/XQD:" + ObjWorkdayEmployeesBLLL.XQDL + "=" +
                         tienAnMotNgay + "<br/>";
            if (ObjWorkdayEmployeesBLLL.F_LeL > 0)
            {
                TienAnLog += "TienAnTemp = TienAnTemp:" + ValueTIENAN;
                ValueTIENAN += tienAnMotNgay*ObjWorkdayEmployeesBLLL.F_LeL;
                TienAnLog += " + tienAnMotNgay:" + tienAnMotNgay + " x LE:" + ObjWorkdayEmployeesBLLL.F_LeL + "=" +
                             ValueTIENAN + "<br/>";
            }
            if (ObjWorkdayEmployeesBLLL.XL > 0)
            {
                TienAnLog += "TienAnTemp = TienAnTemp:" + ValueTIENAN;
                ValueTIENAN += tienAnMotNgay*ObjWorkdayEmployeesBLLL.XL;
                TienAnLog += " + tienAnMotNgay:" + tienAnMotNgay + " x X:" + ObjWorkdayEmployeesBLLL.XL + "=" +
                             ValueTIENAN + "<br/>";
            }
        }

        /// <summary>
        ///     Tinh tien lam dem
        ///     tien1giodem  = (HeSoLCB  + HeSoPCTN + HeSoPCCV + HeSoPCKV + HeSoPCDH) * DonGiaLCB * 0.3/ (8*XQDL);
        ///     tienlamdem = tien1giodem * giodem;
        /// </summary>
        /// <returns></returns>
        public void CalculateTienDem()
        {
            // Tinh tien lam dem

            if (!DefaultValues.IsNotCalculateNightTimeDepartment(ObjWorkdayEmployeesBLLL.DepartmentId))
            {
                double totalCoe = 0;
                if (ObjCoefficientEmployeeFinalBLL != null)
                {
                    totalCoe = ObjCoefficientEmployeeFinalBLL.LCB + ObjCoefficientEmployeeFinalBLL.PCTN +
                               ObjCoefficientEmployeeFinalBLL.PCCV + ObjCoefficientEmployeeFinalBLL.PCKV +
                               ObjCoefficientEmployeeFinalBLL.PCDH;
                    TienDemLog += "TongHeSo = LCB:" + ObjCoefficientEmployeeFinalBLL.LCB + " + PCTN:" +
                                  ObjCoefficientEmployeeFinalBLL.PCTN + " + PCCV:" + ObjCoefficientEmployeeFinalBLL.PCCV +
                                  " + PCKV:" + ObjCoefficientEmployeeFinalBLL.PCKV + " + PCKV:" +
                                  ObjCoefficientEmployeeFinalBLL.PCKV;
                }
                if (totalCoe > 0)
                {
                    var tienMotGioDem = totalCoe*DON_GIA_LCB*0.3/(ObjWorkdayEmployeesBLLL.XQDL*8);
                    TienDemLog += "tienMotGioDem = (TongHeSo:" + totalCoe + " x DonGiaLCB:" + DON_GIA_LCB +
                                  " x 0.3)/(XQD:" + ObjWorkdayEmployeesBLLL.XQDL + " x 8)=" + tienMotGioDem;
                    ValueTIENDEM = tienMotGioDem*ObjWorkdayEmployeesBLLL.NightTimeL;
                    TienDemLog += "tienMotGioDem:" + tienMotGioDem + " x GioDem:" + ObjWorkdayEmployeesBLLL.NightTimeL +
                                  " = " + ValueTIENDEM;
                }
            }
        }

        #region private fields

        #endregion

        #region properties

        public CoefficientEmployeeFinalBLL ObjCoefficientEmployeeFinalBLL { get; set; }

        public WorkdayEmployeesBLLL ObjWorkdayEmployeesBLLL { get; set; }

        public double ValueTIENDEM { get; set; }

        public double ValueTIENAN { get; set; }

        /// <summary>
        ///     Don gia luong can ban
        /// </summary>
        public double DON_GIA_LCB { get; set; }

        /// <summary>
        ///     Don gia tien an
        /// </summary>
        public double DON_GIA_TIENAN { get; set; }

        #endregion
    }
}