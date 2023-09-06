namespace Gateway.Models.HR.LeaveManagement
{
    public class LeaveApplicationModel
    {
        public string No { get; set; }
        public string PostingDate { get; set; }
        public string EmployeeNo { get; set; }
        public string LeaveType { get; set; }
        public string LeavePeriod { get; set; }
        public string LeaveStartDate { get; set; }
        public decimal DaysApplied { get; set; }
        public decimal DaysApproved { get; set; }
        public string RelieverNo { get; set; }
        public string Status { get; set; }

    }
}
