using Domain.Aggregates.FlightAggregate;
using Domain.Common;
using Domain.SeedWork;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Aggregates.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        [Key]
        public Guid Id { get; set; }

        public Guid FlightId { get; set; }
        public Flight Flight { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public OrderEnum State { get; set; } = OrderEnum.Draft;
        public int NumberOfPassangers { get; set; }

        public Order(Guid flightId, decimal price, int numberOfPassangers)
        {
            FlightId = flightId;
            Price = price;
            NumberOfPassangers = numberOfPassangers;
        }

        public Order()
        {
        }
    }
}