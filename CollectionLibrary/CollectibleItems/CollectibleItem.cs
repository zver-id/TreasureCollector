namespace CollectionLibrary.CollectibleItems;

public abstract class CollectibleItem
{
    public virtual int Id { get; set; }
    public virtual CollectionItemType ItemType { get; init; }
    public virtual string Name { get; set; }
    public virtual Country? Country { get; set; }

    protected CollectibleItem()
    {
    }

    protected CollectibleItem(CollectionItemType collectionItemType, Country? country = null)
    {
        this.ItemType = collectionItemType;
        this.Country = country;
    }
}