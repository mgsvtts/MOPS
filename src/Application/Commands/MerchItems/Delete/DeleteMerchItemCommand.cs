using Domain.MerchItemAggregate.ValueObjects;
using Mediator;

namespace Application.Commands.MerchItems.Delete;

public sealed record DeleteMerchItemCommand(MerchItemId Id) : ICommand;
