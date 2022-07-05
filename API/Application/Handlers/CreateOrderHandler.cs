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

        private readonly IFlightRepository _flightRepository;

        private readonly IMapper _mapper;

        public CreateOrderHandler(IOrderRepository orderRepository, IMapper mapper, IFlightRepository flightRepository)
        {
            _orderRepository = orderRepository;
            _flightRepository = flightRepository;
            _mapper = mapper;
        }

        public CreateOrderHandler(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task<OrderViewModel> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var flight = await _flightRepository.GetAsync(request.FlightId);
            var viewModel = new OrderViewModel();

            if (flight != null)
            {
                var order = await _orderRepository.AddAsync(new Order(request.FlightId, request.Price, request.NumberOfPassangers));
                await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
                viewModel = _mapper.Map<OrderViewModel>(order);
            }

            return viewModel;
        }
    }
}