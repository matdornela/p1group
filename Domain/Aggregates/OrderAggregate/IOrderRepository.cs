using Domain.SeedWork;
using System;
using System.Threading.Tasks;

namespace Domain.Aggregates.OrderAggregate
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetAsync(Guid orderId);

        Order Add(Order order);

        void Update(Order order);

        Task Confirm(Guid orderId);
    }
}