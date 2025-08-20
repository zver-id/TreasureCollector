using CollectionLibrary.CollectibleItems;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace CollectionLibrary.Nhibernate.Infrastructure.Overrides;

/// <summary>
/// Отключение ленивной загрузки для связанных сущностей класса Coin.
/// </summary>
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