using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H0;
using HRMUtil;
using HRMUtil.KeyNames.H0;

namespace HRMBLL.H0
{
    public class DepartmentEmployeeBLL
    {
        #region constructors

        public DepartmentEmployeeBLL(int departmentId, int userId)
        {
            DepartmentId = departmentId;
            UserId = userId;
        }

        #endregion

        #region private fields

        private string _UserName;
        private string _EmployeeCode;
        private string _FullName;

        #endregion

        #region properties

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }


        public int UserId { get; set; }

        public string UserName
        {
            get
            {
                if (string.IsNullOrEmpty(_UserName))
                    return string.Empty;
                return _UserName;
            }
            set { _UserName = value; }
        }

        public string EmployeeCode
        {
            get
            {
                if (string.IsNullOrEmpty(_EmployeeCode))
                    return string.Empty;
                return _EmployeeCode;
            }
            set { _EmployeeCode = value; }
        }

        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(_FullName))
                    return string.Empty;
                return _FullName;
            }
            set { _FullName = value; }
        }

        public DateTime Birthday { get; set; }

        #endregion

        #region public methods insert, update, delete

        public long Insert()
        {
            return new DepartmentEmployeeDAL().Insert(DepartmentId, UserId);
        }

        public static long Insert(int departmentId, string userIds)
        {
            return new DepartmentEmployeeDAL().Insert(departmentId, userIds);
        }

        public static long Update(int departmentId, int userId)
        {
            return new DepartmentEmployeeDAL().Update(departmentId, userId);
        }

        public static long DeleteDeptIdUserIds(int departmentId, string userIds)
        {
            return new DepartmentEmployeeDAL().DeleteDeptIdUserIds(departmentId, userIds);
        }

        #endregion

        #region public static Get methods

        public static List<DepartmentEmployeeBLL> GetByDeptId(int departmentId)
        {
            var name = DepartmentsBLL.GetDepartmentNameByDeptId(departmentId);
            return GenerateListDepartmentEmployeeBLLFromDataTable(new DepartmentEmployeeDAL().GetByDeptId(departmentId));
        }

        public static DataRow GetDRByDeptId(int departmentId)
        {
            var dt = new DepartmentEmployeeDAL().GetByDeptId(departmentId);
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        public static List<DepartmentEmployeeBLL> GetByRoot(int rootId)
        {
            return GenerateListDepartmentEmployeeBLLFromDataTable(new DepartmentEmployeeDAL().GetByRoot(rootId));
        }

        public static List<DepartmentEmployeeBLL> GetByRootLeaveDate(int rootId, DateTime leaveDate)
        {
            return
                GenerateListDepartmentEmployeeBLLFromDataTable(new DepartmentEmployeeDAL().GetByRootLeaveDate(rootId,
                    leaveDate));
        }

        public static DataRow GetDRByUserId(int userId)
        {
            var dt = new DepartmentEmployeeDAL().GetByUserId(userId);
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        public static List<DepartmentEmployeeBLL> GetByUserId(int userId)
        {
            return GenerateListDepartmentEmployeeBLLFromDataTable(new DepartmentEmployeeDAL().GetByUserId(userId));
        }

        public static List<DepartmentEmployeeBLL> GetAll()
        {
            return GenerateListDepartmentEmployeeBLLFromDataTable(new DepartmentEmployeeDAL().GetAll());
        }

        public static DataTable GetAllToDT()
        {
            return new DepartmentEmployeeDAL().GetAll();
        }

        public static DataTable GetByStatus(int status)
        {
            return new DepartmentEmployeeDAL().GetByStatus(status);
        }

        #endregion

        #region private methods

        private static List<DepartmentEmployeeBLL> GenerateListDepartmentEmployeeBLLFromDataTable(DataTable dt)
        {
            var list = new List<DepartmentEmployeeBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateDepartmentEmployeeBLLFromDataRow(dr));

            return list;
        }

        public static DepartmentEmployeeBLL GetEmployeeById(int userId)
        {
            var objEmployeesDAL = new DepartmentEmployeeDAL();
            var dt = objEmployeesDAL.GetByUserId(userId);
            if (dt.Rows.Count > 0)
                return GenerateDepartmentEmployeeBLLFromDataRow(dt.Rows[0]);
            return null;
        }

        private static DepartmentEmployeeBLL GenerateDepartmentEmployeeBLLFromDataRow(DataRow dr)
        {
            var objBLL = new DepartmentEmployeeBLL(
                dr[DepartmentKeys.FIELD_DEPARTMENT_ID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[DepartmentKeys.FIELD_DEPARTMENT_ID].ToString()),
                dr[EmployeeKeys.FIELD_EMPLOYEES_USERID] == DBNull.Value
                    ? 0
                    : int.Parse(dr[EmployeeKeys.FIELD_EMPLOYEES_USERID].ToString())
            );

            try
            {
                objBLL.DepartmentName = dr[DepartmentKeys.FIELD_DEPARTMENT_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[DepartmentKeys.FIELD_DEPARTMENT_NAME].ToString();
                objBLL._EmployeeCode = dr[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeKeys.FIELD_EMPLOYEES_EMPLOYEE_CODE].ToString();
                objBLL._FullName = dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeKeys.FIELD_EMPLOYEES_FULLNAME].ToString();
                objBLL._UserName = dr[EmployeeKeys.FIELD_EMPLOYEES_USERNAME] == DBNull.Value
                    ? string.Empty
                    : dr[EmployeeKeys.FIELD_EMPLOYEES_USERNAME].ToString();
                objBLL.Birthday = dr[EmployeeKeys.FIELD_EMPLOYEES_BIRTHDAY] == DBNull.Value
                    ? FormatDate.GetSQLDateMinValue
                    : Convert.ToDateTime(dr[EmployeeKeys.FIELD_EMPLOYEES_BIRTHDAY].ToString());
            }
            catch
            {
            }

            return objBLL;
        }

        #endregion
    }
}