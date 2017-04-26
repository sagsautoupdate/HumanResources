using System;
using System.Text;

namespace HRMDAL.H2.KeyNames
{
    public sealed class CandidateEducationLevelKeys
    {

        /// <summary>
        ///  some fields in H2_CandidateEducationLevel tables
        /// </summary>
        public const string FIELD_CANDIDATE_EDUCATION_LEVEL_ID = "Id";
        public const string FIELD_CANDIDATE_EDUCATION_LEVEL_CANDIDATE_ID = "CandidateId";
        public const string FIELD_CANDIDATE_EDUCATION_LEVEL_EDUCATION_LEVEL_ID = "EducationLevelId";
        public const string FIELD_CANDIDATE_EDUCATION_LEVEL_VALUE = "EducationLevelValue";
        public const string FIELD_CANDIDATE_EDUCATION_LEVEL_REMARK = "Remark";

        /// <summary>
        /// some store procedures H2_CandidateEducationLevel table
        /// </summary>
        public const string SP_CANDIDATE_EDUCATION_LEVEL_INSERT = "Ins_H2_CandidateEducationLevel";
        public const string SP_CANDIDATE_EDUCATION_LEVEL_UPDATE = "Upd_H2_CandidateEducationLevel";
        public const string SP_CANDIDATE_EDUCATION_LEVEL_DELETE = "Del_H2_CandidateEducationLevel";
        public const string SP_CANDIDATE_EDUCATION_LEVEL_GET_IS_NULL = "Sel_H2_CandidateEducationLevel_IsNull";
        public const string SP_CANDIDATE_EDUCATION_LEVEL_GET_BY_ID = "Sel_H2_CandidateEducationLevel_Id";
    }
}
