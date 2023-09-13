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
    public class InterviewAttendanceHeaderController : ControllerBase
    {

        private readonly ILogger<InterviewAttendanceHeaderController> logger;
        private readonly IWebServiceConnection webService;

        public InterviewAttendanceHeaderController(ILogger<InterviewAttendanceHeaderController> logger, IWebServiceConnection webService)
        {
            this.logger = logger;
            this.webService = webService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateInterviewAttendanceHeader([FromBody] InterviewAttendanceHeaderModel model)
        {
            var InterviewAttendanceHeader = await webService
                    .HRRecruitmentManagementWS()
                    .CreateInterviewAttendanceHeaderAsync(
                        model.InterviewCommitteecode,
                        model.InterviewDatefrom,
                        model.InterviewDateto,
                        model.InterviewTime,
                        model.InterviewLocation,
                        model.InterviewChairpersonCode,
                        model.InterviewPurpose,
                        model.JobRequisitionNo,
                        model.CommitteeRemarks,
                        model.Closed,
                        model.MandatoryDocsRequired
                    );


            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(InterviewAttendanceHeader.return_value);

            if (res.Status != 200) return BadRequest(res);
            var payload = JsonConvert.DeserializeObject<InterviewAttendanceHeaderModel>(res.Payload);

            return Ok(payload);

            }

        [HttpGet]
        [Route("view")]
        public async Task<IActionResult> GetAllInterviewAttendanceHeaders()
        {
            var InterviewAttendanceHeader = await webService
                .HRRecruitmentManagementWS()
                .GetInterviewAttendanceHeaderRangeAsync();
            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(InterviewAttendanceHeader.return_value);

            if (res.Status != 200) return BadRequest(res.Message);

            var payload = JsonConvert.DeserializeObject<List<InterviewAttendanceHeaderModel>>(res.Payload);
          
            return Ok(payload);
        }
         
        [HttpGet]
        [Route("view/{InterviewAttendanceHeaderID}")]
        public async Task<IActionResult> GetLeaveApplication(string InterviewAttendanceHeaderID)
        {

            if (InterviewAttendanceHeaderID == null) return BadRequest();

            var InterviewAttendanceHeader = await webService
                .HRRecruitmentManagementWS()
                .GetInterviewAttendanceHeaderAsync(InterviewAttendanceHeaderID);

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(InterviewAttendanceHeader.return_value);

            if (res.Status != 200) return BadRequest(res);

            var payload = JsonConvert.DeserializeObject<InterviewAttendanceHeaderModel>(res.Payload);
            return Ok(payload);
        }


        [HttpPut]
        [Route("update/{InterviewAttendanceHeaderID}")]
        public async Task<IActionResult> UpdateInterviewAttendanceHeader(string InterviewAttendanceHeaderID, [FromBody] InterviewAttendanceHeaderModel model)
        {
            if (InterviewAttendanceHeaderID == null) return BadRequest();

            var InterviewAttendanceHeader = await webService
                .HRRecruitmentManagementWS()
                .UpdateInterviewAttendanceHeaderAsync(
                        InterviewAttendanceHeaderID,
                        model.InterviewCommitteecode,
                        model.InterviewDatefrom,
                        model.InterviewDateto,
                        model.InterviewTime,
                        model.InterviewLocation,
                        model.InterviewChairpersonCode,
                        model.InterviewPurpose,
                        model.JobRequisitionNo,
                        model.CommitteeRemarks,
                        model.Closed,
                        model.MandatoryDocsRequired
                );

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(InterviewAttendanceHeader.return_value);

            if (res.Status != 200) return BadRequest(res);
            var payload = JsonConvert.DeserializeObject<InterviewAttendanceHeaderModel>(res.Payload);

            return Ok(payload);
        }


        [HttpDelete]
        [Route("delete/{InterviewAttendanceHeaderID}")]
        public async Task<IActionResult> DeleteLeaveApplication(string InterviewAttendanceHeaderID)
        {
            if (InterviewAttendanceHeaderID == null) return BadRequest();

            var InterviewAttendanceHeader = await webService
                .HRRecruitmentManagementWS()
                .DeleteInterviewAttendanceHeaderAsync(InterviewAttendanceHeaderID);

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(InterviewAttendanceHeader.return_value);
            if (res.Status != 200) return BadRequest(res.Message);



            return Ok(InterviewAttendanceHeader.return_value);
        }

    }
}
