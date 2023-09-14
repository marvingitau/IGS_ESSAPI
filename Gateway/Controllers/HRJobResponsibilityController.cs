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
    public class HRJobResponsibilityController : ControllerBase
    {


        private readonly ILogger<HRJobResponsibilityController> logger;
        private readonly IWebServiceConnection webService;

        public HRJobResponsibilityController(ILogger<HRJobResponsibilityController> logger, IWebServiceConnection webService)
        {
            this.logger = logger;
            this.webService = webService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateHRJobResponsibility([FromBody] HRJobResponsibilityModel model)
        {
            var HRJobResponsibility = await webService
                    .HRJobsManagementWS()
                    .CreateHRJobResponsibilityAsync(
                        model.JobNo,
                        model.ResponsibilityCode,
                        model.Description
                    );

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJobResponsibility.return_value);

            if (res.Status != 200) return BadRequest(res);
            var payload = JsonConvert.DeserializeObject<HRJobResponsibilityModel>(res.Payload);

            return Ok(payload);

        }

        [HttpGet]
        [Route("view")]
        public async Task<IActionResult> GetAllHRJobResponsibility()
        {
            var HRJobResponsibility = await webService
                .HRJobsManagementWS()
                .GetHRJobResponsibilityRangeAsync();
            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJobResponsibility.return_value);

            if (res.Status != 200) return BadRequest(res.Message);

            var payload = JsonConvert.DeserializeObject<List<HRJobResponsibilityModel>>(res.Payload);
            return Ok(payload);
        }

        [HttpGet]
        [Route("view/{HRJobID}/{LineNo}")]
        public async Task<IActionResult> GetHRJobResponsibility(string HRJobID, string LineNo)
        {
            if (HRJobID == null) return BadRequest();
            if (LineNo == null) return BadRequest();

            var HRJobResponsibility = await webService
            .HRJobsManagementWS()
            .GetHRJobResponsibilityAsync(
                        HRJobID,
                        LineNo);


            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJobResponsibility.return_value);

            if (res.Status != 200) return BadRequest(res);

            var payload = JsonConvert.DeserializeObject<HRJobResponsibilityModel>(res.Payload);
            return Ok(payload);
        }


        [HttpPut]
        [Route("update/{OldHRJobID}/{LineNo}")]
        public async Task<IActionResult> UpdateHRJobResponsibility(string OldHRJobID, string LineNo, [FromBody] HRJobResponsibilityModel model)
        {
            if (OldHRJobID == null) return BadRequest();
            if (LineNo == null) return BadRequest();

            var HRJobResponsibility = await webService
                .HRJobsManagementWS()
                .UpdateHRJobResponsibilityAsync(
                        OldHRJobID,
                        LineNo,
                        model.ResponsibilityCode,
                        model.Description
                );

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJobResponsibility.return_value);

            if (res.Status != 200) return BadRequest(res);
            var payload = JsonConvert.DeserializeObject<HRJobResponsibilityModel>(res.Payload);

            return Ok(payload);
        }


        [HttpDelete]
        [Route("delete/{HRJobID}/{LineNo}")]
        public async Task<IActionResult> DeleteHRJobResponsibility(string HRJobID, string LineNo)
        {
            if (HRJobID == null) return BadRequest();
            if (LineNo == null) return BadRequest();


            var HRJobResponsibility = await webService
            .HRJobsManagementWS()
            .DeleteHRJobResponsibilityAsync(
                        HRJobID,
                        LineNo
                        );

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJobResponsibility.return_value);
            if (res.Status != 200) return BadRequest(res.Message);



            return Ok(HRJobResponsibility.return_value);
        }

    }
}
