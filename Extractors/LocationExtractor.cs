using ao_id_extractor.Helpers;
using System;
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

    protected override void ExtractFromXML(Stream inputXmlFile, MultiStream outputStream, Action<MultiStream, IDContainer, bool> writeItem, bool withLocal = true)
    {
      var xmlDoc = new XmlDocument();
      xmlDoc.Load(inputXmlFile);

      var rootNode = xmlDoc.LastChild.FirstChild;
      var first = true;
      foreach (XmlNode node in rootNode.ChildNodes)
      {
        if (node.NodeType == XmlNodeType.Element)
        {
          var locID = node.Attributes["id"].Value;
          var locName = node.Attributes["displayname"].Value;

          writeItem(outputStream, new IDContainer()
          {
            Index = locID,
            UniqueName = locName
          }, first);
          if (first)
          {
            first = false;
          }
        }
      }
    }

    protected override string GetBinFilePath()
    {
      return Path.Combine(Program.MainGameFolder, @".\game\Albion-Online_Data\StreamingAssets\GameData\cluster\world.bin");
    }
  }
}
