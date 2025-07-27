using System.Reflection;
using CollectionLibrary.CollectibleItems;
using CollectionLibrary.Nhibernate.Infrastructure;
using NHibernate;
using NHibernate.Criterion;
using TreasureCollector.Interfaces;

namespace DataBaseAccess;

/// <summary>
/// Хранилище информации в базе данных
/// </summary>
public class DbRepository : IItemsRepository
{
  #region Методы

  /// <summary>
  /// Получить существующий объект в БД по уникальным полям
  /// </summary>
  /// <param name="item"></param>
  /// <returns>Существующий объект или null в случае отсутствия.</returns>
  private IHasId? GetEqualFromDb(IHasId item)
  {
    Type typeOfItem = item.GetType();
    List<PropertyInfo> uniqueProperties = typeOfItem.GetProperties()
      .Where(x => x.GetCustomAttributes(typeof(UniqueAttribute), true).Length != 0)
      .ToList();

    MethodInfo? getMethod = typeof(DbRepository)
      .GetMethod("Get")?
      .MakeGenericMethod(typeOfItem);
    
    foreach (var property in uniqueProperties)
    {
      var existItem = getMethod?.Invoke(this, new object[] {property.Name, property.GetValue(item)});
      if (existItem != null)
        return existItem as IHasId;
    }
    return null;
  }
  
  /// <summary>
  /// Сохранить или обновить все связанные объекты.
  /// </summary>
  /// <param name="item">Объект, у которого сохраняются связанные объекты.</param>
  /// <param name="session">Сессия, в которой сохраняется объект.</param>
  private void SaveRelatedEntities(IHasId item, ISession session)
  {
    Type typeOfItem = item.GetType();
    PropertyInfo[] propertyInfos = typeOfItem.GetProperties();
    foreach (PropertyInfo propertyInfo in propertyInfos)
    {
      var typeOfProperty = propertyInfo.PropertyType;
      if (typeof(IHasId).IsAssignableFrom(typeOfProperty))
      {
        var childItem = propertyInfo.GetValue(item);
        if (childItem == null)
          continue;
        IHasId existPropertyValue = this.GetEqualFromDb(childItem as IHasId);
        if (existPropertyValue != null)
          propertyInfo.SetValue(item, existPropertyValue);
        else
          session.SaveOrUpdate(childItem);
      }
    }
  } 
  
  #endregion
  
  #region IItemsRepository
  
  public void Add(IHasId item)
  {
    if (this.GetEqualFromDb(item) != null)
      throw new ArgumentException($"Элемент типа {item} уже существует");
    
    using (ISession session = NhibernateHelper.OpenSession())
    {
      using (ITransaction transaction = session.BeginTransaction())
      {
        this.SaveRelatedEntities(item, session);
        session.Save(item);
        transaction.Commit();
      }
    }
  }
  
  public T Get<T>(string fieldName, object value)
  {
    Type typeOfValue = typeof(T);
    PropertyInfo? property = typeOfValue.GetProperty(fieldName);
    var uniqueAttribute = property?.GetCustomAttribute<UniqueAttribute>();
    if (property == null || uniqueAttribute == null)
      throw new ArgumentException("Свойство не уникально или не существует у объекта");
    
    using var session = NhibernateHelper.OpenSession();
    ICriteria criteria = session.CreateCriteria(typeof(T));
    criteria.Add(Restrictions.Eq(fieldName, value));
    return (T)criteria.UniqueResult();
  }
  
  public T GetById<T>(int id)
  {
    using (var session = NhibernateHelper.OpenSession() )
    {
      return session.Get<T>(id);
    }
  }
  
  public List<T> GetByCriteria<T>(Func<T, bool> criteria) 
  {
    using (var session = NhibernateHelper.OpenSession() )
    {
      return session.Query<T>()
        .AsEnumerable()
        .Where(criteria)
        .ToList();
    }
  }
  
  public void Update(IHasId item)
  {
    using var session = NhibernateHelper.OpenSession();
    using (ITransaction transaction = session.BeginTransaction())
    {
      this.SaveRelatedEntities(item, session);
      session.Update(item);
      transaction.Commit();
    }
  }

  public void Delete(IHasId item)
  {
    using var session = NhibernateHelper.OpenSession();
    using (ITransaction  transaction = session.BeginTransaction())
    {
      session.Delete(item);
      transaction.Commit();
    }
  } 

  #endregion
}