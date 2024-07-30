using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.MerchItemAggregate.Entities.ValueObjects.Images;
using Domain.MerchItemAggregate.Repositories;
using Infrastructure.Common;
using LinqToDB;
using Mapster;
using Image = Domain.MerchItemAggregate.Entities.Image;

namespace Infrastructure.Repositories;

public sealed class ImageRepository(Cloudinary _cloudinary) : IImageRepository
{
    public async Task AddAsync(IDictionary<Image, Stream> images, CancellationToken token)
    {
        await Task.WhenAll(images.Select(x => UploadImageAsync(x, token)));
    }

    public async Task DeleteAsync(Image image, CancellationToken token)
    {
        using var db = new DbConnection();

        try
        {
            await db.BeginTransactionAsync(token);

            var dbImage = await db.Images.FirstOrDefaultAsync(x => x.Id == image.Id.Identity, token)
                ?? throw new InvalidOperationException("image not found");

            var result = await _cloudinary.DestroyAsync(new DeletionParams(dbImage.PublicId)
            {
                ResourceType = ResourceType.Image
            });

            await db.Images
                .Where(x => x.Id == dbImage.Id)
                .DeleteAsync(token);

            await db.CommitTransactionAsync(token);
        }
        catch
        {
            await db.RollbackTransactionAsync(token);

            throw;
        }
    }

    public async Task<Image> GetByIdAsync(ImageId id, CancellationToken token)
    {
        using var db = new DbConnection();

        var dbImage = await db.Images.FirstOrDefaultAsync(x => x.Id == id.Identity, token);

        return dbImage.Adapt<Image>();
    }

    private async Task UploadImageAsync(KeyValuePair<Image, Stream> image, CancellationToken token)
    {
        using var db = new DbConnection();

        try
        {
            await db.BeginTransactionAsync(token);

            var result = await _cloudinary.UploadAsync(new ImageUploadParams
            {
                File = new FileDescription(image.Key.Id.Identity.ToString(), image.Value)
            }, token);

            await db.InsertOrReplaceAsync((result, image.Key).Adapt<Models.Image>(), token: token);

            await db.CommitTransactionAsync(token);
        }
        catch
        {
            await db.RollbackTransactionAsync(token);

            throw;
        }
    }
}