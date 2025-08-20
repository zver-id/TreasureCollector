using CollectionLibrary;
using CollectionLibrary.CollectibleItems;
using TreasureCollector.Interfaces;

namespace WebApi.ResponseContracts;

/// <summary>
/// Полное описание монеты.
/// </summary>
public class FullCoinResponse : IResponse
{
  /// <summary>
  /// Id.
  /// </summary>
  public int Id { get; set; }
  
  /// <summary>
  /// Тип предмета.
  /// </summary>
  public string ItemType { get; set; }
  
  /// <summary>
  /// Название.
  /// </summary>
  public string Name { get; set; }
  
  /// <summary>
  /// Номинал.
  /// </summary>
  public string Nominal { get; set; }
  
  /// <summary>
  /// Валюта.
  /// </summary>
  public string Currency { get; set; }
  
  /// <summary>
  /// Страна.
  /// </summary>
  public string Country { get; set; }
  
  /// <summary>
  /// Год.
  /// </summary>
  public int Year { get; set; }
  
  /// <summary>
  /// Изображение аверса.
  /// </summary>
  public string? AversImage { get; set; }
  
  /// <summary>
  /// Путь до изображения аверса.
  /// </summary>
  public string? AversImagePath { get; set; }
  
  /// <summary>
  /// Путь до изображения реверса.
  /// </summary>
  public string? ReversImagePath { get; set; }
  
  /// <summary>
  /// Путь до изображения гурта.
  /// </summary>
  public string? EdgeImagePath { get; set; }
  
  /// <summary>
  /// Конструктор.
  /// </summary>
  public FullCoinResponse() { }
  
  /// <summary>
  /// Конструктор.
  /// </summary>
  public FullCoinResponse(Coin coin)
  {
    this.Id = coin.Id;
    this.ItemType = coin.ItemType.Name;
    this.Name = coin.Name;
    this.Nominal = coin.Nominal;
    this.Currency = coin.Currency;
    this.Country = coin.Country.Name;
    this.Year = coin.Year;
  }
}