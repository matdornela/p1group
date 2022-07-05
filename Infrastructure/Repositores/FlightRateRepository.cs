using Domain.Aggregates.FlightAggregate;
using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositores
{
    public class FlightRateRepository : IFlightRateRepository
    {
        private readonly FlightsContext _context;

        public IUnitOfWork UnitOfWork
        {
            get { return _context; }
        }

        public FlightRateRepository(FlightsContext context)
        {
            _context = context;
        }

        public async Task<FlightRate> GetRateByFlightAsync(Guid flightId)
        {
            return await _context.FlightRates
                                .FirstOrDefaultAsync(x => x.FlightId == flightId);
        }

        public async Task<List<FlightRate>> GetAllAsync()
        {
            return await _context.FlightRates.ToListAsync();
        }

        public async Task<FlightRate> GetAsync(Guid flightRateId)
        {
            return await _context.FlightRates.FirstOrDefaultAsync(x => x.Id == flightRateId);
        }

        public async Task<FlightRate> AddAsync(FlightRate flightRate)
        {
            await _context.AddAsync(flightRate);
            return flightRate;
        }

        public async Task<FlightRate> UpdateAvailablity(Guid flightRateId, int numberOfSeatsReserved)
        {
            var flightRate = await _context.FlightRates
                                .FirstOrDefaultAsync(x => x.Id == flightRateId);

            if (flightRate != null)
            {
                flightRate.Available -= numberOfSeatsReserved;
                _context.FlightRates.Update(flightRate);
            }

            return flightRate;
        }

        public async Task<decimal> GetLowestPriceFlightsByDestinationAsync(Guid airportDestinationId)
        {
            {
                var lowestFlightRate = await _context.FlightRates
                    .Include(x => x.Flight)
                    .Where(x => x.Flight._destinationAirportId == airportDestinationId)
                    .MinAsync(x => x.Price.Value);

                return lowestFlightRate;
            }
        }
    }
}