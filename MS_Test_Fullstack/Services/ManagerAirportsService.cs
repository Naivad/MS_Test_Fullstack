using Microsoft.AspNetCore.Http;
using MS_Test_Fullstack.Domain.Interfaces;
using MS_Test_Fullstack.Domain.IReposotories;
using MS_Test_Fullstack.Domain.Models;
using Newtonsoft.Json;

namespace MS_Test_Fullstack.Services
{
    public class ManagerAirportsService: IManagerAirports
    {
        private readonly IAirportsRepository _airportsRepository;
        public ManagerAirportsService(IAirportsRepository  airportsRepository){

            _airportsRepository = airportsRepository;
        }

        public async Task<Result<IEnumerable<IataCodes>>> GetAirports()
        {
            try
            {

                IEnumerable<IataCodes> airports = await _airportsRepository.GetAirports(); 


                return Result<IEnumerable<IataCodes>>.Success(airports);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<IataCodes>>.Failure(ex.Message);
            }

        }
    }
}
