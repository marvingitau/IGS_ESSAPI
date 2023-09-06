
using System;

namespace Gateway.Models.HR.LeaveManagement
{
    public class LeavePeriodModel
    {
        
        public string Code { get; set; }
        public string Name { get; set; }
        // Converting string to datetime raises errors.
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool Closed { get; set; }
        public int LeaveYear { get; set; }
        public bool EnableLeavePlanning { get; set; }
        public string LeavePlanningEndDate { get; set; }
        public bool EnableLeaveApplication { get; set; }
        public bool EnableLeaveCarryover { get; set; }
        public string LeaveCarryoverEndDate { get; set; }
        public bool EnableLeaveReimbursement { get; set; }
        public string LeaveReimbursementEndDate { get; set; }

    }
}
