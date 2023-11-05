using Domain.OrderAggregate.ValueObjects;
using MediatR;

namespace Application.Commands.Orders.Delete;

public record struct DeleteOrderCommand(OrderId Id) : IRequest;
