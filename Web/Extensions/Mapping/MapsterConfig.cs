using Application.Commands.MerchItems.AddImage;
using Application.Commands.MerchItems.Calculate;
using Application.Commands.MerchItems.Create;
using Application.Commands.MerchItems.Update;
using Application.Commands.Orders.Create;
using Application.Commands.Types.Create;
using Application.Commands.Types.Update;
using Application.Queries.Orders.GetAll;
using Application.Queries.Orders.Statistics;
using Contracts.MerchItems;
using Contracts.MerchItems.Calculate;
using Contracts.MerchItems.Create;
using Contracts.MerchItems.Update;
using Contracts.Orders.Create;
using Contracts.Types;
using Contracts.Types.Create;
using Contracts.Types.Update;
using Domain.Common.ValueObjects;
using Domain.MerchItemAggregate.Entities;
using Domain.MerchItemAggregate.Entities.ValueObjects.Images;
using Domain.MerchItemAggregate.ValueObjects;
using Domain.OrderAggregate.ValueObjects;
using Domain.TypeAggregate.ValueObjects;
using Infrastructure.Models;
using Mapster;
using MapsterMapper;
using System.Reflection;
using Web.Extensions.Mapping.CustomMappers;

namespace Web.Extensions.Mapping;

public static class MapsterConfig
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<Domain.MerchItemAggregate.MerchItem, MerchItemDto>
           .ForType()
           .MapWith(src => MerchItemDtoCustomMapper.Map(src));

        TypeAdapterConfig<Domain.MerchItemAggregate.MerchItem, merch_items>
           .ForType()
           .MapWith(src => new merch_items
           {
               id = src.Id.Identity.ToString(),
               type_id = src.TypeId.Identity.ToString(),
               name = src.Name.Value,
               description = src.Description != null ? src.Description.Value.ToString() : null,
               price = src.Price.Value,
               self_price = src.SelfPrice.Value,
               amount = src.AmountLeft.Value,
               created_at = src.CreatedAt
           });

        TypeAdapterConfig<merch_items, Domain.MerchItemAggregate.MerchItem>
           .ForType()
           .MapWith(src => new Domain.MerchItemAggregate.MerchItem(new MerchItemId(Guid.Parse(src.id)),
                                                                   new TypeId(Guid.Parse(src.type_id)),
                                                                   new Name(src.name),
                                                                   src.description != null ? new Description(src.description) : null,
                                                                   new MerchItemPrice(src.price),
                                                                   new MerchItemPrice(src.self_price),
                                                                   new MerchItemAmount(src.amount),
                                                                   src.created_at,
                                                                   null));

        TypeAdapterConfig<CreateMerchItemRequest, CreateMerchItemCommand>
         .ForType()
         .MapWith(src => new CreateMerchItemCommand(new TypeId(src.TypeId),
                                                    new Name(src.Name),
                                                    src.Description != null ? new Description(src.Description) : null,
                                                    new MerchItemPrice(src.Price),
                                                    new MerchItemPrice(src.SelfPrice),
                                                    new MerchItemAmount(src.AmountLeft)));

        TypeAdapterConfig<UpdateMerchItemRequest, UpdateMerchItemCommand>
        .ForType()
        .MapWith(src => new UpdateMerchItemCommand(new MerchItemId(src.Id),
                                                   src.TypeId != null ? new TypeId(src.TypeId) : null,
                                                   !string.IsNullOrEmpty(src.Name) ? new Name(src.Name) : null,
                                                   src.Description != null ? new Description(src.Description) : null,
                                                   src.Price != null ? new MerchItemPrice(src.Price.Value) : null,
                                                   src.SelfPrice != null ? new MerchItemPrice(src.SelfPrice.Value) : null,
                                                   src.AmountLeft != null ? new MerchItemAmount(src.AmountLeft.Value) : null));

        TypeAdapterConfig<CreateMerchItemCommand, Domain.MerchItemAggregate.MerchItem>
         .ForType()
         .MapWith(src => new Domain.MerchItemAggregate.MerchItem(new MerchItemId(Guid.NewGuid()),
                                                                 src.TypeId,
                                                                 src.Name,
                                                                 src.Description,
                                                                 src.Price,
                                                                 src.SelfPrice,
                                                                 src.AmountLeft,
                                                                 DateTime.Now,
                                                                 null));

        TypeAdapterConfig<CreateTypeRequest, CreateTypeCommand>
         .ForType()
         .MapWith(src => new CreateTypeCommand(new Name(src.Name)));

        TypeAdapterConfig<IEnumerable<CalculateItemRequest>, CalculateMerchItemCommand>
         .ForType()
         .MapWith(src => new CalculateMerchItemCommand(src.Select(x => new CalculateMerchItemRequest(new MerchItemId(x.ItemId), x.Amount))));

        TypeAdapterConfig<UpdateTypeRequest, UpdateTypeCommand>
        .ForType()
        .MapWith(src => new UpdateTypeCommand(new TypeId(src.Id), new Name(src.Name)));

        TypeAdapterConfig<CreateTypeCommand, Domain.TypeAggregate.Type>
             .ForType()
             .MapWith(src => new Domain.TypeAggregate.Type(new TypeId(Guid.NewGuid()),
                                                           src.Name,
                                                           DateTime.Now));

        TypeAdapterConfig<CalculateMerchItemResponse, CalculateItemResponse>
            .ForType()
            .MapWith(src => new CalculateItemResponse(src.TotalPrice));

        TypeAdapterConfig<UpdateTypeCommand, Domain.TypeAggregate.Type>
          .ForType()
          .MapWith(src => new Domain.TypeAggregate.Type(src.Id,
                                                        src.Name,
                                                        DateTime.Now));

        TypeAdapterConfig<types, Domain.TypeAggregate.Type>
          .ForType()
          .MapWith(src => new Domain.TypeAggregate.Type(new TypeId(Guid.Parse(src.id)),
                                                        new Name(src.name),
                                                        src.created_at));

        TypeAdapterConfig<Domain.TypeAggregate.Type, types>
          .ForType()
          .MapWith(src => new types
          {
              id = src.Id.Identity.ToString(),
              name = src.Name.Value,
              created_at = src.CreatedAt
          });

        TypeAdapterConfig<Image, images>
          .ForType()
          .MapWith(src => new images
          {
              id = src.Id.Identity.ToString(),
              is_main = src.IsMain,
              merch_item_id = src.MerchItemId.Identity.ToString(),
              url = src.Url
          });

        TypeAdapterConfig<images, Image>
          .ForType()
          .MapWith(src => new Image(new ImageId(Guid.Parse(src.id)), new MerchItemId(Guid.Parse(src.merch_item_id)), src.url, src.is_main));

        TypeAdapterConfig<Domain.TypeAggregate.Type, TypeDto>
         .ForType()
         .MapWith(src => new TypeDto(src.Id.Identity,
                                     src.Name.Value,
                                     src.CreatedAt));

        TypeAdapterConfig<OrderItem, order_items>
         .ForType()
         .MapWith(src => new order_items
         {
             id = Guid.NewGuid().ToString(),
             merch_item_id = src.ItemId.Identity.ToString(),
             amount = src.Amount.Value,
             price = src.Price.Value,
             self_price = src.SelfPrice.Value
         });

        TypeAdapterConfig<CreateOrderCommand, Domain.OrderAggregate.Order>
        .ForType()
        .MapWith(src => new Domain.OrderAggregate.Order(new OrderId(Guid.NewGuid()),
                                                        null,
                                                        src.PaymentMethod,
                                                        DateTime.Now));

        TypeAdapterConfig<Domain.OrderAggregate.Order, CreateOrderResponse>
       .ForType()
       .MapWith(src => new CreateOrderResponse(src.Id.Identity,
                                               src.Items.Select(x => new OrderItemResponse(x.ItemId.Identity, x.Amount.Value, x.Price.Value)),
                                               src.PaymentMethod));

        TypeAdapterConfig<Domain.OrderAggregate.Order, orders>
        .ForType()
        .MapWith(src => new orders
        {
            id = src.Id.Identity.ToString(),
            created_at = src.CreatedAt,
            payment_method = (Infrastructure.Misc.Order.PaymentMethod)(int)src.PaymentMethod
        });

        TypeAdapterConfig<orders, Domain.OrderAggregate.Order>
       .ForType()
       .MapWith(src => new Domain.OrderAggregate.Order(new OrderId(Guid.Parse(src.id)),
                                                       src.order_items.Select(x=>new OrderItem(new MerchItemId(Guid.Parse(x.merch_item_id)),
                                                                                               new MerchItemAmount(x.amount),
                                                                                               new MerchItemPrice(x.price),
                                                                                               new MerchItemPrice(x.self_price))),
                                                       (PaymentMethod)(int)src.payment_method,
                                                        src.created_at));

        TypeAdapterConfig<orders, GetAllOrdersResponseOrder>
        .ForType()
        .MapWith(src => new GetAllOrdersResponseOrder(Guid.Parse(src.id),
                                                      src.created_at,
                                                      (PaymentMethod)(int)src.payment_method,
                                                      src.order_items.Select(x => new GetAllOrdersResponseOrderItem(Guid.Parse(x.id),
                                                                                                                  x.price,
                                                                                                                  x.self_price,
                                                                                                                  x.amount,
                                                                                                                  new GetAllOrdersResponseMerchItem(Guid.Parse(x.merch_item_id),
                                                                                                                                                               x.merch_item.name,
                                                                                                                                                               new GetAllOrdersResponseType(Guid.Parse(x.merch_item.type_id), x.merch_item.type.name))))));

        TypeAdapterConfig<CreateOrderRequest, CreateOrderCommand>
        .ForType()
        .MapWith(src => new CreateOrderCommand(src.Items.Select(x => new OrderItem(new MerchItemId(x.MerchItemId),
                                                                                                    new MerchItemAmount(x.Amount))),
                                               src.PaymentMethod));

        TypeAdapterConfig<Application.Queries.Orders.Statistics.Dto.MerchItemStatistic, MerchItemStatistic>
          .ForType()
          .MapWith(src => new MerchItemStatistic(src.name,
                                                 src.orders_with_item,
                                                 src.total_self_price,
                                                 src.total_price,
                                                 src.total_amount));

        TypeAdapterConfig<(Guid itemId, IEnumerable<Contracts.MerchItems.AddImage.AddImageRequest> images), AddImageCommand>
          .ForType()
          .MapWith(src => new AddImageCommand(src.images.Select(x => new AddImageRequest(new Image(new ImageId(Guid.NewGuid()),
                                                                                                   new MerchItemId(src.itemId),
                                                                                                   null,
                                                                                                   x.IsMain),
                                                                                          x.File.OpenReadStream()))));

        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());
        var mapperConfig = new Mapper(typeAdapterConfig);

        services.AddSingleton<IMapper>(mapperConfig);
    }
}
