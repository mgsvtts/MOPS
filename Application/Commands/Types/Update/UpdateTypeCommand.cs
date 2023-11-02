using Domain.Common.ValueObjects;
using Domain.TypeAggregate.ValueObjects;
using MediatR;

namespace Application.Commands.Types.Update;

public record struct UpdateTypeCommand(TypeId Id, Name Name) : IRequest<Domain.TypeAggregate.Type>;
