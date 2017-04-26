using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H;
using HRMUtil.KeyNames.H;

namespace HRMBLL.H
{
    public class BonusNamesBLL
    {
        #region constructors

        public BonusNamesBLL(int bonusNameId, string bonusName, string description, int type, bool visible)
        {
            BonusNameId = bonusNameId;
            BonusName = bonusName;
            Description = description;
            Type = type;
            Visible = visible;
        }

        #endregion

        #region public methods insert, update, delete

        public long Save()
        {
            var objDAL = new BonusNamesDAL();
            if (BonusNameId <= 0)
                return objDAL.Insert(BonusName, Description, Type);
            return -1;
        }

        #endregion

        #region private fields

        #endregion

        #region properties

        public int BonusNameId { get; set; }

        public string BonusName { get; set; }

        public string Description { get; set; }

        public int Type { get; set; }

        public bool Visible { get; set; }

        #endregion

        #region methods Get

        public static List<BonusNamesBLL> GetAll()
        {
            return GenerateListBonusNamesBLLFromDataTable(new BonusNamesDAL().GetAll());
        }

        public static List<BonusNamesBLL> GetByType(int type)
        {
            return GenerateListBonusNamesBLLFromDataTable(new BonusNamesDAL().GetByType(type));
        }

        public static List<BonusNamesBLL> GetByIds(string bonusNameIds)
        {
            return GenerateListBonusNamesBLLFromDataTable(new BonusNamesDAL().GetByIds(bonusNameIds));
        }

        #endregion

        #region private static methods

        private static List<BonusNamesBLL> GenerateListBonusNamesBLLFromDataTable(DataTable dt)
        {
            var list = new List<BonusNamesBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateBonusNamesBLLFromDataRow(dr));

            return list;
        }

        private static BonusNamesBLL GenerateBonusNamesBLLFromDataRow(DataRow dr)
        {
            var objBLL = new BonusNamesBLL(
                dr[BonusNameKeys.FIELD_BONUS_NAME_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[BonusNameKeys.FIELD_BONUS_NAME_ID].ToString()),
                dr[BonusNameKeys.FIELD_BONUS_NAME_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[BonusNameKeys.FIELD_BONUS_NAME_NAME].ToString(),
                dr[BonusNameKeys.FIELD_BONUS_NAME_DESCRIPTION] == DBNull.Value
                    ? string.Empty
                    : dr[BonusNameKeys.FIELD_BONUS_NAME_DESCRIPTION].ToString(),
                dr[BonusNameKeys.FIELD_BONUS_NAME_TYPE] == DBNull.Value
                    ? 0
                    : int.Parse(dr[BonusNameKeys.FIELD_BONUS_NAME_TYPE].ToString()),
                dr[BonusNameKeys.FIELD_BONUS_NAME_VISIBLE] == DBNull.Value
                    ? false
                    : Convert.ToBoolean(dr[BonusNameKeys.FIELD_BONUS_NAME_VISIBLE].ToString())
            );

            return objBLL;
        }

        #endregion
    }
}