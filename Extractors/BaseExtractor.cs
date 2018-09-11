using ao_id_extractor.Helpers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace ao_id_extractor.Extractors
{
  public class IDContainer
  {
    public string Index { get; set; }
    public string UniqueName { get; set; }
  }

  public class ItemContainer : IDContainer
  {
    public string LocalizationNameVariable { get; set; }
    public string LocalizationDescriptionVariable { get; set; }
    public Dictionary<string, string> LocalizedNames { get; set; }
    public Dictionary<string, string> LocalizedDescriptions { get; set; }
  }

  public enum ExportType
  {
    TextList,
    Json,
    Both
  }

  public enum ExportMode
  {
    Item_Extraction,
    Location_Extraction,
    Resource_Extraction,
    Dump_All_XML,
    Extract_Items_Locations_Resource
  }

  public abstract class BaseExtractor
  {
    protected BaseExtractor()
    {
      Program.OutputFolderPath = Program.OutputFolderPath?.Length == 0 ? Path.GetDirectoryName(Application.ExecutablePath) : Program.OutputFolderPath;

      if (string.IsNullOrWhiteSpace(Program.MainGameFolder))
      {
        var obj = (string)Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\SandboxAlbionOnline", false).GetValue("DisplayIcon");
        Program.MainGameFolder = Path.Combine(Path.GetDirectoryName(obj.Trim('\"')), "..");
      }
    }

    protected abstract string GetBinFilePath();
    protected abstract List<IDContainer> ExtractFromXML(string xmlFile, bool withLocal = true);

    protected XmlElement FindElement(XmlNode node, string elementName)
    {
      foreach (XmlNode childNode in node.ChildNodes)
      {
        if (childNode is XmlElement ele && ele.Name == elementName)
        {
          return ele;
        }
      }

      return null;
    }

    public void Extract(bool withLocal = true)
    {
      var s = DecryptBinFile(GetBinFilePath());
      var ls = ExtractFromXML(s, withLocal);
      File.Delete(s);

      WriteToFile(ls);
    }

    public List<IDContainer> PureExtract(bool withLocal = true)
    {
      var s = DecryptBinFile(GetBinFilePath());
      return ExtractFromXML(s, withLocal);
    }

    private string DecryptBinFile(string binFile)
    {
      var output = BinaryDecrypter.DecryptBinaryFile(binFile);
      var binFileWOE = Path.GetFileNameWithoutExtension(binFile);

      Console.Out.WriteLine("Extracting " + binFileWOE + ".bin...");

      var finalOutPath = Path.Combine(Program.OutputFolderPath, binFileWOE + ".xml");
      Directory.CreateDirectory(Path.GetDirectoryName(finalOutPath));

      var sw = File.CreateText(finalOutPath);
      sw.Write(output);
      sw.Close();

      return finalOutPath;
    }

    private void WriteToFile(List<IDContainer> items)
    {
      var filePathWithoutExtension = Path.Combine(Program.OutputFolderPath, Path.GetFileNameWithoutExtension(GetBinFilePath()));
      if (!Directory.Exists(Path.GetDirectoryName(filePathWithoutExtension)))
      {
        var di = Directory.CreateDirectory(Path.GetDirectoryName(filePathWithoutExtension));
      }

      if (Program.ExportType == ExportType.TextList || Program.ExportType == ExportType.Both)
      {
        using (var sw = File.CreateText(filePathWithoutExtension + ".txt"))
        {
          foreach (var i in items)
          {
            sw.WriteLine("{0}:{1}", i.Index, i.UniqueName);
          }
        }
      }
      if (Program.ExportType == ExportType.Json || Program.ExportType == ExportType.Both)
      {
        using (var sw = File.CreateText(filePathWithoutExtension + ".json"))
        {
          sw.Write(JSONHelper.FormatJson(items.ToJSON()));
        }
      }
    }
  }
}
