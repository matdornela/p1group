using API.Application.Queries;
using API.Application.ViewModels;
using AutoMapper;
using Domain.Aggregates.FlightAggregate;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Handlers
{
    public class SearchFlightsByDestinationHandler : IRequestHandler<SearchFlightsByDestinationQuery, List<FlightViewModel>>
    {
        private readonly IFlightRepository _flightRepository;

        private readonly IMapper _mapper;

        public SearchFlightsByDestinationHandler(IFlightRepository flightRepository, IMapper mapper)
        {
            _flightRepository = flightRepository;
            _mapper = mapper;
        }

        public async Task<List<FlightViewModel>> Handle(SearchFlightsByDestinationQuery request, CancellationToken cancellationToken)
        {
            var data = await _flightRepository.GetFlightsByDestinationAsync(request._destinationAirportId);
            return _mapper.Map<List<FlightViewModel>>(data);
        }
    }
}
