using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using ao_id_extractor.Helpers;
using System.Windows.Forms;
using Microsoft.Win32;

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
        public BaseExtractor()
        {
            Program.OutputFolderPath = Program.OutputFolderPath == string.Empty ? Path.GetDirectoryName(Application.ExecutablePath) : Program.OutputFolderPath;

            if (string.IsNullOrWhiteSpace(Program.MainGameFolder))
            {
                string obj = (string)Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\SandboxAlbionOnline", false).GetValue("DisplayIcon");
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
            string s = DecryptBinFile(GetBinFilePath());
            List<IDContainer> ls = ExtractFromXML(s, withLocal);
            File.Delete(s);

            WriteToFile(ls);
        }

        public List<IDContainer> PureExtract(bool withLocal = true)
        {
            string s = DecryptBinFile(GetBinFilePath());
            return ExtractFromXML(s, withLocal);
        }

        private string DecryptBinFile(string binFile)
        {
            string output = BinaryDecrypter.DecryptBinaryFile(binFile);
            string binFileWOE = Path.GetFileNameWithoutExtension(binFile);

            Console.Out.WriteLine("Extracting " + binFileWOE + ".bin...");

            string finalOutPath = Path.Combine(Program.OutputFolderPath, binFileWOE + ".xml");
            Directory.CreateDirectory(Path.GetDirectoryName(finalOutPath));

            StreamWriter sw = File.CreateText(finalOutPath);
            sw.Write(output);
            sw.Close();

            return finalOutPath;
        }

        private void WriteToFile(List<IDContainer> items)
        {

            string filePathWithoutExtension = Path.Combine(Program.OutputFolderPath, Path.GetFileNameWithoutExtension(GetBinFilePath()));
            if (!Directory.Exists(Path.GetDirectoryName(filePathWithoutExtension)))
            {
                DirectoryInfo di = Directory.CreateDirectory(Path.GetDirectoryName(filePathWithoutExtension));
            }

            if (Program.ExportType == ExportType.TextList || Program.ExportType == ExportType.Both)
            {
                using (var sw = File.CreateText(filePathWithoutExtension + ".txt"))
                {
                    foreach (IDContainer i in items)
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
