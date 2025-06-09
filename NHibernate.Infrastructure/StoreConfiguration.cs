using FluentNHibernate.Automapping;

namespace NHibernate.Infrastructure;

public class StoreConfiguration : DefaultAutomappingConfiguration
{
    public override bool ShouldMap(System.Type type)
    {
        return type.Namespace == "CollectionLibrary.CollectibleItems";
    }
}