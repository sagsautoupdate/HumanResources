using System;
using System.Collections.Generic;
using System.Data;
using HRMDAL.H0;
using HRMUtil.KeyNames.H0;
using HRMUtil.KeyNames.H2;

namespace HRMBLL.H0
{
    public class EmployeeEducationLevelsBLL
    {
        #region private fields

        //new fields

        private string _SP;
        private string _SPValue;

        #endregion

        #region properties

        public int Id { get; set; }

        public int UserId { get; set; }

        public int EducationLevelId { get; set; }

        public string EducationLevelValue { get; set; }

        public string EducationLevelName { get; set; }

        public string Remark { get; set; }

        public int LastItem { get; set; }

        //new fields
        public string TrainingPlace { get; set; }

        public string TrainingDepartment { get; set; }

        public string Major { get; set; }

        public DateTime GraduatingYear { get; set; }

        public string Grade { get; set; }

        public string Profession { get; set; }

        #endregion

        #region public methods

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
            var objDAL = new EmployeeEducationLevelsDAL();
            if (Id <= 0)
            {
                _SP = "Ins_H0_EmployeeEducationLevel";
                _SPValue =
                    $"UserId: {UserId}, EducationLevelId: {EducationLevelId}, EducationLevelValue: N'{EducationLevelValue}', Remark: N'{Remark}'";
                return objDAL.Insert(UserId, EducationLevelId, EducationLevelValue, Remark);
                //, _TrainingPlace, _TrainingDepartment, _Major, _GraduatingYear, _Grade, _Profession);
            }
            _SP = "Upd_H0_EmployeeEducationLevel_V1";
            _SPValue =
                $"UserId: {UserId}, EducationLevelId: {EducationLevelId}, EducationLevelValue: N'{EducationLevelValue}', Remark: N'{Remark}', Id: {Id}";
            return objDAL.Update(UserId, EducationLevelId, EducationLevelValue, Remark, Id);
            //, _TrainingPlace, _TrainingDepartment, _Major, _GraduatingYear, _Grade, _Profession);
        }


        public static long Update(int userId, int educationLevelId, string educationLevelValue, string remark, int id)
            //, string trainingPlace, string trainingDepartment, string major, DateTime graduatingYear, string grade, string profession)
        {
            return new EmployeeEducationLevelsDAL().Update(userId, educationLevelId, educationLevelValue, remark, id);
            //, trainingPlace, trainingDepartment, major, graduatingYear, grade, profession);
        }

        public static long UpdateHighest(int userId, int educationLevelId, int Id)
        {
            return new EmployeeEducationLevelsDAL().UpdateHighest(userId, educationLevelId, Id);
        }

        public static long Delete(int id)
        {
            return new EmployeeEducationLevelsDAL().Delete(id);
        }

        public static string Delete(string ids)
        {
            var arr = ids.Split(',');
            foreach (var arrItem in arr)
                if (arrItem.Length > 0)
                    Delete(int.Parse(arrItem));
            return ids;
        }

        #endregion

        #region public methods get

        public static List<EmployeeEducationLevelsBLL> GetById(int userId)
        {
            return GenerateListFromDataTable(new EmployeeEducationLevelsDAL().GetById(userId));
        }

        public static DataTable GetDtById(int userId)
        {
            return new EmployeeEducationLevelsDAL().GetById(userId);
        }

        public static DataRow GetDataRowEmployeeEduById(int userId)
        {
            //EmployeesDAL objEmployeesDAL = new EmployeesDAL();
            var dt = new EmployeeEducationLevelsDAL().GetById(userId);
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        public static DataRow GetByFilter(int userId, int EducationLevelId, string EducationLevelValue)
        {
            //EmployeesDAL objEmployeesDAL = new EmployeesDAL();
            var dt = new EmployeeEducationLevelsDAL().GetByFilter(userId, EducationLevelId, EducationLevelValue);
            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            return null;
        }

        #endregion

        #region private methods

        private static List<EmployeeEducationLevelsBLL> GenerateListFromDataTable(DataTable dt)
        {
            var list = new List<EmployeeEducationLevelsBLL>();

            foreach (DataRow dr in dt.Rows)
                list.Add(GenerateFromDataTable(dr, dt.Rows.Count));

            return list;
        }

        public static EmployeeEducationLevelsBLL GenerateFromDataTable(DataRow dr)
        {
            var c = new EmployeeEducationLevelsBLL();

            c.Id = dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_ID] == DBNull.Value
                ? 0
                : int.Parse(dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_ID].ToString());
            c.UserId = dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_USER_ID] == DBNull.Value
                ? 0
                : int.Parse(dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_USER_ID].ToString());
            c.EducationLevelId = dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_EDUCATION_LEVEL_ID] ==
                                 DBNull.Value
                ? 0
                : int.Parse(dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_EDUCATION_LEVEL_ID].ToString());
            c.EducationLevelValue = dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_VALUE] == DBNull.Value
                ? string.Empty
                : dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_VALUE].ToString();
            c.Remark = dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_REMARK] == DBNull.Value
                ? string.Empty
                : dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_REMARK].ToString();

            c.EducationLevelName = dr[EducationLevelKeys.FIELD_EDUCATION_LEVEL_NAME] == DBNull.Value
                ? string.Empty
                : dr[EducationLevelKeys.FIELD_EDUCATION_LEVEL_NAME].ToString();

            //new fields
            //c._TrainingPlace = dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_TRAINING_PLACE] == DBNull.Value ? string.Empty : dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_TRAINING_PLACE].ToString();
            //c._TrainingDepartment = dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_TRAINING_DEPARTMENT] == DBNull.Value ? string.Empty : dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_TRAINING_DEPARTMENT].ToString();
            //c._Major = dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_MAJOR] == DBNull.Value ? string.Empty : dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_MAJOR].ToString();
            //c._GraduatingYear = dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_GRADUATING_YEAR] == DBNull.Value ? FormatDate.GetSQLDateMinValue : Convert.ToDateTime(dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_GRADUATING_YEAR]);
            //c._Grade = dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_GRADE] == DBNull.Value ? string.Empty : dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_GRADE].ToString();
            //c._Profession = dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_PROFESSION] == DBNull.Value ? string.Empty : dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_PROFESSION].ToString();

            return c;
        }

        private static EmployeeEducationLevelsBLL GenerateFromDataTable(DataRow dr, int itemLastIndex)
        {
            var c = new EmployeeEducationLevelsBLL();

            c.Id = dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_ID] == DBNull.Value
                ? 0
                : int.Parse(dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_ID].ToString());
            c.UserId = dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_USER_ID] == DBNull.Value
                ? 0
                : int.Parse(dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_USER_ID].ToString());
            c.EducationLevelId = dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_EDUCATION_LEVEL_ID] ==
                                 DBNull.Value
                ? 0
                : int.Parse(dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_EDUCATION_LEVEL_ID].ToString());
            c.EducationLevelValue = dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_VALUE] == DBNull.Value
                ? string.Empty
                : dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_VALUE].ToString();
            c.Remark = dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_REMARK] == DBNull.Value
                ? string.Empty
                : dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_REMARK].ToString();

            c.EducationLevelName = dr[EducationLevelKeys.FIELD_EDUCATION_LEVEL_NAME] == DBNull.Value
                ? string.Empty
                : dr[EducationLevelKeys.FIELD_EDUCATION_LEVEL_NAME].ToString();

            c.LastItem = itemLastIndex;

            //new fields
            //c._TrainingPlace = dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_TRAINING_PLACE] == DBNull.Value ? string.Empty : dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_TRAINING_PLACE].ToString();
            //c._TrainingDepartment = dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_TRAINING_DEPARTMENT] == DBNull.Value ? string.Empty : dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_TRAINING_DEPARTMENT].ToString();
            //c._Major = dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_MAJOR] == DBNull.Value ? string.Empty : dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_MAJOR].ToString();
            //c._GraduatingYear = dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_GRADUATING_YEAR] == DBNull.Value ? FormatDate.GetSQLDateMinValue : Convert.ToDateTime(dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_GRADUATING_YEAR]);
            //c._Grade = dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_GRADE] == DBNull.Value ? string.Empty : dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_GRADE].ToString();
            //c._Profession = dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_PROFESSION] == DBNull.Value ? string.Empty : dr[EmployeeEducationLevelKeys.FIELD_EMPLOYEE_EDUCATION_LEVEL_PROFESSION].ToString();

            return c;
        }

        #endregion

        #region new version

        #endregion
    }
}