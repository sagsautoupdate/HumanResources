namespace HRMUtil.KeyNames.H0
{
    public sealed class DepartmentKeys
    {
        #region some field names of Departments table

        public const string FIELD_DEPARTMENT_ID = "DepartmentId";
        public const string FIELD_DEPARTMENT_CODE = "DepartmentCode";
        public const string FIELD_DEPARTMENT_NAME = "DepartmentName";
        public const string FIELD_DEPARTMENT_NAME_E = "DepartmentNameE";
        public const string FIELD_DEPARTMENT_DESCRIPTION = "Description";
        public const string FIELD_DEPARTMENT_LEVEL = "Level";
        public const string FIELD_DEPARTMENT_PARENT_ID = "ParentId";
        public const string FIELD_DEPARTMENT_ROOT_ID = "RootId";
        public const string FIELD_DEPARTMENT_ROOTNAME = "RootName";
        public const string FIELD_DEPARTMENT_STATUS = "Status";
        public const string FIELD_DEPARTMENT_CHILD_NODE_COUNT = "ChildNodeCount";
        public const string FIELD_DEPARTMENT_DEPARTMENTFULLNAME = "DepartmentFullName";
        public const string FIELD_DEPARTMENT_DEPARTMENTFULLNAME2 = "DepartmentFullName2";

        #endregion

        #region some store procedures of Department table

        public const string SP_DEPARTMENT_INSERT = "Ins_H0_Department";
        public const string SP_DEPARTMENT_INSERTV1 = "Ins_H0_DepartmentV1";
        public const string SP_DEPARTMENT_UPDATE = "Upd_H0_Department";
        public const string SP_DEPARTMENT_UPDATEV1 = "Upd_H0_DepartmentV1";
        public const string SP_DEPARTMENT_DELETE = "Del_H0_Department";
        public const string SP_DEPARTMENT_MAX_SORT = "Sel_H0_Department_MaxSortBy";


        //public const string SP_DEPARTMENT_GETALL = "Sel_H0_Departments_All";

        public const string SP_DEPARTMENT_GET_ALL = "Sel_H0_Departments_All";
        public const string SP_DEPARTMENT_GET_ALL_ROOT = "Sel_H0_Departments_All_Root";
        public const string SP_DEPARTMENT_GET_ROOT = "Sel_H0_Departments_Root";
        public const string SP_DEPARTMENT_GETALL_SUB_LEVEL = "Sel_H0_Departments_SubLevel";

        public const string SP_DEPARTMENTS_SUBLEVEL_BY_ROOTID_DEPARTMENTID =
            "Sel_H0_Departments_SubLevel_By_RootId_DepartmentId";

        public const string SP_DEPARTMENT_GET_ROOT_BY_SUB_ID = "Sel_H0_Departments_Root_BySubId";
        public const string SP_DEPARTMENT_GET_BY_ID = "Sel_H0_Departments_By_Id";
        public const string SP_DEPARTMENT_GET_BY_IDV1 = "Sel_H0_Departments_By_IdV1";
        public const string SP_SEL_H0_DEPARTMENTS_BY_IDS = "Sel_H0_Departments_By_Ids";
        public const string SP_DEPARTMENT_GET_BY_IDS_ROOTID = "Sel_H0_Departments_By_Ids_RootId";
        public const string SP_SEL_H0_DEPARTMENTS_BY_ROOTID = "Sel_H0_Departments_By_RootId";

        #endregion
    }
}