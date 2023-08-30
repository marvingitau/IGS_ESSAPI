using Gateway.Models.HR.Incident;
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
    public class IncidentController : ControllerBase
    {

        private readonly ILogger<IncidentController> _logger;
        private readonly IWebServiceConnection webService;

        public IncidentController(ILogger<IncidentController> logger, IWebServiceConnection webService)
        {
            _logger = logger;
            this.webService = webService;
        }

        [Route("getInstances")]
        [HttpGet]
        public async Task<IActionResult> AllInstances()
        {
            var countries = await webService.HRIncidentsWS().AllIncidencesAsync();
            dynamic res = JsonConvert.DeserializeObject(countries.return_value);


            List<IncidentModel> jobs = new List<IncidentModel>();
            foreach (var item in res)
            {
                IncidentModel jm = new IncidentModel
                {
                    No = item.No,
                    EmployeeName = item.EmployeeName,
                    EmployeeNo = item.EmployeeNo,
                    IncidentStatus = item.IncidentStatus,
                    ActionDate = item.ActionDate,
                    ActionTime = item.ActionTime,
                    ActionTaken = item.ActionTaken,
                    IncidentLocation = item.IncidentLocation,
                    IncidentFirstDate = item.IncidentFirstDate,
                    IncidentRecordDate = item.IncidentRecordDate,
                    IncidentCloseDate = item.IncidentCloseDate,
                };
                jobs.Add(jm);
            }
            return Ok(new { jobs });
        }


    }
}
