using API.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly ILogger<FlightsController> _logger;
        private readonly IMediator _mediator;

        public FlightsController(
            ILogger<FlightsController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("SearchLowestPriceFlightByDestination")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> SearchLowestPriceFlightByDestination(Guid _destinationAirportId)
        {
            if (_destinationAirportId == Guid.Empty)
            {
                return BadRequest();
            }
            var query = new SearchFlightsLowestPriceByDestinationQuery(_destinationAirportId);
            var data = await _mediator.Send(query);

            if (data.FlightsDetails == null)
            {
                return NotFound();
            }

            return Ok(data);
        }
    }
}