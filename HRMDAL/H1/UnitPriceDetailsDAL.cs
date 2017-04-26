using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H1;

namespace HRMDAL.H1
{
    public class UnitPriceDetailsDAL : Dao
    {
        #region insert, update, delete

        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns>int</returns>
        public int Insert(double UnitPriceLNS, double UnitPriceBonus, double UnitPriceLCB, double UnitPriceLuch,
            int UnitPriceDeptId, DateTime CreateDate, string Remark, int UnitPriceType)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UnitPriceLNS", SqlDbType.Money),
                    new SqlParameter("@UnitPriceBonus", SqlDbType.Money),
                    new SqlParameter("@UnitPriceLCB", SqlDbType.Money),
                    new SqlParameter("@UnitPriceLuch", SqlDbType.Money),
                    new SqlParameter("@UnitPriceDeptId", SqlDbType.Int),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@UnitPriceType", SqlDbType.Int)
                };
                param[0].Value = UnitPriceLNS;
                param[1].Value = UnitPriceBonus;
                param[2].Value = UnitPriceLCB;
                param[3].Value = UnitPriceLuch;
                param[4].Value = UnitPriceDeptId;
                param[5].Value = CreateDate;
                param[6].Value = Remark;
                param[7].Value = UnitPriceType;

                sproc = new StoreProcedure(UnitPriceDetailKeys.Sp_Ins_H1_UnitPriceDetail, param);
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

        public int Update(double UnitPriceLNS, double UnitPriceBonus, double UnitPriceLCB, double UnitPriceLuch,
            int UnitPriceDeptId, DateTime CreateDate, string Remark, int UnitPriceType, int UnitPriceDetailId)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UnitPriceLNS", SqlDbType.Money),
                    new SqlParameter("@UnitPriceBonus", SqlDbType.Money),
                    new SqlParameter("@UnitPriceLCB", SqlDbType.Money),
                    new SqlParameter("@UnitPriceLuch", SqlDbType.Money),
                    new SqlParameter("@UnitPriceDeptId", SqlDbType.Int),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@UnitPriceType", SqlDbType.Int),
                    new SqlParameter("@UnitPriceDetailId", SqlDbType.Int)
                };
                param[0].Value = UnitPriceLNS;
                param[1].Value = UnitPriceBonus;
                param[2].Value = UnitPriceLCB;
                param[3].Value = UnitPriceLuch;
                param[4].Value = UnitPriceDeptId;
                param[5].Value = CreateDate;
                param[6].Value = Remark == null ? "" : Remark;
                param[7].Value = UnitPriceType;
                param[8].Value = UnitPriceDetailId;

                sproc = new StoreProcedure(UnitPriceDetailKeys.Sp_Upd_H1_UnitPriceDetail, param);
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

        public int Delete(int UnitPriceDetailId)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UnitPriceDetailId", SqlDbType.Int)
                };

                param[0].Value = UnitPriceDetailId;

                sproc = new StoreProcedure(UnitPriceDetailKeys.Sp_Del_H1_UnitPriceDetail, param);

                sproc.RunInt();

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

        #region Get       

        public DataTable GetByFilter(int UnitPriceType, int Month, int Year, int UnitPriceDeptId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UnitPriceType", SqlDbType.Int),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int),
                    new SqlParameter("@UnitPriceDeptId", SqlDbType.Int)
                };

                param[0].Value = UnitPriceType;
                param[1].Value = Month;
                param[2].Value = Year;
                param[3].Value = UnitPriceDeptId;

                sproc = new StoreProcedure(UnitPriceDetailKeys.Sp_Sel_H1_UnitPriceDetail_By_Filter, param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return datatable;
        }

        public DataTable GetByDate(int Month, int Year)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int)
                };

                param[0].Value = Month;
                param[1].Value = Year;

                sproc = new StoreProcedure(UnitPriceDetailKeys.Sp_Sel_H1_UnitPriceDetail_By_Date, param);
                sproc.RunFill(datatable);
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }
            finally
            {
                sproc.Dispose();
            }

            return datatable;
        }

        #endregion
    }
}