using CollectionLibrary.CollectibleItems;
using Microsoft.AspNetCore.Mvc;
using TreasureCollector.Application.Services;
using WebApi.ResponseContracts;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CountryController : ControllerBase
{
  private readonly ItemsService coinService = new ();
  
  [HttpGet]
  public async Task<ActionResult<List<CountryResponse>>> GetAllCoins()
  {
    List<Country> countries = await this.coinService.GetItemsByCriteria<Country>(country => true);
    var response = countries
      .Select(country => new CountryResponse(country));
    return Ok(response);
  }
}