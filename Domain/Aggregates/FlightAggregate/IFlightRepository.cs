using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Aggregates.FlightAggregate
{
    public interface IFlightRepository
    {
        Task<List<Flight>> GetFlightsByDestinationAsync(Guid airportDestinationId);
        Task<FlightRate> CheckFlightPriceChangesAsync(Guid flightId);
        Task<FlightRate> GetLowestPriceFlightsByDestinationAsync(Guid airportDestinationId);
    }
}