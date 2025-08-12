namespace CollectionLibrary.CollectibleItems;

using System;

/// <summary>
/// Монета.
/// </summary>
public class Coin : CollectibleItem
{
  #region Поля и свойства
  
  /// <summary>
  /// Имя типа для отображения.
  /// </summary>
  public new const string NameOfClass = "Монета";
  
  /// <summary>
  /// Номинал.
  /// </summary>
  public virtual string? Nominal {get; set;}
  
  /// <summary>
  /// Валюта.
  /// </summary>
  public virtual string? Currency {get; set;}
  
  /// <summary>
  /// Год выпуска.
  /// </summary>
  public virtual int Year {get; set;}

  #endregion

  #region Базовый класс

  public override string ToString()
  {
    return this.Name;
  }

  #endregion

  #region Конструкторы
  
  /// <summary>
  /// Конструктор.
  /// </summary>
  public Coin() { }
  
  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="nominal">Номинал.</param>
  /// <param name="currency">Валюта.</param>
  /// <param name="collectionItemType">Тип </param>
  /// <param name="country"></param>
  public Coin(string nominal, string currency, CollectionItemType collectionItemType, Country? country):
    base(collectionItemType, country)
  {
    this.Nominal = nominal;
    this.Currency = currency;
    this.Name = $"{nominal} {currency}";
    if (this.ItemType.Name != "coin")
      throw new ArgumentException("Тип объекта 'монета' должен быть 'coin'");
  }

  #endregion
}