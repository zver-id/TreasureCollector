using CollectionLibrary.CollectibleItems;
using TreasureCollector.Interfaces;

namespace WebApi.ResponseContracts;

public class FullCoinResponse : IResponse
{
  public int Id { get; set; }
  public string ItemType { get; set; }
  public string Name { get; set; }
  public string Nominal { get; set; }
  public string Currency { get; set; }
  public string Country { get; set; }
  public int Year { get; set; }

  public FullCoinResponse() { }
  
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