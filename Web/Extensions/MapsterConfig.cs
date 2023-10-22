using Application.Commands;
using Contracts.MerchItem;
using Contracts.MerchItem.Create;
using Domain.Common.ValueObjects;
using Domain.MerchItemAggregate;
using Domain.MerchItemAggregate.Entities.ValueObjects.Types;
using Domain.MerchItemAggregate.ValueObjects;
using Infrastructure.Models;
using Mapster;
using MapsterMapper;
using System;
using System.Reflection;

namespace Web.Extensions;

public static class MapsterConfig
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<Domain.MerchItemAggregate.MerchItem, MerchItemDto>
           .ForType()
           .MapWith(src => new MerchItemDto(src.Id.Identity,
                                            src.TypeId.Identity,
                                            src.Name.Value,
                                            src.Description.Value,
                                            src.Price.Value,
                                            src.SelfPrice.Value,
                                            src.AmountLeft.Value));

        TypeAdapterConfig<Domain.MerchItemAggregate.MerchItem, Infrastructure.Models.MerchItem>
           .ForType()
           .Map(dest => dest.Amount, src => src.AmountLeft)
           .Map(dest => dest.Id, src => src.Id.Identity.ToString())
           .Map(dest => dest.TypeId, src => src.TypeId.Identity.ToString())
           .Map(dest => dest.Description, src => src.Description.Value)
           .Map(dest => dest.Name, src => src.Name.Value);


        TypeAdapterConfig<Infrastructure.Models.MerchItem, Domain.MerchItemAggregate.MerchItem>
           .ForType()
           .MapWith(src => new Domain.MerchItemAggregate.MerchItem(new MerchItemId(Guid.Parse(src.Id)),
                                                                   new TypeId(Guid.Parse(src.TypeId)),
                                                                   new Name(src.Name),
                                                                   src.Description != null ? new Description(src.Description) : null,
                                                                   new MerchItemPrice(src.Price),
                                                                   new MerchItemPrice(src.SelfPrice),
                                                                   new MerchItemAmount(src.Amount)));

        TypeAdapterConfig<CreateMerchItemRequest, CreateMerchItemCommand>
         .ForType()
         .MapWith(src => new CreateMerchItemCommand(new TypeId(src.TypeId),
                                                    new Name(src.Name),
                                                    src.Description != null ? new Description(src.Description) : null,
                                                    new MerchItemPrice(src.Price),
                                                    new MerchItemPrice(src.SelfPrice),
                                                    new MerchItemAmount(src.AmountLeft)));

        TypeAdapterConfig<CreateMerchItemCommand, Domain.MerchItemAggregate.MerchItem>
         .ForType()
         .MapWith(src => new Domain.MerchItemAggregate.MerchItem(new MerchItemId(Guid.NewGuid()),
                                                                 src.TypeId,
                                                                 src.Name,
                                                                 src.Description,
                                                                 src.Price,
                                                                 src.SelfPrice,
                                                                 src.AmountLeft));

        TypeAdapterConfig<Guid, MerchItemId>
            .NewConfig()
            .ConstructUsing(x => new MerchItemId(x));

        TypeAdapterConfig<string, MerchItemId>
            .NewConfig()
            .ConstructUsing(x => new MerchItemId(Guid.Parse(x)));

        TypeAdapterConfig<MerchItemId, string>
            .NewConfig()
            .ConstructUsing(x => x.Identity.ToString());

        TypeAdapterConfig<Guid, TypeId>
            .NewConfig()
            .ConstructUsing(x => new TypeId(x));

        TypeAdapterConfig<string, TypeId>
            .NewConfig()
            .ConstructUsing(x => new TypeId(Guid.Parse(x)));

        TypeAdapterConfig<TypeId, string>
            .NewConfig()
            .ConstructUsing(x => x.Identity.ToString());

        TypeAdapterConfig<MerchItemId, Guid>
            .NewConfig()
            .ConstructUsing(x => x.Identity);

        TypeAdapterConfig<TypeId, Guid>
            .NewConfig()
            .ConstructUsing(x => x.Identity);

        TypeAdapterConfig<decimal, MerchItemPrice>
            .NewConfig()
            .ConstructUsing(x => new MerchItemPrice(x));

        TypeAdapterConfig<string, Name>
            .NewConfig()
            .ConstructUsing(x => new Name(x));

        TypeAdapterConfig<string, Description>
            .NewConfig()
            .ConstructUsing(x => new Description(x));

        TypeAdapterConfig<Name, string>
            .NewConfig()
            .ConstructUsing(x => x.Value);

        TypeAdapterConfig<Description, string>
            .NewConfig()
            .ConstructUsing(x => x.Value);

        TypeAdapterConfig<MerchItemPrice, decimal>
           .NewConfig()
           .ConstructUsing(x => x.Value);

        TypeAdapterConfig<MerchItemAmount, int>
          .NewConfig()
          .ConstructUsing(x => x.Value);

        TypeAdapterConfig<int, MerchItemAmount>
            .NewConfig()
            .ConstructUsing(x => new MerchItemAmount(x));

        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());
        var mapperConfig = new Mapper(typeAdapterConfig);

        services.AddSingleton<IMapper>(mapperConfig);
    }
}