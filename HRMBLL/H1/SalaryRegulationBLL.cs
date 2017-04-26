using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H1;
using HRMUtil;
using HRMUtil.KeyNames.H1;

namespace HRMBLL.H1
{
    public class SalaryRegulationBLL
    {
        #region private fields

        #endregion

        #region properties

        public int SalaryRegulationId { get; set; }

        public string SalaryRegulationName { get; set; }

        public DateTime BeginingDate { get; set; }

        public string Description { get; set; }

        public bool InUse { get; set; }

        public int TypeId { get; set; }

        public string TypeName { get; set; }

        #endregion

        #region public methods insert, update, delete

        public int Save()
        {
            var objDAL = new SalaryRegulationDAL();

            if (SalaryRegulationId <= 0)
                return objDAL.Insert(SalaryRegulationName, BeginingDate, Description, InUse, TypeId);
            return objDAL.Update(SalaryRegulationName, BeginingDate, Description, InUse, TypeId, SalaryRegulationId);
        }

        public static void Update(string salaryRegulationName, DateTime beginingDate, string description, bool inUse,
            int typeId, int salaryRegulationId)
        {
            new SalaryRegulationDAL().Update(salaryRegulationName, beginingDate, description, inUse, typeId,
                salaryRegulationId);
        }

        public static void Delete(int salaryRegulationId)
        {
            new SalaryRegulationDAL().Delete(salaryRegulationId);
        }

        #endregion

        #region public methods GET

        /// <summary>
        ///     Author: Giang
        ///     Date: 12/22/2014
        ///     Content: Get salary regulation in use to DT
        /// </summary>
        /// <param name="inUse"></param>
        /// <returns></returns>
        public static DataTable GetByInUseToDT(bool inUse)
        {
            return new SalaryRegulationDAL().GetByInUse(inUse);
        }

        public static DataTable GetByFilterToDT(int typeId)
        {
            return new SalaryRegulationDAL().GetByFilterV1(typeId);
        }

        public static List<SalaryRegulationBLL> GetByInUse(bool inUse)
        {
            return GenerateListSalaryRegulationBLLFromDataTable(new SalaryRegulationDAL().GetByInUse(inUse));
        }

        public static List<SalaryRegulationBLL> GetByFilter(int typeId)
        {
            return GenerateListSalaryRegulationBLLFromDataTable(new SalaryRegulationDAL().GetByFilter(typeId));
        }

        #endregion

        #region private methods

        private static List<SalaryRegulationBLL> GenerateListSalaryRegulationBLLFromDataTable(DataTable dt)
        {
            var list = new List<SalaryRegulationBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateSalaryRegulationBLLFromDataRow(dr));

            return list;
        }

        private static SalaryRegulationBLL GenerateSalaryRegulationBLLFromDataRow(DataRow dr)
        {
            var objBLL = new SalaryRegulationBLL();

            objBLL.SalaryRegulationId = dr[SalaryRegulationKeys.Field_SalaryRegulation_SalaryRegulationId] ==
                                        DBNull.Value
                ? 0
                : int.Parse(dr[SalaryRegulationKeys.Field_SalaryRegulation_SalaryRegulationId].ToString());
            objBLL.SalaryRegulationName = dr[SalaryRegulationKeys.Field_SalaryRegulation_SalaryRegulationName] ==
                                          DBNull.Value
                ? string.Empty
                : dr[SalaryRegulationKeys.Field_SalaryRegulation_SalaryRegulationName].ToString();
            objBLL.BeginingDate = dr[SalaryRegulationKeys.Field_SalaryRegulation_BeginingDate] == DBNull.Value
                ? FormatDate.GetSQLDateMinValue
                : Convert.ToDateTime(dr[SalaryRegulationKeys.Field_SalaryRegulation_BeginingDate].ToString());
            objBLL.Description = dr[SalaryRegulationKeys.Field_SalaryRegulation_Description] == DBNull.Value
                ? ""
                : dr[SalaryRegulationKeys.Field_SalaryRegulation_Description].ToString();
            objBLL.InUse = dr[SalaryRegulationKeys.Field_SalaryRegulation_InUse] == DBNull.Value
                ? false
                : Convert.ToBoolean(dr[SalaryRegulationKeys.Field_SalaryRegulation_InUse].ToString());
            objBLL.TypeId = dr[SalaryRegulationKeys.Field_SalaryRegulation_TypeId] == DBNull.Value
                ? 0
                : int.Parse(dr[SalaryRegulationKeys.Field_SalaryRegulation_TypeId].ToString());
            objBLL.TypeName = Constants.GetSalaryRegulationTypeName(objBLL.TypeId);
            return objBLL;
        }

        #endregion
    }
}