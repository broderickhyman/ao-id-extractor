using ao_id_extractor.Extractors;
using ao_id_extractor.Helpers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ao_id_extractor
{
    public static class ProcessExtensions
    {
        private static string FindIndexedProcessName(int pid)
        {
            var processName = Process.GetProcessById(pid).ProcessName;
            var processesByName = Process.GetProcessesByName(processName);
            string processIndexdName = null;

            for (var index = 0; index < processesByName.Length; index++)
            {
                processIndexdName = index == 0 ? processName : processName + "#" + index;
                var processId = new PerformanceCounter("Process", "ID Process", processIndexdName);
                if ((int)processId.NextValue() == pid)
                {
                    return processIndexdName;
                }
            }

            return processIndexdName;
        }

        private static Process FindPidFromIndexedProcessName(string indexedProcessName)
        {
            var parentId = new PerformanceCounter("Process", "Creating Process ID", indexedProcessName);
            return Process.GetProcessById((int)parentId.NextValue());
        }

        public static Process Parent(this Process process)
        {
            return FindPidFromIndexedProcessName(FindIndexedProcessName(process.Id));
        }
    }

    public class Program
    {
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        public static string OutputFolderPath { get; set; }
        public static ExportType ExportType { get; set; }
        public static ExportMode ExportMode { get; set; }
        public static string MainGameFolder { get; set; }

        static void PrintCmdHelp()
        {
            Console.WriteLine("How to use:\nao-id-extractor.exe modeID outFormat [outFolder]\n" +
                "modeID\t\t#Extraction 0=Item Extraction, 1=Location Extraction, 2=Resource Extraction, 3=Dump All, 4=Extract Items & Locations & Resource\n" +
                "outFormat\t#l=Text List, j=JSON b=Both\n" +
                "[outFolder]\t#OPTIONAL: Output folder path. Default: current directory\n" +
                "[gameFolder]\t#OPTIONAL: Location of the main AlbionOnline folder");
        }

        static List<string> ParseCommandline(string[] args)
        {
            string modeID;
            string outFormat;
            string gameFolder = "";
            string outFolder = "";

            if (args.Length >= 2)
            {

                if (int.Parse(args[0]) > -1 && int.Parse(args[0]) <= 3)
                {
                    modeID = args[0];
                }
                else
                {
                    PrintCmdHelp();
                    return null;
                }


                if (args[1] == "l" || args[1] == "j" || args[1] == "b")
                {
                    outFormat = args[1];
                }
                else
                {
                    PrintCmdHelp();
                    return null;
                }

                if (args.Length == 3)
                {
                    outFolder = args[2];
                }

                if (args.Length == 4)
                {
                    gameFolder = args[3];
                }
            }
            else
            {
                PrintCmdHelp();
                return null;
            }

            return new List<string>() { modeID, outFormat, outFolder, gameFolder };
        }

        [STAThread]
        static void Main(string[] args)
        {
            OutputFolderPath = "";
            MainGameFolder = "";

            string parentName = ProcessExtensions.Parent(Process.GetCurrentProcess()).ProcessName;

            
            DeleteOldFilesAndDirs();

           
            if (parentName != "cmd")
            {
                MainWindow mainWindow = new MainWindow();
                Application.EnableVisualStyles();
                Console.SetOut(new MultiTextWriter(new ControlWriter(mainWindow.tbConsole), Console.Out));
                Application.Run(mainWindow);
                return;
            }
            else
            {
                AllocConsole();
                List<string> cmds = ParseCommandline(args);

                if (cmds == null)
                {
                    Console.Out.WriteLine("\nPress Any Key to Quit");
                    Console.ReadKey();
                    return;
                }

                if (string.IsNullOrWhiteSpace(cmds[2]))
                    cmds[2] = Directory.GetCurrentDirectory();

                OutputFolderPath = cmds[2];

                if (string.IsNullOrWhiteSpace(cmds[1]) || string.IsNullOrWhiteSpace(cmds[0]))
                {
                    Console.Out.WriteLine("\nPress Any Key to Quit");
                    Console.ReadKey();
                    return;
                }

                var exportType = ExportType.Both;
                switch (cmds[1])
                {
                    case "l":
                        exportType = ExportType.TextList;
                        break;
                    case "j":
                        exportType = ExportType.Json;
                        break;
                }

                ExportType = exportType;

                if (int.Parse(cmds[0]) > 4 || int.Parse(cmds[0]) < 0)
                    return;

                ExportMode = (ExportMode)int.Parse(cmds[0]);

                RunExtractions();                
            }

            Console.Out.WriteLine("\nPress Any Key to Quit");
            Console.ReadKey();
        }

        public static void RunExtractions()
        {
            Console.Out.WriteLine("#---- Starting Extraction Operation ----#");

            string exportTypeString = "";
            if (ExportType == ExportType.TextList)
                exportTypeString = "Text List";
            else if (ExportType == ExportType.Json)
                exportTypeString = "JSON";
            else
                exportTypeString = "Text List and JSON";

            switch (ExportMode)
            {
                case ExportMode.Item_Extraction:
                    Console.Out.WriteLine("--- Starting Extraction of Items as " + exportTypeString + " ---");
                    new ItemExtractor().Extract();
                    Console.Out.WriteLine("--- Extraction Complete! ---");
                    break;
                case ExportMode.Location_Extraction:
                    Console.Out.WriteLine("--- Starting Extraction of Locations as " + exportTypeString + " ---");
                    new LocationExtractor().Extract();
                    Console.Out.WriteLine("--- Extraction Complete! ---");
                    break;
                case ExportMode.Resource_Extraction:
                    Console.Out.WriteLine("--- Starting Extraction of Resources as " + exportTypeString + " ---");
                    new ResourceExtractor().Extract();
                    Console.Out.WriteLine("--- Extraction Complete! ---");
                    break;
                case ExportMode.Dump_All_XML:
                    Console.Out.WriteLine("--- Starting Extraction of All Files as XML ---");
                    new BinaryDumper().Extract();
                    Console.Out.WriteLine("--- Extraction Complete! ---");
                    break;
                case ExportMode.Extract_Items_Locations_Resource:
                    Console.Out.WriteLine("--- Starting Extraction of Items as " + exportTypeString + " ---");
                    new ItemExtractor().Extract();
                    Console.Out.WriteLine("--- Extraction Complete! ---");

                    Console.Out.WriteLine("--- Starting Extraction of Locations as " + exportTypeString + " ---");
                    new LocationExtractor().Extract();
                    Console.Out.WriteLine("--- Extraction Complete! ---");

                    Console.Out.WriteLine("--- Starting Extraction of Resources as " + exportTypeString + " ---");
                    new ResourceExtractor().Extract();
                    Console.Out.WriteLine("--- Extraction Complete! ---");
                    break;
            }
            Console.Out.WriteLine("#---- Finished Extraction Operation ----#");
        }

        static void DeleteOldFilesAndDirs()
        {
            DirectoryInfo baseDir = new DirectoryInfo(Path.GetDirectoryName(Application.ExecutablePath));
            if (baseDir.Exists)
            {
                //delete sub directories:
                foreach (var dir in baseDir.EnumerateDirectories())
                {
                    System.IO.Directory.Delete(dir.FullName, true);
                }

                string name = Process.GetCurrentProcess().MainModule.FileName;
                string app = System.IO.Path.GetFileName(name);

                //delete files:
                foreach (var file in baseDir.GetFiles())
                {
                    if (file.Extension != ".exe" && file.Extension != ".md" && file.Extension != ".pdb" && file.Extension != ".bat")
                        file.Delete();
                }
            }
        }
    }
}
