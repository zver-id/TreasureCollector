using CollectionLibrary.Nhibernate.Infrastructure;

namespace CollectionLibrary.CollectibleItems;

public class CollectionItemType : IHasId
{
  public virtual int Id {get; set;}
  
  [Unique]
  public virtual required string Name { get; set; }
}