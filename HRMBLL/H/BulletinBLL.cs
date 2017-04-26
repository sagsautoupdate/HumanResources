using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H;
using HRMUtil;
using HRMUtil.KeyNames.H;

namespace HRMBLL.H
{
    public class BulletinBLL
    {
        #region methods Get

        public static List<BulletinBLL> GetByNumberday(int numberOfDay)
        {
            return GenerateListBulletinBLLFromDataTable(new BulletinDAL().GetByNumberday(numberOfDay));
        }

        #endregion

        #region private fields

        #endregion

        #region properties

        public int BulletinId { get; set; }

        public string BulletinTitle { get; set; }

        public string BulletinDetail { get; set; }

        public DateTime BulletinDate { get; set; }

        #endregion

        #region private static methods

        private static List<BulletinBLL> GenerateListBulletinBLLFromDataTable(DataTable dt)
        {
            var list = new List<BulletinBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateBulletinBLLFromDataRow(dr));

            return list;
        }

        private static BulletinBLL GenerateBulletinBLLFromDataRow(DataRow dr)
        {
            var objBLL = new BulletinBLL();

            objBLL.BulletinId = dr[BulletinKeyNames.Field_Bulletin_BulletinId] == DBNull.Value
                ? 0
                : int.Parse(dr[BulletinKeyNames.Field_Bulletin_BulletinId].ToString());
            objBLL.BulletinTitle = dr[BulletinKeyNames.Field_Bulletin_BulletinTitle] == DBNull.Value
                ? string.Empty
                : dr[BulletinKeyNames.Field_Bulletin_BulletinTitle].ToString();
            objBLL.BulletinDetail = dr[BulletinKeyNames.Field_Bulletin_BulletinDetail] == DBNull.Value
                ? string.Empty
                : dr[BulletinKeyNames.Field_Bulletin_BulletinDetail].ToString();
            objBLL.BulletinDate = dr[BulletinKeyNames.Field_Bulletin_BulletinDate] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr[BulletinKeyNames.Field_Bulletin_BulletinDate].ToString());

            return objBLL;
        }

        #endregion
    }
}