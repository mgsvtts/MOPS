namespace Presentation.Endpoints.MerchItems.Post.Calculate;
public sealed record CalculateItemRequest(Guid ItemId,
                                          int Amount);