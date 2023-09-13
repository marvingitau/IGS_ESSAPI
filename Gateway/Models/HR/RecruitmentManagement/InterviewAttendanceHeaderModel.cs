namespace Gateway.Models.HR.RecruitmentManagement
{
    public class InterviewAttendanceHeaderModel
    {

        public string InterviewNo { get; set; }

        public string InterviewCommitteecode { get; set; }

        public string InterviewJobNo { get; set; }

        public string InterviewDatefrom { get; set; }

        public string InterviewDateto { get; set; }

        public string InterviewTime { get; set; }

        public string InterviewLocation { get; set; }

        public string InterviewChairpersonCode { get; set; }

        public string InterviewPurpose { get; set; }

        public string JobRequisitionNo { get; set; }

        public string CommitteeRemarks { get; set; }

        public bool Closed { get; set; }

        public bool MandatoryDocsRequired { get; set; }

    }
}
