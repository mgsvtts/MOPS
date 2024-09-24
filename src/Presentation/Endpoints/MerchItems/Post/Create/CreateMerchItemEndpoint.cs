using Application.Commands.MerchItems.Create;
using Domain.MerchItemAggregate.Entities;
using Domain.MerchItemAggregate.Entities.ValueObjects.Images;
using Domain.MerchItemAggregate.ValueObjects;
using FastEndpoints;
using Mapster;
using Mediator;
using Microsoft.AspNetCore.Http;
using Presentation.Endpoints.MerchItems.Common;
using Presentation.Endpoints.MerchItems.Get.GetAll;

namespace Presentation.Endpoints.MerchItems.Post.Create;

public sealed class CreateMerchItemEndpoint(ISender _sender) : Endpoint<CreateMerchItemRequest, MerchItemDto>
{
    public override void Configure()
    {
        Post("api/merch-items");
        AllowFileUploads();
        Options(x => x.WithTags("MerchItems"));
    }

    public override async Task HandleAsync(CreateMerchItemRequest request, CancellationToken token)
    {
        var command = request.Adapt<CreateMerchItemCommand>();

        command = await SetImagesAsync(request, command, token);

        var response = await _sender.Send(command, token);

        await DisposeImages(command);

        await SendCreatedAtAsync<GetAllMerchItemsEndpoint>(null, response.Adapt<MerchItemDto>());
    }

    private async Task<CreateMerchItemCommand> SetImagesAsync(CreateMerchItemRequest request, CreateMerchItemCommand command, CancellationToken token)
    {
        var form = await HttpContext.Request.ReadFormAsync(token);
        
        if (form.Files is null || !form.Files.Any())
        {
            return command;
        }

        var images = new List<CreateMerchItemCommandImage>();
        var values = form.ToDictionary(x=>x.Key, x => x.Value.ToString());
        foreach (var image in form.Files)
        {
            var value = values.First(x => x.Key.Contains(image.Name.Split('.').First()));
            images.Add(new CreateMerchItemCommandImage(new Image(new ImageId(),
                                                                 new MerchItemId(),
                                                                 isMain: bool.Parse(value.Value)), 
                                                       image.OpenReadStream()));
        }

        return command with
        {
            Images = images
        };
    }

    private static async Task DisposeImages(CreateMerchItemCommand command)
    {
        if (command.Images is null || command.Images.Count == 0)
        {
            return;
        }

        foreach (var image in command.Images)
        {
            await image.ImageStream.DisposeAsync();
        }
    }
}