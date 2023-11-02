using Domain.MerchItemAggregate.ValueObjects;
using MediatR;

namespace Application.Commands.MerchItems.Delete;

public record struct DeleteMerchItemCommand(MerchItemId Id) : IRequest;
