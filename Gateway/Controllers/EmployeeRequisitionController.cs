using Gateway.Models.HR.LeaveManagement;
using Gateway.Models.HR.RecruitmentManagement;
using Gateway.Models.Responses;
using Gateway.WSAssets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeRequisitionController : ControllerBase
    {
        private readonly ILogger<EmployeeRequisitionController> logger;
        private readonly IWebServiceConnection webService;

        public EmployeeRequisitionController(ILogger<EmployeeRequisitionController> logger, IWebServiceConnection webService)
        {
            this.logger = logger;
            this.webService = webService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateEmployeeRequisition([FromBody] EmployeeRequisitionModel model)
        {
            var employeeRequisition = await webService
                    .HRRecruitmentManagementWS()
                    .CreateEmployeeRequisitionAsync(
                        model.JobNo,
                        model.EmpRequisitionDescription,
                        model.RequestedEmployees,
                        model.ClosingDate,
                        model.RequisitionType,
                        model.JobAdvertPublished
                    );

            // KEEN - employeeRequisition return schema pulls out the EmpRequisitionDescription for some reason.
            // Use employeeRequisition.Body.return_value instead of employeeRequisition.return_value

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(employeeRequisition.Body.return_value);

            if (res.Status != 200) return BadRequest(res);
            var payload = JsonConvert.DeserializeObject<EmployeeRequisitionModel>(res.Payload);

            return Ok(payload);

        }

        [HttpGet]
        [Route("view")]
        public async Task<IActionResult> GetAllEmployeeRequisitions()
        {
            var employeeRequisition = await webService
                .HRRecruitmentManagementWS()
                .GetEmployeeRequisitionRangeAsync();
            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(employeeRequisition.return_value);
            
            if (res.Status != 200) return BadRequest(res.Message);

            var payload = JsonConvert.DeserializeObject<List<EmployeeRequisitionModel>>(res.Payload);
            return Ok(payload);
        }

        [HttpGet]
        [Route("view/{EmployeeRequisitionID}")]
        public async Task<IActionResult> GetLeaveApplication(string EmployeeRequisitionID)
        {
            if (EmployeeRequisitionID == null) return BadRequest();

            var employeeRequisition = await webService
                .HRRecruitmentManagementWS()
                .GetEmployeeRequisitionAsync(EmployeeRequisitionID);

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(employeeRequisition.return_value);
            
            if (res.Status != 200) return BadRequest(res);

            var payload = JsonConvert.DeserializeObject<EmployeeRequisitionModel>(res.Payload);
            return Ok(payload);
        }


        [HttpPut]
        [Route("update/{EmployeeRequisitionID}")]
        public async Task<IActionResult> UpdateEmployeeRequisition(string EmployeeRequisitionID, [FromBody] EmployeeRequisitionModel model)
        {
            if (EmployeeRequisitionID == null) return BadRequest();

            var employeeRequisition = await webService
                .HRRecruitmentManagementWS()
                .UpdateEmployeeRequisitionAsync(
                    EmployeeRequisitionID,
                    model.JobNo,
                    model.EmpRequisitionDescription,
                    model.RequestedEmployees,
                    model.ClosingDate,
                    model.RequisitionType,
                    model.JobAdvertPublished
                );

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(employeeRequisition.return_value);

            if (res.Status != 200) return BadRequest(res);
            var payload = JsonConvert.DeserializeObject<EmployeeRequisitionModel>(res.Payload);

            return Ok(payload);
        }


        [HttpDelete]
        [Route("delete/{EmployeeRequisitionID}")]
        public async Task<IActionResult> DeleteLeaveApplication(string EmployeeRequisitionID)
        {
            if (EmployeeRequisitionID == null) return BadRequest();

            var employeeRequisition = await webService
                .HRRecruitmentManagementWS()
                .DeleteEmployeeRequisitionAsync(EmployeeRequisitionID);

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(employeeRequisition.return_value);
            if (res.Status != 200) return BadRequest(res);

            return Ok(res);
        }

    }
}
