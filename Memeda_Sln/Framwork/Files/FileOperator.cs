using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Framework.Log;

namespace Framework.File
{
    public class FileOperator
    {
        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <param name="fileName">文件路径（带文件名称）</param>
        /// <returns>返回文件内容</returns>
        public static string FileRead(string fileName)
        {
            if (System.IO.File.Exists(fileName))
            {
                StringBuilder builder = new StringBuilder();

                using (StreamReader reader = new StreamReader(fileName, Encoding.GetEncoding("UTF-8")))//txt文件，默认utf-8编码
                {
                    while (reader.Peek() > -1)
                    {
                        builder.Append(reader.ReadLine());
                    }
                    reader.Close();
                    reader.Dispose();

                    return builder.ToString().Trim();
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 检测是否是UTF-8
        /// </summary>
        /// <param name="text">内容</param>
        /// <returns></returns>
        public static int Utf8Probability(byte[] text)
        {
            int score = 0;
            int i, rawtextlen = 0;
            int goodbytes = 0, asciibytes = 0;

            // Maybe also use UTF8 Byte Order Mark:  EF BB BF
            // Check to see if characters fit into acceptable ranges
            rawtextlen = text.Length;
            for (i = 0; i < rawtextlen; i++)
            {
                if ((text[i] & (byte)0x7F) == text[i])
                {
                    // One byte
                    asciibytes++;
                    // Ignore ASCII, can throw off count
                }
                else if (i < rawtextlen - 2)
                {
                    int mRawInt0 = Convert.ToInt16(text[i]);
                    int mRawInt1 = Convert.ToInt16(text[i + 1]);
                    int mRawInt2 = Convert.ToInt16(text[i + 2]);

                    if (256 - 64 <= mRawInt0 && mRawInt0 <= 256 - 33 && // Two bytes
                     i + 1 < rawtextlen &&
                     256 - 128 <= mRawInt1 && mRawInt1 <= 256 - 65)
                    {
                        goodbytes += 2;
                        i++;
                    }
                    else if (256 - 32 <= mRawInt0 && mRawInt0 <= 256 - 17 && // Three bytes
                     i + 2 < rawtextlen &&
                     256 - 128 <= mRawInt1 && mRawInt1 <= 256 - 65 &&
                     256 - 128 <= mRawInt2 && mRawInt2 <= 256 - 65)
                    {
                        goodbytes += 3;
                        i += 2;
                    }
                }
            }

            if (asciibytes == rawtextlen)
            {
                return 0;
            }

            score = (int) (100*((float) goodbytes/(float) (rawtextlen - asciibytes)));

            // If not above 98, reduce to zero to prevent coincidental matches
            // Allows for some (few) bad formed sequences
            if (score > 98)
            {
                return score;
            }
            else if (score > 95 && goodbytes > 30)
            {
                return score;
            }
            else
            {
                return 0;
            }
        }
    }
}
