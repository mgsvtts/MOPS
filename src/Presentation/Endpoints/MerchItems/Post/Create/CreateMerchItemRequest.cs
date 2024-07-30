using Microsoft.AspNetCore.Http;

namespace Presentation.Endpoints.MerchItems.Post.Create;
public sealed record CreateMerchItemRequest(Guid TypeId,
                                            string Name,
                                            string? Description,
                                            decimal Price,
                                            decimal SelfPrice,
                                            int AmountLeft,
                                            IEnumerable<CreateMerchItemRequestImage>? Images);

public record CreateMerchItemRequestImage(IFormFile File, bool IsMain);