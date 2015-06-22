using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Framework.Security;

namespace Framework.File
{
    public class FileInfo
    {
        /// <summary>
        /// 返回文件大小
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        /// <returns>文件大小</returns>
        public static long GetFileSize(string FilePath)
        {
            try
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(FilePath);
                return fi.Length;
            }
            catch
            {
                return 0L;
            }
        }

        /// <summary>
        /// 返回文件创建时间
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        /// <returns>创建时间</returns>
        public static DateTime GetFileCreateTime(string FilePath)
        {
            try
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(FilePath);
                return fi.CreationTime;
            }
            catch
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// 获取文件最近修改时间
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        public static DateTime GetFileLastModifyTime(string FilePath)
        {
            try
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(FilePath);
                return fi.LastWriteTime;
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// 显示文件大小的文字显示
        /// </summary>
        /// <param name="FileSize">文件大小</param>
        /// <returns>显示文字</returns>
        public static string GetFileSizeStr(long FileSize)
        {
            if (FileSize > 1024 * 1024)
            {
                return ((Double)FileSize / 1024.0 / 1024.0).ToString("#.##") + " M";
            }
            else if (FileSize < 1024)
            {
                return FileSize + "字节";
            }
            else
            {
                return ((Double)FileSize / 1024.0).ToString("#") + " K";
            }
        }

        /// <summary>
        /// 显示我的空间中的文件大小的文字显示
        /// </summary>
        /// <param name="FileSize">文件大小</param>
        /// <returns>显示文字</returns>
        public static string GetSpaceSizeStr(int FileSize)
        {
            return ((Double)FileSize / 1024.0 / 1024.0).ToString("#.#") + " M";
        }

        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <param name="FileName">文件全路径或文件名</param>
        /// <returns>扩展名</returns>
        public static string GetFileExt(string FileName)
        {

            int startPos = FileName.LastIndexOf(".");
            if (startPos > -1)
            {
                string ext = FileName.Substring(startPos + 1, FileName.Length - startPos - 1);
                //可能原本就没有扩展名
                if (ext.IndexOf("\\") != -1)
                {
                    return "";
                }
                else
                {
                    return ext.ToLower();
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获取文件路径的文件名部分
        /// </summary>
        /// <param name="FilePath">路径全名</param>
        /// <returns>文件名</returns>
        public static string GetFileName(string FilePath)
        {

            int startPos = FilePath.LastIndexOf("\\");
            if (startPos > -1)
            {
                return FilePath.Substring(startPos + 1, FilePath.Length - startPos - 1);
            }
            else
            {
                return FilePath;
            }
        }

        /// <summary>
        /// 获取文件目录部分
        /// </summary>
        /// <param name="FilePath">路径全名</param>
        /// <returns>目录名</returns>
        public static string GetFileDirectory(string FilePath)
        {

            int startPos = FilePath.LastIndexOf("\\");
            if (startPos > -1)
            {
                return FilePath.Substring(0, startPos);
            }
            else
            {
                return FilePath;
            }
        }

        /// <summary>
        /// 获取文件MD5值
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        /// <returns></returns>
        public static string GetFileMD5(string FilePath)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read, 8192))
                {
                    System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                    byte[] hash = md5.ComputeHash(fs);
                    foreach (byte b in hash)
                    {
                        sb.Append(String.Format("{0:X1}", b));
                    }
                    fs.Close();
                }
                return sb.ToString();
            }
            catch
            {

                return "";

            }

        }

        /// <summary>
        /// 保存文件前过滤文件名中的特殊字符
        /// </summary>
        /// <param name="FileName">文件原名</param>
        /// <returns></returns>
        public static string FilterStr(string FileName)
        {
            return FileName.Replace("\\", "").Replace("/", "").Replace(":", "").Replace("*", "").Replace("?", "").Replace("<", "").Replace(">", "").Replace("|", "");
        }

    }
}
