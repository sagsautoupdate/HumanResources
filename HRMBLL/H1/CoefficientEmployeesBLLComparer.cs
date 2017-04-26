using System;
using System.Collections.Generic;
using HRMUtil.KeyNames.H0;
using HRMUtil.KeyNames.H1;

namespace HRMBLL.H1
{
    public class CoefficientEmployeesBLLComparer : IComparer<CoefficientEmployeesBLL>
    {
        private readonly bool _reverse;
        private readonly string _sortColumn;

        public CoefficientEmployeesBLLComparer(string sortEx)
        {
            if (!string.IsNullOrEmpty(sortEx))
            {
                _reverse = sortEx.ToLowerInvariant().EndsWith(" desc");
                if (_reverse)
                    _sortColumn = sortEx.Substring(0, sortEx.Length - 5);
                else
                    _sortColumn = sortEx.Substring(0, sortEx.Length - 4);
            }
        }

        public int Compare(CoefficientEmployeesBLL x, CoefficientEmployeesBLL y)
        {
            var retVal = 0;
            switch (_sortColumn)
            {
                case EmployeeKeys.FIELD_EMPLOYEES_USERID:
                    retVal = x.UserId - y.UserId;
                    break;
                case EmployeeKeys.FIELD_EMPLOYEES_FULLNAME:
                    retVal = string.Compare(x.FullName, y.FullName, StringComparison.InvariantCultureIgnoreCase);
                    break;
                case PositionKeys.FIELD_POSITION_NAME:
                    retVal = string.Compare(x.PositionName, y.PositionName, StringComparison.InvariantCultureIgnoreCase);
                    break;
                case "LNS":
                    retVal = (int) (x.LNS - y.LNS);
                    break;
                case "LCB":
                    retVal = (int) (x.LCB - y.LCB);
                    break;
                case CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_PCDH:
                    retVal = (int) (x.PCDH - y.PCDH);
                    break;
                case CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_PCTN:
                    retVal = (int) (x.PCTN - y.PCTN);
                    break;
                case CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_PCCV:
                    retVal = (int) (x.PCCV - y.PCCV);
                    break;
                case CoefficientEmployeeKeys.FIELD_COEFFICIENT_EMPLOYEE_PCKV:
                    retVal = (int) (x.PCKV - y.PCKV);
                    break;
            }
            return retVal*(_reverse ? -1 : 1);
        }

        public bool Equals(CoefficientEmployeesBLL x, CoefficientEmployeesBLL y)
        {
            if (x.CoefficientEmployeeId == y.CoefficientEmployeeId)
                return true;
            return false;
        }

        public int GetHashCode(HTCVEmployeeBLL obj)
        {
            // TODO: Implement this, but it's not necessary for sorting
            return 0;
        }
    }
}