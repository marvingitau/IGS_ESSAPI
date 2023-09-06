namespace Gateway.Models.HR.LeaveManagement
{
    public class LeaveTypeModel
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Gender { get; set; }
        public decimal Days { get; set; }
        public bool AnnualLeave { get; set; }
        public bool InclusiveOfNonWorkingDays { get; set; } 
        public string Balance { get; set; }
        public decimal MaxCarryForwardDays { get; set; }
        public decimal AmountPerDay { get; set; }
        public bool LeavePlanMandatory { get; set; }
        public bool AllowNegativeDays { get; set; }
        public bool AttachLeaveApplicationDoc { get; set; }
        public decimal MaxDaysToBeTaken { get; set; }

    }
}
