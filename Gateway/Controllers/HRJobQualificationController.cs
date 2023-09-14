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
    public class HRJobQualificationController : ControllerBase
    {

        private readonly ILogger<HRJobQualificationController> logger;
        private readonly IWebServiceConnection webService;

        public HRJobQualificationController(ILogger<HRJobQualificationController> logger, IWebServiceConnection webService)
        {
            this.logger = logger;
            this.webService = webService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateHRJobQualification([FromBody] HRJobQualificationModel model)
        {
            var HRJobQualification = await webService
                    .HRJobsManagementWS()
                    .CreateHRJobQualificationAsync(
                        model.JobNo,
                        model.QualificationCode,
                        model.Description,
                        model.Mandatory
                    );


            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJobQualification.return_value);

            if (res.Status != 200) return BadRequest(res);
            var payload = JsonConvert.DeserializeObject<HRJobQualificationModel>(res.Payload);

            return Ok(payload);

        }

        [HttpGet]
        [Route("view")]
        public async Task<IActionResult> GetAllHRJobQualifications()
        {
            var HRJobQualification = await webService
                .HRJobsManagementWS()
                .GetHRJobQualificationRangeAsync();
            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJobQualification.return_value);

            if (res.Status != 200) return BadRequest(res.Message);

            var payload = JsonConvert.DeserializeObject<List<HRJobQualificationModel>>(res.Payload);
            return Ok(payload);
        }

        [HttpGet]
        [Route("view/{OldHRJobID}/{OldQualificationTypeID}/{LineNo}")]
        public async Task<IActionResult> GetHRJobQualification(string OldHRJobID, string OldQualificationTypeID, string LineNo)
        {
            if (OldHRJobID == null) return BadRequest();
            if (OldQualificationTypeID == null) return BadRequest();
            if (LineNo == null) return BadRequest();

            var HRJobQualification = await webService
            .HRJobsManagementWS()
            .GetHRJobQualificationAsync(
                        OldHRJobID,
                        OldQualificationTypeID,
                        LineNo);


            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJobQualification.return_value);

            if (res.Status != 200) return BadRequest(res);

            var payload = JsonConvert.DeserializeObject<HRJobQualificationModel>(res.Payload);
            return Ok(payload);
        }


        [HttpPut]
        [Route("update/{OldHRJobID}/{OldQualificationTypeID}/{LineNo}")]
        public async Task<IActionResult> UpdateHRJobQualification(string OldHRJobID, string OldQualificationTypeID, string LineNo, [FromBody] HRJobQualificationModel model)
        {
            if (OldHRJobID == null) return BadRequest();
            if (OldQualificationTypeID == null) return BadRequest();
            if (LineNo == null) return BadRequest();

            var HRJobQualification = await webService
                .HRJobsManagementWS()
                .UpdateHRJobQualificationAsync(
                        OldHRJobID,
                        OldQualificationTypeID,
                        LineNo,
                        model.JobNo,
                        model.QualificationCode,
                        model.Description,
                        model.Mandatory
                );

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJobQualification.return_value);

            if (res.Status != 200) return BadRequest(res);
            var payload = JsonConvert.DeserializeObject<HRJobQualificationModel>(res.Payload);

            return Ok(payload);
        }


        [HttpDelete]
        [Route("delete/{HRJobID}/{OldQualificationTypeID}/{LineNo}")]
        public async Task<IActionResult> DeleteHRJobQualification(string HRJobID, string OldQualificationTypeID, string LineNo)
        {
            if (HRJobID == null) return BadRequest();
            if (OldQualificationTypeID == null) return BadRequest();
            if (LineNo == null) return BadRequest();

            var HRJobQualification = await webService
            .HRJobsManagementWS()
            .DeleteHRJobQualificationAsync(
                        HRJobID,
                        OldQualificationTypeID,
                        LineNo);

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(HRJobQualification.return_value);
            if (res.Status != 200) return BadRequest(res.Message);



            return Ok(HRJobQualification.return_value);
        }


    }
}
