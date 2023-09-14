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
    public class HRJobRequirementController : ControllerBase
    {
        private readonly ILogger<HRJobRequirementController> logger;
        private readonly IWebServiceConnection webService;

        public HRJobRequirementController(ILogger<HRJobRequirementController> logger, IWebServiceConnection webService)
        {
            this.logger = logger;
            this.webService = webService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateHRJobRequirement([FromBody] HRJobRequirementModel model)
        {
            var HRJobRequirement = await webService
                    .HRJobsManagementWS()
                    .CreateHRJobRequirementAsync(
                        model.JobNo,
                        model.RequirementCode,
                        model.Description,
                        model.Mandatory,
                        model.NoofYears
                    );


            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJobRequirement.return_value);

            if (res.Status != 200) return BadRequest(res);
            var payload = JsonConvert.DeserializeObject<HRJobRequirementModel>(res.Payload);

            return Ok(payload);

        }

        [HttpGet]
        [Route("view")]
        public async Task<IActionResult> GetAllHRJobRequirements()
        {
            var HRJobRequirement = await webService
                .HRJobsManagementWS()
                .GetHRJobRequirementRangeAsync();
            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJobRequirement.return_value);

            if (res.Status != 200) return BadRequest(res.Message);

            var payload = JsonConvert.DeserializeObject<List<HRJobRequirementModel>>(res.Payload);
            return Ok(payload);
        }

        [HttpGet]
        [Route("view/{HRJobID}/{MandatoryStr}/{LineNo}")]
        public async Task<IActionResult> GetHRJobRequirement(string HRJobID, string MandatoryStr, string LineNo)
        {   
            bool Mandatory;
            if (HRJobID == null) return BadRequest();
            if (MandatoryStr.ToLower() == "yes"){ Mandatory = true; } 
            else { Mandatory = false; }
            if (LineNo == null) return BadRequest();

            var HRJobRequirement = await webService
            .HRJobsManagementWS()
            .GetHRJobRequirementAsync(
                        HRJobID,
                        Mandatory,
                        LineNo);


            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJobRequirement.return_value);

            if (res.Status != 200) return BadRequest(res);

            var payload = JsonConvert.DeserializeObject<HRJobRequirementModel>(res.Payload);
            return Ok(payload);
        }


        [HttpPut]
        [Route("update/{OldHRJobID}/{MandatoryStr}/{LineNo}")]
        public async Task<IActionResult> UpdateHRJobRequirement(string OldHRJobID, string MandatoryStr, string LineNo, [FromBody] HRJobRequirementModel model)
        {
            bool Mandatory;
            if (OldHRJobID == null) return BadRequest();
            if (MandatoryStr.ToLower() == "yes") { Mandatory = true; }
            else { Mandatory = false; }
            if (LineNo == null) return BadRequest();

            var HRJobRequirement = await webService
                .HRJobsManagementWS()
                .UpdateHRJobRequirementAsync(
                        OldHRJobID,
                        Mandatory,
                        LineNo,
                        model.JobNo,
                        model.RequirementCode,
                        model.Description,
                        model.Mandatory,
                        model.NoofYears
                );

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJobRequirement.return_value);

            if (res.Status != 200) return BadRequest(res);
            var payload = JsonConvert.DeserializeObject<HRJobRequirementModel>(res.Payload);

            return Ok(payload);
        }


        [HttpDelete]
        [Route("delete/{HRJobID}/{MandatoryStr}/{LineNo}")]
        public async Task<IActionResult> DeleteHRJobRequirement(string HRJobID, string MandatoryStr, string LineNo)
        {
            bool Mandatory;
            if (HRJobID == null) return BadRequest();
            if (MandatoryStr.ToLower() == "yes") { Mandatory = true; }
            else { Mandatory = false; }
            if (LineNo == null) return BadRequest();

            var HRJobRequirement = await webService
            .HRJobsManagementWS()
            .DeleteHRJobRequirementAsync(
                        HRJobID,
                        Mandatory,
                        LineNo);

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJobRequirement.return_value);
            if (res.Status != 200) return BadRequest(res.Message);



            return Ok(HRJobRequirement.return_value);
        }


    }
}
