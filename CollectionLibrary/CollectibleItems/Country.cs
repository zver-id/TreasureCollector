using System;
using System.ComponentModel.DataAnnotations.Schema;
using CollectionLibrary.Nhibernate.Infrastructure;

namespace CollectionLibrary.CollectibleItems;

public class Country : IHasId
{
  public virtual int Id {get; set;}
  
  [Unique]
  public virtual string Name { get; set; }

  [Obsolete("Только для использования в NHibernate", true)]
  public Country()
  {
  }
  public Country(string name)
  {
    this.Name = name;
  }
}