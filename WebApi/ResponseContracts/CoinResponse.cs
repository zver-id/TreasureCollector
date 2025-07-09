namespace WebApi.ResponseContracts;

public record CoinResponse(
  int id,
  string name,
  string country,
  string currency, 
  int year
);