using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H3;
using HRMUtil;
using HRMUtil.KeyNames.H0;
using HRMUtil.KeyNames.H3;

namespace HRMBLL.H3
{
    public class DecisionsBLL
    {
        #region private filed

        #endregion

        #region properties

        public int DecisionId { get; set; }

        public int DecisionTypeId { get; set; }

        public string DecisionTypeName { get; set; }

        public string DecisionNo { get; set; }


        public DateTime DecisionDate { get; set; }


        public string DecisionName { get; set; }


        public string BringOutDept { get; set; }


        public string SignUser { get; set; }


        public string Remark { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime IneffectiveDate { get; set; }

        #endregion

        #region public method Get

        public static List<DecisionsBLL> GetByFilter(int decisionTypeId, string decisionNo, string decisionName,
            DateTime decisionDate)
        {
            return
                GenerateListDecisionsBLLFromDataTable(new DecisionsDAL().GetByFilter(decisionTypeId, decisionNo,
                    decisionName, decisionDate));
        }

        public static DecisionsBLL GetById(int decisionId)
        {
            var list = GenerateListDecisionsBLLFromDataTable(new DecisionsDAL().GetById(decisionId));
            if (list.Count == 1)
                return list[0];
            return null;
        }

        #endregion

        #region methods Insert, update, delete

        public int Save()
        {
            var objDAL = new DecisionsDAL();

            if (DecisionId <= 0)
                return objDAL.Insert(DecisionTypeId, DecisionNo, DecisionDate, DecisionName, BringOutDept, SignUser,
                    Remark, EffectiveDate, IneffectiveDate);
            return objDAL.Update(DecisionTypeId, DecisionNo, DecisionDate, DecisionName, BringOutDept, SignUser, Remark,
                DecisionId);
        }

        public static void Update(int decisionTypeId, string decisionNo, DateTime decisionDate, string decisionName,
            string bringOutDept, string signUser, string remark, int decisionId)
        {
            new DecisionsDAL().Update(decisionTypeId, decisionNo, decisionDate, decisionName, bringOutDept, signUser,
                remark, decisionId);
        }

        public static void Delete(int decisionId)
        {
            new DecisionsDAL().Delete(decisionId);
        }

        #endregion

        #region private methods

        private static List<DecisionsBLL> GenerateListDecisionsBLLFromDataTable(DataTable dt)
        {
            var list = new List<DecisionsBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateDecisionsBLLFromDataRow(dr));

            return list;
        }

        private static DecisionsBLL GenerateDecisionsBLLFromDataRow(DataRow dr)
        {
            var objBLL = new DecisionsBLL();

            objBLL.DecisionId = dr[DecisionsKeys.Field_Decisions_DecisionId] == DBNull.Value
                ? 0
                : int.Parse(dr[DecisionsKeys.Field_Decisions_DecisionId].ToString());
            objBLL.DecisionTypeId = dr[DecisionsKeys.Field_Decisions_DecisionTypeId] == DBNull.Value
                ? 0
                : int.Parse(dr[DecisionsKeys.Field_Decisions_DecisionTypeId].ToString());
            objBLL.DecisionNo = dr[DecisionsKeys.Field_Decisions_DecisionNo] == DBNull.Value
                ? string.Empty
                : dr[DecisionsKeys.Field_Decisions_DecisionNo].ToString();
            objBLL.DecisionDate = dr[DecisionsKeys.Field_Decisions_DecisionDate] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr[DecisionsKeys.Field_Decisions_DecisionDate].ToString());
            objBLL.DecisionName = dr[DecisionsKeys.Field_Decisions_DecisionName] == DBNull.Value
                ? string.Empty
                : dr[DecisionsKeys.Field_Decisions_DecisionName].ToString();
            objBLL.BringOutDept = dr[DecisionsKeys.Field_Decisions_BringOutDept] == DBNull.Value
                ? string.Empty
                : dr[DecisionsKeys.Field_Decisions_BringOutDept].ToString();
            objBLL.SignUser = dr[DecisionsKeys.Field_Decisions_SignUser] == DBNull.Value
                ? string.Empty
                : dr[DecisionsKeys.Field_Decisions_SignUser].ToString();
            objBLL.Remark = dr[DecisionsKeys.Field_Decisions_Remark] == DBNull.Value
                ? string.Empty
                : dr[DecisionsKeys.Field_Decisions_Remark].ToString();
            try
            {
                objBLL.DecisionTypeName = dr[ContractTypeKeys.Field_ContractTypes_ContractTypeName] == DBNull.Value
                    ? string.Empty
                    : dr[ContractTypeKeys.Field_ContractTypes_ContractTypeName].ToString();
            }
            catch
            {
            }
            return objBLL;
        }

        #endregion
    }
}