using Domain.OrderAggregate.ValueObjects;

namespace Presentation.Endpoints.Orders.Post.Create;
public sealed record CreateOrderRequest(IEnumerable<OrderItemRequest> Items,
                                        PaymentMethod PaymentMethod);
public record struct OrderItemRequest(Guid MerchItemId,
                                      int Amount);