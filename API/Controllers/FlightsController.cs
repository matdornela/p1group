using API.Application.Queries;
using API.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FlightsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("SearchLowestPriceFlightByDestination")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ListFlightWithLowestPriceViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SearchLowestPriceFlightByDestination(Guid _destinationAirportId, int pageNumber, byte pageSize)
        {
            if (_destinationAirportId == Guid.Empty)
            {
                return BadRequest();
            }
            var query = new SearchFlightsLowestPriceByDestinationQuery(_destinationAirportId, pageNumber, pageSize);
            var data = await _mediator.Send(query);

            if (data.FlightsDetails == null)
            {
                return NotFound();
            }

            return Ok(data);
        }
    }
}