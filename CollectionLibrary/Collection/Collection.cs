using System;
using System.Linq;
using CollectionLibrary.CollectibleItems;
using CollectionLibrary.Nhibernate.Infrastructure;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Infrastructure;

namespace CollectionLibrary.Collection;

public class Collection
{
    /// <summary>
    /// Добавление в базу данных нового объекта
    /// </summary>
    /// <param name="item">Добавяемый объект</param>
    public void Add(IHasId item)
    {
        var typeOfItem = item.GetType();
        var uniqueProperties = typeOfItem.GetProperties()
            .Where(x => x.GetCustomAttributes(typeof(UniqueAttribute), true).Length != 0)
            .ToList();
        
        foreach (var property in uniqueProperties)
        {
            var existItem = Get<IHasId>(property.Name, property.GetValue(item));
            if (existItem == null)
                continue;
            return;
            // $"Элемент типа {typeOfItem} c параметром {property.Name} и значением {property.GetValue(item)} уже существует");
        }
        using (var session = NhibernateHelper.OpenSession())
        {
            using (ITransaction  transaction = session.BeginTransaction())
            {
                session.Save(item);
                transaction.Commit();
            }
        }
    }
    /// <summary>
    /// Обновить свойства объекта в базе данных
    /// </summary>
    /// <param name="item">Объект свойства, которого будут обновляться</param>
    public void Update(IHasId item)
    {
        using (var session = NhibernateHelper.OpenSession() )
        {
            using (ITransaction  transaction = session.BeginTransaction())
            {
                session.Update(item);
                transaction.Commit();
            }
        }
    }
    /// <summary>
    /// Получить объект по ID
    /// </summary>
    /// <param name="id">ID объекта</param>
    /// <typeparam name="T">Тип объекта</typeparam>
    /// <returns></returns>
    public T GetById<T>(int id)
    {
        using (var session = NhibernateHelper.OpenSession() )
        {
           return session.Get<T>(id);
        }
    }
    /// <summary>
    /// Получить объект по свойству и его значению
    /// </summary>
    /// <param name="fieldName">Имя свойства</param>
    /// <param name="value">Значение свойства</param>
    /// <typeparam name="T">Класс объекта</typeparam>
    /// <returns></returns>
    public T Get<T>(string fieldName, object value)
    {
        using (var session = NhibernateHelper.OpenSession() )
        {
            ICriteria criteria = session.CreateCriteria(typeof(T));
            criteria.Add(Restrictions.Eq(fieldName, value));
            return (T)criteria.UniqueResult();
        }
    }
}