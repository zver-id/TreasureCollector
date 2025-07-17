using FluentNHibernate.Automapping;

namespace DataBaseAccess;

public class StoreConfiguration : DefaultAutomappingConfiguration
{
  public override bool ShouldMap(System.Type type)
  {
    return type.Namespace == "CollectionLibrary.CollectibleItems";
  }
}