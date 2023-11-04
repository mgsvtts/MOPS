using Domain.MerchItemAggregate.Entities;
using Domain.MerchItemAggregate.ValueObjects;

namespace Application.Commands.MerchItems.Common.Services;

public interface IImageRepository
{
    Task AddAsync(IDictionary<Image, Stream> images);
    Task UpdateAsync(Image image);
}