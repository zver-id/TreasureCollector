using AutoMapper;
using CollectionLibrary.CollectibleItems;
using WebApi.ResponseContracts;

namespace WebApi.Mappings;

using AutoMapper.Internal;
using TreasureCollector.Interfaces;

/// <summary>
/// Конфигурация маппинга из ответов http в объекты приложения.
/// </summary>
public class ResponseToObjectMappings : Profile
{
  /// <summary>
  /// Настройки конфигурации.
  /// </summary>
  public ResponseToObjectMappings()
  {
    this.CreateMap<PartialCoinResponse, Coin>()
      .ForAllMembers(prop =>
      {
        if (prop is IHasId value)
          prop.MapFrom(prop => value.Name);
      });

    this.CreateMap<FullCoinResponse, Coin>()      
      .ForAllMembers(prop =>
      {
        if (prop is IHasId value)
          prop.MapFrom(prop => value.Name);
      });
  }
  
  
}
