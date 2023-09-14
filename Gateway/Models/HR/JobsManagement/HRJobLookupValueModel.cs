namespace Gateway.Models.HR.JobsManagement
{
    public class HRJobLookupValueModel
    {
        public string Option { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public bool Blocked { get; set; }

        public string RequiredStage { get; set; }
    }
}
