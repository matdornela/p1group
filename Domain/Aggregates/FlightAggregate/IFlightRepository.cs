using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Aggregates.FlightAggregate
{
    public interface IFlightRepository
    {
        Task<List<Flight>> GetFlightsByDestinationAsync(Guid airportDestinationId, int pageNumber,
            int pageSize);

        Task<List<Flight>> GetAllAsync();

        Task<Flight> AddAsync(Flight flight);

        Task<Flight> GetAsync(Guid flightId);
    }
}