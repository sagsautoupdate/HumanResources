using System;
using System.Text;

namespace HRMDAL.H1.KeyNames
{
    sealed public class IncomeKeys
    {

        /// <summary>
        /// Some field names of Incomes table
        /// </summary>
        /// 

        public const string FIELD_INCOME_ID = "IncomeId";
        public const string FIELD_INCOME_LNS = "LNS";
        public const string FIELD_INCOME_LCBNN = "LCBNN";
        public const string FIELD_INCOME_PCCV = "PCCV";
        public const string FIELD_INCOME_PCTN = "PCTN";
        public const string FIELD_INCOME_PCDH = "PCDH";
        public const string FIELD_INCOME_TCBHXH = "TCBHXH";
        public const string FIELD_INCOME_TIEN_AN = "TienAn";
        public const string FIELD_INCOME_BO_SUNG_LUONG = "BoSungLuong";
        public const string FIELD_INCOME_TIEN_THEM_GIO = "TienThemGio";
        public const string FIELD_INCOME_TIEN_LAM_DEM = "TienLamDem";
        public const string FIELD_INCOME_TIEN_THUONG = "TienThuong";
        public const string FIELD_INCOME_TRICH_BHXH = "TrBHXH";
        public const string FIELD_INCOME_TRICH_BHYT = "TrBHYT";
        public const string FIELD_INCOME_TRICH_DOAN_PHI = "TrDoanPhi";
        public const string FIELD_INCOME_THUE_THU_NHAP = "ThueThuNhap";
        public const string FIELD_INCOME_TOTAL_INCOME = "TotalIncome";
        public const string FIELD_INCOME_TOTAL_INCOME_FOR_TAX = "TotalIncomeForTax";
        public const string FIELD_INCOME_CREATE_DATE = "CreateDate";
        public const string FIELD_INCOME_LOCK = "Lock";

        /// <summary>
        /// StoreProcedure name of Incomes object.
        /// </summary>
        public const string SP_INCOME_INSERT = "Ins_H1_Income";

        public const string SP_INCOME_GET_BY_FILTER = "Sel_H1_EmployeeIncomeByFilter";
        public const string SP_INCOME_GET_BY_USER_ID_AND_DATE = "Sel_H1_EmployeeIncome_By_UserId_Date";

    }
}
