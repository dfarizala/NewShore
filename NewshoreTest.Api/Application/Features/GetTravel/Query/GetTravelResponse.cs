namespace NewshoreTest.Api.Application.Features.GetTravel.Query
{
    public class GetTravelResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public JourneyElement Journey { get; set; }
    }

    public class JourneyElement
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public decimal Price { get; set; }
        public List<FlightList> Flights { get; set; }
    }

    public class FlightList
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public decimal Price { get; set; }
        public TransportElement Transport { get; set; }
    }

    public class TransportElement
    {
        public string FlightCarrier { get; set; }
        public string FLightNumber { get; set; }
    }
}
