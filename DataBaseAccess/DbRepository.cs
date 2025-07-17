using System.Reflection;
using CollectionLibrary.CollectibleItems;
using CollectionLibrary.Nhibernate.Infrastructure;
using NHibernate;
using NHibernate.Criterion;
using TreasureCollector.Interfaces;

namespace DataBaseAccess;

public class DbRepository : IItemsRepository
{
  #region IItemsRepository
  
  public void Add(IHasId item)
  {
    if (IsExist(item))
      throw new ArgumentException($"Элемент типа {item} уже существует");
    
    using (var session = NhibernateHelper.OpenSession())
    {
      using (ITransaction  transaction = session.BeginTransaction())
      {
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

  #endregion


  public void Update(IHasId item)
  {
    if (this.IsExist(item))
      return;
    
    using (var session = NhibernateHelper.OpenSession() )
    {
      using (ITransaction  transaction = session.BeginTransaction())
      {
        session.Update(item);
        transaction.Commit();
      }
    }
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

  /// <summary>
  /// Проверка существования объекта в БД по уникальным полям
  /// </summary>
  /// <param name="item"></param>
  /// <returns>Признак существует ли объект с этими полями в базе данных</returns>
  private bool IsExist(IHasId item)
  {
    Type typeOfItem = item.GetType();
    List<PropertyInfo> uniqueProperties = typeOfItem.GetProperties()
      .Where(x => x.GetCustomAttributes(typeof(UniqueAttribute), true).Length != 0)
      .ToList();

    MethodInfo? getMethodInfo = typeof(DbRepository).GetMethod("Get");
    MethodInfo? getMethod = getMethodInfo?.MakeGenericMethod(typeOfItem);
    
    foreach (var property in uniqueProperties)
    {
      var existItem = getMethod?.Invoke(this, new object[] {property.Name, property.GetValue(item)});
      if (existItem == null)
        continue;
      return true;
    }
    return false;
  } 
}