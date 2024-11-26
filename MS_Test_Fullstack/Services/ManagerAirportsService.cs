using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MS_Test_Fullstack.Domain.Interfaces;
using MS_Test_Fullstack.Domain.IReposotories;
using MS_Test_Fullstack.Domain.Models;
using Newtonsoft.Json;

namespace MS_Test_Fullstack.Services
{
    public class ManagerAirportsService: IManagerAirports
    {
        private readonly ILogger<FlightsServices> _logger;
        private readonly IAirportsRepository _airportsRepository;
        public ManagerAirportsService(ILogger<FlightsServices> logger, IAirportsRepository  airportsRepository){
            _logger = logger;
            _airportsRepository = airportsRepository;
        }

        public async Task<Result<IEnumerable<IataCodes>>> GetAirports()
        {
            try
            {
                _logger.LogInformation("service processed a request GetAirports.");
                IEnumerable<IataCodes> airports = await _airportsRepository.GetAirports(); 


                return Result<IEnumerable<IataCodes>>.Success(airports);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred: {Message}", ex.Message);
                return Result<IEnumerable<IataCodes>>.Failure(ex.Message);
            }

        }
    }
}
