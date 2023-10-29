﻿using Domain.OrderAggregate.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Orders.Delete;
public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly IOrderRepository _orderRepository;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var type = await _orderRepository.GetByIdAsync(request.Id)
                   ?? throw new InvalidOperationException($"Cannot find order with id {request.Id.Identity}");

        await _orderRepository.DeleteAsync(type);
    }
}