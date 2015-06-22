using System;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.Configuration;

namespace Framework.Log
{
    /// <summary>
    /// 日志记录帮助类
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="message">异常内容</param>
        public static void LogException(Exception message)
        {
            try
            {
                string LogFileDir = System.Configuration.ConfigurationManager.AppSettings["LogFile"];
                if (!System.IO.Directory.Exists(LogFileDir))
                {
                    System.IO.Directory.CreateDirectory(LogFileDir);
                }

                using (System.IO.StreamWriter fs = new System.IO.StreamWriter(LogFileDir + DateTime.Now.ToString("yyyyMMdd") + ".txt", true))
                {
                    fs.WriteLine(DateTime.Now + ":" + message.Message);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
            }

        }

    }
}
