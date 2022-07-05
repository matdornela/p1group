using System;

namespace API.Application.ViewModels
{
    public class FlightViewModel
    {
        public Guid Id { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public Guid _destinationAirportId { get; set; }
        public Guid _originAirportId { get; set; }
    }
}