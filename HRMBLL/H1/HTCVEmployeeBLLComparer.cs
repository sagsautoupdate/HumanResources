using System;
using System.Collections.Generic;
using HRMUtil.KeyNames.H1;

namespace HRMBLL.H1
{
    public class HTCVEmployeeBLLComparer : IComparer<HTCVEmployeeBLL>
    {
        private readonly bool _reverse;
        private readonly string _sortColumn;

        public HTCVEmployeeBLLComparer(string sortEx)
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

        public int Compare(HTCVEmployeeBLL x, HTCVEmployeeBLL y)
        {
            var retVal = 0;
            switch (_sortColumn)
            {
                case HTCVCatalogueKeys.Field_HTCVCatalogue_ParentId:
                    retVal = x.ParentId - y.ParentId;
                    break;
                case HTCVEmployeeKeys.Field_HTCVEmployee_MarkDate:
                    //if (x.MarkDate.Equals(HRMUtil.FormatDate.GetSQLDateMinValue) || y.MarkDate.Equals(HRMUtil.FormatDate.GetSQLDateMinValue))
                    //{
                    //    retVal = 0;
                    //}
                    //else
                    //{
                    retVal = DateTime.Compare(x.MarkDate, y.MarkDate);
                    //}
                    break;
            }
            return retVal*(_reverse ? -1 : 1);
        }

        public bool Equals(HTCVEmployeeBLL x, HTCVEmployeeBLL y)
        {
            if (x.HTCVEmployeeId == y.HTCVEmployeeId)
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