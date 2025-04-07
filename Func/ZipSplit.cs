using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBuckUp.Func
{
    class ZipSplit
    {
        /// <summary>
        /// 自动判断
        /// </summary>
        /// <param name="inputFile"></param>
        /// <param name="outputPrefix"></param>
        /// <param name="chunkSizeBytes"></param>
        public static void SplitFile(string inputFile, string outputPrefix, long chunkSizeBytes)
        {
            if(chunkSizeBytes > int.MaxValue)
            {
                SplitFile_Big(inputFile, outputPrefix, chunkSizeBytes);
                return;
            }
            try
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
            }catch (Exception ex)
            {
                LogClass.AppendLog($"[严重][SplitFile]备份过程中出现错误: {ex.Message}\r\n{ex.StackTrace}", Color.Red);
            }
        }

        public static void SplitFile_Big(string inputFile, string outputPrefix, long chunkSizeBytes)
        {
            try
            {
                using (var inputStream = File.OpenRead(inputFile))
                {
                    const int bufferSize = 64*1024; // 使用64KB的缓冲区
                    var buffer = new byte[bufferSize];
                    int partNumber = 1;
                    long remainingInChunk = chunkSizeBytes;
                    FileStream outputStream = null;
                    string currentOutputFile = $"{outputPrefix}.{partNumber:D3}";
                    outputStream = File.Create(currentOutputFile);

                    try
                    {
                        while (true)
                        {
                            // 计算本次读取的最大字节数，不超过缓冲区大小或当前分块剩余空间
                            int bytesToRead = (int)Math.Min(buffer.Length, remainingInChunk);
                            int bytesRead = inputStream.Read(buffer, 0, bytesToRead);

                            if (bytesRead == 0)
                                break;

                            outputStream.Write(buffer, 0, bytesRead);
                            remainingInChunk -= bytesRead;

                            // 如果当前分块已满，关闭并创建新分块
                            if (remainingInChunk == 0)
                            {
                                outputStream.Dispose();
                                partNumber++;
                                currentOutputFile = $"{outputPrefix}.{partNumber:D3}";
                                outputStream = File.Create(currentOutputFile);
                                remainingInChunk = chunkSizeBytes;
                            }
                        }
                    }
                    finally
                    {
                        outputStream?.Dispose();
                    }

                    // 删除最后一个空文件（如果存在）
                    if (remainingInChunk == chunkSizeBytes && File.Exists(currentOutputFile))
                    {
                        File.Delete(currentOutputFile);
                    }
                }
            }
            catch (Exception ex)
            {
                LogClass.AppendLog($"[严重][SplitFile]备份过程中出现错误: {ex.Message}\r\n{ex.StackTrace}", Color.Red);
            }
        }
    }
}
