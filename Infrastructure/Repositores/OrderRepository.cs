using Domain.Aggregates.OrderAggregate;
using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Repositores
{
    public class OrderRepository : IOrderRepository
    {
        private readonly FlightsContext _context;

        public IUnitOfWork UnitOfWork
        {
            get { return _context; }
        }

        public OrderRepository(FlightsContext context)
        {
            _context = context;
        }

        public Order Add(Order order)

        {
            order.State = Domain.Common.OrderEnum.Draft;
            return _context.Orders.Add(order).Entity;
        }

        public async Task Confirm(Guid orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
            if (order != null)
                order.State = Domain.Common.OrderEnum.Confirmed;
            Update(order);
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
        }

        public async Task<Order> GetAsync(Guid orderId)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        }
    }
}