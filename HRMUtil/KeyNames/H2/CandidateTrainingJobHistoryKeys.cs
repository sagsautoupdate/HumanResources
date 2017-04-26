namespace HRMUtil.KeyNames.H2
{
    public sealed class CandidateTrainingJobHistoryKeys
    {
        /// <summary>
        ///     some fields in H2_CandidateTrainingJobHistory tables
        /// </summary>
        public const string Filed_CandidateTrainingJobHistory_CandidateTrainingJobHistoryId =
            "CandidateTrainingJobHistoryId";

        public const string Filed_CandidateTrainingJobHistory_CandidateId = "CandidateId";
        public const string Filed_CandidateTrainingJobHistory_Training_Job = "Training_Job";
        public const string Filed_CandidateTrainingJobHistory_Year = "Year";
        public const string Filed_CandidateTrainingJobHistory_School_Position = "School_Position";
        public const string Filed_CandidateTrainingJobHistory_Major_Salary = "Major_Salary";
        public const string Filed_CandidateTrainingJobHistory_GraduateYear_LeaveReason = "GraduateYear_LeaveReason";
        public const string Filed_CandidateTrainingJobHistory_Experience = "Experience";
        public const string Filed_CandidateTrainingJobHistory_Type = "Type";

        /// <summary>
        ///     some store procedures H2_CandidateEducationLevel table
        /// </summary>
        public const string Sp_Ins_H2_CandidateTrainingJobHistory = "Ins_H2_CandidateTrainingJobHistory";

        public const string Sp_Upd_H2_CandidateTrainingJobHistory = "Upd_H2_CandidateTrainingJobHistory";
        public const string Sp_Del_H2_CandidateTrainingJobHistory = "Del_H2_CandidateTrainingJobHistory";

        public const string Sp_Sel_H2_CandidateTrainingJobHistory_By_CandidateId_Type =
            "Sel_H2_CandidateTrainingJobHistory_By_CandidateId_Type";
    }
}