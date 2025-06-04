namespace CollectionLibrary;

public abstract class CollectibleItem
{
    public required CollectionItemType ItemType { get; init; }
    public required string Name { get; set; }
    public Country? Country { get; set; }
}