using FluentNHibernate.Automapping;

namespace DataBaseAccess;

/// <summary>
/// Конфигурация автомаппинга FluentNHibernate.
/// </summary>
public class StoreConfiguration : DefaultAutomappingConfiguration
{
  public override bool ShouldMap(Type type)
  {
    return type.Namespace == "CollectionLibrary.CollectibleItems";
  }
}