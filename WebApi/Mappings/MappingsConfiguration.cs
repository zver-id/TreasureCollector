using AutoMapper;
using CollectionLibrary.CollectibleItems;
using WebApi.ResponseContracts;

namespace WebApi.Mappings;

public class MappingsConfiguration : Profile
{
  public MappingsConfiguration()
  {
    CreateMap<PartialCoinResponse, Coin>();
  }
}