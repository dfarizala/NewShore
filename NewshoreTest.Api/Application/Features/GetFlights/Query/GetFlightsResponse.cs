namespace NewshoreTest.Api.Application.Features.GetFlights.Query
{
    public class GetFlightsResponse
    {
        public DateTime FlightDate { get; set; }
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public string FlightCarrier { get; set; }
        public string FLightNomber { get; set; }
        public decimal Price { get; set; }
    }
}
