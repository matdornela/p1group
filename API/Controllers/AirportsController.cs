using API.Application.Commands;
using API.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AirportsController : ControllerBase
    {
        private readonly ILogger<AirportsController> _logger;
        private readonly IMediator _mediator;

        public AirportsController(
            ILogger<AirportsController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Store")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(AirportViewModel), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Store([FromBody] CreateAirportCommand command)
        {
            if (string.IsNullOrEmpty(command.Code) || string.IsNullOrEmpty(command.Name))
            {
                return BadRequest();
            }
            var data = await _mediator.Send(command);

            return CreatedAtAction(nameof(Store), null, data);
        }
    }
}