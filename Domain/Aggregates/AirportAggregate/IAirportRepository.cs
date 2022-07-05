using Domain.SeedWork;
using System;
using System.Threading.Tasks;

namespace Domain.Aggregates.AirportAggregate
{
    public interface IAirportRepository : IRepository<Airport>
    {
        Task<Airport> AddAsync(Airport airport);

        void Update(Airport airport);

        Task<Airport> GetAsync(Guid airportId);
    }
}