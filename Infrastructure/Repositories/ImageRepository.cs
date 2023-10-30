using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.MerchItems.Common.Services;
public class ImageRepository
{
    private Cloudinary _cloudinary;
    public ImageRepository(IConfiguration config)
    {
        var settings = config.GetSection("Cloudinary");

        _cloudinary = new Cloudinary(new Account(settings.GetValue<string>("CloudName"),
                                                 settings.GetValue<string>("ApiKey"),
                                                 settings.GetValue<string>("ApiSecret")));
    }


    public async Task AddAsync(string fileName, Stream imageStream)
    {
        if (imageStream.Length <= 0)
        {
            return;
        }

        var result = await _cloudinary.UploadAsync(new ImageUploadParams
        {
            File = new FileDescription(fileName, imageStream)
        });


    }
}
