using CollectionLibrary.CollectibleItems;
using Microsoft.AspNetCore.Mvc;
using TreasureCollector.Application.Services;

namespace WebApi.Controllers;

/// <summary>
/// Работа с отчетами о наличии предметов.
/// </summary>
[ApiController]
[Route("[controller]")]
public class InventoryReportController : ControllerBase
{
  /// <summary>
  /// Сервис по получению отчетов.
  /// </summary>
  private readonly InventoryReportService  inventoryReportService = new();
  
  /// <summary>
  /// Получить отчет.
  /// </summary>
  /// <returns>Файл отчета.</returns>
  [HttpGet]
  public async Task<IActionResult> GetInventoryReport()
  {
    var reportPath = await this.inventoryReportService.GetInventoryReport<Coin>();
    if (reportPath == null)
      return this.BadRequest();
    var reportFile = System.IO.File.ReadAllBytes(reportPath);
    return this.File(reportFile, "application/vnd.openxmlformats-officedocument.wordprocessingml.document");
  }
}