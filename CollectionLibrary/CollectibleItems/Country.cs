using System;
using System.ComponentModel.DataAnnotations.Schema;
using CollectionLibrary.Nhibernate.Infrastructure;

namespace CollectionLibrary.CollectibleItems;

using TreasureCollector.Interfaces;

/// <summary>
/// Страна происхождения.
/// </summary>
public class Country : IHasId
{
  /// <summary>
  /// ID.
  /// </summary>
  public virtual int Id {get; set;}
  
  /// <summary>
  /// Название.
  /// </summary>
  [Unique]
  public virtual string Name { get; set; }

  public override string ToString()
  {
    return $"Country: {Name}";
  }
  
  /// <summary>
  /// Конструктор.
  /// </summary>
  [Obsolete("Только для использования в NHibernate", true)]
  public Country() { }
  
  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="name">Название страны.</param>
  public Country(string name)
  {
    this.Name = name;
  }
}