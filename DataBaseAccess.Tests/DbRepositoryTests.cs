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
      typeCoin = new CollectionItemType("Coin");
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

  [Test]
  public void Update_ExistsCoin_UpdateSuccessfully()
  {
    Coin expectedCoin = this.repository.Get<Coin>("Name", "1 dollar");
    expectedCoin.Name = "25 dollars";
    expectedCoin.Nominal = "25";
    this.repository.Update(expectedCoin);
    Coin actualCoin = this.repository.Get<Coin>("Name", "25 dollars");
    Assert.That(actualCoin.Id, Is.EqualTo(expectedCoin.Id));
  }

  [TestCase("3","pens", "USA")]
  public void Delete_ExistCoin_DeleteSuccessfully(string nominal, string currency, string countryName)
  {
    var type = this.repository.Get<CollectionItemType>("Name", "Coin");
    var country = this.repository.Get<Country>("Name", countryName);
    var newCoin = new Coin(nominal, currency, type, country);
    this.repository.Add(newCoin);
    Coin forDelete = this.repository.Get<Coin>("Name", newCoin.Name);
    this.repository.Delete(forDelete);
    Coin actual = this.repository.Get<Coin>("Name", newCoin.Name);
    Assert.IsNull(actual);
  }

  [TearDown]
  public void TearDown()
  {
    
  }
}