using CollectionLibrary.CollectibleItems;
using TreasureCollector.Interfaces;

namespace WebApi.ResponseContracts;

public class PartialCoinResponse : IResponse
{
  public int Id { get; }
  public string Name { get; }
  public string Country { get; }
  public int Year { get; }

  public PartialCoinResponse() { }

  public PartialCoinResponse(Coin coin)
  {
   this.Id = coin.Id;
   this.Name = coin.Name;
   this.Country = coin.Country?.Name;
   this.Year = coin.Year;
  }
}