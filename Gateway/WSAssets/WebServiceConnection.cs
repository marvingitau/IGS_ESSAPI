using EmployeeAccountWS;
using HREmployeeWS;
using IncidentsWS;
using Microsoft.Extensions.Options;
using System.ServiceModel;

namespace Gateway.WSAssets
{
    public class WebServiceConnection : IWebServiceConnection
    {
        private readonly IOptions<WebServiceModel> config;
        private string GetSeviceURL(string service) => 
            config.Value.Protocol 
            + config.Value.DynamicsServer
            + ":" 
            + config.Value.SOAPPort 
            + "/" 
            + config.Value.DynamicsServiceName 
            + "/WS/" + config.Value.CompanyURLName 
            + "/Codeunit/"
            +service;

        public WebServiceConnection(IOptions<WebServiceModel> config)
        {
            this.config = config;
        }


        public EmployeeAccountWebService_PortClient EmployeeAccount()
        {
            EmployeeAccountWebService_PortClient employeeAccountWebService = new EmployeeAccountWebService_PortClient(EmployeeAccountWebService_PortClient.EndpointConfiguration.EmployeeAccountWebService_Port);
            var url = GetSeviceURL("EmployeeAccountWebService");
            employeeAccountWebService.Endpoint.Address = new EndpointAddress(url);
            //employeeAccountWebService.ClientCredentials.Windows.ClientCredential.UserName = "MARVIN";
            //employeeAccountWebService.ClientCredentials.Windows.ClientCredential.Password = "husl2f5yqw";
            employeeAccountWebService.ClientCredentials.UserName.UserName = config.Value.Username;
            employeeAccountWebService.ClientCredentials.UserName.Password = config.Value.Password;

            return employeeAccountWebService;
        }


        public HRIncidentsWS_PortClient HRIncidentsWS()
        {
            HRIncidentsWS_PortClient IncidentsWebService = new HRIncidentsWS_PortClient(
                HRIncidentsWS_PortClient
                .EndpointConfiguration
                .HRIncidentsWS_Port
                );
            var url = GetSeviceURL("HRIncidentsWS");
            IncidentsWebService.Endpoint.Address = new EndpointAddress(url);
   
            return IncidentsWebService;
        }

        public HREmployeeWebService_PortClient HREmployeeWS()
        {
            HREmployeeWebService_PortClient EmployeeWebService = new HREmployeeWebService_PortClient(
                HREmployeeWebService_PortClient
                .EndpointConfiguration
                .HREmployeeWebService_Port
                );
            var url = GetSeviceURL("HREmployeeWebService");
            EmployeeWebService.Endpoint.Address = new EndpointAddress(url);

            return EmployeeWebService;
        }




    }
}
