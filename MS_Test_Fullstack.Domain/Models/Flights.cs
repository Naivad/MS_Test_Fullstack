namespace MS_Test_Fullstack.Domain.Models
{


    public class Flights
    {
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public decimal Price { get; set; }
        public Transport? Transport { get; set; }
    }

    public class Transport
    {
        public string? FlightCarrier { get; set; }
        public string? FlightNumber { get; set; }
    }



}
