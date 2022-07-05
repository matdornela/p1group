using API.Application.ViewModels;
using MediatR;
using System;

namespace API.Application.Commands
{
    public class CreateOrderCommand : IRequest<OrderViewModel>
    {
        public Guid FlightId { get; private set; }
        public int NumberOfPassangers { get; private set; }
        public decimal Price { get; private set; }

        public CreateOrderCommand(Guid flightId, int numberOfPassangers, decimal price)
        {
            FlightId = flightId;
            NumberOfPassangers = numberOfPassangers;
            Price = price;
        }
    }
}