using System;
using System.Collections.Generic;
using System.Text;
//using Ctrip.Common.Utility;


namespace Framework.Security
{
    /// <summary>
    /// Web通信过程的加解密类－包括Cookie和WebService的加密、解密
    /// </summary>
    public class CookieDESCryptographer
    {
        /// <summary>
        /// DES对称加密算法-加密- 该算法的Key 来自AppConfig的CookieDesKey配置项
        /// </summary>
        /// <param name="sourceData">明文</param>
        /// <returns>加密后的密文</returns>
        public static string EncryptDES(string sourceData)
        {
            if (string.IsNullOrEmpty(sourceData))
            {
                return string.Empty;
            }

            return Encrypt.EncryptDES(sourceData);
        }

        /// <summary>
        /// DES对称加密算法-解密--该算法的Key 来自AppConfig的CookieDesKey配置项
        /// </summary>
        /// <param name="sourceData">密文</param>
        /// <returns>解密后的明文</returns>
        public static string DecryptDES(string sourceData)
        {
            if (string.IsNullOrEmpty(sourceData))
            {
                return string.Empty;
            }

            return Encrypt.DecryptDES(sourceData);
        }
    }
}
