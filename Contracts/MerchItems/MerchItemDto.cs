namespace Contracts.MerchItems;

public sealed record MerchItemDto(Guid Id,
                                  Guid TypeId,
                                  string Name,
                                  string? Description,
                                  decimal Price,
                                  decimal SelfPrice,
                                  int AmountLeft,
                                  string Benefit);
