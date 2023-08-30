using EmployeeAccountWS;
using HREmployeeWS;
using IncidentsWS;

namespace Gateway.WSAssets
{
    public interface IWebServiceConnection
    {
        EmployeeAccountWebService_PortClient EmployeeAccount();
        HRIncidentsWS_PortClient HRIncidentsWS();
        HREmployeeWebService_PortClient HREmployeeWS();
    }
}