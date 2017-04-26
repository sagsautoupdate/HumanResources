using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H0;
using HRMUtil;
using HRMUtil.KeyNames.H0;

namespace HRMBLL.H0
{
    public class RelationTypesBLL
    {
        #region constructors

        public RelationTypesBLL(int relationType, string relationTypeName, string description, int type)
        {
            RelationTypeId = relationType;
            RelationTypeName = relationTypeName;
            Description = description;
            Type = type;
        }

        #endregion

        #region private fields

        private string _SP = "";
        private string _SPValue = "";

        #endregion

        #region properties

        public int RelationTypeId { get; set; }

        public string RelationTypeName { get; set; }

        public string Description { get; set; }

        public int Type { get; set; }

        public string TypeName { get; set; }

        #endregion

        #region public methods insert, update, delete

        public string ReturnSP()
        {
            return _SP;
        }

        public string ReturnSPValue()
        {
            return _SPValue;
        }

        public long Save()
        {
            var objDAL = new RelationTypesDAL();
            if (RelationTypeId <= 0)
            {
                _SP = $"Ins_H0_RelationType";
                _SPValue = $"RelationTypeName: N'{RelationTypeName}', Description: N'{Description}', Type: {Type}";
                return objDAL.Insert(RelationTypeName, Description, Type);
            }
            _SP = $"Upd_H0_RelationType";
            _SPValue =
                $"RelationTypeName: N'{RelationTypeName}', Description: N'{Description}', Type: {Type}, RelationTypeId: {RelationTypeId}";
            return objDAL.Update(RelationTypeName, Description, Type, RelationTypeId);
        }

        public static long Update(string relationTypeName, string description, int type, int relationTypeId)
        {
            var objDAL = new RelationTypesDAL();
            return objDAL.Update(relationTypeName, description, type, relationTypeId);
        }

        public static long Delete(int relationTypeId)
        {
            var objDAL = new RelationTypesDAL();
            return objDAL.Delete(relationTypeId);
        }

        #endregion

        #region public static Get methods

        /// <summary>
        ///     Author: Giang
        ///     Date: 12/25/2014
        ///     Content: Get relation by name and type
        /// </summary>
        /// <param name="relationTypeName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DataTable GetByFilterByType(int type)
        {
            var objDAL = new RelationTypesDAL();
            return objDAL.GetByFilterByType(type);
        }

        public static DataRow GetById(int RelationTypeId)
        {
            var dt = new RelationTypesDAL().GetById(RelationTypeId);

            if (dt.Rows.Count == 1)
                return dt.Rows[0];
            return null;
        }

        public static List<RelationTypesBLL> GetByFilter(string relationTypeName, int type)
        {
            var objDAL = new RelationTypesDAL();
            return GenerateListRelationTypesBLLFromDataTable(objDAL.GetByFilter(relationTypeName, type));
        }


        public static List<RelationTypesBLL> GetAll()
        {
            var objDAL = new RelationTypesDAL();
            return GenerateListRelationTypesBLLFromDataTable(objDAL.GetAll());
        }

        public static DataTable GetAllDT()
        {
            var objDAL = new RelationTypesDAL();
            return objDAL.GetAll();
        }

        public static DataTable GetAllRelationGroup()
        {
            var objDAL = new RelationTypesDAL();
            return objDAL.GetAllRelationGroup();
        }

        public static List<RelationTypesBLL> GetAll_N()
        {
            var objDAL = new RelationTypesDAL();
            return GenerateListRelationTypesBLLFromDataTable_N(objDAL.GetAll());
        }

        #endregion

        #region private methods

        private static List<RelationTypesBLL> GenerateListRelationTypesBLLFromDataTable_N(DataTable dt)
        {
            var list = new List<RelationTypesBLL>();

            list.Add(new RelationTypesBLL(0, "None", string.Empty, 0));

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateRelationTypesBLLFromDataRow(dr));

            return list;
        }

        private static List<RelationTypesBLL> GenerateListRelationTypesBLLFromDataTable(DataTable dt)
        {
            var list = new List<RelationTypesBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateRelationTypesBLLFromDataRow(dr));

            return list;
        }

        private static RelationTypesBLL GenerateRelationTypesBLLFromDataRow(DataRow dr)
        {
            var objBLL = new RelationTypesBLL(
                dr[RelationTypeKeys.FIELD_RELATION_TYPE_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[RelationTypeKeys.FIELD_RELATION_TYPE_ID].ToString()),
                dr[RelationTypeKeys.FIELD_RELATION_TYPE_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[RelationTypeKeys.FIELD_RELATION_TYPE_NAME].ToString(),
                dr[RelationTypeKeys.FIELD_RELATION_TYPE_DESCRIPTION] == DBNull.Value
                    ? string.Empty
                    : dr[RelationTypeKeys.FIELD_RELATION_TYPE_DESCRIPTION].ToString(),
                dr[RelationTypeKeys.FIELD_RELATION_TYPE_TYPE] == DBNull.Value
                    ? 0
                    : int.Parse(dr[RelationTypeKeys.FIELD_RELATION_TYPE_TYPE].ToString())
            );
            objBLL.TypeName = Constants.GetRTypeName(objBLL.Type);

            return objBLL;
        }

        #endregion
    }
}