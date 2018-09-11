using ao_id_extractor.Extractors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ao_id_extractor.Helpers
{
  public class MultiStream
  {
    public readonly StreamType[] StreamTypes;

    public MultiStream(StreamType[] streamTypes)
    {
      StreamTypes = streamTypes;
    }
  }

  public class StreamType
  {
    public Stream Stream;
    public ExportType ExportType;
  }
}
