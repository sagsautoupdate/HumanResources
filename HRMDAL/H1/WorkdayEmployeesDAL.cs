using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H1;

namespace HRMDAL.H1
{
    public class WorkdayEmployeesDAL : Dao
    {
        #region insert, update, delete

        public long InsertByDate_DeptIds(string Day25, string Day26, string Day27, string Day28, string Day29,
            string Day30, string Day31, string DepartmentIds, DateTime? WorkdayDate, int? CreateUser, int? Status,
            string writeUserIds, string readUserIds, double? XQD, int? Type, bool isOfficeHours)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@Day25", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day26", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day27", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day28", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day29", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day30", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day31", SqlDbType.VarChar, 20),
                    new SqlParameter("@DeptIds", SqlDbType.VarChar, 1000),
                    new SqlParameter("@WorkdayDate", SqlDbType.DateTime, 8),
                    new SqlParameter("@CreateUser", SqlDbType.Int, 4),
                    new SqlParameter("@Status", SqlDbType.Int, 4),
                    new SqlParameter("@WriteUserIds", SqlDbType.VarChar, 1000),
                    new SqlParameter("@ReadUserIds", SqlDbType.VarChar, 1000),
                    new SqlParameter("@XQD", SqlDbType.Float),
                    new SqlParameter("@Type", SqlDbType.Float),
                    new SqlParameter("@isOfficeHours", SqlDbType.Bit)
                };

                if ((Day25 == null) || (Day25 == ""))
                    param[0].Value = DBNull.Value;
                else
                    param[0].Value = Day25;
                if ((Day26 == null) || (Day26 == ""))
                    param[1].Value = DBNull.Value;
                else
                    param[1].Value = Day26;
                if ((Day27 == null) || (Day27 == ""))
                    param[2].Value = DBNull.Value;
                else
                    param[2].Value = Day27;
                if ((Day28 == null) || (Day28 == ""))
                    param[3].Value = DBNull.Value;
                else
                    param[3].Value = Day28;
                if ((Day29 == null) || (Day29 == ""))
                    param[4].Value = DBNull.Value;
                else
                    param[4].Value = Day29;
                if ((Day30 == null) || (Day30 == ""))
                    param[5].Value = DBNull.Value;
                else
                    param[5].Value = Day30;
                if ((Day31 == null) || (Day31 == ""))
                    param[6].Value = DBNull.Value;
                else
                    param[6].Value = Day31;

                if (DepartmentIds == null)
                    param[7].Value = DBNull.Value;
                else
                    param[7].Value = DepartmentIds;
                if (WorkdayDate.HasValue)
                    param[8].Value = WorkdayDate.Value;
                else
                    param[8].Value = DBNull.Value;
                if (CreateUser.HasValue)
                    param[9].Value = CreateUser.Value;
                else
                    param[9].Value = DBNull.Value;
                if (Status.HasValue)
                    param[10].Value = Status.Value;
                else
                    param[10].Value = DBNull.Value;
                if ((writeUserIds == null) || (writeUserIds == ""))
                    param[11].Value = DBNull.Value;
                else
                    param[11].Value = writeUserIds;
                if ((readUserIds == null) || (readUserIds == ""))
                    param[12].Value = DBNull.Value;
                else
                    param[12].Value = readUserIds;
                if (XQD.HasValue)
                    if (XQD == Constants.WorkdayEmployee_DefaultValue)
                        param[13].Value = DBNull.Value;
                    else
                        param[13].Value = XQD.Value;
                else
                    param[13].Value = DBNull.Value;

                if (Type.HasValue)
                    if (Type == Constants.WorkdayEmployee_DefaultValue)
                        param[14].Value = DBNull.Value;
                    else
                        param[14].Value = (double) Type.Value;
                else
                    param[14].Value = DBNull.Value;
                param[15].Value = isOfficeHours;

                sproc = new StoreProcedure(WorkdayEmployeeKeys.Sp_Ins_H1_WorkdayEmployee_By_Date_DeptIds, param);
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

        public long InsertByDate_UserId(int? UserId, DateTime? WorkdayDate, string Day25, string Day26, string Day27,
            string Day28, string Day29, string Day30, string Day31, int? CreateUser, int? Status, string writeUserIds,
            string readUserIds, double? XQD, int? Type, bool isOfficeHours)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@WorkdayDate", SqlDbType.DateTime, 8),
                    new SqlParameter("@Day25", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day26", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day27", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day28", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day29", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day30", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day31", SqlDbType.VarChar, 20),
                    new SqlParameter("@CreateUser", SqlDbType.Int, 4),
                    new SqlParameter("@Status", SqlDbType.Int, 4),
                    new SqlParameter("@WriteUserIds", SqlDbType.VarChar, 1000),
                    new SqlParameter("@ReadUserIds", SqlDbType.VarChar, 1000),
                    new SqlParameter("@XQD", SqlDbType.Float),
                    new SqlParameter("@Type", SqlDbType.Float),
                    new SqlParameter("@isOfficeHours", SqlDbType.Bit)
                };

                if (UserId.HasValue)
                    if (UserId == Constants.WorkdayEmployee_DefaultValue)
                        param[0].Value = DBNull.Value;
                    else
                        param[0].Value = (double) UserId.Value;
                else
                    param[0].Value = DBNull.Value;

                if (WorkdayDate.HasValue)
                    param[1].Value = WorkdayDate.Value;
                else
                    param[1].Value = DBNull.Value;

                if ((Day25 == null) || (Day25 == ""))
                    param[2].Value = DBNull.Value;
                else
                    param[2].Value = Day25;
                if ((Day26 == null) || (Day26 == ""))
                    param[3].Value = DBNull.Value;
                else
                    param[3].Value = Day26;
                if ((Day27 == null) || (Day27 == ""))
                    param[4].Value = DBNull.Value;
                else
                    param[4].Value = Day27;
                if ((Day28 == null) || (Day28 == ""))
                    param[5].Value = DBNull.Value;
                else
                    param[5].Value = Day28;
                if ((Day29 == null) || (Day29 == ""))
                    param[6].Value = DBNull.Value;
                else
                    param[6].Value = Day29;
                if ((Day30 == null) || (Day30 == ""))
                    param[7].Value = DBNull.Value;
                else
                    param[7].Value = Day30;
                if ((Day31 == null) || (Day31 == ""))
                    param[8].Value = DBNull.Value;
                else
                    param[8].Value = Day31;

                if (CreateUser.HasValue)
                    param[9].Value = CreateUser.Value;
                else
                    param[9].Value = DBNull.Value;
                if (Status.HasValue)
                    param[10].Value = Status.Value;
                else
                    param[10].Value = DBNull.Value;
                if ((writeUserIds == null) || (writeUserIds == ""))
                    param[11].Value = DBNull.Value;
                else
                    param[11].Value = writeUserIds;
                if ((readUserIds == null) || (readUserIds == ""))
                    param[12].Value = DBNull.Value;
                else
                    param[12].Value = readUserIds;
                if (XQD.HasValue)
                    if (XQD == Constants.WorkdayEmployee_DefaultValue)
                        param[13].Value = DBNull.Value;
                    else
                        param[13].Value = XQD.Value;
                else
                    param[13].Value = DBNull.Value;

                if (Type.HasValue)
                    if (Type == Constants.WorkdayEmployee_DefaultValue)
                        param[14].Value = DBNull.Value;
                    else
                        param[14].Value = (double) Type.Value;
                else
                    param[14].Value = DBNull.Value;
                param[15].Value = isOfficeHours;
                sproc = new StoreProcedure(WorkdayEmployeeKeys.Sp_Ins_H1_WorkdayEmployees, param);
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
            DateTime? WorkdayDate,
            string Day1, string Day2, string Day3, string Day4, string Day5, string Day6, string Day7, string Day8,
            string Day9,
            string Day10, string Day11, string Day12, string Day13, string Day14, string Day15, string Day16,
            string Day17, string Day18, string Day19,
            string Day20, string Day21, string Day22, string Day23, string Day24, string Day25, string Day26,
            string Day27, string Day28, string Day29, string Day30, string Day31,
            double? NC_LamViec, double? F_Om, double? F_OmDaiNgay, double? F_ThaiSan, double? F_TNLD, double? F_Nam,
            double? F_db, double? F_KoLuongCLD, double? F_KoLuongKLD, double? F_DiDuong, double? F_CongTac,
            double? F_HocSAGS, double? F_Hoc1, double? F_Hoc2, double? F_Hoc3, double? F_Hoc4, double? F_Hoc5,
            double? F_Hoc6, double? F_Hoc7,
            double? F_Con_Om, double? F_KHHDS, double? F_SayThai, double? F_KhamThai, double? F_ConChet,
            double? F_DinhChiCongTac,
            double? F_TamHoanHD, double? F_HoiHop, double? F_Le,
            double? NghiTuan, double? NghiBu, double? NghiViec, double? NghiMat, double? CongDu,
            double? GC_LamDem, double? Mark,
            string HTCV, DateTime? LastUpdate, int? UpdateUser, int? Status,
            double? Night1, double? Night2, double? Night3, double? Night4, double? Night5, double? Night6,
            double? Night7, double? Night8, double? Night9, double? Night10,
            double? Night11, double? Night12, double? Night13, double? Night14, double? Night15, double? Night16,
            double? Night17, double? Night18, double? Night19, double? Night20,
            double? Night21, double? Night22, double? Night23, double? Night24, double? Night25, double? Night26,
            double? Night27, double? Night28, double? Night29, double? Night30, double? Night31,
            string remark
        )
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@WorkdayDate", SqlDbType.DateTime, 8),

                    #region day
                    new SqlParameter("@Day1", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day2", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day3", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day4", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day5", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day6", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day7", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day8", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day9", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day10", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day11", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day12", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day13", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day14", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day15", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day16", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day17", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day18", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day19", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day20", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day21", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day22", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day23", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day24", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day25", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day26", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day27", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day28", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day29", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day30", SqlDbType.VarChar, 20),
                    new SqlParameter("@Day31", SqlDbType.VarChar, 20),

                    #endregion

                    
                    new SqlParameter("@NC_LamViec", SqlDbType.Float, 8),
                    new SqlParameter("@F_Om", SqlDbType.Float, 8),
                    new SqlParameter("@F_OmDaiNgay", SqlDbType.Float, 8),
                    new SqlParameter("@F_ThaiSan", SqlDbType.Float, 8),
                    new SqlParameter("@F_TNLD", SqlDbType.Float, 8),
                    new SqlParameter("@F_Nam", SqlDbType.Float, 8),
                    new SqlParameter("@F_db", SqlDbType.Float, 8),
                    new SqlParameter("@F_KoLuongCLD", SqlDbType.Float, 8),
                    new SqlParameter("@F_KoLuongKLD", SqlDbType.Float, 8),
                    new SqlParameter("@F_DiDuong", SqlDbType.Float, 8),
                    new SqlParameter("@F_CongTac", SqlDbType.Float, 8),
                    new SqlParameter("@F_HocSAGS", SqlDbType.Float, 8),
                    new SqlParameter("@F_Hoc1", SqlDbType.Float, 8),
                    new SqlParameter("@F_Hoc2", SqlDbType.Float, 8),
                    new SqlParameter("@F_Hoc3", SqlDbType.Float, 8),
                    new SqlParameter("@F_Hoc4", SqlDbType.Float, 8),
                    new SqlParameter("@F_Hoc5", SqlDbType.Float, 8),
                    new SqlParameter("@F_Hoc6", SqlDbType.Float, 8),
                    new SqlParameter("@F_Hoc7", SqlDbType.Float, 8),
                    new SqlParameter("@F_Con_Om", SqlDbType.Float, 8),
                    new SqlParameter("@F_KHHDS", SqlDbType.Float, 8),
                    new SqlParameter("@F_SayThai", SqlDbType.Float, 8),
                    new SqlParameter("@F_KhamThai", SqlDbType.Float, 8),
                    new SqlParameter("@F_ConChet", SqlDbType.Float, 8),
                    new SqlParameter("@F_DinhChiCongTac", SqlDbType.Float, 8),
                    new SqlParameter("@F_TamHoanHD", SqlDbType.Float, 8),
                    new SqlParameter("@F_HoiHop", SqlDbType.Float, 8),
                    new SqlParameter("@F_Le", SqlDbType.Float, 8),
                    new SqlParameter("@NghiTuan", SqlDbType.Float, 8),
                    new SqlParameter("@NghiBu", SqlDbType.Float, 8),
                    new SqlParameter("@NghiViec", SqlDbType.Float, 8),
                    new SqlParameter("@NghiMat", SqlDbType.Float, 8),
                    new SqlParameter("@CongDu", SqlDbType.Float, 8),
                    new SqlParameter("@GC_LamDem", SqlDbType.Float, 8),
                    new SqlParameter("@Mark", SqlDbType.Float, 8),
                    new SqlParameter("@HTCV", SqlDbType.VarChar, 50),
                    new SqlParameter("@LastUpdate", SqlDbType.DateTime, 8),
                    new SqlParameter("@UpdateUser", SqlDbType.Int, 4),
                    new SqlParameter("@Status", SqlDbType.Int, 4),

                    #region night

                    new SqlParameter("@Night1", SqlDbType.Float, 8),
                    new SqlParameter("@Night2", SqlDbType.Float, 8),
                    new SqlParameter("@Night3", SqlDbType.Float, 8),
                    new SqlParameter("@Night4", SqlDbType.Float, 8),
                    new SqlParameter("@Night5", SqlDbType.Float, 8),
                    new SqlParameter("@Night6", SqlDbType.Float, 8),
                    new SqlParameter("@Night7", SqlDbType.Float, 8),
                    new SqlParameter("@Night8", SqlDbType.Float, 8),
                    new SqlParameter("@Night9", SqlDbType.Float, 8),
                    new SqlParameter("@Night10", SqlDbType.Float, 8),
                    new SqlParameter("@Night11", SqlDbType.Float, 8),
                    new SqlParameter("@Night12", SqlDbType.Float, 8),
                    new SqlParameter("@Night13", SqlDbType.Float, 8),
                    new SqlParameter("@Night14", SqlDbType.Float, 8),
                    new SqlParameter("@Night15", SqlDbType.Float, 8),
                    new SqlParameter("@Night16", SqlDbType.Float, 8),
                    new SqlParameter("@Night17", SqlDbType.Float, 8),
                    new SqlParameter("@Night18", SqlDbType.Float, 8),
                    new SqlParameter("@Night19", SqlDbType.Float, 8),
                    new SqlParameter("@Night20", SqlDbType.Float, 8),
                    new SqlParameter("@Night21", SqlDbType.Float, 8),
                    new SqlParameter("@Night22", SqlDbType.Float, 8),
                    new SqlParameter("@Night23", SqlDbType.Float, 8),
                    new SqlParameter("@Night24", SqlDbType.Float, 8),
                    new SqlParameter("@Night25", SqlDbType.Float, 8),
                    new SqlParameter("@Night26", SqlDbType.Float, 8),
                    new SqlParameter("@Night27", SqlDbType.Float, 8),
                    new SqlParameter("@Night28", SqlDbType.Float, 8),
                    new SqlParameter("@Night29", SqlDbType.Float, 8),
                    new SqlParameter("@Night30", SqlDbType.Float, 8),
                    new SqlParameter("@Night31", SqlDbType.Float, 8),

                    #endregion

                    new SqlParameter("@Remark", SqlDbType.NVarChar, 1000)
                };

                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (WorkdayDate.HasValue)
                    param[1].Value = WorkdayDate.Value;
                else
                    param[1].Value = DBNull.Value;

                #region Day

                if ((Day1 == null) || (Day1 == ""))
                    param[2].Value = DBNull.Value;
                else
                    param[2].Value = Day1;
                if ((Day2 == null) || (Day2 == ""))
                    param[3].Value = DBNull.Value;
                else
                    param[3].Value = Day2;
                if ((Day3 == null) || (Day3 == ""))
                    param[4].Value = DBNull.Value;
                else
                    param[4].Value = Day3;
                if ((Day4 == null) || (Day4 == ""))
                    param[5].Value = DBNull.Value;
                else
                    param[5].Value = Day4;
                if ((Day5 == null) || (Day5 == ""))
                    param[6].Value = DBNull.Value;
                else
                    param[6].Value = Day5;
                if ((Day6 == null) || (Day6 == ""))
                    param[7].Value = DBNull.Value;
                else
                    param[7].Value = Day6;
                if ((Day7 == null) || (Day7 == ""))
                    param[8].Value = DBNull.Value;
                else
                    param[8].Value = Day7;
                if ((Day8 == null) || (Day8 == ""))
                    param[9].Value = DBNull.Value;
                else
                    param[9].Value = Day8;
                if ((Day9 == null) || (Day9 == ""))
                    param[10].Value = DBNull.Value;
                else
                    param[10].Value = Day9;
                if ((Day10 == null) || (Day10 == ""))
                    param[11].Value = DBNull.Value;
                else
                    param[11].Value = Day10;
                if ((Day11 == null) || (Day11 == ""))
                    param[12].Value = DBNull.Value;
                else
                    param[12].Value = Day11;
                if ((Day12 == null) || (Day12 == ""))
                    param[13].Value = DBNull.Value;
                else
                    param[13].Value = Day12;
                if ((Day13 == null) || (Day13 == ""))
                    param[14].Value = DBNull.Value;
                else
                    param[14].Value = Day13;
                if ((Day14 == null) || (Day14 == ""))
                    param[15].Value = DBNull.Value;
                else
                    param[15].Value = Day14;
                if ((Day15 == null) || (Day15 == ""))
                    param[16].Value = DBNull.Value;
                else
                    param[16].Value = Day15;
                if ((Day16 == null) || (Day16 == ""))
                    param[17].Value = DBNull.Value;
                else
                    param[17].Value = Day16;
                if ((Day17 == null) || (Day17 == ""))
                    param[18].Value = DBNull.Value;
                else
                    param[18].Value = Day17;
                if ((Day18 == null) || (Day18 == ""))
                    param[19].Value = DBNull.Value;
                else
                    param[19].Value = Day18;
                if ((Day19 == null) || (Day19 == ""))
                    param[20].Value = DBNull.Value;
                else
                    param[20].Value = Day19;
                if ((Day20 == null) || (Day20 == ""))
                    param[21].Value = DBNull.Value;
                else
                    param[21].Value = Day20;
                if ((Day21 == null) || (Day21 == ""))
                    param[22].Value = DBNull.Value;
                else
                    param[22].Value = Day21;
                if ((Day22 == null) || (Day22 == ""))
                    param[23].Value = DBNull.Value;
                else
                    param[23].Value = Day22;
                if ((Day23 == null) || (Day23 == ""))
                    param[24].Value = DBNull.Value;
                else
                    param[24].Value = Day23;
                if ((Day24 == null) || (Day24 == ""))
                    param[25].Value = DBNull.Value;
                else
                    param[25].Value = Day24;
                if ((Day25 == null) || (Day25 == ""))
                    param[26].Value = DBNull.Value;
                else
                    param[26].Value = Day25;
                if ((Day26 == null) || (Day26 == ""))
                    param[27].Value = DBNull.Value;
                else
                    param[27].Value = Day26;
                if ((Day27 == null) || (Day27 == ""))
                    param[28].Value = DBNull.Value;
                else
                    param[28].Value = Day27;
                if ((Day28 == null) || (Day28 == ""))
                    param[29].Value = DBNull.Value;
                else
                    param[29].Value = Day28;
                if ((Day29 == null) || (Day29 == ""))
                    param[30].Value = DBNull.Value;
                else
                    param[30].Value = Day29;
                if ((Day30 == null) || (Day30 == ""))
                    param[31].Value = DBNull.Value;
                else
                    param[31].Value = Day30;
                if ((Day31 == null) || (Day31 == ""))
                    param[32].Value = DBNull.Value;
                else
                    param[32].Value = Day31;

                #endregion

                #region Collect WorkdayEmplyee

                if (NC_LamViec.HasValue)
                    if (NC_LamViec == Constants.WorkdayEmployee_DefaultValue)
                        param[33].Value = DBNull.Value;
                    else
                        param[33].Value = NC_LamViec.Value;
                else
                    param[33].Value = DBNull.Value;
                if (F_Om.HasValue)
                    if (F_Om == Constants.WorkdayEmployee_DefaultValue)
                        param[34].Value = DBNull.Value;
                    else
                        param[34].Value = F_Om.Value;
                else
                    param[34].Value = DBNull.Value;
                if (F_OmDaiNgay.HasValue)
                    if (F_OmDaiNgay == Constants.WorkdayEmployee_DefaultValue)
                        param[35].Value = DBNull.Value;
                    else
                        param[35].Value = F_OmDaiNgay.Value;
                else
                    param[35].Value = DBNull.Value;
                if (F_ThaiSan.HasValue)
                    if (F_ThaiSan == Constants.WorkdayEmployee_DefaultValue)
                        param[36].Value = DBNull.Value;
                    else
                        param[36].Value = F_ThaiSan.Value;
                else
                    param[36].Value = DBNull.Value;
                if (F_TNLD.HasValue)
                    if (F_TNLD == Constants.WorkdayEmployee_DefaultValue)
                        param[37].Value = DBNull.Value;
                    else
                        param[37].Value = F_TNLD.Value;
                else
                    param[37].Value = DBNull.Value;
                if (F_Nam.HasValue)
                    if (F_Nam == Constants.WorkdayEmployee_DefaultValue)
                        param[38].Value = DBNull.Value;
                    else
                        param[38].Value = F_Nam.Value;
                else
                    param[38].Value = DBNull.Value;
                if (F_db.HasValue)
                    if (F_db == Constants.WorkdayEmployee_DefaultValue)
                        param[39].Value = DBNull.Value;
                    else
                        param[39].Value = F_db.Value;
                else
                    param[39].Value = DBNull.Value;
                if (F_KoLuongCLD.HasValue)
                    if (F_KoLuongCLD == Constants.WorkdayEmployee_DefaultValue)
                        param[40].Value = DBNull.Value;
                    else
                        param[40].Value = F_KoLuongCLD.Value;
                else
                    param[40].Value = DBNull.Value;
                if (F_KoLuongKLD.HasValue)
                    if (F_KoLuongKLD == Constants.WorkdayEmployee_DefaultValue)
                        param[41].Value = DBNull.Value;
                    else
                        param[41].Value = F_KoLuongKLD.Value;
                else
                    param[41].Value = DBNull.Value;
                if (F_DiDuong.HasValue)
                    if (F_DiDuong == Constants.WorkdayEmployee_DefaultValue)
                        param[42].Value = DBNull.Value;
                    else
                        param[42].Value = F_DiDuong.Value;
                else
                    param[42].Value = DBNull.Value;
                if (F_CongTac.HasValue)
                    if (F_CongTac == Constants.WorkdayEmployee_DefaultValue)
                        param[43].Value = DBNull.Value;
                    else
                        param[43].Value = F_CongTac.Value;
                else
                    param[43].Value = DBNull.Value;
                if (F_HocSAGS.HasValue)
                    if (F_HocSAGS == Constants.WorkdayEmployee_DefaultValue)
                        param[44].Value = DBNull.Value;
                    else
                        param[44].Value = F_HocSAGS.Value;
                else
                    param[44].Value = DBNull.Value;
                if (F_Hoc1.HasValue)
                    if (F_Hoc1 == Constants.WorkdayEmployee_DefaultValue)
                        param[45].Value = DBNull.Value;
                    else
                        param[45].Value = F_Hoc1.Value;
                else
                    param[45].Value = DBNull.Value;
                if (F_Hoc2.HasValue)
                    if (F_Hoc2 == Constants.WorkdayEmployee_DefaultValue)
                        param[46].Value = DBNull.Value;
                    else
                        param[46].Value = F_Hoc2.Value;
                else
                    param[46].Value = DBNull.Value;
                if (F_Hoc3.HasValue)
                    if (F_Hoc3 == Constants.WorkdayEmployee_DefaultValue)
                        param[47].Value = DBNull.Value;
                    else
                        param[47].Value = F_Hoc3.Value;
                else
                    param[47].Value = DBNull.Value;
                if (F_Hoc4.HasValue)
                    if (F_Hoc4 == Constants.WorkdayEmployee_DefaultValue)
                        param[48].Value = DBNull.Value;
                    else
                        param[48].Value = F_Hoc4.Value;
                else
                    param[48].Value = DBNull.Value;
                if (F_Hoc5.HasValue)
                    if (F_Hoc5 == Constants.WorkdayEmployee_DefaultValue)
                        param[49].Value = DBNull.Value;
                    else
                        param[49].Value = F_Hoc5.Value;
                else
                    param[49].Value = DBNull.Value;
                if (F_Hoc6.HasValue)
                    if (F_Hoc6 == Constants.WorkdayEmployee_DefaultValue)
                        param[50].Value = DBNull.Value;
                    else
                        param[50].Value = F_Hoc6.Value;
                else
                    param[50].Value = DBNull.Value;
                if (F_Hoc7.HasValue)
                    if (F_Hoc7 == Constants.WorkdayEmployee_DefaultValue)
                        param[51].Value = DBNull.Value;
                    else
                        param[51].Value = F_Hoc7.Value;
                else
                    param[51].Value = DBNull.Value;
                if (F_Con_Om.HasValue)
                    if (F_Con_Om == Constants.WorkdayEmployee_DefaultValue)
                        param[52].Value = DBNull.Value;
                    else
                        param[52].Value = F_Con_Om.Value;
                else
                    param[52].Value = DBNull.Value;
                if (F_KHHDS.HasValue)
                    if (F_KHHDS == Constants.WorkdayEmployee_DefaultValue)
                        param[53].Value = DBNull.Value;
                    else
                        param[53].Value = F_KHHDS.Value;
                else
                    param[53].Value = DBNull.Value;
                if (F_SayThai.HasValue)
                    if (F_SayThai == Constants.WorkdayEmployee_DefaultValue)
                        param[54].Value = DBNull.Value;
                    else
                        param[54].Value = F_SayThai.Value;
                else
                    param[54].Value = DBNull.Value;
                if (F_KhamThai.HasValue)
                    if (F_KhamThai == Constants.WorkdayEmployee_DefaultValue)
                        param[55].Value = DBNull.Value;
                    else
                        param[55].Value = F_KhamThai.Value;
                else
                    param[55].Value = DBNull.Value;
                if (F_ConChet.HasValue)
                    if (F_ConChet == Constants.WorkdayEmployee_DefaultValue)
                        param[56].Value = DBNull.Value;
                    else
                        param[56].Value = F_ConChet.Value;
                else
                    param[56].Value = DBNull.Value;
                if (F_DinhChiCongTac.HasValue)
                    if (F_DinhChiCongTac == Constants.WorkdayEmployee_DefaultValue)
                        param[57].Value = DBNull.Value;
                    else
                        param[57].Value = F_DinhChiCongTac.Value;
                else
                    param[57].Value = DBNull.Value;
                if (F_TamHoanHD.HasValue)
                    if (F_TamHoanHD == Constants.WorkdayEmployee_DefaultValue)
                        param[58].Value = DBNull.Value;
                    else
                        param[58].Value = F_TamHoanHD.Value;
                else
                    param[58].Value = DBNull.Value;
                if (F_HoiHop.HasValue)
                    if (F_HoiHop == Constants.WorkdayEmployee_DefaultValue)
                        param[59].Value = DBNull.Value;
                    else
                        param[59].Value = F_HoiHop.Value;
                else
                    param[59].Value = DBNull.Value;
                if (F_Le.HasValue)
                    if (F_Le == Constants.WorkdayEmployee_DefaultValue)
                        param[60].Value = DBNull.Value;
                    else
                        param[60].Value = F_Le.Value;
                else
                    param[60].Value = DBNull.Value;
                if (NghiTuan.HasValue)
                    if (NghiTuan == Constants.WorkdayEmployee_DefaultValue)
                        param[61].Value = DBNull.Value;
                    else
                        param[61].Value = NghiTuan.Value;
                else
                    param[61].Value = DBNull.Value;
                if (NghiBu.HasValue)
                    if (NghiBu == Constants.WorkdayEmployee_DefaultValue)
                        param[62].Value = DBNull.Value;
                    else
                        param[62].Value = NghiBu.Value;
                else
                    param[62].Value = DBNull.Value;
                if (NghiViec.HasValue)
                    if (NghiViec == Constants.WorkdayEmployee_DefaultValue)
                        param[63].Value = DBNull.Value;
                    else
                        param[63].Value = NghiViec.Value;
                else
                    param[63].Value = DBNull.Value;
                if (NghiMat.HasValue)
                    if (NghiViec == Constants.WorkdayEmployee_DefaultValue)
                        param[64].Value = DBNull.Value;
                    else
                        param[64].Value = NghiMat.Value;
                else
                    param[64].Value = DBNull.Value;
                if (CongDu.HasValue)
                    param[65].Value = CongDu.Value;
                else
                    param[65].Value = DBNull.Value;

                if (GC_LamDem.HasValue)
                    if (GC_LamDem == Constants.WorkdayEmployee_DefaultValue)
                        param[66].Value = DBNull.Value;
                    else
                        param[66].Value = GC_LamDem.Value;
                else
                    param[66].Value = DBNull.Value;
                if (Mark.HasValue)
                    if (Mark == Constants.WorkdayEmployee_DefaultValue)
                        param[67].Value = DBNull.Value;
                    else
                        param[67].Value = Mark.Value;
                else
                    param[67].Value = DBNull.Value;
                if (HTCV == null)
                    param[68].Value = DBNull.Value;
                else
                    param[68].Value = HTCV;

                #endregion

                if (LastUpdate.HasValue)
                    param[69].Value = LastUpdate.Value;
                else
                    param[69].Value = DBNull.Value;

                if (UpdateUser.HasValue)
                    param[70].Value = UpdateUser.Value;
                else
                    param[70].Value = DBNull.Value;
                if (Status.HasValue)
                    param[71].Value = Status.Value;
                else
                    param[71].Value = DBNull.Value;

                #region night

                if (Night1.HasValue)
                    if (Night1 == Constants.WorkdayEmployee_DefaultValue)
                        param[72].Value = DBNull.Value;
                    else
                        param[72].Value = Night1.Value;
                else
                    param[72].Value = DBNull.Value;
                if (Night2.HasValue)
                    if (Night2 == Constants.WorkdayEmployee_DefaultValue)
                        param[73].Value = DBNull.Value;
                    else
                        param[73].Value = Night2.Value;
                else
                    param[73].Value = DBNull.Value;
                if (Night3.HasValue)
                    if (Night3 == Constants.WorkdayEmployee_DefaultValue)
                        param[74].Value = DBNull.Value;
                    else
                        param[74].Value = Night3.Value;
                else
                    param[74].Value = DBNull.Value;
                if (Night4.HasValue)
                    if (Night4 == Constants.WorkdayEmployee_DefaultValue)
                        param[75].Value = DBNull.Value;
                    else
                        param[75].Value = Night4.Value;
                else
                    param[75].Value = DBNull.Value;
                if (Night5.HasValue)
                    if (Night5 == Constants.WorkdayEmployee_DefaultValue)
                        param[76].Value = DBNull.Value;
                    else
                        param[76].Value = Night5.Value;
                else
                    param[76].Value = DBNull.Value;
                if (Night6.HasValue)
                    if (Night6 == Constants.WorkdayEmployee_DefaultValue)
                        param[77].Value = DBNull.Value;
                    else
                        param[77].Value = Night6.Value;
                else
                    param[77].Value = DBNull.Value;
                if (Night7.HasValue)
                    if (Night7 == Constants.WorkdayEmployee_DefaultValue)
                        param[78].Value = DBNull.Value;
                    else
                        param[78].Value = Night7.Value;
                else
                    param[78].Value = DBNull.Value;
                if (Night8.HasValue)
                    if (Night8 == Constants.WorkdayEmployee_DefaultValue)
                        param[79].Value = DBNull.Value;
                    else
                        param[79].Value = Night8.Value;
                else
                    param[79].Value = DBNull.Value;
                if (Night9.HasValue)
                    if (Night9 == Constants.WorkdayEmployee_DefaultValue)
                        param[80].Value = DBNull.Value;
                    else
                        param[80].Value = Night9.Value;
                else
                    param[80].Value = DBNull.Value;
                if (Night10.HasValue)
                    if (Night10 == Constants.WorkdayEmployee_DefaultValue)
                        param[81].Value = DBNull.Value;
                    else
                        param[81].Value = Night10.Value;
                else
                    param[81].Value = DBNull.Value;
                if (Night11.HasValue)
                    if (Night11 == Constants.WorkdayEmployee_DefaultValue)
                        param[82].Value = DBNull.Value;
                    else
                        param[82].Value = Night11.Value;
                else
                    param[82].Value = DBNull.Value;
                if (Night12.HasValue)
                    if (Night12 == Constants.WorkdayEmployee_DefaultValue)
                        param[83].Value = DBNull.Value;
                    else
                        param[83].Value = Night12.Value;
                else
                    param[83].Value = DBNull.Value;
                if (Night13.HasValue)
                    if (Night13 == Constants.WorkdayEmployee_DefaultValue)
                        param[84].Value = DBNull.Value;
                    else
                        param[84].Value = Night13.Value;
                else
                    param[84].Value = DBNull.Value;
                if (Night14.HasValue)
                    if (Night14 == Constants.WorkdayEmployee_DefaultValue)
                        param[85].Value = DBNull.Value;
                    else
                        param[85].Value = Night14.Value;
                else
                    param[85].Value = DBNull.Value;
                if (Night15.HasValue)
                    if (Night15 == Constants.WorkdayEmployee_DefaultValue)
                        param[86].Value = DBNull.Value;
                    else
                        param[86].Value = Night15.Value;
                else
                    param[86].Value = DBNull.Value;
                if (Night16.HasValue)
                    if (Night16 == Constants.WorkdayEmployee_DefaultValue)
                        param[87].Value = DBNull.Value;
                    else
                        param[87].Value = Night16.Value;
                else
                    param[87].Value = DBNull.Value;
                if (Night17.HasValue)
                    if (Night17 == Constants.WorkdayEmployee_DefaultValue)
                        param[88].Value = DBNull.Value;
                    else
                        param[88].Value = Night17.Value;
                else
                    param[88].Value = DBNull.Value;
                if (Night18.HasValue)
                    if (Night18 == Constants.WorkdayEmployee_DefaultValue)
                        param[89].Value = DBNull.Value;
                    else
                        param[89].Value = Night18.Value;
                else
                    param[89].Value = DBNull.Value;
                if (Night19.HasValue)
                    if (Night19 == Constants.WorkdayEmployee_DefaultValue)
                        param[90].Value = DBNull.Value;
                    else
                        param[90].Value = Night19.Value;
                else
                    param[90].Value = DBNull.Value;
                if (Night20.HasValue)
                    if (Night20 == Constants.WorkdayEmployee_DefaultValue)
                        param[91].Value = DBNull.Value;
                    else
                        param[91].Value = Night20.Value;
                else
                    param[91].Value = DBNull.Value;
                if (Night21.HasValue)
                    if (Night21 == Constants.WorkdayEmployee_DefaultValue)
                        param[92].Value = DBNull.Value;
                    else
                        param[92].Value = Night21.Value;
                else
                    param[92].Value = DBNull.Value;
                if (Night22.HasValue)
                    if (Night22 == Constants.WorkdayEmployee_DefaultValue)
                        param[93].Value = DBNull.Value;
                    else
                        param[93].Value = Night22.Value;
                else
                    param[93].Value = DBNull.Value;
                if (Night23.HasValue)
                    if (Night23 == Constants.WorkdayEmployee_DefaultValue)
                        param[94].Value = DBNull.Value;
                    else
                        param[94].Value = Night23.Value;
                else
                    param[94].Value = DBNull.Value;
                if (Night24.HasValue)
                    if (Night24 == Constants.WorkdayEmployee_DefaultValue)
                        param[95].Value = DBNull.Value;
                    else
                        param[95].Value = Night24.Value;
                else
                    param[95].Value = DBNull.Value;
                if (Night25.HasValue)
                    if (Night25 == Constants.WorkdayEmployee_DefaultValue)
                        param[96].Value = DBNull.Value;
                    else
                        param[96].Value = Night25.Value;
                else
                    param[96].Value = DBNull.Value;
                if (Night26.HasValue)
                    if (Night26 == Constants.WorkdayEmployee_DefaultValue)
                        param[97].Value = DBNull.Value;
                    else
                        param[97].Value = Night26.Value;
                else
                    param[97].Value = DBNull.Value;
                if (Night27.HasValue)
                    if (Night27 == Constants.WorkdayEmployee_DefaultValue)
                        param[98].Value = DBNull.Value;
                    else
                        param[98].Value = Night27.Value;
                else
                    param[98].Value = DBNull.Value;
                if (Night28.HasValue)
                    if (Night28 == Constants.WorkdayEmployee_DefaultValue)
                        param[99].Value = DBNull.Value;
                    else
                        param[99].Value = Night28.Value;
                else
                    param[99].Value = DBNull.Value;
                if (Night29.HasValue)
                    if (Night29 == Constants.WorkdayEmployee_DefaultValue)
                        param[100].Value = DBNull.Value;
                    else
                        param[100].Value = Night29.Value;
                else
                    param[100].Value = DBNull.Value;
                if (Night30.HasValue)
                    if (Night30 == Constants.WorkdayEmployee_DefaultValue)
                        param[101].Value = DBNull.Value;
                    else
                        param[101].Value = Night30.Value;
                else
                    param[101].Value = DBNull.Value;
                if (Night31.HasValue)
                    if (Night31 == Constants.WorkdayEmployee_DefaultValue)
                        param[102].Value = DBNull.Value;
                    else
                        param[102].Value = Night31.Value;
                else
                    param[102].Value = DBNull.Value;

                #endregion

                if (remark == null)
                    param[103].Value = DBNull.Value;
                else
                    param[103].Value = remark;

                sproc = new StoreProcedure(WorkdayEmployeeKeys.Sp_Upd_H1_WorkdayEmployee_By_UserId_Date, param);
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

        public int Update_By_Sends(string DeptIds, int? Month, int? Year, string ReadUserIds, string WriteUserIds,
            int? Status)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DeptIds", SqlDbType.VarChar, 254),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int),
                    new SqlParameter("@ReadUserIds", SqlDbType.VarChar, 1000),
                    new SqlParameter("@WriteUserIds", SqlDbType.VarChar, 1000),
                    new SqlParameter("@Status", SqlDbType.Int)
                };
                if (DeptIds == null)
                    param[0].Value = DBNull.Value;
                else
                    param[0].Value = DeptIds;

                if (Month.HasValue)
                    param[1].Value = Month.Value;
                else
                    param[1].Value = DBNull.Value;
                if (Year.HasValue)
                    param[2].Value = Year.Value;
                else
                    param[2].Value = DBNull.Value;
                if (ReadUserIds == null)
                    param[3].Value = DBNull.Value;
                else
                    param[3].Value = ReadUserIds;
                if (WriteUserIds == null)
                    param[4].Value = DBNull.Value;
                else
                    param[4].Value = WriteUserIds;
                if (Status.HasValue)
                    param[5].Value = Status.Value;
                else
                    param[5].Value = DBNull.Value;


                sproc = new StoreProcedure(WorkdayEmployeeKeys.Sp_Upd_H1_WorkdayEmployee_By_Sends, param);
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

        public int Update_By_MarkHTCV(int? UserId, double? Mark, DateTime? LastUpdate, int? UpdateUser, int? Month,
            int? Year)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@Mark", SqlDbType.Float),
                    new SqlParameter("@LastUpdate", SqlDbType.DateTime),
                    new SqlParameter("@UpdateUser", SqlDbType.Int),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int)
                };
                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (Mark.HasValue)
                    param[1].Value = Mark.Value;
                else
                    param[1].Value = DBNull.Value;
                if (LastUpdate.HasValue)
                    param[2].Value = LastUpdate.Value;
                else
                    param[2].Value = DBNull.Value;
                if (UpdateUser.HasValue)
                    param[3].Value = UpdateUser.Value;
                else
                    param[3].Value = DBNull.Value;
                if (Month.HasValue)
                    param[4].Value = Month.Value;
                else
                    param[4].Value = DBNull.Value;
                if (Year.HasValue)
                    param[5].Value = Year.Value;
                else
                    param[5].Value = DBNull.Value;


                sproc = new StoreProcedure(WorkdayEmployeeKeys.Sp_Upd_H1_WorkdayEmployee_By_MarkHTCV, param);
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

        public int Update_By_Send_UserId(string ReadUserIds, string WriteUserIds, int? Month, int? Year, int? UserId)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@ReadUserIds", SqlDbType.VarChar, 1000),
                    new SqlParameter("@WriteUserIds", SqlDbType.VarChar, 1000),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                if (ReadUserIds == null)
                    param[0].Value = DBNull.Value;
                else
                    param[0].Value = ReadUserIds;
                if (WriteUserIds == null)
                    param[1].Value = DBNull.Value;
                else
                    param[1].Value = WriteUserIds;
                if (Month.HasValue)
                    param[2].Value = Month.Value;
                else
                    param[2].Value = DBNull.Value;
                if (Year.HasValue)
                    param[3].Value = Year.Value;
                else
                    param[3].Value = DBNull.Value;

                if (UserId.HasValue)
                    param[4].Value = UserId.Value;
                else
                    param[4].Value = DBNull.Value;


                sproc = new StoreProcedure(WorkdayEmployeeKeys.Sp_Upd_H1_WorkdayEmployee_By_Send_UserId, param);
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

        public int Update_By_Send_Read(string ReadUserIds, int? Month, int? Year, int? UserId)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@ReadUserIds", SqlDbType.VarChar, 1000),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                if (ReadUserIds == null)
                    param[0].Value = DBNull.Value;
                else
                    param[0].Value = ReadUserIds;

                if (Month.HasValue)
                    param[1].Value = Month.Value;
                else
                    param[1].Value = DBNull.Value;
                if (Year.HasValue)
                    param[2].Value = Year.Value;
                else
                    param[2].Value = DBNull.Value;

                if (UserId.HasValue)
                    param[3].Value = UserId.Value;
                else
                    param[3].Value = DBNull.Value;


                sproc = new StoreProcedure(WorkdayEmployeeKeys.Sp_Upd_H1_WorkdayEmployee_By_SendRead, param);
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

        public int Update_By_Send_Write(string WriteUserIds, int? Month, int? Year, int? UserId)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@WriteUserIds", SqlDbType.VarChar, 1000),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                if (WriteUserIds == null)
                    param[0].Value = DBNull.Value;
                else
                    param[0].Value = WriteUserIds;
                if (Month.HasValue)
                    param[1].Value = Month.Value;
                else
                    param[1].Value = DBNull.Value;
                if (Year.HasValue)
                    param[2].Value = Year.Value;
                else
                    param[2].Value = DBNull.Value;

                if (UserId.HasValue)
                    param[3].Value = UserId.Value;
                else
                    param[3].Value = DBNull.Value;


                sproc = new StoreProcedure(WorkdayEmployeeKeys.Sp_Upd_H1_WorkdayEmployee_By_SendWrite, param);
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

        public int Update_By_Sends_Read(string ReadUserIds, int? Month, int? Year, string DeptIds)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@ReadUserIds", SqlDbType.VarChar, 1000),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int),
                    new SqlParameter("@DeptIds", SqlDbType.VarChar, 254)
                };

                if (ReadUserIds == null)
                    param[0].Value = DBNull.Value;
                else
                    param[0].Value = ReadUserIds;

                if (Month.HasValue)
                    param[1].Value = Month.Value;
                else
                    param[1].Value = DBNull.Value;
                if (Year.HasValue)
                    param[2].Value = Year.Value;
                else
                    param[2].Value = DBNull.Value;

                param[3].Value = DeptIds;


                sproc = new StoreProcedure(WorkdayEmployeeKeys.Sp_Upd_H1_WorkdayEmployee_By_Sends_Read, param);
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

        public int Update_By_Sends_Write(string WriteUserIds, int? Month, int? Year, string DeptIds)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@WriteUserIds", SqlDbType.VarChar, 1000),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int),
                    new SqlParameter("@DeptIds", SqlDbType.VarChar, 254)
                };

                if (WriteUserIds == null)
                    param[0].Value = DBNull.Value;
                else
                    param[0].Value = WriteUserIds;
                if (Month.HasValue)
                    param[1].Value = Month.Value;
                else
                    param[1].Value = DBNull.Value;
                if (Year.HasValue)
                    param[2].Value = Year.Value;
                else
                    param[2].Value = DBNull.Value;


                param[3].Value = DeptIds;


                sproc = new StoreProcedure(WorkdayEmployeeKeys.Sp_Upd_H1_WorkdayEmployee_By_Sends_Write, param);
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

        public int Update_By_Sharing_Read(string ReadUserIds, int? Month, int? Year, int? UserId)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@ReadUserIds", SqlDbType.VarChar, 1000),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                if (ReadUserIds == null)
                    param[0].Value = DBNull.Value;
                else
                    param[0].Value = ReadUserIds;

                if (Month.HasValue)
                    param[1].Value = Month.Value;
                else
                    param[1].Value = DBNull.Value;
                if (Year.HasValue)
                    param[2].Value = Year.Value;
                else
                    param[2].Value = DBNull.Value;

                if (UserId.HasValue)
                    param[3].Value = UserId.Value;
                else
                    param[3].Value = DBNull.Value;


                sproc = new StoreProcedure(WorkdayEmployeeKeys.Sp_Upd_H1_WorkdayEmployee_By_SharingRead, param);
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

        public int Update_By_Sharing_Write(string WriteUserIds, int? Month, int? Year, int? UserId)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@WriteUserIds", SqlDbType.VarChar, 1000),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int),
                    new SqlParameter("@UserId", SqlDbType.Int)
                };

                if (WriteUserIds == null)
                    param[0].Value = DBNull.Value;
                else
                    param[0].Value = WriteUserIds;
                if (Month.HasValue)
                    param[1].Value = Month.Value;
                else
                    param[1].Value = DBNull.Value;
                if (Year.HasValue)
                    param[2].Value = Year.Value;
                else
                    param[2].Value = DBNull.Value;

                if (UserId.HasValue)
                    param[3].Value = UserId.Value;
                else
                    param[3].Value = DBNull.Value;


                sproc = new StoreProcedure(WorkdayEmployeeKeys.Sp_Upd_H1_WorkdayEmployee_By_SharingWrite, param);
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

        public int Update_By_Sharings_Read(string ReadUserIds, int? Month, int? Year, string DeptIds)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@ReadUserIds", SqlDbType.VarChar, 1000),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int),
                    new SqlParameter("@DeptIds", SqlDbType.VarChar, 254)
                };

                if (ReadUserIds == null)
                    param[0].Value = DBNull.Value;
                else
                    param[0].Value = ReadUserIds;

                if (Month.HasValue)
                    param[1].Value = Month.Value;
                else
                    param[1].Value = DBNull.Value;
                if (Year.HasValue)
                    param[2].Value = Year.Value;
                else
                    param[2].Value = DBNull.Value;

                param[3].Value = DeptIds;


                sproc = new StoreProcedure(WorkdayEmployeeKeys.Sp_Upd_H1_WorkdayEmployee_By_Sharings_Read, param);
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

        public int Update_By_Sharings_Write(string WriteUserIds, int? Month, int? Year, string DeptIds)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@WriteUserIds", SqlDbType.VarChar, 1000),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int),
                    new SqlParameter("@DeptIds", SqlDbType.VarChar, 254)
                };

                if (WriteUserIds == null)
                    param[0].Value = DBNull.Value;
                else
                    param[0].Value = WriteUserIds;
                if (Month.HasValue)
                    param[1].Value = Month.Value;
                else
                    param[1].Value = DBNull.Value;
                if (Year.HasValue)
                    param[2].Value = Year.Value;
                else
                    param[2].Value = DBNull.Value;


                param[3].Value = DeptIds;


                sproc = new StoreProcedure(WorkdayEmployeeKeys.Sp_Upd_H1_WorkdayEmployee_By_Sharings_Write, param);
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

        public int Update_ForCongDu(int? UserId, double? CongDu, int? Month, int? Year)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@CongDu", SqlDbType.Float),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int)
                };
                if (UserId.HasValue)
                    param[0].Value = UserId.Value;
                else
                    param[0].Value = DBNull.Value;
                if (CongDu.HasValue)
                    param[1].Value = CongDu.Value;
                else
                    param[1].Value = DBNull.Value;
                if (Month.HasValue)
                    param[2].Value = Month.Value;
                else
                    param[2].Value = DBNull.Value;
                if (Year.HasValue)
                    param[3].Value = Year.Value;
                else
                    param[3].Value = DBNull.Value;

                sproc = new StoreProcedure(WorkdayEmployeeKeys.Sp_Upd_H1_WorkdayEmployee_For_CongDu, param);
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

                sproc = new StoreProcedure(WorkdayEmployeeKeys.Sp_Del_H1_WorkdayEmployee_By_DeptIds_Date, param);

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

        public int Delete(long WorkdayEmployeeId)
        {
            var identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@WorkdayEmployeeId", SqlDbType.BigInt)
                };

                param[0].Value = WorkdayEmployeeId;

                sproc = new StoreProcedure(WorkdayEmployeeKeys.Sp_Del_H1_WorkdayEmployee, param);

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

        public DataTable GetAllDT(string fullName, string departmentIds, int month, int year)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@DepartmentId", SqlDbType.VarChar, 254),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int)
                };

                param[0].Value = fullName;
                param[1].Value = departmentIds;
                param[2].Value = month;
                param[3].Value = year;

                sproc = new StoreProcedure(WorkdayEmployeeKeys.Sp_Sel_H1_WorkdayEmployee_By_AllV1, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetAll(string fullName, string departmentIds, int month, int year)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@DepartmentId", SqlDbType.VarChar, 254),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int)
                };

                param[0].Value = fullName;
                param[1].Value = departmentIds;
                param[2].Value = month;
                param[3].Value = year;

                sproc = new StoreProcedure(WorkdayEmployeeKeys.Sp_Sel_H1_WorkdayEmployee_By_All, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetAllByFilter(string fullName, string departmentIds, int month, int year, int status,
            int receivedUserId, int TypeSort)
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
                    new SqlParameter("@Status", SqlDbType.Int),
                    new SqlParameter("@ReceivedUserId", SqlDbType.Int),
                    new SqlParameter("@TypeSort", SqlDbType.Int)
                };

                param[0].Value = fullName;
                param[1].Value = departmentIds;
                param[2].Value = month;
                param[3].Value = year;
                param[4].Value = status;
                param[5].Value = receivedUserId;
                param[6].Value = TypeSort;

                sproc = new StoreProcedure(WorkdayEmployeeKeys.Sp_Sel_H1_WorkdayEmployee_By_Filter, param);
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

                sproc = new StoreProcedure(WorkdayEmployeeKeys.Sp_Sel_H1_WorkdayEmployee_By_Id, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByUserId_Month_Year(int userId, int month, int year, int status)
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
                    new SqlParameter("@Status", SqlDbType.Int)
                };

                param[0].Value = userId;
                param[1].Value = month;
                param[2].Value = year;
                param[3].Value = status;

                sproc = new StoreProcedure(WorkdayEmployeeKeys.Sp_Sel_H1_WorkdayEmployee_By_UserId_Month_Year, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByUserId_Month_Year1(int userId, int month, int year)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int)
                };

                param[0].Value = userId;
                param[1].Value = month;
                param[2].Value = year;

                sproc = new StoreProcedure(WorkdayEmployeeKeys.Sp_Sel_H1_WorkdayEmployee_By_UserId_Month_Year1, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByStatistic1(string deptIds, int month, int year, int debit, string hTCV, int workdayType)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DeptIds", SqlDbType.VarChar, 254),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int),
                    new SqlParameter("@Debit", SqlDbType.Int),
                    new SqlParameter("@HTCV", SqlDbType.VarChar, 10),
                    new SqlParameter("@WorkdayType", SqlDbType.Int)
                };

                param[0].Value = deptIds;
                param[1].Value = month;
                param[2].Value = year;
                param[3].Value = debit;
                if (hTCV.Trim().Length == 0)
                    param[4].Value = DBNull.Value;
                else
                    param[4].Value = hTCV;
                param[5].Value = workdayType;

                sproc = new StoreProcedure(WorkdayEmployeeKeys.Sp_Sel_H1_WorkdayEmployee_By_Statistic1, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByStatisticLeave(string fullName, string deptIds, string leaveCode, int month, int year)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@DeptIds", SqlDbType.VarChar, 254),
                    new SqlParameter("@LeaveCode", SqlDbType.VarChar, 50),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int)
                };

                param[0].Value = fullName;
                param[1].Value = deptIds;
                if (leaveCode.Trim().Length == 0)
                    param[2].Value = DBNull.Value;
                else
                    param[2].Value = leaveCode;
                param[3].Value = month;
                param[4].Value = year;


                sproc = new StoreProcedure(WorkdayEmployeeKeys.Sp_Sel_H1_WorkdayEmployee_By_StatisticLeave, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetAllByFilterHTCV(string fullName, string departmentIds, int minMark, int maxMark, int month,
            int year)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 100),
                    new SqlParameter("@DeptIds", SqlDbType.VarChar, 254),
                    new SqlParameter("@MinMark", SqlDbType.Float),
                    new SqlParameter("@MaxMark", SqlDbType.Float),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int)
                };

                param[0].Value = fullName;
                param[1].Value = departmentIds;
                param[2].Value = minMark;
                param[3].Value = maxMark;
                param[4].Value = month;
                param[5].Value = year;


                sproc = new StoreProcedure(WorkdayEmployeeKeys.Sp_Sel_H1_WorkdayEmployee_By_FilterHTCV, param);
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

                sproc = new StoreProcedure(WorkdayEmployeeKeys.Sp_Sel_H1_WorkdayEmployee_By_RootId, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable CheckSend(int ReceivedUserId, int RootId, int Month, int Year)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@ReceivedUserId", SqlDbType.Int),
                    new SqlParameter("@RootId", SqlDbType.Int),
                    new SqlParameter("@Month", SqlDbType.Int),
                    new SqlParameter("@Year", SqlDbType.Int)
                };

                param[0].Value = ReceivedUserId;
                param[1].Value = RootId;
                param[2].Value = Month;
                param[3].Value = Year;

                sproc = new StoreProcedure(WorkdayEmployeeKeys.Sp_Sel_H1_WorkdayEmployee_For_CheckSendToHRMAdmin, param);
                sproc.RunFill(datatable);
                sproc.Dispose();
            }
            catch (SqlException se)
            {
                throw new HRMException(se.Message, se.Number);
            }

            return datatable;
        }

        public DataTable GetByDeptIds(string deptIds, int rootId, string fullname, int TypeSort)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@DeptIds", SqlDbType.VarChar, 254),
                    new SqlParameter("@RootId", SqlDbType.Int),
                    new SqlParameter("@FullName", SqlDbType.NVarChar, 254),
                    new SqlParameter("@TypeSort", SqlDbType.Int)
                };

                param[0].Value = deptIds;
                param[1].Value = rootId;
                param[2].Value = fullname;
                param[3].Value = TypeSort;

                sproc = new StoreProcedure(WorkdayEmployeeKeys.Sp_Sel_H1_WorkingEmployee_By_DeptIds, param);
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