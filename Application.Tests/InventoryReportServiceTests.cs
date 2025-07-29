using CollectionLibrary.CollectibleItems;
using TreasureCollector.Application.Services;

namespace Application.Tests;

public class InventoryReportServiceTests
{
  [SetUp]
  public void Setup()
  {
  }

  [Test]
  public void GetInventoryReport()
  {
    var reportService = new InventoryReportService();
    Assert.DoesNotThrow(()=>reportService.GetInventoryReport<Coin>());
  }
}