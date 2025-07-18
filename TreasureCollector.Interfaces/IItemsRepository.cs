using System;
using System.Collections.Generic;
using CollectionLibrary.CollectibleItems;

namespace TreasureCollector.Interfaces;

/// <summary>
/// Репозиторий сущностей.
/// </summary>
public interface IItemsRepository
{
  /// <summary>
  /// Добавление в базу данных нового объекта.
  /// </summary>
  /// <param name="item">Добавляемый объект.</param>
  public void Add(IHasId item);
  
  /// <summary>
  /// Обновить свойства объекта в базе данных.
  /// </summary>
  /// <param name="item">Объект свойства, которого будут обновляться.</param>
  public void Update(IHasId item);

  /// <summary>
  /// Получить объект по ID.
  /// </summary>
  /// <param name="id">ID объекта.</param>
  /// <typeparam name="T">Тип объекта.</typeparam>
  /// <returns>Объект из репозитория.</returns>
  public T GetById<T>(int id);

  /// <summary>
  /// Получить объект по его уникальному свойству и его значению.
  /// </summary>
  /// <param name="fieldName">Имя свойства.</param>
  /// <param name="value">Значение свойства.</param>
  /// <typeparam name="T">Класс объекта.</typeparam>
  /// <returns>Объект из репозитория.</returns>
  public T Get<T>(string fieldName, object value);

  /// <summary>
  /// Получить типы объекта по критерию.
  /// </summary>
  /// <typeparam name="T">Тип значений, которые нужно получить.</typeparam>
  /// <typeparam name="criteria">Функция для выборки значений.</typeparam>
  /// <returns>Список объектов.</returns>
  public List<T> GetByCriteria<T>(Func<T, bool> criteria);
}