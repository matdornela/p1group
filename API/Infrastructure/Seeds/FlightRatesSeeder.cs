using System;
using System.Linq;
using Domain.Aggregates.FlightAggregate;
using Domain.Common;
using Infrastructure;

namespace API.Infrastructure.Seeds
{
    public class FlightRatesSeeder : FlightsContextSeeder
    {
        public FlightRatesSeeder(FlightsContext flightsContext) : base(flightsContext)
        {
        }

        public override void Seed()
        {
            if (FlightsContext.FlightRates.Any())
            {
                Console.WriteLine("Skipping FlightRates seeder because table is not empty.");
                return;
            };

            var flights = FlightsContext.Flights.ToList();
            var random = new Random();
            
            foreach (var flight in flights)
            {
                var numberOfClasses = random.Next(1, 3);
                var price = new Price(new decimal(random.Next(5000, 30000) / 2), Currency.EUR);
                
                flight.AddRate("Economy class", price, random.Next(20, 100));

                if (numberOfClasses > 1)
                {
                    var secondPrice = new Price(price.Value + new decimal(random.Next(5000, 30000) / 2), Currency.EUR);
                    flight.AddRate("Business class", secondPrice, random.Next(20, 100));
                }

                if (numberOfClasses > 2)
                {
                    var thirdPrice = new Price(price.Value + new decimal(random.Next(5000, 30000) / 2), Currency.EUR);
                    flight.AddRate("First class", thirdPrice, random.Next(20, 100));
                }
            }

            FlightsContext.SaveChanges();
        }
    }
}