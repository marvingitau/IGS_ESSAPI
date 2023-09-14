using Gateway.Models.HR.JobsManagement;
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
    public class HRJobController : ControllerBase
    {

        private readonly ILogger<HRJobController> logger;
        private readonly IWebServiceConnection webService;

        public HRJobController(ILogger<HRJobController> logger, IWebServiceConnection webService)
        {
            this.logger = logger;
            this.webService = webService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateHRJob([FromBody] HRJobModel model)
        {
            var HRJob = await webService
                    .HRJobsManagementWS()
                    .CreateHRJobAsync(
                        model.JobTitle,
                        model.JobGrade,
                        model.MaximumPositions,
                        model.SupervisorJobNo,
                        model.AppraisalLevel,
                        model.Active,
                        model.Status
                    );


            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJob.return_value);

            if (res.Status != 200) return BadRequest(res);
            var payload = JsonConvert.DeserializeObject<HRJobModel>(res.Payload);

            return Ok(payload);

        }

        [HttpGet]
        [Route("view")]
        public async Task<IActionResult> GetAllHRJobs()
        {
            var HRJob = await webService
                .HRJobsManagementWS()
                .GetHRJobRangeAsync();
            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJob.return_value);

            if (res.Status != 200) return BadRequest(res.Message);

            var payload = JsonConvert.DeserializeObject<List<HRJobModel>>(res.Payload);
            return Ok(payload);
        }

        [HttpGet]
        [Route("view/{HRJobID}")]
        public async Task<IActionResult> GetLeaveApplication(string HRJobID)
        {
            if (HRJobID == null) return BadRequest();

            var HRJob = await webService
                .HRJobsManagementWS()
                .GetHRJobAsync(HRJobID);

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJob.return_value);

            if (res.Status != 200) return BadRequest(res);

            var payload = JsonConvert.DeserializeObject<HRJobModel>(res.Payload);
            return Ok(payload);
        }


        [HttpPut]
        [Route("update/{HRJobID}")]
        public async Task<IActionResult> UpdateHRJob(string HRJobID,  [FromBody] HRJobModel model)
        {
            if (HRJobID == null) return BadRequest();

            var HRJob = await webService
                .HRJobsManagementWS()
                .UpdateHRJobAsync(
                        HRJobID,
                        model.JobTitle,
                        model.JobGrade,
                        model.MaximumPositions,
                        model.SupervisorJobNo,
                        model.AppraisalLevel,
                        model.Active,
                        model.Status
                );

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJob.return_value);

            if (res.Status != 200) return BadRequest(res);
        var payload = JsonConvert.DeserializeObject<HRJobModel>(res.Payload);

            return Ok(payload);
    }


    [HttpDelete]
    [Route("delete/{HRJobID}")]
    public async Task<IActionResult> DeleteHRJob(string HRJobID)
    {
        if (HRJobID == null) return BadRequest();

        var HRJob = await webService
        .HRJobsManagementWS()
        .DeleteHRJobAsync(HRJobID);

        dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJob.return_value);
        if (res.Status != 200) return BadRequest(res.Message);



        return Ok(HRJob.return_value);
    }



}
}
