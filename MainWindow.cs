using ao_id_extractor.Extractors;
using ao_id_extractor.Helpers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ao_id_extractor
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            cbExtractionMode.SelectedIndex = 0;
            cbExportType.SelectedIndex = 0;
        }

        private void btnSelectFromRegistry_Click(object sender, EventArgs e)
        {
            string obj = (string)Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\SandboxAlbionOnline", false).GetValue("DisplayIcon");
            Program.MainGameFolder = Path.Combine(Path.GetDirectoryName(obj.Trim('\"')), "..");

            tbAOFolder.Text = Program.MainGameFolder;
        }

        private void btnSelectAOFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = false;
            folderBrowserDialog.SelectedPath = Path.GetDirectoryName(Application.ExecutablePath);
            folderBrowserDialog.Description = "Please select the Albion Online Gamefolder. Example: C:\\AlbionOnline";
            DialogResult res = folderBrowserDialog.ShowDialog();

            if (res == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            {
                Program.MainGameFolder = folderBrowserDialog.SelectedPath;
                tbAOFolder.Text = Program.MainGameFolder;
            }
        }

        private void btnSelectOutFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = true;
            folderBrowserDialog.SelectedPath = Path.GetDirectoryName(Application.ExecutablePath);
            folderBrowserDialog.Description = "Please select an output folder for the Extracted Files";
            DialogResult res = folderBrowserDialog.ShowDialog();

            if (res == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            {
                Program.OutputFolderPath = folderBrowserDialog.SelectedPath;
                tbOutFolder.Text = Program.OutputFolderPath;
            }
        }

        private void btnExtract_Click(object sender, EventArgs e)
        {

            Invoke(new Action(Program.RunExtractions));
        }

        private void cbExtractionMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.ExportMode = (ExportMode)((ComboBox)sender).SelectedIndex;
        }

        private void cbExportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.ExportType = (ExportType)((ComboBox)sender).SelectedIndex;
        }
    }
}
