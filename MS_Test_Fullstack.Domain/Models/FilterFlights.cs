using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS_Test_Fullstack.Domain.Models
{
    public class FilterFlights
    {
        public DateTime? FilterDate { get; set; }
        public string? IATACodeOrigin { get; set; }
        public string? IATACodeDestination { get; set; }

    }
}
