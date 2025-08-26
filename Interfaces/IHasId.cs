namespace TreasureCollector.Interfaces;

/// <summary>
/// Объект, хранимый в репозитории.
/// </summary>
public interface IHasId
{
  /// <summary>
  /// Уникальный ID объекта.
  /// </summary>
  public int Id { get; set; }
  
  /// <summary>
  /// Имя объекта.
  /// </summary>
  public string Name { get; set; }
}