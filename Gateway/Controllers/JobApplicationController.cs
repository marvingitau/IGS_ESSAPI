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
    public class JobApplicationController : ControllerBase
    {

        private readonly ILogger<JobApplicationController> logger;
        private readonly IWebServiceConnection webService;

        public JobApplicationController(ILogger<JobApplicationController> logger, IWebServiceConnection webService)
            {
                this.logger = logger;
                this.webService = webService;
            }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateJobApplication([FromBody] JobApplicationModel model)
        {
            var jobApplication = await webService
                    .HRRecruitmentManagementWS()
                    .CreateJobApplicationAsync(
                        model.EmployeeRequisitionNo,
                        model.Surname,
                        model.Firstname,
                        model.Middlename,
                        model.Gender,
                        model.DateofBirth,
                        model.PostCode,
                        model.PostalAddress,
                        model.County,
                        model.SubCounty,
                        model.AlternativePhoneNo,
                        model.MobilePhoneNo,
                        model.PersonalEmailAddress,
                        model.BirthCertificateNo,
                        model.NationalIDNo,
                        model.PassportNo,
                        model.DrivingLicenceNo,
                        model.MaritalStatus,
                        model.Citizenship,
                        model.PersonLivingWithDisability,
                        model.Religion,
                        model.EthnicGroup,
                        model.PINNo,
                        model.NHIFNo,
                        model.NSSFNo,
                        model.ApplicationDate,
                        model.Qualified,
                        model.ShortListed,
                        model.CommitteeShortlisted,
                        model.Status,
                        model.EmployeeCreated,
                        model.BankCode,
                        model.BankBranchCode,
                        model.ContractStartDate,
                        model.ProbationStartDate,
                        model.ProbationEnddate,
                        model.ContractEndDate,
                        model.ContractDate,
                        model.CEOMeetingDate,
                        model.CEOMeetingTime
                    );


            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(jobApplication.return_value);

            if (res.Status != 200) return BadRequest(res);
        var payload = JsonConvert.DeserializeObject<JobApplicationModel>(res.Payload);

            return Ok(payload);

    }

        [HttpGet]
        [Route("view")]
        public async Task<IActionResult> GetAllJobApplications()
        {
            var jobApplication = await webService
                .HRRecruitmentManagementWS()
                .GetJobApplicationRangeAsync();
            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(jobApplication.return_value);

            if (res.Status != 200) return BadRequest(res.Message);

            var payload = JsonConvert.DeserializeObject<List<JobApplicationModel>>(res.Payload);
            return Ok(payload);
        }

        [HttpGet]
        [Route("view/{JobApplicationID}")]
        public async Task<IActionResult> GetLeaveApplication(string JobApplicationID)
        {
            if (JobApplicationID == null) return BadRequest();

            var jobApplication = await webService
                .HRRecruitmentManagementWS()
                .GetJobApplicationAsync(JobApplicationID);

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(jobApplication.return_value);

            if (res.Status != 200) return BadRequest(res);

            var payload = JsonConvert.DeserializeObject<JobApplicationModel>(res.Payload);
            return Ok(payload);
        }


        [HttpPut]
        [Route("update/{JobApplicationID}")]
        public async Task<IActionResult> UpdateJobApplication(string JobApplicationID, [FromBody] JobApplicationModel model)
        {
            if (JobApplicationID == null) return BadRequest();

            var jobApplication = await webService
                .HRRecruitmentManagementWS()
                .UpdateJobApplicationAsync(
                    JobApplicationID,
                    model.EmployeeRequisitionNo,
                    model.Surname,
                    model.Firstname,
                    model.Middlename,
                    model.Gender,
                    model.DateofBirth,
                    model.PostCode,
                    model.PostalAddress,
                    model.County,
                    model.SubCounty,
                    model.AlternativePhoneNo,
                    model.MobilePhoneNo,
                    model.PersonalEmailAddress,
                    model.BirthCertificateNo,
                    model.NationalIDNo,
                    model.PassportNo,
                    model.DrivingLicenceNo,
                    model.MaritalStatus,
                    model.Citizenship,
                    model.PersonLivingWithDisability,
                    model.Religion,
                    model.EthnicGroup,
                    model.PINNo,
                    model.NHIFNo,
                    model.NSSFNo,
                    model.ApplicationDate,
                    model.Qualified,
                    model.ShortListed,
                    model.CommitteeShortlisted,
                    model.Status,
                    model.EmployeeCreated,
                    model.BankCode,
                    model.BankBranchCode,
                    model.ContractStartDate,
                    model.ProbationStartDate,
                    model.ProbationEnddate,
                    model.ContractEndDate,
                    model.ContractDate,
                    model.CEOMeetingDate,
                    model.CEOMeetingTime
                );

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(jobApplication.return_value);

            if (res.Status != 200) return BadRequest(res);
            var payload = JsonConvert.DeserializeObject<JobApplicationModel>(res.Payload);

            return Ok(payload);
        }


        [HttpDelete]
        [Route("delete/{JobApplicationID}")]
        public async Task<IActionResult> DeleteLeaveApplication(string JobApplicationID)
        {
            if (JobApplicationID == null) return BadRequest();

            var jobApplication = await webService
                .HRRecruitmentManagementWS()
                .DeleteJobApplicationAsync(JobApplicationID);

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(jobApplication.return_value);
            if (res.Status != 200) return BadRequest(res.Message);



            return Ok(jobApplication.return_value);
        }

    }
}
