using System;
using System.Text;

namespace HRMDAL.H2.KeyNames
{
    public sealed class EducationLevelKeys
    {
        /// <summary>
        ///  some fields in H2_EducationLevel tables
        /// </summary>
        public const string FIELD_EDUCATION_LEVEL_ID = "EducationLevelId";
        public const string FIELD_EDUCATION_LEVEL_NAME = "Name";
        public const string FIELD_EDUCATION_LEVEL_REMARK = "Remark";

        /// <summary>
        /// some store procedures H2_EducationLevel table
        /// </summary>
        public const string SP_EDUCATION_LEVEL_INSERT = "Ins_H2_EducationLevel";
        public const string SP_EDUCATION_LEVEL_UPDATE = "Upd_H2_EducationLevel";
        public const string SP_EDUCATION_LEVEL_DELETE = "Del_H2_EducationLevel";
        public const string SP_EDUCATION_LEVEL_GET_ALL = "Sel_H2_EducationLevel_All";        

    }
}
