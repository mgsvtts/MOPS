using Application.Commands.MerchItems.Calculate;
using Application.Commands.MerchItems.Create;
using Application.Commands.MerchItems.Update;
using Application.Commands.Orders.Create;
using Application.Commands.Types.Create;
using Application.Commands.Types.Update;
using Application.Queries.MerchItems.GetAll;
using Domain.Common.ValueObjects;
using Domain.MerchItemAggregate.ValueObjects;
using Domain.OrderAggregate.ValueObjects;
using Domain.TypeAggregate.ValueObjects;
using Mapster;
using Presentation.Endpoints.MerchItems.Common;
using Presentation.Endpoints.MerchItems.Get.GetAll;
using Presentation.Endpoints.MerchItems.Patch.Update;
using Presentation.Endpoints.MerchItems.Post.Calculate;
using Presentation.Endpoints.MerchItems.Post.Create;
using Presentation.Endpoints.Orders.Post.Create;
using Presentation.Endpoints.Types.Common;
using Presentation.Endpoints.Types.Patch.UpdateType;
using Presentation.Endpoints.Types.Post.Create;

namespace Presentation.Common.Mapping;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        TypeAdapterConfig<Domain.MerchItemAggregate.MerchItem, MerchItemDto>
               .ForType()
               .MapWith(src => new MerchItemDto(src.Id.Identity,
                                    src.TypeId.Identity,
                                    src.Name.Value,
                                    src.Description != null ? src.Description.Value.Value : null,
                                    src.Price.Value,
                                    src.SelfPrice.Value,
                                    src.AmountLeft.Value,
                                    src.GetBenefitPercent(),
                                    src.CreatedAt,
                                    src.Images.Select(x => new ImageDto(x.Id.Identity, x.IsMain, x.Url!))));

        TypeAdapterConfig<CreateMerchItemRequest, CreateMerchItemCommand>
         .ForType()
         .MapWith(src => new CreateMerchItemCommand(new TypeId(src.TypeId),
                                                    new Name(src.Name),
                                                    src.Description != null ? new Description(src.Description) : null,
                                                    new MerchItemPrice(src.Price),
                                                    new MerchItemPrice(src.SelfPrice),
                                                    new MerchItemAmount(src.AmountLeft),
                                                    null));

        TypeAdapterConfig<GetAllMerchItemsRequest, GetAllMerchItemsQuery>
        .ForType()
        .MapWith(src => new GetAllMerchItemsQuery(src.ShowNotAvailable, src.Sort));

        TypeAdapterConfig<UpdateMerchItemRequest, UpdateMerchItemCommand>
            .ForType()
            .MapWith(src => new UpdateMerchItemCommand(new MerchItemId(src.Id),
                                                       src.TypeId != null ? new TypeId(src.TypeId) : null,
                                                       !string.IsNullOrEmpty(src.Name) ? new Name(src.Name) : null,
                                                       src.Description != null ? new Description(src.Description) : null,
                                                       src.Price != null ? new MerchItemPrice(src.Price.Value) : null,
                                                       src.SelfPrice != null ? new MerchItemPrice(src.SelfPrice.Value) : null,
                                                       src.AmountLeft != null ? new MerchItemAmount(src.AmountLeft.Value) : null));

        TypeAdapterConfig<CreateTypeRequest, CreateTypeCommand>
         .ForType()
         .MapWith(src => new CreateTypeCommand(new Name(src.Name)));

        TypeAdapterConfig<IEnumerable<Endpoints.MerchItems.Post.Calculate.CalculateMerchItemRequest>, CalculateMerchItemCommand>
         .ForType()
         .MapWith(src => new CalculateMerchItemCommand(src.Select(x => new Application.Commands.MerchItems.Calculate.CalculateMerchItemRequest(new MerchItemId(x.ItemId), x.Amount))));

        TypeAdapterConfig<UpdateTypeRequest, UpdateTypeCommand>
        .ForType()
        .MapWith(src => new UpdateTypeCommand(new TypeId(src.Id), new Name(src.Name)));

        TypeAdapterConfig<Application.Commands.MerchItems.Calculate.CalculateMerchItemResponse, Endpoints.MerchItems.Post.Calculate.CalculateMerchItemResponse>
            .ForType()
            .MapWith(src => new Endpoints.MerchItems.Post.Calculate.CalculateMerchItemResponse(src.TotalPrice));

        TypeAdapterConfig<CreateOrderRequest, CreateOrderCommand>
        .ForType()
        .MapWith(src => new CreateOrderCommand(src.Items.Select(x => new OrderItem(new MerchItemId(x.MerchItemId),
                                                                                                    new MerchItemAmount(x.Amount))),
                                               src.PaymentMethod));

        TypeAdapterConfig<Domain.OrderAggregate.Order, CreateOrderResponse>
       .ForType()
       .MapWith(src => new CreateOrderResponse(src.Id.Identity,
                                               src.Items.Select(x => new OrderItemResponse(x.ItemId.Identity, x.Amount.Value, x.Price.Value)),
                                               src.PaymentMethod));

        TypeAdapterConfig<Domain.TypeAggregate.Type, TypeDto>
         .ForType()
         .MapWith(src => new TypeDto(src.Id.Identity,
                                     src.Name.Value,
                                     src.CreatedAt));
    }
}