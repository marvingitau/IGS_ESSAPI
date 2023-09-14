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
    public class HRJobLookupValueController : ControllerBase
    {

        private readonly ILogger<HRJobLookupValueController> logger;
        private readonly IWebServiceConnection webService;

        public HRJobLookupValueController(ILogger<HRJobLookupValueController> logger, IWebServiceConnection webService)
        {
            this.logger = logger;
            this.webService = webService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateHRJobLookupValue([FromBody] HRJobLookupValueModel model)
        {
            var HRJobLookupValue = await webService
                    .HRJobsManagementWS()
                    .CreateHRJobLookupValueAsync(
                        model.Option,
                        model.Description,
                        model.Blocked,
                        model.RequiredStage
                    );


            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJobLookupValue.return_value);

            if (res.Status != 200) return BadRequest(res);
            var payload = JsonConvert.DeserializeObject<HRJobLookupValueModel>(res.Payload);

            return Ok(payload);

        }

        [HttpGet]
        [Route("view")]
        public async Task<IActionResult> GetAllHRJobLookupValue()
        {
            var HRJobLookupValue = await webService
                .HRJobsManagementWS()
                .GetHRJobLookupValueRangeAsync();
            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJobLookupValue.return_value);

            if (res.Status != 200) return BadRequest(res.Message);

            var payload = JsonConvert.DeserializeObject<List<HRJobLookupValueModel>>(res.Payload);
            return Ok(payload);
        }

        [HttpGet]
        [Route("view/{HRJobLookupValueID}")]
        public async Task<IActionResult> GetHRJobLookupValue(string HRJobLookupValueID)
        {
            if (HRJobLookupValueID == null) return BadRequest();
            

            var HRJobLookupValue = await webService
            .HRJobsManagementWS()
            .GetHRJobLookupValueAsync(
                        HRJobLookupValueID
                        );


            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJobLookupValue.return_value);

            if (res.Status != 200) return BadRequest(res);

            var payload = JsonConvert.DeserializeObject<HRJobLookupValueModel>(res.Payload);
            return Ok(payload);
        }


        [HttpPut]
        [Route("update/{HRJobLookupValueID}")]
        public async Task<IActionResult> UpdateHRJobLookupValue(string HRJobLookupValueID, [FromBody] HRJobLookupValueModel model)
        {
            if (HRJobLookupValueID == null) return BadRequest();

            var HRJobLookupValue = await webService
                .HRJobsManagementWS()
                .UpdateHRJobLookupValueAsync(
                        HRJobLookupValueID,
                        model.Option,
                        model.Description,
                        model.Blocked,
                        model.RequiredStage
                );

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJobLookupValue.return_value);

            if (res.Status != 200) return BadRequest(res);
            var payload = JsonConvert.DeserializeObject<HRJobLookupValueModel>(res.Payload);

            return Ok(payload);
        }


        [HttpDelete]
        [Route("delete/{HRJobLookupValueID}")]
        public async Task<IActionResult> DeleteHRJobLookupValue(string HRJobLookupValueID)
        {
            if (HRJobLookupValueID == null) return BadRequest();


            var HRJobLookupValue = await webService
            .HRJobsManagementWS()
            .DeleteHRJobLookupValueAsync(
                        HRJobLookupValueID
                        );

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJobLookupValue.return_value);
            if (res.Status != 200) return BadRequest(res.Message);



            return Ok(HRJobLookupValue.return_value);
        }



    }
}
