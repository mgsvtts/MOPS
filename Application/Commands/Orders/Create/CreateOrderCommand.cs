using Domain.OrderAggregate;
using Domain.OrderAggregate.ValueObjects;
using MediatR;

namespace Application.Commands.Orders.Create;
public sealed record CreateOrderCommand(IEnumerable<OrderItem> Items,
                                        PaymentMethod PaymentMethod) : IRequest<Order>;
