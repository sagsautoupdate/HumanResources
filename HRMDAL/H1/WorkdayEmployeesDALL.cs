using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H1;

namespace HRMDAL.H1
{
    public class WorkdayEmployeesDALL : Dao
    {
        #region insert, update, delete

        public long InsertByDate_DeptId(string Day25L, string Day26L, string Day27L, string Day28L, string Day29L,
            string Day30L, string Day31L, string DepartmentIds, DateTime? WorkdayDateL, int? CreateUserL, double? XQDL,
            double? XL, int? TypeL, int? StatusL)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Day25L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day26L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day27L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day28L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day29L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day30L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day31L", SqlDbType.VarChar, 20),
                    new SqlParameter("@DeptIds", SqlDbType.VarChar, 1000),
                    new SqlParameter("@WorkdayDateL", SqlDbType.DateTime, 8),
                    new SqlParameter("@CreateUserL", SqlDbType.Int, 4),
                    new SqlParameter("@XQDL", SqlDbType.Float),
                    new SqlParameter("@XL", SqlDbType.Float),
                    new SqlParameter("@TypeL", SqlDbType.Int),
                    new SqlParameter("@StatusL", SqlDbType.Int)
                };

                if ((Day25L == null) || (Day25L == ""))
                    param[0].Value = DBNull.Value;
                else
                    param[0].Value = Day25L;
                if ((Day26L == null) || (Day26L == ""))
                    param[1].Value = DBNull.Value;
                else
                    param[1].Value = Day26L;
                if ((Day27L == null) || (Day27L == ""))
                    param[2].Value = DBNull.Value;
                else
                    param[2].Value = Day27L;
                if ((Day28L == null) || (Day28L == ""))
                    param[3].Value = DBNull.Value;
                else
                    param[3].Value = Day28L;
                if ((Day29L == null) || (Day29L == ""))
                    param[4].Value = DBNull.Value;
                else
                    param[4].Value = Day29L;
                if ((Day30L == null) || (Day30L == ""))
                    param[5].Value = DBNull.Value;
                else
                    param[5].Value = Day30L;
                if ((Day31L == null) || (Day31L == ""))
                    param[6].Value = DBNull.Value;
                else
                    param[6].Value = Day31L;

                if (DepartmentIds == null)
                    param[7].Value = DBNull.Value;
                else
                    param[7].Value = DepartmentIds;
                if (WorkdayDateL.HasValue)
                    param[8].Value = WorkdayDateL.Value;
                else
                    param[8].Value = DBNull.Value;
                if (CreateUserL.HasValue)
                    param[9].Value = CreateUserL.Value;
                else
                    param[9].Value = DBNull.Value;
                if (XQDL.HasValue)
                    if (XQDL == Constants.WorkdayEmployee_DefaultValue)
                        param[10].Value = DBNull.Value;
                    else
                        param[10].Value = XQDL.Value;
                else
                    param[10].Value = DBNull.Value;
                if (XL.HasValue)
                    if (XL == Constants.WorkdayEmployee_DefaultValue)
                        param[11].Value = DBNull.Value;
                    else
                        param[11].Value = XL.Value;
                else
                    param[11].Value = DBNull.Value;
                if (TypeL.HasValue)
                    if (TypeL == Constants.WorkdayEmployee_DefaultValue)
                        param[12].Value = DBNull.Value;
                    else
                        param[12].Value = (double) TypeL.Value;
                else
                    param[12].Value = DBNull.Value;
                if (StatusL.HasValue)
                    if (StatusL == Constants.WorkdayEmployee_DefaultValue)
                        param[13].Value = DBNull.Value;
                    else
                        param[13].Value = (double) StatusL.Value;
                else
                    param[13].Value = DBNull.Value;

                sproc = new StoreProcedure(WorkdayEmployeeLKeys.Sp_Ins_H1_WorkdayEmployeeL_By_Date_DeptIds, param);
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


        public long InsertByDate_UserId(int? UserId, DateTime? WorkdayDateL, string Day25L, string Day26L, string Day27L,
            string Day28L, string Day29L, string Day30L, string Day31L, int? CreateUser, double? XQDL, double? XL,
            int? TypeL, int? StatusL)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@WorkdayDateL", SqlDbType.DateTime, 8),
                    new SqlParameter("@Day25L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day26L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day27L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day28L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day29L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day30L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day31L", SqlDbType.VarChar, 20),
                    new SqlParameter("@CreateUserL", SqlDbType.Int, 4),
                    new SqlParameter("@XQDL", SqlDbType.Float),
                    new SqlParameter("@XL", SqlDbType.Float),
                    new SqlParameter("@TypeL", SqlDbType.Int),
                    new SqlParameter("@StatusL", SqlDbType.Int)
                };

                if (UserId.HasValue)
                    if (UserId == Constants.WorkdayEmployee_DefaultValue)
                        param[0].Value = DBNull.Value;
                    else
                        param[0].Value = (double) UserId.Value;
                else
                    param[0].Value = DBNull.Value;

                if (WorkdayDateL.HasValue)
                    param[1].Value = WorkdayDateL.Value;
                else
                    param[1].Value = DBNull.Value;

                if ((Day25L == null) || (Day25L == ""))
                    param[2].Value = DBNull.Value;
                else
                    param[2].Value = Day25L;
                if ((Day26L == null) || (Day26L == ""))
                    param[3].Value = DBNull.Value;
                else
                    param[3].Value = Day26L;
                if ((Day27L == null) || (Day27L == ""))
                    param[4].Value = DBNull.Value;
                else
                    param[4].Value = Day27L;
                if ((Day28L == null) || (Day28L == ""))
                    param[5].Value = DBNull.Value;
                else
                    param[5].Value = Day28L;
                if ((Day29L == null) || (Day29L == ""))
                    param[6].Value = DBNull.Value;
                else
                    param[6].Value = Day29L;
                if ((Day30L == null) || (Day30L == ""))
                    param[7].Value = DBNull.Value;
                else
                    param[7].Value = Day30L;
                if ((Day31L == null) || (Day31L == ""))
                    param[8].Value = DBNull.Value;
                else
                    param[8].Value = Day31L;
                if (CreateUser.HasValue)
                    if (CreateUser == Constants.WorkdayEmployee_DefaultValue)
                        param[9].Value = DBNull.Value;
                    else
                        param[9].Value = (double) CreateUser.Value;
                else
                    param[9].Value = DBNull.Value;

                if (XQDL.HasValue)
                    if (XQDL == Constants.WorkdayEmployee_DefaultValue)
                        param[10].Value = DBNull.Value;
                    else
                        param[10].Value = XQDL.Value;
                else
                    param[10].Value = DBNull.Value;
                if (XL.HasValue)
                    if (XL == Constants.WorkdayEmployee_DefaultValue)
                        param[11].Value = DBNull.Value;
                    else
                        param[11].Value = XL.Value;
                else
                    param[11].Value = DBNull.Value;
                if (TypeL.HasValue)
                    if (TypeL == Constants.WorkdayEmployee_DefaultValue)
                        param[12].Value = DBNull.Value;
                    else
                        param[12].Value = (double) TypeL.Value;
                else
                    param[12].Value = DBNull.Value;
                if (StatusL.HasValue)
                    if (StatusL == Constants.WorkdayEmployee_DefaultValue)
                        param[13].Value = DBNull.Value;
                    else
                        param[13].Value = (double) StatusL.Value;
                else
                    param[13].Value = DBNull.Value;

                sproc = new StoreProcedure(WorkdayEmployeeLKeys.Sp_Ins_H1_WorkdayEmployeesL, param);
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

        public long UpdateByDate_UserId(
            int? UserId,
            DateTime? WorkdayDateL,
            string Day1L, string Day2L, string Day3L, string Day4L, string Day5L, string Day6L, string Day7L,
            string Day8L, string Day9L,
            string Day10L, string Day11L, string Day12L, string Day13L, string Day14L, string Day15L, string Day16L,
            string Day17L, string Day18L, string Day19L,
            string Day20L, string Day21L, string Day22L, string Day23L, string Day24L, string Day25L, string Day26L,
            string Day27L, string Day28L, string Day29L, string Day30L, string Day31L,
            double? XL,
            double? F_OmL, double? F_OmDaiNgayL, double? F_ThaiSanL, double? F_TNLDL, double? F_NamL,
            double? F_dbL, double? F_KoLuongCLDL, double? F_KoLuongKLDL, double? F_DiDuongL, double? F_CongTacL,
            double? F_HocSAGS, double? F_Hoc1L, double? F_Hoc2L, double? F_Hoc3L, double? F_Hoc4L, double? F_Hoc5L,
            double? F_Hoc6L, double? F_Hoc7L,
            double? F_Con_OmL, double? F_KHHDSL, double? F_SayThaiL, double? F_KhamThaiL, double? F_ConChetL,
            double? F_DinhChiCongTacL,
            double? F_TamHoanHDL, double? F_HoiHopL, double? F_LeL, double? NghiTuanL, double? NghiBuL,
            double? NghiViecL, double? ChuaDiLamL,
            double? NightTimeL, double? MarkL, string RankL, DateTime? LastUpdateL, double? UpdateUserL, string Remark
        )
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@WorkdayDateL", SqlDbType.DateTime, 8),

                    #region day

                    new SqlParameter("@Day1L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day2L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day3L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day4L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day5L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day6L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day7L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day8L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day9L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day10L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day11L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day12L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day13L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day14L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day15L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day16L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day17L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day18L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day19L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day20L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day21L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day22L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day23L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day24L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day25L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day26L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day27L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day28L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day29L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day30L", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day31L", SqlDbType.VarChar, 20),

                    #endregion

                    new SqlParameter("@XL", SqlDbType.Float, 8),
                    new SqlParameter("@F_OmL", SqlDbType.Float, 8),
                    new SqlParameter("@F_OmDaiNgayL", SqlDbType.Float, 8),
                    new SqlParameter("@F_ThaiSanL", SqlDbType.Float, 8),
                    new SqlParameter("@F_TNLDL", SqlDbType.Float, 8),
                    new SqlParameter("@F_NamL", SqlDbType.Float, 8),
                    new SqlParameter("@F_dbL", SqlDbType.Float, 8),
                    new SqlParameter("@F_KoLuongCLDL", SqlDbType.Float, 8),
                    new SqlParameter("@F_KoLuongKLDL", SqlDbType.Float, 8),
                    new SqlParameter("@F_DiDuongL", SqlDbType.Float, 8),
                    new SqlParameter("@F_CongTacL", SqlDbType.Float, 8),
                    new SqlParameter("@F_HocSAGSL", SqlDbType.Float, 8),
                    new SqlParameter("@F_Hoc1L", SqlDbType.Float, 8),
                    new SqlParameter("@F_Hoc2L", SqlDbType.Float, 8),
                    new SqlParameter("@F_Hoc3L", SqlDbType.Float, 8),
                    new SqlParameter("@F_Hoc4L", SqlDbType.Float, 8),
                    new SqlParameter("@F_Hoc5L", SqlDbType.Float, 8),
                    new SqlParameter("@F_Hoc6L", SqlDbType.Float, 8),
                    new SqlParameter("@F_Hoc7L", SqlDbType.Float, 8),
                    new SqlParameter("@F_Con_OmL", SqlDbType.Float, 8),
                    new SqlParameter("@F_KHHDSL", SqlDbType.Float, 8),
                    new SqlParameter("@F_SayThaiL", SqlDbType.Float, 8),
                    new SqlParameter("@F_KhamThaiL", SqlDbType.Float, 8),
                    new SqlParameter("@F_ConChetL", SqlDbType.Float, 8),
                    new SqlParameter("@F_DinhChiCongTacL", SqlDbType.Float, 8),
                    new SqlParameter("@F_TamHoanHDL", SqlDbType.Float, 8),
                    new SqlParameter("@F_HoiHopL", SqlDbType.Float, 8),
                    new SqlParameter("@F_LeL", SqlDbType.Float, 8),
                    new SqlParameter("@NghiTuanL", SqlDbType.Float, 8),
                    new SqlParameter("@NghiBuL", SqlDbType.Float, 8),
                    new SqlParameter("@NghiViecL", SqlDbType.Float, 8),
                    new SqlParameter("@NightTimeL", SqlDbType.Float, 8),
                    new SqlParameter("@MarkL", SqlDbType.Float, 8),
                    new SqlParameter("@RankL", SqlDbType.NVarChar, 50),
                    new SqlParameter("@LastUpdateL", SqlDbType.DateTime, 8),
                    new SqlParameter("@UpdateUserL", SqlDbType.Int, 4),
                    new SqlParameter("@Remark", SqlDbType.NVarChar, 1000),
                    new SqlParameter("@ChuaDiLamL", SqlDbType.Float, 8)
                };

                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (WorkdayDateL.HasValue)
                    param[1].Value = WorkdayDateL.Value;
                else
                    param[1].Value = DBNull.Value;

                #region Day

                if ((Day1L == null) || (Day1L == ""))
                    param[2].Value = DBNull.Value;
                else
                    param[2].Value = Day1L;
                if ((Day2L == null) || (Day2L == ""))
                    param[3].Value = DBNull.Value;
                else
                    param[3].Value = Day2L;
                if ((Day3L == null) || (Day3L == ""))
                    param[4].Value = DBNull.Value;
                else
                    param[4].Value = Day3L;
                if ((Day4L == null) || (Day4L == ""))
                    param[5].Value = DBNull.Value;
                else
                    param[5].Value = Day4L;
                if ((Day5L == null) || (Day5L == ""))
                    param[6].Value = DBNull.Value;
                else
                    param[6].Value = Day5L;
                if ((Day6L == null) || (Day6L == ""))
                    param[7].Value = DBNull.Value;
                else
                    param[7].Value = Day6L;
                if ((Day7L == null) || (Day7L == ""))
                    param[8].Value = DBNull.Value;
                else
                    param[8].Value = Day7L;
                if ((Day8L == null) || (Day8L == ""))
                    param[9].Value = DBNull.Value;
                else
                    param[9].Value = Day8L;
                if ((Day9L == null) || (Day9L == ""))
                    param[10].Value = DBNull.Value;
                else
                    param[10].Value = Day9L;
                if ((Day10L == null) || (Day10L == ""))
                    param[11].Value = DBNull.Value;
                else
                    param[11].Value = Day10L;
                if ((Day11L == null) || (Day11L == ""))
                    param[12].Value = DBNull.Value;
                else
                    param[12].Value = Day11L;
                if ((Day12L == null) || (Day12L == ""))
                    param[13].Value = DBNull.Value;
                else
                    param[13].Value = Day12L;
                if ((Day13L == null) || (Day13L == ""))
                    param[14].Value = DBNull.Value;
                else
                    param[14].Value = Day13L;
                if ((Day14L == null) || (Day14L == ""))
                    param[15].Value = DBNull.Value;
                else
                    param[15].Value = Day14L;
                if ((Day15L == null) || (Day15L == ""))
                    param[16].Value = DBNull.Value;
                else
                    param[16].Value = Day15L;
                if ((Day16L == null) || (Day16L == ""))
                    param[17].Value = DBNull.Value;
                else
                    param[17].Value = Day16L;
                if ((Day17L == null) || (Day17L == ""))
                    param[18].Value = DBNull.Value;
                else
                    param[18].Value = Day17L;
                if ((Day18L == null) || (Day18L == ""))
                    param[19].Value = DBNull.Value;
                else
                    param[19].Value = Day18L;
                if ((Day19L == null) || (Day19L == ""))
                    param[20].Value = DBNull.Value;
                else
                    param[20].Value = Day19L;
                if ((Day20L == null) || (Day20L == ""))
                    param[21].Value = DBNull.Value;
                else
                    param[21].Value = Day20L;
                if ((Day21L == null) || (Day21L == ""))
                    param[22].Value = DBNull.Value;
                else
                    param[22].Value = Day21L;
                if ((Day22L == null) || (Day22L == ""))
                    param[23].Value = DBNull.Value;
                else
                    param[23].Value = Day22L;
                if ((Day23L == null) || (Day23L == ""))
                    param[24].Value = DBNull.Value;
                else
                    param[24].Value = Day23L;
                if ((Day24L == null) || (Day24L == ""))
                    param[25].Value = DBNull.Value;
                else
                    param[25].Value = Day24L;
                if ((Day25L == null) || (Day25L == ""))
                    param[26].Value = DBNull.Value;
                else
                    param[26].Value = Day25L;
                if ((Day26L == null) || (Day26L == ""))
                    param[27].Value = DBNull.Value;
                else
                    param[27].Value = Day26L;
                if ((Day27L == null) || (Day27L == ""))
                    param[28].Value = DBNull.Value;
                else
                    param[28].Value = Day27L;
                if ((Day28L == null) || (Day28L == ""))
                    param[29].Value = DBNull.Value;
                else
                    param[29].Value = Day28L;
                if ((Day29L == null) || (Day29L == ""))
                    param[30].Value = DBNull.Value;
                else
                    param[30].Value = Day29L;
                if ((Day30L == null) || (Day30L == ""))
                    param[31].Value = DBNull.Value;
                else
                    param[31].Value = Day30L;
                if ((Day31L == null) || (Day31L == ""))
                    param[32].Value = DBNull.Value;
                else
                    param[32].Value = Day31L;

                #endregion

                #region workdayEmployees Luong

                if (XL.HasValue)
                    if (XL == Constants.WorkdayEmployee_DefaultValue)
                        param[33].Value = DBNull.Value;
                    else
                        param[33].Value = XL.Value;
                else
                    param[33].Value = DBNull.Value;

                if (F_OmL.HasValue)
                    if (F_OmL == Constants.WorkdayEmployee_DefaultValue)
                        param[34].Value = DBNull.Value;
                    else
                        param[34].Value = F_OmL.Value;
                else
                    param[34].Value = DBNull.Value;
                if (F_OmDaiNgayL.HasValue)
                    if (F_OmDaiNgayL == Constants.WorkdayEmployee_DefaultValue)
                        param[35].Value = DBNull.Value;
                    else
                        param[35].Value = F_OmDaiNgayL.Value;
                else
                    param[35].Value = DBNull.Value;
                if (F_ThaiSanL.HasValue)
                    if (F_ThaiSanL == Constants.WorkdayEmployee_DefaultValue)
                        param[36].Value = DBNull.Value;
                    else
                        param[36].Value = F_ThaiSanL.Value;
                else
                    param[36].Value = DBNull.Value;
                if (F_TNLDL.HasValue)
                    if (F_TNLDL == Constants.WorkdayEmployee_DefaultValue)
                        param[37].Value = DBNull.Value;
                    else
                        param[37].Value = F_TNLDL.Value;
                else
                    param[37].Value = DBNull.Value;
                if (F_NamL.HasValue)
                    if (F_NamL == Constants.WorkdayEmployee_DefaultValue)
                        param[38].Value = DBNull.Value;
                    else
                        param[38].Value = F_NamL.Value;
                else
                    param[38].Value = DBNull.Value;


                if (F_dbL.HasValue)
                    if (F_dbL == Constants.WorkdayEmployee_DefaultValue)
                        param[39].Value = DBNull.Value;
                    else
                        param[39].Value = F_dbL.Value;
                else
                    param[39].Value = DBNull.Value;
                if (F_KoLuongCLDL.HasValue)
                    if (F_KoLuongCLDL == Constants.WorkdayEmployee_DefaultValue)
                        param[40].Value = DBNull.Value;
                    else
                        param[40].Value = F_KoLuongCLDL.Value;
                else
                    param[40].Value = DBNull.Value;
                if (F_KoLuongKLDL.HasValue)
                    if (F_KoLuongKLDL == Constants.WorkdayEmployee_DefaultValue)
                        param[41].Value = DBNull.Value;
                    else
                        param[41].Value = F_KoLuongKLDL.Value;
                else
                    param[41].Value = DBNull.Value;
                if (F_DiDuongL.HasValue)
                    if (F_DiDuongL == Constants.WorkdayEmployee_DefaultValue)
                        param[42].Value = DBNull.Value;
                    else
                        param[42].Value = F_DiDuongL.Value;
                else
                    param[42].Value = DBNull.Value;
                if (F_CongTacL.HasValue)
                    if (F_CongTacL == Constants.WorkdayEmployee_DefaultValue)
                        param[43].Value = DBNull.Value;
                    else
                        param[43].Value = F_CongTacL.Value;
                else
                    param[43].Value = DBNull.Value;

                if (F_HocSAGS.HasValue)
                    if (F_HocSAGS == Constants.WorkdayEmployee_DefaultValue)
                        param[44].Value = DBNull.Value;
                    else
                        param[44].Value = F_HocSAGS.Value;
                else
                    param[44].Value = DBNull.Value;
                if (F_Hoc1L.HasValue)
                    if (F_Hoc1L == Constants.WorkdayEmployee_DefaultValue)
                        param[45].Value = DBNull.Value;
                    else
                        param[45].Value = F_Hoc1L.Value;
                else
                    param[45].Value = DBNull.Value;
                if (F_Hoc2L.HasValue)
                    if (F_Hoc2L == Constants.WorkdayEmployee_DefaultValue)
                        param[46].Value = DBNull.Value;
                    else
                        param[46].Value = F_Hoc2L.Value;
                else
                    param[46].Value = DBNull.Value;
                if (F_Hoc3L.HasValue)
                    if (F_Hoc3L == Constants.WorkdayEmployee_DefaultValue)
                        param[47].Value = DBNull.Value;
                    else
                        param[47].Value = F_Hoc3L.Value;
                else
                    param[47].Value = DBNull.Value;
                if (F_Hoc4L.HasValue)
                    if (F_Hoc4L == Constants.WorkdayEmployee_DefaultValue)
                        param[48].Value = DBNull.Value;
                    else
                        param[48].Value = F_Hoc4L.Value;
                else
                    param[48].Value = DBNull.Value;
                if (F_Hoc5L.HasValue)
                    if (F_Hoc5L == Constants.WorkdayEmployee_DefaultValue)
                        param[49].Value = DBNull.Value;
                    else
                        param[49].Value = F_Hoc5L.Value;
                else
                    param[49].Value = DBNull.Value;
                if (F_Hoc6L.HasValue)
                    if (F_Hoc6L == Constants.WorkdayEmployee_DefaultValue)
                        param[50].Value = DBNull.Value;
                    else
                        param[50].Value = F_Hoc6L.Value;
                else
                    param[50].Value = DBNull.Value;
                if (F_Hoc7L.HasValue)
                    if (F_Hoc7L == Constants.WorkdayEmployee_DefaultValue)
                        param[51].Value = DBNull.Value;
                    else
                        param[51].Value = F_Hoc7L.Value;
                else
                    param[51].Value = DBNull.Value;

                if (F_Con_OmL.HasValue)
                    if (F_Con_OmL == Constants.WorkdayEmployee_DefaultValue)
                        param[52].Value = DBNull.Value;
                    else
                        param[52].Value = F_Con_OmL.Value;
                else
                    param[52].Value = DBNull.Value;
                if (F_KHHDSL.HasValue)
                    if (F_KHHDSL == Constants.WorkdayEmployee_DefaultValue)
                        param[53].Value = DBNull.Value;
                    else
                        param[53].Value = F_KHHDSL.Value;
                else
                    param[53].Value = DBNull.Value;
                if (F_SayThaiL.HasValue)
                    if (F_SayThaiL == Constants.WorkdayEmployee_DefaultValue)
                        param[54].Value = DBNull.Value;
                    else
                        param[54].Value = F_SayThaiL.Value;
                else
                    param[54].Value = DBNull.Value;
                if (F_KhamThaiL.HasValue)
                    if (F_KhamThaiL == Constants.WorkdayEmployee_DefaultValue)
                        param[55].Value = DBNull.Value;
                    else
                        param[55].Value = F_KhamThaiL.Value;
                else
                    param[55].Value = DBNull.Value;
                if (F_ConChetL.HasValue)
                    if (F_ConChetL == Constants.WorkdayEmployee_DefaultValue)
                        param[56].Value = DBNull.Value;
                    else
                        param[56].Value = F_ConChetL.Value;
                else
                    param[56].Value = DBNull.Value;
                if (F_DinhChiCongTacL.HasValue)
                    if (F_DinhChiCongTacL == Constants.WorkdayEmployee_DefaultValue)
                        param[57].Value = DBNull.Value;
                    else
                        param[57].Value = F_DinhChiCongTacL.Value;
                else
                    param[57].Value = DBNull.Value;


                if (F_TamHoanHDL.HasValue)
                    if (F_TamHoanHDL == Constants.WorkdayEmployee_DefaultValue)
                        param[58].Value = DBNull.Value;
                    else
                        param[58].Value = F_TamHoanHDL.Value;
                else
                    param[58].Value = DBNull.Value;
                if (F_HoiHopL.HasValue)
                    if (F_HoiHopL == Constants.WorkdayEmployee_DefaultValue)
                        param[59].Value = DBNull.Value;
                    else
                        param[59].Value = F_HoiHopL.Value;
                else
                    param[59].Value = DBNull.Value;
                if (F_LeL.HasValue)
                    if (F_LeL == Constants.WorkdayEmployee_DefaultValue)
                        param[60].Value = DBNull.Value;
                    else
                        param[60].Value = F_LeL.Value;
                else
                    param[60].Value = DBNull.Value;
                if (NghiTuanL.HasValue)
                    if (NghiTuanL == Constants.WorkdayEmployee_DefaultValue)
                        param[61].Value = DBNull.Value;
                    else
                        param[61].Value = NghiTuanL.Value;
                else
                    param[61].Value = DBNull.Value;

                if (NghiBuL.HasValue)
                    if (NghiBuL == Constants.WorkdayEmployee_DefaultValue)
                        param[62].Value = DBNull.Value;
                    else
                        param[62].Value = NghiBuL.Value;
                else
                    param[62].Value = DBNull.Value;
                if (NghiViecL.HasValue)
                    if (NghiViecL == Constants.WorkdayEmployee_DefaultValue)
                        param[63].Value = DBNull.Value;
                    else
                        param[63].Value = NghiViecL.Value;
                else
                    param[63].Value = DBNull.Value;


                if (NightTimeL.HasValue)
                    if (NightTimeL == Constants.WorkdayEmployee_DefaultValue)
                        param[64].Value = DBNull.Value;
                    else
                        param[64].Value = NightTimeL.Value;
                else
                    param[64].Value = DBNull.Value;

                #endregion

                if (MarkL.HasValue)
                    if (MarkL == Constants.WorkdayEmployee_DefaultValue)
                        param[65].Value = DBNull.Value;
                    else
                        param[65].Value = MarkL.Value;
                else
                    param[65].Value = DBNull.Value;

                if ((RankL == null) || (RankL == ""))
                    param[66].Value = DBNull.Value;
                else
                    param[66].Value = RankL;

                if (LastUpdateL.HasValue)
                    param[67].Value = LastUpdateL.Value;
                else
                    param[67].Value = DBNull.Value;
                if (UpdateUserL.HasValue)
                    if (UpdateUserL == Constants.WorkdayEmployee_DefaultValue)
                        param[68].Value = DBNull.Value;
                    else
                        param[68].Value = UpdateUserL.Value;
                else
                    param[68].Value = DBNull.Value;

                if ((Remark == null) || (Remark == ""))
                    param[69].Value = DBNull.Value;
                else
                    param[69].Value = Remark;

                if (ChuaDiLamL.HasValue)
                    if (ChuaDiLamL == Constants.WorkdayEmployee_DefaultValue)
                        param[70].Value = DBNull.Value;
                    else
                        param[70].Value = ChuaDiLamL.Value;
                else
                    param[70].Value = DBNull.Value;

                sproc = new StoreProcedure(WorkdayEmployeeLKeys.Sp_Upd_H1_WorkdayEmployeeL_By_UserId_Date, param);
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


        public int Update_By_MarkHTCV(int? UserId, double? MarkL, DateTime? LastUpdateL, int? UpdateUserL, int? MonthL,
            int? YearL)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@MarkL", SqlDbType.Float),
                    new SqlParameter("@LastUpdateL", SqlDbType.DateTime),
                    new SqlParameter("@UpdateUserL", SqlDbType.Int),
                    new SqlParameter("@MonthL", SqlDbType.Int),
                    new SqlParameter("@YearL", SqlDbType.Int)
                };
                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (MarkL.HasValue)
                    param[1].Value = MarkL.Value;
                else
                    param[1].Value = DBNull.Value;
                if (LastUpdateL.HasValue)
                    param[2].Value = LastUpdateL.Value;
                else
                    param[2].Value = DBNull.Value;
                if (UpdateUserL.HasValue)
                    param[3].Value = UpdateUserL.Value;
                else
                    param[3].Value = DBNull.Value;
                if (MonthL.HasValue)
                    param[4].Value = MonthL.Value;
                else
                    param[4].Value = DBNull.Value;
                if (YearL.HasValue)
                    param[5].Value = YearL.Value;
                else
                    param[5].Value = DBNull.Value;


                sproc = new StoreProcedure(WorkdayEmployeeLKeys.Sp_Upd_H1_WorkdayEmployeeL_By_MarkHTCV, param);
                sproc.Run();
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


        public int DeleteByDeptIdsDate(string deptIds, int month, int year)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DeptIds", SqlDbType.VarChar, 254),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int)
                };

                param[0].Value = deptIds;
                param[1].Value = month;
                param[2].Value = year;

                sproc = new StoreProcedure(WorkdayEmployeeLKeys.Sp_Del_H1_WorkdayEmployeeL_By_DeptIds_Date, param);

                sproc.Run();
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

        #region Methods GET

        public DataTable GetByFilter(string fullName, string departmentIds, int month, int year, int TypeSort)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@DeptIds", SqlDbType.VarChar, 254),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int),
                    new SqlParameter("@TypeSort", SqlDbType.Int)
                };

                param[0].Value = fullName;
                param[1].Value = departmentIds;
                param[2].Value = month;
                param[3].Value = year;
                param[4].Value = TypeSort;

                sproc = new StoreProcedure(WorkdayEmployeeLKeys.Sp_Sel_H1_WorkdayEmployeeL_By_Filter, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetById(long workdayEmployeeId)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@WorkdayEmployeeId", SqlDbType.BigInt)
                };

                param[0].Value = workdayEmployeeId;

                sproc = new StoreProcedure(WorkdayEmployeeLKeys.Sp_Sel_H1_WorkdayEmployeeL_By_Id, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByUserId_Month_Year(int userId, int month, int year, int statusL)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int),
                    new SqlParameter("@StatusL", SqlDbType.Int)
                };

                param[0].Value = userId;
                param[1].Value = month;
                param[2].Value = year;
                param[3].Value = statusL;

                sproc = new StoreProcedure(WorkdayEmployeeLKeys.Sp_Sel_H1_WorkdayEmployeeL_By_UserId_Month_Year, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByRootId(int rootId, int month, int year)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@RootId", SqlDbType.Int),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int)
                };

                param[0].Value = rootId;
                param[1].Value = month;
                param[2].Value = year;

                sproc = new StoreProcedure(WorkdayEmployeeLKeys.Sp_Sel_H1_WorkdayEmployeeL_By_RootId, param);
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
    }
}