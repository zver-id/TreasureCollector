using System.Reflection;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace NHibernate.Infrastructure;

public static class Settings
{
  public static readonly string DatabaseConnectionString;

  static Settings()
  {
    string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    var settingsJson = File.ReadAllText(Path.Combine(assemblyPath, "settings.json"));
    dynamic settings = JsonConvert.DeserializeObject(settingsJson)!;
    var type = typeof(Settings);
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

