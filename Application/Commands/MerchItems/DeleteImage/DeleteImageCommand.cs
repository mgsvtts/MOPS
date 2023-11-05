using Domain.MerchItemAggregate.Entities.ValueObjects.Images;
using MediatR;

namespace Application.Commands.MerchItems.DeleteImage;
public record struct DeleteImageCommand(ImageId Id) : IRequest;
