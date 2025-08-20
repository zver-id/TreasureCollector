using System.Reflection;
using Commons;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

/// <summary>
/// Контролер для работы с изображениями.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{
  /// <summary>
  /// Получить изображение.
  /// </summary>
  /// <param name="fileName">Имя изображения.</param>
  /// <returns>Файл изображения.</returns>
  [HttpGet]
  public IActionResult GetImage([FromQuery] string fileName)
  {
    var fullFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!,
      ApplicationSettings.imagesFolder, fileName);
    if (!System.IO.File.Exists(fullFilePath))
      return this.NotFound(ResponseDescription.FileNotFound);
    var fileStream = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read);
    var fullFileName = Path.GetFileName(fullFilePath);
        
    return this.File(fileStream, "application/octet-stream", fullFileName);
  }
  
}