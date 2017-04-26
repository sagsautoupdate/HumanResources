using System;
using System.Data;
using HRMDAL.H2;

namespace HRMBLL.H2
{
    public class CandidateContractionsBLL
    {
        // Fields
        // Properties
        public int Acitve { get; set; }

        public int CandidateContractId { get; set; }

        public int CandidateId { get; set; }

        public int ContractNo { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime FromDate { get; set; }

        public string Remark { get; set; }

        public DateTime ToDate { get; set; }

        // Methods
        public static DataRow DR_GetOne(int CandidateContractId)
        {
            var byId = new CandidateContractionsDAL().GetById(CandidateContractId);
            if (byId.Rows.Count > 0)
                return byId.Rows[0];
            return null;
        }

        public static DataTable DT_GetAll()
        {
            return new CandidateContractionsDAL().GetAll();
        }

        public static DataTable DT_GetOne(int CandidateContractId)
        {
            return new CandidateContractionsDAL().GetById(CandidateContractId);
        }

        public static long Insert(int CandidateId, DateTime FromDate, DateTime ToDate, string Remark,
            int EducationHighestLevelId, int PositionIdFee, int SessionIdFee)
        {
            return new CandidateContractionsDAL().Insert(CandidateId, FromDate, ToDate, Remark, EducationHighestLevelId,
                PositionIdFee, SessionIdFee);
        }

        public static long Update(int CandidateContractId, DateTime FromDate, DateTime ToDate, string Remark,
            int EducationHighestLevelId, int PositionIdFee, int SessionIdFee)
        {
            return new CandidateContractionsDAL().Update(CandidateContractId, FromDate, ToDate, Remark,
                EducationHighestLevelId, PositionIdFee, SessionIdFee);
        }
    }
}