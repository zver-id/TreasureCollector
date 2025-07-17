using CollectionLibrary.CollectibleItems;
using TreasureCollector.Interfaces;

namespace DataBaseAccess.Tests;

public class DbRepositoryTests
{
  public IItemsRepository repository;
  
  [SetUp]
  public void Setup()
  {
    this.repository = new DbRepository();
    var typeCoin = this.repository.Get<CollectionItemType>("Name", "Coin");
    if (typeCoin == null)
    {
      typeCoin = new CollectionItemType{Name = "Coin"};
      this.repository.Add(typeCoin);
    }
    List<string> countryNames = ["USA", "Russia"];
    foreach (var countryName in countryNames)
    {
      var existCountry = this.repository.Get<Country>("Name", countryName);
      if (existCountry == null)
      {
        var country = new Country(countryName);
        this.repository.Add(country);
      }
    }
  }

  [TestCase("1","dollar", "USA")]
  [TestCase("1","rouble", "Russia")]
  public void Add_NewCoin_Added(string nominal, string currency, string countryName)
  {
    var type = this.repository.Get<CollectionItemType>("Name", "Coin");
    var country = this.repository.Get<Country>("Name", countryName);
    var expectedCoin = new Coin(nominal, currency, type, country);
    this.repository.Add(expectedCoin);
    Coin actual = this.repository.Get<Coin>("Name", expectedCoin.Name);
    Assert.Multiple(() =>
    {
      Assert.That(actual.Name, Is.EqualTo(expectedCoin.Name));
      Assert.That(actual.Year, Is.EqualTo(expectedCoin.Year));
    });
  }

  [TearDown]
  public void TearDown()
  {
    
  }
}