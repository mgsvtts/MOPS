using Domain.TypeAggregate.ValueObjects;
using Mediator;

namespace Application.Commands.Types.Delete;

public sealed record DeleteTypeCommand(TypeId Id) : ICommand;