using Domain.Common;
using Domain.Events;
using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Domain.Aggregates.FlightAggregate
{
    public class Flight : Entity, IAggregateRoot
    {
        [Key]
        public Guid Id { get; set; }

        public Guid _originAirportId;
        public Guid _destinationAirportId;

        public DateTimeOffset Departure { get; private set; }
        public DateTimeOffset Arrival { get; private set; }

        private List<FlightRate> _rates;
        public IReadOnlyCollection<FlightRate> Rates => _rates;

        public Flight()
        {
            _rates = new List<FlightRate>();
        }

        public Flight(DateTimeOffset departure, DateTimeOffset arrival, Guid originAirportId, Guid
            destinationAirportId) : this()
        {
            _originAirportId = originAirportId;
            _destinationAirportId = destinationAirportId;
            Departure = departure;
            Arrival = arrival;
        }

        public void AddRate(string name, Price price, int numAvailable)
        {
            var rate = new FlightRate(name, price, numAvailable);
            _rates.Add(rate);
        }

        public void UpdateRatePrice(Guid rateId, Price price)
        {
            var rate = GetRate(rateId);

            rate.ChangePrice(price);

            AddDomainEvent(new FlightRatePriceChangedEvent(this, rate));
        }

        public void MutateRateAvailability(Guid rateId, int mutation)
        {
            var rate = GetRate(rateId);

            rate.MutateAvailability(mutation);

            AddDomainEvent(new FlightRateAvailabilityChangedEvent(this, rate, mutation));
        }

        private FlightRate GetRate(Guid rateId)
        {
            var rate = _rates.SingleOrDefault(o => o.Id == rateId);

            if (rate == null)
            {
                throw new ArgumentException("This flight does not contain a rate with the provided rateId");
            }

            return rate;
        }
    }
}