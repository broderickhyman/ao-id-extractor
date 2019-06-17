using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ao_id_extractor.Helpers
{
  public static class JSONHelper
  {
    public static string ToJSON(this object obj)
    {
      return JsonConvert.SerializeObject(obj, Formatting.Indented);
    }

    public static string ToJSON(this object obj, int recursionDepth)
    {
      return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
      {
        MaxDepth = recursionDepth
      });
    }

    public static void ForEach<T>(this IEnumerable<T> ie, Action<T> action)
    {
      foreach (var i in ie)
      {
        action(i);
      }
    }
  }
}
