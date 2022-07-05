using Domain.Common;
using Domain.SeedWork;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Aggregates.FlightAggregate
{
    public class FlightRate : Entity
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public Price Price { get; set; }
        public int Available { get; set; }
        public Guid FlightId { get; set; }
        public Flight Flight { get; set; }

        public FlightRate()
        {
        }

        public FlightRate(string name, Price price, int available)
        {
            Name = name;
            Price = price;
            Available = available;
        }

        public void ChangePrice(Price price)
        {
            Price = price;
        }

        public void MutateAvailability(int quantity)
        {
            Available += quantity;
        }
    }
}