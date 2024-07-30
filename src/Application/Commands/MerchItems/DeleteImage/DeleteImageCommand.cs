using Domain.MerchItemAggregate.Entities.ValueObjects.Images;
using Mediator;

namespace Application.Commands.MerchItems.DeleteImage;
public sealed record DeleteImageCommand(ImageId Id) : ICommand;