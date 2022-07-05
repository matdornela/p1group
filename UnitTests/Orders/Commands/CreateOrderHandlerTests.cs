using API.Application.Commands;
using API.Application.Handlers;
using API.Mapping;
using AutoFixture.Xunit2;
using AutoMapper;
using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.OrderAggregate;
using FluentAssertions;
using NSubstitute;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Orders.Commands
{
    public class CreateOrderHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IFlightRepository _flightRepository;

        public CreateOrderHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<OrderProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _flightRepository = Substitute.For<IFlightRepository>();
            _orderRepository = Substitute.For<IOrderRepository>();
        }

        [Theory, AutoData]
        public async Task CreateOrder_GivenOrder_ReturnsOrder(Order order, Guid id, Flight flight)
        {
            //Arrange
            order.NumberOfPassangers = 207;
            order.Price = 2009;
            order.FlightId = Guid.NewGuid();

            _flightRepository.GetAsync(order.FlightId).Returns(flight);
            _orderRepository.WhenForAnyArgs(x => x.AddAsync(order))
                           .Do(x => order.Id = id);
            var command = new CreateOrderCommand(order.FlightId, order.NumberOfPassangers, order.Price);
            var handler = new CreateOrderHandler(_orderRepository, _mapper, _flightRepository);

            //Act
            await handler
                .Handle(command, CancellationToken.None);

            //Assert
            await _orderRepository.Received(1).AddAsync(Arg.Any<Order>());
            id.Should().Be(order.Id);
        }
    }
}