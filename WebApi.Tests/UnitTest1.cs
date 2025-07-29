using CollectionLibrary.CollectibleItems;
using WebApi.ResponseContracts;

namespace WebApi.Tests;

public class Tests
{
  [SetUp]
  public void Setup()
  {
  }

  [Test]
  public void MappingTest()
  {
    var coin = new Coin
    {
      Id = 1,
      Country = new Country("Russia"),
      Name = "1 rouble",
      Nominal = "1",
      Currency = "rouble",
      ItemType = new CollectionItemType("Coin"),
      
    };
    var response = new PartialCoinResponse(coin);
    var expected = coin;
    Assert.Pass();
  }
}