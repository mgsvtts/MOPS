using Domain.OrderAggregate.ValueObjects;

namespace Contracts.Orders.Create;
public sealed record CreateOrderRequest(IEnumerable<OrderItemRequest> Items,
                                        PaymentMethod PaymentMethod);
