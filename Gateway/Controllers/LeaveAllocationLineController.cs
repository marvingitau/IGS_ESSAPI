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
    public class LeaveAllocationLineController : ControllerBase
    {
        public readonly ILogger<LeaveAllocationLineController> _logger;
        public readonly IWebServiceConnection webService;

        public LeaveAllocationLineController(ILogger<LeaveAllocationLineController> logger, IWebServiceConnection webService)
        {
            _logger = logger;
            this.webService = webService;
        }


        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateLeaveAllocationLine([FromBody] LeaveAllocationLineModel model)
        {
            var res = await webService
                .HRLeaveManagementWS()
                .CreateLeaveAllocationLineAsync(
                    model.LeaveAllocationHeaderNo,
                    model.EmployeeNo,
                    model.EntryType,
                    model.Description
                );
            dynamic allocation = JsonConvert.DeserializeObject<LeaveAllocationLineModel>(res.return_value);
            return Ok(allocation);
        }


        [HttpGet]
        [Route("view")]
        public async Task<IActionResult> GetAllLeaveAllocations()
        {
            var res = await webService
                .HRLeaveManagementWS()
                .GetLeaveAllocationLinesRangeAsync();
            dynamic allocations = JsonConvert.DeserializeObject<List<LeaveAllocationLineModel>>(res.return_value);

            return Ok(new { allocations });
        }

        [HttpGet]
        [Route("generatelines/{leaveAllocationHeaderID}")]
        public async Task<IActionResult> GenerateLeaveAllocationLines(string leaveAllocationHeaderID)
        {
            var res = await webService
                .HRLeaveManagementWS()
                .GetLeaveAllocationLineByHeaderAsync(leaveAllocationHeaderID);
            dynamic allocations = JsonConvert.DeserializeObject<List<LeaveAllocationLineModel>>(res.return_value);

            return Ok(allocations);
        }

        [HttpGet]
        [Route("view/{leaveAllocationHeaderID}/{employeeNo}")]
        public async Task<IActionResult> GetLeaveAllocations(string leaveAllocationHeaderID, string employeeNo)
        {
            var res = await webService
                .HRLeaveManagementWS()
                .GetLeaveAllocationLineAsync(leaveAllocationHeaderID, employeeNo);
            dynamic allocations = JsonConvert.DeserializeObject<LeaveAllocationLineModel>(res.return_value);

            return Ok(allocations);
        }

        [HttpPut]
        [Route("update/leaveAllocationHeaderNo/EmployeeNo")]
        public async Task<IActionResult> UpdateLeaveAllocation(string leaveAllocationHeaderNo, string EmployeeNo, [FromBody] LeaveAllocationLineModel model)
        {
            var res = await webService
                .HRLeaveManagementWS()
                .UpdateLeaveAllocationLineAsync(
                    leaveAllocationHeaderNo,
                    EmployeeNo,
                    model.LeaveAllocationHeaderNo,
                    model.EmployeeNo,
                    model.EntryType,
                    model.Description
                );
            dynamic allocation = JsonConvert.DeserializeObject<LeaveAllocationLineModel>(res.return_value);
            return Ok(allocation);
        }

        [HttpDelete]
        [Route("delete/{leaveAllocationHeaderID}/{employeeID}")]
        public async Task<IActionResult> DeleteLeaveAllocations(string leaveAllocationHeaderID, string employeeID)
        {
            var res = await webService
                .HRLeaveManagementWS()
                .DeleteLeaveAllocationLineAsync(leaveAllocationHeaderID, employeeID);

            return Ok(res.return_value);

        }
    }
    
}
