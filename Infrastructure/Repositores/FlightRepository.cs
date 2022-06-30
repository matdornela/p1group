using Domain.Aggregates.FlightAggregate;
using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositores
{
    public class FlightRepository : IFlightRepository
    {
        private readonly FlightsContext _context;

        public IUnitOfWork UnitOfWork
        {
            get { return _context; }
        }

        public FlightRepository(FlightsContext context)
        {
            _context = context;
        }

        public async Task<List<Flight>> GetFlightsByDestinationAsync(Guid airportDestinationId)
        {
            var data = await _context.Flights
                .Where(x => x._destinationAirportId == airportDestinationId)
                .AsNoTracking()
                .ToListAsync();
            return data;
        }

        public async Task<FlightRate> GetLowestPriceFlightsByDestinationAsync(Guid airportDestinationId)
        {
            {

                var flightRates = await _context.FlightRates
                    .Include(x => x.Flight)
                    .Where(x => x.Flight._destinationAirportId == airportDestinationId)
                    .ToListAsync();
                  
                  var data = flightRates.GroupBy(x => x.Flight._destinationAirportId)
                    .SelectMany(g => g.Where(x => x.Price.Value == g.Min(y => y.Price.Value)))
                    .FirstOrDefault();

                return data;
            }
        }

        public async Task<FlightRate> CheckFlightPriceChangesAsync(Guid flightId)
        {
            var data = await _context.FlightRates.Include(x => x.Flight)
                .FirstOrDefaultAsync(x => x.Id == flightId);
            return data;
        }
    }
}