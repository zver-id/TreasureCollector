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
public class CoinService
{
  private readonly IItemsRepository repository = new DbRepository();
  
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

  public Task<T> GetItemByIdAsync<T>(int id)
  {
    return Task.Run(
      () => this.repository.GetById<T>(id)
      );
  }

  public Task<List<T>> GetItemsByCriteria<T>(Func<T, bool> criteria)
  {
    return Task.Run(() => this.repository.GetByCriteria<T>(criteria));
  }
}