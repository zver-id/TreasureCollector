using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CountryController : ControllerBase
{
  [HttpGet]
  public string Get()
  {
    return "SomeCountry";
  }
}