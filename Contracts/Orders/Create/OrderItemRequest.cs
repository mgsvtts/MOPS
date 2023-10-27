namespace Contracts.Orders.Create;
public record struct OrderItemRequest(Guid MerchItemId,
                                      int Amount);
