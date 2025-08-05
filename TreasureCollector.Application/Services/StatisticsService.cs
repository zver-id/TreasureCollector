using System;
using System.Collections;
using System.Linq;
using CollectionLibrary.CollectibleItems;

namespace TreasureCollector.Application.Services;

using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Сервис получения статистики по монетам.
/// </summary>
public class StatisticsService : ServiceBase
{
  public Task<Dictionary<string, int>> GetFieldStatistics(Func<Coin, string> getField)
  {
    return Task.Run(() =>
    {
      return this.repository.GetByCriteria<Coin>(x => true)
        .GroupBy(getField)
        .Select(coin => new { Value = coin.Key, Count = coin.Count() })
        .ToDictionary(x=>x.Value, x=>x.Count);
    });
  }
}