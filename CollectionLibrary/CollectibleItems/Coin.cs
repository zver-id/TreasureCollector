namespace CollectionLibrary.CollectibleItems;

public class Coin : CollectibleItem
{
  public virtual string Nominal {get; set;}
  public virtual string Currency {get; set;}
  public virtual int Year {get; set;}

  public override string ToString()
  {
    return this.Name;
  }

  public Coin()
  {
  }
  public Coin(string nominal, string currency, CollectionItemType collectionItemType, Country? country):
    base(collectionItemType, country)
  {
    this.Nominal = nominal;
    this.Currency = currency;
    this.Name = $"{nominal} {currency}";
  }
}