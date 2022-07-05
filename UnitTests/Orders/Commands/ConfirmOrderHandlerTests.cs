using API.Application.Commands;
using API.Application.Handlers;
using API.Mapping;
using AutoFixture.Xunit2;
using AutoMapper;
using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.OrderAggregate;
using Domain.Common;
using FluentAssertions;
using Infrastructure;
using Infrastructure.Repositores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Orders.Commands
{
    public class ConfirmOrderHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly DbContextOptions<FlightsContext> _dbContextOptions;

        public ConfirmOrderHandlerTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<FlightsContext>()
                .UseInMemoryDatabase($"flightdb_{DateTime.Now.ToString()}")
                .Options;

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<OrderProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Theory, AutoData]
        public async Task ConfirmOrder_GivenOrderId_ReturnsConfirmedOrder(Order order, Flight flight, FlightRate flightRate)
        {
            //Arrange
            var flightsContext = new FlightsContext(_dbContextOptions);
            var orderRepository = new OrderRepository(flightsContext);
            var flightRepository = new FlightRepository(flightsContext);
            var flightRateRepository = new FlightRateRepository(flightsContext);

            order.State = OrderEnum.Draft;
            order.NumberOfPassangers = 20;
            order.Price = 2000;
            order.Flight = flight;
            flightRate.Flight = flight;

            await orderRepository.AddAsync(order);
            await flightRepository.AddAsync(flight);
            await flightRateRepository.AddAsync(flightRate);
            await flightsContext.SaveEntitiesAsync();
            var command = new ConfirmOrderCommand(order.Id);
            var handler = new ConfirmOrderHandler(orderRepository, flightRateRepository, _mapper);
            var newAvailabilityNumber = flightRate.Available - order.NumberOfPassangers;

            //Act
            await handler
                .Handle(command, CancellationToken.None);

            //Assert
            order.State.Should().Be(OrderEnum.Confirmed);
            flightRate.Available.Should().Be(newAvailabilityNumber);
        }
    }
}