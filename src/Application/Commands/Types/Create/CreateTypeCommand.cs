using Domain.Common.ValueObjects;
using Mediator;

namespace Application.Commands.Types.Create;

public sealed record CreateTypeCommand(Name Name) : ICommand<Domain.TypeAggregate.Type>;
