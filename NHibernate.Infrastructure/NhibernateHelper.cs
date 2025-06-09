using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using CollectionLibrary.CollectibleItems;
using FluentNHibernate.Automapping;

namespace NHibernate.Infrastructure;

public class NhibernateHelper
{
    private static ISessionFactory _sessionFactory;

    public static ISessionFactory SessionFactory
    {
        get
        {
            if (_sessionFactory == null)
            {
                _sessionFactory = CreateSessionFactory();
            }
            return _sessionFactory;
        }
    }

    private static ISessionFactory CreateSessionFactory()
    {
        var cfg = new StoreConfiguration();
        string connectionString = Settings.DatabaseConnection;
        return Fluently.Configure()
            .Database(PostgreSQLConfiguration.Standard
                .ConnectionString(connectionString)) 
            .Mappings(m => m.AutoMappings
                .Add(AutoMap.AssemblyOf<CollectionItemType>(cfg)))
            .BuildSessionFactory();

    }

    public static ISession OpenSession()
    {
        return SessionFactory.OpenSession();
    }
}