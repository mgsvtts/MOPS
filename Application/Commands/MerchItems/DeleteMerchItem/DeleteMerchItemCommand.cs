using Domain.MerchItemAggregate.ValueObjects;
using MediatR;

namespace Application.Commands.MerchItems.DeleteMerchItem;

public sealed record DeleteMerchItemCommand(MerchItemId Id) : IRequest;
