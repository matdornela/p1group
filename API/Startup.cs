using System;
using System.Reflection;
using Domain.Aggregates.AirportAggregate;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Repositores;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using Domain.Aggregates.OrderAggregate;
using Domain.Aggregates.FlightAggregate;
using System.Text.Json.Serialization;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(typeof(Startup).Assembly));

            services.AddControllers().AddJsonOptions(x =>
            {
                // serialize enums as strings in api responses (e.g. Role)
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMediatR(typeof(Startup));

            services.AddOpenApiDocument(d => d.Title = "AcmeFlights API");

            services.AddFlightsContext(
                Configuration["Database:ConnectionString"],
                typeof(Startup).GetTypeInfo().Assembly.GetName().Name);

            services.AddScoped<IAirportRepository, AirportRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<IFlightRateRepository, FlightRateRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}