using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Aggregates.AirportAggregate;
using Infrastructure;

namespace API.Infrastructure.Seeds
{
    public class AirportsSeeder : FlightsContextSeeder
    {
        public AirportsSeeder(FlightsContext flightsContext) : base(flightsContext)
        {
        }

        public override void Seed()
        {
            if (FlightsContext.Airports.Any())
            {
                Console.WriteLine("Skipping Airports seeder because table is not empty.");
                return;
            }

            var airports = new List<Airport>()
            {
                new Airport("AMS", "Amsterdam Airport Schiphol"),
                new Airport("FRA", "Frankfurt am Main Airport"),
                new Airport("LHR", "London Heathrow Airport"),
                new Airport("BCN", "Barcelona International Airport"),
                new Airport("CDG", "Charles de Gaulle International"),
                new Airport("IST", "Istanbul Airport"),
                new Airport("MUC", "Munich Airport"),
                new Airport("ZRH", "Zurich Airport"),
                new Airport("DUB", "Dublin Airport"),
                new Airport("FCO", "Rome Fiumicino Airport"),
                new Airport("ARN", "Stockholm Arlanda Airport"),
                new Airport("CPH", "Copenhagen Airport"),
            };

            FlightsContext.Airports.AddRange(airports);
            FlightsContext.SaveChanges();
        }
    }
}