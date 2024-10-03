namespace Presentation.Endpoints.MerchItems.Common;

public readonly record struct MerchItemDto(Guid Id,
                                  Guid TypeId,
                                  string Name,
                                  string? Description,
                                  decimal Price,
                                  decimal SelfPrice,
                                  int AmountLeft,
                                  MerchItemBenefit Benefit,
                                  DateTime CreatedAt,
                                  IEnumerable<ImageDto> Images);

public readonly record struct MerchItemBenefit(string Percent, 
                                               decimal Value);