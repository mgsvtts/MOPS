namespace Contracts.MerchItems.GetAllMerchItems;

public sealed record GetAllMerchItemsRequest(bool ShowNotAvailable = true,
                                             MerchItemSort Sort = MerchItemSort.NameAsc);


public enum MerchItemSort
{
    NameAsc,
    NameDesc,
    DescriptionAsc,
    DescriptionDesc,
    PriceAsc,
    PriceDesc,
    SelfPriceAsc,
    SelfPriceDesc,
    AmountAsc,
    AmountDesc,
    CreatedAsc,
    CreatedDesc
}
