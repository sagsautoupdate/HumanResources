using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H1;

namespace HRMDAL.H1
{
    public class IncomeEmployeesDAL : Dao
    {
        #region  insert, update, delete

        public long Update(DateTime DataDate, int UserId, decimal LNS, decimal DGGC_LCB, decimal LCB, decimal PCCV,
            decimal PCTN,
            decimal TienAnGiuaCa, decimal BoSungLuong, decimal TienThemGio_BNgay, decimal TienThemGio_BNgayChiuThue,
            decimal TienThemGio_BDem,
            decimal TienThemGio_BDemChiuThue, decimal TienThemGio, decimal TienLamDem, decimal TotalIncome,
            decimal TotalIncomeForTax,
            decimal BHXH, decimal BHYT, decimal BHTN, decimal DoanPhiCD, decimal LuongChiuThue, decimal ThueThuNhap,
            decimal TruocThucLinh,
            decimal DongGop, decimal ThucLinh, DateTime UpdateDate, int UpdateUserId, int DataType, string Remark,
            int IsVCQLNC)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DataDate", SqlDbType.SmallDateTime),
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@LNS", SqlDbType.Money),
                    new SqlParameter("@DGGC_LCB", SqlDbType.Money),
                    new SqlParameter("@LCB", SqlDbType.Money),
                    new SqlParameter("@PCCV", SqlDbType.Money),
                    new SqlParameter("@PCTN money,", SqlDbType.Money),
                    new SqlParameter("@TienAnGiuaCa", SqlDbType.Money),
                    new SqlParameter("@BoSungLuong money,", SqlDbType.Money),
                    new SqlParameter("@TienThemGio_BNgay", SqlDbType.Money),
                    new SqlParameter("@TienThemGio_BNgayChiuThue money,", SqlDbType.Money),
                    new SqlParameter("@TienThemGio_BDem", SqlDbType.Money),
                    new SqlParameter("@TienThemGio_BDemChiuThue", SqlDbType.Money),
                    new SqlParameter("@TienThemGio", SqlDbType.Money),
                    new SqlParameter("@TienLamDem", SqlDbType.Money),
                    new SqlParameter("@TotalIncome", SqlDbType.Money),
                    new SqlParameter("@TotalIncomeForTax", SqlDbType.Money),
                    new SqlParameter("@BHXH", SqlDbType.Money),
                    new SqlParameter("@BHYT", SqlDbType.Money),
                    new SqlParameter("@BHTN", SqlDbType.Money),
                    new SqlParameter("@DoanPhiCD", SqlDbType.Money),
                    new SqlParameter("@LuongChiuThue", SqlDbType.Money),
                    new SqlParameter("@ThueThuNhap", SqlDbType.Money),
                    new SqlParameter("@TruocThucLinh", SqlDbType.Money),
                    new SqlParameter("@DongGop", SqlDbType.Money),
                    new SqlParameter("@ThucLinh", SqlDbType.Money),
                    new SqlParameter("@UpdateDate", SqlDbType.SmallDateTime),
                    new SqlParameter("@UpdateUserId", SqlDbType.Int),
                    new SqlParameter("@DataType", SqlDbType.Int),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@IsVCQLNC", SqlDbType.Int)
                };


                param[0].Value = DataDate;
                param[1].Value = UserId;

                param[2].Value = LNS;
                param[3].Value = DGGC_LCB;
                param[4].Value = LCB;
                param[5].Value = PCCV;
                param[6].Value = PCTN;

                param[7].Value = TienAnGiuaCa;
                param[8].Value = BoSungLuong;

                param[9].Value = TienThemGio_BNgay;
                param[10].Value = TienThemGio_BNgayChiuThue;
                param[11].Value = TienThemGio_BDem;
                param[12].Value = TienThemGio_BDemChiuThue;
                param[13].Value = TienThemGio;
                param[14].Value = TienLamDem;

                param[15].Value = TotalIncome;
                param[16].Value = TotalIncomeForTax;

                param[17].Value = BHXH;
                param[18].Value = BHYT;
                param[19].Value = BHTN;
                param[20].Value = DoanPhiCD;

                param[21].Value = LuongChiuThue;
                param[22].Value = ThueThuNhap;
                param[23].Value = TruocThucLinh;
                param[24].Value = DongGop;
                param[25].Value = ThucLinh;

                param[26].Value = UpdateDate;
                param[27].Value = UpdateUserId;

                param[28].Value = DataType;
                param[29].Value = Remark;
                param[30].Value = IsVCQLNC;

                sproc = new StoreProcedure(IncomeEmployeesKeys.Sp_Upd_H1_IncomeEmployees, param);
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


        /// <summary>
        /// </summary>
        /// <param name=""></param>
        /// <returns>int</returns>
        public long Insert(DateTime DataDate, int UserId, decimal LNS, decimal DGGC_LCB, decimal LCB, decimal PCCV,
            decimal PCTN,
            decimal TienAnGiuaCa, decimal BoSungLuong, decimal TienThemGio_BNgay, decimal TienThemGio_BNgayChiuThue,
            decimal TienThemGio_BDem,
            decimal TienThemGio_BDemChiuThue, decimal TienThemGio, decimal TienLamDem, decimal TotalIncome,
            decimal TotalIncomeForTax,
            decimal BHXH, decimal BHYT, decimal BHTN, decimal DoanPhiCD, decimal LuongChiuThue, decimal ThueThuNhap,
            decimal TruocThucLinh,
            decimal DongGop, decimal ThucLinh, DateTime CreateDate, int CreateUserId, int DataType, string Remark,
            int IsVCQLNC)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DataDate", SqlDbType.SmallDateTime),
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@LNS", SqlDbType.Money),
                    new SqlParameter("@DGGC_LCB", SqlDbType.Money),
                    new SqlParameter("@LCB", SqlDbType.Money),
                    new SqlParameter("@PCCV", SqlDbType.Money),
                    new SqlParameter("@PCTN", SqlDbType.Money),
                    new SqlParameter("@TienAnGiuaCa", SqlDbType.Money),
                    new SqlParameter("@BoSungLuong money,", SqlDbType.Money),
                    new SqlParameter("@TienThemGio_BNgay", SqlDbType.Money),
                    new SqlParameter("@TienThemGio_BNgayChiuThue money,", SqlDbType.Money),
                    new SqlParameter("@TienThemGio_BDem", SqlDbType.Money),
                    new SqlParameter("@TienThemGio_BDemChiuThue", SqlDbType.Money),
                    new SqlParameter("@TienThemGio", SqlDbType.Money),
                    new SqlParameter("@TienLamDem", SqlDbType.Money),
                    new SqlParameter("@TotalIncome", SqlDbType.Money),
                    new SqlParameter("@TotalIncomeForTax", SqlDbType.Money),
                    new SqlParameter("@BHXH", SqlDbType.Money),
                    new SqlParameter("@BHYT", SqlDbType.Money),
                    new SqlParameter("@BHTN", SqlDbType.Money),
                    new SqlParameter("@DoanPhiCD", SqlDbType.Money),
                    new SqlParameter("@LuongChiuThue", SqlDbType.Money),
                    new SqlParameter("@ThueThuNhap", SqlDbType.Money),
                    new SqlParameter("@TruocThucLinh", SqlDbType.Money),
                    new SqlParameter("@DongGop", SqlDbType.Money),
                    new SqlParameter("@ThucLinh", SqlDbType.Money),
                    new SqlParameter("@CreateDate", SqlDbType.SmallDateTime),
                    new SqlParameter("@CreateUserId", SqlDbType.Int),
                    new SqlParameter("@DataType", SqlDbType.Int),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@IsVCQLNC", SqlDbType.Int)
                };


                param[0].Value = DataDate;
                param[1].Value = UserId;

                param[2].Value = LNS;
                param[3].Value = DGGC_LCB;
                param[4].Value = LCB;
                param[5].Value = PCCV;
                param[6].Value = PCTN;

                param[7].Value = TienAnGiuaCa;
                param[8].Value = BoSungLuong;

                param[9].Value = TienThemGio_BNgay;
                param[10].Value = TienThemGio_BNgayChiuThue;
                param[11].Value = TienThemGio_BDem;
                param[12].Value = TienThemGio_BDemChiuThue;
                param[13].Value = TienThemGio;
                param[14].Value = TienLamDem;

                param[15].Value = TotalIncome;
                param[16].Value = TotalIncomeForTax;

                param[17].Value = BHXH;
                param[18].Value = BHYT;
                param[19].Value = BHTN;
                param[20].Value = DoanPhiCD;

                param[21].Value = LuongChiuThue;
                param[22].Value = ThueThuNhap;
                param[23].Value = TruocThucLinh;
                param[24].Value = DongGop;
                param[25].Value = ThucLinh;

                param[26].Value = CreateDate;
                param[27].Value = CreateUserId;
                param[28].Value = DataType;
                param[29].Value = Remark;
                param[30].Value = IsVCQLNC;

                sproc = new StoreProcedure(IncomeEmployeesKeys.Sp_Ins_H1_IncomeEmployees, param);
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

        public long ImportFromExcelACV(DateTime DataDate, string ACVId,
            decimal LNS, decimal LCB, decimal PCCV, decimal PCTN, decimal TienAnGiuaCa, decimal BoSungLuong,
            decimal TienThemGio, decimal TienLamDem,
            decimal TotalIncome, decimal TotalIncomeForTax,
            decimal BHXH, decimal BHYT, decimal BHTN, decimal DoanPhiCD, decimal ThueThuNhap,
            decimal TruocThucLinh, decimal DongGop, decimal Luong, decimal TNTHieuQuaCongViec, decimal ThucLinh,
            DateTime CreateDate, int CreateUserId, DateTime UpdateDate, int UpdateUserId,
            bool Lock, int DataType, string Remark, int UserId)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DataDate", SqlDbType.DateTime),
                    new SqlParameter("@ACVId", SqlDbType.VarChar, 50),
                    new SqlParameter("@LNS", SqlDbType.Money),
                    new SqlParameter("@LCB", SqlDbType.Money),
                    new SqlParameter("@PCCV", SqlDbType.Money),
                    new SqlParameter("@PCTN", SqlDbType.Money),
                    new SqlParameter("@TienAnGiuaCa", SqlDbType.Money),
                    new SqlParameter("@BoSungLuong", SqlDbType.Money),
                    new SqlParameter("@TienThemGio", SqlDbType.Money),
                    new SqlParameter("@TienLamDem", SqlDbType.Money),
                    new SqlParameter("@TotalIncome", SqlDbType.Money),
                    new SqlParameter("@TotalIncomeForTax", SqlDbType.Money),
                    new SqlParameter("@BHXH", SqlDbType.Money),
                    new SqlParameter("@BHYT", SqlDbType.Money),
                    new SqlParameter("@BHTN", SqlDbType.Money),
                    new SqlParameter("@DoanPhiCD", SqlDbType.Money),
                    new SqlParameter("@ThueThuNhap", SqlDbType.Money),
                    new SqlParameter("@TruocThucLinh", SqlDbType.Money),
                    new SqlParameter("@DongGop", SqlDbType.Money),
                    new SqlParameter("@Luong", SqlDbType.Money),
                    new SqlParameter("@TNTHieuQuaCongViec", SqlDbType.Money),
                    new SqlParameter("@ThucLinh", SqlDbType.Money),
                    new SqlParameter("@CreateDate", SqlDbType.DateTime, 8),
                    new SqlParameter("@CreateUserId", SqlDbType.Int),
                    new SqlParameter("@UpdateDate", SqlDbType.DateTime, 8),
                    new SqlParameter("@UpdateUserId", SqlDbType.Int),
                    new SqlParameter("@Lock", SqlDbType.Bit),
                    new SqlParameter("@DataType", SqlDbType.Int),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                param[0].Value = DataDate;
                param[1].Value = ACVId;

                param[2].Value = LNS;
                param[3].Value = LCB;
                param[4].Value = PCCV;
                param[5].Value = PCTN;
                param[6].Value = TienAnGiuaCa;
                param[7].Value = BoSungLuong;
                param[8].Value = TienThemGio;
                param[9].Value = TienLamDem;

                param[10].Value = TotalIncome;
                param[11].Value = TotalIncomeForTax;

                param[12].Value = BHXH;
                param[13].Value = BHYT;
                param[14].Value = BHTN;
                param[15].Value = DoanPhiCD;
                param[16].Value = ThueThuNhap;

                param[17].Value = TruocThucLinh;
                param[18].Value = DongGop;
                param[19].Value = Luong;
                param[20].Value = TNTHieuQuaCongViec;
                param[21].Value = ThucLinh;

                param[22].Value = CreateDate;
                param[23].Value = CreateUserId;
                param[24].Value = UpdateDate;
                param[25].Value = UpdateUserId;

                param[26].Value = Lock;
                param[27].Value = DataType;
                param[28].Value = Remark;
                param[29].Value = UserId;

                sproc = new StoreProcedure(IncomeEmployeesKeys.Sp_Imp_H1_IncomeEmployees, param);
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

        //public int DeleteByRootDate(int rootId, int month, int year)
        //{
        //    int identity = 0;
        //    Debug.Assert(sproc == null);
        //    try
        //    {
        //        SqlParameter[] param = 
        //        {
        //            new SqlParameter("@RootId",SqlDbType.Int),
        //            new SqlParameter("@Month",SqlDbType.Int),
        //            new SqlParameter("@Year",SqlDbType.Int)
        //        };

        //        param[0].Value = rootId;
        //        param[1].Value = month;
        //        param[2].Value = year;

        //        sproc = new StoreProcedure(IncomeKeys.Sp_Del_H1_Incomes_By_Root_Date, param);

        //        sproc.Run();
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

        #region method get

        public DataTable GetByFilter(DateTime DataDate, int DataType, int isVCQLNN, string DepartmentIds)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            ;
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DataDate", SqlDbType.DateTime),
                    new SqlParameter("@DataType", SqlDbType.Int, 4),
                    new SqlParameter("@IsVCQLNC", SqlDbType.Int, 4),
                    new SqlParameter("@DepartmentIds", SqlDbType.VarChar, 254)
                };
                param[0].Value = DataDate;
                param[1].Value = DataType;
                param[2].Value = isVCQLNN;
                param[3].Value = DepartmentIds;

                sproc = new StoreProcedure("Sel_H1_IncomeEmployeesByFilter", param);
                sproc.RunFill(dt);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        public DataTable GetByDataDate(DateTime DataDate, int isVCQLNN, string DepartmentIds)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            ;
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DataDate", SqlDbType.DateTime),
                    new SqlParameter("@IsVCQLNC", SqlDbType.Int, 4),
                    new SqlParameter("@DepartmentIds", SqlDbType.VarChar, 254)
                };
                param[0].Value = DataDate;
                param[1].Value = isVCQLNN;
                param[2].Value = DepartmentIds;

                sproc = new StoreProcedure(IncomeEmployeesKeys.Sp_Sel_H1_IncomeEmployeesByDataDate, param);
                sproc.RunFill(dt);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return dt;
        }

        /// <summary>
        ///     Author: Giang
        ///     Date: 20-Oct-14
        ///     Content: Lay thong tin luong cua 1 nhan vien tra ve DT
        /// </summary>
        /// <param name="DataDate"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public DataTable GetByUserId(DateTime DataDate, int UserId)
        {
            Debug.Assert(sproc == null);

            var dt = new DataTable();
            ;
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DataDate", SqlDbType.DateTime),
                    //new SqlParameter("@IsVCQLNC",SqlDbType.Int, 4),
                    new SqlParameter("@UserId", SqlDbType.VarChar, 254)
                };
                param[0].Value = DataDate;
                //param[1].Value = isVCQLNN;
                param[1].Value = UserId;

                sproc = new StoreProcedure(IncomeEmployeesKeys.Sp_Sel_H1_IncomeEmployeesByUserId, param);
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