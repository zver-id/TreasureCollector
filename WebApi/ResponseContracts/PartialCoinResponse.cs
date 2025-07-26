using CollectionLibrary.CollectibleItems;
using TreasureCollector.Interfaces;

namespace WebApi.ResponseContracts;

public class PartialCoinResponse : IResponse
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string Country { get; set; }
  public int Year { get; set; }

  public PartialCoinResponse() { }

  public PartialCoinResponse(Coin coin)
  {
   this.Id = coin.Id;
   this.Name = coin.Name;
   this.Country = coin.Country?.Name;
   this.Year = coin.Year;
  }
}