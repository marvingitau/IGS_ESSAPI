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
    public class HRJobGradeLevelController : ControllerBase
    {

        private readonly ILogger<HRJobGradeLevelController> logger;
        private readonly IWebServiceConnection webService;

        public HRJobGradeLevelController(ILogger<HRJobGradeLevelController> logger, IWebServiceConnection webService)
        {
            this.logger = logger;
            this.webService = webService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateHRJobGradeLevel([FromBody] HRJobGradeLevelModel model)
        {
            var HRJobGradeLevel = await webService
                    .HRJobsManagementWS()
                    .CreateHRJobGradeLevelAsync(
                        model.JobGrade,
                        model.JobGradeLevel,
                        model.BasicPayAmount,
                        model.BasicPayDifference,
                        model.AllowanceCode,
                        model.AllowanceDescription,
                        model.AllowanceAmount,
                        model.AllowanceSetup,
                        model.Sequence
                    );

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJobGradeLevel.return_value);

            if (res.Status != 200) return BadRequest(res);
            var payload = JsonConvert.DeserializeObject<HRJobGradeLevelModel>(res.Payload);

            return Ok(payload);

        }

        [HttpGet]
        [Route("view")]
        public async Task<IActionResult> GetAllHRJobGradeLevel()
        {
            var HRJobGradeLevel = await webService
                .HRJobsManagementWS()
                .GetHRJobGradeLevelRangeAsync();
            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJobGradeLevel.return_value);

            if (res.Status != 200) return BadRequest(res.Message);

            var payload = JsonConvert.DeserializeObject<List<HRJobGradeLevelModel>>(res.Payload);
            return Ok(payload);
        }

        [HttpGet]
        [Route("view/{HRJobID}/{LineNo}")]
        public async Task<IActionResult> GetHRJobGradeLevel(string HRJobID, string LineNo)
        {
            if (HRJobID == null) return BadRequest();
            if (LineNo == null) return BadRequest();

            var HRJobGradeLevel = await webService
            .HRJobsManagementWS()
            .GetHRJobGradeLevelAsync(
                        HRJobID,
                        LineNo);


            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJobGradeLevel.return_value);

            if (res.Status != 200) return BadRequest(res);

            var payload = JsonConvert.DeserializeObject<HRJobGradeLevelModel>(res.Payload);
            return Ok(payload);
        }


        [HttpPut]
        [Route("update/{OldJobGrade}/{OldJobGradeLevel}")]
        public async Task<IActionResult> UpdateHRJobGradeLevel(string OldJobGrade, string OldJobGradeLevel, [FromBody] HRJobGradeLevelModel model)
        {
            if (OldJobGrade == null) return BadRequest();
            if (OldJobGradeLevel == null) return BadRequest();

            var HRJobGradeLevel = await webService
                .HRJobsManagementWS()
                .UpdateHRJobGradeLevelAsync(
                        OldJobGrade, 
                        OldJobGradeLevel,
                        model.JobGrade,
                        model.JobGradeLevel,
                        model.BasicPayAmount,
                        model.BasicPayDifference,
                        model.AllowanceCode,
                        model.AllowanceDescription,
                        model.AllowanceAmount,
                        model.AllowanceSetup,
                        model.Sequence
                    );

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJobGradeLevel.return_value);

            if (res.Status != 200) return BadRequest(res);
            var payload = JsonConvert.DeserializeObject<HRJobGradeLevelModel>(res.Payload);

            return Ok(payload);
        }


        [HttpDelete]
        [Route("delete/{OldJobGrade}/{OldJobGradeLevel}")]
        public async Task<IActionResult> DeleteHRJobGradeLevel(string OldJobGrade, string OldJobGradeLevel)
        {
            if (OldJobGrade == null) return BadRequest();
            if (OldJobGradeLevel == null) return BadRequest();

            var HRJobGradeLevel = await webService
            .HRJobsManagementWS()
            .DeleteHRJobGradeLevelAsync(
                       OldJobGrade,
                       OldJobGradeLevel
                        );

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJobGradeLevel.return_value);
            if (res.Status != 200) return BadRequest(res.Message);



            return Ok(HRJobGradeLevel.return_value);
        }

    }
}
