namespace CollectionLibrary.CollectibleItems;

public class CollectionItemType : IHasId
{
    public virtual int Id {get; set;}
    public virtual required string Name { get; set; }
}