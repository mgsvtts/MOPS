using Application.Commands.MerchItems.CalculateMerchItem;
using Application.Commands.MerchItems.CreateMerchItem;
using Application.Commands.MerchItems.UpdateMerchItem;
using Application.Commands.Orders.Create;
using Application.Commands.Types.CreateType;
using Application.Commands.Types.UpdateType;
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
                                                                   src.created_at));

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
                                                                 DateTime.Now));

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

        TypeAdapterConfig<Domain.TypeAggregate.Type, types>
          .ForType()
          .Map(dest => dest.id, src => src.Id.Identity.ToString())
          .Map(dest => dest.name, src => src.Name.Value);

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
              name = src.Name.Value
          });

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
             price = src.Price.Value
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

        TypeAdapterConfig<CreateOrderRequest, CreateOrderCommand>
        .ForType()
        .MapWith(src => new CreateOrderCommand(src.Items.Select(x => new OrderItem(new MerchItemId(x.MerchItemId),
                                                                                                                    new MerchItemAmount(x.Amount),
                                                                                                                    null)),
                                               src.PaymentMethod));

        TypeAdapterConfig<Application.Queries.Orders.Statistics.Dto.MerchItemStatistic, MerchItemStatistic>
          .ForType()
          .MapWith(src => new MerchItemStatistic(src.name,
                                                 src.orders_with_item,
                                                 src.total_self_price,
                                                 src.total_price,
                                                 src.total_amount));

        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());
        var mapperConfig = new Mapper(typeAdapterConfig);

        services.AddSingleton<IMapper>(mapperConfig);
    }
}
