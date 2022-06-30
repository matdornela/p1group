using Domain.Aggregates.FlightAggregate;
using MediatR;

namespace Domain.Events
{
    public class FlightRatePriceChangedEvent : INotification
    {
        public Flight Flight { get; private set; }
        public FlightRate FlightRate { get; private set; }

        public FlightRatePriceChangedEvent(Flight flight, FlightRate flightRate)
        {
            Flight = flight;
            FlightRate = flightRate;
        }
    }
}