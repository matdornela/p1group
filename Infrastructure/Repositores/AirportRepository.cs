using Domain.Aggregates.AirportAggregate;
using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Repositores
{
    public class AirportRepository : IAirportRepository
    {
        private readonly FlightsContext _context;

        public IUnitOfWork UnitOfWork
        {
            get { return _context; }
        }

        public AirportRepository(FlightsContext context)
        {
            _context = context;
        }

        public async Task<Airport> AddAsync(Airport airport)
        {
            await _context.AddAsync(airport);
            return airport;
        }

        public void Update(Airport airport)
        {
            _context.Airports.Update(airport);
        }

        public async Task<Airport> GetAsync(Guid airportId)
        {
            return await _context.Airports.FirstOrDefaultAsync(o => o.Id == airportId);
        }
    }
}