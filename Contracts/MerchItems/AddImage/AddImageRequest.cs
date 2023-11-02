using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.MerchItems.AddImage;
public class AddImageRequest
{
    public IFormFile File { get; set; }
    public bool IsMain { get; set; }
}
