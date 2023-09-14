namespace Gateway.Models.HR.JobsManagement
{
    public class HRJobModel
    {


        public string No { get; set; }

        public string JobTitle { get; set; }

        public string JobGrade { get; set; }

        public int MaximumPositions { get; set; }

        public int OccupiedPositions { get; set; }

        public int VacantPositions { get; set; }

        public string SupervisorJobNo { get; set; }

        public string SupervisorJobTitle { get; set; }

        public string AppraisalLevel { get; set; }

        public bool Active { get; set; }

        public string Status { get; set; }


    }
}
