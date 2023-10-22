using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("/api/v1/merch")]
public class MerchItemController : ControllerBase
{
    [HttpGet]
    public async Task GetAll()
    {
    }
}
