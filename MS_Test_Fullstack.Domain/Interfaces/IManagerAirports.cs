using Microsoft.AspNetCore.Http;
using MS_Test_Fullstack.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS_Test_Fullstack.Domain.Interfaces
{
    public interface IManagerAirports
    {
        Task<Result<IEnumerable<IataCodes>>> GetAirports();
    }
}
