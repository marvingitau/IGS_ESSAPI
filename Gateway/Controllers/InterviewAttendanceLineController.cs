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
    public class InterviewAttendanceLineController : ControllerBase 
    {
        private readonly ILogger<InterviewAttendanceLineController> logger;
        private readonly IWebServiceConnection webService;

        public InterviewAttendanceLineController(ILogger<InterviewAttendanceLineController> logger, IWebServiceConnection webService)
            {
                this.logger = logger;
                this.webService = webService;
            }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateInterviewAttendanceLine([FromBody] InterviewAttendanceLineModel model)
        {
            var InterviewAttendanceLine = await webService
                    .HRRecruitmentManagementWS()
                    .CreateInterviewAttendanceLineAsync(
                        model.InterviewNo,
                        model.EmployeeNo,
                        model.Comments,
                        model.Closed
                    );


            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(InterviewAttendanceLine.return_value);

            if (res.Status != 200) return BadRequest(res);
            var payload = JsonConvert.DeserializeObject<InterviewAttendanceLineModel>(res.Payload);

            return Ok(payload);

        }

        [HttpGet]
        [Route("view")]
        public async Task<IActionResult> GetAllInterviewAttendanceLines()
        {
            var InterviewAttendanceLine = await webService
                .HRRecruitmentManagementWS()
                .GetInterviewAttendanceLineRangeAsync();
            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(InterviewAttendanceLine.return_value);

            if (res.Status != 200) return BadRequest(res.Message);

            var payload = JsonConvert.DeserializeObject<List<InterviewAttendanceLineModel>>(res.Payload);
            return Ok(payload);
        }

        [HttpGet]
        [Route("view/{interviewAttendanceLineID}/{employeeID}")]
        public async Task<IActionResult> GetLeaveApplication(string interviewAttendanceLineID, string employeeID)
        {
            if (interviewAttendanceLineID == null) return BadRequest();
            if (employeeID == null) return BadRequest();

            var InterviewAttendanceLine = await webService
                .HRRecruitmentManagementWS()
                .GetInterviewAttendanceLineAsync(interviewAttendanceLineID, employeeID);

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(InterviewAttendanceLine.return_value);

            if (res.Status != 200) return BadRequest(res);

            var payload = JsonConvert.DeserializeObject<InterviewAttendanceLineModel>(res.Payload);
            return Ok(payload);
        }


        [HttpPut]
        [Route("update/{interviewAttendanceLineID}/{employeeID}")]
        public async Task<IActionResult> UpdateInterviewAttendanceLine(string interviewAttendanceLineID, string employeeID, [FromBody] InterviewAttendanceLineModel model)
        {
            if (interviewAttendanceLineID == null) return BadRequest();
            if (employeeID == null) return BadRequest();

            var InterviewAttendanceLine = await webService
                .HRRecruitmentManagementWS()
                .UpdateInterviewAttendanceLineAsync(
                        interviewAttendanceLineID,
                        employeeID,
                        model.InterviewNo,
                        model.EmployeeNo,
                        model.Comments,
                        model.Closed
                );

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(InterviewAttendanceLine.return_value);

            if (res.Status != 200) return BadRequest(res);
            var payload = JsonConvert.DeserializeObject<InterviewAttendanceLineModel>(res.Payload);

            return Ok(payload);
        }


        [HttpDelete]
        [Route("delete/{interviewAttendanceLineID}/{employeeID}")]
        public async Task<IActionResult> DeleteLeaveApplication(string interviewAttendanceLineID, string employeeID)
            {
                if (interviewAttendanceLineID == null) return BadRequest();
                if (employeeID == null) return BadRequest();

                var InterviewAttendanceLine = await webService
                .HRRecruitmentManagementWS()
                .DeleteInterviewAttendanceLineAsync(interviewAttendanceLineID, employeeID);

                dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(InterviewAttendanceLine.return_value);
            if (res.Status != 200) return BadRequest(res.Message);



            return Ok(InterviewAttendanceLine.return_value);
        }


    }
}
