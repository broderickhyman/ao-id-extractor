using ao_id_extractor.Helpers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ao_id_extractor.Extractors
{
    public class BinaryDumper
    {
        protected string OutputFolderPath { get; set; }
        protected string MainGameFolder { get; set; }

        public BinaryDumper(string outputFolderPath, string gameFolder)
        {
            OutputFolderPath = outputFolderPath;
            MainGameFolder = gameFolder;

            if (string.IsNullOrWhiteSpace(MainGameFolder))
            {
                string obj = (string)Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\SandboxAlbionOnline", false).GetValue("DisplayIcon");
                MainGameFolder = Path.Combine(Path.GetDirectoryName(obj.Trim('\"')), "..");
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
            return Path.Combine(MainGameFolder, @".\game\Albion-Online_Data\StreamingAssets\GameData");
        }

        private string DecryptBinFile(string binFile, string subdir)
        {
            string output = BinaryDecrypter.DecryptBinaryFile(binFile);
            string binFileWOE = Path.GetFileNameWithoutExtension(binFile);
            string outSubdirs = Path.GetDirectoryName(OutputFolderPath + "\\" + subdir);
            Directory.CreateDirectory(outSubdirs);
            string finalOutPath = Path.Combine(outSubdirs, binFileWOE + ".xml");

            StreamWriter sw = File.CreateText(finalOutPath);
            sw.Write(output);
            sw.Close();

            return finalOutPath;
        }
    }
}
