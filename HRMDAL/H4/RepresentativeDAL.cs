using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H4;

namespace HRMDAL.H4
{
    public class RepresentativeDAL : Dao
    {
        #region Methods Get

        public DataTable GetByFilter(int InvestorNo, string FullName, int IsOk, int KTTCDB)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@InvestorNo", SqlDbType.Int, 4),
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@IsOk", SqlDbType.Int, 4),
                    new SqlParameter("@KTTCDB", SqlDbType.Int, 4)
                };

                param[0].Value = InvestorNo;
                param[1].Value = FullName;
                param[2].Value = IsOk;
                param[3].Value = KTTCDB;

                sproc = new StoreProcedure(RepresentativeKeys.Sp_Sel_H4_Representative_By_Filter, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetForTotal(int InvestorNo, string FullName, int IsOk)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@InvestorNo", SqlDbType.Int, 4),
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@IsOk", SqlDbType.Int, 4)
                };

                param[0].Value = InvestorNo;
                param[1].Value = FullName;
                param[2].Value = IsOk;

                sproc = new StoreProcedure(RepresentativeKeys.Sp_Sel_H4_Representative_For_Total, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByAttorneyCode(int AttorneyCode)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@AttorneyCode", SqlDbType.Int, 4)
                };

                param[0].Value = AttorneyCode;

                sproc = new StoreProcedure(RepresentativeKeys.Sp_Sel_H4_Representative_By_AttorneyCode, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        #endregion

        #region methods inset, update , delete

        public int Insert(int InvestorNo, string RepresentativeName, string RepresentativeName2, double Stock,
            double StockRatio, double AttorneyCode, double HDQT_Vote, double HDQT_A, double HDQT_B, double HDQT_C,
            double HDQT_D, double HDQT_E, double HDQT_F, double HDQT_G, double BKS_Vote, double BKS_A, double BKS_B,
            double BKS_C, int IsOk, string Remark)
        {
            Debug.Assert(sproc == null);
            var identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@InvestorNo", SqlDbType.Int, 4),
                    new SqlParameter("@RepresentativeName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@RepresentativeName2", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Stock", SqlDbType.Float),
                    new SqlParameter("@StockRatio", SqlDbType.Float),
                    new SqlParameter("@AttorneyCode", SqlDbType.Int, 4),
                    new SqlParameter("@HDQT_Vote", SqlDbType.Float),
                    new SqlParameter("@HDQT_A", SqlDbType.Float),
                    new SqlParameter("@HDQT_B", SqlDbType.Float),
                    new SqlParameter("@HDQT_C", SqlDbType.Float),
                    new SqlParameter("@HDQT_D", SqlDbType.Float),
                    new SqlParameter("@HDQT_E", SqlDbType.Float),
                    new SqlParameter("@HDQT_F", SqlDbType.Float),
                    new SqlParameter("@HDQT_G", SqlDbType.Float),
                    new SqlParameter("@BKS_Vote", SqlDbType.Float),
                    new SqlParameter("@BKS_A", SqlDbType.Float),
                    new SqlParameter("@BKS_B", SqlDbType.Float),
                    new SqlParameter("@BKS_C", SqlDbType.Float),
                    new SqlParameter("@IsOk", SqlDbType.Int, 4),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 254)
                };


                param[0].Value = InvestorNo;
                param[1].Value = RepresentativeName;
                param[2].Value = RepresentativeName2;
                param[3].Value = Stock;
                param[4].Value = StockRatio;
                param[5].Value = AttorneyCode;
                param[6].Value = HDQT_Vote;
                param[7].Value = HDQT_A;
                param[8].Value = HDQT_B;
                param[9].Value = HDQT_C;
                param[10].Value = HDQT_D;
                param[11].Value = HDQT_E;
                param[12].Value = HDQT_F;
                param[13].Value = HDQT_G;
                param[14].Value = BKS_Vote;
                param[15].Value = BKS_A;
                param[16].Value = BKS_B;
                param[17].Value = BKS_C;
                param[18].Value = IsOk;
                param[19].Value = Remark;


                sproc = new StoreProcedure(RepresentativeKeys.Sp_Ins_H4_Representative, param);
                identity = sproc.RunInt();
                sproc.Commit();
            }
            catch (SqlException se)
            {
                sproc.RollBack();
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return identity;
        }

        public int UpdateForHDQT(int RepresentativeId, double HDQT_A, double HDQT_B, double HDQT_C, double HDQT_D,
            double HDQT_E, double HDQT_F, double HDQT_G, bool HDQT_IsValid)
        {
            Debug.Assert(sproc == null);
            var identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@RepresentativeId", SqlDbType.Int, 4),
                    new SqlParameter("@HDQT_A", SqlDbType.Float),
                    new SqlParameter("@HDQT_B", SqlDbType.Float),
                    new SqlParameter("@HDQT_C", SqlDbType.Float),
                    new SqlParameter("@HDQT_D", SqlDbType.Float),
                    new SqlParameter("@HDQT_E", SqlDbType.Float),
                    new SqlParameter("@HDQT_F", SqlDbType.Float),
                    new SqlParameter("@HDQT_G", SqlDbType.Float),
                    new SqlParameter("@HDQT_IsValid", SqlDbType.Bit)
                };

                param[0].Value = RepresentativeId;
                param[1].Value = HDQT_A;
                param[2].Value = HDQT_B;
                param[3].Value = HDQT_C;
                param[4].Value = HDQT_D;
                param[5].Value = HDQT_E;
                param[6].Value = HDQT_F;
                param[7].Value = HDQT_G;
                param[8].Value = HDQT_IsValid;


                sproc = new StoreProcedure(RepresentativeKeys.Sp_Upd_H4_Representative_For_HDQT, param);
                identity = sproc.RunInt();
                sproc.Commit();
            }
            catch (SqlException se)
            {
                sproc.RollBack();
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return identity;
        }

        public int UpdateForBKS(int RepresentativeId, double BKS_A, double BKS_B, double BKS_C, int BKS_IsValid)
        {
            Debug.Assert(sproc == null);
            var identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@RepresentativeId", SqlDbType.Int, 4),
                    new SqlParameter("@BKS_A", SqlDbType.Float),
                    new SqlParameter("@BKS_B", SqlDbType.Float),
                    new SqlParameter("@BKS_C", SqlDbType.Float),
                    new SqlParameter("@BKS_IsValid", SqlDbType.Int, 4)
                };

                param[0].Value = RepresentativeId;
                param[1].Value = BKS_A;
                param[2].Value = BKS_B;
                param[3].Value = BKS_C;
                param[4].Value = BKS_IsValid;


                sproc = new StoreProcedure(RepresentativeKeys.Sp_Upd_H4_Representative_For_BKS, param);
                identity = sproc.RunInt();
                sproc.Commit();
            }
            catch (SqlException se)
            {
                sproc.RollBack();
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return identity;
        }

        public int UpdateForCheck(int InvestorNo, int IsOk)
        {
            Debug.Assert(sproc == null);
            var identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@InvestorNo", SqlDbType.Int, 4),
                    new SqlParameter("@IsOk", SqlDbType.Int, 4)
                };

                param[0].Value = InvestorNo;
                param[1].Value = IsOk;


                sproc = new StoreProcedure(RepresentativeKeys.Sp_Upd_H4_Representative_For_Check, param);
                identity = sproc.RunInt();
                sproc.Commit();
            }
            catch (SqlException se)
            {
                sproc.RollBack();
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return identity;
        }

        public int UpdateForAttorneyCode(int InvestorNo, int AttorneyCode)
        {
            Debug.Assert(sproc == null);
            var identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@InvestorNo", SqlDbType.Int, 4),
                    new SqlParameter("@AttorneyCode", SqlDbType.Int, 4)
                };

                param[0].Value = InvestorNo;
                param[1].Value = AttorneyCode;


                sproc = new StoreProcedure(RepresentativeKeys.Sp_Upd_H4_Representative_For_AttorneyCode, param);
                identity = sproc.RunInt();
                sproc.Commit();
            }
            catch (SqlException se)
            {
                sproc.RollBack();
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return identity;
        }

        //public long Delete(System.Nullable<int> DecisionId)
        //{

        //    Debug.Assert(sproc == null);
        //    long identity = 0;

        //    try
        //    {

        //        SqlParameter[] param = 
        //        {                                
        //            new SqlParameter("@DecisionId", System.Data.SqlDbType.Int, 4)
        //        };

        //        param[0].Value = DecisionId;
        //        sproc = new StoreProcedure(DecisionsKeys.Sp_Del_H3_Decisions, param);
        //        identity = sproc.RunInt();
        //        sproc.Commit();

        //    }
        //    catch (SqlException se)
        //    {
        //        sproc.RollBack();
        //        throw new HRMException(se.Message, se.Number);
        //    }
        //    finally
        //    {
        //        sproc.Dispose();
        //    }

        //    return identity;

        //}

        #endregion
    }
}