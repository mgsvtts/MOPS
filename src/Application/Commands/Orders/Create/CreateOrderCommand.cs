using Domain.OrderAggregate;
using Domain.OrderAggregate.ValueObjects;
using Mediator;

namespace Application.Commands.Orders.Create;
public sealed record CreateOrderCommand(IEnumerable<OrderItem> Items,
                                        PaymentMethod PaymentMethod) : ICommand<Order>;
