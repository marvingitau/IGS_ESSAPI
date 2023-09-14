namespace Gateway.Models.HR.JobsManagement
{
    public class HRJobQualificationModel
    {
        public string JobNo { get; set; }

        public string QualificationCode { get; set; }

        public string Description { get; set; }

        public bool Mandatory { get; set; }

        public int LineNo { get; set; }
    }
}
