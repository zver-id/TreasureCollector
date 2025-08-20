namespace WebApi.Controllers;

/// <summary>
/// Описания ответа сервера.
/// </summary>
public class ResponseDescription
{
  /// <summary>
  /// Неожиданный результат.
  /// </summary>
  public const string UnexpectedResult = "Неожиданный результат";

  /// <summary>
  /// Запрос не может быть пустым.
  /// </summary>
  public const string NotBeNull = "Cannot be null";
  
  /// <summary>
  /// Файл не найден.
  /// </summary>
  public const string FileNotFound = "File not found";
}