using ao_id_extractor.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    protected override void ExtractFromXML(Stream inputXmlFile, MultiStream outputStream, Action<MultiStream, IDContainer, bool> writeItem, bool withLocal = true)
    {
      var journals = new List<IDContainer>();
      var xmlDoc = new XmlDocument();
      xmlDoc.Load(inputXmlFile);

      var rootNode = xmlDoc.LastChild;

      var localizationData = default(LocalizationData);
      if (withLocal)
      {
        localizationData = ExtractLocalization();
      }

      var index = 0;
      var first = true;
      foreach (XmlNode node in rootNode.ChildNodes)
      {
        if (node.NodeType == XmlNodeType.Element && !string.IsNullOrEmpty(node.Attributes["uniquename"]?.Value))
        {
          var uniqueName = node.Attributes["uniquename"].Value;
          var enchantmentLevel = node.Attributes["enchantmentlevel"];
          var description = node.Attributes["descriptionlocatag"];
          var name = node.Attributes["descvariable0"];
          var enchantment = "";
          if (enchantmentLevel != null && enchantmentLevel.Value != "0")
          {
            enchantment = "@" + enchantmentLevel.Value;
          }
          var localizationNameVariable = name != null ? name.Value : LocalizationItemPrefix + uniqueName;
          if (uniqueName.Contains("ARTEFACT"))
          {
            localizationNameVariable = LocalizationItemPrefix + uniqueName;
          }
          var container = new ItemContainer()
          {
            Index = index.ToString(),
            UniqueName = uniqueName + enchantment,
            LocalizationDescriptionVariable = description != null ? description.Value : LocalizationItemPrefix + uniqueName + LocalizationItemDescPostfix,
            LocalizationNameVariable = localizationNameVariable
          };
          SetLocalization(localizationData, container);
          writeItem(outputStream, container, first);
          if (first)
          {
            first = false;
          }
          index++;

          if (node.Name == "journalitem")
          {
            journals.Add(new ItemContainer()
            {
              UniqueName = uniqueName
            });
          }

          var element = FindElement(node, "enchantments");
          if (element != null)
          {
            foreach (XmlElement childNode in element.ChildNodes)
            {
              var enchantmentName = node.Attributes["uniquename"].Value + "@" + childNode.Attributes["enchantmentlevel"].Value;
              container = new ItemContainer()
              {
                Index = index.ToString(),
                UniqueName = enchantmentName,
                LocalizationDescriptionVariable = description != null ? description.Value : LocalizationItemPrefix + uniqueName + LocalizationItemDescPostfix,
                LocalizationNameVariable = name != null ? name.Value : LocalizationItemPrefix + uniqueName
              };
              SetLocalization(localizationData, container);
              writeItem(outputStream, container, false);

              index++;
            }
          }
        }
      }

      foreach (ItemContainer j in journals)
      {
        var container = new ItemContainer()
        {
          Index = index.ToString(),
          UniqueName = j.UniqueName + "_EMPTY",
          LocalizationDescriptionVariable = LocalizationItemPrefix + j.UniqueName + "_EMPTY" + LocalizationItemDescPostfix,
          LocalizationNameVariable = LocalizationItemPrefix + j.UniqueName + "_EMPTY"
        };
        SetLocalization(localizationData, container);
        writeItem(outputStream, container, false);
        index++;
        container = new ItemContainer()
        {
          Index = index.ToString(),
          UniqueName = j.UniqueName + "_FULL",
          LocalizationDescriptionVariable = LocalizationItemPrefix + j.UniqueName + "_FULL" + LocalizationItemDescPostfix,
          LocalizationNameVariable = LocalizationItemPrefix + j.UniqueName + "_FULL"
        };
        SetLocalization(localizationData, container);
        writeItem(outputStream, container, false);
        index++;
      }
    }

    private void SetLocalization(LocalizationData data, ItemContainer item)
    {
      if (data == default(LocalizationData)) return;
      if (data.LocalizedDescriptions.TryGetValue(item.LocalizationDescriptionVariable, out var descriptions))
      {
        item.LocalizedDescriptions = descriptions;
      }
      if (data.LocalizedNames.TryGetValue(item.LocalizationNameVariable, out var names))
      {
        item.LocalizedNames = names;
      }
    }

    private LocalizationData ExtractLocalization()
    {
      var localizationData = new LocalizationData();

      var xmlFileLocation = DecryptBinFile(Path.Combine(Program.MainGameFolder, @".\game\Albion-Online_Data\StreamingAssets\GameData\localization.bin"));

      var xmlDoc = new XmlDocument();
      using (var inputStream = File.OpenRead(xmlFileLocation))
      {
        xmlDoc.Load(inputStream);

        var rootNode = xmlDoc.LastChild.LastChild;


        foreach (XmlNode node in rootNode.ChildNodes)
        {
          if (node.NodeType == XmlNodeType.Element)
          {
            var tuid = node.Attributes["tuid"];
            if (tuid?.Value.StartsWith(LocalizationItemPrefix) == true)
            {
              var languages = node.ChildNodes
                  .Cast<XmlNode>()
                  .ToDictionary(x => x.Attributes["xml:lang"].Value, y => y.LastChild.InnerText);
              // is the item description
              if (tuid.Value.EndsWith(LocalizationItemDescPostfix))
              {
                localizationData.LocalizedDescriptions[tuid.Value] = languages;
              }
              // is item name
              else
              {
                localizationData.LocalizedNames[tuid.Value] = languages;
              }
            }
          }
        }
      }

      return localizationData;
    }

    protected override string GetBinFilePath()
    {
      return Path.Combine(Program.MainGameFolder, @".\game\Albion-Online_Data\StreamingAssets\GameData\items.bin");
    }

    public class LocalizationData
    {
      public Dictionary<string, Dictionary<string, string>> LocalizedNames = new Dictionary<string, Dictionary<string, string>>();
      public Dictionary<string, Dictionary<string, string>> LocalizedDescriptions = new Dictionary<string, Dictionary<string, string>>();
    }
  }
}
