using Domain.MerchItemAggregate.ValueObjects;
using MediatR;

namespace Application.Commands.MerchItems.Delete;

public sealed record DeleteMerchItemCommand(MerchItemId Id) : IRequest;
