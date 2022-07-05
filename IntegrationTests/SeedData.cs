using Domain.Aggregates.AirportAggregate;
using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.OrderAggregate;
using Domain.Common;
using Infrastructure;
using System;

namespace IntegrationTests
{
    public static class SeedData
    {
        public const string FLIGHT_ID = "3fc00054-05d1-4721-af42-432210978aa9";

        public static void PopulateTestData(FlightsContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            dbContext.Airports.Add(new Airport
            {
                Id = new Guid("a0a66824-5513-421b-aec2-e1f84c0096a4"),
                Name = "Guarulhos",
                Code = "GRU"
            });
            dbContext.Airports.Add(new Airport
            {
                Id = new Guid("3eb5c618-05a5-4d99-8dc7-f764ae8d13f6"),
                Name = "Amazon Web Services",
                Code = "AWS"
            });

            var flight = new Flight
            {
                _destinationAirportId = new Guid("a0a66824-5513-421b-aec2-e1f84c0096a4"),
                _originAirportId = new Guid("3eb5c618-05a5-4d99-8dc7-f764ae8d13f6"),
                Id = new Guid(FLIGHT_ID)
            };
            dbContext.Flights.Add(flight);

            dbContext.FlightRates.Add(new FlightRate
            {
                Id = new Guid("3909c4e5-bb20-42e9-a5c5-e0fb06e9a72e"),
                Price = new Price(2000, Currency.EUR),
                FlightId = new Guid("3fc00054-05d1-4721-af42-432210978aa9"),
                Name = "Rate 1",
                Flight = flight
            });

            dbContext.Orders.Add(new Order
            {
                Id = new Guid("c47ccfaa-e67b-4b39-97fd-56b5329553da"),
                Price = 2700,
                NumberOfPassangers = 20,
                FlightId = new Guid("3fc00054-05d1-4721-af42-432210978aa9"),
                Flight = flight
            });

            dbContext.SaveChanges();
        }
    }
}