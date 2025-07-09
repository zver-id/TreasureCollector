using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CollectionLibrary.CollectibleItems;
using Nhibernate.Infrastucture;
using TreasureCollector.Interfaces;

namespace TreasureCollector.Application.Services;

public class ItemsService
{
  private readonly IItemsRepository repository = new DbRepository();
  
  public Task AddItem(IHasId item)
  {
    //TODO добавить валидацию добавления
    return Task.Run(() =>
    {
      this.repository.Add(item);
      return Task.CompletedTask;
    });
  }

  public Task UpdateItemAsync(IHasId item)
  {
    return Task.Run(() =>
    {
      this.repository.Update(item);
      return Task.CompletedTask;
    });
  }

  public Task<T> GetItemByIdAsync<T>(int id)
  {
    return Task.Run(() => this.repository.GetById<T>(id));
  }

  public Task<List<T>> GetItemsByCriteria<T>(Func<T, bool> criteria)
  {
    return Task.Run(() => this.repository.GetByCriteria<T>(criteria));
  }
}