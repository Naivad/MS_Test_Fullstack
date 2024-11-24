using Microsoft.AspNetCore.Http;
using MS_Test_Fullstack.Domain.Models;

namespace MS_Test_Fullstack.Domain.Interfaces
{
    public interface IFlightsServices
    {
        Task<Result<List<ResultFlights>>> CreateFlights(HttpRequest req);
    }
}
