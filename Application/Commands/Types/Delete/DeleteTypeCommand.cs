using Domain.TypeAggregate.ValueObjects;
using MediatR;

namespace Application.Commands.Types.Delete;

public record struct DeleteTypeCommand(TypeId Id) : IRequest;
