using Domain.Common.ValueObjects;
using MediatR;

namespace Application.Commands.Types.CreateType;

public sealed record CreateTypeCommand(Name Name) : IRequest<Domain.TypeAggregate.Type>;
