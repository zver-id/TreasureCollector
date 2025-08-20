using System;
using System.IO;
using System.Reflection;
using Commons;

namespace TreasureCollector.Application;

/// <summary>
/// Обработчик изображений.
/// </summary>
public static class ImageUploader
{
  /// <summary>
  /// Сохранить изображение.
  /// </summary>
  /// <param name="base64File">Изображение.</param>
  /// <param name="imageType">Тип изображения.</param>
  /// <returns>Путь до сохраненного изображения.</returns>
  public static string SaveImage(string base64File, string imageType)
  {
    var uploadsFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!,
      ApplicationSettings.imagesFolder);
    if (!Directory.Exists(uploadsFolder))
      Directory.CreateDirectory(uploadsFolder);

    var fileName = $"{Guid.NewGuid()}_{imageType}.jpg";
    var filePath = Path.Combine(uploadsFolder, fileName);
    
    var partsOfDataString = base64File.Split(',');
    if (partsOfDataString.Length > 1 && partsOfDataString[0].Contains("base64"))
      base64File = partsOfDataString[1];
  
    var imageBytes = Convert.FromBase64String(base64File);
    File.WriteAllBytesAsync(filePath, imageBytes);
    
    return fileName;
  }
}