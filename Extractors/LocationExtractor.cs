using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace ao_id_extractor.Extractors
{
  public class LocationExtractor : BaseExtractor
  {
    public LocationExtractor() : base()
    {

    }

    protected override List<IDContainer> ExtractFromXML(string xmlFile, bool withLocal = true)
    {
      var outputList = new HashSet<IDContainer>();

      // Param 0 is the xml file
      var encodedString = Encoding.UTF8.GetBytes(File.ReadAllText(xmlFile, Encoding.UTF8));

      // Put the byte array into a stream and rewind it to the beginning
      var ms = new MemoryStream(encodedString);
      ms.Flush();
      ms.Position = 0;

      // Build the XmlDocument from the MemorySteam of UTF-8 encoded bytes
      var xmlDoc = new XmlDocument();
      xmlDoc.Load(ms);

      var rootNode = xmlDoc.LastChild.FirstChild;

      foreach (XmlNode node in rootNode.ChildNodes)
      {
        if (node.NodeType == XmlNodeType.Element)
        {
          var locID = node.Attributes["id"].Value;
          var locName = node.Attributes["displayname"].Value;

          outputList.Add(new IDContainer() { Index = locID, UniqueName = locName });
        }
      }

      ms.Close();

      return outputList.ToList();
    }

    protected override string GetBinFilePath()
    {
      return Path.Combine(Program.MainGameFolder, @".\game\Albion-Online_Data\StreamingAssets\GameData\cluster\world.bin");
    }
  }
}
