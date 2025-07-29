namespace TreasureCollector.Application.Services;

/// <summary>
/// Описания результата выполнения обработки в сервисе.
/// </summary>
public static class ResultDescription
{
  /// <summary>
  /// Действие выполнено успешно.
  /// </summary>
  public const string Success = "Выполнено успешно";
  
  /// <summary>
  /// Элемент уже существует.
  /// </summary>
  public const string IsExist = "Такой элемент уже сущетсвует";

  /// <summary>
  /// Неожиданный результат.
  /// </summary>
  public const string UnexpectedResult = "Неожиданный результат";
}