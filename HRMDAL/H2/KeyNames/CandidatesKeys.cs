using System;
using System.Text;

namespace HRMDAL.H2.KeyNames
{
    public sealed class CandidatesKeys
    {
        /// <summary>
        ///  some fields in H2_Candidates tables
        /// </summary>
        public const string FIELD_CANDIDATE_ID = "CandidateId";
        public const string FIELD_CANDIDATE_SESSION_ID = "SessionId";
        public const string FIELD_CANDIDATE_ORDER_NUMBER = "OrderNumber";
        public const string FIELD_CANDIDATE_LASTNAME = "LastName";
        public const string FIELD_CANDIDATE_FIRSTNAME = "FirstName";
        public const string FIELD_CANDIDATE_DATE_OF_BIRTH = "DayOfBirth";
        public const string FIELD_CANDIDATE_MONTH_OF_BIRTH = "MonthOfBirth";
        public const string FIELD_CANDIDATE_YEAR_OF_BIRTH = "YearOfBirth";
        public const string FIELD_CANDIDATE_SEX = "Sex";
        public const string FIELD_CANDIDATE_EDUCATION_LEVEL_ID = "EducationLevelId";
        public const string FIELD_CANDIDATE_EXPERIENCE = "Experience";
        public const string FIELD_CANDIDATE_HEIGHT = "Height";
        public const string FIELD_CANDIDATE_HOME_PHONE = "HomePhone";
        public const string FIELD_CANDIDATE_HAND_PHONE = "HandPhone";
        public const string FIELD_CANDIDATE_REMARK = "Remark";
        public const string FIELD_CANDIDATE_RESULT = "Result";

        public const string FIELD_CANDIDATE_POSITION_ID = "PositionId";
        
        

        /// <summary>
        /// some store procedures H2_Candidates table
        /// </summary>
        public const string SP_CANDIDATE_INSERT = "Ins_H2_Candidates";
        public const string SP_CANDIDATE_UPDATE = "Upd_H2_Candidates";
        public const string SP_CANDIDATE_DELETE = "Del_H2_Candidates";
        public const string SP_CANDIDATE_UPDATE_RESULT = "Upd_H2_Candidates_Result";
        public const string SP_CANDIDATE_GET_BY_FILTER = "Sel_H2_Candidate_Filter";
        public const string SP_CANDIDATE_GET_BY_ID = "Sel_H2_Candidate_By_Id";
        public const string SP_CANDIDATE_GET_BY_ALL = "Sel_H2_Candidate_All";
    }
}
