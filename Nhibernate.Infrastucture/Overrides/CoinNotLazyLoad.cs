using CollectionLibrary.CollectibleItems;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace CollectionLibrary.Nhibernate.Infrastructure.Overrides;

public class CoinNotLazyLoad : IAutoMappingOverride<Coin>
{
  public void Override(AutoMapping<Coin> mapping)
  {
    mapping.References(x => x.Country)
      .Not.LazyLoad();
    mapping.References(x => x.ItemType)
      .Not.LazyLoad();
  }
}