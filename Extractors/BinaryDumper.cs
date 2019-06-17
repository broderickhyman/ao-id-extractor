using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Xml;

namespace ao_id_extractor.Extractors
{
  public class BinaryDumper
  {
    public BinaryDumper()
    {
      if (string.IsNullOrWhiteSpace(Program.MainGameFolder))
      {
        var obj = (string)Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\SandboxAlbionOnline", false).GetValue("DisplayIcon");
        Program.MainGameFolder = Path.Combine(Path.GetDirectoryName(obj.Trim('\"')), "..");
      }
    }

    public void Extract()
    {
      var allfiles = Directory.GetFiles(GetBinFilePath(), "*.bin", SearchOption.AllDirectories);
      var outFiles = (string[])allfiles.Clone();
      for (var i = 0; i < outFiles.Length; i++)
        outFiles[i] = outFiles[i].Remove(0, outFiles[i].LastIndexOf("GameData\\") + "GameData\\".Length);

      for (var i = 0; i < allfiles.Length; i++)
        DecryptBinFile(allfiles[i], outFiles[i]);
    }

    private string GetBinFilePath()
    {
      return Path.Combine(Program.MainGameFolder, @".\game\Albion-Online_Data\StreamingAssets\GameData");
    }

    private string DecryptBinFile(string binFile, string subdir)
    {
      var binFileWOE = Path.GetFileNameWithoutExtension(binFile);
      var outSubdirs = Path.GetDirectoryName(Path.Combine(Program.OutputFolderPath, subdir));

      Console.Out.WriteLine("Extracting " + binFileWOE + ".bin...");

      if (outSubdirs != "")
        Directory.CreateDirectory(outSubdirs);
      var finalOutPath = Path.Combine(outSubdirs, binFileWOE);
      var finalXmlPath = finalOutPath + ".xml";
      var finalJsonPath = finalOutPath + ".json";

      using (var outputXmlFile = File.Create(finalXmlPath))
      {
        BinaryDecrypter.DecryptBinaryFile(binFile, outputXmlFile);
      }

      if (!subdir.StartsWith("cluster") && !subdir.StartsWith("templates"))
      {
        var xmlDocument = new XmlDocument();
        var xmlReaderSettings = new XmlReaderSettings
        {
          IgnoreComments = true
        };
        var xmlReader = XmlReader.Create(finalXmlPath, xmlReaderSettings);
        xmlDocument.Load(xmlReader);
        File.WriteAllText(finalJsonPath, JsonConvert.SerializeXmlNode(xmlDocument, Newtonsoft.Json.Formatting.Indented, false));
      }

      return finalOutPath;
    }
  }
}
