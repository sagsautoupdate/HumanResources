using System;
using System.Collections.Generic;
using System.Data;
using HRMBLL.H0.Helper;
using HRMDAL.H0;
using HRMUtil;
using HRMUtil.KeyNames.H0;

namespace HRMBLL.H0
{
    public class PositionsBLL
    {
        #region private fields

        private string _SP = "";
        private string _SPValue = "";

        public double F { get; set; }

        public double Om { get; set; }

        public double Ko { get; set; }

        #endregion

        #region properties

        public int PositionId { get; set; }

        public string PositionName { get; set; }

        public int QualificationId { get; set; }

        public string QualificationName { get; set; }

        public string Description { get; set; }

        public int LevelPosition { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = "";

        public string LevelPositionName { get; set; } = "";

        public string JobDecription { get; set; }

        #endregion

        #region constructors

        public PositionsBLL(int positionId, string positionName, string description)
        {
            PositionId = positionId;
            PositionName = positionName;
            Description = description;
        }

        public PositionsBLL(int positionId, string positionName, string description, string jobDecription)
        {
            PositionId = positionId;
            PositionName = positionName;
            Description = description;
            JobDecription = jobDecription;
        }

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
            var objPositionsDAL = new PositionsDAL();
            if (PositionId <= 0)
            {
                _SP = $"Ins_H0_Position";
                _SPValue = $"N'{PositionName}', N'{Description}', {LevelPosition}, {DepartmentId}";
                return objPositionsDAL.Insert(PositionName, Description, LevelPosition, DepartmentId);
            }
            _SP = $"Upd_H0_Position";
            _SPValue = $"{PositionId}, N'{PositionName}', N'{Description}', {LevelPosition}, {DepartmentId}";
            return objPositionsDAL.Update(PositionId, PositionName, Description, LevelPosition, DepartmentId);
        }

        /// <summary>
        ///     Author: Giang
        ///     Date: 12/09/2014
        ///     Content: Them JobDescription
        /// </summary>
        /// <returns></returns>
        public long Save_V1()
        {
            var objPositionsDAL = new PositionsDAL();
            if (PositionId <= 0)
            {
                _SP = $"Ins_H0_PositionV1";
                _SPValue =
                    $"PositionName: N'{PositionName}', Description: N'{Description}', LevelPosition: {LevelPosition}, DepartmentId: {DepartmentId}, JobDecription: N'{JobDecription}', Hide: {1}, F: {F}, Om: {Om}";
                return objPositionsDAL.Insert_V1(PositionName, Description, LevelPosition, DepartmentId, JobDecription,
                    1, F, Om);
            }
            _SP = $"Upd_H0_PositionV1";
            _SPValue =
                $"PositionId: {PositionId}, PositionName: N'{PositionName}', Description: N'{Description}', LevelPosition: {LevelPosition}, DepartmentId: {DepartmentId}, JobDescription: N'{JobDecription}', F: {F}, Om: {Om}";
            return objPositionsDAL.Update_V1(PositionId, PositionName, Description, LevelPosition, DepartmentId,
                JobDecription, F, Om);
        }

        /// <summary>
        ///     Author: Giang
        ///     Date: 12/09/2014
        ///     Content: Them JobDescription
        /// </summary>
        /// <returns></returns>
        public static long Update_V1(int positionId, string positionName, string description, int levelPosition,
            int departmentId, string jobDecription, double f, double om)
        {
            var objPositionsDAL = new PositionsDAL();
            if (description == null)
                description = string.Empty;
            return objPositionsDAL.Update_V1(positionId, positionName, description, levelPosition, departmentId,
                jobDecription, f, om);
        }

        public static long Update(int positionId, string positionName, string description, int levelPosition,
            int departmentId)
        {
            var objPositionsDAL = new PositionsDAL();
            if (description == null)
                description = string.Empty;
            return objPositionsDAL.Update(positionId, positionName, description, levelPosition, departmentId);
        }

        public static long Delete(int positionId)
        {
            var objPositionsDAL = new PositionsDAL();
            return objPositionsDAL.Delete(positionId);
        }

        #endregion

        #region public static Get methods

        public static DataRow GetPositionId(int positionId)
        {
            var dt = new PositionsDAL().GetPositionId(positionId);

            if (dt.Rows.Count == 1)
                return dt.Rows[0];
            return null;
        }

        /// <summary>
        ///     Author: Giang
        ///     Date: 1/13/2015
        ///     Content: Get position from view position
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllView()
        {
            var objPositionsDAL = new PositionsDAL();
            return objPositionsDAL.GetAllView();
        }

        public static List<PositionsBLL> GetAll()
        {
            var objPositionsDAL = new PositionsDAL();
            return GenerateListPositionFromDataTable(objPositionsDAL.GetAll());
        }

        public static List<PositionsBLL> GetByFilter(string positionName, int levelPosition, int departmentId)
        {
            return
                GenerateListPositionFromDataTable(new PositionsDAL().GetByFilter(positionName, levelPosition,
                    departmentId));
        }

        public static DataRow GetDRByFilter(string positionName, int levelPosition, int departmentId)
        {
            var dt = new PositionsDAL().GetByFilter(positionName, levelPosition, departmentId);

            if (dt.Rows.Count == 1)
                return dt.Rows[0];
            return null;
        }

        /// <summary>
        ///     Author: Giang
        ///     Date: 12/08/2014
        ///     Content: Lay ds position tra ve dt
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllToDT()
        {
            var objPositionsDAL = new PositionsDAL();
            return objPositionsDAL.GetDataByHide(1);
        }

        public static DataTable GetAllToDTForEducationContract()
        {
            var objPositionsDAL = new PositionsDAL();
            return objPositionsDAL.GetAllToDTForEducationContract();
        }

        public static DataTable GetPositionLeader(string positionName, int levelPosition, int departmentId)
        {
            var objPositionsDAL = new PositionsDAL();
            return objPositionsDAL.GetPositionLeader(positionName, levelPosition, departmentId);
        }

        public static DataTable GetAllToDT_V1()
        {
            var objPositionsDAL = new PositionsDAL();
            return objPositionsDAL.GetDataByHide(-1);
        }

        public static List<PositionsBLL> GetAll_N()
        {
            var objPositionsDAL = new PositionsDAL();
            return GenerateListPositionFromDataTable_N(objPositionsDAL.GetAll());
        }

        //public static List<PositionsBLL> GetAllByIds(string positionIds)
        //{
        //    PositionsDAL objPositionsDAL = new PositionsDAL();
        //    return GenerateListPositionFromDataTable(objPositionsDAL.GetByIds(positionIds));
        //}

        //public static List<PositionsBLL> GetIsRecruitment()
        //{
        //    return GenerateListPositionFromDataTable(new PositionsDAL().GetIsRecruitment());
        //}

        #endregion

        #region private methods

        private static List<PositionsBLL> GenerateListPositionFromDataTable_N(DataTable dt)
        {
            var lstPositions = new List<PositionsBLL>();

            lstPositions.Add(new PositionsBLL(0, "", string.Empty));

            foreach (DataRow dr in dt.Rows)
                lstPositions.Add(GeneratePositionFromDataRow(dr));

            return lstPositions;
        }

        private static List<PositionsBLL> GenerateListPositionFromDataTable(DataTable dt)
        {
            var lstPositions = new List<PositionsBLL>();

            foreach (DataRow dr in dt.Rows)
                lstPositions.Add(GeneratePositionFromDataRow(dr));

            return lstPositions;
        }

        private static PositionsBLL GeneratePositionFromDataRow(DataRow dr)
        {
            var objBLL = new PositionsBLL(
                dr[PositionKeys.FIELD_POSITION_ID] == DBNull.Value
                    ? DefaultValues.PositionIdMinValue
                    : int.Parse(dr[PositionKeys.FIELD_POSITION_ID].ToString()),
                dr[PositionKeys.FIELD_POSITION_NAME] == DBNull.Value
                    ? string.Empty
                    : dr[PositionKeys.FIELD_POSITION_NAME].ToString(),
                dr[PositionKeys.FIELD_POSITION_DESCRIPTION] == DBNull.Value
                    ? string.Empty
                    : dr[PositionKeys.FIELD_POSITION_DESCRIPTION].ToString());

            objBLL.LevelPosition = dr[PositionKeys.Field_Position_LevelPosition] == DBNull.Value
                ? 0
                : int.Parse(dr[PositionKeys.Field_Position_LevelPosition].ToString());
            objBLL.DepartmentId = dr[PositionKeys.Field_Position_DepartmentId] == DBNull.Value
                ? 0
                : int.Parse(dr[PositionKeys.Field_Position_DepartmentId].ToString());
            try
            {
                objBLL.DepartmentName = dr[DepartmentKeys.FIELD_DEPARTMENT_NAME] == DBNull.Value
                    ? ""
                    : dr[DepartmentKeys.FIELD_DEPARTMENT_NAME].ToString();
            }
            catch
            {
            }
            objBLL.LevelPositionName = Constants.GetLevelPositionNameById(objBLL.LevelPosition);
            return objBLL;
        }

        #endregion
    }
}