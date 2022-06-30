using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.FlightAggregate
{
    public interface IFlightRateRepository
    {
        Task<FlightRate> GetRateByFlightAsync(Guid flightId);
        Task UpdateAvailablity(Guid flightRateId, int numberOfSeatsReserved);
    }
}
