using Gateway.Models.HR.Employee;
using Gateway.WSAssets;
using IncidentsWS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController:ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebServiceConnection webService;

        public EmployeeController(ILogger<EmployeeController> logger, IWebServiceConnection ws) 
        {
            this._logger = logger;
            this.webService = ws;
        }

        [HttpGet]
        [Route("{pageNo}/{recordPerPage}")]
        public async Task<IActionResult> GetEmployeesRange(int pageNo, int recordPerPage)
        {
            var employees = await webService.HREmployeeWS().GetEmployeesRangeAsync(pageNo, recordPerPage, "active");
            dynamic res = JsonConvert.DeserializeObject(employees.return_value);

            var employeeList = new List<EmployeeModel>();
            foreach (var item in res)
            {
                var emp = new EmployeeModel
                {
                    No = item.No,
                    FullName = item.FullName,
                    Gender = item.Gender,
                    Title = item.Title,
                    MaritalStatus = item.MaritalStatus,
                    NationalID = item.NationalID,
                    NSSFNo = item.NSSFNo,
                    NHIFNo = item.NHIFNo,
                    PIN = item.PIN,
                    Address = item.Address,
                    City = item.City,
                    CountryRegionCode = item.CountryRegionCode,
                    MobilePhoneNo = item.MobilePhoneNo,
                    CompanyEMail = item.CompanyEMail,
                    EMail = item.EMail,
                    JobNo = item.JobNo,
                    JobTitle = item.JobTitle,
                    Age = item.Age,
                    ProbationExpiryDate = item.ProbationExpiryDate,
                    ContractExpiryDate = item.ContractExpiryDate,
                    Service = item.Service,
                    Religion = item.Religion,
                    Status = item.Status,
                    PhoneNo = item.PhoneNo,
                    BankCode = item.BankCode,
                };

                employeeList.Add(emp);
            }
            return Ok(new { employeeList });
        }

        // Active Employee
        // Create
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateActiveEmployee([FromBody] EmployeeModel model)
        {
            var firstName = model.FullName.Split(" ")[0];
            var middleName = model.FullName.Split(" ")[1];
            var lastName = model.FullName.Split(" ")[2];

            var employee = await webService
                .HREmployeeWS()
                .CreateEmployeeAsync(
                    firstName, 
                    middleName, 
                    lastName, 
                    model.Gender,
                    model.JobTitle,
                    model.ManagerNo,
                    model.EMail,
                    model.PhoneNo
                    );
            dynamic res = JsonConvert.DeserializeObject(employee.return_value) ;
            return Ok(new {res});
        }


        // Read
        [HttpGet]
        [Route("view/{employeeID}")]
        public async Task<IActionResult> GetEmployee(string employeeID)
        {
            var employee = await webService.HREmployeeWS().GetEmployeeAsync(employeeID, "active");
            dynamic res = JsonConvert.DeserializeObject<EmployeeModel>(employee.return_value);
            
            return Ok(new { res });
        }

        // Update
        [HttpPut]
        [Route("update/{employeeID}")]
        public async Task<IActionResult> UpdateEmployee(string employeeID, [FromBody] EmployeeModel model)
        {

            var firstName = model.FullName.Split(" ")[0];
            var middleName = model.FullName.Split(" ")[1];
            var lastName = model.FullName.Split(" ")[2];

            var employee = await webService
                .HREmployeeWS()
                .UpdateEmployeeAsync(
                    employeeID,
                    firstName,
                    middleName,
                    lastName,
                    model.Gender,
                    model.JobTitle,
                    model.ManagerNo,
                    model.EMail,
                    model.PhoneNo
                );

            // problem with deserializer
            return Ok(employee.return_value);
        }


        // Delete
        [HttpDelete]
        [Route("update/{employeeID}")]
        public async Task<IActionResult> DeleteEmployee(string employeeID)
        {
            var employee = await webService.HREmployeeWS().DeleteEmployeeAsync(employeeID);
            //dynamic res = JsonConvert.DeserializeObject(employee.return_value);
            // problem with deserializer

            return Ok(employee.return_value);
        }


        // --------------------------- INACTIVE Employees --------------------------- 
        [HttpGet]
        [Route("inactive/{employeeID}")]
        public async Task<IActionResult> GetInactiveEmployee(string employeeID)
        {
            var employee = await webService.HREmployeeWS().GetEmployeeAsync(employeeID, "inactive");
            dynamic res = JsonConvert.DeserializeObject<EmployeeModel>(employee.return_value);

            return Ok(new { res });
        }
       
        
        // --------------------------- TEMINANTED Employees --------------------------- 
        [HttpGet]
        [Route("terminated/{employeeID}")]
        public async Task<IActionResult> GetTerminatedEmployee(string employeeID)
        {
            var employee = await webService.HREmployeeWS().GetEmployeeAsync(employeeID, "terminated");
            dynamic res = JsonConvert.DeserializeObject<EmployeeModel>(employee.return_value);

            return Ok(new { res });
        }
        
    }
}
