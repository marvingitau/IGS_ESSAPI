﻿using EmployeeAccountWS;
using HREmployeeWS;
using IncidentsWS;
using LeaveManagementWS;
using RecruitmentManagementWS;
using JobsManagementWS;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Options;
using System.Security.Policy;
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


        public HRLeaveManagementWS_PortClient HRLeaveManagementWS()
        {
            HRLeaveManagementWS_PortClient leaveManagementWS = new HRLeaveManagementWS_PortClient(
                HRLeaveManagementWS_PortClient
                .EndpointConfiguration
                .HRLeaveManagementWS_Port);
            var url = GetSeviceURL("HRLeaveManagementWS");
            
            leaveManagementWS.Endpoint.Address = new EndpointAddress(url);
            return leaveManagementWS;
        }

        public HRRecruitmentManagementWS_PortClient HRRecruitmentManagementWS()
        {
            HRRecruitmentManagementWS_PortClient recruitmentMgtWs = new HRRecruitmentManagementWS_PortClient(
                HRRecruitmentManagementWS_PortClient
                .EndpointConfiguration
                .HRRecruitmentManagementWS_Port);
            var url = GetSeviceURL("HRRecruitmentManagementWS");
            recruitmentMgtWs.Endpoint.Address = new EndpointAddress(url);
            return recruitmentMgtWs;
        }

        

        public HRJobsManagementWS_PortClient HRJobsManagementWS()
        {
            HRJobsManagementWS_PortClient jobsMgtWs = new HRJobsManagementWS_PortClient(
                HRJobsManagementWS_PortClient
                .EndpointConfiguration
                .HRJobsManagementWS_Port);
            var url = GetSeviceURL("HRJobsManagementWS");
            jobsMgtWs.Endpoint.Address = new EndpointAddress(url);
            return jobsMgtWs;
        }
    }
}
