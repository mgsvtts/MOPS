using Application.Commands.MerchItems.Create;
using Application.Commands.Orders.Create;
using Application.Commands.Types.Create;
using Application.Commands.Types.Update;
using Application.Queries.Orders.GetAll;
using Domain.Common.ValueObjects;
using Domain.MerchItemAggregate.ValueObjects;
using Domain.OrderAggregate.ValueObjects;
using Domain.TypeAggregate.ValueObjects;
using Infrastructure.Models;
using Mapster;
using OrderItem = Domain.OrderAggregate.ValueObjects.OrderItem;

namespace Application.Common.Mapping;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
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

        TypeAdapterConfig<CreateTypeCommand, Domain.TypeAggregate.Type>
             .ForType()
             .MapWith(src => new Domain.TypeAggregate.Type(new TypeId(Guid.NewGuid()),
                                                           src.Name,
                                                           DateTime.Now));

        TypeAdapterConfig<UpdateTypeCommand, Domain.TypeAggregate.Type>
          .ForType()
          .MapWith(src => new Domain.TypeAggregate.Type(src.Id,
                                                        src.Name,
                                                        DateTime.Now));

        TypeAdapterConfig<CreateOrderCommand, Domain.OrderAggregate.Order>
        .ForType()
        .MapWith(src => new Domain.OrderAggregate.Order(new OrderId(Guid.NewGuid()),
                                                        null,
                                                        src.PaymentMethod,
                                                        DateTime.Now));

        TypeAdapterConfig<Domain.OrderAggregate.Order, Order>
        .ForType()
        .MapWith(src => new Order
        {
            Id = src.Id.Identity,
            CreatedAt = src.CreatedAt,
            PaymentMethod = src.PaymentMethod
        });

        TypeAdapterConfig<Order, Domain.OrderAggregate.Order>
       .ForType()
       .MapWith(src => new Domain.OrderAggregate.Order(new OrderId(src.Id),
                                                       src.OrderItems.Select(x => new OrderItem(new MerchItemId(x.MerchItemId),
                                                                                               new MerchItemAmount(x.Amount),
                                                                                               new Price(x.Price),
                                                                                               new Price(x.SelfPrice))),
                                                       src.PaymentMethod,
                                                       src.CreatedAt));

        TypeAdapterConfig<Order, GetAllOrdersResponseOrder>
        .ForType()
        .MapWith(src => new GetAllOrdersResponseOrder(src.Id,
                                                      src.CreatedAt,
                                                      src.PaymentMethod,
                                                      new GetAllOrdersResponseTotals(src.OrderItems.Sum(x => x.Price),
                                                                                     src.OrderItems.Sum(x => x.SelfPrice)),
                                                      src.OrderItems.Select(x => new GetAllOrdersResponseOrderItem(x.Id,
                                                                                                                  x.Price,
                                                                                                                  x.SelfPrice,
                                                                                                                  x.Amount,
                                                                                                                  new GetAllOrdersResponseMerchItem(x.MerchItemId,
                                                                                                                                                    x.MerchItem.Name,
                                                                                                                                                    new GetAllOrdersResponseType(x.MerchItem.TypeId, x.MerchItem.Type.Name))))));
    }
}