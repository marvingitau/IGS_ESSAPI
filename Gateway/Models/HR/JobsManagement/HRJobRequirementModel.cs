namespace Gateway.Models.HR.JobsManagement
{
    public class HRJobRequirementModel
    {

        public string JobNo { get; set; }

        public string RequirementCode { get; set; }

        public string Description { get; set; }

        public bool Mandatory { get; set; }

        public int LineNo { get; set; }

        public int NoofYears { get; set; }
    }
}
