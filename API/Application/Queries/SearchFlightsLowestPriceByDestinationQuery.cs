using API.Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;

namespace API.Application.Queries
{
    public class SearchFlightsLowestPriceByDestinationQuery : IRequest<ListFlightWithLowestPriceViewModel>
    {
        public Guid _destinationAirportId { get; private set; }

        public SearchFlightsLowestPriceByDestinationQuery(Guid destinationAirportId)
        {
            _destinationAirportId = destinationAirportId;
        }
    }
}