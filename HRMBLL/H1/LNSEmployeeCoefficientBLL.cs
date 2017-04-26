using System;
using System.Data;
using HRMDAL.H1;

namespace HRMBLL.H1
{
    public class LNSEmployeeCoefficientBLL
    {
        public static DataTable Get_ByFilter(int UserId, int LNSCoefficientEmployeesId, string DepartmentIds)
        {
            return new LNSEmployeeCoefficientDAL().Get_ByFilter(UserId, LNSCoefficientEmployeesId, DepartmentIds);
        }

        public static DataTable Get_ById(int UserId, int LNSCoefficientEmployeesId, int Active)
        {
            return new LNSEmployeeCoefficientDAL().Get_ById(UserId, LNSCoefficientEmployeesId, Active);
        }

        public static DataRow GetDR_ById(int UserId, int LNSCoefficientEmployeesId, int Active)
        {
            var oneByUserName = new LNSEmployeeCoefficientDAL().Get_ById(UserId, LNSCoefficientEmployeesId, Active);
            if (oneByUserName.Rows.Count > 0)
                return oneByUserName.Rows[0];
            return null;
        }

        public static long Save(int LNSCoefficientEmployeesId,
            int UserId,
            int ScaleOfSalaryId,
            int CoefficientLevel,
            double Ratio,
            double TheoreticalValue,
            double ActualValue,
            DateTime FromDate,
            DateTime ToDate,
            DateTime CreatedDate,
            int Active,
            string Remark)
        {
            return new LNSEmployeeCoefficientDAL().Save(LNSCoefficientEmployeesId,
                UserId,
                ScaleOfSalaryId,
                CoefficientLevel,
                Ratio,
                TheoreticalValue,
                ActualValue,
                FromDate,
                ToDate,
                CreatedDate,
                Active,
                Remark);
        }
    }
}