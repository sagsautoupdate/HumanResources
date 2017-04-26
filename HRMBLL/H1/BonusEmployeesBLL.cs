using System;
using System.Data;
using HRMDAL.H1;

namespace HRMBLL.H1
{
    public class BonusEmployeesBLL
    {
        public static DataTable GetByFilter(int bonusyear, DateTime paydate, int bonusnameId)
        {
            return new BonusEmployeesDAL().GetByFilter(bonusyear, paydate, bonusnameId);
        }
    }
}