using System;
using System.Collections.Generic;
using System.Data;
using HRMBLL.H1.Helper;
using HRMDAL.H1;
using HRMUtil.KeyNames.H1;

namespace HRMBLL.H1
{
    public class CoefficientNamesBLL
    {
        #region private fields

        #endregion

        #region properties

        public int CoefficientNameId { get; set; }

        public string CoefficientName { get; set; }

        public string CoefficientNameDescription { get; set; }

        public double ValueLevel1 { get; set; }

        public double Condition1 { get; set; }

        public double ValueLevel2 { get; set; }

        public double Condition2 { get; set; }

        public double ValueLevel3 { get; set; }

        public double Condition3 { get; set; }

        public double ValueLevel4 { get; set; }

        public double Condition4 { get; set; }

        public double ValueLevel5 { get; set; }

        public double Condition5 { get; set; }

        public double ValueLevel6 { get; set; }

        public double Condition6 { get; set; }

        public double ValueLevel7 { get; set; }

        public double Condition7 { get; set; }

        public double ValueLevel8 { get; set; }

        public double Condition8 { get; set; }

        public double ValueLevel9 { get; set; }

        public double Condition9 { get; set; }

        public double ValueLevel10 { get; set; }

        public double Condition10 { get; set; }

        public double ValueLevel11 { get; set; }

        public double Condition11 { get; set; }

        public double ValueLevel12 { get; set; }

        public double Condition12 { get; set; }

        #endregion

        #region Constructor

        public CoefficientNamesBLL()
        {
        }

        public CoefficientNamesBLL(int coefficientNameId, string coefficientName, string coefficientNameDescription)
        {
            CoefficientNameId = coefficientNameId;
            CoefficientName = coefficientName;
            CoefficientNameDescription = coefficientNameDescription;
        }

        #endregion

        #region public method Insert, Update, Delete

        //public int Save()
        //{
        //    CoefficientNamesDAL objLNS_CoefficientNamesDAL = new CoefficientNamesDAL();
        //    if (LNS_CoefficientNameId <= 0)
        //    {                
        //        return objLNS_CoefficientNamesDAL.Insert(LNS_CoefficientName, LNS_CoefficientNameDescription);    
        //    }
        //    else 
        //    {
        //        return objLNS_CoefficientNamesDAL.Update(LNS_CoefficientNameId, LNS_CoefficientName, LNS_CoefficientNameDescription);    
        //    }
        //}

        //public static int Update(int lNS_CoefficientNameId, string lNS_CoefficientName, string lNS_CoefficientNameDescription)
        //{
        //    CoefficientNamesDAL objLNS_CoefficientNamesDAL = new CoefficientNamesDAL();
        //    return objLNS_CoefficientNamesDAL.Update(lNS_CoefficientNameId, lNS_CoefficientName, lNS_CoefficientNameDescription);    
        //}

        #endregion

        #region public method Get

        public static List<CoefficientNamesBLL> GetByFilter(int type, string coefficientName, int SalaryRegulationId)
        {
            var objDAL = new CoefficientValuesDAL();
            if ((type == CoefficientNameKeys.CONST_LNS_COEFFICIENT_NAME_POSITION_TYPE) ||
                (type == CoefficientNameKeys.CONST_LNS_COEFFICIENT_NAME_FIXEDRATE_TYPE))
                return
                    GenerateListLNSCoefficientNamesBLLFromDataTable(objDAL.GetByFilter(type, coefficientName,
                        SalaryRegulationId));
            return
                GenerateListLCBCoefficientNamesBLLFromDataTable(objDAL.GetByFilter(type, coefficientName,
                    SalaryRegulationId));
        }

        /// <summary>
        ///     Author: Giang
        ///     Date: 11/27/2015
        ///     Content: Lay coefficient name tra ve DT
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DataTable GetAllToDT(int type, int regulationid)
        {
            var objDAL = new CoefficientValuesDAL();
            if (type == 0)
                return objDAL.GetAllToDT(type, regulationid);
            return objDAL.GetAllToDT(type, regulationid);
        }

        public static List<CoefficientNamesBLL> GetAll(int type)
        {
            var objDAL = new CoefficientValuesDAL();
            if (type == 0)
                return GenerateListLNSCoefficientNamesBLLFromDataTable(objDAL.GetAll(type));
            return GenerateListLCBCoefficientNamesBLLFromDataTable(objDAL.GetAll(type));
        }

        public static List<CoefficientNamesBLL> GetAllNames(int type)
        {
            var objDAL = new CoefficientNamesDAL();
            return GenerateListCoefficientNamesBLLFromDataTable_AllNames(objDAL.GetAllNames(type));
        }

        public static List<CoefficientNamesBLL> GetByInUseTypeId(bool inUse, int typeId, int type)
        {
            var objDAL = new CoefficientNamesDAL();
            return GenerateListCoefficientNamesBLLFromDataTable_AllNames(objDAL.GetByInUseTypeId(inUse, typeId, type));
        }

        #endregion

        #region private method

        private static List<CoefficientNamesBLL> GenerateListLNSCoefficientNamesBLLFromDataTable(DataTable dt)
        {
            var list = new List<CoefficientNamesBLL>();
            var flag = false;
            double v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0;
            var level = 0;

            for (var i = 0; i < dt.Rows.Count - 1; i++)
            {
                var idF = 0;
                var idA = 0;

                var drF = dt.Rows[i];
                idF = drF[CoefficientNameKeys.Field_CoefficientNames_CoefficientNameId] == DBNull.Value
                    ? DefaultValues.CoefficientNameIdMinValue
                    : int.Parse(drF[CoefficientNameKeys.Field_CoefficientNames_CoefficientNameId].ToString());
                level = drF[CoefficientLevelKeys.FIELD_COEFFICIENT_LEVEL_ID] == DBNull.Value
                    ? DefaultValues.CoefficientLevelIdMinValue
                    : int.Parse(drF[CoefficientLevelKeys.FIELD_COEFFICIENT_LEVEL_ID].ToString());

                var drA = dt.Rows[i + 1];
                idA = drA[CoefficientNameKeys.Field_CoefficientNames_CoefficientNameId] == DBNull.Value
                    ? DefaultValues.CoefficientNameIdMinValue
                    : int.Parse(drA[CoefficientNameKeys.Field_CoefficientNames_CoefficientNameId].ToString());

                if ((idF > 0) && (idA > 0))
                {
                    if (idF == idA)
                    {
                        if ((level == CoefficientLevelKeys.CONST_LNS_LEVEL_1) ||
                            (level == CoefficientLevelKeys.CONST_LNS_KHOAN_LEVEL_1))
                        {
                            v1 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE] == DBNull.Value
                                ? 0
                                : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE].ToString());
                            continue;
                        }
                        if ((level == CoefficientLevelKeys.CONST_LNS_LEVEL_2) ||
                            (level == CoefficientLevelKeys.CONST_LNS_KHOAN_LEVEL_2))
                        {
                            v2 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE] == DBNull.Value
                                ? 0
                                : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE].ToString());
                            continue;
                        }
                        if ((level == CoefficientLevelKeys.CONST_LNS_LEVEL_3) ||
                            (level == CoefficientLevelKeys.CONST_LNS_KHOAN_LEVEL_3))
                        {
                            v3 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE] == DBNull.Value
                                ? 0
                                : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE].ToString());
                            continue;
                        }
                        if (i == dt.Rows.Count - 2)
                        {
                            if ((level == CoefficientLevelKeys.CONST_LNS_LEVEL_4) ||
                                (level == CoefficientLevelKeys.CONST_LNS_KHOAN_LEVEL_4))
                                v4 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE] == DBNull.Value
                                    ? 0
                                    : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE].ToString());

                            var drLast = dt.Rows[dt.Rows.Count - 1];
                            idA = drLast[CoefficientNameKeys.Field_CoefficientNames_CoefficientNameId] == DBNull.Value
                                ? DefaultValues.CoefficientNameIdMinValue
                                : int.Parse(
                                    drLast[CoefficientNameKeys.Field_CoefficientNames_CoefficientNameId].ToString());
                            level = drLast[CoefficientLevelKeys.FIELD_COEFFICIENT_LEVEL_ID] == DBNull.Value
                                ? DefaultValues.CoefficientLevelIdMinValue
                                : int.Parse(drLast[CoefficientLevelKeys.FIELD_COEFFICIENT_LEVEL_ID].ToString());

                            if ((level == CoefficientLevelKeys.CONST_LNS_LEVEL_5) ||
                                (level == CoefficientLevelKeys.CONST_LNS_KHOAN_LEVEL_5))
                            {
                                v5 = drLast[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE] == DBNull.Value
                                    ? 0
                                    : double.Parse(drLast[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE].ToString());
                                flag = true;
                            }
                        }
                        else
                        {
                            if ((level == CoefficientLevelKeys.CONST_LNS_LEVEL_4) ||
                                (level == CoefficientLevelKeys.CONST_LNS_KHOAN_LEVEL_4))
                            {
                                v4 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE] == DBNull.Value
                                    ? 0
                                    : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE].ToString());
                                continue;
                            }
                        }
                    }
                    else
                    {
                        if ((level == CoefficientLevelKeys.CONST_LNS_LEVEL_5) ||
                            (level == CoefficientLevelKeys.CONST_LNS_KHOAN_LEVEL_5))
                        {
                            v5 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE] == DBNull.Value
                                ? 0
                                : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE].ToString());
                            flag = true;
                        }
                    }

                    if (flag)
                    {
                        var objBLL = new CoefficientNamesBLL(idF,
                            drF[CoefficientNameKeys.Field_CoefficientNames_CoefficientName] == DBNull.Value
                                ? string.Empty
                                : (string) drF[CoefficientNameKeys.Field_CoefficientNames_CoefficientName],
                            string.Empty);

                        objBLL.ValueLevel1 = v1;
                        objBLL.ValueLevel2 = v2;
                        objBLL.ValueLevel3 = v3;
                        objBLL.ValueLevel4 = v4;
                        objBLL.ValueLevel5 = v5;
                        list.Add(objBLL);
                        flag = false;
                    }
                }
            } // for

            return list;
        }

        private static List<CoefficientNamesBLL> GenerateListLCBCoefficientNamesBLLFromDataTable(DataTable dt)
        {
            var list = new List<CoefficientNamesBLL>();
            var flag = false;
            double v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0;
            double v6 = 0, v7 = 0, v8 = 0, v9 = 0, v10 = 0, v11 = 0, v12 = 0;
            double c1 = 0, c2 = 0, c3 = 0, c4 = 0, c5 = 0;
            double c6 = 0, c7 = 0, c8 = 0, c9 = 0, c10 = 0, c11 = 0, c12 = 0;

            var level = 0;

            for (var i = 0; i < dt.Rows.Count - 1; i++)
            {
                var idF = 0;
                var idA = 0;

                var drF = dt.Rows[i];
                idF = drF[CoefficientNameKeys.Field_CoefficientNames_CoefficientNameId] == DBNull.Value
                    ? DefaultValues.CoefficientNameIdMinValue
                    : int.Parse(drF[CoefficientNameKeys.Field_CoefficientNames_CoefficientNameId].ToString());
                level = drF[CoefficientLevelKeys.FIELD_COEFFICIENT_LEVEL_ID] == DBNull.Value
                    ? DefaultValues.CoefficientLevelIdMinValue
                    : int.Parse(drF[CoefficientLevelKeys.FIELD_COEFFICIENT_LEVEL_ID].ToString());

                var drA = dt.Rows[i + 1];
                idA = drA[CoefficientNameKeys.Field_CoefficientNames_CoefficientNameId] == DBNull.Value
                    ? DefaultValues.CoefficientNameIdMinValue
                    : int.Parse(drA[CoefficientNameKeys.Field_CoefficientNames_CoefficientNameId].ToString());

                if ((idF > 0) && (idA > 0))
                {
                    if (idF == idA)
                    {
                        if (level == CoefficientLevelKeys.CONST_LCB_LEVEL_1)
                        {
                            v1 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE] == DBNull.Value
                                ? 0
                                : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE].ToString());
                            c1 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS] == DBNull.Value
                                ? 0
                                : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS].ToString());
                            continue;
                        }
                        if (level == CoefficientLevelKeys.CONST_LCB_LEVEL_2)
                        {
                            v2 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE] == DBNull.Value
                                ? 0
                                : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE].ToString());
                            c2 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS] == DBNull.Value
                                ? 0
                                : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS].ToString());
                            continue;
                        }
                        if (level == CoefficientLevelKeys.CONST_LCB_LEVEL_3)
                        {
                            v3 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE] == DBNull.Value
                                ? 0
                                : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE].ToString());
                            c3 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS] == DBNull.Value
                                ? 0
                                : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS].ToString());
                            continue;
                        }
                        if (level == CoefficientLevelKeys.CONST_LCB_LEVEL_4)
                        {
                            v4 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE] == DBNull.Value
                                ? 0
                                : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE].ToString());
                            c4 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS] == DBNull.Value
                                ? 0
                                : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS].ToString());
                            continue;
                        }
                        if (level == CoefficientLevelKeys.CONST_LCB_LEVEL_5)
                        {
                            v5 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE] == DBNull.Value
                                ? 0
                                : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE].ToString());
                            c5 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS] == DBNull.Value
                                ? 0
                                : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS].ToString());
                            continue;
                        }
                        if (level == CoefficientLevelKeys.CONST_LCB_LEVEL_6)
                        {
                            v6 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE] == DBNull.Value
                                ? 0
                                : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE].ToString());
                            c6 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS] == DBNull.Value
                                ? 0
                                : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS].ToString());
                            continue;
                        }
                        if (level == CoefficientLevelKeys.CONST_LCB_LEVEL_7)
                        {
                            v7 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE] == DBNull.Value
                                ? 0
                                : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE].ToString());
                            c7 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS] == DBNull.Value
                                ? 0
                                : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS].ToString());
                            continue;
                        }
                        if (level == CoefficientLevelKeys.CONST_LCB_LEVEL_8)
                        {
                            v8 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE] == DBNull.Value
                                ? 0
                                : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE].ToString());
                            c8 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS] == DBNull.Value
                                ? 0
                                : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS].ToString());
                            continue;
                        }
                        if (level == CoefficientLevelKeys.CONST_LCB_LEVEL_9)
                        {
                            v9 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE] == DBNull.Value
                                ? 0
                                : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE].ToString());
                            c9 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS] == DBNull.Value
                                ? 0
                                : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS].ToString());
                            continue;
                        }
                        if (level == CoefficientLevelKeys.CONST_LCB_LEVEL_10)
                        {
                            v10 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE] == DBNull.Value
                                ? 0
                                : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE].ToString());
                            c10 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS] == DBNull.Value
                                ? 0
                                : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS].ToString());
                            continue;
                        }

                        if (i == dt.Rows.Count - 2)
                        {
                            if (level == CoefficientLevelKeys.CONST_LCB_LEVEL_11)
                            {
                                v11 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE] == DBNull.Value
                                    ? 0
                                    : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE].ToString());
                                c11 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS] == DBNull.Value
                                    ? 0
                                    : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS].ToString());
                            }

                            var drLast = dt.Rows[dt.Rows.Count - 1];
                            idA = drLast[CoefficientNameKeys.Field_CoefficientNames_CoefficientNameId] == DBNull.Value
                                ? DefaultValues.CoefficientNameIdMinValue
                                : int.Parse(
                                    drLast[CoefficientNameKeys.Field_CoefficientNames_CoefficientNameId].ToString());
                            level = drLast[CoefficientLevelKeys.FIELD_COEFFICIENT_LEVEL_ID] == DBNull.Value
                                ? DefaultValues.CoefficientLevelIdMinValue
                                : int.Parse(drLast[CoefficientLevelKeys.FIELD_COEFFICIENT_LEVEL_ID].ToString());

                            if (level == CoefficientLevelKeys.CONST_LCB_LEVEL_12)
                            {
                                v12 = drLast[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE] == DBNull.Value
                                    ? 0
                                    : double.Parse(drLast[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE].ToString());
                                c12 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS] == DBNull.Value
                                    ? 0
                                    : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS].ToString());
                                flag = true;
                            }
                        }
                        else
                        {
                            if (level == CoefficientLevelKeys.CONST_LCB_LEVEL_11)
                            {
                                v11 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE] == DBNull.Value
                                    ? 0
                                    : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE].ToString());
                                c11 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS] == DBNull.Value
                                    ? 0
                                    : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS].ToString());
                                continue;
                            }
                        }
                    }
                    else
                    {
                        if (level == CoefficientLevelKeys.CONST_LCB_LEVEL_12)
                        {
                            v12 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE] == DBNull.Value
                                ? 0
                                : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_VALUE].ToString());
                            c12 = drF[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS] == DBNull.Value
                                ? 0
                                : double.Parse(drF[CoefficientValueKeys.FIELD_COEFFICIENT_CONDITIONS].ToString());
                            flag = true;
                        }
                    }

                    if (flag)
                    {
                        var objBLL = new CoefficientNamesBLL(idF,
                            drF[CoefficientNameKeys.Field_CoefficientNames_CoefficientName] == DBNull.Value
                                ? string.Empty
                                : (string) drF[CoefficientNameKeys.Field_CoefficientNames_CoefficientName],
                            string.Empty);

                        objBLL.ValueLevel1 = v1;
                        objBLL.Condition1 = c1;
                        objBLL.ValueLevel2 = v2;
                        objBLL.Condition2 = c2;
                        objBLL.ValueLevel3 = v3;
                        objBLL.Condition3 = c3;
                        objBLL.ValueLevel4 = v4;
                        objBLL.Condition4 = c4;
                        objBLL.ValueLevel5 = v5;
                        objBLL.Condition5 = c5;

                        objBLL.ValueLevel6 = v6;
                        objBLL.Condition6 = c6;
                        objBLL.ValueLevel7 = v7;
                        objBLL.Condition7 = c7;
                        objBLL.ValueLevel8 = v8;
                        objBLL.Condition8 = c8;
                        objBLL.ValueLevel9 = v9;
                        objBLL.Condition9 = c9;
                        objBLL.ValueLevel10 = v10;
                        objBLL.Condition10 = c10;
                        objBLL.ValueLevel11 = v11;
                        objBLL.Condition11 = c11;
                        objBLL.ValueLevel12 = v12;
                        objBLL.Condition12 = c12;

                        list.Add(objBLL);
                        flag = false;
                    }
                }
            } // for

            return list;
        }

        private static List<CoefficientNamesBLL> GenerateListCoefficientNamesBLLFromDataTable_AllNames(DataTable dt)
        {
            var list = new List<CoefficientNamesBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateCoefficientNamesBLLFromDataRow(dr));

            return list;
        }

        private static CoefficientNamesBLL GenerateCoefficientNamesBLLFromDataRow(DataRow dr)
        {
            var objBLL = new CoefficientNamesBLL(
                dr[CoefficientNameKeys.Field_CoefficientNames_CoefficientNameId] == DBNull.Value
                    ? DefaultValues.CoefficientNameIdMinValue
                    : int.Parse(dr[CoefficientNameKeys.Field_CoefficientNames_CoefficientNameId].ToString()),
                dr[CoefficientNameKeys.Field_CoefficientNames_CoefficientName] == DBNull.Value
                    ? string.Empty
                    : (string) dr[CoefficientNameKeys.Field_CoefficientNames_CoefficientName],
                dr[CoefficientNameKeys.Field_CoefficientNames_CoefficientNameDescription] == DBNull.Value
                    ? string.Empty
                    : (string) dr[CoefficientNameKeys.Field_CoefficientNames_CoefficientNameDescription]
            );

            return objBLL;
        }

        #endregion
    }
}