using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using MS_Test_Fullstack.Domain.Interfaces;

namespace MS_Test_Fullstack.Controllers
{
    public class FlightsController
    {

        private readonly ILogger<FlightsController> _logger;
        private readonly IFlightsServices _flightsServices;

        public FlightsController(ILogger<FlightsController> logger, IFlightsServices flightsServices)
        {
            _logger = logger;
            _flightsServices = flightsServices;
        }
        
        [Function("CreateFlights")]
        public async Task<IActionResult> Create([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request CreateFlights.");

            var res = await _flightsServices.CreateFlights(req);

            return res.IsSuccess ? new OkObjectResult(res) : new BadRequestObjectResult(res);
        }

        [Function("CheckFlights")]
        public async Task<IActionResult> GetFlights([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request CreateFlights.");

            var res = await _flightsServices.GetFlights(req);

            return res.IsSuccess ? new OkObjectResult(res) : new BadRequestObjectResult(res);
        }
    }
}
