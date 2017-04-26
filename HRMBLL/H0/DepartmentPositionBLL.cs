using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H0;
using HRMUtil.KeyNames.H0;

namespace HRMBLL.H0
{
    public class DepartmentPositionBLL
    {
        #region private fields

        #endregion

        #region properties

        public int DeptPositionId { get; set; }

        public int DepartmentId { get; set; }

        public int PositionId { get; set; }

        public string PositionName { get; set; } = "";

        public string DepartmentName { get; set; } = "";

        #endregion

        #region public methods insert, update, delete

        public long Save()
        {
            var objDAL = new DepartmentPositionDAL();
            if (DeptPositionId <= 0)
                return objDAL.Insert(DepartmentId, PositionId);
            return objDAL.Update(DepartmentId, PositionId, DeptPositionId);
        }


        public static long Update(int departmentId, int positionId, int deptPositionId, string PositionName)
        {
            if (deptPositionId <= 0)
                return new DepartmentPositionDAL().Insert(departmentId, positionId);
            return new DepartmentPositionDAL().Update(departmentId, positionId, deptPositionId);
        }

        public static long Delete(int deptPositionId)
        {
            return new DepartmentPositionDAL().Delete(deptPositionId);
        }

        #endregion

        #region public static Get methods

        public static List<DepartmentPositionBLL> GetByFilter(int departmentId, int positionId)
        {
            return
                GenerateListDepartmentPositionBLLFromDataTable(new DepartmentPositionDAL().GetByFilter(departmentId,
                    positionId));
        }

        public static DataRow GetDRByFilter(int departmentId, int positionId)
        {
            var dt = new DepartmentPositionDAL().GetByFilter(departmentId, positionId);
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        #endregion

        #region private methods

        private static List<DepartmentPositionBLL> GenerateListDepartmentPositionBLLFromDataTable(DataTable dt)
        {
            var list = new List<DepartmentPositionBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateDepartmentPositionBLLFromDataRow(dr));

            return list;
        }

        private static DepartmentPositionBLL GenerateDepartmentPositionBLLFromDataRow(DataRow dr)
        {
            var objBLL = new DepartmentPositionBLL();
            objBLL.DeptPositionId = dr[DepartmentPositionKeys.Field_DepartmentPosition_DeptPositionId] == DBNull.Value
                ? 0
                : int.Parse(dr[DepartmentPositionKeys.Field_DepartmentPosition_DeptPositionId].ToString());
            objBLL.DepartmentId = dr[DepartmentPositionKeys.Field_DepartmentPosition_DepartmentId] == DBNull.Value
                ? 0
                : int.Parse(dr[DepartmentPositionKeys.Field_DepartmentPosition_DepartmentId].ToString());
            objBLL.PositionId = dr[DepartmentPositionKeys.Field_DepartmentPosition_PositionId] == DBNull.Value
                ? 0
                : int.Parse(dr[DepartmentPositionKeys.Field_DepartmentPosition_PositionId].ToString());
            try
            {
                objBLL.PositionName = dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                    ? ""
                    : dr[PositionKeys.FIELD_POSITION_NAME].ToString();
            }
            catch
            {
            }
            try
            {
                objBLL.DepartmentName = dr[DepartmentKeys.FIELD_DEPARTMENT_NAME] == DBNull.Value
                    ? ""
                    : dr[DepartmentKeys.FIELD_DEPARTMENT_NAME].ToString();
            }
            catch
            {
            }
            return objBLL;
        }

        #endregion
    }
}