﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Dapper;
using Domain.MerchItemAggregate;
using Domain.MerchItemAggregate.Entities;
using Domain.MerchItemAggregate.ValueObjects;
using Infrastructure;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.MerchItems.Common.Services;
public class ImageRepository : IImageRepository
{
    private readonly Cloudinary _cloudinary;
    private readonly DbContext _db;
    public ImageRepository(IConfiguration config, DbContext db)
    {
        var settings = config.GetSection("Cloudinary");

        _cloudinary = new Cloudinary(new Account(settings.GetValue<string>("CloudName"),
                                                 settings.GetValue<string>("ApiKey"),
                                                 settings.GetValue<string>("ApiSecret")));
        _db = db;
    }


    public async Task AddAsync(IDictionary<Image, Stream> images)
    {
        images = images.Where(x => x.Value.Length != 0).ToDictionary(x => x.Key, x => x.Value);
        foreach(var image in images)
        {
            await UploadImageAsync(image);
        }
        //await Task.WhenAll(images.Select(UploadImageAsync));
    } 

    private async Task UploadImageAsync(KeyValuePair<Image, Stream> image)
    {
        var result = await _cloudinary.UploadAsync(new ImageUploadParams
        {
            File = new FileDescription(image.Key.Id.Identity.ToString(), image.Value)
        });

        var query = @$"INSERT INTO {nameof(images)} ({nameof(images.id)},
                                                     {nameof(images.merch_item_id)},
                                                     {nameof(images.url)},
                                                     {nameof(images.is_main)})
                       VALUES (@{image.Key.Id.Identity},
                               @{image.Key.MerchItemId.Identity},
                               @{result.SecureUrl},
                               @{image.Key.IsMain})";

        using var connection = _db.CreateConnection();

        await connection.ExecuteAsync(query);
    }
}