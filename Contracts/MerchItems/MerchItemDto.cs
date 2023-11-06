using Contracts.Images;

namespace Contracts.MerchItems;

public record struct MerchItemDto(Guid Id,
                                  Guid TypeId,
                                  string Name,
                                  string? Description,
                                  decimal Price,
                                  decimal SelfPrice,
                                  int AmountLeft,
                                  float Benefit,
                                  DateTime CreatedAt,
                                  IEnumerable<ImageDto> Images);
