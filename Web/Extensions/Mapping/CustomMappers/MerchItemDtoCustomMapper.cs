using Contracts.MerchItems;

namespace Web.Extensions.Mapping.CustomMappers;

public class MerchItemDtoCustomMapper
{
    public static MerchItemDto Map(Domain.MerchItemAggregate.MerchItem src)
    {
        var benefit = src.GetBenefitPercent();
        var percent = benefit > 1 ? $"+{benefit - 1:#0%}" : $"-{1 - benefit:#0%}";

        return new MerchItemDto(src.Id.Identity,
                                src.TypeId.Identity,
                                src.Name.Value,
                                src.Description?.Value,
                                src.Price.Value,
                                src.SelfPrice.Value,
                                src.AmountLeft.Value,
                                percent,
                                src.CreatedAt);
    }
}
