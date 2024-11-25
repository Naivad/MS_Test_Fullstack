namespace MS_Test_Fullstack.Domain.Models
{
    public class AvailableFlights
    {

        public int FlightID { get; set; }


        public int JourneyID { get; set; }


        public string? Origin { get; set; }

        public string? Destination { get; set; }


        public bool IsDirect { get; set; }


        public DateTime? DirectDepartureTime { get; set; }

        public DateTime? DirectArrivalTime { get; set; }

        public string? DirectCarrier { get; set; }


        public string? DirectFlightNumber { get; set; }

        public decimal? DirectPrice { get; set; }

        public int? FlightOrder { get; set; }

        public string? SegmentOrigin { get; set; }

        public string? SegmentDestination { get; set; }

        public DateTime? SegmentDepartureTime { get; set; }

        public DateTime? SegmentArrivalTime { get; set; }

        public string? SegmentCarrier { get; set; }

        public string? SegmentFlightNumber { get; set; }

        public decimal? SegmentPrice { get; set; }
        public string? TravelDirection { get; set; }
        public List<PathSegments>? PathSegments { get; set; }
    }


    public class PathSegments
    { 
        public string? SegmentOrigin { get; set; }
        public string? SegmentDestination { get; set; }
        public DateTime? SegmentDepartureTime { get; set; }
        public string? DirectCarrier { get; set; }
        public string? DirectFlightNumber { get; set; }

    }
}
