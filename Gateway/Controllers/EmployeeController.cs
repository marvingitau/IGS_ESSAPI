using Gateway.Models.HR.Employee;
using Gateway.Models.HR.JobsManagement;
using Gateway.Models.Responses;
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

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(employees.return_value);

            if (res.Status != 200) return BadRequest(res);
            var payload = JsonConvert.DeserializeObject<EmployeeModel>(res.Payload);

            return Ok(payload);
           
        }

        // Active Employee
        // Create
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateActiveEmployee()
        {


            var employee = await webService
                .HREmployeeWS()
                .CreateEmployeeAsync();

            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(employee.return_value);

            return Ok(res.Payload);

        }


        // Read
        [HttpGet]
        [Route("view/{employeeID}")]
        public async Task<IActionResult> GetEmployee(string employeeID)
        {
            var employee = await webService.HREmployeeWS().GetEmployeeAsync(employeeID, "active");
            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(employee.return_value);

            if (res.Status != 200) return BadRequest(res);
            var payload = JsonConvert.DeserializeObject<EmployeeModel>(res.Payload);

            return Ok(payload);
        }

        // Update
        [HttpPut]
        [Route("update/{employeeID}")]
        public async Task<IActionResult> UpdateEmployee(string employeeID, [FromBody] EmployeeModel model)
        {
            string[] stringArr = new string[187];
            decimal[] decimalArr = new decimal[20];
            int[] intArr = new int[2];
            bool[] boolArr = new bool[10];






            stringArr[0] = model.No;
            stringArr[1] = model.FirstName;
            stringArr[2] = model.MiddleName;
            stringArr[3] = model.LastName;
            stringArr[4] = model.Initials;
            stringArr[5] = model.JobTitle;
            stringArr[6] = model.SearchName;
            stringArr[7] = model.Address;
            stringArr[8] = model.Address2;
            stringArr[9] = model.City;
            stringArr[10] = model.PostCode;
            stringArr[11] = model.County;
            stringArr[12] = model.PhoneNo;
            stringArr[13] = model.MobilePhoneNo;
            stringArr[14] = model.EMail;
            stringArr[15] = model.AltAddressCode;
            stringArr[16] = model.AltAddressStartDate;
            stringArr[17] = model.AltAddressEndDate;
            stringArr[18] = model.Picture;
            stringArr[19] = model.BirthDate;
            stringArr[20] = model.SocialSecurityNo;
            stringArr[21] = model.UnionCode;
            stringArr[22] = model.UnionMembershipNo;
            stringArr[23] = model.Gender;
            stringArr[24] = model.CountryRegionCode;
            stringArr[25] = model.ManagerNo;
            stringArr[26] = model.EmplymtContractCode;
            stringArr[27] = model.StatisticsGroupCode;
            stringArr[28] = model.EmploymentDate;
            stringArr[29] = model.Status;
            stringArr[30] = model.InactiveDate;
            stringArr[31] = model.CauseofInactivityCode;
            stringArr[32] = model.TerminationDate;
            stringArr[33] = model.GroundsforTermCode;
            stringArr[34] = model.GlobalDimension1Code;
            stringArr[35] = model.GlobalDimension2Code;
            stringArr[36] = model.ResourceNo;
            boolArr[0] = model.Comment;
            stringArr[37] = model.LastDateModified;
            stringArr[38] = model.DateFilter;
            stringArr[39] = model.GlobalDimension1Filter;
            stringArr[40] = model.GlobalDimension2Filter;
            stringArr[41] = model.CauseofAbsenceFilter;
            decimalArr[0] = model.TotalAbsenceBase;
            stringArr[42] = model.Extension;
            stringArr[43] = model.EmployeeNoFilter;
            stringArr[44] = model.Pager;
            stringArr[45] = model.FaxNo;
            stringArr[46] = model.CompanyEMail;
            stringArr[47] = model.Title;
            stringArr[48] = model.SalespersPurchCode;
            stringArr[49] = model.NoSeries;
            stringArr[50] = model.LastModifiedDateTime;
            stringArr[51] = model.EmployeePostingGroup;
            stringArr[52] = model.BankBranchNo;
            stringArr[53] = model.BankAccountNo1;
            stringArr[54] = model.IBAN;
            decimalArr[1] = model.Balance;
            stringArr[55] = model.SWIFTCode;
            stringArr[56] = model.ApplicationMethod;
            stringArr[57] = model.Image;
            boolArr[1] = model.PrivacyBlocked;
            stringArr[58] = model.CostCenterCode;
            stringArr[59] = model.CostObjectCode;
            stringArr[60] = model.Id;
            stringArr[61] = model.EmpBranchCode;
            stringArr[62] = model.BankBranchCode;
            stringArr[63] = model.NationalID;
            stringArr[64] = model.CustomerNo;
            stringArr[65] = model.NameofOldEmployer;
            stringArr[66] = model.AddressofOldEmployer;
            stringArr[67] = model.NameofNewEmployer;
            stringArr[68] = model.AddressofNewEmployer;
            stringArr[69] = model.CalculationScheme;
            stringArr[70] = model.ModeofPayment;
            stringArr[71] = model.BankCode;
            stringArr[72] = model.BankAccountNo;
            stringArr[73] = model.EDCodeFilter;
            stringArr[74] = model.PeriodFilter;
            decimalArr[2] = model.Amount;
            stringArr[75] = model.CalculationGroupFilter;
            decimalArr[3] = model.Loans;
            stringArr[76] = model.PostingGroup;
            stringArr[77] = model.SalaryScale;
            stringArr[78] = model.ScaleStep;
            stringArr[79] = model.Paystation;
            decimalArr[4] = model.FixedPay;
            stringArr[80] = model.BasicPay;
            decimalArr[5] = model.HourlyRate;
            decimalArr[6] = model.DailyRate;
            decimalArr[7] = model.AmountToDate;
            stringArr[81] = model.PayrollCode;
            stringArr[82] = model.MembershipNo;
            decimalArr[8] = model.AmountLCY;
            decimalArr[9] = model.AmountToDateLCY;
            stringArr[83] = model.CurrencyFilter;
            stringArr[84] = model.BasicPayCurrency;
            stringArr[85] = model.HousingForEmployee;
            decimalArr[10] = model.ValueofQuarters;
            stringArr[86] = model.EmployeeGrade;
            stringArr[87] = model.PersonalIDNo;
            stringArr[88] = model.PIN;
            stringArr[89] = model.VisaNo;
            stringArr[90] = model.VisaEndDate;
            stringArr[91] = model.WorkPermitNo;
            stringArr[92] = model.WorkPermitEndDate;
            decimalArr[11] = model.TotalEmplFactor;
            stringArr[93] = model.NSSFNo;
            stringArr[94] = model.NHIFNo;
            stringArr[95] = model.BranchCode;
            stringArr[96] = model.LocationCode;
            stringArr[97] = model.HousingEligibility;
            stringArr[98] = model.Service;
            boolArr[2] = model.Driver;
            stringArr[99] = model.Position;
            stringArr[100] = model.PositionTitle;
            stringArr[101] = model.MaritalStatus;
            stringArr[102] = model.PhysicallyChallenged;
            stringArr[103] = model.PhysicallyChallengedDetails;
            stringArr[104] = model.PhysicallyChallengedGrade;
            stringArr[105] = model.PhysicalFileNo;
            stringArr[106] = model.ConfirmationDate;
            stringArr[107] = model.FullTimePartTime;
            stringArr[108] = model.Age;
            stringArr[109] = model.WeddingAnniversary;
            stringArr[110] = model.ContractEndDate;
            stringArr[111] = model.ExitInterviewDate;
            stringArr[112] = model.ExitInterviewDoneBy;
            boolArr[3] = model.AllowReEmploymentinFuture;
            stringArr[113] = model.ProbationExpiryDate;
            decimalArr[12] = model.NoofDays;
            stringArr[114] = model.EmployeeType;
            decimalArr[13] = model.Sanlam;
            decimalArr[14] = model.Liberty;
            decimalArr[15] = model.HELB;
            stringArr[115] = model.ActiveServiceYears;
            intArr[0] = model.Ages;
            stringArr[116] = model.LaptrustNo;
            boolArr[4] = model.GroupEmployee;
            stringArr[117] = model.DatePromotedMovedtoGroup;
            stringArr[118] = model.ESGlevel;
            stringArr[119] = model.EmployeeFunctionGroup;
            stringArr[120] = model.EmployeeLevel;
            stringArr[121] = model.SeparationRemarks;
            stringArr[122] = model.MaritalStatusd;
            stringArr[123] = model.BirthCertificateNo;
            stringArr[124] = model.NationalIDNod;
            stringArr[125] = model.PINNod;
            stringArr[126] = model.NSSFNod;
            stringArr[127] = model.NHIFNod;
            stringArr[128] = model.PassportNod;
            stringArr[129] = model.DrivingLicenceNo;
            stringArr[130] = model.JobNod;
            stringArr[131] = model.JobGraded;
            stringArr[132] = model.Aged;
            stringArr[133] = model.BankCoded;
            stringArr[134] = model.BankName;
            stringArr[135] = model.BankBranchCoded;
            stringArr[136] = model.BankBranchName;
            stringArr[137] = model.ContractStartDate;
            stringArr[138] = model.Citizenship;
            stringArr[139] = model.Religion;
            stringArr[140] = model.CountyCode;
            stringArr[141] = model.CountyName;
            stringArr[142] = model.SubCountyCode;
            stringArr[143] = model.SubCountyName;
            stringArr[144] = model.LeaveStatus;
            stringArr[145] = model.LeaveCalendar;
            stringArr[146] = model.PasswordResetToken;
            stringArr[147] = model.PasswordResetTokenExpiry;
            stringArr[148] = model.PortalPassword;
            boolArr[5] = model.DefaultPortalPassword;
            stringArr[149] = model.ContractExpiryDate;
            stringArr[150] = model.EmployeeSignature;
            stringArr[151] = model.PersonLivingwithDisability;
            stringArr[152] = model.EthnicGroup;
            stringArr[153] = model.HudumaNo;
            stringArr[154] = model.HRSalaryNotch;
            stringArr[155] = model.SupervisorJobTitle;
            stringArr[156] = model.UserID;
            stringArr[157] = model.ImprestPostingGroup;
            stringArr[158] = model.Department;
            stringArr[159] = model.Location;
            stringArr[160] = model.ShortcutDimension3Code;
            stringArr[161] = model.ShortcutDimension4Code;
            stringArr[162] = model.ShortcutDimension5Code;
            stringArr[163] = model.ShortcutDimension6Code;
            stringArr[164] = model.ShortcutDimension7Code;
            stringArr[165] = model.ShortcutDimension8Code;
            stringArr[166] = model.DrivingLicenseExpiryDate;
            stringArr[167] = model.PracticeCertNo;
            stringArr[168] = model.EmployementYearsofService;
            stringArr[169] = model.DateofLeaving;
            stringArr[170] = model.SupervisorJobNo;
            stringArr[171] = model.TerminationGrounds;
            decimalArr[16] = model.TotalLeaveTaken;
            decimalArr[17] = model.LeaveBalance;
            stringArr[172] = model.LeavePeriodFilter;
            intArr[1] = model.AllocatedLeaveDays;
            stringArr[173] = model.ReasonForLeaving;
            stringArr[174] = model.ReasonForLeavingOther;
            boolArr[6] = model.OnProbation;
            stringArr[175] = model.ContractPeriod;
            stringArr[176] = model.ProbationStartDate;
            stringArr[177] = model.ProbationPeriod;
            stringArr[178] = model.ProbationEnddate;
            stringArr[179] = model.ReactivationTime;
            stringArr[180] = model.LeaveGroup;
            decimalArr[18] = model.ImprestBalance;
            stringArr[181] = model.FullName;
            stringArr[182] = model.PayrollGroupCode;
            decimalArr[19] = model.NonPayrollReceipts;
            stringArr[183] = model.Responsibilty;
            stringArr[184] = model.Level;
            boolArr[7] = model.CanEditJobTargets;
            boolArr[8] = model.SubstituteNotRequired;
            boolArr[9] = model.Confirmed;
            stringArr[185] = model.Rank;
            stringArr[186] = model.HOD;


            var employee = await webService
                .HREmployeeWS()
                .UpdateEmployeeAsync(
                    stringArr, boolArr, decimalArr, intArr
                );
            
            dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(employee.return_value);

            if (res.Status != 200) return BadRequest(res);
            var payload = JsonConvert.DeserializeObject<EmployeeModel>(res.Payload);

            return Ok(payload);
        
    }


        // Delete
        [HttpDelete]
        [Route("update/{employeeID}")]
        public async Task<IActionResult> DeleteEmployee(string employeeID)
        {
            var employee = await webService.HREmployeeWS().DeleteEmployeeAsync(employeeID);
        //dynamic res = JsonConvert.DeserializeObject(employee.return_value);
        // problem with deserializer

        dynamic res = JsonConvert.DeserializeObject<CodeUnitResponseModel>(employee.return_value);

        if (res.Status != 200) return BadRequest(res);
        var payload = JsonConvert.DeserializeObject<EmployeeModel>(res.Payload);

        return Ok(payload);
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
        

        // 
        
    }
}
