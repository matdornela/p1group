using System.IO;
using System.Reflection;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace API.Infrastructure
{
    public class FlightsDbContextFactory : IDesignTimeDbContextFactory<FlightsContext>
    {
        public FlightsContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<FlightsContext>();

            optionsBuilder.UseNpgsql(config["Database:ConnectionString"], npgsqlOptionsAction: o => o.MigrationsAssembly
                (typeof(Startup).GetTypeInfo().Assembly.GetName().Name));

            return new FlightsContext(optionsBuilder.Options);
        }
    }
}