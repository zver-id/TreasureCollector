using DataBaseAccess;
using TreasureCollector.Interfaces;

namespace TreasureCollector.Application.Services;

/// <summary>
/// Базовый сервис.
/// </summary>
public class ServiceBase
{
  /// <summary>
  /// Репозиторий.
  /// </summary>
  protected readonly DbRepository repository = new DbRepository();
}