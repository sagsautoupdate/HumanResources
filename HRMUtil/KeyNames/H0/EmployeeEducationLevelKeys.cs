namespace HRMUtil.KeyNames.H0
{
    public sealed class EmployeeEducationLevelKeys
    {
        /// <summary>
        ///     some fields in H0_EmployeeEducationLevel tables
        /// </summary>
        public const string FIELD_EMPLOYEE_EDUCATION_LEVEL_ID = "Id";

        public const string FIELD_EMPLOYEE_EDUCATION_LEVEL_USER_ID = "UserId";
        public const string FIELD_EMPLOYEE_EDUCATION_LEVEL_EDUCATION_LEVEL_ID = "EducationLevelId";
        public const string FIELD_EMPLOYEE_EDUCATION_LEVEL_VALUE = "EducationLevelValue";
        public const string FIELD_EMPLOYEE_EDUCATION_LEVEL_REMARK = "Remark";

        //new fields
        public const string FIELD_EMPLOYEE_EDUCATION_LEVEL_TRAINING_PLACE = "TrainingPlace";
        public const string FIELD_EMPLOYEE_EDUCATION_LEVEL_TRAINING_DEPARTMENT = "TrainingDepartment";
        public const string FIELD_EMPLOYEE_EDUCATION_LEVEL_GRADUATING_YEAR = "GraduatingYear";
        public const string FIELD_EMPLOYEE_EDUCATION_LEVEL_GRADE = "Grade";
        public const string FIELD_EMPLOYEE_EDUCATION_LEVEL_MAJOR = "Major";
        public const string FIELD_EMPLOYEE_EDUCATION_LEVEL_PROFESSION = "Profession";

        /// <summary>
        ///     some store procedures H0_EmployeeEducationLevel table
        /// </summary>
        public const string SP_EMPLOYEE_EDUCATION_LEVEL_INSERT = "Ins_H0_EmployeeEducationLevel";

        public const string SP_EMPLOYEE_EDUCATION_LEVEL_UPDATE = "Upd_H0_EmployeeEducationLevel";
        public const string SP_EMPLOYEE_EDUCATION_LEVEL_UPDATEV1 = "Upd_H0_EmployeeEducationLevel_V1";
        public const string SP_EMPLOYEE_EDUCATION_LEVEL_UPDATE_HIGHEST = "Upd_H0_EmployeeEducationLevel_Highest";
        public const string SP_EMPLOYEE_EDUCATION_LEVEL_DELETE = "Del_H0_EmployeeEducationLevel";
        public const string SP_EMPLOYEE_EDUCATION_LEVEL_GET_BY_ID = "Sel_H0_EmployeeEducationLevel_Id";
    }
}