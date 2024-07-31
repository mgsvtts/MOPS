using CloudinaryDotNet.Actions;
using Domain.Common.ValueObjects;
using Domain.MerchItemAggregate.Entities.ValueObjects.Images;
using Domain.MerchItemAggregate.ValueObjects;
using Domain.TypeAggregate.ValueObjects;
using Infrastructure.Models;
using Mapster;

namespace Infrastructure.Common.Mapping;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        TypeAdapterConfig<Domain.MerchItemAggregate.MerchItem, MerchItem>
        .ForType()
        .MapWith(src => new MerchItem
        {
            Id = src.Id.Identity,
            TypeId = src.TypeId.Identity,
            Name = src.Name.Value,
            Description = src.Description != null ? src.Description.Value.Value.ToString() : null,
            Price = src.Price.Value,
            SelfPrice = src.SelfPrice.Value,
            Amount = src.AmountLeft.Value,
            CreatedAt = src.CreatedAt
        });

        TypeAdapterConfig<MerchItem, Domain.MerchItemAggregate.MerchItem>
           .ForType()
           .MapWith(src => new Domain.MerchItemAggregate.MerchItem(new MerchItemId(src.Id),
                                                                   new TypeId(src.TypeId),
                                                                   new Name(src.Name),
                                                                   src.Description != null ? new Description(src.Description) : null,
                                                                   new MerchItemPrice(src.Price),
                                                                   new MerchItemPrice(src.SelfPrice),
                                                                   new MerchItemAmount(src.Amount),
                                                                   src.CreatedAt,
                                                                   src.Images.Select(x => new Domain.MerchItemAggregate.Entities.Image(new ImageId(x.Id), new MerchItemId(x.MerchItemId), x.Url, x.IsMain))));

        TypeAdapterConfig<Models.Type, Domain.TypeAggregate.Type>
          .ForType()
          .MapWith(src => new Domain.TypeAggregate.Type(new TypeId(src.Id),
                                                        new Name(src.Name),
                                                        src.CreatedAt));

        TypeAdapterConfig<Domain.TypeAggregate.Type, Models.Type>
          .ForType()
          .MapWith(src => new Models.Type
          {
              Id = src.Id.Identity,
              Name = src.Name.Value,
              CreatedAt = src.CreatedAt
          });

        TypeAdapterConfig<(ImageUploadResult Result, Domain.MerchItemAggregate.Entities.Image Image), Image>
          .ForType()
          .MapWith(src => new Image
          {
              Id = src.Image.Id.Identity,
              IsMain = src.Image.IsMain,
              MerchItemId = src.Image.MerchItemId.Identity,
              Url = src.Result.Url.AbsoluteUri,
              PublicId = src.Result.PublicId,
          });

        TypeAdapterConfig<Image, Domain.MerchItemAggregate.Entities.Image>
          .ForType()
          .MapWith(src => new Domain.MerchItemAggregate.Entities.Image(new ImageId(src.Id), new MerchItemId(src.MerchItemId), src.Url, src.IsMain));

        TypeAdapterConfig<Domain.OrderAggregate.ValueObjects.OrderItem, OrderItem>
         .ForType()
         .MapWith(src => new OrderItem
         {
             Id = Guid.NewGuid(),
             MerchItemId = src.ItemId.Identity,
             Amount = src.Amount.Value,
             Price = src.Price.Value,
             SelfPrice = src.SelfPrice.Value
         });
    }
}