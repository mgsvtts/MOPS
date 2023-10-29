using Domain.MerchItemAggregate.ValueObjects;

namespace Application.Commands.MerchItems.Calculate;
public record struct CalculateMerchItemRequest(MerchItemId ItemId, int Amount);
