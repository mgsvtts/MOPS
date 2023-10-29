using Domain.Common.ValueObjects;
using MediatR;

namespace Application.Commands.Types.Create;

public sealed record CreateTypeCommand(Name Name) : IRequest<Domain.TypeAggregate.Type>;
