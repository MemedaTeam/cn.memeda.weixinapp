using System;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.Configuration;

namespace Framework.Log
{
    /// <summary>
    /// ��־��¼������
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// ��¼������־
        /// </summary>
        /// <param name="message">�쳣����</param>
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
