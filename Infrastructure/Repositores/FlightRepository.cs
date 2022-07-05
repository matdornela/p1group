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

        public async Task<List<Flight>> GetFlightsByDestinationAsync(Guid airportDestinationId, int pageNumber, int pageSize)
        {
            var startingRecordNumber = pageSize * (pageNumber - 1);

            var data = await _context.Flights
                .Where(x => x._destinationAirportId == airportDestinationId)
                .Skip(startingRecordNumber)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
            return data;
        }

        public async Task<List<Flight>> GetAllAsync()
        {
            return await _context.Flights.ToListAsync();
        }

        public async Task<Flight> AddAsync(Flight flight)

        {
            await _context.AddAsync(flight);
            return flight;
        }

        public async Task<Flight> GetAsync(Guid flightId)
        {
            return await _context.Flights.FirstOrDefaultAsync(x => x.Id == flightId);
        }
    }
}