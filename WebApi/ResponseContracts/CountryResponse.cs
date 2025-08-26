using CollectionLibrary.CollectibleItems;
using TreasureCollector.Interfaces;

namespace WebApi.ResponseContracts;

public class CountryResponse : IResponse
{
  /// <summary>
  /// Id страны.
  /// </summary>
  public int Id { get; }
  
  /// <summary>
  /// Название страны.
  /// </summary>
  public string Name { get; }

  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="country">Страна.</param>
  public CountryResponse(Country country)
  {
    this.Id = country.Id;
    this.Name = country.Name;
  }
}