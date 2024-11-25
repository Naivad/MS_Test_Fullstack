using Microsoft.Extensions.Configuration;
using MS_Test_Fullstack.Domain.IReposotories;
using MS_Test_Fullstack.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS_Test_Fullstack.Infrastructure.Repositories
{
    public class FlightsRepository: IFlightsRepository
    {
        private readonly IDataAccessRepository _dataAccessRepository;
        private readonly IConfiguration _configuration;
        public FlightsRepository(
            IDataAccessRepository dataAccessRepository,
            IConfiguration configuration)
        {
            _dataAccessRepository = dataAccessRepository;
            _configuration = configuration;
        }

        public async Task<ResultFlights> Create(Flights flights)
        {
           return await _dataAccessRepository.ExecuteFirst<ResultFlights>(_configuration["create_Flights"]!, flights, CommandType.StoredProcedure);
        }
        

        public async Task<IEnumerable<AvailableFlights>> GetFlights(FilterFlights filterFlights)
        {
            return await _dataAccessRepository.Execute<AvailableFlights>(_configuration["get_Flights"]!, filterFlights, CommandType.StoredProcedure);
        }
    }
}
