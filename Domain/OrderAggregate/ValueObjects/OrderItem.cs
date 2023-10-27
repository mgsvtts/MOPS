using Domain.MerchItemAggregate.ValueObjects;

namespace Domain.OrderAggregate.ValueObjects;
public sealed record OrderItem(MerchItemId ItemId,
                               MerchItemAmount Amount,
                               MerchItemPrice Price);
