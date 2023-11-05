using Microsoft.AspNetCore.Http;

namespace Contracts.MerchItems.AddImage;

public class AddImageRequest
{
    public IFormFile File { get; set; }

    public bool IsMain { get; set; }
}
