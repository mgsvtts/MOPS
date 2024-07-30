using Domain.OrderAggregate.ValueObjects;
using Mediator;

namespace Application.Commands.Orders.Delete;

public sealed record DeleteOrderCommand(OrderId Id) : ICommand;