using Domain.Aggregates.FlightAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Repositores
{
    public class FlightRateRepository : IFlightRateRepository
    {
        private readonly FlightsContext _context;

        public FlightRateRepository(FlightsContext context)
        {
            _context = context;
        }

        public async Task<FlightRate> GetRateByFlightAsync(Guid flightId)
        {
            return await _context.FlightRates
                                .FirstOrDefaultAsync(x => x.FlightId == flightId);
        }

        public async Task UpdateAvailablity(Guid flightRateId, int numberOfSeatsReserved)
        {
            var flight = await _context.FlightRates
                                .FirstOrDefaultAsync(x => x.Id == flightRateId);

            if (flight != null)
            {
                flight.Available = flight.Available - numberOfSeatsReserved;
                _context.FlightRates.Update(flight);
            }
        }
    }
}