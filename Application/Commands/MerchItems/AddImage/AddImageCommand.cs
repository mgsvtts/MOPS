using Domain.MerchItemAggregate.Entities;
using MediatR;

namespace Application.Commands.MerchItems.AddImage;

public record struct AddImageCommand(IEnumerable<AddImageRequest> Images) : IRequest;

public record struct AddImageRequest(Image Image, Stream ImageStream);
