using ao_id_extractor.Extractors;
using Microsoft.Win32;
using System;
using System.IO;
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
      AOFolder.Text = Program.MainGameFolder;
      OutFolder.Text = Program.OutputFolderPath;
      ExtractionMode.SelectedIndex = (int)Program.ExportMode;
      ExportType.SelectedIndex = (int)Program.ExportType;
    }

    private void SelectFromRegistry_Click(object sender, EventArgs e)
    {
      var obj = (string)Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\SandboxAlbionOnline", false).GetValue("DisplayIcon");
      Program.MainGameFolder = Path.Combine(Path.GetDirectoryName(obj.Trim('\"')), "..");

      AOFolder.Text = Program.MainGameFolder;
    }

    private void SelectAOFolder_Click(object sender, EventArgs e)
    {
      var folderBrowserDialog = new FolderBrowserDialog
      {
        ShowNewFolderButton = false,
        SelectedPath = Path.GetDirectoryName(Application.ExecutablePath),
        Description = "Please select the Albion Online Gamefolder. Example: C:\\AlbionOnline"
      };
      var res = folderBrowserDialog.ShowDialog();

      if (res == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
      {
        Program.MainGameFolder = folderBrowserDialog.SelectedPath;
        AOFolder.Text = Program.MainGameFolder;
      }
    }

    private void SelectOutFolder_Click(object sender, EventArgs e)
    {
      var folderBrowserDialog = new FolderBrowserDialog
      {
        ShowNewFolderButton = true,
        SelectedPath = Path.GetDirectoryName(Application.ExecutablePath),
        Description = "Please select an output folder for the Extracted Files"
      };
      var res = folderBrowserDialog.ShowDialog();

      if (res == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
      {
        Program.OutputFolderPath = folderBrowserDialog.SelectedPath;
        OutFolder.Text = Program.OutputFolderPath;
      }
    }

    private void Extract_Click(object sender, EventArgs e)
    {
      Invoke(new Action(Program.RunExtractions));
    }

    private void ExtractionMode_SelectedIndexChanged(object sender, EventArgs e)
    {
      Program.ExportMode = (ExportMode)((ComboBox)sender).SelectedIndex;
    }

    private void ExportType_SelectedIndexChanged(object sender, EventArgs e)
    {
      Program.ExportType = (ExportType)((ComboBox)sender).SelectedIndex;
    }
  }
}
