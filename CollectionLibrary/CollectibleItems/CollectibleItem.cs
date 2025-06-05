namespace CollectionLibrary.CollectibleItems;

public abstract class CollectibleItem
{
    public CollectionItemType ItemType { get; init; }
    public required string Name { get; set; }
    public Country? Country { get; set; }

    protected CollectibleItem(CollectionItemType collectionItemType, Country? country = null)
    {
        this.ItemType = collectionItemType;
        this.Country = country;
    }
}