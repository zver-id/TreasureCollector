using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Spire.Doc;
using Spire.Doc.Documents;
using CollectionLibrary.CollectibleItems;
using DataBaseAccess;
using TreasureCollector.Interfaces;

namespace TreasureCollector.Application.Services;

/// <summary>
/// Получение документов с отчетами о наличии предметов.
/// </summary>
public class InventoryReportService
{
  /// <summary>
  /// Репозиторий.
  /// </summary>
  private IItemsRepository repository = new DbRepository();
  
  /// <summary>
  /// Создать отчет о наличии предметов.
  /// </summary>
  public Task<string> GetInventoryReport<T>() where T: CollectibleItem
  {
    return Task.Run(() =>
    {
      var inventory = this.repository.GetByCriteria<T>(x => true);

      var report = new Document();
      Section section = report.AddSection();
      Paragraph titleParagraph = section.AddParagraph();
      titleParagraph.AppendText
      ($"Отчет о наличии предметов типа: {typeof(T)
        .GetField("NameOfClass", BindingFlags.Public | BindingFlags.Static)?
        .GetValue(null)}");
      titleParagraph.ApplyStyle(BuiltinStyle.Title);
      foreach (var item in inventory)
      {
        Paragraph itemParagraph = section.AddParagraph();
        itemParagraph.ApplyStyle(BuiltinStyle.BodyText);
        itemParagraph.AppendText($"Id: {item.GetType().GetProperty("Id")?.GetValue(item)} \n");

        var properties = item.GetType().GetProperties();
        foreach (var property in properties)
        {
          if (property.Name == "Id")
            continue;
          if (typeof(IHasId).IsAssignableFrom(property.PropertyType))
            itemParagraph.AppendText($"{property.GetValue(item)} \n");
          else
            itemParagraph.AppendText($"{property.Name}: {property.GetValue(item)} \n");
        }
      }
     
      var reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
        "reports", $"{Guid.NewGuid()}.docx");
      report.SaveToFile(reportPath, FileFormat.Docx);
      return reportPath;
    });
  }
}