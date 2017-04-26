using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H3;

namespace HRMDAL.H3
{
    public class DecisionsDAL : Dao
    {
        #region Methods Get

        public DataTable GetByFilter(int? DecisionTypeId, string DecisionNo, string DecisionName, DateTime? DecisionDate)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DecisionTypeId", SqlDbType.Int, 4),
                    new SqlParameter("@DecisionNo", SqlDbType.VarChar, 50),
                    new SqlParameter("@DecisionName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@DecisionDate", SqlDbType.SmallDateTime, 4)
                };

                if (DecisionTypeId.HasValue)
                    param[0].Value = DecisionTypeId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (DecisionNo == null)
                    param[1].Value = DBNull.Value;
                else
                    param[1].Value = DecisionNo;
                if (DecisionName == null)
                    param[2].Value = DBNull.Value;
                else
                    param[2].Value = DecisionName;
                if (DecisionDate.HasValue)
                    param[3].Value = DecisionDate.Value;
                else
                    param[3].Value = DBNull.Value;

                sproc = new StoreProcedure(DecisionsKeys.Sp_Sel_H3_Decisions_By_Filter, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetById(int? DecisionId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DecisionId", SqlDbType.Int, 4)
                };

                if (DecisionId.HasValue)
                    param[0].Value = DecisionId.Value;
                else
                    param[0].Value = DBNull.Value;


                sproc = new StoreProcedure(DecisionsKeys.Sp_Sel_H3_Decision_By_Id, param);
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

        public int Insert(int? DecisionTypeId, string DecisionNo, DateTime? DecisionDate, string DecisionName,
            string BringOutDept, string SignUser, string Remark, DateTime EffectiveDate, DateTime IneffectiveDate)
        {
            Debug.Assert(sproc == null);
            var identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DecisionTypeId", SqlDbType.Int, 4),
                    new SqlParameter("@DecisionNo", SqlDbType.VarChar, 50),
                    new SqlParameter("@DecisionDate", SqlDbType.SmallDateTime, 4),
                    new SqlParameter("@DecisionName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@BringOutDept", SqlDbType.NVarChar, 254),
                    new SqlParameter("@SignUser", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@EffectiveDate", SqlDbType.DateTime),
                    new SqlParameter("@IneffectiveDate", SqlDbType.DateTime)
                };

                if (DecisionTypeId.HasValue)
                    param[0].Value = DecisionTypeId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (DecisionNo == null)
                    param[1].Value = DBNull.Value;
                else
                    param[1].Value = DecisionNo;
                if (DecisionDate.HasValue)
                    param[2].Value = DecisionDate.Value;
                else
                    param[2].Value = DBNull.Value;
                if (DecisionName == null)
                    param[3].Value = DBNull.Value;
                else
                    param[3].Value = DecisionName;
                if (BringOutDept == null)
                    param[4].Value = DBNull.Value;
                else
                    param[4].Value = BringOutDept;
                if (SignUser == null)
                    param[5].Value = DBNull.Value;
                else
                    param[5].Value = SignUser;
                if (Remark == null)
                    param[6].Value = DBNull.Value;
                else
                    param[6].Value = Remark;
                if (EffectiveDate == null)
                    param[7].Value = DBNull.Value;
                else
                    param[7].Value = EffectiveDate;
                if (IneffectiveDate == null)
                    param[8].Value = DBNull.Value;
                else
                    param[8].Value = IneffectiveDate;


                sproc = new StoreProcedure(DecisionsKeys.Sp_Ins_H3_Decisions, param);
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

        public int Update(int? DecisionTypeId, string DecisionNo, DateTime? DecisionDate, string DecisionName,
            string BringOutDept, string SignUser, string Remark, int? DecisionId)
        {
            Debug.Assert(sproc == null);
            var identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DecisionTypeId", SqlDbType.Int, 4),
                    new SqlParameter("@DecisionNo", SqlDbType.VarChar, 50),
                    new SqlParameter("@DecisionDate", SqlDbType.SmallDateTime, 4),
                    new SqlParameter("@DecisionName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@BringOutDept", SqlDbType.NVarChar, 254),
                    new SqlParameter("@SignUser", SqlDbType.NVarChar, 254),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@DecisionId", SqlDbType.Int, 4)
                };

                if (DecisionTypeId.HasValue)
                    param[0].Value = DecisionTypeId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (DecisionNo == null)
                    param[1].Value = DBNull.Value;
                else
                    param[1].Value = DecisionNo;
                if (DecisionDate.HasValue)
                    param[2].Value = DecisionDate.Value;
                else
                    param[2].Value = DBNull.Value;
                if (DecisionName == null)
                    param[3].Value = DBNull.Value;
                else
                    param[3].Value = DecisionName;
                if (BringOutDept == null)
                    param[4].Value = DBNull.Value;
                else
                    param[4].Value = BringOutDept;
                if (SignUser == null)
                    param[5].Value = DBNull.Value;
                else
                    param[5].Value = SignUser;
                if (Remark == null)
                    param[6].Value = DBNull.Value;
                else
                    param[6].Value = Remark;
                if (DecisionId.HasValue)
                    param[7].Value = DecisionId.Value;
                else
                    param[7].Value = DBNull.Value;

                sproc = new StoreProcedure(DecisionsKeys.Sp_Upd_H3_Decisions, param);
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

        public long Delete(int? DecisionId)
        {
            Debug.Assert(sproc == null);
            long identity = 0;

            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DecisionId", SqlDbType.Int, 4)
                };

                param[0].Value = DecisionId;
                sproc = new StoreProcedure(DecisionsKeys.Sp_Del_H3_Decisions, param);
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

        #endregion
    }
}