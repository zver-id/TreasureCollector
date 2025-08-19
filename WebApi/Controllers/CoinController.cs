using System.Reflection;
using System.Text.Json;
using AutoMapper;
using CollectionLibrary;
using CollectionLibrary.CollectibleItems;
using Microsoft.AspNetCore.Mvc;
using TreasureCollector.Application;
using TreasureCollector.Application.Services;
using WebApi.ResponseContracts;

namespace WebApi.Controllers;


/// <summary>
/// Контроллер для работы с монетами.
/// </summary>
[ApiController]
[Route("[controller]")]
public class CoinController : ControllerBase
{
  /// <summary>
  /// Сервис для работы с монетами.
  /// </summary>
  private readonly CoinService coinService;
  
  /// <summary>
  /// Маппер.
  /// </summary>
  private readonly IMapper mapper;
  
  /// <summary>
  /// Получить все монеты.
  /// </summary>
  /// <returns>HTTP-ответ, содержащий сокращенную информацию о монетах - PartialCoinResponse.</returns>
  [HttpGet]
  public async Task<ActionResult<List<PartialCoinResponse>>> GetAllCoins()
  {
    List<Coin> coins = await this.coinService.GetItemsByCriteria<Coin>(coin => true);
    IEnumerable<PartialCoinResponse> response = coins
      .Select(coin => new PartialCoinResponse(coin));
    return this.Ok(response);
  }
  
  /// <summary>
  /// Получить всю информацию о монете по ID.
  /// </summary>
  /// <param name="id">ID монеты.</param>
  /// <returns>HTTP-ответ, содержащий полную информацию о монете - FullCoinResponse.</returns>
  [HttpGet("{id}")]
  public async Task<ActionResult<FullCoinResponse>> Get(int id)
  {
    var coin = await this.coinService.GetItemByIdAsync<Coin>(id);
    var response = this.mapper.Map<FullCoinResponse>(coin);
    return Ok(response);
  }
  
  /// <summary>
  /// Создать новую монету.
  /// </summary>
  /// <param name="coinInfo">JSON с информацией о монете в формате FullCoinResponse.</param>
  /// <returns>HTTP-ответ о результате создания новой монеты.</returns>
  [HttpPost]
  public async Task<ActionResult<string>> Post([FromForm] FullCoinResponse coinInfo)
  {
    var newCoin = this.mapper.Map<Coin>(coinInfo);
    ConvertImages(newCoin);
    var resultOfAdding = await this.coinService.AddItem(newCoin);
    if (resultOfAdding == ResultDescription.Success)
      return this.Created();
    else if (resultOfAdding == ResultDescription.IsExist)
      return this.Conflict(resultOfAdding);
    else
      return this.BadRequest(ResponseDescription.UnexpectedResult);
  }
  
  /// <summary>
  /// Изменить данные монеты.
  /// </summary>
  /// <param name="coinInfo">Измененные данные монеты в формате FullCoinResponse.</param>
  /// <returns>HTTP-ответ о результате изменения монеты.</returns>
  [HttpPut]
  public async Task<ActionResult<string>> Update([FromBody] FullCoinResponse coinInfo)
  {
    var coinToChange = this.mapper.Map<Coin>(coinInfo);
    ConvertImages(coinToChange);
    var resultOfChange = await this.coinService.Update(coinToChange);
    if (resultOfChange == ResultDescription.Success)
      return this.Ok(ResultDescription.Success);
    else
      return this.BadRequest(ResponseDescription.UnexpectedResult);
  }

  /// <summary>
  /// Обработать поступившие в ответе изображения. 
  /// </summary>
  /// <param name="coin">Объект монеты.</param>
  private void ConvertImages(Coin coin)
  {
    PropertyInfo[] coinProperties = coin.GetType().GetProperties();
    foreach (PropertyInfo property in coinProperties)
    {
      if (property.GetCustomAttribute<ImageAttribute>() != null)
      {
        if (property.GetValue(coin) != null)
          property.SetValue(coin,
            ImageUploader.SaveImage(property.GetValue(coin) as string, property.Name));
      }
    }
  }
  
  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="mapper">Маппер.</param>
  public CoinController(IMapper mapper)
  {
    this.coinService = new CoinService();
    this.mapper = mapper;
  }
}