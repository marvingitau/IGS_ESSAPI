using Gateway.Models.HR.LeaveManagement;
using Gateway.WSAssets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gateway.Controllers
{
    /**
     * These Endpoints should only be accessible to certain users.
     * Include user validation 
     */
    [ApiController]
    [Route("api/[controller]")]
    public class LeavePeriodController : ControllerBase
    {
        public readonly ILogger<LeavePeriodController> _logger;
        public readonly IWebServiceConnection webservices;

        public LeavePeriodController(ILogger<LeavePeriodController> logger, IWebServiceConnection webservices)
        {
            _logger = logger;
            this.webservices = webservices;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateLeavePeriod([FromBody] LeavePeriodModel model)
        {
            // US date format - Month/Date/Year.

            var res = await webservices
                .HRLeaveManagementWS()
                .CreateLeavePeriodAsync(
                    model.Name,
                    model.StartDate,
                    model.EndDate,
                    model.Closed,
                    model.LeaveYear,
                    model.EnableLeavePlanning,
                    model.LeavePlanningEndDate,
                    model.EnableLeaveApplication,
                    model.EnableLeaveCarryover,
                    model.LeaveCarryoverEndDate,
                    model.EnableLeaveReimbursement,
                    model.LeaveReimbursementEndDate
                );
            dynamic period = JsonConvert.DeserializeObject<LeavePeriodModel>(res.return_value);
            return Ok(period);
        }


        [HttpGet]
        [Route("view")]
        public async Task<IActionResult> GetAllLeavePeriods()
        {
            var leavePeriods = await webservices
                .HRLeaveManagementWS()
                .GetLeavePeriodsRangeAsync();

            dynamic res = JsonConvert.DeserializeObject<List<LeavePeriodModel>>(leavePeriods.return_value);

            return Ok(new { res });
        }

        [HttpGet]
        [Route("view/{LeavePeriodID}")]
        public async Task<IActionResult> GetLeavePeriod(string leavePeriodID)
        {

            var res = await webservices
                .HRLeaveManagementWS()
                .GetLeavePeriodAsync(leavePeriodID);
            dynamic period = JsonConvert.DeserializeObject<LeavePeriodModel>(res.return_value);

            return Ok(period);
        }

        [HttpPut]
        [Route("update/{LeavePeriodID}")]
        public async Task<IActionResult> UpdateLeavePeriod(string LeavePeriodID, [FromBody] LeavePeriodModel model)
        {
            var res = await webservices
                .HRLeaveManagementWS()
                .UpdateLeavePeriodAsync(
                    LeavePeriodID,
                    model.Name,
                    model.StartDate,
                    model.EndDate,
                    model.Closed,
                    model.LeaveYear,
                    model.EnableLeavePlanning,
                    model.LeavePlanningEndDate,
                    model.EnableLeaveApplication,
                    model.EnableLeaveCarryover,
                    model.LeaveCarryoverEndDate,
                    model.EnableLeaveReimbursement,
                    model.LeaveReimbursementEndDate
                );
            dynamic period = JsonConvert.DeserializeObject<LeavePeriodModel>(res.return_value);

            return Ok(period);
        }

        [HttpDelete]
        [Route("delete/{LeavePeriodID}")]
        public async Task<IActionResult> DeleteLeavePeriod(string leavePeriodID)
        {

            var res = await webservices
                .HRLeaveManagementWS()
                .DeleteLeavePeriodAsync(leavePeriodID);
            dynamic period = JsonConvert.DeserializeObject(res.return_value);

            return Ok(period);
        }

    }
}
