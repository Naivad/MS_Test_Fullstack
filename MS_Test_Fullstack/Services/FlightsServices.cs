using Microsoft.AspNetCore.Http;
using MS_Test_Fullstack.Domain.Interfaces;
using MS_Test_Fullstack.Domain.IReposotories;
using MS_Test_Fullstack.Domain.Models;
using Newtonsoft.Json;

namespace MS_Test_Fullstack.Services
{
    public class FlightsServices : IFlightsServices
    {
        private readonly IFlightsRepository _flightsRepository;
        public FlightsServices(IFlightsRepository flightsRepository)
        {
            _flightsRepository = flightsRepository;
        }

        public async Task<Result<List<ResultFlights>>> CreateFlights(HttpRequest req)
        {

            try
            {
                ResultFlights resultFlights = new();
               List<ResultFlights> resultFlightsList = new();
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                List<Flights> flights = JsonConvert.DeserializeObject<List<Flights>>(requestBody)!;

                foreach (Flights flight in flights)
                {
                    resultFlights =  await _flightsRepository.Create(flight);
                    resultFlights.Route = $"{flight.Origin}-{flight.Destination}";
                    resultFlightsList.Add(resultFlights);
                }
                

                return Result<List<ResultFlights>>.Success(resultFlightsList);
            }
            catch (Exception ex)
            {
                return Result<List<ResultFlights>>.Failure(ex.Message);
            }
        }
    }
}
