using AutoFixture;
using Domain.Aggregates.AirportAggregate;
using Domain.Aggregates.FlightAggregate;
using FluentAssertions;
using Infrastructure;
using Infrastructure.Repositores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Orders.Queries
{
    public class SearchFlightsLowestPriceByDestinationHandlerTests
    {
        private readonly DbContextOptions<FlightsContext> _dbContextOptions;

        public SearchFlightsLowestPriceByDestinationHandlerTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<FlightsContext>()
                .UseInMemoryDatabase($"flightdb_{DateTime.Now.ToString()}")
                .Options;
        }

        [Fact]
        public async Task SearchLowestFlights_GivenAirportId_ReturnListFlight()
        {
            //Arrange
            var flightsContext = new FlightsContext(_dbContextOptions);
            var flightRepository = new FlightRepository(flightsContext);
            var flightRateRepository = new FlightRateRepository(flightsContext);
            var aiportRepository = new AirportRepository(flightsContext);

            var fixture = new Fixture();

            var airport = fixture.Create<Airport>();

            var addedAirport = await aiportRepository.AddAsync(airport);
            await aiportRepository.UnitOfWork.SaveEntitiesAsync();

            for (int i = 0; i < 3; i++)
            {
                var flight = fixture.Build<Flight>()
                    .With(x => x.Id, Guid.NewGuid())
                    .With(x => x._destinationAirportId, addedAirport.Id)
                    .Create();

                var flightRate = fixture.Build<FlightRate>()
                    .With(x => x.Flight, flight)
                    .With(x => x.Id, Guid.NewGuid())
                    .Create();

                flightRate.Price.Value += 15;

                await flightRepository.AddAsync(flight);
                await flightRateRepository.AddAsync(flightRate);
                await flightsContext.SaveChangesAsync();
            }

            //Act
            using (var context = new FlightsContext(_dbContextOptions))
            {
                var listFlightRates = context.FlightRates
                    .Include(x => x.Flight)
                    .Where(x => x.Flight._destinationAirportId == airport.Id)
                    .ToList();

                if (listFlightRates.Any())
                {
                    var expectedPrice = listFlightRates.Min(x => x.Price.Value);
                    var actualPrice = await flightRateRepository.GetLowestPriceFlightsByDestinationAsync(airport.Id);

                    //Assert
                    expectedPrice.Should().Be(actualPrice);
                }
            }
        }
    }
}