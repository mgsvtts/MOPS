using Domain.OrderAggregate.ValueObjects;

namespace Contracts.Orders.Create;
public record struct CreateOrderResponse(Guid Id,
                                         IEnumerable<OrderItemResponse> Items,
                                         PaymentMethod PaymentMethod);
