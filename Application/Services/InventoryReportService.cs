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

using System.Collections.Generic;

/// <summary>
/// Получение документов с отчетами о наличии предметов.
/// </summary>
public class InventoryReportService : ServiceBase
{
  /// <summary>
  /// Создать отчет о наличии предметов.
  /// </summary>
  public Task<string> GetInventoryReport<T>() where T: CollectibleItem
  {
    return Task.Run(() =>
    {
      List<T> inventory = this.repository.GetByCriteria<T>(x => true);

      var report = new Document();
      Section section = report.AddSection();
      Paragraph titleParagraph = section.AddParagraph();
      titleParagraph.AppendText
      ($"Отчет о наличии предметов типа: {typeof(T)
        .GetField("NameOfClass", BindingFlags.Public | BindingFlags.Static)?
        .GetValue(null)}");
      titleParagraph.ApplyStyle(BuiltinStyle.Title);
      foreach (T item in inventory)
      {
        Paragraph itemParagraph = section.AddParagraph();
        itemParagraph.ApplyStyle(BuiltinStyle.BodyText);
        itemParagraph.AppendText($"Id: {item.GetType().GetProperty("Id")?.GetValue(item)} \n");

        PropertyInfo[] properties = item.GetType().GetProperties();
        foreach (PropertyInfo property in properties)
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