using API.Application.Commands;
using API.Application.ViewModels;
using AutoMapper;
using Domain.Aggregates.AirportAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Handlers
{
    public class CreateAirportHandler : IRequestHandler<CreateAirportCommand, AirportViewModel>
    {
        private readonly IAirportRepository _airportRepository;

        private readonly IMapper _mapper;

        public CreateAirportHandler(IAirportRepository airportRepository, IMapper mapper)
        {
            _airportRepository = airportRepository;
            _mapper = mapper;
        }

        public async Task<AirportViewModel> Handle(CreateAirportCommand request, CancellationToken cancellationToken)
        {
            var airport = await _airportRepository.AddAsync(new Airport(request.Code, request.Name));

            await _airportRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            var data = _mapper.Map<AirportViewModel>(airport);

            return data;
        }
    }
}