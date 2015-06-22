using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Framework.Files
{
    public class FileZip
    {
        /// <summary>
        /// 压缩文件流并存为文件
        /// </summary>
        /// <param name="sourceStream">源文件流</param>
        /// <param name="destinationFile">目标文件</param>
        public static void SaveCompressStream(MemoryStream sourceStream, string destinationFile)
        {
            sourceStream.Position = 0;
            byte[] buffer = new byte[sourceStream.Length];
            int checkCounter = sourceStream.Read(buffer, 0, buffer.Length);
            if (checkCounter != buffer.Length) throw new ApplicationException();
            using (FileStream destinationStream = new FileStream(destinationFile, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (GZipStream compressedStream = new GZipStream(destinationStream, CompressionMode.Compress, true))
                {
                    compressedStream.Write(buffer, 0, buffer.Length);
                }
            }
        }

        /// <summary>
        /// 读取文件流
        /// </summary>
        /// <param name="sourceFile">源文件</param>
        /// <returns>文件流</returns>
        public static Stream ReadCompressStream(string sourceFile)
        {
            System.IO.MemoryStream xmlStream = new System.IO.MemoryStream();
            if (!System.IO.File.Exists(sourceFile)) throw new FileNotFoundException();
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open))
            {
                byte[] quartetBuffer = new byte[4];
                int position = (int)sourceStream.Length - 4;
                sourceStream.Position = position;
                sourceStream.Read(quartetBuffer, 0, 4);
                sourceStream.Position = 0;
                int checkLength = BitConverter.ToInt32(quartetBuffer, 0);
                byte[] buffer = new byte[checkLength + 100];
                using (GZipStream decompressedStream = new GZipStream(sourceStream, CompressionMode.Decompress, true))
                {
                    int total = 0;
                    for (int offset = 0; ; )
                    {
                        int bytesRead = decompressedStream.Read(buffer, offset, 100);
                        if (bytesRead == 0) break;
                        offset += bytesRead;
                        total += bytesRead;
                    }
                    xmlStream.Write(buffer, 0, total);
                }
            }
            xmlStream.Position = 0;
            return xmlStream;
        }


        /// <summary>
        /// 压缩文件流并存为文件(Deflate)
        /// </summary>
        /// <param name="sourceStream">源文件流</param>
        /// <param name="destinationFile">目标文件</param>
        public static void SaveCompressStreamDeflate(MemoryStream sourceStream, string destinationFile)
        {
            sourceStream.Position = 0;
            byte[] buffer = new byte[sourceStream.Length];
            int checkCounter = sourceStream.Read(buffer, 0, buffer.Length);
            if (checkCounter != buffer.Length) throw new ApplicationException();
            using (FileStream destinationStream = new FileStream(destinationFile, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (DeflateStream compressedStream = new DeflateStream(destinationStream, CompressionMode.Compress, true))
                {
                    compressedStream.Write(buffer, 0, buffer.Length);
                }
            }
        }

        /// <summary>
        /// 读取文件流(Deflate)
        /// </summary>
        /// <param name="sourceFile">源文件</param>
        /// <returns>文件流</returns>
        public static Stream ReadCompressStreamDeflate(string sourceFile)
        {
            System.IO.MemoryStream xmlStream = new System.IO.MemoryStream();
            if (!System.IO.File.Exists(sourceFile)) throw new FileNotFoundException();
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open))
            {
                byte[] quartetBuffer = new byte[4];
                int position = (int)sourceStream.Length - 4;
                sourceStream.Position = position;
                sourceStream.Read(quartetBuffer, 0, 4);
                sourceStream.Position = 0;
                int checkLength = BitConverter.ToInt32(quartetBuffer, 0);
                byte[] buffer = new byte[checkLength + 100];
                using (DeflateStream decompressedStream = new DeflateStream(sourceStream, CompressionMode.Decompress, true))
                {
                    int total = 0;
                    for (int offset = 0; ; )
                    {
                        int bytesRead = decompressedStream.Read(buffer, offset, 100);
                        if (bytesRead == 0) break;
                        offset += bytesRead;
                        total += bytesRead;
                    }
                    xmlStream.Write(buffer, 0, total);
                }
            }
            xmlStream.Position = 0;
            return xmlStream;
        }


        /// <summary>
        /// 压缩文件(gzip)
        /// </summary>
        /// <param name="sourceFile">源文件路径</param>
        /// <param name="destinationFile">目标文件路径</param>
        public static void CompressFile(string sourceFile, string destinationFile)
        {
            if (!System.IO.File.Exists(sourceFile)) throw new FileNotFoundException();
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                byte[] buffer = new byte[sourceStream.Length];
                int checkCounter = sourceStream.Read(buffer, 0, buffer.Length);
                if (checkCounter != buffer.Length) throw new ApplicationException();
                using (FileStream destinationStream = new FileStream(destinationFile, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (GZipStream compressedStream = new GZipStream(destinationStream, CompressionMode.Compress, true))
                    {
                        compressedStream.Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }
        /// <summary>
        /// 解压缩文件(gzip)
        /// </summary>
        /// <param name="sourceFile">源文件路径</param>
        /// <param name="destinationFile">压缩文件路径</param>
        public static void DecompressFile(string sourceFile, string destinationFile)
        {
            if (!System.IO.File.Exists(sourceFile)) throw new FileNotFoundException();
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open))
            {
                byte[] quartetBuffer = new byte[4];
                int position = (int)sourceStream.Length - 4;
                sourceStream.Position = position;
                sourceStream.Read(quartetBuffer, 0, 4);
                sourceStream.Position = 0;
                int checkLength = BitConverter.ToInt32(quartetBuffer, 0);
                byte[] buffer = new byte[checkLength + 100];
                using (GZipStream decompressedStream = new GZipStream(sourceStream, CompressionMode.Decompress, true))
                {
                    int total = 0;
                    for (int offset = 0; ; )
                    {
                        int bytesRead = decompressedStream.Read(buffer, offset, 100);
                        if (bytesRead == 0) break;
                        offset += bytesRead;
                        total += bytesRead;
                    }
                    using (FileStream destinationStream = new FileStream(destinationFile, FileMode.Create))
                    {
                        destinationStream.Write(buffer, 0, total);
                        destinationStream.Flush();
                    }
                }
            }
        }

        /// <summary>
        /// 压缩文件(Deflate)
        /// </summary>
        /// <param name="sourceFile">源文件路径</param>
        /// <param name="destinationFile">目标文件路径</param>
        public static void CompressFileDeflate(string sourceFile, string destinationFile)
        {
            if (!System.IO.File.Exists(sourceFile)) throw new FileNotFoundException();
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                byte[] buffer = new byte[sourceStream.Length];
                int checkCounter = sourceStream.Read(buffer, 0, buffer.Length);
                if (checkCounter != buffer.Length) throw new ApplicationException();
                using (FileStream destinationStream = new FileStream(destinationFile, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (DeflateStream compressedStream = new DeflateStream(destinationStream, CompressionMode.Compress, true))
                    {
                        compressedStream.Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }
        /// <summary>
        /// 解压缩文件(Deflate)
        /// </summary>
        /// <param name="sourceFile">源文件路径</param>
        /// <param name="destinationFile">压缩文件路径</param>
        public static void DecompressFileDeflate(string sourceFile, string destinationFile)
        {
            if (!System.IO.File.Exists(sourceFile)) throw new FileNotFoundException();
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open))
            {
                byte[] quartetBuffer = new byte[4];
                int position = (int)sourceStream.Length - 4;
                sourceStream.Position = position;
                sourceStream.Read(quartetBuffer, 0, 4);
                sourceStream.Position = 0;
                int checkLength = BitConverter.ToInt32(quartetBuffer, 0);
                byte[] buffer = new byte[checkLength + 100];
                using (DeflateStream decompressedStream = new DeflateStream(sourceStream, CompressionMode.Decompress, true))
                {
                    int total = 0;
                    for (int offset = 0; ; )
                    {
                        int bytesRead = decompressedStream.Read(buffer, offset, 100);
                        if (bytesRead == 0) break;
                        offset += bytesRead;
                        total += bytesRead;
                    }
                    using (FileStream destinationStream = new FileStream(destinationFile, FileMode.Create))
                    {
                        destinationStream.Write(buffer, 0, total);
                        destinationStream.Flush();
                    }
                }
            }
        }

        /// <summary>
        /// GZipStream压缩字符串
        /// </summary>
        /// <param name="uncompressedString"></param>
        /// <returns></returns>
        public static string GZipCompress(string uncompressedString)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(uncompressedString);
            MemoryStream msReturn;

            using (MemoryStream msTemp = new MemoryStream())
            {
                using (GZipStream gz = new GZipStream(msTemp, CompressionMode.Compress, true))
                {
                    //从指定的字节数组中将压缩的字节写入基础流
                    gz.Write(buffer, 0, buffer.Length);
                    //关闭当前流并释放与之关联的所有资源
                    gz.Close();
                    //基于字节数组的指定区域
                    msReturn = new MemoryStream(msTemp.GetBuffer(), 0, (int)msTemp.Length);
                }
            }
            //将8位无符号整数数组转换为他的等效System.String表示形式
            return Convert.ToBase64String(msReturn.ToArray());
        }

        /// <summary>
        /// GZipStream解压字符串
        /// </summary>
        /// <param name="compressedString"></param>
        /// <returns></returns>
        public static string GZipDecompress(string compressedString)
        {
            //将指定的System.String转换成等效的8位无符号整数数组
            byte[] bytes = Convert.FromBase64String(compressedString);
            MemoryStream memoryStream = new MemoryStream(bytes);
            byte[] buffer = new byte[2048];
            int length = 0;
            //使用指定的System.IO.Compression.CompressionMode值初始化System.IO.Compression.GZipStream类的新实例
            using (GZipStream gz = new GZipStream(memoryStream, CompressionMode.Decompress))
            {
                using (MemoryStream msTemp = new MemoryStream())
                {
                    //gz.Read()将若干解压缩的字节读入指定的字节数组
                    while ((length = gz.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        //使用从缓冲区读取的数据将字节块写入当前流
                        msTemp.Write(buffer, 0, length);
                    }
                    //在派生类中重写时，将指定的字节数组中的所有字节解码为一个字符串
                    return System.Text.Encoding.UTF8.GetString(msTemp.ToArray());
                }
            }
        }
    }
}
