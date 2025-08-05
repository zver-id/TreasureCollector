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
  private CoinService coinService;
  private IMapper mapper;
  
  [HttpGet]
  public async Task<ActionResult<List<PartialCoinResponse>>> GetAllCoins()
  {
    List<Coin> coins = await this.coinService.GetItemsByCriteria<Coin>(coin => true);
    IEnumerable<PartialCoinResponse> response = coins
      .Select(coin => new PartialCoinResponse(coin));
    return this.Ok(response);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<FullCoinResponse>> Get(int id)
  {
    var coin = await this.coinService.GetItemByIdAsync<Coin>(id);
    var response = this.mapper.Map<FullCoinResponse>(coin);
    return Ok(response);
  }

  [HttpPost]
  public async Task<ActionResult<string>> Post([FromBody] PartialCoinResponse partialCoin)
  {
    var newCoin = this.mapper.Map<Coin>(partialCoin);
    var resultOfAdding = await this.coinService.AddItem(newCoin);
    if (resultOfAdding == ResultDescription.Success)
      return this.Created();
    else if (resultOfAdding == ResultDescription.IsExist)
      return this.Conflict(resultOfAdding);
    else
      return this.BadRequest(ResponseDescription.UnexpectedResult);
  }

  [HttpPut]
  public async Task<ActionResult<string>> Update([FromBody] FullCoinResponse coinResponse)
  {
    var coinToChange = this.mapper.Map<Coin>(coinResponse);
    var resultOfChange = await this.coinService.Update(coinToChange);
    if (resultOfChange == ResultDescription.Success)
      return this.Ok(ResultDescription.Success);
    else
      return this.BadRequest(ResponseDescription.UnexpectedResult);
  }

  public CoinController(IMapper mapper)
  {
    this.coinService = new CoinService();
    this.mapper = mapper;
  }
}