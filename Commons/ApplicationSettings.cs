using System.Reflection;
using Newtonsoft.Json;

namespace Commons;

/// <summary>
/// Общие настройки приложения.
/// </summary>
public static class ApplicationSettings
{
  /// <summary>
  /// Строка подключения к базе данных.
  /// </summary>
  public static string databaseConnectionString;

  /// <summary>
  /// Папка хранения изображений.
  /// </summary>
  public static string imagesFolder;

  /// <summary>
  /// Конструктор.
  /// </summary>
  static ApplicationSettings()
  {
    string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException();
    var settingsJson = File.ReadAllText(Path.Combine(assemblyPath, "settings.json"));
    dynamic settings = JsonConvert.DeserializeObject(settingsJson)!;
    var type = typeof(ApplicationSettings);
    foreach (var setting in settings)
    {
      var fieldName = setting.Name;
      var fieldValue = setting.Value.ToString();
      FieldInfo fieldInfo = type.GetField(fieldName,
        BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
      if (fieldInfo != null)
      {
        object value = Convert.ChangeType(fieldValue, fieldInfo.FieldType);
        fieldInfo.SetValue(null, value);
      }
    }
  }
}

