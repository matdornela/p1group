using API.Application.ViewModels;
using MediatR;
using System;

namespace API.Application.Commands
{
    public class ConfirmOrderCommand : IRequest<OrderViewModel>
    {
        public Guid Id { get; private set; }

        public ConfirmOrderCommand(Guid id)
        {
            Id = id;
        }
    }
}