using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

//using Ctrip.Common.Security;

namespace Framework.Security
{
    /// <summary>
    /// 游票存储数据安全类
    /// 对重点的数据进行数据加解密
    /// </summary>
    public static class TMPayDataSecurity
    {
        /// <summary>
        /// DES对称加密算法-加密
        /// </summary>
        /// <param name="sourceData">明文</param>
        /// <returns>加密后的密文</returns>
        //public static string EncryptDES(string sourceData)
        //{
        //    if (string.IsNullOrEmpty(sourceData))
        //    {
        //        return string.Empty;
        //    }

        //    Cryptographer cryptographer = new Cryptographer();
        //    return cryptographer.EncryptCommon(sourceData);
        //}

        /// <summary>
        /// DES对称加密算法-解密
        /// </summary>
        /// <param name="sourceData">密文</param>
        /// <returns>解密后的明文</returns>
        //public static string DecryptDES(string sourceData)
        //{
        //    if (string.IsNullOrEmpty(sourceData))
        //    {
        //        return string.Empty;
        //    }

        //    Cryptographer cryptographer = new Cryptographer();
        //    return cryptographer.DecryptCommon(sourceData);
        //}

        /// <summary>
        /// 对数据进行MD5散列
        /// </summary>
        /// <param name="sourceData">加密前的明文</param>
        /// <returns>加密后的密文</returns>
        public static string DataHashByMD5(string sourceData)
        {
            if (String.IsNullOrEmpty(sourceData))
            {
                throw new ArgumentNullException("The param [sourceData] cann't be null.");
            }

            //对数据进行一次混合
            sourceData = MixData(sourceData);

            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(sourceData));

            //转换为16进制字符串
            StringBuilder builder = new StringBuilder();
            foreach (byte currentByte in md5Hasher.Hash)
            {

                builder.Append(currentByte.ToString("x2"));
            }

            return builder.ToString();
        }

        /// <summary>
        /// 混合数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns>结果</returns>
        private static string MixData(string data)
        {
            //混合规则：原数据 ＋ 奇数位数据 ＋ 偶数位数据
            //原数据如果为：12345，则混合后的数据为：12345 ＋ 135 ＋ 24
            int strLen = data.Length;

            string oddData = "", evenData = string.Empty;
            for (int i = 0; i < strLen; i++)
            {
                if (!Convert.ToBoolean(i % 2))
                {
                    //奇数位
                    oddData += data.Substring(i, 1);
                }
                else
                {
                    //偶数位
                    evenData += data.Substring(i, 1);
                }
            }
            return data + oddData + evenData;
        }
    }
}
