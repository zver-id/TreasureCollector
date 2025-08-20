using CollectionLibrary.CollectibleItems;
using Microsoft.AspNetCore.Mvc;
using TreasureCollector.Application.Services;
using WebApi.ResponseContracts;

namespace WebApi.Controllers;

/// <summary>
/// Контроллер работы со списком стран.
/// </summary>
[ApiController]
[Route("[controller]")]
public class CountryController : ControllerBase
{
  private readonly CoinService coinService = new ();
  
  
  [HttpGet]
  public async Task<ActionResult<List<CountryResponse>>> GetAllCountries()
  {
    List<Country> countries = await this.coinService.GetItemsByCriteria<Country>(country => true);
    var response = countries
      .Select(country => new CountryResponse(country));
    return Ok(response);
  }
}