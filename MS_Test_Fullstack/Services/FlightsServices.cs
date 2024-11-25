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
                    resultFlights = await _flightsRepository.Create(flight);
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

        public async Task<Result<List<AvailableFlights>>> GetFlights(HttpRequest req)
        {
            List<AvailableFlights> flightsData = new();

            try
            {
                // Deserializar filtros de la consulta
                DateTime? startDate = Convert.ToDateTime(req.Query["startDate"]!);
                DateTime? endDate = null;
                if (!string.IsNullOrEmpty(req.Query["EdnDate"]))
                {
                    endDate = DateTime.TryParse(req.Query["EdnDate"], out DateTime parsedDate) ? parsedDate : null;
                }
                string? iataCodeOrigin = req.Query["iataCodeOrigin"]!;
                string? iataCodeDestination = req.Query["iataCodeDestination"]!;

                // Configurar filtros iniciales
                FilterFlights filterFlights = new()
                {
                    FilterDate = startDate,
                    IATACodeOrigin = iataCodeOrigin,
                    IATACodeDestination = iataCodeDestination
                };

                // Obtener vuelos de ida
                var outboundFlights = await _flightsRepository.GetFlights(filterFlights);
                ProcessFlights(outboundFlights, flightsData, "OneWay");

                // Obtener vuelos de regreso si la fecha de regreso está definida
                if (endDate != null)
                {
                    filterFlights.FilterDate = endDate;
                    filterFlights.IATACodeOrigin = iataCodeDestination;
                    filterFlights.IATACodeDestination = iataCodeOrigin;

                    var returnFlights = await _flightsRepository.GetFlights(filterFlights);
                    ProcessFlights(returnFlights, flightsData, "Return");
                }

                return Result<List<AvailableFlights>>.Success(flightsData);
            }
            catch (Exception ex)
            {
                return Result<List<AvailableFlights>>.Failure(ex.Message);
            }
        }

        private void ProcessFlights(IEnumerable<AvailableFlights>? flights, List<AvailableFlights> flightsData, string travelDirection)
        {
            if (flights == null) return;

            var groupedFlights = flights
                .GroupBy(f => f.JourneyID)
                .ToList();

            foreach (var group in groupedFlights)
            {
                var mainFlight = group.FirstOrDefault(f => f.FlightOrder == 1);

                if (mainFlight != null)
                {
                    // Agregar los segmentos como PathSegments
                    mainFlight.PathSegments = group
                        .Where(f => f.FlightOrder > 1)
                        .Select(f => new PathSegments
                        {
                            SegmentOrigin = f.SegmentOrigin,
                            SegmentDestination = f.SegmentDestination,
                            SegmentDepartureTime = f.SegmentDepartureTime,
                            DirectCarrier = f.DirectCarrier,
                            DirectFlightNumber = f.SegmentFlightNumber
                        }).ToList();

                    mainFlight.TravelDirection = travelDirection;
                    flightsData.Add(mainFlight);
                }
            }
        }


    }
}
