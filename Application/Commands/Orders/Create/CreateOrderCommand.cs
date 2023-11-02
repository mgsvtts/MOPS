using Domain.OrderAggregate;
using Domain.OrderAggregate.ValueObjects;
using MediatR;

namespace Application.Commands.Orders.Create;
public record struct CreateOrderCommand(IEnumerable<OrderItem> Items,
                                        PaymentMethod PaymentMethod) : IRequest<Order>;
