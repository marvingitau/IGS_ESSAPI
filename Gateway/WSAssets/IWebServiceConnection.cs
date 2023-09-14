using EmployeeAccountWS;
using HREmployeeWS;
using IncidentsWS;
using LeaveManagementWS;
using RecruitmentManagementWS;
using JobsManagementWS;

namespace Gateway.WSAssets
{
    public interface IWebServiceConnection
    {
        EmployeeAccountWebService_PortClient EmployeeAccount();
        HRIncidentsWS_PortClient HRIncidentsWS();
        HREmployeeWebService_PortClient HREmployeeWS();
        HRLeaveManagementWS_PortClient HRLeaveManagementWS();
        HRRecruitmentManagementWS_PortClient HRRecruitmentManagementWS();
        HRJobsManagementWS_PortClient HRJobsManagementWS();

    }
}