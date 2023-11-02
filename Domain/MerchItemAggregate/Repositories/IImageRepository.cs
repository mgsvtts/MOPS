using Domain.MerchItemAggregate.Entities;

namespace Application.Commands.MerchItems.Common.Services;

public interface IImageRepository
{
    Task AddAsync(IDictionary<Image, Stream> images);
}