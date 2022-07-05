using Domain.Exceptions;
using Domain.SeedWork;
using System;

namespace Domain.Aggregates.AirportAggregate
{
    public class Airport : Entity, IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Code { get; set; }

        public string Name { get; set; }

        public Airport()
        {
        }

        public Airport(string code, string name) : this()
        {
            if (code.Length != 3)
            {
                throw new AirportDomainException("The Airport code must be three characters.");
            }

            Code = code;
            Name = name;
        }
    }
}