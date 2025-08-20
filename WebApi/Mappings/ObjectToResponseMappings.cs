using System.Reflection;
using AutoMapper;
using CollectionLibrary;
using CollectionLibrary.CollectibleItems;
using FluentNHibernate.Conventions;
using WebApi.ResponseContracts;

namespace WebApi.Mappings;

public class ObjectToResponseMappings : Profile
{
  /// <summary>
  /// Конструктор. Основные настройки конфигурации.
  /// </summary>
  public ObjectToResponseMappings()
  {
    this.CreateMap<Coin, FullCoinResponse>()
      .AfterMap((src, dest) =>
      {
        var imageProperties = typeof(Coin)
          .GetProperties()
          .Where(prop => prop.GetCustomAttribute<ImageAttribute>() != null);
        
        foreach (var imageProp in imageProperties)
        {
          var pathPropertyName = imageProp.Name + "Path";
          var pathProperty = typeof(FullCoinResponse).GetProperty(pathPropertyName);
            
          if (pathProperty != null && pathProperty.CanWrite)
          {
            var value = imageProp.GetValue(src);
            pathProperty.SetValue(dest, value);
          }
        }
      });
  }
}