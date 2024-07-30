using Domain.OrderAggregate.ValueObjects;

namespace Presentation.Endpoints.Orders.Post.Create;
public record struct CreateOrderResponse(Guid Id,
                                         IEnumerable<OrderItemResponse> Items,
                                         PaymentMethod PaymentMethod);

public record struct OrderItemResponse(Guid ItemId, int Amount, decimal Price);