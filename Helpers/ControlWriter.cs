using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ao_id_extractor.Helpers
{
  public class ControlWriter : TextWriter
  {
    private readonly TextBox textbox;

    private static readonly StringBuilder buffer = new StringBuilder();

    public ControlWriter(TextBox textbox)
    {
      this.textbox = textbox;
    }

    public override void Write(char value)
    {
      buffer.Append(value);
      if (value == '\n')
      {
        WriteBuffer();
      }
    }

    public override void Write(string value)
    {
      buffer.Append(value);
      if (value.Contains(NewLine))
      {
        WriteBuffer();
      }
    }

    public override void WriteLine(string value)
    {
      buffer.Append(value);
      buffer.Append(NewLine);
      WriteBuffer();
    }

    private void WriteBuffer()
    {
      textbox.AppendText(buffer.ToString());
      buffer.Clear();
    }

    public override Encoding Encoding
    {
      get { return Encoding.ASCII; }
    }
  }
}
