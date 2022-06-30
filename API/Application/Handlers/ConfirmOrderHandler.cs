using API.Application.Commands;
using API.Application.ViewModels;
using AutoMapper;
using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.OrderAggregate;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API.Application.Handlers
{
    public class ConfirmOrderHandler : IRequestHandler<ConfirmOrderCommand, OrderViewModel>
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IFlightRateRepository _flightRateRepository;

        private readonly IMapper _mapper;

        public ConfirmOrderHandler()
        {
        }

        public ConfirmOrderHandler(IOrderRepository orderRepository, IFlightRateRepository flightRateRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _flightRateRepository = flightRateRepository;
            _mapper = mapper;
        }

        public async Task<OrderViewModel> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetAsync(request.Id);
            var viewModel = new OrderViewModel();

            if (order != null)
            {
                var flightRate = await _flightRateRepository.GetRateByFlightAsync(order.FlightId);

                if (order.Price != flightRate.Price.Value)
                {
                    Console.WriteLine($"The Flight rate has been changed to {flightRate.Price.Value}");
                }

                await _orderRepository.Confirm(order.Id);
                await _flightRateRepository.UpdateAvailablity(flightRate.Id, order.NumberOfPassangers);
                await _orderRepository.UnitOfWork.SaveEntitiesAsync();
                viewModel = _mapper.Map<OrderViewModel>(order);
            }

            return viewModel;
        }
    }
}