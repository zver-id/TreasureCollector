using System;
using CollectionLibrary.Nhibernate.Infrastructure;

namespace CollectionLibrary.CollectibleItems;

public class CollectionItemType : IHasId
{
  public virtual int Id {get; set;}
  
  [Unique]
  public virtual string Name { get; set; }

  public override string ToString()
  {
    return $"Type: {Name}";
  }

  [Obsolete("Только для использования в NHibernate", true)]
  public CollectionItemType() { }
  
  public CollectionItemType(string name)
  {
    this.Name = name;
  }
}