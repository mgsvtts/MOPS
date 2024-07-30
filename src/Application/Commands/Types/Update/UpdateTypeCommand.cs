using Domain.Common.ValueObjects;
using Domain.TypeAggregate.ValueObjects;
using Mediator;

namespace Application.Commands.Types.Update;

public sealed record UpdateTypeCommand(TypeId Id, Name Name) : ICommand<Domain.TypeAggregate.Type>;