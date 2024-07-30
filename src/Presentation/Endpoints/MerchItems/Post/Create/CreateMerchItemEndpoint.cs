using Application.Commands.MerchItems.Create;
using Domain.MerchItemAggregate.Entities;
using Domain.MerchItemAggregate.Entities.ValueObjects.Images;
using Domain.MerchItemAggregate.ValueObjects;
using FastEndpoints;
using Mapster;
using Mediator;
using Presentation.Endpoints.MerchItems.Common;

namespace Presentation.Endpoints.MerchItems.Post.Create;

public sealed class CreateMerchItemEndpoint(ISender _sender) : Endpoint<CreateMerchItemRequest, MerchItemDto>
{
    public override void Configure()
    {
        Post("api/merch-items");
    }

    public override async Task HandleAsync(CreateMerchItemRequest request, CancellationToken token)
    {
        var command = request.Adapt<CreateMerchItemCommand>();

        if (request.Images?.Any() == true)
        {
            command = SetImages(request, command);
        }

        var response = await _sender.Send(command, token);

        Response = response.Adapt<MerchItemDto>();
    }

    private static CreateMerchItemCommand SetImages(CreateMerchItemRequest request, CreateMerchItemCommand command)
    {
        command = command with
        {
            Images = request.Images!.Select(x => new CreateMerchItemCommandImage(
                new Image(new ImageId(), new MerchItemId(), isMain: x.IsMain),
                x.File.OpenReadStream())).ToList()
        };
        return command;
    }
}