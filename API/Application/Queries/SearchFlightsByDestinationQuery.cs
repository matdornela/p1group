using API.Application.ViewModels;
using Domain.Aggregates.FlightAggregate;
using MediatR;
using System;
using System.Collections.Generic;

namespace API.Application.Queries
{
    public class SearchFlightsByDestinationQuery : IRequest<List<FlightViewModel>>
    {
        public Guid _destinationAirportId { get; private set; }

        public SearchFlightsByDestinationQuery(Guid destinationAirportId)
        {
            _destinationAirportId = destinationAirportId;

        }
    }
}
