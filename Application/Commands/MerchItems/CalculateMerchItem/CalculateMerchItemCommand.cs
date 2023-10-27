using MediatR;

namespace Application.Commands.MerchItems.CalculateMerchItem;
public record struct CalculateMerchItemCommand(IEnumerable<CalculateMerchItemRequest> Items) : IRequest<CalculateMerchItemResponse>;
