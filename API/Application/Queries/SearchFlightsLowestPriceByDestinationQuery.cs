using API.Application.ViewModels;
using MediatR;
using System;

namespace API.Application.Queries
{
    public class SearchFlightsLowestPriceByDestinationQuery : IRequest<ListFlightWithLowestPriceViewModel>
    {
        public Guid _destinationAirportId { get; private set; }
        public int PageNumber { get; private set; }
        public byte PageSize { get; private set; }

        public SearchFlightsLowestPriceByDestinationQuery(Guid destinationAirportId, int pageNumber, byte pageSize)
        {
            _destinationAirportId = destinationAirportId;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}