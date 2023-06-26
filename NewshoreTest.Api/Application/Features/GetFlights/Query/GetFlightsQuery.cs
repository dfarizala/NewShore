using MediatR;

namespace NewshoreTest.Api.Application.Features.GetFlights.Query
{
    public class GetFlightsQuery : IRequest<List<GetFlightsResponse>>
    {
    }
}
