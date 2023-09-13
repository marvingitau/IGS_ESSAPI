using Gateway.Models.HR.RecruitmentManagement;
using Gateway.Models.Responses;
using Gateway.WSAssets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterviewCommitteeDeptLineController : ControllerBase
    {

        private readonly ILogger<InterviewCommitteeDeptLineController> logger;
        private readonly IWebServiceConnection webService;

        public InterviewCommitteeDeptLineController(ILogger<InterviewCommitteeDeptLineController> logger, IWebServiceConnection webService)
        {
            this.logger = logger;
            this.webService = webService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateInterviewCommitteeDeptLine([FromBody] InterviewCommitteeDeptLineModel model)
        {
            var InterviewCommitteeDeptLine = await webService
                    .HRRecruitmentManagementWS()
                    .CreateInterviewCommitteeDeptLineAsync(
                        model.DepartmentCode,
                        model.EmployeeNo
                    );


            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(InterviewCommitteeDeptLine.return_value);

            if (res.Status != 200) return BadRequest(res);
            var payload = JsonConvert.DeserializeObject<InterviewCommitteeDeptLineModel>(res.Payload);

            return Ok(payload);

        }

        [HttpGet]
        [Route("view")]
        public async Task<IActionResult> GetAllInterviewCommitteeDeptLines()
        {
            var InterviewCommitteeDeptLine = await webService
                .HRRecruitmentManagementWS()
                .GetInterviewCommitteeDeptLineRangeAsync();
            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(InterviewCommitteeDeptLine.return_value);

            if (res.Status != 200) return BadRequest(res.Message);

            var payload = JsonConvert.DeserializeObject<List<InterviewCommitteeDeptLineModel>>(res.Payload);
            return Ok(payload);
        }

        [HttpGet]
        [Route("view/{InterviewCommitteeDeptLineID}/{employeeID}")]
        public async Task<IActionResult> GetLeaveApplication(string InterviewCommitteeDeptLineID, string employeeID)
        {
            if (InterviewCommitteeDeptLineID == null) return BadRequest();
            if (employeeID == null) return BadRequest();

            var InterviewCommitteeDeptLine = await webService
                .HRRecruitmentManagementWS()
                .GetInterviewCommitteeDeptLineAsync(InterviewCommitteeDeptLineID, employeeID);

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(InterviewCommitteeDeptLine.return_value);

            if (res.Status != 200) return BadRequest(res);

            var payload = JsonConvert.DeserializeObject<InterviewCommitteeDeptLineModel>(res.Payload);
            return Ok(payload);
        }


        [HttpPut]
        [Route("update/{InterviewCommitteeDeptLineID}/{employeeID}")]
        public async Task<IActionResult> UpdateInterviewCommitteeDeptLine(string InterviewCommitteeDeptLineID, string employeeID, [FromBody] InterviewCommitteeDeptLineModel model)
        {
            if (InterviewCommitteeDeptLineID == null) return BadRequest();
            if (employeeID == null) return BadRequest();

            var InterviewCommitteeDeptLine = await webService
                .HRRecruitmentManagementWS()
                .UpdateInterviewCommitteeDeptLineAsync(
                        InterviewCommitteeDeptLineID,
                        employeeID,
                        model.DepartmentCode,
                        model.EmployeeNo
                );

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(InterviewCommitteeDeptLine.return_value);

            if (res.Status != 200) return BadRequest(res);
            var payload = JsonConvert.DeserializeObject<InterviewCommitteeDeptLineModel>(res.Payload);

            return Ok(payload);
        }


        [HttpDelete]
        [Route("delete/{InterviewCommitteeDeptLineID}/{employeeID}")]
        public async Task<IActionResult> DeleteLeaveApplication(string InterviewCommitteeDeptLineID, string employeeID)
        {
            if (InterviewCommitteeDeptLineID == null) return BadRequest();
            if (employeeID == null) return BadRequest();

            var InterviewCommitteeDeptLine = await webService
            .HRRecruitmentManagementWS()
            .DeleteInterviewCommitteeDeptLineAsync(InterviewCommitteeDeptLineID, employeeID);

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(InterviewCommitteeDeptLine.return_value);
            if (res.Status != 200) return BadRequest(res.Message);



            return Ok(InterviewCommitteeDeptLine.return_value);
        }


    }
}
