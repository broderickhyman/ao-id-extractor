using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace ao_id_extractor.Extractors
{
  public class ItemExtractor : BaseExtractor
  {
    private readonly string LocalizationItemPrefix = "@ITEMS_";
    private readonly string LocalizationItemDescPostfix = "_DESC";

    public ItemExtractor() : base()
    {
    }

    protected override List<IDContainer> ExtractFromXML(string xmlFile, bool withLocal = true)
    {
      var outputList = new List<IDContainer>();
      var journals = new List<IDContainer>();

      // Param 0 is the xml file
      var encodedString = Encoding.UTF8.GetBytes(File.ReadAllText(xmlFile, Encoding.UTF8));

      // Put the byte array into a stream and rewind it to the beginning
      var ms = new MemoryStream(encodedString);
      ms.Flush();
      ms.Position = 0;

      // Build the XmlDocument from the MemorySteam of UTF-8 encoded bytes
      var xmlDoc = new XmlDocument();
      xmlDoc.Load(ms);

      var rootNode = xmlDoc.LastChild;

      var index = 0;
      foreach (XmlNode node in rootNode.ChildNodes)
      {
        if (node.NodeType == XmlNodeType.Element)
        {
          var aName = node.Attributes["uniquename"].Value;
          var ent = node.Attributes["enchantmentlevel"];
          var desc = node.Attributes["descriptionlocatag"];
          var name = node.Attributes["descvariable0"];
          var entch = "";
          if (ent != null && ent.Value != "0")
          {
            entch = "@" + ent.Value;
          }
          outputList.Add(new ItemContainer()
          {
            Index = index.ToString(),
            UniqueName = aName + entch,
            LocalizationDescriptionVariable = desc != null ? desc.Value : LocalizationItemPrefix + aName + LocalizationItemDescPostfix,
            LocalizationNameVariable = name != null ? name.Value : LocalizationItemPrefix + aName
          });
          index++;

          if (node.Name == "journalitem")
          {
            journals.Add(new ItemContainer() { UniqueName = aName });
          }

          var ele = FindElement(node, "enchantments");
          if (ele != null)
          {
            foreach (XmlElement el in ele.ChildNodes)
            {
              var eName = node.Attributes["uniquename"].Value + "@" + el.Attributes["enchantmentlevel"].Value;
              outputList.Add(new ItemContainer() { Index = index.ToString(), UniqueName = eName, LocalizationDescriptionVariable = desc != null ? desc.Value : LocalizationItemPrefix + aName + LocalizationItemDescPostfix, LocalizationNameVariable = name != null ? name.Value : LocalizationItemPrefix + aName });

              index++;
            }
          }
        }
      }

      ms.Close();

      foreach (ItemContainer j in journals)
      {
        outputList.Add(new ItemContainer() { Index = index.ToString(), UniqueName = j.UniqueName + "_EMPTY", LocalizationDescriptionVariable = LocalizationItemPrefix + j.UniqueName + "_EMPTY" + LocalizationItemDescPostfix, LocalizationNameVariable = LocalizationItemPrefix + j.UniqueName + "_EMPTY" });
        index++;
        outputList.Add(new ItemContainer() { Index = index.ToString(), UniqueName = j.UniqueName + "_FULL", LocalizationDescriptionVariable = LocalizationItemPrefix + j.UniqueName + "_FULL" + LocalizationItemDescPostfix, LocalizationNameVariable = LocalizationItemPrefix + j.UniqueName + "_FULL" });
        index++;
      }

      if (withLocal)
        ExtractAndSetLocalization(outputList);

      return outputList;
    }

    private void ExtractAndSetLocalization(List<IDContainer> itemsList)
    {
      // Put the byte array into a stream and rewind it to the beginning
      var ms = new MemoryStream();
      BinaryDecrypter.DecryptBinaryFile(Path.Combine(Program.MainGameFolder, @".\game\Albion-Online_Data\StreamingAssets\GameData\localization.bin"), ms);
      ms.Flush();
      ms.Position = 0;

      // Build the XmlDocument from the MemorySteam of UTF-8 encoded bytes
      var xmlDoc = new XmlDocument();
      xmlDoc.Load(ms);

      var rootNode = xmlDoc.LastChild.LastChild;

      foreach (XmlNode node in rootNode.ChildNodes)
      {
        if (node.NodeType == XmlNodeType.Element)
        {
          var tuid = node.Attributes["tuid"];
          if (tuid != null)
          {
            if (tuid.Value.StartsWith(LocalizationItemPrefix))
            {
              // is the item description
              if (tuid.Value.EndsWith(LocalizationItemDescPostfix))
              {
                var item = itemsList.FindAll(cont => ((ItemContainer)cont).LocalizationDescriptionVariable == tuid.Value);

                var descDict = new Dictionary<string, string>();
                foreach (XmlNode n in node.ChildNodes)
                {
                  var lang = n.Attributes["xml:lang"].Value;
                  var desc = n.LastChild.InnerText;

                  descDict.Add(lang, desc);
                }

                foreach (ItemContainer cont in item)
                {
                  cont.LocalizedDescriptions = descDict;
                }
              }
              // is item name
              else
              {
                var item = itemsList.FindAll(cont => ((ItemContainer)cont).LocalizationNameVariable == tuid.Value);

                var nameDict = new Dictionary<string, string>();
                foreach (XmlNode n in node.ChildNodes)
                {
                  var lang = n.Attributes["xml:lang"].Value;
                  var name = n.LastChild.InnerText;

                  nameDict.Add(lang, name);
                }

                foreach (ItemContainer cont in item)
                {
                  cont.LocalizedNames = nameDict;
                }
              }
            }
            else
            {
              continue;
            }
          }
        }
      }

      foreach (ItemContainer cont in itemsList)
      {
        if (cont.LocalizedDescriptions == null)
        {
          cont.LocalizationDescriptionVariable = null;
        }

        if (cont.LocalizedNames == null)
        {
          cont.LocalizationNameVariable = null;
        }
      }
    }

    protected override string GetBinFilePath()
    {
      return Path.Combine(Program.MainGameFolder, @".\game\Albion-Online_Data\StreamingAssets\GameData\items.bin");
    }
  }
}
