using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

using TreasureCollector.Application.Services;


/// <summary>
/// Контроллер статистики.
/// </summary>
[ApiController]
[Route("[controller]")]
public class CoinStatisticsController : ControllerBase
{
  private readonly StatisticsService statisticsService = new ();
  
  /// <summary>
  /// Получить статистику по полю монеты. 
  /// </summary>
  /// <param name="field">Имя параметра.</param>
  /// <returns>Словарь "имя параметра" - "количество".</returns>
  [HttpGet]
  public async Task<ActionResult<Dictionary<string, int>>> GetCoinStatistics(string field)
  {
    field = field.ToLower();
    switch (field)
    {
      case "country":
        Dictionary<string, int> result = await this.statisticsService.GetFieldStatistics(x=>x.Country?.Name);
        return this.Ok(result);
      case "year":
        result = await this.statisticsService.GetFieldStatistics(x=>x.Year.ToString());
        return this.Ok(result);
      default:
        return this.BadRequest(ResponseDescription.UnexpectedResult);
    }
  }
}