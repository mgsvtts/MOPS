using Domain.TypeAggregate.ValueObjects;
using MediatR;

namespace Application.Commands.Types.DeleteType;

public sealed record DeleteTypeCommand(TypeId Id) : IRequest;
