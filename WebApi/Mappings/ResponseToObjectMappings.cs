using System.Reflection;
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
  /// Добавление настройки сопоставления вложенных сущностей. Они должны возвращать только имя. 
  /// </summary>
  /// <typeparam name="TSource">Исходный тип.</typeparam>
  /// <typeparam name="TDestination">Тип результата.</typeparam>
  private void ConfigureHasIdMappings<TSource, TDestination>()
  { 
    this.CreateMap<TSource, TDestination>().ForAllMembers(prop =>
    {
      if (prop is IHasId value)
        prop.MapFrom(prop => value.Name);
    });
  }
  
  /// <summary>
  /// Конструктор. Основные настройки конфигурации.
  /// </summary>
  public ResponseToObjectMappings()
  {
    ConfigureHasIdMappings<PartialCoinResponse, Coin>();
    ConfigureHasIdMappings<FullCoinResponse, Coin>();
  }
}
