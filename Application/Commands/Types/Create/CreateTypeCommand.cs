using Domain.Common.ValueObjects;
using MediatR;

namespace Application.Commands.Types.Create;

public record struct CreateTypeCommand(Name Name) : IRequest<Domain.TypeAggregate.Type>;
