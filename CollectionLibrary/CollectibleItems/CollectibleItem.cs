using CollectionLibrary.Nhibernate.Infrastructure;

namespace CollectionLibrary.CollectibleItems;

using TreasureCollector.Interfaces;

/// <summary>
/// Коллекционный предмет.
/// </summary>
public abstract class CollectibleItem : IHasId
{
  #region Поля и свойства
  
  /// <summary>
  /// Имя типа для отображения.
  /// </summary>
  public const string NameOfClass = "Коллекционный предмет";
  
  /// <summary>
  /// ID предмета.
  /// </summary>
  public virtual int Id { get; set; }
  
  /// <summary>
  /// Тип предмета.
  /// </summary>
  public virtual CollectionItemType ItemType { get; init; }
  
  /// <summary>
  /// Название предмета.
  /// </summary>
  [Unique]
  public virtual string Name { get; set; }
  
  /// <summary>
  /// Страна происхождения.
  /// </summary>
  public virtual Country? Country { get; set; }

  #endregion 

  #region Базовый класс

  public override string ToString()
  {
    return $"{this.ItemType.Name} with name {this.Name}";
  }

  #endregion
  
  #region Конструкторы
  protected CollectibleItem() { }

  protected CollectibleItem(CollectionItemType collectionItemType, Country? country = null)
  {
    this.ItemType = collectionItemType;
    this.Country = country;
  }
  
  #endregion
}