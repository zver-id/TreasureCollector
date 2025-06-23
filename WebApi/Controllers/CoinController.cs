using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CoinController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "SomeCoin";
    }
}