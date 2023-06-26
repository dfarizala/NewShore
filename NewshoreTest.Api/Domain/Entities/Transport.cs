namespace NewshoreTest.Api.Domain.Entities
{
    public class Transport
    {
        public int Id { get; set; }
        public int FlightCarrier { get; set; }
        public string FlightNumber { get; set; }
        public int Aircraft { get; set; }
    }
}
