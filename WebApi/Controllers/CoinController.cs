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
  public async Task<ActionResult<List<PartialCoinResponse>>> GetAllCoins()
  {
    var coins = await this.itemsService.GetItemsByCriteria<Coin>(coin => true);
    var response = coins
      .Select(coin => new PartialCoinResponse(coin));
    return Ok(response);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<PartialCoinResponse>> Get(int id)
  {
    var coin = await this.itemsService.GetItemByIdAsync<Coin>(id);
    var response = new FullCoinResponse(coin);
    return Ok(response);
  }

  [HttpPost]
  public async Task<ActionResult<int>> Post([FromBody] PartialCoinResponse partialCoin)
  {
    if (partialCoin == null)
      return  BadRequest("Coin cannot be null");
    await this.itemsService.AddItem(
      new Coin
      {
        Name = partialCoin.Name,
        Year = partialCoin.Year,
      });
    return Ok(1);
  }

  public CoinController()
  {
    this.itemsService = new ItemsService();
  }
}