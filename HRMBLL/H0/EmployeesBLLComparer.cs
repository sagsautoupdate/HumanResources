using System;
using System.Collections.Generic;
using HRMUtil.KeyNames.H0;

namespace HRMBLL.H0
{
    public class EmployeesBLLComparer : IComparer<EmployeesBLL>
    {
        private readonly bool _reverse;
        private readonly string _sortColumn;

        public EmployeesBLLComparer(string sortEx)
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

        public int Compare(EmployeesBLL x, EmployeesBLL y)
        {
            var retVal = 0;
            switch (_sortColumn)
            {
                case EmployeeKeys.FIELD_EMPLOYEES_USERID:
                    retVal = x.UserId - y.UserId;
                    break;
                case EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE:
                    retVal = string.Compare(x.EmployeeCode, y.EmployeeCode, StringComparison.InvariantCultureIgnoreCase);
                    break;
                case EmployeeKeys.FIELD_EMPLOYEES_FULLNAME:
                    retVal = string.Compare(x.FullName, y.FullName, StringComparison.InvariantCultureIgnoreCase);
                    break;
                case PositionKeys.FIELD_POSITION_NAME:
                    retVal = string.Compare(x.PositionName, y.PositionName, StringComparison.InvariantCultureIgnoreCase);
                    break;
                case DepartmentKeys.FIELD_DEPARTMENT_DEPARTMENTFULLNAME:
                    retVal = string.Compare(x.DepartmentFullName, y.DepartmentFullName,
                        StringComparison.InvariantCultureIgnoreCase);
                    break;
                case DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID:
                    retVal = x.RootId - y.RootId;
                    break;
                case EmployeeKeys.FIELD_EMPLOYEES_JOINDATE:
                    retVal = DateTime.Compare(x.JoinDate, y.JoinDate);
                    break;
                case EmployeeKeys.FIELD_EMPLOYEES_LeaveDate:
                    retVal = DateTime.Compare(x.LeaveDate, y.LeaveDate);
                    break;
                case PositionKeys.Field_Position_LevelPosition:
                    retVal = x.LevelPosition - y.LevelPosition;
                    break;
            }
            return retVal*(_reverse ? -1 : 1);
        }

        public bool Equals(EmployeesBLL x, EmployeesBLL y)
        {
            if (x.UserId == y.UserId)
                return true;
            return false;
        }

        public int GetHashCode(EmployeesBLL obj)
        {
            // TODO: Implement this, but it's not necessary for sorting
            return 0;
        }
    }
}