using CollectionLibrary.CollectibleItems;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Settings = DataBaseAccess.Settings;

namespace DataBaseAccess;

/// <summary>
/// Управление работы с базой данных.
/// </summary>
public static class NhibernateHelper
{
  #region Поля и свойства
  
  private static ISessionFactory sessionFactory;

  /// <summary>
  /// Фабрика сессий NHibernate.
  /// </summary>
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
  
  #endregion
  
  #region Методы
  
  /// <summary>
  /// Открыть сессию подключения к базе данных.
  /// </summary>
  /// <returns>Сессия.</returns>
  public static ISession OpenSession()
  {
    return SessionFactory.OpenSession();
  }
  
  /// <summary>
  /// Обновить схему в соответствии с маппингами.
  /// </summary>
  /// <param name="configuration"></param>
  private static void Expose(Configuration configuration)
  {
    new SchemaUpdate(configuration).Execute(true, true);
  }
  
  /// <summary>
  /// Сгенерировать модель конфигурации базы данных.
  /// </summary>
  /// <returns>Модель конфигурации базы данных.</returns>
  private static AutoPersistenceModel GetAutoPersistenceModel() =>
    AutoMap.AssemblyOf<CollectibleItem>(new StoreConfiguration())
      //.Conventions.AddFromAssemblyOf<IdConvention>()
      //.Conventions.AddFromAssemblyOf<NHibernateInitializer>()
      .UseOverridesFromAssemblyOf<DbRepository>();

  /// <summary>
  /// Создать фабрику сессий NHibernate.
  /// </summary>
  /// <returns>Фабрика сессий.</returns>
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
  
  #endregion
}