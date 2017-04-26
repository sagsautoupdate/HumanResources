using System;
using System.Data;
using HRMDAL.H1;

namespace HRMBLL.H1
{
    public class BonusEmployeeConditionBLL
    {
        public static DataTable GetByFilter(int bonusyear, DateTime paydate, int bonusTitleId, string departmentIds)
        {
            return new BonusEmployeeConditionDAL().GetByFilter(bonusyear, paydate, bonusTitleId, departmentIds);
        }

        public static long Update(int MonthNumber, int MonthNumber_tv, decimal HSLNS, decimal HSK, decimal NC_X,
            decimal NC_OmCoKHH, decimal NC_ThaiSan, decimal NC_TNLD,
            decimal NC_FDiDuong, decimal NC_Khac, decimal NgayCongThuong, decimal HSLNSThuong, decimal BonusValue,
            decimal ThueTamTinh, decimal ThueQuyetToan, decimal ThucLinh, string BonusTitleDetail, int UserId,
            int BonusTitleId, int BonusYear, DateTime PayDate)
        {
            return new BonusEmployeeConditionDAL().Update(MonthNumber, MonthNumber_tv, HSLNS, HSK, NC_X, NC_OmCoKHH,
                NC_ThaiSan, NC_TNLD, NC_FDiDuong, NC_Khac, NgayCongThuong, HSLNSThuong, BonusValue, ThueTamTinh,
                ThueQuyetToan, ThucLinh, BonusTitleDetail, UserId, BonusTitleId, BonusYear, PayDate);
        }
    }
}