using API.Application.Commands;
using API.Application.Handlers;
using API.Mapping;
using AutoFixture.Xunit2;
using AutoMapper;
using Domain.Aggregates.AirportAggregate;
using FluentAssertions;
using NSubstitute;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Orders.Commands
{
    public class CreateAirportHandlerTests
    {
        private readonly IAirportRepository _airportRepository;
        private readonly IMapper _mapper;

        public CreateAirportHandlerTests()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<OrderProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _airportRepository = Substitute.For<IAirportRepository>();
        }

        [Theory, AutoData]
        public async Task CreateOrder_GivenOrder_ReturnsOrder(Airport airport, Guid id)
        {
            //Arrange
            airport.Name = "Guarulhos";
            airport.Code = "GRU";

            _airportRepository.WhenForAnyArgs(x => x.AddAsync(airport))
                .Do(x => airport.Id = id);

            var command = new CreateAirportCommand(airport.Code, airport.Name);
            var handler = new CreateAirportHandler(_airportRepository, _mapper);

            //Act
            await handler
                .Handle(command, CancellationToken.None);

            //Assert
            await _airportRepository.Received(1).AddAsync(Arg.Any<Airport>());
            id.Should().Be(airport.Id);
        }
    }
}