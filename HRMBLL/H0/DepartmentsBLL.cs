using System;
using System.Collections.Generic;
using System.Data;
using HRMBLL.H0.Helper;
using HRMDAL.H0;
using HRMUtil.KeyNames.H0;

namespace HRMBLL.H0
{
    public class DepartmentsBLL
    {
        #region private fields

        private string _ChildNodeIds = "";
        private string _SP = "";
        private string _SPValue = "";
        private string _OldContent = "";

        #endregion

        #region properties

        public int Sortby { get; set; }

        public bool Direct { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentCode { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentNameLevel { get; set; }

        public string DepartmentNameE { get; set; }

        public string DepartmentFullName { get; set; }

        public string DepartmentFullName2 { get; set; }

        public string RootName { get; set; }

        public string Description { get; set; }

        public int ParentId { get; set; }

        public int ChildNodeCount { get; set; }

        public int Level { get; set; }

        public int RootId { get; set; }

        public string ChildNodeIds
        {
            get
            {
                var ids = _ChildNodeIds;
                if ((ids != null) && (ids.Length > 0))
                    ids = _ChildNodeIds.Substring(0, _ChildNodeIds.Length - 1);
                return ids;
            }
            set { _ChildNodeIds = value; }
        }

        public int Status { get; set; }

        #endregion

        #region constructors

        public DepartmentsBLL()
        {
        }

        public DepartmentsBLL(int departmentId, string departmentName)
        {
            DepartmentId = departmentId;
            DepartmentName = departmentName;
        }

        #endregion

        #region public methods insert, update, delete

        public long SaveV1()
        {
            var objDAL = new DepartmentsDAL();
            if (DepartmentId <= 0)
            {
                _SP = $"{DepartmentKeys.SP_DEPARTMENT_INSERTV1}";
                _SPValue =
                    $"{ParentId}, N'{DepartmentCode}', N'{DepartmentName}', N'{DepartmentNameE}', N'{Description}'";
                return objDAL.InsertV1(ParentId, DepartmentCode, DepartmentName, DepartmentNameE, Description, Direct,
                    RootId, Level, Sortby, DepartmentFullName);
            }
            _SP = $"{DepartmentKeys.SP_DEPARTMENT_UPDATEV1}";
            _SPValue =
                $"N'{DepartmentCode}', N'{DepartmentName}', N'{DepartmentNameE}', N'{Description}', {DepartmentId}";
            return objDAL.UpdateV1(DepartmentCode, DepartmentName, DepartmentNameE, Description, Direct, RootId, Level,
                Sortby, DepartmentFullName, DepartmentId);
        }

        public long Insert(int parentId, string departmentCode, string departmentName, string departmentNameE,
            string description)
        {
            _SP = $"{DepartmentKeys.SP_DEPARTMENT_INSERT}";
            _SPValue = $"{ParentId}, N'{DepartmentCode}', N'{DepartmentName}', N'{DepartmentNameE}', N'{Description}'";
            var objDAL = new DepartmentsDAL();
            return objDAL.Insert(ParentId, DepartmentCode, DepartmentName, DepartmentNameE, Description);
        }

        public long Update(string departmentCode, string departmentName, string departmentNameE, string description,
            int departmentId)
        {
            _SP = $"{DepartmentKeys.SP_DEPARTMENT_UPDATE}";
            _SPValue =
                $"N'{DepartmentCode}', N'{DepartmentName}', N'{DepartmentNameE}', N'{Description}', {DepartmentId}";
            var objDAL = new DepartmentsDAL();
            return objDAL.Update(DepartmentCode, DepartmentName, DepartmentNameE, Description, DepartmentId);
        }

        public long Save()
        {
            var objDAL = new DepartmentsDAL();
            if (DepartmentId <= 0)
            {
                _SP = $"{DepartmentKeys.SP_DEPARTMENT_INSERT}";
                _SPValue =
                    $"ParentId: {ParentId}, DepartmentCode: N'{DepartmentCode}', DepartmentName: N'{DepartmentName}', DepartmentNameE: N'{DepartmentNameE}', Description: N'{Description}'";
                return objDAL.Insert(ParentId, DepartmentCode, DepartmentName, DepartmentNameE, Description);
            }
            _SP = $"{DepartmentKeys.SP_DEPARTMENT_UPDATE}";
            _SPValue =
                $"DepartmentCode: N'{DepartmentCode}', DepartmentName: N'{DepartmentName}', DepartmentNameE: N'{DepartmentNameE}', Description: N'{Description}', DepartmentId: {DepartmentId}";
            return objDAL.Update(DepartmentCode, DepartmentName, DepartmentNameE, Description, DepartmentId);
        }

        public string ReturnSP()
        {
            return _SP;
        }

        public string ReturnSPValue()
        {
            return _SPValue;
        }

        public static long Delete(int departmentId)
        {
            var objDAL = new DepartmentsDAL();
            return objDAL.Delete(departmentId);
        }

        #endregion

        #region public static Get methods

        public static List<DepartmentsBLL> GetIndirectRoot()
        {
            return GenerateListDepartmentFromDataTable(new DepartmentsDAL().GetAllRoots(1));
        }

        public static DataRow GetMaxSortNumber(int parentId)
        {
            var dt = new DepartmentsDAL().GetMaxSortNumber(parentId);
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        public static List<DepartmentsBLL> GetAllRoots()
        {
            return GenerateListDepartmentFromDataTable(new DepartmentsDAL().GetAllRoots(0));
        }

        public static DataTable GetAllRootsDT()
        {
            return new DepartmentsDAL().GetAllRoots(0);
        }

        public static List<DepartmentsBLL> GetAllRootsNoSAGS()
        {
            return GenerateListDepartmentFromDataTableNoSAGS(new DepartmentsDAL().GetAllRoots(0));
        }

        public static DataTable GetAllRootsNoSAGSDT()
        {
            return new DepartmentsDAL().GetAllRoots(0);
        }

        public static List<DepartmentsBLL> GetAllDepartments()
        {
            return GenerateListDepartmentFromDataTable(new DepartmentsDAL().GetAllDepartments());
        }

        public static DataTable GetDTAllDepartments()
        {
            return new DepartmentsDAL().GetAllDepartments();
        }

        public static List<DepartmentsBLL> GetDepartmentRoot()
        {
            return GenerateListDepartmentFromDataTable(new DepartmentsDAL().GetDepartmentRoot());
        }

        public static DataTable GetDTDepartmentRoot()
        {
            return new DepartmentsDAL().GetDepartmentRoot();
        }

        public void GetAllChildId(int parentId)
        {
            if (parentId > 0)
            {
                var dt = new DepartmentsDAL().GetDepartmentSubLevel(parentId);
                foreach (DataRow dr in dt.Rows)
                {
                    var id = dr[DepartmentKeys.FIELD_DEPARTMENT_ID] == DBNull.Value
                        ? 0
                        : int.Parse(dr[DepartmentKeys.FIELD_DEPARTMENT_ID].ToString());
                    _ChildNodeIds += id + ",";
                    GetAllChildId(id);
                }
                _ChildNodeIds += parentId + ",";
            }
        }

        public void GetAllLeafId(int parentId)
        {
            var dt = new DepartmentsDAL().GetDepartmentSubLevel(parentId);
            foreach (DataRow dr in dt.Rows)
            {
                var id = dr[DepartmentKeys.FIELD_DEPARTMENT_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[DepartmentKeys.FIELD_DEPARTMENT_ID].ToString());
                if (new DepartmentsDAL().GetDepartmentSubLevel(id).Rows.Count <= 0)
                    _ChildNodeIds += id + ",";

                GetAllChildId(id);
            }
        }

        public static List<DepartmentsBLL> GetAll_SubLevel(int parentId)
        {
            return GenerateListDepartmentFromDataTable(new DepartmentsDAL().GetDepartmentSubLevel(parentId));
        }

        public static List<DepartmentsBLL> GetAll_SubLevel_ByRootIdDepartmentId(int parentId, int rootId,
            int departmentId)
        {
            return
                GenerateListDepartmentFromDataTable(
                    new DepartmentsDAL().GetDepartmentSubLevelByRootIdDepartmentId(parentId, rootId, departmentId));
        }

        public static string GetDepartmentNameByDeptId(int departmentId)
        {
            var departmentName = string.Empty;
            var parentId = 0;
            var objBLL = GetById(departmentId);
            if (objBLL != null)
            {
                parentId = objBLL.ParentId;
                departmentName += objBLL.DepartmentName;
                while (parentId != -1)
                {
                    objBLL = GetById(parentId);
                    parentId = objBLL.ParentId;
                    departmentName += objBLL.DepartmentName;
                }
            }
            return departmentName;
        }

        public static DepartmentsBLL GetRootBySubId(int subId)
        {
            var list = GenerateListDepartmentFromDataTable(new DepartmentsDAL().GetDepartmentRootBySub(subId));
            if (list.Count == 1)
                return list[0];
            return null;
        }

        public static DataRow GetByIdDT(int departmentId)
        {
            var list = new DepartmentsDAL().GetByIdV1(departmentId);
            if (list.Rows.Count > 0)
                return list.Rows[0];
            return null;
        }

        public static DataRow GetByIdDR(int departmentId)
        {
            var list = new DepartmentsDAL().GetById(departmentId);
            if (list.Rows.Count > 0)
                return list.Rows[0];
            return null;
        }

        public static DepartmentsBLL GetById(int departmentId)
        {
            var list = GenerateListDepartmentFromDataTable(new DepartmentsDAL().GetById(departmentId));
            if (list.Count == 1)
                return list[0];
            return null;
        }

        public static List<DepartmentsBLL> GetByIds(string departmentIds)
        {
            return GenerateListDepartmentFromDataTable(new DepartmentsDAL().GetByIds(departmentIds));
        }

        public static string GetDepartmentNamesByIdsRootId(string departmentIds, int rootId, string dash,
            string separator)
        {
            var list = new List<DepartmentsBLL>();
            list = GenerateListDepartmentFromDataTable(new DepartmentsDAL().GetByIdsRoot(departmentIds, rootId));
            return GetDepartmentNames(list, dash, separator);
        }

        private static string GetDepartmentNames(List<DepartmentsBLL> list, string dash, string separator)
        {
            var departmentNames = string.Empty;
            if (list.Count > 0)
                for (var i = 0; i < list.Count; i++)
                {
                    var obj = list[i];
                    if (i < list.Count - 1)
                        departmentNames += dash + obj.DepartmentName + separator;
                    else
                        departmentNames += dash + obj.DepartmentName;
                }
            return departmentNames;
        }

        public static List<DepartmentsBLL> GetByRoot(int rootId)
        {
            return GenerateListDepartmentFromDataTable(new DepartmentsDAL().GetByRoot(rootId));
        }

        #endregion

        #region private methods

        private static List<DepartmentsBLL> GenerateListDepartmentFromDataTableNoSAGS(DataTable dt)
        {
            var lstDepartments = new List<DepartmentsBLL>();

            foreach (DataRow dr in dt.Rows)
            {
                var obj = GenerateDepartmentFromDataRow(dr);
                if (obj.DepartmentId > 0)
                    lstDepartments.Add(obj);
            }

            return lstDepartments;
        }

        private static List<DepartmentsBLL> GenerateListDepartmentFromDataTable(DataTable dt)
        {
            var lstDepartments = new List<DepartmentsBLL>();

            foreach (DataRow dr in dt.Rows)
                lstDepartments.Add(GenerateDepartmentFromDataRow(dr));

            return lstDepartments;
        }

        private static DepartmentsBLL GenerateDepartmentFromDataRow(DataRow dr)
        {
            var objBLL = new DepartmentsBLL(
                dr[DepartmentKeys.FIELD_DEPARTMENT_ID] == DBNull.Value
                    ? DefaultValues.DepartmentIdMinValue
                    : int.Parse(dr[DepartmentKeys.FIELD_DEPARTMENT_ID].ToString()),
                dr[DepartmentKeys.FIELD_DEPARTMENT_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[DepartmentKeys.FIELD_DEPARTMENT_NAME].ToString()
            );

            try
            {
                objBLL.ChildNodeCount = dr["ChildNodeCount"] == DBNull.Value
                    ? 0
                    : int.Parse(dr["ChildNodeCount"].ToString());
            }
            catch
            {
            }

            objBLL.ParentId = dr[DepartmentKeys.FIELD_DEPARTMENT_PARENT_ID] == DBNull.Value
                ? 0
                : int.Parse(dr[DepartmentKeys.FIELD_DEPARTMENT_PARENT_ID].ToString());

            objBLL.DepartmentCode = dr[DepartmentKeys.FIELD_DEPARTMENT_CODE] == DBNull.Value
                ? string.Empty
                : dr[DepartmentKeys.FIELD_DEPARTMENT_CODE].ToString();
            objBLL.DepartmentNameE = dr[DepartmentKeys.FIELD_DEPARTMENT_NAME_E] == DBNull.Value
                ? string.Empty
                : dr[DepartmentKeys.FIELD_DEPARTMENT_NAME_E].ToString();
            objBLL.Description = dr[DepartmentKeys.FIELD_DEPARTMENT_DESCRIPTION] == DBNull.Value
                ? string.Empty
                : dr[DepartmentKeys.FIELD_DEPARTMENT_DESCRIPTION].ToString();
            objBLL.Level = dr[DepartmentKeys.FIELD_DEPARTMENT_LEVEL] == DBNull.Value
                ? 0
                : int.Parse(dr[DepartmentKeys.FIELD_DEPARTMENT_LEVEL].ToString());

            objBLL.DepartmentNameLevel = GetDash(objBLL.Level) + objBLL.DepartmentName;

            try
            {
                objBLL.RootId = dr[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[DepartmentKeys.FIELD_DEPARTMENT_ROOT_ID].ToString());
            }
            catch
            {
            }
            try
            {
                objBLL.ChildNodeCount = dr[DepartmentKeys.FIELD_DEPARTMENT_CHILD_NODE_COUNT] == DBNull.Value
                    ? 0
                    : int.Parse(dr[DepartmentKeys.FIELD_DEPARTMENT_CHILD_NODE_COUNT].ToString());
            }
            catch
            {
            }
            try
            {
                objBLL.DepartmentFullName = dr[DepartmentKeys.FIELD_DEPARTMENT_DEPARTMENTFULLNAME] == DBNull.Value
                    ? string.Empty
                    : dr[DepartmentKeys.FIELD_DEPARTMENT_DEPARTMENTFULLNAME].ToString();
                var arr = objBLL.DepartmentFullName.Split('-');
                if (arr.Length > 1)
                    if (objBLL.Level > 2)
                        objBLL.DepartmentFullName2 = arr[0] + "-" + arr[1];
                    else
                        objBLL.DepartmentFullName2 = arr[0];
                else
                    objBLL.DepartmentFullName2 = objBLL.DepartmentFullName;
            }
            catch
            {
            }
            try
            {
                objBLL.RootName = dr[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME] == DBNull.Value
                    ? string.Empty
                    : dr[DepartmentKeys.FIELD_DEPARTMENT_ROOTNAME].ToString();
            }
            catch
            {
            }
            try
            {
                objBLL.Status = dr[DepartmentKeys.FIELD_DEPARTMENT_STATUS] == DBNull.Value
                    ? 0
                    : int.Parse(dr[DepartmentKeys.FIELD_DEPARTMENT_STATUS].ToString());
            }
            catch
            {
            }

            return objBLL;
        }

        private static string GetDash(int a)
        {
            var returnValue = string.Empty;
            switch (a)
            {
                case 1:
                    returnValue = " - ";
                    break;
                case 2:
                    returnValue = " -- ";
                    break;
                case 3:
                    returnValue = " --- ";
                    break;
            }
            return returnValue;
        }

        #endregion
    }
}