using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Dapper;
using Domain.MerchItemAggregate.Entities;
using Domain.MerchItemAggregate.Entities.ValueObjects.Images;
using Domain.MerchItemAggregate.Repositories;
using Infrastructure.Models;
using MapsterMapper;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class ImageRepository : IImageRepository
{
    private readonly DbContext _db;
    private readonly IMapper _mapper;
    private readonly Cloudinary _cloudinary;

    public ImageRepository(IConfiguration config, DbContext db, IMapper mapper)
    {
        var settings = config.GetSection("Cloudinary");

        _cloudinary = new Cloudinary(new Account(settings.GetValue<string>("CloudName"),
                                                 settings.GetValue<string>("ApiKey"),
                                                 settings.GetValue<string>("ApiSecret")));
        _db = db;
        _mapper = mapper;
    }

    public async Task AddAsync(IDictionary<Image, Stream> images)
    {
        await Task.WhenAll(images.Select(UploadImageAsync));
    }

    public async Task UpdateAsync(Image image)
    {
        var query = Queries.Image.Update();

        var dbImage = _mapper.Map<images>(image);

        using var connection = _db.CreateConnection();

        await connection.ExecuteAsync(query, dbImage);
    }

    public async Task DeleteAsync(Image image)
    {
        using var connection = _db.CreateConnection();

        var getQuery = Queries.Image.GetById();
        var deleteQuery = Queries.Image.Delete();

        var dbImage = await connection.QueryFirstOrDefaultAsync<images>(getQuery, new { id = image.Id.Identity.ToString() });

        var result = await _cloudinary.DestroyAsync(new DeletionParams(dbImage.public_id)
        {
            ResourceType = ResourceType.Image
        });

        await connection.ExecuteAsync(deleteQuery, new { id = image.Id.Identity.ToString() });
    }

    public async Task<Image> GetByIdAsync(ImageId id)
    {
        var query = Queries.Image.GetById();

        using var connection = _db.CreateConnection();

        var image = await connection.QueryFirstOrDefaultAsync<images>(query, new { id = id.Identity.ToString() });

        return _mapper.Map<Image>(image);
    }

    private async Task UploadImageAsync(KeyValuePair<Image, Stream> image)
    {
        var result = await _cloudinary.UploadAsync(new ImageUploadParams
        {
            File = new FileDescription(image.Key.Id.Identity.ToString(), image.Value)
        });

        var query = Queries.Image.Add();

        using var connection = _db.CreateConnection();

        await connection.ExecuteAsync(query, new
        {
            ImageId = image.Key.Id.Identity.ToString(),
            ItemId = image.Key.MerchItemId.Identity.ToString(),
            SecureUrl = result.SecureUrl.ToString(),
            image.Key.IsMain,
            result.PublicId
        });
    }
}
