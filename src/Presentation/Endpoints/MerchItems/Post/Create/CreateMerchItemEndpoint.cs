using Application.Commands.MerchItems.Create;
using Domain.MerchItemAggregate.Entities;
using Domain.MerchItemAggregate.Entities.ValueObjects.Images;
using Domain.MerchItemAggregate.ValueObjects;
using FastEndpoints;
using Mapster;
using Mediator;
using Microsoft.AspNetCore.Http;
using Presentation.Endpoints.MerchItems.Common;

namespace Presentation.Endpoints.MerchItems.Post.Create;

public sealed class CreateMerchItemEndpoint(ISender _sender) : Endpoint<CreateMerchItemRequest, MerchItemDto>
{
    public override void Configure()
    {
        Post("api/merch-items");
        Options(x => x.WithTags("MerchItems"));
    }

    public override async Task HandleAsync(CreateMerchItemRequest request, CancellationToken token)
    {
        var command = request.Adapt<CreateMerchItemCommand>();

        SetImages(request, command);

        var response = await _sender.Send(command, token);

        await DisposeImages(command);

        Response = response.Adapt<MerchItemDto>();
    }

    private static CreateMerchItemCommand SetImages(CreateMerchItemRequest request, CreateMerchItemCommand command)
    {
        if (request.Images is null || !request.Images.Any())
        {
            return command;
        }

        return command with
        {
            Images = request.Images!.Select(x => new CreateMerchItemCommandImage(
                new Image(new ImageId(), new MerchItemId(), isMain: x.IsMain),
                x.File.OpenReadStream())).ToList()
        };
    }

    private static async Task DisposeImages(CreateMerchItemCommand command)
    {
        if (command.Images is null || !command.Images.Any())
        {
            return;
        }

        foreach (var image in command.Images)
        {
            await image.ImageStream.DisposeAsync();
        }
    }
}