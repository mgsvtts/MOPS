using Domain.MerchItemAggregate.ValueObjects;

namespace Application.Commands.MerchItems.Calculate;
public readonly record struct CalculateMerchItemRequest(MerchItemId ItemId, int Amount);
