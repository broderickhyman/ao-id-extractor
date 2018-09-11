using ao_id_extractor.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace ao_id_extractor.Extractors
{
  public class ResourceExtractor : BaseExtractor
  {
    public ResourceExtractor() : base()
    {

    }

    protected override void ExtractFromXML(Stream inputXmlFile, MultiStream outputStream, Action<MultiStream, IDContainer> writeItem, bool withLocal = true)
    {
      var xmlDoc = new XmlDocument();
      xmlDoc.Load(inputXmlFile);

      XmlNode rootNode = xmlDoc.DocumentElement;

      foreach (XmlNode node in rootNode.ChildNodes)
      {
        if (node.NodeType == XmlNodeType.Element)
        {
          var locID = node.Attributes["id"]?.Value;
          if (string.IsNullOrEmpty(locID)) { continue; }
          var locName = node.Attributes["displayname"].Value;

          writeItem(outputStream, new IDContainer()
          {
            Index = locID,
            UniqueName = locName
          });
        }
      }
    }

    protected override string GetBinFilePath()
    {
      return Path.Combine(Program.MainGameFolder, @".\game\Albion-Online_Data\StreamingAssets\GameData\resources.bin");
    }
  }
}
