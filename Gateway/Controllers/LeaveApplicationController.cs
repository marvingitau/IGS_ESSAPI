using Gateway.Models.HR.LeaveManagement;
using Gateway.WSAssets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog.Core;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class LeaveApplicationController : ControllerBase
    {
        private readonly ILogger<LeaveApplicationController> _logger;
        private readonly IWebServiceConnection webservice;

        public LeaveApplicationController(ILogger<LeaveApplicationController> logger, IWebServiceConnection service)
        {
            _logger = logger;
            webservice = service;
        }

        [HttpPost]
        [Route("create/")]
        public async Task<IActionResult> CreateLeaveApplication([FromBody] LeaveApplicationModel model)
        {
            var LeaveApplication = await webservice
                .HRLeaveManagementWS()
                .CreateLeaveApplicationAsync
                (
                    model.PostingDate,
                    model.EmployeeNo,
                    model.LeaveType,
                    model.LeavePeriod,
                    model.LeaveStartDate,
                    model.DaysApplied,
                    model.DaysApproved,
                    model.RelieverNo,
                    model.Status
                );

            dynamic res = JsonConvert.DeserializeObject<LeaveApplicationModel>(LeaveApplication.return_value);
            return Ok(res);
        }



        [HttpGet]
        [Route("view")]
        public async Task<IActionResult> GetAllLeaveApplications()
        {
            var LeaveApplications = await webservice.HRLeaveManagementWS().GetLeaveApplicationsRangeAsync();
            dynamic res = JsonConvert.DeserializeObject<List<LeaveApplicationModel>>(LeaveApplications.return_value);
            return Ok(res);
        }

        [HttpGet]
        [Route("view/{LeaveApplicationID}")]
        public async Task<IActionResult> GetLeaveApplication(string LeaveApplicationID)
        {
            if (LeaveApplicationID == null) return BadRequest();
            var LeaveApplication = await webservice.HRLeaveManagementWS().GetLeaveApplicationAsync(LeaveApplicationID);
            dynamic res = JsonConvert.DeserializeObject<LeaveApplicationModel>(LeaveApplication.return_value);
            if (res.No == "N/A") return BadRequest(); // Item was not found.
            return Ok(res);
        }

        [HttpPut]
        [Route("update/{LeaveApplicationID}")]
        public async Task<IActionResult> UpdateLeaveApplication(string LeaveApplicationID, [FromBody] LeaveApplicationModel model)
        {
            var LeaveApplication = await webservice
                .HRLeaveManagementWS()
                .UpdateLeaveApplicationAsync
                (
                    LeaveApplicationID,
                    model.PostingDate,
                    model.EmployeeNo,
                    model.LeaveType,
                    model.LeavePeriod,
                    model.LeaveStartDate,
                    model.DaysApplied,
                    model.DaysApproved,
                    model.RelieverNo,
                    model.Status
                );
            dynamic res = JsonConvert.DeserializeObject<LeaveApplicationModel>(LeaveApplication.return_value);
            return Ok(res);
        }


        [HttpDelete]
        [Route("delete/{LeaveApplicationID}")]
        public async Task<IActionResult> DeleteLeaveApplication(string LeaveApplicationID)
        {
            if (LeaveApplicationID == null) return BadRequest();
            var LeaveApplication = await webservice
                .HRLeaveManagementWS()
                .DeleteLeaveApplicationAsync(LeaveApplicationID);
            // handle for item not found.
            //dynamic res = JsonConvert.DeserializeObject(LeaveApplication.return_value);

            return Ok(LeaveApplication.return_value);
        }


    }
}
