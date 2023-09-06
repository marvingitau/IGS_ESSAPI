using EmployeeAccountWS;
using HREmployeeWS;
using IncidentsWS;
using LeaveManagementWS;

namespace Gateway.WSAssets
{
    public interface IWebServiceConnection
    {
        EmployeeAccountWebService_PortClient EmployeeAccount();
        HRIncidentsWS_PortClient HRIncidentsWS();
        HREmployeeWebService_PortClient HREmployeeWS();
        HRLeaveManagementWS_PortClient HRLeaveManagementWS();
    }
}