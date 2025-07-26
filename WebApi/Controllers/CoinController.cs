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
  public async Task<ActionResult<FullCoinResponse>> Get(int id)
  {
    var coin = await this.itemsService.GetItemByIdAsync<Coin>(id);
    var response = new FullCoinResponse(coin);
    return Ok(response);
  }

  [HttpPost]
  public async Task<ActionResult<string>> Post([FromBody] PartialCoinResponse partialCoin, IMapper mapper)
  {
    if (partialCoin == null)
      return  BadRequest(ResponseDescription.NotBeNull);
    var newCoin = mapper.Map<Coin>(partialCoin);
    var result = await this.itemsService.AddItem(newCoin);
    if (result == ResultDescription.Success)
      return this.Created();
    else if (result == ResultDescription.IsExist)
      return this.Conflict(result);
    else
      return this.BadRequest(ResponseDescription.UnexpectedResult);
  }

  public CoinController(IMapper mapper)
  {
    this.itemsService = new ItemsService();
    this.mapper = mapper;
  }
}