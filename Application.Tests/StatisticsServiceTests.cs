using TreasureCollector.Application.Services;

namespace Application.Tests;

public class StatisticsServiceTests
{
  [Test]
  public void GetFieldStatistics_Dictionary()
  {
    var service = new StatisticsService();
    var fields = service.GetFieldStatistics(x=>x.Country?.Name);
    Assert.That(fields, Is.Not.Null);
  }
}