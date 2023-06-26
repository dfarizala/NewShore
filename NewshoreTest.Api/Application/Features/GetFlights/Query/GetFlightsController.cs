using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NewshoreTest.Api.Application.Features.GetFlights.Query
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetFlightsController : Controller
    {
        private readonly IMediator _Mediator;

        public GetFlightsController(IMediator mediator)
        {
            _Mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<GetFlightsResponse>>> Get()
        {
            var result = await _Mediator.Send(new GetFlightsQuery());
            if (result == null) return NoContent();
            return Ok(result);
        }
    }
}
