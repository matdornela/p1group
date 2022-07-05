using System;
using System.Threading.Tasks;
using API.Infrastructure.Seeds;
using Domain.Aggregates.FlightAggregate;
using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API
{
    public static class HostExtensions
    {
        public static IHost SeedData(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var configuration = services.GetService<IConfiguration>();

                if (configuration.GetValue<bool>("Database:Seed"))
                {
                    var context = services.GetService<FlightsContext>();

                    //if (!context.Airports.AnyAsync().Result)
                    //{
                        new AirportsSeeder(context).Seed();
                        new FlightsSeeder(context).Seed();
                        new FlightRatesSeeder(context).Seed();
                        Console.WriteLine("Database seeded");
                    //}
                }
            }

            return host;
        }

        //public static IHost MigrateDatabase(this IHost host)
        //{
        //    using (var scope = host.Services.CreateScope())
        //    {
        //        var services = scope.ServiceProvider;
        //        var context = services.GetService<FlightsContext>();
        //        context.Database.Migrate();
        //    }

        //    return host;
        //}

        public static IHost ConnectToDatabase(this IHost host, int maxRetryCount, TimeSpan retryDelay)
        {
            {
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var context = services.GetService<FlightsContext>();
                    var connected = false;
                    var retries = 0;
                    while (!connected)
                    {
                        try
                        {
                            context.Database.CanConnect();
                            connected = true;
                        }
                        catch (Exception e)
                        {
                            retries++;
                            if (retries > maxRetryCount)
                            {
                                throw e;
                            }

                            Task.Delay(retryDelay).Wait();
                        }
                    }
                }

                return host;
            }
        }
    }
}