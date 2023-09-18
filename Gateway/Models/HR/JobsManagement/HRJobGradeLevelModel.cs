namespace Gateway.Models.HR.JobsManagement
{
    public class HRJobGradeLevelModel
    {

        public string JobGrade { get; set; }

        public string JobGradeLevel { get; set; }

        public decimal BasicPayAmount { get; set; }

        public decimal BasicPayDifference { get; set; }

        public string AllowanceCode { get; set; }

        public string AllowanceDescription { get; set; }

        public decimal AllowanceAmount { get; set; }

        public bool AllowanceSetup { get; set; }

        public int Sequence { get; set; }

    }
}
