using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CollectionLibrary.CollectibleItems;
using DataBaseAccess;
using TreasureCollector.Interfaces;

namespace TreasureCollector.Application.Services;

/// <summary>
/// Сервис для работы с монетами в коллекции.
/// </summary>
public class CoinService : ServiceBase
{
  /// <summary>
  /// Добавить новый предмет.
  /// </summary>
  /// <param name="item">Предмет.</param>
  /// <returns>Результат выполнения операции.</returns>
  public Task<string> AddItem(IHasId item)
  {
    return Task.Run(() => 
      {
        try
        {
          this.repository.Add(item);
        }
        catch (ArgumentException)
        {
          return ResultDescription.IsExist;
        }
        return ResultDescription.Success;
      }
    );
  }

  /// <summary>
  /// Обновить предмет.
  /// </summary>
  /// <param name="item">Предмет.</param>
  /// <returns>Результат выполнения добавления.</returns>
  public Task<string> Update(IHasId item)
  {
    return Task.Run(() =>
      {
        try
        {
          this.repository.Update(item);
        }
        catch (Exception e)
        {
          Console.WriteLine(e.Message);
          throw;
        }
        return ResultDescription.Success;
      }
    );
  }
  
  /// <summary>
  /// Получить элемент по ID.
  /// </summary>
  /// <param name="id">ID предмета.</param>
  /// <typeparam name="T">Тип добавляемого предмета.</typeparam>
  /// <returns>Искомый предмет.</returns>
  public Task<T> GetItemById<T>(int id)
  {
    return Task.Run(
      () => this.repository.GetById<T>(id)
      );
  }

  /// <summary>
  /// Получить предметы по условию.
  /// </summary>
  /// <param name="criteria">Критерий поиска.</param>
  /// <typeparam name="T">Тип предмета.</typeparam>
  /// <returns>Список искомых предметов.</returns>
  public Task<List<T>> GetItemsByCriteria<T>(Func<T, bool> criteria)
  {
    return Task.Run(() => this.repository.GetByCriteria<T>(criteria));
  }
}