using CollectionLibrary.CollectibleItems;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Settings = DataBaseAccess.Settings;

namespace DataBaseAccess;

public static class NhibernateHelper
{
  private static ISessionFactory sessionFactory;

  private static ISessionFactory SessionFactory
  {
    get
    {
      if (sessionFactory == null)
      {
        sessionFactory = CreateSessionFactory();
      }
      return sessionFactory;
    }
  }
  
  private static void Expose(Configuration configuration)
  {
    new SchemaUpdate(configuration).Execute(true, true);
  }
  
  private static AutoPersistenceModel GetAutoPersistenceModel() =>
    AutoMap.AssemblyOf<CollectibleItem>(new StoreConfiguration())
      //.Conventions.AddFromAssemblyOf<IdConvention>()
      //.Conventions.AddFromAssemblyOf<NHibernateInitializer>()
      .UseOverridesFromAssemblyOf<DbRepository>();

  private static ISessionFactory CreateSessionFactory()
  {
    var cfg = new StoreConfiguration();
    string connectionString = Settings.DatabaseConnectionString;
    return Fluently.Configure()
      .Database(PostgreSQLConfiguration.Standard
        .ConnectionString(connectionString)
        .ShowSql())
      .Mappings(x => x.AutoMappings.Add(GetAutoPersistenceModel()))
      //.Mappings(m => m.AutoMappings
      //  .Add(AutoMap.AssemblyOf<CollectionItemType>(cfg)))
      .ExposeConfiguration(Expose)
      .BuildSessionFactory();
  }

  public static ISession OpenSession()
  {
    return SessionFactory.OpenSession();
  }
}