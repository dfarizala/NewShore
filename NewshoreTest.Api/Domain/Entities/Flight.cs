namespace NewshoreTest.Api.Domain.Entities
{
    public class Flight
    {
        public int Id { get; set; }
        public int TransportId { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public decimal Price { get; set; }
    }
}
