using Domain.Common.ValueObjects;
using Domain.TypeAggregate;
using Domain.TypeAggregate.ValueObjects;
using MediatR;

namespace Application.Commands.Types.UpdateType;

public sealed record UpdateTypeCommand(TypeId Id, Name Name) : IRequest<Domain.TypeAggregate.Type>;