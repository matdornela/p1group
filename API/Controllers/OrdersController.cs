using API.Application.Commands;
using AutoMapper;
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
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IMediator _mediator;

        public OrdersController(ILogger<OrdersController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
        {
            if (command.NumberOfPassangers == 0 || command.FlightId == Guid.Empty)
            {
                return BadRequest();
            }

            var order = await _mediator.Send(command);

            if (order.Id == Guid.Empty)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(Create), null, order);
        }

        [HttpPost]
        [Route("Confirm")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Confirm([FromBody] ConfirmOrderCommand command)
        {
            if (command.Id == Guid.Empty)
            {
                return BadRequest();
            }

            var order = await _mediator.Send(command);

            if (order.Id == Guid.Empty)
            {
                return NotFound();
            }

            return Ok(order);
        }
    }
}