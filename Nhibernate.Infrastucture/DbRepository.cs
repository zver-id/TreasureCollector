using CollectionLibrary.CollectibleItems;
using CollectionLibrary.Nhibernate.Infrastructure;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Infrastructure;
using TreasureCollector.Interfaces;

namespace Nhibernate.Infrastucture;

public class DbRepository : IItemsRepository
{
    public void Add(IHasId item)
    {
        try
        {
            this.IsExist(item);
        }
        catch (ArgumentException ex)
        {
            //TODO нужно понять как это обработать
            throw ex;
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

    public T Get<T>(string fieldName, object value)
    {
        using var session = NhibernateHelper.OpenSession();
        ICriteria criteria = session.CreateCriteria(typeof(T));
        criteria.Add(Restrictions.Eq(fieldName, value));
        return (T)criteria.UniqueResult();
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
        var typeOfItem = item.GetType();
        var uniqueProperties = typeOfItem.GetProperties()
            .Where(x => x.GetCustomAttributes(typeof(UniqueAttribute), true).Length != 0)
            .ToList();
        
        foreach (var property in uniqueProperties)
        {
            var existItem = this.Get<IHasId>(property.Name, property.GetValue(item));
            if (existItem == null)
                continue;
            throw new ArgumentException(
                $"Элемент типа {typeOfItem} c параметром {property.Name} и значением {property.GetValue(item)} уже существует");
        }
        return false;
    } 
}