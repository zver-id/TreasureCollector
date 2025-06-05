namespace CollectionLibrary.CollectibleItems;

public class Coin : CollectibleItem
{
    public string Nominal {get; set;}
    public string Currency {get; set;}
    public Coin(string nominal, string currency, CollectionItemType collectionItemType, Country? country):
        base(collectionItemType, country)
    {
        this.Nominal = nominal;
        this.Currency = currency;
        this.Name = $"{nominal} {currency}";
    }
}