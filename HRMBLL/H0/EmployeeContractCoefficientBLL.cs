using HRMDAL.H0;

namespace HRMBLL.H0
{
    public class EmployeeContractCoefficientBLL
    {
        public int EmployeeContractId { get; set; }

        public int LCB_CoefficientEmployeeId { get; set; }

        public int LNS_CoefficientEmployeeId { get; set; }

        public static long Insert(int employeeContractId, int lCB_CoefficientEmployeeId, int lNS_CoefficientEmployeeId)
        {
            return new EmployeeContractCoefficientDAL().Insert(employeeContractId, lCB_CoefficientEmployeeId,
                lNS_CoefficientEmployeeId);
        }

        public static long Delete(int employeeContractId, int lCB_CoefficientEmployeeId, int lNS_CoefficientEmployeeId)
        {
            return new EmployeeContractCoefficientDAL().Delete(employeeContractId, lCB_CoefficientEmployeeId,
                lNS_CoefficientEmployeeId);
        }
    }
}