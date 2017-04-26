using System.Data;
using HRMDAL.H4;

namespace HRMBLL.H4
{
    public class RepresentativeBLL
    {
        #region public method Get

        public static DataTable GetByFilter(int InvestorNo, string FullName, int IsOk, int KTTCDB)
        {
            return new RepresentativeDAL().GetByFilter(InvestorNo, FullName, IsOk, KTTCDB);
        }

        public static DataRow GetForTotal(int InvestorNo, string FullName, int IsOk)
        {
            var dt = new RepresentativeDAL().GetForTotal(InvestorNo, FullName, IsOk);

            if (dt.Rows.Count == 1)
                return dt.Rows[0];
            return null;
        }

        public static DataTable GetByAttorneyCode(int AttorneyCode)
        {
            return new RepresentativeDAL().GetByAttorneyCode(AttorneyCode);
        }

        #endregion

        #region methods Insert, update, delete

        public static void Insert(int InvestorNo, string RepresentativeName, string RepresentativeName2, double Stock,
            double StockRatio, double AttorneyCode, double HDQT_Vote, double HDQT_A, double HDQT_B, double HDQT_C,
            double HDQT_D, double HDQT_E, double HDQT_F, double HDQT_G, double BKS_Vote, double BKS_A, double BKS_B,
            double BKS_C, int IsOk, string Remark)
        {
            new RepresentativeDAL().Insert(InvestorNo, RepresentativeName, RepresentativeName2, Stock, StockRatio,
                AttorneyCode, HDQT_Vote, HDQT_A, HDQT_B, HDQT_C, HDQT_D, HDQT_E, HDQT_F, HDQT_G, BKS_Vote, BKS_A, BKS_B,
                BKS_C, IsOk, Remark);
        }

        public static void UpdateForAttorneyCode(int InvestorNo, int AttorneyCode)
        {
            new RepresentativeDAL().UpdateForAttorneyCode(InvestorNo, AttorneyCode);
        }

        public static void UpdateForCheck(int InvestorNo, int IsOk)
        {
            new RepresentativeDAL().UpdateForCheck(InvestorNo, IsOk);
        }


        public static void UpdateForHDQT(int RepresentativeId, double HDQT_A, double HDQT_B, double HDQT_C,
            double HDQT_D, double HDQT_E, double HDQT_F, double HDQT_G, bool HDQT_IsValid)
        {
            new RepresentativeDAL().UpdateForHDQT(RepresentativeId, HDQT_A, HDQT_B, HDQT_C, HDQT_D, HDQT_E, HDQT_F,
                HDQT_G, HDQT_IsValid);
        }

        public static void UpdateForBKS(int RepresentativeId, double BKS_A, double BKS_B, double BKS_C, int BKS_IsValid)
        {
            new RepresentativeDAL().UpdateForBKS(RepresentativeId, BKS_A, BKS_B, BKS_C, BKS_IsValid);
        }

        //public static void Delete(int decisionId)
        //{
        //    new RepresentativeDAL().Delete(decisionId);
        //}

        #endregion
    }
}