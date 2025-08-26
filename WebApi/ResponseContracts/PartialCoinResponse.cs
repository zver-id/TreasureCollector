using CollectionLibrary.CollectibleItems;
using TreasureCollector.Interfaces;

namespace WebApi.ResponseContracts;

/// <summary>
/// Краткое описание монеты.
/// </summary>
public class PartialCoinResponse : IResponse
{
  /// <summary>
  /// Id монеты.
  /// </summary>
  public int Id { get; set; }
  
  /// <summary>
  /// Имя монеты.
  /// </summary>
  public string Name { get; set; }
  
  /// <summary>
  /// Страна.
  /// </summary>
  public string Country { get; set; }
  
  /// <summary>
  /// Год.
  /// </summary>
  public int Year { get; set; }

  /// <summary>
  /// Конструктор.
  /// </summary>
  public PartialCoinResponse() { }
  
  /// <summary>
  /// Конструктор.
  /// </summary>
  public PartialCoinResponse(Coin coin)
  {
   this.Id = coin.Id;
   this.Name = coin.Name;
   this.Country = coin.Country?.Name;
   this.Year = coin.Year;
  }
}