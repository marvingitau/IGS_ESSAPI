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
    public class InterviewCommitteeDepHeaderController : ControllerBase
    {
        private readonly ILogger<InterviewCommitteeDepHeaderController> logger;
        private readonly IWebServiceConnection webService;

        public InterviewCommitteeDepHeaderController(ILogger<InterviewCommitteeDepHeaderController> logger, IWebServiceConnection webService)
        {
            this.logger = logger;
            this.webService = webService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateInterviewCommitteeDepHeader([FromBody] InterviewCommitteeDepHeaderModel model)
        {
            var InterviewCommitteeDepHeader = await webService
                    .HRRecruitmentManagementWS()
                    .CreateInterviewCommitteeDepHeaderAsync(
                        model.DeptCommitteeName
                    );


            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(InterviewCommitteeDepHeader.return_value);

            if (res.Status != 200) return BadRequest(res);
            var payload = JsonConvert.DeserializeObject<InterviewCommitteeDepHeaderModel>(res.Payload);

            return Ok(payload);

        }

        [HttpGet]
        [Route("view")]
        public async Task<IActionResult> GetAllInterviewCommitteeDepHeaders()
        {
            var InterviewCommitteeDepHeader = await webService
                .HRRecruitmentManagementWS()
                .GetInterviewCommitteeDepHeaderRangeAsync();
            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(InterviewCommitteeDepHeader.return_value);

            if (res.Status != 200) return BadRequest(res.Message);

            var payload = JsonConvert.DeserializeObject<List<InterviewCommitteeDepHeaderModel>>(res.Payload);

            return Ok(payload);
        }

        [HttpGet]
        [Route("view/{InterviewCommitteeDepHeaderID}")]
        public async Task<IActionResult> GetLeaveApplication(string InterviewCommitteeDepHeaderID)
        {

            if (InterviewCommitteeDepHeaderID == null) return BadRequest();

            var InterviewCommitteeDepHeader = await webService
                .HRRecruitmentManagementWS()
                .GetInterviewCommitteeDepHeaderAsync(InterviewCommitteeDepHeaderID);

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(InterviewCommitteeDepHeader.return_value);

            if (res.Status != 200) return BadRequest(res);

            var payload = JsonConvert.DeserializeObject<InterviewCommitteeDepHeaderModel>(res.Payload);
            return Ok(payload);
        }


        [HttpPut]
        [Route("update/{InterviewCommitteeDepHeaderID}")]
        public async Task<IActionResult> UpdateInterviewCommitteeDepHeader(string InterviewCommitteeDepHeaderID, [FromBody] InterviewCommitteeDepHeaderModel model)
        {
            if (InterviewCommitteeDepHeaderID == null) return BadRequest();

            var InterviewCommitteeDepHeader = await webService
                .HRRecruitmentManagementWS()
                .UpdateInterviewCommitteeDepHeaderAsync(
                    InterviewCommitteeDepHeaderID,
                    model.DeptCommitteeName
                );

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(InterviewCommitteeDepHeader.return_value);

            if (res.Status != 200) return BadRequest(res);
            var payload = JsonConvert.DeserializeObject<InterviewCommitteeDepHeaderModel>(res.Payload);

            return Ok(payload);
        }


        [HttpDelete]
        [Route("delete/{InterviewCommitteeDepHeaderID}")]
        public async Task<IActionResult> DeleteLeaveApplication(string InterviewCommitteeDepHeaderID)
        {
            if (InterviewCommitteeDepHeaderID == null) return BadRequest();

            var InterviewCommitteeDepHeader = await webService
                .HRRecruitmentManagementWS()
                .DeleteInterviewCommitteeDepHeaderAsync(InterviewCommitteeDepHeaderID);

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(InterviewCommitteeDepHeader.return_value);
            if (res.Status != 200) return BadRequest(res.Message);



            return Ok(InterviewCommitteeDepHeader.return_value);
        }

    }
}
