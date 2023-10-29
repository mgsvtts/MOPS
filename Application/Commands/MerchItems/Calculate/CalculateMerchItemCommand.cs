using MediatR;

namespace Application.Commands.MerchItems.Calculate;
public record struct CalculateMerchItemCommand(IEnumerable<CalculateMerchItemRequest> Items) : IRequest<CalculateMerchItemResponse>;
