using Domain.TypeAggregate.ValueObjects;
using MediatR;

namespace Application.Commands.Types.Delete;

public sealed record DeleteTypeCommand(TypeId Id) : IRequest;
