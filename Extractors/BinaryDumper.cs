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
                string obj = (string)Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\SandboxAlbionOnline", false).GetValue("DisplayIcon");
                Program.MainGameFolder = Path.Combine(Path.GetDirectoryName(obj.Trim('\"')), "..");
            }
        }

        public void Extract()
        {
            string[] allfiles = Directory.GetFiles(GetBinFilePath(), "*.bin", SearchOption.AllDirectories);
            string[] outFiles = (string[])allfiles.Clone();
            for (int i = 0; i < outFiles.Length; i++)
                outFiles[i] = outFiles[i].Remove(0, outFiles[i].LastIndexOf("GameData\\")+ "GameData\\".Length);

            for (int i = 0; i < allfiles.Length; i++)
                DecryptBinFile(allfiles[i], outFiles[i]);
        }

        private string GetBinFilePath()
        {
            return Path.Combine(Program.MainGameFolder, @".\game\Albion-Online_Data\StreamingAssets\GameData");
        }

        private string DecryptBinFile(string binFile, string subdir)
        {          
            string output = BinaryDecrypter.DecryptBinaryFile(binFile);
            string binFileWOE = Path.GetFileNameWithoutExtension(binFile);
            string outSubdirs = Path.GetDirectoryName(Path.Combine(Program.OutputFolderPath, subdir));

            Console.Out.WriteLine("Extracting " + binFileWOE + ".bin...");

            if (outSubdirs != "")
                Directory.CreateDirectory(outSubdirs);
            string finalOutPath = Path.Combine(outSubdirs, binFileWOE + ".xml");

            StreamWriter sw = File.CreateText(finalOutPath);
            sw.Write(output);
            sw.Close();

            return finalOutPath;
        }
    }
}
