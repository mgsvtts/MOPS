namespace Contracts.MerchItems.GetAllMerchItems;
public record struct GetAllMerchItemsResponse(IEnumerable<MerchItemDto> Items);
