using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using HRMDAL.Utilities;
using HRMUtil;
using HRMUtil.KeyNames.H1;

namespace HRMDAL.H1
{
    public class BonusEmployeeConditionDAL : Dao
    {
        public long Update(int MonthNumber, int MonthNumber_tv, decimal HSLNS, decimal HSK, decimal NC_X,
            decimal NC_OmCoKHH, decimal NC_ThaiSan, decimal NC_TNLD,
            decimal NC_FDiDuong, decimal NC_Khac, decimal NgayCongThuong, decimal HSLNSThuong, decimal BonusValue,
            decimal ThueTamTinh, decimal ThueQuyetToan, decimal ThucLinh, string BonusTitleDetail, int UserId,
            int BonusTitleId, int BonusYear, DateTime PayDate)
        {
            long identity = 0;
            Debug.Assert(sproc == null);
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@MonthNumber", SqlDbType.Int),
                    new SqlParameter("@MonthNumber_tv", SqlDbType.Int),
                    new SqlParameter("@HSLNS", SqlDbType.Float),
                    new SqlParameter("@HSK", SqlDbType.Float),
                    new SqlParameter("@NC_X", SqlDbType.Float),
                    new SqlParameter("@NC_OmCoKHH", SqlDbType.Float),
                    new SqlParameter("@NC_ThaiSan", SqlDbType.Float),
                    new SqlParameter("@NC_TNLD", SqlDbType.Float),
                    new SqlParameter("@NC_FDiDuong", SqlDbType.Float),
                    new SqlParameter("@NC_Khac", SqlDbType.Float),
                    new SqlParameter("@NgayCongThuong", SqlDbType.Float),
                    new SqlParameter("@HSLNSThuong", SqlDbType.Float),
                    new SqlParameter("@BonusValue", SqlDbType.Float),
                    new SqlParameter("@ThueTamTinh", SqlDbType.Float),
                    new SqlParameter("@ThueQuyetToan", SqlDbType.Float),
                    new SqlParameter("@ThucLinh", SqlDbType.Float),
                    new SqlParameter("@BonusTitleDetail", SqlDbType.NVarChar, 254),
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@BonusTitleId", SqlDbType.Int),
                    new SqlParameter("@BonusYear", SqlDbType.Int),
                    new SqlParameter("@PayDate", SqlDbType.DateTime)
                };

                param[0].Value = MonthNumber;
                param[1].Value = MonthNumber_tv;
                param[2].Value = HSLNS;
                param[3].Value = HSK;
                param[4].Value = NC_X;
                param[5].Value = NC_OmCoKHH;
                param[6].Value = NC_ThaiSan;
                param[7].Value = NC_TNLD;
                param[8].Value = NC_FDiDuong;
                param[9].Value = NC_Khac;

                param[10].Value = NgayCongThuong;
                param[11].Value = HSLNSThuong;
                param[12].Value = BonusValue;
                param[13].Value = ThueTamTinh;
                param[14].Value = ThueQuyetToan;
                param[15].Value = ThucLinh;

                param[16].Value = BonusTitleDetail;

                param[17].Value = UserId;
                param[18].Value = BonusTitleId;
                param[19].Value = BonusYear;
                param[20].Value = PayDate;


                sproc = new StoreProcedure(BonusEmployeeConditionKeys.Sp_Upd_H1_BonusEmployeeCondition, param);
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

        #region Get

        public DataTable GetByFilter(int bonusYear, DateTime payDate, int bonusTitleId, string departmentIds)
        {
            Debug.Assert(sproc == null);
            var datatable = new DataTable();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@BonusYear", SqlDbType.Int),
                    new SqlParameter("@PayDate", SqlDbType.DateTime),
                    new SqlParameter("@BonusTitleId", SqlDbType.Int),
                    new SqlParameter("@DepartmentIds", SqlDbType.NVarChar, 4000)
                };

                param[0].Value = bonusYear;
                param[1].Value = payDate;
                param[2].Value = bonusTitleId;
                param[3].Value = departmentIds;
                sproc = new StoreProcedure(BonusEmployeeConditionKeys.Sp_Sel_H1_BonusEmployeeCondition_By_Filter, param);
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