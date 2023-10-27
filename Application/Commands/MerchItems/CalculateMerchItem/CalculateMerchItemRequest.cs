using Domain.MerchItemAggregate.ValueObjects;

namespace Application.Commands.MerchItems.CalculateMerchItem;
public record struct CalculateMerchItemRequest(MerchItemId ItemId, int Amount);
