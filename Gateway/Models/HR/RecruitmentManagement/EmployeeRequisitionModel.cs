namespace Gateway.Models.HR.RecruitmentManagement
{
    public class EmployeeRequisitionModel
    {
        public string No { get; set; }
        public string JobNo { get; set; }
        public string JobTitle { get; set; }
        public string EmpRequisitionDescription { get; set; }
        public int RequestedEmployees { get; set; }
        public string RequisitionType { get; set; }
        public string ClosingDate { get; set; }
        public bool JobAdvertPublished { get; set; }
    }
}
