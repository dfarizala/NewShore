using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewshoreTest.Api.Application.Features.GetFlights.Query;

namespace NewshoreTest.Api.Application.Features.GetTravel.Query
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetTravelController : Controller
    {
        private readonly IMediator _Mediator;

        public GetTravelController(IMediator mediator)
        {
            _Mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<GetTravelResponse>> Get([FromBody] GetTravelRequest Request)
        {
            var Query = new GetTravelQuery
            {
                Currency = Request.Currency,
                Destination = Request.Destination,
                Origin = Request.Origin
            };

            var result = await _Mediator.Send(Query);
            if (result == null) return NoContent();
            return Ok(result);
        }

    }
}
