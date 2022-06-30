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

            var flightDetails = await _flightRepository.GetFlightsByDestinationAsync(request._destinationAirportId);

            if (flightDetails.Any())
            {
                var lowestPrice = await _flightRepository.GetLowestPriceFlightsByDestinationAsync(request._destinationAirportId);

                viewModel = new ListFlightWithLowestPriceViewModel
                {
                    LowestFlightPrice = new LowestPriceViewModel
                    {
                        Id = lowestPrice.Flight.Id,
                        Price = lowestPrice.Price.Value
                    },
                    FlightsDetails = _mapper.Map<List<FlightViewModel>>(flightDetails)
                };
            }

            return _mapper.Map<ListFlightWithLowestPriceViewModel>(viewModel);
        }
    }
}