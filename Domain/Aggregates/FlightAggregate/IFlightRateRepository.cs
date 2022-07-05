using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Aggregates.FlightAggregate
{
    public interface IFlightRateRepository
    {
        Task<FlightRate> GetRateByFlightAsync(Guid flightId);

        public Task<List<FlightRate>> GetAllAsync();

        Task<FlightRate> GetAsync(Guid flightRateId);

        public Task<FlightRate> AddAsync(FlightRate flightRate);

        Task<FlightRate> UpdateAvailablity(Guid flightRateId, int numberOfSeatsReserved);

        Task<decimal> GetLowestPriceFlightsByDestinationAsync(Guid airportDestinationId);
    }
}