namespace Contracts.MerchItems.Calculate;
public sealed record CalculateItemRequest(Guid ItemId,
                                          int Amount);
