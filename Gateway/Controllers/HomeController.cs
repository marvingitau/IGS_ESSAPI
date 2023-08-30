using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
   
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [Route("time")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));
        }

      
    }
}
