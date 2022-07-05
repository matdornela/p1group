using API.Application.Queries;
using API.Application.ViewModels;
using AutoMapper;
using Domain.Aggregates.FlightAggregate;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Handlers
{
    public class SearchFlightsLowestPriceByDestinationHandler : IRequestHandler<SearchFlightsLowestPriceByDestinationQuery, ListFlightWithLowestPriceViewModel>
    {
        private readonly IFlightRepository _flightRepository;

        private readonly IFlightRateRepository _flightRateRepository;

        private readonly IMapper _mapper;

        public SearchFlightsLowestPriceByDestinationHandler(IFlightRepository flightRepository, IMapper mapper, IFlightRateRepository flightRateRepository)
        {
            _flightRepository = flightRepository;
            _mapper = mapper;
            _flightRateRepository = flightRateRepository;
        }

        public async Task<ListFlightWithLowestPriceViewModel> Handle(SearchFlightsLowestPriceByDestinationQuery request, CancellationToken cancellationToken)
        {
            var viewModel = new ListFlightWithLowestPriceViewModel();

            var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
            var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            var flightDetails = await _flightRepository.GetFlightsByDestinationAsync(request._destinationAirportId, pageNumber, pageSize);

            if (flightDetails.Any())
            {
                var lowestPrice = await _flightRateRepository.GetLowestPriceFlightsByDestinationAsync(request._destinationAirportId);

                viewModel = new ListFlightWithLowestPriceViewModel
                {
                    LowestFlightPrice = lowestPrice,
                    FlightsDetails = _mapper.Map<List<FlightViewModel>>(flightDetails)
                };
            }

            return _mapper.Map<ListFlightWithLowestPriceViewModel>(viewModel);
        }
    }
}