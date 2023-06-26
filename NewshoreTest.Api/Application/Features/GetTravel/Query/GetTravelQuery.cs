using MediatR;

namespace NewshoreTest.Api.Application.Features.GetTravel.Query
{
    public class GetTravelQuery : IRequest<GetTravelResponse>
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Currency { get; set; }
    }
}
