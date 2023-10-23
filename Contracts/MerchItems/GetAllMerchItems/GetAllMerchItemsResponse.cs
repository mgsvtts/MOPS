namespace Contracts.MerchItems.GetAllMerchItems;
public sealed record GetAllMerchItemsResponse(IEnumerable<MerchItemDto> Items);
