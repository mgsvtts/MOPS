using Mediator;

namespace Application.Commands.MerchItems.Calculate;
public sealed record CalculateMerchItemCommand(IEnumerable<CalculateMerchItemRequest> Items) : ICommand<CalculateMerchItemResponse>;