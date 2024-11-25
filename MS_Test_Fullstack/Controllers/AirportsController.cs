using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using MS_Test_Fullstack.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS_Test_Fullstack.Controllers
{
    public class AirportsController
    {

        private readonly ILogger<AirportsController> _logger;
        private readonly IManagerAirports _managerAirports;

        public AirportsController(ILogger<AirportsController> logger, IManagerAirports managerAirports)
        {
            _logger = logger;
            _managerAirports = managerAirports;
        }

        [Function("GetAirports")]
        public async Task<IActionResult> GetAirports([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request CreateFlights.");

            var res = await _managerAirports.GetAirports();

            return res.IsSuccess ? new OkObjectResult(res) : new BadRequestObjectResult(res);
        }
    }
}
