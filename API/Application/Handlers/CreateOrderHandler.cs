using API.Application.Commands;
using API.Application.ViewModels;
using AutoMapper;
using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.OrderAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Handlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderViewModel>
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IFlightRateRepository _flightRateRepository;

        private readonly IMapper _mapper;

        public CreateOrderHandler(IOrderRepository orderRepository, IFlightRepository flightRepository, IFlightRateRepository flightRateRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _flightRateRepository = flightRateRepository;
            _mapper = mapper;

        }

        public async Task<OrderViewModel> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            var flightRates = await _flightRateRepository.GetRateByFlightAsync(request.FlightId);
            OrderViewModel viewModel = new OrderViewModel();

            if(flightRates != null)
            {
                var order = _orderRepository.Add(new Order(request.FlightId, flightRates.Price.Value, request.NumberOfPassangers));
                await _orderRepository.UnitOfWork.SaveEntitiesAsync();
                viewModel = _mapper.Map<OrderViewModel>(order);
            }

            return viewModel;
        }
    }
}