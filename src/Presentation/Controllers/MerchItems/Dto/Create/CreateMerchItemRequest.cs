using Microsoft.AspNetCore.Http;

namespace Contracts.MerchItems.Create;
public sealed record CreateMerchItemRequest(Guid TypeId,
                                            string Name,
                                            string? Description,
                                            decimal Price,
                                            decimal SelfPrice,
                                            int AmountLeft,
                                            IEnumerable<CreateMerchItemRequestImage>? Images);

public record CreateMerchItemRequestImage(IFormFile File, bool IsMain);
