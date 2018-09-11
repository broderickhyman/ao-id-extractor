using Microsoft.Win32;
using System;
using System.IO;

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
      var output = BinaryDecrypter.DecryptBinaryFile(binFile);
      var binFileWOE = Path.GetFileNameWithoutExtension(binFile);
      var outSubdirs = Path.GetDirectoryName(Path.Combine(Program.OutputFolderPath, subdir));

      Console.Out.WriteLine("Extracting " + binFileWOE + ".bin...");

      if (outSubdirs != "")
        Directory.CreateDirectory(outSubdirs);
      var finalOutPath = Path.Combine(outSubdirs, binFileWOE + ".xml");

      var sw = File.CreateText(finalOutPath);
      sw.Write(output);
      sw.Close();

      return finalOutPath;
    }
  }
}
