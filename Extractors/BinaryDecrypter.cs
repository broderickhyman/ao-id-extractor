using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;

namespace ao_id_extractor.Extractors
{
  public class BinaryDecrypter
  {
    private static readonly byte[] Key = new byte[] { 48, 239, 114, 71, 66, 242, 4, 50 };
    private static readonly byte[] Iv = new byte[] { 14, 166, 220, 137, 219, 237, 220, 79 };

    public static string DecryptBinaryFile(string filePath)
    {
      var fs = File.OpenRead(filePath);
      var fileBuffer = new byte[fs.Length];
      fs.Read(fileBuffer, 0, fileBuffer.Length);

      var tDES = new DESCryptoServiceProvider
      {
        IV = Iv,
        Mode = CipherMode.CBC,
        Key = Key
      };
      var outBuffer = tDES.CreateDecryptor().TransformFinalBlock(fileBuffer, 0, fileBuffer.Length);

      var stream = new GZipStream(new MemoryStream(outBuffer), CompressionMode.Decompress);
      const int size = 4096;
      var buffer = new byte[size];
      using (var memory = new MemoryStream())
      {
        var count = 0;
        do
        {
          count = stream.Read(buffer, 0, size);
          if (count > 0)
          {
            memory.Write(buffer, 0, count);
          }
        }
        while (count > 0);

        var i = Array.IndexOf(memory.ToArray(), (byte)60);
        return System.Text.Encoding.UTF8.GetString(memory.ToArray(), i, memory.ToArray().Length - i);
      }
    }
  }
}
