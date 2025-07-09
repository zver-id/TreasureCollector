using System.Text.Json;
using CollectionLibrary.CollectibleItems;
using Microsoft.AspNetCore.Mvc;
using TreasureCollector.Application.Services;
using WebApi.ResponseContracts;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CoinController : ControllerBase
{
  private ItemsService itemsService;
  
  [HttpGet]
  public async Task<ActionResult<List<CoinResponse>>> GetAllCoins()
  {
    var coins = await this.itemsService.GetItemsByCriteria<Coin>(coin => true);
    var response = coins
      .Select(coin => new CoinResponse(coin.Id, coin.Name, "Russia", coin.Currency, coin.Year));
    return Ok(response);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<CoinResponse>> Get(int itemId)
  {
    var coin = await this.itemsService.GetItemByIdAsync<Coin>(itemId);
    var response = new CoinResponse( coin.Id, coin.Name, "Russia", coin.Currency, coin.Year );
    return Ok(response);
  }

  [HttpPost]
  public async Task<ActionResult<int>> Post([FromBody] CoinResponse coin)
  {
    if (coin == null)
      return  BadRequest("Coin cannot be null");
    this.itemsService.AddItem(
      new Coin
      {
        Name = coin.name,
        Currency = coin.currency,
        Year = coin.year,
      });
    return Ok(1);
  }

  public CoinController()
  {
    this.itemsService = new ItemsService();
  }
}