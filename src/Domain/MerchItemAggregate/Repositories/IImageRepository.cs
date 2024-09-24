using Domain.MerchItemAggregate.Entities;
using Domain.MerchItemAggregate.Entities.ValueObjects.Images;

namespace Domain.MerchItemAggregate.Repositories;

public interface IImageRepository
{
    Task<IEnumerable<Image>> AddAsync(IDictionary<Image, Stream> images, CancellationToken token);

    Task DeleteAsync(Image image, CancellationToken token);

    Task<Image> GetByIdAsync(ImageId id, CancellationToken token);

    Task UpdateMainImageAsync(CancellationToken token);
}