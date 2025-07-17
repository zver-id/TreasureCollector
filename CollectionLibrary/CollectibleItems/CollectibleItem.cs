using CollectionLibrary.Nhibernate.Infrastructure;

namespace CollectionLibrary.CollectibleItems;

public abstract class CollectibleItem : IHasId
{
  public virtual int Id { get; set; }
  public virtual CollectionItemType ItemType { get; init; }
  
  [Unique]
  public virtual string Name { get; set; }
  public virtual Country? Country { get; set; }

  #region Базовый класс

  public override string ToString()
  {
    return $"{this.ItemType.Name} with name {this.Name}";
  }

  #endregion

  protected CollectibleItem()
  {
  }

  protected CollectibleItem(CollectionItemType collectionItemType, Country? country = null)
  {
    this.ItemType = collectionItemType;
    this.Country = country;
  }
}