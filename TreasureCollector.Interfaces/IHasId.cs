namespace TreasureCollector.Interfaces;

public interface IHasId
{
  /// <summary>
  /// ID объекта.
  /// </summary>
  public int Id { get; set; }
  
  /// <summary>
  /// Имя объекта.
  /// </summary>
  public string Name { get; set; }
}