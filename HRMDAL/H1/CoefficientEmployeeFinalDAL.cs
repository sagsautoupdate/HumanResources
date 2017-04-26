using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H1;

namespace HRMDAL.H1
{
    public class CoefficientEmployeeFinalDAL : Dao
    {
        #region Insert, Update, Delete

        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns>int</returns>
        public long Insert(int? UserId, DateTime? CreateDate, double? LNS, double? LNSPCTN, double? LCB, double? PCDH,
            double? PCTN, double? PCKV, double? PCCV)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@CreateDate", SqlDbType.SmallDateTime, 4),
                    new SqlParameter("@LNS", SqlDbType.Float, 8),
                    new SqlParameter("@LNSPCTN", SqlDbType.Float, 8),
                    new SqlParameter("@LCB", SqlDbType.Float, 8),
                    new SqlParameter("@PCDH", SqlDbType.Float, 8),
                    new SqlParameter("@PCTN", SqlDbType.Float, 8),
                    new SqlParameter("@PCKV", SqlDbType.Float, 8),
                    new SqlParameter("@PCCV", SqlDbType.Float, 8)
                };
                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (CreateDate.HasValue)
                    param[1].Value = CreateDate.Value;
                else
                    param[1].Value = DBNull.Value;
                if (LNS.HasValue)
                    param[2].Value = LNS.Value;
                else
                    param[2].Value = DBNull.Value;
                if (LNSPCTN.HasValue)
                    param[3].Value = LNSPCTN.Value;
                else
                    param[3].Value = DBNull.Value;
                if (LCB.HasValue)
                    param[4].Value = LCB.Value;
                else
                    param[4].Value = DBNull.Value;
                if (PCDH.HasValue)
                    param[5].Value = PCDH.Value;
                else
                    param[5].Value = DBNull.Value;
                if (PCTN.HasValue)
                    param[6].Value = PCTN.Value;
                else
                    param[6].Value = DBNull.Value;
                if (PCKV.HasValue)
                    param[7].Value = PCKV.Value;
                else
                    param[7].Value = DBNull.Value;
                if (PCCV.HasValue)
                    param[8].Value = PCCV.Value;
                else
                    param[8].Value = DBNull.Value;

                sproc = new StoreProcedure(CoefficientEmployeeFinalKeys.Sp_Ins_H1_CoefficientEmployeeFinal, param);
                identity = sproc.Run();
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


        public long ImportFromExcelACV(string ACVId, DateTime DataDate, double LNS, double LNSPCTN, double LCB,
            double PCDH, double PCTN, double PCKV, double PCCV, double K, double DTNopThue, double NguoiPThuoc,
            string Remark, DateTime CreateDate, DateTime UpdateDate, int CreateUserId, int UpdateUserId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@ACVId", SqlDbType.VarChar, 50),
                    new SqlParameter("@DataDate", SqlDbType.SmallDateTime, 4),
                    new SqlParameter("@LNS", SqlDbType.Float, 8),
                    new SqlParameter("@LNSPCTN", SqlDbType.Float, 8),
                    new SqlParameter("@LCB", SqlDbType.Float, 8),
                    new SqlParameter("@PCDH", SqlDbType.Float, 8),
                    new SqlParameter("@PCTN", SqlDbType.Float, 8),
                    new SqlParameter("@PCKV", SqlDbType.Float, 8),
                    new SqlParameter("@PCCV", SqlDbType.Float, 8),
                    new SqlParameter("@K", SqlDbType.Float, 8),
                    new SqlParameter("@DTNopThue", SqlDbType.Float, 8),
                    new SqlParameter("@NguoiPThuoc", SqlDbType.Float, 8),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@CreateDate", SqlDbType.SmallDateTime, 4),
                    new SqlParameter("@UpdateDate", SqlDbType.SmallDateTime, 4),
                    new SqlParameter("@CreateUserId", SqlDbType.Int, 4),
                    new SqlParameter("@UpdateUserId", SqlDbType.Int, 4)
                };
                param[0].Value = ACVId;
                param[1].Value = DataDate;
                param[2].Value = LNS;
                param[3].Value = LNSPCTN;
                param[4].Value = LCB;
                param[5].Value = PCDH;
                param[6].Value = PCTN;
                param[7].Value = PCKV;
                param[8].Value = PCCV;
                param[9].Value = K;
                param[10].Value = DTNopThue;
                param[11].Value = NguoiPThuoc;
                param[12].Value = Remark;
                param[13].Value = CreateDate;
                param[14].Value = UpdateDate;
                param[15].Value = CreateUserId;
                param[16].Value = UpdateUserId;


                sproc = new StoreProcedure(CoefficientEmployeeFinalKeys.Sp_Imp_H1_CoefficientEmployeeFinal, param);
                identity = sproc.Run();
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


        public long UpdateForLNS(int? UserId, DateTime? CreateDate, double? LNS, double? LNSPCTN)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@CreateDate", SqlDbType.SmallDateTime, 4),
                    new SqlParameter("@LNS", SqlDbType.Float, 8),
                    new SqlParameter("@LNSPCTN", SqlDbType.Float, 8)
                };
                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (CreateDate.HasValue)
                    param[1].Value = CreateDate.Value;
                else
                    param[1].Value = DBNull.Value;
                if (LNS.HasValue)
                    param[2].Value = LNS.Value;
                else
                    param[2].Value = DBNull.Value;
                if (LNSPCTN.HasValue)
                    param[3].Value = LNSPCTN.Value;
                else
                    param[3].Value = DBNull.Value;


                sproc = new StoreProcedure(CoefficientEmployeeFinalKeys.Sp_Upd_H1_CoefficientEmployeeFinal_ForLNS, param);
                identity = sproc.Run();
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

        public long UpdateForLCB(int? UserId, DateTime? CreateDate, double? LCB)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@CreateDate", SqlDbType.SmallDateTime, 4),
                    new SqlParameter("@LCB", SqlDbType.Float, 8)
                };
                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (CreateDate.HasValue)
                    param[1].Value = CreateDate.Value;
                else
                    param[1].Value = DBNull.Value;
                if (LCB.HasValue)
                    param[2].Value = LCB.Value;
                else
                    param[2].Value = DBNull.Value;


                sproc = new StoreProcedure(CoefficientEmployeeFinalKeys.Sp_Upd_H1_CoefficientEmployeeFinal_ForLCB, param);
                identity = sproc.Run();
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

        public long UpdateForOther(int? UserId, DateTime? CreateDate, double? PCDH, double? PCTN, double? PCKV,
            double? PCCV)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@CreateDate", SqlDbType.SmallDateTime, 4),
                    new SqlParameter("@PCDH", SqlDbType.Float, 8),
                    new SqlParameter("@PCTN", SqlDbType.Float, 8),
                    new SqlParameter("@PCKV", SqlDbType.Float, 8),
                    new SqlParameter("@PCCV", SqlDbType.Float, 8)
                };
                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (CreateDate.HasValue)
                    param[1].Value = CreateDate.Value;
                else
                    param[1].Value = DBNull.Value;
                if (PCDH.HasValue)
                    param[2].Value = PCDH.Value;
                else
                    param[2].Value = DBNull.Value;
                if (PCTN.HasValue)
                    param[3].Value = PCTN.Value;
                else
                    param[3].Value = DBNull.Value;
                if (PCKV.HasValue)
                    param[4].Value = PCKV.Value;
                else
                    param[4].Value = DBNull.Value;
                if (PCCV.HasValue)
                    param[5].Value = PCCV.Value;
                else
                    param[5].Value = DBNull.Value;

                sproc = new StoreProcedure(CoefficientEmployeeFinalKeys.Sp_Upd_H1_CoefficientEmployeeFinal_ForOther,
                    param);
                identity = sproc.Run();
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

        public long UpdateForSpecial(int? UserId, DateTime? CreateDate, double? LCB, double? LNS, double? LNSPCTN,
            double? PCDH, double? PCTN, double? PCKV, double? PCCV)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int, 4),
                    new SqlParameter("@CreateDate", SqlDbType.SmallDateTime, 4),
                    new SqlParameter("@LCB", SqlDbType.Float, 8),
                    new SqlParameter("@LNS", SqlDbType.Float, 8),
                    new SqlParameter("@LNSPCTN", SqlDbType.Float, 8),
                    new SqlParameter("@PCDH", SqlDbType.Float, 8),
                    new SqlParameter("@PCTN", SqlDbType.Float, 8),
                    new SqlParameter("@PCKV", SqlDbType.Float, 8),
                    new SqlParameter("@PCCV", SqlDbType.Float, 8)
                };
                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (CreateDate.HasValue)
                    param[1].Value = CreateDate.Value;
                else
                    param[1].Value = DBNull.Value;
                if (LCB.HasValue)
                    param[2].Value = LCB.Value;
                else
                    param[2].Value = DBNull.Value;
                if (LNS.HasValue)
                    param[3].Value = LNS.Value;
                else
                    param[3].Value = DBNull.Value;
                if (LNSPCTN.HasValue)
                    param[4].Value = LNSPCTN.Value;
                else
                    param[4].Value = DBNull.Value;
                if (PCDH.HasValue)
                    param[5].Value = PCDH.Value;
                else
                    param[5].Value = DBNull.Value;
                if (PCTN.HasValue)
                    param[6].Value = PCTN.Value;
                else
                    param[6].Value = DBNull.Value;
                if (PCKV.HasValue)
                    param[7].Value = PCKV.Value;
                else
                    param[7].Value = DBNull.Value;
                if (PCCV.HasValue)
                    param[8].Value = PCCV.Value;
                else
                    param[8].Value = DBNull.Value;

                sproc = new StoreProcedure(CoefficientEmployeeFinalKeys.Sp_Upd_H1_CoefficientEmployeeFinal_For_Special,
                    param);
                identity = sproc.Run();
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

        #endregion

        #region GET

        public DataTable GetByFilter(string FullName, int RootId, int Month, int Year)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            ;
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@RootId", SqlDbType.Int),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int)
                };
                param[0].Value = FullName;
                param[1].Value = RootId;
                param[2].Value = Month;
                param[3].Value = Year;

                sproc = new StoreProcedure(CoefficientEmployeeFinalKeys.Sp_Sel_H1_CoefficientEmployeeFinal_By_Filter,
                    param);
                sproc.RunFill(dt);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetByRootDateForTotal(int RootId, int Month, int Year)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            ;
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@RootId", SqlDbType.Int),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int)
                };
                param[0].Value = RootId;
                param[1].Value = Month;
                param[2].Value = Year;

                sproc =
                    new StoreProcedure(
                        CoefficientEmployeeFinalKeys.Sp_Sel_H1_CoefficientEmployeeFinal_By_RootDate_ForTotal, param);
                sproc.RunFill(dt);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetByUserDate(int UserId, int Month, int Year)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            ;
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int)
                };
                param[0].Value = UserId;
                param[1].Value = Month;
                param[2].Value = Year;

                sproc = new StoreProcedure(CoefficientEmployeeFinalKeys.Sp_Sel_H1_CoefficientEmployeeFinal_By_UserDate,
                    param);
                sproc.RunFill(dt);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }


        public DataTable GetCoefficientWorkdayByDataDate(DateTime dataDate)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            ;
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DataDate", SqlDbType.DateTime)
                };
                param[0].Value = dataDate;

                sproc =
                    new StoreProcedure(CoefficientEmployeeFinalKeys.Sp_Sel_H1_CoefficientWorkdayEmployees_By_DataDate,
                        param);
                sproc.RunFill(dt);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        #endregion
    }
}