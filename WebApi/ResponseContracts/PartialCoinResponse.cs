using CollectionLibrary.CollectibleItems;

namespace WebApi.ResponseContracts;

public class PartialCoinResponse
{
  public int Id { get; }
  public string Name { get; }
  public string Country { get; }
  public int Year { get; }

  public PartialCoinResponse(Coin coin)
  {
   this.Id = coin.Id;
   this.Name = coin.Name;
   this.Country = coin.Country?.Name;
   this.Year = coin.Year;
  }
}