namespace Contracts.Orders.Create;
public record struct OrderItemResponse(Guid ItemId, int Amount, decimal Price);
