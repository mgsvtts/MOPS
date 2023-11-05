using Domain.MerchItemAggregate.Entities;
using Domain.MerchItemAggregate.Entities.ValueObjects.Images;

namespace Domain.MerchItemAggregate.Repositories;

public interface IImageRepository
{
    Task AddAsync(IDictionary<Image, Stream> images);

    Task DeleteAsync(Image image);

    Task<Image> GetByIdAsync(ImageId id);

    Task UpdateAsync(Image image);
}
