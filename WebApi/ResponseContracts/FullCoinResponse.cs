using CollectionLibrary.CollectibleItems;

namespace WebApi.ResponseContracts;

public class FullCoinResponse
{
  public int Id { get; }
  public string ItemType { get; }
  public string Name { get; }
  public string Nominal { get; }
  public string Currency { get; }
  public string Country { get; }
  public int Year { get; }

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