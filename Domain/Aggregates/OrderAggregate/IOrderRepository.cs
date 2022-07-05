using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Aggregates.OrderAggregate
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetAsync(Guid orderId);

        Task<List<Order>> GetAllAsync();

        Task<Order> AddAsync(Order order);

        void Update(Order order);

        Task<Order> Confirm(Guid orderId);
    }
}