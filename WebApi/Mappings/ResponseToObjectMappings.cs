using AutoMapper;
using CollectionLibrary.CollectibleItems;
using WebApi.ResponseContracts;

namespace WebApi.Mappings;

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
    this.CreateMap<PartialCoinResponse, Coin>();
    this.CreateMap<FullCoinResponse, Coin>();
  }
}