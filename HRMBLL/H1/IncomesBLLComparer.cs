using System;
using System.Collections.Generic;
using System.Text;

using HRMUtil.KeyNames.H1;
using HRMUtil.KeyNames.H0;

namespace HRMBLL.H1
{
    public class IncomesBLLComparer : IComparer<IncomesBLL>
    {
        private string _sortColumn;
        private bool _reverse;

        public IncomesBLLComparer(string sortEx)
        {
            if (!String.IsNullOrEmpty(sortEx))
            {
                _reverse = sortEx.ToLowerInvariant().EndsWith(" desc");
                if (_reverse)
                    _sortColumn = sortEx.Substring(0, sortEx.Length - 5);
                else
                    _sortColumn = sortEx.Substring(0, sortEx.Length - 4);
            }
        }

        public bool Equals(IncomesBLL x, IncomesBLL y)
        {
            if (x.UserId == y.UserId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int Compare(IncomesBLL x, IncomesBLL y)
        {
            int retVal = 0;
            switch (_sortColumn)
            {

                case EmployeeKeys.FIELD_EMPLOYEES_FULLNAME:
                    retVal = String.Compare(x.FullName, y.FullName, StringComparison.InvariantCultureIgnoreCase);
                    break;
                case IncomeKeys.Field_Income_LNS:
                    retVal = (int)(x.LNS - y.LNS);
                    break;
                case IncomeKeys.Field_Income_LCBNN:
                    retVal = (int)(x.LCBNN - y.LCBNN);
                    break;
                case IncomeKeys.Field_Income_PCCV:
                    retVal = (int)(x.PCCV - y.PCCV);
                    break;
                case IncomeKeys.Field_Income_PCTN:
                    retVal = (int)(x.PCTN - y.PCTN);
                    break;
                case IncomeKeys.Field_Income_PCDH:
                    retVal = (int)(x.PCDH - y.PCDH);
                    break;
                case IncomeKeys.Field_Income_TienAn:
                    retVal = (int)(x.TienAn - y.TienAn);
                    break;
                case IncomeKeys.Field_Income_BoSungLuong:
                    retVal = (int)(x.BoSungLuong - y.BoSungLuong);
                    break;
                case IncomeKeys.Field_Income_TotalIncome:
                    retVal = (int)(x.TotalIncome - y.TotalIncome);
                    break;
                case IncomeKeys.Field_Income_TrBHXH:
                    retVal = (int)(x.TrBHXH - y.TrBHXH);
                    break;
                case IncomeKeys.Field_Income_TrBHYT:
                    retVal = (int)(x.TrBHYT - y.TrBHYT);
                    break;
                case IncomeKeys.Field_Income_TrDoanPhi:
                    retVal = (int)(x.TrDoanPhi - y.TrDoanPhi);
                    break;
                case "ThueThuNhap":
                    retVal = (int)(x.ThueThuNhap - y.ThueThuNhap);
                    break;
                case "RealIncome":
                    retVal = (int)(x.RealIncome - y.RealIncome);
                    break;
            }
            return (retVal * (_reverse ? -1 : 1));
        }

        public int GetHashCode(HTCVEmployeeBLL obj)
        {
            // TODO: Implement this, but it's not necessary for sorting
            return 0;
        }
    }
}
