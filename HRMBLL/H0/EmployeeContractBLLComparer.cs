using System;
using System.Collections.Generic;
using HRMUtil.KeyNames.H0;

namespace HRMBLL.H0
{
    public class EmployeeContractBLLComparer : IComparer<EmployeeContractBLL>
    {
        private readonly bool _reverse;
        private readonly string _sortColumn;

        public EmployeeContractBLLComparer(string sortEx)
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

        public int Compare(EmployeeContractBLL x, EmployeeContractBLL y)
        {
            var retVal = 0;
            switch (_sortColumn)
            {
                case ContractTypeKeys.Field_ContractTypes_ContractTypeName:
                    retVal = string.Compare(x.ContractTypeName, y.ContractTypeName,
                        StringComparison.InvariantCultureIgnoreCase);
                    break;
                case EmployeeKeys.FIELD_EMPLOYEES_FULLNAME:
                    retVal = string.Compare(x.FullName, y.FullName, StringComparison.InvariantCultureIgnoreCase);
                    break;
                case PositionKeys.FIELD_POSITION_NAME:
                    retVal = string.Compare(x.PositionName, y.PositionName, StringComparison.InvariantCultureIgnoreCase);
                    break;
                case EmployeeContractKeys.Field_EmployeeContract_FromDate:
                    retVal = DateTime.Compare(x.FromDate, y.FromDate);
                    break;
                case EmployeeContractKeys.Field_EmployeeContract_ToDate:
                    retVal = DateTime.Compare(x.ToDate, y.ToDate);
                    break;
            }
            return retVal*(_reverse ? -1 : 1);
        }

        public bool Equals(EmployeeContractBLL x, EmployeeContractBLL y)
        {
            if (x.EmployeeContractId == y.EmployeeContractId)
                return true;
            return false;
        }

        public int GetHashCode(EmployeeContractBLL obj)
        {
            // TODO: Implement this, but it's not necessary for sorting
            return 0;
        }
    }
}