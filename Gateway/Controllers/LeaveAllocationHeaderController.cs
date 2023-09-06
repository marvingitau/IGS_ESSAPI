using Gateway.Models.HR.LeaveManagement;
using Gateway.WSAssets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveAllocationHeaderController : ControllerBase
    {
        public readonly ILogger<LeaveAllocationHeaderController> _logger;
        public readonly IWebServiceConnection webService;

        public LeaveAllocationHeaderController(ILogger<LeaveAllocationHeaderController> logger, IWebServiceConnection webService)
        {
            _logger = logger;
            this.webService = webService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateLeaveAllocationHeader([FromBody] LeaveAllocationHeaderModel model)
        {
            var res = await webService
                .HRLeaveManagementWS()
                .CreateLeaveAllocationHeaderAsync(
                    model.PostingDate,
                    model.LeaveType,
                    model.LeavePeriod,
                    model.Description,
                    model.Status
                );
            dynamic allocation = JsonConvert.DeserializeObject<LeaveAllocationHeaderModel>(res.return_value);
            return Ok(allocation);
        }


        [HttpGet]
        [Route("view")]
        public async Task<IActionResult> GetAllLeaveAllocations()
        {
            var res = await webService
                .HRLeaveManagementWS()
                .GetLeaveAllocationHeadersRangeAsync();
            dynamic allocations = JsonConvert.DeserializeObject<List<LeaveAllocationHeaderModel>>(res.return_value);

            return Ok(new { allocations });
        }

        [HttpGet]
        [Route("view/{leaveAllocationID}")]
        public async Task<IActionResult> GetLeaveAllocations(string leaveAllocationID)
        {
            var res = await webService
                .HRLeaveManagementWS()
                .GetLeaveAllocationHeaderAsync(leaveAllocationID);
            dynamic allocations = JsonConvert.DeserializeObject<LeaveAllocationHeaderModel>(res.return_value);

            return Ok(allocations);
        }

        [HttpPut]
        [Route("update/{leaveAllocationID}")]
        public async Task<IActionResult> UpdateLeaveAllocation(string leaveAllocationID, [FromBody] LeaveAllocationHeaderModel model)
        {
            var res = await webService
                .HRLeaveManagementWS()
                .UpdateLeaveAllocationHeaderAsync(
                    leaveAllocationID,
                    model.PostingDate,
                    model.LeaveType,
                    model.LeavePeriod,
                    model.Description,
                    model.Status
                );
            dynamic allocation = JsonConvert.DeserializeObject<LeaveAllocationHeaderModel>(res.return_value);
            return Ok(allocation);
        }

        [HttpDelete]
        [Route("delete/{leaveAllocationID}")]
        public async Task<IActionResult> DeleteLeaveAllocations(string leaveAllocationID)
        {
            var res = await webService
                .HRLeaveManagementWS()
                .DeleteLeaveAllocationHeaderAsync(leaveAllocationID);

            return Ok(res.return_value);

        }
    }
}
