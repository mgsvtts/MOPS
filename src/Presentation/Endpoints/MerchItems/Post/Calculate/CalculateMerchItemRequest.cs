namespace Presentation.Endpoints.MerchItems.Post.Calculate;
public sealed record CalculateMerchItemRequest(Guid ItemId,
                                               int Amount);