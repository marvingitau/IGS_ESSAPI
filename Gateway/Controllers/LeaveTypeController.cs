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
    public class LeaveTypeController : ControllerBase
    {
        private readonly ILogger<LeaveTypeController> _logger;
        private readonly IWebServiceConnection webservice;

        public LeaveTypeController(ILogger<LeaveTypeController> logger, IWebServiceConnection service)
        {
            _logger = logger;
            webservice = service;
        }

        [HttpPost]
        [Route("create/")]
        public async Task<IActionResult> CreateLeaveType([FromBody] LeaveTypeModel model)
        {
            var leaveType = await webservice
                .HRLeaveManagementWS()
                .CreateLeaveTypeAsync
                (
                    model.Description,
                    model.Gender,
                    model.Days,
                    model.AnnualLeave,
                    model.InclusiveOfNonWorkingDays,
                    model.Balance,
                    model.MaxCarryForwardDays,
                    model.AmountPerDay,
                    model.LeavePlanMandatory,
                    model.AllowNegativeDays,
                    model.AttachLeaveApplicationDoc,
                    model.MaxDaysToBeTaken
                );
            dynamic res = JsonConvert.DeserializeObject<LeaveTypeModel>(leaveType.return_value);
            return Ok(res);
        }



        [HttpGet]
        [Route("view")]
        public async Task<IActionResult> GetAllLeaveTypes()
        {
            var leaveTypes = await webservice.HRLeaveManagementWS().GetLeaveTypesRangeAsync();
            dynamic res = JsonConvert.DeserializeObject<List<LeaveTypeModel>>(leaveTypes.return_value);
            return Ok(res);
        }

        [HttpGet]
        [Route("view/{leaveTypeID}")]
        public async Task<IActionResult> GetLeaveType(string leaveTypeID)
        {
            if (leaveTypeID == null) return BadRequest();
            var leaveType = await webservice.HRLeaveManagementWS().GetLeaveTypeAsync(leaveTypeID);
            dynamic res = JsonConvert.DeserializeObject<LeaveTypeModel>(leaveType.return_value);
            if (res.Code == "N/A") return BadRequest(); // Item was not found.
            return Ok(res);
        }

        [HttpPut]
        [Route("update/{leaveTypeID}")]
        public async Task<IActionResult> UpdateLeaveType(string leaveTypeID, [FromBody] LeaveTypeModel model)
        {
            var leaveType = await webservice
                .HRLeaveManagementWS()
                .UpdateLeaveTypeAsync
                (
                    leaveTypeID,
                    model.Description,
                    model.Gender,
                    model.Days,
                    model.AnnualLeave,
                    model.InclusiveOfNonWorkingDays,
                    model.Balance,
                    model.MaxCarryForwardDays,
                    model.AmountPerDay,
                    model.LeavePlanMandatory,
                    model.AllowNegativeDays,
                    model.AttachLeaveApplicationDoc,
                    model.MaxDaysToBeTaken
                );
            dynamic res = JsonConvert.DeserializeObject<LeaveTypeModel>(leaveType.return_value);
            return Ok(res);
        }


        [HttpDelete]
        [Route("delete/{leaveTypeID}")]
        public async Task<IActionResult> DeleteLeaveType(string leaveTypeID)
        {
            if (leaveTypeID == null) return BadRequest();
            var leaveType = await webservice
                .HRLeaveManagementWS()
                .DeleteLeaveTypeAsync(leaveTypeID);
            // handle for item not found.
            //dynamic res = JsonConvert.DeserializeObject(leaveType.return_value);

            return Ok(leaveType.return_value);
        }


    }
}
