using System.Text.Json;
using AutoMapper;
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
  private IMapper mapper;
  
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
  public async Task<ActionResult<int>> Post([FromBody] PartialCoinResponse partialCoin, IMapper mapper)
  {
    if (partialCoin == null)
      return  BadRequest("Coin cannot be null");
    var newCoin = mapper.Map<Coin>(partialCoin);
    await this.itemsService.AddItem(newCoin);
    return Ok(1);
  }

  public CoinController(IMapper mapper)
  {
    this.itemsService = new ItemsService();
    this.mapper = mapper;
  }
}