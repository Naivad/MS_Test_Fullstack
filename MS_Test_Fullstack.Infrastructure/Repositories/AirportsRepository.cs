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
    public class AirportsRepository : IAirportsRepository
    {
        private readonly IDataAccessRepository _dataAccessRepository;
        private readonly IConfiguration _configuration;
        public AirportsRepository(
            IDataAccessRepository dataAccessRepository,
            IConfiguration configuration)
        {
            _dataAccessRepository = dataAccessRepository;
            _configuration = configuration;
        }

        public async Task<IEnumerable<IataCodes>> GetAirports()
        {
            return await _dataAccessRepository.Execute<IataCodes>(_configuration["get_Airports"]!, null!, CommandType.StoredProcedure);
        }
    }
}
