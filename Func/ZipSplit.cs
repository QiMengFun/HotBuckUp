using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBuckUp.Func
{
    class ZipSplit
    {
        public static void SplitFile(string inputFile, string outputPrefix, long chunkSizeBytes)
        {
            using (FileStream inputStream = File.OpenRead(inputFile))
            {
                byte[] buffer = new byte[chunkSizeBytes];
                int partNumber = 1;
                int bytesRead;

                while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string outputFile = $"{outputPrefix}.{partNumber:D3}";
                    File.WriteAllBytes(outputFile, buffer.Take(bytesRead).ToArray());
                    partNumber++;
                }
            }
        }
    }
}
