using System;
using CollectionLibrary.Nhibernate.Infrastructure;

namespace CollectionLibrary.CollectibleItems;

using TreasureCollector.Interfaces;

/// <summary>
/// Тип коллекционного предмета.
/// </summary>
public class CollectionItemType : IHasId
{
  /// <summary>
  /// ID.
  /// </summary>
  public virtual int Id {get; set;}
  
  /// <summary>
  /// Название типа предмета. 
  /// </summary>
  [Unique]
  public virtual string Name { get; set; }

  public override string ToString()
  {
    return $"Type: {Name}";
  }
  
  /// <summary>
  /// Конструктор.
  /// </summary>
  [Obsolete("Только для использования в NHibernate", true)]
  public CollectionItemType() { }
  
  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="name">Имя предмета.</param>
  public CollectionItemType(string name)
  {
    this.Name = name;
  }
}